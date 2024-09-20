Imports System
Imports System.Collections.Generic
Imports System.IO
Imports Newtonsoft.Json

Module Program
    Private employees As New List(Of Employee)
    Private Const DataFilePath As String = "employees.json"

    Sub Main(args As String())
        LoadEmployees()

        Dim choice As Integer
        Do
            Console.Clear()
            DisplayMenu()
            Console.Write("Enter your choice (1-8): ")

            If Integer.TryParse(Console.ReadLine(), choice) Then
                Select Case choice
                    Case 1
                        AddFullTimeEmployee()
                    Case 2
                        AddPartTimeEmployee()
                    Case 3
                        ShowFullTimeEmployees()
                    Case 4
                        ShowPartTimeEmployees()
                    Case 5
                        ShowAllEmployees()
                    Case 6
                        SearchEmployee()
                    Case 7
                        DeleteEmployee()
                    Case 8
                        SaveEmployees()
                        Console.WriteLine("Exiting the system. Goodbye!")
                        Exit Sub

                    Case Else
                        DisplayError("Invalid choice, please try again.")
                End Select
            Else
                DisplayError("Invalid input, please enter a number.")
            End If
        Loop While choice <> 8
    End Sub

    Sub DisplayMenu()
        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("----- Employee Management System -----")
        Console.ResetColor()
        Console.WriteLine("1. Add Full-Time Employee")
        Console.WriteLine("2. Add Part-Time Employee")
        Console.WriteLine("3. Show Full-Time Employees")
        Console.WriteLine("4. Show Part-Time Employees")
        Console.WriteLine("5. Show All Employees")
        Console.WriteLine("6. Search Employee")
        Console.WriteLine("7. Delete Employee")
        Console.WriteLine("8. Save & Exit")
        Console.WriteLine()
    End Sub


    Function GetValidatedString(prompt As String) As String
        Dim input As String
        Do
            Console.Write(prompt)
            input = Console.ReadLine().Trim()
            If Not String.IsNullOrEmpty(input) Then
                Exit Do
            Else
                DisplayError("Input cannot be empty. Please try again.")
            End If
        Loop
        Return input
    End Function

    Function GetValidatedInteger(prompt As String) As Integer
        Dim value As Integer
        Do
            Console.Write(prompt)
            If Integer.TryParse(Console.ReadLine(), value) AndAlso value > 0 Then
                Exit Do
            Else
                DisplayError("Invalid input. Please enter a positive integer.")
            End If
        Loop
        Return value
    End Function

    Function GetValidatedDecimal(prompt As String) As Decimal
        Dim value As Decimal
        Do
            Console.Write(prompt)
            If Decimal.TryParse(Console.ReadLine(), value) AndAlso value >= 0 Then
                Exit Do
            Else
                DisplayError("Invalid input. Please enter a non-negative number.")
            End If
        Loop
        Return value
    End Function


    Sub DisplayError(message As String)
        Console.ForegroundColor = ConsoleColor.Red
        Console.WriteLine(message)
        Console.ResetColor()
    End Sub


    Sub DisplaySuccess(message As String)
        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine(message)
        Console.ResetColor()
    End Sub


    Sub AddFullTimeEmployee()
        Console.Clear()
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.WriteLine("----- Add Full-Time Employee -----")
        Console.ResetColor()

        Dim name As String = GetValidatedString("Enter name: ")
        Dim id As Integer = GetValidatedInteger("Enter ID: ")
        Dim salary As Decimal = GetValidatedDecimal("Enter annual salary: ")


        If employees.Exists(Function(emp) emp.Id = id) Then
            DisplayError("An employee with this ID already exists.")
        Else
            Dim employee As New FullTimeEmployee(name, id, salary)
            employees.Add(employee)
            DisplaySuccess("Full-Time Employee added successfully.")
        End If

        Pause()
    End Sub


    Sub AddPartTimeEmployee()
        Console.Clear()
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.WriteLine("----- Add Part-Time Employee -----")
        Console.ResetColor()

        Dim name As String = GetValidatedString("Enter name: ")
        Dim id As Integer = GetValidatedInteger("Enter ID: ")
        Dim hourlyRate As Decimal = GetValidatedDecimal("Enter hourly rate: ")
        Dim hoursWorked As Decimal = GetValidatedDecimal("Enter hours worked: ")


        If employees.Exists(Function(emp) emp.Id = id) Then
            DisplayError("An employee with this ID already exists.")
        Else
            Dim employee As New PartTimeEmployee(name, id, hourlyRate, hoursWorked)
            employees.Add(employee)
            DisplaySuccess("Part-Time Employee added successfully.")
        End If

        Pause()
    End Sub


    Sub ShowFullTimeEmployees()
        Console.Clear()
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.WriteLine("----- Full-Time Employees -----")
        Console.ResetColor()

        Dim fullTimeEmps = employees.OfType(Of FullTimeEmployee)().ToList()

        If fullTimeEmps.Count = 0 Then
            Console.WriteLine("No full-time employees to display.")
        Else
            For Each emp As FullTimeEmployee In fullTimeEmps
                Console.WriteLine(emp.GetDetails())
            Next
        End If

        Pause()
    End Sub


    Sub ShowPartTimeEmployees()
        Console.Clear()
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.WriteLine("----- Part-Time Employees -----")
        Console.ResetColor()

        Dim partTimeEmps = employees.OfType(Of PartTimeEmployee)().ToList()

        If partTimeEmps.Count = 0 Then
            Console.WriteLine("No part-time employees to display.")
        Else
            For Each emp As PartTimeEmployee In partTimeEmps
                Console.WriteLine(emp.GetDetails())
            Next
        End If

        Pause()
    End Sub


    Sub ShowAllEmployees()
        Console.Clear()
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.WriteLine("----- All Employees -----")
        Console.ResetColor()

        If employees.Count = 0 Then
            Console.WriteLine("No employees to display.")
        Else
            For Each emp As Employee In employees
                Console.WriteLine(emp.GetDetails())
            Next
        End If

        Pause()
    End Sub


    Sub SearchEmployee()
        Console.Clear()
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.WriteLine("----- Search Employee -----")
        Console.ResetColor()

        Console.WriteLine("Search by:")
        Console.WriteLine("1. ID")
        Console.WriteLine("2. Name")
        Console.Write("Enter your choice (1-2): ")

        Dim searchChoice As Integer
        If Integer.TryParse(Console.ReadLine(), searchChoice) Then
            Select Case searchChoice
                Case 1
                    Dim id As Integer = GetValidatedInteger("Enter Employee ID to search: ")
                    Dim emp = employees.Find(Function(e) e.Id = id)
                    If emp IsNot Nothing Then
                        Console.WriteLine(emp.GetDetails())
                    Else
                        DisplayError("Employee not found.")
                    End If
                Case 2
                    Dim name As String = GetValidatedString("Enter Employee Name to search: ")
                    Dim emps = employees.FindAll(Function(e) e.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                    If emps.Count > 0 Then
                        For Each emp In emps
                            Console.WriteLine(emp.GetDetails())
                        Next
                    Else
                        DisplayError("No employees found with the given name.")
                    End If
                Case Else
                    DisplayError("Invalid choice.")
            End Select
        Else
            DisplayError("Invalid input.")
        End If

        Pause()
    End Sub


    Sub DeleteEmployee()
        Console.Clear()
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.WriteLine("----- Delete Employee -----")
        Console.ResetColor()

        Dim id As Integer = GetValidatedInteger("Enter Employee ID to delete: ")
        Dim emp = employees.Find(Function(e) e.Id = id)

        If emp Is Nothing Then
            DisplayError("Employee not found.")
        Else
            employees.Remove(emp)
            DisplaySuccess("Employee deleted successfully.")
            SaveEmployees()
        End If

        Pause()
    End Sub


    Sub Pause()
        Console.WriteLine()
        Console.WriteLine("Press Enter to continue...")
        Console.ReadLine()
    End Sub


    Sub SaveEmployees()
        Try
            Dim settings As New JsonSerializerSettings With {
                .TypeNameHandling = TypeNameHandling.Auto,
                .Formatting = Formatting.Indented
            }
            Dim jsonData As String = JsonConvert.SerializeObject(employees, settings)
            File.WriteAllText(DataFilePath, jsonData)
            DisplaySuccess("Employees saved successfully.")
        Catch ex As Exception
            DisplayError($"Error saving employees: {ex.Message}")
        End Try
    End Sub


    Sub LoadEmployees()
        Try
            If File.Exists(DataFilePath) Then
                Dim jsonData As String = File.ReadAllText(DataFilePath)
                Dim settings As New JsonSerializerSettings With {
                    .TypeNameHandling = TypeNameHandling.Auto
                }
                employees = JsonConvert.DeserializeObject(Of List(Of Employee))(jsonData, settings)
                If employees Is Nothing Then
                    employees = New List(Of Employee)()
                End If
                DisplaySuccess("Employees loaded successfully.")
                Pause()
            End If
        Catch ex As Exception
            DisplayError($"Error loading employees: {ex.Message}")
            employees = New List(Of Employee)()
            Pause()
        End Try
    End Sub
End Module
