<?php
// config.php - Store configuration settings

// **Database Configuration**
define('DB_HOST', getenv('DB_HOST'));       // e.g., 'localhost' or the specific hostname provided by GoDaddy
define('DB_USER', getenv('DB_USER'));       // e.g., 'your_username' (provided by GoDaddy)
define('DB_PASS', getenv('DB_PASSWORD'));  // e.g., 'your_password' (provided by GoDaddy)
define('DB_NAME', getenv('DB_NAME'));         // e.g., 'your_database_name' (provided by GoDaddy)

// **Important Security Setting: Secret Key**
define('SECRET_KEY', getenv('SECRET_KEY'));

?>