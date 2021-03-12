using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002D7 RID: 727
	internal sealed class RopGetReceiveFolderTable : InputRop
	{
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0002F292 File Offset: 0x0002D492
		internal override RopId RopId
		{
			get
			{
				return RopId.GetReceiveFolderTable;
			}
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0002F296 File Offset: 0x0002D496
		internal static Rop CreateRop()
		{
			return new RopGetReceiveFolderTable();
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0002F29D File Offset: 0x0002D49D
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0002F2C4 File Offset: 0x0002D4C4
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, (Reader readerParameter) => SuccessfulGetReceiveFolderTableResult.Parse(reader, string8Encoding), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0002F320 File Offset: 0x0002D520
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopGetReceiveFolderTable.resultFactory;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0002F327 File Offset: 0x0002D527
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0002F33C File Offset: 0x0002D53C
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.GetReceiveFolderTable(serverObject, RopGetReceiveFolderTable.resultFactory);
		}

		// Token: 0x04000845 RID: 2117
		private const RopId RopType = RopId.GetReceiveFolderTable;

		// Token: 0x04000846 RID: 2118
		private static GetReceiveFolderTableResultFactory resultFactory = new GetReceiveFolderTableResultFactory();
	}
}
