using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Constraints
{
	// Token: 0x0200003C RID: 60
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadCapacityConstraint : IAllocationConstraint
	{
		// Token: 0x06000245 RID: 581 RVA: 0x00007C90 File Offset: 0x00005E90
		public LoadCapacityConstraint(LoadContainer container)
		{
			this.container = container;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007CA0 File Offset: 0x00005EA0
		public ConstraintValidationResult Accept(LoadEntity entity)
		{
			LoadMetric exceededMetric;
			long requestedUnits;
			long availableUnits;
			return new LoadCapacityConstraintValidationResult(this, this.container.AvailableCapacity.SupportsAdditional(entity.ConsumedLoad, out exceededMetric, out requestedUnits, out availableUnits), exceededMetric, availableUnits, requestedUnits);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00007CD4 File Offset: 0x00005ED4
		public void ValidateAccepted(LoadEntity entity)
		{
			LoadMetric loadMetric;
			long requestedCapacityUnits;
			long availableCapacityUnits;
			if (!this.container.AvailableCapacity.SupportsAdditional(entity.ConsumedLoad, out loadMetric, out requestedCapacityUnits, out availableCapacityUnits))
			{
				throw new NotEnoughDatabaseCapacityPermanentException(this.container.Guid.ToString(), loadMetric.ToString(), requestedCapacityUnits, availableCapacityUnits);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007D26 File Offset: 0x00005F26
		public IAllocationConstraint CloneForContainer(LoadContainer container)
		{
			return new LoadCapacityConstraint(container);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00007D2E File Offset: 0x00005F2E
		public override string ToString()
		{
			return string.Format("HasCapacity", new object[0]);
		}

		// Token: 0x040000A1 RID: 161
		[DataMember]
		private readonly LoadContainer container;
	}
}
