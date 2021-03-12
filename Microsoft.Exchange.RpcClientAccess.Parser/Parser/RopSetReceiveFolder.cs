using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000342 RID: 834
	internal sealed class RopSetReceiveFolder : InputRop
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x0003546F File Offset: 0x0003366F
		internal override RopId RopId
		{
			get
			{
				return RopId.SetReceiveFolder;
			}
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x00035473 File Offset: 0x00033673
		internal static Rop CreateRop()
		{
			return new RopSetReceiveFolder();
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0003547A File Offset: 0x0003367A
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreId folderId, string messageClass)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.folderId = folderId;
			this.messageClass = messageClass;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x00035493 File Offset: 0x00033693
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable, IParseLogonTracker logonTracker)
		{
			base.InternalParseInput(reader, serverObjectHandleTable, logonTracker);
			this.folderId = StoreId.Parse(reader);
			this.messageClass = reader.ReadAsciiString(StringFlags.IncludeNull);
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x000354B7 File Offset: 0x000336B7
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.folderId.Serialize(writer);
			writer.WriteAsciiString(this.messageClass, StringFlags.IncludeNull);
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x000354DA File Offset: 0x000336DA
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x00035508 File Offset: 0x00033708
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSetReceiveFolder.resultFactory;
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0003550F File Offset: 0x0003370F
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00035524 File Offset: 0x00033724
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SetReceiveFolder(serverObject, this.folderId, this.messageClass, RopSetReceiveFolder.resultFactory);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x00035544 File Offset: 0x00033744
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" FID=").Append(this.folderId.ToString());
			stringBuilder.Append(" MessageClass=[").Append(this.messageClass).Append("]");
		}

		// Token: 0x04000A9F RID: 2719
		private const RopId RopType = RopId.SetReceiveFolder;

		// Token: 0x04000AA0 RID: 2720
		private static SetReceiveFolderResultFactory resultFactory = new SetReceiveFolderResultFactory();

		// Token: 0x04000AA1 RID: 2721
		private StoreId folderId;

		// Token: 0x04000AA2 RID: 2722
		private string messageClass;
	}
}
