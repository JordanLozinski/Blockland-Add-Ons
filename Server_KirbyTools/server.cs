package serverTools{
	
	function getPingAll(){
		for(%i = 0; %i < ClientGroup.getCount(); %i++)
		{
			%c = ClientGroup.getObject(%i);
			%s = %c.getPing();
			talk(%c.name @ " : " @ %s);
		}	
	}
	

	
	function fcbn(%t)
	{
		return findClientByName(%t);
	}
	
	function p()
	{
		return fcbn().p;
	}
	function talk(%msg)
	{
		messageAll('',"\c5KIRBOT\c6:"SPC%msg);
	}

	function serverCmdTeleport(%cl,%targ)
	{
		if(%cl.hasTeleportAccess && isObject(%targ.player))
		{
			%cl.player.position = %targ.player.position;
		}
		else
		{
			messageClient(%cl,'',"\c2Your target doesn't exist, or you haven't been granted teleport access.");
		}
	}
	function serverCmdGrantTeleportAccess(%cl,%targ)
	{
		if(%cl.isAdmin || %cl.isSuperAdmin){
			fcbn(%targ).hasTeleportAccess = 1;
			messageClient(%cl,'',"\c2"@%targ@" has been granted teleport access.");
			messageClient(fcbn(%targ),'',"\c2You have been granted teleport access. Type /teleport playername to use.");
		}
		else{
			messageClient(%cl,'',"\c2You'renot admin!");
		}
	}
	
	function serverCmdSuicide(%cl){
		if($suicideDisabled)
		{
			messageClient(%cl,'',"\c6Suicide is disabled.");
		}
		else
		{
			Parent::serverCmdSuicide(%cl);
		}
	}
	function serverCmdToggleSuicide(%cl)
	{
		if($suicideDisabled){
			$suicideDisabled = 0;
			messageClient(%cl,'',"\c5Suicide enabled.");
		}
		else
		{
			$suicideDisabled = 1;
			messageClient(%cl,'',"\c5Suicide disabled.");
		}
	}	
};

	datablock AudioProfile(Test)
	{
		filename = "add-ons/sound_phone/Phone_Ring_1.wav";
		description = AudioClosest3d;
		preload = true;
	};

activatePackage("serverTools");
$zz = 1;