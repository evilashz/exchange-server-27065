using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200067E RID: 1662
	public static class WebControlExtension
	{
		// Token: 0x060047EC RID: 18412 RVA: 0x000DAD30 File Offset: 0x000D8F30
		public static Panel CreateWarningPanel(this WebControl webControl, string cssClass)
		{
			return webControl.CreateWarningPanel(false, cssClass);
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x000DAD3C File Offset: 0x000D8F3C
		public static Panel CreateWarningPanel(this WebControl webControl, bool warningAsError, string cssClass)
		{
			Panel panel = new Panel();
			panel.ID = "warningPanel";
			panel.CssClass = cssClass;
			panel.Style.Add(HtmlTextWriterStyle.Display, "none");
			Table table = new Table();
			table.CellPadding = 0;
			table.CellSpacing = 0;
			TableRow tableRow = new TableRow();
			TableCell cell = new TableCell();
			tableRow.Cells.Add(cell);
			TableCell tableCell = new TableCell();
			tableCell.Controls.Add(new Label
			{
				ID = panel.ID + "_WarningMessage"
			});
			tableRow.Cells.Add(tableCell);
			table.Rows.Add(tableRow);
			panel.Controls.Add(table);
			return panel;
		}
	}
}
