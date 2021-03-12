using System;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000505 RID: 1285
	[Serializable]
	public class SyncServiceInstance : ADConfigurationObject
	{
		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x060038DC RID: 14556 RVA: 0x000DC10A File Offset: 0x000DA30A
		// (set) Token: 0x060038DD RID: 14557 RVA: 0x000DC11C File Offset: 0x000DA31C
		public ADObjectId AccountPartition
		{
			get
			{
				return (ADObjectId)this[SyncServiceInstanceSchema.AccountPartition];
			}
			set
			{
				this[SyncServiceInstanceSchema.AccountPartition] = value;
			}
		}

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x000DC12A File Offset: 0x000DA32A
		// (set) Token: 0x060038DF RID: 14559 RVA: 0x000DC141 File Offset: 0x000DA341
		[Parameter(Mandatory = false)]
		public Version MinVersion
		{
			get
			{
				return SyncServiceInstance.IntToVersion((int)this[SyncServiceInstanceSchema.MinVersion]);
			}
			set
			{
				this[SyncServiceInstanceSchema.MinVersion] = SyncServiceInstance.VersionToInt(value);
			}
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x060038E0 RID: 14560 RVA: 0x000DC159 File Offset: 0x000DA359
		// (set) Token: 0x060038E1 RID: 14561 RVA: 0x000DC170 File Offset: 0x000DA370
		[Parameter(Mandatory = false)]
		public Version MaxVersion
		{
			get
			{
				return SyncServiceInstance.IntToVersion((int)this[SyncServiceInstanceSchema.MaxVersion]);
			}
			set
			{
				this[SyncServiceInstanceSchema.MaxVersion] = SyncServiceInstance.VersionToInt(value);
			}
		}

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x060038E2 RID: 14562 RVA: 0x000DC188 File Offset: 0x000DA388
		// (set) Token: 0x060038E3 RID: 14563 RVA: 0x000DC19A File Offset: 0x000DA39A
		[Parameter(Mandatory = false)]
		public int ActiveInstanceSleepInterval
		{
			get
			{
				return (int)this[SyncServiceInstanceSchema.ActiveInstanceSleepInterval];
			}
			set
			{
				this[SyncServiceInstanceSchema.ActiveInstanceSleepInterval] = value;
			}
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x060038E4 RID: 14564 RVA: 0x000DC1AD File Offset: 0x000DA3AD
		// (set) Token: 0x060038E5 RID: 14565 RVA: 0x000DC1BF File Offset: 0x000DA3BF
		[Parameter(Mandatory = false)]
		public int PassiveInstanceSleepInterval
		{
			get
			{
				return (int)this[SyncServiceInstanceSchema.PassiveInstanceSleepInterval];
			}
			set
			{
				this[SyncServiceInstanceSchema.PassiveInstanceSleepInterval] = value;
			}
		}

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x060038E6 RID: 14566 RVA: 0x000DC1D2 File Offset: 0x000DA3D2
		// (set) Token: 0x060038E7 RID: 14567 RVA: 0x000DC1E4 File Offset: 0x000DA3E4
		[Parameter(Mandatory = false)]
		public bool IsEnabled
		{
			get
			{
				return (bool)this[SyncServiceInstanceSchema.IsEnabled];
			}
			set
			{
				this[SyncServiceInstanceSchema.IsEnabled] = value;
			}
		}

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x060038E8 RID: 14568 RVA: 0x000DC1F7 File Offset: 0x000DA3F7
		// (set) Token: 0x060038E9 RID: 14569 RVA: 0x000DC209 File Offset: 0x000DA409
		[Parameter(Mandatory = false)]
		public bool UseCentralConfig
		{
			get
			{
				return (bool)this[SyncServiceInstanceSchema.UseCentralConfig];
			}
			set
			{
				this[SyncServiceInstanceSchema.UseCentralConfig] = value;
			}
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x060038EA RID: 14570 RVA: 0x000DC21C File Offset: 0x000DA41C
		// (set) Token: 0x060038EB RID: 14571 RVA: 0x000DC22E File Offset: 0x000DA42E
		[Parameter(Mandatory = false)]
		public bool IsHalted
		{
			get
			{
				return (bool)this[SyncServiceInstanceSchema.IsHalted];
			}
			set
			{
				this[SyncServiceInstanceSchema.IsHalted] = value;
			}
		}

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x060038EC RID: 14572 RVA: 0x000DC241 File Offset: 0x000DA441
		// (set) Token: 0x060038ED RID: 14573 RVA: 0x000DC253 File Offset: 0x000DA453
		[Parameter(Mandatory = false)]
		public bool IsHaltRecoveryDisabled
		{
			get
			{
				return (bool)this[SyncServiceInstanceSchema.IsHaltRecoveryDisabled];
			}
			set
			{
				this[SyncServiceInstanceSchema.IsHaltRecoveryDisabled] = value;
			}
		}

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x060038EE RID: 14574 RVA: 0x000DC266 File Offset: 0x000DA466
		// (set) Token: 0x060038EF RID: 14575 RVA: 0x000DC278 File Offset: 0x000DA478
		[Parameter(Mandatory = false)]
		public bool IsMultiObjectCookieEnabled
		{
			get
			{
				return (bool)this[SyncServiceInstanceSchema.IsMultiObjectCookieEnabled];
			}
			set
			{
				this[SyncServiceInstanceSchema.IsMultiObjectCookieEnabled] = value;
			}
		}

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x060038F0 RID: 14576 RVA: 0x000DC28B File Offset: 0x000DA48B
		// (set) Token: 0x060038F1 RID: 14577 RVA: 0x000DC29D File Offset: 0x000DA49D
		[Parameter(Mandatory = false)]
		public bool IsNewCookieBlocked
		{
			get
			{
				return (bool)this[SyncServiceInstanceSchema.IsNewCookieBlocked];
			}
			set
			{
				this[SyncServiceInstanceSchema.IsNewCookieBlocked] = value;
			}
		}

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x060038F2 RID: 14578 RVA: 0x000DC2B0 File Offset: 0x000DA4B0
		// (set) Token: 0x060038F3 RID: 14579 RVA: 0x000DC2C2 File Offset: 0x000DA4C2
		[Parameter(Mandatory = false)]
		public bool IsUsedForTenantToServiceInstanceAssociation
		{
			get
			{
				return (bool)this[SyncServiceInstanceSchema.IsUsedForTenantToServiceInstanceAssociation];
			}
			set
			{
				this[SyncServiceInstanceSchema.IsUsedForTenantToServiceInstanceAssociation] = value;
			}
		}

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x060038F4 RID: 14580 RVA: 0x000DC2D5 File Offset: 0x000DA4D5
		// (set) Token: 0x060038F5 RID: 14581 RVA: 0x000DC2EC File Offset: 0x000DA4EC
		[Parameter(Mandatory = false)]
		public Version NewTenantMinVersion
		{
			get
			{
				return SyncServiceInstance.IntToVersion((int)this[SyncServiceInstanceSchema.NewTenantMinVersion]);
			}
			set
			{
				this[SyncServiceInstanceSchema.NewTenantMinVersion] = SyncServiceInstance.VersionToInt(value);
			}
		}

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x060038F6 RID: 14582 RVA: 0x000DC304 File Offset: 0x000DA504
		// (set) Token: 0x060038F7 RID: 14583 RVA: 0x000DC31B File Offset: 0x000DA51B
		[Parameter(Mandatory = false)]
		public Version NewTenantMaxVersion
		{
			get
			{
				return SyncServiceInstance.IntToVersion((int)this[SyncServiceInstanceSchema.NewTenantMaxVersion]);
			}
			set
			{
				this[SyncServiceInstanceSchema.NewTenantMaxVersion] = SyncServiceInstance.VersionToInt(value);
			}
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x060038F8 RID: 14584 RVA: 0x000DC333 File Offset: 0x000DA533
		// (set) Token: 0x060038F9 RID: 14585 RVA: 0x000DC34A File Offset: 0x000DA54A
		[Parameter(Mandatory = false)]
		public Version TargetServerMinVersion
		{
			get
			{
				return SyncServiceInstance.IntToVersion((int)this[SyncServiceInstanceSchema.TargetServerMinVersion]);
			}
			set
			{
				this[SyncServiceInstanceSchema.TargetServerMinVersion] = SyncServiceInstance.VersionToInt(value);
			}
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x060038FA RID: 14586 RVA: 0x000DC362 File Offset: 0x000DA562
		// (set) Token: 0x060038FB RID: 14587 RVA: 0x000DC379 File Offset: 0x000DA579
		[Parameter(Mandatory = false)]
		public Version TargetServerMaxVersion
		{
			get
			{
				return SyncServiceInstance.IntToVersion((int)this[SyncServiceInstanceSchema.TargetServerMaxVersion]);
			}
			set
			{
				this[SyncServiceInstanceSchema.TargetServerMaxVersion] = SyncServiceInstance.VersionToInt(value);
			}
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x060038FC RID: 14588 RVA: 0x000DC391 File Offset: 0x000DA591
		// (set) Token: 0x060038FD RID: 14589 RVA: 0x000DC3A3 File Offset: 0x000DA5A3
		[Parameter(Mandatory = false)]
		public string ForwardSyncConfigurationXML
		{
			get
			{
				return (string)this[SyncServiceInstanceSchema.ForwardSyncConfigurationXML];
			}
			set
			{
				this[SyncServiceInstanceSchema.ForwardSyncConfigurationXML] = value;
			}
		}

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x060038FE RID: 14590 RVA: 0x000DC3B1 File Offset: 0x000DA5B1
		internal override ADObjectSchema Schema
		{
			get
			{
				return SyncServiceInstance.SchemaObject;
			}
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x060038FF RID: 14591 RVA: 0x000DC3B8 File Offset: 0x000DA5B8
		internal override string MostDerivedObjectClass
		{
			get
			{
				return SyncServiceInstance.MostDerivedClass;
			}
		}

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06003900 RID: 14592 RVA: 0x000DC3BF File Offset: 0x000DA5BF
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x000DC3C6 File Offset: 0x000DA5C6
		internal static ADObjectId GetServiceInstanceObjectId(string serviceInstanceName)
		{
			return SyncServiceInstance.GetMsoSyncRootContainer().GetChildId(serviceInstanceName);
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x000DC3D3 File Offset: 0x000DA5D3
		internal static ADObjectId GetMsoSyncRootContainer()
		{
			return SyncServiceInstance.MsoSyncRootContainer.Value;
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x000DC3DF File Offset: 0x000DA5DF
		internal static ADObjectId GetDivergenceContainerId(ADObjectId serviceInstanceObjectId)
		{
			return serviceInstanceObjectId.GetChildId("Divergence");
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x000DC3EC File Offset: 0x000DA5EC
		private static Version IntToVersion(int value)
		{
			int revision = value & 255;
			int build = value >> 8 & 4095;
			int minor = value >> 20 & 31;
			int major = value >> 25 & 63;
			return new Version(major, minor, build, revision);
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x000DC424 File Offset: 0x000DA624
		private static int VersionToInt(Version value)
		{
			if (value.Major > SyncServiceInstance.MaxSupportedVersion.Major || value.Minor > SyncServiceInstance.MaxSupportedVersion.Minor || value.Build > SyncServiceInstance.MaxSupportedVersion.Build || value.Revision > SyncServiceInstance.MaxSupportedVersion.Revision)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			return (value.Revision & 255) | (value.Build & 4095) << 8 | (value.Minor & 31) << 20 | (value.Major & 63) << 25;
		}

		// Token: 0x040026EF RID: 9967
		private const string DivergenceContainerName = "Divergence";

		// Token: 0x040026F0 RID: 9968
		internal static readonly string MSOSyncRootContainer = "Microsoft Exchange MSO Sync";

		// Token: 0x040026F1 RID: 9969
		internal static readonly string MostDerivedClass = "msExchMSOSyncServiceInstance";

		// Token: 0x040026F2 RID: 9970
		private static readonly SyncServiceInstanceSchema SchemaObject = ObjectSchema.GetInstance<SyncServiceInstanceSchema>();

		// Token: 0x040026F3 RID: 9971
		private static readonly Lazy<ADObjectId> MsoSyncRootContainer = new Lazy<ADObjectId>(delegate()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 265, "MsoSyncRootContainer", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\SyncServiceInstance.cs");
			return topologyConfigurationSession.GetRootDomainNamingContext().GetChildId(SyncServiceInstance.MSOSyncRootContainer);
		}, LazyThreadSafetyMode.PublicationOnly);

		// Token: 0x040026F4 RID: 9972
		internal static readonly Version MaxSupportedVersion = new Version(63, 31, 4095, 255);
	}
}
