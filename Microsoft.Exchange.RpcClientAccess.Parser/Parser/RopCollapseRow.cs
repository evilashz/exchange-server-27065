using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000287 RID: 647
	internal sealed class RopCollapseRow : InputRop
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0002A44B File Offset: 0x0002864B
		internal override RopId RopId
		{
			get
			{
				return RopId.CollapseRow;
			}
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0002A44F File Offset: 0x0002864F
		internal static Rop CreateRop()
		{
			return new RopCollapseRow();
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0002A456 File Offset: 0x00028656
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreId categoryId)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.categoryId = categoryId;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0002A467 File Offset: 0x00028667
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.categoryId.Serialize(writer);
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0002A47D File Offset: 0x0002867D
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulCollapseRowResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0002A4AB File Offset: 0x000286AB
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopCollapseRow.resultFactory;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0002A4B2 File Offset: 0x000286B2
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.categoryId = StoreId.Parse(reader);
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0002A4C8 File Offset: 0x000286C8
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0002A4DD File Offset: 0x000286DD
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.CollapseRow(serverObject, this.categoryId, RopCollapseRow.resultFactory);
		}

		// Token: 0x0400073D RID: 1853
		private const RopId RopType = RopId.CollapseRow;

		// Token: 0x0400073E RID: 1854
		private static CollapseRowResultFactory resultFactory = new CollapseRowResultFactory();

		// Token: 0x0400073F RID: 1855
		private StoreId categoryId;
	}
}
