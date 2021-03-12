using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001BB RID: 443
	internal abstract class MapiHttpResponse
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x0001E209 File Offset: 0x0001C409
		protected MapiHttpResponse(uint statusCode, ArraySegment<byte> auxiliaryBuffer)
		{
			this.statusCode = statusCode;
			this.auxiliaryBuffer = auxiliaryBuffer;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001E21F File Offset: 0x0001C41F
		protected MapiHttpResponse(Reader reader)
		{
			this.statusCode = reader.ReadUInt32();
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0001E233 File Offset: 0x0001C433
		public uint StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0001E23B File Offset: 0x0001C43B
		public ArraySegment<byte> AuxiliaryBuffer
		{
			get
			{
				return this.auxiliaryBuffer;
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0001E243 File Offset: 0x0001C443
		public virtual void Serialize(Writer writer)
		{
			writer.WriteUInt32(this.statusCode);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0001E254 File Offset: 0x0001C454
		public long GetSerializedSize()
		{
			long position;
			using (CountWriter countWriter = new CountWriter())
			{
				this.Serialize(countWriter);
				position = countWriter.Position;
			}
			return position;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001E294 File Offset: 0x0001C494
		public virtual void AppendLogString(StringBuilder stringBuilder)
		{
			if (this.statusCode == 0U)
			{
				stringBuilder.Append(";SC:0");
				return;
			}
			stringBuilder.Append(";SC:");
			stringBuilder.Append(this.statusCode);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001E2C4 File Offset: 0x0001C4C4
		protected void ParseAuxiliaryBuffer(Reader reader)
		{
			this.auxiliaryBuffer = reader.ReadSizeAndByteArraySegment(FieldLength.DWordSize);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0001E2D3 File Offset: 0x0001C4D3
		protected void SerializeAuxiliaryBuffer(Writer writer)
		{
			writer.WriteSizedBytesSegment(this.auxiliaryBuffer, FieldLength.DWordSize);
		}

		// Token: 0x0400041D RID: 1053
		private readonly uint statusCode;

		// Token: 0x0400041E RID: 1054
		private ArraySegment<byte> auxiliaryBuffer;
	}
}
