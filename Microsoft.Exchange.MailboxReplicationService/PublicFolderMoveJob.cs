using System;
using System.Collections.Generic;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000099 RID: 153
	internal class PublicFolderMoveJob : MoveBaseJob
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00034BF7 File Offset: 0x00032DF7
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x00034BFF File Offset: 0x00032DFF
		public SourceMailboxWrapper SourceMbxWrapper { get; private set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00034C08 File Offset: 0x00032E08
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x00034C10 File Offset: 0x00032E10
		public MailboxWrapper PrimaryHierarchyMbxWrapper { get; private set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00034C19 File Offset: 0x00032E19
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x00034C21 File Offset: 0x00032E21
		public override bool IsOnlineMove
		{
			get
			{
				return base.IsOnlineMove;
			}
			protected set
			{
				base.IsOnlineMove = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00034C2A File Offset: 0x00032E2A
		protected override bool ReachedThePointOfNoReturn
		{
			get
			{
				return base.TimeTracker.CurrentState == RequestState.Completed || base.SyncStage >= SyncStage.FinalIncrementalSync;
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00034C4C File Offset: 0x00032E4C
		public override void Initialize(TransactionalRequestJob moveRequestJob)
		{
			MrsTracer.Service.Function("PublicFolderMoveJob.Initialize: Moving folders from , SourceMailbox=\"{0}\", TargetMailbox=\"{1}\", Flags={2}", new object[]
			{
				moveRequestJob.SourceExchangeGuid,
				moveRequestJob.TargetExchangeGuid,
				moveRequestJob.Flags
			});
			base.Initialize(moveRequestJob);
			TenantPublicFolderConfigurationCache.Instance.RemoveValue(moveRequestJob.OrganizationId);
			this.publicFolderConfiguration = TenantPublicFolderConfigurationCache.Instance.GetValue(moveRequestJob.OrganizationId);
			this.foldersToMove = new List<byte[]>(moveRequestJob.FolderList.Count);
			foreach (MoveFolderInfo moveFolderInfo in moveRequestJob.FolderList)
			{
				this.foldersToMove.Add(HexConverter.HexStringToByteArray(moveFolderInfo.EntryId));
			}
			string orgID = (moveRequestJob.OrganizationId != null && moveRequestJob.OrganizationId.OrganizationalUnit != null) ? (moveRequestJob.OrganizationId.OrganizationalUnit.Name + "\\") : string.Empty;
			LocalizedString sourceTracingID = MrsStrings.PublicFolderMoveTracingId(orgID, moveRequestJob.SourceExchangeGuid);
			LocalizedString targetTracingID = MrsStrings.PublicFolderMoveTracingId(orgID, moveRequestJob.TargetExchangeGuid);
			this.publicFolderMover = new PublicFolderMover(moveRequestJob, this, this.foldersToMove, MailboxCopierFlags.Root, sourceTracingID, targetTracingID);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00034DAC File Offset: 0x00032FAC
		public override List<MailboxCopierBase> GetAllCopiers()
		{
			return new List<MailboxCopierBase>
			{
				this.publicFolderMover
			};
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00034DCC File Offset: 0x00032FCC
		public override void ValidateAndPopulateRequestJob(List<ReportEntry> entries)
		{
			this.ConfigureProviders(false);
			MailboxServerInformation mailboxServerInformation = null;
			MailboxInformation mailboxInformation = null;
			this.ConnectSource(this.publicFolderMover, out mailboxServerInformation, out mailboxInformation);
			if (mailboxInformation != null && mailboxInformation.ServerVersion != 0)
			{
				base.CachedRequestJob.SourceVersion = mailboxInformation.ServerVersion;
				base.CachedRequestJob.SourceServer = ((mailboxServerInformation != null) ? mailboxServerInformation.MailboxServerName : null);
			}
			this.ConnectDestination(this.publicFolderMover, out mailboxServerInformation, out mailboxInformation);
			if (mailboxInformation != null && mailboxInformation.ServerVersion != 0)
			{
				base.CachedRequestJob.TargetVersion = mailboxInformation.ServerVersion;
				base.CachedRequestJob.TargetServer = ((mailboxServerInformation != null) ? mailboxServerInformation.MailboxServerName : null);
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00034E6C File Offset: 0x0003306C
		protected override void ConfigureProviders(bool continueAfterConfiguringProviders)
		{
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				ISourceMailbox sourceMailbox = this.ConfigureSourceMailbox();
				disposeGuard.Add<ISourceMailbox>(sourceMailbox);
				IDestinationMailbox destinationMailbox = this.ConfigureDestinationMailbox();
				disposeGuard.Add<IDestinationMailbox>(destinationMailbox);
				this.PrimaryHierarchyMbxWrapper = this.ConfigureHierarchyMailbox(sourceMailbox, destinationMailbox);
				string orgID = (base.CachedRequestJob.OrganizationId != null && base.CachedRequestJob.OrganizationId.OrganizationalUnit != null) ? (base.CachedRequestJob.OrganizationId.OrganizationalUnit.Name + "\\") : string.Empty;
				this.SourceMbxWrapper = new SourceMailboxWrapper(sourceMailbox, MailboxWrapperFlags.Source, MrsStrings.PublicFolderMoveTracingId(orgID, base.CachedRequestJob.SourceExchangeGuid));
				this.publicFolderMover.SetMailboxWrappers(this.SourceMbxWrapper, destinationMailbox);
				disposeGuard.Success();
			}
			base.ConfigureProviders(continueAfterConfiguringProviders);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00034F5C File Offset: 0x0003315C
		protected override void UnconfigureProviders()
		{
			if (this.SourceMbxWrapper != null)
			{
				this.SourceMbxWrapper.Dispose();
				this.SourceMbxWrapper = null;
			}
			if (this.PrimaryHierarchyMbxWrapper != null)
			{
				this.PrimaryHierarchyMbxWrapper.Dispose();
				this.PrimaryHierarchyMbxWrapper = null;
			}
			base.UnconfigureProviders();
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00034F98 File Offset: 0x00033198
		protected override void MigrateSecurityDescriptors()
		{
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00034F9C File Offset: 0x0003319C
		protected override void MakeConnections()
		{
			base.MakeConnections();
			if (!this.publicFolderMover.IsSourceConnected)
			{
				this.publicFolderMover.ConnectSourceMailbox(MailboxConnectFlags.None);
				base.TimeTracker.SetTimestamp(RequestJobTimestamp.LastSuccessfulSourceConnection, new DateTime?(DateTime.UtcNow));
				base.TimeTracker.SetTimestamp(RequestJobTimestamp.SourceConnectionFailure, null);
				MailboxServerInformation mailboxServerInformation;
				bool flag;
				this.publicFolderMover.SourceMailboxWrapper.UpdateLastConnectedServerInfo(out mailboxServerInformation, out flag);
			}
			this.publicFolderMover.UpdateSourceDestinationFolderIds();
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00035014 File Offset: 0x00033214
		protected override MailboxChangesManifest EnumerateSourceHierarchyChanges(MailboxCopierBase mbxCtx, bool catchup, SyncContext syncContext)
		{
			return new MailboxChangesManifest
			{
				ChangedFolders = new List<byte[]>(),
				DeletedFolders = new List<byte[]>()
			};
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00035040 File Offset: 0x00033240
		protected override void CleanupOrphanedDestinationMailbox()
		{
			MrsTracer.Service.Debug("WorkItem: CleanupOrphanedDestinationMailbox - cleaning up partially created contents if any at the destination mailbox", new object[0]);
			if (this.IsOnlineMove)
			{
				base.CheckServersHealth();
			}
			this.publicFolderMover.ClearSyncState(SyncStateClearReason.CleanupOrphanedMailbox);
			base.Report.Append(MrsStrings.ReportCleanUpFoldersDestination("PreparingToMove"));
			this.CleanUpFoldersAtDestination();
			this.SetDestinationInTransitStatus(this.publicFolderMover);
			this.publicFolderMover.SyncState = new PersistedSyncData(base.RequestJobGuid);
			this.publicFolderMover.ICSSyncState = new MailboxMapiSyncState();
			uint mailboxSignatureVersion = (this.publicFolderMover.DestMailboxWrapper.LastConnectedServerInfo != null) ? this.publicFolderMover.DestMailboxWrapper.LastConnectedServerInfo.MailboxSignatureVersion : 0U;
			this.publicFolderMover.SyncState.MailboxSignatureVersion = mailboxSignatureVersion;
			base.ScheduleWorkItem(new Action(this.CatchupFolderHierarchy), WorkloadType.Unknown);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00035124 File Offset: 0x00033324
		protected override void CatchupFolderHierarchy()
		{
			if (!this.PrimaryHierarchyMbxWrapper.Mailbox.IsConnected())
			{
				this.PrimaryHierarchyMbxWrapper.Mailbox.Connect(MailboxConnectFlags.None);
			}
			bool flag = false;
			bool flag2 = false;
			Guid hierarchyMailboxGuid = this.publicFolderConfiguration.GetHierarchyMailboxInformation().HierarchyMailboxGuid;
			if (hierarchyMailboxGuid == base.CachedRequestJob.SourceExchangeGuid)
			{
				flag = true;
			}
			else if (hierarchyMailboxGuid == base.CachedRequestJob.TargetExchangeGuid)
			{
				flag2 = true;
			}
			foreach (byte[] array in this.foldersToMove)
			{
				if (flag)
				{
					using (IFolder folder = ((ISourceMailbox)this.PrimaryHierarchyMbxWrapper.Mailbox).GetFolder(array))
					{
						using (IDestinationFolder folder2 = this.publicFolderMover.DestMailbox.GetFolder(this.publicFolderMover.DestMailbox.GetSessionSpecificEntryId(array)))
						{
							if (folder2 == null)
							{
								if (folder != null)
								{
									MrsTracer.Service.Error("Inconsistency of hierarchy seen at target mailbox...May be a delay in hierarchy synchronization", new object[0]);
									throw new DestinationFolderHierarchyInconsistentTransientException();
								}
								MrsTracer.Service.Debug("Folder {0} unavailable at both source and destination mailbox during catchup", new object[]
								{
									HexConverter.ByteArrayToHexString(array)
								});
							}
						}
						continue;
					}
				}
				if (flag2)
				{
					using (IFolder folder3 = ((IDestinationMailbox)this.PrimaryHierarchyMbxWrapper.Mailbox).GetFolder(array))
					{
						using (ISourceFolder folder4 = this.publicFolderMover.SourceMailbox.GetFolder(this.publicFolderMover.SourceMailbox.GetSessionSpecificEntryId(array)))
						{
							if (folder4 == null)
							{
								if (folder3 != null)
								{
									MrsTracer.Service.Error("Inconsistency of hierarchy seen at source mailbox...May be a delay in hierarchy synchronization", new object[0]);
									throw new SourceFolderHierarchyInconsistentTransientException();
								}
								MrsTracer.Service.Debug("Folder {0} unavailable at both source and destination mailbox during catchup", new object[]
								{
									HexConverter.ByteArrayToHexString(array)
								});
							}
						}
						continue;
					}
				}
				using (IFolder folder5 = ((IDestinationMailbox)this.PrimaryHierarchyMbxWrapper.Mailbox).GetFolder(array))
				{
					if (folder5 == null)
					{
						MrsTracer.Service.Debug("Folder {0} unavailable at hierarchy mailbox during catchup", new object[]
						{
							HexConverter.ByteArrayToHexString(array)
						});
					}
					else
					{
						using (ISourceFolder folder6 = this.publicFolderMover.SourceMailbox.GetFolder(this.publicFolderMover.SourceMailbox.GetSessionSpecificEntryId(array)))
						{
							if (folder6 == null)
							{
								MrsTracer.Service.Error("Inconsistency of hierarchy seen at source mailbox...May be a delay in hierarchy synchronization", new object[0]);
								throw new SourceFolderHierarchyInconsistentTransientException();
							}
						}
						using (IDestinationFolder folder7 = this.publicFolderMover.DestMailbox.GetFolder(this.publicFolderMover.DestMailbox.GetSessionSpecificEntryId(array)))
						{
							if (folder7 == null)
							{
								MrsTracer.Service.Error("Inconsistency of hierarchy seen at target mailbox...May be a delay in hierarchy synchronization", new object[0]);
								throw new DestinationFolderHierarchyInconsistentTransientException();
							}
						}
					}
				}
			}
			base.SyncStage = SyncStage.CreatingInitialSyncCheckpoint;
			base.OverallProgress = 15;
			base.TimeTracker.CurrentState = RequestState.CreatingInitialSyncCheckpoint;
			base.SaveState(SaveStateFlags.Regular, delegate(TransactionalRequestJob moveRequest)
			{
				moveRequest.RestartFromScratch = false;
			});
			base.ScheduleWorkItem(new Action(base.CatchupFolders), WorkloadType.Unknown);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0003552C File Offset: 0x0003372C
		protected override void SetSourceInTransitStatus(MailboxCopierBase mbxCtx, InTransitStatus status, out bool sourceSupportsOnlineMove)
		{
			mbxCtx.SourceMailbox.SetInTransitStatus(status | InTransitStatus.ForPublicFolderMove, out sourceSupportsOnlineMove);
			if (!sourceSupportsOnlineMove)
			{
				MrsTracer.Service.Debug("Source does not support online move for public folder move job.", new object[0]);
				throw new OnlineMoveNotSupportedPermanentException(base.CachedRequestJob.SourceExchangeGuid.ToString());
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00035584 File Offset: 0x00033784
		protected override void SetDestinationInTransitStatus(MailboxCopierBase mbxCtx)
		{
			InTransitStatus inTransitStatus = InTransitStatus.MoveDestination | InTransitStatus.OnlineMove | InTransitStatus.ForPublicFolderMove;
			if (base.CachedRequestJob.AllowLargeItems)
			{
				inTransitStatus |= InTransitStatus.AllowLargeItems;
			}
			bool flag;
			mbxCtx.DestMailbox.SetInTransitStatus(inTransitStatus, out flag);
			if (!flag)
			{
				MrsTracer.Service.Debug("Destination does not support online move for public folder move job.", new object[0]);
				throw new OnlineMoveNotSupportedPermanentException(base.CachedRequestJob.TargetExchangeGuid.ToString());
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x000355EC File Offset: 0x000337EC
		protected override void FinalSync()
		{
			base.TestIntegration.Barrier("PostponeFinalSync", new Action(base.RefreshRequestIfNeeded));
			if (!this.PrimaryHierarchyMbxWrapper.Mailbox.IsConnected())
			{
				this.PrimaryHierarchyMbxWrapper.Mailbox.Connect(MailboxConnectFlags.None);
			}
			base.FinalSync();
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0003563E File Offset: 0x0003383E
		protected override bool ValidateTargetMailbox(MailboxInformation mailboxInfo, out LocalizedString moveFinishedReason)
		{
			moveFinishedReason = MrsStrings.ReportTargetPublicFolderContentMailboxGuidUpdated;
			return true;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x000358F0 File Offset: 0x00033AF0
		protected override void CleanupCanceledJob()
		{
			base.CheckDisposed();
			MrsTracer.Service.Debug("Deleting messages and dumpster folder.", new object[0]);
			using (List<MailboxCopierBase>.Enumerator enumerator = this.GetAllCopiers().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MailboxCopierBase mbxCtx = enumerator.Current;
					CommonUtils.CatchKnownExceptions(delegate
					{
						if (!mbxCtx.IsDestinationConnected)
						{
							mbxCtx.ConnectDestinationMailbox(MailboxConnectFlags.None);
						}
						SyncStateError syncStateError = mbxCtx.LoadSyncState(this.Report);
						if (syncStateError != SyncStateError.None && this.CanBeCanceledOrSuspended())
						{
							MrsTracer.Service.Debug("Deleting folders at destination mailbox {0}", new object[]
							{
								mbxCtx.TargetTracingID
							});
							MrsTracer.Service.Debug(MrsStrings.ReportCleanUpFoldersDestination("CleanupCanceledJob"), new object[0]);
							this.CleanUpFoldersAtDestination();
						}
						mbxCtx.ClearSyncState(SyncStateClearReason.JobCanceled);
						bool flag;
						mbxCtx.DestMailbox.SetInTransitStatus(InTransitStatus.NotInTransit, out flag);
					}, delegate(Exception failure)
					{
						LocalizedString localizedString = CommonUtils.FullExceptionMessage(failure);
						this.Report.Append(MrsStrings.ReportDestinationMailboxCleanupFailed2(CommonUtils.GetFailureType(failure)), failure, ReportEntryFlags.Cleanup | ReportEntryFlags.Target);
						MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_DestinationMailboxCleanupFailed, new object[]
						{
							this.RequestJobIdentity,
							mbxCtx.TargetTracingID,
							this.CachedRequestJob.TargetMDBName,
							localizedString
						});
					});
				}
			}
			MoveHistoryEntryInternal mhei;
			base.RemoveRequest(true, out mhei);
			using (List<MailboxCopierBase>.Enumerator enumerator2 = this.GetAllCopiers().GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					PublicFolderMoveJob.<>c__DisplayClassc CS$<>8__locals3 = new PublicFolderMoveJob.<>c__DisplayClassc();
					CS$<>8__locals3.mbxCtx = enumerator2.Current;
					bool resetInTransitSuccess = false;
					CommonUtils.CatchKnownExceptions(delegate
					{
						if (!CS$<>8__locals3.mbxCtx.IsSourceConnected)
						{
							CS$<>8__locals3.mbxCtx.ConnectSourceMailbox(MailboxConnectFlags.None);
						}
						bool flag;
						CS$<>8__locals3.mbxCtx.SourceMailbox.SetInTransitStatus(InTransitStatus.NotInTransit, out flag);
						resetInTransitSuccess = true;
						if (CS$<>8__locals3.mbxCtx.IsRoot && mhei != null)
						{
							CS$<>8__locals3.mbxCtx.SourceMailbox.AddMoveHistoryEntry(mhei, this.GetConfig<int>("MaxMoveHistoryLength"));
						}
					}, delegate(Exception failure)
					{
						if (!resetInTransitSuccess)
						{
							LocalizedString localizedString = CommonUtils.FullExceptionMessage(failure);
							MailboxReplicationService.LogEvent(MRSEventLogConstants.Tuple_SourceMailboxResetFailed, new object[]
							{
								this.RequestJobIdentity,
								CS$<>8__locals3.mbxCtx.SourceTracingID,
								this.CachedRequestJob.SourceMDBName,
								localizedString
							});
						}
					});
				}
			}
			base.Disconnect();
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00035A68 File Offset: 0x00033C68
		protected override void UpdateMovedMailbox()
		{
			if (!this.PrimaryHierarchyMbxWrapper.Mailbox.IsConnected())
			{
				this.PrimaryHierarchyMbxWrapper.Mailbox.Connect(MailboxConnectFlags.None);
			}
			Guid hierarchyMailboxGuid = this.publicFolderConfiguration.GetHierarchyMailboxInformation().HierarchyMailboxGuid;
			IDataConverter<PropValue, PropValueData> dataConverter = new PropValueConverter();
			int num = 0;
			byte[] array = null;
			PropTag propTag = this.PrimaryHierarchyMbxWrapper.Mailbox.GetIDsFromNames(true, new NamedPropData[]
			{
				new NamedPropData(FolderSchema.LastMovedTimeStamp.Guid, FolderSchema.LastMovedTimeStamp.PropertyName)
			})[0];
			propTag = propTag.ChangePropType(PropType.SysTime);
			List<MoveFolderInfo> moveFolderList = base.CachedRequestJob.FolderList;
			foreach (MoveFolderInfo moveFolderInfo in moveFolderList)
			{
				array = HexConverter.HexStringToByteArray(moveFolderInfo.EntryId);
				if (!moveFolderInfo.IsFinalized)
				{
					IFolder folder2;
					if (!(hierarchyMailboxGuid == base.CachedRequestJob.SourceExchangeGuid))
					{
						IFolder folder = ((IDestinationMailbox)this.PrimaryHierarchyMbxWrapper.Mailbox).GetFolder(array);
						folder2 = folder;
					}
					else
					{
						folder2 = ((ISourceMailbox)this.PrimaryHierarchyMbxWrapper.Mailbox).GetFolder(array);
					}
					PropValue nativeRepresentation2;
					using (IFolder folder3 = folder2)
					{
						if (folder3 == null)
						{
							MrsTracer.Service.Debug("Folder {0} unavailable at hierarchy mailbox during finalization", new object[]
							{
								HexConverter.ByteArrayToHexString(array)
							});
							continue;
						}
						List<PropValueData> list = new List<PropValueData>(2);
						PropValueData[] props = folder3.GetProps(new PropTag[]
						{
							PropTag.TimeInServer,
							PropTag.PfProxy
						});
						PropValue nativeRepresentation = dataConverter.GetNativeRepresentation(props[0]);
						list.Add(new PropValueData(propTag, ExDateTime.UtcNow));
						if (!nativeRepresentation.IsNull() && !nativeRepresentation.IsError())
						{
							ELCFolderFlags elcfolderFlags = (ELCFolderFlags)nativeRepresentation.Value;
							if (elcfolderFlags.HasFlag(ELCFolderFlags.DumpsterFolder))
							{
								moveFolderInfo.IsFinalized = true;
								folder3.SetProps(list.ToArray());
								continue;
							}
						}
						list.Add(new PropValueData(PropTag.ReplicaList, ReplicaListProperty.GetBytesFromStringArray(new string[]
						{
							base.CachedRequestJob.TargetExchangeGuid.ToString()
						})));
						folder3.SetProps(list.ToArray());
						nativeRepresentation2 = dataConverter.GetNativeRepresentation(props[1]);
					}
					if (hierarchyMailboxGuid != base.CachedRequestJob.SourceExchangeGuid)
					{
						this.UpdateSourceFolder(array);
					}
					this.UpdateDestinationFolder(array, nativeRepresentation2);
					moveFolderInfo.IsFinalized = true;
					base.Report.AppendDebug(string.Format("Folder {0} has been successfully finalized.", HexConverter.ByteArrayToHexString(array)));
					num++;
					if (num == 100)
					{
						base.SaveRequest(true, delegate(TransactionalRequestJob rj)
						{
							rj.FolderList = moveFolderList;
						});
						num = 0;
					}
				}
			}
			base.SaveRequest(true, delegate(TransactionalRequestJob rj)
			{
				rj.FolderList = base.CachedRequestJob.FolderList;
			});
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00035DA0 File Offset: 0x00033FA0
		protected override void PostMoveUpdateSourceMailbox(MailboxCopierBase mbxCtx)
		{
			mbxCtx.SyncState.CompletedCleanupTasks |= PostMoveCleanupStatusFlags.UpdateSourceMailbox;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00035DB6 File Offset: 0x00033FB6
		protected override void PostMoveCleanupSourceMailbox(MailboxCopierBase mbxCtx)
		{
			mbxCtx.SyncState.CompletedCleanupTasks |= PostMoveCleanupStatusFlags.SourceMailboxCleanup;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00035DCB File Offset: 0x00033FCB
		protected override void PostMoveMarkRehomeOnRelatedRequests(MailboxCopierBase mbxCtx)
		{
			mbxCtx.SyncState.CompletedCleanupTasks |= PostMoveCleanupStatusFlags.SetRelatedRequestsRehome;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00035DE1 File Offset: 0x00033FE1
		protected override void CleanupDestinationMailbox(MailboxCopierBase mbxCtx, bool moveIsSuccessful)
		{
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00035E20 File Offset: 0x00034020
		protected override void ScheduleContentVerification(List<FolderSizeRec> verificationResults)
		{
			FolderMap sourceFolderMap = this.publicFolderMover.GetSourceFolderMap(GetFolderMapFlags.None);
			sourceFolderMap.EnumerateFolderHierarchy(EnumHierarchyFlags.NormalFolders | EnumHierarchyFlags.RootFolder, delegate(FolderRecWrapper folderRec, FolderMap.EnumFolderContext ctx)
			{
				this.ScheduleWorkItem<PublicFolderMover, FolderRecWrapper, List<FolderSizeRec>>(new Action<PublicFolderMover, FolderRecWrapper, List<FolderSizeRec>>(this.VerifyFolderContents), this.publicFolderMover, folderRec, verificationResults, WorkloadType.Unknown);
			});
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00035E64 File Offset: 0x00034064
		protected override void VerifyFolderContents(MailboxCopierBase mbxCtx, FolderRecWrapper folderRecWrapper, List<FolderSizeRec> verificationResults)
		{
			FolderSizeRec folderSizeRec = mbxCtx.VerifyFolderContents(folderRecWrapper, WellKnownFolderType.None, false);
			verificationResults.Add(folderSizeRec);
			mbxCtx.ReportBadItems(folderSizeRec.MissingItems);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00035E90 File Offset: 0x00034090
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.SourceMbxWrapper != null)
				{
					this.SourceMbxWrapper.Dispose();
					this.SourceMbxWrapper = null;
				}
				if (this.PrimaryHierarchyMbxWrapper != null)
				{
					this.PrimaryHierarchyMbxWrapper.Dispose();
					this.PrimaryHierarchyMbxWrapper = null;
				}
				if (this.publicFolderMover != null)
				{
					this.publicFolderMover.UnconfigureProviders();
					this.publicFolderMover = null;
				}
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00035EF5 File Offset: 0x000340F5
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PublicFolderMoveJob>(this);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00035F00 File Offset: 0x00034100
		private static void EmptyContents(IFolder folder)
		{
			List<MessageRec> list = new List<MessageRec>();
			list = folder.EnumerateMessages(EnumerateMessagesFlags.AllMessages, null);
			if (list.Count > 0)
			{
				List<byte[]> list2 = new List<byte[]>();
				foreach (MessageRec messageRec in list)
				{
					list2.Add(messageRec.EntryId);
				}
				folder.DeleteMessages(list2.ToArray());
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00035F80 File Offset: 0x00034180
		private ISourceMailbox ConfigureSourceMailbox()
		{
			PublicFolderRecipient localMailboxRecipient = this.publicFolderConfiguration.GetLocalMailboxRecipient(base.CachedRequestJob.SourceExchangeGuid);
			if (localMailboxRecipient == null)
			{
				throw new RecipientNotFoundPermanentException(base.CachedRequestJob.SourceExchangeGuid);
			}
			List<MRSProxyCapabilities> list = new List<MRSProxyCapabilities>();
			list.Add(MRSProxyCapabilities.PublicFolderMove);
			LocalMailboxFlags sourceMbxFlags = CommonUtils.MapDatabaseToProxyServer(localMailboxRecipient.Database.ObjectGuid).ExtraFlags | LocalMailboxFlags.PublicFolderMove | LocalMailboxFlags.Move;
			ISourceMailbox sourceMailbox = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				sourceMailbox = this.publicFolderMover.GetSourceMailbox(localMailboxRecipient.Database, sourceMbxFlags, list);
				disposeGuard.Add<ISourceMailbox>(sourceMailbox);
				sourceMailbox.Config(base.GetReservation(localMailboxRecipient.Database.ObjectGuid, ReservationFlags.Read), base.CachedRequestJob.SourceExchangeGuid, base.CachedRequestJob.SourceExchangeGuid, CommonUtils.GetPartitionHint(base.CachedRequestJob.OrganizationId), localMailboxRecipient.Database.ObjectGuid, MailboxType.SourceMailbox, null);
				disposeGuard.Success();
			}
			return sourceMailbox;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00036094 File Offset: 0x00034294
		private IDestinationMailbox ConfigureDestinationMailbox()
		{
			PublicFolderRecipient localMailboxRecipient = this.publicFolderConfiguration.GetLocalMailboxRecipient(base.CachedRequestJob.TargetExchangeGuid);
			if (localMailboxRecipient == null)
			{
				throw new RecipientNotFoundPermanentException(base.CachedRequestJob.TargetExchangeGuid);
			}
			List<MRSProxyCapabilities> list = new List<MRSProxyCapabilities>();
			list.Add(MRSProxyCapabilities.PublicFolderMove);
			LocalMailboxFlags targetMbxFlags = CommonUtils.MapDatabaseToProxyServer(localMailboxRecipient.Database.ObjectGuid).ExtraFlags | LocalMailboxFlags.PublicFolderMove | LocalMailboxFlags.Move;
			IDestinationMailbox destinationMailbox = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				destinationMailbox = this.publicFolderMover.GetDestinationMailbox(localMailboxRecipient.Database.ObjectGuid, targetMbxFlags, list);
				disposeGuard.Add<IDestinationMailbox>(destinationMailbox);
				destinationMailbox.Config(base.GetReservation(localMailboxRecipient.Database.ObjectGuid, ReservationFlags.Write), base.CachedRequestJob.TargetExchangeGuid, base.CachedRequestJob.TargetExchangeGuid, CommonUtils.GetPartitionHint(base.CachedRequestJob.OrganizationId), localMailboxRecipient.Database.ObjectGuid, MailboxType.DestMailboxIntraOrg, null);
				disposeGuard.Success();
			}
			return destinationMailbox;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000361AC File Offset: 0x000343AC
		private MailboxWrapper ConfigureHierarchyMailbox(ISourceMailbox sourceMailbox, IDestinationMailbox destinationMailbox)
		{
			MailboxWrapper mailboxWrapper = null;
			Guid hierarchyMailboxGuid = this.publicFolderConfiguration.GetHierarchyMailboxInformation().HierarchyMailboxGuid;
			string orgID = (base.CachedRequestJob.OrganizationId != null && base.CachedRequestJob.OrganizationId.OrganizationalUnit != null) ? (base.CachedRequestJob.OrganizationId.OrganizationalUnit.Name + "\\") : string.Empty;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				if (hierarchyMailboxGuid == base.CachedRequestJob.SourceExchangeGuid)
				{
					mailboxWrapper = new SourceMailboxWrapper(sourceMailbox, MailboxWrapperFlags.Source, MrsStrings.PublicFolderMoveTracingId(orgID, base.CachedRequestJob.SourceExchangeGuid));
					disposeGuard.Add<MailboxWrapper>(mailboxWrapper);
				}
				else if (hierarchyMailboxGuid == base.CachedRequestJob.TargetExchangeGuid)
				{
					mailboxWrapper = new DestinationMailboxWrapper(destinationMailbox, MailboxWrapperFlags.Target, MrsStrings.PublicFolderMoveTracingId(orgID, base.CachedRequestJob.TargetExchangeGuid), new Guid[]
					{
						hierarchyMailboxGuid
					});
					disposeGuard.Add<MailboxWrapper>(mailboxWrapper);
				}
				else
				{
					PublicFolderRecipient localMailboxRecipient = this.publicFolderConfiguration.GetLocalMailboxRecipient(hierarchyMailboxGuid);
					if (localMailboxRecipient == null)
					{
						throw new RecipientNotFoundPermanentException(hierarchyMailboxGuid);
					}
					List<MRSProxyCapabilities> list = new List<MRSProxyCapabilities>();
					list.Add(MRSProxyCapabilities.PublicFolderMove);
					ProxyServerSettings proxyServerSettings = CommonUtils.MapDatabaseToProxyServer(localMailboxRecipient.Database.ObjectGuid);
					LocalMailboxFlags flags = proxyServerSettings.ExtraFlags | LocalMailboxFlags.PublicFolderMove | LocalMailboxFlags.Move;
					RemoteDestinationMailbox remoteDestinationMailbox = new RemoteDestinationMailbox(proxyServerSettings.Fqdn, null, null, MailboxCopierBase.DefaultProxyControlFlags, list, false, flags);
					disposeGuard.Add<RemoteDestinationMailbox>(remoteDestinationMailbox);
					mailboxWrapper = new DestinationMailboxWrapper(remoteDestinationMailbox, MailboxWrapperFlags.Target, MrsStrings.PublicFolderMoveTracingId(orgID, hierarchyMailboxGuid), new Guid[]
					{
						hierarchyMailboxGuid
					});
					disposeGuard.Add<MailboxWrapper>(mailboxWrapper);
					mailboxWrapper.Mailbox.Config(base.GetReservation(localMailboxRecipient.Database.ObjectGuid, ReservationFlags.Write), hierarchyMailboxGuid, hierarchyMailboxGuid, CommonUtils.GetPartitionHint(base.CachedRequestJob.OrganizationId), localMailboxRecipient.Database.ObjectGuid, MailboxType.DestMailboxIntraOrg, null);
				}
				disposeGuard.Success();
			}
			return mailboxWrapper;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x000363D0 File Offset: 0x000345D0
		private void ConnectSource(MailboxCopierBase mbxCtx, out MailboxServerInformation sourceMailboxServerInfo, out MailboxInformation sourceMailboxInfo)
		{
			mbxCtx.ConnectSourceMailbox(MailboxConnectFlags.None);
			sourceMailboxServerInfo = mbxCtx.SourceMailbox.GetMailboxServerInformation();
			sourceMailboxInfo = mbxCtx.SourceMailbox.GetMailboxInformation();
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000363F3 File Offset: 0x000345F3
		private void ConnectDestination(MailboxCopierBase mbxCtx, out MailboxServerInformation destinationMailboxServerInfo, out MailboxInformation destinationMailboxInfo)
		{
			mbxCtx.ConnectDestinationMailbox(MailboxConnectFlags.None);
			destinationMailboxServerInfo = mbxCtx.DestMailbox.GetMailboxServerInformation();
			destinationMailboxInfo = mbxCtx.DestMailbox.GetMailboxInformation();
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00036418 File Offset: 0x00034618
		private void CleanUpFoldersAtDestination()
		{
			IDataConverter<PropValue, PropValueData> dataConverter = new PropValueConverter();
			foreach (byte[] array in this.foldersToMove)
			{
				byte[] sessionSpecificEntryId = this.publicFolderMover.DestMailbox.GetSessionSpecificEntryId(array);
				using (IDestinationFolder folder = this.publicFolderMover.DestMailbox.GetFolder(sessionSpecificEntryId))
				{
					if (folder == null)
					{
						MrsTracer.Service.Debug("CleanUpFoldersAtDestination: Folder {0} is not present to cleanup at target mailbox", new object[]
						{
							HexConverter.ByteArrayToHexString(array)
						});
					}
					else
					{
						PropValueData[] props = folder.GetProps(new PropTag[]
						{
							PropTag.ReplicaList
						});
						PropValue nativeRepresentation = dataConverter.GetNativeRepresentation(props[0]);
						byte[] array2 = nativeRepresentation.Value as byte[];
						if (!nativeRepresentation.IsNull() && !nativeRepresentation.IsError() && array2 != null)
						{
							StorePropertyDefinition replicaList = CoreFolderSchema.ReplicaList;
							string[] stringArrayFromBytes = ReplicaListProperty.GetStringArrayFromBytes(array2);
							Guid empty = Guid.Empty;
							if (stringArrayFromBytes.Length > 0 && GuidHelper.TryParseGuid(stringArrayFromBytes[0], out empty) && empty == base.CachedRequestJob.TargetExchangeGuid)
							{
								throw new FolderAlreadyInTargetPermanentException(HexConverter.ByteArrayToHexString(array));
							}
						}
						PublicFolderMoveJob.EmptyContents(folder);
						folder.SetRules(Array<RuleData>.Empty);
					}
				}
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00036598 File Offset: 0x00034798
		private void UpdateSourceFolder(byte[] hierarchyFolderEntryId)
		{
			byte[] sessionSpecificEntryId = this.publicFolderMover.SourceMailbox.GetSessionSpecificEntryId(hierarchyFolderEntryId);
			using (ISourceFolder folder = this.publicFolderMover.SourceMailbox.GetFolder(sessionSpecificEntryId))
			{
				if (folder == null)
				{
					MrsTracer.Service.Error("UpdateSourceFolder: Folder {0} unavailable at source mailbox", new object[]
					{
						HexConverter.ByteArrayToHexString(hierarchyFolderEntryId)
					});
					throw new UpdateFolderFailedTransientException();
				}
				folder.SetProps(new PropValueData[]
				{
					new PropValueData(PropTag.ReplicaList, ReplicaListProperty.GetBytesFromStringArray(new string[]
					{
						base.CachedRequestJob.TargetExchangeGuid.ToString()
					}))
				});
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00036658 File Offset: 0x00034858
		private void UpdateDestinationFolder(byte[] hierarchyFolderEntryId, PropValue pfProxyValue)
		{
			byte[] sessionSpecificEntryId = this.publicFolderMover.DestMailbox.GetSessionSpecificEntryId(hierarchyFolderEntryId);
			using (IDestinationFolder folder = this.publicFolderMover.DestMailbox.GetFolder(sessionSpecificEntryId))
			{
				if (folder == null)
				{
					MrsTracer.Service.Error("UpdateDestinationFolder: Folder {0} unavailable at target mailbox", new object[]
					{
						HexConverter.ByteArrayToHexString(hierarchyFolderEntryId)
					});
					throw new UpdateFolderFailedTransientException();
				}
				folder.SetProps(new PropValueData[]
				{
					new PropValueData(PropTag.ReplicaList, ReplicaListProperty.GetBytesFromStringArray(new string[]
					{
						base.CachedRequestJob.TargetExchangeGuid.ToString()
					}))
				});
				if (!pfProxyValue.IsNull() && !pfProxyValue.IsError())
				{
					byte[] bytes = pfProxyValue.GetBytes();
					if (bytes != null && bytes.Length == 16 && new Guid(bytes) != Guid.Empty)
					{
						Guid a = folder.LinkMailPublicFolder(LinkMailPublicFolderFlags.ObjectGuid, bytes);
						if (a == Guid.Empty)
						{
							base.Report.Append(new ReportEntry(MrsStrings.ReportFailedToLinkADPublicFolder(folder.GetFolderRec(null, GetFolderRecFlags.None).FolderName, BitConverter.ToString(bytes), BitConverter.ToString(hierarchyFolderEntryId)), ReportEntryType.Warning));
						}
					}
				}
			}
		}

		// Token: 0x0400030B RID: 779
		private const int DefaultBatchSize = 100;

		// Token: 0x0400030C RID: 780
		private const int SizeOfGuid = 16;

		// Token: 0x0400030D RID: 781
		private PublicFolderMover publicFolderMover;

		// Token: 0x0400030E RID: 782
		private List<byte[]> foldersToMove;

		// Token: 0x0400030F RID: 783
		private TenantPublicFolderConfiguration publicFolderConfiguration;
	}
}
