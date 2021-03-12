using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200031D RID: 797
	internal sealed class RopReadPerUserInformation : InputRop
	{
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x000330BF File Offset: 0x000312BF
		internal override RopId RopId
		{
			get
			{
				return RopId.ReadPerUserInformation;
			}
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x000330C3 File Offset: 0x000312C3
		internal static Rop CreateRop()
		{
			return new RopReadPerUserInformation();
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x000330CC File Offset: 0x000312CC
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" LTID=[").Append(this.longTermId).Append("]");
			stringBuilder.Append(" WantIfChanged=").Append(this.wantIfChanged);
			stringBuilder.Append(" DataOffset=").Append(this.dataOffset);
			stringBuilder.Append(" MaxDataSize=").Append(this.maxDataSize);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x0003314B File Offset: 0x0003134B
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreLongTermId longTermId, bool wantIfChanged, uint dataOffset, ushort maxDataSize)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.longTermId = longTermId;
			this.wantIfChanged = wantIfChanged;
			this.dataOffset = dataOffset;
			this.maxDataSize = maxDataSize;
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00033174 File Offset: 0x00031374
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.longTermId.Serialize(writer);
			writer.WriteBool(this.wantIfChanged);
			writer.WriteUInt32(this.dataOffset);
			writer.WriteUInt16(this.maxDataSize);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x000331AE File Offset: 0x000313AE
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulReadPerUserInformationResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x000331DC File Offset: 0x000313DC
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopReadPerUserInformation.resultFactory;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x000331E3 File Offset: 0x000313E3
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.longTermId = StoreLongTermId.Parse(reader);
			this.wantIfChanged = reader.ReadBool();
			this.dataOffset = reader.ReadUInt32();
			this.maxDataSize = reader.ReadUInt16();
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0003321D File Offset: 0x0003141D
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00033232 File Offset: 0x00031432
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.ReadPerUserInformation(serverObject, this.longTermId, this.wantIfChanged, this.dataOffset, this.maxDataSize, RopReadPerUserInformation.resultFactory);
		}

		// Token: 0x04000A13 RID: 2579
		private const RopId RopType = RopId.ReadPerUserInformation;

		// Token: 0x04000A14 RID: 2580
		private static ReadPerUserInformationResultFactory resultFactory = new ReadPerUserInformationResultFactory();

		// Token: 0x04000A15 RID: 2581
		private StoreLongTermId longTermId;

		// Token: 0x04000A16 RID: 2582
		private bool wantIfChanged;

		// Token: 0x04000A17 RID: 2583
		private uint dataOffset;

		// Token: 0x04000A18 RID: 2584
		private ushort maxDataSize;
	}
}
