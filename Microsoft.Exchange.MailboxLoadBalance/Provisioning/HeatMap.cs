using System;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.CapacityData;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Provisioning
{
	// Token: 0x020000C9 RID: 201
	internal abstract class HeatMap : IHeatMap
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x000123B0 File Offset: 0x000105B0
		public virtual bool IsReady
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000674 RID: 1652
		public abstract LoadContainer GetLoadTopology();

		// Token: 0x06000675 RID: 1653 RVA: 0x000123B4 File Offset: 0x000105B4
		public HeatMapCapacityData ToCapacityData()
		{
			LoadContainer loadTopology = this.GetLoadTopology();
			return loadTopology.ToCapacityData();
		}

		// Token: 0x06000676 RID: 1654
		public abstract void UpdateBands(Band[] bands);
	}
}
