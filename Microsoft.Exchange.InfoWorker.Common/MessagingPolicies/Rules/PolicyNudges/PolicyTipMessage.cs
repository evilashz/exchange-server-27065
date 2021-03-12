using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules.PolicyNudges
{
	// Token: 0x0200017E RID: 382
	internal sealed class PolicyTipMessage : IVersionedItem
	{
		// Token: 0x06000A3B RID: 2619 RVA: 0x0002BFA0 File Offset: 0x0002A1A0
		public PolicyTipMessage(string val, string id, DateTime version)
		{
			this.Value = val;
			this.ID = id;
			this.Version = version;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0002BFBD File Offset: 0x0002A1BD
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x0002BFC5 File Offset: 0x0002A1C5
		public string Value { get; private set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0002BFCE File Offset: 0x0002A1CE
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x0002BFD6 File Offset: 0x0002A1D6
		public string ID { get; private set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x0002BFDF File Offset: 0x0002A1DF
		// (set) Token: 0x06000A41 RID: 2625 RVA: 0x0002BFE7 File Offset: 0x0002A1E7
		public DateTime Version { get; private set; }

		// Token: 0x040007F2 RID: 2034
		internal static readonly PolicyTipMessage Empty = new PolicyTipMessage("empty", string.Empty, DateTime.MinValue);
	}
}
