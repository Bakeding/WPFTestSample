using DataBinding.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace DataBinding.ViewWindow
{
    /// <summary>
    /// PrintPreView.xaml 的交互逻辑
    /// </summary>
    public partial class PrintPreView : Window
    {
        private readonly FlowDocument m_doc;
        private delegate void LoadXpsMethod();

        public PrintPreView()
        {
            InitializeComponent();
        }

        public PrintPreView(string strTmplName, Object data)
        {
            InitializeComponent();
            m_doc = LoadDocument(strTmplName, data);
            Dispatcher.BeginInvoke(new LoadXpsMethod(LoadXps), DispatcherPriority.ApplicationIdle);
        }

        public static FlowDocument LoadDocument(string strTmplName, Object data)
        {
            FlowDocument doc = (FlowDocument)Application.LoadComponent(new Uri(strTmplName, UriKind.RelativeOrAbsolute));
            doc.PagePadding = new Thickness(50);
            doc.DataContext = data;
            //if (renderer != null)
            {
                FillTalbe(doc, data);
            }
            return doc;
        }

        public void LoadXps()
        {
            //构造一个基于内存的xps document
            MemoryStream ms = new MemoryStream();
            Package package = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite);
            Uri DocumentUri = new Uri("pack://InMemoryDocument.xps");
            PackageStore.RemovePackage(DocumentUri);
            PackageStore.AddPackage(DocumentUri, package);
            XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Fast, DocumentUri.AbsoluteUri);

            //将flow document写入基于内存的xps document中去
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            writer.Write(((IDocumentPaginatorSource)m_doc).DocumentPaginator);

            //获取这个基于内存的xps document的fixed document
            docViewer.Document = xpsDocument.GetFixedDocumentSequence();

            //关闭基于内存的xps document
            xpsDocument.Close();
        }

        public static void FillTalbe(FlowDocument doc, object data)
        {
            TableRowGroup group = doc.FindName("rowsDetails") as TableRowGroup;
            Style styleCell = doc.Resources["BorderedCell"] as Style;
            foreach (UserInfo item in ((Person)data).UserList)
            {
                TableRow row = new TableRow();

                TableCell cell = new TableCell(new Paragraph(new Run(item.UserName)));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.UserLevel)));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.PassWord)));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.LoginState.ToString(CultureInfo.InvariantCulture))));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                group.Rows.Add(row);
            }
        }
    }
}
