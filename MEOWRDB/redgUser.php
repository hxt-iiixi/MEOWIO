<?php

$servername = "localhost";
$username = "root";
$password = "";
$dbname = "cats";

//variables from users
$signInUser = $_POST["signInUser"];
$signInPass = $_POST["signInPass"];
$signInEmail = $_POST["signInEmail"];


// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
echo "connected successfully, showing users". "<br>";

$sql = "SELECT user_name FROM users WHERE user_name = '" . $signInUser . "'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    //name already taken
    echo "username already taken";
  
} else {
  echo "Creating new user";
  //insert info to database
  $sql = "INSERT INTO users (user_name, user_password, user_email) VALUES ('" . $signInUser . "', '". $signInPass . "', '". $signInEmail ."')";
    if ($conn->query($sql) === TRUE) {
        echo "New user created successfully";
      } else {
        echo "Error: " . $sql2 . "<br>" . $conn->error;
      }
    }

$user_name = "\$5\$rounds=2000\$" . 
$conn->close();
?>