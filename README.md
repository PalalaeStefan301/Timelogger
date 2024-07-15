# My implementation of 'Timelogger'
- My first thought was to separate the controller code if had to be used in different places from
the main project, within a library, but I've seen 'Timelogger' having the Entities folder so I ended up using
the existing library as a BLL and a DAL basically. I'm saying this because I usually separete them, creating
a dependency in BLL for DAL and in BLL for 'Timelogger.API'. I've added 'Abstract' and 'Concrete' folders
to 'Timelogger' for creating my services, afterwards injecting them into my API.
- The autosave idea when adding/updating is for the cases that a lot of projects/tempos must be
added at the same time, saving them for every batch of 100 let's say, making the process faster. Found
out recently that SaveChanges() slows everything when called multiple times.
- Another idea I came across was the fact that in description are named as users 2 categories,
freelancer and customer, so I created a filter called 'ValidationUserType' that basically checks if in the
queries request there's an userType=Freelancer or userType=Customer(this could be changed to be
checked from another place of the request, like a encrypted header), and for any case there's gonna be
different controllers managing the requests bonding even more the user's types that makes the call with
the functionalities allowed to it. Is more like an experimental idea, this is the first time I've done this. I
could ask Reza for more information, if in the future could be added multiple types of users, but I
thought that's not the main reason of the project, I had to come up with a personal implementation. If
my idea would not work for future features, then just remove the flag on the upper of the controller and
delete those additional controllers.
- Another implementation that makes the code from controllers easier is another filter called
'ValidateModelState'. This API being so validation heavy, this meaning that every request must be
checked if the required fields exist, so this is done before entering the method's main body, and works
for all the methods withing the controller who got the Filter flag. I thought if for some reason a method
doesn't have to be checked I additionally created a flag that skips all the validation named
'SkipCustomFilterAttribute'. I've tested it on TempoController POST.
- About the entities, I've added Tempo, a class that represents the time logged into a project,
having a FK to it and a FK to a user that represents who created it. For using the Data Annotations
validation, I had to declare the main classes as partial (if using DB-first that's how the code looked
anyway) and added metadata classes to create requirements for any fields, like the name for project
being mandatory.
- For the tempo I've came up with the idea to let the user choose if you save the tempo with a
'Hours' field, that being the simpler version of the call, not caring of any minutes, or to send an 'endDate'
that is more specific. For this one I had to create the 'RequiredIfAttribute' that checks if 'hours' already is
part of the request than the 'endDate' is not required anymore, and vice versa.
- For the minimum 30 mins rule I just added it on the controller. Being such a specific rule, I
thought doesn't have to be added in multiple places or on the filter, maybe in future there could be
multiple types of Tempos that don't work the same way.
- Additionally, I created IProjects and ITempos, to make it clearer what must be implemented for
any project/tempo like controller. At this point in time all the projects’ controllers work the same way,
but this is just because I didn't know how should have, I change them to be more specific for any user
type. Just basic interaction with the API, representing CRUD principles.
About the 2nd point, an overview of time registrations per project, by using the GET Tempos
request with a projectid instead of the id that's how the user would see all the tempos added to a single
project.
- I've seen the db is in-memory and I didn't think much of it, to change it to connect to a SQL
Server. That because after I've tested multiple times using Postman finally, I've added a Testing project to
the solution. Afterwards using that to check if all the steps still work after I do some change. Not a lot of
tests, but some I consider being more important at this time. For a faster testing process the HttpClient is
instantiating only once in the constructor.
For the 3rd point ProjectsController/GetByDeadline returns all the projects ordered desc or not
by deadline.
- Other ideas came to mind, like returning only projects/tempos that are created by that user that
requests them, but this is not part of the requirements for now.
