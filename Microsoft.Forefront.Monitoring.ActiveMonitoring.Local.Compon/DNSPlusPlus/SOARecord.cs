using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus
{
	// Token: 0x02000006 RID: 6
	internal class SOARecord
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002473 File Offset: 0x00000673
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000247B File Offset: 0x0000067B
		public string PrimaryNameServer { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002484 File Offset: 0x00000684
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000248C File Offset: 0x0000068C
		public string ResponsibleMailAddress { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002495 File Offset: 0x00000695
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000249D File Offset: 0x0000069D
		public int Serial { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000024A6 File Offset: 0x000006A6
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000024AE File Offset: 0x000006AE
		public int Refresh { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000024B7 File Offset: 0x000006B7
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000024BF File Offset: 0x000006BF
		public int Retry { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000024C8 File Offset: 0x000006C8
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000024D0 File Offset: 0x000006D0
		public int Expire { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000024D9 File Offset: 0x000006D9
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000024E1 File Offset: 0x000006E1
		public int DefaultTTL { get; private set; }

		// Token: 0x06000031 RID: 49 RVA: 0x000024EC File Offset: 0x000006EC
		public int ProcessResponse(byte[] message, int position)
		{
			int num = position;
			this.PrimaryNameServer = DnsHelper.ReadDomain(message, ref num);
			this.ResponsibleMailAddress = DnsHelper.ReadDomain(message, ref num);
			this.Serial = DnsHelper.GetInt(message, num);
			num += 4;
			this.Refresh = DnsHelper.GetInt(message, num);
			num += 4;
			this.Retry = DnsHelper.GetInt(message, num);
			num += 4;
			this.Expire = DnsHelper.GetInt(message, num);
			num += 4;
			this.DefaultTTL = DnsHelper.GetInt(message, num);
			num += 4;
			if (string.IsNullOrWhiteSpace(this.PrimaryNameServer))
			{
				throw new FormatException("SOARecord PrimaryNameServer is empty");
			}
			return num;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002588 File Offset: 0x00000788
		public override string ToString()
		{
			return string.Format("SOA PrimaryNameServer={0}, ResponsibleMailAddress={1}", this.PrimaryNameServer, this.ResponsibleMailAddress);
		}
	}
}
