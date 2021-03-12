using System;
using System.Security.Cryptography;

namespace Microsoft.Exchange.Transport.RecipientAPI
{
	// Token: 0x020001BF RID: 447
	[Serializable]
	internal class StringHasher
	{
		// Token: 0x0600125F RID: 4703 RVA: 0x00058BE5 File Offset: 0x00056DE5
		public StringHasher()
		{
			this.scenario = UsageScenario.Production;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00058BF4 File Offset: 0x00056DF4
		public StringHasher(UsageScenario scenario)
		{
			this.scenario = scenario;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00058C04 File Offset: 0x00056E04
		public ulong GetHash(string input)
		{
			byte[] array = new byte[input.Length];
			for (int i = 0; i < input.Length; i++)
			{
				byte b = (byte)input[i];
				if (b >= 65 && b <= 90)
				{
					b = b + 97 - 65;
				}
				array[i] = b;
			}
			return this.GetHash(array);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00058C58 File Offset: 0x00056E58
		private ulong GetHash(byte[] input)
		{
			ulong num = 0UL;
			if (this.hasher == null && UsageScenario.Test != this.scenario)
			{
				this.hasher = new SHA256Cng();
			}
			if (this.hasher == null)
			{
				num = (ulong)input[0];
			}
			else
			{
				byte[] array = this.hasher.ComputeHash(input);
				for (int i = 0; i < 8; i++)
				{
					num <<= 8;
					num |= (ulong)array[i];
				}
			}
			return num;
		}

		// Token: 0x04000A9C RID: 2716
		private UsageScenario scenario;

		// Token: 0x04000A9D RID: 2717
		private SHA256Cng hasher;
	}
}
