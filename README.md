\# Secure Department



Secure Department is a secure university department-selection and allocation system that allows students to choose their college, submit department preferences according to their academic eligibility, and receive their final allocation results through a controlled and transparent workflow.



\## 1. Problem



Traditional department-selection processes may face several challenges:



\- Unauthorized access or student impersonation.

\- Incorrect student information during preference submission.

\- Difficulty verifying student eligibility.

\- Repeated or unauthorized changes to submitted preferences.

\- Manual allocation errors and difficulty tracking allocation decisions.

\- Delays in informing students about their allocation results and next steps.



\## 2. Proposed Solution



Secure Department provides a structured and secure platform for managing the department-selection process.



The system allows students to select their college, follow the appropriate registration workflow, view eligible departments, and submit their preferences.



It also provides administrators with tools to manage academic information, monitor submissions, and control the allocation process according to university rules.



\## 3. Student Workflow



\### Step 1: College Selection



\- The student opens the application.

\- The system displays the available colleges at the university.

\- The student selects their desired college.



\### Step 2: Study System Selection



If the student selects \*\*Computers and Information\*\*, the system displays two study systems:



\- Regular

\- Credit



The workflow depends on the selected study system.



\### A. Computers and Information – Regular System



1\. The student selects the Regular system.

2\. The student enters their university email.

3\. The system validates the university email.

4\. If the email is valid, the system retrieves the student's academic information from the university database.

5\. The retrieved information includes:

&#x20;  - Student Name

&#x20;  - University ID

&#x20;  - Academic Information

&#x20;  - Latest GPA

6\. The student reviews the displayed information.

7\. The system displays the available departments according to the student's eligibility.

8\. The student selects and arranges departments according to their preferences.

9\. The student reviews the final preference list and confirms the submission.

10\. Once confirmed, the preferences are saved and locked against further student modifications.



\### B. Computers and Information – Credit System



1\. The student selects the Credit system.

2\. The system requests the required personal and academic information.

3\. The student enters their information, including their High School Total.

4\. The system validates the submitted information and checks the applicable eligibility requirements.

5\. The system displays the departments available to the student.

6\. The student selects and arranges departments according to their preferences.

7\. The student reviews the final preference list and confirms the submission.

8\. Once confirmed, the preferences are saved and locked against further student modifications.



\### C. Other Colleges



For colleges such as Specific Education and Applied Arts:



1\. The student selects the desired college.

2\. The system follows the registration workflow defined for that college.

3\. Students enter the required information if official university records are not available for retrieval.

4\. The system validates the submitted information according to the college's requirements.

5\. The available departments are displayed.

6\. The student arranges their preferences and confirms the submission.

7\. The preferences are locked after confirmation.



> Note: The exact registration fields and eligibility rules for each college must be configured according to university requirements.



\### Step 3: Preference Confirmation



For all supported student workflows:



\- Students can review their selected departments before submission.

\- The system validates the selected departments and their priority order.

\- Students must explicitly confirm their final preferences.

\- After confirmation, students cannot modify or reorder their preferences through the normal application workflow.

\- The backend enforces the preference-locking rule.



\### Step 4: Allocation Results



After the allocation process:



\- Students can view their final allocated department.

\- The system sends notifications when results are published.

\- Students can access the relevant course-registration link and next steps.



\## 4. Admin Workflow



The system provides an administrative dashboard that allows authorized administrators to:



\- Manage student records and review eligibility information.

\- Manage colleges and their associated departments.

\- Configure department capacities and required GPA rules.

\- View submitted student preferences.

\- Monitor the preference-submission process.

\- Run or approve department allocation according to university rules.

\- Review allocation results before publishing them.

\- Publish final results and send student notifications.

\- Review audit records for important administrative actions.



\## 5. Main Features



\- College selection and study-system selection.

\- Different registration workflows for Regular and Credit students.

\- University email validation for Regular students.

\- Academic information retrieval from the university database.

\- Student information entry and High School Total validation for Credit students.

\- College-specific department availability.

\- GPA- and capacity-based eligibility and allocation rules.

\- Ordered department preferences.

\- Preference review, confirmation, and locking.

\- Admin dashboard for managing academic information.

\- Student allocation results and notifications.

\- Course-registration links and next-step information.

\- Backend validation, authorization, and auditability.



\## 6. User Roles



| Role | Main Permissions |

|---|---|

| Student | Select a college and study system, complete the required registration process, view eligible departments, submit preferences, and view allocation results. |

| Admin | Manage student records, colleges, departments, eligibility rules, capacities, preferences, allocation, and published results. |



\## 7. Tech Stack



The planned implementation stack is:



| Layer | Technology |

|---|---|

| Mobile Application | Flutter, Dart |

| Backend API | ASP.NET Core Web API |

| ORM / Data Access | Entity Framework Core |

| Database | Microsoft SQL Server |

| Authentication / Authorization | ASP.NET Core Identity and/or JWT Bearer Authentication |

| API Communication | REST API, JSON, HTTPS |

| Push Notifications | Firebase Cloud Messaging (FCM) |

| API Testing | Postman |

| UI/UX Design | Figma |

| Version Control | Git and GitHub |

| Admin Interface | Web Dashboard (ASP.NET MVC / Razor is a proposed option) |



> Note: This is the planned technology stack. Technologies and implementation details may be updated as the project develops.



\## 8. High-Level Architecture



```text

&#x20;                Student

&#x20;                   |

&#x20;                   v

&#x20;            Flutter Mobile App

&#x20;                   |

&#x20;                   | HTTPS / REST / JSON

&#x20;                   v

&#x20;             ASP.NET Core API

&#x20;                   |

&#x20;       +-----------+------------+

&#x20;       |           |            |

&#x20;       v           v            v

&#x20;  Authentication  Student    Preference

&#x20;  \& Authorization Verification Management

&#x20;       |           |            |

&#x20;       +-----------+------------+

&#x20;                   |

&#x20;                   v

&#x20;            Allocation Logic

&#x20;                   |

&#x20;                   v

&#x20;            Entity Framework Core

&#x20;                   |

&#x20;                   v

&#x20;            Microsoft SQL Server

&#x20;                   |

&#x20;                   v

&#x20;             Allocation Results

&#x20;                   |

&#x20;                   v

&#x20;          Firebase Cloud Messaging

&#x20;                   |

&#x20;                   v

&#x20;              Student Device



&#x20;       Admin Web Dashboard

&#x20;                   |

&#x20;                   | HTTPS / REST

&#x20;                   v

&#x20;             ASP.NET Core API

```



\## 9. Security Considerations



\- Enforce authentication and role-based authorization on the backend.

\- Validate university email addresses for Regular students.

\- Retrieve academic information only through an authorized university data source.

\- Validate student-submitted information for Credit students.

\- Use HTTPS for all client-server communication.

\- Never rely only on hiding or disabling UI controls to protect submitted preferences.

\- Validate preference ownership and submission status on the backend.

\- Enforce the confirmed-preferences lock in backend logic and, where appropriate, database constraints.

\- Apply rate limiting to sensitive verification and authentication endpoints.

\- Protect student academic information and avoid exposing unnecessary personal data.

\- Record important administrative actions and allocation changes in audit logs.

\- Protect API keys, credentials, and connection strings; never commit them to GitHub.



\## 10. Target Audience



\- University Students: Students who need to select their college, choose their study system when applicable, submit department preferences, and receive allocation results.

\- University Administrators: Authorized staff responsible for managing student information, academic departments, eligibility requirements, and allocation procedures.



\## 11. Project Goals



\- Simplify and organize the university department-selection process.

\- Provide different registration workflows according to student study systems.

\- Improve the accuracy of student information and eligibility checks.

\- Prevent unauthorized modifications after preference confirmation.

\- Reduce manual allocation errors.

\- Improve transparency and communication between students and university administrators.



\## 12. Disclaimer



Secure Department is a university project. Any integration with official university databases, student academic records, or university email systems must be authorized by the university and comply with its security and privacy policies.

