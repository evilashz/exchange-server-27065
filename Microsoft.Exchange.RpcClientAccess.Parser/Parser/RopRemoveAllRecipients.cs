using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000326 RID: 806
	internal sealed class RopRemoveAllRecipients : InputRop
	{
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x00033E03 File Offset: 0x00032003
		internal override RopId RopId
		{
			get
			{
				return RopId.RemoveAllRecipients;
			}
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00033E07 File Offset: 0x00032007
		internal static Rop CreateRop()
		{
			return new RopRemoveAllRecipients();
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00033E0E File Offset: 0x0003200E
		internal void SetInput(byte logonIndex, byte handleTableIndex)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00033E18 File Offset: 0x00032018
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt32(0U);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00033E29 File Offset: 0x00032029
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x00033E57 File Offset: 0x00032057
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopRemoveAllRecipients.resultFactory;
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00033E5E File Offset: 0x0003205E
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			reader.ReadUInt32();
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00033E6F File Offset: 0x0003206F
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00033E84 File Offset: 0x00032084
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.RemoveAllRecipients(serverObject, RopRemoveAllRecipients.resultFactory);
		}

		// Token: 0x04000A41 RID: 2625
		private const RopId RopType = RopId.RemoveAllRecipients;

		// Token: 0x04000A42 RID: 2626
		private static RemoveAllRecipientsResultFactory resultFactory = new RemoveAllRecipientsResultFactory();
	}
}
