using Infragistics.Win.UltraWinGrid;
using System.Data;

namespace KB14595_UltraGrid_SummaryValues_WinFormsApp1;

public partial class Form1 : Form
{
    private Random _rnd = new Random();
    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        ultraGrid1.DataSource = GetData();

        label1.Text = "集計結果\n";
        foreach (SummaryValue summaryValue in ultraGrid1.Rows.SummaryValues)
        {
            label1.Text = label1.Text + $"列: {summaryValue.SummarySettings.SourceColumn.Key}, 集計タイプ: {summaryValue.SummarySettings.SummaryType}, 集計結果: {summaryValue.Value}\n";
        }
    }

    private DataTable GetData()
    {
        var dt = new DataTable();

        var idColumn = dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Col1", typeof(String));
        dt.Columns.Add("Col2", typeof(int));

        for (int i = 0; i < 5; i++)
        {
            dt.Rows.Add(i, $"Row {i}", _rnd.Next(2, 6));
        }

        dt.PrimaryKey = new DataColumn[] { idColumn };

        return dt;
    }

    private void ultraGrid1_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
    {
        // 集計結果を表示する行を非表示にしています。
        e.Layout.Override.SummaryDisplayArea = SummaryDisplayAreas.None;

        // 集計を追加
        e.Layout.Bands[0].Summaries.Add(SummaryType.Maximum, e.Layout.Bands[0].Columns["ID"]);
        e.Layout.Bands[0].Summaries.Add(SummaryType.Count, e.Layout.Bands[0].Columns["Col1"]);
        e.Layout.Bands[0].Summaries.Add(SummaryType.Sum, e.Layout.Bands[0].Columns["Col2"]);
    }
}