using System;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000051 RID: 81
	[ClientScriptResource("NewConnectSubscription", "Microsoft.Exchange.Management.ControlPanel.Client.Connect.js")]
	public class NewConnectSubscription : ScriptControlBase
	{
		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x00052EED File Offset: 0x000510ED
		// (set) Token: 0x060019DB RID: 6619 RVA: 0x00052EF5 File Offset: 0x000510F5
		public bool CreateFacebook { get; set; }

		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x00052EFE File Offset: 0x000510FE
		// (set) Token: 0x060019DD RID: 6621 RVA: 0x00052F06 File Offset: 0x00051106
		public bool CreateLinkedIn { get; set; }

		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x00052F0F File Offset: 0x0005110F
		// (set) Token: 0x060019DF RID: 6623 RVA: 0x00052F17 File Offset: 0x00051117
		public bool CloseWindowWithoutCreatingSubscription { get; set; }

		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x060019E0 RID: 6624 RVA: 0x00052F20 File Offset: 0x00051120
		// (set) Token: 0x060019E1 RID: 6625 RVA: 0x00052F28 File Offset: 0x00051128
		public string AppAuthorizationCode { get; set; }

		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x00052F31 File Offset: 0x00051131
		// (set) Token: 0x060019E3 RID: 6627 RVA: 0x00052F39 File Offset: 0x00051139
		public string RedirectUri { get; set; }

		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x00052F42 File Offset: 0x00051142
		// (set) Token: 0x060019E5 RID: 6629 RVA: 0x00052F4A File Offset: 0x0005114A
		public string RequestToken { get; set; }

		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x060019E6 RID: 6630 RVA: 0x00052F53 File Offset: 0x00051153
		// (set) Token: 0x060019E7 RID: 6631 RVA: 0x00052F5B File Offset: 0x0005115B
		public string RequestSecret { get; set; }

		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x00052F64 File Offset: 0x00051164
		// (set) Token: 0x060019E9 RID: 6633 RVA: 0x00052F6C File Offset: 0x0005116C
		public string Verifier { get; set; }

		// Token: 0x060019EA RID: 6634 RVA: 0x00052F75 File Offset: 0x00051175
		public NewConnectSubscription() : base(HtmlTextWriterTag.Div)
		{
			Util.RequireUpdateProgressPopUp(this);
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00052F88 File Offset: 0x00051188
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddProperty("CreateFacebook", this.CreateFacebook);
			descriptor.AddProperty("CreateLinkedIn", this.CreateLinkedIn);
			descriptor.AddProperty("CloseWindowWithoutCreatingSubscription", this.CloseWindowWithoutCreatingSubscription);
			descriptor.AddProperty("AppAuthorizationCode", this.AppAuthorizationCode);
			descriptor.AddProperty("RedirectUri", this.RedirectUri);
			descriptor.AddProperty("RequestToken", this.RequestToken);
			descriptor.AddProperty("RequestSecret", this.RequestSecret);
			descriptor.AddProperty("Verifier", this.Verifier);
		}

		// Token: 0x04001AE9 RID: 6889
		private const string NewConnectSubscriptionScriptComponent = "NewConnectSubscription";
	}
}
