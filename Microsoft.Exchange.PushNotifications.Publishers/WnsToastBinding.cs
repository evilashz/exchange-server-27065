using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000FB RID: 251
	internal class WnsToastBinding : WnsBinding
	{
		// Token: 0x0600081B RID: 2075 RVA: 0x00018D49 File Offset: 0x00016F49
		public WnsToastBinding(WnsToast template, WnsToast? fallback = null, string language = null, string baseUri = null, WnsBranding? branding = null, bool? addImageQuery = null, WnsText[] texts = null, WnsImage[] images = null) : base(language, baseUri, branding, addImageQuery, texts, images)
		{
			this.Template = template;
			this.Fallback = fallback;
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x00018D6A File Offset: 0x00016F6A
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x00018D72 File Offset: 0x00016F72
		public WnsToast Template { get; private set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00018D7B File Offset: 0x00016F7B
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x00018D83 File Offset: 0x00016F83
		public WnsToast? Fallback { get; private set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00018D8C File Offset: 0x00016F8C
		public override WnsTemplateDescription TemplateDescription
		{
			get
			{
				return WnsTemplateDescription.GetToastDescription(this.Template);
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00018D9C File Offset: 0x00016F9C
		public override WnsTemplateDescription FallbackTemplateDescription
		{
			get
			{
				if (this.Fallback != null)
				{
					return WnsTemplateDescription.GetToastDescription(this.Fallback.Value);
				}
				return null;
			}
		}
	}
}
