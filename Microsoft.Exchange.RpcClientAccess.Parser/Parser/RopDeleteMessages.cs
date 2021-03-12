using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200029A RID: 666
	internal sealed class RopDeleteMessages : RopDeleteMessagesBase
	{
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x0002B80E File Offset: 0x00029A0E
		internal override RopId RopId
		{
			get
			{
				return RopId.DeleteMessages;
			}
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0002B812 File Offset: 0x00029A12
		internal static Rop CreateRop()
		{
			return new RopDeleteMessages();
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0002B819 File Offset: 0x00029A19
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = DeleteMessagesResultFactory.Parse(reader);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0002B830 File Offset: 0x00029A30
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			DeleteMessagesResultFactory resultFactory = new DeleteMessagesResultFactory(base.LogonIndex);
			this.result = ropHandler.DeleteMessages(serverObject, base.ReportProgress, base.IsOkToSendNonReadNotification, base.MessageIds, resultFactory);
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0002B869 File Offset: 0x00029A69
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new DeleteMessagesResultFactory(base.LogonIndex);
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0002B876 File Offset: 0x00029A76
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x04000789 RID: 1929
		private const RopId RopType = RopId.DeleteMessages;
	}
}
