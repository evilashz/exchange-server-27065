using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000482 RID: 1154
	[ClientScriptResource(null, "Microsoft.Exchange.Management.ControlPanel.Client.SMSProperties.js")]
	public sealed class SmsCommunicationSlabProperties : SmsSlabProperties
	{
		// Token: 0x060039C9 RID: 14793 RVA: 0x000AF6E7 File Offset: 0x000AD8E7
		public SmsCommunicationSlabProperties() : base("DisableObject", "EditNotification.aspx")
		{
		}

		// Token: 0x170022D3 RID: 8915
		// (get) Token: 0x060039CA RID: 14794 RVA: 0x000AF6F9 File Offset: 0x000AD8F9
		// (set) Token: 0x060039CB RID: 14795 RVA: 0x000AF701 File Offset: 0x000AD901
		private string RedirectionUrl { get; set; }

		// Token: 0x060039CC RID: 14796 RVA: 0x000AF70C File Offset: 0x000AD90C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string text = this.Page.Request.QueryString["backUr"];
			Uri uri;
			if (!string.IsNullOrEmpty(text) && Uri.TryCreate(text, UriKind.Absolute, out uri) && (string.Equals(uri.Scheme, "http", StringComparison.InvariantCultureIgnoreCase) || string.Equals(uri.Scheme, "https", StringComparison.InvariantCultureIgnoreCase)) && string.Equals(uri.Host, this.Context.GetRequestUrl().Host, StringComparison.InvariantCultureIgnoreCase))
			{
				this.RedirectionUrl = text;
			}
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000AF79C File Offset: 0x000AD99C
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.Type = "SmsCommunicationSlabProperties";
			if (this.RedirectionUrl != null)
			{
				scriptDescriptor.AddScriptProperty("RedirectionUrl", this.RedirectionUrl.ToJsonString(null));
			}
			return scriptDescriptor;
		}
	}
}
