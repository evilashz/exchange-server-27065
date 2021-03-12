using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000283 RID: 643
	internal sealed class RopAbort : InputRop
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0002A174 File Offset: 0x00028374
		internal override RopId RopId
		{
			get
			{
				return RopId.Abort;
			}
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0002A178 File Offset: 0x00028378
		internal static Rop CreateRop()
		{
			return new RopAbort();
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0002A17F File Offset: 0x0002837F
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0002A189 File Offset: 0x00028389
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulAbortResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0002A1B7 File Offset: 0x000283B7
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopAbort.resultFactory;
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0002A1BE File Offset: 0x000283BE
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0002A1D3 File Offset: 0x000283D3
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.Abort(serverObject, RopAbort.resultFactory);
		}

		// Token: 0x04000733 RID: 1843
		private const RopId RopType = RopId.Abort;

		// Token: 0x04000734 RID: 1844
		private static AbortResultFactory resultFactory = new AbortResultFactory();
	}
}
