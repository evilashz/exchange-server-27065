using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000290 RID: 656
	internal sealed class RopCreateBookmark : InputRop
	{
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0002AE26 File Offset: 0x00029026
		internal override RopId RopId
		{
			get
			{
				return RopId.CreateBookmark;
			}
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0002AE2A File Offset: 0x0002902A
		internal static Rop CreateRop()
		{
			return new RopCreateBookmark();
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0002AE31 File Offset: 0x00029031
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0002AE3B File Offset: 0x0002903B
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulCreateBookmarkResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0002AE69 File Offset: 0x00029069
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopCreateBookmark.resultFactory;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0002AE70 File Offset: 0x00029070
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0002AE85 File Offset: 0x00029085
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.CreateBookmark(serverObject, RopCreateBookmark.resultFactory);
		}

		// Token: 0x04000759 RID: 1881
		private const RopId RopType = RopId.CreateBookmark;

		// Token: 0x0400075A RID: 1882
		private static CreateBookmarkResultFactory resultFactory = new CreateBookmarkResultFactory();
	}
}
