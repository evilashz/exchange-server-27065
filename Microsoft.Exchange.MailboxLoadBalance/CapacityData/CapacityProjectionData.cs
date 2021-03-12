using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;

namespace Microsoft.Exchange.MailboxLoadBalance.CapacityData
{
	// Token: 0x02000025 RID: 37
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CapacityProjectionData
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00006844 File Offset: 0x00004A44
		public CapacityProjectionData(double consumerGrowthRate, double organizationGrowthRate, int growthPeriods, ByteQuantifiedSize reservedCapacity)
		{
			this.ConsumerGrowthRate = consumerGrowthRate;
			this.OrganizationGrowthRate = organizationGrowthRate;
			this.GrowthPeriods = growthPeriods;
			this.ReservedCapacity = reservedCapacity;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00006869 File Offset: 0x00004A69
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00006871 File Offset: 0x00004A71
		public double ConsumerGrowthRate { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000687A File Offset: 0x00004A7A
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00006882 File Offset: 0x00004A82
		public int GrowthPeriods { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000688B File Offset: 0x00004A8B
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00006893 File Offset: 0x00004A93
		public double OrganizationGrowthRate { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000689C File Offset: 0x00004A9C
		// (set) Token: 0x06000131 RID: 305 RVA: 0x000068A4 File Offset: 0x00004AA4
		public ByteQuantifiedSize ReservedCapacity { get; set; }

		// Token: 0x06000132 RID: 306 RVA: 0x000068B0 File Offset: 0x00004AB0
		public static CapacityProjectionData FromSettings(ILoadBalanceSettings settings)
		{
			ByteQuantifiedSize reservedCapacity = ByteQuantifiedSize.FromGB((ulong)((long)settings.ReservedCapacityInGb));
			double consumerGrowthRate = (double)settings.ConsumerGrowthRate / 100.0;
			double organizationGrowthRate = (double)settings.OrganizationGrowthRate / 100.0;
			return new CapacityProjectionData(consumerGrowthRate, organizationGrowthRate, settings.CapacityGrowthPeriods, reservedCapacity);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000068FC File Offset: 0x00004AFC
		public override string ToString()
		{
			return string.Format("Consumer growth rate of {0}, Org growth rate of {1}, {2} growth periods and {3} of reserved capcity.", new object[]
			{
				this.ConsumerGrowthRate,
				this.OrganizationGrowthRate,
				this.GrowthPeriods,
				this.ReservedCapacity
			});
		}
	}
}
