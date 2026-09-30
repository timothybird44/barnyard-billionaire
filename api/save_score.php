<?php
// save_score.php - Endpoint to save a player's score with authentication

require_once 'config.php'; // Include database and secret key
require_once 'connect.php'; // Include database connection
require_once 'functions.php'; // Include helper functions (savePlayerData, verifyToken)

// Get raw posted data
$json = file_get_contents('php://input');
$data = json_decode($json, true);

// Check if data is valid
if (!$data || !isset($data['playerName'], $data['playerWallet'], $data['score'], $data['scoreString'], $data['token'])) {
    http_response_code(400); // Bad Request
    echo json_encode(array("error" => "Invalid data. playerName, playerWallet, score, scoreString, and token are required."));
    $conn->close();
    exit;
}

$playerName = $data['playerName'];
$playerWallet = $data['playerWallet'];
$score = $data['score'];
$scoreString = $data['scoreString'];
$receivedToken = $data['token'];

// Save the player data using the function
if (savePlayerData($conn, $playerName, $playerWallet, $score, $scoreString, $receivedToken)) {
    http_response_code(201); // Created
    echo json_encode(array("message" => "Score saved successfully!"));
} else {
    http_response_code(403); // Or 500, depending on the error
    echo json_encode(array("error" => "Failed to save score. Invalid token or database error."));
}

$conn->close();
?>