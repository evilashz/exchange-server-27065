using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus
{
	// Token: 0x02000005 RID: 5
	internal class NSRecord
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002407 File Offset: 0x00000607
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000240F File Offset: 0x0000060F
		public string DomainName { get; private set; }

		// Token: 0x06000020 RID: 32 RVA: 0x00002418 File Offset: 0x00000618
		public int ProcessResponse(byte[] message, int position)
		{
			this.DomainName = DnsHelper.ReadDomain(message, ref position);
			if (string.IsNullOrWhiteSpace(this.DomainName))
			{
				throw new FormatException(string.Format("NSRecord domainName is empty", new object[0]));
			}
			return position;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000244C File Offset: 0x0000064C
		public override string ToString()
		{
			return string.Format("NS={0}", this.DomainName);
		}
	}
}
