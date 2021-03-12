using System;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200000D RID: 13
	internal struct BufDeserializer
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00002F17 File Offset: 0x00001117
		public BufDeserializer(byte[] workingBuf, int startOffset)
		{
			this.serializeIx = startOffset;
			this.buf = workingBuf;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002F27 File Offset: 0x00001127
		public void Reset(byte[] workingBuf, int startOffset)
		{
			this.serializeIx = startOffset;
			this.buf = workingBuf;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F38 File Offset: 0x00001138
		public long ExtractInt64()
		{
			long result = BitConverter.ToInt64(this.buf, this.serializeIx);
			this.serializeIx += 8;
			return result;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002F68 File Offset: 0x00001168
		public ulong ExtractUInt64()
		{
			ulong result = BitConverter.ToUInt64(this.buf, this.serializeIx);
			this.serializeIx += 8;
			return result;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002F98 File Offset: 0x00001198
		public int ExtractInt32()
		{
			int result = BitConverter.ToInt32(this.buf, this.serializeIx);
			this.serializeIx += 4;
			return result;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002FC8 File Offset: 0x000011C8
		public uint ExtractUInt32()
		{
			uint result = BitConverter.ToUInt32(this.buf, this.serializeIx);
			this.serializeIx += 4;
			return result;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002FF8 File Offset: 0x000011F8
		public ushort ExtractUInt16()
		{
			ushort result = BitConverter.ToUInt16(this.buf, this.serializeIx);
			this.serializeIx += 2;
			return result;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003028 File Offset: 0x00001228
		public DateTime ExtractDateTime()
		{
			long dateData = this.ExtractInt64();
			return DateTime.FromBinary(dateData);
		}

		// Token: 0x04000033 RID: 51
		private int serializeIx;

		// Token: 0x04000034 RID: 52
		private byte[] buf;
	}
}
