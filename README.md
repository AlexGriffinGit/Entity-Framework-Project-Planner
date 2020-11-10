# Entity Framework Project Planner #

## Table Of Contents ##
* [Summary](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#summary)
* [Project Goal](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#project-goal)
* [Project Definition Of Done](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#project-definition-of-done)
* [User Story Definition Of Done](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#user-story-definition-of-done)
* [Sprints](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#sprints)
  * [Sprint 1](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#sprint-1)
    * [Sprint Goal](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#sprint-goal)
    * [Sprint Review](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#sprint-review)
    * [Sprint Retrospective](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#sprint-retrospective)
  * [Sprint 2](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#sprint-2)
    * [Sprint Goal](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#sprint-goal-1)
    * [Sprint Review](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#sprint-review-1)
    * [Sprint Retrospective](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#sprint-retrospective-1)
  * [Sprint 3](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#sprint-3)
    * [Sprint Goal](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#sprint-goal-2)
    * [Sprint Review](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#sprint-review-2)
    * [Sprint Retrospective](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#sprint-retrospective-2)
* [Project Retrospective](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#project-retrospective)
* [Class Diagrams](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#class-diagrams)
* [Kanban Board](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#project-board)
* [Wireframes](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#wireframes)
* [Screenshots](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#screenshots)
* [Download](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner#download)

## Summary ##

A simple project planner designed for small teams, built with Entity Framework with a SQLServer database and a WPF frontend. The project planner can keep track of projects, features and issues for that project, and general notes that aren't linked to a project.

## Project Goal ##
To create a simple easy to navigate project planner that allows single users or small teams to track their current and completed projects along with any features and issues relating to that project. In addition to this allow the user to add notes that may or may not relate to a given project.

## Project Definition Of Done ##
The project is considered done when:
- All 4 tables (Projects, Issues, Features and Notes) can be modified with CRUD operations within the program.
- The GUI is conveying the changes made to the tables.
- All unit tests that cover all of the aspects of the program are passing, including functional, edge and error cases. 
- The projects has been reviewed and signed off.
- All user stories are marked as done.
- The project has a complete README file including class diagrams.

## User Story Definition Of Done ##

A user story is considered done when:
- Program functions the way defined in the user story.
- All unit tests for that operation all pass, including edge cases and error cases. 
- When the stakeholder accepts the user story is complete.
- When the user story is reviewed and signed off.

## Sprints ##
* ### Sprint 1 ###
  * #### Sprint Goal ####
    The goal for my first sprint was to create the Minimum Viable Product, this means be able to create and view items that are in the database for all 4 different  tables. In addition to this create unit tests for the implementations of these creating and view methods and ensure they all pass.
   
  * #### Sprint Review ####
    Completed user stories:
    * [Epic 1 - User Story 1 - List the projects currently in the database.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689148)
    * [Epic 1 - User Story 2 - Select a project from the project list.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689139)
    * [Epic 1 - User Story 3 - Adding a new project to the project database.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689129)
    * [Epic 2 - User Story 1 - Adding a new feature to the feature table in the database.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689095)
    * [Epic 3 - User Story 1 - Add a new issue to the issue table in the database.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689061)
    * [Epic 4 - User Story 1 - Add a new note to the note table in the database.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689025)

    Not completed user stories:
    * [Epic 1 - User Story 4 - Viewing a specific project and it's associated information, methods not adequately covered by unit tests.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689119)
    * [Epic 2 - User Story 2 - Viewing features and their details for a specific project, methods not adequately covered by unit tests.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689084)
    * [Epic 3 - User Story 2- Viewing issues and their details for a specific project, methods not adequately covered by unit tests.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689058)
    * [Epic 4 - User Story 2- Viewing notes and their details, methods not adequately covered by unit tests.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689007)
  
  * #### Sprint Retrospective ####
    Overall I think my first sprint went well and I am happy with the progress I have made on the project.

    * <b>What went well?</b>
      * The creation of the database schema and database was very quick and easy as I had planned out the structure before hand.
      * The create and the view methods worked well and passed the tests I created without much alteration.
      * The GUI and the database are interacting they way I expected them to.
    * <b>Improvements to make?</b>
      * Don't focus on the unimportant aspects and details of the GUI.
      * Focus more on testing all of the CRUD methods before calling them from the GUI.
    * <b>What will I be doing in the next sprint?</b>
      * Finish testing all of my CRUD methods before moving on to the bigger tasks.
      * Make sure I've got unit tests for any CRUD methods I add before implementing them in the GUI.
      * Add the update and delete crud methods for all 4 tables.
      
* ### Sprint 2 ###
  * #### Sprint Goal ####
    The goal for my second sprint was to extend the Minimum Viable Product to include the ability to modify and delete existing items that are in the database for all 4 different tables. In addition to that create unit tests for the implementations of the modification and deletion methods and ensure they all pass.
    
   
  * #### Sprint Review ####
    Completed user stories:
    * [Epic 1 - User Story 4 - Viewing a specific project and it's associated information.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689119)
    * [Epic 2 - User Story 2 - Viewing features and their details for a specific project.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689084)
    * [Epic 3 - User Story 2- Viewing issues and their details for a specific project.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689058)
    * [Epic 4 - User Story 2- Viewing notes and their details.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689007)
    * [Epic 1 - User Story 5 - Modify existing projects in the database.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689115)
    * [Epic 2 - User Story 3 - The ability to modify a feature present in the database.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689075)
    * [Epic 3 - User Story 3 - Modify an existing issue related to a project in the database.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689052)
    * [Epic 4 - User Story 3 - Modify an existing note in the database.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48688997)
    * [Epic 1 - User Story 6 - Deleting existing projects in the database.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689104)
    * [Epic 2 - User Story 4 - The ability to delete a feature present in the database.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689068)
    * [Epic 3 - User Story 4- Delete an existing issue related to a project in the database.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48689040)
    * [Epic 4 - User Story 4 - Delete an existing note in the database.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48688983)
    
    Not completed user stories:  
    * All Completed.
    
  * #### Sprint Retrospective ####
    My second sprint went very smoothly and I'm very happy with where the project planner currently is. The improvements I outlined in my first sprint retrospective really helped to focus my efforts where they needed to be.

    * <b>What went well?</b>
      * I created tests for all of the functionality before implementing the GUI aspects.
      * The update and the delete methods behaved as expected after a few alterations and they passed the tests I created after these alterations.
      * The GUI is updating to changes in the database straight away.
      * The changes I made to improve the readability of the code have certainly helped me in fixing bugs and finding specific code.
    * <b>Improvements to make?</b>
      * Although better than last sprint I still find I'm focusing on the details of the GUI too much.
      * I need to find ways to stop reusing very similar code in multiple different places.
      * More focus on planning certain aspect so I don’t have make alterations later down the line.
    * <b>What will I be doing in the next sprint?</b>
      * Adding more test coverage that’ll focus on edge and error cases
      * Adding the ability to search using a search bar which will search for that word or phrase.
      * Adding the ability to export all of the data for projects, features, issues and notes as XML and JSON.
      * Add missing GUI elements, such as the list titles.
      * Move any non CRUD methods out of the CRUDManager class.
      
* ### Sprint 3 ###
  * #### Sprint Goal ####
    The goal for my final sprint was to add a number of additional features to the application that make it more user friendly. These features are the ability to export data from the database into XML and JSON format, the ability to search the database for keywords and phrases and the ability to drag and drop elements into different status columns and have this change occur in the database.
   
  * #### Sprint Review ####
    Completed user stories:
    * [User Story 1 - Be able to search for specific words or phrases and have a list of items where that word or phrase occurs](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48688974)

    Not completed user stories:
    * [User Story 2 - Be able to export all of or some of the data that is in the database to an XML file, unit tests do not check for a valid XML file or if the right information had been exported.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48978910)
    * [User Story 3 - Be able to export all of or some of the data that is in the database to a JSON file, unit tests do not check for a valid JSON file or if the right information had been exported.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48978936)
    * [User Story 4 - Be able to drag and drop issues and features from one status column to another to change their status, Implementation not started.](https://github.com/AlexGriffinGit/Entity-Framework-Project-Planner/projects/1#card-48979115)
  
  * #### Sprint Retrospective ####
    My final sprint has been fairly productive although I couldn't quite get all the extras I was planning to include implemented into the application. These extra modifications will be moved back onto the product backlog and can be developed in a version 2.0.

    * <b>What went well?</b>
      * Unit tests for checking that the search functionality was exhibiting the expected behaviour were easier to create than i had first envisioned.
      * Implementing the XML and JSON exporting was also easier to implement than I expected.
      * GUI behaved as I expected it to.
      * refactoring was time consuming but went fairly smoothly.
    * <b>Improvements to make?</b>
      * Better planning of the application would mean I wouldn't have had to spend as much time refactoring and reorganising than I did in this sprint.
      * I was unsure of how to unit test the exported XML and JSON were valid and held the correct information.
      * There are probably more efficient ways to implement a search function than my implementation.
    * <b>What will I be doing in the next sprint?</b>
      * Research and create more unit tests for the XML and JSON exporting.
      * Research on how to search through a database more efficiently.
      
## Project Retrospective ##

## Class Diagrams ##

## Project Board ## 

## Wireframes ## 

## Screenshots ## 

## Download ##
