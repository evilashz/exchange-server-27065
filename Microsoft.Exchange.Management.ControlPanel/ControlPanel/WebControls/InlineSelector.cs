using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005FA RID: 1530
	[ClientScriptResource("InlineSelector", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[ToolboxData("<{0}:InlineSelector runat=server></{0}:InlineSelector>")]
	public class InlineSelector : ScriptControlBase
	{
		// Token: 0x06004497 RID: 17559 RVA: 0x000CF170 File Offset: 0x000CD370
		public InlineSelector() : base(HtmlTextWriterTag.Div)
		{
			this.browseButton = new IconButton();
			this.browseButton.ID = "browseButton";
			this.cancelButton = new HyperLink();
			this.cancelButton.ID = "clearButton";
			this.cancelImage = new CommandSprite();
			this.cancelImage.ID = "ImageClearButton";
			this.pickerTextBox = new TextBox();
			this.pickerTextBox.ID = "pickerTextBox";
			this.pickerTextBox.ReadOnly = true;
			this.BrowseButtonText = Strings.Browse;
		}

		// Token: 0x17002682 RID: 9858
		// (get) Token: 0x06004498 RID: 17560 RVA: 0x000CF20D File Offset: 0x000CD40D
		// (set) Token: 0x06004499 RID: 17561 RVA: 0x000CF22E File Offset: 0x000CD42E
		[Localizable(true)]
		[DefaultValue(null)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Bindable(BindableSupport.Yes)]
		public string Text
		{
			get
			{
				if (!this.pickerTextBox.Text.IsNullOrBlank())
				{
					return this.pickerTextBox.Text;
				}
				return null;
			}
			set
			{
				if (!value.IsNullOrBlank())
				{
					this.pickerTextBox.Text = value;
					return;
				}
				this.pickerTextBox.Text = null;
			}
		}

		// Token: 0x17002683 RID: 9859
		// (get) Token: 0x0600449A RID: 17562 RVA: 0x000CF251 File Offset: 0x000CD451
		// (set) Token: 0x0600449B RID: 17563 RVA: 0x000CF25E File Offset: 0x000CD45E
		[Browsable(false)]
		public CommandSprite.SpriteId BrowseButtonImageId
		{
			get
			{
				return this.browseButton.ImageId;
			}
			set
			{
				this.browseButton.ImageId = value;
			}
		}

		// Token: 0x17002684 RID: 9860
		// (get) Token: 0x0600449C RID: 17564 RVA: 0x000CF26C File Offset: 0x000CD46C
		// (set) Token: 0x0600449D RID: 17565 RVA: 0x000CF279 File Offset: 0x000CD479
		[Browsable(false)]
		[Localizable(true)]
		public string BrowseButtonText
		{
			get
			{
				return this.browseButton.Text;
			}
			set
			{
				this.browseButton.Text = value;
			}
		}

		// Token: 0x17002685 RID: 9861
		// (get) Token: 0x0600449E RID: 17566 RVA: 0x000CF287 File Offset: 0x000CD487
		// (set) Token: 0x0600449F RID: 17567 RVA: 0x000CF28F File Offset: 0x000CD48F
		[DefaultValue("false")]
		public bool HideClearButton { get; set; }

		// Token: 0x060044A0 RID: 17568 RVA: 0x000CF298 File Offset: 0x000CD498
		public void SetIndicatorVisible(bool showIndicator)
		{
			this.cancelButton.CssClass = (showIndicator ? string.Empty : "hidden");
			this.indCell.CssClass = this.indCell.CssClass + (showIndicator ? " EnabledPickerIndicatorTd" : string.Empty);
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x000CF2EC File Offset: 0x000CD4EC
		protected override void CreateChildControls()
		{
			Table table = new Table();
			table.CellPadding = 0;
			table.CellSpacing = 0;
			table.CssClass = "pickerTextBoxContainer";
			table.Attributes.Add("role", "presentation");
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.CssClass = "pickerTextBoxTd";
			this.pickerTextBox.CssClass = "pickerTextBox";
			this.pickerTextBox.TabIndex = 0;
			this.pickerTextBox.ReadOnly = true;
			tableCell.Controls.Add(this.pickerTextBox);
			this.cancelButton.NavigateUrl = "#";
			this.cancelButton.Attributes.Add("onclick", "javascript:return false;");
			this.cancelImage.ImageId = CommandSprite.SpriteId.ClearDefault;
			this.cancelImage.AlternateText = Strings.ClearSelectionTooltip;
			this.cancelButton.ToolTip = Strings.ClearSelectionTooltip;
			this.cancelButton.Controls.Add(this.cancelImage);
			EncodingLabel encodingLabel = new EncodingLabel();
			encodingLabel.Text = "×";
			encodingLabel.CssClass = "pickerTextBoxImageAlter";
			this.cancelButton.Controls.Add(encodingLabel);
			this.indCell = new TableCell();
			this.indCell.ID = "indCell";
			this.indCell.CssClass = (this.HideClearButton ? "hidden" : "pickerTextBoxIndicatorTd");
			this.indCell.Controls.Add(this.cancelButton);
			tableRow.Cells.Add(tableCell);
			tableRow.Cells.Add(this.indCell);
			table.Rows.Add(tableRow);
			Table table2 = new Table();
			table2.CellPadding = 0;
			table2.CellSpacing = 1;
			table2.CssClass = "singleSelect";
			table2.Attributes.Add("role", "presentation");
			TableRow tableRow2 = new TableRow();
			TableCell tableCell2 = new TableCell();
			tableCell2.Controls.Add(table);
			tableRow2.Cells.Add(tableCell2);
			this.browseButton.CssClass = "pickerBrowseButton" + (Util.IsIE() ? " pickerBrowseButton-IE" : string.Empty);
			TableCell tableCell3 = new TableCell();
			tableCell3.Controls.Add(this.browseButton);
			tableRow2.Cells.Add(tableCell3);
			table2.Rows.Add(tableRow2);
			this.Controls.Add(table2);
			EncodingLabel child = Util.CreateHiddenForSRLabel(string.Empty, this.browseButton.ID);
			EncodingLabel child2 = Util.CreateHiddenForSRLabel(string.Empty, this.cancelButton.ID);
			this.browseButton.Controls.Add(child);
			this.cancelButton.Controls.Add(child2);
		}

		// Token: 0x060044A2 RID: 17570 RVA: 0x000CF5C1 File Offset: 0x000CD7C1
		protected override void OnPreRender(EventArgs e)
		{
			if (!this.HideClearButton)
			{
				this.SetIndicatorVisible(!string.IsNullOrEmpty(this.Text) && this.Enabled);
			}
			base.OnPreRender(e);
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x000CF5EE File Offset: 0x000CD7EE
		protected override void Render(HtmlTextWriter writer)
		{
			this.pickerTextBox.Enabled = this.Enabled;
			this.browseButton.Disabled = !this.Enabled;
			base.Render(writer);
		}

		// Token: 0x060044A4 RID: 17572 RVA: 0x000CF61C File Offset: 0x000CD81C
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddElementProperty("BrowseButton", this.browseButton.ClientID);
			descriptor.AddElementProperty("IndicatorTd", this.indCell.ClientID);
			descriptor.AddElementProperty("Indicator", this.cancelButton.ClientID);
			descriptor.AddElementProperty("TextBox", this.pickerTextBox.ClientID);
			descriptor.AddProperty("HideClearButton", this.HideClearButton);
		}

		// Token: 0x04002DFF RID: 11775
		private TextBox pickerTextBox;

		// Token: 0x04002E00 RID: 11776
		private HyperLink cancelButton;

		// Token: 0x04002E01 RID: 11777
		private TableCell indCell;

		// Token: 0x04002E02 RID: 11778
		private CommandSprite cancelImage;

		// Token: 0x04002E03 RID: 11779
		private IconButton browseButton;
	}
}
