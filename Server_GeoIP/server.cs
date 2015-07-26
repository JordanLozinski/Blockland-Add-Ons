new httpObject(httpObj);
function httpObj::onLine(%this, %line)
{ 
	%this.result = %this.result @ "\n" @ %line;
	//echo(%this.result);
	$geoIPResult = %this.result;
}

function httpObj::getResult(%this)
{
	return %this.result;
}

function locate(%cl){
	httpObj.get("freegeoip.net:80","/csv/"@ %cl.getRawIP()); 
	//values returned: IP, initials of country, country name, state/province initials, 
	//state/province, city, zip code, latitude, longitude, possible area codes
	%result = httpObj.getResult();
	%cl.rawGeoIPData = %result;
	echo(%cl.rawGeoIPData);

	parseCSV(%result,%cl);
	//echo(%result SPC "swag");
	chatMessageAll(%cl.name SPC " lives in" @ %cl.city @ ", " @ %cl.country);
}

function GameConnection::onConnectRequest(%cl,%ip,%lan,%net,%prefix,%suffix,%arg5,%rtb,%arg7,%arg8,%arg9,%arg10,%arg11,%arg12,%arg13,%arg14,%arg15)
{
	locate(%ip,%cl);
	chatMessageAll(%cl.name SPC " lives in" @ %cl.city @ ", " @ %cl.country);
	return Parent::onConnectRequest(%cl,%ip,%lan,%net,%prefix,%suffix,%arg5,%rtb,%arg7,%arg8,%arg9,%arg10,%arg11,%arg12,%arg13,%arg14,%arg15);
} 

function serverCmdGetCountry(%cl,%targ)
{
	messageClient(%cl,'',findClientByName(%targ).country);
	echo(findClientByName(%targ).country);
}

function serverCmdGetCity(%cl,%targ)
{
	messageClient(%cl,'',findClientByName(%targ).city);
}

function parseCSV(%str,%cl)
{
	%count = 0;
	while(%str !$= "" && %count < 500)
	{
		echo("initStr " @ %str);
		%str = nextToken(%str,"temp",",");
		echo("postToken str " @ %str);
		%temp = nextToken(%temp,"temp2","\"");
		echo("pureToken " @ %temp2);
		$csv[%count] = %temp2;
		echo("csv assign " @ %count SPC %temp2);

		echo("switch " @ %count);
		switch(%count)
		{
			//values returned: IP, initials of country, country name, state/province initials, 
			//state/province, city, zip code, latitude, longitude, possible area codes
			case 0: //IP, throw it away
				break;
			case 1: //intials of country e.g US
				%cl.countryInit = %temp2;
				echo(%temp2 SPC "countryInit");
			case 2: //country
				%cl.country = %temp2;
				echo(%temp2 SPC "country");
			case 3: //state/province initials
				%cl.stateInit = %temp2;
				echo(%temp2 SPC "stateInit");
			case 4: //state/province
				%cl.state = %temp2;
				echo(%temp2 SPC "state");
			case 5: //city
				%cl.city = %temp2;
				echo(%temp2 SPC "city");
			case 6: //zip code
				%cl.zipCode = %temp2;
				echo(%temp2 SPC "zipCode");
			case 7: //latitude
				%cl.geoLatitude = %temp2;
				echo(%temp2 SPC "latitude");
			case 8: //longitude
				%cl.geoLongitude = %temp2;
				echo(%temp2 SPC "longitude");
			case 9: //area code
				%cl.areaCode = %temp2;
				echo(%temp2 SPC "areaCode");
			default:
				echo("[geoIP]: too many CSVs returned! may malfunction");
		}
		echo("switch past");
		%count++;
		if(%str !$= "" && %count < 500)
		{
			echo("true");
		}
		else
		{
			echo("false");
		}
	}
	if(%count > 499)
	{
		echo("[geoIP]: likely invalid string passed to parseCSV() "@%str);
	}

	//echo(%cl.name NL "-----------------" NL "country initials:" @ %cl.countryInit NL "country:" @ %cl.country NL "state initials:" @ %cl.stateInit NL "state:" @ %cl.state NL "city:" @ %cl.city NL "zipcode:" @ %cl.zipCode NL "latitude:" @ %cl.geoLatitude NL "longitude:" @ %cl.geoLongitude NL "area code:" @ %cl.areaCode);
}

locate(findClientByName("dr"));

//
// not sure what im gonna do with the stuff below this. was gonna make a grid map but 2 lazy
//

function buildMap()
{
	for(%y = 0; %y < 17; %y++)
	{
		for(%x = 0; %x < 36; %x++)
		{
			if(0)//replace with %isLand later
			{
				%b = 1;
			}
			else
			{
				%b = 0;
			}
			%mapSched = schedule(0,0,heatmapBrickPlant,%x,%y);
		}
	}
}

function heatmapBrickPlant(%x, %y)
{
	%brick = new fxDTSBrick()
	{
		colorID = "14";
		printID = "60";
		position = "1 "@ 3.25 + (0.5 * %x) SPC 0.3 + (0.6 * %y) @ "";//.5 for y, //.6 for z
		datablock = "brick1x1PrintData";
		rotation = "0 0 -1 90.002";
	};
	%brick.setTrusted(true);
	%brick.plant();
	BrickGroup_999999.add(%brick);
	$MapGroup[%x,%y] = %brick;
	return %brick;
}
