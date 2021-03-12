using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001B9 RID: 441
	internal abstract class MapiHttpRequest
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x0001E0CB File Offset: 0x0001C2CB
		protected MapiHttpRequest(ArraySegment<byte> auxiliaryBuffer)
		{
			this.auxiliaryBuffer = auxiliaryBuffer;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0001E0DA File Offset: 0x0001C2DA
		protected MapiHttpRequest(Reader reader)
		{
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0001E0E2 File Offset: 0x0001C2E2
		public ArraySegment<byte> AuxiliaryBuffer
		{
			get
			{
				return this.auxiliaryBuffer;
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0001E0EA File Offset: 0x0001C2EA
		protected void ParseAuxiliaryBuffer(Reader reader)
		{
			this.auxiliaryBuffer = reader.ReadSizeAndByteArraySegment(FieldLength.DWordSize);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001E0F9 File Offset: 0x0001C2F9
		protected void SerializeAuxiliaryBuffer(Writer writer)
		{
			writer.WriteSizedBytesSegment(this.auxiliaryBuffer, FieldLength.DWordSize);
		}

		// Token: 0x06000960 RID: 2400
		public abstract void Serialize(Writer writer);

		// Token: 0x04000417 RID: 1047
		private ArraySegment<byte> auxiliaryBuffer;
	}
}
