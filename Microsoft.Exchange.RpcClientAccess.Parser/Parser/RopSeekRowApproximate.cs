using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000331 RID: 817
	internal sealed class RopSeekRowApproximate : InputRop
	{
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x000344DC File Offset: 0x000326DC
		internal override RopId RopId
		{
			get
			{
				return RopId.SeekRowApproximate;
			}
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x000344E0 File Offset: 0x000326E0
		internal static Rop CreateRop()
		{
			return new RopSeekRowApproximate();
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x000344E7 File Offset: 0x000326E7
		internal void SetInput(byte logonIndex, byte handleTableIndex, uint numerator, uint denominator)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.numerator = numerator;
			this.denominator = denominator;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00034500 File Offset: 0x00032700
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt32(this.numerator);
			writer.WriteUInt32(this.denominator);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00034522 File Offset: 0x00032722
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00034550 File Offset: 0x00032750
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSeekRowApproximate.resultFactory;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00034557 File Offset: 0x00032757
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.numerator = reader.ReadUInt32();
			this.denominator = reader.ReadUInt32();
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00034579 File Offset: 0x00032779
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0003458E File Offset: 0x0003278E
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SeekRowApproximate(serverObject, this.numerator, this.denominator, RopSeekRowApproximate.resultFactory);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x000345AE File Offset: 0x000327AE
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Numerator=").Append(this.numerator);
			stringBuilder.Append(" Denominator=").Append(this.denominator);
		}

		// Token: 0x04000A5F RID: 2655
		private const RopId RopType = RopId.SeekRowApproximate;

		// Token: 0x04000A60 RID: 2656
		private static SeekRowApproximateResultFactory resultFactory = new SeekRowApproximateResultFactory();

		// Token: 0x04000A61 RID: 2657
		private uint numerator;

		// Token: 0x04000A62 RID: 2658
		private uint denominator;
	}
}
