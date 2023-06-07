Imports DevExpress.DashboardAspNetCore
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWeb
Imports DevExpress.DataAccess.Excel
Imports DevExpress.DataAccess.Sql
Imports Microsoft.Extensions.FileProviders

Namespace CSPDashboardExample

    Public Module DashboardUtils

        Public Function CreateDashboardConfigurator(ByVal configuration As IConfiguration, ByVal fileProvider As IFileProvider) As DashboardConfigurator
            Dim configurator As DashboardConfigurator = New DashboardConfigurator()
            configurator.SetConnectionStringsProvider(New DashboardConnectionStringsProvider(configuration))
            Dim dashboardFileStorage As DashboardFileStorage = New DashboardFileStorage(fileProvider.GetFileInfo("Data/Dashboards").PhysicalPath)
            configurator.SetDashboardStorage(dashboardFileStorage)
            Dim dataSourceStorage As DataSourceInMemoryStorage = New DataSourceInMemoryStorage()
            ' Registers an SQL data source.
            Dim sqlDataSource As DashboardSqlDataSource = New DashboardSqlDataSource("SQL Data Source", "NWindConnectionString")
            sqlDataSource.DataProcessingMode = DataProcessingMode.Client
            Dim query As SelectQuery = SelectQueryFluentBuilder.AddTable("Categories").SelectAllColumnsFromTable().Join("Products", "CategoryID").SelectAllColumnsFromTable().Build("Products_Categories")
            sqlDataSource.Queries.Add(query)
            dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml())
            ' Registers an Object data source.
            Dim objDataSource As DashboardObjectDataSource = New DashboardObjectDataSource("Object Data Source")
            objDataSource.DataId = "Object Data Source Data Id"
            dataSourceStorage.RegisterDataSource("objDataSource", objDataSource.SaveToXml())
            ' Registers an Excel data source.
            Dim excelDataSource As DashboardExcelDataSource = New DashboardExcelDataSource("Excel Data Source")
            excelDataSource.ConnectionName = "Excel Data Source Connection Name"
            excelDataSource.SourceOptions = New ExcelSourceOptions(New ExcelWorksheetSettings("Sheet1"))
            dataSourceStorage.RegisterDataSource("excelDataSource", excelDataSource.SaveToXml())
            configurator.SetDataSourceStorage(dataSourceStorage)
            configurator.DataLoading += Function(s, e)
                If e.DataId Is "Object Data Source Data Id" Then
                    e.Data = Invoices.CreateData()
                End If
            End Function
            configurator.ConfigureDataConnection += Function(s, e)
                If e.ConnectionName Is "Excel Data Source Connection Name" Then
                    Dim excelParameters As ExcelDataSourceConnectionParameters = CType(e.ConnectionParameters, ExcelDataSourceConnectionParameters)
                    excelParameters.FileName = fileProvider.GetFileInfo("Data/Sales.xlsx").PhysicalPath
                End If
            End Function
            Return configurator
        End Function
    End Module
End Namespace
