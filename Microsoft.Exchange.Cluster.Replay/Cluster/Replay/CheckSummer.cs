using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000244 RID: 580
	internal class CheckSummer
	{
		// Token: 0x06001624 RID: 5668 RVA: 0x0005A678 File Offset: 0x00058878
		public void Sum(byte[] buf, int off, int len)
		{
			int num = off;
			int i = len;
			while (i >= 4)
			{
				uint num2 = BitConverter.ToUInt32(buf, num);
				num += 4;
				i -= 4;
				this.m_csum ^= num2;
				this.m_totalBytes += 4UL;
			}
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x0005A6BC File Offset: 0x000588BC
		public uint GetSum()
		{
			return this.m_csum;
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0005A6C4 File Offset: 0x000588C4
		public ulong GetTotalBytes()
		{
			return this.m_totalBytes;
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x0005A6CC File Offset: 0x000588CC
		public void Reset()
		{
			this.m_csum = 0U;
			this.m_totalBytes = 0UL;
		}

		// Token: 0x040008C1 RID: 2241
		private uint m_csum;

		// Token: 0x040008C2 RID: 2242
		private ulong m_totalBytes;
	}
}
