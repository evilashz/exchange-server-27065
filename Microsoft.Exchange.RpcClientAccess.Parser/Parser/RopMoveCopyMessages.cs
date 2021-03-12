using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000307 RID: 775
	internal sealed class RopMoveCopyMessages : RopMoveCopyMessagesBase
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x00031B92 File Offset: 0x0002FD92
		internal override RopId RopId
		{
			get
			{
				return RopId.MoveCopyMessages;
			}
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00031B96 File Offset: 0x0002FD96
		internal static Rop CreateRop()
		{
			return new RopMoveCopyMessages();
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00031B9D File Offset: 0x0002FD9D
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = MoveCopyMessagesResultFactory.Parse(reader);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00031BB4 File Offset: 0x0002FDB4
		protected override void InternalExecute(IServerObject sourceServerObject, IServerObject destinationServerObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			MoveCopyMessagesResultFactory resultFactory = new MoveCopyMessagesResultFactory(base.LogonIndex, (uint)base.DestinationHandleTableIndex);
			this.result = ropHandler.MoveCopyMessages(sourceServerObject, destinationServerObject, base.MessageIds, base.ReportProgress, base.IsCopy, resultFactory);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00031BF4 File Offset: 0x0002FDF4
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new MoveCopyMessagesResultFactory(base.LogonIndex, (uint)base.DestinationHandleTableIndex);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00031C07 File Offset: 0x0002FE07
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x040009D0 RID: 2512
		private const RopId RopType = RopId.MoveCopyMessages;
	}
}
