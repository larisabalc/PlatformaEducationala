# 🎓 Educational Platform Management Application

## Overview
This C# and WPF (Windows Presentation Foundation) application, using SQL Server 2019 or newer, is designed to manage an educational platform for a school. The application is structured on the MVVM model, utilizing Entity Framework for database operations, and supports four types of users: administrators, teachers, form masters, and students.

### Key Features:
- **Database Normalization:** The database is normalized to the third normal form (3NF) to ensure efficient data storage and retrieval.
- **Stored Procedures:** The application includes stored procedures to streamline database interactions.
- **Layered Architecture:** The application is structured on multiple layers to promote separation of concerns and maintainability.
- **SQL Injection Prevention:** Parameterized queries are used to prevent SQL injection attacks, ensuring secure database operations.
- **MVVM and Data Binding:** The application strictly follows the MVVM pattern and utilizes data binding for dynamic and responsive UI updates.
- **Entity Framework:** Leveraged for efficient and simplified database operations, reducing the complexity of data access code.

## Features
### 1. **Administrator Functionality**
   - **CRUD Operations:** Manage students, teachers, subjects, and specializations.
   - **Associations:** 
     - **Study Year, Specializations, and Subjects:** e.g., 10th grade - Math specialization and its subjects.
     - **Teacher, Subject, and Class:** Assign teachers to subjects and classes.
     - **Student, Study Year, and Specialization:** e.g., John Doe - 10th grade - Math.
     - **Modifications:** Change student classes, assign new teachers or form masters.

### 2. **Teacher Functionality**
   - **Absence Management:** View, add, and justify absences.
   - **Grade Management:** View, add, and delete grades.
   - **Calculate Averages:** Manual intervention to calculate averages, considering thesis requirements.

### 3. **Form Master Functionality**
   - **Absence Justification:** Justify absences for all subjects for students in their class.
   - **View Absences:** Access all absences and unexcused absences for a selected subject.
   - **View Averages:** Access final averages per semester and general average for students.
   - **Class Rankings:** View rankings based on averages.
   - **Awards and Failures:** View honor students, students with failing grades, and those at risk of expulsion due to absences.

### 4. **Student Functionality**
   - **View Grades:** See grades for all subjects.
   - **View Absences:** See absences for all subjects.
   - **View Averages:** See final averages for all subjects and the general average if all grades are finalized.

## Technologies
- **C# and WPF:** Core programming languages and frameworks used for application development.
- **SQL Server 2019+:** Database management system used for storing application data.
- **Entity Framework:** ORM framework used for database operations.
- **MVVM Pattern:** Ensures a clean separation between the UI and business logic.
- **Data Binding:** Utilized for efficient data handling and UI updates.
