package kaphonaits
{
	function clientCmdChatMessage(%a, %b, %c, %fmsg, %cp, %name, %cs, %msg)
    {
    	if(%msg $= "*puffs cigar*"){
			transformation();
		}
		parent::clientCmdChatMessage(%a, %b, %c, %fmsg, %cp, %name, %cs, %msg);
	}

	function transformation()
	{
		AvatarGui.ClickFav(9);
		Avatar_Done();
		commandToServer('hate');
		schedule(5000,0,transformationCancel);

	}

	function transformationCancel()
	{
		AvatarGui.ClickFav(2);
		Avatar_Done();
	}
};

activatePackage("kaphonaits");