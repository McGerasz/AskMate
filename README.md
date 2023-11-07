# AskMate

## Story

It is time to put your newly acquired ASP.NET skills to use.
Your next big task is to implement a crowdsourced Q&A site, similar to Stack Overflow.

## What are you going to learn?

- How to write ASP.NET Core Web Applications
- The Repository Pattern
- Cookie authentication
- What is password hashing

## Tasks

1. Implement the `/Question` endpoint that returns all questions.

   - The questions are available under `/Question`.
   - The endpoint uses the `GET` HTTP Request Method.
   - The data is loaded from the database.
   - The questions are sorted by most recent.
   - [OPTIONAL] The endpoint accepts parameters to customize the sorting of the list.

2. Create an endpoint that returns a question and the answers for it.

   - The question and its answers are available under `/Question/{questionId}`.
   - The endpoint uses the `GET` HTTP Request Method.

3. Create an endpoint that inserts a new question into the database.

   - A question can be inserted by calling the `/Question` endpoint.
   - The endpoint uses the `POST` HTTP Request Method.
   - A question has at least an `id`, a `title`, a `description`, and a `submission_time`
   - After calling it, the endpoint returns the `id` of the new question entry.

4. Create an endpoint that inserts a new answer to a question into the database.

   - An answer can be inserted by calling the `/Answer` endpoint.
   - The endpoint uses the `POST` HTTP Request Method.
   - An answer has at least an `id`, a `message`, a `question_id`, and a `submission_time`
   - After calling it, the endpoint returns the `id` of the new answer entry.

5. Create an endpoint that deletes a question from the database.

   - A question can be deleted by calling the `/Question/{questionId}` endpoint.
   - The endpoint uses the `DELETE` HTTP Request Method.

6. Create an endpoint that deletes an answer from the database.

   - An answer can be deleted by calling the `/Answer/{answerId}` endpoint.
   - The endpoint uses the `DELETE` HTTP Request Method.

7. Create an endpoint that registers a user.

   - A user can be registered by calling the `/User` endpoint.
   - The endpoint uses the `POST` HTTP Request Method.
   - A user has at least an `id`, a `username`, an `email`, a `password`, and a `registration_time`

8. Create an endpoint that logs a user into the system.

   - A user can be logged in by calling the `/User/Login` endpoint.
   - The endpoint uses the `POST` HTTP Request Method.
   - The endpoint accepts a username (or email address) and a password.
   - It is only possible to ask or answer a question when logged in.

9. Create an endpoint that logs a user out of the system.

   - A user can be logged out by calling the `/User/Logout` endpoint.
   - The endpoint uses the `POST` HTTP Request Method.

10. Create an endpoint that marks an answer as accepted.

    - An answer can be marked as accepted by calling the `/Answer/Accept/{answerId}` endpoint.
    - The endpoint uses the `PATCH` HTTP Request Method.
    - It is only possible to accept an answer when the logged-in user is the same one who asked the question.

11. Write unit tests.

    - The service layer of the application is covered with unit tests.

12. [OPTIONAL] Create the frontend of the application.

    - There is a UI for each functionality of the application.
    - The frontend uses basic HTML, CSS, and Vanilla JavaScript.

13. [OPTIONAL] Create an endpoint that searches for a specific phrase in the database.

    - There is a `/Search/{searchPhrase}` endpoint.
    - The endpoint uses the `GET` HTTP Request Method.
    - After calling it, the endpoint returns questions for which the title or description contain the searched phrase.
    - The result list also contains questions which have answers for which the message contains the searched phrase.

14. [OPTIONAL] Create an endpoint that edits an existing question.

    - A question can be edited by calling the `/Question/{questionId}` endpoint.
    - The endpoint uses the `PUT` HTTP Request Method.
    - It is only possible to edit a question when logged in.

15. [OPTIONAL] Create an endpoint that edits an existing answer.

    - An answer can be edited by calling the `/Answer/{answerId}` endpoint.
    - The endpoint uses the `PUT` HTTP Request Method.
    - It is only possible to edit an answer when logged in.

16. [OPTIONAL] Create an endpoint that adds a tag to a question.

    - A tag can be added to a question by calling the `/Tag` endpoint.
    - The endpoint uses the `POST` HTTP Request Method.
    - A tag has at least an `id` and a `name`
    - It is only possible to add a tag to a question when logged in.
    - The `/Question` endpoint returns all questions, their answers, and the related tags as well when called using the `GET` HTTP Request Method.

17. [OPTIONAL] Create an endpoint that returns the 3 most active users from the database.

    - The 3 most active users (who have the most questions and/or answers) are returned by the `/User/MostActive` endpoint.
    - The endpoint uses the `GET` HTTP Request Method.

## General requirements

- The application uses a PostgreSQL database.
- The application uses the Repository pattern.
- The design follows the SOLID, OOP, and clean code principles.

## Hints

- The purpose of this project is to get familiar with ASP.NET Core. If you design a frontend for the application, keep it simple. Don't use complicated frameworks or libraries.
- Do the base data features first and add user management later, extend already existing ones if necessary.
- Try to follow the Agile manifesto and SCRUM when working on the application (plan with estimations, create small user stories to work on, and have daily stand-up meetings).

## Background materials

- 📖 [The Model View Controller Pattern](https://www.freecodecamp.org/news/the-model-view-controller-pattern-mvc-architecture-and-frameworks-explained/)
- 🎥 [Learn ASP.NET](https://dotnet.microsoft.com/en-us/learn/aspnet)
- 🎥 [Repository Pattern](https://youtu.be/x6C20zhZHw8)
- 📖 [What Is Swagger?](https://swagger.io/docs/specification/2-0/what-is-swagger/)
- 📖 [Use cookie authentication without ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/cookie)
- 📖 [What is Password Hashing?](https://www.passcamp.com/blog/what-is-password-hashing/)
- 📖 [Hashing and Salting Passwords in C#](https://code-maze.com/csharp-hashing-salting-passwords-best-practices/)
- 🍭 [Enable Cross-Origin Requests (CORS) in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/cors)
- 🍭 [What is scrum?](https://www.atlassian.com/agile/scrum)
