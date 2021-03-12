using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000DB RID: 219
	internal abstract class WnsBinding
	{
		// Token: 0x06000720 RID: 1824 RVA: 0x000164E0 File Offset: 0x000146E0
		public WnsBinding(string language = null, string baseUri = null, WnsBranding? branding = null, bool? addImageQuery = null, WnsText[] texts = null, WnsImage[] images = null)
		{
			this.Language = language;
			this.BaseUri = baseUri;
			this.Branding = branding;
			this.AddImageQuery = addImageQuery;
			this.Images = images;
			this.Texts = texts;
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000721 RID: 1825
		public abstract WnsTemplateDescription TemplateDescription { get; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000722 RID: 1826
		public abstract WnsTemplateDescription FallbackTemplateDescription { get; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x00016515 File Offset: 0x00014715
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x0001651D File Offset: 0x0001471D
		public string Language { get; private set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00016526 File Offset: 0x00014726
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x0001652E File Offset: 0x0001472E
		public string BaseUri { get; private set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x00016537 File Offset: 0x00014737
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x0001653F File Offset: 0x0001473F
		public WnsBranding? Branding { get; private set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00016548 File Offset: 0x00014748
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x00016550 File Offset: 0x00014750
		public bool? AddImageQuery { get; private set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x00016559 File Offset: 0x00014759
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x00016561 File Offset: 0x00014761
		public WnsImage[] Images { get; private set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001656A File Offset: 0x0001476A
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x00016572 File Offset: 0x00014772
		public WnsText[] Texts { get; private set; }

		// Token: 0x0600072F RID: 1839 RVA: 0x0001657C File Offset: 0x0001477C
		public override string ToString()
		{
			return string.Format("{{template:{0}; fallback:{1}; lang:{2}; baseUri:{3}; branding:{4}; addImageQuery:{5}; images:{6}; texts:{7}}}", new object[]
			{
				this.TemplateDescription.Name,
				this.FallbackTemplateDescription.ToNullableString(null),
				this.Language.ToNullableString(),
				this.BaseUri.ToNullableString(),
				this.Branding.ToNullableString<WnsBranding>(),
				this.AddImageQuery.ToNullableString<bool>(),
				this.Images.ToNullableString(null),
				this.Texts.ToNullableString(null)
			});
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00016610 File Offset: 0x00014810
		internal void WriteWnsPayload(WnsPayloadWriter wpw)
		{
			ArgumentValidator.ThrowIfNull("wpw", wpw);
			wpw.WriteElementStart("binding", true);
			wpw.WriteTemplateAttribute("template", this.TemplateDescription, false);
			wpw.WriteTemplateAttribute("fallback", this.FallbackTemplateDescription, true);
			wpw.WriteLanguageAttribute("lang", this.Language, true);
			wpw.WriteUriAttribute("baseUri", this.BaseUri, true);
			wpw.WriteAttribute<WnsBranding>("branding", this.Branding, true);
			wpw.WriteAttribute<bool>("addImageQuery", this.AddImageQuery, true);
			wpw.WriteAttributesEnd();
			int num = 0;
			while (this.Images != null && num < this.Images.Length)
			{
				this.Images[num].WriteWnsPayload(num + 1, wpw);
				num++;
			}
			int num2 = 0;
			while (this.Texts != null && num2 < this.Texts.Length)
			{
				this.Texts[num2].WriteWnsPayload(num2 + 1, wpw);
				num2++;
			}
			wpw.WriteElementEnd();
		}
	}
}
