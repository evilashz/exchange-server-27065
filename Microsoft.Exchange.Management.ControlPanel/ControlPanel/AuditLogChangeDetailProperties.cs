using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003B0 RID: 944
	public abstract class AuditLogChangeDetailProperties : Properties
	{
		// Token: 0x17001F7A RID: 8058
		// (get) Token: 0x06003195 RID: 12693
		protected abstract string DetailsPaneId { get; }

		// Token: 0x17001F7B RID: 8059
		// (get) Token: 0x06003196 RID: 12694
		protected abstract string HeaderLabelId { get; }

		// Token: 0x06003197 RID: 12695
		protected abstract string GetDetailsPaneHeader();

		// Token: 0x06003198 RID: 12696
		protected abstract void RenderChanges();

		// Token: 0x06003199 RID: 12697 RVA: 0x00099028 File Offset: 0x00097228
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.detailsPane == null)
			{
				this.FindDetailsPane();
			}
			if (base.Results != null && base.Results.Succeeded)
			{
				this.RenderDetailsHeader();
				this.RenderChanges();
				return;
			}
			base.ContentContainer.Visible = false;
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x00099078 File Offset: 0x00097278
		private void RenderDetailsHeader()
		{
			Control control = this.detailsPane.FindControl(this.HeaderLabelId);
			if (control != null)
			{
				((Label)control).Text = this.GetDetailsPaneHeader();
			}
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x000990AC File Offset: 0x000972AC
		protected TableRow GetDetailRowForTable(string typeOfInformation, string information)
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.Text = typeOfInformation;
			tableCell.CssClass = "breakWord";
			tableCell.VerticalAlign = VerticalAlign.Bottom;
			TableCell tableCell2 = new TableCell();
			tableCell2.Width = 5;
			TableCell tableCell3 = new TableCell();
			tableCell3.Text = information;
			tableCell3.VerticalAlign = VerticalAlign.Bottom;
			tableCell3.CssClass = "breakWord";
			tableRow.Cells.Add(tableCell);
			tableRow.Cells.Add(tableCell2);
			tableRow.Cells.Add(tableCell3);
			return tableRow;
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x00099138 File Offset: 0x00097338
		protected TableRow GetDetailRowForTable(string textInRow)
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.Text = textInRow;
			tableCell.CssClass = "breakWord";
			tableCell.VerticalAlign = VerticalAlign.Bottom;
			tableRow.Cells.Add(tableCell);
			return tableRow;
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x00099178 File Offset: 0x00097378
		protected TableRow GetEmptyRowForTable()
		{
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.Text = "&nbsp;";
			tableRow.Cells.Add(tableCell);
			return tableRow;
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x000991AA File Offset: 0x000973AA
		private void FindDetailsPane()
		{
			this.detailsPane = (HtmlGenericControl)base.ContentContainer.FindControl(this.DetailsPaneId);
		}

		// Token: 0x04002411 RID: 9233
		protected HtmlGenericControl detailsPane;
	}
}
