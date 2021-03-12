using System;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x02000018 RID: 24
	internal sealed class CMD5
	{
		// Token: 0x06000092 RID: 146 RVA: 0x0000413C File Offset: 0x0000233C
		private CMD5()
		{
			this.hash = new byte[8];
			Array.Clear(this.hash, 0, 8);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004160 File Offset: 0x00002360
		private void Update(byte[] data)
		{
			int num = data.Length >> 2;
			uint num2 = (uint)(((int)this.hash[3] << 24) + ((int)this.hash[2] << 16) + ((int)this.hash[1] << 8) + (int)this.hash[0]);
			uint num3 = (uint)(((int)this.hash[7] << 24) + ((int)this.hash[6] << 16) + ((int)this.hash[5] << 8) + (int)this.hash[4]);
			for (int i = 0; i < num; i++)
			{
				uint num4 = (uint)(((int)data[i * 4 + 3] << 24) + ((int)data[i * 4 + 2] << 16) + ((int)data[i * 4 + 1] << 8) + (int)data[i * 4]);
				num2 += num4;
				num2 *= 2167765871U;
				num3 += num4;
				num3 *= 3875120053U;
			}
			for (int j = num * 4; j < data.Length; j++)
			{
				num2 += (uint)data[j];
				num2 *= 2167765871U;
				num3 += (uint)data[j];
				num3 *= 3875120053U;
			}
			this.hash[3] = (byte)((num2 & 4278190080U) >> 24);
			this.hash[2] = (byte)((num2 & 16711680U) >> 16);
			this.hash[1] = (byte)((num2 & 65280U) >> 8);
			this.hash[0] = (byte)(num2 & 255U);
			this.hash[7] = (byte)((num3 & 4278190080U) >> 24);
			this.hash[6] = (byte)((num3 & 16711680U) >> 16);
			this.hash[5] = (byte)((num3 & 65280U) >> 8);
			this.hash[4] = (byte)(num3 & 255U);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000042DC File Offset: 0x000024DC
		private byte[] Final()
		{
			uint num = (uint)(((int)this.hash[3] << 24) + ((int)this.hash[2] << 16) + ((int)this.hash[1] << 8) + (int)this.hash[0]);
			num += (num >> 16) + (num >> 24);
			this.hash[3] = (byte)((num & 4278190080U) >> 24);
			this.hash[2] = (byte)((num & 16711680U) >> 16);
			this.hash[1] = (byte)((num & 65280U) >> 8);
			this.hash[0] = (byte)(num & 255U);
			return this.hash;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004370 File Offset: 0x00002570
		public static byte[] GetHash(byte[] data)
		{
			CMD5 cmd = new CMD5();
			cmd.Update(data);
			return cmd.Final();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004390 File Offset: 0x00002590
		public static int HashSize()
		{
			return 8;
		}

		// Token: 0x0400004A RID: 74
		private const int CMD5Length = 8;

		// Token: 0x0400004B RID: 75
		private byte[] hash;
	}
}
