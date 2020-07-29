# Software-for-training-routines-management
Final project for Higher Level Education Cycle in Multiplatform Application Development

This project consists of a desktop application that manages the workouts of the customers in a gym.

The interface of the application has been created using the WPF framework and the design pattern Model, View, Controller (MVC). MariaDB is used ad Database Management System and for the management of the data flow between the interface and the database an API Rest has been created using Node.js

In the application there are 3 roles, administrator, trainer and student. When a user log in on the system, it displays an screen acording to the role or roles assigned to the user.
The functions that can be carried out with each role are the following

- Administrator: He/she is in charge of registering the users, assigning the roles and resetting the passwords.

- Trainer: He/she can create exercises for the gym and incorporate them to the training routines created by the trainer. These training routines will be assigned to his/her students to be executed on certain dates. Finally, he/she can check whether or not the student has performed the assigned routines on a range of dates.
- Student: he/she can create his/her own training routines and assign to himself/herself, so the student can run its own training routines or those assigned by your trainer. The student can also manage whether or not has performed a training routine. Finally, the student can also change the trainer assigned.
