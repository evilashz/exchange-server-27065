using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000A5 RID: 165
	[DataContract(Name = "GetFederationInformationResponse", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class GetFederationInformationResponse : AutodiscoverResponse
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00017BEA File Offset: 0x00015DEA
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x00017BF2 File Offset: 0x00015DF2
		[DataMember(Name = "Domains", IsRequired = false)]
		public DomainCollection Domains { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00017BFB File Offset: 0x00015DFB
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x00017C03 File Offset: 0x00015E03
		[DataMember(Name = "ApplicationUri", IsRequired = false)]
		public Uri ApplicationUri { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00017C0C File Offset: 0x00015E0C
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x00017C14 File Offset: 0x00015E14
		[DataMember(Name = "TokenIssuers", IsRequired = false)]
		public TokenIssuerCollection TokenIssuers { get; set; }
	}
}
