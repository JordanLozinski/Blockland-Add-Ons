package Chatbot
{
    function clientCmdChatMessage(%a, %b, %c, %fmsg, %cp, %name, %cs, %msg)
    {
		  parent::clientCmdChatMessage(%a, %b, %c, %fmsg, %cp, %name, %cs, %msg);
		  if(%name $= $Pref::Player::NetName){
			  	if(getWord(%msg,0) $= $chatbot || getWord(%msg,0) $= ($chatbot @ ","))
			  	{
					
					if(strLwr(getWord(%msg,1)) $= "eval")
					{
						eval(getSubStr(%msg,strStr(%msg,"eval")+5,1000));
					}
					else if(strLwr(getWord(%msg,1)) $= "activate")
					{
						if(strLwr(getWord(%msg,2) $= "yourself"))
						{
							exec("Add-Ons/Client_Kirbot/client.cs");
							sendAMessage("RETURN: executed " @ $chatbot);
						}
						else
						{
							sendAMessage("RETURN: executed " @ getWord(%msg,2));
							exec(strLwr(getWord(%msg,2)));
						}
					}

					else if(strLwr(getWord(%msg,1)) $= "rename")
					{
						$chatbot = strLwr(getWord(%msg,2));				
						sendAMessage("RETURN: name is now" SPC $chatbot);
					}
					else if(strLwr(getWord(%msg,1)) $= "test")
					{
						sendAMessage("RETURN: functioning");
					}

					else if(strLwr(getWord(%msg,1)) $= "mute")
					{
						$mute = true;
						echo("Muted.");
					}
					else if(strLwr(getWord(%msg,1)) $= "unmute")
					{
						$mute = false;
						echo("Unmuted.");
					}
					else if(strLwr(getWord(%msg,1)) $= "ping")
					{
						%s = getPingAvg();
						schedule(3000,0,sendAMessage,%s);
					}
					else if(strLwr(getWord(%msg,1)) $= "copy")
					{
						parrot(getSubStr(%msg,strStr(%msg,"copy")+5,1000));
					}
				}
			}
		}
	function sendAMessage(%str)
	{
		if($mute == false)
		{
			commandToServer('messageSent',%str);
		}else
		{
			echo(%str);
		}
	}
};
$chatbot="Kirbot";
activatePackage("Chatbot");