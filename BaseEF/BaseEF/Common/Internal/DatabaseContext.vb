Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports System.Data.SQLite
Imports System.Linq
Imports System.Text

Namespace Common.Internal
    Public Class DatabaseContext
        Inherits DbContext

        Public Sub New()
            MyBase.New(New SQLiteConnection() With {
                .ConnectionString = New SQLiteConnectionStringBuilder() With {
                    .DataSource = "base.db",
                    .ForeignKeys = True
                }.ConnectionString
            }, True)
        End Sub

        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            modelBuilder.Conventions.Remove(Of PluralizingTableNameConvention)()
            ' SQLite EF6 does not have initializer implementation
            'Database.SetInitializer(Of DatabaseContext)(New CreateDatabaseIfNotExists(Of DatabaseContext)())

            Database.SetInitializer(Of DatabaseContext)(New SQLiteInitializer(modelBuilder))

            MyBase.OnModelCreating(modelBuilder)
        End Sub

        Public Property ProjectMaster As DbSet(Of Core.Scenario)
        Public Property RingSets As DbSet(Of Core.Ringset)

    End Class

End Namespace
