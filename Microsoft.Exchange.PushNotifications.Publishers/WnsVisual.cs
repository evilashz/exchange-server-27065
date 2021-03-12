using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000F9 RID: 249
	internal abstract class WnsVisual
	{
		// Token: 0x06000808 RID: 2056 RVA: 0x00018B5E File Offset: 0x00016D5E
		public WnsVisual(string language = null, string baseUri = null, WnsBranding? branding = null, bool? addImageQuery = null)
		{
			this.Language = language;
			this.BaseUri = baseUri;
			this.Branding = branding;
			this.AddImageQuery = addImageQuery;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x00018B83 File Offset: 0x00016D83
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x00018B8B File Offset: 0x00016D8B
		public string Language { get; private set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00018B94 File Offset: 0x00016D94
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x00018B9C File Offset: 0x00016D9C
		public string BaseUri { get; private set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x00018BA5 File Offset: 0x00016DA5
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x00018BAD File Offset: 0x00016DAD
		public WnsBranding? Branding { get; private set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x00018BB6 File Offset: 0x00016DB6
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x00018BBE File Offset: 0x00016DBE
		public bool? AddImageQuery { get; private set; }

		// Token: 0x06000811 RID: 2065 RVA: 0x00018BC8 File Offset: 0x00016DC8
		public override string ToString()
		{
			return string.Format("{{lang:{0}; baseUri:{1}; branding:{2}; addImageQuery:{3}}}", new object[]
			{
				this.Language.ToNullableString(),
				this.BaseUri.ToNullableString(),
				this.Branding.ToNullableString<WnsBranding>(),
				this.AddImageQuery.ToNullableString<bool>()
			});
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00018C20 File Offset: 0x00016E20
		internal void WriteWnsPayload(WnsPayloadWriter wpw)
		{
			ArgumentValidator.ThrowIfNull("wpw", wpw);
			wpw.WriteElementStart("visual", true);
			wpw.WriteLanguageAttribute("lang", this.Language, true);
			wpw.WriteUriAttribute("baseUri", this.BaseUri, true);
			wpw.WriteAttribute<WnsBranding>("branding", this.Branding, true);
			wpw.WriteAttribute<bool>("addImageQuery", this.AddImageQuery, true);
			wpw.WriteAttributesEnd();
			this.WriteWnsBindings(wpw);
			wpw.WriteElementEnd();
		}

		// Token: 0x06000813 RID: 2067
		internal abstract void WriteWnsBindings(WnsPayloadWriter wpw);
	}
}
