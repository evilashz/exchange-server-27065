using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Band
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BandDataAggregator : ILoadEntityVisitor
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000052F6 File Offset: 0x000034F6
		public BandDataAggregator(Band band)
		{
			this.band = band;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00005310 File Offset: 0x00003510
		public IEnumerable<BandData> BandData
		{
			get
			{
				return this.bandData;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005318 File Offset: 0x00003518
		public bool Visit(LoadContainer container)
		{
			if (container.ContainerType != ContainerType.Database)
			{
				return container.CanAcceptRegularLoad;
			}
			this.AggregateDatabase(container);
			return false;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005332 File Offset: 0x00003532
		public bool Visit(LoadEntity entity)
		{
			return false;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005338 File Offset: 0x00003538
		private void AggregateDatabase(LoadContainer database)
		{
			BandData bandData = database.ConsumedLoad.GetBandData(this.band);
			bandData.Database = database;
			this.bandData.Add(bandData);
		}

		// Token: 0x04000048 RID: 72
		private readonly Band band;

		// Token: 0x04000049 RID: 73
		private readonly List<BandData> bandData = new List<BandData>();
	}
}
