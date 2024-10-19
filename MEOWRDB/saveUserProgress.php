<?php
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "cats"; // Replace with your actual database name

// Get the data from the Unity request
$user_name = $_POST['user_name'];  // Get the user_name from Unity
$levelsUnlocked = $_POST['levelsUnlocked'];
$currentLevelIndex = $_POST['currentLevelIndex'];

// Check if data is missing
if (empty($user_name) || empty($levelsUnlocked) || empty($currentLevelIndex)) {
    die("Missing data. User Name, Levels Unlocked, or Current Level Index is empty.");
}

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Database connection failed: " . $conn->connect_error);
}

// Prepare and execute the update query using user_name
$sql = "UPDATE users SET levels_unlocked = ?, current_level_index = ? WHERE user_name = ?";
$stmt = $conn->prepare($sql);

if (!$stmt) {
    die("Statement preparation failed: " . $conn->error);
}

// Bind parameters
$stmt->bind_param("iis", $levelsUnlocked, $currentLevelIndex, $user_name);

if ($stmt->execute()) {
    echo "User progress updated successfully";
} else {
    echo "Error updating user progress: " . $stmt->error;
}

$stmt->close();
$conn->close();
?>
