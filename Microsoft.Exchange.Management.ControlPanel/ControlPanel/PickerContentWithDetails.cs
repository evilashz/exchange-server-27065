using System;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200062F RID: 1583
	public class PickerContentWithDetails : PickerContent
	{
		// Token: 0x170026E9 RID: 9961
		// (get) Token: 0x060045D1 RID: 17873 RVA: 0x000D3217 File Offset: 0x000D1417
		// (set) Token: 0x060045D2 RID: 17874 RVA: 0x000D321F File Offset: 0x000D141F
		public string DetailsUrl { get; set; }

		// Token: 0x060045D3 RID: 17875 RVA: 0x000D3228 File Offset: 0x000D1428
		protected override void AddDetails()
		{
			if (!string.IsNullOrEmpty(this.DetailsUrl))
			{
				DetailsPane detailsPane = new DetailsPane();
				detailsPane.SuppressFrameCache = true;
				detailsPane.CssClass = "rolePickerDetailsPane";
				detailsPane.ID = "DetailedContent";
				detailsPane.SourceID = base.ListView.ID;
				detailsPane.BaseUrl = this.DetailsUrl;
				detailsPane.FrameTitle = Strings.ViewDetails;
				base.IsMasterDetailed = true;
				Table table = new Table();
				table.CssClass = "masterDetailsTable";
				table.BorderWidth = Unit.Pixel(0);
				table.CellPadding = 0;
				table.CellSpacing = 0;
				TableRow tableRow = new TableRow();
				TableCell tableCell = new TableCell();
				tableCell.CssClass = "rolePickerListCell";
				tableCell.VerticalAlign = VerticalAlign.Top;
				tableCell.Controls.Add(base.ListView);
				tableRow.Cells.Add(tableCell);
				TableCell tableCell2 = new TableCell();
				tableCell2.CssClass = "rolePickerDetailsCell";
				tableCell2.Controls.Add(detailsPane);
				tableRow.Cells.Add(tableCell2);
				table.Rows.Add(tableRow);
				base.ContentPanel.Controls.Add(table);
				return;
			}
			base.AddDetails();
		}
	}
}
