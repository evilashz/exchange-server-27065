using System;
using System.Net;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus
{
	// Token: 0x02000003 RID: 3
	internal class ARecord
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000232C File Offset: 0x0000052C
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002334 File Offset: 0x00000534
		public IPAddress IPAddressValue { get; private set; }

		// Token: 0x06000016 RID: 22 RVA: 0x00002340 File Offset: 0x00000540
		public int ProcessResponse(byte[] message, int position)
		{
			byte[] array = new byte[4];
			Buffer.BlockCopy(message, position, array, 0, 4);
			int result = position + 4;
			this.IPAddressValue = new IPAddress(array);
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002374 File Offset: 0x00000574
		public override string ToString()
		{
			return string.Format("A={0}", this.IPAddressValue);
		}

		// Token: 0x04000009 RID: 9
		public const ushort RecordLength = 4;
	}
}
