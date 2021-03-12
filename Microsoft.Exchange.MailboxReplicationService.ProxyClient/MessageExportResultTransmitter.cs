using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000008 RID: 8
	internal class MessageExportResultTransmitter
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002B68 File Offset: 0x00000D68
		public MessageExportResultTransmitter(IDataImport destination, bool clientIsDownlevel)
		{
			this.destination = destination;
			this.clientIsDownlevel = clientIsDownlevel;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002B80 File Offset: 0x00000D80
		public void SendMessageExportResults(List<BadMessageRec> badMessages)
		{
			List<MessageRec> list = null;
			if (this.clientIsDownlevel)
			{
				list = new List<MessageRec>();
				List<BadMessageRec> list2 = new List<BadMessageRec>();
				foreach (BadMessageRec badMessageRec in badMessages)
				{
					if (badMessageRec.Kind == BadItemKind.MissingItem)
					{
						list.Add(new MessageRec
						{
							EntryId = badMessageRec.EntryId,
							FolderId = badMessageRec.FolderId
						});
					}
					else
					{
						badMessageRec.XmlData = badMessageRec.Serialize(false);
						list2.Add(badMessageRec);
					}
				}
				badMessages = list2;
			}
			this.destination.SendMessage(new MessageExportResultsMessage(list, badMessages));
		}

		// Token: 0x04000018 RID: 24
		private IDataImport destination;

		// Token: 0x04000019 RID: 25
		private bool clientIsDownlevel;
	}
}
