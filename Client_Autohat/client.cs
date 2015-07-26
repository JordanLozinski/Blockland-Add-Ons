package autohat
{
	function autohat()
	{
		$autohat = 1;
		hatCycle();
		echo("autohat toggled");
	}	

	function hatCycle()
	{
		cancel($autohatSchedule);
		if($autohat == 1)
		{
			commandToServer('hat', "rand");
		}
		$autohatSchedule = schedule(500,0,hatCycle);
	}
};

$autohat = 0;
activatePackage("autohat");

