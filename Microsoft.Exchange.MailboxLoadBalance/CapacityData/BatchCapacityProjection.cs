using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.CapacityData
{
	// Token: 0x02000024 RID: 36
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BatchCapacityProjection : ICapacityProjection
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00006814 File Offset: 0x00004A14
		public BatchCapacityProjection(int numberOfMailboxes)
		{
			this.numberOfMailboxes = numberOfMailboxes;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006824 File Offset: 0x00004A24
		public BatchCapacityDatum GetCapacity()
		{
			return new BatchCapacityDatum
			{
				MaximumNumberOfMailboxes = this.numberOfMailboxes
			};
		}

		// Token: 0x04000076 RID: 118
		private readonly int numberOfMailboxes;
	}
}
