using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002AC RID: 684
	internal sealed class RopFastTransferGetIncrementalState : InputOutputRop
	{
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x0002C800 File Offset: 0x0002AA00
		internal override RopId RopId
		{
			get
			{
				return RopId.FastTransferGetIncrementalState;
			}
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0002C807 File Offset: 0x0002AA07
		internal static Rop CreateRop()
		{
			return new RopFastTransferGetIncrementalState();
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0002C80E File Offset: 0x0002AA0E
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0002C819 File Offset: 0x0002AA19
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulFastTransferGetIncrementalStateResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0002C847 File Offset: 0x0002AA47
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopFastTransferGetIncrementalState.resultFactory;
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0002C84E File Offset: 0x0002AA4E
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0002C863 File Offset: 0x0002AA63
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.FastTransferGetIncrementalState(serverObject, RopFastTransferGetIncrementalState.resultFactory);
		}

		// Token: 0x040007B8 RID: 1976
		private const RopId RopType = RopId.FastTransferGetIncrementalState;

		// Token: 0x040007B9 RID: 1977
		private static FastTransferGetIncrementalStateResultFactory resultFactory = new FastTransferGetIncrementalStateResultFactory();
	}
}
