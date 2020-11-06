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
* [Project Retrospective](link)
* [Class Diagrams](link)
* [Kanban Board](link)

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
    * Epic 1 - User Story 1 - List the projects currently in the database.
    * Epic 1 - User Story 2 - Select a project from the project list.
    * Epic 1 - User Story 3 - Adding a new project to the project database.
    * Epic 2 - User Story 1 - Adding a new feature to the feature table in the database.
    * Epic 3 - User Story 1 - Add a new issue to the issue table in the database.
    * Epic 4 - User Story 1 - Add a new note to the note table in the database.

    Not completed user stories:
    * Epic 1 - User Story 4 - Viewing a specific project and it's associated information, methods not adequately covered by unit tests.
    * Epic 2 - User Story 2 - Viewing features and their details for a specific project, methods not adequately covered by unit tests.
    * Epic 3 - User Story 2- Viewing issues and their details for a specific project, methods not adequately covered by unit tests.
    * Epic 4 - User Story 2- Viewing notes and their details, methods not adequately covered by unit tests.
  
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
    
   
  * #### Sprint Review ####
    Completed user stories:
    

    Not completed user stories:
    
  
  * #### Sprint Retrospective ####
    

    * <b>What went well?</b>
      
    * <b>Improvements to make?</b>
      
    * <b>What will I be doing in the next sprint?</b>
    
* ### Sprint 3 ###
  * #### Sprint Goal ####
    
   
  * #### Sprint Review ####
    Completed user stories:
    

    Not completed user stories:
    
  
  * #### Sprint Retrospective ####
    

    * <b>What went well?</b>
      
    * <b>Improvements to make?</b>
      
    * <b>What will I be doing in the next sprint?</b>
      
