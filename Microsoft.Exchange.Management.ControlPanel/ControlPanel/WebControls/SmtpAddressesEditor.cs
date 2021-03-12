using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000662 RID: 1634
	[ParseChildren(true, "Value")]
	[ToolboxData("<{0}:SmtpAddressesEditor runat=server></{0}:SmtpAddressesEditor>")]
	[DefaultProperty("Value")]
	[ClientScriptResource("SmtpAddressesEditor", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	public class SmtpAddressesEditor : ScriptControlBase
	{
		// Token: 0x0600470E RID: 18190 RVA: 0x000D7A40 File Offset: 0x000D5C40
		public SmtpAddressesEditor() : base(HtmlTextWriterTag.Span)
		{
			this.ValueProperty = "PrimarySmtpAddress";
			this.pickerButton = new HtmlButton();
			this.pickerButton.ID = "pickerButton";
			this.textBox = new TextBox();
			this.textBox.ID = "textBox";
			this.ButtonText = Strings.AddUsers;
		}

		// Token: 0x17002753 RID: 10067
		// (get) Token: 0x0600470F RID: 18191 RVA: 0x000D7AA6 File Offset: 0x000D5CA6
		// (set) Token: 0x06004710 RID: 18192 RVA: 0x000D7AAE File Offset: 0x000D5CAE
		[Browsable(false)]
		public SmtpAddressesEditorType EditorType { get; set; }

		// Token: 0x17002754 RID: 10068
		// (get) Token: 0x06004711 RID: 18193 RVA: 0x000D7AB7 File Offset: 0x000D5CB7
		// (set) Token: 0x06004712 RID: 18194 RVA: 0x000D7AC4 File Offset: 0x000D5CC4
		[Localizable(true)]
		[DefaultValue(null)]
		public string Value
		{
			get
			{
				return this.textBox.Text;
			}
			set
			{
				this.textBox.Text = value;
			}
		}

		// Token: 0x17002755 RID: 10069
		// (get) Token: 0x06004713 RID: 18195 RVA: 0x000D7AD2 File Offset: 0x000D5CD2
		// (set) Token: 0x06004714 RID: 18196 RVA: 0x000D7ADF File Offset: 0x000D5CDF
		[Browsable(false)]
		public int MaxLength
		{
			get
			{
				return this.textBox.MaxLength;
			}
			set
			{
				this.textBox.MaxLength = value;
			}
		}

		// Token: 0x17002756 RID: 10070
		// (get) Token: 0x06004715 RID: 18197 RVA: 0x000D7AED File Offset: 0x000D5CED
		// (set) Token: 0x06004716 RID: 18198 RVA: 0x000D7AFA File Offset: 0x000D5CFA
		[Localizable(true)]
		[Browsable(false)]
		public string ButtonText
		{
			get
			{
				return this.pickerButton.InnerText;
			}
			set
			{
				this.pickerButton.InnerText = value;
			}
		}

		// Token: 0x17002757 RID: 10071
		// (get) Token: 0x06004717 RID: 18199 RVA: 0x000D7B08 File Offset: 0x000D5D08
		// (set) Token: 0x06004718 RID: 18200 RVA: 0x000D7B10 File Offset: 0x000D5D10
		[Browsable(false)]
		public string PickerFormUrl { get; set; }

		// Token: 0x17002758 RID: 10072
		// (get) Token: 0x06004719 RID: 18201 RVA: 0x000D7B19 File Offset: 0x000D5D19
		// (set) Token: 0x0600471A RID: 18202 RVA: 0x000D7B21 File Offset: 0x000D5D21
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] PickerRoles { get; set; }

		// Token: 0x17002759 RID: 10073
		// (get) Token: 0x0600471B RID: 18203 RVA: 0x000D7B2A File Offset: 0x000D5D2A
		// (set) Token: 0x0600471C RID: 18204 RVA: 0x000D7B32 File Offset: 0x000D5D32
		[Browsable(true)]
		[DefaultValue("PrimarySmtpAddress")]
		public string ValueProperty { get; set; }

		// Token: 0x1700275A RID: 10074
		// (get) Token: 0x0600471D RID: 18205 RVA: 0x000D7B3B File Offset: 0x000D5D3B
		public bool SingleSelect
		{
			get
			{
				return this.EditorType == SmtpAddressesEditorType.SingleSmtpAddress;
			}
		}

		// Token: 0x1700275B RID: 10075
		// (get) Token: 0x0600471E RID: 18206 RVA: 0x000D7B46 File Offset: 0x000D5D46
		// (set) Token: 0x0600471F RID: 18207 RVA: 0x000D7B4E File Offset: 0x000D5D4E
		[DefaultValue("false")]
		public bool IsRequiredField { get; set; }

		// Token: 0x06004720 RID: 18208 RVA: 0x000D7B57 File Offset: 0x000D5D57
		protected override void CreateChildControls()
		{
			this.hasButton = (this.PickerRoles == null || LoginUtil.IsInRoles(this.Context.User, this.PickerRoles));
			if (this.hasButton)
			{
				this.CreateChildControlsWithButton();
				return;
			}
			this.CreateChildControlsWithoutButton();
		}

		// Token: 0x06004721 RID: 18209 RVA: 0x000D7B98 File Offset: 0x000D5D98
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			EcpRegularExpressionValidator ecpRegularExpressionValidator = null;
			switch (this.EditorType)
			{
			case SmtpAddressesEditorType.SingleSmtpAddress:
				ecpRegularExpressionValidator = new EcpRegularExpressionValidator();
				ecpRegularExpressionValidator.ValidationExpression = "^\\s*\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*\\s*$";
				break;
			case SmtpAddressesEditorType.MultipleSmtpAddresses:
				ecpRegularExpressionValidator = new EcpRegularExpressionValidator();
				ecpRegularExpressionValidator.ValidationExpression = "^\\s*([\\w-+.]+@[\\w-+.]+(\\s*,\\s*[\\w-+.]+@[\\w-+.]+)*)*\\s*$";
				break;
			case SmtpAddressesEditorType.RecipientIds:
				ecpRegularExpressionValidator = new EcpAliasListValidator();
				break;
			case SmtpAddressesEditorType.FreeForm:
				ecpRegularExpressionValidator = new EcpRegularExpressionValidator();
				ecpRegularExpressionValidator.ValidationExpression = "^(.)+$";
				break;
			}
			this.AddValidator(ecpRegularExpressionValidator, "_Validator1");
			if (this.IsRequiredField)
			{
				this.AddValidator(new EcpRequiredFieldValidator(), "_Validator2");
			}
		}

		// Token: 0x06004722 RID: 18210 RVA: 0x000D7C30 File Offset: 0x000D5E30
		protected override void Render(HtmlTextWriter writer)
		{
			this.textBox.Enabled = this.Enabled;
			this.pickerButton.Disabled = !this.Enabled;
			base.Render(writer);
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x000D7C60 File Offset: 0x000D5E60
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			if (this.hasButton)
			{
				descriptor.AddElementProperty("PickerButton", this.pickerButton.ClientID);
			}
			descriptor.AddElementProperty("TextBox", this.textBox.ClientID);
			descriptor.AddProperty("PickerFormUrl", base.ResolveClientUrl(this.PickerFormUrl));
			descriptor.AddProperty("ValueProperty", this.ValueProperty);
			descriptor.AddProperty("SingleSelect", this.SingleSelect);
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x000D7CE8 File Offset: 0x000D5EE8
		private void CreateChildControlsWithoutButton()
		{
			Panel panel = new Panel();
			panel.Controls.Add(this.textBox);
			panel.CssClass = "saeTextboxPadding";
			this.Controls.Add(panel);
		}

		// Token: 0x06004725 RID: 18213 RVA: 0x000D7D24 File Offset: 0x000D5F24
		private void CreateChildControlsWithButton()
		{
			Table table = new Table();
			table.CellPadding = 0;
			table.CellSpacing = 0;
			table.CssClass = "saeTable";
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.Controls.Add(this.textBox);
			tableCell.CssClass = "saePaddingCol";
			this.textBox.CssClass = "saeTextbox";
			tableRow.Cells.Add(tableCell);
			table.Rows.Add(tableRow);
			Table table2 = new Table();
			table2.CellPadding = 0;
			table2.CellSpacing = 1;
			table2.CssClass = "singleSelect";
			tableRow = new TableRow();
			TableCell tableCell2 = new TableCell();
			tableCell2.Controls.Add(table);
			tableRow.Cells.Add(tableCell2);
			TableCell tableCell3 = new TableCell();
			this.pickerButton.CausesValidation = false;
			this.pickerButton.Attributes["onClick"] = "javascript:return false;";
			this.pickerButton.Attributes.Add("class", "pickerBrowseButton" + (Util.IsIE() ? " pickerBrowseButton-IE" : string.Empty));
			tableCell3.Controls.Add(this.pickerButton);
			tableRow.Cells.Add(tableCell3);
			table2.Rows.Add(tableRow);
			Panel panel = new Panel();
			panel.Controls.Add(table2);
			this.Controls.Add(panel);
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x000D7E96 File Offset: 0x000D6096
		private void AddValidator(BaseValidator validator, string name)
		{
			validator.ID = this.textBox.ID + name;
			validator.ControlToValidate = this.textBox.ID;
			this.Controls.Add(validator);
		}

		// Token: 0x04002FEB RID: 12267
		private TextBox textBox;

		// Token: 0x04002FEC RID: 12268
		private HtmlButton pickerButton;

		// Token: 0x04002FED RID: 12269
		private bool hasButton;
	}
}
