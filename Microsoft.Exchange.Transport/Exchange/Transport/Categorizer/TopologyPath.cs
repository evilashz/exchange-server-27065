using System;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200020A RID: 522
	internal abstract class TopologyPath
	{
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x0005E24C File Offset: 0x0005C44C
		// (set) Token: 0x0600172D RID: 5933 RVA: 0x0005E254 File Offset: 0x0005C454
		public bool Optimal
		{
			get
			{
				return this.optimal;
			}
			set
			{
				this.optimal = value;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600172E RID: 5934
		public abstract int TotalCost { get; }

		// Token: 0x0600172F RID: 5935
		public abstract void ReplaceIfBetter(TopologyPath newPrePath, ITopologySiteLink newLink, DateTime timestamp);

		// Token: 0x04000B70 RID: 2928
		private bool optimal;
	}
}
