using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000049 RID: 73
	internal class Bits8 : ABit
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000F493 File Offset: 0x0000D693
		public new static Func<int[], uint, int> GetBitsFunc
		{
			get
			{
				return (int[] array, uint index) => Bits8.CurrentValueInLogicIndexFunc(array[(int)((UIntPtr)(index >> 2))], index);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000F4D7 File Offset: 0x0000D6D7
		public new static Action<int[], uint, int> SetBitsAction
		{
			get
			{
				return delegate(int[] array, uint index, int value)
				{
					int num = (int)(index >> 2);
					array[num] = Bits8.NewValueInPhysicalIndexFunc(array[num], index, value);
				};
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000F4FB File Offset: 0x0000D6FB
		public new static Func<uint, int> GetPhysicalIndexFunc
		{
			get
			{
				return (uint index) => (int)(index >> 2);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000F535 File Offset: 0x0000D735
		public new static Func<int, uint, int, int> NewValueInPhysicalIndexFunc
		{
			get
			{
				return (int oldValue, uint logicIndex, int value) => (oldValue & (int)Bits8.masks[(int)((UIntPtr)(logicIndex & 3U))]) | value << (int)((3U - (logicIndex & 3U)) * 8U);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000F568 File Offset: 0x0000D768
		public new static Func<int, uint, int> CurrentValueInLogicIndexFunc
		{
			get
			{
				return (int physicalValue, uint index) => physicalValue >> (int)((3U - (index & 3U)) * 8U) & 255;
			}
		}

		// Token: 0x04000181 RID: 385
		public new const int BitsForCount = 8;

		// Token: 0x04000182 RID: 386
		private static readonly uint[] masks = new uint[]
		{
			16777215U,
			4278255615U,
			4294902015U,
			4294967040U
		};
	}
}
