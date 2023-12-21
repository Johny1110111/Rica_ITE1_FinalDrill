using System;
using System.Collections.Generic;
using System.Linq;

class Person
{
    public string Surname { get; set; }
    public string FirstName { get; set; }

    public Person(string surName, string firstName)
    {
        Surname = surName;
        FirstName = firstName;
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Name: {Surname}, {FirstName}");
    }
}

class Student : Person
{
    public string StudentNumber { get; set; }
    public string Occupation { get; set; }
    public char Gender { get; set; }
    public string PhoneNumber { get; set; }

    public Student(string studentNumber, string surName, string firstName, string occupation, char gender, string phoneNumber)
        : base(surName, firstName)
    {
        StudentNumber = studentNumber;
        Occupation = occupation;
        Gender = gender;
        PhoneNumber = phoneNumber;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Student Number: {StudentNumber}");
        base.DisplayInfo();
        Console.WriteLine($"Occupation: {Occupation}");
        Console.WriteLine($"Gender: {Gender}");
        Console.WriteLine($"Phone Number: {PhoneNumber}");
        Console.WriteLine();
    }
}

class Country
{
    public string Name { get; set; }
    public int CountryCode { get; set; }
    public List<Student> Students { get; }

    public Country(string name, int countryCode)
    {
        Name = name;
        CountryCode = countryCode;
        Students = new List<Student>();
    }

    public void AddStudent(Student student)
    {
        Students.Add(student);
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Country: {Name} Country Code: {CountryCode}");
        Console.WriteLine("Students:");
        foreach (var student in Students)
        {
            student.DisplayInfo();
        }
        Console.WriteLine();
    }
}

class ASEANPhonebook
{
    public List<Country> Countries { get; }

    public ASEANPhonebook()
    {
        Countries = new List<Country>
        {
            new Country("Federation of Malaysia", 60),
            new Country("Republic of Indonesia", 62),
            new Country("Republic of the Philippines", 63),
            new Country("Republic of Singapore", 65),
            new Country("Kingdom of Thailand", 66)
        };
    }

    public void EditEntry(string studentNumber)
    {
        var student = Countries.SelectMany(c => c.Students).FirstOrDefault(s => s.StudentNumber == studentNumber);

        if (student != null)
        {
            Console.WriteLine($"Here is the existing information about {studentNumber}:");
            Console.WriteLine($"{student.FirstName} {student.Surname} is a {student.Occupation}. His number is {student.PhoneNumber}");

            do
            {
                Console.WriteLine("\nEdit Menu:");
                Console.WriteLine("[1] Student Number");
                Console.WriteLine("[2] Surname");
                Console.WriteLine("[3] Gender");
                Console.WriteLine("[4] Occupation");
                Console.WriteLine("[5] Country Code");
                Console.WriteLine("[6] Area Code");
                Console.WriteLine("[7] Phone Number");
                Console.WriteLine("[8] None - Go back to main menu");

                Console.Write("Enter choice (1-8): ");
                string editChoice = Console.ReadLine();

                switch (editChoice)
                {
                    case "1":
                        Console.Write("Enter new student number: ");
                        string newStudentNumber = Console.ReadLine();
                        student.StudentNumber = newStudentNumber;
                        Console.WriteLine("Student number updated successfully.");
                        break;

                    case "2":
                        Console.Write("Enter new surname: ");
                        string newSurname = Console.ReadLine();
                        student.Surname = newSurname;
                        Console.WriteLine("Surname updated successfully.");
                        break;

                    case "3":
                        Console.Write("Enter new gender (M for male, F for female): ");
                        char newGender;
                        while (!char.TryParse(Console.ReadLine(), out newGender) || (newGender != 'M' && newGender != 'F'))
                        {
                            Console.WriteLine("Invalid gender. Please enter 'M' for male or 'F' for female.");
                        }
                        student.Gender = newGender;
                        Console.WriteLine("Gender updated successfully.");
                        break;

                    case "4":
                        Console.Write("Enter new occupation: ");
                        string newOccupation = Console.ReadLine();
                        student.Occupation = newOccupation;
                        Console.WriteLine("Occupation updated successfully.");
                        break;

                    case "5":
                        Console.Write("Enter new country code: ");
                        int newCountryCode;
                        while (!int.TryParse(Console.ReadLine(), out newCountryCode))
                        {
                            Console.WriteLine("Invalid country code. Please enter a valid number.");
                        }
                        Console.WriteLine("Country code cannot be changed once set.");
                        break;

                    case "6":
                        Console.Write("Enter new area code: ");
                        int newAreaCode;
                        while (!int.TryParse(Console.ReadLine(), out newAreaCode))
                        {
                            Console.WriteLine("Invalid area code. Please enter a valid number.");
                        }
                        Console.WriteLine("Area code cannot be changed once set.");
                        break;

                    case "7":
                        Console.Write("Enter new phone number: ");
                        string newPhoneNumber = Console.ReadLine();
                        student.PhoneNumber = newPhoneNumber;
                        Console.WriteLine("Phone number updated successfully.");
                        break;

                    case "8":
                        Console.WriteLine("Back to the main menu.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice! Please enter a number between 1 and 8!");
                        break;
                }

            } while (true);
        }
        else
        {
            Console.WriteLine($"Student with the number {studentNumber} not found in the ASEAN Phonebook.");
        }
    }

    public void SearchByCountry()
    {
        List<Country> selectedCountries = new List<Country>();

        Console.WriteLine("From which country:");
        Console.WriteLine("[1] Malaysia [2] Indonesia [3] Philippines [4] Singapore [5] Thailand [6] ALL [0] No more");

        do
        {
            Console.Write("Enter choice: ");
            int countryChoice;

            if (!int.TryParse(Console.ReadLine(), out countryChoice))
            {
                Console.WriteLine("Invalid choice! Please enter a number between 0 and 6!");
                continue;
            }

            if (countryChoice == 0)
            {
                break;
            }

            if (countryChoice < 1 || countryChoice > 6)
            {
                Console.WriteLine("Invalid choice! Please enter a number between 1 and 6!");
                continue;
            }

            if (countryChoice == 6)
            {
                DisplayStudentsFromCountries(Countries);
                Console.WriteLine("Returning to the main menu.");
                return;
            }

            selectedCountries.Add(Countries[countryChoice - 1]);

        } while (true);

        DisplayStudentsFromCountries(selectedCountries);

        Console.WriteLine("Returning to the main menu.");
    }


    private void DisplayStudentsFromCountries(List<Country> countries)
    {
        List<Student> students = countries.SelectMany(c => c.Students).ToList();
        students.Sort((s1, s2) => string.Compare(s1.Surname, s2.Surname, StringComparison.Ordinal));

        Console.WriteLine("Here are the students from the selected countries:");

        foreach (var student in students)
        {
            Console.WriteLine($"{student.Surname}, {student.FirstName}, with student number {student.StudentNumber}, is a {student.Occupation}. His phone number is {student.PhoneNumber}");
        }
    }

    public void DisplayInfo()
    {
        Console.WriteLine("ASEAN Phonebook:");
        foreach (var country in Countries)
        {
            country.DisplayInfo();
        }
    }
}

class Program
{
    static void Main()
    {
        ASEANPhonebook aseanPhonebook = new ASEANPhonebook();

        do
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Store to ASEAN Phonebook");
            Console.WriteLine("2. Edit entry in ASEAN Phonebook");
            Console.WriteLine("3. Search ASEAN Phonebook by Country");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice (1-4): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    StoreToASEANPhonebook(aseanPhonebook);
                    break;

                case "2":
                    Console.Write("Enter the student number to edit: ");
                    string editStudentNumber = Console.ReadLine();
                    aseanPhonebook.EditEntry(editStudentNumber);
                    break;

                case "3":
                    aseanPhonebook.SearchByCountry();
                    break;

                case "4":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice! Please enter a number between 1 and 4!");
                    break;
            }

        } while (true);
    }

    static void StoreToASEANPhonebook(ASEANPhonebook aseanPhonebook)
    {
        Console.WriteLine("Choose a country from the list:");
        Console.WriteLine("1. Federation of Malaysia");
        Console.WriteLine("2. Republic of Indonesia");
        Console.WriteLine("3. Republic of the Philippines");
        Console.WriteLine("4. Republic of Singapore");
        Console.WriteLine("5. Kingdom of Thailand");

        Console.Write("Enter the number of your choice (1-5): ");
        int countryChoice;
        while (!int.TryParse(Console.ReadLine(), out countryChoice) || countryChoice < 1 || countryChoice > 5)
        {
            Console.WriteLine("Invalid choice! Please enter a number between 1 and 5!");
        }

        int countryCode;
        string countryName;

        switch (countryChoice)
        {
            case 1:
                countryName = "Federation of Malaysia";
                countryCode = 60;
                break;
            case 2:
                countryName = "Republic of Indonesia";
                countryCode = 62;
                break;
            case 3:
                countryName = "Republic of the Philippines";
                countryCode = 63;
                break;
            case 4:
                countryName = "Republic of Singapore";
                countryCode = 65;
                break;
            case 5:
                countryName = "Kingdom of Thailand";
                countryCode = 66;
                break;
            default:
                throw new InvalidOperationException("Invalid country choice.");
        }

        Country country = aseanPhonebook.Countries.FirstOrDefault(c => c.Name == countryName);

        do
        {
            Console.WriteLine($"Enter student information for {countryName} (enter 'done' to finish):");

            Console.Write("Enter student number: ");
            string studentNumber = Console.ReadLine();

            if (studentNumber.ToLower() == "done")
            {
                break;
            }

            Console.Write("Enter surname: ");
            string surName = Console.ReadLine();

            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter occupation: ");
            string occupation = Console.ReadLine();

            Console.Write("Enter gender (M for male, F for female): ");
            char gender;
            while (!char.TryParse(Console.ReadLine(), out gender) || (gender != 'M' && gender != 'F'))
            {
                Console.WriteLine("Invalid gender. Please enter 'M' for male or 'F' for female.");
            }

            Console.Write("Enter area code: ");
            int areaCode;
            while (!int.TryParse(Console.ReadLine(), out areaCode))
            {
                Console.WriteLine("Invalid area code. Please enter a valid number.");
            }

            Console.Write("Enter number: ");
            string number = Console.ReadLine();

            string phoneNumber = $"{countryCode}-{number}";

            Student student = new Student(studentNumber, surName, firstName, occupation, gender, phoneNumber);
            country.AddStudent(student);

            Console.Write("Do you want to add another entry [Y/N]? ");
        } while (Console.ReadLine().ToUpper() == "Y");
    }
}