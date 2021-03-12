using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000288 RID: 648
	internal sealed class RopCommitStream : InputRop
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x0002A50B File Offset: 0x0002870B
		internal override RopId RopId
		{
			get
			{
				return RopId.CommitStream;
			}
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0002A50F File Offset: 0x0002870F
		internal static Rop CreateRop()
		{
			return new RopCommitStream();
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0002A516 File Offset: 0x00028716
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0002A520 File Offset: 0x00028720
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0002A54E File Offset: 0x0002874E
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopCommitStream.resultFactory;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0002A555 File Offset: 0x00028755
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0002A56A File Offset: 0x0002876A
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.CommitStream(serverObject, RopCommitStream.resultFactory);
		}

		// Token: 0x04000740 RID: 1856
		private const RopId RopType = RopId.CommitStream;

		// Token: 0x04000741 RID: 1857
		private static CommitStreamResultFactory resultFactory = new CommitStreamResultFactory();
	}
}
