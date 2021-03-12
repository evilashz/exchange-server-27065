using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000FF RID: 255
	internal static class BufferOperations
	{
		// Token: 0x06000A24 RID: 2596 RVA: 0x0002F57C File Offset: 0x0002D77C
		internal static void Zero(byte[] buf, int offset, int len)
		{
			while (len > 0)
			{
				buf[offset++] = 0;
				len--;
			}
		}
	}
}
