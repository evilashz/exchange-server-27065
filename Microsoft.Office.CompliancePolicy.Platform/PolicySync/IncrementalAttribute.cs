using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000F6 RID: 246
	[DataContract]
	public sealed class IncrementalAttribute<T> : IncrementalAttributeBase
	{
		// Token: 0x06000697 RID: 1687 RVA: 0x00014870 File Offset: 0x00012A70
		public IncrementalAttribute()
		{
			this.Changed = false;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001487F File Offset: 0x00012A7F
		public IncrementalAttribute(T value)
		{
			this.Changed = true;
			this.Value = value;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00014895 File Offset: 0x00012A95
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x0001489D File Offset: 0x00012A9D
		[DataMember]
		public T Value { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x000148A6 File Offset: 0x00012AA6
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x000148AE File Offset: 0x00012AAE
		[DataMember]
		public override bool Changed { get; protected set; }

		// Token: 0x0600069D RID: 1693 RVA: 0x000148B7 File Offset: 0x00012AB7
		public override object GetObjectValue()
		{
			if (!this.Changed)
			{
				throw new InvalidOperationException("Incremental value not present");
			}
			return this.Value;
		}
	}
}
