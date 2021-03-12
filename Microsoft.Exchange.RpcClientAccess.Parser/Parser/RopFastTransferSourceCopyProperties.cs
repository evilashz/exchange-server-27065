using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002B1 RID: 689
	internal sealed class RopFastTransferSourceCopyProperties : InputOutputRop
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0002CB52 File Offset: 0x0002AD52
		internal override RopId RopId
		{
			get
			{
				return RopId.FastTransferSourceCopyProperties;
			}
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0002CB56 File Offset: 0x0002AD56
		internal static Rop CreateRop()
		{
			return new RopFastTransferSourceCopyProperties();
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0002CB5D File Offset: 0x0002AD5D
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, byte level, FastTransferCopyPropertiesFlag flags, FastTransferSendOption sendOptions, PropertyTag[] propertyTags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.level = level;
			this.flags = flags;
			this.sendOptions = sendOptions;
			this.propertyTags = propertyTags;
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0002CB88 File Offset: 0x0002AD88
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte(this.level);
			writer.WriteByte((byte)this.flags);
			writer.WriteByte((byte)this.sendOptions);
			writer.WriteCountAndPropertyTagArray(this.propertyTags, FieldLength.WordSize);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0002CBC3 File Offset: 0x0002ADC3
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulFastTransferSourceCopyPropertiesResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0002CBF1 File Offset: 0x0002ADF1
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopFastTransferSourceCopyProperties.resultFactory;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0002CBF8 File Offset: 0x0002ADF8
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.level = reader.ReadByte();
			this.flags = (FastTransferCopyPropertiesFlag)reader.ReadByte();
			this.sendOptions = (FastTransferSendOption)reader.ReadByte();
			this.propertyTags = reader.ReadCountAndPropertyTagArray(FieldLength.WordSize);
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0002CC33 File Offset: 0x0002AE33
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0002CC48 File Offset: 0x0002AE48
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.FastTransferSourceCopyProperties(serverObject, this.level, this.flags, this.sendOptions, this.propertyTags, RopFastTransferSourceCopyProperties.resultFactory);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0002CC74 File Offset: 0x0002AE74
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" level=").Append(this.level.ToString());
			stringBuilder.Append(" flags=").Append(this.flags.ToString());
			stringBuilder.Append(" sendOptions=").Append(this.sendOptions.ToString());
			stringBuilder.Append(" propertyTags=[");
			Util.AppendToString<PropertyTag>(stringBuilder, this.propertyTags);
			stringBuilder.Append("]");
		}

		// Token: 0x040007CD RID: 1997
		private const RopId RopType = RopId.FastTransferSourceCopyProperties;

		// Token: 0x040007CE RID: 1998
		private static FastTransferSourceCopyPropertiesResultFactory resultFactory = new FastTransferSourceCopyPropertiesResultFactory();

		// Token: 0x040007CF RID: 1999
		private byte level;

		// Token: 0x040007D0 RID: 2000
		private FastTransferCopyPropertiesFlag flags;

		// Token: 0x040007D1 RID: 2001
		private FastTransferSendOption sendOptions;

		// Token: 0x040007D2 RID: 2002
		private PropertyTag[] propertyTags;
	}
}
