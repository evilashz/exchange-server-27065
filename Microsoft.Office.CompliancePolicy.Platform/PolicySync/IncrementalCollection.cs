using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000F7 RID: 247
	[DataContract]
	public sealed class IncrementalCollection<T> : IncrementalAttributeBase
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x000148D7 File Offset: 0x00012AD7
		public IncrementalCollection()
		{
			this.Changed = false;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000148E6 File Offset: 0x00012AE6
		public IncrementalCollection(IEnumerable<T> changedValues, IEnumerable<T> removedValues)
		{
			this.Changed = true;
			this.ChangedValues = changedValues;
			this.RemovedValues = removedValues;
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00014903 File Offset: 0x00012B03
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x0001490B File Offset: 0x00012B0B
		[DataMember]
		public IEnumerable<T> ChangedValues { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00014914 File Offset: 0x00012B14
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x0001491C File Offset: 0x00012B1C
		[DataMember]
		public IEnumerable<T> RemovedValues { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00014925 File Offset: 0x00012B25
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x0001492D File Offset: 0x00012B2D
		[DataMember]
		public override bool Changed { get; protected set; }

		// Token: 0x060006A6 RID: 1702 RVA: 0x00014936 File Offset: 0x00012B36
		public override object GetObjectValue()
		{
			if (!this.Changed)
			{
				throw new InvalidOperationException("Incremental value not present");
			}
			return this.ChangedValues;
		}
	}
}
