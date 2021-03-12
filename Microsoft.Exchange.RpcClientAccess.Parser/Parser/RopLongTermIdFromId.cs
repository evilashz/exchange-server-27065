using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000300 RID: 768
	internal sealed class RopLongTermIdFromId : InputRop
	{
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x000316B7 File Offset: 0x0002F8B7
		internal override RopId RopId
		{
			get
			{
				return RopId.LongTermIdFromId;
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x000316BB File Offset: 0x0002F8BB
		internal static Rop CreateRop()
		{
			return new RopLongTermIdFromId();
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x000316C2 File Offset: 0x0002F8C2
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreId storeId)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.storeId = storeId;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x000316D3 File Offset: 0x0002F8D3
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.storeId.Serialize(writer);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x000316E9 File Offset: 0x0002F8E9
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulLongTermIdFromIdResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00031717 File Offset: 0x0002F917
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopLongTermIdFromId.resultFactory;
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x0003171E File Offset: 0x0002F91E
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.storeId = StoreId.Parse(reader);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00031734 File Offset: 0x0002F934
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00031749 File Offset: 0x0002F949
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.LongTermIdFromId(serverObject, this.storeId, RopLongTermIdFromId.resultFactory);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00031763 File Offset: 0x0002F963
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" ID=").Append(this.storeId.ToString());
		}

		// Token: 0x040009B3 RID: 2483
		private const RopId RopType = RopId.LongTermIdFromId;

		// Token: 0x040009B4 RID: 2484
		private static LongTermIdFromIdResultFactory resultFactory = new LongTermIdFromIdResultFactory();

		// Token: 0x040009B5 RID: 2485
		private StoreId storeId;
	}
}
