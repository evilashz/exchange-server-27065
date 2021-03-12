using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000F7 RID: 247
	internal class WnsTileBinding : WnsBinding
	{
		// Token: 0x060007F4 RID: 2036 RVA: 0x00018941 File Offset: 0x00016B41
		public WnsTileBinding(WnsTile template, WnsTile? fallback = null, string language = null, string baseUri = null, WnsBranding? branding = null, bool? addImageQuery = null, WnsText[] texts = null, WnsImage[] images = null) : base(language, baseUri, branding, addImageQuery, texts, images)
		{
			this.Template = template;
			this.Fallback = fallback;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00018962 File Offset: 0x00016B62
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x0001896A File Offset: 0x00016B6A
		public WnsTile Template { get; private set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x00018973 File Offset: 0x00016B73
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0001897B File Offset: 0x00016B7B
		public WnsTile? Fallback { get; private set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00018984 File Offset: 0x00016B84
		public override WnsTemplateDescription TemplateDescription
		{
			get
			{
				return WnsTemplateDescription.GetTileDescription(this.Template);
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x00018994 File Offset: 0x00016B94
		public override WnsTemplateDescription FallbackTemplateDescription
		{
			get
			{
				if (this.Fallback != null)
				{
					return WnsTemplateDescription.GetTileDescription(this.Fallback.Value);
				}
				return null;
			}
		}
	}
}
