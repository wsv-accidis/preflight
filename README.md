Preflight
=========

This is an app which displays weather forecast data gathered from web sources published by LFV 
(the air transport authority) and SMHI (the state meteorological institute) in Sweden. It is 
intended for private pilots. It supports forecast content in Swedish and English (default). 
Additional features include a crosswind calculator and a fuel calculator/unit converter.

The app is available from the [Windows Phone Store](http://www.windowsphone.com/en-us/store/app/preflight/5d1e6bd9-f941-4a14-8d2e-7fe6cb298dcd).

While the data published by LFV is public, free and regionally unrestricted there is no public
API to access it, so the code relies heavily on scraping content from web pages and cleaning
it up. This causes the code to occasionally break as the format changes, but this is rare and
usually easy to fix.

The code was originally written for Windows Phone 7 so it has a bit of legacy cruft in it, but the
current version has been updated and works on Windows Phone 8.1 onwards. I imported the project
from Subversion and commit history was unfortunately lost. Development was started sometime in 2011.
The first release was certainly in december 2011.

## License
Preflight is distributed under the terms of the **Apache License version 2.0**.

## Acknowledgements
I was inspired to create this app by [FlightMet Sweden](https://play.google.com/store/apps/details?id=se.doit.flightmet) which does
pretty much the same thing, for Android. It was developed by DoIT Systemutveckling.

I used example code from [Håkan Reis](http://www.jayway.com/2011/02/03/create-a-round-button-control-for-windows-phone-7/) and
artwork from [Mariee Jamieson](http://digimaree.deviantart.com/) (used with permission) in building the UI.

The [Protobuf-net](https://code.google.com/p/protobuf-net/) library is used for some serialization. It was created
by Mark Gravell.

Earlier versions also used [ImageTools for Silverlight](https://imagetools.codeplex.com/) by Sebastian Stehle,
but it was no longer necessary after support for Windows Phone 7 was dropped.
