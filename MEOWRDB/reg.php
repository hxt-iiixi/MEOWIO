<?php
$con = mysqli_connect('localhost', 'root', '', 'cats');

if ($mysqli -> connect_errno) 
    {
        echo "Failed to connect to MySQL: " . $mysqli -> connect_error;
        exit();
}
$signInUser = $_POST["signInUser"];
$signInPass = $_POST["signInPass"];
$signInEmail = $_POST["signInEmail"];

$namecheckquery = "SELECT user_name FROM users WHERE user_name = '" . $signInUser . "'";
$namecheck = mysqli_query($con, $namecheckquery) or die ("2: name check fail");

if (mysqli_num_rows($namecheck)> 0){
    echo "3: name already taken"
    exit();
}
$user_name = "\$5\$rounds=2000\$" .  $signInUser
$user_pass = "\$5\$rounds=2000\$" . $signInPass
$user_email = "\$5\$rounds=2000\$" . $signInEmail
$insertquery = "INSERT INTO users (user_name, user_password, user_email) VALUES ('" . $signInUser . "', '". $signInPass . "', '". $signInEmail ."')";
mysqli_query($con, $namecheckquery) or die ("4: query fail");

echo("0");
?>
