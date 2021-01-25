Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports SQLite.CodeFirst
Imports SQLite.CodeFirst.Console.Entity

Namespace Common.Internal
    Public Class SQLiteInitializer
        Inherits SqliteDropCreateDatabaseWhenModelChanges(Of DatabaseContext)
        Public Sub New(modelBuilder As DbModelBuilder)
            MyBase.New(modelBuilder)
        End Sub




    End Class

End Namespace

