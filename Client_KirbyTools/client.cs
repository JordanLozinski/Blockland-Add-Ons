package Client_KirbyTools{
    
    function cexec(%a){
    	exec("Add-Ons/Client_" @ %a @"/client.cs");
    }
    function sexec(%a){
    	exec("Add-Ons/Script_" @ %a @ "/client.cs");
    }

    function c(%n)
    {
        return findClientByName(%n);
    }

    //function autoClick(){cancel($autoClick);mousefire(true);schedule(25,0,mouseFire,false);$autoClick=schedule(50,0,autoClick);}

    function getPingAvg()
    {
        $CB_pings = 0;
        while($CB_pings <= 100)
        {
            $CB_pingAverage += serverConnection.getPing();
            schedule(30,0,doNothing);
            $CB_pings++;
        }
        $CB_pingAverage = $CB_pingAverage / 100;
        %z = $CB_pingAverage;
        $CB_pings = 0;
        $CB_pingAverage = 0;
        return %z;

    }
    function servexec(%a)
    {
        exec("Add-Ons/Server_" @ %a @ "/server.cs");
    }

    function getMyPlayer()
    {
        if(!isObject(%cl = ServerConnection)) return -1;
        if(!isObject(%pl = %cl.getControlObject())) return -1;
        return %pl;
    }

    function smp()
    {
        setModPaths(getModPaths());
    }

    function doIHaveAPlayer()
    {
        return isObject(%pl = getMyPlayer()) && (%pl.getType() & $Typemasks::PlayerObjectType);
    }
};

activatePackage("Client_KirbyTools");
exec("Add-Ons/Server_KirbyTools/server.cs");
