using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000692 RID: 1682
	[ClientScriptResource("CallerIdsPicker", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	[ToolboxData("<{0}:CallerIdsPicker runat=server></{0}:CallerIdsPicker>")]
	[ControlValueProperty("Value")]
	public class CallerIdsPicker : ScriptControlBase, INamingContainer
	{
		// Token: 0x06004862 RID: 18530 RVA: 0x000DC9F8 File Offset: 0x000DABF8
		public CallerIdsPicker() : base(HtmlTextWriterTag.Div)
		{
			this.isFromNumbersChbx = new CheckBox();
			this.isInContactsFolderChbx = new CheckBox();
			this.phoneNumbersInEditor = new InlineEditor();
			this.isAmongSelectedContactsChbx = new CheckBox();
			this.openPeoplePickerLink = new HyperLink();
			this.phoneNumbersInEditor.ValidationExpression = CommonRegex.GetRegexExpressionById("UMNumberingPlanFormat").ToString();
			this.phoneNumbersInEditor.ValidationErrorMessage = Strings.CallerIdsInlineEditorErrorText;
		}

		// Token: 0x06004863 RID: 18531 RVA: 0x000DCA74 File Offset: 0x000DAC74
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			this.EnsureChildControls();
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddElementProperty("IsFromNumbersChbx", this.isFromNumbersChbx.ClientID, this);
			descriptor.AddElementProperty("IsInContactsFolderChbx", this.isInContactsFolderChbx.ClientID, this);
			descriptor.AddComponentProperty("PhoneNumbersInEditor", this.phoneNumbersInEditor.ClientID, this);
			descriptor.AddElementProperty("IsAmongSelectedContactsChbx", this.isAmongSelectedContactsChbx.ClientID, this);
			descriptor.AddElementProperty("OpenPeoplePickerLink", this.openPeoplePickerLink.ClientID, this);
			descriptor.AddProperty("PeoplePickerLinkText", Strings.CallerIdsPeoplePickerLinkText, false);
		}

		// Token: 0x06004864 RID: 18532 RVA: 0x000DCB18 File Offset: 0x000DAD18
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.isFromNumbersChbx.ID = "isFromNumbersId";
			this.isInContactsFolderChbx.ID = "isInContactsFolderId";
			this.phoneNumbersInEditor.ID = "phoneNumbersId";
			this.isAmongSelectedContactsChbx.ID = "isAmongSelectedContactsId";
			this.openPeoplePickerLink.ID = "openPeoplePickerId";
			Table table = new Table();
			table.CssClass = "RuleParametersTable";
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableRow.Cells.Add(tableCell);
			TableRow tableRow2 = new TableRow();
			TableCell cell = new TableCell();
			tableRow2.Cells.Add(cell);
			TableRow tableRow3 = new TableRow();
			TableCell tableCell2 = new TableCell();
			tableRow3.Cells.Add(tableCell2);
			TableRow tableRow4 = new TableRow();
			TableCell tableCell3 = new TableCell();
			tableRow4.Cells.Add(tableCell3);
			this.phoneNumbersInEditor.InputWaterMarkText = Strings.CallerIdsInlineEditorWatermarkText;
			this.phoneNumbersInEditor.CssClass = "PhoneNumbersInEditor";
			this.isFromNumbersChbx.Text = Strings.CallerIdsCallingFromNumbersText;
			this.isInContactsFolderChbx.Text = Strings.CallerIdsIsInContactsFolderText;
			this.isAmongSelectedContactsChbx.Text = Strings.CallerIdsIsAmongContactsText;
			this.openPeoplePickerLink.Text = Strings.CallerIdsPeoplePickerLinkText;
			tableCell3.Controls.Add(this.isAmongSelectedContactsChbx);
			tableCell3.Controls.Add(this.openPeoplePickerLink);
			tableCell3.CssClass = "RuleParameterEditor";
			tableCell.Controls.Add(this.isFromNumbersChbx);
			tableCell.Controls.Add(this.phoneNumbersInEditor);
			tableCell2.Controls.Add(this.isInContactsFolderChbx);
			table.Rows.Add(tableRow);
			table.Rows.Add(tableRow2);
			table.Rows.Add(tableRow3);
			table.Rows.Add(tableRow4);
			this.Controls.Add(table);
		}

		// Token: 0x04003094 RID: 12436
		private CheckBox isFromNumbersChbx;

		// Token: 0x04003095 RID: 12437
		private CheckBox isInContactsFolderChbx;

		// Token: 0x04003096 RID: 12438
		private CheckBox isAmongSelectedContactsChbx;

		// Token: 0x04003097 RID: 12439
		private InlineEditor phoneNumbersInEditor;

		// Token: 0x04003098 RID: 12440
		private HyperLink openPeoplePickerLink;
	}
}
