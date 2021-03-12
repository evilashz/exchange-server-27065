using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.CapacityData
{
	// Token: 0x02000022 RID: 34
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AvailableCapacityProjection : ICapacityProjection
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00006614 File Offset: 0x00004814
		public AvailableCapacityProjection(HeatMapCapacityData heatMapData, CapacityProjectionData capacityData, int queryBufferPeriod, ByteQuantifiedSize averageMailboxSize, ILogger logger)
		{
			this.heatMapData = heatMapData;
			this.capacityData = capacityData;
			this.queryBufferPeriod = queryBufferPeriod;
			this.averageMailboxSize = averageMailboxSize;
			this.logger = logger;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006644 File Offset: 0x00004844
		public BatchCapacityDatum GetCapacity()
		{
			this.logger.LogVerbose("Calculating projected available capacity with inputs: HeatMapData {0}, CapacityProjectionData: {1}, QueryBufferPeriod: {2}, Average Mailbox Size: {3}.", new object[]
			{
				this.heatMapData,
				this.capacityData,
				this.queryBufferPeriod,
				this.averageMailboxSize
			});
			double num = Math.Pow(1.0 + this.capacityData.ConsumerGrowthRate, (double)this.capacityData.GrowthPeriods);
			double num2 = this.heatMapData.ConsumerSize.ToBytes() * num;
			double num3 = this.heatMapData.OrganizationSize.ToBytes() * Math.Pow(1.0 + this.capacityData.OrganizationGrowthRate, (double)this.capacityData.GrowthPeriods);
			double num4 = this.heatMapData.TotalCapacity.ToBytes() - num2 - num3;
			double num5 = num4 / num - this.capacityData.ReservedCapacity.ToBytes();
			double num6 = num5 / (double)this.queryBufferPeriod;
			int num7 = (int)Math.Floor(num6 / this.averageMailboxSize.ToBytes());
			if (num7 < 0)
			{
				num7 = 0;
			}
			this.logger.LogVerbose("Calculated projected capacity: {0} bytes and {1} mailboxes.", new object[]
			{
				num6,
				num7
			});
			return new BatchCapacityDatum
			{
				MaximumNumberOfMailboxes = num7
			};
		}

		// Token: 0x0400006F RID: 111
		private readonly ByteQuantifiedSize averageMailboxSize;

		// Token: 0x04000070 RID: 112
		private readonly ILogger logger;

		// Token: 0x04000071 RID: 113
		private readonly CapacityProjectionData capacityData;

		// Token: 0x04000072 RID: 114
		private readonly HeatMapCapacityData heatMapData;

		// Token: 0x04000073 RID: 115
		private readonly int queryBufferPeriod;
	}
}
