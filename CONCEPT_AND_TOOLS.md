\# Concept and Tools — Secure Department



\## 1. Project Concept



Secure Department is a proposed university system designed to simplify and secure the process of college selection, department preference submission, and student allocation.



The system provides different registration workflows depending on the selected college and study system, ensuring that students follow the appropriate process based on their academic status.



\### The Problem



Traditional department-selection processes may face several challenges:



\- Student identity and academic information may not be properly verified before preference submission.

\- Unauthorized individuals may attempt to access or submit preferences on behalf of students.

\- Students may change submitted preferences repeatedly or without authorization.

\- Different colleges and study systems may require different registration procedures.

\- Manual preference processing and allocation can lead to errors.

\- Students may not receive allocation results and next-step information efficiently.



\### The Solution



Secure Department provides a centralized platform for managing the college and department selection process.



The system allows students to:



\- View and select available colleges at the university.

\- Choose the appropriate study system when required.

\- Follow a registration workflow based on their selected college and study system.

\- Retrieve official academic information through university email verification for Regular students.

\- Enter the required personal and academic information, including High School Total, for Credit students.

\- View departments available according to their eligibility.

\- Submit an ordered list of department preferences.

\- Confirm and lock their preferences to prevent unauthorized modifications.

\- Receive their final allocation results and relevant course-registration information.



The system also provides administrators with tools to manage student records, colleges, departments, eligibility requirements, and allocation procedures.



\### Target Audience



\- \*\*Students:\*\* University students who need to select their college, complete the appropriate registration process, submit department preferences, and receive their allocation results.

\- \*\*Administrators:\*\* Authorized university staff responsible for managing student records, college and department configurations, eligibility requirements, allocation, and result publication.



\---



\## 2. Main Workflow



\### Step 1: College Selection



\- The student opens the application.

\- The system displays all available university colleges.

\- The student selects their desired college.



\### Step 2: Study System Selection



If the student selects the Faculty of Computers and Information, the system displays two available study systems:



\- Regular

\- Credit



The registration workflow depends on the selected study system.



\### Step 3: Student Registration



\#### A. Computers and Information — Regular System



1\. The student selects the Regular system.

2\. The student enters their university email.

3\. The system validates the university email.

4\. The system retrieves the student's official academic information from the university database.

5\. The retrieved information includes the student's name, University ID, academic information, and latest GPA.

6\. The student reviews the displayed information.

7\. The system displays the departments available according to the student's eligibility.



\#### B. Computers and Information — Credit System



1\. The student selects the Credit system.

2\. The system requests the required personal and academic information.

3\. The student enters the required information, including their High School Total.

4\. The system validates the submitted information according to the applicable admission requirements.

5\. The system displays the departments available to the student.



\#### C. Other Colleges



For colleges such as Specific Education and Applied Arts:



1\. The student selects the desired college.

2\. The system follows the registration workflow configured for that college.

3\. Students provide the required information if official university records are not available for retrieval.

4\. The system validates the submitted information according to the college's requirements.

5\. The system displays the available departments.



> Note: The exact registration fields and validation rules for each college must be determined according to university requirements.



\### Step 4: Department Preference Submission



1\. The student views the available departments.

2\. The student selects and arranges departments according to personal preferences.

3\. The system validates the selected departments and their priority order.

4\. The student reviews the final preference list.

5\. The student confirms the submission.

6\. Once confirmed, the preferences are saved and locked against further student modifications.



\### Step 5: Department Allocation



1\. The administrator reviews the submitted preferences.

2\. The allocation process is executed or approved according to university rules.

3\. Students are allocated based on the approved GPA, preference, eligibility, and department-capacity rules.

4\. The administrator reviews the allocation results before publishing them.



\### Step 6: Results and Notifications



\- The system publishes the final allocation results.

\- Students can view their assigned departments.

\- Students receive notifications when results become available.

\- The system provides the relevant course-registration link and next steps.



\---



\## 3. Tools and Technologies (Planned Tech Stack)



| Area | Tool / Technology | Purpose |

|---|---|---|

| Mobile Frontend | Flutter | Build the student mobile application |

| Programming Language | Dart | Implement the Flutter application |

| Backend | ASP.NET Core Web API | Implement business logic and REST endpoints |

| ORM | Entity Framework Core | Map application entities to database tables and query data |

| Database | Microsoft SQL Server | Store student records, preferences, departments, and allocation results |

| Authentication | ASP.NET Core Identity / JWT | Authenticate users and authorize protected API requests |

| API Format | REST / JSON | Communication between frontend and backend |

| Transport Security | HTTPS | Protect data during transmission |

| Notifications | Firebase Cloud Messaging (FCM) | Deliver allocation and process notifications |

| API Testing | Postman | Test API endpoints and responses |

| UI/UX | Figma | Design application screens and user flows |

| Version Control | Git / GitHub | Track source code, issues, and team collaboration |

| Admin Interface | Web Dashboard; ASP.NET MVC / Razor is a candidate | Provide administrators with management features |



\*\*Implementation Note:\*\* The technologies above describe the planned architecture. A technology should only be marked as implemented after it has actually been integrated into the repository.



\---



\## 4. Core Security Requirements



\- Student and administrator operations must be authenticated and authorized on the backend.

\- Regular students' university emails must be validated through an appropriate university-approved mechanism.

\- Academic information must only be retrieved through an authorized university data source.

\- Student-submitted information for Credit students and other applicable colleges must be validated on the server.

\- HTTPS must be used for all client-server communication.

\- Student academic and personal information must be protected against unauthorized access.

\- A student must not be able to modify preferences after confirmation.

\- The preference-lock rule must be enforced by the API, not only by the mobile application UI.

\- Sensitive authentication and verification endpoints should use rate limiting and safe error messages.

\- Administrative changes and allocation actions must be recorded in audit logs.

\- Allocation must follow documented and university-approved rules.

\- API keys, credentials, and database connection strings must never be committed to GitHub.



\---



\## 5. Scope Notes



The following requirements need to be clarified or approved by the university before implementation:



\- The official list of university colleges and their available departments.

\- The registration workflow and required fields for each college.

\- Whether Regular students' university email validation is sufficient for retrieving their academic records, or whether an additional verification mechanism is required.

\- The official university database integration method.

\- The exact academic information retrieved for Regular students, including the latest GPA.

\- The required personal and academic information for Credit students.

\- The High School Total validation rules and eligibility thresholds.

\- Department capacity and minimum GPA requirements.

\- The official allocation formula, preference-priority rules, and tie-breaking rules.

\- The notification process and official course-registration links.

\- The administrator permissions and approval process.



These requirements should be based on official university policies and must not be assumed or hard-coded using sample data.

