<?php
include 'config.php';

if (isset($_POST['user_name'])) {
    $username = $_POST['user_name'];

    // Fetch the user's progress
    $sql = "SELECT user_coins, user_level FROM users WHERE user_name = ?";
    $stmt = $conn->prepare($sql);
    $stmt->bind_param("s", $username);
    $stmt->execute();
    $stmt->bind_result($user_coins, $user_level);

    if ($stmt->fetch()) {
        echo json_encode(array("user_coins" => $user_coins, "user_level" => $user_level));
    } else {
        echo "User not found.";
    }
    $stmt->close();
} else {
    echo "Missing data. User Name is empty.";
}

$conn->close();
?>
