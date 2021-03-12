using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000570 RID: 1392
	[ToolboxData("<{0}:CaptionPanel runat=\"server\" />")]
	public class CaptionPanel : Panel, INamingContainer
	{
		// Token: 0x1700251E RID: 9502
		// (get) Token: 0x060040C1 RID: 16577 RVA: 0x000C5DBD File Offset: 0x000C3FBD
		// (set) Token: 0x060040C2 RID: 16578 RVA: 0x000C5DCA File Offset: 0x000C3FCA
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				return this.lblText.Text;
			}
			set
			{
				this.lblText.Text = value;
			}
		}

		// Token: 0x1700251F RID: 9503
		// (get) Token: 0x060040C3 RID: 16579 RVA: 0x000C5DD8 File Offset: 0x000C3FD8
		public WebControl TextLabel
		{
			get
			{
				return this.lblText.TextContainer;
			}
		}

		// Token: 0x17002520 RID: 9504
		// (get) Token: 0x060040C4 RID: 16580 RVA: 0x000C5DE5 File Offset: 0x000C3FE5
		// (set) Token: 0x060040C5 RID: 16581 RVA: 0x000C5DF2 File Offset: 0x000C3FF2
		[Category("Behavior")]
		[DefaultValue(EACHelpId.Default)]
		[Bindable(true)]
		public string HelpId
		{
			get
			{
				return this.helpControl.HelpId;
			}
			set
			{
				this.helpControl.HelpId = value;
			}
		}

		// Token: 0x17002521 RID: 9505
		// (get) Token: 0x060040C6 RID: 16582 RVA: 0x000C5E00 File Offset: 0x000C4000
		// (set) Token: 0x060040C7 RID: 16583 RVA: 0x000C5E0D File Offset: 0x000C400D
		[Bindable(true)]
		[DefaultValue(true)]
		[Category("Appearance")]
		public bool ShowHelp
		{
			get
			{
				return this.helpControl.ShowHelp;
			}
			set
			{
				this.helpControl.ShowHelp = value;
			}
		}

		// Token: 0x17002522 RID: 9506
		// (get) Token: 0x060040C8 RID: 16584 RVA: 0x000C5E1B File Offset: 0x000C401B
		// (set) Token: 0x060040C9 RID: 16585 RVA: 0x000C5E23 File Offset: 0x000C4023
		public bool AddHelpButton { get; set; }

		// Token: 0x17002523 RID: 9507
		// (get) Token: 0x060040CA RID: 16586 RVA: 0x000C5E2C File Offset: 0x000C402C
		// (set) Token: 0x060040CB RID: 16587 RVA: 0x000C5E34 File Offset: 0x000C4034
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(true)]
		public bool ShowCaption { get; set; }

		// Token: 0x060040CC RID: 16588 RVA: 0x000C5E40 File Offset: 0x000C4040
		public CaptionPanel()
		{
			this.lblText = new EllipsisLabel();
			this.helpControl = new HelpControl();
			this.lblText.CssClass = "hdrTxt";
			this.lblText.ID = "lblText";
			this.lblText.TextContainer.Attributes["role"] = "header";
			this.helpControl.NeedPublishHelpLinkWhenHidden = true;
			this.AddHelpButton = true;
			this.ID = "CaptionPanel";
			this.CssClass = "capPane";
			this.ShowCaption = true;
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x000C5ED8 File Offset: 0x000C40D8
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			HtmlTable htmlTable = new HtmlTable();
			htmlTable.CellPadding = 0;
			htmlTable.CellSpacing = 0;
			htmlTable.Attributes.Add("class", "tb ");
			this.Controls.Add(htmlTable);
			HtmlTableRow htmlTableRow = new HtmlTableRow();
			htmlTable.Rows.Add(htmlTableRow);
			HtmlTableCell htmlTableCell = new HtmlTableCell();
			if (this.ShowCaption)
			{
				htmlTableCell.Controls.Add(this.lblText);
			}
			htmlTableCell.Attributes.Add("class", "captionTd");
			htmlTableRow.Cells.Add(htmlTableCell);
			if (this.AddHelpButton)
			{
				htmlTableCell = new HtmlTableCell();
				htmlTableCell.Controls.Add(this.helpControl);
				htmlTableCell.Attributes.Add("class", "helpTd");
				htmlTableRow.Cells.Add(htmlTableCell);
			}
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x000C5FB8 File Offset: 0x000C41B8
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (string.IsNullOrEmpty(this.Text))
			{
				this.lblText.Attributes.Add("data-value", "{DefaultCaptionText, Mode=OneWay}");
				return;
			}
			if (this.Text.IsBindingExpression())
			{
				this.lblText.Attributes.Add("data-value", this.Text);
				this.Text = string.Empty;
			}
		}

		// Token: 0x04002B08 RID: 11016
		private EllipsisLabel lblText;

		// Token: 0x04002B09 RID: 11017
		private HelpControl helpControl;
	}
}
