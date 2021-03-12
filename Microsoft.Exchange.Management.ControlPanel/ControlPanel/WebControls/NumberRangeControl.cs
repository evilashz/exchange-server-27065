using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000622 RID: 1570
	[ToolboxData("<{0}:NumberRangeControl runat=server></{0}:NumberRangeControl>")]
	[ClientScriptResource("NumberRangeControl", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	public class NumberRangeControl : ScriptControlBase
	{
		// Token: 0x06004599 RID: 17817 RVA: 0x000D24F2 File Offset: 0x000D06F2
		public NumberRangeControl() : base(HtmlTextWriterTag.Div)
		{
			this.CssClass = "valueRangePicker";
		}

		// Token: 0x170026D4 RID: 9940
		// (get) Token: 0x0600459A RID: 17818 RVA: 0x000D2507 File Offset: 0x000D0707
		public string AtLeastTextboxID
		{
			get
			{
				this.EnsureChildControls();
				return this.tbxAtLeast.ClientID;
			}
		}

		// Token: 0x170026D5 RID: 9941
		// (get) Token: 0x0600459B RID: 17819 RVA: 0x000D251A File Offset: 0x000D071A
		public string AtMostTextboxID
		{
			get
			{
				this.EnsureChildControls();
				return this.tbxAtMost.ClientID;
			}
		}

		// Token: 0x0600459C RID: 17820 RVA: 0x000D2530 File Offset: 0x000D0730
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.tbxAtLeast = new TextBox();
			this.tbxAtLeast.ID = "tbxAtLeast";
			this.tbxAtMost = new TextBox();
			this.tbxAtMost.ID = "tbxAtMost";
			Label label = new Label();
			Label label2 = new Label();
			label.ID = this.tbxAtLeast.ID + "_label";
			label2.ID = this.tbxAtMost.ID + "_label";
			label.Text = Strings.AtLeast;
			label2.Text = Strings.AtMost;
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
			htmlTableCell2.Attributes.Add("class", "labelCell");
			htmlTableCell.Attributes.Add("class", "labelCell");
			htmlTableCell4.Attributes.Add("class", "inputCell");
			htmlTableCell3.Attributes.Add("class", "inputCell");
			htmlGenericControl.Controls.Add(label);
			htmlTableCell.Controls.Add(htmlGenericControl);
			htmlTableCell3.Controls.Add(this.tbxAtLeast);
			htmlTableRow.Cells.Add(htmlTableCell);
			htmlTableRow.Cells.Add(htmlTableCell3);
			htmlTable.Rows.Add(htmlTableRow);
			htmlGenericControl2.Controls.Add(label2);
			htmlTableCell2.Controls.Add(htmlGenericControl2);
			htmlTableCell4.Controls.Add(this.tbxAtMost);
			htmlTableRow2.Cells.Add(htmlTableCell2);
			htmlTableRow2.Cells.Add(htmlTableCell4);
			htmlTable.Rows.Add(htmlTableRow2);
			this.Controls.Add(htmlTable);
			NumericInputExtender numericInputExtender = new NumericInputExtender();
			numericInputExtender.TargetControlID = this.tbxAtLeast.UniqueID;
			this.Controls.Add(numericInputExtender);
			NumericInputExtender numericInputExtender2 = new NumericInputExtender();
			numericInputExtender2.TargetControlID = this.tbxAtMost.UniqueID;
			this.Controls.Add(numericInputExtender2);
		}

		// Token: 0x0600459D RID: 17821 RVA: 0x000D278D File Offset: 0x000D098D
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddElementProperty("AtLeastTextbox", this.AtLeastTextboxID, this);
			descriptor.AddElementProperty("AtMostTextbox", this.AtMostTextboxID, this);
		}

		// Token: 0x04002EA6 RID: 11942
		private TextBox tbxAtLeast;

		// Token: 0x04002EA7 RID: 11943
		private TextBox tbxAtMost;
	}
}
