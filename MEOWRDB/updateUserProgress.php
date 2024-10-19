<?php
include 'config.php';

if (isset($_POST['user_name']) && isset($_POST['coins']) && isset($_POST['level'])) {
    $username = $_POST['user_name'];
    $coins = $_POST['coins'];
    $level = $_POST['level'];

    // Update the user's coins and level progress
    $sql = "UPDATE users SET user_coins = user_coins + ?, user_level = ? WHERE user_name = ?";
    $stmt = $conn->prepare($sql);
    $stmt->bind_param("iis", $coins, $level, $username);

    if ($stmt->execute()) {
        echo "Progress updated successfully!";
    } else {
        echo "Error: " . $conn->error;
    }
    $stmt->close();
} else {
    echo "Missing data. User Name, Coins, or Level is empty.";
}

$conn->close();
?>
