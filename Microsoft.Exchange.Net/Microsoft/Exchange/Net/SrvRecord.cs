using System;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C6B RID: 3179
	internal class SrvRecord
	{
		// Token: 0x06004672 RID: 18034 RVA: 0x000BC976 File Offset: 0x000BAB76
		public SrvRecord(string name, string targetHost, int priority, int weight, int port)
		{
			this.name = name;
			this.targetHost = targetHost;
			this.priority = weight;
			this.weight = weight;
			this.port = port;
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x000BC9A4 File Offset: 0x000BABA4
		private SrvRecord()
		{
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x06004674 RID: 18036 RVA: 0x000BC9AC File Offset: 0x000BABAC
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x06004675 RID: 18037 RVA: 0x000BC9B4 File Offset: 0x000BABB4
		public string TargetHost
		{
			get
			{
				return this.targetHost;
			}
		}

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x06004676 RID: 18038 RVA: 0x000BC9BC File Offset: 0x000BABBC
		public int Priority
		{
			get
			{
				return this.priority;
			}
		}

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06004677 RID: 18039 RVA: 0x000BC9C4 File Offset: 0x000BABC4
		public int Weight
		{
			get
			{
				return this.weight;
			}
		}

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x06004678 RID: 18040 RVA: 0x000BC9CC File Offset: 0x000BABCC
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x04003ABC RID: 15036
		private string name;

		// Token: 0x04003ABD RID: 15037
		private string targetHost;

		// Token: 0x04003ABE RID: 15038
		private int priority;

		// Token: 0x04003ABF RID: 15039
		private int weight;

		// Token: 0x04003AC0 RID: 15040
		private int port;
	}
}
