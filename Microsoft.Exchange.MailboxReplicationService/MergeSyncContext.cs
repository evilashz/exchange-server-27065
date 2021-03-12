using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200003F RID: 63
	internal class MergeSyncContext : SyncContext, IEntryIdTranslator
	{
		// Token: 0x06000338 RID: 824 RVA: 0x00014FE6 File Offset: 0x000131E6
		public MergeSyncContext(MailboxMerger merger) : base(merger.SourceHierarchy, merger.DestHierarchy)
		{
			this.Merger = merger;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0001500C File Offset: 0x0001320C
		// (set) Token: 0x0600033A RID: 826 RVA: 0x00015014 File Offset: 0x00013214
		public MailboxMerger Merger { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0001501D File Offset: 0x0001321D
		// (set) Token: 0x0600033C RID: 828 RVA: 0x00015025 File Offset: 0x00013225
		public int NumberOfActionsReplayed { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0001502E File Offset: 0x0001322E
		// (set) Token: 0x0600033E RID: 830 RVA: 0x00015036 File Offset: 0x00013236
		public int NumberOfActionsIgnored { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0001503F File Offset: 0x0001323F
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00015047 File Offset: 0x00013247
		public ReplayAction LastActionProcessed { get; set; }

		// Token: 0x06000341 RID: 833 RVA: 0x00015050 File Offset: 0x00013250
		byte[] IEntryIdTranslator.GetSourceFolderIdFromTargetFolderId(byte[] targetFolderId)
		{
			ArgumentValidator.ThrowIfNull("targetFolderId", targetFolderId);
			byte[] array = this.Merger.FolderIdTranslator.TranslateTargetFolderId(targetFolderId);
			if (array == null)
			{
				MrsTracer.Service.Warning("Destination folder {0} doesn't have mapped source folder", new object[]
				{
					TraceUtils.DumpEntryId(targetFolderId)
				});
			}
			return array;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000150A0 File Offset: 0x000132A0
		byte[] IEntryIdTranslator.GetSourceMessageIdFromTargetMessageId(byte[] targetMessageId)
		{
			ArgumentValidator.ThrowIfNull("targetMessageId", targetMessageId);
			byte[] result;
			this.prefetchedSourceEntryIdMap.TryGetValue(targetMessageId, out result);
			return result;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000150C8 File Offset: 0x000132C8
		public override byte[] GetSourceEntryIdFromTargetFolder(FolderRecWrapper targetFolder)
		{
			return this.Merger.GetSourceFolderEntryId(targetFolder);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000150D8 File Offset: 0x000132D8
		public override FolderRecWrapper GetTargetFolderBySourceId(byte[] sourceId)
		{
			byte[] destinationFolderEntryId = this.Merger.GetDestinationFolderEntryId(sourceId);
			if (destinationFolderEntryId == null)
			{
				return null;
			}
			return this.Merger.DestHierarchy[destinationFolderEntryId];
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00015108 File Offset: 0x00013308
		public override FolderRecWrapper CreateSourceFolderRec(FolderRec fRec)
		{
			return new FolderMapping(fRec);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00015110 File Offset: 0x00013310
		public override FolderRecWrapper CreateTargetFolderRec(FolderRecWrapper sourceFolderRec)
		{
			FolderMapping folderMapping = (FolderMapping)sourceFolderRec;
			FolderMapping folderMapping2 = new FolderMapping(sourceFolderRec.FolderRec);
			folderMapping2.SourceFolder = folderMapping;
			folderMapping.TargetFolder = folderMapping2;
			return folderMapping2;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00015140 File Offset: 0x00013340
		public void PrefetchSourceMessageIdsFromTargetMessageIds(EntryIdMap<List<byte[]>> destMessagesToTranslate)
		{
			this.prefetchedSourceEntryIdMap.Clear();
			PropTag sourceEntryIDPtag = this.Merger.DestHierarchy.SourceEntryIDPtag;
			PropTag[] additionalPtagsToLoad = new PropTag[]
			{
				sourceEntryIDPtag
			};
			foreach (KeyValuePair<byte[], List<byte[]>> keyValuePair in destMessagesToTranslate)
			{
				byte[] key = keyValuePair.Key;
				List<byte[]> value = keyValuePair.Value;
				foreach (byte[] key2 in value)
				{
					this.prefetchedSourceEntryIdMap[key2] = null;
				}
				using (IDestinationFolder folder = this.Merger.DestMailbox.GetFolder(key))
				{
					if (folder == null)
					{
						MrsTracer.Service.Warning("Destination folder {0} disappeared", new object[]
						{
							TraceUtils.DumpEntryId(key)
						});
					}
					else
					{
						List<MessageRec> list = folder.LookupMessages(PropTag.EntryId, value, additionalPtagsToLoad);
						foreach (MessageRec messageRec in list)
						{
							byte[] array = messageRec[sourceEntryIDPtag] as byte[];
							if (array == null)
							{
								MrsTracer.Service.Warning("Destination message {0} doesn't have mapped source message", new object[]
								{
									TraceUtils.DumpEntryId(messageRec.EntryId)
								});
							}
							else
							{
								this.prefetchedSourceEntryIdMap[messageRec.EntryId] = array;
							}
						}
					}
				}
			}
		}

		// Token: 0x04000146 RID: 326
		private EntryIdMap<byte[]> prefetchedSourceEntryIdMap = new EntryIdMap<byte[]>();
	}
}
