using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002BC RID: 700
	internal sealed class RopFreeBookmark : InputRop
	{
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x0002D8DF File Offset: 0x0002BADF
		internal override RopId RopId
		{
			get
			{
				return RopId.FreeBookmark;
			}
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0002D8E6 File Offset: 0x0002BAE6
		internal static Rop CreateRop()
		{
			return new RopFreeBookmark();
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0002D8ED File Offset: 0x0002BAED
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte[] bookmark)
		{
			Util.ThrowOnNullArgument(bookmark, "bookmark");
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.bookmark = bookmark;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0002D909 File Offset: 0x0002BB09
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteSizedBytes(this.bookmark);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0002D91F File Offset: 0x0002BB1F
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0002D94D File Offset: 0x0002BB4D
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopFreeBookmark.resultFactory;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0002D954 File Offset: 0x0002BB54
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.bookmark = reader.ReadSizeAndByteArray();
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0002D96A File Offset: 0x0002BB6A
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0002D97F File Offset: 0x0002BB7F
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.FreeBookmark(serverObject, this.bookmark, RopFreeBookmark.resultFactory);
		}

		// Token: 0x040007FD RID: 2045
		private const RopId RopType = RopId.FreeBookmark;

		// Token: 0x040007FE RID: 2046
		private static FreeBookmarkResultFactory resultFactory = new FreeBookmarkResultFactory();

		// Token: 0x040007FF RID: 2047
		private byte[] bookmark;
	}
}
