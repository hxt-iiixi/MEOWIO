<?php
// Include database configuration file
include 'config.php'; // Ensure this file contains your database connection settings

// Get user input
$user_name = $_POST['user_name'] ?? '';
$level = $_POST['level'] ?? 1; // Default to 0 if not provided

// Validate inputs
if (empty($user_name) || $level < 1) {
    echo "Invalid input. User name or level is missing.";
    exit; // Exit if validation fails
}

// Prepare SQL statement to update user progress
try {
    $stmt = $pdo->prepare("UPDATE users SET user_level = :level WHERE user_name = :username");
    $stmt->bindParam(':level', $level);
    $stmt->bindParam(':username', $user_name);
    
    // Execute the statement
    if ($stmt->execute()) {
        echo "User progress updated successfully.";
    } else {
        echo "Failed to update user progress.";
    }
} catch (PDOException $e) {
    // Handle any errors
    echo "Error: " . $e->getMessage();
}
?>
