using System;
using System.Text;
using Microsoft.Exchange.Conversion;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200002E RID: 46
	internal class NetworkChannelPacket
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00005648 File Offset: 0x00003848
		internal byte[] Buffer
		{
			get
			{
				return this.m_buf;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00005650 File Offset: 0x00003850
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00005658 File Offset: 0x00003858
		internal int Position
		{
			get
			{
				return this.m_position;
			}
			set
			{
				this.m_position = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00005661 File Offset: 0x00003861
		internal int Length
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00005669 File Offset: 0x00003869
		internal int Capacity
		{
			get
			{
				if (this.m_buf == null)
				{
					return 0;
				}
				return this.m_buf.Length;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005680 File Offset: 0x00003880
		internal void SetCapacity(int lenNeeded)
		{
			if (this.m_buf == null)
			{
				this.m_buf = new byte[lenNeeded];
				return;
			}
			if (this.m_buf.Length < lenNeeded)
			{
				int num = Math.Max(lenNeeded, this.m_buf.Length * 2);
				byte[] array = new byte[num];
				Array.Copy(this.m_buf, 0, array, 0, this.m_buf.Length);
				this.m_buf = array;
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000056E2 File Offset: 0x000038E2
		internal void SetLength(int newLen)
		{
			if (newLen > this.Capacity)
			{
				this.SetCapacity(newLen);
				this.m_length = newLen;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000056FC File Offset: 0x000038FC
		private static ushort CheckStringLen(int expectedLen)
		{
			if (expectedLen > 65535)
			{
				NetworkChannel.StaticTraceError("CheckStringLen: {0} exceeds max string len: {1}", new object[]
				{
					expectedLen,
					65535
				});
				throw new NetworkDataOverflowGenericException();
			}
			return (ushort)expectedLen;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005744 File Offset: 0x00003944
		private static int CheckByteArrayLen(int expectedLen)
		{
			if (expectedLen > 2097152)
			{
				NetworkChannel.StaticTraceError("CheckByteArrayLen: {0} exceeds max byteArray len: {1}", new object[]
				{
					expectedLen,
					2097152
				});
				throw new NetworkDataOverflowGenericException();
			}
			return expectedLen;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005788 File Offset: 0x00003988
		private void MakeSpaceToAppend(int len)
		{
			if (this.GrowthDisabled)
			{
				return;
			}
			int num = this.m_position + len;
			this.SetCapacity(num);
			this.m_length = num;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000057B5 File Offset: 0x000039B5
		internal NetworkChannelPacket()
		{
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000057BD File Offset: 0x000039BD
		internal NetworkChannelPacket(int size)
		{
			this.m_buf = new byte[size];
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000057D1 File Offset: 0x000039D1
		internal NetworkChannelPacket(byte[] initBuf)
		{
			this.m_buf = initBuf;
			this.m_position = 0;
			this.m_length = this.m_buf.Length;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000057F5 File Offset: 0x000039F5
		internal NetworkChannelPacket(byte[] initBuf, int initialWritePos)
		{
			this.m_buf = initBuf;
			this.m_position = initialWritePos;
			this.m_length = this.m_buf.Length;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005819 File Offset: 0x00003A19
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00005821 File Offset: 0x00003A21
		internal bool GrowthDisabled { get; set; }

		// Token: 0x0600013B RID: 315 RVA: 0x0000582A File Offset: 0x00003A2A
		internal void PrepareToWrite()
		{
			this.m_position = 0;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005833 File Offset: 0x00003A33
		internal void Append(int val)
		{
			this.MakeSpaceToAppend(4);
			ExBitConverter.Write(val, this.m_buf, this.m_position);
			this.m_position += 4;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000585D File Offset: 0x00003A5D
		internal void Append(uint val)
		{
			this.MakeSpaceToAppend(4);
			ExBitConverter.Write(val, this.m_buf, this.m_position);
			this.m_position += 4;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005887 File Offset: 0x00003A87
		internal void Append(long val)
		{
			this.MakeSpaceToAppend(8);
			ExBitConverter.Write(val, this.m_buf, this.m_position);
			this.m_position += 8;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000058B1 File Offset: 0x00003AB1
		internal void Append(ulong val)
		{
			this.MakeSpaceToAppend(8);
			ExBitConverter.Write(val, this.m_buf, this.m_position);
			this.m_position += 8;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000058DB File Offset: 0x00003ADB
		internal void Append(ushort val)
		{
			this.MakeSpaceToAppend(2);
			ExBitConverter.Write(val, this.m_buf, this.m_position);
			this.m_position += 2;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005908 File Offset: 0x00003B08
		internal void Append(DateTime time)
		{
			long val = time.ToBinary();
			this.Append(val);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005924 File Offset: 0x00003B24
		internal void Append(Guid g)
		{
			this.MakeSpaceToAppend(16);
			ExBitConverter.Write(g, this.m_buf, this.m_position);
			this.m_position += 16;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005950 File Offset: 0x00003B50
		internal void Append(byte b)
		{
			this.MakeSpaceToAppend(1);
			this.m_buf[this.m_position++] = b;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005980 File Offset: 0x00003B80
		internal void Append(bool b)
		{
			byte b2 = b ? 1 : 0;
			this.Append(b2);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000599D File Offset: 0x00003B9D
		internal void Append(byte[] inBuf, int inOffset, int len)
		{
			this.MakeSpaceToAppend(len);
			Array.Copy(inBuf, inOffset, this.m_buf, this.m_position, len);
			this.m_position += len;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000059C8 File Offset: 0x00003BC8
		internal void Append(byte[] buf, int len)
		{
			this.Append(buf, 0, len);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000059D3 File Offset: 0x00003BD3
		internal void Append(byte[] buf)
		{
			this.Append(buf, 0, buf.Length);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000059E0 File Offset: 0x00003BE0
		internal bool ExtractBool()
		{
			byte b = this.ExtractUInt8();
			return b != 0;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000059FC File Offset: 0x00003BFC
		internal byte ExtractUInt8()
		{
			return this.m_buf[this.m_position++];
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005A24 File Offset: 0x00003C24
		internal ushort ExtractUInt16()
		{
			ushort result = BitConverter.ToUInt16(this.m_buf, this.m_position);
			this.m_position += 2;
			return result;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005A54 File Offset: 0x00003C54
		internal uint ExtractUInt32()
		{
			uint result = BitConverter.ToUInt32(this.m_buf, this.m_position);
			this.m_position += 4;
			return result;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005A84 File Offset: 0x00003C84
		internal int ExtractInt32()
		{
			int result = BitConverter.ToInt32(this.m_buf, this.m_position);
			this.m_position += 4;
			return result;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005AB4 File Offset: 0x00003CB4
		internal ulong ExtractUInt64()
		{
			ulong result = BitConverter.ToUInt64(this.m_buf, this.m_position);
			this.m_position += 8;
			return result;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005AE4 File Offset: 0x00003CE4
		internal long ExtractInt64()
		{
			long result = BitConverter.ToInt64(this.m_buf, this.m_position);
			this.m_position += 8;
			return result;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005B14 File Offset: 0x00003D14
		internal DateTime ExtractDateTime()
		{
			long dateData = this.ExtractInt64();
			return DateTime.FromBinary(dateData);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005B30 File Offset: 0x00003D30
		internal Guid ExtractGuid()
		{
			byte[] b = this.ExtractBytes(16);
			return new Guid(b);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005B4C File Offset: 0x00003D4C
		internal byte[] ExtractBytes(int len)
		{
			byte[] array = new byte[len];
			Array.Copy(this.m_buf, this.m_position, array, 0, len);
			this.m_position += len;
			return array;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005B83 File Offset: 0x00003D83
		internal byte[] ExtractBytes(int len, out int position)
		{
			position = this.m_position;
			this.m_position += len;
			return this.m_buf;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005BA1 File Offset: 0x00003DA1
		public static byte[] EncodeString(string str)
		{
			return Encoding.UTF8.GetBytes(str);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005BAE File Offset: 0x00003DAE
		public static string DecodeString(byte[] buf, int offset, int len)
		{
			return Encoding.UTF8.GetString(buf, offset, len);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005BC0 File Offset: 0x00003DC0
		public void Append(string str)
		{
			ushort num = 0;
			if (str != null)
			{
				byte[] array = NetworkChannelPacket.EncodeString(str);
				int expectedLen = array.Length;
				num = NetworkChannelPacket.CheckStringLen(expectedLen);
				this.Append(num);
				this.Append(array, (int)num);
				return;
			}
			this.Append(num);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005BFC File Offset: 0x00003DFC
		public string ExtractString()
		{
			ushort num = this.ExtractUInt16();
			int num2 = this.Length - this.Position;
			if ((int)num > num2)
			{
				NetworkChannel.StaticTraceError("ExtractString: {0} exceeds max string len: {1}", new object[]
				{
					num,
					num2
				});
				throw new NetworkCorruptDataGenericException();
			}
			string result;
			if (num > 0)
			{
				result = NetworkChannelPacket.DecodeString(this.m_buf, this.Position, (int)num);
				this.m_position += (int)num;
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005C7C File Offset: 0x00003E7C
		public void AppendByteArray(byte[] bytes)
		{
			int num = bytes.Length;
			NetworkChannelPacket.CheckByteArrayLen(num);
			this.Append(num);
			this.Append(bytes, 0, num);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public byte[] ExtractByteArray()
		{
			int num = this.ExtractInt32();
			int num2 = this.Length - this.Position;
			if (num > num2 || num < 0)
			{
				NetworkChannel.StaticTraceError("ExtractByteArray: {0} exceeds max byte len: {1}", new object[]
				{
					num,
					num2
				});
				throw new NetworkCorruptDataGenericException();
			}
			return this.ExtractBytes(num);
		}

		// Token: 0x040000CD RID: 205
		private const int GuidLen = 16;

		// Token: 0x040000CE RID: 206
		public const int MaxStringLength = 65535;

		// Token: 0x040000CF RID: 207
		public const int MaxByteArrayLength = 2097152;

		// Token: 0x040000D0 RID: 208
		protected int m_length;

		// Token: 0x040000D1 RID: 209
		protected int m_position;

		// Token: 0x040000D2 RID: 210
		protected byte[] m_buf;
	}
}
