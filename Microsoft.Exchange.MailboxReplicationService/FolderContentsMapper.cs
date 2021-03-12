using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000008 RID: 8
	internal class FolderContentsMapper
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00003344 File Offset: 0x00001544
		protected FolderContentsMapper(FolderMapping folderMapping, ISourceFolder srcFolder, FolderHierarchy sourceHierarchy, IDestinationFolder destFolder, FolderHierarchy destHierarchy, ConflictResolutionOption conflictResolutionOption, FAICopyOption faiCopyOption, FolderContentsMapperFlags mapperFlags)
		{
			this.folderMapping = folderMapping;
			this.srcFolder = srcFolder;
			this.destFolder = destFolder;
			this.destHierarchy = destHierarchy;
			this.sourceHierarchy = sourceHierarchy;
			this.conflictResolutionOption = conflictResolutionOption;
			this.faiCopyOption = faiCopyOption;
			this.sourceMapping = new EntryIdMap<MessageRec>();
			this.targetMapping = new EntryIdMap<MessageRec>();
			this.mapperFlags = mapperFlags;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000033AA File Offset: 0x000015AA
		public static FolderContentsMapper Create(FolderMapping folderMapping, ISourceFolder srcFolder, FolderHierarchy sourceHierarchy, IDestinationFolder destFolder, FolderHierarchy destHierarchy, ConflictResolutionOption conflictResolutionOption, FAICopyOption faiCopyOption, FolderContentsMapperFlags mapperFlags)
		{
			if (!mapperFlags.HasFlag(FolderContentsMapperFlags.ImapSync))
			{
				return new FolderContentsMapper(folderMapping, srcFolder, sourceHierarchy, destFolder, destHierarchy, conflictResolutionOption, faiCopyOption, mapperFlags);
			}
			return new ImapFolderContentsMapper(folderMapping, srcFolder, sourceHierarchy, destFolder, destHierarchy, conflictResolutionOption, faiCopyOption, mapperFlags);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000033E4 File Offset: 0x000015E4
		public static bool ShouldItemBeIgnored(MessageRec msgRec, EntryIdMap<BadItemMarker> badItemMarkers, FAICopyOption faiCopyOption, out string faiMessageClass)
		{
			faiMessageClass = null;
			if (msgRec.IsFAI)
			{
				if (faiCopyOption == FAICopyOption.DoNotCopy)
				{
					return true;
				}
				faiMessageClass = (msgRec[PropTag.MessageClass] as string);
				if (!string.IsNullOrEmpty(faiMessageClass) && FolderContentsMapper.IgnoredFaiMessageClasses.Contains(faiMessageClass))
				{
					return true;
				}
			}
			if (badItemMarkers != null && badItemMarkers.ContainsKey(msgRec.EntryId))
			{
				BadItemMarker badItemMarker = badItemMarkers[msgRec.EntryId];
				if (FolderContentsMapper.SkippedBadItemKinds.Contains(badItemMarker.Kind))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003460 File Offset: 0x00001660
		public void ComputeMapping(EntryIdMap<BadItemMarker> badItemMarkers, out int skippedItemCount, out ulong skippedItemSize, out List<MessageRec> itemsToCopy, out List<MessageRec> targetMessagePropsChanges)
		{
			MrsTracer.Service.Function("FolderContentsMapper.ComputeMapping", new object[0]);
			MrsTracer.Service.Debug("Loading source folder contents", new object[0]);
			List<MessageRec> sourceMessages = this.srcFolder.EnumerateMessages(EnumerateMessagesFlags.RegularMessages | EnumerateMessagesFlags.IncludeExtendedData, this.GetSourcePTagsInitialSync());
			MrsTracer.Service.Debug("Loading target folder contents", new object[0]);
			List<MessageRec> targetMessages = this.destFolder.EnumerateMessages(EnumerateMessagesFlags.RegularMessages, this.GetTargetPTagsInitialSync());
			this.ComputeMapping(sourceMessages, targetMessages, badItemMarkers, out skippedItemCount, out skippedItemSize, out itemsToCopy, out targetMessagePropsChanges);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000034E4 File Offset: 0x000016E4
		public bool ComputeMappingPaged(FolderContentsCrawler folderCrawler, EntryIdMap<BadItemMarker> badItemMarkers, out int skippedItemCount, out ulong skippedItemSize, out List<MessageRec> itemsToCopy, out List<MessageRec> targetMessagePropsChanges)
		{
			MrsTracer.Service.Function("FolderContentsMapper.ComputeMappingPaged", new object[0]);
			MrsTracer.Service.Debug("Loading source folder contents", new object[0]);
			IReadOnlyCollection<MessageRec> messagesNextPage = folderCrawler.GetMessagesNextPage();
			if (messagesNextPage.Count == 0)
			{
				itemsToCopy = null;
				targetMessagePropsChanges = null;
				skippedItemCount = 0;
				skippedItemSize = 0UL;
				return false;
			}
			List<byte[]> list = new List<byte[]>(messagesNextPage.Count);
			foreach (MessageRec messageRec in messagesNextPage)
			{
				list.Add(messageRec.EntryId);
			}
			MrsTracer.Service.Debug("Looking up target messages", new object[0]);
			List<MessageRec> targetMessages = this.destFolder.LookupMessages(this.destHierarchy.SourceEntryIDPtag, list, this.GetTargetPTagsInitialSync());
			this.ComputeMapping(messagesNextPage, targetMessages, badItemMarkers, out skippedItemCount, out skippedItemSize, out itemsToCopy, out targetMessagePropsChanges);
			return true;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000035D4 File Offset: 0x000017D4
		public void ComputeMapping(IReadOnlyCollection<MessageRec> sourceMessages, IReadOnlyCollection<MessageRec> targetMessages, EntryIdMap<BadItemMarker> badItemMarkers, out int skippedItemCount, out ulong skippedItemSize, out List<MessageRec> itemsToCopy, out List<MessageRec> targetMessagePropsChanges)
		{
			itemsToCopy = new List<MessageRec>(sourceMessages.Count);
			targetMessagePropsChanges = new List<MessageRec>();
			skippedItemCount = 0;
			skippedItemSize = 0UL;
			if (sourceMessages.Count == 0)
			{
				MrsTracer.Service.Debug("No more contents in source folder", new object[0]);
				return;
			}
			EntryIdMap<MessageRec> entryIdMap = new EntryIdMap<MessageRec>();
			HashSet<byte[]> hashSet = new HashSet<byte[]>(ArrayComparer<byte>.EqualityComparer);
			EntryIdMap<MessageRec> entryIdMap2 = new EntryIdMap<MessageRec>();
			Dictionary<string, List<MessageRec>> dictionary = null;
			Dictionary<string, List<MessageRec>> dictionary2 = null;
			if (this.faiCopyOption == FAICopyOption.MapByMessageClass)
			{
				dictionary = this.CreateFaiMap(sourceMessages, badItemMarkers);
				dictionary2 = this.CreateFaiMap(targetMessages, badItemMarkers);
			}
			foreach (MessageRec messageRec in targetMessages)
			{
				byte[] keyPlusLMTHash = this.GetKeyPlusLMTHash(messageRec, this.destHierarchy.SourceEntryIDPtag, this.destHierarchy.SourceLastModifiedTimestampPtag);
				if (keyPlusLMTHash != null)
				{
					entryIdMap2[keyPlusLMTHash] = messageRec;
				}
				List<byte[]> secondaryKeys = this.GetSecondaryKeys(messageRec, MessageRecType.Target);
				this.RegisterUniqueTargetSecondaryKeys(messageRec, secondaryKeys, entryIdMap, hashSet);
			}
			HashSet<byte[]> uniqueSecondaryKeys = new HashSet<byte[]>(ArrayComparer<byte>.EqualityComparer);
			HashSet<byte[]> hashSet2 = new HashSet<byte[]>(ArrayComparer<byte>.EqualityComparer);
			foreach (MessageRec message in sourceMessages)
			{
				List<byte[]> secondaryKeys2 = this.GetSecondaryKeys(message, MessageRecType.Source);
				this.IdentifyDuplicateSecondaryKeys(message, secondaryKeys2, uniqueSecondaryKeys, hashSet2);
			}
			foreach (MessageRec messageRec2 in sourceMessages)
			{
				string text;
				if (FolderContentsMapper.ShouldItemBeIgnored(messageRec2, badItemMarkers, this.faiCopyOption, out text))
				{
					skippedItemCount++;
					skippedItemSize += (ulong)((long)messageRec2.MessageSize);
				}
				else
				{
					MessageRec messageRec3 = null;
					List<MessageRec> list;
					List<MessageRec> list2;
					if (this.faiCopyOption == FAICopyOption.MapByMessageClass && messageRec2.IsFAI && !string.IsNullOrEmpty(text) && dictionary.TryGetValue(text, out list) && list.Count == 1 && dictionary2.TryGetValue(text, out list2) && list2.Count == 1)
					{
						messageRec3 = list2[0];
						MrsTracer.Service.Debug("Mapped FAI message with message class '{0}'", new object[]
						{
							text
						});
					}
					if (messageRec3 == null)
					{
						this.MapSourceToTargetMessage(messageRec2, entryIdMap2, entryIdMap, hashSet, hashSet2, out messageRec3);
					}
					MessageRec item = null;
					if (this.ShouldTargetMessagePropsBeUpdated(messageRec2, messageRec3, out item))
					{
						skippedItemCount++;
						skippedItemSize += (ulong)((long)messageRec2.MessageSize);
						targetMessagePropsChanges.Add(item);
					}
					else if (this.ShouldItemBeCopied(messageRec2, messageRec3, hashSet))
					{
						itemsToCopy.Add(messageRec2);
						this.sourceMapping[messageRec2.EntryId] = messageRec2;
						if (messageRec3 != null)
						{
							this.targetMapping[messageRec2.EntryId] = messageRec3;
						}
					}
					else
					{
						skippedItemCount++;
						skippedItemSize += (ulong)((long)messageRec2.MessageSize);
					}
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000038DC File Offset: 0x00001ADC
		public void ComputeIncrementalMapping(FolderChangesManifest folderChanges, EntryIdMap<BadItemMarker> badItemMarkers, out List<MessageRec> itemsToCopy, out byte[][] deletedTargetEntryIDs, out byte[][] readTargetEntryIDs, out byte[][] unreadTargetEntryIDs, out int skippedItemCount)
		{
			MrsTracer.Service.Function("FolderContentsMapper.ComputeIncrementalMapping", new object[0]);
			skippedItemCount = 0;
			itemsToCopy = null;
			deletedTargetEntryIDs = null;
			readTargetEntryIDs = null;
			unreadTargetEntryIDs = null;
			List<byte[]> list = new List<byte[]>();
			List<byte[]> list2 = new List<byte[]>();
			List<byte[]> list3 = new List<byte[]>();
			if (folderChanges.ChangedMessages != null)
			{
				foreach (MessageRec messageRec in folderChanges.ChangedMessages)
				{
					list2.Add(messageRec.EntryId);
					if (!messageRec.IsDeleted)
					{
						list.Add(messageRec.EntryId);
					}
					else
					{
						list3.Add(messageRec.EntryId);
					}
				}
			}
			if (folderChanges.ReadMessages != null)
			{
				list2.AddRange(folderChanges.ReadMessages);
			}
			if (folderChanges.UnreadMessages != null)
			{
				list2.AddRange(folderChanges.UnreadMessages);
			}
			if (list2.Count == 0)
			{
				return;
			}
			list2.Sort(ArrayComparer<byte>.Comparer);
			List<PropTag> list4 = new List<PropTag>();
			list4.Add(PropTag.SearchKey);
			list4.Add(PropTag.LastModificationTime);
			list4.Add(PropTag.MessageClass);
			List<MessageRec> list5 = null;
			if (list.Count > 0)
			{
				MrsTracer.Service.Debug("Loading changed source messages", new object[0]);
				list.Sort(ArrayComparer<byte>.Comparer);
				list5 = this.srcFolder.LookupMessages(PropTag.EntryId, list, list4.ToArray());
			}
			EntryIdMap<MessageRec> entryIdMap = new EntryIdMap<MessageRec>();
			if (this.conflictResolutionOption != ConflictResolutionOption.KeepAll)
			{
				list4.Add(this.destHierarchy.SourceEntryIDPtag);
				list4.Add(this.destHierarchy.SourceLastModifiedTimestampPtag);
				MrsTracer.Service.Debug("Looking up target messages", new object[0]);
				List<MessageRec> list6 = this.destFolder.LookupMessages(this.destHierarchy.SourceEntryIDPtag, list2, list4.ToArray());
				foreach (MessageRec messageRec2 in list6)
				{
					byte[] array = messageRec2[this.destHierarchy.SourceEntryIDPtag] as byte[];
					if (array != null)
					{
						this.targetMapping[array] = messageRec2;
					}
					byte[] keyPlusLMTHash = this.GetKeyPlusLMTHash(messageRec2, this.destHierarchy.SourceEntryIDPtag, this.destHierarchy.SourceLastModifiedTimestampPtag);
					if (keyPlusLMTHash != null)
					{
						entryIdMap[keyPlusLMTHash] = messageRec2;
					}
				}
			}
			if (list5 == null)
			{
				MrsTracer.Service.Debug("ChangedSourceIds {0}, SourceMessages looked up is null", new object[]
				{
					list.Count
				});
			}
			else
			{
				if (list5.Count != list.Count)
				{
					MrsTracer.Service.Debug("ChangedSourceIds {0}, SourceMessages looked up {1}", new object[]
					{
						list.Count,
						list5.Count
					});
				}
				itemsToCopy = new List<MessageRec>();
				foreach (MessageRec messageRec3 in list5)
				{
					string text;
					if (FolderContentsMapper.ShouldItemBeIgnored(messageRec3, badItemMarkers, this.faiCopyOption, out text))
					{
						skippedItemCount++;
					}
					else
					{
						MessageRec messageRec4 = null;
						this.MapSourceToTargetMessageBySourceEntryId(messageRec3, entryIdMap, out messageRec4);
						if (!this.ShouldItemBeCopied(messageRec3, messageRec4, null))
						{
							skippedItemCount++;
						}
						else
						{
							if (messageRec4 == null)
							{
								messageRec3.Flags |= MsgRecFlags.New;
							}
							itemsToCopy.Add(messageRec3);
							this.sourceMapping[messageRec3.EntryId] = messageRec3;
						}
					}
				}
			}
			if (list3.Count > 0)
			{
				deletedTargetEntryIDs = this.RemapSourceIDsToTargetIDs(list3);
			}
			if (folderChanges.ReadMessages != null && folderChanges.ReadMessages.Count > 0)
			{
				readTargetEntryIDs = this.RemapSourceIDsToTargetIDs(folderChanges.ReadMessages);
			}
			if (folderChanges.UnreadMessages != null && folderChanges.UnreadMessages.Count > 0)
			{
				unreadTargetEntryIDs = this.RemapSourceIDsToTargetIDs(folderChanges.UnreadMessages);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003CD0 File Offset: 0x00001ED0
		public IFxProxyPool GetFxProxyTransformer(IFxProxyPool targetFxProxyPool)
		{
			return new FolderContentsMapper.TranslatingProxyPool(this, targetFxProxyPool);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003CDC File Offset: 0x00001EDC
		protected virtual void MapSourceToTargetMessage(MessageRec sourceMR, EntryIdMap<MessageRec> targetBySourceEntryId, EntryIdMap<MessageRec> targetBySecondaryKey, HashSet<byte[]> duplicateTargetSecondaryKeys, HashSet<byte[]> duplicateSourceSecondaryKeys, out MessageRec targetMR)
		{
			targetMR = null;
			this.MapSourceToTargetMessageBySourceEntryId(sourceMR, targetBySourceEntryId, out targetMR);
			if (targetMR == null)
			{
				byte[] keyPlusLMTHash = this.GetKeyPlusLMTHash(sourceMR, PropTag.SearchKey, PropTag.LastModificationTime);
				if (keyPlusLMTHash != null && !duplicateSourceSecondaryKeys.Contains(keyPlusLMTHash))
				{
					targetBySecondaryKey.TryGetValue(keyPlusLMTHash, out targetMR);
				}
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003D28 File Offset: 0x00001F28
		protected virtual List<byte[]> GetSecondaryKeys(MessageRec message, MessageRecType messageRecType)
		{
			if (!messageRecType.Equals(MessageRecType.Source))
			{
				return new List<byte[]>(1)
				{
					this.GetKeyPlusLMTHash(message, PropTag.SearchKey, this.destHierarchy.SourceLastModifiedTimestampPtag)
				};
			}
			return new List<byte[]>(1)
			{
				this.GetKeyPlusLMTHash(message, PropTag.SearchKey, PropTag.LastModificationTime)
			};
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003D8D File Offset: 0x00001F8D
		protected virtual PropTag[] GetSourcePTagsInitialSync()
		{
			return FolderContentsMapper.SourcePTagsInitialSync;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003D94 File Offset: 0x00001F94
		protected virtual PropTag[] GetTargetPTagsInitialSync()
		{
			return new PropTag[]
			{
				PropTag.SearchKey,
				PropTag.LastModificationTime,
				PropTag.MessageClass,
				this.destHierarchy.SourceLastModifiedTimestampPtag,
				this.destHierarchy.SourceEntryIDPtag
			};
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003DE0 File Offset: 0x00001FE0
		private static DateTime GetDateTimeValue(MessageRec mr, PropTag pTag)
		{
			object obj = mr[pTag];
			if (obj == null || !(obj is DateTime))
			{
				return CommonUtils.DefaultLastModificationTime;
			}
			return (DateTime)obj;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003E0C File Offset: 0x0000200C
		private void MapSourceToTargetMessageBySourceEntryId(MessageRec sourceMR, EntryIdMap<MessageRec> targetBySourceEntryId, out MessageRec targetMR)
		{
			targetMR = null;
			byte[] keyPlusLMTHash = this.GetKeyPlusLMTHash(sourceMR, PropTag.EntryId, PropTag.LastModificationTime);
			if (keyPlusLMTHash != null)
			{
				targetBySourceEntryId.TryGetValue(keyPlusLMTHash, out targetMR);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003E3C File Offset: 0x0000203C
		private void IdentifyDuplicateSecondaryKeys(MessageRec message, List<byte[]> secondaryKeys, HashSet<byte[]> uniqueSecondaryKeys, HashSet<byte[]> duplicateSecondaryKeys)
		{
			foreach (byte[] array in secondaryKeys)
			{
				if (array != null && !duplicateSecondaryKeys.Contains(array) && !uniqueSecondaryKeys.Add(array))
				{
					MrsTracer.Service.Debug("Duplicate secondary key found for source message {0}", new object[]
					{
						TraceUtils.DumpEntryId(message.EntryId)
					});
					uniqueSecondaryKeys.Remove(array);
					duplicateSecondaryKeys.Add(array);
				}
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003ED0 File Offset: 0x000020D0
		private void RegisterUniqueTargetSecondaryKeys(MessageRec message, List<byte[]> secondaryKeys, EntryIdMap<MessageRec> messageMap, HashSet<byte[]> duplicateKeys)
		{
			foreach (byte[] array in secondaryKeys)
			{
				if (array != null && !duplicateKeys.Contains(array))
				{
					if (!messageMap.ContainsKey(array))
					{
						messageMap.Add(array, message);
					}
					else
					{
						MrsTracer.Service.Debug("Duplicate SecondaryKey found for target message {0}", new object[]
						{
							TraceUtils.DumpEntryId(message.EntryId)
						});
						messageMap.Remove(array);
						duplicateKeys.Add(array);
					}
				}
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003F70 File Offset: 0x00002170
		private byte[] GetKeyPlusLMTHash(MessageRec mr, PropTag keyPtag, PropTag lmtPtag)
		{
			byte[] array = ((keyPtag == PropTag.EntryId) ? mr.EntryId : mr[keyPtag]) as byte[];
			if (array == null)
			{
				return null;
			}
			if (this.conflictResolutionOption != ConflictResolutionOption.KeepAll)
			{
				return array;
			}
			byte[] bytes = BitConverter.GetBytes(FolderContentsMapper.GetDateTimeValue(mr, lmtPtag).ToBinary());
			byte[] array2 = new byte[array.Length + bytes.Length];
			array.CopyTo(array2, 0);
			bytes.CopyTo(array2, array.Length);
			return array2;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003FDF File Offset: 0x000021DF
		protected virtual bool ShouldTargetMessagePropsBeUpdated(MessageRec sourceMR, MessageRec targetMR, out MessageRec updateTargetMR)
		{
			updateTargetMR = null;
			return false;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003FE8 File Offset: 0x000021E8
		protected virtual bool ShouldItemBeCopied(MessageRec sourceMR, MessageRec targetMR, HashSet<byte[]> duplicateTargetSecondaryKeys)
		{
			if (targetMR == null)
			{
				return true;
			}
			if (!sourceMR.IsFAI && this.conflictResolutionOption == ConflictResolutionOption.KeepAll)
			{
				return false;
			}
			DateTime dateTimeValue = FolderContentsMapper.GetDateTimeValue(sourceMR, PropTag.LastModificationTime);
			DateTime dateTimeValue2 = FolderContentsMapper.GetDateTimeValue(targetMR, this.destHierarchy.SourceLastModifiedTimestampPtag);
			DateTime dateTimeValue3 = FolderContentsMapper.GetDateTimeValue(targetMR, PropTag.LastModificationTime);
			return dateTimeValue != dateTimeValue2 && (this.conflictResolutionOption != ConflictResolutionOption.KeepLatestItem || !(dateTimeValue <= dateTimeValue3));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000405C File Offset: 0x0000225C
		private Dictionary<string, List<MessageRec>> CreateFaiMap(IReadOnlyCollection<MessageRec> messages, EntryIdMap<BadItemMarker> badItemMarkers)
		{
			Dictionary<string, List<MessageRec>> dictionary = new Dictionary<string, List<MessageRec>>(StringComparer.OrdinalIgnoreCase);
			foreach (MessageRec messageRec in messages)
			{
				string text;
				if (messageRec.IsFAI && !FolderContentsMapper.ShouldItemBeIgnored(messageRec, badItemMarkers, this.faiCopyOption, out text) && !string.IsNullOrEmpty(text))
				{
					List<MessageRec> list;
					if (!dictionary.TryGetValue(text, out list))
					{
						list = new List<MessageRec>(1);
						dictionary.Add(text, list);
					}
					list.Add(messageRec);
				}
			}
			return dictionary;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000040F0 File Offset: 0x000022F0
		private byte[][] RemapSourceIDsToTargetIDs(ICollection<byte[]> sourceEntryIDs)
		{
			if (sourceEntryIDs == null)
			{
				return null;
			}
			List<byte[]> list = new List<byte[]>();
			foreach (byte[] key in sourceEntryIDs)
			{
				MessageRec messageRec;
				if (this.targetMapping.TryGetValue(key, out messageRec))
				{
					list.Add(messageRec.EntryId);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00004160 File Offset: 0x00002360
		private void RemapMessages(List<byte[]> sourceEntryIDs, List<byte[]> targetEntryIDs)
		{
			if (sourceEntryIDs == null || targetEntryIDs == null)
			{
				return;
			}
			int num = 0;
			while (num < sourceEntryIDs.Count && num < targetEntryIDs.Count)
			{
				byte[] key = sourceEntryIDs[num];
				byte[] entryId = targetEntryIDs[num];
				MessageRec messageRec = new MessageRec();
				messageRec.EntryId = entryId;
				messageRec.FolderId = this.folderMapping.TargetFolder.EntryId;
				this.targetMapping[key] = messageRec;
				num++;
			}
		}

		// Token: 0x04000015 RID: 21
		protected FolderHierarchy destHierarchy;

		// Token: 0x04000016 RID: 22
		protected FolderHierarchy sourceHierarchy;

		// Token: 0x04000017 RID: 23
		private static readonly PropTag[] SourcePTagsInitialSync = new PropTag[]
		{
			PropTag.SearchKey,
			PropTag.LastModificationTime,
			PropTag.MessageClass
		};

		// Token: 0x04000018 RID: 24
		private static readonly HashSet<string> IgnoredFaiMessageClasses = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"IPM.ExtendedRule.Message",
			"IPM.Rule.Message"
		};

		// Token: 0x04000019 RID: 25
		private static readonly HashSet<BadItemKind> SkippedBadItemKinds = new HashSet<BadItemKind>
		{
			BadItemKind.CorruptItem,
			BadItemKind.LargeItem
		};

		// Token: 0x0400001A RID: 26
		private ISourceFolder srcFolder;

		// Token: 0x0400001B RID: 27
		private IDestinationFolder destFolder;

		// Token: 0x0400001C RID: 28
		private FolderMapping folderMapping;

		// Token: 0x0400001D RID: 29
		private ConflictResolutionOption conflictResolutionOption;

		// Token: 0x0400001E RID: 30
		private FAICopyOption faiCopyOption;

		// Token: 0x0400001F RID: 31
		private EntryIdMap<MessageRec> sourceMapping;

		// Token: 0x04000020 RID: 32
		private EntryIdMap<MessageRec> targetMapping;

		// Token: 0x04000021 RID: 33
		private FolderContentsMapperFlags mapperFlags;

		// Token: 0x02000009 RID: 9
		private class TranslatingProxyPool : DisposableWrapper<IFxProxyPool>, IFxProxyPool, IDisposable
		{
			// Token: 0x0600003B RID: 59 RVA: 0x00004247 File Offset: 0x00002447
			public TranslatingProxyPool(FolderContentsMapper mapper, IFxProxyPool destination) : base(destination, true)
			{
				this.mapper = mapper;
				this.uploadedSourceIDs = new List<byte[]>();
			}

			// Token: 0x0600003C RID: 60 RVA: 0x00004264 File Offset: 0x00002464
			EntryIdMap<byte[]> IFxProxyPool.GetFolderData()
			{
				EntryIdMap<byte[]> folderData = base.WrappedObject.GetFolderData();
				EntryIdMap<byte[]> entryIdMap = new EntryIdMap<byte[]>();
				foreach (KeyValuePair<byte[], byte[]> keyValuePair in folderData)
				{
					byte[] array = keyValuePair.Key;
					if (CommonUtils.IsSameEntryId(array, this.mapper.folderMapping.TargetFolder.EntryId))
					{
						array = this.mapper.folderMapping.EntryId;
					}
					entryIdMap.Add(array, keyValuePair.Value);
				}
				return entryIdMap;
			}

			// Token: 0x0600003D RID: 61 RVA: 0x00004304 File Offset: 0x00002504
			void IFxProxyPool.Flush()
			{
				base.WrappedObject.Flush();
			}

			// Token: 0x0600003E RID: 62 RVA: 0x00004311 File Offset: 0x00002511
			void IFxProxyPool.SetItemProperties(ItemPropertiesBase props)
			{
				base.WrappedObject.SetItemProperties(props);
			}

			// Token: 0x0600003F RID: 63 RVA: 0x0000431F File Offset: 0x0000251F
			IFolderProxy IFxProxyPool.CreateFolder(FolderRec folder)
			{
				return base.WrappedObject.CreateFolder(folder);
			}

			// Token: 0x06000040 RID: 64 RVA: 0x00004330 File Offset: 0x00002530
			IFolderProxy IFxProxyPool.GetFolderProxy(byte[] folderId)
			{
				folderId = this.TranslateFolderId(folderId);
				IFolderProxy folderProxy = base.WrappedObject.GetFolderProxy(folderId);
				return new FolderContentsMapper.TranslatingProxyPool.TranslatingFolderProxy(this, folderProxy, this.mapper);
			}

			// Token: 0x06000041 RID: 65 RVA: 0x00004360 File Offset: 0x00002560
			List<byte[]> IFxProxyPool.GetUploadedMessageIDs()
			{
				return base.WrappedObject.GetUploadedMessageIDs();
			}

			// Token: 0x06000042 RID: 66 RVA: 0x0000439C File Offset: 0x0000259C
			protected override void InternalDispose(bool calledFromDispose)
			{
				if (calledFromDispose)
				{
					CommonUtils.CatchKnownExceptions(delegate
					{
						List<byte[]> uploadedMessageIDs = base.WrappedObject.GetUploadedMessageIDs();
						this.mapper.RemapMessages(this.uploadedSourceIDs, uploadedMessageIDs);
					}, null);
				}
				base.InternalDispose(calledFromDispose);
			}

			// Token: 0x06000043 RID: 67 RVA: 0x000043CC File Offset: 0x000025CC
			private byte[] TranslateFolderId(byte[] folderId)
			{
				if (CommonUtils.IsSameEntryId(folderId, this.mapper.folderMapping.EntryId))
				{
					folderId = this.mapper.folderMapping.TargetFolder.EntryId;
				}
				return folderId;
			}

			// Token: 0x04000022 RID: 34
			private FolderContentsMapper mapper;

			// Token: 0x04000023 RID: 35
			private List<byte[]> uploadedSourceIDs;

			// Token: 0x0200000A RID: 10
			private abstract class TranslatingEntryProxy : DisposableWrapper<IMapiFxProxyEx>, IMapiFxProxyEx, IMapiFxProxy, IDisposeTrackable, IDisposable
			{
				// Token: 0x06000045 RID: 69 RVA: 0x000043FE File Offset: 0x000025FE
				public TranslatingEntryProxy(IMapiFxProxyEx destProxy, FolderContentsMapper mapper) : base(destProxy, true)
				{
					this.mapper = mapper;
				}

				// Token: 0x1700000D RID: 13
				// (get) Token: 0x06000046 RID: 70 RVA: 0x0000440F File Offset: 0x0000260F
				public FolderContentsMapper Mapper
				{
					get
					{
						return this.mapper;
					}
				}

				// Token: 0x06000047 RID: 71 RVA: 0x00004417 File Offset: 0x00002617
				void IMapiFxProxyEx.SetProps(PropValueData[] pvda)
				{
					base.WrappedObject.SetProps(pvda);
				}

				// Token: 0x06000048 RID: 72 RVA: 0x00004425 File Offset: 0x00002625
				void IMapiFxProxyEx.SetItemProperties(ItemPropertiesBase props)
				{
					base.WrappedObject.SetItemProperties(props);
				}

				// Token: 0x06000049 RID: 73 RVA: 0x00004433 File Offset: 0x00002633
				byte[] IMapiFxProxy.GetObjectData()
				{
					return base.WrappedObject.GetObjectData();
				}

				// Token: 0x0600004A RID: 74 RVA: 0x00004440 File Offset: 0x00002640
				void IMapiFxProxy.ProcessRequest(FxOpcodes opCode, byte[] request)
				{
					base.WrappedObject.ProcessRequest(opCode, request);
				}

				// Token: 0x04000024 RID: 36
				private FolderContentsMapper mapper;
			}

			// Token: 0x0200000B RID: 11
			private class TranslatingFolderProxy : FolderContentsMapper.TranslatingProxyPool.TranslatingEntryProxy, IFolderProxy, IMapiFxProxyEx, IMapiFxProxy, IDisposeTrackable, IDisposable
			{
				// Token: 0x0600004B RID: 75 RVA: 0x0000444F File Offset: 0x0000264F
				public TranslatingFolderProxy(FolderContentsMapper.TranslatingProxyPool owner, IFolderProxy destFolder, FolderContentsMapper mapper) : base(destFolder, mapper)
				{
					this.owner = owner;
				}

				// Token: 0x0600004C RID: 76 RVA: 0x00004460 File Offset: 0x00002660
				IMessageProxy IFolderProxy.OpenMessage(byte[] entryId)
				{
					MessageRec messageRec;
					if (!base.Mapper.sourceMapping.TryGetValue(entryId, out messageRec))
					{
						return null;
					}
					List<PropValueData> list = new List<PropValueData>(6);
					list.Add(new PropValueData(base.Mapper.destHierarchy.SourceEntryIDPtag, messageRec.EntryId));
					object obj = messageRec[PropTag.LastModificationTime];
					if (obj == null)
					{
						obj = CommonUtils.DefaultLastModificationTime;
					}
					list.Add(new PropValueData(base.Mapper.destHierarchy.SourceLastModifiedTimestampPtag, obj));
					if (messageRec.IsFAI)
					{
						list.Add(new PropValueData(base.Mapper.destHierarchy.SourceMessageClassPtag, messageRec[PropTag.MessageClass]));
					}
					if (base.Mapper.mapperFlags.HasFlag(FolderContentsMapperFlags.ImapSync))
					{
						object obj2 = messageRec[base.Mapper.sourceHierarchy.SourceSyncMessageIdPtag];
						if (obj2 != null)
						{
							list.Add(new PropValueData(base.Mapper.destHierarchy.SourceSyncMessageIdPtag, (string)obj2));
						}
						object obj3 = messageRec[base.Mapper.sourceHierarchy.SourceSyncAccountNamePtag];
						if (obj3 != null)
						{
							list.Add(new PropValueData(base.Mapper.destHierarchy.SourceSyncAccountNamePtag, (string)obj3));
						}
						byte[] array = messageRec[base.Mapper.sourceHierarchy.SourceSyncFolderIdPtag] as byte[];
						if (array != null)
						{
							list.Add(new PropValueData(base.Mapper.destHierarchy.SourceSyncFolderIdPtag, array));
						}
					}
					IFolderProxy folderProxy = (IFolderProxy)base.WrappedObject;
					MessageRec messageRec2;
					IMessageProxy destMessage;
					if (base.Mapper.targetMapping.TryGetValue(entryId, out messageRec2))
					{
						folderProxy.DeleteMessage(messageRec2.EntryId);
						destMessage = folderProxy.CreateMessage(messageRec.IsFAI);
					}
					else
					{
						destMessage = folderProxy.CreateMessage(messageRec.IsFAI);
					}
					this.owner.uploadedSourceIDs.Add(entryId);
					return new FolderContentsMapper.TranslatingProxyPool.TranslatingMessageProxy(destMessage, list.ToArray(), base.Mapper);
				}

				// Token: 0x0600004D RID: 77 RVA: 0x0000465B File Offset: 0x0000285B
				IMessageProxy IFolderProxy.CreateMessage(bool isAssociated)
				{
					throw new UnexpectedErrorPermanentException(-2147024809);
				}

				// Token: 0x0600004E RID: 78 RVA: 0x00004667 File Offset: 0x00002867
				void IFolderProxy.DeleteMessage(byte[] entryId)
				{
					throw new UnexpectedErrorPermanentException(-2147024809);
				}

				// Token: 0x04000025 RID: 37
				private FolderContentsMapper.TranslatingProxyPool owner;
			}

			// Token: 0x0200000C RID: 12
			private class TranslatingMessageProxy : FolderContentsMapper.TranslatingProxyPool.TranslatingEntryProxy, IMessageProxy, IMapiFxProxyEx, IMapiFxProxy, IDisposeTrackable, IDisposable
			{
				// Token: 0x0600004F RID: 79 RVA: 0x00004673 File Offset: 0x00002873
				public TranslatingMessageProxy(IMessageProxy destMessage, PropValueData[] dataToSetOnSave, FolderContentsMapper mapper) : base(destMessage, mapper)
				{
					this.dataToSetOnSave = dataToSetOnSave;
				}

				// Token: 0x06000050 RID: 80 RVA: 0x00004684 File Offset: 0x00002884
				void IMessageProxy.SaveChanges()
				{
					IMessageProxy messageProxy = (IMessageProxy)base.WrappedObject;
					if (this.dataToSetOnSave != null)
					{
						messageProxy.SetProps(this.dataToSetOnSave);
					}
					messageProxy.SaveChanges();
				}

				// Token: 0x06000051 RID: 81 RVA: 0x000046B8 File Offset: 0x000028B8
				void IMessageProxy.WriteToMime(byte[] buffer)
				{
					IMessageProxy messageProxy = (IMessageProxy)base.WrappedObject;
					messageProxy.WriteToMime(buffer);
				}

				// Token: 0x04000026 RID: 38
				private PropValueData[] dataToSetOnSave;
			}
		}
	}
}
