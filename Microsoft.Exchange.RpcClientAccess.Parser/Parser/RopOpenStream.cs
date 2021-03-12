using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000311 RID: 785
	internal sealed class RopOpenStream : InputOutputRop
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x000325C7 File Offset: 0x000307C7
		internal override RopId RopId
		{
			get
			{
				return RopId.OpenStream;
			}
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x000325CB File Offset: 0x000307CB
		internal static Rop CreateRop()
		{
			return new RopOpenStream();
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x000325D2 File Offset: 0x000307D2
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndexInput, PropertyTag propertyTagInput, OpenMode openModeInput)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndexInput);
			this.propertyTag = propertyTagInput;
			this.openMode = openModeInput;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x000325ED File Offset: 0x000307ED
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WritePropertyTag(this.propertyTag);
			writer.WriteByte((byte)this.openMode);
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0003260F File Offset: 0x0003080F
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulOpenStreamResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0003263D File Offset: 0x0003083D
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopOpenStream.resultFactory;
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00032644 File Offset: 0x00030844
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.propertyTag = reader.ReadPropertyTag();
			this.openMode = (OpenMode)reader.ReadByte();
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00032666 File Offset: 0x00030866
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x0003267B File Offset: 0x0003087B
		internal override int? MinimumResponseSizeRequired
		{
			get
			{
				return new int?(10);
			}
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00032684 File Offset: 0x00030884
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.OpenStream(serverObject, this.propertyTag, this.openMode, RopOpenStream.resultFactory);
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x000326A4 File Offset: 0x000308A4
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" OpenMode=").Append(this.openMode);
			stringBuilder.Append(" Tag=").Append(this.propertyTag);
		}

		// Token: 0x040009E9 RID: 2537
		private const RopId RopType = RopId.OpenStream;

		// Token: 0x040009EA RID: 2538
		private static OpenStreamResultFactory resultFactory = new OpenStreamResultFactory();

		// Token: 0x040009EB RID: 2539
		private PropertyTag propertyTag;

		// Token: 0x040009EC RID: 2540
		private OpenMode openMode;
	}
}
