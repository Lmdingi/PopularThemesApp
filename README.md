# PopularThemesApp

### Overview
This program determines the most popular colour, and:
* Display the colour name
* Determine all users who voted for this colour.
* Display the list of users who voted for the colour in sorted order.

### Motivation
What advantages does this language offer for solving this problem?
* I chose to solve this problem using C# because it comes with powerful builtin tools for both reading files and managing lists. It also allows you to build models (your own Types) and can warn when they have been used wrong. On top of that I already have experience working with C#, so this allows me to write this solution without worrying to much about the language.

In what ways does the language make it more difficult to solve this problem?
* One of the difficulties I faced from C# while solving this problem was making a choice not to use the async/await on my methods especially when reading data from the files.
* Another difficulty was when I decided to change or update my solution from writing all the logic in the Main method to a layered approach with repositories and services introducing more classes and wiring, which added extra work but also I believe it helped improve readability.

Why did you select this particular language to solve the problem?
* I chose C# because I already have hands on experience using it and it allows me to use my existing knowledge, and write solution quick.

Did you do anything to make the solution run faster?
* Yes, I have and one was refactoring the code to a layered structure instead of overloading the main method and this made a lot of difference.
* The second approach was the choice to load each file data once into a list variable and then perform all processing on those lists.