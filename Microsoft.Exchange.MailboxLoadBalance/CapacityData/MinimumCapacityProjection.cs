using System;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.CapacityData
{
	// Token: 0x02000028 RID: 40
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MinimumCapacityProjection : ICapacityProjection
	{
		// Token: 0x0600014E RID: 334 RVA: 0x00006D98 File Offset: 0x00004F98
		public MinimumCapacityProjection(ILogger logger, params ICapacityProjection[] projections)
		{
			this.logger = logger;
			this.projections = projections;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006DB8 File Offset: 0x00004FB8
		public BatchCapacityDatum GetCapacity()
		{
			BatchCapacityDatum batchCapacityDatum = this.projections.Min((ICapacityProjection p) => p.GetCapacity());
			this.logger.LogVerbose("Selected minimum capacity datum: {0}", new object[]
			{
				batchCapacityDatum
			});
			return batchCapacityDatum;
		}

		// Token: 0x0400008A RID: 138
		private readonly ILogger logger;

		// Token: 0x0400008B RID: 139
		private readonly ICapacityProjection[] projections;
	}
}
