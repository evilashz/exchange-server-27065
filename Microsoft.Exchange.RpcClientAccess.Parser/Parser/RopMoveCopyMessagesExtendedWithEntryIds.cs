using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200030A RID: 778
	internal sealed class RopMoveCopyMessagesExtendedWithEntryIds : RopMoveCopyMessagesExtendedBase
	{
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x00031D51 File Offset: 0x0002FF51
		internal override RopId RopId
		{
			get
			{
				return RopId.MoveCopyMessagesExtendedWithEntryIds;
			}
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00031D58 File Offset: 0x0002FF58
		internal static Rop CreateRop()
		{
			return new RopMoveCopyMessagesExtendedWithEntryIds();
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00031D5F File Offset: 0x0002FF5F
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = MoveCopyMessagesExtendedWithEntryIdsResultFactory.Parse(reader);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00031D78 File Offset: 0x0002FF78
		protected override void InternalExecute(IServerObject sourceServerObject, IServerObject destinationServerObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			MoveCopyMessagesExtendedWithEntryIdsResultFactory resultFactory = new MoveCopyMessagesExtendedWithEntryIdsResultFactory(base.LogonIndex, (uint)base.DestinationHandleTableIndex);
			this.result = ropHandler.MoveCopyMessagesExtendedWithEntryIds(sourceServerObject, destinationServerObject, base.MessageIds, base.ReportProgress, base.IsCopy, this.propertyValues, resultFactory);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00031DBE File Offset: 0x0002FFBE
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new MoveCopyMessagesExtendedWithEntryIdsResultFactory(base.LogonIndex, (uint)base.DestinationHandleTableIndex);
		}

		// Token: 0x040009D3 RID: 2515
		private const RopId RopType = RopId.MoveCopyMessagesExtendedWithEntryIds;
	}
}
