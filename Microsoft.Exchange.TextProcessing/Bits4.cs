using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000048 RID: 72
	internal class Bits4 : ABit
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000F34B File Offset: 0x0000D54B
		public new static Func<int[], uint, int> GetBitsFunc
		{
			get
			{
				return (int[] array, uint index) => Bits4.CurrentValueInLogicIndexFunc(array[(int)((UIntPtr)(index >> 3))], index);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000F38F File Offset: 0x0000D58F
		public new static Action<int[], uint, int> SetBitsAction
		{
			get
			{
				return delegate(int[] array, uint index, int value)
				{
					int num = (int)(index >> 3);
					array[num] = Bits4.NewValueInPhysicalIndexFunc(array[num], index, value);
				};
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000F3B3 File Offset: 0x0000D5B3
		public new static Func<uint, int> GetPhysicalIndexFunc
		{
			get
			{
				return (uint index) => (int)(index >> 3);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000F3ED File Offset: 0x0000D5ED
		public new static Func<int, uint, int, int> NewValueInPhysicalIndexFunc
		{
			get
			{
				return (int oldValue, uint logicIndex, int value) => (oldValue & (int)Bits4.masks[(int)((UIntPtr)(logicIndex & 7U))]) | value << (int)((7U - (logicIndex & 7U)) * 4U);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000F41D File Offset: 0x0000D61D
		public new static Func<int, uint, int> CurrentValueInLogicIndexFunc
		{
			get
			{
				return (int physicalValue, uint index) => physicalValue >> (int)((7U - (index & 7U)) * 4U) & 15;
			}
		}

		// Token: 0x0400017A RID: 378
		public new const int BitsForCount = 4;

		// Token: 0x0400017B RID: 379
		private static readonly uint[] masks = new uint[]
		{
			268435455U,
			4043309055U,
			4279238655U,
			4293984255U,
			4294905855U,
			4294963455U,
			4294967055U,
			4294967280U
		};
	}
}
