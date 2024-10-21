+-----------------+           +-------------------+       +---------------------+
|                 |           |                   |       |                     |
|   MealsyHr      |           |   MevService      |       |  POSClientApplet    |
|   (User API)    |           |   (User Storage)  |       |  (Background Task)  |
|                 |           |                   |       |                     |
+--------+--------+           +--------+----------+       +-----------+---------+
         |                             |                              |
         | (1) Create User             |                              |
         |---------------------------->|                              |
         |                             | (2) Save User with Status:   |
         |                             |      Pending                 |
         |                             |                              |
         |                             | (3) Check for Pending Users  |
         |                             |<-----------------------------|
         |                             |                              |
         |                             | (4) Validate User (via       |
         |                             |     external logic)          |
         |                             |                              |
         |                             | (5) Update User Status       |
         |                             |----------------------------->|
         |                             |                              |
         | (6) Poll for User Status    |                              |
         |<----------------------------|                              |
         |                             |                              |
         +-----------------------------+------------------------------+
 
(1) MealsyHr creates a new user and sends it to MevService with status 'Pending'.
(2) MevService stores the user and sets the status to 'Pending'.
(3) POSClientApplet periodically checks MevService for users with status 'Pending'.
(4) POSClientApplet validates the user using external logic.
(5) POSClientApplet updates the user status in MevService (either 'Processed' or 'Failed').
(6) MealsyHr polls MevService to check the user's status.
