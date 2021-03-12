using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002E0 RID: 736
	internal sealed class RopHardDeleteMessages : RopDeleteMessagesBase
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x0002F90B File Offset: 0x0002DB0B
		internal override RopId RopId
		{
			get
			{
				return RopId.HardDeleteMessages;
			}
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0002F912 File Offset: 0x0002DB12
		internal static Rop CreateRop()
		{
			return new RopHardDeleteMessages();
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0002F919 File Offset: 0x0002DB19
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = HardDeleteMessagesResultFactory.Parse(reader);
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0002F930 File Offset: 0x0002DB30
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			HardDeleteMessagesResultFactory resultFactory = new HardDeleteMessagesResultFactory(base.LogonIndex);
			this.result = ropHandler.HardDeleteMessages(serverObject, base.ReportProgress, base.IsOkToSendNonReadNotification, base.MessageIds, resultFactory);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0002F969 File Offset: 0x0002DB69
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new HardDeleteMessagesResultFactory(base.LogonIndex);
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0002F976 File Offset: 0x0002DB76
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0400086E RID: 2158
		private const RopId RopType = RopId.HardDeleteMessages;
	}
}
