using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000352 RID: 850
	internal sealed class RopTellVersion : InputRop
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x000360F0 File Offset: 0x000342F0
		internal override RopId RopId
		{
			get
			{
				return RopId.TellVersion;
			}
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x000360F7 File Offset: 0x000342F7
		internal static Rop CreateRop()
		{
			return new RopTellVersion();
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x000360FE File Offset: 0x000342FE
		internal void SetInput(byte logonIndex, byte handleTableIndex, ushort productMajorVersion, ushort buildMajorVersion, ushort buildMinorVersion)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.productMajorVersion = productMajorVersion;
			this.buildMajorVersion = buildMajorVersion;
			this.buildMinorVersion = buildMinorVersion;
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0003611F File Offset: 0x0003431F
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt16(this.productMajorVersion);
			writer.WriteUInt16(this.buildMajorVersion);
			writer.WriteUInt16(this.buildMinorVersion);
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0003614D File Offset: 0x0003434D
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulTellVersionResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0003617B File Offset: 0x0003437B
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopTellVersion.resultFactory;
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x00036182 File Offset: 0x00034382
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.productMajorVersion = reader.ReadUInt16();
			this.buildMajorVersion = reader.ReadUInt16();
			this.buildMinorVersion = reader.ReadUInt16();
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x000361B0 File Offset: 0x000343B0
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x000361C5 File Offset: 0x000343C5
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.TellVersion(serverObject, this.productMajorVersion, this.buildMajorVersion, this.buildMinorVersion, RopTellVersion.resultFactory);
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x000361EC File Offset: 0x000343EC
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Product=").Append(this.productMajorVersion);
			stringBuilder.Append(" BuildMajor=").Append(this.buildMajorVersion);
			stringBuilder.Append(" BuildMinor=").Append(this.buildMinorVersion);
		}

		// Token: 0x04000AD6 RID: 2774
		private const RopId RopType = RopId.TellVersion;

		// Token: 0x04000AD7 RID: 2775
		private static TellVersionResultFactory resultFactory = new TellVersionResultFactory();

		// Token: 0x04000AD8 RID: 2776
		private ushort productMajorVersion;

		// Token: 0x04000AD9 RID: 2777
		private ushort buildMajorVersion;

		// Token: 0x04000ADA RID: 2778
		private ushort buildMinorVersion;
	}
}
