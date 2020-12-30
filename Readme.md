# Proto
Proto is a project that's an umbrella project for trials and prototypes. I use it to:
* keep my experimental code in one place
* keep my experiments structured in a uniform way
* keep my experiments documented so I can reread them later on

Because experimentation is a such a large part of this, I set the app up to with a commandline parser. This should cause it to be easy to play around with a particular aspect.

## Setup
As mentioned above, the app is set up as a console app with a commandline parser built in. The parser used is the   `CommandLineParser` library, see (https://github.com/commandlineparser/commandline/wiki) for more documentation.

Every trial should get its own `Verb`, the options should be a separate Options class in the Options directory. The trial should be started from the `Program.Run()` method.

# Todo
* [ ] Create the awaiter trial.
* [x] Create a gitignore file.
* [ ] Add the project to version control.