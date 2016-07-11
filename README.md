# Forum
The features of this solution

1. At the moment there are no logging operations (delete, add, update).
2. In this implementation, UnitofWork depends on the Repository, it's not really correctly but for simplicity designed.
3. When the application starts, run EF migrations, the industrial solution is not quite right. It is advisable to adjust the version of the database with which the application is running
4. Added MessageDto, although it coincides with the Message. Made for the future, to layer the Web Api does not depend on the database.
5. Limited the maximum length of the Header 250.
6. Added Pagination for page download.
7. Provided that in the future may appear sort and filter, added the appropriate methods
