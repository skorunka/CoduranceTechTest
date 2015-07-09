# Condurance Tech Test

* Shared AssemblyInfo
* Team shared R# rules
* Shared Code Analysys rules(incl. StyleCop rules)
* Issues tracked on GitHub

## Requirements
* Easily to add new commands
* The input parameters separator is " "(one space), it fails if more spaces are provided.

## Notes
* To keep it simple I deceided to use something like noSQL DB. It stores the whole graph of an Entity. Related Objects are referenced by object reference, not by foreign key(as we know it from RDBs).
* The project structure is simple = folders instead of projects.
