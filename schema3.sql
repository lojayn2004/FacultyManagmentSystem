

-- USER TABLE
create table users(
userID int identity(1,1) primary key,
firstName varchar(50) not null,
lastName varchar(50)not null,
email varchar(50) not null,
phone varchar(50)not null,
userPassword varchar(50) not null,
gender varchar(50) not null,
userType varchar(50) not null,
birthDate int not null
);

-- DEPARTMENT TABLE
create table department(
deptID int primary key identity(1, 1),
deptName varchar(200) not null
);

-- PROFESSOR TABLE
create table professor(
userID int primary key ,
deptID int ,
position varchar(200)  null ,
foreign key (userID) references users(userID) ON DELETE CASCADE ON UPDATE CASCADE ,
foreign key (deptID) references department(deptID) ON DELETE CASCADE ON UPDATE CASCADE 
);

-- COURSE TABLE

create table course(
course_code varchar(100) primary key,
course_name varchar(200) not null,
credit_hours int not null,
locations varchar(200) not null,
capacity int not null,
descriptions text not null ,
userID int,
deptID int ,
foreign key (userID) references professor(userID) ON DELETE CASCADE ON UPDATE CASCADE ,
foreign key (deptID) references department(deptID) ON DELETE CASCADE ON UPDATE CASCADE,
);

-- Student Table
create table student(
userID int primary key ,
deptID int ,
gpa float null,
graduation_year int  null,
total_credit_hours int not null DEFAULT 0,
foreign key (deptID) references department(deptID) ON DELETE CASCADE ON UPDATE CASCADE,
foreign key (userID) references users(userID) ON DELETE CASCADE ON UPDATE CASCADE ,
);


-- Enrollment Table
create table enrollment(
userID int ,
course_code varchar(100) ,
grade int  null,
years int  null,
semester varchar(200)  null,
primary key(userID,course_code),
foreign key (course_code) references course(course_code) ON DELETE CASCADE ON UPDATE CASCADE ,
foreign key (userID) references student(userID) ON DELETE CASCADE ON UPDATE CASCADE,
);

-- prequisite table
create table prerequisite(
pre_course_code varchar(100),
course_code varchar(100),
primary key (pre_course_code,course_code),
foreign key (course_code) references course(course_code) ON DELETE CASCADE ON UPDATE CASCADE ,
foreign key (pre_course_code) references course (course_code) ON DELETE CASCADE ON UPDATE CASCADE,
);

INSERT INTO department (deptName) VALUES ( 'Computer Science');
INSERT INTO department (deptName) VALUES ('Information System');

INSERT INTO users (firstName, lastName, email, phone, userPassword, gender, userType, birthDate)
VALUES ('Ahmed', 'Hussein', 'Ahmed.Hussein@gmail.com', '01598435698', '123456', 'Male', 'student', 2004);

INSERT INTO users (firstName, lastName, email, phone, userPassword, gender, userType, birthDate)
VALUES ('Heba', 'Ahmed', 'Heba.Ahmed@gmail.com', '01098787659', '234567', 'Female', 'Admin',1955);
INSERT INTO users (firstName, lastName, email, phone, userPassword, gender, userType, birthDate)
VALUES ('Sara', 'Mohamed', 'Sara.Mohamed@gmail.com', '01246753478', 'abcde', 'Female', 'professor', 1985);
INSERT INTO users (firstName, lastName, email, phone, userPassword, gender, userType, birthDate)
VALUES ('Mostafa', 'Mohamed', 'Mostafa.Mohamed@gmail.com', '01089684783', 'abccc', 'Male', 'professor', 1980);
INSERT INTO users (firstName, lastName, email, phone, userPassword, gender, userType, birthDate)
VALUES ('Hala', 'Mohmoud', 'Hala.Mohmoud@gmail.com', '0124623456', 'abcdezz', 'Female', 'professor', 1990);



INSERT INTO professor (userID, deptID, position) VALUES (3, 1, 'Associate Professor');
INSERT INTO professor (userID, deptID, position) VALUES (4, 1, 'Professor');
INSERT INTO professor (userID, deptID, position) VALUES (5, 1, 'Assistant Professor');

INSERT INTO course (course_code, course_name, credit_hours, locations, capacity, descriptions, userID, deptID)
VALUES ('CS101', 'Introduction to Computer Science', 3, 'Building A Room 101', 500, 'This course provides an overview of computer science fundamentals.', 3, 1);

INSERT INTO course (course_code, course_name, credit_hours, locations, capacity, descriptions, userID, deptID)
VALUES ('CS201', 'Data Structures and Algorithms',  4, 'Building B Room 203', 400, 'This course covers data structures and algorithms in depth.', 4, 1);

INSERT INTO course (course_code, course_name, credit_hours, locations, capacity, descriptions, userID, deptID)
VALUES ('CS301', 'Software Engineering',  4, 'Building C Room 305', 350, 'This course focuses on software engineering principles and practices.', 5, 1);

alter table student 
alter column graduation_year int null;
alter table professor
alter column position varchar(200) null;
