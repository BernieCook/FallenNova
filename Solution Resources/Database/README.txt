In order to create the Fallen Nova database several steps must be executed. The only prerequisite is that you have SQL 2008 R2 or SQL 2012 running.

If you do want a MySQL or SQL Azure copy do let me know and I'll put something together.

1. Create the empty Fallen Nova database
2. Create the EVE Online database
3. Import the base EVE Online data into the Fallen Nova database
4. Insert several base table entries into the Fallen Nova database
5. Delete the EVE Online database
6. Create the ELMAH database
7. Test

After this you're ready to go.

-----------------------------------------------------------------------------

DETAILED DATABSE SETUP INSTRUCTION

So here they are in more detail.

- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
1. Create the empty Fallen Nova database

Run this SQL script: \FallenNova\SolutionResources\Database\1. Create Fallen Database.sql

- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
2. Create the EVE Online database

You can download the latest official EVE Online SQL database dump from here:

http://community.eveonline.com/community/toolkit.asp

Download the ZIP file that ends with "_db.zip". Open the ZIP file up and extract the file named: "DATADUMP201212190948.bak".
Open up SQL Enterprise Manager and restore the .BAK file, naming it "EveOnline".
TIP: You can run the scripts but it's easier just to restore the .BAK file. 

- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
3. Import the base EVE Online data into the Fallen Nova database

Run this SQL script: \FallenNova\SolutionResources\Database\3. Copy Eve Online Data.sql

- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
4. Insert several base table entries into the Fallen Nova database

Run this SQL script: \FallenNova\SolutionResources\Database\4. Insert Base Data.sql

- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
5. Delete the EVE Online database

Delete the Eve Online database restored in step 2.

- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
6. Create the ELMAH database

Run this SQL script: \FallenNova\SolutionResources\Elmah\ELMAH-1.2-db-SQLServer.sql

- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
7. Test

To test everything is okay sign in with the new user 'berniecook@fallennova.com' with the password 'password123'.



