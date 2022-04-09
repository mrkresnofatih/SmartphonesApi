# SmartphonesAPI

This is just a test codebase for learning unit testing & exception handling.

### What I've Done Here

1. Tested the controllers based on the following [video](https://youtu.be/ULJ3UEezisw).
2. Exception Handler using Exception Filter based on the following [video](https://www.linkedin.com/learning/advanced-asp-dot-net-core-unit-testing).

### Notes

1. Somehow when an error is thrown, it doesn't automatically log the 
Exception Class and the stack trace. I had to manually setup `ILogger<>` and 
use `[TypeFilter]` for dependency injection in order to log them.