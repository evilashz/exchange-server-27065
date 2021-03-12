using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000D3 RID: 211
	internal struct BufDeserializer
	{
		// Token: 0x06000882 RID: 2178 RVA: 0x00028BB4 File Offset: 0x00026DB4
		public BufDeserializer(byte[] workingBuf, int startOffset)
		{
			this.serializeIx = startOffset;
			this.buf = workingBuf;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00028BC4 File Offset: 0x00026DC4
		public void Reset(byte[] workingBuf, int startOffset)
		{
			this.serializeIx = startOffset;
			this.buf = workingBuf;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00028BD4 File Offset: 0x00026DD4
		public long ExtractInt64()
		{
			long result = BitConverter.ToInt64(this.buf, this.serializeIx);
			this.serializeIx += 8;
			return result;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00028C04 File Offset: 0x00026E04
		public ulong ExtractUInt64()
		{
			ulong result = BitConverter.ToUInt64(this.buf, this.serializeIx);
			this.serializeIx += 8;
			return result;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00028C34 File Offset: 0x00026E34
		public int ExtractInt32()
		{
			int result = BitConverter.ToInt32(this.buf, this.serializeIx);
			this.serializeIx += 4;
			return result;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00028C64 File Offset: 0x00026E64
		public uint ExtractUInt32()
		{
			uint result = BitConverter.ToUInt32(this.buf, this.serializeIx);
			this.serializeIx += 4;
			return result;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00028C94 File Offset: 0x00026E94
		public ushort ExtractUInt16()
		{
			ushort result = BitConverter.ToUInt16(this.buf, this.serializeIx);
			this.serializeIx += 2;
			return result;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00028CC4 File Offset: 0x00026EC4
		public DateTime ExtractDateTime()
		{
			long dateData = this.ExtractInt64();
			return DateTime.FromBinary(dateData);
		}

		// Token: 0x040003A9 RID: 937
		private int serializeIx;

		// Token: 0x040003AA RID: 938
		private byte[] buf;
	}
}
