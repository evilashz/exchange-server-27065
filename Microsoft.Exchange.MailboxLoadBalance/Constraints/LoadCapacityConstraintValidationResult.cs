using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Constraints
{
	// Token: 0x0200003D RID: 61
	[DataContract]
	internal class LoadCapacityConstraintValidationResult : ConstraintValidationResult
	{
		// Token: 0x0600024A RID: 586 RVA: 0x00007D40 File Offset: 0x00005F40
		public LoadCapacityConstraintValidationResult(LoadCapacityConstraint constraint, bool accepted, LoadMetric exceededMetric, long availableUnits, long requestedUnits) : base(constraint, accepted)
		{
			this.ExceededMetric = exceededMetric;
			this.AvailableUnits = availableUnits;
			this.RequestedUnits = requestedUnits;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00007D61 File Offset: 0x00005F61
		// (set) Token: 0x0600024C RID: 588 RVA: 0x00007D69 File Offset: 0x00005F69
		protected long RequestedUnits { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00007D72 File Offset: 0x00005F72
		// (set) Token: 0x0600024E RID: 590 RVA: 0x00007D7A File Offset: 0x00005F7A
		protected long AvailableUnits { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00007D83 File Offset: 0x00005F83
		// (set) Token: 0x06000250 RID: 592 RVA: 0x00007D8B File Offset: 0x00005F8B
		protected LoadMetric ExceededMetric { get; set; }

		// Token: 0x06000251 RID: 593 RVA: 0x00007D94 File Offset: 0x00005F94
		public override string ToString()
		{
			return string.Format("LoadConstraint: '{0}' Accepted: {1}. Requested {2} units of {3} but only had {4} available.", new object[]
			{
				base.Constraint,
				base.Accepted,
				this.RequestedUnits,
				this.ExceededMetric,
				this.AvailableUnits
			});
		}
	}
}
