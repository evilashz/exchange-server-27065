using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000347 RID: 839
	internal sealed class RopSetTransport : InputRop
	{
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x000359D1 File Offset: 0x00033BD1
		internal override RopId RopId
		{
			get
			{
				return RopId.SetTransport;
			}
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x000359D5 File Offset: 0x00033BD5
		internal static Rop CreateRop()
		{
			return new RopSetTransport();
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x000359DC File Offset: 0x00033BDC
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x000359E6 File Offset: 0x00033BE6
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulSetTransportResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x00035A14 File Offset: 0x00033C14
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSetTransport.resultFactory;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x00035A1B File Offset: 0x00033C1B
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x00035A30 File Offset: 0x00033C30
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SetTransport(serverObject, RopSetTransport.resultFactory);
		}

		// Token: 0x04000AB0 RID: 2736
		private const RopId RopType = RopId.SetTransport;

		// Token: 0x04000AB1 RID: 2737
		private static SetTransportResultFactory resultFactory = new SetTransportResultFactory();
	}
}
