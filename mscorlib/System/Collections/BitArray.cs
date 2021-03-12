using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000464 RID: 1124
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class BitArray : ICollection, IEnumerable, ICloneable
	{
		// Token: 0x060036E8 RID: 14056 RVA: 0x000D25C1 File Offset: 0x000D07C1
		private BitArray()
		{
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x000D25C9 File Offset: 0x000D07C9
		[__DynamicallyInvokable]
		public BitArray(int length) : this(length, false)
		{
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x000D25D4 File Offset: 0x000D07D4
		[__DynamicallyInvokable]
		public BitArray(int length, bool defaultValue)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_array = new int[BitArray.GetArrayLength(length, 32)];
			this.m_length = length;
			int num = defaultValue ? -1 : 0;
			for (int i = 0; i < this.m_array.Length; i++)
			{
				this.m_array[i] = num;
			}
			this._version = 0;
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000D2648 File Offset: 0x000D0848
		[__DynamicallyInvokable]
		public BitArray(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (bytes.Length > 268435455)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ArrayTooLarge", new object[]
				{
					8
				}), "bytes");
			}
			this.m_array = new int[BitArray.GetArrayLength(bytes.Length, 4)];
			this.m_length = bytes.Length * 8;
			int num = 0;
			int num2 = 0;
			while (bytes.Length - num2 >= 4)
			{
				this.m_array[num++] = ((int)(bytes[num2] & byte.MaxValue) | (int)(bytes[num2 + 1] & byte.MaxValue) << 8 | (int)(bytes[num2 + 2] & byte.MaxValue) << 16 | (int)(bytes[num2 + 3] & byte.MaxValue) << 24);
				num2 += 4;
			}
			switch (bytes.Length - num2)
			{
			case 1:
				goto IL_103;
			case 2:
				break;
			case 3:
				this.m_array[num] = (int)(bytes[num2 + 2] & byte.MaxValue) << 16;
				break;
			default:
				goto IL_11C;
			}
			this.m_array[num] |= (int)(bytes[num2 + 1] & byte.MaxValue) << 8;
			IL_103:
			this.m_array[num] |= (int)(bytes[num2] & byte.MaxValue);
			IL_11C:
			this._version = 0;
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000D2778 File Offset: 0x000D0978
		[__DynamicallyInvokable]
		public BitArray(bool[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			this.m_array = new int[BitArray.GetArrayLength(values.Length, 32)];
			this.m_length = values.Length;
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i])
				{
					this.m_array[i / 32] |= 1 << i % 32;
				}
			}
			this._version = 0;
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x000D27F0 File Offset: 0x000D09F0
		[__DynamicallyInvokable]
		public BitArray(int[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length > 67108863)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ArrayTooLarge", new object[]
				{
					32
				}), "values");
			}
			this.m_array = new int[values.Length];
			this.m_length = values.Length * 32;
			Array.Copy(values, this.m_array, values.Length);
			this._version = 0;
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x000D2870 File Offset: 0x000D0A70
		[__DynamicallyInvokable]
		public BitArray(BitArray bits)
		{
			if (bits == null)
			{
				throw new ArgumentNullException("bits");
			}
			int arrayLength = BitArray.GetArrayLength(bits.m_length, 32);
			this.m_array = new int[arrayLength];
			this.m_length = bits.m_length;
			Array.Copy(bits.m_array, this.m_array, arrayLength);
			this._version = bits._version;
		}

		// Token: 0x1700082D RID: 2093
		[__DynamicallyInvokable]
		public bool this[int index]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Get(index);
			}
			[__DynamicallyInvokable]
			set
			{
				this.Set(index, value);
			}
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x000D28E8 File Offset: 0x000D0AE8
		[__DynamicallyInvokable]
		public bool Get(int index)
		{
			if (index < 0 || index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return (this.m_array[index / 32] & 1 << index % 32) != 0;
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x000D2924 File Offset: 0x000D0B24
		[__DynamicallyInvokable]
		public void Set(int index, bool value)
		{
			if (index < 0 || index >= this.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (value)
			{
				this.m_array[index / 32] |= 1 << index % 32;
			}
			else
			{
				this.m_array[index / 32] &= ~(1 << index % 32);
			}
			this._version++;
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x000D29A0 File Offset: 0x000D0BA0
		[__DynamicallyInvokable]
		public void SetAll(bool value)
		{
			int num = value ? -1 : 0;
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] = num;
			}
			this._version++;
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x000D29E8 File Offset: 0x000D0BE8
		[__DynamicallyInvokable]
		public BitArray And(BitArray value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length != value.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"));
			}
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] &= value.m_array[i];
			}
			this._version++;
			return this;
		}

		// Token: 0x060036F5 RID: 14069 RVA: 0x000D2A64 File Offset: 0x000D0C64
		[__DynamicallyInvokable]
		public BitArray Or(BitArray value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length != value.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"));
			}
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] |= value.m_array[i];
			}
			this._version++;
			return this;
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x000D2AE0 File Offset: 0x000D0CE0
		[__DynamicallyInvokable]
		public BitArray Xor(BitArray value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.Length != value.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ArrayLengthsDiffer"));
			}
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] ^= value.m_array[i];
			}
			this._version++;
			return this;
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x000D2B5C File Offset: 0x000D0D5C
		[__DynamicallyInvokable]
		public BitArray Not()
		{
			int arrayLength = BitArray.GetArrayLength(this.m_length, 32);
			for (int i = 0; i < arrayLength; i++)
			{
				this.m_array[i] = ~this.m_array[i];
			}
			this._version++;
			return this;
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060036F8 RID: 14072 RVA: 0x000D2BA3 File Offset: 0x000D0DA3
		// (set) Token: 0x060036F9 RID: 14073 RVA: 0x000D2BAC File Offset: 0x000D0DAC
		[__DynamicallyInvokable]
		public int Length
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_length;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				int arrayLength = BitArray.GetArrayLength(value, 32);
				if (arrayLength > this.m_array.Length || arrayLength + 256 < this.m_array.Length)
				{
					int[] array = new int[arrayLength];
					Array.Copy(this.m_array, array, (arrayLength > this.m_array.Length) ? this.m_array.Length : arrayLength);
					this.m_array = array;
				}
				if (value > this.m_length)
				{
					int num = BitArray.GetArrayLength(this.m_length, 32) - 1;
					int num2 = this.m_length % 32;
					if (num2 > 0)
					{
						this.m_array[num] &= (1 << num2) - 1;
					}
					Array.Clear(this.m_array, num + 1, arrayLength - num - 1);
				}
				this.m_length = value;
				this._version++;
			}
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x000D2C90 File Offset: 0x000D0E90
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (array is int[])
			{
				Array.Copy(this.m_array, 0, array, index, BitArray.GetArrayLength(this.m_length, 32));
				return;
			}
			if (array is byte[])
			{
				int arrayLength = BitArray.GetArrayLength(this.m_length, 8);
				if (array.Length - index < arrayLength)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				byte[] array2 = (byte[])array;
				for (int i = 0; i < arrayLength; i++)
				{
					array2[index + i] = (byte)(this.m_array[i / 4] >> i % 4 * 8 & 255);
				}
				return;
			}
			else
			{
				if (!(array is bool[]))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_BitArrayTypeUnsupported"));
				}
				if (array.Length - index < this.m_length)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
				}
				bool[] array3 = (bool[])array;
				for (int j = 0; j < this.m_length; j++)
				{
					array3[index + j] = ((this.m_array[j / 32] >> j % 32 & 1) != 0);
				}
				return;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060036FB RID: 14075 RVA: 0x000D2DD8 File Offset: 0x000D0FD8
		public int Count
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x000D2DE0 File Offset: 0x000D0FE0
		public object Clone()
		{
			return new BitArray(this.m_array)
			{
				_version = this._version,
				m_length = this.m_length
			};
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060036FD RID: 14077 RVA: 0x000D2E12 File Offset: 0x000D1012
		public object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060036FE RID: 14078 RVA: 0x000D2E34 File Offset: 0x000D1034
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060036FF RID: 14079 RVA: 0x000D2E37 File Offset: 0x000D1037
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x000D2E3A File Offset: 0x000D103A
		[__DynamicallyInvokable]
		public IEnumerator GetEnumerator()
		{
			return new BitArray.BitArrayEnumeratorSimple(this);
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x000D2E42 File Offset: 0x000D1042
		private static int GetArrayLength(int n, int div)
		{
			if (n <= 0)
			{
				return 0;
			}
			return (n - 1) / div + 1;
		}

		// Token: 0x0400180F RID: 6159
		private const int BitsPerInt32 = 32;

		// Token: 0x04001810 RID: 6160
		private const int BytesPerInt32 = 4;

		// Token: 0x04001811 RID: 6161
		private const int BitsPerByte = 8;

		// Token: 0x04001812 RID: 6162
		private int[] m_array;

		// Token: 0x04001813 RID: 6163
		private int m_length;

		// Token: 0x04001814 RID: 6164
		private int _version;

		// Token: 0x04001815 RID: 6165
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04001816 RID: 6166
		private const int _ShrinkThreshold = 256;

		// Token: 0x02000B78 RID: 2936
		[Serializable]
		private class BitArrayEnumeratorSimple : IEnumerator, ICloneable
		{
			// Token: 0x06006C99 RID: 27801 RVA: 0x0017673F File Offset: 0x0017493F
			internal BitArrayEnumeratorSimple(BitArray bitarray)
			{
				this.bitarray = bitarray;
				this.index = -1;
				this.version = bitarray._version;
			}

			// Token: 0x06006C9A RID: 27802 RVA: 0x00176761 File Offset: 0x00174961
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06006C9B RID: 27803 RVA: 0x0017676C File Offset: 0x0017496C
			public virtual bool MoveNext()
			{
				if (this.version != this.bitarray._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this.index < this.bitarray.Count - 1)
				{
					this.index++;
					this.currentElement = this.bitarray.Get(this.index);
					return true;
				}
				this.index = this.bitarray.Count;
				return false;
			}

			// Token: 0x17001284 RID: 4740
			// (get) Token: 0x06006C9C RID: 27804 RVA: 0x001767EC File Offset: 0x001749EC
			public virtual object Current
			{
				get
				{
					if (this.index == -1)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this.index >= this.bitarray.Count)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return this.currentElement;
				}
			}

			// Token: 0x06006C9D RID: 27805 RVA: 0x00176840 File Offset: 0x00174A40
			public void Reset()
			{
				if (this.version != this.bitarray._version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this.index = -1;
			}

			// Token: 0x0400346F RID: 13423
			private BitArray bitarray;

			// Token: 0x04003470 RID: 13424
			private int index;

			// Token: 0x04003471 RID: 13425
			private int version;

			// Token: 0x04003472 RID: 13426
			private bool currentElement;
		}
	}
}
