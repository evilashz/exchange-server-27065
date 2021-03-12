using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000ED RID: 237
	[KnownType(typeof(SyncCallerContext))]
	[DataContract]
	[KnownType(typeof(TenantCookieCollection))]
	[KnownType(typeof(TenantCookie))]
	public class GetChangesRequest
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00014707 File Offset: 0x00012907
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x0001470F File Offset: 0x0001290F
		[DataMember]
		public SyncCallerContext CallerContext { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00014718 File Offset: 0x00012918
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x00014720 File Offset: 0x00012920
		[DataMember]
		public TenantCookieCollection TenantCookies { get; set; }
	}
}
