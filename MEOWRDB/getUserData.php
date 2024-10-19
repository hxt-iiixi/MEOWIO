<?php
include 'config.php';

if (isset($_POST['user_name'])) {
    $user_name = $_POST['user_name'];

    // Query to retrieve user data
    $query = "SELECT user_coins, user_level FROM users WHERE user_name = '$user_name'";
    $result = $conn->query($query);

    if ($result->num_rows > 0) {
        $row = $result->fetch_assoc();
        echo json_encode(array("coins" => $row['user_coins'], "level" => $row['user_level']));
    } else {
        echo "User not found.";
    }
} else {
    echo "Missing user name.";
}
?>
