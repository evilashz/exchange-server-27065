using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Constraints;

namespace Microsoft.Exchange.MailboxLoadBalance.Data
{
	// Token: 0x0200004B RID: 75
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NumberOfEntitiesSelector : BandRandomEntitySelector
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x00009369 File Offset: 0x00007569
		public NumberOfEntitiesSelector(Band band, long numberOfEntities, LoadContainer sourceContainer, string constraintSetIdentity) : base(band, sourceContainer, constraintSetIdentity)
		{
			this.totalNumberOfEntities = numberOfEntities;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000937C File Offset: 0x0000757C
		public override IEnumerable<LoadEntity> GetEntities(LoadContainer targetContainer)
		{
			List<LoadEntity> list = new List<LoadEntity>(base.SourceEntities);
			List<LoadEntity> list2 = new List<LoadEntity>();
			IAllocationConstraint allocationConstraint = targetContainer.Constraint ?? new AnyLoadConstraint();
			Random random = new Random();
			while (list.Count > 0 && (long)list2.Count < this.totalNumberOfEntities)
			{
				int index = random.Next(list.Count);
				LoadEntity loadEntity = list[index];
				if (allocationConstraint.Accept(loadEntity))
				{
					list2.Add(loadEntity);
				}
				list.RemoveAt(index);
			}
			return list2;
		}

		// Token: 0x040000CA RID: 202
		private readonly long totalNumberOfEntities;
	}
}
