using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.MailboxSignature;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200014B RID: 331
	internal abstract class MailboxProviderBase : DisposeTrackableBase, IMailbox, IDisposable, ISettingsContextProvider
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x000158E8 File Offset: 0x00013AE8
		public MailboxProviderBase(LocalMailboxFlags flags)
		{
			this.TestIntegration = new TestIntegration(false);
			this.MdbId = null;
			this.ConfiguredMdbName = null;
			this.ConfiguredMdbGuid = Guid.Empty;
			this.MailboxGuid = Guid.Empty;
			this.PrimaryMailboxGuid = Guid.Empty;
			this.MailboxId = null;
			this.DomainControllerName = null;
			this.ConfigDomainControllerName = null;
			this.MailboxDN = null;
			this.TraceMailboxId = null;
			this.Credential = null;
			this.ServerDN = null;
			this.ServerDisplayName = null;
			this.ServerFqdn = null;
			this.ServerGuid = Guid.Empty;
			this.wkpMapper = new WellKnownPrincipalMapper();
			this.publicFoldersToSkip = new EntryIdMap<bool>();
			this.Flags = flags;
			this.Options = MailboxOptions.None;
			this.RestoreFlags = MailboxRestoreType.None;
			this.useMdbQuotaDefaults = null;
			this.recipientType = 0;
			this.recipientDisplayType = 0;
			this.recipientTypeDetails = 0L;
			this.mbxQuota = null;
			this.mbxDumpsterQuota = null;
			this.mbxArchiveQuota = null;
			this.archiveGuid = Guid.Empty;
			this.alternateMailboxes = null;
			this.mbxHomeMdb = null;
			this.preferredDomainControllerName = null;
			this.syncState = null;
			this.configContext = null;
			this.MRSVersion = VersionInformation.MRSProxy;
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00015A2A File Offset: 0x00013C2A
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x00015A32 File Offset: 0x00013C32
		protected string ConfiguredMdbName { get; set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x00015A3B File Offset: 0x00013C3B
		protected bool IsE15OrHigher
		{
			get
			{
				return this.ServerVersion >= Server.E15MinVersion;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00015A4D File Offset: 0x00013C4D
		public bool IsPublicFolderMailbox
		{
			get
			{
				return this.recipientTypeDetails == 68719476736L || this.IsPublicFolderMailboxRestore;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000AFE RID: 2814
		// (set) Token: 0x06000AFF RID: 2815
		public abstract int ServerVersion { get; protected set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x00015A68 File Offset: 0x00013C68
		// (set) Token: 0x06000B01 RID: 2817 RVA: 0x00015A70 File Offset: 0x00013C70
		public MailboxRelease ServerMailboxRelease { get; protected set; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x00015A79 File Offset: 0x00013C79
		// (set) Token: 0x06000B03 RID: 2819 RVA: 0x00015A81 File Offset: 0x00013C81
		public VersionInformation OtherSideVersion { get; private set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00015A8A File Offset: 0x00013C8A
		// (set) Token: 0x06000B05 RID: 2821 RVA: 0x00015A92 File Offset: 0x00013C92
		public VersionInformation MRSVersion { get; internal set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00015A9B File Offset: 0x00013C9B
		// (set) Token: 0x06000B07 RID: 2823 RVA: 0x00015AA3 File Offset: 0x00013CA3
		public ADObjectId MdbId { get; protected set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x00015AAC File Offset: 0x00013CAC
		public Guid MdbGuid
		{
			get
			{
				if (this.MdbId == null)
				{
					return Guid.Empty;
				}
				return this.MdbId.ObjectGuid;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00015AC7 File Offset: 0x00013CC7
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x00015ACF File Offset: 0x00013CCF
		public Guid ConfiguredMdbGuid { get; protected set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00015AD8 File Offset: 0x00013CD8
		public Guid MbxHomeMdbGuid
		{
			get
			{
				if (this.mbxHomeMdb == null)
				{
					return Guid.Empty;
				}
				return this.mbxHomeMdb.ObjectGuid;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x00015AF3 File Offset: 0x00013CF3
		public Guid MbxArchiveMdbGuid
		{
			get
			{
				if (this.archiveMdb == null)
				{
					return Guid.Empty;
				}
				return this.archiveMdb.ObjectGuid;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00015B0E File Offset: 0x00013D0E
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x00015B16 File Offset: 0x00013D16
		public Guid? MailboxContainerGuid { get; private set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00015B1F File Offset: 0x00013D1F
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x00015B27 File Offset: 0x00013D27
		public Guid MailboxGuid { get; private set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00015B30 File Offset: 0x00013D30
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x00015B38 File Offset: 0x00013D38
		public TenantPartitionHint PartitionHint { get; private set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00015B41 File Offset: 0x00013D41
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x00015B49 File Offset: 0x00013D49
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00015B52 File Offset: 0x00013D52
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x00015B5A File Offset: 0x00013D5A
		public string MailboxDN { get; protected set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00015B63 File Offset: 0x00013D63
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x00015B6B File Offset: 0x00013D6B
		public NetworkCredential Credential { get; protected set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00015B74 File Offset: 0x00013D74
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x00015B7C File Offset: 0x00013D7C
		public string DomainControllerName { get; protected set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00015B85 File Offset: 0x00013D85
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x00015B8D File Offset: 0x00013D8D
		public string ConfigDomainControllerName { get; protected set; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00015B96 File Offset: 0x00013D96
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x00015B9E File Offset: 0x00013D9E
		public ADObjectId MailboxId { get; protected set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00015BA7 File Offset: 0x00013DA7
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x00015BAF File Offset: 0x00013DAF
		public string TraceMailboxId { get; protected set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00015BB8 File Offset: 0x00013DB8
		public string TraceMdbId
		{
			get
			{
				if (this.MdbId == null)
				{
					return "(null)";
				}
				return this.MdbId.ToString();
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x00015BD3 File Offset: 0x00013DD3
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x00015BDB File Offset: 0x00013DDB
		public MailboxType MbxType { get; private set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x00015BE4 File Offset: 0x00013DE4
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x00015BEC File Offset: 0x00013DEC
		public LocalMailboxFlags Flags { get; protected set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00015BF5 File Offset: 0x00013DF5
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x00015BFD File Offset: 0x00013DFD
		public MailboxOptions Options { get; private set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00015C06 File Offset: 0x00013E06
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x00015C0E File Offset: 0x00013E0E
		public Guid PrimaryMailboxGuid { get; private set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x00015C17 File Offset: 0x00013E17
		public bool IsPrimaryMailbox
		{
			get
			{
				return this.MailboxGuid == this.PrimaryMailboxGuid;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x00015C2A File Offset: 0x00013E2A
		public bool IsAggregatedMailbox
		{
			get
			{
				return this.Flags.HasFlag(LocalMailboxFlags.AggregatedMailbox);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x00015C46 File Offset: 0x00013E46
		public bool IsArchiveMailbox
		{
			get
			{
				return !this.IsPrimaryMailbox && !this.IsAggregatedMailbox;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00015C5B File Offset: 0x00013E5B
		public ResourceHealthTracker RHTracker
		{
			get
			{
				if (this.rhTracker == null)
				{
					this.rhTracker = new ResourceHealthTracker(this.reservation);
				}
				return this.rhTracker;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x00015C7C File Offset: 0x00013E7C
		// (set) Token: 0x06000B2F RID: 2863 RVA: 0x00015C84 File Offset: 0x00013E84
		public TestIntegration TestIntegration { get; private set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x00015C8D File Offset: 0x00013E8D
		// (set) Token: 0x06000B31 RID: 2865 RVA: 0x00015C95 File Offset: 0x00013E95
		public string ServerDisplayName { get; protected set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00015C9E File Offset: 0x00013E9E
		// (set) Token: 0x06000B33 RID: 2867 RVA: 0x00015CA6 File Offset: 0x00013EA6
		public string ServerDN { get; protected set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00015CAF File Offset: 0x00013EAF
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x00015CB7 File Offset: 0x00013EB7
		public Guid ServerGuid { get; protected set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00015CC0 File Offset: 0x00013EC0
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x00015CC8 File Offset: 0x00013EC8
		public string ServerFqdn { get; protected set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00015CD1 File Offset: 0x00013ED1
		public bool UseHomeMDB
		{
			get
			{
				return this.Flags.HasFlag(LocalMailboxFlags.UseHomeMDB);
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00015CE9 File Offset: 0x00013EE9
		public bool IsFolderMove
		{
			get
			{
				return this.Flags.HasFlag(LocalMailboxFlags.FolderMove);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00015D05 File Offset: 0x00013F05
		public bool IsPublicFolderMove
		{
			get
			{
				return this.Flags.HasFlag(LocalMailboxFlags.PublicFolderMove);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00015D21 File Offset: 0x00013F21
		public bool IsPublicFolderMailboxRestore
		{
			get
			{
				return this.RestoreFlags.HasFlag(MailboxRestoreType.PublicFolderMailbox);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00015D3A File Offset: 0x00013F3A
		public bool IsPureMAPI
		{
			get
			{
				return this.Flags.HasFlag(LocalMailboxFlags.PureMAPI);
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00015D52 File Offset: 0x00013F52
		public bool IsRestore
		{
			get
			{
				return this.Flags.HasFlag(LocalMailboxFlags.Restore);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00015D6E File Offset: 0x00013F6E
		public bool IsMove
		{
			get
			{
				return this.Flags.HasFlag(LocalMailboxFlags.Move);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00015D8A File Offset: 0x00013F8A
		public bool IsOlcSync
		{
			get
			{
				return this.Flags.HasFlag(LocalMailboxFlags.OlcSync);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00015DA6 File Offset: 0x00013FA6
		public bool IsPublicFolderMigrationSource
		{
			get
			{
				return this.Flags.HasFlag(LocalMailboxFlags.LegacyPublicFolders);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00015DBF File Offset: 0x00013FBF
		public bool IsTitanium
		{
			get
			{
				return this.ServerVersion < Server.E2007MinVersion;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00015DCE File Offset: 0x00013FCE
		public bool IsExchange2007
		{
			get
			{
				return this.ServerVersion >= Server.E2007MinVersion && this.ServerVersion < Server.E14MinVersion;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00015DEC File Offset: 0x00013FEC
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x00015DF4 File Offset: 0x00013FF4
		private protected MailboxRestoreType RestoreFlags { protected get; private set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00015DFD File Offset: 0x00013FFD
		protected string EffectiveDomainControllerName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.preferredDomainControllerName))
				{
					return this.preferredDomainControllerName;
				}
				return this.DomainControllerName;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00015E19 File Offset: 0x00014019
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x00015E21 File Offset: 0x00014021
		public virtual MapiSyncState SyncState
		{
			get
			{
				return this.syncState;
			}
			protected set
			{
				this.syncState = value;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00015E2A File Offset: 0x0001402A
		internal virtual bool SupportsSavingSyncState
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000B49 RID: 2889
		public abstract SyncProtocol GetSyncProtocol();

		// Token: 0x06000B4A RID: 2890 RVA: 0x00015E2D File Offset: 0x0001402D
		ISettingsContext ISettingsContextProvider.GetSettingsContext()
		{
			return this.configContext;
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00015E35 File Offset: 0x00014035
		LatencyInfo IMailbox.GetLatencyInfo()
		{
			return new LatencyInfo();
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00015E3C File Offset: 0x0001403C
		void IMailbox.Config(IReservation reservation, Guid primaryMailboxGuid, Guid physicalMailboxGuid, TenantPartitionHint partitionHint, Guid mdbGuid, MailboxType mbxType, Guid? mailboxContainerGuid)
		{
			this.ConfiguredMdbGuid = mdbGuid;
			this.ConfiguredMdbName = null;
			this.MailboxGuid = physicalMailboxGuid;
			this.PrimaryMailboxGuid = primaryMailboxGuid;
			this.MbxType = mbxType;
			this.MailboxDN = null;
			this.TraceMailboxId = null;
			this.MailboxId = null;
			this.MdbId = null;
			this.PartitionHint = partitionHint;
			this.MailboxContainerGuid = mailboxContainerGuid;
			if (reservation != null)
			{
				this.reservation = (ReservationManager.FindReservation(reservation.Id) as MailboxReservation);
				this.reservation.Activate(this.MailboxGuid);
			}
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00015EC4 File Offset: 0x000140C4
		void IMailbox.ConfigRestore(MailboxRestoreType restoreFlags)
		{
			this.RestoreFlags = restoreFlags;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00015ECD File Offset: 0x000140CD
		bool IMailbox.IsCapabilitySupported(MRSProxyCapabilities capability)
		{
			return true;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00015ED0 File Offset: 0x000140D0
		void IMailbox.UpdateMovedMailbox(UpdateMovedMailboxOperation op, ADUser remoteRecipientData, string domainController, out ReportEntry[] entries, Guid newDatabaseGuid, Guid newArchiveDatabaseGuid, string archiveDomain, ArchiveStatusFlags archiveStatus, UpdateMovedMailboxFlags updateMovedMailboxFlags, Guid? newMailboxContainerGuid, CrossTenantObjectId newUnifiedMailboxId)
		{
			MrsTracer.Provider.Function("IMailbox.UpdateMovedMailbox(op={0}, remoteRecipientData={1}, dc={2}, newMDB={3}, newArchiveMDB={4}, archiveDomain={5}, archiveStatus={6}, updateMovedMailboxFlags={7}, newMailboxContainerGuid={8}, newUnifiedMailboxId={9})", new object[]
			{
				op,
				remoteRecipientData,
				domainController,
				newDatabaseGuid,
				newArchiveDatabaseGuid,
				archiveDomain,
				archiveStatus,
				updateMovedMailboxFlags,
				newMailboxContainerGuid,
				newUnifiedMailboxId
			});
			if (this.IsPureMAPI || this.IsRestore)
			{
				throw new UnexpectedErrorPermanentException(-2147024809);
			}
			this.VerifyMailboxConnection(VerifyMailboxConnectionFlags.MailboxSessionNotRequired);
			entries = null;
			List<ReportEntry> list = new List<ReportEntry>();
			try
			{
				using (MrsPSHandler mrsPSHandler = new MrsPSHandler("StorageMailbox.UpdateMovedMailbox Monad"))
				{
					mrsPSHandler.MonadConnection.RunspaceProxy.SetVariable("UMM_UpdateSucceeded", false);
					mrsPSHandler.MonadConnection.RunspaceProxy.SetVariable("UMM_DCName", null);
					mrsPSHandler.MonadConnection.RunspaceProxy.SetVariable("UMM_ReportEntries", list);
					bool flag = updateMovedMailboxFlags.HasFlag(UpdateMovedMailboxFlags.MakeExoPrimary);
					using (MonadCommand command = mrsPSHandler.GetCommand(flag ? MrsCmdlet.SetConsumerMailbox : MrsCmdlet.UpdateMovedMailbox))
					{
						if (flag)
						{
							command.Parameters.Add(new MonadParameter("Identity", new ConsumerMailboxIdParameter(this.MailboxId)));
							command.Parameters.AddSwitch("MakeExoPrimary");
						}
						else
						{
							command.Parameters.Add(new MonadParameter("Identity", new MailboxOrMailUserIdParameter(this.MailboxId)));
							if (this.PartitionHint != null)
							{
								command.Parameters.Add(new MonadParameter("PartitionHint", this.PartitionHint.GetPersistablePartitionHint()));
							}
							if (newDatabaseGuid == Guid.Empty && newArchiveDatabaseGuid == Guid.Empty && (op == UpdateMovedMailboxOperation.UpdateMailbox || op == UpdateMovedMailboxOperation.MorphToMailbox))
							{
								newDatabaseGuid = this.MdbId.ObjectGuid;
								newArchiveDatabaseGuid = this.MdbId.ObjectGuid;
							}
							command.Parameters.Add(new MonadParameter("NewArchiveMDB", newArchiveDatabaseGuid));
							command.Parameters.Add(new MonadParameter("ArchiveDomain", archiveDomain));
							command.Parameters.Add(new MonadParameter("ArchiveStatus", archiveStatus));
							switch (op)
							{
							case UpdateMovedMailboxOperation.UpdateMailbox:
								command.Parameters.AddSwitch("UpdateMailbox");
								command.Parameters.Add(new MonadParameter("NewHomeMDB", newDatabaseGuid));
								command.Parameters.Add(new MonadParameter("NewContainerGuid", newMailboxContainerGuid));
								command.Parameters.Add(new MonadParameter("NewUnifiedMailboxId", newUnifiedMailboxId));
								if ((updateMovedMailboxFlags & UpdateMovedMailboxFlags.SkipMailboxReleaseCheck) != UpdateMovedMailboxFlags.None)
								{
									command.Parameters.AddSwitch("SkipMailboxReleaseCheck");
								}
								if ((updateMovedMailboxFlags & UpdateMovedMailboxFlags.SkipProvisioningCheck) != UpdateMovedMailboxFlags.None)
								{
									command.Parameters.AddSwitch("SkipProvisioningCheck");
								}
								break;
							case UpdateMovedMailboxOperation.MorphToMailbox:
								command.Parameters.AddSwitch("MorphToMailbox");
								command.Parameters.Add(new MonadParameter("NewHomeMDB", newDatabaseGuid));
								command.Parameters.Add(new MonadParameter("RemoteRecipientData", remoteRecipientData));
								if ((updateMovedMailboxFlags & UpdateMovedMailboxFlags.SkipProvisioningCheck) != UpdateMovedMailboxFlags.None)
								{
									command.Parameters.AddSwitch("SkipProvisioningCheck");
								}
								break;
							case UpdateMovedMailboxOperation.MorphToMailUser:
								command.Parameters.AddSwitch("MorphToMailUser");
								command.Parameters.Add(new MonadParameter("RemoteRecipientData", remoteRecipientData));
								break;
							case UpdateMovedMailboxOperation.UpdateArchiveOnly:
								command.Parameters.AddSwitch("UpdateArchiveOnly");
								if (remoteRecipientData != null)
								{
									command.Parameters.Add(new MonadParameter("RemoteRecipientData", remoteRecipientData));
								}
								if ((updateMovedMailboxFlags & UpdateMovedMailboxFlags.SkipMailboxReleaseCheck) != UpdateMovedMailboxFlags.None)
								{
									command.Parameters.AddSwitch("SkipMailboxReleaseCheck");
								}
								break;
							default:
								throw new UpdateMovedMailboxPermanentException();
							}
							if (!string.IsNullOrEmpty(this.DomainControllerName))
							{
								command.Parameters.Add(new MonadParameter("DomainController", this.DomainControllerName));
								PSCredential value = null;
								if (this.Credential != null)
								{
									SecureString secureString = new SecureString();
									foreach (char c in this.Credential.Password)
									{
										secureString.AppendChar(c);
									}
									string text = this.Credential.UserName;
									if (!string.IsNullOrEmpty(this.Credential.Domain))
									{
										text = this.Credential.Domain + "\\" + text;
									}
									value = new PSCredential(text, secureString);
								}
								command.Parameters.Add(new MonadParameter("Credential", value));
								command.Parameters.Add(new MonadParameter("ConfigDomainController", this.ConfigDomainControllerName));
							}
							else if (!string.IsNullOrEmpty(domainController))
							{
								command.Parameters.Add(new MonadParameter("DomainController", domainController));
							}
						}
						bool flag2 = false;
						command.ErrorReport += MailboxProviderBase.ummErrorReportHandler;
						try
						{
							try
							{
								command.Execute();
							}
							finally
							{
								flag2 = (bool)mrsPSHandler.MonadConnection.RunspaceProxy.GetVariable("UMM_UpdateSucceeded");
								if (flag2)
								{
									this.preferredDomainControllerName = (string)mrsPSHandler.MonadConnection.RunspaceProxy.GetVariable("UMM_DCName");
								}
							}
						}
						catch (MonadDataAdapterInvocationException ex)
						{
							LocalizedException ex2 = this.ClassifyWrapAndReturnPowershellException(ex);
							if (!flag2)
							{
								throw ex2;
							}
							list.Add(new ReportEntry(MrsStrings.ReportUpdateMovedMailboxFailureAfterADSwitchover(new LocalizedString(CommonUtils.GetFailureType(ex))), ReportEntryType.Warning, ex2, ReportEntryFlags.Cleanup));
						}
						catch (CmdletInvocationException ex3)
						{
							LocalizedException ex4 = this.ClassifyWrapAndReturnPowershellException(ex3);
							if (!flag2)
							{
								throw ex4;
							}
							list.Add(new ReportEntry(MrsStrings.ReportUpdateMovedMailboxFailureAfterADSwitchover(new LocalizedString(CommonUtils.GetFailureType(ex3))), ReportEntryType.Warning, ex4, ReportEntryFlags.Cleanup));
						}
						if (!flag && !flag2)
						{
							throw this.ClassifyWrapAndReturnPowershellException((mrsPSHandler.ExceptionsReported.Count > 0) ? mrsPSHandler.ExceptionsReported[0] : null);
						}
						foreach (ReportEntry reportEntry in list)
						{
							if (reportEntry.Type == ReportEntryType.Error)
							{
								reportEntry.Type = ReportEntryType.WarningCondition;
							}
						}
						if (this.TestIntegration.UpdateMoveRequestFailsAfterStampingHomeMdb)
						{
							throw new UpdateMovedMailboxPermanentException(new Exception("Failing UpdateMoveRequest due to a test hook"));
						}
					}
					list.AddRange(mrsPSHandler.ReportEntries);
				}
			}
			finally
			{
				entries = list.ToArray();
			}
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00016B84 File Offset: 0x00014D84
		MappedPrincipal[] IMailbox.ResolvePrincipals(MappedPrincipal[] principals)
		{
			this.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			if (this.IsPureMAPI)
			{
				return new MappedPrincipal[principals.Length];
			}
			List<MappedPrincipal> result = new List<MappedPrincipal>(principals.Length);
			this.RunADRecipientOperation(true, delegate(IRecipientSession adSession)
			{
				IRecipientSession recipientSession = this.GetRecipientSession(true, true);
				this.wkpMapper.Initialize(recipientSession);
				MappedPrincipal[] principals2 = principals;
				for (int i = 0; i < principals2.Length; i++)
				{
					MappedPrincipal principal = principals2[i];
					ExecutionContext.Create(new DataContext[]
					{
						new SimpleValueDataContext("Principal", principal)
					}).Execute(delegate
					{
						ADRawEntry adrawEntry = null;
						if (principal.HasField(MappedPrincipalFields.MailboxGuid))
						{
							MrsTracer.Provider.Debug("Looking up principal by mailboxGuid {0}", new object[]
							{
								principal.MailboxGuid
							});
							SecurityIdentifier securityIdentifier = this.wkpMapper[principal.MailboxGuid];
							if (securityIdentifier != null)
							{
								MrsTracer.Provider.Debug("Found well-known principal '{0}'", new object[]
								{
									securityIdentifier
								});
								MappedPrincipal mappedPrincipal = new MappedPrincipal();
								mappedPrincipal.MailboxGuid = principal.MailboxGuid;
								mappedPrincipal.ObjectSid = securityIdentifier;
								result.Add(mappedPrincipal);
								return;
							}
							adrawEntry = adSession.FindByExchangeGuid(principal.MailboxGuid, MappedPrincipal.PrincipalProperties);
						}
						if (adrawEntry == null && principal.HasField(MappedPrincipalFields.ObjectGuid))
						{
							MrsTracer.Provider.Debug("Looking up principal by objectGuid {0}", new object[]
							{
								principal.ObjectGuid
							});
							adrawEntry = adSession.ReadADRawEntry(new ADObjectId(principal.ObjectGuid), MappedPrincipal.PrincipalProperties);
						}
						if (adrawEntry == null && principal.HasField(MappedPrincipalFields.ObjectSid))
						{
							MrsTracer.Provider.Debug("Looking up principal by SID {0}", new object[]
							{
								principal.ObjectSid
							});
							Guid guid = this.wkpMapper[principal.ObjectSid];
							if (guid != Guid.Empty)
							{
								MappedPrincipal mappedPrincipal2 = new MappedPrincipal();
								mappedPrincipal2.MailboxGuid = guid;
								mappedPrincipal2.ObjectSid = principal.ObjectSid;
								result.Add(mappedPrincipal2);
								return;
							}
							try
							{
								adrawEntry = adSession.FindADRawEntryBySid(principal.ObjectSid, MappedPrincipal.PrincipalProperties);
							}
							catch (NonUniqueRecipientException)
							{
								MrsTracer.Provider.Debug("More than one recipient found for SID {0}, ignoring.", new object[]
								{
									principal.ObjectSid
								});
							}
						}
						if (adrawEntry == null && principal.HasField(MappedPrincipalFields.LegacyDN))
						{
							MrsTracer.Provider.Debug("Looking up principal by LegDN or X500 proxy '{0}'", new object[]
							{
								principal.LegacyDN
							});
							ProxyAddress proxyAddress = new CustomProxyAddress((CustomProxyAddressPrefix)ProxyAddressPrefix.LegacyDN, principal.LegacyDN, true);
							try
							{
								adrawEntry = adSession.FindByProxyAddress(proxyAddress, MappedPrincipal.PrincipalProperties);
							}
							catch (NonUniqueRecipientException)
							{
								MrsTracer.Provider.Debug("More than one recipient found for LegDN '{0}', ignoring.", new object[]
								{
									principal.LegacyDN
								});
							}
						}
						if (adrawEntry == null && principal.HasField(MappedPrincipalFields.ProxyAddresses))
						{
							MrsTracer.Provider.Debug("Looking up principal by proxies [{0}]", new object[]
							{
								string.Join(",", principal.ProxyAddresses)
							});
							ProxyAddressCollection proxyAddressCollection = new ProxyAddressCollection(principal.ProxyAddresses);
							Result<ADRawEntry>[] array = adSession.FindByProxyAddresses(proxyAddressCollection.ToArray(), MappedPrincipal.PrincipalProperties);
							if (array != null && array.Length == 1)
							{
								adrawEntry = array[0].Data;
							}
						}
						List<ADRawEntry> list = new List<ADRawEntry>();
						if (adrawEntry != null)
						{
							list.Add(adrawEntry);
						}
						if (adrawEntry == null && principal.HasField(MappedPrincipalFields.Alias))
						{
							MrsTracer.Provider.Debug("Looking up principal by ID '{0}'", new object[]
							{
								principal.Alias
							});
							RecipientIdParameter recipientIdParameter = new RecipientIdParameter(principal.Alias);
							IEnumerable<ADRecipient> objects = recipientIdParameter.GetObjects<ADRecipient>(null, adSession);
							if (objects != null)
							{
								foreach (ADRecipient adrecipient in objects)
								{
									if (adrecipient != null)
									{
										list.Add(adrecipient);
									}
								}
							}
						}
						MappedPrincipal mappedPrincipal3 = null;
						foreach (ADRawEntry adrawEntry2 in list)
						{
							MrsTracer.Provider.Debug("Found principal '{0}'", new object[]
							{
								adrawEntry2.Identity
							});
							mappedPrincipal3 = new MappedPrincipal(adrawEntry2)
							{
								NextEntry = mappedPrincipal3
							};
						}
						if (mappedPrincipal3 == null)
						{
							MrsTracer.Provider.Debug("Unable to locate principal", new object[0]);
						}
						result.Add(mappedPrincipal3);
					});
				}
			});
			return result.ToArray();
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00016C24 File Offset: 0x00014E24
		Guid[] IMailbox.ResolvePolicyTag(string policyTagStr)
		{
			this.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			if (this.IsPureMAPI)
			{
				return null;
			}
			IEnumerable<RetentionPolicyTag> results = null;
			this.RunADRecipientOperation(true, delegate(IRecipientSession adSession)
			{
				RetentionPolicyTagIdParameter retentionPolicyTagIdParameter = new RetentionPolicyTagIdParameter(policyTagStr);
				results = retentionPolicyTagIdParameter.GetObjects<RetentionPolicyTag>(null, adSession);
			});
			if (results == null)
			{
				return null;
			}
			List<Guid> list = new List<Guid>();
			foreach (RetentionPolicyTag retentionPolicyTag in results)
			{
				list.Add(retentionPolicyTag.Guid);
			}
			return list.ToArray();
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00016CEC File Offset: 0x00014EEC
		ADUser IMailbox.GetADUser()
		{
			MrsTracer.Provider.Function("IMailbox.GetADUser", new object[0]);
			ADUser adUser = null;
			this.RunADRecipientOperation(false, delegate(IRecipientSession adSession)
			{
				adUser = (adSession.Read(this.MailboxId) as ADUser);
			});
			if (adUser == null)
			{
				throw new RecipientNotFoundPermanentException(this.MailboxGuid);
			}
			return adUser;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00016DA4 File Offset: 0x00014FA4
		bool IMailbox.UpdateRemoteHostName(string value)
		{
			MrsTracer.Provider.Function("UpdateRemoteHostName({0})", new object[]
			{
				value
			});
			this.RunADRecipientOperation(false, delegate(IRecipientSession adSession)
			{
				ADUser aduser = adSession.Read(this.MailboxId) as ADUser;
				if (aduser == null)
				{
					throw new RecipientNotFoundPermanentException(this.MailboxGuid);
				}
				aduser.MailboxMoveRemoteHostName = value;
				adSession.Save(aduser);
			});
			return true;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00016DF9 File Offset: 0x00014FF9
		void IMailbox.ConfigMDBByName(string mdbName)
		{
			this.ConfiguredMdbGuid = Guid.Empty;
			this.ConfiguredMdbName = mdbName;
			this.MdbId = null;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00016E14 File Offset: 0x00015014
		void IMailbox.ConfigADConnection(string domainControllerName, string configDomainControllerName, NetworkCredential cred)
		{
			this.DomainControllerName = domainControllerName;
			this.ConfigDomainControllerName = configDomainControllerName;
			this.Credential = cred;
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00016E2B File Offset: 0x0001502B
		void IMailbox.ConfigMailboxOptions(MailboxOptions options)
		{
			this.Options = options;
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00016E34 File Offset: 0x00015034
		void IMailbox.ConfigPreferredADConnection(string preferredDomainControllerName)
		{
			this.preferredDomainControllerName = preferredDomainControllerName;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00016E3D File Offset: 0x0001503D
		void IMailbox.ConfigOlc(OlcMailboxConfiguration config)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00016E44 File Offset: 0x00015044
		MailboxServerInformation IMailbox.GetMailboxServerInformation()
		{
			MrsTracer.Provider.Function("MapiMailbox.GetMailboxServerInformation", new object[0]);
			MailboxServerInformation mailboxServerInformation = new MailboxServerInformation();
			mailboxServerInformation.MailboxServerName = this.ServerDisplayName;
			mailboxServerInformation.MailboxServerVersion = this.ServerVersion;
			mailboxServerInformation.MailboxServerGuid = this.ServerGuid;
			mailboxServerInformation.ProxyServerName = null;
			mailboxServerInformation.ProxyServerVersion = null;
			if (!this.IsPureMAPI)
			{
				using (ExRpcAdmin rpcAdmin = this.GetRpcAdmin())
				{
					using (this.RHTracker.Start())
					{
						mailboxServerInformation.MailboxSignatureVersion = rpcAdmin.GetMailboxSignatureServerVersion();
						mailboxServerInformation.DeleteMailboxVersion = rpcAdmin.GetDeleteMailboxServerVersion();
						mailboxServerInformation.InTransitStatusVersion = rpcAdmin.GetInTransitStatusServerVersion();
						mailboxServerInformation.MailboxShapeVersion = rpcAdmin.GetMailboxShapeServerVersion();
					}
				}
			}
			return mailboxServerInformation;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00016F20 File Offset: 0x00015120
		void IMailbox.DeleteMailbox(int flags)
		{
			this.DeleteMailboxInternal(flags);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00016F50 File Offset: 0x00015150
		RawSecurityDescriptor IMailbox.GetMailboxSecurityDescriptor()
		{
			MrsTracer.Provider.Function("MapiMailbox.GetMailboxSecurityDescriptor", new object[0]);
			if (!this.IsE15OrHigher)
			{
				RawSecurityDescriptor mailboxSecurityDescriptor;
				using (ExRpcAdmin rpcAdmin = this.GetRpcAdmin())
				{
					using (this.RHTracker.Start())
					{
						mailboxSecurityDescriptor = rpcAdmin.GetMailboxSecurityDescriptor(this.MdbGuid, this.MailboxGuid);
					}
				}
				return mailboxSecurityDescriptor;
			}
			ADUser adUser = null;
			this.RunADRecipientOperation(true, delegate(IRecipientSession adSession)
			{
				adUser = (adSession.Read(this.MailboxId) as ADUser);
			});
			if (adUser == null)
			{
				throw new RecipientNotFoundPermanentException(this.MailboxGuid);
			}
			return adUser.ExchangeSecurityDescriptor;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00017040 File Offset: 0x00015240
		RawSecurityDescriptor IMailbox.GetUserSecurityDescriptor()
		{
			MrsTracer.Provider.Function("MapiMailbox.GetUserSecurityDescriptor", new object[0]);
			RawSecurityDescriptor sd = null;
			this.RunADRecipientOperation(true, delegate(IRecipientSession adSession)
			{
				sd = adSession.ReadSecurityDescriptor(this.MailboxId);
			});
			return sd;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0001708F File Offset: 0x0001528F
		void IMailbox.SeedMBICache()
		{
			if (this.IsTitanium)
			{
				return;
			}
			this.DiscoverUmmDcForTarget();
			this.SeedMBICacheInternal(null);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x000170A7 File Offset: 0x000152A7
		ServerHealthStatus IMailbox.CheckServerHealth()
		{
			return this.CheckServerHealthInternal();
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x000170AF File Offset: 0x000152AF
		SessionStatistics IMailbox.GetSessionStatistics(SessionStatisticsFlags statisticsTypes)
		{
			return new SessionStatistics();
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x000170B6 File Offset: 0x000152B6
		Guid IMailbox.StartIsInteg(List<uint> mailboxCorruptionTypes)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x000170BD File Offset: 0x000152BD
		List<StoreIntegrityCheckJob> IMailbox.QueryIsInteg(Guid isIntegRequestGuid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x000170C4 File Offset: 0x000152C4
		void IMailbox.Connect(MailboxConnectFlags connectFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x000170CB File Offset: 0x000152CB
		bool IMailbox.IsConnected()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x000170D2 File Offset: 0x000152D2
		bool IMailbox.IsMailboxCapabilitySupported(MailboxCapabilities capability)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x000170D9 File Offset: 0x000152D9
		MailboxInformation IMailbox.GetMailboxInformation()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x000170E0 File Offset: 0x000152E0
		public virtual void Disconnect()
		{
			if (this.rhTracker != null)
			{
				this.rhTracker.Dispose();
				this.rhTracker = null;
			}
			this.connectedWithoutMailboxSession = false;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00017103 File Offset: 0x00015303
		VersionInformation IMailbox.GetVersion()
		{
			return VersionInformation.MRSProxy;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0001710A File Offset: 0x0001530A
		void IMailbox.SetOtherSideVersion(VersionInformation otherSideVersion)
		{
			this.OtherSideVersion = otherSideVersion;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00017113 File Offset: 0x00015313
		void IMailbox.SetInTransitStatus(InTransitStatus status, out bool onlineMoveSupported)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0001711A File Offset: 0x0001531A
		List<FolderRec> IMailbox.EnumerateFolderHierarchy(EnumerateFolderHierarchyFlags flags, PropTag[] additionalPtagsToLoad)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00017121 File Offset: 0x00015321
		List<WellKnownFolder> IMailbox.DiscoverWellKnownFolders(int flags)
		{
			return FolderHierarchyUtils.DiscoverWellKnownFolders(this, flags);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0001712A File Offset: 0x0001532A
		NamedPropData[] IMailbox.GetNamesFromIDs(PropTag[] pta)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00017131 File Offset: 0x00015331
		PropTag[] IMailbox.GetIDsFromNames(bool createIfNotExists, NamedPropData[] npda)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00017138 File Offset: 0x00015338
		void IMailbox.AddMoveHistoryEntry(MoveHistoryEntryInternal mhei, int maxMoveHistoryLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0001713F File Offset: 0x0001533F
		PropValueData[] IMailbox.GetProps(PropTag[] ptags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00017146 File Offset: 0x00015346
		byte[] IMailbox.GetReceiveFolderEntryId(string msgClass)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0001714D File Offset: 0x0001534D
		string IMailbox.LoadSyncState(byte[] key)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00017154 File Offset: 0x00015354
		MessageRec IMailbox.SaveSyncState(byte[] key, string syncStateStr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0001715B File Offset: 0x0001535B
		byte[] IMailbox.GetSessionSpecificEntryId(byte[] entryId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00017162 File Offset: 0x00015362
		void IMailbox.ConfigPst(string filePath, int? contentCodePage)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00017169 File Offset: 0x00015369
		void IMailbox.ConfigEas(NetworkCredential userCredential, SmtpAddress smtpAddress, Guid mailboxGuid, string remoteHostName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00017170 File Offset: 0x00015370
		public ExRpcAdmin GetRpcAdmin()
		{
			MrsTracer.Provider.Function("MapiMailbox.GetRpcAdmin", new object[0]);
			base.CheckDisposed();
			if (this.IsPureMAPI)
			{
				throw new UnexpectedErrorPermanentException(-2147024809);
			}
			string user;
			string domain;
			string password;
			this.GetCreds(out user, out domain, out password);
			MrsTracer.Provider.Debug("Opening ExRpcAdmin connection to {0}", new object[]
			{
				this.ServerFqdn
			});
			ExRpcAdmin result;
			using (this.RHTracker.Start())
			{
				result = ExRpcAdmin.Create("Client=MSExchangeMigration", this.ServerFqdn, user, domain, password);
			}
			return result;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0001721C File Offset: 0x0001541C
		public void GetFolderViewsOrRestrictions(FolderRec folderRec, GetFolderRecFlags flags, byte[] folderId)
		{
			this.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			using (ExRpcAdmin rpcAdmin = this.GetRpcAdmin())
			{
				try
				{
					DateTime t = DateTime.UtcNow - new TimeSpan(7, 0, 0, 0);
					if ((flags & GetFolderRecFlags.Views) != GetFolderRecFlags.None)
					{
						PropTag[] propTagsRequested = new PropTag[]
						{
							PropTag.NextLocalId,
							PropTag.ViewAccessTime,
							PropTag.LCID,
							PropTag.MailboxQuarantined,
							PropTag.ConversationsFilter,
							PropTag.ViewCoveringPropertyTags,
							PropTag.ISCViewFilter
						};
						List<SortOrderData> list = new List<SortOrderData>();
						List<ICSViewData> list2 = new List<ICSViewData>();
						PropValue[][] viewsTable;
						using (this.RHTracker.Start())
						{
							viewsTable = rpcAdmin.GetViewsTable(MdbFlags.Private, this.MdbGuid, this.MailboxGuid, folderId, propTagsRequested);
						}
						foreach (PropValue[] array2 in viewsTable)
						{
							DateTime dateTime = array2[1].GetDateTime();
							if (!(dateTime < t))
							{
								bool flag = array2.Length >= 7 && !array2[6].IsNull() && !array2[6].IsError() && array2[6].GetBoolean();
								if (flag)
								{
									list2.Add(new ICSViewData
									{
										Conversation = (array2.Length >= 5 && !array2[4].IsNull() && !array2[4].IsError() && array2[4].GetBoolean()),
										CoveringPropertyTags = ((array2.Length < 6 || array2[5].IsNull() || array2[5].IsError()) ? Array<int>.Empty : array2[5].GetIntArray())
									});
								}
								else
								{
									SortOrder native = new SortOrder(array2[0].GetBytes());
									SortOrderData data = DataConverter<SortOrderConverter, SortOrder, SortOrderData>.GetData(native);
									data.LCID = ((array2.Length < 3 || array2[2].IsNull() || array2[2].IsError()) ? 0 : array2[2].GetInt());
									data.FAI = (array2.Length >= 4 && !array2[3].IsNull() && !array2[3].IsError() && array2[3].GetBoolean());
									data.Conversation = (array2.Length >= 5 && !array2[4].IsNull() && !array2[4].IsError() && array2[4].GetBoolean());
									list.Add(data);
								}
							}
						}
						MrsTracer.Provider.Debug("Loaded {0} views.", new object[]
						{
							list.Count
						});
						folderRec.Views = list.ToArray();
						MrsTracer.Provider.Debug("Loaded {0} ICS views.", new object[]
						{
							list2.Count
						});
						folderRec.ICSViews = list2.ToArray();
					}
					if ((flags & GetFolderRecFlags.Restrictions) != GetFolderRecFlags.None)
					{
						List<RestrictionData> list3 = new List<RestrictionData>();
						PropTag[] propTagsRequested2 = new PropTag[]
						{
							PropTag.ViewRestriction,
							PropTag.ViewAccessTime,
							PropTag.LCIDRestriction
						};
						PropValue[][] restrictionTable;
						using (this.RHTracker.Start())
						{
							restrictionTable = rpcAdmin.GetRestrictionTable(MdbFlags.Private, this.MdbGuid, this.MailboxGuid, folderId, propTagsRequested2);
						}
						foreach (PropValue[] array4 in restrictionTable)
						{
							DateTime dateTime2 = array4[1].GetDateTime();
							if (!(dateTime2 < t))
							{
								RestrictionData data2 = DataConverter<RestrictionConverter, Restriction, RestrictionData>.GetData((Restriction)array4[0].Value);
								data2.LCID = ((array4.Length < 3 || array4[2].IsNull() || array4[2].IsError()) ? 0 : array4[2].GetInt());
								list3.Add(data2);
							}
						}
						MrsTracer.Provider.Debug("Loaded {0} restrictions.", new object[]
						{
							list3.Count
						});
						folderRec.Restrictions = list3.ToArray();
					}
				}
				catch (MapiExceptionVersion)
				{
					MrsTracer.Provider.Debug("Source server does not support Views/Restrictions tables.", new object[0]);
				}
				catch (MapiExceptionInvalidType ex)
				{
					MrsTracer.Provider.Warning("Loading extended properties failed with error {0}", new object[]
					{
						CommonUtils.FullExceptionMessage(ex)
					});
				}
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00017738 File Offset: 0x00015938
		public void RunADRecipientOperation(Action del)
		{
			try
			{
				del();
			}
			catch (LocalizedException ex)
			{
				if (CommonUtils.ExceptionIsAny(ex, new WellKnownException[]
				{
					WellKnownException.AD,
					WellKnownException.MapiADUnavailable
				}))
				{
					this.preferredDomainControllerName = null;
				}
				throw;
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x000177B8 File Offset: 0x000159B8
		public void RunADRecipientOperation(bool readOnly, Action<IRecipientSession> del)
		{
			this.RunADRecipientOperation(delegate()
			{
				IRecipientSession recipientSession = this.GetRecipientSession(readOnly);
				del(recipientSession);
			});
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x000177F4 File Offset: 0x000159F4
		public void VerifyCapability(MRSProxyCapabilities capability, CapabilityCheck whomToCheck)
		{
			if (whomToCheck.HasFlag(CapabilityCheck.MRS) && !this.MRSVersion[(int)capability])
			{
				throw new UnsupportedRemoteServerVersionWithOperationPermanentException(this.MRSVersion.ComputerName, this.MRSVersion.ToString(), capability.ToString());
			}
			if (whomToCheck.HasFlag(CapabilityCheck.OtherProvider))
			{
				if (this.OtherSideVersion == null)
				{
					throw new UnsupportedRemoteServerVersionWithOperationPermanentException(this.MRSVersion.ComputerName, this.MRSVersion.ToString(), "SetOtherSideVersion");
				}
				if (!this.OtherSideVersion[(int)capability])
				{
					throw new UnsupportedRemoteServerVersionWithOperationPermanentException(this.OtherSideVersion.ComputerName, this.OtherSideVersion.ToString(), capability.ToString());
				}
			}
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x000178BC File Offset: 0x00015ABC
		public Guid LinkMailPublicFolder(byte[] folderId, LinkMailPublicFolderFlags flags, byte[] objectId)
		{
			bool flag = !CommonUtils.IsMultiTenantEnabled() || this.PartitionHint == null;
			ADSessionSettings sessionSettings = flag ? ADSessionSettings.FromRootOrgScopeSet() : ADSessionSettings.FromTenantPartitionHint(this.PartitionHint);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.PartiallyConsistent, sessionSettings, 2196, "LinkMailPublicFolder", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\Common\\MailboxProviderBase.cs");
			if (flag)
			{
				tenantOrRootOrgRecipientSession.EnforceDefaultScope = false;
				tenantOrRootOrgRecipientSession.UseGlobalCatalog = true;
			}
			Guid result = Guid.Empty;
			ADUser aduser = tenantOrRootOrgRecipientSession.Read(this.MailboxId) as ADUser;
			if (aduser == null)
			{
				throw new RecipientNotFoundPermanentException(this.MailboxGuid);
			}
			ADPublicFolder adpublicFolder = null;
			switch (flags)
			{
			case LinkMailPublicFolderFlags.ObjectGuid:
				adpublicFolder = (tenantOrRootOrgRecipientSession.Read(new ADObjectId(objectId)) as ADPublicFolder);
				break;
			case LinkMailPublicFolderFlags.EntryId:
			{
				string propertyValue = PublicFolderSession.ConvertToLegacyDN("e71f13d1-0178-42a7-8c47-24206de84a77", HexConverter.ByteArrayToHexString(objectId));
				ADRecipient[] array = tenantOrRootOrgRecipientSession.Find(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.LegacyExchangeDN, propertyValue), null, 2);
				if (array.Length == 1)
				{
					adpublicFolder = (array[0] as ADPublicFolder);
				}
				break;
			}
			default:
				throw new UnexpectedErrorPermanentException(-2147024809);
			}
			if (adpublicFolder != null)
			{
				adpublicFolder.ContentMailbox = aduser.Id;
				adpublicFolder.EntryId = HexConverter.ByteArrayToHexString(folderId);
				tenantOrRootOrgRecipientSession.Save(adpublicFolder);
				result = adpublicFolder.Guid;
			}
			return result;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x000179F4 File Offset: 0x00015BF4
		protected static void PopulateMailboxInformation(MailboxInformation info, PropTag tag, object value)
		{
			if (tag <= PropTag.AssocContentCount)
			{
				if (tag <= PropTag.MailboxPartitionMailboxGuids)
				{
					if (tag == PropTag.MessageSizeExtended)
					{
						info.RegularItemsSize = MailboxProviderBase.GetULong(value);
						info.MailboxSize += info.RegularItemsSize;
						return;
					}
					if (tag != PropTag.MailboxPartitionMailboxGuids)
					{
						return;
					}
					info.ContainerMailboxGuids = (Guid[])value;
					return;
				}
				else
				{
					if (tag == PropTag.ContentCount)
					{
						info.RegularItemCount = MailboxProviderBase.GetULong(value);
						info.MailboxItemCount += info.RegularItemCount;
						return;
					}
					if (tag != PropTag.AssocContentCount)
					{
						return;
					}
					info.AssociatedItemCount = MailboxProviderBase.GetULong(value);
					info.MailboxItemCount += info.AssociatedItemCount;
					return;
				}
			}
			else if (tag <= PropTag.DeletedAssocMsgCount)
			{
				if (tag == PropTag.DeletedMsgCount)
				{
					info.RegularDeletedItemCount = MailboxProviderBase.GetULong(value);
					info.MailboxItemCount += info.RegularDeletedItemCount;
					return;
				}
				if (tag != PropTag.DeletedAssocMsgCount)
				{
					return;
				}
				info.AssociatedDeletedItemCount = MailboxProviderBase.GetULong(value);
				info.MailboxItemCount += info.AssociatedDeletedItemCount;
				return;
			}
			else
			{
				if (tag == PropTag.DeletedMessageSizeExtended)
				{
					info.RegularDeletedItemsSize = MailboxProviderBase.GetULong(value);
					info.MailboxSize += info.RegularDeletedItemsSize;
					return;
				}
				if (tag == PropTag.DeleteAssocMessageSizeExtended)
				{
					info.AssociatedDeletedItemsSize = MailboxProviderBase.GetULong(value);
					info.MailboxSize += info.AssociatedDeletedItemsSize;
					return;
				}
				if (tag != PropTag.AssocMessageSizeExtended)
				{
					return;
				}
				info.AssociatedItemsSize = MailboxProviderBase.GetULong(value);
				info.MailboxSize += info.AssociatedItemsSize;
				return;
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00017B88 File Offset: 0x00015D88
		protected virtual void ValidateRecipient(MiniRecipient recipient)
		{
			if (recipient.RecipientType != RecipientType.UserMailbox && recipient.RecipientType != RecipientType.MailUser)
			{
				throw new UnsupportedRecipientTypePermanentException(recipient.ToString(), recipient.RecipientType.ToString());
			}
			if (string.IsNullOrEmpty(recipient.LegacyExchangeDN))
			{
				throw new RecipientMissingLegDNPermanentException(recipient.ToString());
			}
			if (this.PrimaryMailboxGuid != recipient.ExchangeGuid)
			{
				MrsTracer.Provider.Error("We managed to find the wrong user.", new object[0]);
				throw new UnexpectedErrorPermanentException(-2147221233);
			}
			if (this.IsAggregatedMailbox)
			{
				MultiValuedProperty<Guid> multiValuedProperty = recipient.AggregatedMailboxGuids ?? new MultiValuedProperty<Guid>();
				if (!multiValuedProperty.Contains(this.MailboxGuid))
				{
					MrsTracer.Provider.Error("Unable to locate aggregated mailbox with guid {0}", new object[]
					{
						this.MailboxGuid
					});
					throw new RecipientAggregatedMailboxNotFoundPermanentException(recipient.ToString(), string.Join<Guid>(",", multiValuedProperty.ToArray()), this.MailboxGuid);
				}
			}
			if (this.IsArchiveMailbox && this.MailboxGuid != recipient.ArchiveGuid)
			{
				MrsTracer.Provider.Error("Unable to locate archive mailbox with guid {0}", new object[]
				{
					this.MailboxGuid
				});
				throw new RecipientArchiveGuidMismatchPermanentException(recipient.ToString(), recipient.ArchiveGuid, this.MailboxGuid);
			}
			if (this.IsMove && (this.MbxType == MailboxType.SourceMailbox || this.MbxType == MailboxType.DestMailboxCrossOrg))
			{
				MultiValuedProperty<string> multiValuedProperty2 = recipient[ADRecipientSchema.AllowedAttributesEffective] as MultiValuedProperty<string>;
				foreach (ADPropertyDefinition adpropertyDefinition in MailboxProviderBase.WriteableProperties)
				{
					bool flag = false;
					if (multiValuedProperty2 != null)
					{
						foreach (string x in multiValuedProperty2)
						{
							if (StringComparer.OrdinalIgnoreCase.Equals(x, adpropertyDefinition.LdapDisplayName))
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						throw new RecipientPropertyIsNotWriteablePermanentException(recipient.ToString(), adpropertyDefinition.LdapDisplayName);
					}
				}
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00017D98 File Offset: 0x00015F98
		protected void CreateStoreSession(MailboxConnectFlags connectFlags, Action createSessionDelegate)
		{
			base.CheckDisposed();
			if (((IMailbox)this).IsConnected())
			{
				return;
			}
			this.LocateAndValidateADUser();
			if ((connectFlags & MailboxConnectFlags.DoNotOpenMapiSession) != MailboxConnectFlags.None)
			{
				this.connectedWithoutMailboxSession = true;
				return;
			}
			createSessionDelegate();
			if (((IMailbox)this).IsConnected())
			{
				this.AfterConnect();
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00017DD0 File Offset: 0x00015FD0
		protected void VerifyMailboxConnection(VerifyMailboxConnectionFlags flags = VerifyMailboxConnectionFlags.None)
		{
			base.CheckDisposed();
			if (this.reservation != null && this.reservation.IsDisposed)
			{
				throw new ExpiredReservationException();
			}
			if (!flags.HasFlag(VerifyMailboxConnectionFlags.MailboxSessionNotRequired) && (this.connectedWithoutMailboxSession || !((IMailbox)this).IsConnected()))
			{
				throw new NotConnectedPermanentException();
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00017EA4 File Offset: 0x000160A4
		protected virtual void CopyMessagesOneByOne(List<MessageRec> messages, IFxProxyPool proxyPool, PropTag[] propsToCopyExplicitly, PropTag[] excludeProps, Action<MessageRec> changeSourceFolderAction = null)
		{
			MrsTracer.Provider.Function("MailboxProviderBase.CopyMessagesOneByOne({0} messages)", new object[]
			{
				messages.Count
			});
			bool exportCompleted = false;
			CommonUtils.ProcessKnownExceptions(delegate
			{
				this.CopyMessagesIndividually(messages, proxyPool, propsToCopyExplicitly, excludeProps, changeSourceFolderAction);
				exportCompleted = true;
				proxyPool.Flush();
			}, delegate(Exception ex)
			{
				if (!exportCompleted)
				{
					MrsTracer.Provider.Debug("Flushing target proxy after receiving an exception.", new object[0]);
					CommonUtils.CatchKnownExceptions(new Action(proxyPool.Flush), null);
				}
				return false;
			});
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00017F31 File Offset: 0x00016131
		protected virtual void CopySingleMessage(MessageRec curMsg, IFolderProxy folderProxy, PropTag[] propsToCopyExplicitly, PropTag[] excludeProps)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00017F38 File Offset: 0x00016138
		protected void DiscoverUmmDcForTarget()
		{
			if (!string.IsNullOrEmpty(this.preferredDomainControllerName))
			{
				return;
			}
			ADUser aduser = ((IMailbox)this).GetADUser();
			Guid mdbGuid = this.MdbGuid;
			ADObjectId adobjectId = this.IsArchiveMailbox ? aduser.ArchiveDatabase : aduser.Database;
			if (adobjectId != null && mdbGuid.Equals(adobjectId.ObjectGuid))
			{
				this.preferredDomainControllerName = aduser.OriginatingServer;
				MrsTracer.Provider.Debug("Updated preferred DC to {0}", new object[]
				{
					this.preferredDomainControllerName
				});
				return;
			}
			throw new CouldNotFindDCHavingUmmUpdateTransientException(mdbGuid, this.MailboxId.ToString());
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00017FC8 File Offset: 0x000161C8
		protected void VerifyMdbIsOnline(Exception originalException)
		{
			MrsTracer.Provider.Function("MapiMailbox.VerifyMdbIsOnline", new object[0]);
			if (this.IsPureMAPI)
			{
				return;
			}
			MdbStatus[] array;
			using (ExRpcAdmin rpcAdmin = this.GetRpcAdmin())
			{
				using (this.RHTracker.Start())
				{
					array = rpcAdmin.ListMdbStatus(new Guid[]
					{
						this.MdbGuid
					});
				}
			}
			if (array.Length != 1 || (array[0].Status & MdbStatusFlags.Online) == MdbStatusFlags.Offline)
			{
				MrsTracer.Provider.Warning("MDB {0} is offline", new object[]
				{
					this.MdbGuid
				});
				throw new MdbIsOfflineTransientException(this.MdbGuid, originalException);
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x000180EC File Offset: 0x000162EC
		protected void LocateAndValidateADUser()
		{
			if (this.IsPureMAPI)
			{
				return;
			}
			if (this.IsPublicFolderMigrationSource)
			{
				PublicFolderDatabase publicFolderDatabase = this.FindDatabaseByGuid<PublicFolderDatabase>(this.ConfiguredMdbGuid);
				this.mbxHomeMdb = publicFolderDatabase.Id;
				this.MailboxId = publicFolderDatabase.Id;
				this.TraceMailboxId = publicFolderDatabase.Identity.ToString();
			}
			else if (this.IsRestore)
			{
				this.TraceMailboxId = string.Format("{0} {1}", this.MailboxGuid, this.MdbGuid);
			}
			else
			{
				MiniRecipient recipient = null;
				this.RunADRecipientOperation(delegate()
				{
					recipient = CommonUtils.FindUserByMailboxGuid(this.PrimaryMailboxGuid, this.PartitionHint, this.Credential, this.EffectiveDomainControllerName, MailboxProviderBase.UserPropertiesToLoad);
				});
				if (recipient == null)
				{
					throw new RecipientNotFoundPermanentException(this.MailboxGuid);
				}
				this.ValidateRecipient(recipient);
				this.MailboxId = recipient.Id;
				this.MailboxDN = recipient.LegacyExchangeDN;
				this.OrganizationId = recipient.OrganizationId;
				this.recipientType = (int)recipient.RecipientType;
				if (!this.IsOlcSync)
				{
					RecipientDisplayType? recipientDisplayType = (RecipientDisplayType?)recipient[ADRecipientSchema.RecipientDisplayType];
					this.recipientDisplayType = (int)((recipientDisplayType != null) ? recipientDisplayType.Value : RecipientDisplayType.MailboxUser);
				}
				this.recipientTypeDetails = (long)recipient.RecipientTypeDetails;
				if (this.IsPrimaryMailbox)
				{
					this.TraceMailboxId = this.MailboxDN;
				}
				else
				{
					this.TraceMailboxId = string.Format("{0}:{1}", this.MailboxDN, this.MailboxGuid);
				}
				if (this.IsOlcSync)
				{
					this.useMdbQuotaDefaults = new bool?(false);
					this.mbxQuota = null;
					this.mbxDumpsterQuota = null;
					this.mbxArchiveQuota = null;
				}
				else
				{
					this.useMdbQuotaDefaults = (bool?)recipient[ADMailboxRecipientSchema.UseDatabaseQuotaDefaults];
					this.mbxQuota = MailboxProviderBase.GetQuotaValue(recipient, ADMailboxRecipientSchema.ProhibitSendReceiveQuota);
					this.mbxDumpsterQuota = MailboxProviderBase.GetQuotaValue(recipient, ADUserSchema.RecoverableItemsQuota);
					this.mbxArchiveQuota = MailboxProviderBase.GetQuotaValue(recipient, ADUserSchema.ArchiveQuota);
				}
				this.mbxHomeMdb = (ADObjectId)recipient[ADMailboxRecipientSchema.Database];
				this.archiveMdb = (((ADObjectId)recipient[ADUserSchema.ArchiveDatabase]) ?? this.mbxHomeMdb);
				this.archiveGuid = recipient.ArchiveGuid;
				MultiValuedProperty<Guid> multiValuedProperty = (MultiValuedProperty<Guid>)recipient[ADUserSchema.AggregatedMailboxGuids];
				if (multiValuedProperty != null && multiValuedProperty.Count > 0)
				{
					this.alternateMailboxes = new Guid[multiValuedProperty.Count];
					int num = 0;
					foreach (Guid guid in multiValuedProperty)
					{
						this.alternateMailboxes[num++] = guid;
					}
				}
			}
			if (this.UseHomeMDB)
			{
				this.MdbId = (this.IsArchiveMailbox ? this.archiveMdb : this.mbxHomeMdb);
				if (this.MdbId == null)
				{
					throw new RecipientIsNotAMailboxPermanentException(this.MailboxId.ToString());
				}
			}
			this.ResolveMDB(false);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0001844C File Offset: 0x0001664C
		protected void ResolveMDB(bool forceRediscovery)
		{
			if (this.MdbId == null)
			{
				if (this.ConfiguredMdbGuid != Guid.Empty)
				{
					Database database = this.FindDatabaseByGuid<Database>(this.ConfiguredMdbGuid);
					this.MdbId = database.Id;
				}
				else if (!string.IsNullOrEmpty(this.ConfiguredMdbName))
				{
					Database database = CommonUtils.FindMdbByName(this.ConfiguredMdbName, this.Credential, this.ConfigDomainControllerName);
					this.MdbId = database.Id;
				}
				else
				{
					this.MdbId = (this.IsArchiveMailbox ? this.archiveMdb : this.mbxHomeMdb);
					if (this.MdbId == null)
					{
						throw new RecipientIsNotAMailboxPermanentException(this.MailboxId.ToString());
					}
				}
			}
			FindServerFlags findServerFlags = FindServerFlags.None;
			if (forceRediscovery)
			{
				findServerFlags |= FindServerFlags.ForceRediscovery;
			}
			DatabaseInformation databaseInformation = MapiUtils.FindServerForMdb(this.MdbGuid, this.ConfigDomainControllerName, this.Credential, findServerFlags);
			this.ServerDN = databaseInformation.ServerDN;
			this.ServerFqdn = databaseInformation.ServerFqdn;
			this.ServerGuid = databaseInformation.ServerGuid;
			this.ServerVersion = databaseInformation.ServerVersion;
			this.ServerMailboxRelease = databaseInformation.MailboxRelease;
			this.ServerDisplayName = this.ServerFqdn;
			if ((this.Flags & LocalMailboxFlags.LocalMachineMapiOnly) != LocalMailboxFlags.None && !databaseInformation.IsOnThisServer)
			{
				throw new MailboxDatabaseNotOnServerTransientException(databaseInformation.DatabaseName, this.MdbGuid, this.ServerFqdn, CommonUtils.LocalComputerName);
			}
			MRSRequestType requestType;
			if (this.Flags.HasFlag(LocalMailboxFlags.Move))
			{
				requestType = MRSRequestType.Move;
			}
			else if (this.Flags.HasFlag(LocalMailboxFlags.AggregatedMailbox) || this.Flags.HasFlag(LocalMailboxFlags.EasSync))
			{
				requestType = MRSRequestType.Sync;
			}
			else if (this.Flags.HasFlag(LocalMailboxFlags.LegacyPublicFolders))
			{
				if (this.Flags.HasFlag(LocalMailboxFlags.ParallelPublicFolderMigration))
				{
					requestType = MRSRequestType.PublicFolderMailboxMigration;
				}
				else
				{
					requestType = MRSRequestType.PublicFolderMigration;
				}
			}
			else if (this.Flags.HasFlag(LocalMailboxFlags.PstImport))
			{
				requestType = MRSRequestType.MailboxImport;
			}
			else if (this.Flags.HasFlag(LocalMailboxFlags.PstExport))
			{
				requestType = MRSRequestType.MailboxExport;
			}
			else if (this.Flags.HasFlag(LocalMailboxFlags.PublicFolderMove))
			{
				requestType = MRSRequestType.PublicFolderMove;
			}
			else if (this.Flags.HasFlag(LocalMailboxFlags.Restore))
			{
				requestType = MRSRequestType.MailboxRestore;
			}
			else if (this.Flags.HasFlag(LocalMailboxFlags.FolderMove))
			{
				requestType = MRSRequestType.FolderMove;
			}
			else
			{
				requestType = MRSRequestType.Merge;
			}
			this.configContext = CommonUtils.CreateConfigContext(this.MailboxGuid, (this.MdbId == null) ? Guid.Empty : this.MdbId.ObjectGuid, this.OrganizationId, RequestWorkloadType.None, requestType, this.GetSyncProtocol());
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00018724 File Offset: 0x00016924
		protected TDatabase FindDatabaseByGuid<TDatabase>(Guid dbGuid) where TDatabase : Database, new()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.ConfigDomainControllerName, true, ConsistencyMode.PartiallyConsistent, this.Credential, ADSessionSettings.FromRootOrgScopeSet(), 2852, "FindDatabaseByGuid", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\Common\\MailboxProviderBase.cs");
			TDatabase tdatabase = topologyConfigurationSession.FindDatabaseByGuid<TDatabase>(dbGuid);
			if (tdatabase == null)
			{
				MrsTracer.Provider.Error("Unable to locate DB by guid {0}", new object[]
				{
					dbGuid
				});
				throw new DatabaseNotFoundByGuidPermanentException(dbGuid);
			}
			return tdatabase;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00018798 File Offset: 0x00016998
		protected void GetCreds(out string userName, out string userDomain, out string userPassword)
		{
			userName = ((this.Credential != null) ? this.Credential.UserName : null);
			userDomain = ((this.Credential != null) ? this.Credential.Domain : null);
			userPassword = ((this.Credential != null) ? this.Credential.Password : null);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00018808 File Offset: 0x00016A08
		protected virtual void DeleteMailboxInternal(int flags)
		{
			MrsTracer.Provider.Function("MapiMailbox.DeleteMailbox({0})", new object[]
			{
				flags
			});
			base.CheckDisposed();
			if (this.IsTitanium)
			{
				flags &= 1;
			}
			using (ExRpcAdmin rpcAdmin = this.GetRpcAdmin())
			{
				MrsTracer.Provider.Debug("Deleting mailbox \"{0}\" in MDB '{1}', flags={2}", new object[]
				{
					this.TraceMailboxId,
					this.TraceMdbId,
					flags
				});
				using (this.RHTracker.Start())
				{
					if (MapiUtils.IsMailboxInDatabase(rpcAdmin, this.MdbGuid, this.MailboxGuid))
					{
						rpcAdmin.DeletePrivateMailbox(this.MdbGuid, this.MailboxGuid, flags);
					}
					else
					{
						MrsTracer.Provider.Debug("Mailbox is already not in MDB, not attempting to delete it.", new object[0]);
					}
					if ((flags & 1) != 0 && (flags & 4) == 0)
					{
						CommonUtils.CatchKnownExceptions(delegate
						{
							this.SeedMBICacheInternal(rpcAdmin);
						}, null);
					}
				}
			}
			((IMailbox)this).Disconnect();
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00018954 File Offset: 0x00016B54
		protected CreateMailboxResult CreateMailbox(byte[] mailboxData, MailboxSignatureFlags sourceSignatureFlags)
		{
			MrsTracer.Provider.Debug("Creating destination mailbox \"{0}\" in MDB {1}{2}", new object[]
			{
				this.TraceMailboxId,
				this.MdbGuid,
				(this.MailboxContainerGuid != null) ? (" in Container " + this.MailboxContainerGuid.Value) : string.Empty
			});
			using (ExRpcAdmin rpcAdmin = this.GetRpcAdmin())
			{
				using (this.RHTracker.Start())
				{
					rpcAdmin.PurgeCachedMailboxObject(this.MailboxGuid);
				}
				uint mailboxSignatureServerVersion = rpcAdmin.GetMailboxSignatureServerVersion();
				if (mailboxSignatureServerVersion >= 102U)
				{
					mailboxData = MailboxSignatureConverter.ConvertTenantHint(mailboxData, sourceSignatureFlags, this.PartitionHint);
				}
				if (this.MailboxContainerGuid != null)
				{
					PartitionInformation.ControlFlags flags = this.Flags.HasFlag(LocalMailboxFlags.CreateNewPartition) ? PartitionInformation.ControlFlags.CreateNewPartition : PartitionInformation.ControlFlags.None;
					PartitionInformation partitionInformation = new PartitionInformation(this.MailboxContainerGuid.Value, flags);
					mailboxData = MailboxSignatureConverter.ConvertPartitionInformation(mailboxData, sourceSignatureFlags, partitionInformation);
				}
				try
				{
					using (this.RHTracker.Start())
					{
						rpcAdmin.SetMailboxBasicInfo(this.MdbGuid, this.MailboxGuid, mailboxData);
					}
				}
				catch (MapiExceptionDuplicateObject ex)
				{
					MrsTracer.Provider.Debug("SetMailboxBasicInfo failed with ecDuplicateObject\n{0}", new object[]
					{
						CommonUtils.FullExceptionMessage(ex)
					});
					return CreateMailboxResult.CleanupNotComplete;
				}
				catch (MapiExceptionFolderNotCleanedUp ex2)
				{
					MrsTracer.Provider.Debug("SetMailboxBasicInfo failed with ecFolderNotCleanedUp\n{0}", new object[]
					{
						CommonUtils.FullExceptionMessage(ex2)
					});
					return CreateMailboxResult.CleanupNotComplete;
				}
				catch (MapiExceptionNotFound ex3)
				{
					MrsTracer.Provider.Debug("SetMailboxBasicInfo failed with ecNotFound\n{0}", new object[]
					{
						CommonUtils.FullExceptionMessage(ex3)
					});
					return CreateMailboxResult.ObjectNotFound;
				}
			}
			return CreateMailboxResult.Success;
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00018BD0 File Offset: 0x00016DD0
		protected void ProcessMailboxSignature(byte[] mailboxData)
		{
			MrsTracer.Provider.Debug("Process destination mailbox signature \"{0}\" in MDB {1}", new object[]
			{
				this.TraceMailboxId,
				this.MdbGuid
			});
			using (ExRpcAdmin rpcAdmin = this.GetRpcAdmin())
			{
				uint mailboxSignatureServerVersion = rpcAdmin.GetMailboxSignatureServerVersion();
				if (mailboxSignatureServerVersion < 103U)
				{
					throw new InputDataIsInvalidPermanentException();
				}
				using (this.RHTracker.Start())
				{
					rpcAdmin.SetMailboxBasicInfo(this.MdbGuid, this.MailboxGuid, mailboxData);
				}
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00018CCB File Offset: 0x00016ECB
		protected void CleanupAdUserAfterDeleteMailbox()
		{
			MrsTracer.Provider.Function("StorageMailbox.CleanupAdUserAfterDeleteMailbox()", new object[0]);
			this.RunADRecipientOperation(false, delegate(IRecipientSession adSession)
			{
				ADUser aduser = adSession.Read(this.MailboxId) as ADUser;
				aduser.Database = null;
				aduser.ServerLegacyDN = null;
				aduser.SetExchangeVersion(null);
				aduser[ADRecipientSchema.RecipientTypeDetails] = null;
				aduser[ADRecipientSchema.RecipientDisplayType] = null;
				adSession.Save(aduser);
			});
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00018D18 File Offset: 0x00016F18
		protected RawSecurityDescriptor GetUserSecurityDescriptor()
		{
			MrsTracer.Provider.Function("IMailbox.GetUserSecurityDescriptor", new object[0]);
			RawSecurityDescriptor sd = null;
			this.RunADRecipientOperation(true, delegate(IRecipientSession adSession)
			{
				sd = adSession.ReadSecurityDescriptor(this.MailboxId);
			});
			return sd;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00018DB4 File Offset: 0x00016FB4
		protected void SetUserSecurityDescriptor(RawSecurityDescriptor sd)
		{
			MrsTracer.Provider.Function("IMailbox.SetUserSecurityDescriptor", new object[0]);
			this.RunADRecipientOperation(false, delegate(IRecipientSession adSession)
			{
				ADUser aduser = adSession.Read(this.MailboxId) as ADUser;
				if (aduser == null)
				{
					throw new RecipientNotFoundPermanentException(this.MailboxGuid);
				}
				aduser.SaveSecurityDescriptor(sd);
			});
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00018E54 File Offset: 0x00017054
		protected void SetMailboxSecurityDescriptor(RawSecurityDescriptor sd)
		{
			if (this.IsE15OrHigher)
			{
				this.RunADRecipientOperation(false, delegate(IRecipientSession adSession)
				{
					ADUser aduser = adSession.Read(this.MailboxId) as ADUser;
					if (aduser == null)
					{
						throw new RecipientNotFoundPermanentException(this.MailboxGuid);
					}
					aduser.ExchangeSecurityDescriptor = sd;
					adSession.Save(aduser);
				});
				using (ExRpcAdmin rpcAdmin = this.GetRpcAdmin())
				{
					using (this.RHTracker.Start())
					{
						rpcAdmin.PurgeCachedMailboxObject(this.MailboxGuid);
					}
					return;
				}
			}
			using (ExRpcAdmin rpcAdmin2 = this.GetRpcAdmin())
			{
				using (this.RHTracker.Start())
				{
					rpcAdmin2.SetMailboxSecurityDescriptor(this.MdbGuid, this.MailboxGuid, sd);
				}
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00018F4C File Offset: 0x0001714C
		private static ulong GetULong(object value)
		{
			if (value is int)
			{
				return (ulong)((long)((int)value));
			}
			return (ulong)((long)value);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00018F64 File Offset: 0x00017164
		private static ulong? GetQuotaValue(MiniRecipient recipient, ADPropertyDefinition quotaProperty)
		{
			Unlimited<ByteQuantifiedSize> unlimited = (Unlimited<ByteQuantifiedSize>)recipient[quotaProperty];
			if (!unlimited.IsUnlimited)
			{
				return new ulong?(unlimited.Value.ToBytes());
			}
			return null;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00019048 File Offset: 0x00017248
		private void SeedMBICacheInternal(ExRpcAdmin rpcAdmin)
		{
			if (this.IsTitanium)
			{
				return;
			}
			ExRpcAdmin exRpcAdmin = null;
			if (rpcAdmin == null)
			{
				exRpcAdmin = this.GetRpcAdmin();
				rpcAdmin = exRpcAdmin;
			}
			try
			{
				MrsTracer.Provider.Debug("Clearing MBI cache entry for {0}", new object[]
				{
					this.MailboxGuid
				});
				using (this.RHTracker.Start())
				{
					rpcAdmin.PurgeCachedMailboxObject(this.MailboxGuid);
				}
				if (!string.IsNullOrEmpty(this.EffectiveDomainControllerName))
				{
					try
					{
						using (this.RHTracker.Start())
						{
							this.RunADRecipientOperation(delegate()
							{
								MrsTracer.Provider.Debug("Seeding DSAccess cache for '{0}' from {1}", new object[]
								{
									this.MailboxDN,
									this.EffectiveDomainControllerName
								});
								rpcAdmin.PrePopulateCache(this.MdbGuid, this.MailboxDN, this.MailboxGuid, (this.PartitionHint != null) ? TenantPartitionHint.Serialize(this.PartitionHint) : null, this.EffectiveDomainControllerName);
							});
						}
					}
					catch (MapiExceptionVersion)
					{
						MrsTracer.Provider.Debug("The PrePopulateCache API is supported only by E14 and higher version of Store.", new object[0]);
					}
				}
			}
			finally
			{
				if (exRpcAdmin != null)
				{
					exRpcAdmin.Dispose();
				}
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00019178 File Offset: 0x00017378
		private LocalizedException ClassifyWrapAndReturnPowershellException(Exception ex)
		{
			if (ex is MonadDataAdapterInvocationException && ((MonadDataAdapterInvocationException)ex).ErrorRecord != null && ((MonadDataAdapterInvocationException)ex).ErrorRecord.Exception != null)
			{
				ex = ((MonadDataAdapterInvocationException)ex).ErrorRecord.Exception;
			}
			else if (ex is CmdletInvocationException && ((CmdletInvocationException)ex).ErrorRecord != null && ((CmdletInvocationException)ex).ErrorRecord.Exception != null)
			{
				ex = ((CmdletInvocationException)ex).ErrorRecord.Exception;
			}
			if (CommonUtils.IsTransientException(ex))
			{
				return new UpdateMovedMailboxTransientException(ex);
			}
			return new UpdateMovedMailboxPermanentException(ex);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0001920E File Offset: 0x0001740E
		protected IRecipientSession GetRecipientSession(bool readOnly)
		{
			return this.GetRecipientSession(readOnly, false);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00019218 File Offset: 0x00017418
		protected void SetMailboxSyncState(string syncStateStr)
		{
			MrsTracer.Provider.Function("MailboxProviderBase.SetMailboxSyncState", new object[0]);
			this.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			this.ValidateIfSourceMailbox("MailboxProviderBase.SetMailboxSyncState");
			MapiSyncState mapiSyncState = MapiSyncState.Deserialize(syncStateStr);
			if (mapiSyncState == null)
			{
				MrsTracer.Provider.Debug("Using empty sync state", new object[0]);
				mapiSyncState = new MapiSyncState();
			}
			this.SyncState = mapiSyncState;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00019278 File Offset: 0x00017478
		protected string GetMailboxSyncState()
		{
			MrsTracer.Provider.Function("MailboxProviderBase.SerializeMailboxSyncState", new object[0]);
			this.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			this.ValidateIfSourceMailbox("MailboxProviderBase.SerializeMailboxSyncState");
			if (this.syncState == null)
			{
				return null;
			}
			return this.syncState.Serialize(false);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x000192B8 File Offset: 0x000174B8
		protected void VerifyRestoreSource(MailboxConnectFlags mailboxConnectFlags)
		{
			if (!this.IsRestore || mailboxConnectFlags.HasFlag(MailboxConnectFlags.AllowRestoreFromConnectedMailbox))
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			MailboxDatabase mailboxDatabase = CommonUtils.FindMdbByGuid(this.MdbGuid, null, null);
			flag3 = mailboxDatabase.Recovery;
			if (!flag3)
			{
				using (ExRpcAdmin rpcAdmin = this.GetRpcAdmin())
				{
					flag = MapiUtils.IsStoreDisconnectedMailbox(rpcAdmin, this.MdbGuid, this.MailboxGuid);
				}
			}
			if (!flag && !flag3 && this.PartitionHint != null)
			{
				IRecipientSession recipientSession = CommonUtils.CreateRecipientSession(this.PartitionHint.GetExternalDirectoryOrganizationId(), null, null);
				ADRecipient adrecipient = recipientSession.FindByExchangeGuidIncludingArchive(this.MailboxGuid);
				flag2 = (adrecipient != null && adrecipient.IsSoftDeleted);
			}
			if (!flag && !flag2 && !flag3)
			{
				throw new RestoringConnectedMailboxPermanentException(this.MailboxGuid);
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00019390 File Offset: 0x00017590
		private IRecipientSession GetRecipientSession(bool readOnly, bool rootOrgScoped)
		{
			ADSessionSettings adsessionSettings;
			if (rootOrgScoped || this.PartitionHint == null)
			{
				adsessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			}
			else
			{
				adsessionSettings = ADSessionSettings.FromTenantPartitionHint(this.PartitionHint);
			}
			adsessionSettings.IncludeSoftDeletedObjects = true;
			adsessionSettings.IncludeInactiveMailbox = true;
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.EffectiveDomainControllerName, readOnly, ConsistencyMode.PartiallyConsistent, this.Credential, adsessionSettings, 3435, "GetRecipientSession", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\Common\\MailboxProviderBase.cs");
			if (rootOrgScoped)
			{
				tenantOrRootOrgRecipientSession.EnforceDefaultScope = false;
			}
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00019400 File Offset: 0x00017600
		private ServerHealthStatus CheckServerHealthInternal()
		{
			ServerHealthStatus serverHealthStatus = new ServerHealthStatus(ServerHealthState.Healthy);
			if (this.MbxType == MailboxType.SourceMailbox)
			{
				return serverHealthStatus;
			}
			ILegacyResourceHealthProvider legacyResourceHealthProvider = ResourceHealthMonitorManager.Singleton.Get(new LegacyResourceHealthMonitorKey(this.MdbGuid)) as ILegacyResourceHealthProvider;
			TimeSpan timeSpan;
			LocalizedString localizedString;
			ConstraintCheckAgent constraintCheckAgent;
			ConstraintCheckResultType constraintCheckResultType = CommonUtils.DumpsterStatus.CheckReplicationHealthConstraint(this.MdbGuid, out timeSpan, out localizedString, out constraintCheckAgent);
			if (constraintCheckResultType != ConstraintCheckResultType.Satisfied)
			{
				MrsTracer.Provider.Warning("Move for mailbox '{0}' is stalled because DataMoveReplicationConstraint is not satisfied for the target database '{1}'. Failure Reason: {2}, agent: {3}", new object[]
				{
					this.TraceMailboxId,
					this.TraceMdbId,
					localizedString,
					constraintCheckAgent
				});
				serverHealthStatus.HealthState = ServerHealthState.NotHealthy;
				serverHealthStatus.Agent = constraintCheckAgent;
				serverHealthStatus.FailureReason = MrsStrings.MoveIsStalled(this.TraceMailboxId, this.TraceMdbId, localizedString, constraintCheckAgent.ToString());
			}
			if (legacyResourceHealthProvider != null)
			{
				legacyResourceHealthProvider.Update(constraintCheckResultType, constraintCheckAgent, localizedString);
			}
			return serverHealthStatus;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x000194D8 File Offset: 0x000176D8
		private void CopyMessagesIndividually(IEnumerable<MessageRec> messages, IFxProxyPool proxyPool, PropTag[] propsToCopyExplicitly, PropTag[] excludeProps, Action<MessageRec> changeSourceFolderAction)
		{
			IFolderProxy folderProxy = null;
			byte[] eid = null;
			try
			{
				foreach (MessageRec messageRec in messages)
				{
					if (!CommonUtils.IsSameEntryId(messageRec.FolderId, eid))
					{
						if (folderProxy != null)
						{
							folderProxy.Dispose();
							folderProxy = null;
						}
						folderProxy = proxyPool.GetFolderProxy(messageRec.FolderId);
						eid = messageRec.FolderId;
						if (changeSourceFolderAction != null)
						{
							changeSourceFolderAction(messageRec);
						}
					}
					this.CopySingleMessage(messageRec, folderProxy, propsToCopyExplicitly, excludeProps);
				}
			}
			finally
			{
				if (folderProxy != null)
				{
					folderProxy.Dispose();
				}
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0001957C File Offset: 0x0001777C
		private void ValidateIfSourceMailbox(string methodName)
		{
			this.MbxType.Equals(MailboxType.SourceMailbox);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00019598 File Offset: 0x00017798
		private MailboxChangesManifest RunICSManifestSync(bool catchup, SyncHierarchyManifestState hierState, MapiStore mapiStore)
		{
			MrsTracer.Provider.Function("MailboxProviderBase.RunICSManifestSync", new object[0]);
			MailboxChangesManifest mailboxChangesManifest = new MailboxChangesManifest();
			MailboxProviderBase.ManifestHierarchyCallback iMapiManifestCallback = new MailboxProviderBase.ManifestHierarchyCallback(catchup, mailboxChangesManifest, this.publicFoldersToSkip, this.IsPublicFolderMigrationSource);
			using (this.RHTracker.Start())
			{
				using (MapiFolder rootFolder = mapiStore.GetRootFolder())
				{
					SyncConfigFlags syncConfigFlags = SyncConfigFlags.ManifestHierReturnDeletedEntryIds;
					if (((this.ServerVersion >= Server.E14MinVersion && this.ServerVersion < Server.E15MinVersion) || (long)this.ServerVersion >= MailboxProviderBase.E15MinVersionSupportsOnlySpecifiedPropsForHierarchy) && !this.IsPureMAPI)
					{
						syncConfigFlags |= SyncConfigFlags.OnlySpecifiedProps;
					}
					if (catchup && this.isStorageProvider)
					{
						syncConfigFlags |= SyncConfigFlags.Catchup;
					}
					PropTag[] tagsInclude = MailboxProviderBase.PropTagsForRegularMoves;
					if (this.IsPublicFolderMigrationSource)
					{
						if (syncConfigFlags.HasFlag(SyncConfigFlags.OnlySpecifiedProps))
						{
							tagsInclude = MailboxProviderBase.PropTagsForPublicFolderMigration;
						}
						else
						{
							syncConfigFlags |= SyncConfigFlags.NoForeignKeys;
						}
					}
					using (MapiHierarchyManifestEx mapiHierarchyManifestEx = rootFolder.CreateExportHierarchyManifestEx(syncConfigFlags, hierState.IdsetGiven, hierState.CnsetSeen, iMapiManifestCallback, tagsInclude, null))
					{
						while (mapiHierarchyManifestEx.Synchronize() != ManifestStatus.Done)
						{
						}
						byte[] idsetGiven;
						byte[] cnsetSeen;
						mapiHierarchyManifestEx.GetState(out idsetGiven, out cnsetSeen);
						hierState.IdsetGiven = idsetGiven;
						hierState.CnsetSeen = cnsetSeen;
					}
				}
			}
			return mailboxChangesManifest;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00019708 File Offset: 0x00017908
		protected virtual MailboxChangesManifest EnumerateHierarchyChanges(bool catchup, Func<SyncHierarchyManifestState, MailboxChangesManifest> hierarchySyncAction)
		{
			MrsTracer.Provider.Function("MailboxProviderBase.EnumerateHierarchyChanges", new object[0]);
			this.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			this.ValidateIfSourceMailbox("MailboxProviderBase.EnumerateHierarchyChanges");
			MailboxChangesManifest mailboxChangesManifest = new MailboxChangesManifest();
			if (hierarchySyncAction != null)
			{
				mailboxChangesManifest = hierarchySyncAction(this.SyncState.HierarchyData);
			}
			if (mailboxChangesManifest.DeletedFolders != null && mailboxChangesManifest.DeletedFolders.Count > 0)
			{
				foreach (byte[] folderId in mailboxChangesManifest.DeletedFolders)
				{
					this.SyncState.RemoveContentsManifestState(folderId);
				}
			}
			if (catchup)
			{
				return null;
			}
			return mailboxChangesManifest;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000197C0 File Offset: 0x000179C0
		protected virtual void AfterConnect()
		{
			if (this.SupportsSavingSyncState)
			{
				this.syncState = new MapiSyncState();
			}
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000197D8 File Offset: 0x000179D8
		protected virtual MailboxChangesManifest DoManifestSync(EnumerateHierarchyChangesFlags flags, int maxChanges, SyncHierarchyManifestState hierState, MapiStore mapiStore)
		{
			MrsTracer.Provider.Function("MailboxProviderBase.DoManifestSync", new object[0]);
			this.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			this.ValidateIfSourceMailbox("MailboxProviderBase.DoManifestSync");
			MailboxChangesManifest mailboxChangesManifest = new MailboxChangesManifest();
			bool flag = flags.HasFlag(EnumerateHierarchyChangesFlags.Catchup);
			bool flag2 = flag || hierState.ManualSyncData == null;
			if (flag2)
			{
				try
				{
					bool flag3 = maxChanges != 0;
					if (flag3)
					{
						if (flags.HasFlag(EnumerateHierarchyChangesFlags.FirstPage))
						{
							if (this.hierarchyChangesFetcher != null)
							{
								this.hierarchyChangesFetcher.Dispose();
							}
							this.hierarchyChangesFetcher = new ManifestHierarchyChangesFetcher(mapiStore, this, true);
						}
						mailboxChangesManifest = this.hierarchyChangesFetcher.EnumerateHierarchyChanges(hierState, flags, maxChanges);
					}
					else
					{
						mailboxChangesManifest = this.RunICSManifestSync(flag, hierState, mapiStore);
					}
				}
				catch (MapiExceptionNotFound ex)
				{
					if (!flag)
					{
						throw;
					}
					MrsTracer.Provider.Warning("Got ecNotFound during ICS hierarchy catchup, will try manual. {0}", new object[]
					{
						CommonUtils.FullExceptionMessage(ex, true)
					});
					flag2 = false;
				}
			}
			if (!flag2)
			{
				mailboxChangesManifest = this.RunManualHierarchySync(flag, hierState);
			}
			if (!flag)
			{
				MrsTracer.Provider.Debug("Changes discovered: {0} changed folders, {1} deleted folders.", new object[]
				{
					mailboxChangesManifest.ChangedFolders.Count,
					mailboxChangesManifest.DeletedFolders.Count
				});
			}
			return mailboxChangesManifest;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00019930 File Offset: 0x00017B30
		protected virtual MailboxChangesManifest RunManualHierarchySync(bool catchup, SyncHierarchyManifestState hierState)
		{
			MrsTracer.Provider.Function("MailboxProviderBase.RunManualHierarchySync", new object[0]);
			this.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			this.ValidateIfSourceMailbox("MailboxProviderBase.RunManualHierarchySync");
			MailboxChangesManifest mailboxChangesManifest = new MailboxChangesManifest();
			List<FolderRec> list = ((IMailbox)this).EnumerateFolderHierarchy(EnumerateFolderHierarchyFlags.None, null);
			EntryIdMap<SyncHierarchyManifestState.FolderData> entryIdMap = new EntryIdMap<SyncHierarchyManifestState.FolderData>();
			List<SyncHierarchyManifestState.FolderData> list2 = new List<SyncHierarchyManifestState.FolderData>();
			foreach (FolderRec folderRec in list)
			{
				if (folderRec.FolderType != FolderType.Search)
				{
					SyncHierarchyManifestState.FolderData folderData = new SyncHierarchyManifestState.FolderData(folderRec);
					entryIdMap[folderData.EntryId] = folderData;
					list2.Add(folderData);
				}
			}
			if (!catchup)
			{
				EntryIdMap<SyncHierarchyManifestState.FolderData> entryIdMap2 = new EntryIdMap<SyncHierarchyManifestState.FolderData>();
				SyncHierarchyManifestState.FolderData[] manualSyncData = hierState.ManualSyncData;
				foreach (SyncHierarchyManifestState.FolderData folderData2 in hierState.ManualSyncData)
				{
					entryIdMap2[folderData2.EntryId] = folderData2;
				}
				mailboxChangesManifest.DeletedFolders = new List<byte[]>();
				foreach (byte[] array in entryIdMap2.Keys)
				{
					if (!entryIdMap.ContainsKey(array))
					{
						mailboxChangesManifest.DeletedFolders.Add(array);
					}
				}
				mailboxChangesManifest.ChangedFolders = new List<byte[]>();
				foreach (FolderRec folderRec2 in list)
				{
					if (folderRec2.FolderType != FolderType.Search)
					{
						SyncHierarchyManifestState.FolderData folderData3;
						if (entryIdMap2.TryGetValue(folderRec2.EntryId, out folderData3))
						{
							if (!CommonUtils.IsSameEntryId(folderData3.ParentId, folderRec2.ParentId) || folderRec2.LastModifyTimestamp > folderData3.LastModifyTimestamp)
							{
								mailboxChangesManifest.ChangedFolders.Add(folderRec2.EntryId);
							}
						}
						else
						{
							mailboxChangesManifest.ChangedFolders.Add(folderRec2.EntryId);
						}
					}
				}
			}
			hierState.ManualSyncData = list2.ToArray();
			return mailboxChangesManifest;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00019B48 File Offset: 0x00017D48
		public virtual List<ItemPropertiesBase> GetMailboxSettings(GetMailboxSettingsFlags flags)
		{
			return null;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00019B4C File Offset: 0x00017D4C
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.hierarchyChangesFetcher != null)
				{
					this.hierarchyChangesFetcher.Dispose();
					this.hierarchyChangesFetcher = null;
				}
				if (this.rhTracker != null)
				{
					this.rhTracker.Dispose();
					this.rhTracker = null;
				}
				if (this.reservation != null)
				{
					this.reservation.Deactivate(this.MailboxGuid);
					this.reservation = null;
				}
			}
		}

		// Token: 0x04000662 RID: 1634
		public const string RootPublicFolderName = "Public Root";

		// Token: 0x04000663 RID: 1635
		public const string SyncStateFolderName = "MailboxReplicationService SyncStates";

		// Token: 0x04000664 RID: 1636
		public const string UpdateSucceededVar = "UMM_UpdateSucceeded";

		// Token: 0x04000665 RID: 1637
		public const string DcNameVar = "UMM_DCName";

		// Token: 0x04000666 RID: 1638
		public const string ReportEntriesVar = "UMM_ReportEntries";

		// Token: 0x04000667 RID: 1639
		protected const string SyncStateMessageClass = "IPM.MS-Exchange.MailboxSyncState";

		// Token: 0x04000668 RID: 1640
		public static readonly StorePropertyDefinition SyncStateStorePropertyDefinition = ItemSchema.TextBody;

		// Token: 0x04000669 RID: 1641
		protected static readonly PropTag[] MailboxInformationPropertyTags = new PropTag[]
		{
			PropTag.ContentCount,
			PropTag.DeletedMsgCount,
			PropTag.AssocContentCount,
			PropTag.DeletedAssocMsgCount,
			PropTag.MessageSizeExtended,
			PropTag.DeletedMessageSizeExtended,
			PropTag.AssocMessageSizeExtended,
			PropTag.DeleteAssocMessageSizeExtended,
			PropTag.MailboxPartitionMailboxGuids
		};

		// Token: 0x0400066A RID: 1642
		private static readonly long E15MinVersionSupportsOnlySpecifiedPropsForHierarchy = (long)new ServerVersion(15, 0, 922, 0).ToInt();

		// Token: 0x0400066B RID: 1643
		protected static readonly byte[] NullFolderKey = Array<byte>.Empty;

		// Token: 0x0400066C RID: 1644
		private static readonly ADPropertyDefinition[] UserPropertiesToLoad = new ADPropertyDefinition[]
		{
			ADRecipientSchema.RecipientDisplayType,
			ADMailboxRecipientSchema.UseDatabaseQuotaDefaults,
			ADMailboxRecipientSchema.ProhibitSendReceiveQuota,
			ADUserSchema.RecoverableItemsQuota,
			ADUserSchema.ArchiveQuota,
			ADMailboxRecipientSchema.Database,
			ADUserSchema.ArchiveDatabase,
			ADUserSchema.AggregatedMailboxGuids,
			ADRecipientSchema.AllowedAttributesEffective,
			ADUserSchema.PrimaryMailboxSource
		};

		// Token: 0x0400066D RID: 1645
		private static readonly ADPropertyDefinition[] WriteableProperties = new ADPropertyDefinition[]
		{
			ADMailboxRecipientSchema.Database,
			ADMailboxRecipientSchema.ServerLegacyDN
		};

		// Token: 0x0400066E RID: 1646
		private static readonly PropTag[] PropTagsForPublicFolderMigration = new PropTag[]
		{
			PropTag.EntryId,
			PropTag.ParentEntryId
		};

		// Token: 0x0400066F RID: 1647
		private static readonly PropTag[] PropTagsForRegularMoves = new PropTag[]
		{
			PropTag.EntryId
		};

		// Token: 0x04000670 RID: 1648
		private static EventHandler<ErrorReportEventArgs> ummErrorReportHandler = delegate(object sender, ErrorReportEventArgs args)
		{
			args.Handled = true;
		};

		// Token: 0x04000671 RID: 1649
		private MailboxReservation reservation;

		// Token: 0x04000672 RID: 1650
		private SettingsContextBase configContext;

		// Token: 0x04000673 RID: 1651
		private IHierarchyChangesFetcher hierarchyChangesFetcher;

		// Token: 0x04000674 RID: 1652
		protected EntryIdMap<bool> publicFoldersToSkip;

		// Token: 0x04000675 RID: 1653
		protected WellKnownPrincipalMapper wkpMapper;

		// Token: 0x04000676 RID: 1654
		protected int recipientType;

		// Token: 0x04000677 RID: 1655
		protected int recipientDisplayType;

		// Token: 0x04000678 RID: 1656
		protected long recipientTypeDetails;

		// Token: 0x04000679 RID: 1657
		protected bool? useMdbQuotaDefaults;

		// Token: 0x0400067A RID: 1658
		protected ulong? mbxQuota;

		// Token: 0x0400067B RID: 1659
		protected ulong? mbxDumpsterQuota;

		// Token: 0x0400067C RID: 1660
		protected ulong? mbxArchiveQuota;

		// Token: 0x0400067D RID: 1661
		protected Guid archiveGuid;

		// Token: 0x0400067E RID: 1662
		protected Guid[] alternateMailboxes;

		// Token: 0x0400067F RID: 1663
		protected ADObjectId mbxHomeMdb;

		// Token: 0x04000680 RID: 1664
		protected ADObjectId archiveMdb;

		// Token: 0x04000681 RID: 1665
		protected ResourceHealthTracker rhTracker;

		// Token: 0x04000682 RID: 1666
		protected string preferredDomainControllerName;

		// Token: 0x04000683 RID: 1667
		protected MapiSyncState syncState;

		// Token: 0x04000684 RID: 1668
		protected bool connectedWithoutMailboxSession;

		// Token: 0x04000685 RID: 1669
		protected bool isStorageProvider;

		// Token: 0x0200014C RID: 332
		private class ManifestHierarchyCallback : IMapiHierarchyManifestCallback
		{
			// Token: 0x06000BA5 RID: 2981 RVA: 0x00019D20 File Offset: 0x00017F20
			public ManifestHierarchyCallback(bool catchup, MailboxChangesManifest changes, EntryIdMap<bool> foldersToSkip, bool isPublicFolderMigration)
			{
				this.catchup = catchup;
				this.changes = changes;
				this.changes.ChangedFolders = new List<byte[]>(0);
				this.changes.DeletedFolders = new List<byte[]>(0);
				this.foldersToSkip = foldersToSkip;
				this.isPublicFolderMigration = isPublicFolderMigration;
			}

			// Token: 0x06000BA6 RID: 2982 RVA: 0x00019D74 File Offset: 0x00017F74
			ManifestCallbackStatus IMapiHierarchyManifestCallback.Change(PropValue[] props)
			{
				if (this.catchup)
				{
					return ManifestCallbackStatus.Continue;
				}
				byte[] array = null;
				byte[] parentId = null;
				foreach (PropValue propValue in props)
				{
					if (propValue.PropTag == PropTag.EntryId)
					{
						array = propValue.GetBytes();
					}
					else if (propValue.PropTag == PropTag.ParentEntryId)
					{
						parentId = propValue.GetBytes();
					}
				}
				if (this.ShouldSkipFolder(array, parentId))
				{
					this.foldersToSkip[array] = true;
				}
				else
				{
					this.changes.ChangedFolders.Add(array);
				}
				return ManifestCallbackStatus.Continue;
			}

			// Token: 0x06000BA7 RID: 2983 RVA: 0x00019E0A File Offset: 0x0001800A
			ManifestCallbackStatus IMapiHierarchyManifestCallback.Delete(byte[] entryId)
			{
				if (this.catchup)
				{
					return ManifestCallbackStatus.Continue;
				}
				this.changes.DeletedFolders.Add(entryId);
				return ManifestCallbackStatus.Continue;
			}

			// Token: 0x06000BA8 RID: 2984 RVA: 0x00019E28 File Offset: 0x00018028
			private bool ShouldSkipFolder(byte[] entryId, byte[] parentId)
			{
				return this.foldersToSkip.ContainsKey(entryId) || (this.isPublicFolderMigration && parentId != null && this.foldersToSkip.ContainsKey(parentId));
			}

			// Token: 0x040006A1 RID: 1697
			private readonly bool catchup;

			// Token: 0x040006A2 RID: 1698
			private readonly bool isPublicFolderMigration;

			// Token: 0x040006A3 RID: 1699
			private MailboxChangesManifest changes;

			// Token: 0x040006A4 RID: 1700
			private EntryIdMap<bool> foldersToSkip;
		}
	}
}
