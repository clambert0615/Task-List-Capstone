using System;
using System.Collections.Generic;
using System.Globalization;

namespace Task_List_Capstone
{
    class Task
    {
        //fields

        private string teamMemberName;
        private string taskDescription;
        private DateTime dueDate;
        private bool completion;

        // properties
        public string TeamMemberName
        {
            get { return teamMemberName; }
            set { teamMemberName = value; }
        }

        public string TaskDescription
        {
            get { return taskDescription; }
            set { taskDescription = value; }

        }

        public DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }

        public bool Completion
        {
            get { return completion; }
            set { completion = value; }
        }

        // constructors
        public Task()
        {
            completion = false;
        }

        public Task(string _teamMemberName, string _taskdescription, DateTime _dueDate)
        {
            teamMemberName = _teamMemberName;
            taskDescription = _taskdescription;
            dueDate = _dueDate;
            completion = false;
        }

        //methods

        public static List<Task> GetTaskList()
        {
            List<Task> taskList = new List<Task>
            {
                new Task("Adam Smith", "Update Student Workbooks", DateTime.Parse("4/15/2020")),
                new Task("Jill Jones", "Make Chapter 7 Test", DateTime.Parse("5/01/2020")),
                new Task("Sara White", "Make Final Exam", DateTime.Parse("5/07/2020")),
                new Task("Pete Black", "Grade Chapter 7 Test", DateTime.Parse("5/06/2020")),
                new Task("Adam Smith", "Grade Final Exam", DateTime.Parse("5/23/2020")),
                new Task("Jill Jones", "Compute Final Grade", DateTime.Parse("5/28/2020"))
            };

            return taskList;

        }

        public void PrintListInfo()
        {
            string completionText;

            if (completion)
            {
                completionText = "Yes";
            }
            else
            {
                completionText = "No";
            }

            string taskDue = dueDate.ToString("d");

            Console.WriteLine($"\t Team Member Name: {teamMemberName} \n \t Task Description: {taskDescription} \n \t Due Date: {taskDue} \n \t Completion Status: {completionText}");
        }

        public static void AddTask(List<Task> taskList)
        {
            DateTime taskDate = DateTime.Now;

            while (true)
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                Console.WriteLine("Who is the team member responsible for this task?");
                string member = Console.ReadLine();
                member = textInfo.ToTitleCase(member);

                if (string.IsNullOrEmpty(member))
                {
                    Console.WriteLine("Invalid input try again");
                    continue;
                }
                
                Console.WriteLine("What is the task description?");
                string description = Console.ReadLine().ToLower().Trim();
                if (string.IsNullOrEmpty(description))
                {
                    Console.WriteLine("Invalid input try again.");
                    continue;
                }
                try
                {
                    Console.WriteLine("What is the due date?");
                    taskDate = DateTime.Parse(Console.ReadLine());
                }
                catch(FormatException)
                {
                    Console.WriteLine("Invalid date try agian.");
                    continue;
                }


                if(taskDate == DateTime.MinValue)
                {
                    Console.WriteLine("Invalid input try again");
                    continue;
                }
                
                    taskList.Add(new Task(member, description, taskDate));
                    break;
                
            }
        }

        public static void DeleteTask(List<Task> taskList)
        {
            while (true)
            {
                int numToDelete = 0;
                int taskToDelete;

                try
                {
                    Console.WriteLine("Which task would you like to delete?");
                    numToDelete = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("This is not a number. Try again");
                    continue;
                }
                if (numToDelete > 0 && numToDelete <= taskList.Count)
                {
                    taskToDelete = numToDelete - 1;
                    Task task = taskList[taskToDelete];
                    string completionText;

                    if (task.completion)
                    {
                        completionText = "Yes";
                    }
                    else
                    {
                        completionText = "No";
                    }

                    string taskDue = task.dueDate.ToString("d");
                    Console.WriteLine($"Are you sure you want to delete the following task? y or n \n \t Team Member Name: {task.teamMemberName} \n \t Task Description: {task.taskDescription} \n \t Due Date: {taskDue} \n \t Completion: {completionText} ");
                    string deleteDecision = Console.ReadLine().ToLower().Trim();
                    if (deleteDecision == "y")
                    {
                        taskList.RemoveAt(numToDelete - 1);
                        break;
                    }
                    else if (deleteDecision == "n")
                    {
                        Console.WriteLine("Returning to main menu");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Try again");
                        continue;
                    }

                }
                else
                {
                    Console.WriteLine("The number is not in the range.  Try again");
                    continue;
                }
            }
        }

        public static int GetTaskNumber(List <Task> taskList)
        {
            int numToMark;
            int indexToMark;

            while (true)
            {
                try
                { 
                Console.WriteLine("What task number do you want to mark as complete?");
                    numToMark = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("This is not a number. Try again");
                    continue;
                }
                if (numToMark > 0 && numToMark <= taskList.Count)
                {
                    indexToMark = numToMark - 1;
                    return indexToMark;
                }
                else
                {
                    Console.WriteLine("Number is not in range. Try again");
                    continue;
                }
            }
        }

        public static void MarkComplete(List<Task> taskList)
        {

            
            int taskToMarkComplete = Task.GetTaskNumber(taskList);

            while (true)
            {
                string markDecision = Task.VerifyCompletion(taskList, taskToMarkComplete);

                Task task = taskList[taskToMarkComplete];
                if (markDecision == "y")
                {
                    task.completion = true;
                    break;
                }
                else if (markDecision == "n")
                {
                    Console.WriteLine("Returning to main menu");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again");
                    continue;
                }
            }
        }

        public static string VerifyCompletion(List<Task>taskList, int taskToMarkComplete)
        {
            Task task = taskList[taskToMarkComplete];
            string completionText;

            if (task.completion)
            {
                completionText = "Yes";
            }
            else
            {
                completionText = "No";
            }

            string taskDue = task.dueDate.ToString("d");
            Console.WriteLine($"Are you sure you want to mark the following task as completed? y or n \n \t Team Member Name: {task.teamMemberName} \n \t Task Description: {task.taskDescription} \n \t Due Date: {taskDue} \n \t Completion: {completionText} ");
            string markDecision = Console.ReadLine().ToLower().Trim();
            return markDecision;
        }
        
        public static int  GetTaskToEdit(List<Task> taskList)
        {
            int numToEdit;
            int indexToEdit;

            while (true)
            {
                try
                {
                    Console.WriteLine("What task number do you want to edit?");
                    numToEdit = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("This is not a number. Try again");
                    continue;
                }
                if (numToEdit > 0 && numToEdit <= taskList.Count)
                {
                    indexToEdit= numToEdit - 1;
                    return indexToEdit;
                }
                else
                {
                    Console.WriteLine("Number is not in range. Try again");
                    continue;
                }
            }
        }
    }
        
    
}
