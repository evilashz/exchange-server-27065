using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002DF RID: 735
	internal sealed class RopGetStreamSize : InputRop
	{
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x0002F87A File Offset: 0x0002DA7A
		internal override RopId RopId
		{
			get
			{
				return RopId.GetStreamSize;
			}
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0002F87E File Offset: 0x0002DA7E
		internal static Rop CreateRop()
		{
			return new RopGetStreamSize();
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0002F885 File Offset: 0x0002DA85
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0002F88F File Offset: 0x0002DA8F
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0002F899 File Offset: 0x0002DA99
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetStreamSizeResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0002F8C7 File Offset: 0x0002DAC7
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopGetStreamSize.resultFactory;
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0002F8CE File Offset: 0x0002DACE
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0002F8E3 File Offset: 0x0002DAE3
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.GetStreamSize(serverObject, RopGetStreamSize.resultFactory);
		}

		// Token: 0x0400086C RID: 2156
		private const RopId RopType = RopId.GetStreamSize;

		// Token: 0x0400086D RID: 2157
		private static GetStreamSizeResultFactory resultFactory = new GetStreamSizeResultFactory();
	}
}
