using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002C3 RID: 707
	internal sealed class RopGetContentsTableExtended : InputOutputRop
	{
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x0002DFAC File Offset: 0x0002C1AC
		internal override RopId RopId
		{
			get
			{
				return RopId.GetContentsTableExtended;
			}
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0002DFB3 File Offset: 0x0002C1B3
		internal static Rop CreateRop()
		{
			return new RopGetContentsTableExtended();
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0002DFBA File Offset: 0x0002C1BA
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new GetContentsTableExtendedResultFactory(base.PeekReturnServerObjectHandleValue);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0002DFC7 File Offset: 0x0002C1C7
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.extendedTableFlags = (ExtendedTableFlags)reader.ReadInt32();
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0002DFDD File Offset: 0x0002C1DD
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0002DFF4 File Offset: 0x0002C1F4
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			GetContentsTableExtendedResultFactory resultFactory = new GetContentsTableExtendedResultFactory(base.PeekReturnServerObjectHandleValue);
			this.result = ropHandler.GetContentsTableExtended(serverObject, this.extendedTableFlags, resultFactory);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0002E021 File Offset: 0x0002C221
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, ExtendedTableFlags extendedTableFlags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.extendedTableFlags = extendedTableFlags;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0002E034 File Offset: 0x0002C234
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteInt32((int)this.extendedTableFlags);
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0002E04A File Offset: 0x0002C24A
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetContentsTableExtendedResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0002E078 File Offset: 0x0002C278
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.extendedTableFlags);
		}

		// Token: 0x0400080D RID: 2061
		private const RopId RopType = RopId.GetContentsTableExtended;

		// Token: 0x0400080E RID: 2062
		private ExtendedTableFlags extendedTableFlags;
	}
}
