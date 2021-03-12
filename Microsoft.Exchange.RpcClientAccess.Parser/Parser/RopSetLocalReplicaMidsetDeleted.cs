using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000339 RID: 825
	internal sealed class RopSetLocalReplicaMidsetDeleted : InputRop
	{
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00034B2A File Offset: 0x00032D2A
		internal override RopId RopId
		{
			get
			{
				return RopId.SetLocalReplicaMidsetDeleted;
			}
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00034B31 File Offset: 0x00032D31
		internal static Rop CreateRop()
		{
			return new RopSetLocalReplicaMidsetDeleted();
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00034B38 File Offset: 0x00032D38
		internal void SetInput(byte logonIndex, byte handleTableIndex, LongTermIdRange[] longTermIdRanges)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.longTermIdRanges = longTermIdRanges;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00034B49 File Offset: 0x00032D49
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteSizedLongTermIdRanges(this.longTermIdRanges);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00034B5F File Offset: 0x00032D5F
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00034B8D File Offset: 0x00032D8D
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SetLocalReplicaMidsetDeleted(serverObject, this.longTermIdRanges, RopSetLocalReplicaMidsetDeleted.resultFactory);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00034BA7 File Offset: 0x00032DA7
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSetLocalReplicaMidsetDeleted.resultFactory;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00034BAE File Offset: 0x00032DAE
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.longTermIdRanges = reader.ReadSizeAndLongTermIdRangeArray();
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00034BC4 File Offset: 0x00032DC4
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x04000A7B RID: 2683
		private const RopId RopType = RopId.SetLocalReplicaMidsetDeleted;

		// Token: 0x04000A7C RID: 2684
		private static SetLocalReplicaMidsetDeletedResultFactory resultFactory = new SetLocalReplicaMidsetDeletedResultFactory();

		// Token: 0x04000A7D RID: 2685
		private LongTermIdRange[] longTermIdRanges;
	}
}
