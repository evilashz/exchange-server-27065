using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000F5 RID: 245
	[DataContract]
	public abstract class IncrementalAttributeBase
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000693 RID: 1683
		// (set) Token: 0x06000694 RID: 1684
		[DataMember]
		public abstract bool Changed { get; protected set; }

		// Token: 0x06000695 RID: 1685
		public abstract object GetObjectValue();
	}
}
