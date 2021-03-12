using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000047 RID: 71
	internal class Bit1 : ABit
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000F2C5 File Offset: 0x0000D4C5
		public new static Func<int[], uint, int> GetBitsFunc
		{
			get
			{
				return (int[] array, uint index) => array[(int)((UIntPtr)(index >> 5))] >> (int)(31U - (index & 31U)) & 1;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000F308 File Offset: 0x0000D508
		public new static Action<int[], uint, int> SetBitsAction
		{
			get
			{
				return delegate(int[] array, uint index, int value)
				{
					array[(int)((UIntPtr)(index >> 5))] |= 1 << (int)(31U - (index & 31U));
				};
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000F327 File Offset: 0x0000D527
		public new static Func<uint, int> GetPhysicalIndexFunc
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000F32A File Offset: 0x0000D52A
		public new static Func<int, uint, int, int> NewValueInPhysicalIndexFunc
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000F32D File Offset: 0x0000D52D
		public new static Func<int, uint, int> CurrentValueInLogicIndexFunc
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000177 RID: 375
		public new const int BitsForCount = 1;
	}
}
