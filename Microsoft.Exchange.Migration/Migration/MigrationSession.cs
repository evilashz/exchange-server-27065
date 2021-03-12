using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Migration;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200007E RID: 126
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationSession : MigrationPersistableBase, IMigrationConfig
	{
		// Token: 0x06000706 RID: 1798 RVA: 0x00020443 File Offset: 0x0001E643
		protected MigrationSession()
		{
			this.Jobs = new ConcurrentDictionary<Guid, MigrationJob>();
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x00020456 File Offset: 0x0001E656
		public IMigrationConfig Config
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x000204BC File Offset: 0x0001E6BC
		public int RunnableJobCount
		{
			get
			{
				this.CheckQueueInitialization();
				ExDateTime utcNow = ExDateTime.UtcNow;
				return this.Jobs.Values.Count((MigrationJob job) => MigrationSession.RunnableJobStatuses.Contains(job.Status) && job.NextProcessTime <= utcNow && MigrationApplication.IsMigrationTypeEnabled(job.MigrationType));
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x000204FC File Offset: 0x0001E6FC
		public int ActiveJobCount
		{
			get
			{
				int num = 0;
				foreach (MigrationJob migrationJob in this.Jobs.Values)
				{
					if (migrationJob.Status != MigrationJobStatus.Removed && migrationJob.Status != MigrationJobStatus.Failed && migrationJob.Status != MigrationJobStatus.Corrupted)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0002056C File Offset: 0x0001E76C
		public int TotalJobCount
		{
			get
			{
				this.CheckQueueInitialization();
				return this.Jobs.Count;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x00020580 File Offset: 0x0001E780
		public override long CurrentSupportedVersion
		{
			get
			{
				long config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<long>("SessionCurrentVersion");
				long maximumSupportedVersion = this.MaximumSupportedVersion;
				MigrationLogger.Log(MigrationEventType.Verbose, "Current configured version is '{0}' max is {1}", new object[]
				{
					config,
					maximumSupportedVersion
				});
				return Math.Min(config, maximumSupportedVersion);
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x000205CB File Offset: 0x0001E7CB
		public override long MaximumSupportedVersion
		{
			get
			{
				return 5L;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x000205CF File Offset: 0x0001E7CF
		public override long MinimumSupportedVersion
		{
			get
			{
				return 1L;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x000205D3 File Offset: 0x0001E7D3
		public long SupportedVersionUpgrade
		{
			get
			{
				if (!this.CanBeOverwritten)
				{
					return base.Version;
				}
				return this.CurrentSupportedVersion;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x000205EC File Offset: 0x0001E7EC
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x0002062A File Offset: 0x0001E82A
		public int MaxNumberOfBatches
		{
			get
			{
				int? num = base.ExtendedProperties.Get<int?>("MaxNumberOfBatches", null);
				if (num != null)
				{
					return num.Value;
				}
				return this.GetMaxNumberOfBatchesDefault();
			}
			set
			{
				base.ExtendedProperties.Set<int?>("MaxNumberOfBatches", new int?(value));
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x00020644 File Offset: 0x0001E844
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
				{
					base.PropertyDefinitions,
					new StorePropertyDefinition[]
					{
						MigrationBatchMessageSchema.MigrationRuntimeJobData
					}
				});
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0002067A File Offset: 0x0001E87A
		public bool HasJobs
		{
			get
			{
				return this.Jobs.Count > 0;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0002068C File Offset: 0x0001E88C
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x000206D5 File Offset: 0x0001E8D5
		public MigrationFeature EnabledFeatures
		{
			get
			{
				MigrationFeature migrationFeature = base.ExtendedProperties.Get<MigrationFeature>("EnabledFeatures", MigrationFeature.None);
				if (migrationFeature == MigrationFeature.None && base.Version >= 2L)
				{
					migrationFeature = ConfigBase<MigrationServiceConfigSchema>.GetConfig<MigrationFeature>("PublishedMigrationFeatures");
					base.ExtendedProperties.Set<MigrationFeature>("EnabledFeatures", migrationFeature);
				}
				return migrationFeature;
			}
			set
			{
				base.ExtendedProperties.Set<MigrationFeature>("EnabledFeatures", value);
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x000206E8 File Offset: 0x0001E8E8
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x000206FF File Offset: 0x0001E8FF
		public ExDateTime LastUpgradeConstraintEnforcedTimestamp
		{
			get
			{
				return base.ExtendedProperties.Get<ExDateTime>("LastUpdateConstraintCheckTimestamp", ExDateTime.MinValue);
			}
			private set
			{
				base.ExtendedProperties.Set<ExDateTime>("LastUpdateConstraintCheckTimestamp", value);
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00020712 File Offset: 0x0001E912
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x00020725 File Offset: 0x0001E925
		public MigrationBatchFlags BatchFlags
		{
			get
			{
				return base.ExtendedProperties.Get<MigrationBatchFlags>("MigrationBatchFlags", MigrationBatchFlags.None);
			}
			private set
			{
				base.ExtendedProperties.Set<MigrationBatchFlags>("MigrationBatchFlags", value);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00020738 File Offset: 0x0001E938
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x00020788 File Offset: 0x0001E988
		public Unlimited<int> MaxConcurrentMigrations
		{
			get
			{
				string text = base.ExtendedProperties.Get<string>("MaxConcurrentMigrations", string.Empty);
				Unlimited<int> result;
				if (!string.IsNullOrWhiteSpace(text) && Unlimited<int>.TryParse(text, out result))
				{
					return result;
				}
				if (!MigrationServiceFactory.Instance.IsMultiTenantEnabled())
				{
					return Unlimited<int>.UnlimitedValue;
				}
				return new Unlimited<int>(100);
			}
			set
			{
				base.ExtendedProperties.Set<string>("MaxConcurrentMigrations", value.ToString());
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x000207A7 File Offset: 0x0001E9A7
		internal bool CanBeOverwritten
		{
			get
			{
				return !this.HasJobs;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x000207B4 File Offset: 0x0001E9B4
		private static HashSet<MigrationJobStatus> RunnableJobStatuses
		{
			get
			{
				if (MigrationSession.runnableJobStatuses == null)
				{
					HashSet<MigrationJobStatus> hashSet = new HashSet<MigrationJobStatus>();
					foreach (MigrationSession.JobStage jobStage in MigrationSession.RunnableJobStages)
					{
						foreach (MigrationJobStatus item in jobStage.SupportedStatuses)
						{
							hashSet.Add(item);
						}
					}
					MigrationSession.runnableJobStatuses = hashSet;
				}
				return MigrationSession.runnableJobStatuses;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0002081F File Offset: 0x0001EA1F
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x00020831 File Offset: 0x0001EA31
		private ExDateTime? InitializationTime
		{
			get
			{
				return base.ExtendedProperties.Get<ExDateTime?>("InitializationTime");
			}
			set
			{
				base.ExtendedProperties.Set<ExDateTime?>("InitializationTime", value);
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00020844 File Offset: 0x0001EA44
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x0002084C File Offset: 0x0001EA4C
		private ConcurrentDictionary<Guid, MigrationJob> Jobs { get; set; }

		// Token: 0x06000721 RID: 1825 RVA: 0x00020855 File Offset: 0x0001EA55
		public static MigrationSession Get(IMigrationDataProvider dataProvider)
		{
			return MigrationSession.Get(dataProvider, true);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00020860 File Offset: 0x0001EA60
		public static MigrationSession Get(IMigrationDataProvider dataProvider, bool intializeQueue)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			MigrationSession migrationSession = new MigrationSession();
			if (!migrationSession.TryLoad(dataProvider, dataProvider.Folder.Id))
			{
				migrationSession.ReinitializeSession();
				migrationSession.Create(dataProvider);
			}
			if (intializeQueue)
			{
				migrationSession.RefreshJobQueueIfNeeded(dataProvider);
			}
			return migrationSession;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000208AA File Offset: 0x0001EAAA
		public static IMigrationConfig GetConfig(IMigrationDataProvider dataProvider)
		{
			return MigrationSession.Get(dataProvider).Config;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000208B7 File Offset: 0x0001EAB7
		public static bool SupportsCutover(IMigrationDataProvider dataProvider)
		{
			return dataProvider.ADProvider.IsMSOSyncEnabled && !dataProvider.ADProvider.IsDirSyncEnabled;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x000208D8 File Offset: 0x0001EAD8
		public MigrationStatistics GetMigrationStatistics(IMigrationDataProvider provider)
		{
			MigrationStatistics migrationStatistics = new MigrationStatistics();
			migrationStatistics.Identity = new MigrationStatisticsId(provider.OrganizationId);
			migrationStatistics.TotalCount = 0;
			migrationStatistics.FinalizedCount = 0;
			migrationStatistics.ProvisionedCount = 0;
			migrationStatistics.SyncedCount = 0;
			migrationStatistics.FailedCount = 0;
			migrationStatistics.ActiveCount = 0;
			migrationStatistics.StoppedCount = 0;
			migrationStatistics.PendingCount = 0;
			migrationStatistics.MigrationType = MigrationType.None;
			foreach (MigrationJob migrationJob in this.Jobs.Values)
			{
				migrationStatistics.MigrationType |= migrationJob.MigrationType;
				migrationStatistics.TotalCount += migrationJob.TotalCount;
				migrationStatistics.SyncedCount += migrationJob.SyncedItemCount;
				migrationStatistics.FinalizedCount += migrationJob.FinalizedItemCount;
				migrationStatistics.ActiveCount += migrationJob.ActiveItemCount;
				migrationStatistics.StoppedCount += migrationJob.StoppedItemCount;
				migrationStatistics.ProvisionedCount += migrationJob.ProvisionedItemCount;
				migrationStatistics.FailedCount += migrationJob.FailedItemCount;
				migrationStatistics.PendingCount += migrationJob.PendingCount;
			}
			return migrationStatistics;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00020A2C File Offset: 0x0001EC2C
		public MigrationConfig GetMigrationConfig(IMigrationDataProvider provider)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationConfig migrationConfig = new MigrationConfig();
			migrationConfig.Identity = new MigrationConfigId(provider.OrganizationId);
			migrationConfig.MaxNumberOfBatches = this.MaxNumberOfBatches;
			migrationConfig.SupportsCutover = MigrationSession.SupportsCutover(provider);
			migrationConfig.MaxConcurrentMigrations = this.MaxConcurrentMigrations;
			migrationConfig.CanSubmitNewBatch = true;
			if (!provider.ADProvider.IsMigrationEnabled || this.MaxNumberOfBatches <= this.Jobs.Count)
			{
				migrationConfig.CanSubmitNewBatch = false;
			}
			migrationConfig.Features = MigrationFeature.None;
			foreach (MigrationFeature migrationFeature in MigrationSession.FeatureVersionMap.Keys)
			{
				if (this.IsSupported(migrationFeature))
				{
					migrationConfig.Features |= migrationFeature;
				}
			}
			return migrationConfig;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00020B10 File Offset: 0x0001ED10
		public string GetJobName(Guid jobId)
		{
			return this.GetJob(null, jobId, true).JobName;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00020B20 File Offset: 0x0001ED20
		public bool IsSupported(MigrationFeature features)
		{
			long num = (base.Version == -1L) ? this.CurrentSupportedVersion : base.Version;
			MigrationFeature enabledFeatures = this.EnabledFeatures;
			return !MigrationUtil.IsFeatureBlocked(features) && num >= MigrationSession.GetMinimumSupportedVersion(features) && (enabledFeatures & features) == features;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00020B68 File Offset: 0x0001ED68
		public bool IsDisplaySupported(MigrationFeature features)
		{
			return this.MaximumSupportedVersion >= MigrationSession.GetMinimumSupportedVersion(features);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00020ECC File Offset: 0x0001F0CC
		public IEnumerable<MigrationJob> FindJobsToPickUp(IMigrationDataProvider dataProvider)
		{
			this.CheckQueueInitialization();
			this.RefreshJobQueueIfNeeded(dataProvider);
			List<MigrationJob> localJobQueue = new List<MigrationJob>(from job in this.Jobs.Values
			orderby job.NextProcessTime
			select job);
			ExDateTime utcNow = ExDateTime.UtcNow;
			foreach (MigrationSession.JobStage stage in MigrationSession.RunnableJobStages)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "looking for jobs in stage:{0}", new object[]
				{
					stage.Name
				});
				foreach (MigrationJob job2 in localJobQueue)
				{
					if (job2 != null)
					{
						if (!MigrationApplication.IsMigrationTypeEnabled(job2.MigrationType))
						{
							MigrationLogger.Log(MigrationEventType.Information, "Skipping job '{0}' as migration type is not enabled: {1}", new object[]
							{
								job2.JobId,
								job2.MigrationType
							});
						}
						else
						{
							if (job2.NextProcessTime > utcNow)
							{
								break;
							}
							if (stage.IsStatusSupported(job2.Status))
							{
								yield return job2;
							}
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00020F04 File Offset: 0x0001F104
		public IEnumerable<MigrationJob> GetOrderedJobs(IMigrationDataProvider dataProvider)
		{
			List<MigrationJob> source = new List<MigrationJob>(from job in this.Jobs.Values
			orderby job.OriginalCreationTime
			select job);
			return from job in source
			where job != null
			select job;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00020F68 File Offset: 0x0001F168
		public void CheckFeaturesAvailableToUpgrade(MigrationFeature features)
		{
			long minimumSupportedVersion = MigrationSession.GetMinimumSupportedVersion(features);
			if (minimumSupportedVersion > base.Version && !this.CanBeOverwritten)
			{
				throw new MigrationPermanentException(Strings.CannotUpgradeMigrationVersion("there are active jobs"));
			}
			if (MigrationUtil.IsFeatureBlocked(features))
			{
				throw new MigrationPermanentException(Strings.CannotUpgradeMigrationVersion(string.Format("feature {0} is currently blocked.", features)));
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00020FC0 File Offset: 0x0001F1C0
		public void SetMigrationConfig(IMigrationDataProvider dataProvider, MigrationConfig config)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			MigrationUtil.ThrowOnNullArgument(config, "config");
			bool flag = false;
			if (config.MaxNumberOfBatches != this.MaxNumberOfBatches)
			{
				MigrationLogger.Log(MigrationEventType.Information, "Setting max number of batches from {0} to {1} for session {2}", new object[]
				{
					this.MaxNumberOfBatches,
					config.MaxNumberOfBatches,
					this
				});
				this.MaxNumberOfBatches = config.MaxNumberOfBatches;
				flag = true;
			}
			if (config.MaxConcurrentMigrations != this.MaxConcurrentMigrations)
			{
				MigrationLogger.Log(MigrationEventType.Information, "Setting max concurrent migrations from {0} to {1} for session {2}", new object[]
				{
					this.MaxConcurrentMigrations,
					config.MaxConcurrentMigrations,
					this
				});
				this.MaxConcurrentMigrations = config.MaxConcurrentMigrations;
				flag = true;
			}
			if (flag)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "updating config with new info", new object[0]);
				this.SaveExtendedProperties(dataProvider);
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x000210A4 File Offset: 0x0001F2A4
		public bool EnableFeatures(IMigrationDataProvider dataProvider, MigrationFeature features)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			this.CheckFeaturesAvailableToUpgrade(features);
			if (base.StoreObjectId == null)
			{
				this.Create(dataProvider);
			}
			long minimumSupportedVersion = MigrationSession.GetMinimumSupportedVersion(features);
			if (base.Version < minimumSupportedVersion)
			{
				this.UpgradeVersion(dataProvider, minimumSupportedVersion);
			}
			this.EnabledFeatures |= features;
			this.SaveExtendedProperties(dataProvider);
			return true;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00021100 File Offset: 0x0001F300
		public void DisableFeatures(IMigrationDataProvider dataProvider, MigrationFeature features, bool force)
		{
			if ((features & MigrationFeature.MultiBatch) == MigrationFeature.MultiBatch && !force)
			{
				throw new MultiBatchCannotBeDisabledPermanentException(features.ToString());
			}
			this.EnabledFeatures &= ~features;
			this.SaveExtendedProperties(dataProvider);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00021134 File Offset: 0x0001F334
		public void CheckAndUpgradeToSupportedFeaturesAndVersion(IMigrationDataProvider dataProvider)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			long supportedVersionUpgrade = this.SupportedVersionUpgrade;
			if (supportedVersionUpgrade > base.Version)
			{
				this.UpgradeVersion(dataProvider, supportedVersionUpgrade);
			}
			MigrationFeature migrationFeature = MigrationFeature.None;
			string settingName = "PublishedMigrationFeatures";
			MigrationFeature config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<MigrationFeature>(settingName);
			foreach (object obj in Enum.GetValues(typeof(MigrationFeature)))
			{
				MigrationFeature migrationFeature2 = (MigrationFeature)obj;
				if ((config & migrationFeature2) != MigrationFeature.None && !this.IsSupported(migrationFeature2) && !MigrationUtil.IsFeatureBlocked(migrationFeature2))
				{
					try
					{
						this.CheckFeaturesAvailableToUpgrade(migrationFeature2);
						migrationFeature |= migrationFeature2;
					}
					catch (MigrationPermanentException exception)
					{
						MigrationLogger.Log(MigrationEventType.Verbose, exception, "Not applying feature {0} to session {1} because CheckAndUpgradeToSupportedFeature failed.", new object[]
						{
							migrationFeature2,
							this
						});
					}
				}
			}
			if (migrationFeature != MigrationFeature.None)
			{
				MigrationLogger.Log(MigrationEventType.Information, "The session '{0}' will get the features '{1}' enabled.", new object[]
				{
					this,
					migrationFeature
				});
				this.EnableFeatures(dataProvider, migrationFeature);
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00021270 File Offset: 0x0001F470
		public bool CanCreateNewJobOfType(MigrationType migrationType, bool isStaged, out LocalizedException exception)
		{
			if (migrationType == MigrationType.None)
			{
				exception = new UnsupportedMigrationTypeException(migrationType);
				return false;
			}
			if (migrationType == MigrationType.BulkProvisioning)
			{
				exception = new UnsupportedMigrationTypeException(migrationType);
				return false;
			}
			if (this.ActiveJobCount >= this.MaxNumberOfBatches)
			{
				exception = new MaximumNumberOfBatchesReachedException();
				return false;
			}
			if (migrationType == MigrationType.PublicFolder)
			{
				bool flag = this.Jobs.Values.Any((MigrationJob job) => job.MigrationType == MigrationType.PublicFolder);
				if (flag)
				{
					exception = new OnlyOnePublicFolderBatchIsAllowedException();
					return false;
				}
			}
			bool flag2 = this.Jobs.Values.Any((MigrationJob job) => job.IsStaged);
			if (isStaged != flag2 && this.Jobs.Count > 0)
			{
				exception = new CutoverAndStagedBatchesCannotCoexistException();
				return false;
			}
			if (!isStaged && this.Jobs.Count > 0)
			{
				exception = new OnlyOneCutoverBatchIsAllowedException();
				return false;
			}
			exception = null;
			return true;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00021358 File Offset: 0x0001F558
		public void AddMigrationJob(IMigrationDataProvider dataProvider, MigrationJob job)
		{
			if (!this.queueInitialized)
			{
				this.RefreshJobQueueIfNeeded(dataProvider);
			}
			this.AddMigrationJob(job);
			this.SaveExtendedProperties(dataProvider);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00021377 File Offset: 0x0001F577
		public void Create(IMigrationDataProvider dataProvider)
		{
			MigrationUtil.AssertOrThrow(this.CanBeOverwritten, "Session cannot be re-created while active.", new object[0]);
			this.Initialize();
			this.InitializationTime = new ExDateTime?(ExDateTime.UtcNow);
			this.CreateInStore(dataProvider);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x000213AC File Offset: 0x0001F5AC
		public override XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			return base.GetDiagnosticInfo(dataProvider, argument, new XElement("MigrationSession"));
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000213C8 File Offset: 0x0001F5C8
		public bool RemoveJob(IMigrationDataProvider dataProvider, Guid jobId)
		{
			MigrationJob job = this.GetJob(dataProvider, jobId, true);
			job.Delete(dataProvider, true);
			if (this.Jobs.ContainsKey(jobId))
			{
				this.Jobs.TryRemove(jobId, out job);
			}
			if (this.Jobs.Count > 0)
			{
				return false;
			}
			IUpgradeConstraintAdapter upgradeConstraintAdapter = MigrationServiceFactory.Instance.GetUpgradeConstraintAdapter(this);
			upgradeConstraintAdapter.MarkUpgradeConstraintForExpiry(dataProvider, null);
			return false;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00021434 File Offset: 0x0001F634
		public MigrationJob CreateJob(IMigrationDataProvider dataProvider, MigrationBatch migrationBatch)
		{
			if (!base.IsPersisted)
			{
				this.ReinitializeSession();
				this.Create(dataProvider);
			}
			if (this.BatchFlags != MigrationBatchFlags.None)
			{
				MigrationLogger.Log(MigrationEventType.Information, "applying batch flags {0} to current flags {1} for batch", new object[]
				{
					migrationBatch.BatchFlags,
					this.BatchFlags,
					migrationBatch
				});
				migrationBatch.BatchFlags |= this.BatchFlags;
			}
			MigrationJob migrationJob = MigrationJob.Create(dataProvider, this, migrationBatch);
			MigrationLogger.Log(MigrationEventType.Verbose, "session already existed, adding job {0} to back of queue", new object[]
			{
				migrationJob
			});
			this.AddMigrationJob(dataProvider, migrationJob);
			this.RefreshJobQueueIfNeeded(dataProvider);
			return migrationJob;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x000214D4 File Offset: 0x0001F6D4
		public void Initialize()
		{
			MigrationLogger.Log(MigrationEventType.Verbose, "resetting extended properties {0} for {1}", new object[]
			{
				base.ExtendedProperties,
				this
			});
			base.ExtendedProperties = new PersistableDictionary();
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0002150C File Offset: 0x0001F70C
		public void SetLastUpdateConstraintEnforcedTimestamp(IMigrationDataProvider dataProvider, ExDateTime whenEnforced)
		{
			this.LastUpgradeConstraintEnforcedTimestamp = whenEnforced;
			if (base.IsPersisted)
			{
				this.SaveExtendedProperties(dataProvider);
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00021524 File Offset: 0x0001F724
		public override IMigrationStoreObject FindStoreObject(IMigrationDataProvider dataProvider, StoreObjectId id, PropertyDefinition[] properties)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			MigrationUtil.ThrowOnNullArgument(id, "id");
			IMigrationStoreObject result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IMigrationStoreObject rootFolder = dataProvider.GetRootFolder(MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
				{
					MigrationFolder.FolderIdPropertyDefinition,
					properties
				}));
				disposeGuard.Add<IMigrationStoreObject>(rootFolder);
				ExAssert.RetailAssert(MigrationFolderName.SyncMigration.ToString().Equals(rootFolder.Name, StringComparison.OrdinalIgnoreCase), "folder names don't match, expected {0}, found {1}", new object[]
				{
					MigrationFolderName.SyncMigration,
					rootFolder.Name
				});
				disposeGuard.Success();
				result = rootFolder;
			}
			return result;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x000215E4 File Offset: 0x0001F7E4
		protected override IMigrationStoreObject CreateStoreObject(IMigrationDataProvider dataProvider)
		{
			return this.FindStoreObject(dataProvider, dataProvider.Folder.Id, null);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x000215FC File Offset: 0x0001F7FC
		private static long GetMinimumSupportedVersion(MigrationFeature features)
		{
			long num = 1L;
			foreach (object obj in Enum.GetValues(typeof(MigrationFeature)))
			{
				MigrationFeature migrationFeature = (MigrationFeature)obj;
				if ((features & migrationFeature) == migrationFeature)
				{
					long val;
					if (!MigrationSession.FeatureVersionMap.TryGetValue(migrationFeature, out val))
					{
						throw new MigrationFeatureNotSupportedException(migrationFeature.ToString());
					}
					num = Math.Max(num, val);
				}
			}
			return num;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0002168C File Offset: 0x0001F88C
		private int GetMaxNumberOfBatchesDefault()
		{
			return ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("MaximumNumberOfBatchesPerSession");
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x000216A5 File Offset: 0x0001F8A5
		private void CreateInStore(IMigrationDataProvider dataProvider)
		{
			base.StoreObjectId = null;
			base.Version = -1L;
			this.CreateInStore(dataProvider, null);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x000216C0 File Offset: 0x0001F8C0
		private void ReinitializeSession()
		{
			MigrationLogger.Log(MigrationEventType.Verbose, "Reinitializing session {0} to be reused elsewhere", new object[]
			{
				this
			});
			base.Version = -1L;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x000216EC File Offset: 0x0001F8EC
		private void AddMigrationJob(MigrationJob job)
		{
			MigrationUtil.ThrowOnNullArgument(job, "job");
			if (this.Jobs.ContainsKey(job.JobId))
			{
				return;
			}
			MigrationLogger.Log(MigrationEventType.Verbose, "Adding job {0} to the job map.", new object[]
			{
				job.JobId
			});
			this.Jobs[job.JobId] = job;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0002174C File Offset: 0x0001F94C
		private void UpgradeVersion(IMigrationDataProvider dataProvider, long version)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			if (base.Version >= version)
			{
				return;
			}
			ExAssert.RetailAssert(this.MaximumSupportedVersion >= version, "Cannot update to a version that is greater than the maximum supported.");
			MigrationLogger.Log(MigrationEventType.Information, "upgrading from version {0} to {1}", new object[]
			{
				base.Version,
				version
			});
			base.SetVersion(dataProvider, version);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000217B8 File Offset: 0x0001F9B8
		private MigrationJob GetJob(IMigrationDataProvider dataProvider, Guid jobId, bool failIfNotFound)
		{
			this.CheckQueueInitialization();
			MigrationJob migrationJob;
			if (!this.Jobs.TryGetValue(jobId, out migrationJob) && dataProvider != null)
			{
				this.RefreshJobQueueIfNeeded(dataProvider);
				this.Jobs.TryGetValue(jobId, out migrationJob);
			}
			if (migrationJob == null && failIfNotFound)
			{
				throw new MigrationJobNotFoundException(jobId);
			}
			return migrationJob;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00021804 File Offset: 0x0001FA04
		private void RefreshJobQueueIfNeeded(IMigrationDataProvider dataProvider)
		{
			if (this.queueInitialized && this.jobCacheLastUpdated != null && DateTime.UtcNow - this.jobCacheLastUpdated.Value < MigrationSession.JobCacheInterval)
			{
				return;
			}
			MigrationLogger.Log(MigrationEventType.Verbose, "Refreshing job queue for session.", new object[0]);
			this.Jobs.Clear();
			this.jobCacheLastUpdated = new DateTime?(DateTime.UtcNow);
			foreach (MigrationJob migrationJob in MigrationJob.Get(dataProvider, this.Config))
			{
				this.Jobs[migrationJob.JobId] = migrationJob;
			}
			this.queueInitialized = true;
			MigrationLogger.Log(MigrationEventType.Verbose, "After refreshing, found {0} jobs.", new object[]
			{
				this.Jobs.Count
			});
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x000218F4 File Offset: 0x0001FAF4
		private void CheckQueueInitialization()
		{
			MigrationUtil.AssertOrThrow(this.queueInitialized, "Expected the job queue to be initialized already.", new object[0]);
		}

		// Token: 0x040002FA RID: 762
		public const long MinimumSessionVersion = 1L;

		// Token: 0x040002FB RID: 763
		public const long MultiBatchVersion = 2L;

		// Token: 0x040002FC RID: 764
		public const long EndpointsVersion = 4L;

		// Token: 0x040002FD RID: 765
		public const long PAWVersion = 5L;

		// Token: 0x040002FE RID: 766
		public const long MaximumSessionVersion = 5L;

		// Token: 0x040002FF RID: 767
		private const string InitializationTimeKey = "InitializationTime";

		// Token: 0x04000300 RID: 768
		private const string MaxNumberOfBatchesKey = "MaxNumberOfBatches";

		// Token: 0x04000301 RID: 769
		private const string MaxConcurrentMigrationsKey = "MaxConcurrentMigrations";

		// Token: 0x04000302 RID: 770
		private const string EnabledFeaturesKey = "EnabledFeatures";

		// Token: 0x04000303 RID: 771
		private const string LastUpgradeConstraintCheckTimestampKey = "LastUpdateConstraintCheckTimestamp";

		// Token: 0x04000304 RID: 772
		private const string MigrationBatchFlagsKey = "MigrationBatchFlags";

		// Token: 0x04000305 RID: 773
		private static readonly MigrationSession.JobStage InitialSyncStage = new MigrationSession.JobStage("Initial Syncing", new MigrationJobStatus[]
		{
			MigrationJobStatus.SyncInitializing,
			MigrationJobStatus.Validating,
			MigrationJobStatus.ProvisionStarting,
			MigrationJobStatus.SyncStarting,
			MigrationJobStatus.SyncCompleting
		});

		// Token: 0x04000306 RID: 774
		private static readonly MigrationSession.JobStage[] RunnableJobStages = new MigrationSession.JobStage[]
		{
			new MigrationSession.JobStage("Finalizing", new MigrationJobStatus[]
			{
				MigrationJobStatus.CompletionInitializing,
				MigrationJobStatus.CompletionStarting,
				MigrationJobStatus.Completing,
				MigrationJobStatus.Completed,
				MigrationJobStatus.Removing
			}),
			MigrationSession.InitialSyncStage,
			new MigrationSession.JobStage("Incremental Syncing", new MigrationJobStatus[]
			{
				MigrationJobStatus.SyncCompleted
			})
		};

		// Token: 0x04000307 RID: 775
		private static readonly Dictionary<MigrationFeature, long> FeatureVersionMap = new Dictionary<MigrationFeature, long>
		{
			{
				MigrationFeature.None,
				1L
			},
			{
				MigrationFeature.MultiBatch,
				2L
			},
			{
				MigrationFeature.UpgradeBlock,
				1L
			},
			{
				MigrationFeature.Endpoints,
				4L
			},
			{
				MigrationFeature.PAW,
				5L
			}
		};

		// Token: 0x04000308 RID: 776
		private static readonly TimeSpan JobCacheInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000309 RID: 777
		private static HashSet<MigrationJobStatus> runnableJobStatuses;

		// Token: 0x0400030A RID: 778
		private DateTime? jobCacheLastUpdated;

		// Token: 0x0400030B RID: 779
		private bool queueInitialized;

		// Token: 0x0200007F RID: 127
		private class JobStage
		{
			// Token: 0x0600074A RID: 1866 RVA: 0x000219ED File Offset: 0x0001FBED
			public JobStage(string name, MigrationJobStatus[] supportedStatuses)
			{
				this.Name = name;
				this.SupportedStatuses = supportedStatuses;
			}

			// Token: 0x1700024D RID: 589
			// (get) Token: 0x0600074B RID: 1867 RVA: 0x00021A03 File Offset: 0x0001FC03
			// (set) Token: 0x0600074C RID: 1868 RVA: 0x00021A0B File Offset: 0x0001FC0B
			public string Name { get; private set; }

			// Token: 0x1700024E RID: 590
			// (get) Token: 0x0600074D RID: 1869 RVA: 0x00021A14 File Offset: 0x0001FC14
			// (set) Token: 0x0600074E RID: 1870 RVA: 0x00021A1C File Offset: 0x0001FC1C
			public MigrationJobStatus[] SupportedStatuses { get; private set; }

			// Token: 0x0600074F RID: 1871 RVA: 0x00021A28 File Offset: 0x0001FC28
			public bool IsStatusSupported(MigrationJobStatus status)
			{
				foreach (MigrationJobStatus migrationJobStatus in this.SupportedStatuses)
				{
					if (status == migrationJobStatus)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000750 RID: 1872 RVA: 0x00021A5C File Offset: 0x0001FC5C
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(this.Name);
				stringBuilder.Append(" states:");
				foreach (MigrationJobStatus migrationJobStatus in this.SupportedStatuses)
				{
					stringBuilder.Append(" ");
					stringBuilder.Append(migrationJobStatus.ToString());
				}
				return stringBuilder.ToString();
			}
		}
	}
}
