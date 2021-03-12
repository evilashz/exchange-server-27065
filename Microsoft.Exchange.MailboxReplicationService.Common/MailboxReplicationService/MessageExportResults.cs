using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200015D RID: 349
	public class MessageExportResults : XMLSerializableBase
	{
		// Token: 0x06000C53 RID: 3155 RVA: 0x0001D56E File Offset: 0x0001B76E
		public MessageExportResults()
		{
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0001D578 File Offset: 0x0001B778
		internal MessageExportResults(List<MessageRec> missingMessages, List<BadMessageRec> badMessages)
		{
			if (missingMessages != null)
			{
				this.MissingMessages = new MissingMessageRec[missingMessages.Count];
				for (int i = 0; i < missingMessages.Count; i++)
				{
					this.MissingMessages[i] = new MissingMessageRec(missingMessages[i]);
				}
			}
			else
			{
				this.MissingMessages = Array<MissingMessageRec>.Empty;
			}
			if (badMessages != null)
			{
				this.BadMessages = new BadMessageRec[badMessages.Count];
				badMessages.CopyTo(this.BadMessages, 0);
				return;
			}
			this.BadMessages = Array<BadMessageRec>.Empty;
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0001D5FE File Offset: 0x0001B7FE
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x0001D606 File Offset: 0x0001B806
		[XmlArrayItem("MissingMessage")]
		[XmlArray("MissingMessages")]
		public MissingMessageRec[] MissingMessages { get; set; }

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0001D60F File Offset: 0x0001B80F
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x0001D617 File Offset: 0x0001B817
		[XmlArrayItem("BadMessage")]
		[XmlArray("BadMessages")]
		public BadMessageRec[] BadMessages { get; set; }

		// Token: 0x06000C59 RID: 3161 RVA: 0x0001D620 File Offset: 0x0001B820
		internal List<MessageRec> GetMissingMessages()
		{
			List<MessageRec> list = new List<MessageRec>();
			foreach (MissingMessageRec missingMessageRec in this.MissingMessages)
			{
				list.Add(new MessageRec
				{
					EntryId = missingMessageRec.EntryId,
					FolderId = missingMessageRec.FolderId,
					Flags = (MsgRecFlags)missingMessageRec.Flags
				});
			}
			return list;
		}
	}
}
