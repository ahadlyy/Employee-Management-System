Public Class PartTimeEmployee
    Inherits Employee

    Private _hourlyRate As Decimal
    Private _hoursWorked As Decimal


    Public Sub New(name As String, id As Integer, hourlyRate As Decimal, hoursWorked As Decimal)
        MyBase.New(name, id, hourlyRate * hoursWorked)
        _hourlyRate = hourlyRate
        _hoursWorked = hoursWorked
    End Sub

    Public Overrides Function GetDetails() As String
        'Return MyBase.GetDetials()
        Return $"Part Time Employee:- [Employee name: {Name}, Employee id: {Id}, Hourly Rate: {_hourlyRate}, Houres Worked: {_hoursWorked}, Total pay: {Salary}]"
    End Function

End Class
