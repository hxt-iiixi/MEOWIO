<?php
include 'config.php';

if (isset($_POST['user_name']) && isset($_POST['coins']) && isset($_POST['level'])) {
    $user_name = $_POST['user_name'];
    $coins = intval($_POST['coins']);
    $level = intval($_POST['level']);

    if (!empty($user_name)) {
        // Query to check if the user exists
        $checkUserQuery = "SELECT * FROM users WHERE user_name = '$user_name'";
        $userResult = $conn->query($checkUserQuery);

        if ($userResult->num_rows > 0) {
            // Update the user's coins and level
            $updateQuery = "UPDATE users SET user_coins = user_coins + $coins, user_level = $level WHERE user_name = '$user_name'";
            if ($conn->query($updateQuery) === TRUE) {
                echo "Coins and level updated successfully.";
            } else {
                echo "Error updating record: " . $conn->error;
            }
        } else {
            echo "User not found.";
        }
    } else {
        echo "Missing data. User Name is empty.";
    }
} else {
    echo "Missing data. User Name, Coins, or Level not found.";
}
?>
