using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000F2 RID: 242
	[KnownType(typeof(AssociationConfiguration))]
	[DataContract]
	public class GetObjectResultForAssociation
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00014795 File Offset: 0x00012995
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x0001479D File Offset: 0x0001299D
		[DataMember]
		public AssociationConfiguration GetObjectResult { get; set; }
	}
}
