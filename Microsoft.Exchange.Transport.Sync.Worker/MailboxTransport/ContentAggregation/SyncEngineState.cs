using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Worker.Health;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000214 RID: 532
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SyncEngineState : DisposeTrackableBase
	{
		// Token: 0x060012BF RID: 4799 RVA: 0x0004089C File Offset: 0x0003EA9C
		internal SyncEngineState(ISubscriptionInformationLoader subscriptionInformationLoader, SyncLogSession syncLogSession, bool originalIsUnderRecoveryFlag, SyncPoisonStatus subscriptionPoisonStatus, SyncHealthData syncHealthData, string subscriptionPoisonCallstack, string legacyDn, MailSubmitter mailSubmitter, SyncMode syncMode, SyncStorageProviderConnectionStatistics connectionStatistics, ISyncWorkerData mailboxServerSubscription, IRemoteServerHealthChecker remoteServerHealthChecker)
		{
			SyncUtilities.ThrowIfArgumentNull("subscriptionInformationLoader", subscriptionInformationLoader);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			SyncUtilities.ThrowIfArgumentNull("syncHealthData", syncHealthData);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("legacyDn", legacyDn);
			SyncUtilities.ThrowIfArgumentNull("mailSubmitter", mailSubmitter);
			SyncUtilities.ThrowIfArgumentNull("connectionStatistics", connectionStatistics);
			SyncUtilities.ThrowIfArgumentNull("remoteServerHealthChecker", remoteServerHealthChecker);
			this.mailboxServerSubscription = mailboxServerSubscription;
			if (this.mailboxServerSubscription != null)
			{
				this.OnSubscriptionLoad(mailboxServerSubscription);
			}
			this.subscriptionInformationLoader = subscriptionInformationLoader;
			this.connectionStatistics = connectionStatistics;
			this.syncMailboxSession = new SyncMailboxSession(syncLogSession);
			this.syncLogSession = syncLogSession;
			this.startSyncTime = ExDateTime.UtcNow;
			this.originalIsUnderRecoveryFlag = originalIsUnderRecoveryFlag;
			this.subscriptionPoisonStatus = subscriptionPoisonStatus;
			this.syncHealthData = syncHealthData;
			this.subscriptionPoisonCallstack = subscriptionPoisonCallstack;
			this.legacyDn = legacyDn;
			this.mailSubmitter = mailSubmitter;
			this.syncMode = syncMode;
			this.remoteServerHealthChecker = remoteServerHealthChecker;
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x00040990 File Offset: 0x0003EB90
		internal SyncProgress PreviousSyncProgress
		{
			get
			{
				return this.previousSyncProgress.Value;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x0004099D File Offset: 0x0003EB9D
		internal OrganizationId OrganizationId
		{
			get
			{
				base.CheckDisposed();
				return this.organizationId;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x000409AB File Offset: 0x0003EBAB
		internal ISyncWorkerData MailboxServerSubscription
		{
			get
			{
				base.CheckDisposed();
				return this.mailboxServerSubscription;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x000409B9 File Offset: 0x0003EBB9
		internal ISyncWorkerData UserMailboxSubscription
		{
			get
			{
				base.CheckDisposed();
				return this.userMailboxSubscription;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x000409C7 File Offset: 0x0003EBC7
		internal SyncMailboxSession SyncMailboxSession
		{
			get
			{
				base.CheckDisposed();
				return this.syncMailboxSession;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x000409D5 File Offset: 0x0003EBD5
		internal NativeSyncStorageProvider NativeProvider
		{
			get
			{
				base.CheckDisposed();
				return this.nativeProvider;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x000409E3 File Offset: 0x0003EBE3
		internal ISyncStorageProvider CloudProvider
		{
			get
			{
				base.CheckDisposed();
				return this.cloudProvider;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x000409F1 File Offset: 0x0003EBF1
		internal IStateStorage StateStorage
		{
			get
			{
				base.CheckDisposed();
				return this.stateStorage;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x000409FF File Offset: 0x0003EBFF
		// (set) Token: 0x060012C9 RID: 4809 RVA: 0x00040A0D File Offset: 0x0003EC0D
		internal AsyncOperationResult<SyncProviderResultData> LastSyncOperationResult
		{
			get
			{
				base.CheckDisposed();
				return this.lastSyncOperationResult;
			}
			set
			{
				base.CheckDisposed();
				this.lastSyncOperationResult = value;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x00040A1C File Offset: 0x0003EC1C
		// (set) Token: 0x060012CB RID: 4811 RVA: 0x00040A2A File Offset: 0x0003EC2A
		internal SyncEngineStep CurrentStep
		{
			get
			{
				base.CheckDisposed();
				return this.currentStep;
			}
			set
			{
				base.CheckDisposed();
				this.currentStep = value;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x00040A39 File Offset: 0x0003EC39
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x00040A48 File Offset: 0x0003EC48
		internal SyncEngineStep? ContinutationSyncStep
		{
			get
			{
				base.CheckDisposed();
				return this.continuationSyncStep;
			}
			set
			{
				base.CheckDisposed();
				if (value == SyncEngineStep.PreSyncStepInEnumerateChangesMode || value == SyncEngineStep.PreSyncStepInCheckForChangesMode)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Setting Invalid ContinutationSyncStep:{0}, it should not be a PreSyncStep.", new object[]
					{
						value
					}), "ContinutationSyncStep");
				}
				this.continuationSyncStep = value;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x00040ABC File Offset: 0x0003ECBC
		internal SyncLogSession SyncLogSession
		{
			get
			{
				base.CheckDisposed();
				return this.syncLogSession;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x00040ACA File Offset: 0x0003ECCA
		internal ExDateTime StartSyncTime
		{
			get
			{
				base.CheckDisposed();
				return this.startSyncTime;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x00040AD8 File Offset: 0x0003ECD8
		internal NativeSyncStorageProviderState NativeProviderState
		{
			get
			{
				base.CheckDisposed();
				return this.nativeProviderState;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x00040AE6 File Offset: 0x0003ECE6
		internal SyncStorageProviderState CloudProviderState
		{
			get
			{
				base.CheckDisposed();
				return this.cloudProviderState;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00040AF4 File Offset: 0x0003ECF4
		internal SyncStorageProviderConnectionStatistics ConnectionStatistics
		{
			get
			{
				base.CheckDisposed();
				return this.connectionStatistics;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x00040B02 File Offset: 0x0003ED02
		internal bool OriginalIsUnderRecoveryFlag
		{
			get
			{
				base.CheckDisposed();
				return this.originalIsUnderRecoveryFlag;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00040B10 File Offset: 0x0003ED10
		internal bool UnderRetry
		{
			get
			{
				base.CheckDisposed();
				return this.underRetry;
			}
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00040B1E File Offset: 0x0003ED1E
		internal void SetSyncUnderRetry()
		{
			base.CheckDisposed();
			this.underRetry = true;
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00040B2D File Offset: 0x0003ED2D
		internal bool MoreItemsAvailable
		{
			get
			{
				base.CheckDisposed();
				return this.moreItemsAvailable;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00040B3B File Offset: 0x0003ED3B
		internal SyncPhase SyncPhaseBeforeSync
		{
			get
			{
				base.CheckDisposed();
				return this.syncPhaseBeforeSync;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00040B49 File Offset: 0x0003ED49
		internal AggregationStatus UserMailboxSubscriptionStatusBeforeSync
		{
			get
			{
				base.CheckDisposed();
				return this.userMailboxSubscriptionStatusBeforeSync;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00040B57 File Offset: 0x0003ED57
		public bool SubscriptionNotificationSent
		{
			get
			{
				base.CheckDisposed();
				return this.subscriptionNotificationSent;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x00040B65 File Offset: 0x0003ED65
		// (set) Token: 0x060012DB RID: 4827 RVA: 0x00040B73 File Offset: 0x0003ED73
		internal int CloudItemsSynced
		{
			get
			{
				base.CheckDisposed();
				return this.cloudItemsSynced;
			}
			set
			{
				base.CheckDisposed();
				this.cloudItemsSynced = value;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00040B82 File Offset: 0x0003ED82
		// (set) Token: 0x060012DD RID: 4829 RVA: 0x00040B90 File Offset: 0x0003ED90
		internal string UpdatedSyncWatermark
		{
			get
			{
				base.CheckDisposed();
				return this.updatedSyncWatermark;
			}
			set
			{
				base.CheckDisposed();
				SyncUtilities.ThrowIfArgumentNull("UpdatedSyncWatermark", value);
				this.updatedSyncWatermark = value;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x00040BAA File Offset: 0x0003EDAA
		internal SyncHealthData SyncHealthData
		{
			get
			{
				base.CheckDisposed();
				return this.syncHealthData;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x00040BB8 File Offset: 0x0003EDB8
		internal object SyncRoot
		{
			get
			{
				base.CheckDisposed();
				return this.syncRoot;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x00040BC6 File Offset: 0x0003EDC6
		internal int TotalSuccessfulRoundtrips
		{
			get
			{
				base.CheckDisposed();
				return this.connectionStatistics.TotalSuccessfulRoundtrips;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x00040BD9 File Offset: 0x0003EDD9
		internal TimeSpan AverageSuccessfulRoundtripTime
		{
			get
			{
				base.CheckDisposed();
				return this.connectionStatistics.AverageSuccessfulRoundtripTime;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x00040BEC File Offset: 0x0003EDEC
		internal int TotalUnsuccessfulRoundtrips
		{
			get
			{
				base.CheckDisposed();
				return this.connectionStatistics.TotalUnsuccessfulRoundtrips;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x00040BFF File Offset: 0x0003EDFF
		internal TimeSpan AverageUnsuccessfulRoundtripTime
		{
			get
			{
				base.CheckDisposed();
				return this.connectionStatistics.AverageUnsuccessfulRoundtripTime;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x00040C12 File Offset: 0x0003EE12
		internal TimeSpan AverageBackoffTime
		{
			get
			{
				base.CheckDisposed();
				return this.connectionStatistics.AverageBackoffTime;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x00040C25 File Offset: 0x0003EE25
		internal ThrottlingStatistics ThrottlingStatistics
		{
			get
			{
				base.CheckDisposed();
				return this.connectionStatistics.ThrottlingStatistics;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x00040C38 File Offset: 0x0003EE38
		internal bool TryCancel
		{
			get
			{
				base.CheckDisposed();
				return this.tryCancel;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x00040C46 File Offset: 0x0003EE46
		internal SyncPoisonStatus SubscriptionPoisonStatus
		{
			get
			{
				base.CheckDisposed();
				return this.subscriptionPoisonStatus;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x00040C54 File Offset: 0x0003EE54
		internal string SubscriptionPoisonCallstack
		{
			get
			{
				base.CheckDisposed();
				return this.subscriptionPoisonCallstack;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x00040C62 File Offset: 0x0003EE62
		internal string LegacyDN
		{
			get
			{
				base.CheckDisposed();
				return this.legacyDn;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x00040C70 File Offset: 0x0003EE70
		// (set) Token: 0x060012EB RID: 4843 RVA: 0x00040C7E File Offset: 0x0003EE7E
		internal bool HasTransientItemLevelErrors
		{
			get
			{
				base.CheckDisposed();
				return this.hasTransientItemLevelErrors;
			}
			set
			{
				base.CheckDisposed();
				this.hasTransientItemLevelErrors = value;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x00040C8D File Offset: 0x0003EE8D
		internal bool WasSyncInterrupted
		{
			get
			{
				base.CheckDisposed();
				return this.syncInterrupted;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x00040C9B File Offset: 0x0003EE9B
		internal ISubscriptionInformationLoader SubscriptionInformationLoader
		{
			get
			{
				base.CheckDisposed();
				return this.subscriptionInformationLoader;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x00040CA9 File Offset: 0x0003EEA9
		public SyncMode SyncMode
		{
			get
			{
				return this.syncMode;
			}
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00040CB1 File Offset: 0x0003EEB1
		public void SwitchToEnumerateChangeMode()
		{
			this.syncMode = SyncMode.EnumerateChangesMode;
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x00040CBA File Offset: 0x0003EEBA
		public bool WasAttemptMadeToOpenMailboxSession
		{
			get
			{
				return this.syncMode == SyncMode.EnumerateChangesMode;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x00040CC5 File Offset: 0x0003EEC5
		public IRemoteServerHealthChecker RemoteServerHealthChecker
		{
			get
			{
				return this.remoteServerHealthChecker;
			}
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00040CCD File Offset: 0x0003EECD
		public void SetUserMailboxSubscription(ISyncWorkerData newSubscription)
		{
			SyncUtilities.ThrowIfArgumentNull("newSubscription", newSubscription);
			this.PreserveNewestDatesFromMailboxServerSubscriptionOn(newSubscription);
			this.userMailboxSubscriptionCachedLastSyncTime = newSubscription.LastSyncTime;
			this.userMailboxSubscriptionStatusBeforeSync = newSubscription.Status;
			this.userMailboxSubscription = newSubscription;
			this.OnSubscriptionLoad(newSubscription);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00040D07 File Offset: 0x0003EF07
		public void SetNativeProvider(NativeSyncStorageProvider nativeSyncStorageProvider)
		{
			SyncUtilities.ThrowIfArgumentNull("nativeSyncStorageProvider", nativeSyncStorageProvider);
			this.nativeProvider = nativeSyncStorageProvider;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00040D1B File Offset: 0x0003EF1B
		public void SetCloudProvider(ISyncStorageProvider cloudSyncStorageProvider)
		{
			SyncUtilities.ThrowIfArgumentNull("cloudSyncStorageProvider", cloudSyncStorageProvider);
			this.cloudProvider = cloudSyncStorageProvider;
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00040D2F File Offset: 0x0003EF2F
		public void SetPreviousSyncProgress(SyncProgress previousSyncProgress)
		{
			if (this.previousSyncProgress != null)
			{
				throw new InvalidOperationException("previousSyncProgress already has a value, but SetPreviousSyncProgress must only be called once for the lifetime of SyncEngineState");
			}
			this.previousSyncProgress = new SyncProgress?(previousSyncProgress);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00040D58 File Offset: 0x0003EF58
		private void PreserveNewestDatesFromMailboxServerSubscriptionOn(ISyncWorkerData newSubscription)
		{
			if (this.mailboxServerSubscription == null || this.mailboxServerSubscription.LastSyncTime == null)
			{
				return;
			}
			if (newSubscription.LastSyncTime == null)
			{
				this.CopyDatesFromExistingSubscriptionInto(newSubscription);
				return;
			}
			if (this.mailboxServerSubscription.LastSyncTime.Value >= newSubscription.LastSyncTime.Value)
			{
				this.CopyDatesFromExistingSubscriptionInto(newSubscription);
			}
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00040DCC File Offset: 0x0003EFCC
		private void CopyDatesFromExistingSubscriptionInto(ISyncWorkerData newSubscription)
		{
			this.syncLogSession.LogDebugging((TSLID)1162UL, "CopyDatesFromExistingSubscriptionInto::LastSyncTime:{0},LastSuccessfulSyncTime:{1},AdjustedLastSuccessfulSyncTime:{2}", new object[]
			{
				this.mailboxServerSubscription.LastSyncTime,
				this.mailboxServerSubscription.LastSuccessfulSyncTime,
				this.mailboxServerSubscription.AdjustedLastSuccessfulSyncTime
			});
			newSubscription.LastSyncTime = this.mailboxServerSubscription.LastSyncTime;
			newSubscription.LastSuccessfulSyncTime = this.mailboxServerSubscription.LastSuccessfulSyncTime;
			newSubscription.AdjustedLastSuccessfulSyncTime = this.mailboxServerSubscription.AdjustedLastSuccessfulSyncTime;
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00040E68 File Offset: 0x0003F068
		public override string ToString()
		{
			base.CheckDisposed();
			return string.Format(CultureInfo.InvariantCulture, "Step: {0}, MoreItemsAvailable: {1}", new object[]
			{
				this.currentStep,
				this.moreItemsAvailable
			});
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00040EAE File Offset: 0x0003F0AE
		internal void SetMoreItemsAvailable()
		{
			base.CheckDisposed();
			this.moreItemsAvailable = true;
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00040EBD File Offset: 0x0003F0BD
		internal void SetTryCancel()
		{
			base.CheckDisposed();
			this.tryCancel = true;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00040ECC File Offset: 0x0003F0CC
		internal void OnRoundtripComplete(object sender, RoundtripCompleteEventArgs roundtripCompleteEventArgs)
		{
			base.CheckDisposed();
			if (roundtripCompleteEventArgs == null)
			{
				throw new ArgumentNullException("roundtripCompleteEventArgs");
			}
			this.connectionStatistics.OnRoundtripComplete(sender, roundtripCompleteEventArgs);
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00040EEF File Offset: 0x0003F0EF
		public Exception CommitStateStorage(bool commitState)
		{
			base.CheckDisposed();
			return this.stateStorage.Commit(commitState, this.SyncMailboxSession.MailboxSession, new EventHandler<RoundtripCompleteEventArgs>(this.OnRoundtripComplete));
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00040F1A File Offset: 0x0003F11A
		internal void SetSyncInterrupted()
		{
			base.CheckDisposed();
			this.syncInterrupted = true;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00040F2C File Offset: 0x0003F12C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.nativeProviderState != null)
				{
					this.nativeProvider.Unbind(this.nativeProviderState);
					this.nativeProviderState.Dispose();
					this.nativeProviderState = null;
				}
				if (this.cloudProviderState != null)
				{
					this.cloudProvider.Unbind(this.cloudProviderState);
					this.cloudProviderState.Dispose();
					this.cloudProviderState = null;
				}
				if (this.stateStorage != null)
				{
					this.stateStorage.Dispose();
					this.stateStorage = null;
				}
				if (this.syncMailboxSession != null)
				{
					this.syncMailboxSession.Dispose();
					this.syncMailboxSession = null;
				}
			}
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00040FC9 File Offset: 0x0003F1C9
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SyncEngineState>(this);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00040FD1 File Offset: 0x0003F1D1
		public void SetCloudProviderState(SyncStorageProviderState newCloudProviderState)
		{
			this.cloudProviderState = newCloudProviderState;
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00040FDA File Offset: 0x0003F1DA
		public void SetNativeProviderState(NativeSyncStorageProviderState newNativeProviderState)
		{
			this.nativeProviderState = newNativeProviderState;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00040FE3 File Offset: 0x0003F1E3
		public void SetStateStorage(IStateStorage newStateStorage)
		{
			SyncUtilities.ThrowIfArgumentNull("newStateStorage", newStateStorage);
			this.stateStorage = newStateStorage;
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00040FF7 File Offset: 0x0003F1F7
		public bool HasConnectedMailboxSession()
		{
			return this.SyncMailboxSession.MailboxSession != null && this.SyncMailboxSession.MailboxSession.IsConnected;
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x00041018 File Offset: 0x0003F218
		public MailSubmitter MailSubmitter
		{
			get
			{
				return this.mailSubmitter;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x00041020 File Offset: 0x0003F220
		public DateTime? UserMailboxSubscriptionCachedLastSyncTime
		{
			get
			{
				return this.userMailboxSubscriptionCachedLastSyncTime;
			}
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00041028 File Offset: 0x0003F228
		internal void SetOrganizationId(OrganizationId newOrganizationId)
		{
			SyncUtilities.ThrowIfArgumentNull("newOrganizationId", newOrganizationId);
			this.organizationId = newOrganizationId;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0004103C File Offset: 0x0003F23C
		internal void SetSubscriptionNotificationSent()
		{
			this.subscriptionNotificationSent = true;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00041045 File Offset: 0x0003F245
		private void OnSubscriptionLoad(ISyncWorkerData subscription)
		{
			this.syncPhaseBeforeSync = subscription.SyncPhase;
		}

		// Token: 0x040009E6 RID: 2534
		private readonly SyncLogSession syncLogSession;

		// Token: 0x040009E7 RID: 2535
		private readonly ExDateTime startSyncTime;

		// Token: 0x040009E8 RID: 2536
		private readonly SyncPoisonStatus subscriptionPoisonStatus;

		// Token: 0x040009E9 RID: 2537
		private readonly SyncHealthData syncHealthData;

		// Token: 0x040009EA RID: 2538
		private readonly bool originalIsUnderRecoveryFlag;

		// Token: 0x040009EB RID: 2539
		private readonly string subscriptionPoisonCallstack;

		// Token: 0x040009EC RID: 2540
		private readonly string legacyDn;

		// Token: 0x040009ED RID: 2541
		private readonly SyncStorageProviderConnectionStatistics connectionStatistics;

		// Token: 0x040009EE RID: 2542
		private readonly object syncRoot = new object();

		// Token: 0x040009EF RID: 2543
		private readonly ISubscriptionInformationLoader subscriptionInformationLoader;

		// Token: 0x040009F0 RID: 2544
		private readonly MailSubmitter mailSubmitter;

		// Token: 0x040009F1 RID: 2545
		private readonly IRemoteServerHealthChecker remoteServerHealthChecker;

		// Token: 0x040009F2 RID: 2546
		private readonly ISyncWorkerData mailboxServerSubscription;

		// Token: 0x040009F3 RID: 2547
		private ISyncWorkerData userMailboxSubscription;

		// Token: 0x040009F4 RID: 2548
		private SyncMailboxSession syncMailboxSession;

		// Token: 0x040009F5 RID: 2549
		private NativeSyncStorageProvider nativeProvider;

		// Token: 0x040009F6 RID: 2550
		private ISyncStorageProvider cloudProvider;

		// Token: 0x040009F7 RID: 2551
		private DateTime? userMailboxSubscriptionCachedLastSyncTime;

		// Token: 0x040009F8 RID: 2552
		private AsyncOperationResult<SyncProviderResultData> lastSyncOperationResult;

		// Token: 0x040009F9 RID: 2553
		private SyncEngineStep currentStep;

		// Token: 0x040009FA RID: 2554
		private SyncEngineStep? continuationSyncStep;

		// Token: 0x040009FB RID: 2555
		private bool underRetry;

		// Token: 0x040009FC RID: 2556
		private NativeSyncStorageProviderState nativeProviderState;

		// Token: 0x040009FD RID: 2557
		private SyncStorageProviderState cloudProviderState;

		// Token: 0x040009FE RID: 2558
		private IStateStorage stateStorage;

		// Token: 0x040009FF RID: 2559
		private bool tryCancel;

		// Token: 0x04000A00 RID: 2560
		private bool moreItemsAvailable;

		// Token: 0x04000A01 RID: 2561
		private int cloudItemsSynced;

		// Token: 0x04000A02 RID: 2562
		private bool hasTransientItemLevelErrors;

		// Token: 0x04000A03 RID: 2563
		private bool syncInterrupted;

		// Token: 0x04000A04 RID: 2564
		private OrganizationId organizationId;

		// Token: 0x04000A05 RID: 2565
		private SyncMode syncMode;

		// Token: 0x04000A06 RID: 2566
		private string updatedSyncWatermark;

		// Token: 0x04000A07 RID: 2567
		private SyncPhase syncPhaseBeforeSync;

		// Token: 0x04000A08 RID: 2568
		private AggregationStatus userMailboxSubscriptionStatusBeforeSync;

		// Token: 0x04000A09 RID: 2569
		private bool subscriptionNotificationSent;

		// Token: 0x04000A0A RID: 2570
		private SyncProgress? previousSyncProgress;
	}
}
