using System;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200027D RID: 637
	internal class FqdnPortPair
	{
		// Token: 0x06001B85 RID: 7045 RVA: 0x00070C9B File Offset: 0x0006EE9B
		public FqdnPortPair(string fqdn, ushort port)
		{
			this.fqdn = fqdn;
			this.port = port;
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x00070CB4 File Offset: 0x0006EEB4
		public override bool Equals(object obj)
		{
			FqdnPortPair fqdnPortPair = obj as FqdnPortPair;
			return fqdnPortPair != null && this.fqdn.Equals(fqdnPortPair.fqdn, StringComparison.OrdinalIgnoreCase) && this.port == fqdnPortPair.port;
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x00070CF1 File Offset: 0x0006EEF1
		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this.fqdn) ^ (int)this.port;
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x00070D0C File Offset: 0x0006EF0C
		public override string ToString()
		{
			return string.Format("{0}:{1}", this.fqdn, this.port.ToString());
		}

		// Token: 0x04000CFB RID: 3323
		private readonly string fqdn;

		// Token: 0x04000CFC RID: 3324
		private readonly ushort port;
	}
}
