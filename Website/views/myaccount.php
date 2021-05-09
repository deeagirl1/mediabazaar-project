<?php
require('phpScripts/isSessionValid.php');
?>
<section>
<link rel="stylesheet" href="css/myaccount.css">
<div class="container">

<form action="phpScripts/changePassword.php" method="post">

    <h1>Change password</h1>
    <hr>
    <label for="psw"><b>Current Password</b></label>
    <input type="password" placeholder="Current Password" name="psw" id="psw" required>
  
    <label for="new-psw"><b>New Password</b></label>
    <input type="password" placeholder="New Password" name="new-psw" id="new-psw" required>

    <label for="psw-repeat"><b>Repeat New Password</b></label>
    <input type="password" placeholder="Repeat New Password" name="psw-repeat" id="psw-repeat" required>
  

    <button type="Change" class="passwordbtn">Change your password</button>
</form>
<hr>


<?php
require('phpScripts/showReadonlyAccountDetails.php');
?>

<h1>Change personal details</h1><br>
<br>
<?php
require('phpScripts/showAccountDetails.php');
?>

<?php
require('phpScripts/showShiftPreference.php');
?>

</section>