using System;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.CapacityData
{
	// Token: 0x02000026 RID: 38
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ConsumerSizeProjection : ICapacityProjection
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00006953 File Offset: 0x00004B53
		public ConsumerSizeProjection(HeatMapCapacityData heatMapData, CapacityProjectionData capacityProjectionData, ByteQuantifiedSize averageMailboxSize, int queryBufferPeriod, double maxConsumerSizePercentage, ILogger logger)
		{
			this.heatMapData = heatMapData;
			this.capacityProjectionData = capacityProjectionData;
			this.averageMailboxSize = averageMailboxSize;
			this.queryBufferPeriod = queryBufferPeriod;
			this.maxConsumerSizePercentage = maxConsumerSizePercentage;
			this.logger = logger;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006988 File Offset: 0x00004B88
		public BatchCapacityDatum GetCapacity()
		{
			this.logger.LogVerbose("Calculating projected consumer capacity with inputs: HeatMapData {0}, CapacityProjectionData: {1}, QueryBufferPeriod: {2}, Average Mailbox Size: {3}, Maximum Consumer Size Percentage: {4}.", new object[]
			{
				this.heatMapData,
				this.capacityProjectionData,
				this.queryBufferPeriod,
				this.averageMailboxSize,
				this.maxConsumerSizePercentage
			});
			double num = Math.Pow(1.0 + this.capacityProjectionData.ConsumerGrowthRate, (double)this.capacityProjectionData.GrowthPeriods);
			double num2 = this.heatMapData.ConsumerSize.ToBytes() * num;
			double num3 = this.heatMapData.TotalCapacity.ToBytes() * this.maxConsumerSizePercentage - num2;
			double num4 = num3 / num;
			double num5 = num4 / (double)this.queryBufferPeriod;
			int num6 = (int)Math.Floor(num5 / this.averageMailboxSize.ToBytes());
			if (num6 < 0)
			{
				num6 = 0;
			}
			this.logger.LogVerbose("Calculated projected capacity: {0} bytes and {1} mailboxes.", new object[]
			{
				num5,
				num6
			});
			return new BatchCapacityDatum
			{
				MaximumNumberOfMailboxes = num6
			};
		}

		// Token: 0x0400007B RID: 123
		private readonly ByteQuantifiedSize averageMailboxSize;

		// Token: 0x0400007C RID: 124
		private readonly CapacityProjectionData capacityProjectionData;

		// Token: 0x0400007D RID: 125
		private readonly HeatMapCapacityData heatMapData;

		// Token: 0x0400007E RID: 126
		private readonly int queryBufferPeriod;

		// Token: 0x0400007F RID: 127
		private readonly double maxConsumerSizePercentage;

		// Token: 0x04000080 RID: 128
		private readonly ILogger logger;
	}
}
