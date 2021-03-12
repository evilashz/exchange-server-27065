using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A49 RID: 2633
	[DataContract]
	public class ExtensibilityContext
	{
		// Token: 0x06004A75 RID: 19061 RVA: 0x00104793 File Offset: 0x00102993
		public ExtensibilityContext(Extension[] extensions, string marketplaceUrl, string ewsUrl, bool isInOrgMarketplaceRole)
		{
			this.Extensions = extensions;
			this.MarketplaceUrl = marketplaceUrl;
			this.EwsUrl = ewsUrl;
			this.IsInOrgMarketplaceRole = isInOrgMarketplaceRole;
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x06004A76 RID: 19062 RVA: 0x001047B8 File Offset: 0x001029B8
		// (set) Token: 0x06004A77 RID: 19063 RVA: 0x001047C0 File Offset: 0x001029C0
		[DataMember]
		public Extension[] Extensions { get; set; }

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06004A78 RID: 19064 RVA: 0x001047C9 File Offset: 0x001029C9
		// (set) Token: 0x06004A79 RID: 19065 RVA: 0x001047D1 File Offset: 0x001029D1
		[DataMember]
		public string MarketplaceUrl { get; set; }

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x06004A7A RID: 19066 RVA: 0x001047DA File Offset: 0x001029DA
		// (set) Token: 0x06004A7B RID: 19067 RVA: 0x001047E2 File Offset: 0x001029E2
		[DataMember]
		public string EwsUrl { get; set; }

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x06004A7C RID: 19068 RVA: 0x001047EB File Offset: 0x001029EB
		// (set) Token: 0x06004A7D RID: 19069 RVA: 0x001047F3 File Offset: 0x001029F3
		[DataMember]
		public bool IsInOrgMarketplaceRole { get; set; }
	}
}
