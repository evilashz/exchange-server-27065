using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200008E RID: 142
	internal class MergeJob : BaseJob
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0002FCF9 File Offset: 0x0002DEF9
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x0002FD01 File Offset: 0x0002DF01
		public DateTime MailboxSnapshotTimestamp { get; protected set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0002FD0A File Offset: 0x0002DF0A
		protected override int CopyStartPercentage
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0002FD0E File Offset: 0x0002DF0E
		protected override int CopyEndPercentage
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0002FD12 File Offset: 0x0002DF12
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0002FD1A File Offset: 0x0002DF1A
		protected MailboxMerger MailboxMerger { get; set; }

		// Token: 0x06000717 RID: 1815 RVA: 0x0002FD24 File Offset: 0x0002DF24
		public override void Initialize(TransactionalRequestJob mergeRequest)
		{
			base.Initialize(mergeRequest);
			if (mergeRequest.RequestType == MRSRequestType.MailboxImport || mergeRequest.RequestType == MRSRequestType.MailboxExport || mergeRequest.RequestType == MRSRequestType.MailboxRestore || mergeRequest.RequestType == MRSRequestType.Sync)
			{
				return;
			}
			base.RequestJobIdentity = mergeRequest.Identity.ToString();
			bool flag = mergeRequest.RequestStyle == RequestStyle.CrossOrg && mergeRequest.Direction == RequestDirection.Pull;
			bool flag2 = mergeRequest.RequestStyle == RequestStyle.CrossOrg && mergeRequest.Direction == RequestDirection.Push;
			LocalizedString sourceTracingID = LocalizedString.Empty;
			LocalizedString targetTracingID = LocalizedString.Empty;
			string orgID = (mergeRequest.OrganizationId != null && mergeRequest.OrganizationId.OrganizationalUnit != null) ? (mergeRequest.OrganizationId.OrganizationalUnit.Name + "\\") : string.Empty;
			Guid guid;
			if (flag)
			{
				guid = Guid.Empty;
				sourceTracingID = MrsStrings.RPCHTTPMailboxId(mergeRequest.RemoteMailboxLegacyDN);
			}
			else
			{
				guid = mergeRequest.SourceExchangeGuid;
				sourceTracingID = (mergeRequest.SourceIsArchive ? MrsStrings.ArchiveMailboxTracingId(orgID, guid) : MrsStrings.PrimaryMailboxTracingId(orgID, guid));
			}
			Guid guid2;
			if (flag2)
			{
				guid2 = Guid.Empty;
				targetTracingID = MrsStrings.RPCHTTPMailboxId(mergeRequest.RemoteMailboxLegacyDN);
			}
			else
			{
				guid2 = mergeRequest.TargetExchangeGuid;
				targetTracingID = (mergeRequest.TargetIsArchive ? MrsStrings.ArchiveMailboxTracingId(orgID, guid2) : MrsStrings.PrimaryMailboxTracingId(orgID, guid2));
			}
			MailboxCopierFlags mailboxCopierFlags = MailboxCopierFlags.Merge;
			if (mergeRequest.RequestStyle == RequestStyle.CrossOrg)
			{
				mailboxCopierFlags |= MailboxCopierFlags.CrossOrg;
			}
			if (mergeRequest.SourceIsArchive)
			{
				mailboxCopierFlags |= MailboxCopierFlags.SourceIsArchive;
			}
			if (mergeRequest.TargetIsArchive)
			{
				mailboxCopierFlags |= MailboxCopierFlags.TargetIsArchive;
			}
			this.MailboxMerger = new MailboxMerger(guid, guid2, mergeRequest, this, mailboxCopierFlags, sourceTracingID, targetTracingID);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0002FEA4 File Offset: 0x0002E0A4
		public override List<MailboxCopierBase> GetAllCopiers()
		{
			return new List<MailboxCopierBase>
			{
				this.MailboxMerger
			};
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0002FEC4 File Offset: 0x0002E0C4
		public override void ValidateAndPopulateRequestJob(List<ReportEntry> entries)
		{
			base.ConfigureMailboxProviders();
			if (base.CachedRequestJob.TargetIsLocal || !base.CachedRequestJob.SkipInitialConnectionValidation)
			{
				this.MailboxMerger.ConnectDestinationMailbox(MailboxConnectFlags.ValidateOnly);
				base.CachedRequestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.LastSuccessfulTargetConnection, new DateTime?(DateTime.UtcNow));
				base.CachedRequestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.TargetConnectionFailure, null);
				MailboxServerInformation mailboxServerInformation = this.MailboxMerger.DestMailbox.GetMailboxServerInformation();
				MailboxInformation mailboxInformation = this.MailboxMerger.DestMailbox.GetMailboxInformation();
				if (mailboxInformation != null && mailboxInformation.ServerVersion != 0)
				{
					base.CachedRequestJob.TargetVersion = mailboxInformation.ServerVersion;
					base.CachedRequestJob.TargetServer = ((mailboxServerInformation != null) ? mailboxServerInformation.MailboxServerName : null);
				}
			}
			this.AfterTargetConnect();
			if (base.CachedRequestJob.SourceIsLocal || !base.CachedRequestJob.SkipInitialConnectionValidation)
			{
				this.MailboxMerger.ConnectSourceMailbox(MailboxConnectFlags.ValidateOnly);
				base.CachedRequestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.LastSuccessfulSourceConnection, new DateTime?(DateTime.UtcNow));
				base.CachedRequestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.SourceConnectionFailure, null);
				MailboxServerInformation mailboxServerInformation2 = this.MailboxMerger.SourceMailbox.GetMailboxServerInformation();
				MailboxInformation mailboxInformation2 = this.MailboxMerger.SourceMailbox.GetMailboxInformation();
				if (mailboxInformation2 != null && mailboxInformation2.ServerVersion != 0)
				{
					base.CachedRequestJob.SourceVersion = mailboxInformation2.ServerVersion;
					base.CachedRequestJob.SourceServer = ((mailboxServerInformation2 != null) ? mailboxServerInformation2.MailboxServerName : null);
				}
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00030051 File Offset: 0x0002E251
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.MailboxMerger != null)
			{
				this.MailboxMerger.UnconfigureProviders();
				this.MailboxMerger = null;
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00030077 File Offset: 0x0002E277
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MergeJob>(this);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0003007F File Offset: 0x0002E27F
		protected virtual void AfterTargetConnect()
		{
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0003024C File Offset: 0x0002E44C
		protected override void MakeConnections()
		{
			Exception sourceConnectFailure = null;
			Exception targetConnectFailure = null;
			int sourceVersion = 0;
			int targetVersion = 0;
			string sourceServerName = null;
			string targetServerName = null;
			CommonUtils.CatchKnownExceptions(delegate
			{
				MrsTracer.Service.Debug("Attempting to connect to the destination mailbox {0}.", new object[]
				{
					this.MailboxMerger.TargetTracingID
				});
				this.MailboxMerger.ConnectDestinationMailbox(MailboxConnectFlags.None);
			}, delegate(Exception failure)
			{
				MrsTracer.Service.Warning("Failed to connect to destination mailbox: {0}", new object[]
				{
					CommonUtils.FullExceptionMessage(failure)
				});
				targetConnectFailure = failure;
			});
			if (targetConnectFailure == null)
			{
				MailboxServerInformation mailboxServerInformation = this.MailboxMerger.DestMailbox.GetMailboxServerInformation();
				MailboxInformation mailboxInformation = this.MailboxMerger.DestMailbox.GetMailboxInformation();
				this.MailboxMerger.TargetServerInfo = mailboxServerInformation;
				if (mailboxServerInformation != null)
				{
					ConnectivityRec connectivityRec = new ConnectivityRec(ServerKind.Target, mailboxInformation, mailboxServerInformation);
					base.Report.Append(MrsStrings.ReportDestinationMailboxConnection(this.MailboxMerger.TargetTracingID, mailboxServerInformation.ServerInfoString, (mailboxInformation != null) ? mailboxInformation.MdbName : "(null)"), connectivityRec);
					targetServerName = mailboxServerInformation.MailboxServerName;
					targetVersion = mailboxServerInformation.MailboxServerVersion;
				}
				if (!this.MailboxMerger.DestMailbox.MailboxExists())
				{
					throw new MailboxDoesNotExistPermanentException(this.MailboxMerger.TargetTracingID);
				}
				MrsTracer.Service.Debug("Destination mailbox {0} exists.", new object[]
				{
					this.MailboxMerger.TargetTracingID
				});
				base.TimeTracker.SetTimestamp(RequestJobTimestamp.LastSuccessfulTargetConnection, new DateTime?(DateTime.UtcNow));
				base.TimeTracker.SetTimestamp(RequestJobTimestamp.TargetConnectionFailure, null);
				this.MailboxMerger.LoadSyncState(base.Report);
			}
			if (targetConnectFailure == null)
			{
				this.AfterTargetConnect();
			}
			CommonUtils.CatchKnownExceptions(delegate
			{
				MrsTracer.Service.Debug("Connecting to the source mailbox {0}.", new object[]
				{
					this.MailboxMerger.SourceTracingID
				});
				this.MailboxMerger.ConnectSourceMailbox(MailboxConnectFlags.None);
			}, delegate(Exception failure)
			{
				MrsTracer.Service.Warning("Failed to connect to source mailbox: {0}", new object[]
				{
					CommonUtils.FullExceptionMessage(failure)
				});
				sourceConnectFailure = failure;
			});
			if (sourceConnectFailure == null)
			{
				base.TimeTracker.SetTimestamp(RequestJobTimestamp.LastSuccessfulSourceConnection, new DateTime?(DateTime.UtcNow));
				base.TimeTracker.SetTimestamp(RequestJobTimestamp.SourceConnectionFailure, null);
				MailboxServerInformation mailboxServerInformation2 = this.MailboxMerger.SourceMailbox.GetMailboxServerInformation();
				MailboxInformation mailboxInformation2 = this.MailboxMerger.SourceMailbox.GetMailboxInformation();
				this.MailboxMerger.SourceServerInfo = mailboxServerInformation2;
				if (mailboxServerInformation2 != null)
				{
					ConnectivityRec connectivityRec2 = new ConnectivityRec(ServerKind.Source, mailboxInformation2, mailboxServerInformation2);
					base.Report.Append(MrsStrings.ReportSourceMailboxConnection(this.MailboxMerger.SourceTracingID, mailboxServerInformation2.ServerInfoString, (mailboxInformation2 != null) ? mailboxInformation2.MdbName : "(null)"), connectivityRec2);
					sourceServerName = mailboxServerInformation2.MailboxServerName;
					sourceVersion = mailboxServerInformation2.MailboxServerVersion;
				}
			}
			if (sourceConnectFailure != null || targetConnectFailure != null)
			{
				base.CheckRequestIsValid();
				if (sourceConnectFailure != null)
				{
					throw sourceConnectFailure;
				}
				if (targetConnectFailure != null)
				{
					throw targetConnectFailure;
				}
			}
			base.SaveRequest(true, delegate(TransactionalRequestJob rj)
			{
				this.TimeTracker.SetTimestamp(RequestJobTimestamp.Failure, null);
				this.TimeTracker.SetTimestamp(RequestJobTimestamp.Suspended, null);
				rj.FailureCode = null;
				rj.FailureType = null;
				rj.FailureSide = null;
				rj.Message = LocalizedString.Empty;
				rj.SourceServer = sourceServerName;
				rj.SourceVersion = sourceVersion;
				rj.TargetServer = targetServerName;
				rj.TargetVersion = targetVersion;
				rj.Status = RequestStatus.InProgress;
				this.TimeTracker.CurrentState = RequestState.InitialSeeding;
				RequestJobLog.Write(rj);
			});
			if (!string.IsNullOrEmpty(base.CachedRequestJob.ContentFilter))
			{
				RestrictionData contentRestriction;
				string text;
				ContentFilterBuilder.ProcessContentFilter(base.CachedRequestJob.ContentFilter, base.CachedRequestJob.ContentFilterLCID, null, this.MailboxMerger.SourceMailboxWrapper, out contentRestriction, out text);
				this.MailboxMerger.ContentRestriction = contentRestriction;
			}
			if (this.MailboxMerger.SupportsRuleAPIs)
			{
				this.MailboxMerger.SourceMailbox.ConfigMailboxOptions(MailboxOptions.IgnoreExtendedRuleFAIs);
			}
			this.MailboxMerger.ExchangeSourceAndTargetVersions();
			base.ScheduleWorkItem(new Action(this.StartMerge), WorkloadType.Unknown);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000305A0 File Offset: 0x0002E7A0
		protected override void UpdateRequestOnSave(TransactionalRequestJob rj, UpdateRequestOnSaveType updateType)
		{
			if (this.MailboxMerger.MailboxSizeTracker.IsFinishedEstimating)
			{
				rj.TotalMailboxItemCount = (ulong)((long)this.MailboxMerger.MailboxSizeTracker.MessageCount);
				rj.TotalMailboxSize = this.MailboxMerger.MailboxSizeTracker.TotalMessageSize;
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x000305EC File Offset: 0x0002E7EC
		protected virtual void StartMerge()
		{
			MrsTracer.Service.Debug("WorkItem: StartMerge", new object[0]);
			base.CheckServersHealth();
			base.Report.Append(MrsStrings.ReportRequestStarted);
			MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_MoveStarted, new object[]
			{
				base.RequestJobIdentity,
				base.RequestJobGuid.ToString(),
				base.CachedRequestJob.SourceMDBName,
				base.CachedRequestJob.TargetMDBName,
				base.CachedRequestJob.Flags.ToString()
			});
			this.MailboxMerger.SyncState = new PersistedSyncData(base.RequestJobGuid);
			this.MailboxMerger.ICSSyncState = new MailboxMapiSyncState();
			if (base.TimeTracker.GetTimestamp(RequestJobTimestamp.Start) == null)
			{
				base.TimeTracker.SetTimestamp(RequestJobTimestamp.Start, new DateTime?(DateTime.UtcNow));
			}
			if (base.CachedRequestJob.IgnoreRuleLimitErrors)
			{
				MailboxInformation mailboxInformation = this.MailboxMerger.SourceMailbox.GetMailboxInformation();
				if (mailboxInformation != null && mailboxInformation.RulesSize > 32768)
				{
					MailboxInformation mailboxInformation2 = this.MailboxMerger.DestMailbox.GetMailboxInformation();
					if (mailboxInformation2 != null && mailboxInformation2.ServerVersion < Server.E2007MinVersion)
					{
						base.Report.Append(MrsStrings.ReportRulesWillNotBeCopied);
					}
				}
			}
			base.SyncStage = SyncStage.CreatingFolderHierarchy;
			base.OverallProgress = 5;
			base.TimeTracker.CurrentState = RequestState.CreatingFolderHierarchy;
			base.SaveState(SaveStateFlags.Regular, null);
			base.ScheduleWorkItem(new Action(this.AnalyzeFolderHierarchy), WorkloadType.Unknown);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00030771 File Offset: 0x0002E971
		protected virtual void CopyFolderData(FolderMapping fm, ISourceFolder srcFolder, IDestinationFolder destFolder)
		{
			this.MailboxMerger.CopyFolderData(fm, srcFolder, destFolder);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00030781 File Offset: 0x0002E981
		protected virtual void FinalizeMerge()
		{
			base.CheckBadItemCount(true);
			if (!base.SkipContentVerification)
			{
				this.ScheduleContentVerification();
			}
			base.ScheduleWorkItem(new Action(this.CompleteMerge), WorkloadType.Unknown);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000307F4 File Offset: 0x0002E9F4
		protected void ScheduleContentVerification()
		{
			base.Report.Append(MrsStrings.ReportVerifyingMailboxContents);
			List<FolderSizeRec> verificationResults = new List<FolderSizeRec>();
			this.MailboxMerger.SourceHierarchy.EnumerateFolderHierarchy(EnumHierarchyFlags.NormalFolders | EnumHierarchyFlags.RootFolder, delegate(FolderRecWrapper folderRecWrapper, FolderMap.EnumFolderContext ctx)
			{
				FolderMapping folderMapping = (FolderMapping)folderRecWrapper;
				if (folderMapping.IsIncluded)
				{
					this.ScheduleWorkItem<FolderMapping, List<FolderSizeRec>>(new Action<FolderMapping, List<FolderSizeRec>>(this.VerifyFolderContents), folderMapping, verificationResults, WorkloadType.Unknown);
				}
			});
			base.ScheduleWorkItem<List<FolderSizeRec>>(new Action<List<FolderSizeRec>>(this.OutputVerificationResults), verificationResults, WorkloadType.Unknown);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00030860 File Offset: 0x0002EA60
		protected void ComputeFolderMapping(bool createMissingFolderRecs)
		{
			MrsTracer.Service.Debug("Loading source hierarchy", new object[0]);
			List<PropTag> list = new List<PropTag>
			{
				PropTag.ContentCount,
				PropTag.MessageSizeExtended,
				PropTag.AssocContentCount,
				PropTag.AssocMessageSizeExtended
			};
			list.AddRange(this.MailboxMerger.GetAdditionalFolderPtags());
			string text = base.CachedRequestJob.SourceRootFolder;
			if (string.IsNullOrEmpty(text) && base.CachedRequestJob.RequestType == MRSRequestType.MailboxImport)
			{
				text = FolderFilterParser.GetAlias(WellKnownFolderType.IpmSubtree);
			}
			this.MailboxMerger.SourceHierarchy.LoadHierarchy(EnumerateFolderHierarchyFlags.None, text, false, list.ToArray());
			base.RefreshRequestIfNeeded();
			base.CheckServersHealth();
			this.MailboxMerger.SourceHierarchy.SetFolderFilter(base.CachedRequestJob.IncludeFolders, !base.CachedRequestJob.ExcludeDumpster, base.CachedRequestJob.ExcludeFolders, base.CachedRequestJob.SourceRootFolder, base.CachedRequestJob.IsLivePublicFolderMailboxRestore, base.CachedRequestJob.SourceExchangeGuid);
			MrsTracer.Service.Debug("Loading target hierarchy", new object[0]);
			string text2 = base.CachedRequestJob.TargetRootFolder;
			if (string.IsNullOrEmpty(text2) && base.CachedRequestJob.RequestType == MRSRequestType.MailboxExport)
			{
				text2 = FolderFilterParser.GetAlias(WellKnownFolderType.IpmSubtree);
			}
			this.MailboxMerger.DestHierarchy.LoadHierarchy(EnumerateFolderHierarchyFlags.None, text2, true, base.CachedRequestJob.IsPublicFolderMailboxRestore ? MergeJob.PublicFolderPropTags : null);
			MrsTracer.Service.Debug("Retrieving destination mailbox culture", new object[0]);
			CultureInfo mailboxCulture = this.MailboxMerger.DestMailboxWrapper.MailboxCulture;
			this.MailboxMerger.SourceHierarchy.TargetMailboxCulture = mailboxCulture;
			MrsTracer.Service.Debug("Computing folder hierarchy mapping", new object[0]);
			if (base.CachedRequestJob.IsPublicFolderMailboxRestore)
			{
				createMissingFolderRecs = false;
			}
			this.MailboxMerger.SourceHierarchy.ComputeFolderMapping(this.MailboxMerger.DestHierarchy, createMissingFolderRecs);
			foreach (LocalizedString msg in this.MailboxMerger.SourceHierarchy.Warnings)
			{
				base.Report.Append(msg);
			}
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00030AA8 File Offset: 0x0002ECA8
		protected void CopyMailboxProperties()
		{
			MrsTracer.Service.Debug("WorkItem: CopyMailboxProperties", new object[0]);
			base.RefreshRequestIfNeeded();
			base.CheckServersHealth();
			this.MailboxMerger.ReportSourceMailboxSize();
			this.MailboxMerger.ReportTargetMailboxSize();
			if (base.CachedRequestJob.RequestType == MRSRequestType.Merge && string.IsNullOrEmpty(base.CachedRequestJob.SourceRootFolder) && string.IsNullOrEmpty(base.CachedRequestJob.TargetRootFolder) && !base.CachedRequestJob.SourceIsArchive && !base.CachedRequestJob.TargetIsArchive)
			{
				FolderMapping wellKnownFolder = this.MailboxMerger.SourceHierarchy.GetWellKnownFolder(WellKnownFolderType.Root);
				if (wellKnownFolder.IsIncluded)
				{
					MrsTracer.Service.Debug("Copying mailbox-level properties", new object[0]);
					this.MailboxMerger.CopyMailboxProperties();
				}
			}
			else
			{
				MrsTracer.Service.Debug("Mailbox-level properties were not copied", new object[0]);
			}
			base.Report.Append(MrsStrings.ReportMessagesCopied);
			this.MailboxMerger.CopyChangedFoldersData();
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00030BA4 File Offset: 0x0002EDA4
		protected virtual void BeforeDataCopy()
		{
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00030D48 File Offset: 0x0002EF48
		protected void CopyFolder(FolderMapping fm)
		{
			base.RefreshRequestIfNeeded();
			base.TimeTracker.CurrentState = RequestState.CopyingMessages;
			base.TestIntegration.Barrier("PostponeWriteMessages", new Action(base.RefreshRequestIfNeeded));
			base.CheckServersHealth();
			ExecutionContext.Create(new DataContext[]
			{
				new FolderRecWrapperDataContext(fm)
			}).Execute(delegate
			{
				if (this.CachedRequestJob.IsPublicFolderMailboxRestore && fm.TargetFolder == null)
				{
					this.Warnings.Add(MrsStrings.FolderIsMissing(HexConverter.ByteArrayToHexString(fm.EntryId)));
					return;
				}
				using (ISourceFolder folder = this.MailboxMerger.SourceMailbox.GetFolder(fm.EntryId))
				{
					if (folder == null)
					{
						this.Report.Append(MrsStrings.ReportSourceFolderDeleted(fm.FullFolderName, TraceUtils.DumpEntryId(fm.EntryId)));
					}
					else
					{
						using (IDestinationFolder folder2 = this.MailboxMerger.DestMailbox.GetFolder(fm.TargetFolder.EntryId))
						{
							if (folder2 == null)
							{
								this.Report.Append(MrsStrings.ReportTargetFolderDeleted(fm.TargetFolder.FullFolderName, TraceUtils.DumpEntryId(fm.TargetFolder.EntryId), fm.FullFolderName));
							}
							else
							{
								this.Report.Append(MrsStrings.ReportMergingFolder(fm.FullFolderName, fm.TargetFolder.FullFolderName));
								this.CopyFolderData(fm, folder, folder2);
								folder2.Flush();
							}
						}
					}
				}
			});
			base.SaveState(SaveStateFlags.Lazy, null);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00030DD2 File Offset: 0x0002EFD2
		private void AnalyzeFolderHierarchy()
		{
			base.RefreshRequestIfNeeded();
			base.CheckServersHealth();
			MrsTracer.Service.Debug("WorkItem: AnalyzeFolderHierarchy", new object[0]);
			this.ComputeFolderMapping(true);
			base.ScheduleWorkItem(new Action(this.CreateFolderHierarchy), WorkloadType.Unknown);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00030E50 File Offset: 0x0002F050
		private void CreateFolderHierarchy()
		{
			base.RefreshRequestIfNeeded();
			base.CheckServersHealth();
			MrsTracer.Service.Debug("WorkItem: create folder hierarchy.", new object[0]);
			MrsTracer.Service.Debug("Creating missing folders in target", new object[0]);
			this.MailboxMerger.DestHierarchy.CreateMissingFolders();
			base.RefreshRequestIfNeeded();
			base.CheckServersHealth();
			List<FolderMapping> foldersToCopy = new List<FolderMapping>();
			foreach (WellKnownFolderType wkfType in MergeJob.FolderSyncOrder)
			{
				FolderMapping wellKnownFolder = this.MailboxMerger.SourceHierarchy.GetWellKnownFolder(wkfType);
				if (wellKnownFolder != null && wellKnownFolder.IsIncluded)
				{
					foldersToCopy.Add(wellKnownFolder);
				}
			}
			this.MailboxMerger.SourceHierarchy.EnumerateFolderHierarchy(EnumHierarchyFlags.AllFolders, delegate(FolderRecWrapper folderRec, FolderMap.EnumFolderContext context)
			{
				FolderMapping folderMapping = (FolderMapping)folderRec;
				if (folderMapping.IsIncluded && !foldersToCopy.Contains(folderMapping))
				{
					foldersToCopy.Add(folderMapping);
				}
			});
			base.ScheduleWorkItem<List<FolderMapping>>(new Action<List<FolderMapping>>(this.CopyMessages), foldersToCopy, WorkloadType.Unknown);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00030F40 File Offset: 0x0002F140
		protected virtual void CopyMessages(IReadOnlyCollection<FolderMapping> foldersToCopy)
		{
			this.BeforeDataCopy();
			foreach (FolderMapping folderMapping in foldersToCopy)
			{
				if (folderMapping.FolderType != FolderType.Search)
				{
					this.MailboxMerger.MailboxSizeTracker.TrackFolder(folderMapping.FolderRec);
				}
				base.ScheduleWorkItem<FolderMapping>(new Action<FolderMapping>(this.CopyFolder), folderMapping, WorkloadType.Unknown);
			}
			this.MailboxMerger.MailboxSizeTracker.IsFinishedEstimating = true;
			base.ScheduleWorkItem(new Action(this.CopyMailboxProperties), WorkloadType.Unknown);
			base.ScheduleWorkItem(new Action(this.FinalizeMerge), WorkloadType.Unknown);
			base.Report.Append(MrsStrings.ReportMergeInitialized(this.MailboxMerger.TargetTracingID, foldersToCopy.Count, this.MailboxMerger.MailboxSizeTracker.MessageCount, new ByteQuantifiedSize(this.MailboxMerger.MailboxSizeTracker.TotalMessageSize).ToString()));
			base.SyncStage = SyncStage.CopyingMessages;
			base.OverallProgress = this.CopyStartPercentage;
			base.TimeTracker.CurrentState = RequestState.CopyingMessages;
			base.TotalMessages = this.MailboxMerger.MailboxSizeTracker.MessageCount;
			base.TotalMessageByteSize = this.MailboxMerger.MailboxSizeTracker.TotalMessageSize;
			this.MailboxSnapshotTimestamp = DateTime.UtcNow;
			base.SaveState(SaveStateFlags.Regular, null);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000310A8 File Offset: 0x0002F2A8
		private void OutputVerificationResults(List<FolderSizeRec> verificationResults)
		{
			int num = 0;
			ulong num2 = 0UL;
			bool flag = false;
			foreach (FolderSizeRec folderSizeRec in verificationResults)
			{
				num += folderSizeRec.Source.Count + folderSizeRec.SourceFAI.Count;
				num2 += folderSizeRec.Source.Size + folderSizeRec.SourceFAI.Size;
				if (folderSizeRec.Missing.Count > 0)
				{
					flag = true;
				}
			}
			base.Report.Append(MrsStrings.ReportMailboxContentsVerificationComplete(verificationResults.Count, num, new ByteQuantifiedSize(num2).ToString()), verificationResults);
			if (flag)
			{
				base.GetSkippedItemCounts().RecordMissingItems(verificationResults);
			}
			base.CheckBadItemCount(true);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00031180 File Offset: 0x0002F380
		private void VerifyFolderContents(FolderMapping fm, List<FolderSizeRec> verificationResults)
		{
			FolderSizeRec folderSizeRec = this.MailboxMerger.VerifyFolderContents(fm, fm.WKFType, false);
			if (folderSizeRec != null)
			{
				verificationResults.Add(folderSizeRec);
				this.MailboxMerger.ReportBadItems(folderSizeRec.MissingItems);
			}
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0003126C File Offset: 0x0002F46C
		protected void CompleteMerge()
		{
			base.TestIntegration.Barrier("PostponeResumeAccessToMailbox", new Action(base.RefreshRequestIfNeeded));
			base.ReportProgress(true);
			base.TimeTracker.SetTimestamp(RequestJobTimestamp.InitialSeedingCompleted, new DateTime?(DateTime.UtcNow));
			base.SyncStage = SyncStage.SyncFinished;
			base.OverallProgress = 100;
			base.TimeTracker.CurrentState = RequestState.Completed;
			CommonUtils.CatchKnownExceptions(delegate
			{
				this.MailboxMerger.ClearSyncState(SyncStateClearReason.MergeComplete);
			}, null);
			CommonUtils.CatchKnownExceptions(delegate
			{
				base.Report.Append(MrsStrings.ReportRequestCompleted);
				MoveHistoryEntryInternal moveHistoryEntryInternal;
				base.CompleteRequest(this.MailboxMerger.IsOlcSync, out moveHistoryEntryInternal);
				RequestJobLog.Write(base.CachedRequestJob, RequestState.InitialSeedingComplete);
			}, delegate(Exception failure)
			{
				LocalizedString localizedString = CommonUtils.FullExceptionMessage(failure);
				MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_FailedToUpdateCompletedRequest, new object[]
				{
					base.RequestJobIdentity,
					base.GetRequestKeyGuid().ToString(),
					base.RequestJobStoringMDB.ToString(),
					localizedString
				});
			});
		}

		// Token: 0x040002FB RID: 763
		public static readonly WellKnownFolderType[] FolderSyncOrder = new WellKnownFolderType[]
		{
			WellKnownFolderType.Contacts,
			WellKnownFolderType.Calendar,
			WellKnownFolderType.Tasks,
			WellKnownFolderType.Notes,
			WellKnownFolderType.Inbox,
			WellKnownFolderType.SentItems
		};

		// Token: 0x040002FC RID: 764
		public static readonly PropTag[] PublicFolderPropTags = new PropTag[]
		{
			PropTag.LTID,
			PropTag.IpmWasteBasketEntryId,
			PropTag.TimeInServer
		};
	}
}
