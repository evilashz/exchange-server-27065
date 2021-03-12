using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000014 RID: 20
	[DataContract]
	internal sealed class FolderChangesManifest
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00003766 File Offset: 0x00001966
		public FolderChangesManifest()
		{
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000376E File Offset: 0x0000196E
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00003776 File Offset: 0x00001976
		[DataMember(Name = "folderId", IsRequired = true)]
		public byte[] FolderId { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000377F File Offset: 0x0000197F
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00003787 File Offset: 0x00001987
		[DataMember(Name = "changedMessages", EmitDefaultValue = false)]
		public List<MessageRec> ChangedMessages { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00003790 File Offset: 0x00001990
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00003798 File Offset: 0x00001998
		[DataMember(Name = "readMessages", EmitDefaultValue = false)]
		public List<byte[]> ReadMessages { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000037A1 File Offset: 0x000019A1
		// (set) Token: 0x0600016B RID: 363 RVA: 0x000037A9 File Offset: 0x000019A9
		[DataMember(Name = "unreadMessages", EmitDefaultValue = false)]
		public List<byte[]> UnreadMessages { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000037B2 File Offset: 0x000019B2
		// (set) Token: 0x0600016D RID: 365 RVA: 0x000037BA File Offset: 0x000019BA
		[DataMember(Name = "folderRecoverySync", EmitDefaultValue = false)]
		public bool FolderRecoverySync { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000037C3 File Offset: 0x000019C3
		// (set) Token: 0x0600016F RID: 367 RVA: 0x000037CB File Offset: 0x000019CB
		[DataMember(Name = "hasMoreChanges", EmitDefaultValue = false)]
		public bool HasMoreChanges { get; set; }

		// Token: 0x06000170 RID: 368 RVA: 0x000037D4 File Offset: 0x000019D4
		public FolderChangesManifest(byte[] folderId)
		{
			this.FolderId = folderId;
			this.ChangedMessages = null;
			this.ReadMessages = null;
			this.UnreadMessages = null;
			this.FolderRecoverySync = false;
			this.HasMoreChanges = false;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00003808 File Offset: 0x00001A08
		public int EntryCount
		{
			get
			{
				int num = 0;
				if (this.ChangedMessages != null)
				{
					num += this.ChangedMessages.Count;
				}
				if (this.ReadMessages != null)
				{
					num += this.ReadMessages.Count;
				}
				if (this.UnreadMessages != null)
				{
					num += this.UnreadMessages.Count;
				}
				return num;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000385A File Offset: 0x00001A5A
		public bool HasChanges
		{
			get
			{
				return this.EntryCount > 0 || this.FolderRecoverySync;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00003870 File Offset: 0x00001A70
		public void GetMessageCounts(out int newMessages, out int updated, out int deleted)
		{
			newMessages = 0;
			updated = 0;
			deleted = 0;
			if (this.ChangedMessages != null)
			{
				foreach (MessageRec messageRec in this.ChangedMessages)
				{
					if (messageRec.IsDeleted)
					{
						deleted++;
					}
					else if (messageRec.IsNew)
					{
						newMessages++;
					}
					else
					{
						updated++;
					}
				}
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000038F4 File Offset: 0x00001AF4
		public override string ToString()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			if (this.ChangedMessages != null)
			{
				foreach (MessageRec messageRec in this.ChangedMessages)
				{
					if (messageRec.IsDeleted)
					{
						if (messageRec.IsFAI)
						{
							num6++;
						}
						else
						{
							num5++;
						}
					}
					else if (messageRec.IsNew)
					{
						if (messageRec.IsFAI)
						{
							num2++;
						}
						else
						{
							num++;
						}
					}
					else if (messageRec.IsFAI)
					{
						num4++;
					}
					else
					{
						num3++;
					}
				}
			}
			return string.Format("{0} new; {1} new FAI; {2} changed; {3} changed FAI; {4} deleted; {5} deleted FAI; {6} read; {7} unread; more changes to be enumerated: {8}", new object[]
			{
				num,
				num2,
				num3,
				num4,
				num5,
				num6,
				(this.ReadMessages != null) ? this.ReadMessages.Count : 0,
				(this.UnreadMessages != null) ? this.UnreadMessages.Count : 0,
				this.HasMoreChanges
			});
		}
	}
}
