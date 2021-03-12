using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200001D RID: 29
	public sealed class FolderStateSnapshot : XMLSerializableBase
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00007F25 File Offset: 0x00006125
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00007F2D File Offset: 0x0000612D
		[XmlElement(ElementName = "FolderId")]
		public byte[] FolderId { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00007F36 File Offset: 0x00006136
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00007F3E File Offset: 0x0000613E
		[XmlElement(ElementName = "LocalCommitTimeMax")]
		public DateTime LocalCommitTimeMax { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00007F47 File Offset: 0x00006147
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00007F4F File Offset: 0x0000614F
		[XmlElement(ElementName = "DeletedCountTotal")]
		public int DeletedCountTotal { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00007F58 File Offset: 0x00006158
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00007F60 File Offset: 0x00006160
		[XmlElement(ElementName = "CopyPropertiesTimestamp")]
		public DateTime CopyPropertiesTimestamp { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00007F69 File Offset: 0x00006169
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00007F71 File Offset: 0x00006171
		[XmlElement(ElementName = "State")]
		public FolderState State { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00007F7A File Offset: 0x0000617A
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00007F82 File Offset: 0x00006182
		[XmlElement(ElementName = "LastSeedTimestamp")]
		public DateTime LastSeedTimestamp { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00007F8B File Offset: 0x0000618B
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00007F93 File Offset: 0x00006193
		[XmlElement(ElementName = "LastSeedEntryId")]
		public byte[] LastSeedEntryId { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00007F9C File Offset: 0x0000619C
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00007FA4 File Offset: 0x000061A4
		[XmlElement(ElementName = "TotalMessages")]
		public int TotalMessages { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00007FAD File Offset: 0x000061AD
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00007FB5 File Offset: 0x000061B5
		[XmlElement(ElementName = "TotalMessageByteSize")]
		public ulong TotalMessageByteSize { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00007FBE File Offset: 0x000061BE
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00007FC6 File Offset: 0x000061C6
		[XmlElement(ElementName = "MessagesWritten")]
		public int MessagesWritten { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00007FCF File Offset: 0x000061CF
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00007FD7 File Offset: 0x000061D7
		[XmlElement(ElementName = "MessageByteSizeWritten")]
		public ulong MessageByteSizeWritten { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00007FE0 File Offset: 0x000061E0
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00007FE8 File Offset: 0x000061E8
		[XmlElement(ElementName = "SoftDeletedMessageCount")]
		public int SoftDeletedMessageCount { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00007FF1 File Offset: 0x000061F1
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00007FF9 File Offset: 0x000061F9
		[XmlElement(ElementName = "TotalSoftDeletedMessageSize")]
		public ulong TotalSoftDeletedMessageSize { get; set; }

		// Token: 0x06000123 RID: 291 RVA: 0x00008002 File Offset: 0x00006202
		internal void UpdateMessageCopyWatermark(MessageRec msgRec)
		{
			this.LastSeedTimestamp = msgRec.CreationTimestamp;
			this.LastSeedEntryId = msgRec.EntryId;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000801C File Offset: 0x0000621C
		internal List<MessageRec> GetMessagesToCopy(List<MessageRec> inputList, MessageRecSortBy sortBy, out int messagesWritten, out ulong totalMessageSizeWritten, out ulong totalMessageByteSize)
		{
			messagesWritten = 0;
			totalMessageSizeWritten = 0UL;
			totalMessageByteSize = 0UL;
			List<MessageRec> list = new List<MessageRec>(inputList.Count);
			foreach (MessageRec messageRec in inputList)
			{
				totalMessageByteSize += (ulong)((long)messageRec.MessageSize);
				if (!this.ShouldCopy(messageRec, sortBy))
				{
					messagesWritten++;
					totalMessageSizeWritten += (ulong)((long)messageRec.MessageSize);
				}
				else
				{
					list.Add(messageRec);
				}
			}
			return list;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000080B0 File Offset: 0x000062B0
		internal ContentChangeResult VerifyContentsChanged(FolderRec folderRec)
		{
			if (this.State.HasFlag(FolderState.IsGhosted))
			{
				return ContentChangeResult.Ghosted;
			}
			if (!this.IsFolderChanged(folderRec, this.LocalCommitTimeMax))
			{
				return ContentChangeResult.NotChanged;
			}
			return ContentChangeResult.Changed;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000080DF File Offset: 0x000062DF
		internal bool IsFolderChanged(FolderRec folderRec)
		{
			return this.IsFolderChanged(folderRec, this.LocalCommitTimeMax);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000080EE File Offset: 0x000062EE
		internal bool IsFolderDataChanged(FolderRec folderRec)
		{
			return this.IsFolderChanged(folderRec, this.CopyPropertiesTimestamp);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000080FD File Offset: 0x000062FD
		internal void UpdateContentsCopied(FolderRec folderRec)
		{
			this.LocalCommitTimeMax = folderRec.LocalCommitTimeMax;
			this.DeletedCountTotal = folderRec.DeletedCountTotal;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00008117 File Offset: 0x00006317
		internal void UpdateFolderDataCopied(FolderRec folderRec)
		{
			this.CopyPropertiesTimestamp = ((folderRec.FolderType == FolderType.Search) ? folderRec.LastModifyTimestamp : folderRec.LocalCommitTimeMax);
			this.State &= ~FolderState.PropertiesNotCopied;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00008148 File Offset: 0x00006348
		private bool IsFolderChanged(FolderRec folderRec, DateTime timestamp)
		{
			bool flag = folderRec.FolderType == FolderType.Search;
			DateTime d = flag ? folderRec.LastModifyTimestamp : folderRec.LocalCommitTimeMax;
			if (d == DateTime.MinValue || d != timestamp)
			{
				return true;
			}
			if (!flag)
			{
				object obj = folderRec[PropTag.DeletedCountTotal];
				if (obj == null || (int)obj != this.DeletedCountTotal)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000081AC File Offset: 0x000063AC
		private bool ShouldCopy(MessageRec msgRec, MessageRecSortBy sortBy)
		{
			return sortBy == MessageRecSortBy.SkipSort || 1 == msgRec.CompareTo(sortBy, this.LastSeedTimestamp, this.FolderId, this.LastSeedEntryId);
		}
	}
}
