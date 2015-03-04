# guitar_hero_bot
Repository for Guitar Hero playing bot Arduino board

This overall design for this project is a C# program that will parse a data file containing a song,
send it via serial to the Arduino, which will have pin-outs to control the buttons on the Guitar Hero
guitar (via soldered wires).

I have referenced both this instructable (http://www.instructables.com/id/Guitar-Hero-Arduino-Bot/?ALLSTEPS)
and also Rock Bots (which recently went down, but if it ever comes back up, it was here: http://www.rock-bots.com/RockBot-System/Guitar-Hero-Guitar/Page1.aspx.html).

Initial thoughts for data file format:
# Define frames per second of the data file
fps:30
# Comment line, can be used for section names, like "Chorus 1"

# Data line, frame delay before from the previous line to until this line is executed,
# - means open string, upper case letter for pressing that color button, and S at the end to indicate
# a strum (to support hammer ons / pull offs)
10,--YB--
4,-R-B-S