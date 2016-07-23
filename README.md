# _Band Tracker Database_

#### By _**Adam Luchini**_
#### _7/22/2016_ ####

## Description

_This application is used to track which bands are playing at which venues around town._


## Setup/Installation Requirements

* _Clone this repository_
* _Open Windows PowerShell_
* _Type sqlcmd -S "(localdb)\mssqllocaldb" into PowerShell to prompt SQLCMD_
* _Set up the hair salon database by typing the following into SQLCMD_
  * _CREATE DATABASE band_tracker;_
  * _GO_
  * _USE band_tracker;_
  * _GO_
  * _CREATE TABLE venues (id INT IDENTITY(1,1), name VARCHAR(255));_
  * _CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(255));_
  * _CREATE TABLE venue_band(id INT IDENTITY(1,1), venue_id INT, band_id INT);
  * _GO_
  * _quit_
* _Navigate to project folder_
* _Run dnu restore in the command line_
* _Run dnx kestrel in the command line_
* _Open Google Chrome to localhost:5004_

## Technologies Used
* _Microsoft SQL Server Management Studio 2016_
* _C#_
* _Nancy 1.3.0_
* _Razor 1.3.0_
* _xunit 2.1.0_

### Legal

Copyright (c) 2016 **_Adam Luchini_**

This software is licensed under the MIT license.
