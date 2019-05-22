using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using System;

namespace WinForms_Dashboard_BeforeExportEvent_Example
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
            dashboardDesigner1.CreateRibbon();
            dashboardDesigner1.ConfigureDataConnection += DashboardDesigner1_ConfigureDataConnection;
            dashboardDesigner1.CustomizeDashboardTitle += DashboardDesigner1_CustomizeDashboardTitle;
            dashboardDesigner1.LoadDashboard(@"Data\Dashboard2Print.xml");
            dashboardDesigner1.BeforeExportDocument += DashboardDesigner1_BeforeExportDocument;
        }

        private void DashboardDesigner1_CustomizeDashboardTitle(object sender, CustomizeDashboardTitleEventArgs e)
        {
            e.Items.Add(new DashboardToolbarItem()
            {
                ClickAction = new Action<DashboardToolbarItemClickEventArgs>((args) =>
                {
                    dashboardDesigner1.ExportToPdf("test.pdf");
                    System.Diagnostics.Process.Start("test.pdf");
                }
            ),
                Caption = "Export to PDF",
                SvgImage = DevExpress.Utils.Svg.SvgImage.FromFile(@"Action_Export_ToPDF.svg")
            });
        }
        private void DashboardDesigner1_BeforeExportDocument(object sender, BeforeExportDocumentEventArgs e)
        {
            if (e.ExportAction == DashboardExportAction.ExportToPdf &&
                e.PdfExportOptions.ExportFilters == false)
            {
                e.HideItem(item => item is FilterElementDashboardItem ||
                item is DateFilterDashboardItem);
            }
        }

        private void DashboardDesigner1_ConfigureDataConnection(object sender, DevExpress.DashboardCommon.DashboardConfigureDataConnectionEventArgs e)
        {
            if (e.ConnectionParameters is ExtractDataSourceConnectionParameters extractParams)
                extractParams.FileName = @"Data\SalesPerson.dat";
        }
    }
}
