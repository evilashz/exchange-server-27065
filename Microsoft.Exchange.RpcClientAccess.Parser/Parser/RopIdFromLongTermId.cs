using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002E4 RID: 740
	internal sealed class RopIdFromLongTermId : InputRop
	{
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x0002FA2C File Offset: 0x0002DC2C
		internal override RopId RopId
		{
			get
			{
				return RopId.IdFromLongTermId;
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0002FA30 File Offset: 0x0002DC30
		internal static Rop CreateRop()
		{
			return new RopIdFromLongTermId();
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0002FA37 File Offset: 0x0002DC37
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreLongTermId longTermId)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.longTermId = longTermId;
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0002FA48 File Offset: 0x0002DC48
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.longTermId.Serialize(writer);
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0002FA5E File Offset: 0x0002DC5E
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulIdFromLongTermIdResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0002FA8C File Offset: 0x0002DC8C
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopIdFromLongTermId.resultFactory;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0002FA93 File Offset: 0x0002DC93
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.longTermId = StoreLongTermId.Parse(reader);
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0002FAA9 File Offset: 0x0002DCA9
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0002FABE File Offset: 0x0002DCBE
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.IdFromLongTermId(serverObject, this.longTermId, RopIdFromLongTermId.resultFactory);
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0002FAD8 File Offset: 0x0002DCD8
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" LTID=[").Append(this.longTermId).Append("]");
		}

		// Token: 0x04000920 RID: 2336
		private const RopId RopType = RopId.IdFromLongTermId;

		// Token: 0x04000921 RID: 2337
		private static IdFromLongTermIdResultFactory resultFactory = new IdFromLongTermIdResultFactory();

		// Token: 0x04000922 RID: 2338
		private StoreLongTermId longTermId;
	}
}
