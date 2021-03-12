using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x0200008C RID: 140
	[DataContract(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class GetOrganizationRelationshipSettingsRequest : AutodiscoverRequest
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00016ACC File Offset: 0x00014CCC
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00016AD4 File Offset: 0x00014CD4
		[DataMember(Name = "Domains", IsRequired = true)]
		public DomainCollection Domains { get; set; }
	}
}
