using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000353 RID: 851
	internal abstract class RopTransportDeliverMessageBase : InputRop
	{
		// Token: 0x0600149B RID: 5275 RVA: 0x00036259 File Offset: 0x00034459
		internal void SetInput(byte logonIndex, byte handleTableIndex, TransportRecipientType recipientType)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.recipientType = recipientType;
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0003626A File Offset: 0x0003446A
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteInt32((int)this.recipientType);
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x00036280 File Offset: 0x00034480
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.recipientType = (TransportRecipientType)reader.ReadInt32();
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x00036296 File Offset: 0x00034496
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x04000ADB RID: 2779
		protected TransportRecipientType recipientType;
	}
}
