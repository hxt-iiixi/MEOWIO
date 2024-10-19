<?php

$servername = "localhost";
$username = "root";
$password = "";
$dbname = "cats";

//variables from users
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];


// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
echo "connected successfully, showing users". "<br>";

$sql = "SELECT user_password FROM users WHERE user_name = '" . $loginUser . "'" ;
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    if($row["user_password"] == $loginPass){
        echo "username and password correct";
       

        


    }
    else {
        echo "wrong username/password";
    }
  }
} else {
  echo "user not living";
}
$conn->close();
?>