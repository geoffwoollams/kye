<?php

$levels = array();
$_level = 0;
$unknown = 1;
$levelNames = array();

$kyeFiles = glob("*.[kK][yY][eE]");
//$kyeFiles = glob("levels/registered/*.[kK][yY][eE]");

foreach($kyeFiles as $kye)
{
	AddLevels($kye);
}

function AlternateName($line)
{
	global $levelNames;

	if(!$levelNames[strtolower($line)])
		return $line;

	for($n = 2; $n < 100; $n++)
	{
		$thisLine = trim($line) . " " . $n . "\n";
		echo "Alternate Attempt: " . $thisLine . "\n";
		if(!$levelNames[strtolower($thisLine)])
			return $thisLine;
	}

	echo "FAIL\n";
	exit;
}

function AddLevels($filename)
{
	global $levels, $_level, $unknown, $levelNames;
	$_line = 0;

	$file = file($filename);
	array_shift($file);

	foreach($file as $line)
	{
		$_line++;

		if($_line == 1)
		{
			$line = trim($line) . "\n";

			$orig = $line;
			$line = preg_replace('/[^a-zA-Z0-9-_\.!\'\:\(\)\?\,\+ ]/', '', $line);

			if(trim($line) == "")
			{
				$line = "Unknown " . $unknown;
				$unknown++;
			}
			$line .= "\n";

			if($orig != $line)
			{
				//echo "orig: " . $orig . "\n";
				//echo "line: " . $line . "\n";
			}

			if($levelNames[strtolower($line)])
				$line = AlternateName($line);

			$levelNames[strtolower($line)] = true;
		}

		$levels[$_level] .= $line;

		if($_line == 23)
		{
			$_level++;
			$_line = 0;
		}
	}
}

$lines = "";

foreach($levels as $level)
{
	$name = preg_split("/\n/", $level)[0];
	$line = "\t\t{\"" . trim(strtolower($name)) . "\", \"" . base64_encode($level) . "\"},\n";
	$lines .= $line;
}

file_put_contents("levels.txt", $lines);

?>