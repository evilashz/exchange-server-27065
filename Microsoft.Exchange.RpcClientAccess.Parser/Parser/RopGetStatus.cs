using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002DD RID: 733
	internal sealed class RopGetStatus : InputRop
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x0002F76C File Offset: 0x0002D96C
		internal override RopId RopId
		{
			get
			{
				return RopId.GetStatus;
			}
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0002F770 File Offset: 0x0002D970
		internal static Rop CreateRop()
		{
			return new RopGetStatus();
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0002F777 File Offset: 0x0002D977
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0002F781 File Offset: 0x0002D981
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetStatusResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0002F7AF File Offset: 0x0002D9AF
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopGetStatus.resultFactory;
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0002F7B6 File Offset: 0x0002D9B6
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0002F7CB File Offset: 0x0002D9CB
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.GetStatus(serverObject, RopGetStatus.resultFactory);
		}

		// Token: 0x04000868 RID: 2152
		private const RopId RopType = RopId.GetStatus;

		// Token: 0x04000869 RID: 2153
		private static GetStatusResultFactory resultFactory = new GetStatusResultFactory();
	}
}
