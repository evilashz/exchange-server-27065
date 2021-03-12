using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.MailboxAssistants.Assistants.ELC.Logging;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000088 RID: 136
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LocalArchiveProcessor : DisposeTrackableBase, IArchiveProcessor, IDisposable
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x00025B70 File Offset: 0x00023D70
		private void InitializeArchiveMailboxSession()
		{
			if (this.primaryMailboxSession != null)
			{
				IMailboxInfo archiveMailbox = this.primaryMailboxSession.MailboxOwner.GetArchiveMailbox();
				if (archiveMailbox != null)
				{
					try
					{
						ExchangePrincipal mailboxOwner = ExchangePrincipal.FromMailboxGuid(this.primaryMailboxSession.GetADSessionSettings(), archiveMailbox.MailboxGuid, RemotingOptions.AllowCrossSite | RemotingOptions.AllowCrossPremise, null);
						this.archiveMailboxSession = MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.CurrentCulture, "Client=TBA;Action=MoveToArchive;Interactive=False", null, true);
					}
					catch (ObjectNotFoundException arg)
					{
						LocalArchiveProcessor.Tracer.TraceDebug<IExchangePrincipal, ObjectNotFoundException>((long)this.primaryMailboxSession.GetHashCode(), "{0}: Problems opening the archive.{1}", this.primaryMailboxSession.MailboxOwner, arg);
					}
					catch (StorageTransientException arg2)
					{
						LocalArchiveProcessor.Tracer.TraceWarning<IExchangePrincipal, MailboxSession, StorageTransientException>((long)this.primaryMailboxSession.GetHashCode(), "{0}: Failed to connect to the the archive mailbox : {1}.\nError:\n{2}", this.primaryMailboxSession.MailboxOwner, this.primaryMailboxSession, arg2);
					}
					catch (StoragePermanentException arg3)
					{
						LocalArchiveProcessor.Tracer.TraceError<IExchangePrincipal, MailboxSession, StoragePermanentException>((long)this.primaryMailboxSession.GetHashCode(), "{0}: Failed to connect to the the archive mailbox : {1}.\nError:\n{2}", this.primaryMailboxSession.MailboxOwner, this.primaryMailboxSession, arg3);
					}
				}
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00025C8C File Offset: 0x00023E8C
		public MailboxSession ArchiveMailboxSession
		{
			get
			{
				return this.archiveMailboxSession;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00025C94 File Offset: 0x00023E94
		public int MaxMessageSizeInArchive
		{
			get
			{
				if (this.maxMessageSizeInArchive == null)
				{
					object obj = this.archiveMailboxSession.Mailbox.TryGetProperty(MailboxSchema.MaxMessageSize);
					if (obj is int)
					{
						this.maxMessageSizeInArchive = new int?((int)obj * 1024);
						LocalArchiveProcessor.Tracer.TraceDebug<IExchangePrincipal, int>((long)this.GetHashCode(), "{0}: The MaxMessageSize for the archive for this mailbox is {1}", this.primaryMailboxSession.MailboxOwner, this.maxMessageSizeInArchive.Value);
					}
					else
					{
						this.maxMessageSizeInArchive = new int?(int.MaxValue);
						LocalArchiveProcessor.Tracer.TraceError<IExchangePrincipal>((long)this.GetHashCode(), "{0}: The property MaxMessageSize is not available for the archive of this mailbox.", this.primaryMailboxSession.MailboxOwner);
					}
				}
				return this.maxMessageSizeInArchive.Value;
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00025D50 File Offset: 0x00023F50
		public LocalArchiveProcessor(MailboxSession session)
		{
			this.primaryMailboxSession = session;
			this.InitializeArchiveMailboxSession();
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00025D65 File Offset: 0x00023F65
		public LocalArchiveProcessor(MailboxSession session, StatisticsLogEntry statisticsLogEntry)
		{
			this.primaryMailboxSession = session;
			this.InitializeArchiveMailboxSession();
			this.statisticsLogEntry = statisticsLogEntry;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00025D81 File Offset: 0x00023F81
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<LocalArchiveProcessor>(this);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00025D89 File Offset: 0x00023F89
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.archiveMailboxSession != null)
			{
				this.archiveMailboxSession.Dispose();
				this.archiveMailboxSession = null;
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00025DA8 File Offset: 0x00023FA8
		public bool SaveConfigItemInArchive(byte[] xmlData)
		{
			LocalArchiveProcessor.Tracer.TraceDebug<IExchangePrincipal>((long)this.primaryMailboxSession.GetHashCode(), "{0}: Processing Local Archive, no need to save config item", this.primaryMailboxSession.MailboxOwner);
			return true;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00025DD1 File Offset: 0x00023FD1
		public void DeleteConfigItemInArchive()
		{
			LocalArchiveProcessor.Tracer.TraceDebug<IExchangePrincipal>((long)this.primaryMailboxSession.GetHashCode(), "{0}: Processing Local Archive, no need to delete config item", this.primaryMailboxSession.MailboxOwner);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00025DFC File Offset: 0x00023FFC
		public void MoveToArchiveDumpster(DefaultFolderType folderType, List<ItemData> itemsToMove, ElcSubAssistant assistant, FolderArchiver folderArchiver, int totalFailuresSoFar, ref List<Exception> allExceptionsSoFar, out List<string> foldersWithErrors, out int newMoveErrorsTotal)
		{
			foldersWithErrors = new List<string>();
			newMoveErrorsTotal = 0;
			if (this.archiveMailboxSession == null)
			{
				LocalArchiveProcessor.Tracer.TraceWarning<LocalArchiveProcessor>((long)this.GetHashCode(), "{0}: Could not open archive session for this mailbox", this);
				return;
			}
			assistant.EnableLoadTrackingOnSession(this.archiveMailboxSession);
			try
			{
				using (Folder folder = Folder.Bind(this.primaryMailboxSession, folderType))
				{
					using (Folder folder2 = Folder.Bind(this.archiveMailboxSession, folderType))
					{
						if (folder2 != null)
						{
							LocalArchiveProcessor.Tracer.TraceDebug<LocalArchiveProcessor, DefaultFolderType>((long)this.GetHashCode(), "{0}: Was able to open target folder in the archive dumpster of type {1}. Will proceed to move in batches.", this, folderType);
							this.ExpireInBatches(itemsToMove, folder, folder2, assistant, ExpirationExecutor.Action.MoveToArchiveDumpster, totalFailuresSoFar, ref allExceptionsSoFar, out foldersWithErrors, out newMoveErrorsTotal);
						}
						else
						{
							LocalArchiveProcessor.Tracer.TraceWarning<LocalArchiveProcessor, DefaultFolderType>((long)this.GetHashCode(), "{0}: Unable to open target folder in the archive dumpster of type {1}. Will not move anything to it (obviously).", this, folderType);
						}
					}
				}
			}
			catch (ObjectNotFoundException arg)
			{
				LocalArchiveProcessor.Tracer.TraceError<LocalArchiveProcessor, ObjectNotFoundException>((long)this.GetHashCode(), "{0}: Dumpster folder does not exist in archive. Skipping move to archive dumpster for this run. Exception: {1}", this, arg);
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00025F00 File Offset: 0x00024100
		public void MoveToArchive(TagExpirationExecutor.ItemSet itemSet, ElcSubAssistant assistant, FolderArchiver folderArchiver, int totalFailuresSoFar, ref List<Exception> allExceptionsSoFar, out List<string> foldersWithErrors, out int newMoveErrorsTotal)
		{
			if (itemSet == null)
			{
				throw new ArgumentNullException("itemSet");
			}
			if (assistant == null)
			{
				throw new ArgumentNullException("assistant");
			}
			if (folderArchiver == null)
			{
				throw new ArgumentNullException("folderArchiver");
			}
			foldersWithErrors = new List<string>();
			newMoveErrorsTotal = 0;
			if (this.archiveMailboxSession == null)
			{
				LocalArchiveProcessor.Tracer.TraceWarning<LocalArchiveProcessor>((long)this.GetHashCode(), "{0}: Could not open archive session for this mailbox", this);
				return;
			}
			assistant.EnableLoadTrackingOnSession(this.archiveMailboxSession);
			using (Folder folder = Folder.Bind(this.primaryMailboxSession, itemSet.FolderId))
			{
				if (folder != null)
				{
					FolderTuple archiveFolderTuple = folderArchiver.GetArchiveFolderTuple(folder.Id.ObjectId);
					if (archiveFolderTuple != null)
					{
						using (Folder folder2 = Folder.Bind(this.archiveMailboxSession, archiveFolderTuple.FolderId))
						{
							if (folder2 != null)
							{
								LocalArchiveProcessor.Tracer.TraceDebug<LocalArchiveProcessor, string, string>((long)this.GetHashCode(), "{0}: Was able to open target folder {1} in the archive, corresponding to source folder {2}. Will proceed to move in batches.", this, folder2.DisplayName, folder.DisplayName);
								this.ExpireInBatches(itemSet.Items, folder, folder2, assistant, ExpirationExecutor.Action.MoveToArchive, totalFailuresSoFar, ref allExceptionsSoFar, out foldersWithErrors, out newMoveErrorsTotal);
							}
							else
							{
								LocalArchiveProcessor.Tracer.TraceWarning<LocalArchiveProcessor, string>((long)this.GetHashCode(), "{0}: Unable to open target folder in the archive corresponding to source folder {1}. Will not move anything to it (obviously).", this, folder.DisplayName);
							}
							goto IL_14B;
						}
					}
					LocalArchiveProcessor.Tracer.TraceWarning<LocalArchiveProcessor, string>((long)this.GetHashCode(), "{0}: Unable to get target folder in the archive corresponding to source folder {1}. Will not move anything to it (obviously).", this, folder.DisplayName);
				}
				else
				{
					LocalArchiveProcessor.Tracer.TraceWarning<LocalArchiveProcessor, string>((long)this.GetHashCode(), "{0}: Unable to open source folder {1}. Will not move anything from it (obviously).", this, itemSet.FolderId.ToHexEntryId());
				}
				IL_14B:;
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00026080 File Offset: 0x00024280
		private void ExpireInBatches(List<ItemData> listToSend, Folder sourceFolder, Folder targetFolder, ElcSubAssistant elcSubAssistant, ExpirationExecutor.Action retentionActionType, int totalFailuresSoFar, ref List<Exception> allExceptionsSoFar, out List<string> foldersWithErrors, out int newMoveErrorsTotal)
		{
			int count = listToSend.Count;
			int num = 0;
			int i = 0;
			int num2 = 0;
			int num3 = 0;
			long num4 = 0L;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			OperationResult operationResult = OperationResult.Succeeded;
			Exception ex = null;
			ItemData[] sourceArray = listToSend.ToArray();
			foldersWithErrors = new List<string>();
			newMoveErrorsTotal = 0;
			try
			{
				while (i < count)
				{
					elcSubAssistant.ThrottleStoreCallAndCheckForShutdown(this.archiveMailboxSession.MailboxOwner, ELCHealthMonitor.GetArchiveResourceHealthMonitorKeys(this.archiveMailboxSession, this.primaryMailboxSession));
					num2 = ((count - i >= 100) ? 100 : (count - i));
					List<VersionedId> list;
					bool flag;
					int num9;
					Dictionary<ItemData.EnforcerType, int> dictionary;
					num4 += (long)LocalArchiveProcessor.CopyIdsToTmpArray(sourceArray, i, num2, this.MaxMessageSizeInArchive, out list, out flag, out num9, out dictionary);
					num7 += num9;
					num3 = list.Count;
					if (flag)
					{
						foldersWithErrors.Add(sourceFolder.DisplayName);
						LocalArchiveProcessor.Tracer.TraceDebug<LocalArchiveProcessor, string>((long)this.GetHashCode(), "{0}: Added folder {1} to the list of bad folders to be event logged.", this, sourceFolder.DisplayName);
					}
					if (num3 > 0)
					{
						GroupOperationResult groupOperationResult = sourceFolder.CopyItems(this.archiveMailboxSession, targetFolder.Id, list.ToArray());
						if (groupOperationResult.OperationResult == OperationResult.Succeeded)
						{
							LocalArchiveProcessor.Tracer.TraceDebug<LocalArchiveProcessor, int>((long)this.GetHashCode(), "{0}: Copied to archive batch of {1} items. Will proceed to hard delete the batch.", this, num3);
							try
							{
								this.primaryMailboxSession.COWSettings.TemporaryDisableHold = true;
								AggregateOperationResult aggregateOperationResult = this.primaryMailboxSession.Delete(DeleteItemFlags.HardDelete | DeleteItemFlags.SuppressReadReceipt, list.ToArray());
								operationResult = aggregateOperationResult.OperationResult;
								ex = ElcExceptionHelper.ExtractExceptionsFromAggregateOperationResult(aggregateOperationResult);
								goto IL_18E;
							}
							finally
							{
								this.primaryMailboxSession.COWSettings.TemporaryDisableHold = false;
							}
						}
						operationResult = groupOperationResult.OperationResult;
						ex = groupOperationResult.Exception;
					}
					else
					{
						LocalArchiveProcessor.Tracer.TraceDebug<LocalArchiveProcessor>((long)this.GetHashCode(), "{0}: The tmpList was empty during this loop. Nothing to send, don't do anything.", this);
					}
					IL_18E:
					i += num2;
					num += num3;
					if (operationResult == OperationResult.Failed || operationResult == OperationResult.PartiallySucceeded)
					{
						LocalArchiveProcessor.Tracer.TraceError((long)this.GetHashCode(), "{0}: An error occured when trying to expire a batch of {1} items. Expiration action is {2}. Result: {3}", new object[]
						{
							this,
							num3,
							retentionActionType.ToString(),
							operationResult
						});
						Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ExpirationOfCurrentBatchFailed, null, new object[]
						{
							this.primaryMailboxSession.MailboxOwner,
							retentionActionType.ToString(),
							(sourceFolder == null) ? string.Empty : sourceFolder.DisplayName,
							(targetFolder == null) ? string.Empty : targetFolder.DisplayName,
							(sourceFolder == null) ? string.Empty : sourceFolder.Id.ObjectId.ToHexEntryId(),
							(targetFolder == null) ? string.Empty : targetFolder.Id.ObjectId.ToHexEntryId(),
							(ex == null) ? string.Empty : ex.ToString()
						});
						newMoveErrorsTotal++;
						num8++;
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
						num5 += (dictionary.ContainsKey(ItemData.EnforcerType.DumpsterExpirationEnforcer) ? dictionary[ItemData.EnforcerType.DumpsterExpirationEnforcer] : 0);
						num6 += (dictionary.ContainsKey(ItemData.EnforcerType.ExpirationTagEnforcer) ? dictionary[ItemData.EnforcerType.ExpirationTagEnforcer] : 0);
					}
				}
			}
			finally
			{
				ELCPerfmon.TotalItemsExpired.IncrementBy((long)num);
				ELCPerfmon.TotalSizeItemsExpired.IncrementBy(num4);
				ELCPerfmon.TotalItemsMoved.IncrementBy((long)num);
				ELCPerfmon.TotalSizeItemsMoved.IncrementBy(num4);
				if (this.statisticsLogEntry != null)
				{
					this.statisticsLogEntry.NumberOfItemsActuallyArchivedByDumpsterExpirationEnforcer += (long)num5;
					this.statisticsLogEntry.NumberOfItemsActuallyArchivedByTag += (long)num6;
					this.statisticsLogEntry.NumberOfItemsSkippedDueToSizeRestrictionInArchiveProcessor += (long)num7;
					this.statisticsLogEntry.NumberOfBatchesFailedToMoveInArchiveProcessor += (long)num8;
				}
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0002649C File Offset: 0x0002469C
		public Dictionary<StoreObjectId, FolderTuple> GetFolderHierarchyInArchive()
		{
			if (this.archiveMailboxSession == null)
			{
				LocalArchiveProcessor.Tracer.TraceWarning<LocalArchiveProcessor>((long)this.GetHashCode(), "{0}: Could not open archive session for this mailbox", this);
				return new Dictionary<StoreObjectId, FolderTuple>();
			}
			return FolderHelper.QueryFolderHierarchy(this.archiveMailboxSession);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000264D0 File Offset: 0x000246D0
		public void UpdatePropertiesOnFolderInArchive(FolderTuple source, FolderTuple target)
		{
			if (this.archiveMailboxSession == null)
			{
				LocalArchiveProcessor.Tracer.TraceWarning<LocalArchiveProcessor>((long)this.GetHashCode(), "{0}: Could not open archive session for this mailbox", this);
			}
			using (Folder folder = Folder.Bind(this.archiveMailboxSession, target.FolderId, FolderHelper.DataColumns))
			{
				FolderTuple.AssignTagPropsToFolder(source, folder, this.archiveMailboxSession);
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0002653C File Offset: 0x0002473C
		public FolderTuple CreateAndUpdateFolderInArchive(FolderTuple parentInArchive, FolderTuple sourceInPrimary)
		{
			FolderTuple result = null;
			using (Folder folder = Folder.Create(this.archiveMailboxSession, parentInArchive.FolderId, StoreObjectType.Folder, sourceInPrimary.DisplayName, CreateMode.OpenIfExists))
			{
				folder.Save();
				folder.Load(FolderHelper.DataColumns);
				FolderTuple.AssignTagPropsToFolder(sourceInPrimary, folder, this.archiveMailboxSession);
				folder.Load(FolderHelper.DataColumns);
				result = new FolderTuple(folder.Id.ObjectId, folder.ParentId, sourceInPrimary.DisplayName, sourceInPrimary.FolderProps);
			}
			return result;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000265D0 File Offset: 0x000247D0
		private static int CopyIdsToTmpArray(ItemData[] sourceArray, int srcIndex, int sizeToCopy, int MaxMessageSize, out List<VersionedId> destinationList, out bool maxMessageSizeExceeded, out int countSkipped, out Dictionary<ItemData.EnforcerType, int> itemCountPerEnforcer)
		{
			maxMessageSizeExceeded = false;
			destinationList = new List<VersionedId>();
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
					destinationList.Add(sourceArray[i].Id);
					num += sourceArray[i].MessageSize;
				}
			}
			return num;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0002667C File Offset: 0x0002487C
		private bool IsLocalArchive()
		{
			return string.Compare(this.primaryMailboxSession.MailboxOwner.MailboxInfo.Location.ServerLegacyDn, this.archiveMailboxSession.MailboxOwner.MailboxInfo.Location.ServerLegacyDn, false) == 0;
		}

		// Token: 0x040003CB RID: 971
		private const int SendBatchSize = 100;

		// Token: 0x040003CC RID: 972
		private readonly MailboxSession primaryMailboxSession;

		// Token: 0x040003CD RID: 973
		private MailboxSession archiveMailboxSession;

		// Token: 0x040003CE RID: 974
		private int? maxMessageSizeInArchive;

		// Token: 0x040003CF RID: 975
		private StatisticsLogEntry statisticsLogEntry;

		// Token: 0x040003D0 RID: 976
		private static readonly Trace Tracer = ExTraceGlobals.ELCAssistantTracer;
	}
}
