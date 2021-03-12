using System;
using System.Collections.Generic;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Provisioning
{
	// Token: 0x020000D1 RID: 209
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class HeatMapDatabaseSelector : DatabaseSelector
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x00012C28 File Offset: 0x00010E28
		public HeatMapDatabaseSelector(IHeatMap heatMap, ILogger logger) : base(logger)
		{
			this.heatMap = heatMap;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00012C38 File Offset: 0x00010E38
		protected override IEnumerable<LoadContainer> GetAvailableDatabases()
		{
			DatabaseCollector databaseCollector = new DatabaseCollector();
			this.heatMap.GetLoadTopology().Accept(databaseCollector);
			return databaseCollector.Databases;
		}

		// Token: 0x0400027B RID: 635
		private readonly IHeatMap heatMap;
	}
}
