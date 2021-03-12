using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000688 RID: 1672
	[ClientScriptResource("WellKnownRecipientTypeControl", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[ToolboxData("<{0}:WellKnownRecipientTypeControl runat=server></{0}:WellKnownRecipientTypeControl>")]
	public class WellKnownRecipientTypeControl : ScriptControlBase
	{
		// Token: 0x06004845 RID: 18501 RVA: 0x000DBA88 File Offset: 0x000D9C88
		public WellKnownRecipientTypeControl() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x06004846 RID: 18502 RVA: 0x000DBA94 File Offset: 0x000D9C94
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.includeOnlySpecifiedRecipientTypesRadioButtonList = new EcpRadioButtonList();
			this.includeOnlySpecifiedRecipientTypesRadioButtonList.CssClass = "RadioInputTable";
			this.includeOnlySpecifiedRecipientTypesRadioButtonList.RepeatDirection = RepeatDirection.Vertical;
			this.includeOnlySpecifiedRecipientTypesRadioButtonList.Attributes.Add("uid", "includeOnlySpecifiedRecipientTypesRadioButtonList");
			this.includeOnlySpecifiedRecipientTypesRadioButtonList.Attributes.Add("data-control", "RadioButtonList");
			this.includeOnlySpecifiedRecipientTypesRadioButtonList.Items.Add(new ListItem(Strings.IncludeAllRecipientTypes, "false"));
			this.includeOnlySpecifiedRecipientTypesRadioButtonList.Items.Add(new ListItem(Strings.IncludeSpecifiedRecipientTypes, "true"));
			this.includeOnlySpecifiedRecipientTypesRadioButtonList.SelectedValue = "false";
			this.Controls.Add(this.includeOnlySpecifiedRecipientTypesRadioButtonList);
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes.Add("class", "DependenceControl");
			this.specifiedRecipientTypesCheckBoxList = new EnumCheckBoxList();
			this.specifiedRecipientTypesCheckBoxList.CssClass = "CheckboxInputTable";
			this.specifiedRecipientTypesCheckBoxList.RepeatDirection = RepeatDirection.Vertical;
			this.specifiedRecipientTypesCheckBoxList.Attributes.Add("data-control", "EnumCheckBoxList");
			this.specifiedRecipientTypesCheckBoxList.Attributes.Add("data-enabled", "{value, Mode=OneWay, ElementId=includeOnlySpecifiedRecipientTypesRadioButtonList, ConvertTo=ValueConverter.StringToBool}");
			this.specifiedRecipientTypesCheckBoxList.Items.Add(new ListItem(Strings.MailboxUsers, "MailboxUsers"));
			this.specifiedRecipientTypesCheckBoxList.Items.Add(new ListItem(Strings.MailUsers, "MailUsers"));
			this.specifiedRecipientTypesCheckBoxList.Items.Add(new ListItem(Strings.ResourceMailboxes, "Resources"));
			this.specifiedRecipientTypesCheckBoxList.Items.Add(new ListItem(Strings.MailContacts, "MailContacts"));
			this.specifiedRecipientTypesCheckBoxList.Items.Add(new ListItem(Strings.MailGroups, "MailGroups"));
			this.specifiedRecipientTypesCheckBoxList.SelectedValue = string.Empty;
			htmlGenericControl.Controls.Add(this.specifiedRecipientTypesCheckBoxList);
			this.Controls.Add(htmlGenericControl);
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x000DBCC7 File Offset: 0x000D9EC7
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddComponentProperty("IncludeOnlySpecifiedRecipientTypesRadioButtonList", this.includeOnlySpecifiedRecipientTypesRadioButtonList.ClientID, this);
			descriptor.AddComponentProperty("SpecifiedRecipientTypesCheckBoxList", this.specifiedRecipientTypesCheckBoxList.ClientID, this);
		}

		// Token: 0x0400307E RID: 12414
		private EcpRadioButtonList includeOnlySpecifiedRecipientTypesRadioButtonList;

		// Token: 0x0400307F RID: 12415
		private EnumCheckBoxList specifiedRecipientTypesCheckBoxList;
	}
}
