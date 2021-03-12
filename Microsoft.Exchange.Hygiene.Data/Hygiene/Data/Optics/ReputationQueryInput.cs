using System;

namespace Microsoft.Exchange.Hygiene.Data.Optics
{
	// Token: 0x020001B7 RID: 439
	[Serializable]
	internal class ReputationQueryInput
	{
		// Token: 0x06001256 RID: 4694 RVA: 0x000381DD File Offset: 0x000363DD
		public ReputationQueryInput(byte entityType, string entityKey, int dataPointType, int ttl, int udpTimeout, int flags = 0)
		{
			this.EntityType = entityType;
			this.EntityKey = entityKey;
			this.DataPointType = dataPointType;
			this.Ttl = ttl;
			this.Flags = flags;
			this.UdpTimeout = udpTimeout;
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x00038212 File Offset: 0x00036412
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x0003821A File Offset: 0x0003641A
		public byte EntityType { get; set; }

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x00038223 File Offset: 0x00036423
		// (set) Token: 0x0600125A RID: 4698 RVA: 0x0003822B File Offset: 0x0003642B
		public string EntityKey { get; set; }

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x00038234 File Offset: 0x00036434
		// (set) Token: 0x0600125C RID: 4700 RVA: 0x0003823C File Offset: 0x0003643C
		public int DataPointType { get; set; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00038245 File Offset: 0x00036445
		// (set) Token: 0x0600125E RID: 4702 RVA: 0x0003824D File Offset: 0x0003644D
		public int Ttl { get; set; }

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00038256 File Offset: 0x00036456
		// (set) Token: 0x06001260 RID: 4704 RVA: 0x0003825E File Offset: 0x0003645E
		public int Flags { get; set; }

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x00038267 File Offset: 0x00036467
		// (set) Token: 0x06001262 RID: 4706 RVA: 0x0003826F File Offset: 0x0003646F
		public int UdpTimeout { get; set; }
	}
}
