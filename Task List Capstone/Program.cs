using System;
using System.Collections.Generic;
using System.Globalization;

namespace Task_List_Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Task Manager!");
            Console.WriteLine();
            List<Task> taskList = Task.GetTaskList();
           
            bool askAgain = true;
            while (askAgain)

            {
                int menuChoice = GetUserInput("Choose a number. \n \t 1. Lists Tasks \n \t 2. Add Task \n \t 3. Delete task \n \t 4. Mark Task Complete \n \t 5. Edit Task \n \t 6. List Task by Team Member \n \t 7. List Tasks Before a Due Date \n \t 8. Exit Program");


                if (menuChoice == 1)
                {
                    int taskNumber = 0;

                    for (int i = 0; i < taskList.Count; i++)
                    {
                        Console.WriteLine();
                        taskNumber += 1;
                        Console.WriteLine($"Task number:  { taskNumber}");
                        taskList[i].PrintListInfo();
                        Console.WriteLine();
                    }
                }
                else if (menuChoice == 2)
                {
                    Task.AddTask(taskList);
                    Console.WriteLine();
                }
                else if (menuChoice == 3)
                {
                    Task.DeleteTask(taskList);
                }
                else if (menuChoice == 4)
                {
                    Task.MarkComplete(taskList);
                }
                else if (menuChoice == 5)
                {
                    int taskToEdit = Task.GetTaskToEdit(taskList);
                    EditTask(taskToEdit, taskList);

                }
                else if (menuChoice == 6)
                {
                    bool getName = true;
                    while (getName)
                    {
                        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                        Console.WriteLine("What team member do you want to see tasks for?");
                        string teamMember = Console.ReadLine();
                        teamMember = textInfo.ToTitleCase(teamMember);
                        bool isFound = false; 
                        
                       for (int i = 0; i < taskList.Count; i++)

                        {
                            if (teamMember == taskList[i].TeamMemberName)
                            {

                                taskList[i].PrintListInfo();
                                Console.WriteLine();
                                isFound = true;
                            }
                               
                        }
                        if (isFound)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Team member not found.  Enter a valid name.");

                        }
                    }
                }
                else if (menuChoice == 7)
                {
                    DateTime userDate = DateTime.MinValue;
                    bool getDate = true;
                    while (getDate)
                    {

                        try
                        {
                        
                        Console.WriteLine("Enter the due date you want to see tasks before");
                         userDate = DateTime.Parse(Console.ReadLine());
                         }
                        catch(FormatException)
                        {
                            Console.WriteLine("This is not a valid date. Try again.");
                            continue;
                        }
                        
                        bool dateFound = false;

                        Console.WriteLine("These are the tasks with due dates before the date you entered:");
                        for (int i = 0; i < taskList.Count; i++)
                        {
                            int result = DateTime.Compare(userDate, taskList[i].DueDate);
                            if (result > 0)
                            {
                                taskList[i].PrintListInfo();
                                Console.WriteLine();
                                dateFound = true;
                            }

                        }
                        if (dateFound)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("No tasks were found with dates before the date you entered.");
                        }
                    }
                }
                else if (menuChoice == 8)
                {
                    Console.WriteLine("Quitting Task Manager. Goodbye!");
                    break;
                }
            }

        }
        //methods

        public static int GetUserInput(string message)
        {
            while (true)
            {
                int input = 0;

                try
                {
                    Console.WriteLine(message);
                    input = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("This is not a number.  Try again");
                    continue;
                }

                if (input >= 1 && input <= 8)
                {
                    return input;

                }

                else
                {
                    Console.WriteLine("This number is not between 1 and 8.  Try again");
                    continue;
                }

            }
        }

        public static void EditTask(int taskToEdit, List<Task> taskList)
        {
            bool keepAsking = true;
            while (keepAsking)
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                Console.WriteLine("What would you like to edit?  \n \t To edit team member name enter: name \n \t To edit task description enter: description \n \t To edit due date enter: date \n \t To edit completion status enter: completion");
                string userEdit = Console.ReadLine().ToLower().Trim();
                

                if (userEdit == "name" || userEdit == "description" || userEdit == "date" || userEdit == "completion")
                {
                    if (userEdit == "name")
                    {
                        Console.WriteLine("Who is the new team member responsible for this task?");
                        string newName = Console.ReadLine();
                        newName = textInfo.ToTitleCase(newName);

                        taskList[taskToEdit].TeamMemberName = newName;
                        if(string.IsNullOrEmpty(newName))
                        {
                            Console.WriteLine("You did not enter anything. Please try again.");
                        }

                    }
                    else if (userEdit == "description")
                    {
                        Console.WriteLine("What would you like to the new description to be?");
                        string newDescription = Console.ReadLine().Trim();
                        taskList[taskToEdit].TaskDescription = newDescription;
                        if(string.IsNullOrEmpty(newDescription))
                        {
                            Console.WriteLine("You must enter something. Try again.");
                        }
                    }
                    else if (userEdit == "date")
                    {
                       try
                            {
                            Console.WriteLine("What would you like the new due date to be?");
                            DateTime newDueDate = DateTime.Parse(Console.ReadLine());
                            taskList[taskToEdit].DueDate = newDueDate;
                        }
                        catch(FormatException)
                        {
                            Console.WriteLine("This is not a valid date, try again.");
                        }
                    }
                    else if (userEdit == "completion")
                    {
                        while (true)
                        {
                            Console.WriteLine("What would you like the completion status to be? \n \t Enter complete or incomplete");
                            string completionDecision = Console.ReadLine().ToLower().Trim();
                            if (completionDecision == "complete")
                            {
                                taskList[taskToEdit].Completion = true;
                                break;
                            }
                            else if (completionDecision == "incomplete")
                            {
                                taskList[taskToEdit].Completion = false;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input.  Please try again");
                                continue;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.  Please try again.");
                        continue;
                    }

                }
                else
                {
                    Console.WriteLine("Invalid input. Try again");
                    continue;
                }
                keepAsking = KeepEditing();
            }

        }
        public static bool KeepEditing()
        {
            while (true)
            {
                Console.WriteLine("Would you like to make another edit for this task? y or n");
                string continueDecision = Console.ReadLine().ToLower().Trim();
                if (continueDecision == "y")
                {
                    return true;
                }
                else if (continueDecision == "n")
                {
                    Console.WriteLine("Done Editing");
                    return false;
                }
                else
                {
                    continue;
                }
            }
        }

        

    }
}

