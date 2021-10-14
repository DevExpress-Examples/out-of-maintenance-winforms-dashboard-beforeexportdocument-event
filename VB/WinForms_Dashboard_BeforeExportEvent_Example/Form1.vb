Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports System

Namespace WinForms_Dashboard_BeforeExportEvent_Example

    Public Partial Class Form1
        Inherits DevExpress.XtraEditors.XtraForm

        Public Sub New()
            InitializeComponent()
            dashboardDesigner1.CreateRibbon()
            Me.dashboardDesigner1.ConfigureDataConnection += AddressOf DashboardDesigner1_ConfigureDataConnection
            Me.dashboardDesigner1.CustomizeDashboardTitle += AddressOf DashboardDesigner1_CustomizeDashboardTitle
            dashboardDesigner1.LoadDashboard("Data\Dashboard2Print.xml")
            Me.dashboardDesigner1.BeforeExportDocument += AddressOf DashboardDesigner1_BeforeExportDocument
        End Sub

        Private Sub DashboardDesigner1_CustomizeDashboardTitle(ByVal sender As Object, ByVal e As CustomizeDashboardTitleEventArgs)
            e.Items.Add(New DashboardToolbarItem() With {.ClickAction = New Action(Of DashboardToolbarItemClickEventArgs)(Sub(args)
                dashboardDesigner1.ExportToPdf("test.pdf")
                System.Diagnostics.Process.Start("test.pdf")
            End Sub), .Caption = "Export to PDF", .SvgImage = DevExpress.Utils.Svg.SvgImage.FromFile("Action_Export_ToPDF.svg")})
        End Sub

        Private Sub DashboardDesigner1_BeforeExportDocument(ByVal sender As Object, ByVal e As BeforeExportDocumentEventArgs)
            If e.ExportAction Is DashboardExportAction.ExportToPdf AndAlso e.PdfExportOptions.ExportFilters = False Then
                e.HideItem(Function(item) TypeOf item Is FilterElementDashboardItem OrElse TypeOf item Is DateFilterDashboardItem)
            End If
        End Sub

        Private Sub DashboardDesigner1_ConfigureDataConnection(ByVal sender As Object, ByVal e As DevExpress.DashboardCommon.DashboardConfigureDataConnectionEventArgs)
            Dim extractParams As ExtractDataSourceConnectionParameters = Nothing
            If CSharpImpl.__Assign(extractParams, TryCast(e.ConnectionParameters, ExtractDataSourceConnectionParameters)) IsNot Nothing Then extractParams.FileName = "Data\SalesPerson.dat"
        End Sub

        Private Class CSharpImpl

            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class
End Namespace
