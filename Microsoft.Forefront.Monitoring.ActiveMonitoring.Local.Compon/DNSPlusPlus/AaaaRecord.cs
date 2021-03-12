using System;
using System.Net;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus
{
	// Token: 0x02000004 RID: 4
	internal class AaaaRecord
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000239B File Offset: 0x0000059B
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000023A3 File Offset: 0x000005A3
		public IPAddress IPAddressValue { get; private set; }

		// Token: 0x0600001B RID: 27 RVA: 0x000023AC File Offset: 0x000005AC
		public int ProcessResponse(byte[] message, int position)
		{
			byte[] array = new byte[16];
			Buffer.BlockCopy(message, position, array, 0, 16);
			int result = position + 16;
			this.IPAddressValue = new IPAddress(array);
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023E0 File Offset: 0x000005E0
		public override string ToString()
		{
			return string.Format("AAAA={0}", this.IPAddressValue);
		}

		// Token: 0x0400000B RID: 11
		public const ushort RecordLength = 16;
	}
}
