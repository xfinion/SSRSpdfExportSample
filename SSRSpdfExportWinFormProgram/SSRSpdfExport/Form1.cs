using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SSRSpdfExport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Microsoft.Reporting.WinForms.ReportViewer reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();

            // Set the processing mode for the ReportViewer to Local  
            reportViewer.ProcessingMode = ProcessingMode.Local;

            LocalReport localReport = reportViewer.LocalReport;

            localReport.ReportPath = "CHARTI.rdlc";

            DataSet dataset = new DataSet("CHRTIDataSet");

            // Get the sales order data  
            GetCHRTIDataSet(ref dataset);

            // Create a report data source for the sales order data  
            ReportDataSource chrtIdataSource = new ReportDataSource();
            chrtIdataSource.Name = "CHRTIDataSet";
            chrtIdataSource.Value = dataset.Tables["CHRTIDataSet"];

            localReport.DataSources.Add(chrtIdataSource);

            // Get the sales order detail data  
            GetFootNoteDataSet(ref dataset);

            // Create a report data source for the sales order detail   
            // data  
            ReportDataSource chrtIFootNote =
                new ReportDataSource();
            chrtIFootNote.Name = "FootNoteDataSet";
            chrtIFootNote.Value = dataset.Tables["FootNoteDataSet"];

            localReport.DataSources.Add(chrtIFootNote);

            ////// Create a report parameter for the report   
            //ReportParameter reportParameterChartTitle = new ReportParameter("ChartTtle", "CHART I: COMMAND ORGANIZATION OF THE LAND FORCES AND AIR AND AIR DEFENSE AVIATION FORCES OF United States of America");
            //ReportParameter reportParameterNORTH = new ReportParameter("NORTH", "N");
            //ReportParameter reportParameterSOUTH = new ReportParameter("SOUTH", "S");
            //ReportParameter reportParameterEAST = new ReportParameter("EAST", "E");
            //ReportParameter reportParameterWEST = new ReportParameter("WEST", "W");
            //ReportParameter reportParameterPERDISAG = new ReportParameter("PERDISAG", "D");
            //ReportParameter reportParameterPrintInfo = new ReportParameter("PrintInfo", "CFE 2017: print date 03-Oct-17");
            //ReportParameter reportParameterValidAsOf = new ReportParameter("ValidAsOf", "VALID AS OF: 1 JANUARY 2017");
            //ReportParameter reportParameterStateNames = new ReportParameter("StateNames", "true");
            //ReportParameter reportParameterLocationAndPersonnel = new ReportParameter("LocationAndPersonnel", "true");
            //ReportParameter reportParameterText8 = new ReportParameter("Text8", "LINE NUMBER-1");
            //ReportParameter reportParameterText9 = new ReportParameter("Text9", "FORMATION OR UNIT RECORD Number");
            //ReportParameter reportParameterText10 = new ReportParameter("Text10", "DESIGNATION OF FORMATION OR UNIT");
            //ReportParameter reportParameterText11 = new ReportParameter("Text11", "SUBORDINATION");
            //ReportParameter reportParameterText14 = new ReportParameter("Text14", "PEACETIME LOCATION");
            //ReportParameter reportParameterText15 = new ReportParameter("Text15", "NUMBER OF PERSONNEL");
            //ReportParameter reportParameterText12 = new ReportParameter("Text12", "1ST HIGHER ECHELON");
            //ReportParameter reportParameterText13 = new ReportParameter("Text13", "2ND HIGHER ECHELON");
            //ReportParameter reportParameterFont = new ReportParameter("Font", "Courier New");
            //ReportParameter reportParameterFontSize = new ReportParameter("FontSize", "7.9pt");
            //ReportParameter reportParameterPAGE = new ReportParameter("PAGE", "PAGE");
            //ReportParameter reportParameterOF = new ReportParameter("OF", "OF");
            //ReportParameter reportParameterDoubleSpace = new ReportParameter("DoubleSpace", "false");
            //ReportParameter reportParameterShowZero = new ReportParameter("ShowZero", "true");
            ////Add the parameters to the reportviewercontrol
            //reportViewer1.LocalReport.SetParameters(new ReportParameter[] { reportParameterChartTitle,
            //                                                                reportParameterNORTH,
            //                                                                reportParameterSOUTH,
            //                                                                reportParameterEAST,
            //                                                                reportParameterWEST,
            //                                                                reportParameterPERDISAG,
            //                                                                reportParameterPrintInfo,
            //                                                                reportParameterValidAsOf,
            //                                                                reportParameterStateNames,
            //                                                                reportParameterLocationAndPersonnel,
            //                                                                reportParameterText8,
            //                                                                reportParameterText9,
            //                                                                reportParameterText10,
            //                                                                reportParameterText11,
            //                                                                reportParameterText14,
            //                                                                reportParameterText15,
            //                                                                reportParameterText12,
            //                                                                reportParameterText13,
            //                                                                reportParameterFont,
            //                                                                reportParameterFontSize,
            //                                                                reportParameterPAGE,
            //                                                                reportParameterOF,
            //                                                                reportParameterDoubleSpace,
            //                                                                reportParameterShowZero
            //});

            // Refresh the report  
            //reportViewer2.RefreshReport();
            reportViewer.RefreshReport();

            //export to PDF
            //Warning[] warnings;
            //string[] streamIds;
            //string mimeType = string.Empty;
            //string encoding = string.Empty;
            //string extension = string.Empty;

            byte[] bytes = reportViewer.LocalReport.Render("PDF");
            File.WriteAllBytes("chrt1.pdf", bytes);
            
        }
        private void GetCHRTIDataSet(ref DataSet chrtIDataSet)
        {
            string chrtIQuery = "select * from CHRTI order by HIERARCSEQ";

            SqlConnection connection = new
                SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=Data;Integrated Security=SSPI");

            SqlCommand command =
                new SqlCommand(chrtIQuery, connection);

            SqlDataAdapter chrtIAdapter = new
                SqlDataAdapter(command);

            chrtIAdapter.Fill(chrtIDataSet, "CHRTIDataSet");
        }
        private void GetFootNoteDataSet(ref DataSet chrtIFootNote)
        {
            string chrtIFootNoteQuery = "select * from FOOTNOTE where file_X = 1";

            SqlConnection connection = new
                SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=Data;Integrated Security=SSPI");

            SqlCommand command =
                new SqlCommand(chrtIFootNoteQuery, connection);

            SqlDataAdapter chrtIAdapter = new
                SqlDataAdapter(command);

            chrtIAdapter.Fill(chrtIFootNote, "FootNoteDataSet");
        }
    }
}
