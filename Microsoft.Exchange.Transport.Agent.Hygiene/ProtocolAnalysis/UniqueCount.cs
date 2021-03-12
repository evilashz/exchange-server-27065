using System;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Transport.Agent.Hygiene;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	internal class UniqueCount
	{
		// Token: 0x06000159 RID: 345 RVA: 0x0000BD01 File Offset: 0x00009F01
		public UniqueCount()
		{
			this.tbl = new byte[1000];
			this.Reset();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000BD20 File Offset: 0x00009F20
		public void Merge(UniqueCount source)
		{
			for (int i = 0; i < this.tbl.Length; i++)
			{
				byte[] array = this.tbl;
				int num = i;
				array[num] |= source.tbl[i];
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000BD61 File Offset: 0x00009F61
		public void Reset()
		{
			Array.Clear(this.tbl, 0, this.tbl.Length);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000BD78 File Offset: 0x00009F78
		private static int Lowest1Bit32(uint value)
		{
			for (int i = 0; i < 32; i++)
			{
				if ((value & 1U) == 1U)
				{
					return i + 1;
				}
				value >>= 1;
			}
			return 0;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000BDA4 File Offset: 0x00009FA4
		private int GetBitArrayValue(int index)
		{
			int result = 0;
			switch (index & 7)
			{
			case 0:
				result = (int)(this.tbl[index / 8 * 5] & 31);
				break;
			case 1:
				result = ((int)(this.tbl[index / 8 * 5 + 1] & 3) << 3) + ((this.tbl[index / 8 * 5] & 224) >> 5);
				break;
			case 2:
				result = (this.tbl[index / 8 * 5 + 1] & 124) >> 2;
				break;
			case 3:
				result = ((int)(this.tbl[index / 8 * 5 + 2] & 15) << 1) + ((this.tbl[index / 8 * 5 + 1] & 128) >> 7);
				break;
			case 4:
				result = ((int)(this.tbl[index / 8 * 5 + 3] & 1) << 4) + ((this.tbl[index / 8 * 5 + 2] & 240) >> 4);
				break;
			case 5:
				result = (this.tbl[index / 8 * 5 + 3] & 62) >> 1;
				break;
			case 6:
				result = ((int)(this.tbl[index / 8 * 5 + 4] & 7) << 2) + ((this.tbl[index / 8 * 5 + 3] & 192) >> 6);
				break;
			case 7:
				result = (this.tbl[index / 8 * 5 + 4] & 248) >> 3;
				break;
			}
			return result;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
		private void SetBitArrayValue(int index, int val)
		{
			if (val >= 32)
			{
				throw new LocalizedException(AgentStrings.InvalidInsertValue(val));
			}
			val &= 31;
			switch (index & 7)
			{
			case 0:
				this.tbl[index / 8 * 5] = (byte)((int)(this.tbl[index / 8 * 5] & 224) + val);
				return;
			case 1:
				this.tbl[index / 8 * 5] = (byte)((val << 5) + (int)(this.tbl[index / 8 * 5] & 31));
				this.tbl[index / 8 * 5 + 1] = (byte)((int)(this.tbl[index / 8 * 5 + 1] & 252) + (val >> 3));
				return;
			case 2:
				this.tbl[index / 8 * 5 + 1] = (byte)((int)(this.tbl[index / 8 * 5 + 1] & 131) + (val << 2));
				return;
			case 3:
				this.tbl[index / 8 * 5 + 1] = (byte)((int)(this.tbl[index / 8 * 5 + 1] & 127) + (val << 7));
				this.tbl[index / 8 * 5 + 2] = (byte)((int)(this.tbl[index / 8 * 5 + 2] & 240) + (val >> 1));
				return;
			case 4:
				this.tbl[index / 8 * 5 + 2] = (byte)((val << 4) + (int)(this.tbl[index / 8 * 5 + 2] & 15));
				this.tbl[index / 8 * 5 + 3] = (byte)((int)(this.tbl[index / 8 * 5 + 3] & 254) + (val >> 4));
				return;
			case 5:
				this.tbl[index / 8 * 5 + 3] = (byte)((int)(this.tbl[index / 8 * 5 + 3] & 193) + (val << 1));
				return;
			case 6:
				this.tbl[index / 8 * 5 + 3] = (byte)((val << 6) + (int)(this.tbl[index / 8 * 5 + 3] & 63));
				this.tbl[index / 8 * 5 + 4] = (byte)((int)(this.tbl[index / 8 * 5 + 4] & 248) + (val >> 2));
				return;
			case 7:
				this.tbl[index / 8 * 5 + 4] = (byte)((val << 3) + (int)(this.tbl[index / 8 * 5 + 4] & 7));
				return;
			default:
				return;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000C104 File Offset: 0x0000A304
		public void AddItem(string itemName)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(itemName);
			uint uint32HashCode = PrivateHashAlgorithm1.GetUInt32HashCode(bytes);
			int index = (int)(uint32HashCode % 1600U);
			uint uint32HashCode2 = PrivateHashAlgorithm2.GetUInt32HashCode(bytes);
			int num = UniqueCount.Lowest1Bit32(uint32HashCode2);
			int bitArrayValue = this.GetBitArrayValue(index);
			if (num > bitArrayValue)
			{
				this.SetBitArrayValue(index, num);
				if (this.GetBitArrayValue(index) != num)
				{
					throw new InvalidOperationException("SetBitArrayValue failed.");
				}
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000C16C File Offset: 0x0000A36C
		public int Count()
		{
			int num = 0;
			int num2 = 0;
			int[] array = new int[32];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = 0;
			}
			for (int i = 0; i < 1600; i++)
			{
				int bitArrayValue = this.GetBitArrayValue(i);
				array[bitArrayValue]++;
			}
			for (int i = array.Length - 1; i > 0; i--)
			{
				num += array[i];
				if (num + array[i - 1] > 800)
				{
					num2 = i;
					break;
				}
			}
			return (int)(Math.Round(Math.Pow(2.0, (double)(num2 - 1))) * 1600.0 * -Math.Log(1.0 - (double)num / 1600.0));
		}

		// Token: 0x04000151 RID: 337
		private const int K = 1600;

		// Token: 0x04000152 RID: 338
		private const int Bits = 5;

		// Token: 0x04000153 RID: 339
		private byte[] tbl;
	}
}
