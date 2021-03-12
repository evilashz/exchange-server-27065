using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.MailboxAssistants.Assistants.ELC.Logging;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000089 RID: 137
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RemoteArchiveProcessor : IArchiveProcessor
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x000266D4 File Offset: 0x000248D4
		public int MaxMessageSizeInArchive
		{
			get
			{
				if (this.maxMessageSizeInArchive == null)
				{
					object obj = this.primaryMailboxSession.Mailbox.TryGetProperty(MailboxSchema.MaxMessageSize);
					if (obj is int)
					{
						this.maxMessageSizeInArchive = new int?((int)obj * 1024);
						RemoteArchiveProcessor.Tracer.TraceDebug<IExchangePrincipal, int>((long)this.GetHashCode(), "{0}: The MaxMessageSize for this mailbox is {1}", this.primaryMailboxSession.MailboxOwner, this.maxMessageSizeInArchive.Value);
					}
					else
					{
						this.maxMessageSizeInArchive = new int?(int.MaxValue);
						RemoteArchiveProcessor.Tracer.TraceError<IExchangePrincipal>((long)this.GetHashCode(), "{0}: The property MaxMessageSize is not available of this mailbox.", this.primaryMailboxSession.MailboxOwner);
					}
				}
				return this.maxMessageSizeInArchive.Value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00026790 File Offset: 0x00024990
		private Dictionary<PropertyDefinition, ExtendedPropertyDefinition> EwsStorePropertyMapping
		{
			get
			{
				if (this.ewsStorePropertyMapping == null)
				{
					this.InitializeRetentionProperties();
				}
				return this.ewsStorePropertyMapping;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x000267A6 File Offset: 0x000249A6
		private List<ExtendedPropertyDefinition> RetentionExtendedProperties
		{
			get
			{
				if (this.retentionExtendedProperties == null)
				{
					this.InitializeRetentionProperties();
				}
				return this.retentionExtendedProperties;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x000267BC File Offset: 0x000249BC
		public List<PropertyDefinitionBase> FolderSyncProperties
		{
			get
			{
				List<PropertyDefinitionBase> list = new List<PropertyDefinitionBase>();
				list.Add(FolderSchema.DisplayName);
				list.Add(FolderSchema.ParentFolderId);
				list.AddRange(this.RetentionExtendedProperties.ToArray());
				return list;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x000267F7 File Offset: 0x000249F7
		public ElcArchiveStoreDataProvider EwsDataProvider
		{
			get
			{
				return this.ewsDataprovider;
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00026800 File Offset: 0x00024A00
		public RemoteArchiveProcessor(MailboxSession session)
		{
			this.primaryMailboxSession = session;
			this.ewsDataprovider = new ElcArchiveStoreDataProvider(session.MailboxOwner);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00026915 File Offset: 0x00024B15
		public RemoteArchiveProcessor(MailboxSession session, StatisticsLogEntry statisticsLogEntry) : this(session)
		{
			this.statisticsLogEntry = statisticsLogEntry;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00026928 File Offset: 0x00024B28
		public bool SaveConfigItemInArchive(byte[] xmlData)
		{
			Exception arg = null;
			string configName = "MRM";
			bool flag = this.EwsDataProvider.SaveUserConfiguration(xmlData, configName, out arg);
			if (!flag)
			{
				RemoteArchiveProcessor.Tracer.TraceError<MailboxSession, Exception>((long)this.GetHashCode(), "The MRM FAI message could not be saved in remote archive for mailbox {0}, Exception: {1}", this.primaryMailboxSession, arg);
			}
			return flag;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00026970 File Offset: 0x00024B70
		public void DeleteConfigItemInArchive()
		{
			Exception ex = null;
			string configName = "MRM";
			this.EwsDataProvider.DeleteUserConfiguration(configName, out ex);
			if (ex != null)
			{
				RemoteArchiveProcessor.Tracer.TraceError<MailboxSession, Exception>((long)this.GetHashCode(), "The MRM FAI message could not be deleted in remote archive for mailbox {0}, Exception: {1}", this.primaryMailboxSession, ex);
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000269B4 File Offset: 0x00024BB4
		public void MoveToArchive(TagExpirationExecutor.ItemSet itemSet, ElcSubAssistant assistant, FolderArchiver folderArchiver, int totalFailuresSoFar, ref List<Exception> allExceptionsSoFar, out List<string> foldersWithErrors, out int newMoveErrorsTotal)
		{
			foldersWithErrors = new List<string>();
			newMoveErrorsTotal = 0;
			using (Folder folder = Folder.Bind(this.primaryMailboxSession, itemSet.FolderId))
			{
				FolderTupleCrossServerArchive folderTupleCrossServerArchive = folderArchiver.GetArchiveFolderTuple(itemSet.FolderId) as FolderTupleCrossServerArchive;
				if (folderTupleCrossServerArchive != null)
				{
					RemoteArchiveProcessor.Tracer.TraceDebug<RemoteArchiveProcessor, string, string>((long)this.GetHashCode(), "{0}: Was able to open target folder {1} in the archive, corresponding to source folder {2}. Will proceed to move in batches.", this, folderTupleCrossServerArchive.DisplayName, folder.DisplayName);
					this.MoveItemsInBatches(itemSet.Items, folder, folderTupleCrossServerArchive.Folder, assistant, ExpirationExecutor.Action.MoveToArchive, totalFailuresSoFar, ref allExceptionsSoFar, out foldersWithErrors, out newMoveErrorsTotal);
				}
				else
				{
					RemoteArchiveProcessor.Tracer.TraceWarning<RemoteArchiveProcessor, string>((long)this.GetHashCode(), "{0}: Unable to open target folder in the archive corresponding to source folder {1}. Will not move anything to it (obviously).", this, folder.DisplayName);
				}
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00026A70 File Offset: 0x00024C70
		public void MoveToArchiveDumpster(DefaultFolderType folderType, List<ItemData> itemsToMove, ElcSubAssistant assistant, FolderArchiver folderArchiver, int totalFailuresSoFar, ref List<Exception> allExceptionsSoFar, out List<string> foldersWithErrors, out int newMoveErrorsTotal)
		{
			foldersWithErrors = new List<string>();
			newMoveErrorsTotal = 0;
			using (Folder folder = Folder.Bind(this.primaryMailboxSession, folderType))
			{
				WellKnownFolderName folderName;
				if (this.archiveEwsDumpsterFolderMapping.TryGetValue(folderType, out folderName))
				{
					Exception ex;
					Folder defaultFolder = this.EwsDataProvider.GetDefaultFolder(folderName, out ex);
					if (defaultFolder != null && ex == null)
					{
						RemoteArchiveProcessor.Tracer.TraceDebug<RemoteArchiveProcessor, string, string>((long)this.GetHashCode(), "{0}: Was able to open target folder {1} in the archive, corresponding to source folder {2}. Will proceed to move in batches.", this, defaultFolder.DisplayName, folder.DisplayName);
						this.MoveItemsInBatches(itemsToMove, folder, defaultFolder, assistant, ExpirationExecutor.Action.MoveToArchiveDumpster, totalFailuresSoFar, ref allExceptionsSoFar, out foldersWithErrors, out newMoveErrorsTotal);
					}
					else
					{
						RemoteArchiveProcessor.Tracer.TraceWarning<RemoteArchiveProcessor, string, string>((long)this.GetHashCode(), "{0}: Unable to open target folder in the archive corresponding to source folder {1}. Will not move anything to it (obviously). Exception : {2}", this, folder.DisplayName, (ex == null) ? string.Empty : ex.ToString());
					}
				}
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00026B3C File Offset: 0x00024D3C
		public Dictionary<StoreObjectId, FolderTuple> GetFolderHierarchyInArchive()
		{
			Dictionary<StoreObjectId, FolderTuple> dictionary = new Dictionary<StoreObjectId, FolderTuple>();
			RemoteArchiveProcessor.Tracer.TraceDebug<IExchangePrincipal, bool>((long)this.primaryMailboxSession.GetHashCode(), "{0}: QueryFolderHierarchy: Archive mailbox: {1}.", this.primaryMailboxSession.MailboxOwner, this.primaryMailboxSession.MailboxOwner.MailboxInfo.IsArchive);
			Folder folder = this.EwsDataProvider.GetFolder(21, this.FolderSyncProperties);
			FolderTupleCrossServerArchive folderTupleCrossServerArchive = new FolderTupleCrossServerArchive(folder, folder.Id, folder.ParentFolderId, folder.DisplayName, this.ConvertToFolderProps(folder.ExtendedProperties), true);
			dictionary.Add(folderTupleCrossServerArchive.FolderId, folderTupleCrossServerArchive);
			IEnumerable<Folder> folderHierarchy = this.EwsDataProvider.GetFolderHierarchy(100, this.FolderSyncProperties, 21);
			foreach (Folder folder2 in folderHierarchy)
			{
				FolderTupleCrossServerArchive folderTupleCrossServerArchive2 = new FolderTupleCrossServerArchive(folder2, folder2.Id, folder2.ParentFolderId, folder2.DisplayName, this.ConvertToFolderProps(folder2.ExtendedProperties));
				dictionary.Add(folderTupleCrossServerArchive2.FolderId, folderTupleCrossServerArchive2);
			}
			return dictionary;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00026C5C File Offset: 0x00024E5C
		public void UpdatePropertiesOnFolderInArchive(FolderTuple source, FolderTuple targetInArchive)
		{
			FolderTupleCrossServerArchive folderTupleCrossServerArchive = targetInArchive as FolderTupleCrossServerArchive;
			this.AssignTagPropsToFolder(source, folderTupleCrossServerArchive, folderTupleCrossServerArchive.Folder, true);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00026C80 File Offset: 0x00024E80
		public FolderTuple CreateAndUpdateFolderInArchive(FolderTuple parentInArchive, FolderTuple sourceInPrimary)
		{
			FolderTupleCrossServerArchive folderTupleCrossServerArchive = (FolderTupleCrossServerArchive)parentInArchive;
			Folder folder = new Folder(this.EwsDataProvider.Service);
			folder.DisplayName = sourceInPrimary.DisplayName;
			FolderTupleCrossServerArchive folderTupleCrossServerArchive2 = new FolderTupleCrossServerArchive(folder, folder.Id, folderTupleCrossServerArchive.EwsFolderId, folder.DisplayName, this.ConvertToFolderProps(null));
			this.AssignTagPropsToFolder(sourceInPrimary, folderTupleCrossServerArchive2, folderTupleCrossServerArchive2.Folder, false);
			this.EwsDataProvider.SaveFolder(folder, folderTupleCrossServerArchive.EwsFolderId);
			folderTupleCrossServerArchive2.EwsFolderId = folder.Id;
			return folderTupleCrossServerArchive2;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00026D00 File Offset: 0x00024F00
		private void InitializeRetentionProperties()
		{
			this.retentionExtendedProperties = new List<ExtendedPropertyDefinition>();
			this.ewsStorePropertyMapping = new Dictionary<PropertyDefinition, ExtendedPropertyDefinition>();
			foreach (int num in this.retentionPropertyTags.Keys)
			{
				MapiPropertyType mapiPropertyType = this.retentionPropertyTags[num];
				ExtendedPropertyDefinition extendedPropertyDefinition;
				if (num == -1)
				{
					Guid guid = new Guid("C7A4569B-F7AE-4DC2-9279-A8FE2F3CAF89");
					extendedPropertyDefinition = new ExtendedPropertyDefinition(guid, "RetentionTagEntryId", mapiPropertyType);
				}
				else
				{
					extendedPropertyDefinition = new ExtendedPropertyDefinition(num, mapiPropertyType);
				}
				this.retentionExtendedProperties.Add(extendedPropertyDefinition);
				FolderHelper.DataColumnIndex dataColumnIndex = this.retentionPropertyTagsMapping[num];
				PropertyDefinition key = FolderHelper.DataColumns[(int)dataColumnIndex];
				this.ewsStorePropertyMapping.Add(key, extendedPropertyDefinition);
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00026DD0 File Offset: 0x00024FD0
		private int CopyIdsToTmpArray(ItemData[] sourceArray, int srcIndex, int sizeToCopy, int MaxMessageSize, out List<ItemId> destinationListEws, out List<VersionedId> destinationListStore, out bool maxMessageSizeExceeded, out int countSkipped, out Dictionary<ItemData.EnforcerType, int> itemCountPerEnforcer)
		{
			maxMessageSizeExceeded = false;
			destinationListEws = new List<ItemId>();
			destinationListStore = new List<VersionedId>();
			itemCountPerEnforcer = new Dictionary<ItemData.EnforcerType, int>();
			int num = 0;
			countSkipped = 0;
			for (int i = srcIndex; i < srcIndex + sizeToCopy; i++)
			{
				if (sourceArray[i].MessageSize >= MaxMessageSize)
				{
					countSkipped++;
					maxMessageSizeExceeded = true;
				}
				else
				{
					if (!itemCountPerEnforcer.ContainsKey(sourceArray[i].Enforcer))
					{
						itemCountPerEnforcer[sourceArray[i].Enforcer] = 0;
					}
					Dictionary<ItemData.EnforcerType, int> dictionary;
					ItemData.EnforcerType enforcer;
					(dictionary = itemCountPerEnforcer)[enforcer = sourceArray[i].Enforcer] = dictionary[enforcer] + 1;
					destinationListStore.Add(sourceArray[i].Id);
					string text = StoreId.StoreIdToEwsId(this.primaryMailboxSession.MailboxGuid, sourceArray[i].Id);
					ItemId item = new ItemId(text);
					destinationListEws.Add(item);
					num += sourceArray[i].MessageSize;
				}
			}
			return num;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00026EB8 File Offset: 0x000250B8
		private void AssignTagPropsToFolder(FolderTuple source, FolderTupleCrossServerArchive target, Folder targetFolder, bool update)
		{
			for (FolderHelper.DataColumnIndex dataColumnIndex = FolderHelper.DataColumnIndex.startOfTagPropsIndex; dataColumnIndex <= FolderHelper.DataColumnIndex.containerClassIndex; dataColumnIndex++)
			{
				PropertyDefinition key = FolderHelper.DataColumns[(int)dataColumnIndex];
				ExtendedPropertyDefinition extendedPropertyDefinition = this.EwsStorePropertyMapping[key];
				object obj = source.FolderProps[key];
				object obj2 = target.FolderProps[key];
				if (obj != null && !(obj is PropertyError))
				{
					string text = obj as string;
					if (text == null || !string.IsNullOrEmpty(text))
					{
						targetFolder.SetExtendedProperty(extendedPropertyDefinition, obj);
					}
				}
				else if (obj2 != null)
				{
					targetFolder.RemoveExtendedProperty(extendedPropertyDefinition);
				}
			}
			if (update)
			{
				this.EwsDataProvider.UpdateFolder(targetFolder);
				RemoteArchiveProcessor.Tracer.TraceDebug(0L, "AssignedTagPropsToFolder.");
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00026F5C File Offset: 0x0002515C
		private object[] ConvertToFolderProps(ExtendedPropertyCollection propertyList)
		{
			object[] array = new object[FolderHelper.DataColumns.Length];
			if (propertyList != null)
			{
				foreach (ExtendedProperty extendedProperty in propertyList)
				{
					if (string.Compare("RetentionTagEntryId", extendedProperty.PropertyDefinition.Name, true) == 0)
					{
						array[8] = extendedProperty.Value;
					}
					else if (extendedProperty.PropertyDefinition.Tag != null)
					{
						int value = extendedProperty.PropertyDefinition.Tag.Value;
						int num = (int)this.retentionPropertyTagsMapping[value];
						array[num] = extendedProperty.Value;
					}
				}
			}
			return array;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0002701C File Offset: 0x0002521C
		private void MoveItemsInBatches(List<ItemData> listToSend, Folder sourceFolder, Folder targetFolder, ElcSubAssistant elcSubAssistant, ExpirationExecutor.Action retentionActionType, int totalFailuresSoFar, ref List<Exception> allExceptionsSoFar, out List<string> foldersWithErrors, out int newMoveErrorsTotal)
		{
			int count = listToSend.Count;
			int num = 0;
			int i = 0;
			long num2 = 0L;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			bool flag = false;
			Exception ex = null;
			ItemData[] sourceArray = listToSend.ToArray();
			foldersWithErrors = new List<string>();
			newMoveErrorsTotal = 0;
			try
			{
				while (i < count)
				{
					int num7 = (count - i >= 100) ? 100 : (count - i);
					List<ItemId> list;
					List<VersionedId> list2;
					bool flag2;
					int num8;
					Dictionary<ItemData.EnforcerType, int> dictionary;
					num2 += (long)this.CopyIdsToTmpArray(sourceArray, i, num7, this.MaxMessageSizeInArchive, out list, out list2, out flag2, out num8, out dictionary);
					num5 += num8;
					int count2 = list.Count;
					if (flag2)
					{
						foldersWithErrors.Add(sourceFolder.DisplayName);
						RemoteArchiveProcessor.Tracer.TraceDebug<RemoteArchiveProcessor, string>((long)this.GetHashCode(), "{0}: Added folder {1} to the list of bad folders to be event logged.", this, sourceFolder.DisplayName);
					}
					if (count2 > 0)
					{
						ex = null;
						flag = this.EwsDataProvider.MoveItems(list, targetFolder.Id, out ex);
						if (flag)
						{
							RemoteArchiveProcessor.Tracer.TraceDebug<RemoteArchiveProcessor, int>((long)this.GetHashCode(), "{0}: Moved to archive batch of {1} items.", this, count2);
						}
					}
					else
					{
						RemoteArchiveProcessor.Tracer.TraceDebug<RemoteArchiveProcessor>((long)this.GetHashCode(), "{0}: The tmpList was empty during this loop. Nothing to send, don't do anything.", this);
					}
					i += num7;
					num += count2;
					if (!flag)
					{
						RemoteArchiveProcessor.Tracer.TraceError((long)this.GetHashCode(), "{0}: An error occured when trying to expire a batch of {1} items. Expiration action is {2}. Result: {3}", new object[]
						{
							this,
							count2,
							retentionActionType.ToString(),
							ex
						});
						Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ExpirationOfCurrentBatchFailed, null, new object[]
						{
							this.primaryMailboxSession.MailboxOwner,
							retentionActionType.ToString(),
							(sourceFolder == null) ? string.Empty : sourceFolder.DisplayName,
							(targetFolder == null) ? string.Empty : targetFolder.DisplayName,
							(sourceFolder == null) ? string.Empty : sourceFolder.Id.ObjectId.ToHexEntryId(),
							(targetFolder == null) ? string.Empty : targetFolder.Id.ToString(),
							(ex == null) ? string.Empty : ex.ToString()
						});
						newMoveErrorsTotal++;
						num6++;
						if (ex != null)
						{
							allExceptionsSoFar.Add(ex);
						}
						if (totalFailuresSoFar + newMoveErrorsTotal > MailboxData.MaxErrorsAllowed)
						{
							throw new TransientMailboxException(Strings.descELCEnforcerTooManyErrors(this.primaryMailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), MailboxData.MaxErrorsAllowed), new AggregateException(allExceptionsSoFar), null);
						}
					}
					else
					{
						num3 += (dictionary.ContainsKey(ItemData.EnforcerType.DumpsterExpirationEnforcer) ? dictionary[ItemData.EnforcerType.DumpsterExpirationEnforcer] : 0);
						num4 += (dictionary.ContainsKey(ItemData.EnforcerType.ExpirationTagEnforcer) ? dictionary[ItemData.EnforcerType.ExpirationTagEnforcer] : 0);
					}
				}
			}
			finally
			{
				ELCPerfmon.TotalItemsExpired.IncrementBy((long)num);
				ELCPerfmon.TotalSizeItemsExpired.IncrementBy(num2);
				ELCPerfmon.TotalItemsMoved.IncrementBy((long)num);
				ELCPerfmon.TotalSizeItemsMoved.IncrementBy(num2);
				if (this.statisticsLogEntry != null)
				{
					this.statisticsLogEntry.NumberOfItemsActuallyArchivedByDumpsterExpirationEnforcer += (long)num3;
					this.statisticsLogEntry.NumberOfItemsActuallyArchivedByTag += (long)num4;
					this.statisticsLogEntry.NumberOfItemsSkippedDueToSizeRestrictionInArchiveProcessor += (long)num5;
					this.statisticsLogEntry.NumberOfBatchesFailedToMoveInArchiveProcessor += (long)num6;
				}
			}
		}

		// Token: 0x040003D1 RID: 977
		private const int SendBatchSize = 100;

		// Token: 0x040003D2 RID: 978
		private const int FolderSyncBatchSize = 100;

		// Token: 0x040003D3 RID: 979
		private ElcArchiveStoreDataProvider ewsDataprovider;

		// Token: 0x040003D4 RID: 980
		private static readonly Trace Tracer = ExTraceGlobals.ELCTracer;

		// Token: 0x040003D5 RID: 981
		private readonly MailboxSession primaryMailboxSession;

		// Token: 0x040003D6 RID: 982
		private int? maxMessageSizeInArchive;

		// Token: 0x040003D7 RID: 983
		private StatisticsLogEntry statisticsLogEntry;

		// Token: 0x040003D8 RID: 984
		private readonly Dictionary<int, MapiPropertyType> retentionPropertyTags = new Dictionary<int, MapiPropertyType>
		{
			{
				12313,
				2
			},
			{
				12314,
				14
			},
			{
				12317,
				14
			},
			{
				12312,
				2
			},
			{
				12318,
				14
			},
			{
				-1,
				2
			},
			{
				13843,
				25
			}
		};

		// Token: 0x040003D9 RID: 985
		private readonly Dictionary<int, FolderHelper.DataColumnIndex> retentionPropertyTagsMapping = new Dictionary<int, FolderHelper.DataColumnIndex>
		{
			{
				12313,
				FolderHelper.DataColumnIndex.startOfTagPropsIndex
			},
			{
				12314,
				FolderHelper.DataColumnIndex.retentionPeriodIndex
			},
			{
				12317,
				FolderHelper.DataColumnIndex.retentionFlagsIndex
			},
			{
				12312,
				FolderHelper.DataColumnIndex.archiveTagIndex
			},
			{
				12318,
				FolderHelper.DataColumnIndex.archivePeriodIndex
			},
			{
				-1,
				FolderHelper.DataColumnIndex.retentionTagEntryId
			},
			{
				13843,
				FolderHelper.DataColumnIndex.containerClassIndex
			}
		};

		// Token: 0x040003DA RID: 986
		private Dictionary<PropertyDefinition, ExtendedPropertyDefinition> ewsStorePropertyMapping;

		// Token: 0x040003DB RID: 987
		private readonly Dictionary<DefaultFolderType, WellKnownFolderName> archiveEwsDumpsterFolderMapping = new Dictionary<DefaultFolderType, WellKnownFolderName>
		{
			{
				DefaultFolderType.RecoverableItemsDeletions,
				24
			},
			{
				DefaultFolderType.RecoverableItemsPurges,
				26
			},
			{
				DefaultFolderType.RecoverableItemsVersions,
				25
			}
		};

		// Token: 0x040003DC RID: 988
		private List<ExtendedPropertyDefinition> retentionExtendedProperties;
	}
}
