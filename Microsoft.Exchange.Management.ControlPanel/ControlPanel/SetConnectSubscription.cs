using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000052 RID: 82
	[ClientScriptResource("SetConnectSubscription", "Microsoft.Exchange.Management.ControlPanel.Client.Connect.js")]
	public class SetConnectSubscription : ScriptControlBase
	{
		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x0005302C File Offset: 0x0005122C
		// (set) Token: 0x060019ED RID: 6637 RVA: 0x00053034 File Offset: 0x00051234
		public bool SetFacebook { get; set; }

		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x0005303D File Offset: 0x0005123D
		// (set) Token: 0x060019EF RID: 6639 RVA: 0x00053045 File Offset: 0x00051245
		public bool CloseWindowWithoutUpdatingSubscription { get; set; }

		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x0005304E File Offset: 0x0005124E
		// (set) Token: 0x060019F1 RID: 6641 RVA: 0x00053056 File Offset: 0x00051256
		public string AppAuthorizationCode { get; set; }

		// Token: 0x17001823 RID: 6179
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x0005305F File Offset: 0x0005125F
		// (set) Token: 0x060019F3 RID: 6643 RVA: 0x00053067 File Offset: 0x00051267
		public string RedirectUri { get; set; }

		// Token: 0x060019F4 RID: 6644 RVA: 0x00053070 File Offset: 0x00051270
		public SetConnectSubscription() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x0005307C File Offset: 0x0005127C
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddProperty("SetFacebook", this.SetFacebook);
			descriptor.AddProperty("CloseWindowWithoutUpdatingSubscription", this.CloseWindowWithoutUpdatingSubscription);
			descriptor.AddProperty("AppAuthorizationCode", this.AppAuthorizationCode);
			descriptor.AddProperty("RedirectUri", this.RedirectUri);
		}

		// Token: 0x04001AF2 RID: 6898
		private const string SetConnectSubscriptionScriptComponent = "SetConnectSubscription";
	}
}
