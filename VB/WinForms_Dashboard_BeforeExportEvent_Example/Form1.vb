Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports System

Namespace WinForms_Dashboard_BeforeExportEvent_Example
    Partial Public Class Form1
        Inherits DevExpress.XtraEditors.XtraForm

        Public Sub New()
            InitializeComponent()
            dashboardDesigner1.CreateRibbon()
            AddHandler dashboardDesigner1.ConfigureDataConnection, AddressOf DashboardDesigner1_ConfigureDataConnection
            AddHandler dashboardDesigner1.CustomizeDashboardTitle, AddressOf DashboardDesigner1_CustomizeDashboardTitle
            dashboardDesigner1.LoadDashboard("Data\Dashboard2Print.xml")
            AddHandler dashboardDesigner1.BeforeExportDocument, AddressOf DashboardDesigner1_BeforeExportDocument
        End Sub

        Private Sub DashboardDesigner1_CustomizeDashboardTitle(ByVal sender As Object, ByVal e As CustomizeDashboardTitleEventArgs)
            Dim btn As DashboardToolbarItem = New DashboardToolbarItem()
            Dim action As Action(Of DashboardToolbarItemClickEventArgs) =
                Sub()
                    dashboardDesigner1.ExportToPdf("test.pdf")
                    System.Diagnostics.Process.Start("test.pdf")
                End Sub
            btn.ClickAction = action
            btn.Caption = "Export to PDF"
            btn.SvgImage = DevExpress.Utils.Svg.SvgImage.FromFile("Action_Export_ToPDF.svg")
            e.Items.Add(btn)
        End Sub

        Private Sub DashboardDesigner1_BeforeExportDocument(ByVal sender As Object, ByVal e As BeforeExportDocumentEventArgs)
            If e.ExportAction = DashboardExportAction.ExportToPdf AndAlso e.PdfExportOptions.ExportFilters = False Then
                e.HideItem(Function(item) TypeOf item Is FilterElementDashboardItem OrElse TypeOf item Is DateFilterDashboardItem)
            End If
        End Sub

        Private Sub DashboardDesigner1_ConfigureDataConnection(ByVal sender As Object, ByVal e As DevExpress.DashboardCommon.DashboardConfigureDataConnectionEventArgs)
            If TypeOf e.ConnectionParameters Is ExtractDataSourceConnectionParameters Then
                Dim extractParams As ExtractDataSourceConnectionParameters = DirectCast(e.ConnectionParameters, ExtractDataSourceConnectionParameters)
                extractParams.FileName = "Data\SalesPerson.dat"
            End If
        End Sub
    End Class
End Namespace
