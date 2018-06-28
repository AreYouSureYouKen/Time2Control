<?php

require_once('connect.php');

$outstr = NULL;



header("Content-Type: text/csv");

header("Content-Disposition: attachment;Filename=Loginnamen.csv");





// Query database to get column names  

$querystring = "SELECT login,Phase,name,lastname,TimePhase1,TimePhase2,TimePhase3,TimePhase4,TimePhase5,TimePhase6,TimePhase7 FROM login";

$result = mysqli_query($database,$querystring);

// Write column names

$outstr .= "Login;Phase;Naam;Achternaam;TimePhase1;TimePhase2;TimePhase3;TimePhase4;TimePhase5;TimePhase6;TimePhase7 \n";

// Write data rows

if(mysqli_affected_rows($database) != 0)

   {

while ($row = mysqli_fetch_array($result)) {

    $outstr.= $row['login'].";".$row['Phase'].";".$row['name'].";".$row['lastname'].";".$row['TimePhase1'].";".$row['TimePhase2'].";".$row['TimePhase3'].";".$row['TimePhase4'].";".$row['TimePhase5'].";".$row['TimePhase6'].";". $row['TimePhase7']." \n";

}

}

else

{

  $outstr.= "failed";

}

echo $outstr;

?>