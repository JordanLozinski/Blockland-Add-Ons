 function autoClicker() { cancel($autoClicker); commandToServer('activateStuff'); $autoClicker = schedule(32, 0, autoClicker); }
