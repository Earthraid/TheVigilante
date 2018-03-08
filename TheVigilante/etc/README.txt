General Information:
--------------------

/In Game Options/

--------------------
/Opening Game Menu/

/New Game/
Initiates a new game that allows the user to input a name for the character and reads  
OriginStory.txt, located in the etc file associated with The Vigilante console game files.
Sets Player Money to "0.00" and Player Level to "1".

/Load Game/
Pulls information from a SQL database and displays previous save games for the user to load
and continue playing.

/Read Me/
Displays text from this README.txt file, located in the etc file associated with 
The Vigilante console game files.

--------------------
/At Home Menu/

/Store/
The Vigilante "store" option pulls information from a SQL database to populate 
weapons and armor for the user to purchase with money gained from defeating criminals.

/Go Fight/
Randomly generates a criminal from an array of first and last names along with an array of 
crimes.  Sets the criminal level to a span of +1, -1, or equal to the user's level.
Prompts confirmation of battle.

/Save Game/
Accessses SQL database and saves a new game file if none exists or inputs new data if the 
game has been previously saved.

/Quit Game/
Exits the play environment and promts user to press any key to close the command prompt.

--------------------
/At Store Menu/

/Weapons/
The "Weapons" option pulls information from a SQL database to populate a list of
weapons for the user to purchase with money gained from defeating criminals.

--Weapons List--

Allows the user to select a weapon for purchase, prompts confirmation.
If confirmed, alters user owned weapon and damage output.

/Armor/
The "Armor" option pulls information from a SQL database to populate a list of
armor for the user to purchase with money gained from defeating criminals.

--Weapons List--

Allows the user to select armor for purchase, prompts confirmation.
If confirmed, alters user owned armor and damage protection.

/Fight Calculations/

Upon selection of "Go Fight" from the main menu, the user if prompted to 
1. Go Fight, which continues on to the battle sequence. 
2. Run Away, which removes 10% of player money and returns to the "At Home" menu.

"Go Fight"
Randomly selects if the user or criminal strikes first.

The generated criminal's level is used to randomly roll between a set of numbers 
to represent damage to the user's character.  If armor is owned by the user, the 
value is deducted from the criminal's roll.

The player's level is used to randomly roll between a set of numbers to represent 
to represent damage to the generated criminal. If a weapon is owned by the user
the damage is added to the user's roll.

--------------------

/Credits/

--------------------

Designed and Built By : Jerrad Christian

Special Thanks To : Jessica Parent
