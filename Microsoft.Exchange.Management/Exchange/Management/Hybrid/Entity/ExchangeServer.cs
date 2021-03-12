using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Hybrid.Entity
{
	// Token: 0x020008E6 RID: 2278
	internal class ExchangeServer : IExchangeServer
	{
		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x060050CD RID: 20685 RVA: 0x001519DC File Offset: 0x0014FBDC
		// (set) Token: 0x060050CE RID: 20686 RVA: 0x001519E4 File Offset: 0x0014FBE4
		public ADObjectId Identity { get; set; }

		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x060050CF RID: 20687 RVA: 0x001519ED File Offset: 0x0014FBED
		// (set) Token: 0x060050D0 RID: 20688 RVA: 0x001519F5 File Offset: 0x0014FBF5
		public string Name { get; set; }

		// Token: 0x17001823 RID: 6179
		// (get) Token: 0x060050D1 RID: 20689 RVA: 0x001519FE File Offset: 0x0014FBFE
		// (set) Token: 0x060050D2 RID: 20690 RVA: 0x00151A06 File Offset: 0x0014FC06
		public ServerRole ServerRole { get; set; }

		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x060050D3 RID: 20691 RVA: 0x00151A0F File Offset: 0x0014FC0F
		// (set) Token: 0x060050D4 RID: 20692 RVA: 0x00151A17 File Offset: 0x0014FC17
		public ServerVersion AdminDisplayVersion { get; set; }

		// Token: 0x060050D5 RID: 20693 RVA: 0x00151A20 File Offset: 0x0014FC20
		public override string ToString()
		{
			return this.Name;
		}
	}
}
