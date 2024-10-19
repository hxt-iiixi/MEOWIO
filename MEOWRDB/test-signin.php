<?php
// Database connection details
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "cats";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Variables from POST request (sent by Unity)
if (isset($_POST['signInUser']) && isset($_POST['signInPass']) && isset($_POST['signInEmail'])) {
    $signInUser = $_POST["signInUser"];
    $signInPass = $_POST["signInPass"];  
    $signInEmail = $_POST["signInEmail"];

    // Check if username already exists
    $sql = "SELECT user_name FROM users WHERE user_name = '$signInUser'";
    $result = $conn->query($sql);

    if ($result->num_rows > 0) {
        // Username already exists
        echo "Username already taken";
    } else {
        // Insert new user
        $sql = "INSERT INTO users (user_name, user_password, user_email) VALUES ('$signInUser', '$signInPass', '$signInEmail')";
        if ($conn->query($sql) === TRUE) {
            echo "New user created successfully";
        } else {
            echo "Error: " . $conn->error;
        }
    }
} else {
    echo "Incomplete data provided";
}

$conn->close();
?>
