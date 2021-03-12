using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000F1 RID: 241
	[KnownType(typeof(BindingConfiguration))]
	[DataContract]
	public class GetObjectResultForBinding
	{
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001477C File Offset: 0x0001297C
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x00014784 File Offset: 0x00012984
		[DataMember]
		public BindingConfiguration GetObjectResult { get; set; }
	}
}
