# ProjectOSRS
A project to port all or most of OSRS items to a Total Miner: Forge mod.

## Releases
This project isn't currently ready for release, but will be in the coming weeks. Stay tuned.

## Building
**Downloading the sprites for this mod can be time consuming, so you can get a file cache to place in your `bin/Debug` or `bin/Release` from [here](https://mega.nz/#!tn5QXDDD!16m1jjyEDxCXmjhp2lqy19kwcxPm2EHWtSabt0--nEE)**

 1. Before running OSRStoTMF, you must place a [osrs.me item schema copy](https://github.com/justync7/osrs.me/raw/master/data/raw/items.json) in the `bin/Debug` folder. Be sure to overwrite the one in the item cache with this, as it is not always up to date.
 2. To build this mod you have to run the OSRStoTMF command line program to generate the XML and spritesheet, then build  the solution again to copy all the needed files to `ProjectOSRS/Install`.
