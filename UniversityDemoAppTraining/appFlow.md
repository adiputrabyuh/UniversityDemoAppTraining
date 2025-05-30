# University Demo App - Workflow
---

## DATABASE:

### 1. Student
    1. Id
    2. First Name
    3. Last Name
    4. Email
    5. Enroll Date


### 2. Teacher
    1. Id
    2. First Name
    3. Last Name
    4. Email

### 3. Courses
    1. Id
    2. Course Name

### 4. Enrollments (Join to student/id)
    1. Id
    2. Student Id
    3. Course Id
    4. Teacher Id
    5. Grade

---

## API SPECS:

### 1. Students Specs:

    Add - POST

    Get (All Students) - GET    

    Get (Id) + Enrollments - GET

    Update (Id) - PUT

    Delete (Id) - DELETE

### 2. Teachers Specs:

    Add - POST

    Get (All Teachers) - GET

    Get (Id) - GET

    Update (Id) - PUT

    Delete (Id) - DELETE

### 3. Teachers Specs:

    Add - POST

    Get (All Courses) - GET

    Get (Id) - GET

    Update (Id) - PUT

    Delete (Id) - DELETE