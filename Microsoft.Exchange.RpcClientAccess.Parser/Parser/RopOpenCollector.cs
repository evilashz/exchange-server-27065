using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200030D RID: 781
	internal sealed class RopOpenCollector : InputOutputRop
	{
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x00032064 File Offset: 0x00030264
		internal override RopId RopId
		{
			get
			{
				return RopId.OpenCollector;
			}
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00032068 File Offset: 0x00030268
		internal static Rop CreateRop()
		{
			return new RopOpenCollector();
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0003206F File Offset: 0x0003026F
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndexInput, bool wantMessageCollector)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndexInput);
			this.wantMessageCollector = wantMessageCollector;
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00032082 File Offset: 0x00030282
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteBool(this.wantMessageCollector);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00032098 File Offset: 0x00030298
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulOpenCollectorResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000320C6 File Offset: 0x000302C6
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopOpenCollector.resultFactory;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x000320CD File Offset: 0x000302CD
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.wantMessageCollector = reader.ReadBool();
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x000320E3 File Offset: 0x000302E3
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x000320F8 File Offset: 0x000302F8
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.OpenCollector(serverObject, this.wantMessageCollector, RopOpenCollector.resultFactory);
		}

		// Token: 0x040009DA RID: 2522
		private const RopId RopType = RopId.OpenCollector;

		// Token: 0x040009DB RID: 2523
		private static OpenCollectorResultFactory resultFactory = new OpenCollectorResultFactory();

		// Token: 0x040009DC RID: 2524
		private bool wantMessageCollector;
	}
}
