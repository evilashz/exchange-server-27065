using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000687 RID: 1671
	[ToolboxData("<{0}:WellControl runat=server></{0}:WellControl>")]
	[ClientScriptResource("WellControl", "Microsoft.Exchange.Management.ControlPanel.Client.Pickers.js")]
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class WellControl : ScriptControlBase
	{
		// Token: 0x0600483C RID: 18492 RVA: 0x000DB9FA File Offset: 0x000D9BFA
		public WellControl()
		{
			this.RemoveLinkText = Strings.WellControlRemoveLinkText;
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x000DBA12 File Offset: 0x000D9C12
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("RemoveLinkText", this.RemoveLinkText, true);
			descriptor.AddProperty("IdentityProperty", this.IdentityProperty, true);
			descriptor.AddProperty("DisplayProperty", this.DisplayProperty, true);
		}

		// Token: 0x170027B0 RID: 10160
		// (get) Token: 0x0600483E RID: 18494 RVA: 0x000DBA51 File Offset: 0x000D9C51
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x170027B1 RID: 10161
		// (get) Token: 0x0600483F RID: 18495 RVA: 0x000DBA55 File Offset: 0x000D9C55
		// (set) Token: 0x06004840 RID: 18496 RVA: 0x000DBA5D File Offset: 0x000D9C5D
		[Localizable(true)]
		public string RemoveLinkText
		{
			get
			{
				return this.removeLinkText;
			}
			set
			{
				this.removeLinkText = value;
			}
		}

		// Token: 0x170027B2 RID: 10162
		// (get) Token: 0x06004841 RID: 18497 RVA: 0x000DBA66 File Offset: 0x000D9C66
		// (set) Token: 0x06004842 RID: 18498 RVA: 0x000DBA6E File Offset: 0x000D9C6E
		public string IdentityProperty { get; set; }

		// Token: 0x170027B3 RID: 10163
		// (get) Token: 0x06004843 RID: 18499 RVA: 0x000DBA77 File Offset: 0x000D9C77
		// (set) Token: 0x06004844 RID: 18500 RVA: 0x000DBA7F File Offset: 0x000D9C7F
		public string DisplayProperty { get; set; }

		// Token: 0x0400307B RID: 12411
		private string removeLinkText;
	}
}
