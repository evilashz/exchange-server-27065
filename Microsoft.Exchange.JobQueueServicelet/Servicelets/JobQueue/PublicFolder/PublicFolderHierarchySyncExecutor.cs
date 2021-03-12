using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.PublicFolder;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Servicelets.JobQueue.PublicFolder
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class PublicFolderHierarchySyncExecutor : IHierarchySyncExecutor
	{
		// Token: 0x06000050 RID: 80 RVA: 0x0000315C File Offset: 0x0000135C
		protected PublicFolderHierarchySyncExecutor(PublicFolderSynchronizerContext syncContext, IBudget budget, string callerInfo)
		{
			ArgumentValidator.ThrowIfNull("syncContext", syncContext);
			this.syncContext = syncContext;
			this.performanceLogger = new PublicFolderPerformanceLogger(this.syncContext);
			this.transientExceptionHandler = new TransientExceptionHandler(PublicFolderHierarchySyncExecutor.Tracer, 3, PublicFolderHierarchySyncExecutor.RetryDelayOnTransientException, new FilterDelegate(null, (UIntPtr)ldftn(ShouldRetryException)), this.syncContext.CorrelationId, null, budget, callerInfo);
			this.batchSize = PublicFolderHierarchySyncExecutor.GetConfigValue("FoldersPerHierarchySyncBatch", "SyncBatchSize", 3);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000031E6 File Offset: 0x000013E6
		protected PublicFolderSynchronizerContext SyncContext
		{
			get
			{
				return this.syncContext;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000031EE File Offset: 0x000013EE
		protected PublicFolderPerformanceLogger PerformanceLogger
		{
			get
			{
				return this.performanceLogger;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000031F6 File Offset: 0x000013F6
		protected bool IsFirstBatch
		{
			get
			{
				return this.batchNumber == 1;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003201 File Offset: 0x00001401
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00003209 File Offset: 0x00001409
		private protected MailboxChangesManifest HierarchyChangeManifest { protected get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000056 RID: 86
		protected abstract bool IsLastSync { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000057 RID: 87
		protected abstract EnumerateHierarchyChangesFlags EnumerateHierarchyChangesFlags { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000058 RID: 88
		protected abstract int MaxHierarchyChanges { get; }

		// Token: 0x06000059 RID: 89 RVA: 0x00003212 File Offset: 0x00001412
		public static PublicFolderHierarchySyncExecutor CreateForSingleFolderSync(PublicFolderSynchronizerContext syncContext)
		{
			ArgumentValidator.ThrowIfNull("syncContext", syncContext);
			return new PublicFolderHierarchySyncExecutor.SyncAllAndProcessEach(syncContext, null, null);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003228 File Offset: 0x00001428
		public static PublicFolderHierarchySyncExecutor Create(PublicFolderSynchronizerContext syncContext, IBudget budget, string callerInfo)
		{
			ArgumentValidator.ThrowIfNull("syncContext", syncContext);
			bool config = ConfigBase<MRSConfigSchema>.GetConfig<bool>("CanExportFoldersInBatch");
			bool flag = config && syncContext.SourceMailbox.IsCapabilitySupported(MRSProxyCapabilities.SetItemProperties) && syncContext.SourceMailbox.IsCapabilitySupported(MRSProxyCapabilities.ExportFolders) && syncContext.SourceMailbox.IsCapabilitySupported(MRSProxyCapabilities.PagedEnumeration);
			if (syncContext.Logger != null)
			{
				syncContext.Logger.LogEvent(LogEventType.Verbose, string.Format("Creating PublicFolderHierarchySyncExecutor. IsBulkProcess={0}, CanExportFoldersInBatch={1}, Server Version Information='{2}'", flag, config, ((RemoteMailbox)syncContext.SourceMailbox).ServerVersion));
			}
			if (flag)
			{
				return new PublicFolderHierarchySyncExecutor.PagedSyncAndBulkProcess(syncContext, budget, callerInfo);
			}
			return new PublicFolderHierarchySyncExecutor.SyncAllAndProcessEach(syncContext, budget, callerInfo);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000334C File Offset: 0x0000154C
		public void SyncSingleFolder(byte[] folderId)
		{
			CommonUtils.CatchKnownExceptions(delegate
			{
				PublicFolderHierarchySyncExecutor.PublicFolderRec publicFolderRec;
				this.CoreSyncFolderUpdate(folderId, out publicFolderRec);
				if (publicFolderRec != null && publicFolderRec.DumpsterFolderId != null)
				{
					PublicFolderHierarchySyncExecutor.PublicFolderRec publicFolderRec2;
					this.CoreSyncFolderUpdate(publicFolderRec.DumpsterFolderId, out publicFolderRec2);
				}
			}, delegate(Exception syncException)
			{
				if (CommonUtils.IsTransientException(syncException))
				{
					throw new PublicFolderSyncTransientException(ServerStrings.PublicFolderSyncFolderFailed(CommonUtils.FullExceptionMessage(syncException, true)));
				}
				throw new PublicFolderSyncPermanentException(ServerStrings.PublicFolderSyncFolderFailed(CommonUtils.FullExceptionMessage(syncException, true)));
			});
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000339C File Offset: 0x0000159C
		public bool ProcessNextBatch()
		{
			this.performanceLogger.InitializeCounters(this.batchNumber);
			bool result;
			try
			{
				using (this.performanceLogger.GetTaskFrame(SyncActivity.ProcessNextBatch))
				{
					if (this.batchNumber > 10 && ExEnvironment.IsTest && ExEnvironment.GetTestRegistryValue("\\PublicFolder", "CheckPointBreak", 0) == 1)
					{
						throw new PublicFolderSyncPermanentException(ServerStrings.PublicFolderSyncFolderHierarchyFailed("This happens only in Test Topology. This is to test checkpoint logic. Please rerun the synchronizer to sync from the point where the synchronizer failed"));
					}
					result = this.InternalProcessNextBatch();
				}
			}
			finally
			{
				this.performanceLogger.WriteActivitiesCountersToLog();
			}
			return result;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003438 File Offset: 0x00001638
		public void HandleException(Exception syncException)
		{
			if (this.syncContext.IsLoggerInitialized && !this.isTrySaveExecuted)
			{
				try
				{
					if (this.currentFolderId != null)
					{
						string text = HexConverter.ByteArrayToHexString(this.currentFolderId);
						this.syncContext.Logger.ReportError(string.Format(CultureInfo.InvariantCulture, (this.isCurrentOperationUpdate ? "UPDATING:" : "DELETING:") + "{0}", new object[]
						{
							text
						}), PublicFolderHierarchySyncExecutor.GetExceptionToLog(syncException));
					}
					else
					{
						this.syncContext.Logger.ReportError("ERROR", PublicFolderHierarchySyncExecutor.GetExceptionToLog(syncException));
					}
				}
				catch (StorageTransientException)
				{
				}
				catch (StoragePermanentException)
				{
				}
				this.syncContext.Logger.SetSyncMetadataValue("NumberOfBatchesExecuted", this.batchNumber - 1);
				this.syncContext.Logger.SetSyncMetadataValue("NumberOfFoldersSynced", this.numberOfFoldersSynced);
				this.syncContext.Logger.TrySave();
				return;
			}
			PublicFolderSynchronizerLogger.LogOnServer(PublicFolderHierarchySyncExecutor.GetExceptionToLog(syncException));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003560 File Offset: 0x00001760
		public void EnsureDestinationFolderHasParentChain(FolderRec sourceFolderRec)
		{
			ArgumentValidator.ThrowIfNull("sourceFolderRec", sourceFolderRec);
			ISourceFolder sourceMailboxFolder = this.GetSourceMailboxFolder(sourceFolderRec.EntryId);
			PublicFolderHierarchySyncExecutor.PublicFolderRec sourceFolderRec2 = new PublicFolderHierarchySyncExecutor.PublicFolderRec(sourceMailboxFolder, sourceFolderRec);
			this.EnsureDestinationFolderHasParentChain(sourceFolderRec2);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003594 File Offset: 0x00001794
		protected static int GetConfigValue(string configSettingName, string testOverrideRegistryKey, int testOverrideDefaultValue)
		{
			int result;
			if (!ExEnvironment.IsTest)
			{
				result = ConfigBase<MRSConfigSchema>.GetConfig<int>(configSettingName);
			}
			else if (!ConfigBase<MRSConfigSchema>.TryGetConfig<int>(configSettingName, out result))
			{
				result = ExEnvironment.GetTestRegistryValue("\\PublicFolder", testOverrideRegistryKey, testOverrideDefaultValue);
			}
			return result;
		}

		// Token: 0x06000060 RID: 96
		protected abstract int ProcessChanges();

		// Token: 0x06000061 RID: 97 RVA: 0x000035F8 File Offset: 0x000017F8
		protected int ProcessChangeOneByOne()
		{
			int i = 0;
			while (i < this.batchSize)
			{
				if (this.updatedFolderIndex != this.HierarchyChangeManifest.ChangedFolders.Count)
				{
					PublicFolderHierarchySyncExecutor.<>c__DisplayClass6 CS$<>8__locals1 = new PublicFolderHierarchySyncExecutor.<>c__DisplayClass6();
					CS$<>8__locals1.entryId = this.HierarchyChangeManifest.ChangedFolders[this.updatedFolderIndex];
					if (!this.folderIdSet.Contains(IdConverter.GuidGlobCountFromEntryId(CS$<>8__locals1.entryId)))
					{
						PublicFolderHierarchySyncExecutor.<>c__DisplayClass8 CS$<>8__locals2 = new PublicFolderHierarchySyncExecutor.<>c__DisplayClass8();
						CS$<>8__locals2.CS$<>8__locals7 = CS$<>8__locals1;
						CS$<>8__locals2.<>4__this = this;
						this.transientExceptionHandler.ExecuteWithRetry(new TryDelegate(CS$<>8__locals2, (UIntPtr)ldftn(<ProcessChangeOneByOne>b__5)));
						this.folderIdSet.Insert(IdConverter.GuidGlobCountFromEntryId(CS$<>8__locals1.entryId));
						i++;
					}
					this.updatedFolderIndex++;
				}
				else
				{
					if (this.deletedFolderIndex == this.HierarchyChangeManifest.DeletedFolders.Count)
					{
						this.isCurrentManifestBatchFullyProcessed = true;
						break;
					}
					byte[] array = this.HierarchyChangeManifest.DeletedFolders[this.deletedFolderIndex];
					if (!this.folderIdSet.Contains(IdConverter.GuidGlobCountFromEntryId(array)))
					{
						this.CoreSyncFolderDelete(array);
						this.folderIdSet.Insert(IdConverter.GuidGlobCountFromEntryId(array));
						i++;
					}
					this.deletedFolderIndex++;
				}
			}
			return i;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000376C File Offset: 0x0000196C
		protected int ProcessChangeInBulk()
		{
			int i = 0;
			int num = this.updatedFolderIndex;
			List<byte[]> list = new List<byte[]>(this.batchSize);
			IdSet idSet = new IdSet();
			while (num < this.HierarchyChangeManifest.ChangedFolders.Count && i + list.Count < this.batchSize)
			{
				PublicFolderHierarchySyncExecutor.<>c__DisplayClassb CS$<>8__locals1 = new PublicFolderHierarchySyncExecutor.<>c__DisplayClassb();
				CS$<>8__locals1.entryId = this.HierarchyChangeManifest.ChangedFolders[num];
				GuidGlobCount guidGlobCount = IdConverter.GuidGlobCountFromEntryId(CS$<>8__locals1.entryId);
				if (!this.folderIdSet.Contains(guidGlobCount) && !idSet.Contains(guidGlobCount))
				{
					WellKnownPublicFolders.FolderType? folderType;
					if (this.syncContext.SourceWellKnownFolders.GetFolderType(CS$<>8__locals1.entryId, out folderType))
					{
						PublicFolderHierarchySyncExecutor.<>c__DisplayClassd CS$<>8__locals2 = new PublicFolderHierarchySyncExecutor.<>c__DisplayClassd();
						CS$<>8__locals2.CS$<>8__localsc = CS$<>8__locals1;
						CS$<>8__locals2.<>4__this = this;
						this.transientExceptionHandler.ExecuteWithRetry(new TryDelegate(CS$<>8__locals2, (UIntPtr)ldftn(<ProcessChangeInBulk>b__a)));
						this.folderIdSet.Insert(guidGlobCount);
						i++;
					}
					else
					{
						list.Add(CS$<>8__locals1.entryId);
						idSet.Insert(guidGlobCount);
					}
				}
				num++;
			}
			this.UpdateFoldersInBatch(list);
			foreach (byte[] entryId in list)
			{
				this.folderIdSet.Insert(IdConverter.GuidGlobCountFromEntryId(entryId));
			}
			i += list.Count;
			this.updatedFolderIndex = num;
			while (i < this.batchSize)
			{
				if (this.deletedFolderIndex == this.HierarchyChangeManifest.DeletedFolders.Count)
				{
					this.isCurrentManifestBatchFullyProcessed = true;
					break;
				}
				byte[] array = this.HierarchyChangeManifest.DeletedFolders[this.deletedFolderIndex];
				if (!this.folderIdSet.Contains(IdConverter.GuidGlobCountFromEntryId(array)))
				{
					this.CoreSyncFolderDelete(array);
					this.folderIdSet.Insert(IdConverter.GuidGlobCountFromEntryId(array));
					i++;
				}
				this.deletedFolderIndex++;
			}
			if (i == this.batchSize && this.updatedFolderIndex == this.HierarchyChangeManifest.ChangedFolders.Count && this.deletedFolderIndex == this.HierarchyChangeManifest.DeletedFolders.Count)
			{
				this.isCurrentManifestBatchFullyProcessed = true;
			}
			return i;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000039B0 File Offset: 0x00001BB0
		private static IdSet GetIdSetFromByteArray(byte[] bytes)
		{
			IdSet result;
			using (Reader reader = Reader.CreateBufferReader(bytes))
			{
				try
				{
					result = IdSet.ParseWithReplGuids(reader);
				}
				catch (BufferParseException ex)
				{
					throw new PublicFolderSyncPermanentException(ServerStrings.PublicFolderSyncFolderFailed(CommonUtils.FullExceptionMessage(ex, true)));
				}
			}
			return result;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003A0C File Offset: 0x00001C0C
		private static IdSet GetIdSetFromSyncState(string syncState)
		{
			MapiSyncState mapiSyncState = MapiSyncState.Deserialize(syncState);
			byte[] idsetGiven = mapiSyncState.HierarchyData.IdsetGiven;
			return PublicFolderHierarchySyncExecutor.GetIdSetFromByteArray(idsetGiven);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003A34 File Offset: 0x00001C34
		private static Exception GetExceptionToLog(Exception syncException)
		{
			Exception result = syncException;
			IMRSRemoteException ex = syncException as IMRSRemoteException;
			if (ex != null)
			{
				string message = string.Format("This is a remote MRS exception. [RemoteStackTrace:{0}]", string.IsNullOrEmpty(ex.RemoteStackTrace) ? string.Empty : ex.RemoteStackTrace.Replace("\r\n", string.Empty));
				result = new Exception(message, syncException);
			}
			return result;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003A8C File Offset: 0x00001C8C
		private static void AppendEntryIdList(StringBuilder output, string description, IList<byte[]> entryIdList)
		{
			output.AppendLine();
			output.AppendLine(description);
			if (entryIdList != null)
			{
				foreach (byte[] array in entryIdList)
				{
					if (array != null && array.Length > 0)
					{
						for (int i = 0; i < array.Length; i++)
						{
							output.Append(array[i].ToString("X2"));
						}
					}
					output.AppendLine();
				}
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003B18 File Offset: 0x00001D18
		private static void ExtractReconcileFolderProperties(PropValueData[] propValueData, out MapiFolderPath folderPath, out int contentCount, out bool hasSubfolders)
		{
			folderPath = null;
			contentCount = -1;
			hasSubfolders = false;
			foreach (PropValueData propValueData2 in propValueData)
			{
				PublicFolderHierarchySyncExecutor.Tracer.TraceDebug<int, object>(0L, "ExtractReconcileFolderProperties. PropTag:{0}, Value:{1}", propValueData2.PropTag, propValueData2.Value);
				PropTag propTag = (PropTag)propValueData2.PropTag;
				PropTag propTag2 = propTag;
				if (propTag2 != PropTag.ContentCount)
				{
					if (propTag2 != PropTag.SubFolders)
					{
						if (propTag.Id() == PropTag.FolderPathName.Id())
						{
							string input = propValueData2.Value as string;
							folderPath = MapiFolderPath.Parse(input);
						}
					}
					else
					{
						hasSubfolders = (bool)propValueData2.Value;
					}
				}
				else
				{
					contentCount = (int)propValueData2.Value;
				}
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003BCC File Offset: 0x00001DCC
		private static bool ShouldRetryException(object e)
		{
			Exception exception = e as Exception;
			return TransientExceptionHandler.IsTransientException(exception) && !TransientExceptionHandler.IsConnectionFailure(exception);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003BF4 File Offset: 0x00001DF4
		private IdSet GetIdSetFromContentMailbox()
		{
			PublicFolderSession destinationMailboxSession = this.syncContext.DestinationMailboxSession;
			StorageIcsState state = default(StorageIcsState);
			using (Folder folder = Folder.Bind(destinationMailboxSession, destinationMailboxSession.GetPublicFolderRootId()))
			{
				CoreFolder coreFolder = folder.CoreObject as CoreFolder;
				using (HierarchyManifestProvider hierarchyManifest = coreFolder.GetHierarchyManifest(ManifestConfigFlags.Catchup, state, new PropertyDefinition[0], new PropertyDefinition[0]))
				{
					ManifestFolderChange manifestFolderChange;
					while (hierarchyManifest.TryGetNextChange(out manifestFolderChange))
					{
					}
					hierarchyManifest.GetFinalState(ref state);
				}
			}
			return PublicFolderHierarchySyncExecutor.GetIdSetFromByteArray(state.StateIdsetGiven);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003C9C File Offset: 0x00001E9C
		private bool InternalProcessNextBatch()
		{
			PublicFolderHierarchySyncExecutor.Tracer.TraceDebug<int, Guid>((long)this.GetHashCode(), "Starting processing batch {0} on mailbox {1}", this.batchNumber, this.syncContext.ContentMailboxGuid);
			if (this.IsFirstBatch)
			{
				this.PrepareInitialStates();
			}
			if (this.isCurrentManifestBatchFullyProcessed)
			{
				this.GetHierarchyChangeManifest();
				this.updatedFolderIndex = 0;
				this.deletedFolderIndex = 0;
				this.isCurrentManifestBatchFullyProcessed = false;
				this.nextSyncState = this.PrepareNextSyncState();
			}
			this.syncContext.Logger.LogEvent(LogEventType.Verbose, "Iteration=" + this.batchNumber);
			int folderProcessed = this.ProcessChanges();
			this.CommitCurrentJobBatch(folderProcessed);
			if (this.isCurrentManifestBatchFullyProcessed)
			{
				if (this.IsLastSync)
				{
					if (this.syncContext.ExecuteReconcileFolders)
					{
						this.Reconcile();
					}
					else
					{
						this.syncContext.Logger.LogEvent(LogEventType.Verbose, "SkippingReconcilation");
					}
				}
				this.CommitCurrentSync();
				if (this.IsLastSync)
				{
					this.isTrySaveExecuted = true;
					this.syncContext.Logger.TrySave();
					this.SetHierarchyReadyFlag();
					return false;
				}
			}
			PublicFolderHierarchySyncExecutor.Tracer.TraceDebug<int, Guid>((long)this.GetHashCode(), "Finished processing batch {0} on mailbox {1}", this.batchNumber, this.syncContext.ContentMailboxGuid);
			this.batchNumber++;
			return true;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003DE0 File Offset: 0x00001FE0
		private void UpdateFoldersInBatch(List<byte[]> entryIds)
		{
			if (entryIds.Count > 0)
			{
				using (this.performanceLogger.GetTaskFrame(SyncActivity.UpdateFoldersInBatch))
				{
					using (IFxProxyPool fxProxyPool = this.syncContext.DestinationMailbox.GetFxProxyPool(new List<byte[]>()))
					{
						using (PublicFolderHierarchyProxyPool publicFolderHierarchyProxyPool = new PublicFolderHierarchyProxyPool(this.syncContext, this, fxProxyPool, this.transientExceptionHandler))
						{
							this.syncContext.SourceMailbox.ExportFolders(entryIds, publicFolderHierarchyProxyPool, ExportFoldersDataToCopyFlags.OutputCreateMessages | ExportFoldersDataToCopyFlags.IncludeCopyToStream, GetFolderRecFlags.None, PublicFolderHierarchySyncExecutor.AdditionalPtagsToLoadForGetFolderRec, CopyPropertiesFlags.None, PublicFolderHierarchySyncExecutor.BatchFolderUpdatePtagsToExclude, AclFlags.FolderAcl);
						}
					}
				}
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003EC4 File Offset: 0x000020C4
		private void PrepareInitialStates()
		{
			using (this.performanceLogger.GetTaskFrame(SyncActivity.GetChangeManifestInitializeSyncContext))
			{
				PublicFolderHierarchySyncExecutor.<>c__DisplayClass10 CS$<>8__locals1 = new PublicFolderHierarchySyncExecutor.<>c__DisplayClass10();
				CS$<>8__locals1.<>4__this = this;
				this.syncContext.Logger.TryGetSyncMetadataValue<string>("SyncState", out CS$<>8__locals1.syncState);
				this.transientExceptionHandler.ExecuteWithRetry(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<PrepareInitialStates>b__f)));
				this.syncContext.SyncStateCounter.BytesSent += (long)((CS$<>8__locals1.syncState == null) ? 0 : CS$<>8__locals1.syncState.Length);
				byte[] array;
				if (this.syncContext.Logger.TryGetSyncMetadataValue<byte[]>("PartiallyCommittedFolderIds", out array) && array != null)
				{
					using (Reader reader = Reader.CreateBufferReader(array))
					{
						this.folderIdSet = IdSet.ParseWithReplGuids(reader);
						goto IL_C1;
					}
				}
				this.folderIdSet = new IdSet();
				IL_C1:;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003FE4 File Offset: 0x000021E4
		private string PrepareNextSyncState()
		{
			string nextJobSyncState;
			using (this.performanceLogger.GetTaskFrame(SyncActivity.GetChangeManifestPersistSyncContext))
			{
				PublicFolderHierarchySyncExecutor.<>c__DisplayClass14 CS$<>8__locals1 = new PublicFolderHierarchySyncExecutor.<>c__DisplayClass14();
				CS$<>8__locals1.<>4__this = this;
				this.syncContext.Logger.SetSyncMetadataValue("NumberOfFoldersToBeSynced", this.HierarchyChangeManifest.ChangedFolders.Count + this.HierarchyChangeManifest.DeletedFolders.Count);
				this.syncContext.Logger.SetSyncMetadataValue("BatchSize", this.batchSize);
				this.syncContext.Logger.TryGetSyncMetadataValue<string>("FinalJobSyncState", out CS$<>8__locals1.nextJobSyncState);
				if (string.IsNullOrEmpty(CS$<>8__locals1.nextJobSyncState))
				{
					this.transientExceptionHandler.ExecuteWithRetry(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<PrepareNextSyncState>b__12)));
					this.syncContext.SyncStateCounter.BytesReceived += (long)((CS$<>8__locals1.nextJobSyncState == null) ? 0 : CS$<>8__locals1.nextJobSyncState.Length);
					this.syncContext.Logger.SetSyncMetadataValue("FinalJobSyncState", CS$<>8__locals1.nextJobSyncState);
				}
				nextJobSyncState = CS$<>8__locals1.nextJobSyncState;
			}
			return nextJobSyncState;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004128 File Offset: 0x00002328
		private void CommitCurrentJobBatch(int folderProcessed)
		{
			using (this.performanceLogger.GetTaskFrame(SyncActivity.CommitBatch))
			{
				this.numberOfFoldersSynced += folderProcessed;
				this.syncContext.Logger.SetSyncMetadataValue("NumberOfBatchesExecuted", this.batchNumber);
				this.syncContext.Logger.SetSyncMetadataValue("NumberOfFoldersSynced", this.numberOfFoldersSynced);
				byte[] array = this.folderIdSet.SerializeWithReplGuids();
				if (array != null && array.Length > 0)
				{
					this.syncContext.Logger.SetSyncMetadataValue("PartiallyCommittedFolderIds", array);
				}
				this.syncContext.Logger.SaveCheckPoint();
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000041E8 File Offset: 0x000023E8
		private void CommitCurrentSync()
		{
			using (this.performanceLogger.GetTaskFrame(SyncActivity.SetIcsState))
			{
				this.syncContext.Logger.SetSyncMetadataValue("PartiallyCommittedFolderIds", null);
				this.syncContext.Logger.SetSyncMetadataValue("SyncState", this.nextSyncState);
				this.syncContext.Logger.SetSyncMetadataValue("FinalJobSyncState", null);
				this.syncContext.Logger.SaveCheckPoint();
				this.folderIdSet = new IdSet();
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004284 File Offset: 0x00002484
		private void GetHierarchyChangeManifest()
		{
			using (this.PerformanceLogger.GetTaskFrame(SyncActivity.EnumerateHierarchyChanges))
			{
				this.HierarchyChangeManifest = this.SyncContext.SourceMailbox.EnumerateHierarchyChanges(this.EnumerateHierarchyChangesFlags, this.MaxHierarchyChanges);
			}
			if (PublicFolderHierarchySyncExecutor.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				this.TraceManifest();
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000042F0 File Offset: 0x000024F0
		private void TraceManifest()
		{
			if (this.HierarchyChangeManifest == null)
			{
				PublicFolderHierarchySyncExecutor.Tracer.TraceDebug((long)this.GetHashCode(), "HierarchyChangeManifest:<null>");
				return;
			}
			int capacity = 200 + ((this.HierarchyChangeManifest.ChangedFolders != null) ? (this.HierarchyChangeManifest.ChangedFolders.Count * 50) : 0) + ((this.HierarchyChangeManifest.DeletedFolders != null) ? (this.HierarchyChangeManifest.DeletedFolders.Count * 50) : 0);
			StringBuilder stringBuilder = new StringBuilder(capacity);
			PublicFolderHierarchySyncExecutor.AppendEntryIdList(stringBuilder, "ChangedFolders:", this.HierarchyChangeManifest.ChangedFolders);
			PublicFolderHierarchySyncExecutor.AppendEntryIdList(stringBuilder, "DeletedFolders:", this.HierarchyChangeManifest.DeletedFolders);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("HasMoreHierarchyChanges: " + this.HierarchyChangeManifest.HasMoreHierarchyChanges);
			PublicFolderHierarchySyncExecutor.Tracer.TraceDebug<StringBuilder>((long)this.GetHashCode(), "HierarchyChangeManifest:{0}", stringBuilder);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000043DC File Offset: 0x000025DC
		private void Reconcile()
		{
			this.syncContext.Logger.LogEvent(LogEventType.Verbose, "BeginReconcilation");
			this.transientExceptionHandler.ExecuteWithRetry(new TryDelegate(this, (UIntPtr)ldftn(ReconcileFolders)));
			this.syncContext.Logger.LogEvent(LogEventType.Verbose, "EndReconcilation");
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004638 File Offset: 0x00002838
		private void ReconcileFolders()
		{
			IdSet idSetFromContentMailbox;
			using (this.performanceLogger.GetTaskFrame(SyncActivity.GetDestinationFolderIdSet))
			{
				idSetFromContentMailbox = this.GetIdSetFromContentMailbox();
			}
			IdSet idSetFromSyncState;
			using (this.performanceLogger.GetTaskFrame(SyncActivity.GetSourceFolderIdSet))
			{
				idSetFromSyncState = PublicFolderHierarchySyncExecutor.GetIdSetFromSyncState(this.nextSyncState);
			}
			if (idSetFromSyncState.CountIds > 0UL && idSetFromContentMailbox.CountIds > 0UL)
			{
				IdConverter.ExpandIdSet(IdSet.Subtract(idSetFromSyncState, idSetFromContentMailbox), delegate(byte[] sourceFolderId)
				{
					byte[] sourceSessionSpecificEntryId = this.GetSourceSessionSpecificEntryId(sourceFolderId);
					WellKnownPublicFolders.FolderType? folderType;
					if (!this.syncContext.SourceWellKnownFolders.GetFolderType(sourceSessionSpecificEntryId, out folderType))
					{
						this.syncContext.Logger.LogEvent(LogEventType.Warning, string.Format(CultureInfo.InvariantCulture, "ReconcileFolders found missing folder in the content mailbox and will attempt to sync it. Id:{0}.", new object[]
						{
							HexConverter.ByteArrayToHexString(sourceSessionSpecificEntryId)
						}));
						PublicFolderHierarchySyncExecutor.PublicFolderRec publicFolderRec;
						this.CoreSyncFolderUpdate(sourceFolderId, out publicFolderRec);
					}
				});
				IdConverter.ExpandIdSet(IdSet.Subtract(idSetFromContentMailbox, idSetFromSyncState), delegate(byte[] destinationFolderId)
				{
					using (this.performanceLogger.GetTaskFrame(SyncActivity.FixOrphanFolders))
					{
						byte[] destinationSessionSpecificEntryId = this.GetDestinationSessionSpecificEntryId(destinationFolderId);
						WellKnownPublicFolders.FolderType? folderType;
						if (!this.syncContext.DestinationWellKnownFolders.GetFolderType(destinationSessionSpecificEntryId, out folderType))
						{
							string text = HexConverter.ByteArrayToHexString(destinationSessionSpecificEntryId);
							this.syncContext.FolderOperationCount.OrphanDetected++;
							using (IDestinationFolder folder = this.syncContext.DestinationMailbox.GetFolder(destinationSessionSpecificEntryId))
							{
								if (folder != null)
								{
									FolderRec folderRec = folder.GetFolderRec(PublicFolderHierarchySyncExecutor.PropertiesToLoadForOrphanFolder, GetFolderRecFlags.None);
									if (folderRec.AdditionalProps.Length > 0)
									{
										MapiFolderPath mapiFolderPath;
										int num;
										bool flag;
										PublicFolderHierarchySyncExecutor.ExtractReconcileFolderProperties(folderRec.AdditionalProps, out mapiFolderPath, out num, out flag);
										if (mapiFolderPath.IsIpmPath)
										{
											PublicFolderHierarchySyncExecutor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ReconcileFolders: Processing orphan folder. Id:{0}.", text);
											this.syncContext.Logger.LogEvent(LogEventType.Verbose, string.Format(CultureInfo.InvariantCulture, "ReconcileFolders: Fixing Orphan folder. FolderId={0};ItemCount={1};HasSubfolders={2};", new object[]
											{
												text,
												num,
												flag
											}));
											this.FixOrphanFolder(folder, folderRec);
										}
										else
										{
											PublicFolderHierarchySyncExecutor.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ReconcileFolders: Skipping folder recovery for Non IPM folder. Orphan Id:{0}.", text);
										}
									}
									else
									{
										PublicFolderHierarchySyncExecutor.Tracer.TraceWarning<string>((long)this.GetHashCode(), "ReconcileFolders: Skipping folder recovery as the folder path can't be retrieved. Orphan Id:{0}.", text);
									}
								}
								else
								{
									PublicFolderHierarchySyncExecutor.Tracer.TraceWarning<string>((long)this.GetHashCode(), "ReconcileFolders: Skipping folder recovery due to not found object during ReconcileFolder operation. Orphan Id:{0}.", text);
								}
							}
						}
					}
				});
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004700 File Offset: 0x00002900
		private void FixOrphanFolder(IDestinationFolder orphanFolder, FolderRec orphanFolderRec)
		{
			using (IDestinationFolder folder = this.syncContext.DestinationMailbox.GetFolder(orphanFolderRec.ParentId))
			{
				if (folder != null)
				{
					FolderRec folderRec = folder.GetFolderRec(PublicFolderHierarchySyncExecutor.AdditionalPtagsToLoadForGetFolderRec, GetFolderRecFlags.None);
					byte[] array = PublicFolderHierarchyProxyPool.GetDumpsterEntryIdFromFolderRec(folderRec);
					if (array != null)
					{
						PublicFolderHierarchySyncExecutor.Tracer.TraceDebug<int>((long)this.GetHashCode(), "ReconcileFolders: Setting OverallAgeLimit on the orphan folder to {0}.", 5184000);
						array = this.GetDestinationSessionSpecificEntryId(array);
						orphanFolder.SetProps(new PropValueData[]
						{
							new PropValueData(PropTag.OverallAgeLimit, 5184000)
						});
						if (PublicFolderHierarchySyncExecutor.Tracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							PublicFolderHierarchySyncExecutor.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "ReconcileFolders: Moving orphan folder to parent dumpster. Parent Id:{1}, Dumpster Id:{2}.", HexConverter.ByteArrayToHexString(orphanFolderRec.ParentId), HexConverter.ByteArrayToHexString(array));
						}
						this.syncContext.DestinationMailbox.MoveFolder(orphanFolderRec.EntryId, orphanFolderRec.ParentId, array);
						this.syncContext.FolderOperationCount.OrphanFixed++;
						PublicFolderHierarchySyncExecutor.Tracer.TraceDebug((long)this.GetHashCode(), "ReconcileFolders: Succeeded processing orphan folder.");
					}
					else
					{
						PublicFolderHierarchySyncExecutor.Tracer.TraceWarning((long)this.GetHashCode(), "ReconcileFolders: Skipping folder recovery as the parent dumpster is unknown.");
					}
				}
				else
				{
					PublicFolderHierarchySyncExecutor.Tracer.TraceWarning((long)this.GetHashCode(), "ReconcileFolders: Skipping folder recovery as the parent folder is unknown.");
				}
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004864 File Offset: 0x00002A64
		private void CoreSyncFolderUpdate(byte[] folderId, out PublicFolderHierarchySyncExecutor.PublicFolderRec sourceFolderRec)
		{
			this.currentFolderId = folderId;
			this.isCurrentOperationUpdate = true;
			sourceFolderRec = null;
			using (ISourceFolder sourceMailboxFolder = this.GetSourceMailboxFolder(this.GetSourceSessionSpecificEntryId(folderId)))
			{
				if (sourceMailboxFolder != null)
				{
					sourceFolderRec = new PublicFolderHierarchySyncExecutor.PublicFolderRec(sourceMailboxFolder, this.GetFolderRec(sourceMailboxFolder));
					this.EnsureDestinationFolderHasParentChain(sourceFolderRec);
					this.CoreSyncFolderUpdate(sourceFolderRec);
				}
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000048D0 File Offset: 0x00002AD0
		private void CoreSyncFolderUpdate(PublicFolderHierarchySyncExecutor.PublicFolderRec sourceFolderRec)
		{
			if (sourceFolderRec.FolderRec.FolderType == FolderType.Search)
			{
				return;
			}
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				bool flag;
				byte[] array = this.MapSourceToDestinationFolderId(sourceFolderRec.FolderRec.EntryId, out flag);
				byte[] array2 = null;
				if (sourceFolderRec.FolderRec.ParentId != null)
				{
					bool flag2;
					array2 = this.MapSourceToDestinationFolderId(sourceFolderRec.FolderRec.ParentId, out flag2);
				}
				IDestinationFolder destinationMailboxFolder = this.GetDestinationMailboxFolder(array);
				if (destinationMailboxFolder == null)
				{
					byte[] entryId = sourceFolderRec.FolderRec.EntryId;
					byte[] parentId = sourceFolderRec.FolderRec.ParentId;
					sourceFolderRec.FolderRec.EntryId = array;
					sourceFolderRec.FolderRec.ParentId = array2;
					byte[] folderId;
					using (this.performanceLogger.GetTaskFrame(SyncActivity.CreateFolder))
					{
						this.syncContext.DestinationMailbox.CreateFolder(sourceFolderRec.FolderRec, CreateFolderFlags.None, out folderId);
					}
					sourceFolderRec.FolderRec.EntryId = entryId;
					sourceFolderRec.FolderRec.ParentId = parentId;
					destinationMailboxFolder = this.GetDestinationMailboxFolder(folderId);
					disposeGuard.Add<IDestinationFolder>(destinationMailboxFolder);
					PublicFolderHierarchySyncExecutor.Tracer.TraceDebug<FolderRec>((long)this.GetHashCode(), "Folder created: {0}", sourceFolderRec.FolderRec);
					if (this.syncContext.Logger != null)
					{
						this.syncContext.Logger.LogFolderCreated(sourceFolderRec.FolderRec.EntryId);
					}
				}
				else
				{
					disposeGuard.Add<IDestinationFolder>(destinationMailboxFolder);
					FolderRec folderRec = destinationMailboxFolder.GetFolderRec(null, GetFolderRecFlags.None);
					if (!CommonUtils.IsSameEntryId(folderRec.ParentId, array2))
					{
						using (this.performanceLogger.GetTaskFrame(SyncActivity.MoveFolder))
						{
							this.syncContext.DestinationMailbox.MoveFolder(folderRec.EntryId, folderRec.ParentId, array2);
						}
					}
					PublicFolderHierarchySyncExecutor.Tracer.TraceDebug<FolderRec>((long)this.GetHashCode(), "Folder updated: {0}", sourceFolderRec.FolderRec);
					if (this.syncContext.Logger != null)
					{
						this.syncContext.Logger.LogFolderUpdated(sourceFolderRec.FolderRec.EntryId);
					}
				}
				List<PropTag> list = new List<PropTag>(PublicFolderHierarchySyncExecutor.AlwaysExcludedFolderPtags.Length);
				list.AddRange(PublicFolderHierarchySyncExecutor.AlwaysExcludedFolderPtags);
				if (sourceFolderRec.FolderRec.FolderType == FolderType.Root)
				{
					list.AddRange(PublicFolderHierarchySyncExecutor.ExcludedRootFolderPtags);
				}
				if (flag)
				{
					list.Add(PropTag.ReplicaList);
				}
				using (this.performanceLogger.GetTaskFrame(SyncActivity.ClearFolderProperties))
				{
					destinationMailboxFolder.SetProps(CommonUtils.PropertiesToDelete);
				}
				using (this.performanceLogger.GetTaskFrame(SyncActivity.SetSecurityDescriptor))
				{
					if (this.ShouldUseExtendedAclInformation())
					{
						destinationMailboxFolder.SetExtendedAcl(AclFlags.FolderAcl, sourceFolderRec.Folder.GetExtendedAcl(AclFlags.FolderAcl));
					}
					else
					{
						destinationMailboxFolder.SetSecurityDescriptor(SecurityProp.NTSD, sourceFolderRec.Folder.GetSecurityDescriptor(SecurityProp.NTSD));
					}
				}
				using (this.performanceLogger.GetTaskFrame(SyncActivity.FxCopyProperties))
				{
					using (IFxProxy fxProxy = destinationMailboxFolder.GetFxProxy(FastTransferFlags.None))
					{
						((ISourceFolder)sourceFolderRec.Folder).CopyTo(fxProxy, CopyPropertiesFlags.None, list.ToArray());
					}
				}
				if (sourceFolderRec.DumpsterFolderId != null)
				{
					byte[] sourceSessionSpecificEntryId = this.GetSourceSessionSpecificEntryId(sourceFolderRec.DumpsterFolderId);
					bool flag3;
					byte[] value = this.MapSourceToDestinationFolderId(sourceSessionSpecificEntryId, out flag3);
					if (flag3)
					{
						using (this.performanceLogger.GetTaskFrame(SyncActivity.UpdateDumpsterId))
						{
							destinationMailboxFolder.SetProps(new PropValueData[]
							{
								new PropValueData(PropTag.IpmWasteBasketEntryId, value)
							});
						}
					}
				}
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004D04 File Offset: 0x00002F04
		private void CoreSyncFolderDelete(byte[] folderId)
		{
			this.currentFolderId = folderId;
			this.isCurrentOperationUpdate = false;
			using (IDestinationFolder destinationMailboxFolder = this.GetDestinationMailboxFolder(this.GetDestinationSessionSpecificEntryId(folderId)))
			{
				if (destinationMailboxFolder != null)
				{
					PublicFolderHierarchySyncExecutor.PublicFolderRec publicFolderRec = new PublicFolderHierarchySyncExecutor.PublicFolderRec(destinationMailboxFolder, this.GetFolderRec(destinationMailboxFolder));
					if (publicFolderRec.FolderRec.FolderType != FolderType.Search)
					{
						using (this.performanceLogger.GetTaskFrame(SyncActivity.DeleteFolder))
						{
							this.syncContext.DestinationMailbox.DeleteFolder(publicFolderRec.FolderRec);
						}
						PublicFolderHierarchySyncExecutor.Tracer.TraceDebug<FolderRec>((long)this.GetHashCode(), "Folder deleted: {0}", publicFolderRec.FolderRec);
						if (this.syncContext.Logger != null)
						{
							this.syncContext.Logger.LogFolderDeleted(folderId);
						}
					}
				}
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004DE4 File Offset: 0x00002FE4
		private void EnsureDestinationFolderHasParentChain(PublicFolderHierarchySyncExecutor.PublicFolderRec sourceFolderRec)
		{
			List<PublicFolderHierarchySyncExecutor.PublicFolderRec> list = new List<PublicFolderHierarchySyncExecutor.PublicFolderRec>();
			if (sourceFolderRec.FolderRec.ParentId == null)
			{
				return;
			}
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				for (;;)
				{
					bool flag;
					byte[] folderId = this.MapSourceToDestinationFolderId(sourceFolderRec.FolderRec.ParentId, out flag);
					using (IDestinationFolder destinationMailboxFolder = this.GetDestinationMailboxFolder(folderId))
					{
						if (destinationMailboxFolder == null)
						{
							ISourceFolder sourceMailboxFolder = this.GetSourceMailboxFolder(sourceFolderRec.FolderRec.ParentId);
							if (sourceMailboxFolder != null)
							{
								disposeGuard.Add<ISourceFolder>(sourceMailboxFolder);
								PublicFolderHierarchySyncExecutor.PublicFolderRec publicFolderRec = new PublicFolderHierarchySyncExecutor.PublicFolderRec(sourceMailboxFolder, this.GetFolderRec(sourceMailboxFolder));
								list.Insert(0, publicFolderRec);
								sourceFolderRec = publicFolderRec;
							}
							continue;
						}
					}
					break;
				}
				this.SyncParentChainFolders(list);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004EB4 File Offset: 0x000030B4
		private void SyncParentChainFolders(List<PublicFolderHierarchySyncExecutor.PublicFolderRec> sourceParentFolderChain)
		{
			if (sourceParentFolderChain.Count > 0)
			{
				this.syncContext.FolderOperationCount.ParentChainMissing++;
			}
			foreach (PublicFolderHierarchySyncExecutor.PublicFolderRec publicFolderRec in sourceParentFolderChain)
			{
				this.CoreSyncFolderUpdate(publicFolderRec);
				if (publicFolderRec.DumpsterFolderId != null)
				{
					PublicFolderHierarchySyncExecutor.PublicFolderRec publicFolderRec2;
					this.CoreSyncFolderUpdate(publicFolderRec.DumpsterFolderId, out publicFolderRec2);
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004F3C File Offset: 0x0000313C
		private IDestinationFolder GetDestinationMailboxFolder(byte[] folderId)
		{
			IDestinationFolder folder;
			using (this.performanceLogger.GetTaskFrame(SyncActivity.GetDestinationMailboxFolder))
			{
				folder = this.syncContext.DestinationMailbox.GetFolder(folderId);
			}
			return folder;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004F88 File Offset: 0x00003188
		private byte[] GetDestinationSessionSpecificEntryId(byte[] sourceEntryId)
		{
			byte[] sessionSpecificEntryId;
			using (this.performanceLogger.GetTaskFrame(SyncActivity.GetDestinationSessionSpecificEntryId))
			{
				sessionSpecificEntryId = this.syncContext.DestinationMailbox.GetSessionSpecificEntryId(sourceEntryId);
			}
			return sessionSpecificEntryId;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004FD4 File Offset: 0x000031D4
		private ISourceFolder GetSourceMailboxFolder(byte[] folderId)
		{
			ISourceFolder folder;
			using (this.performanceLogger.GetTaskFrame(SyncActivity.GetSourceMailboxFolder))
			{
				folder = this.syncContext.SourceMailbox.GetFolder(folderId);
			}
			return folder;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005020 File Offset: 0x00003220
		private byte[] GetSourceSessionSpecificEntryId(byte[] folderEntryId)
		{
			byte[] sessionSpecificEntryId;
			using (this.performanceLogger.GetTaskFrame(SyncActivity.GetSourceSessionSpecificEntryId))
			{
				sessionSpecificEntryId = this.syncContext.SourceMailbox.GetSessionSpecificEntryId(folderEntryId);
			}
			return sessionSpecificEntryId;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000506C File Offset: 0x0000326C
		private byte[] MapSourceToDestinationFolderId(byte[] folderId, out bool isWellKnownFolder)
		{
			byte[] result;
			using (this.performanceLogger.GetTaskFrame(SyncActivity.MapSourceToDestinationFolderId))
			{
				result = this.syncContext.MapSourceToDestinationFolderId(folderId, out isWellKnownFolder);
			}
			return result;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000050B4 File Offset: 0x000032B4
		private bool ShouldUseExtendedAclInformation()
		{
			return CommonUtils.ShouldUseExtendedAclInformation(this.syncContext.SourceMailbox, this.syncContext.DestinationMailbox);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000050D4 File Offset: 0x000032D4
		private FolderRec GetFolderRec(IFolder folder)
		{
			FolderRec folderRec;
			using (this.performanceLogger.GetTaskFrame(SyncActivity.GetFolderRec))
			{
				folderRec = folder.GetFolderRec(PublicFolderHierarchySyncExecutor.AdditionalPtagsToLoadForGetFolderRec, GetFolderRecFlags.None);
			}
			return folderRec;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000511C File Offset: 0x0000331C
		private void SetHierarchyReadyFlag()
		{
			IRecipientSession adsession = this.syncContext.GetADSession();
			ADRecipient contentMailboxADRecipient = this.syncContext.GetContentMailboxADRecipient(adsession);
			if (contentMailboxADRecipient == null)
			{
				this.syncContext.Logger.LogEvent(LogEventType.Warning, "IsHierarchyReady not set as user was not found on AD");
				return;
			}
			if (!(bool)contentMailboxADRecipient[ADRecipientSchema.IsHierarchyReady])
			{
				this.syncContext.Logger.LogEvent(LogEventType.Verbose, "Setting IsHierarchyReady=true");
				contentMailboxADRecipient[ADRecipientSchema.IsHierarchyReady] = true;
				adsession.Save(contentMailboxADRecipient);
				return;
			}
			this.syncContext.Logger.LogEvent(LogEventType.Verbose, "IsHierarchyReady was already set");
		}

		// Token: 0x0400003E RID: 62
		private const string SyncState = "SyncState";

		// Token: 0x0400003F RID: 63
		private const string FinalJobSyncState = "FinalJobSyncState";

		// Token: 0x04000040 RID: 64
		private const string PartiallyCommittedFolderIds = "PartiallyCommittedFolderIds";

		// Token: 0x04000041 RID: 65
		private const int MaxRetriesOnTransientException = 3;

		// Token: 0x04000042 RID: 66
		private const int RetentionPolicyInSecondsForOrphanFolder = 5184000;

		// Token: 0x04000043 RID: 67
		private static readonly Trace Tracer = ExTraceGlobals.PublicFolderSynchronizerTracer;

		// Token: 0x04000044 RID: 68
		private static readonly TimeSpan RetryDelayOnTransientException = TimeSpan.FromSeconds(10.0);

		// Token: 0x04000045 RID: 69
		private static readonly PropTag[] AlwaysExcludedFolderPtags = new PropTag[]
		{
			PropTag.ContainerContents,
			PropTag.FolderAssociatedContents,
			PropTag.ContainerHierarchy,
			PropTag.MessageSize,
			PropTag.InternetArticleNumber,
			PropTag.Access,
			PropTag.SubFolders,
			PropTag.AssocContentCount,
			PropTag.SourceKey,
			PropTag.ParentSourceKey,
			PropTag.ChangeKey,
			PropTag.NTSD,
			PropTag.FreeBusyNTSD
		};

		// Token: 0x04000046 RID: 70
		private static readonly PropTag[] ExcludedRootFolderPtags = new PropTag[]
		{
			PropTag.DisplayName,
			PropTag.Comment
		};

		// Token: 0x04000047 RID: 71
		private static readonly PropTag[] AdditionalPtagsToLoadForGetFolderRec = new PropTag[]
		{
			PropTag.IpmWasteBasketEntryId
		};

		// Token: 0x04000048 RID: 72
		private static readonly PropTag[] PropertiesToLoadForOrphanFolder = new PropTag[]
		{
			PropTag.FolderPathName,
			PropTag.ContentCount,
			PropTag.SubFolders
		};

		// Token: 0x04000049 RID: 73
		private static readonly PropTag[] BatchFolderUpdatePtagsToExclude = new List<PropTag>(PublicFolderHierarchySyncExecutor.AlwaysExcludedFolderPtags.Union(PublicFolderHierarchySyncExecutor.AdditionalPtagsToLoadForGetFolderRec)).ToArray();

		// Token: 0x0400004A RID: 74
		private readonly int batchSize;

		// Token: 0x0400004B RID: 75
		private readonly PublicFolderSynchronizerContext syncContext;

		// Token: 0x0400004C RID: 76
		private readonly PublicFolderPerformanceLogger performanceLogger;

		// Token: 0x0400004D RID: 77
		private readonly TransientExceptionHandler transientExceptionHandler;

		// Token: 0x0400004E RID: 78
		private int batchNumber = 1;

		// Token: 0x0400004F RID: 79
		private int updatedFolderIndex;

		// Token: 0x04000050 RID: 80
		private int deletedFolderIndex;

		// Token: 0x04000051 RID: 81
		private IdSet folderIdSet;

		// Token: 0x04000052 RID: 82
		private string nextSyncState;

		// Token: 0x04000053 RID: 83
		private int numberOfFoldersSynced;

		// Token: 0x04000054 RID: 84
		private bool isCurrentManifestBatchFullyProcessed = true;

		// Token: 0x04000055 RID: 85
		private bool isTrySaveExecuted;

		// Token: 0x04000056 RID: 86
		private byte[] currentFolderId;

		// Token: 0x04000057 RID: 87
		private bool isCurrentOperationUpdate;

		// Token: 0x0200000E RID: 14
		private sealed class PublicFolderRec
		{
			// Token: 0x06000086 RID: 134 RVA: 0x000052CD File Offset: 0x000034CD
			public PublicFolderRec(IFolder folder, FolderRec folderRec)
			{
				this.Folder = folder;
				this.FolderRec = folderRec;
				this.DumpsterFolderId = PublicFolderHierarchyProxyPool.GetDumpsterEntryIdFromFolderRec(folderRec);
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x06000087 RID: 135 RVA: 0x000052EF File Offset: 0x000034EF
			// (set) Token: 0x06000088 RID: 136 RVA: 0x000052F7 File Offset: 0x000034F7
			public IFolder Folder { get; private set; }

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x06000089 RID: 137 RVA: 0x00005300 File Offset: 0x00003500
			// (set) Token: 0x0600008A RID: 138 RVA: 0x00005308 File Offset: 0x00003508
			public FolderRec FolderRec { get; private set; }

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x0600008B RID: 139 RVA: 0x00005311 File Offset: 0x00003511
			// (set) Token: 0x0600008C RID: 140 RVA: 0x00005319 File Offset: 0x00003519
			public byte[] DumpsterFolderId { get; private set; }
		}

		// Token: 0x0200000F RID: 15
		private sealed class SyncAllAndProcessEach : PublicFolderHierarchySyncExecutor
		{
			// Token: 0x0600008D RID: 141 RVA: 0x00005322 File Offset: 0x00003522
			public SyncAllAndProcessEach(PublicFolderSynchronizerContext syncContext, IBudget budget, string callerInfo) : base(syncContext, budget, callerInfo)
			{
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x0600008E RID: 142 RVA: 0x0000532D File Offset: 0x0000352D
			protected override bool IsLastSync
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x0600008F RID: 143 RVA: 0x00005330 File Offset: 0x00003530
			protected override EnumerateHierarchyChangesFlags EnumerateHierarchyChangesFlags
			{
				get
				{
					return EnumerateHierarchyChangesFlags.None;
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000090 RID: 144 RVA: 0x00005333 File Offset: 0x00003533
			protected override int MaxHierarchyChanges
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06000091 RID: 145 RVA: 0x00005336 File Offset: 0x00003536
			protected override int ProcessChanges()
			{
				return base.ProcessChangeOneByOne();
			}
		}

		// Token: 0x02000010 RID: 16
		private sealed class PagedSyncAndBulkProcess : PublicFolderHierarchySyncExecutor
		{
			// Token: 0x06000092 RID: 146 RVA: 0x00005340 File Offset: 0x00003540
			public PagedSyncAndBulkProcess(PublicFolderSynchronizerContext syncContext, IBudget budget, string callerInfo) : base(syncContext, budget, callerInfo)
			{
				this.maxHierarchyChanges = PublicFolderHierarchySyncExecutor.GetConfigValue("ChangesPerIcsManifestPage", "SyncMaxChanges", 1000);
				this.syncContext.SourceMailbox.SetOtherSideVersion(this.syncContext.DestinationMailbox.GetVersion());
				this.syncContext.DestinationMailbox.SetOtherSideVersion(this.syncContext.SourceMailbox.GetVersion());
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x06000093 RID: 147 RVA: 0x000053BB File Offset: 0x000035BB
			protected override bool IsLastSync
			{
				get
				{
					return base.HierarchyChangeManifest != null && !base.HierarchyChangeManifest.HasMoreHierarchyChanges;
				}
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000094 RID: 148 RVA: 0x000053D5 File Offset: 0x000035D5
			protected override EnumerateHierarchyChangesFlags EnumerateHierarchyChangesFlags
			{
				get
				{
					if (!base.IsFirstBatch)
					{
						return EnumerateHierarchyChangesFlags.None;
					}
					return EnumerateHierarchyChangesFlags.FirstPage;
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x06000095 RID: 149 RVA: 0x000053E2 File Offset: 0x000035E2
			protected override int MaxHierarchyChanges
			{
				get
				{
					return this.maxHierarchyChanges;
				}
			}

			// Token: 0x06000096 RID: 150 RVA: 0x000053EA File Offset: 0x000035EA
			protected override int ProcessChanges()
			{
				return base.ProcessChangeInBulk();
			}

			// Token: 0x0400005D RID: 93
			private readonly int maxHierarchyChanges = 500;
		}
	}
}
