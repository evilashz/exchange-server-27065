using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200035B RID: 859
	internal sealed class RopTransportSend : InputRop
	{
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x0003675D File Offset: 0x0003495D
		internal override RopId RopId
		{
			get
			{
				return RopId.TransportSend;
			}
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00036761 File Offset: 0x00034961
		internal static Rop CreateRop()
		{
			return new RopTransportSend();
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00036768 File Offset: 0x00034968
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00036772 File Offset: 0x00034972
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x00036794 File Offset: 0x00034994
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, (Reader readerParameter) => SuccessfulTransportSendResult.Parse(readerParameter, string8Encoding), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x000367DF File Offset: 0x000349DF
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new TransportSendResultFactory(outputBuffer.Count, connection.String8Encoding);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x000367F3 File Offset: 0x000349F3
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x000367FD File Offset: 0x000349FD
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x00036814 File Offset: 0x00034A14
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			TransportSendResultFactory resultFactory = new TransportSendResultFactory(outputBuffer.Count, serverObject.String8Encoding);
			this.result = ropHandler.TransportSend(serverObject, resultFactory);
		}

		// Token: 0x04000B08 RID: 2824
		private const RopId RopType = RopId.TransportSend;
	}
}
