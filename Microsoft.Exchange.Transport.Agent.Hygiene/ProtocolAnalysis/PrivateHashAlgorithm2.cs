using System;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis
{
	// Token: 0x0200003C RID: 60
	internal sealed class PrivateHashAlgorithm2
	{
		// Token: 0x06000157 RID: 343 RVA: 0x0000BCB4 File Offset: 0x00009EB4
		public static uint GetUInt32HashCode(byte[] data)
		{
			uint num = 0U;
			for (int i = 0; i < data.Length; i++)
			{
				num += (uint)data[i];
				num += num << 10;
				num ^= num >> 6;
			}
			num += num << 3;
			num ^= num >> 11;
			return num + (num << 15);
		}
	}
}
