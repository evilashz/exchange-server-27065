using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000346 RID: 838
	internal sealed class RopSetSynchronizationNotificationGuid : InputRop
	{
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x000358E9 File Offset: 0x00033AE9
		internal override RopId RopId
		{
			get
			{
				return RopId.SetSynchronizationNotificationGuid;
			}
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x000358F0 File Offset: 0x00033AF0
		internal static Rop CreateRop()
		{
			return new RopSetSynchronizationNotificationGuid();
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x000358F7 File Offset: 0x00033AF7
		internal void SetInput(byte logonIndex, byte handleTableIndex, Guid notificationGuid)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.notificationGuid = notificationGuid;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00035908 File Offset: 0x00033B08
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteGuid(this.notificationGuid);
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0003591E File Offset: 0x00033B1E
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0003594C File Offset: 0x00033B4C
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSetSynchronizationNotificationGuid.resultFactory;
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x00035953 File Offset: 0x00033B53
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.notificationGuid = reader.ReadGuid();
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x00035969 File Offset: 0x00033B69
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0003597E File Offset: 0x00033B7E
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SetSynchronizationNotificationGuid(serverObject, this.notificationGuid, RopSetSynchronizationNotificationGuid.resultFactory);
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x00035998 File Offset: 0x00033B98
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" NotificationGuid=").Append(this.notificationGuid);
		}

		// Token: 0x04000AAD RID: 2733
		private const RopId RopType = RopId.SetSynchronizationNotificationGuid;

		// Token: 0x04000AAE RID: 2734
		private static SetSynchronizationNotificationGuidResultFactory resultFactory = new SetSynchronizationNotificationGuidResultFactory();

		// Token: 0x04000AAF RID: 2735
		private Guid notificationGuid;
	}
}
