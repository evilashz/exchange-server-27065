using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200062B RID: 1579
	[ControlValueProperty("Value")]
	[ClientScriptResource("PeoplePicker", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	[ToolboxData("<{0}:PeoplePicker runat=server></{0}:PeoplePicker>")]
	public class PeoplePicker : PickerControlBase
	{
		// Token: 0x060045AE RID: 17838 RVA: 0x000D2A6A File Offset: 0x000D0C6A
		public PeoplePicker() : base(HtmlTextWriterTag.Div)
		{
			this.AllowTyping = true;
			this.PreferOwaPicker = true;
			base.ValueProperty = "PrimarySmtpAddress";
			base.PickerFormUrl = "~/Pickers/MemberPicker.aspx";
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x000D2A98 File Offset: 0x000D0C98
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			if (this.owaPickerUtil != null)
			{
				descriptor.AddComponentProperty("OwaPickerUtil", this.owaPickerUtil.ClientID, this);
			}
			if (this.IsStandalonePicker)
			{
				descriptor.AddElementProperty("PickerLink", this.PickerLink);
				descriptor.AddProperty("PickerType", PickerType.PickUsersAndGroups.ToString());
				descriptor.AddProperty("IsStandalonePicker", this.IsStandalonePicker);
				descriptor.AddProperty("LinkText", this.LinkText.IsNullOrBlank() ? Strings.PeoplePickerSelectOne : this.LinkText);
			}
			descriptor.AddProperty("AllowTyping", this.AllowTyping, true);
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x000D2B50 File Offset: 0x000D0D50
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (this.PreferOwaPicker && OwaPickerUtil.CanUseOwaPicker)
			{
				this.owaPickerUtil = new OwaPickerUtil();
				this.Controls.Add(this.owaPickerUtil);
			}
			if (this.IsStandalonePicker)
			{
				this.linkPicker = new HyperLink();
				this.linkPicker.ID = "lnkPicker";
				this.linkPicker.Text = this.LinkText;
				this.Controls.Add(this.linkPicker);
				this.CssClass = "peoplePickerLink";
			}
		}

		// Token: 0x170026DB RID: 9947
		// (get) Token: 0x060045B1 RID: 17841 RVA: 0x000D2BDE File Offset: 0x000D0DDE
		// (set) Token: 0x060045B2 RID: 17842 RVA: 0x000D2BE6 File Offset: 0x000D0DE6
		public bool IsStandalonePicker { get; set; }

		// Token: 0x170026DC RID: 9948
		// (get) Token: 0x060045B3 RID: 17843 RVA: 0x000D2BEF File Offset: 0x000D0DEF
		// (set) Token: 0x060045B4 RID: 17844 RVA: 0x000D2BF7 File Offset: 0x000D0DF7
		public bool AllowTyping { get; set; }

		// Token: 0x170026DD RID: 9949
		// (get) Token: 0x060045B5 RID: 17845 RVA: 0x000D2C00 File Offset: 0x000D0E00
		// (set) Token: 0x060045B6 RID: 17846 RVA: 0x000D2C08 File Offset: 0x000D0E08
		public string LinkText { get; set; }

		// Token: 0x170026DE RID: 9950
		// (get) Token: 0x060045B7 RID: 17847 RVA: 0x000D2C11 File Offset: 0x000D0E11
		public string PickerLink
		{
			get
			{
				return this.linkPicker.ClientID;
			}
		}

		// Token: 0x170026DF RID: 9951
		// (get) Token: 0x060045B8 RID: 17848 RVA: 0x000D2C1E File Offset: 0x000D0E1E
		// (set) Token: 0x060045B9 RID: 17849 RVA: 0x000D2C26 File Offset: 0x000D0E26
		public bool PreferOwaPicker { get; set; }

		// Token: 0x04002F0E RID: 12046
		private OwaPickerUtil owaPickerUtil;

		// Token: 0x04002F0F RID: 12047
		private HyperLink linkPicker;
	}
}
