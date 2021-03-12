using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002D5 RID: 725
	internal sealed class RopGetReceiveFolder : InputRop
	{
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x0002F0C7 File Offset: 0x0002D2C7
		internal override RopId RopId
		{
			get
			{
				return RopId.GetReceiveFolder;
			}
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0002F0CB File Offset: 0x0002D2CB
		internal static Rop CreateRop()
		{
			return new RopGetReceiveFolder();
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0002F0D2 File Offset: 0x0002D2D2
		internal void SetInput(byte logonIndex, byte handleTableIndex, string messageClass)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.messageClass = messageClass;
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0002F0E3 File Offset: 0x0002D2E3
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteAsciiString(this.messageClass, StringFlags.IncludeNull);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0002F0FA File Offset: 0x0002D2FA
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetReceiveFolderResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0002F128 File Offset: 0x0002D328
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopGetReceiveFolder.resultFactory;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0002F12F File Offset: 0x0002D32F
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.messageClass = reader.ReadAsciiString(StringFlags.IncludeNull);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0002F146 File Offset: 0x0002D346
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0002F15B File Offset: 0x0002D35B
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.GetReceiveFolder(serverObject, this.messageClass, RopGetReceiveFolder.resultFactory);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0002F175 File Offset: 0x0002D375
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Message Class=[").Append(this.messageClass).Append("]");
		}

		// Token: 0x04000840 RID: 2112
		private const RopId RopType = RopId.GetReceiveFolder;

		// Token: 0x04000841 RID: 2113
		private static GetReceiveFolderResultFactory resultFactory = new GetReceiveFolderResultFactory();

		// Token: 0x04000842 RID: 2114
		private string messageClass;
	}
}
