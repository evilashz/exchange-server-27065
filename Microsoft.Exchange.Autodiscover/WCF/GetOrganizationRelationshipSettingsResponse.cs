using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000A6 RID: 166
	[DataContract(Name = "GetOrganizationRelationshipSettingsResponse", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class GetOrganizationRelationshipSettingsResponse : AutodiscoverResponse
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00017C25 File Offset: 0x00015E25
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x00017C2D File Offset: 0x00015E2D
		[DataMember(Name = "OrganizationRelationshipSettingsCollection", IsRequired = false)]
		public OrganizationRelationshipSettingsCollection OrganizationRelationships { get; set; }
	}
}
