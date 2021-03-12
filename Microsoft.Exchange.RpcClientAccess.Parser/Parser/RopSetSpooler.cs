using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000345 RID: 837
	internal sealed class RopSetSpooler : InputRop
	{
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0003584E File Offset: 0x00033A4E
		internal override RopId RopId
		{
			get
			{
				return RopId.SetSpooler;
			}
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00035852 File Offset: 0x00033A52
		internal static Rop CreateRop()
		{
			return new RopSetSpooler();
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x00035859 File Offset: 0x00033A59
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00035863 File Offset: 0x00033A63
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0003586D File Offset: 0x00033A6D
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0003589B File Offset: 0x00033A9B
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSetSpooler.resultFactory;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x000358A2 File Offset: 0x00033AA2
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x000358AC File Offset: 0x00033AAC
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x000358C1 File Offset: 0x00033AC1
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SetSpooler(serverObject, RopSetSpooler.resultFactory);
		}

		// Token: 0x04000AAB RID: 2731
		private const RopId RopType = RopId.SetSpooler;

		// Token: 0x04000AAC RID: 2732
		private static SetSpoolerResultFactory resultFactory = new SetSpoolerResultFactory();
	}
}
