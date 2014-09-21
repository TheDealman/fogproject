<?php
@session_set_cookie_params(0);
@session_start();
(!isset($_SESSION['locale']) ? $_SESSION['locale'] = 'en_US' : null);
putenv("LC_ALL=".$_SESSION['locale']);
setlocale(LC_ALL, $_SESSION['locale']);
bindtextdomain("messages", "../languages");
textdomain("messages");
ob_start('ob_gzhandler');
print '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">';
print "\n<html>";
print "\n\t<head>";
print "\n\t\t".'<link rel="stylesheet" type="text/css" href="./css/static.css" />';
print "\n\t</head>";
print "\n<body>";
print "\n\t".'<div class="main">';
print "\n\t\t<h3>"._('FOG General Help').'</h3>';
print "\n\t\t<h5>"._('Description').'</h5>';
print "\n\t\t<p>";
print "\n\t\t\t".base64_decode($_REQUEST['data']);
print "\n\t\t</p>";
print "\n\t</div>";
print "\n</body>";
print "\n</html>";
@session_write_close();
ob_end_flush();
