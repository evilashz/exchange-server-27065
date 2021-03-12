using System;
using System.IO;
using Microsoft.Exchange.Diagnostics.Components.Diagnostics;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001AC RID: 428
	internal class CsvRowBuffer
	{
		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002B864 File Offset: 0x00029A64
		public CsvRowBuffer(int readSize)
		{
			ExTraceGlobals.CommonTracer.TraceDebug<int>((long)this.GetHashCode(), "CsvRowbuffer is constructed with readSize {0}", readSize);
			this.readSize = readSize;
			this.buffer = new byte[readSize];
			this.conversionBuffer = new byte[1024];
			this.fields = new CsvOffsetMap(44, 34);
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0002B8C0 File Offset: 0x00029AC0
		public int Count
		{
			get
			{
				return this.fields.Count;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0002B8CD File Offset: 0x00029ACD
		public bool AtEnd
		{
			get
			{
				return this.atEnd;
			}
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002B8D5 File Offset: 0x00029AD5
		public int GetLength(int index)
		{
			return this.fields.GetLength(index);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002B8E4 File Offset: 0x00029AE4
		public object Decode(int index, CsvDecoderCallback decoder)
		{
			ExTraceGlobals.CommonTracer.TraceDebug<int>((long)this.GetHashCode(), "CsvRowbuffer Decode index {0}", index);
			int num = this.GetOffset(index) + this.rowBase;
			int num2 = num + this.GetLength(index);
			return this.Decode(num, num2, decoder);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002B92C File Offset: 0x00029B2C
		public int Copy(int srcOffset, byte[] dest, int offset, int count, int[] fieldsToSkip)
		{
			ExTraceGlobals.CommonTracer.TraceDebug<int, int, int>((long)this.GetHashCode(), "CsvRowbuffer Copy srcOffset {0}, destOffset {1} and count {2}", srcOffset, offset, count);
			srcOffset += this.rowBase;
			int count2 = this.Count;
			int num = this.GetOffset(count2 - 1) + this.GetLength(count2 - 1) + this.rowBase;
			int num2 = Math.Min(num - srcOffset, count);
			if (fieldsToSkip == null || fieldsToSkip.Length == 0)
			{
				Buffer.BlockCopy(this.buffer, srcOffset, dest, offset, num2);
			}
			else
			{
				num2 = this.CopyFieldsToBuffer(dest, srcOffset, offset, num2, fieldsToSkip);
			}
			return num2;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002B9B5 File Offset: 0x00029BB5
		public void Reset()
		{
			ExTraceGlobals.CommonTracer.TraceDebug((long)this.GetHashCode(), "CsvRowbuffer Reset");
			this.rowBase = 0;
			this.offset = 0;
			this.end = 0;
			this.atEnd = false;
			this.fields.Reset();
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002B9F4 File Offset: 0x00029BF4
		public object ResetAndDecode(byte[] value, CsvDecoderCallback decoder)
		{
			this.Reset();
			this.buffer = value;
			this.end = value.Length;
			return this.Decode(0, value.Length, decoder);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002BA18 File Offset: 0x00029C18
		public long ReadNext(Stream src)
		{
			ExTraceGlobals.CommonTracer.TraceDebug((long)this.GetHashCode(), "CsvRowbuffer ReadNext");
			long num = 0L;
			bool flag = false;
			this.rowBase = this.offset;
			for (;;)
			{
				int num2 = this.fields.Parse(this.buffer, this.offset, this.end - this.offset, this.rowBase);
				num += (long)((num2 == -1) ? (this.end - this.offset) : (num2 - this.offset));
				this.offset = num2;
				if (this.offset == -1)
				{
					if (this.rowBase > 0)
					{
						Buffer.BlockCopy(this.buffer, this.rowBase, this.buffer, 0, this.end - this.rowBase);
						this.end -= this.rowBase;
						this.rowBase = 0;
					}
					else if (this.end == this.buffer.Length)
					{
						if (this.buffer.Length < 10485760)
						{
							byte[] dst = new byte[Math.Min(this.buffer.Length * 2, 10485760)];
							Buffer.BlockCopy(this.buffer, 0, dst, 0, this.buffer.Length);
							this.offset = this.buffer.Length;
							this.buffer = dst;
							this.rowBase = 0;
						}
						else
						{
							flag = true;
							this.end = 0;
						}
					}
					int num3;
					try
					{
						num3 = src.Read(this.buffer, this.end, Math.Min(this.buffer.Length - this.end, this.readSize));
					}
					catch (IOException ex)
					{
						ExTraceGlobals.CommonTracer.TraceError<string>((long)this.GetHashCode(), "CsvRowbuffer ReadNext read stream failed with error {0}", ex.Message);
						num3 = 0;
					}
					if (num3 == 0)
					{
						break;
					}
					this.offset = this.end;
					this.end += num3;
				}
				else
				{
					if (!flag)
					{
						return num;
					}
					flag = false;
					this.rowBase = this.offset;
				}
			}
			this.atEnd = true;
			return -1L;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002BC14 File Offset: 0x00029E14
		public int CopyFieldsToBuffer(byte[] dest, int readOffset, int writeOffset, int count, int[] fieldsToSkip)
		{
			int i = 0;
			int num = 0;
			int num2 = writeOffset;
			while (i < count)
			{
				if (num < fieldsToSkip.Length && readOffset - this.rowBase == this.fields.GetOffset(fieldsToSkip[num]))
				{
					int length = this.fields.GetLength(fieldsToSkip[num]);
					readOffset += length;
					i += length;
					if (this.buffer[readOffset] == 44)
					{
						readOffset++;
						i++;
					}
					num++;
				}
				else
				{
					dest[writeOffset] = this.buffer[readOffset];
					writeOffset++;
					readOffset++;
					i++;
				}
			}
			if (this.fields.GetLength(this.Count - 1) == 0 && readOffset >= this.fields.GetOffset(this.Count - 1) && writeOffset > 0 && dest[writeOffset - 1] == 13)
			{
				dest[--writeOffset] = 0;
			}
			if (fieldsToSkip[fieldsToSkip.Length - 1] == this.Count - 1 && readOffset >= this.fields.GetOffset(fieldsToSkip[fieldsToSkip.Length - 1]) && writeOffset > 0 && dest[writeOffset - 1] == 44)
			{
				dest[--writeOffset] = 0;
			}
			return writeOffset - num2;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002BD20 File Offset: 0x00029F20
		public byte[] GetRawFieldValue(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				return null;
			}
			int srcOffset = this.fields.GetOffset(index) + this.rowBase;
			int length = this.fields.GetLength(index);
			byte[] array = new byte[length];
			Buffer.BlockCopy(this.buffer, srcOffset, array, 0, length);
			return array;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002BD74 File Offset: 0x00029F74
		public bool IsHeaderRow()
		{
			return this.buffer != null && this.buffer.Length > 0 && this.buffer[this.rowBase] == 35;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0002BDA0 File Offset: 0x00029FA0
		private static bool FieldListIsSorted(int[] fieldList)
		{
			int num = -1;
			foreach (int num2 in fieldList)
			{
				if (num >= num2)
				{
					return false;
				}
				num = num2;
			}
			return true;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0002BDD8 File Offset: 0x00029FD8
		private object Decode(int offset, int end, CsvDecoderCallback decoder)
		{
			if (offset < end && this.buffer[offset] == 34)
			{
				int num = 0;
				bool flag = true;
				offset++;
				while (offset < end && num < this.conversionBuffer.Length)
				{
					byte b = this.buffer[offset++];
					if (b == 34)
					{
						flag = !flag;
					}
					if (flag)
					{
						this.conversionBuffer[num++] = b;
					}
					if (num == this.conversionBuffer.Length && this.conversionBuffer.Length < 10485760)
					{
						byte[] dst = new byte[Math.Min(this.conversionBuffer.Length * 2, 10485760)];
						Buffer.BlockCopy(this.conversionBuffer, 0, dst, 0, this.conversionBuffer.Length);
						this.conversionBuffer = dst;
					}
				}
				return decoder(this.conversionBuffer, 0, num);
			}
			return decoder(this.buffer, offset, end - offset);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002BEB0 File Offset: 0x0002A0B0
		private int GetOffset(int index)
		{
			return this.fields.GetOffset(index);
		}

		// Token: 0x040008AA RID: 2218
		private const int Capacity = 10485760;

		// Token: 0x040008AB RID: 2219
		private const byte Pound = 35;

		// Token: 0x040008AC RID: 2220
		private CsvOffsetMap fields;

		// Token: 0x040008AD RID: 2221
		private byte[] buffer;

		// Token: 0x040008AE RID: 2222
		private byte[] conversionBuffer;

		// Token: 0x040008AF RID: 2223
		private int rowBase;

		// Token: 0x040008B0 RID: 2224
		private int offset;

		// Token: 0x040008B1 RID: 2225
		private int end;

		// Token: 0x040008B2 RID: 2226
		private bool atEnd;

		// Token: 0x040008B3 RID: 2227
		private int readSize;
	}
}
