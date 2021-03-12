using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.TransportLogSearchTasks
{
	// Token: 0x0200004E RID: 78
	internal sealed class LatencyComponent
	{
		// Token: 0x060002DD RID: 733 RVA: 0x0000BB90 File Offset: 0x00009D90
		public LatencyComponent(string serverFqdn, string code, LocalizedString name, ushort latency, int sequenceNumber)
		{
			this.serverFqdn = serverFqdn;
			this.code = code;
			this.name = name;
			this.latency = latency;
			this.sequenceNumber = sequenceNumber;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000BBBD File Offset: 0x00009DBD
		public string ServerFqdn
		{
			get
			{
				return this.serverFqdn;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000BBC5 File Offset: 0x00009DC5
		public string Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000BBCD File Offset: 0x00009DCD
		public LocalizedString Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000BBD5 File Offset: 0x00009DD5
		public int Latency
		{
			get
			{
				return (int)this.latency;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000BBDD File Offset: 0x00009DDD
		public int SequenceNumber
		{
			get
			{
				return this.sequenceNumber;
			}
		}

		// Token: 0x0400010D RID: 269
		private readonly string serverFqdn;

		// Token: 0x0400010E RID: 270
		private readonly string code;

		// Token: 0x0400010F RID: 271
		private readonly LocalizedString name;

		// Token: 0x04000110 RID: 272
		private readonly ushort latency;

		// Token: 0x04000111 RID: 273
		private readonly int sequenceNumber;
	}
}
