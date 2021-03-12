using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000613 RID: 1555
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("MicroEditPanel", "Microsoft.Exchange.Management.ControlPanel.Client.List.js")]
	public class MicroEditPanel : DataContextProvider
	{
		// Token: 0x06004531 RID: 17713 RVA: 0x000D13E4 File Offset: 0x000CF5E4
		public MicroEditPanel()
		{
			base.ViewModel = "MicroEditViewModel";
		}

		// Token: 0x170026B1 RID: 9905
		// (get) Token: 0x06004532 RID: 17714 RVA: 0x000D13F7 File Offset: 0x000CF5F7
		// (set) Token: 0x06004533 RID: 17715 RVA: 0x000D13FF File Offset: 0x000CF5FF
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(MicroEditPanel))]
		public ITemplate Content { get; set; }

		// Token: 0x170026B2 RID: 9906
		// (get) Token: 0x06004534 RID: 17716 RVA: 0x000D1408 File Offset: 0x000CF608
		// (set) Token: 0x06004535 RID: 17717 RVA: 0x000D1410 File Offset: 0x000CF610
		public string Title { get; set; }

		// Token: 0x170026B3 RID: 9907
		// (get) Token: 0x06004536 RID: 17718 RVA: 0x000D1419 File Offset: 0x000CF619
		// (set) Token: 0x06004537 RID: 17719 RVA: 0x000D1421 File Offset: 0x000CF621
		public string LinkText { get; set; }

		// Token: 0x170026B4 RID: 9908
		// (get) Token: 0x06004538 RID: 17720 RVA: 0x000D142A File Offset: 0x000CF62A
		// (set) Token: 0x06004539 RID: 17721 RVA: 0x000D1432 File Offset: 0x000CF632
		public string Roles { get; set; }

		// Token: 0x170026B5 RID: 9909
		// (get) Token: 0x0600453A RID: 17722 RVA: 0x000D143B File Offset: 0x000CF63B
		// (set) Token: 0x0600453B RID: 17723 RVA: 0x000D1443 File Offset: 0x000CF643
		public string LocStringsResource { get; set; }

		// Token: 0x170026B6 RID: 9910
		// (get) Token: 0x0600453C RID: 17724 RVA: 0x000D144C File Offset: 0x000CF64C
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x000D1450 File Offset: 0x000CF650
		protected override void CreateChildControls()
		{
			this.ID = "EditPanel";
			this.CssClass = "MicroEdit";
			base.Attributes.Add("title", string.Empty);
			this.CreateTitleRow();
			this.CreateBodyRow();
			base.CreateChildControls();
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x000D1490 File Offset: 0x000CF690
		private void CreateButtonRow(Panel parent)
		{
			Panel panel = new Panel();
			panel.CssClass = "btnPane";
			parent.Controls.Add(panel);
			HtmlButton htmlButton = new HtmlButton();
			htmlButton.ID = "OkButton";
			htmlButton.InnerText = Strings.CommitButtonText;
			htmlButton.Attributes.Add("data-control", "Button");
			htmlButton.Attributes.Add("data-Command", "{SaveCommand, Mode=OneWay}");
			panel.Controls.Add(htmlButton);
			HtmlButton htmlButton2 = new HtmlButton();
			htmlButton2.ID = "CancelButton";
			htmlButton2.InnerText = Strings.CancelButtonText;
			htmlButton2.Attributes.Add("data-control", "Button");
			htmlButton2.Attributes.Add("data-Command", "{CancelCommand, Mode=OneWay}");
			panel.Controls.Add(htmlButton2);
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x000D1568 File Offset: 0x000CF768
		private void CreateBodyRow()
		{
			Panel panel = new Panel();
			panel.ID = "divBody";
			this.Controls.Add(panel);
			panel.CssClass = "MicroEditReservedSpaceForFVA";
			if (this.Content != null)
			{
				this.Content.InstantiateIn(panel);
			}
			if (this.LocStringsResource != null)
			{
				this.fvaExtender = new FieldValidationAssistantExtender();
				panel.Controls.Add(this.fvaExtender);
				this.fvaExtender.LocStringsResource = this.LocStringsResource;
				this.fvaExtender.TargetControlID = panel.UniqueID;
				this.fvaExtender.Canvas = panel.ClientID;
				this.fvaExtender.IndentCssClass = "baseFrmFvaIndent";
				((ToolkitScriptManager)ScriptManager.GetCurrent(this.Page)).CombineScript(this.LocStringsResource);
			}
			this.CreateButtonRow(panel);
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x000D163C File Offset: 0x000CF83C
		private void CreateTitleRow()
		{
			Panel panel = new Panel();
			this.Controls.Add(panel);
			Table table = new Table();
			panel.Controls.Add(table);
			TableRow tableRow = new TableRow();
			table.Rows.Add(tableRow);
			TableCell tableCell = new TableCell();
			tableCell.Width = Unit.Percentage(100.0);
			tableRow.Cells.Add(tableCell);
			Label label = new Label();
			label.CssClass = "MicroEditTitle";
			label.Text = this.Title;
			tableCell.Controls.Add(label);
			if (this.LinkText != null)
			{
				TableCell tableCell2 = new TableCell();
				tableRow.Cells.Add(tableCell2);
				LinkButton linkButton = new LinkButton();
				linkButton.ID = "Link";
				linkButton.Text = HttpUtility.HtmlEncode(this.LinkText);
				linkButton.CssClass = "MicroEditLink";
				tableCell2.Controls.Add(linkButton);
			}
		}

		// Token: 0x04002E64 RID: 11876
		private FieldValidationAssistantExtender fvaExtender;
	}
}
