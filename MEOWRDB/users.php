<?php
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
echo "connected successfully, showing users". "<br>";

$sql = "SELECT user_id, user_name FROM users";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    echo "user_id: " . $row["user_id"]. " - user_name: " . $row["user_name"]. "<br>";
  }
} else {
  echo "connected 0";
}
$conn->close();
?>