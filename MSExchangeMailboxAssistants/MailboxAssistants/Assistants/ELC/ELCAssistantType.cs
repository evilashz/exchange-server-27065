using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200003D RID: 61
	internal sealed class ELCAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000DCFB File Offset: 0x0000BEFB
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.ElcAssistant;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000DCFE File Offset: 0x0000BEFE
		public LocalizedString Name
		{
			get
			{
				return Strings.elcName;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000DD05 File Offset: 0x0000BF05
		public string NonLocalizedName
		{
			get
			{
				return "ElcAssistant";
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000DD0C File Offset: 0x0000BF0C
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.ELCAssistant;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000DD0F File Offset: 0x0000BF0F
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.ManagedFolderWorkCycle.Read();
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000DD1B File Offset: 0x0000BF1B
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.ManagedFolderWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000DD27 File Offset: 0x0000BF27
		internal AdDataCache AdCache
		{
			get
			{
				return this.adDataCache;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000DD2F File Offset: 0x0000BF2F
		internal DiscoveryHoldQueryCache DiscoveryHoldCache
		{
			get
			{
				return this.discoveryHoldCache;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000DD37 File Offset: 0x0000BF37
		internal OrganizationCache OrgCache
		{
			get
			{
				return this.organizationCache;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000DD3F File Offset: 0x0000BF3F
		internal ELCPerfCountersWrapper PerfCountersWrapper
		{
			get
			{
				return this.perfCountersWrapper;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000DD47 File Offset: 0x0000BF47
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForElcAssistant;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000DD50 File Offset: 0x0000BF50
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return new PropertyTagPropertyDefinition[]
				{
					MailboxSchema.QuotaUsedExtended,
					MailboxSchema.DumpsterQuotaUsedExtended,
					(PropertyTagPropertyDefinition)StoreObjectSchema.RetentionFlags
				};
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000DD84 File Offset: 0x0000BF84
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			if (mailboxInformation.IsPublicFolderMailbox())
			{
				return false;
			}
			if (VariantConfiguration.InvariantNoFlightingSnapshot.MailboxAssistants.ElcAssistantAlwaysProcessMailbox.Enabled)
			{
				return true;
			}
			object mailboxProperty = mailboxInformation.GetMailboxProperty((PropertyTagPropertyDefinition)StoreObjectSchema.RetentionFlags);
			if (mailboxProperty != null && mailboxProperty is int && (RetentionAndArchiveFlags)mailboxProperty == RetentionAndArchiveFlags.EHAMigration)
			{
				ELCAssistantType.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Mailbox with Display name {0} is an eha migration mailbox, ELC is processing it.", mailboxInformation.DisplayName);
				return true;
			}
			object mailboxProperty2 = mailboxInformation.GetMailboxProperty(MailboxSchema.QuotaUsedExtended);
			if (mailboxProperty2 == null)
			{
				return true;
			}
			object mailboxProperty3 = mailboxInformation.GetMailboxProperty(MailboxSchema.DumpsterQuotaUsedExtended);
			long num = (long)mailboxProperty2;
			long num2 = (mailboxProperty3 == null) ? 0L : ((long)mailboxProperty3);
			long num3 = num + num2;
			if (num3 >= this.minMailboxSizeInBytesToProcess)
			{
				ELCAssistantType.Tracer.TraceDebug<string, long, long>((long)this.GetHashCode(), "Mailbox with Display name {0} has size {1} bytes which is equal or bigger than {2} bytes, processing it.", mailboxInformation.DisplayName, num3, this.minMailboxSizeInBytesToProcess);
				return true;
			}
			ELCAssistantType.Tracer.TraceDebug<string, long, long>((long)this.GetHashCode(), "Mailbox with Display name {0} has size {1} bytes which is smaller than {2} bytes, skip processing it.", mailboxInformation.DisplayName, num3, this.minMailboxSizeInBytesToProcess);
			Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ELCSkipProcessingMailbox, null, new object[]
			{
				mailboxInformation.DisplayName,
				num3,
				this.minMailboxSizeInBytesToProcess
			});
			EventNotificationItem.Publish(ExchangeComponent.Elc.Name, "ELC.MailboxSizeSkipped", null, string.Format("Mailbox with Display name {0} has size {1} bytes which is smaller than {2} bytes, skip processing it.", mailboxInformation.DisplayName, num3, this.minMailboxSizeInBytesToProcess), ResultSeverityLevel.Warning, false);
			return false;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000DF04 File Offset: 0x0000C104
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new ELCAssistant(databaseInfo, this, this.Name, this.NonLocalizedName);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000DF19 File Offset: 0x0000C119
		public override string ToString()
		{
			return "ELCAssistantType: ";
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000DF20 File Offset: 0x0000C120
		public void OnWorkCycleCheckpoint()
		{
			ELCAssistantType.Tracer.TraceDebug<ELCAssistantType>((long)this.GetHashCode(), "{0}: OnWorkCycleCheckpoint entered.", this);
			Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_MRMExpirationStatistics, null, new object[]
			{
				ELCPerfmon.TotalItemsSoftDeleted.RawValue,
				ELCPerfmon.TotalItemsPermanentlyDeleted.RawValue,
				ELCPerfmon.TotalItemsMoved.RawValue,
				ELCPerfmon.TotalItemsMovedToDiscoveryHolds.RawValue,
				ELCPerfmon.TotalSizeItemsSoftDeleted.RawValue,
				ELCPerfmon.TotalSizeItemsPermanentlyDeleted.RawValue,
				ELCPerfmon.TotalSizeItemsMoved.RawValue,
				ELCPerfmon.TotalSizeItemsMovedToDiscoveryHolds.RawValue,
				ELCPerfmon.TotalItemsWithPersonalTag.RawValue,
				ELCPerfmon.TotalItemsWithDefaultTag.RawValue,
				ELCPerfmon.TotalItemsWithSystemCleanupTag.RawValue,
				ELCPerfmon.TotalItemsExpiredByDefaultExpiryTag.RawValue,
				ELCPerfmon.TotalItemsExpiredByPersonalExpiryTag.RawValue,
				ELCPerfmon.TotalItemsMovedByDefaultArchiveTag.RawValue,
				ELCPerfmon.TotalItemsMovedByPersonalArchiveTag.RawValue,
				ELCPerfmon.NumberOfMailboxesWhoseProcessingTimeIsAboveFirstLevelThreshold.RawValue,
				ELCPerfmon.NumberOfMailboxesWhoseProcessingTimeIsAboveSecondLevelThreshold.RawValue,
				ELCPerfmon.NumberOfFailures.RawValue,
				ELCPerfmon.NumberOfWarnings.RawValue,
				ELCPerfmon.NumberOfMailboxesProcessed.RawValue,
				ELCPerfmon.NumberOfDiscoveryHoldSearchExceptions.RawValue
			});
			this.adDataCache.MarkOrgsToRefresh();
			this.DiscoveryHoldCache.MarkOrgsToRefresh();
			ELCPerfmon.TotalSizeItemsMovedToDiscoveryHolds.RawValue = 0L;
			ELCPerfmon.TotalItemsMovedToDiscoveryHolds.RawValue = 0L;
			ELCPerfmon.TotalItemsAutoCopied.RawValue = 0L;
			ELCPerfmon.TotalItemsExpired.RawValue = 0L;
			ELCPerfmon.TotalSizeItemsExpired.RawValue = 0L;
			ELCPerfmon.TotalItemsTagged.RawValue = 0L;
			ELCPerfmon.TotalItemsMoved.RawValue = 0L;
			ELCPerfmon.TotalSizeItemsMoved.RawValue = 0L;
			ELCPerfmon.TotalItemsSoftDeleted.RawValue = 0L;
			ELCPerfmon.TotalSizeItemsSoftDeleted.RawValue = 0L;
			ELCPerfmon.TotalItemsPermanentlyDeleted.RawValue = 0L;
			ELCPerfmon.TotalSizeItemsPermanentlyDeleted.RawValue = 0L;
			ELCPerfmon.TotalSkippedDumpsters.RawValue = 0L;
			ELCPerfmon.TotalExpiredDumpsterItems.RawValue = 0L;
			ELCPerfmon.TotalMovedDumpsterItems.RawValue = 0L;
			ELCPerfmon.TotalExpiredSystemDataItems.RawValue = 0L;
			ELCPerfmon.TotalOverQuotaDumpsters.RawValue = 0L;
			ELCPerfmon.TotalOverQuotaDumpsterItems.RawValue = 0L;
			ELCPerfmon.TotalOverQuotaDumpsterItemsDeleted.RawValue = 0L;
			ELCPerfmon.TotalItemsWithPersonalTag.RawValue = 0L;
			ELCPerfmon.TotalItemsWithDefaultTag.RawValue = 0L;
			ELCPerfmon.TotalItemsWithSystemCleanupTag.RawValue = 0L;
			ELCPerfmon.TotalItemsExpiredByDefaultExpiryTag.RawValue = 0L;
			ELCPerfmon.TotalItemsExpiredByPersonalExpiryTag.RawValue = 0L;
			ELCPerfmon.TotalItemsMovedByDefaultArchiveTag.RawValue = 0L;
			ELCPerfmon.TotalItemsMovedByPersonalArchiveTag.RawValue = 0L;
			ELCPerfmon.HealthMonitorAverageDelay.RawValue = 0L;
			ELCPerfmon.HealthMonitorDelayRate.RawValue = 0L;
			ELCPerfmon.HealthMonitorUnhealthy.RawValue = 0L;
			ELCPerfmon.TotalMigratedItemsDeletedDueToItemAgeBased.RawValue = 0L;
			ELCPerfmon.TotalItemMovedToArchiveForMigration.RawValue = 0L;
			ELCPerfmon.NumberOfMailboxesWhoseProcessingTimeIsAboveFirstLevelThreshold.RawValue = 0L;
			ELCPerfmon.NumberOfMailboxesWhoseProcessingTimeIsAboveSecondLevelThreshold.RawValue = 0L;
			ELCPerfmon.NumberOfFailures.RawValue = 0L;
			ELCPerfmon.NumberOfWarnings.RawValue = 0L;
			ELCPerfmon.NumberOfMailboxesProcessed.RawValue = 0L;
			ELCPerfmon.NumberOfDiscoveryHoldSearchExceptions.RawValue = 0L;
			this.perfCountersWrapper.ResetCounters();
			this.minMailboxSizeInBytesToProcess = ELCAssistantType.GetMinimumMailboxSizeToBeProcessed();
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000E2CC File Offset: 0x0000C4CC
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User | MailboxType.System | MailboxType.Archive | MailboxType.Arbitration | MailboxType.PublicFolder | MailboxType.InactiveMailbox;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000E2D0 File Offset: 0x0000C4D0
		private static long GetMinimumMailboxSizeToBeProcessed()
		{
			int num = ELCAssistantType.DefaultMinMailboxSizeInMBToProcess;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters"))
			{
				if (registryKey != null && registryKey.GetValue(ELCAssistantType.MinMailboxSizeInMBToProcessRegKey) != null)
				{
					object value = registryKey.GetValue(ELCAssistantType.MinMailboxSizeInMBToProcessRegKey);
					if (value != null && value is int)
					{
						int num2 = (int)value;
						if (num2 >= 0)
						{
							num = num2;
						}
					}
				}
			}
			return (long)(num * 1048576);
		}

		// Token: 0x040001C3 RID: 451
		internal const string AssistantName = "ElcAssistant";

		// Token: 0x040001C4 RID: 452
		private static readonly Trace Tracer = ExTraceGlobals.ELCAssistantTracer;

		// Token: 0x040001C5 RID: 453
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x040001C6 RID: 454
		private static readonly string MinMailboxSizeInMBToProcessRegKey = "MinMailboxSizeInMBToProcess";

		// Token: 0x040001C7 RID: 455
		private static readonly int DefaultMinMailboxSizeInMBToProcess = 10;

		// Token: 0x040001C8 RID: 456
		private readonly ELCPerfCountersWrapper perfCountersWrapper = new ELCPerfCountersWrapper(new Tuple<ExPerformanceCounter, ExPerformanceCounter>[]
		{
			new Tuple<ExPerformanceCounter, ExPerformanceCounter>(ELCPerfmon.NumberOfMailboxesWhoseProcessingTimeIsAboveFirstLevelThreshold, ELCPerfmon.NumberOfMailboxesWhoseProcessingTimeIsAboveFirstLevelThresholdPerHour),
			new Tuple<ExPerformanceCounter, ExPerformanceCounter>(ELCPerfmon.NumberOfMailboxesWhoseProcessingTimeIsAboveSecondLevelThreshold, ELCPerfmon.NumberOfMailboxesWhoseProcessingTimeIsAboveSecondLevelThresholdPerHour),
			new Tuple<ExPerformanceCounter, ExPerformanceCounter>(ELCPerfmon.NumberOfFailures, ELCPerfmon.NumberOfFailuresPerHour),
			new Tuple<ExPerformanceCounter, ExPerformanceCounter>(ELCPerfmon.NumberOfWarnings, ELCPerfmon.NumberOfWarningsPerHour),
			new Tuple<ExPerformanceCounter, ExPerformanceCounter>(ELCPerfmon.NumberOfMailboxesProcessed, ELCPerfmon.NumberOfMailboxesProcessedPerHour),
			new Tuple<ExPerformanceCounter, ExPerformanceCounter>(ELCPerfmon.NumberOfDiscoveryHoldSearchExceptions, ELCPerfmon.NumberOfDiscoveryHoldSearchExceptionsPerHour)
		});

		// Token: 0x040001C9 RID: 457
		private long minMailboxSizeInBytesToProcess = ELCAssistantType.GetMinimumMailboxSizeToBeProcessed();

		// Token: 0x040001CA RID: 458
		private AdDataCache adDataCache = new AdDataCache();

		// Token: 0x040001CB RID: 459
		private DiscoveryHoldQueryCache discoveryHoldCache = new DiscoveryHoldQueryCache();

		// Token: 0x040001CC RID: 460
		private OrganizationCache organizationCache = new OrganizationCache();
	}
}
