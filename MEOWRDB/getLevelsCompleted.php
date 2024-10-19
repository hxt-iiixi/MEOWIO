<?php
include 'config.php';

if (isset($_POST['user_name'])) {
    $user_name = $_POST['user_name'];

    // Query to get the number of levels completed for the user
    $query = "SELECT user_level FROM users WHERE user_name = '$user_name'";
    $result = $conn->query($query);

    if ($result->num_rows > 0) {
        $row = $result->fetch_assoc();
        echo $row['user_level']; // bibigay mga levels na tapos
    } else {
        echo "1"; 
    }
} else {
    echo "0"; // 
}
?>
