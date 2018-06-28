<?php

require_once('connect.php');

?>

<br />

 <form name='downloader' method='POST' action='./downloadLogins.php'>

<input type='submit' name='download' value='Download Excel'/>
</form>

<?php



$getLogins = "SELECT login, name, lastname FROM login";

  $getLogins = mysqli_query($database,$getLogins);

  $nr = 1;

   if(mysqli_affected_rows($database) != 0)

   {

       echo "<table style='text-align:left;'><tr><th>nr</th><th>Login</th><th>Naam</th><th>Achternaam</th></tr>";

       while($result = mysqli_fetch_array($getLogins))

       {

       



       echo "<tr><td>$nr</td><td>".$result['login']."</td><td>".$result['name']."</td><td>".$result['lastname']."</td></tr>";

       $nr++;

       }

       echo "</table>";

   }



?>



<?php

?>