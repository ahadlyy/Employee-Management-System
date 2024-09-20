Public Class Employee

    Private _name As String
    Private _id As Integer
    Private _salary As Decimal


    Public Sub New(name As String, id As Integer, salary As Decimal)
        _name = name
        _id = id
        _salary = salary
    End Sub

    Public Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set

    End Property

    Public Property Id As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property

    Public Property Salary As Decimal
        Get
            Return _salary
        End Get
        Set(value As Decimal)
            _salary = value
        End Set
    End Property

    Public Overridable Function GetDetails() As String
        Return $"Employee name: {_name}, Employee id: {_id}, Employee salary: {_salary}"
    End Function

End Class
