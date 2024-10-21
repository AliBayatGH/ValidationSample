```
+-----------------+           +-------------------+       +---------------------+         +-----------------------+
|                 |           |                   |       |                     |         |                       |
|   MealsyHr      |           |   MevService      |       |  MealsyNotification |         |    POSClientApplet    |
|   (User API)    |           |   (User)          |       |   (Notification API)|         |    (Background Task)  |
|                 |           |                   |       |                     |         |                       |
+--------+--------+           +--------+----------+       +-----------+---------+         +-----------+-----------+
         |                             |                              |                               |
         | (1) Create User             |                              |                               |
         |---------------------------->|                              |                               |
         |                             | (2) Save User with Status:   |                               |
         |                             |      Pending                 |                               |
         |                             |                              |                               |
         |                             |                              |                               |
         |                             |                              |                               |
         |                             |                              | (3) Check for Pending Users   |
         |                             | (4) Forward GET Request to   |<------------------------------|
         |                             | MevService for Pending Users |                               |
         |                             |<-----------------------------|                               |
         |                             |                              |                               |
         |                             |                              |                               |
         |                             |                              |                               |
         |                             |                              |                               |
         |                             |                              |(5) Validate User (via Web-Srm)|
         |                             |                              |                               |
         |                             |                              |                               |
         |                             | (6) Update User Status       |                               |
         |                             |<-----------------------------|-------------------------------|
         |                             |                              |                               |
         +-----------------------------+------------------------------+-------------------------------+

```

### Diagram Explanation:
- **(1)** MealsyHr creates a new user and sends it to MevService with a status of 'Pending'.
- **(2)** MevService stores the user and sets the status to 'Pending'.
- **(3)** POSClientApplet periodically makes a GET request to MealsyNotification for users with status 'Pending'.
- **(4)** MealsyNotification forwards the GET request from POSClientApplet to MevService to get users with status 'Pending'.
- **(5)** POSClientApplet validates the user using external logic.
- **(6)** POSClientApplet updates the user status in MevService to either 'Processed' or 'Failed'.