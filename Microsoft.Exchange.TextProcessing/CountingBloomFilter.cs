using System;
using System.Reflection;
using System.Threading;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000045 RID: 69
	internal class CountingBloomFilter<TBit> where TBit : ABit
	{
		// Token: 0x06000231 RID: 561 RVA: 0x0000EDCE File Offset: 0x0000CFCE
		public CountingBloomFilter() : this(10)
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000EDD8 File Offset: 0x0000CFD8
		public CountingBloomFilter(int powerIndexOf2) : this(powerIndexOf2, int.MaxValue, 4)
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000EDE8 File Offset: 0x0000CFE8
		public CountingBloomFilter(int powerIndexOf2, int maxValue, int hashNumbers)
		{
			if (hashNumbers < 2)
			{
				throw new ArgumentOutOfRangeException("hashNumbers");
			}
			this.hashNumbers = hashNumbers;
			if (powerIndexOf2 < 0)
			{
				throw new ArgumentOutOfRangeException("powerIndexOf2");
			}
			this.length = 2U << powerIndexOf2;
			FieldInfo field = typeof(TBit).GetField("BitsForCount");
			CountingBloomFilter<TBit>.bitsForCount = (int)field.GetValue(null);
			PropertyInfo property = typeof(TBit).GetProperty("GetBitsFunc");
			this.getBitsFunc = (Func<int[], uint, int>)property.GetValue(null);
			PropertyInfo property2 = typeof(TBit).GetProperty("SetBitsAction");
			this.setBitsAction = (Action<int[], uint, int>)property2.GetValue(null);
			PropertyInfo property3 = typeof(TBit).GetProperty("GetPhysicalIndexFunc");
			this.getPhysicalIndexFunc = (Func<uint, int>)property3.GetValue(null);
			PropertyInfo property4 = typeof(TBit).GetProperty("NewValueInPhysicalIndexFunc");
			this.newValueInPhysicalIndexFunc = (Func<int, uint, int, int>)property4.GetValue(null);
			PropertyInfo property5 = typeof(TBit).GetProperty("CurrentValueInLogicIndexFunc");
			this.currentValueInLogicIndexFunc = (Func<int, uint, int>)property5.GetValue(null);
			if (typeof(TBit) == typeof(Bit1))
			{
				this.markBits = new Func<uint, int, int>(this.MarkBit);
			}
			else
			{
				this.markBits = new Func<uint, int, int>(this.MarkBits);
			}
			CountingBloomFilter<TBit>.maxValue = Math.Min(maxValue, (1 << CountingBloomFilter<TBit>.bitsForCount) - 1);
			this.array = new int[this.GetArrayLength(this.length)];
			for (int i = 0; i < this.array.Length; i++)
			{
				this.array[i] = 0;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000EFAA File Offset: 0x0000D1AA
		public uint Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000EFB4 File Offset: 0x0000D1B4
		public bool Add(byte[] bytes, int count)
		{
			ulong hashcode = FnvHash.Fnv1A64(bytes);
			return this.Add(hashcode, count);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000EFD0 File Offset: 0x0000D1D0
		public bool MaySignificant(byte[] bytes)
		{
			ulong hashcode = FnvHash.Fnv1A64(bytes);
			return this.MaySignificant(hashcode);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000EFEC File Offset: 0x0000D1EC
		public bool Add(string stringValue, int count)
		{
			ulong hashcode = FnvHash.Fnv1A64(stringValue);
			return this.Add(hashcode, count);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000F008 File Offset: 0x0000D208
		public bool MaySignificant(string stringValue)
		{
			ulong hashcode = FnvHash.Fnv1A64(stringValue);
			return this.MaySignificant(hashcode);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000F024 File Offset: 0x0000D224
		public bool Add(ulong hashcode, int count)
		{
			uint num = (uint)(hashcode >> 32) & uint.MaxValue;
			uint num2 = (uint)hashcode & uint.MaxValue;
			int num3 = CountingBloomFilter<TBit>.maxValue;
			for (int i = 0; i < this.hashNumbers; i++)
			{
				num3 = Math.Min(num3, this.markBits((uint)((ulong)num + (ulong)((long)i * (long)((ulong)num2)) & (ulong)(this.length - 1U)), count));
			}
			return num3 == CountingBloomFilter<TBit>.maxValue;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000F088 File Offset: 0x0000D288
		public bool MaySignificant(ulong hashcode)
		{
			uint num = (uint)(hashcode >> 32) & uint.MaxValue;
			uint num2 = (uint)hashcode & uint.MaxValue;
			for (int i = 0; i < this.hashNumbers; i++)
			{
				if (this.getBitsFunc(this.array, (uint)((ulong)num + (ulong)((long)i * (long)((ulong)num2)) & (ulong)(this.length - 1U))) != CountingBloomFilter<TBit>.maxValue)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000F0E4 File Offset: 0x0000D2E4
		public bool Add(uint hashcode, int count)
		{
			uint num = hashcode >> 16 & 65535U;
			uint num2 = hashcode & 65535U;
			int num3 = CountingBloomFilter<TBit>.maxValue;
			for (int i = 0; i < this.hashNumbers; i++)
			{
				num3 = Math.Min(num3, this.markBits((uint)((ulong)num + (ulong)((long)i * (long)((ulong)num2)) & (ulong)(this.length - 1U)), count));
			}
			return num3 == CountingBloomFilter<TBit>.maxValue;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000F14C File Offset: 0x0000D34C
		public bool MaySignificant(uint hashcode)
		{
			uint num = hashcode >> 16 & 65535U;
			uint num2 = hashcode & 65535U;
			for (int i = 0; i < this.hashNumbers; i++)
			{
				if (this.getBitsFunc(this.array, (uint)((ulong)num + (ulong)((long)i * (long)((ulong)num2)) & (ulong)(this.length - 1U))) != CountingBloomFilter<TBit>.maxValue)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000F1AB File Offset: 0x0000D3AB
		private uint GetArrayLength(uint logicLength)
		{
			if (logicLength <= 0U)
			{
				return 0U;
			}
			return (uint)(((ulong)logicLength * (ulong)((long)CountingBloomFilter<TBit>.bitsForCount) - 1UL) / 32UL) + 1U;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000F1C6 File Offset: 0x0000D3C6
		private int MarkBit(uint index, int value)
		{
			if (value != 0)
			{
				this.setBitsAction(this.array, index, value);
			}
			return this.getBitsFunc(this.array, index);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000F1F0 File Offset: 0x0000D3F0
		private int MarkBits(uint index, int value)
		{
			int num = 0;
			for (;;)
			{
				num++;
				int num2 = this.getPhysicalIndexFunc(index);
				int num3 = this.array[num2];
				int num4 = this.currentValueInLogicIndexFunc(num3, index);
				if (num4 >= CountingBloomFilter<TBit>.maxValue)
				{
					break;
				}
				int num5 = num4 + value;
				if (num5 >= CountingBloomFilter<TBit>.maxValue)
				{
					goto Block_2;
				}
				int value2 = this.newValueInPhysicalIndexFunc(num3, index, num5);
				int num6 = Interlocked.CompareExchange(ref this.array[num2], value2, num3);
				if (num3 == num6 || num >= 1000)
				{
					return num5;
				}
			}
			return CountingBloomFilter<TBit>.maxValue;
			Block_2:
			this.setBitsAction(this.array, index, CountingBloomFilter<TBit>.maxValue);
			return CountingBloomFilter<TBit>.maxValue;
		}

		// Token: 0x04000169 RID: 361
		private const int DefaultHashNumber = 4;

		// Token: 0x0400016A RID: 362
		private const int DefaultIndexOf2 = 10;

		// Token: 0x0400016B RID: 363
		private static int bitsForCount;

		// Token: 0x0400016C RID: 364
		private static int maxValue;

		// Token: 0x0400016D RID: 365
		private readonly Func<int[], uint, int> getBitsFunc;

		// Token: 0x0400016E RID: 366
		private readonly Action<int[], uint, int> setBitsAction;

		// Token: 0x0400016F RID: 367
		private readonly Func<uint, int, int> markBits;

		// Token: 0x04000170 RID: 368
		private readonly Func<uint, int> getPhysicalIndexFunc;

		// Token: 0x04000171 RID: 369
		private readonly Func<int, uint, int, int> newValueInPhysicalIndexFunc;

		// Token: 0x04000172 RID: 370
		private readonly Func<int, uint, int> currentValueInLogicIndexFunc;

		// Token: 0x04000173 RID: 371
		private readonly int[] array;

		// Token: 0x04000174 RID: 372
		private readonly int hashNumbers;

		// Token: 0x04000175 RID: 373
		private readonly uint length;
	}
}
