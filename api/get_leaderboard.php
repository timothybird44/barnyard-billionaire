<?php
// get_leaderboard.php - Endpoint to retrieve leaderboard data

require_once 'config.php';
require_once 'connect.php';
require_once 'functions.php';

$leaderboard_data = getLeaderboard($conn);
echo json_encode($leaderboard_data);
$conn->close();
?>