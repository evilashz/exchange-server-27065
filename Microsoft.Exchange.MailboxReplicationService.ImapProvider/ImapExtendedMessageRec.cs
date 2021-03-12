using System;
using System.Collections.Generic;
using Microsoft.Exchange.Connections.Imap;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000007 RID: 7
	internal sealed class ImapExtendedMessageRec : ImapMessageRec
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002AAE File Offset: 0x00000CAE
		private ImapExtendedMessageRec(ImapMailbox folder, string uidString, string messageId, ImapMailFlags imapMailFlags, long messageSize, int messageSeqNum) : base(uidString, imapMailFlags)
		{
			if (string.IsNullOrEmpty(messageId))
			{
				messageId = this.ConstructMessageId(folder);
			}
			this.MessageId = messageId;
			this.MessageSize = messageSize;
			this.MessageSeqNum = messageSeqNum;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002AE1 File Offset: 0x00000CE1
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002AE9 File Offset: 0x00000CE9
		public string MessageId { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002AF2 File Offset: 0x00000CF2
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002AFA File Offset: 0x00000CFA
		public long MessageSize { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002B03 File Offset: 0x00000D03
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002B0B File Offset: 0x00000D0B
		public int MessageSeqNum { get; private set; }

		// Token: 0x06000037 RID: 55 RVA: 0x00002B14 File Offset: 0x00000D14
		public static List<ImapMessageRec> FromImapResultData(ImapMailbox folder, ImapResultData resultData)
		{
			IList<string> messageUids = resultData.MessageUids;
			List<ImapMessageRec> list = new List<ImapMessageRec>(messageUids.Count);
			for (int i = 0; i < messageUids.Count; i++)
			{
				list.Add(new ImapExtendedMessageRec(folder, resultData.MessageUids[i], resultData.MessageIds[i], resultData.MessageFlags[i], resultData.MessageSizes[i], resultData.MessageSeqNums[i]));
			}
			return list;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002B90 File Offset: 0x00000D90
		private string ConstructMessageId(ImapMailbox folder)
		{
			if (folder.UidValidity == null)
			{
				throw new CannotCreateMessageIdException((long)((ulong)base.Uid), folder.Name);
			}
			string text = string.Format("{0}.{1}", folder.UidValidity.Value, base.Uid);
			MrsTracer.Provider.Debug(string.Format("MessageID missing, using <uidvalidity><uid> instead.  Folder = {0}.  UID = {1}.  MessageId = {2}.", folder.Name, base.Uid, text), new object[0]);
			return text;
		}
	}
}
