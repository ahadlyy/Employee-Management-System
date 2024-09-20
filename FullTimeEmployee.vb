Imports System.Security.Cryptography

Public Class FullTimeEmployee
    Inherits Employee

    Public Sub New(name As String, id As Integer, salary As Decimal)
        MyBase.New(name, id, salary)
    End Sub

    Public Overrides Function GetDetails() As String
        '//Return MyBase.GetDetials()
        Return $"Full Time Employee:- [Employee name: {Name}, Employee id: {Id}, Employee annual salary: {Salary}]"
    End Function
End Class
