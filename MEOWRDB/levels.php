<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "cats"; // Replace with your actual database name

// Get the data from the Unity request
$user_name = $_POST['user_name'];  // Get the user_name from Unity
$coins = $_POST['coins']; // Get the coins from Unity

// Debugging output to verify data received
print_r($_POST); // Print incoming POST data for debugging

// Check if data is missing
if (empty($user_name) || empty($coins)) {
    die("Missing data. User Name or Coins is empty.");
}

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Database connection failed: " . $conn->connect_error);
}

// Prepare and execute the update query using user_name
$sql = "UPDATE users SET user_coins = user_coins + ? WHERE user_name = ?";
$stmt = $conn->prepare($sql);

if (!$stmt) {
    die("Statement preparation failed: " . $conn->error);
}

// Bind coins and user_name
$stmt->bind_param("is", $coins, $user_name);

// Debug output before execution
echo "Executing SQL: UPDATE users SET user_coins = user_coins + $coins WHERE user_name = $user_name";

if ($stmt->execute()) {
    echo "User coins updated successfully";
} else {
    echo "Error updating user coins: " . $stmt->error;
}

$stmt->close();
$conn->close();
?>
