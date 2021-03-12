using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000309 RID: 777
	internal sealed class RopMoveCopyMessagesExtended : RopMoveCopyMessagesExtendedBase
	{
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x00031CCA File Offset: 0x0002FECA
		internal override RopId RopId
		{
			get
			{
				return RopId.MoveCopyMessagesExtended;
			}
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00031CD1 File Offset: 0x0002FED1
		internal static Rop CreateRop()
		{
			return new RopMoveCopyMessagesExtended();
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00031CD8 File Offset: 0x0002FED8
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = MoveCopyMessagesExtendedResultFactory.Parse(reader);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00031CF0 File Offset: 0x0002FEF0
		protected override void InternalExecute(IServerObject sourceServerObject, IServerObject destinationServerObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			MoveCopyMessagesExtendedResultFactory resultFactory = new MoveCopyMessagesExtendedResultFactory(base.LogonIndex, (uint)base.DestinationHandleTableIndex);
			this.result = ropHandler.MoveCopyMessagesExtended(sourceServerObject, destinationServerObject, base.MessageIds, base.ReportProgress, base.IsCopy, this.propertyValues, resultFactory);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00031D36 File Offset: 0x0002FF36
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new MoveCopyMessagesExtendedResultFactory(base.LogonIndex, (uint)base.DestinationHandleTableIndex);
		}

		// Token: 0x040009D2 RID: 2514
		private const RopId RopType = RopId.MoveCopyMessagesExtended;
	}
}
