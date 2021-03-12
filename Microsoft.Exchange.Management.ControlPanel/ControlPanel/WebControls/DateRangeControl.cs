using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005A0 RID: 1440
	[ToolboxData("<{0}:DateRangeControl runat=server></{0}:NumberRangeControl>")]
	[ClientScriptResource("DateRangeControl", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	public class DateRangeControl : ScriptControlBase
	{
		// Token: 0x060041EB RID: 16875 RVA: 0x000C8934 File Offset: 0x000C6B34
		public DateRangeControl() : base(HtmlTextWriterTag.Div)
		{
			this.CssClass = "valueRangePicker";
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x000C894C File Offset: 0x000C6B4C
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.chkAfterDate = new CheckBox();
			this.chkAfterDate.Text = Strings.After;
			this.chkBeforeDate = new CheckBox();
			this.chkBeforeDate.Text = Strings.Before;
			this.afterDatePicker = new DateTimePicker();
			this.afterDatePicker.HasTimePicker = false;
			this.beforeDatePicker = new DateTimePicker();
			this.beforeDatePicker.HasTimePicker = false;
			HtmlTable htmlTable = new HtmlTable();
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			HtmlTableRow htmlTableRow2 = new HtmlTableRow();
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			HtmlTableCell htmlTableCell2 = new HtmlTableCell();
			HtmlTableCell htmlTableCell3 = new HtmlTableCell();
			HtmlTableCell htmlTableCell4 = new HtmlTableCell();
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("div");
			htmlTable.CellPadding = (htmlTable.CellSpacing = 0);
			htmlTableCell.Attributes.Add("class", "checkBoxCell");
			htmlTableCell2.Attributes.Add("class", "dateCell");
			htmlTableCell3.Attributes.Add("class", "checkBoxCell");
			htmlTableCell4.Attributes.Add("class", "dateCell");
			htmlGenericControl2.Controls.Add(this.chkAfterDate);
			htmlTableCell.Controls.Add(htmlGenericControl2);
			htmlTableCell2.Controls.Add(this.afterDatePicker);
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableRow.Cells.Add(htmlTableCell2);
			htmlTable.Rows.Add(htmlTableRow);
			htmlGenericControl.Controls.Add(this.chkBeforeDate);
			htmlTableCell3.Controls.Add(htmlGenericControl);
			htmlTableCell4.Controls.Add(this.beforeDatePicker);
			htmlTableRow2.Cells.Add(htmlTableCell3);
			htmlTableRow2.Cells.Add(htmlTableCell4);
			htmlTable.Rows.Add(htmlTableRow2);
			this.Controls.Add(htmlTable);
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x000C8B38 File Offset: 0x000C6D38
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddComponentProperty("BeforeDatePicker", this.beforeDatePicker.ClientID, this);
			descriptor.AddComponentProperty("AfterDatePicker", this.afterDatePicker.ClientID, this);
			descriptor.AddElementProperty("BeforeDateCheckBox", this.chkBeforeDate.ClientID, this);
			descriptor.AddElementProperty("AfterDateCheckBox", this.chkAfterDate.ClientID, this);
		}

		// Token: 0x04002B6E RID: 11118
		private DateTimePicker beforeDatePicker;

		// Token: 0x04002B6F RID: 11119
		private DateTimePicker afterDatePicker;

		// Token: 0x04002B70 RID: 11120
		private CheckBox chkBeforeDate;

		// Token: 0x04002B71 RID: 11121
		private CheckBox chkAfterDate;
	}
}
