<?php
setlocale(LC_ALL, 'nl_NL');
$database = mysqli_connect( "localhost", "time2_gameuser", "i1XtoB7Q" ) or die('Er ging iets mis: ' . mysqli_error() );
mysqli_select_db($database,"time2_game");
?>
