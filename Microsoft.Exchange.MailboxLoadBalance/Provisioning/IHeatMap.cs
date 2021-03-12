using System;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.CapacityData;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Provisioning
{
	// Token: 0x020000C8 RID: 200
	internal interface IHeatMap
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600066F RID: 1647
		bool IsReady { get; }

		// Token: 0x06000670 RID: 1648
		LoadContainer GetLoadTopology();

		// Token: 0x06000671 RID: 1649
		HeatMapCapacityData ToCapacityData();

		// Token: 0x06000672 RID: 1650
		void UpdateBands(Band[] bands);
	}
}
