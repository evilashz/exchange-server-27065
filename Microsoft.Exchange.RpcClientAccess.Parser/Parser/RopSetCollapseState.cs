using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000335 RID: 821
	internal sealed class RopSetCollapseState : InputRop
	{
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x0003483D File Offset: 0x00032A3D
		internal override RopId RopId
		{
			get
			{
				return RopId.SetCollapseState;
			}
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00034841 File Offset: 0x00032A41
		internal static Rop CreateRop()
		{
			return new RopSetCollapseState();
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00034848 File Offset: 0x00032A48
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte[] collapseState)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.collapseState = collapseState;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00034859 File Offset: 0x00032A59
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteSizedBytes(this.collapseState);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0003486F File Offset: 0x00032A6F
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulSetCollapseStateResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0003489D File Offset: 0x00032A9D
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSetCollapseState.resultFactory;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x000348A4 File Offset: 0x00032AA4
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.collapseState = reader.ReadSizeAndByteArray();
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x000348BA File Offset: 0x00032ABA
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x000348CF File Offset: 0x00032ACF
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SetCollapseState(serverObject, this.collapseState, RopSetCollapseState.resultFactory);
		}

		// Token: 0x04000A70 RID: 2672
		private const RopId RopType = RopId.SetCollapseState;

		// Token: 0x04000A71 RID: 2673
		private static SetCollapseStateResultFactory resultFactory = new SetCollapseStateResultFactory();

		// Token: 0x04000A72 RID: 2674
		private byte[] collapseState;
	}
}
