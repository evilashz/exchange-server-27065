using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000344 RID: 836
	internal sealed class RopSetSizeStream : InputRop
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x00035764 File Offset: 0x00033964
		internal override RopId RopId
		{
			get
			{
				return RopId.SetSizeStream;
			}
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00035768 File Offset: 0x00033968
		internal static Rop CreateRop()
		{
			return new RopSetSizeStream();
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0003576F File Offset: 0x0003396F
		internal void SetInput(byte logonIndex, byte handleTableIndex, ulong streamSize)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.streamSize = streamSize;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x00035780 File Offset: 0x00033980
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt64(this.streamSize);
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x00035796 File Offset: 0x00033996
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x000357C4 File Offset: 0x000339C4
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSetSizeStream.resultFactory;
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x000357CB File Offset: 0x000339CB
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.streamSize = reader.ReadUInt64();
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x000357E1 File Offset: 0x000339E1
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x000357F6 File Offset: 0x000339F6
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SetSizeStream(serverObject, this.streamSize, RopSetSizeStream.resultFactory);
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x00035810 File Offset: 0x00033A10
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Size=0x").Append(this.streamSize.ToString("X"));
		}

		// Token: 0x04000AA8 RID: 2728
		private const RopId RopType = RopId.SetSizeStream;

		// Token: 0x04000AA9 RID: 2729
		private static SetSizeStreamResultFactory resultFactory = new SetSizeStreamResultFactory();

		// Token: 0x04000AAA RID: 2730
		private ulong streamSize;
	}
}
