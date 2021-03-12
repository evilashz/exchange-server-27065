using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000EE RID: 238
	[DataContract]
	[KnownType(typeof(SyncCallerContext))]
	public class GetObjectRequest
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00014731 File Offset: 0x00012931
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x00014739 File Offset: 0x00012939
		[DataMember]
		public SyncCallerContext callerContext { get; set; }
	}
}
