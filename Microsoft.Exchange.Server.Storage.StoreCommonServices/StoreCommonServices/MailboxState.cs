using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000B5 RID: 181
	public class MailboxState : MailboxLockNameBase, INotificationSubscriptionList
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x000241F4 File Offset: 0x000223F4
		public MailboxState(MailboxStateCache mailboxStateCache, int mailboxNumber, int mailboxPartitionNumber, Guid mailboxGuid, Guid mailboxInstanceGuid, MailboxStatus status, ulong globalIdLowWatermark, ulong globalCnLowWatermark, bool countersAlreadyPatched, bool quarantined, DateTime lastMailboxMaintenanceTime, DateTime lastQuotaCheckTime, TenantHint tenantHint, MailboxInfo.MailboxType mailboxType, MailboxInfo.MailboxTypeDetail mailboxTypeDetail) : base(mailboxStateCache.Database.MdbGuid, mailboxPartitionNumber)
		{
			this.mailboxStateCache = mailboxStateCache;
			this.mailboxNumber = mailboxNumber;
			this.mailboxGuid = mailboxGuid;
			this.mailboxInstanceGuid = mailboxInstanceGuid;
			this.status = status;
			this.globalIdLowWatermark = globalIdLowWatermark;
			this.globalCnLowWatermark = globalCnLowWatermark;
			this.countersAlreadyPatched = countersAlreadyPatched;
			this.quarantined = quarantined;
			this.digestCollector = mailboxStateCache.Database.ResourceDigest.CreateDigestCollector(mailboxGuid, mailboxNumber);
			this.tenantHint = tenantHint;
			this.mailboxType = mailboxType;
			this.mailboxTypeDetail = mailboxTypeDetail;
			this.lastMailboxMaintenanceTime = lastMailboxMaintenanceTime;
			this.lastQuotaCheckTime = lastQuotaCheckTime;
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x000242A5 File Offset: 0x000224A5
		public int MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x000242AD File Offset: 0x000224AD
		public bool SupportsPerUserFeatures
		{
			get
			{
				return this.mailboxTypeDetail == MailboxInfo.MailboxTypeDetail.TeamMailbox || this.mailboxType == MailboxInfo.MailboxType.PublicFolderPrimary || this.mailboxType == MailboxInfo.MailboxType.PublicFolderSecondary;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x000242CC File Offset: 0x000224CC
		public bool IsValid
		{
			get
			{
				return this.status != MailboxStatus.Invalid;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x000242DA File Offset: 0x000224DA
		public bool IsNew
		{
			get
			{
				return this.status == MailboxStatus.New;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x000242E5 File Offset: 0x000224E5
		public bool IsUserAccessible
		{
			get
			{
				return this.status == MailboxStatus.UserAccessible;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x000242F0 File Offset: 0x000224F0
		public bool IsDisabled
		{
			get
			{
				return this.status == MailboxStatus.Disabled;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x000242FB File Offset: 0x000224FB
		public bool IsSoftDeleted
		{
			get
			{
				return this.status == MailboxStatus.SoftDeleted;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00024306 File Offset: 0x00022506
		public bool IsDisconnected
		{
			get
			{
				return this.IsDisabled || this.IsSoftDeleted;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00024318 File Offset: 0x00022518
		public bool IsAccessible
		{
			get
			{
				return this.IsUserAccessible || this.IsDisconnected;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0002432A File Offset: 0x0002252A
		public bool IsHardDeleted
		{
			get
			{
				return this.status == MailboxStatus.HardDeleted;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00024335 File Offset: 0x00022535
		public bool IsTombstone
		{
			get
			{
				return this.status == MailboxStatus.Tombstone;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00024340 File Offset: 0x00022540
		public bool IsRemoved
		{
			get
			{
				return this.IsHardDeleted || this.IsTombstone;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00024352 File Offset: 0x00022552
		public bool IsCurrent
		{
			get
			{
				return this.IsAccessible || this.IsRemoved;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00024364 File Offset: 0x00022564
		public bool IsDeleted
		{
			get
			{
				return this.IsSoftDeleted || this.IsRemoved;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00024376 File Offset: 0x00022576
		public DateTime InvalidatedTime
		{
			get
			{
				return this.invalidatedTime;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0002437E File Offset: 0x0002257E
		public MailboxStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00024386 File Offset: 0x00022586
		public StoreDatabase Database
		{
			get
			{
				return this.MailboxStateCache.Database;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00024393 File Offset: 0x00022593
		public override Guid DatabaseGuid
		{
			get
			{
				return this.MailboxStateCache.Database.MdbGuid;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x000243A5 File Offset: 0x000225A5
		public MailboxStateCache MailboxStateCache
		{
			get
			{
				return this.mailboxStateCache;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x000243AD File Offset: 0x000225AD
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x000243B5 File Offset: 0x000225B5
		public bool IsPublicFolderMailbox
		{
			get
			{
				return this.MailboxType == MailboxInfo.MailboxType.PublicFolderPrimary || this.MailboxType == MailboxInfo.MailboxType.PublicFolderSecondary;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x000243CB File Offset: 0x000225CB
		public bool IsGroupMailbox
		{
			get
			{
				return this.MailboxTypeDetail == MailboxInfo.MailboxTypeDetail.GroupMailbox;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x000243D6 File Offset: 0x000225D6
		public bool IsNewMailboxPartition
		{
			get
			{
				return this.unifiedMailboxState != null && this.IsNew && this.MailboxNumber == base.MailboxPartitionNumber;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x000243F8 File Offset: 0x000225F8
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x00024400 File Offset: 0x00022600
		public Guid MailboxInstanceGuid
		{
			get
			{
				return this.mailboxInstanceGuid;
			}
			set
			{
				this.mailboxInstanceGuid = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x00024409 File Offset: 0x00022609
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x00024425 File Offset: 0x00022625
		public override LockManager.NamedLockObject CachedLockObject
		{
			get
			{
				if (this.unifiedMailboxState != null)
				{
					return this.unifiedMailboxState.CachedLockObject;
				}
				return base.CachedLockObject;
			}
			set
			{
				if (this.unifiedMailboxState != null)
				{
					this.unifiedMailboxState.CachedLockObject = value;
					return;
				}
				base.CachedLockObject = value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00024443 File Offset: 0x00022643
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0002445F File Offset: 0x0002265F
		public ulong GlobalIdLowWatermark
		{
			get
			{
				if (this.unifiedMailboxState != null)
				{
					return this.unifiedMailboxState.GlobalIdLowWatermark;
				}
				return this.globalIdLowWatermark;
			}
			set
			{
				if (this.unifiedMailboxState != null)
				{
					this.unifiedMailboxState.GlobalIdLowWatermark = value;
					return;
				}
				this.globalIdLowWatermark = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0002447D File Offset: 0x0002267D
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x00024499 File Offset: 0x00022699
		public ulong GlobalCnLowWatermark
		{
			get
			{
				if (this.unifiedMailboxState != null)
				{
					return this.unifiedMailboxState.GlobalCnLowWatermark;
				}
				return this.globalCnLowWatermark;
			}
			set
			{
				if (this.unifiedMailboxState != null)
				{
					this.unifiedMailboxState.GlobalCnLowWatermark = value;
					return;
				}
				this.globalCnLowWatermark = value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x000244B7 File Offset: 0x000226B7
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x000244D3 File Offset: 0x000226D3
		public bool CountersAlreadyPatched
		{
			get
			{
				if (this.unifiedMailboxState != null)
				{
					return this.unifiedMailboxState.CountersAlreadyPatched;
				}
				return this.countersAlreadyPatched;
			}
			set
			{
				if (this.unifiedMailboxState != null)
				{
					this.unifiedMailboxState.CountersAlreadyPatched = value;
					return;
				}
				this.countersAlreadyPatched = value;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x000244F4 File Offset: 0x000226F4
		public DateTime UtcNow
		{
			get
			{
				DeterministicTime deterministicTime = (DeterministicTime)this.GetComponentData(MailboxState.deterministicTimeSlot);
				if (deterministicTime == null)
				{
					deterministicTime = new DeterministicTime();
					DeterministicTime deterministicTime2 = (DeterministicTime)this.CompareExchangeComponentData(MailboxState.deterministicTimeSlot, null, deterministicTime);
					if (deterministicTime2 != null)
					{
						deterministicTime = deterministicTime2;
					}
				}
				DateTime utcNow;
				using (LockManager.Lock(deterministicTime))
				{
					utcNow = deterministicTime.UtcNow;
				}
				return utcNow;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00024564 File Offset: 0x00022764
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x0002456C File Offset: 0x0002276C
		public bool Quarantined
		{
			get
			{
				return this.quarantined;
			}
			set
			{
				this.quarantined = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x00024575 File Offset: 0x00022775
		public IDigestCollector ActivityDigestCollector
		{
			get
			{
				return this.digestCollector;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0002457D File Offset: 0x0002277D
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x00024585 File Offset: 0x00022785
		public TenantHint TenantHint
		{
			get
			{
				return this.tenantHint;
			}
			set
			{
				this.tenantHint = value;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0002458E File Offset: 0x0002278E
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x00024596 File Offset: 0x00022796
		public MailboxInfo.MailboxType MailboxType
		{
			get
			{
				return this.mailboxType;
			}
			set
			{
				this.mailboxType = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0002459F File Offset: 0x0002279F
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x000245A7 File Offset: 0x000227A7
		public MailboxInfo.MailboxTypeDetail MailboxTypeDetail
		{
			get
			{
				return this.mailboxTypeDetail;
			}
			set
			{
				this.mailboxTypeDetail = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x000245B0 File Offset: 0x000227B0
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x000245D4 File Offset: 0x000227D4
		public DateTime LastUpdatedActiveTime
		{
			get
			{
				MailboxState.ActiveMailboxData activeMailboxDataNoCreate = this.GetActiveMailboxDataNoCreate();
				if (activeMailboxDataNoCreate == null)
				{
					return DateTime.MinValue;
				}
				return activeMailboxDataNoCreate.LastUpdatedActiveTime;
			}
			set
			{
				MailboxState.ActiveMailboxData activeMailboxData = this.GetActiveMailboxData();
				activeMailboxData.LastUpdatedActiveTime = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x000245EF File Offset: 0x000227EF
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x000245F7 File Offset: 0x000227F7
		public DateTime LastQuotaCheckTime
		{
			get
			{
				return this.lastQuotaCheckTime;
			}
			set
			{
				this.lastQuotaCheckTime = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x00024600 File Offset: 0x00022800
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x00024608 File Offset: 0x00022808
		public DateTime LastMailboxMaintenanceTime
		{
			get
			{
				return this.lastMailboxMaintenanceTime;
			}
			set
			{
				this.lastMailboxMaintenanceTime = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00024614 File Offset: 0x00022814
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x00024634 File Offset: 0x00022834
		public ICache PerUserCache
		{
			get
			{
				MailboxState.ActiveMailboxData activeMailboxDataNoCreate = this.GetActiveMailboxDataNoCreate();
				if (activeMailboxDataNoCreate == null)
				{
					return null;
				}
				return activeMailboxDataNoCreate.PerUserCache;
			}
			set
			{
				MailboxState.ActiveMailboxData activeMailboxData = this.GetActiveMailboxData();
				activeMailboxData.PerUserCache = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0002464F File Offset: 0x0002284F
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x00024657 File Offset: 0x00022857
		internal bool CurrentlyActive
		{
			get
			{
				return this.currentlyActive;
			}
			set
			{
				this.currentlyActive = value;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00024660 File Offset: 0x00022860
		internal bool HasComponentDataForTest
		{
			get
			{
				return !this.componentData.IsEmpty && (this.unifiedMailboxState == null || !this.unifiedMailboxState.HasComponentDataForTest);
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00024689 File Offset: 0x00022889
		internal MailboxState.UnifiedMailboxState UnifiedState
		{
			get
			{
				return this.unifiedMailboxState;
			}
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00024694 File Offset: 0x00022894
		public static void GetMailboxLock(Guid databaseGuid, int mailboxPartitionNumber, bool shared, ILockStatistics lockStats)
		{
			IMailboxLockName mailboxLockName = MailboxLockNameBase.GetMailboxLockName(databaseGuid, mailboxPartitionNumber);
			MailboxState.GetMailboxLock(mailboxLockName, shared, lockStats);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x000246B4 File Offset: 0x000228B4
		public static bool TryGetMailboxLock(Guid databaseGuid, int mailboxPartitionNumber, bool shared, TimeSpan timeout, ILockStatistics lockStats)
		{
			IMailboxLockName mailboxLockName = MailboxLockNameBase.GetMailboxLockName(databaseGuid, mailboxPartitionNumber);
			return MailboxState.TryGetMailboxLock(mailboxLockName, shared, timeout, lockStats);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x000246D4 File Offset: 0x000228D4
		public static void ReleaseMailboxLock(Guid databaseGuid, int mailboxPartitionNumber, bool shared)
		{
			IMailboxLockName mailboxLockName = MailboxLockNameBase.GetMailboxLockName(databaseGuid, mailboxPartitionNumber);
			MailboxState.ReleaseMailboxLock(mailboxLockName, shared);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000246F0 File Offset: 0x000228F0
		public static int AllocateComponentDataSlot(bool privateSlot)
		{
			return MailboxState.ComponentDataStorage.AllocateSlot(privateSlot);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000246F8 File Offset: 0x000228F8
		public override ILockName GetLockNameToCache()
		{
			return MailboxLockNameBase.GetMailboxLockName(this.DatabaseGuid, base.MailboxPartitionNumber);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0002470C File Offset: 0x0002290C
		public override string GetFriendlyNameForLogging()
		{
			if (this.UnifiedState == null)
			{
				return this.MailboxGuid.ToString();
			}
			return this.UnifiedState.GetFriendlyNameForLogging() + this.MailboxGuid.ToString();
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0002475A File Offset: 0x0002295A
		public void InvalidateLogons()
		{
			this.invalidatedTime = this.UtcNow;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00024768 File Offset: 0x00022968
		public void GetMailboxLock(bool shared, ILockStatistics lockStats)
		{
			MailboxState.GetMailboxLock(this, shared, lockStats);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00024772 File Offset: 0x00022972
		public bool TryGetMailboxLock(bool shared, TimeSpan timeout, ILockStatistics lockStats)
		{
			return MailboxState.TryGetMailboxLock(this, shared, timeout, lockStats);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0002477D File Offset: 0x0002297D
		public void ReleaseMailboxLock(bool shared)
		{
			MailboxState.ReleaseMailboxLock(this, shared);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00024788 File Offset: 0x00022988
		public void AddReference()
		{
			using (LockManager.Lock(this))
			{
				this.referenceCount++;
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000247CC File Offset: 0x000229CC
		public void ReleaseReference()
		{
			using (LockManager.Lock(this))
			{
				this.referenceCount--;
				if (this.referenceCount == 0)
				{
					MailboxState.NotificationSubscriptions notificationSubscriptions = (MailboxState.NotificationSubscriptions)this.componentData[MailboxState.mailboxSubscriptionListSlot];
					this.componentData[MailboxState.mailboxSubscriptionListSlot] = null;
					this.componentData[MailboxState.deterministicTimeSlot] = null;
				}
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00024850 File Offset: 0x00022A50
		public void DangerousReleaseReference()
		{
			using (LockManager.Lock(this))
			{
				this.referenceCount--;
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00024894 File Offset: 0x00022A94
		public void RegisterSubscription(NotificationSubscription subscription)
		{
			MailboxState.NotificationSubscriptions notificationSubscriptions = (MailboxState.NotificationSubscriptions)this.GetComponentData(MailboxState.mailboxSubscriptionListSlot);
			if (notificationSubscriptions == null)
			{
				notificationSubscriptions = new MailboxState.NotificationSubscriptions(10);
				MailboxState.NotificationSubscriptions notificationSubscriptions2 = (MailboxState.NotificationSubscriptions)this.CompareExchangeComponentData(MailboxState.mailboxSubscriptionListSlot, null, notificationSubscriptions);
				if (notificationSubscriptions2 != null)
				{
					notificationSubscriptions = notificationSubscriptions2;
				}
			}
			using (LockManager.Lock(this))
			{
				notificationSubscriptions.Add(subscription);
			}
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00024904 File Offset: 0x00022B04
		public void UnregisterSubscription(NotificationSubscription subscription)
		{
			MailboxState.NotificationSubscriptions notificationSubscriptions = (MailboxState.NotificationSubscriptions)this.GetComponentData(MailboxState.mailboxSubscriptionListSlot);
			using (LockManager.Lock(this))
			{
				if (notificationSubscriptions != null)
				{
					notificationSubscriptions.Remove(subscription);
				}
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00024954 File Offset: 0x00022B54
		public void EnumerateSubscriptionsForEvent(NotificationPublishPhase phase, Context transactionContext, NotificationEvent nev, SubscriptionEnumerationCallback callback)
		{
			NotificationSubscription[] array = null;
			using (LockManager.Lock(this, transactionContext.Diagnostics))
			{
				MailboxState.NotificationSubscriptions notificationSubscriptions = (MailboxState.NotificationSubscriptions)this.GetComponentData(MailboxState.mailboxSubscriptionListSlot);
				if (notificationSubscriptions != null)
				{
					array = notificationSubscriptions.ToArray();
				}
			}
			if (this.referenceCount > 0 && array != null)
			{
				foreach (NotificationSubscription notificationSubscription in array)
				{
					if ((notificationSubscription.EventTypeValueMask & nev.EventTypeValue) != 0 && (notificationSubscription.Kind & (SubscriptionKind)phase) != (SubscriptionKind)0U)
					{
						if (ExTraceGlobals.NotificationTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.NotificationTracer.TraceDebug<NotificationEvent>(30652L, "MbxStateNotifEnumeration: {0}", nev);
						}
						callback(phase, transactionContext, notificationSubscription, nev);
					}
					else if (ExTraceGlobals.NotificationTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.NotificationTracer.TraceDebug<NotificationEvent, NotificationSubscription>(35961L, "MbxStateNotifEnumeration: skipping callback for {0}, {1}", nev, notificationSubscription);
					}
				}
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00024A4C File Offset: 0x00022C4C
		public void SetUserAccessible()
		{
			Globals.AssertRetail(this.status == MailboxStatus.New || this.status == MailboxStatus.Disabled, "unexpected mailbox status");
			this.status = MailboxStatus.UserAccessible;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00024A74 File Offset: 0x00022C74
		public void SetTombstoned()
		{
			Globals.AssertRetail(this.status == MailboxStatus.HardDeleted, "unexpected mailbox status");
			this.status = MailboxStatus.Tombstone;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00024A90 File Offset: 0x00022C90
		public object GetComponentData(int slotNumber)
		{
			if (this.unifiedMailboxState != null && !MailboxState.ComponentDataStorage.IsPrivateSlot(slotNumber))
			{
				return this.unifiedMailboxState.GetComponentData(slotNumber);
			}
			return this.componentData[slotNumber];
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00024ABC File Offset: 0x00022CBC
		public void SetComponentData(int slotNumber, object value)
		{
			if (this.unifiedMailboxState != null && !MailboxState.ComponentDataStorage.IsPrivateSlot(slotNumber))
			{
				this.unifiedMailboxState.SetComponentData(slotNumber, value);
			}
			else
			{
				using (LockManager.Lock(this))
				{
					this.componentData[slotNumber] = value;
				}
			}
			if (value != null && !this.CurrentlyActive)
			{
				MailboxStateCache.OnMailboxActivity(this);
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00024B30 File Offset: 0x00022D30
		public object CompareExchangeComponentData(int slotNumber, object comparand, object value)
		{
			object obj;
			if (this.unifiedMailboxState != null && !MailboxState.ComponentDataStorage.IsPrivateSlot(slotNumber))
			{
				obj = this.unifiedMailboxState.CompareExchangeComponentData(slotNumber, comparand, value);
			}
			else
			{
				obj = this.componentData.CompareExchange(slotNumber, comparand, value);
			}
			bool flag = object.ReferenceEquals(obj, comparand);
			if (flag && value != null && !this.CurrentlyActive)
			{
				MailboxStateCache.OnMailboxActivity(this);
			}
			return obj;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00024B8E File Offset: 0x00022D8E
		public void CleanupAsNonActive(Context context)
		{
			this.CleanupAsNonActive(context, true);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00024B98 File Offset: 0x00022D98
		public void CleanupAsNonActive(Context context, bool doFlush)
		{
			if (this.PerUserCache != null)
			{
				if (doFlush && this.PerUserCache.FlushAllDirtyEntries(context))
				{
					context.Commit();
				}
				this.PerUserCache = null;
			}
			this.componentData.CleanupDataSlots(context);
			if (this.unifiedMailboxState != null)
			{
				this.unifiedMailboxState.CleanupAsNonActive(context, doFlush);
			}
			this.CachedLockObject = null;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00024BF4 File Offset: 0x00022DF4
		public bool CleanupDataSlots(Context context)
		{
			this.componentData.CleanupDataSlots(context);
			bool flag = true;
			if (this.unifiedMailboxState != null)
			{
				flag = this.unifiedMailboxState.CleanupDataSlots(context);
			}
			return this.componentData.IsEmpty && flag;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00024C34 File Offset: 0x00022E34
		internal static void Initialize()
		{
			if (MailboxState.mailboxSubscriptionListSlot == -1)
			{
				MailboxState.mailboxSubscriptionListSlot = MailboxState.AllocateComponentDataSlot(true);
			}
			if (MailboxState.deterministicTimeSlot == -1)
			{
				MailboxState.deterministicTimeSlot = MailboxState.AllocateComponentDataSlot(true);
			}
			if (MailboxState.activeMailboxDataSlot == -1)
			{
				MailboxState.activeMailboxDataSlot = MailboxState.AllocateComponentDataSlot(true);
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00024C6F File Offset: 0x00022E6F
		internal void Invalidate(Context context)
		{
			this.componentData.CleanupDataSlots(context);
			if (this.unifiedMailboxState != null)
			{
				this.unifiedMailboxState.CleanupDataSlots(context);
			}
			this.status = MailboxStatus.Invalid;
			this.mailboxStateCache.AddToPostDisposeCleanupList(this);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00024CA5 File Offset: 0x00022EA5
		internal void SetUnifiedMailboxState(MailboxState.UnifiedMailboxState unifiedMailboxState)
		{
			this.unifiedMailboxState = unifiedMailboxState;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00024CAE File Offset: 0x00022EAE
		private static void GetMailboxLock(IMailboxLockName mailboxLockName, bool shared, ILockStatistics lockStats)
		{
			MailboxState.TryGetMailboxLock(mailboxLockName, shared, LockManager.InfiniteTimeout, lockStats);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00024CC0 File Offset: 0x00022EC0
		private static bool TryGetMailboxLock(IMailboxLockName mailboxLockName, bool shared, TimeSpan timeout, ILockStatistics lockStats)
		{
			LockManager.LockType lockType = shared ? LockManager.LockType.MailboxShared : LockManager.LockType.MailboxExclusive;
			return LockManager.TryGetLock(mailboxLockName, lockType, timeout, lockStats);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00024CE0 File Offset: 0x00022EE0
		private static void ReleaseMailboxLock(IMailboxLockName mailboxLockName, bool shared)
		{
			LockManager.LockType lockType = shared ? LockManager.LockType.MailboxShared : LockManager.LockType.MailboxExclusive;
			LockManager.ReleaseLock(mailboxLockName, lockType);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00024CFE File Offset: 0x00022EFE
		private MailboxState.ActiveMailboxData GetActiveMailboxDataNoCreate()
		{
			return (MailboxState.ActiveMailboxData)this.GetComponentData(MailboxState.activeMailboxDataSlot);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00024D10 File Offset: 0x00022F10
		private MailboxState.ActiveMailboxData GetActiveMailboxData()
		{
			MailboxState.ActiveMailboxData activeMailboxData = this.GetActiveMailboxDataNoCreate();
			if (activeMailboxData == null)
			{
				activeMailboxData = new MailboxState.ActiveMailboxData(this);
				this.SetComponentData(MailboxState.activeMailboxDataSlot, activeMailboxData);
			}
			return activeMailboxData;
		}

		// Token: 0x0400043F RID: 1087
		public const int PartitionScopeMailboxNumber = -1;

		// Token: 0x04000440 RID: 1088
		private const int AvgNotificationSubscriptionsPerMailbox = 10;

		// Token: 0x04000441 RID: 1089
		private static int mailboxSubscriptionListSlot = -1;

		// Token: 0x04000442 RID: 1090
		private static int deterministicTimeSlot = -1;

		// Token: 0x04000443 RID: 1091
		private static int activeMailboxDataSlot = -1;

		// Token: 0x04000444 RID: 1092
		private readonly MailboxStateCache mailboxStateCache;

		// Token: 0x04000445 RID: 1093
		private readonly Guid mailboxGuid;

		// Token: 0x04000446 RID: 1094
		private readonly int mailboxNumber;

		// Token: 0x04000447 RID: 1095
		private DateTime invalidatedTime;

		// Token: 0x04000448 RID: 1096
		private DateTime lastMailboxMaintenanceTime;

		// Token: 0x04000449 RID: 1097
		private DateTime lastQuotaCheckTime;

		// Token: 0x0400044A RID: 1098
		private Guid mailboxInstanceGuid;

		// Token: 0x0400044B RID: 1099
		private MailboxState.ComponentDataStorage componentData = new MailboxState.ComponentDataStorage();

		// Token: 0x0400044C RID: 1100
		private ulong globalIdLowWatermark;

		// Token: 0x0400044D RID: 1101
		private ulong globalCnLowWatermark;

		// Token: 0x0400044E RID: 1102
		private bool countersAlreadyPatched;

		// Token: 0x0400044F RID: 1103
		private bool quarantined;

		// Token: 0x04000450 RID: 1104
		private bool currentlyActive;

		// Token: 0x04000451 RID: 1105
		private MailboxInfo.MailboxType mailboxType;

		// Token: 0x04000452 RID: 1106
		private MailboxInfo.MailboxTypeDetail mailboxTypeDetail;

		// Token: 0x04000453 RID: 1107
		private MailboxStatus status;

		// Token: 0x04000454 RID: 1108
		private int referenceCount;

		// Token: 0x04000455 RID: 1109
		private IDigestCollector digestCollector;

		// Token: 0x04000456 RID: 1110
		private TenantHint tenantHint;

		// Token: 0x04000457 RID: 1111
		private MailboxState.UnifiedMailboxState unifiedMailboxState;

		// Token: 0x020000B6 RID: 182
		internal class UnifiedMailboxState : MailboxLockNameBase
		{
			// Token: 0x06000766 RID: 1894 RVA: 0x00024D50 File Offset: 0x00022F50
			public UnifiedMailboxState(MailboxStateCache mailboxStateCache, Guid unifiedMailboxGuid, int mailboxPartitionNumber, ulong globalIdLowWatermark, ulong globalCnLowWatermark, bool countersAlreadyPatched) : base(mailboxStateCache.Database.MdbGuid, mailboxPartitionNumber)
			{
				this.mailboxStateCache = mailboxStateCache;
				this.unifiedMailboxGuid = unifiedMailboxGuid;
				this.globalIdLowWatermark = globalIdLowWatermark;
				this.globalCnLowWatermark = globalCnLowWatermark;
				this.countersAlreadyPatched = countersAlreadyPatched;
			}

			// Token: 0x170001D5 RID: 469
			// (get) Token: 0x06000767 RID: 1895 RVA: 0x00024DA0 File Offset: 0x00022FA0
			public Guid UnifiedMailboxGuid
			{
				get
				{
					return this.unifiedMailboxGuid;
				}
			}

			// Token: 0x170001D6 RID: 470
			// (get) Token: 0x06000768 RID: 1896 RVA: 0x00024DA8 File Offset: 0x00022FA8
			public override Guid DatabaseGuid
			{
				get
				{
					return this.mailboxStateCache.Database.MdbGuid;
				}
			}

			// Token: 0x170001D7 RID: 471
			// (get) Token: 0x06000769 RID: 1897 RVA: 0x00024DBA File Offset: 0x00022FBA
			// (set) Token: 0x0600076A RID: 1898 RVA: 0x00024DC2 File Offset: 0x00022FC2
			public ulong GlobalIdLowWatermark
			{
				get
				{
					return this.globalIdLowWatermark;
				}
				set
				{
					this.globalIdLowWatermark = value;
				}
			}

			// Token: 0x170001D8 RID: 472
			// (get) Token: 0x0600076B RID: 1899 RVA: 0x00024DCB File Offset: 0x00022FCB
			// (set) Token: 0x0600076C RID: 1900 RVA: 0x00024DD3 File Offset: 0x00022FD3
			public ulong GlobalCnLowWatermark
			{
				get
				{
					return this.globalCnLowWatermark;
				}
				set
				{
					this.globalCnLowWatermark = value;
				}
			}

			// Token: 0x170001D9 RID: 473
			// (get) Token: 0x0600076D RID: 1901 RVA: 0x00024DDC File Offset: 0x00022FDC
			// (set) Token: 0x0600076E RID: 1902 RVA: 0x00024DE4 File Offset: 0x00022FE4
			public bool CountersAlreadyPatched
			{
				get
				{
					return this.countersAlreadyPatched;
				}
				set
				{
					this.countersAlreadyPatched = value;
				}
			}

			// Token: 0x0600076F RID: 1903 RVA: 0x00024DED File Offset: 0x00022FED
			public void SetNewUnfiedMailboxGuid(Guid newUnifiedMailboxGuid)
			{
				this.unifiedMailboxGuid = newUnifiedMailboxGuid;
			}

			// Token: 0x06000770 RID: 1904 RVA: 0x00024DF6 File Offset: 0x00022FF6
			public override ILockName GetLockNameToCache()
			{
				return MailboxLockNameBase.GetMailboxLockName(this.DatabaseGuid, base.MailboxPartitionNumber);
			}

			// Token: 0x06000771 RID: 1905 RVA: 0x00024E09 File Offset: 0x00023009
			public override string GetFriendlyNameForLogging()
			{
				return string.Format("[{0}]", this.unifiedMailboxGuid);
			}

			// Token: 0x06000772 RID: 1906 RVA: 0x00024E20 File Offset: 0x00023020
			public object GetComponentData(int slotNumber)
			{
				return this.componentData[slotNumber];
			}

			// Token: 0x06000773 RID: 1907 RVA: 0x00024E30 File Offset: 0x00023030
			public void SetComponentData(int slotNumber, object value)
			{
				using (LockManager.Lock(this))
				{
					this.componentData[slotNumber] = value;
				}
			}

			// Token: 0x06000774 RID: 1908 RVA: 0x00024E74 File Offset: 0x00023074
			public object CompareExchangeComponentData(int slotNumber, object comparand, object value)
			{
				return this.componentData.CompareExchange(slotNumber, comparand, value);
			}

			// Token: 0x06000775 RID: 1909 RVA: 0x00024E84 File Offset: 0x00023084
			public void CleanupAsNonActive(Context context)
			{
				this.CleanupAsNonActive(context, true);
			}

			// Token: 0x06000776 RID: 1910 RVA: 0x00024E8E File Offset: 0x0002308E
			public void CleanupAsNonActive(Context context, bool doFlush)
			{
				this.componentData.CleanupDataSlots(context);
				this.CachedLockObject = null;
			}

			// Token: 0x06000777 RID: 1911 RVA: 0x00024EA3 File Offset: 0x000230A3
			public bool CleanupDataSlots(Context context)
			{
				this.componentData.CleanupDataSlots(context);
				return this.componentData.IsEmpty;
			}

			// Token: 0x170001DA RID: 474
			// (get) Token: 0x06000778 RID: 1912 RVA: 0x00024EBC File Offset: 0x000230BC
			internal bool HasComponentDataForTest
			{
				get
				{
					return !this.componentData.IsEmpty;
				}
			}

			// Token: 0x04000458 RID: 1112
			private readonly MailboxStateCache mailboxStateCache;

			// Token: 0x04000459 RID: 1113
			private Guid unifiedMailboxGuid;

			// Token: 0x0400045A RID: 1114
			private MailboxState.ComponentDataStorage componentData = new MailboxState.ComponentDataStorage();

			// Token: 0x0400045B RID: 1115
			private ulong globalIdLowWatermark;

			// Token: 0x0400045C RID: 1116
			private ulong globalCnLowWatermark;

			// Token: 0x0400045D RID: 1117
			private bool countersAlreadyPatched;
		}

		// Token: 0x020000B7 RID: 183
		private class ComponentDataStorage : ComponentDataStorageBase
		{
			// Token: 0x06000779 RID: 1913 RVA: 0x00024ECC File Offset: 0x000230CC
			internal static bool IsPrivateSlot(int slotNumber)
			{
				return MailboxState.ComponentDataStorage.slotsRegistration[slotNumber];
			}

			// Token: 0x0600077A RID: 1914 RVA: 0x00024ED9 File Offset: 0x000230D9
			internal static int AllocateSlot(bool privateSlot)
			{
				MailboxState.ComponentDataStorage.slotsRegistration.Add(privateSlot);
				return MailboxState.ComponentDataStorage.slotsRegistration.Count - 1;
			}

			// Token: 0x170001DB RID: 475
			// (get) Token: 0x0600077B RID: 1915 RVA: 0x00024EF2 File Offset: 0x000230F2
			internal override int SlotCount
			{
				get
				{
					return MailboxState.ComponentDataStorage.slotsRegistration.Count;
				}
			}

			// Token: 0x0400045E RID: 1118
			private static List<bool> slotsRegistration = new List<bool>();
		}

		// Token: 0x020000B8 RID: 184
		private class ActiveMailboxData
		{
			// Token: 0x0600077E RID: 1918 RVA: 0x00024F12 File Offset: 0x00023112
			internal ActiveMailboxData(MailboxState mailboxState)
			{
			}

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x0600077F RID: 1919 RVA: 0x00024F1A File Offset: 0x0002311A
			// (set) Token: 0x06000780 RID: 1920 RVA: 0x00024F22 File Offset: 0x00023122
			internal DateTime LastUpdatedActiveTime
			{
				get
				{
					return this.lastUpdatedActiveTime;
				}
				set
				{
					this.lastUpdatedActiveTime = value;
				}
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x06000781 RID: 1921 RVA: 0x00024F2B File Offset: 0x0002312B
			// (set) Token: 0x06000782 RID: 1922 RVA: 0x00024F33 File Offset: 0x00023133
			internal ICache PerUserCache
			{
				get
				{
					return this.perUserCache;
				}
				set
				{
					if (value != null)
					{
						Interlocked.CompareExchange<ICache>(ref this.perUserCache, value, null);
						return;
					}
					this.perUserCache = null;
				}
			}

			// Token: 0x0400045F RID: 1119
			private DateTime lastUpdatedActiveTime;

			// Token: 0x04000460 RID: 1120
			private ICache perUserCache;
		}

		// Token: 0x020000B9 RID: 185
		private class NotificationSubscriptions : List<NotificationSubscription>, IComponentData
		{
			// Token: 0x06000783 RID: 1923 RVA: 0x00024F4E File Offset: 0x0002314E
			internal NotificationSubscriptions(int initialCapacity) : base(initialCapacity)
			{
			}

			// Token: 0x06000784 RID: 1924 RVA: 0x00024F57 File Offset: 0x00023157
			bool IComponentData.DoCleanup(Context context)
			{
				return base.Count == 0;
			}
		}
	}
}
