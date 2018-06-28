<?php
   header("Access-Control-Allow-Origin: *");
require_once("connect.php");



if(isset($_GET['login']))

{

   

   $loginselect =  "SELECT login, Phase, name, lastname FROM login WHERE login = '".$_GET['login']."'";

   $loginselect = mysqli_query($database,$loginselect);

   if(mysqli_affected_rows($database) != 0)
   {

       $result = mysqli_fetch_array($loginselect);



       echo "".$result['login'].";".$result['Phase'].";".$result['name'].";".$result['lastname']."";

   }
   else
   {
        $loginInsert = "INSERT INTO login (login) VALUES ('".$_GET['login']."')";
   
        $result = mysqli_query($database, $loginInsert);
        
        if($result)
        {
            echo "".$_GET['login'].";0";
        }

   }

}



if(isset($_GET['savePhase']))

{

  $updatePhase = "UPDATE login SET Phase=".$_GET['savePhase'].", TimePhase".$_GET['savePhase']." = ".$_GET['timePhase']." WHERE login = '".$_GET['logincode']."'";

  $updatePhase = mysqli_query($database,$updatePhase);

}



if(isset($_GET['saveName']) && isset($_GET['nameLogin']) && isset($_GET['lastName']) && isset($_GET['continue']))

{
  if($_GET['continue'] == "false")
  {
      $saveName = "UPDATE login SET name='".$_GET['saveName']."', lastname='".$_GET['lastName']."' WHERE login='".$_GET['nameLogin']."'";
  }
  else
  {
      $resetLogin = "DELETE FROM login WHERE login='".$_GET['nameLogin']."'";
      $saveName = "INSERT INTO login  (login, name, lastname)  VALUES ('".$_GET['nameLogin']."','".$_GET['saveName']."','".$_GET['lastName']."')";
  }
  
  if($resetLogin != null)
  {
     mysqli_query($database,$resetLogin);
  }
                       
  $saveName = mysqli_query($database,$saveName);

}



if(isset($_GET['completePhase']))

{

  $getEndPhase = "SELECT login,name,lastname,TimePhase1,TimePhase2,TimePhase3,TimePhase4,TimePhase5,TimePhase6,TimePhase7 FROM login WHERE login='".$_GET['completePhase']."'";

  $getEndPhase = mysqli_query($database,$getEndPhase);

   if(mysqli_affected_rows($database) != 0)

   {

       $result = mysqli_fetch_array($getEndPhase);



       echo "".$result['login'].";".$result['name'].";".$result['lastname'].";".$result['TimePhase1'].";".$result['TimePhase2'].";".$result['TimePhase3'].";".$result['TimePhase4'].";".$result['TimePhase5'].";".$result['TimePhase6'].";".$result['TimePhase7']."";

   }

}



?>

