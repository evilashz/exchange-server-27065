using System;
using System.Text;
using Microsoft.Exchange.Conversion;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200024A RID: 586
	internal class NetworkChannelPacket
	{
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x0005C836 File Offset: 0x0005AA36
		internal byte[] Buffer
		{
			get
			{
				return this.m_buf;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x0005C83E File Offset: 0x0005AA3E
		// (set) Token: 0x06001684 RID: 5764 RVA: 0x0005C846 File Offset: 0x0005AA46
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

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x0005C84F File Offset: 0x0005AA4F
		internal int Length
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x0005C857 File Offset: 0x0005AA57
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

		// Token: 0x06001687 RID: 5767 RVA: 0x0005C86C File Offset: 0x0005AA6C
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

		// Token: 0x06001688 RID: 5768 RVA: 0x0005C8CE File Offset: 0x0005AACE
		internal void SetLength(int newLen)
		{
			if (newLen > this.Capacity)
			{
				this.SetCapacity(newLen);
				this.m_length = newLen;
			}
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0005C8E8 File Offset: 0x0005AAE8
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

		// Token: 0x0600168A RID: 5770 RVA: 0x0005C930 File Offset: 0x0005AB30
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

		// Token: 0x0600168B RID: 5771 RVA: 0x0005C974 File Offset: 0x0005AB74
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

		// Token: 0x0600168C RID: 5772 RVA: 0x0005C9A1 File Offset: 0x0005ABA1
		internal NetworkChannelPacket()
		{
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0005C9A9 File Offset: 0x0005ABA9
		internal NetworkChannelPacket(int size)
		{
			this.m_buf = new byte[size];
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0005C9BD File Offset: 0x0005ABBD
		internal NetworkChannelPacket(byte[] initBuf)
		{
			this.m_buf = initBuf;
			this.m_position = 0;
			this.m_length = this.m_buf.Length;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0005C9E1 File Offset: 0x0005ABE1
		internal NetworkChannelPacket(byte[] initBuf, int initialWritePos)
		{
			this.m_buf = initBuf;
			this.m_position = initialWritePos;
			this.m_length = this.m_buf.Length;
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x0005CA05 File Offset: 0x0005AC05
		// (set) Token: 0x06001691 RID: 5777 RVA: 0x0005CA0D File Offset: 0x0005AC0D
		internal bool GrowthDisabled { get; set; }

		// Token: 0x06001692 RID: 5778 RVA: 0x0005CA16 File Offset: 0x0005AC16
		internal void PrepareToWrite()
		{
			this.m_position = 0;
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0005CA1F File Offset: 0x0005AC1F
		internal void Append(int val)
		{
			this.MakeSpaceToAppend(4);
			ExBitConverter.Write(val, this.m_buf, this.m_position);
			this.m_position += 4;
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0005CA49 File Offset: 0x0005AC49
		internal void Append(uint val)
		{
			this.MakeSpaceToAppend(4);
			ExBitConverter.Write(val, this.m_buf, this.m_position);
			this.m_position += 4;
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0005CA73 File Offset: 0x0005AC73
		internal void Append(long val)
		{
			this.MakeSpaceToAppend(8);
			ExBitConverter.Write(val, this.m_buf, this.m_position);
			this.m_position += 8;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0005CA9D File Offset: 0x0005AC9D
		internal void Append(ulong val)
		{
			this.MakeSpaceToAppend(8);
			ExBitConverter.Write(val, this.m_buf, this.m_position);
			this.m_position += 8;
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0005CAC7 File Offset: 0x0005ACC7
		internal void Append(ushort val)
		{
			this.MakeSpaceToAppend(2);
			ExBitConverter.Write(val, this.m_buf, this.m_position);
			this.m_position += 2;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0005CAF4 File Offset: 0x0005ACF4
		internal void Append(DateTime time)
		{
			long val = time.ToBinary();
			this.Append(val);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0005CB10 File Offset: 0x0005AD10
		internal void Append(Guid g)
		{
			this.MakeSpaceToAppend(16);
			ExBitConverter.Write(g, this.m_buf, this.m_position);
			this.m_position += 16;
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0005CB3C File Offset: 0x0005AD3C
		internal void Append(byte b)
		{
			this.MakeSpaceToAppend(1);
			this.m_buf[this.m_position++] = b;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0005CB6C File Offset: 0x0005AD6C
		internal void Append(bool b)
		{
			byte b2 = b ? 1 : 0;
			this.Append(b2);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0005CB89 File Offset: 0x0005AD89
		internal void Append(byte[] inBuf, int inOffset, int len)
		{
			this.MakeSpaceToAppend(len);
			Array.Copy(inBuf, inOffset, this.m_buf, this.m_position, len);
			this.m_position += len;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0005CBB4 File Offset: 0x0005ADB4
		internal void Append(byte[] buf, int len)
		{
			this.Append(buf, 0, len);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0005CBBF File Offset: 0x0005ADBF
		internal void Append(byte[] buf)
		{
			this.Append(buf, 0, buf.Length);
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0005CBCC File Offset: 0x0005ADCC
		internal bool ExtractBool()
		{
			byte b = this.ExtractUInt8();
			return b != 0;
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0005CBE8 File Offset: 0x0005ADE8
		internal byte ExtractUInt8()
		{
			return this.m_buf[this.m_position++];
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x0005CC10 File Offset: 0x0005AE10
		internal ushort ExtractUInt16()
		{
			ushort result = BitConverter.ToUInt16(this.m_buf, this.m_position);
			this.m_position += 2;
			return result;
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x0005CC40 File Offset: 0x0005AE40
		internal uint ExtractUInt32()
		{
			uint result = BitConverter.ToUInt32(this.m_buf, this.m_position);
			this.m_position += 4;
			return result;
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0005CC70 File Offset: 0x0005AE70
		internal int ExtractInt32()
		{
			int result = BitConverter.ToInt32(this.m_buf, this.m_position);
			this.m_position += 4;
			return result;
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0005CCA0 File Offset: 0x0005AEA0
		internal ulong ExtractUInt64()
		{
			ulong result = BitConverter.ToUInt64(this.m_buf, this.m_position);
			this.m_position += 8;
			return result;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0005CCD0 File Offset: 0x0005AED0
		internal long ExtractInt64()
		{
			long result = BitConverter.ToInt64(this.m_buf, this.m_position);
			this.m_position += 8;
			return result;
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0005CD00 File Offset: 0x0005AF00
		internal DateTime ExtractDateTime()
		{
			long dateData = this.ExtractInt64();
			return DateTime.FromBinary(dateData);
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0005CD1C File Offset: 0x0005AF1C
		internal Guid ExtractGuid()
		{
			byte[] b = this.ExtractBytes(16);
			return new Guid(b);
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x0005CD38 File Offset: 0x0005AF38
		internal byte[] ExtractBytes(int len)
		{
			byte[] array = new byte[len];
			Array.Copy(this.m_buf, this.m_position, array, 0, len);
			this.m_position += len;
			return array;
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0005CD6F File Offset: 0x0005AF6F
		internal byte[] ExtractBytes(int len, out int position)
		{
			position = this.m_position;
			this.m_position += len;
			return this.m_buf;
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x0005CD8D File Offset: 0x0005AF8D
		public static byte[] EncodeString(string str)
		{
			return Encoding.UTF8.GetBytes(str);
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x0005CD9A File Offset: 0x0005AF9A
		public static string DecodeString(byte[] buf, int offset, int len)
		{
			return Encoding.UTF8.GetString(buf, offset, len);
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x0005CDAC File Offset: 0x0005AFAC
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

		// Token: 0x060016AD RID: 5805 RVA: 0x0005CDE8 File Offset: 0x0005AFE8
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

		// Token: 0x060016AE RID: 5806 RVA: 0x0005CE68 File Offset: 0x0005B068
		public void AppendByteArray(byte[] bytes)
		{
			int num = bytes.Length;
			NetworkChannelPacket.CheckByteArrayLen(num);
			this.Append(num);
			this.Append(bytes, 0, num);
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0005CE90 File Offset: 0x0005B090
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

		// Token: 0x040008E0 RID: 2272
		private const int GuidLen = 16;

		// Token: 0x040008E1 RID: 2273
		public const int MaxStringLength = 65535;

		// Token: 0x040008E2 RID: 2274
		public const int MaxByteArrayLength = 2097152;

		// Token: 0x040008E3 RID: 2275
		protected int m_length;

		// Token: 0x040008E4 RID: 2276
		protected int m_position;

		// Token: 0x040008E5 RID: 2277
		protected byte[] m_buf;
	}
}
