using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002B3 RID: 691
	internal sealed class RopFastTransferSourceCopyTo : InputOutputRop
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x0002CD1E File Offset: 0x0002AF1E
		internal override RopId RopId
		{
			get
			{
				return RopId.FastTransferSourceCopyTo;
			}
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0002CD22 File Offset: 0x0002AF22
		internal static Rop CreateRop()
		{
			return new RopFastTransferSourceCopyTo();
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0002CD29 File Offset: 0x0002AF29
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, byte level, FastTransferCopyFlag flags, FastTransferSendOption sendOptions, PropertyTag[] excludedPropertyTags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.level = level;
			this.flags = flags;
			this.sendOptions = sendOptions;
			this.excludedPropertyTags = excludedPropertyTags;
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0002CD54 File Offset: 0x0002AF54
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte(this.level);
			writer.WriteUInt32((uint)this.flags);
			writer.WriteByte((byte)this.sendOptions);
			writer.WriteCountAndPropertyTagArray(this.excludedPropertyTags, FieldLength.WordSize);
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0002CD8F File Offset: 0x0002AF8F
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulFastTransferSourceCopyToResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0002CDBD File Offset: 0x0002AFBD
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopFastTransferSourceCopyTo.resultFactory;
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0002CDC4 File Offset: 0x0002AFC4
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.level = reader.ReadByte();
			this.flags = (FastTransferCopyFlag)reader.ReadUInt32();
			this.sendOptions = (FastTransferSendOption)reader.ReadByte();
			this.excludedPropertyTags = reader.ReadCountAndPropertyTagArray(FieldLength.WordSize);
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0002CDFF File Offset: 0x0002AFFF
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0002CE14 File Offset: 0x0002B014
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.FastTransferSourceCopyTo(serverObject, this.level, this.flags, this.sendOptions, this.excludedPropertyTags, RopFastTransferSourceCopyTo.resultFactory);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0002CE40 File Offset: 0x0002B040
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Level=").Append(this.level);
			stringBuilder.Append(" Flags=").Append(this.flags);
			stringBuilder.Append(" SendOptions=").Append(this.sendOptions);
			stringBuilder.Append(" ExcludeTags=[");
			Util.AppendToString<PropertyTag>(stringBuilder, this.excludedPropertyTags);
			stringBuilder.Append("]");
		}

		// Token: 0x040007E0 RID: 2016
		private const RopId RopType = RopId.FastTransferSourceCopyTo;

		// Token: 0x040007E1 RID: 2017
		private static FastTransferSourceCopyToResultFactory resultFactory = new FastTransferSourceCopyToResultFactory();

		// Token: 0x040007E2 RID: 2018
		private byte level;

		// Token: 0x040007E3 RID: 2019
		private FastTransferCopyFlag flags;

		// Token: 0x040007E4 RID: 2020
		private FastTransferSendOption sendOptions;

		// Token: 0x040007E5 RID: 2021
		private PropertyTag[] excludedPropertyTags;
	}
}
