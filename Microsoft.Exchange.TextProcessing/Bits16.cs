using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200004A RID: 74
	internal class Bits16 : ABit
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000F5CB File Offset: 0x0000D7CB
		public new static Func<int[], uint, int> GetBitsFunc
		{
			get
			{
				return (int[] array, uint index) => Bits16.CurrentValueInLogicIndexFunc(array[(int)((UIntPtr)(index >> 1))], index);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000F60F File Offset: 0x0000D80F
		public new static Action<int[], uint, int> SetBitsAction
		{
			get
			{
				return delegate(int[] array, uint index, int value)
				{
					int num = (int)(index >> 1);
					array[num] = Bits16.NewValueInPhysicalIndexFunc(array[num], index, value);
				};
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000F633 File Offset: 0x0000D833
		public new static Func<uint, int> GetPhysicalIndexFunc
		{
			get
			{
				return (uint index) => (int)(index >> 1);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000F66E File Offset: 0x0000D86E
		public new static Func<int, uint, int, int> NewValueInPhysicalIndexFunc
		{
			get
			{
				return (int oldValue, uint logicIndex, int value) => (oldValue & (int)Bits16.masks[(int)((UIntPtr)(logicIndex & 1U))]) | value << (int)((1U - (logicIndex & 1U)) * 16U);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000F6A2 File Offset: 0x0000D8A2
		public new static Func<int, uint, int> CurrentValueInLogicIndexFunc
		{
			get
			{
				return (int physicalValue, uint index) => physicalValue >> (int)((1U - (index & 1U)) * 16U) & 65535;
			}
		}

		// Token: 0x04000188 RID: 392
		public new const int BitsForCount = 16;

		// Token: 0x04000189 RID: 393
		private static readonly uint[] masks = new uint[]
		{
			65535U,
			4294901760U
		};
	}
}
