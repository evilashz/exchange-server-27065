using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000360 RID: 864
	internal sealed class RopUploadStateStreamEnd : InputRop
	{
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x00036BA5 File Offset: 0x00034DA5
		internal override RopId RopId
		{
			get
			{
				return RopId.UploadStateStreamEnd;
			}
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00036BA9 File Offset: 0x00034DA9
		internal static Rop CreateRop()
		{
			return new RopUploadStateStreamEnd();
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00036BB0 File Offset: 0x00034DB0
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00036BBA File Offset: 0x00034DBA
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x00036BC4 File Offset: 0x00034DC4
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x00036BF2 File Offset: 0x00034DF2
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopUploadStateStreamEnd.resultFactory;
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x00036BF9 File Offset: 0x00034DF9
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x00036C03 File Offset: 0x00034E03
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00036C18 File Offset: 0x00034E18
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.UploadStateStreamEnd(serverObject, RopUploadStateStreamEnd.resultFactory);
		}

		// Token: 0x04000B16 RID: 2838
		private const RopId RopType = RopId.UploadStateStreamEnd;

		// Token: 0x04000B17 RID: 2839
		private static UploadStateStreamEndResultFactory resultFactory = new UploadStateStreamEndResultFactory();
	}
}
