using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200044D RID: 1101
	internal class TransportPropertyReader
	{
		// Token: 0x060032B6 RID: 12982 RVA: 0x000C7818 File Offset: 0x000C5A18
		public TransportPropertyReader(byte[] data, int start, int length)
		{
			this.data = data;
			this.current = start;
			this.end = start + length;
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x060032B7 RID: 12983 RVA: 0x000C7837 File Offset: 0x000C5A37
		public Guid Range
		{
			get
			{
				return this.range;
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x060032B8 RID: 12984 RVA: 0x000C783F File Offset: 0x000C5A3F
		public uint Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x060032B9 RID: 12985 RVA: 0x000C7847 File Offset: 0x000C5A47
		public byte[] Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x000C7850 File Offset: 0x000C5A50
		public bool ReadNextProperty()
		{
			if (this.current == this.end)
			{
				return false;
			}
			this.range = this.ReadGuid();
			this.id = this.ReadUInt32();
			int num = this.ReadInt32();
			if (num < 0)
			{
				throw new TransportPropertyException("invalid property length");
			}
			this.value = this.Read(num);
			return true;
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x000C78AC File Offset: 0x000C5AAC
		private byte[] Read(int length)
		{
			if (length > this.end - this.current)
			{
				throw new TransportPropertyException("unexpected end of data");
			}
			byte[] array = new byte[length];
			Buffer.BlockCopy(this.data, this.current, array, 0, length);
			this.current += length;
			return array;
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000C7900 File Offset: 0x000C5B00
		private Guid ReadGuid()
		{
			byte[] b = this.Read(16);
			return new Guid(b);
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x000C791C File Offset: 0x000C5B1C
		private uint ReadUInt32()
		{
			if (4 > this.end - this.current)
			{
				throw new TransportPropertyException("unexpected end of data");
			}
			uint result = BitConverter.ToUInt32(this.data, this.current);
			this.current += 4;
			return result;
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x000C7968 File Offset: 0x000C5B68
		private int ReadInt32()
		{
			if (4 > this.end - this.current)
			{
				throw new TransportPropertyException("unexpected end of data");
			}
			int result = BitConverter.ToInt32(this.data, this.current);
			this.current += 4;
			return result;
		}

		// Token: 0x040019B1 RID: 6577
		private byte[] data;

		// Token: 0x040019B2 RID: 6578
		private int current;

		// Token: 0x040019B3 RID: 6579
		private int end;

		// Token: 0x040019B4 RID: 6580
		private Guid range;

		// Token: 0x040019B5 RID: 6581
		private uint id;

		// Token: 0x040019B6 RID: 6582
		private byte[] value;
	}
}
