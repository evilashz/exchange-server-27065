using System;
using System.Threading;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Assistants.Diagnostics;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.SharePointSignalStore;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SharePointSignalStore
{
	// Token: 0x02000228 RID: 552
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SharePointSignalStoreAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00077AB5 File Offset: 0x00075CB5
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.SharePointSignalStoreAssistant;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x00077AB9 File Offset: 0x00075CB9
		public LocalizedString Name
		{
			get
			{
				return Strings.sharePointSignalStoreAssistantName;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00077AC0 File Offset: 0x00075CC0
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.SharePointSignalStoreAssistant;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x00077AC4 File Offset: 0x00075CC4
		public string NonLocalizedName
		{
			get
			{
				return "SharePointSignalStoreAssistant";
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x00077ACB File Offset: 0x00075CCB
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.SharePointSignalStoreWorkCycle.Read();
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x00077AD7 File Offset: 0x00075CD7
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.SharePointSignalStoreWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x00077AE3 File Offset: 0x00075CE3
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x00077AE6 File Offset: 0x00075CE6
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForSharePointSignalStoreAssistant;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x00077AF0 File Offset: 0x00075CF0
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return new PropertyTagPropertyDefinition[]
				{
					this.ControlDataPropertyDefinition
				};
			}
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00077B10 File Offset: 0x00075D10
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			if (Interlocked.Read(ref this.MaxNumberOfInterestingMailboxesForWorkCycleCheckpoint) == -1L)
			{
				this.CalculateMaxNumberOfInterestingMailboxesInWorkCycle();
			}
			bool flag = mailboxInformation.LastLogonTime <= DateTime.UtcNow - SharePointSignalStoreAssistantType.AllowedInactivity;
			if (flag)
			{
				return false;
			}
			object mailboxProperty = mailboxInformation.GetMailboxProperty(this.ControlDataPropertyDefinition);
			bool flag2 = mailboxProperty == null || mailboxProperty is PropertyError;
			if (flag2)
			{
				Interlocked.Increment(ref this.NumberOfInterestingMailboxesForWorkCycleCheckpoint);
				return true;
			}
			ControlData controlData = ControlData.CreateFromByteArray(mailboxProperty as byte[]);
			bool flag3 = DateTime.UtcNow - controlData.LastProcessedDate <= this.WorkCycle - this.WorkCycleCheckpoint;
			if (flag3)
			{
				return false;
			}
			bool flag4 = Interlocked.Read(ref this.NumberOfInterestingMailboxesForWorkCycleCheckpoint) >= Interlocked.Read(ref this.MaxNumberOfInterestingMailboxesForWorkCycleCheckpoint);
			if (flag4)
			{
				return false;
			}
			Interlocked.Increment(ref this.NumberOfInterestingMailboxesForWorkCycleCheckpoint);
			return true;
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00077BEC File Offset: 0x00075DEC
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			ITimeBasedAssistant timeBasedAssistant = new SharePointSignalStoreAssistant(databaseInfo, this.Name, this.NonLocalizedName);
			this.diagnosticsSession = ((SharePointSignalStoreAssistant)timeBasedAssistant).DiagnosticsSession;
			this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, string.Concat(new object[]
			{
				"SharePointSignalStoreAssistantType.CreateInstance: Database = ",
				databaseInfo.DatabaseName,
				", WorkCycle = ",
				this.WorkCycle,
				", workCycleCheckpoint = ",
				this.WorkCycleCheckpoint
			}), new object[0]);
			return timeBasedAssistant;
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x00077C7A File Offset: 0x00075E7A
		public void OnWorkCycleCheckpoint()
		{
			Interlocked.Exchange(ref this.MaxNumberOfInterestingMailboxesForWorkCycleCheckpoint, -1L);
			Interlocked.Exchange(ref this.NumberOfInterestingMailboxesForWorkCycleCheckpoint, 0L);
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00077C98 File Offset: 0x00075E98
		private void CalculateMaxNumberOfInterestingMailboxesInWorkCycle()
		{
			int num = 10000;
			if (this.driver != null)
			{
				DiagnosticsSummaryJobWindow[] windowJobHistory = this.driver.GetWindowJobHistory();
				if (windowJobHistory != null && windowJobHistory.Length > 0 && windowJobHistory[windowJobHistory.Length - 1].TotalOnDatabaseCount > 0)
				{
					num = windowJobHistory[windowJobHistory.Length - 1].TotalOnDatabaseCount;
				}
			}
			long calculatedInterestingMailboxesPerCheckpoint = SharePointSignalStoreAssistantType.GetCalculatedInterestingMailboxesPerCheckpoint(num, this.WorkCycleCheckpoint, this.WorkCycle, 1.2);
			Interlocked.Exchange(ref this.MaxNumberOfInterestingMailboxesForWorkCycleCheckpoint, calculatedInterestingMailboxesPerCheckpoint);
			this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, string.Concat(new object[]
			{
				"SharePointSignalStoreAssistantType.CalculateMaxNumberOfProcessedMailboxesInWorkCycle: Mailboxes = ",
				num,
				", MaxNumberOfProcessedMailboxesInWorkCycle = ",
				Interlocked.Read(ref this.MaxNumberOfInterestingMailboxesForWorkCycleCheckpoint)
			}), new object[0]);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x00077D58 File Offset: 0x00075F58
		internal static long GetCalculatedInterestingMailboxesPerCheckpoint(int numberOfMailboxes, TimeSpan workCycleCheckpoint, TimeSpan workCycle, double multiplier)
		{
			if (workCycle.Ticks > 0L)
			{
				return (long)(((double)numberOfMailboxes * ((double)workCycleCheckpoint.Ticks / (double)workCycle.Ticks) + 1.0) * multiplier);
			}
			return 0L;
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00077D89 File Offset: 0x00075F89
		public override TimeBasedDatabaseDriver CreateDriver(ThrottleGovernor governor, DatabaseInfo databaseInfo, ITimeBasedAssistantType timeBasedAssistantType, PoisonMailboxControl poisonControl, PerformanceCountersPerDatabaseInstance databaseCounters)
		{
			this.driver = new TimeBasedStoreDatabaseDriver(governor, databaseInfo, timeBasedAssistantType, poisonControl, databaseCounters);
			return this.driver;
		}

		// Token: 0x04000C92 RID: 3218
		internal const string AssistantName = "SharePointSignalStoreAssistant";

		// Token: 0x04000C93 RID: 3219
		internal const int MaxNumberOfMailboxesExpected = 10000;

		// Token: 0x04000C94 RID: 3220
		private const double MaxNumberOfInterestingMailboxesForWorkCycleCheckpointMultiplier = 1.2;

		// Token: 0x04000C95 RID: 3221
		private const long MaxNumberOfInterestingMailboxesForWorkCycleCheckpointNotSet = -1L;

		// Token: 0x04000C96 RID: 3222
		internal long NumberOfInterestingMailboxesForWorkCycleCheckpoint;

		// Token: 0x04000C97 RID: 3223
		internal long MaxNumberOfInterestingMailboxesForWorkCycleCheckpoint = -1L;

		// Token: 0x04000C98 RID: 3224
		private static readonly TimeSpan AllowedInactivity = TimeSpan.FromDays(30.0);

		// Token: 0x04000C99 RID: 3225
		private TimeBasedDatabaseDriver driver;

		// Token: 0x04000C9A RID: 3226
		private IDiagnosticsSession diagnosticsSession;

		// Token: 0x04000C9B RID: 3227
		private static readonly Trace Tracer = ExTraceGlobals.GeneralTracer;
	}
}
