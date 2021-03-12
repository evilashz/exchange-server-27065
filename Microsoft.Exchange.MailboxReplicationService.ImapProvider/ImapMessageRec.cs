using System;
using System.Collections.Generic;
using Microsoft.Exchange.Connections.Imap;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000006 RID: 6
	internal class ImapMessageRec : IComparable<ImapMessageRec>
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000029E0 File Offset: 0x00000BE0
		protected ImapMessageRec(string uidString, ImapMailFlags imapMailFlags)
		{
			uint uid;
			if (!uint.TryParse(uidString, out uid))
			{
				throw new InvalidUidException(uidString);
			}
			this.Uid = uid;
			this.ImapMailFlags = imapMailFlags;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002A12 File Offset: 0x00000C12
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002A1A File Offset: 0x00000C1A
		public uint Uid { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002A23 File Offset: 0x00000C23
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002A2B File Offset: 0x00000C2B
		public ImapMailFlags ImapMailFlags { get; private set; }

		// Token: 0x0600002E RID: 46 RVA: 0x00002A34 File Offset: 0x00000C34
		public static List<ImapMessageRec> FromImapResultData(ImapResultData resultData)
		{
			IList<string> messageUids = resultData.MessageUids;
			List<ImapMessageRec> list = new List<ImapMessageRec>(messageUids.Count);
			for (int i = 0; i < messageUids.Count; i++)
			{
				list.Add(new ImapMessageRec(resultData.MessageUids[i], resultData.MessageFlags[i]));
			}
			return list;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A8C File Offset: 0x00000C8C
		int IComparable<ImapMessageRec>.CompareTo(ImapMessageRec other)
		{
			return -this.Uid.CompareTo(other.Uid);
		}
	}
}
