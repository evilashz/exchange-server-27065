using System;
using System.Text;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000358 RID: 856
	internal sealed class RopTransportDuplicateDeliveryCheck : InputRop
	{
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x00036433 File Offset: 0x00034633
		internal override RopId RopId
		{
			get
			{
				return RopId.TransportDuplicateDeliveryCheck;
			}
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0003643A File Offset: 0x0003463A
		internal static Rop CreateRop()
		{
			return new RopTransportDuplicateDeliveryCheck();
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00036441 File Offset: 0x00034641
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte flags, ExDateTime submitTime, string internetMessageId)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.flags = flags;
			this.submitTime = submitTime;
			this.internetMessageId = internetMessageId;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x00036462 File Offset: 0x00034662
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte(this.flags);
			writer.WriteInt64(PropertyValue.ExDateTimeToFileTimeUtc(this.submitTime));
			writer.WriteAsciiString(this.internetMessageId, StringFlags.IncludeNull | StringFlags.Sized16);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00036496 File Offset: 0x00034696
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x000364C4 File Offset: 0x000346C4
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopTransportDuplicateDeliveryCheck.resultFactory;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x000364CB File Offset: 0x000346CB
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.flags = reader.ReadByte();
			this.submitTime = PropertyValue.ExDateTimeFromFileTimeUtc(reader.ReadInt64());
			this.internetMessageId = reader.ReadAsciiString(StringFlags.IncludeNull | StringFlags.Sized16);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x000364FF File Offset: 0x000346FF
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00036514 File Offset: 0x00034714
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.TransportDuplicateDeliveryCheck(serverObject, this.flags, this.submitTime, this.internetMessageId, RopTransportDuplicateDeliveryCheck.resultFactory);
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0003653C File Offset: 0x0003473C
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.flags);
			stringBuilder.Append(" SubmitTime=").Append(this.submitTime);
			stringBuilder.Append(" InternetMessageId=").Append(this.internetMessageId);
		}

		// Token: 0x04000AEB RID: 2795
		private const RopId RopType = RopId.TransportDuplicateDeliveryCheck;

		// Token: 0x04000AEC RID: 2796
		private static TransportDuplicateDeliveryCheckResultFactory resultFactory = new TransportDuplicateDeliveryCheckResultFactory();

		// Token: 0x04000AED RID: 2797
		private byte flags;

		// Token: 0x04000AEE RID: 2798
		private ExDateTime submitTime;

		// Token: 0x04000AEF RID: 2799
		private string internetMessageId;
	}
}
