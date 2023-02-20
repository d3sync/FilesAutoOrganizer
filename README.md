# FilesAutoOrganizer
File Auto organizer, in addition it calculates md5hashes for each file and drops it inside a txt file.

This a console app and runs by providing the options --organize and/or --calchash, 
After the first run it produces the configration file (a JSON file with the options for the file organization based on their extentions.)
Optionally you can provide the --path=C:\Temp or so otherwise the default path is the current users Downloads path.

Produces 1 file with the md5hashes for each file when using --calchash in the folder the exe is located.
Produces 1 file with all the files moved from and to when using --organize in the folder the exe is located.
