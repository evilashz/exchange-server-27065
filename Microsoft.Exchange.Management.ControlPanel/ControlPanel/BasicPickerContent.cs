using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200062E RID: 1582
	[ParseChildren(true)]
	[ControlValueProperty("Values")]
	[ClientScriptResource("BasicPickerContent", "Microsoft.Exchange.Management.ControlPanel.Client.Pickers.js")]
	[PersistChildren(true)]
	[ToolboxData("<{0}:BasicPickerContent runat=server></{0}:BasicPickerContent>")]
	public class BasicPickerContent : ScriptControlBase
	{
		// Token: 0x060045BA RID: 17850 RVA: 0x000D2C30 File Offset: 0x000D0E30
		public BasicPickerContent()
		{
			this.CssClass = "pickerContainer";
			this.pickerTextBox = new TextBox();
			this.pickerTextBox.ID = "pickerTextBox";
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x000D2C8C File Offset: 0x000D0E8C
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.CreateContentPanel();
			this.CreateBottomPanel();
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl(HtmlTextWriterTag.Div.ToString());
			htmlGenericControl.Attributes["class"] = "PropertyDiv";
			htmlGenericControl.Controls.Add(this.contentPanel);
			htmlGenericControl.Controls.Add(this.bottomPanel);
			this.Controls.Add(htmlGenericControl);
		}

		// Token: 0x060045BC RID: 17852 RVA: 0x000D2D00 File Offset: 0x000D0F00
		private void CreateBottomPanel()
		{
			this.btnAddItem = new HtmlButton();
			this.btnAddItem.ID = "btnAddItem";
			this.btnAddItem.CausesValidation = false;
			this.btnAddItem.Attributes["type"] = "button";
			this.btnAddItem.Attributes["onClick"] = "javascript:return false;";
			this.btnAddItem.Attributes.Add("class", "selectbutton");
			this.btnAddItem.InnerText = Strings.PickerFormItemsButtonText;
			TableCell tableCell = new TableCell();
			tableCell.CssClass = "selectButtonCell";
			tableCell.Controls.Add(this.btnAddItem);
			this.wellControl = new WellControl();
			this.wellControl.ID = "wellControl";
			this.wellControl.DisplayProperty = this.NameProperty;
			this.wellControl.IdentityProperty = this.IdentityProperty;
			this.wellControl.CssClass = "wellControl";
			TableCell tableCell2 = new TableCell();
			tableCell2.Controls.Add(this.wellControl);
			TableRow tableRow = new TableRow();
			tableRow.Cells.Add(tableCell2);
			Table table = new Table();
			table.CssClass = "wellWrapperTable";
			table.CellSpacing = 0;
			table.CellPadding = 0;
			table.Rows.Add(tableRow);
			TableCell tableCell3 = new TableCell();
			tableCell3.CssClass = "wellContainerCell";
			tableCell3.Controls.Add(table);
			TableRow tableRow2 = new TableRow();
			tableRow2.Cells.Add(tableCell);
			tableRow2.Cells.Add(tableCell3);
			Table table2 = new Table();
			table2.Width = Unit.Percentage(100.0);
			table2.CellSpacing = 0;
			table2.CellPadding = 0;
			table2.Rows.Add(tableRow2);
			this.selectionPanel = new Panel();
			this.selectionPanel.ID = "selectionPanel";
			this.selectionPanel.CssClass = "selectionPanel";
			this.selectionPanel.Controls.Add(table2);
			this.bottomPanel = new Panel();
			this.bottomPanel.ID = "bottomPanel";
			this.bottomPanel.CssClass = "bottom";
			this.bottomPanel.Controls.Add(this.selectionPanel);
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x000D2F5C File Offset: 0x000D115C
		private void CreateContentPanel()
		{
			this.pickerTextBox.CssClass = "pickerTextBox";
			this.pickerTextBox.Style.Add(HtmlTextWriterStyle.MarginTop, "0px");
			TableCell tableCell = new TableCell();
			tableCell.CssClass = "pickerTextBoxTd";
			tableCell.Controls.Add(this.pickerTextBox);
			EncodingLabel child = Util.CreateHiddenForSRLabel(string.Empty, this.pickerTextBox.ID);
			tableCell.Controls.Add(child);
			TableRow tableRow = new TableRow();
			tableRow.Cells.Add(tableCell);
			Table table = new Table();
			table.CellPadding = 0;
			table.CellSpacing = 0;
			table.CssClass = "pickerTextBoxContainer";
			table.Rows.Add(tableRow);
			this.contentPanel = new Panel();
			this.contentPanel.ID = "contentPanel";
			this.contentPanel.CssClass = "contentPanel";
			if (!string.IsNullOrWhiteSpace(this.Caption))
			{
				Label label = new Label();
				label.Text = this.Caption;
				label.CssClass = "detailsLabel";
				this.contentPanel.Controls.Add(label);
			}
			this.contentPanel.Controls.Add(table);
		}

		// Token: 0x170026E0 RID: 9952
		// (get) Token: 0x060045BE RID: 17854 RVA: 0x000D308E File Offset: 0x000D128E
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x170026E1 RID: 9953
		// (get) Token: 0x060045BF RID: 17855 RVA: 0x000D3092 File Offset: 0x000D1292
		// (set) Token: 0x060045C0 RID: 17856 RVA: 0x000D309A File Offset: 0x000D129A
		[DefaultValue("Error")]
		public string ErrorProperty
		{
			get
			{
				return this.errorProperty;
			}
			set
			{
				this.errorProperty = value;
			}
		}

		// Token: 0x170026E2 RID: 9954
		// (get) Token: 0x060045C1 RID: 17857 RVA: 0x000D30A3 File Offset: 0x000D12A3
		// (set) Token: 0x060045C2 RID: 17858 RVA: 0x000D30AB File Offset: 0x000D12AB
		[DefaultValue("CanRetry")]
		public string CanRetryProperty
		{
			get
			{
				return this.canRetryProperty;
			}
			set
			{
				this.canRetryProperty = value;
			}
		}

		// Token: 0x170026E3 RID: 9955
		// (get) Token: 0x060045C3 RID: 17859 RVA: 0x000D30B4 File Offset: 0x000D12B4
		// (set) Token: 0x060045C4 RID: 17860 RVA: 0x000D30BC File Offset: 0x000D12BC
		[DefaultValue("Warning")]
		public string WarningProperty
		{
			get
			{
				return this.warningProperty;
			}
			set
			{
				this.warningProperty = value;
			}
		}

		// Token: 0x170026E4 RID: 9956
		// (get) Token: 0x060045C5 RID: 17861 RVA: 0x000D30C5 File Offset: 0x000D12C5
		// (set) Token: 0x060045C6 RID: 17862 RVA: 0x000D30CD File Offset: 0x000D12CD
		[DefaultValue("DisplayName")]
		public string NameProperty
		{
			get
			{
				return this.nameProperty;
			}
			set
			{
				this.nameProperty = value;
			}
		}

		// Token: 0x170026E5 RID: 9957
		// (get) Token: 0x060045C7 RID: 17863 RVA: 0x000D30D6 File Offset: 0x000D12D6
		// (set) Token: 0x060045C8 RID: 17864 RVA: 0x000D30DE File Offset: 0x000D12DE
		[DefaultValue("Identity")]
		public string IdentityProperty
		{
			get
			{
				return this.identityProperty;
			}
			set
			{
				this.identityProperty = value;
			}
		}

		// Token: 0x170026E6 RID: 9958
		// (get) Token: 0x060045C9 RID: 17865 RVA: 0x000D30E7 File Offset: 0x000D12E7
		// (set) Token: 0x060045CA RID: 17866 RVA: 0x000D30EF File Offset: 0x000D12EF
		[DefaultValue("")]
		public string Caption
		{
			get
			{
				return this.caption;
			}
			set
			{
				this.caption = value;
			}
		}

		// Token: 0x170026E7 RID: 9959
		// (get) Token: 0x060045CB RID: 17867 RVA: 0x000D30F8 File Offset: 0x000D12F8
		// (set) Token: 0x060045CC RID: 17868 RVA: 0x000D3100 File Offset: 0x000D1300
		public WebServiceReference ServiceUrl { get; set; }

		// Token: 0x060045CD RID: 17869 RVA: 0x000D310C File Offset: 0x000D130C
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			BaseForm baseForm = this.Page as BaseForm;
			if (baseForm != null)
			{
				descriptor.AddComponentProperty("Form", "aspnetForm");
			}
			descriptor.AddElementProperty("PickerTextBox", this.pickerTextBox.ClientID, this);
			descriptor.AddElementProperty("AddButton", this.btnAddItem.ClientID, this);
			descriptor.AddElementProperty("BottomPanel", this.bottomPanel.ClientID, this);
			descriptor.AddComponentProperty("WellControl", this.wellControl.ClientID, this);
			descriptor.AddProperty("ServiceUrl", EcpUrl.ProcessUrl(this.ServiceUrl.ServiceUrl), true);
			descriptor.AddProperty("ErrorProperty", this.ErrorProperty, true);
			descriptor.AddProperty("CanRetryProperty", this.CanRetryProperty, true);
			descriptor.AddProperty("WarningProperty", this.WarningProperty, true);
			descriptor.AddProperty("SpriteSrc", Util.GetSpriteImageSrc(this));
		}

		// Token: 0x170026E8 RID: 9960
		// (get) Token: 0x060045CE RID: 17870 RVA: 0x000D31FE File Offset: 0x000D13FE
		// (set) Token: 0x060045CF RID: 17871 RVA: 0x000D3206 File Offset: 0x000D1406
		public string WrapperControlID { get; set; }

		// Token: 0x04002F29 RID: 12073
		private Panel contentPanel;

		// Token: 0x04002F2A RID: 12074
		private TextBox pickerTextBox;

		// Token: 0x04002F2B RID: 12075
		private Panel bottomPanel;

		// Token: 0x04002F2C RID: 12076
		private Panel selectionPanel;

		// Token: 0x04002F2D RID: 12077
		private HtmlButton btnAddItem;

		// Token: 0x04002F2E RID: 12078
		private WellControl wellControl;

		// Token: 0x04002F2F RID: 12079
		private string nameProperty;

		// Token: 0x04002F30 RID: 12080
		private string identityProperty;

		// Token: 0x04002F31 RID: 12081
		private string caption;

		// Token: 0x04002F32 RID: 12082
		private string errorProperty = "Error";

		// Token: 0x04002F33 RID: 12083
		private string canRetryProperty = "CanRetry";

		// Token: 0x04002F34 RID: 12084
		private string warningProperty = "Warning";
	}
}
