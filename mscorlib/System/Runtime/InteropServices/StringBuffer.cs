using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200092D RID: 2349
	internal class StringBuffer : NativeBuffer
	{
		// Token: 0x060060D3 RID: 24787 RVA: 0x0014A4A0 File Offset: 0x001486A0
		public StringBuffer(uint initialCapacity = 0U) : base((ulong)initialCapacity * 2UL)
		{
		}

		// Token: 0x060060D4 RID: 24788 RVA: 0x0014A4AD File Offset: 0x001486AD
		public StringBuffer(string initialContents) : base(0UL)
		{
			if (initialContents != null)
			{
				this.Append(initialContents, 0, -1);
			}
		}

		// Token: 0x060060D5 RID: 24789 RVA: 0x0014A4C3 File Offset: 0x001486C3
		public StringBuffer(StringBuffer initialContents) : base(0UL)
		{
			if (initialContents != null)
			{
				this.Append(initialContents, 0U);
			}
		}

		// Token: 0x170010F1 RID: 4337
		public unsafe char this[uint index]
		{
			[SecuritySafeCritical]
			get
			{
				if (index >= this._length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.CharPointer[(ulong)index * 2UL / 2UL];
			}
			[SecuritySafeCritical]
			set
			{
				if (index >= this._length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.CharPointer[(ulong)index * 2UL / 2UL] = value;
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x060060D8 RID: 24792 RVA: 0x0014A524 File Offset: 0x00148724
		public uint CharCapacity
		{
			[SecuritySafeCritical]
			get
			{
				ulong byteCapacity = base.ByteCapacity;
				ulong num = (byteCapacity == 0UL) ? 0UL : (byteCapacity / 2UL);
				if (num <= (ulong)-1)
				{
					return (uint)num;
				}
				return uint.MaxValue;
			}
		}

		// Token: 0x060060D9 RID: 24793 RVA: 0x0014A54D File Offset: 0x0014874D
		[SecuritySafeCritical]
		public void EnsureCharCapacity(uint minCapacity)
		{
			base.EnsureByteCapacity((ulong)minCapacity * 2UL);
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x060060DA RID: 24794 RVA: 0x0014A55A File Offset: 0x0014875A
		// (set) Token: 0x060060DB RID: 24795 RVA: 0x0014A562 File Offset: 0x00148762
		public unsafe uint Length
		{
			get
			{
				return this._length;
			}
			[SecuritySafeCritical]
			set
			{
				if (value == 4294967295U)
				{
					throw new ArgumentOutOfRangeException("Length");
				}
				this.EnsureCharCapacity(value + 1U);
				this.CharPointer[(ulong)value * 2UL / 2UL] = '\0';
				this._length = value;
			}
		}

		// Token: 0x060060DC RID: 24796 RVA: 0x0014A594 File Offset: 0x00148794
		[SecuritySafeCritical]
		public unsafe void SetLengthToFirstNull()
		{
			char* charPointer = this.CharPointer;
			uint charCapacity = this.CharCapacity;
			for (uint num = 0U; num < charCapacity; num += 1U)
			{
				if (charPointer[(ulong)num * 2UL / 2UL] == '\0')
				{
					this._length = num;
					return;
				}
			}
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x060060DD RID: 24797 RVA: 0x0014A5CE File Offset: 0x001487CE
		internal unsafe char* CharPointer
		{
			[SecurityCritical]
			get
			{
				return (char*)base.VoidPointer;
			}
		}

		// Token: 0x060060DE RID: 24798 RVA: 0x0014A5D8 File Offset: 0x001487D8
		[SecurityCritical]
		public unsafe bool Contains(char value)
		{
			char* charPointer = this.CharPointer;
			uint length = this._length;
			for (uint num = 0U; num < length; num += 1U)
			{
				if (*(charPointer++) == value)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060060DF RID: 24799 RVA: 0x0014A60B File Offset: 0x0014880B
		[SecuritySafeCritical]
		public bool StartsWith(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this._length >= (uint)value.Length && this.SubstringEquals(value, 0U, value.Length);
		}

		// Token: 0x060060E0 RID: 24800 RVA: 0x0014A63C File Offset: 0x0014883C
		[SecuritySafeCritical]
		public unsafe bool SubstringEquals(string value, uint startIndex = 0U, int count = -1)
		{
			if (value == null)
			{
				return false;
			}
			if (count < -1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (startIndex > this._length)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			uint num = (count == -1) ? (this._length - startIndex) : ((uint)count);
			if (checked(startIndex + num) > this._length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int length = value.Length;
			if (num != (uint)length)
			{
				return false;
			}
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = this.CharPointer + (ulong)startIndex * 2UL / 2UL;
				for (int i = 0; i < length; i++)
				{
					if (*(ptr2++) != ptr[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060060E1 RID: 24801 RVA: 0x0014A6EA File Offset: 0x001488EA
		[SecuritySafeCritical]
		public void Append(string value, int startIndex = 0, int count = -1)
		{
			this.CopyFrom(this._length, value, startIndex, count);
		}

		// Token: 0x060060E2 RID: 24802 RVA: 0x0014A6FB File Offset: 0x001488FB
		public void Append(StringBuffer value, uint startIndex = 0U)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length == 0U)
			{
				return;
			}
			value.CopyTo(startIndex, this, this._length, value.Length);
		}

		// Token: 0x060060E3 RID: 24803 RVA: 0x0014A728 File Offset: 0x00148928
		public void Append(StringBuffer value, uint startIndex, uint count)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (count == 0U)
			{
				return;
			}
			value.CopyTo(startIndex, this, this._length, count);
		}

		// Token: 0x060060E4 RID: 24804 RVA: 0x0014A74C File Offset: 0x0014894C
		[SecuritySafeCritical]
		public unsafe void CopyTo(uint bufferIndex, StringBuffer destination, uint destinationIndex, uint count)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (destinationIndex > destination._length)
			{
				throw new ArgumentOutOfRangeException("destinationIndex");
			}
			if (bufferIndex >= this._length)
			{
				throw new ArgumentOutOfRangeException("bufferIndex");
			}
			checked
			{
				if (this._length < bufferIndex + count)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count == 0U)
				{
					return;
				}
				uint num = destinationIndex + count;
				if (destination._length < num)
				{
					destination.Length = num;
				}
				Buffer.MemoryCopy((void*)(unchecked(this.CharPointer + (ulong)bufferIndex * 2UL / 2UL)), (void*)(unchecked(destination.CharPointer + (ulong)destinationIndex * 2UL / 2UL)), (long)(destination.ByteCapacity - unchecked((ulong)(checked(destinationIndex * 2U)))), (long)(unchecked((ulong)count) * 2UL));
			}
		}

		// Token: 0x060060E5 RID: 24805 RVA: 0x0014A7F4 File Offset: 0x001489F4
		[SecuritySafeCritical]
		public unsafe void CopyFrom(uint bufferIndex, string source, int sourceIndex = 0, int count = -1)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (bufferIndex > this._length)
			{
				throw new ArgumentOutOfRangeException("bufferIndex");
			}
			if (sourceIndex < 0 || sourceIndex >= source.Length)
			{
				throw new ArgumentOutOfRangeException("sourceIndex");
			}
			if (count == -1)
			{
				count = source.Length - sourceIndex;
			}
			if (count < 0 || source.Length - count < sourceIndex)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count == 0)
			{
				return;
			}
			uint num = bufferIndex + (uint)count;
			if (this._length < num)
			{
				this.Length = num;
			}
			fixed (string text = source)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				Buffer.MemoryCopy((void*)(ptr + sourceIndex), (void*)(this.CharPointer + (ulong)bufferIndex * 2UL / 2UL), checked((long)(base.ByteCapacity - unchecked((ulong)(checked(bufferIndex * 2U))))), (long)count * 2L);
			}
		}

		// Token: 0x060060E6 RID: 24806 RVA: 0x0014A8BC File Offset: 0x00148ABC
		[SecuritySafeCritical]
		public unsafe void TrimEnd(char[] values)
		{
			if (values == null || values.Length == 0 || this._length == 0U)
			{
				return;
			}
			char* ptr = this.CharPointer + (ulong)this._length * 2UL / 2UL - 1;
			while (this._length > 0U && Array.IndexOf<char>(values, *ptr) >= 0)
			{
				this.Length = this._length - 1U;
				ptr--;
			}
		}

		// Token: 0x060060E7 RID: 24807 RVA: 0x0014A916 File Offset: 0x00148B16
		[SecuritySafeCritical]
		public override string ToString()
		{
			if (this._length == 0U)
			{
				return string.Empty;
			}
			if (this._length > 2147483647U)
			{
				throw new InvalidOperationException();
			}
			return new string(this.CharPointer, 0, (int)this._length);
		}

		// Token: 0x060060E8 RID: 24808 RVA: 0x0014A94C File Offset: 0x00148B4C
		[SecuritySafeCritical]
		public string Substring(uint startIndex, int count = -1)
		{
			if (startIndex > ((this._length == 0U) ? 0U : (this._length - 1U)))
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			if (count < -1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			uint num = (count == -1) ? (this._length - startIndex) : ((uint)count);
			if (num > 2147483647U || checked(startIndex + num) > this._length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (num == 0U)
			{
				return string.Empty;
			}
			return new string(this.CharPointer + (ulong)startIndex * 2UL / 2UL, 0, (int)num);
		}

		// Token: 0x060060E9 RID: 24809 RVA: 0x0014A9D4 File Offset: 0x00148BD4
		[SecuritySafeCritical]
		public override void Free()
		{
			base.Free();
			this._length = 0U;
		}

		// Token: 0x04002ACB RID: 10955
		private uint _length;
	}
}
