# SchedulesDirect.NET
.NET Wrapper for SchedulesDirect.org 20141201 JSON API

<a href="http://www.schedulesdirect.org/">SchedulesDirect.org</a>

Based upon the API documented at <a href="https://github.com/SchedulesDirect/JSON-Service/wiki/API-20141201">https://github.com/SchedulesDirect/JSON-Service/wiki/API-20141201</a>

You'll need your own account with your own username & password.

The password is a SHA1 hex representation of your SchedulesDirect password.

To keep it separate from this repository, I've setup an external file for the appSettings.  There's a template in the App.config.

This is very much still a work in progress.
Currently supported operations are:
<ul>
<li>Obtain a token [GET /token]</li>
<li>Getting status [POST /status]</li>
<li>List the lineups a user has added to their account [GET /lineups]</li>
<li>StationID / channel mapping for a lineup [GET /lineups/{COUNTRY}-{LINEUP}-{DEVICE}]</li>
<li>Download program information [POST /programs]</li>
</ul>

There is currently no error handling.

Written in Visual Studio 2015.
