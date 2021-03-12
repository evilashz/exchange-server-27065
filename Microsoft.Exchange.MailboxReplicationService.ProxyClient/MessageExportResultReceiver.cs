using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000007 RID: 7
	internal class MessageExportResultReceiver : DisposableWrapper<IDataImport>, IDataImport, IDisposable
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002A80 File Offset: 0x00000C80
		public MessageExportResultReceiver(IDataImport destination, bool ownsDestination) : base(destination, ownsDestination)
		{
			this.MissingMessages = new List<MessageRec>();
			this.BadMessages = new List<BadMessageRec>();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002AA0 File Offset: 0x00000CA0
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public List<MessageRec> MissingMessages { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002AB1 File Offset: 0x00000CB1
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002AB9 File Offset: 0x00000CB9
		public List<BadMessageRec> BadMessages { get; private set; }

		// Token: 0x0600001A RID: 26 RVA: 0x00002AC2 File Offset: 0x00000CC2
		IDataMessage IDataImport.SendMessageAndWaitForReply(IDataMessage message)
		{
			return base.WrappedObject.SendMessageAndWaitForReply(message);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002AD0 File Offset: 0x00000CD0
		void IDataImport.SendMessage(IDataMessage message)
		{
			MessageExportResultsMessage messageExportResultsMessage = message as MessageExportResultsMessage;
			if (messageExportResultsMessage == null)
			{
				base.WrappedObject.SendMessage(message);
				return;
			}
			this.MissingMessages = messageExportResultsMessage.MissingMessages;
			this.BadMessages = messageExportResultsMessage.BadMessages;
			foreach (BadMessageRec badMessageRec in this.BadMessages)
			{
				badMessageRec.Failure = FailureRec.Create(new RemotePermanentException(new LocalizedString(badMessageRec.XmlData), null));
			}
		}
	}
}
