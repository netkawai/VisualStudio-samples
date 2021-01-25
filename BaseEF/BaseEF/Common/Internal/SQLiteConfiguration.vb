Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Data.Entity.Core.Common
Imports System.Data.SQLite
Imports System.Data.SQLite.EF6
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace Common.Internal
    Public Class SQLiteConfiguration
        Inherits DbConfiguration
        Public Sub New()
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance)
            SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance)
            SetProviderServices("System.Data.SQLite", CType(SQLiteProviderFactory.Instance.GetService(GetType(DbProviderServices)), DbProviderServices))
        End Sub
    End Class

End Namespace
