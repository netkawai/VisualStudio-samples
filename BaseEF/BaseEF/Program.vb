Module Program

    Sub Main()

        Using dbx = New Common.Internal.DatabaseContext
            Dim newScenario = New Core.Scenario
            newScenario.Id = "Scenario 1"
            Dim ringset1 = New Core.Ringset()
            ringset1.Id = "Ringset 1 - Scenario 1"
            Dim ringset2 = New Core.Ringset()
            ringset2.Id = "Ringset 2 - Scenario 1"
            newScenario.Ringsets = New List(Of Core.Ringset)
            newScenario.Ringsets.Add(ringset1)
            newScenario.Ringsets.Add(ringset2)
            dbx.ProjectMaster.Add(newScenario)

            Dim newScenario2 = New Core.Scenario

            newScenario2.Id = "Scenario 2"
            ringset1 = New Core.Ringset()
            ringset1.Id = "Ringset 1 - Scenario 2"
            ringset2 = New Core.Ringset()
            ringset2.Id = "Ringset 2 - Scenario 2"
            newScenario2.Ringsets = New List(Of Core.Ringset)
            newScenario2.Ringsets.Add(ringset1)
            newScenario2.Ringsets.Add(ringset2)
            dbx.ProjectMaster.Add(newScenario2)

            dbx.SaveChanges()


            For Each ringset In dbx.RingSets
                Console.WriteLine("{0} , {1}", ringset.Id, ringset.Scenario.Id)
            Next


        End Using


    End Sub

End Module
