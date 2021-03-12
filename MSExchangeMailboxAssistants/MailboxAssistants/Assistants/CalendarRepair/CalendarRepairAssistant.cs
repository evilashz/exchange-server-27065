using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.CalendarDiagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Calendar;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarRepair;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.Infoworker.MeetingValidator;
using Microsoft.Exchange.MailboxAssistants.Assistants.ResourceBooking;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarRepair
{
	// Token: 0x02000159 RID: 345
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarRepairAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase, IDisposable
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x00054C81 File Offset: 0x00052E81
		// (set) Token: 0x06000E07 RID: 3591 RVA: 0x00054C89 File Offset: 0x00052E89
		internal CalendarRepairPolicy RepairPolicy { get; private set; }

		// Token: 0x06000E08 RID: 3592 RVA: 0x00054CB0 File Offset: 0x00052EB0
		public CalendarRepairAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			this.incrementTotalItemsInspected = delegate(long inc)
			{
				CalendarRepairPerfmon.TotalItemsInspected.IncrementBy(inc);
			};
			this.incrementTotalItemsRepaired = delegate(long inc)
			{
				CalendarRepairPerfmon.TotalItemsRepaired.IncrementBy(inc);
			};
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00054D0C File Offset: 0x00052F0C
		// (set) Token: 0x06000E0A RID: 3594 RVA: 0x00054D14 File Offset: 0x00052F14
		internal static TimeSpan CvsPopulationTimeout
		{
			get
			{
				return CalendarRepairAssistant.cvsPopulationTimeout;
			}
			set
			{
				if (value < TimeSpan.Zero && value.TotalMilliseconds != -1.0)
				{
					CalendarRepairAssistant.Tracer.TraceError<TimeSpan>(0L, "CalendarRepairAssistant: Invalid CVS Population Timeout ({0}).", value);
					return;
				}
				CalendarRepairAssistant.cvsPopulationTimeout = value;
				CalendarRepairAssistant.Tracer.TraceDebug<TimeSpan>(0L, "CalendarRepairAssistant: CVS population timeout is set to {0}.", value);
			}
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00054D6C File Offset: 0x00052F6C
		public void OnWorkCycleCheckpoint()
		{
			CalendarRepairAssistant.Tracer.TraceDebug<CalendarRepairAssistant>((long)this.GetHashCode(), "{0}: OnWorkCycleCheckpoint entered.", this);
			CalendarRepairPerfmon.TotalItemsInspected.RawValue = 0L;
			CalendarRepairPerfmon.TotalItemsRepaired.RawValue = 0L;
			Server localServer = LocalServerCache.LocalServer;
			if (localServer != null)
			{
				string name = localServer.Fqdn + " Server CalendarRepair Policy";
				this.RepairPolicy = CalendarRepairPolicy.CreateInstance(name);
				this.RepairPolicy.InitializeWithDefaults();
				this.RepairPolicy.DaysInWindowBackward = 0;
				this.RepairPolicy.DaysInWindowForward = localServer.CalendarRepairIntervalEndWindow;
				this.RepairPolicy.RepairMode = localServer.CalendarRepairMode;
				if (localServer.CalendarRepairMissingItemFixDisabled)
				{
					this.RepairPolicy.RemoveRepairFlags(new CalendarInconsistencyFlag[]
					{
						CalendarInconsistencyFlag.MissingItem,
						CalendarInconsistencyFlag.ExtraOccurrenceDeletion
					});
				}
			}
			else
			{
				this.RepairPolicy = CalendarRepairPolicy.CreateInstance("Default CalendarRepair Policy");
				this.RepairPolicy.InitializeWithDefaults();
				this.RepairPolicy.DaysInWindowBackward = 0;
			}
			CalendarRepairLogger.Instance.UpdateConfigurationFromADSetting(localServer);
			CalendarRepairAssistant.Tracer.TraceDebug<CalendarRepairAssistant>((long)this.GetHashCode(), "{0}: OnWorkCycleCheckpoint exited.", this);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00054E78 File Offset: 0x00053078
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			MailboxSession mailboxSession = invokeArgs.StoreSession as MailboxSession;
			if (mailboxSession == null)
			{
				return;
			}
			if (mailboxSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.UserMailbox && mailboxSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.LinkedMailbox && mailboxSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.GroupMailbox)
			{
				CalendarRepairAssistant.CachedStateTracer.TraceDebug<Guid, string, string>((long)this.GetHashCode(), "Skipping mailbox with guid {0} and display name {1} since this is a {2} and not a UserMailbox or a LinkedMailbox", mailboxSession.MailboxGuid, mailboxSession.MailboxOwner.MailboxInfo.DisplayName, mailboxSession.MailboxOwner.RecipientTypeDetails.ToString());
				return;
			}
			ExDateTime now = ExDateTime.Now;
			ExDateTime rangeStart = now.AddDays((double)(-(double)this.RepairPolicy.DaysInWindowBackward));
			ExDateTime rangeEnd = now.AddDays((double)this.RepairPolicy.DaysInWindowForward);
			CachedState cachedState = AssistantsService.CachedObjectsList.GetCachedState(mailboxSession.MailboxGuid);
			bool flag = ResourceCheck.DetailedCheckForAutomatedBooking(mailboxSession, cachedState);
			if (flag)
			{
				CalendarRepairAssistant.CachedStateTracer.TraceDebug((long)this.GetHashCode(), "{0}: Calendar Repair Assistant is skipping resource mailbox.", new object[]
				{
					TraceContext.Get()
				});
				return;
			}
			CalendarRepairAssistantLogEntry calendarRepairAssistantLogEntry = new CalendarRepairAssistantLogEntry
			{
				DatabaseGuid = mailboxSession.MailboxOwner.MailboxInfo.GetDatabaseGuid(),
				MailboxGuid = mailboxSession.MailboxGuid,
				TenantGuid = mailboxSession.MailboxOwner.MailboxInfo.OrganizationId.GetTenantGuid(),
				RepairMode = this.RepairPolicy.RepairMode.ToString(),
				RangeStartTime = string.Format("{0:O}", rangeStart.ToUtc()),
				RangeEndTime = string.Format("{0:O}", rangeEnd.ToUtc())
			};
			this.MarkMailboxForUpgrade(mailboxSession, calendarRepairAssistantLogEntry);
			try
			{
				CalendarRepairAssistant.CachedStateTracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Calendar Repair Assistant starting to validate mailbox: {1}.", TraceContext.Get(), mailboxSession.MailboxOwner.MailboxInfo.DisplayName);
				CalendarValidator calendarValidator = CalendarValidator.CreateRepairingInstance(mailboxSession, rangeStart, rangeEnd, this.RepairPolicy, CalendarRepairAssistant.cvsPopulationTimeout);
				calendarValidator.OnItemInspected += this.incrementTotalItemsInspected;
				calendarValidator.OnItemRepaired += this.incrementTotalItemsRepaired;
				List<MeetingValidationResult> validationResults = calendarValidator.Run();
				calendarValidator.OnItemInspected -= this.incrementTotalItemsInspected;
				calendarValidator.OnItemRepaired -= this.incrementTotalItemsRepaired;
				calendarRepairAssistantLogEntry.AddValidationResults(validationResults);
				CalendarRepairLogger.Instance.Log(validationResults, mailboxSession.MailboxOwner, rangeStart, rangeEnd);
				if (CalendarRepairAssistant.IsCRAReliabilityLoggerEnabled(mailboxSession))
				{
					CalendarReliabilityInsigntLogger.Instance.Log(calendarRepairAssistantLogEntry, validationResults);
				}
				CalendarRepairAssistant.CachedStateTracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Calendar Repair Assistant completed validating mailbox: {1}.", TraceContext.Get(), mailboxSession.MailboxOwner.MailboxInfo.DisplayName);
			}
			catch (WrongServerException ex)
			{
				string message = string.Format("Could not access the mailbox (ExchangePrincipal:'{0}'). Exception: {1}.", mailboxSession.MailboxOwner, ex);
				CalendarRepairAssistant.Tracer.TraceDebug((long)this.GetHashCode(), message);
				calendarRepairAssistantLogEntry.AddExceptionToLog(ex);
			}
			catch (MailboxUserNotFoundException ex2)
			{
				CalendarRepairAssistant.Tracer.TraceDebug<MailboxUserNotFoundException>((long)this.GetHashCode(), "MailboxUserNotFoundException:{0}", ex2);
				calendarRepairAssistantLogEntry.AddExceptionToLog(ex2);
			}
			catch (CorruptDataException ex3)
			{
				CalendarRepairAssistant.Tracer.TraceDebug<CorruptDataException>((long)this.GetHashCode(), "CorruptDataException:{0}", ex3);
				calendarRepairAssistantLogEntry.AddExceptionToLog(ex3);
			}
			catch (SubmissionQuotaExceededException ex4)
			{
				CalendarRepairAssistant.Tracer.TraceDebug<SubmissionQuotaExceededException>((long)this.GetHashCode(), "SubmissionQuotaExceededException:{0}", ex4);
				calendarRepairAssistantLogEntry.AddExceptionToLog(ex4);
			}
			catch (StoragePermanentException ex5)
			{
				CalendarRepairAssistant.Tracer.TraceDebug<StoragePermanentException>((long)this.GetHashCode(), "StoragePermanentException:{0}", ex5);
				calendarRepairAssistantLogEntry.AddExceptionToLog(ex5);
			}
			catch (Exception ex6)
			{
				CalendarRepairAssistant.Tracer.TraceDebug<Exception>((long)this.GetHashCode(), "Exception:{0}", ex6);
				calendarRepairAssistantLogEntry.AddExceptionToLog(ex6);
				throw ex6;
			}
			finally
			{
				stopwatch.Stop();
				calendarRepairAssistantLogEntry.TotalProcessingTime = stopwatch.Elapsed.TotalMilliseconds.ToString();
				customDataToLog.AddRange(calendarRepairAssistantLogEntry.FormatCustomData());
			}
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x000552F4 File Offset: 0x000534F4
		protected override void OnShutdownInternal()
		{
			if (CalendarReliabilityInsigntLogger.Instance != null)
			{
				CalendarReliabilityInsigntLogger.Instance.FlushAndDispose();
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00055307 File Offset: 0x00053507
		public void Dispose()
		{
			this.incrementTotalItemsInspected = null;
			this.incrementTotalItemsRepaired = null;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00055318 File Offset: 0x00053518
		private void MarkMailboxForUpgrade(MailboxSession mbxSession, CalendarRepairAssistantLogEntry logEntry)
		{
			try
			{
				CalendarUpgrade.CalendarUpgradeStatus calendarUpgradeStatus = CalendarUpgrade.MarkMailboxForUpgrade(mbxSession, new XSOFactory());
				CalendarRepairAssistant.Tracer.TraceDebug<Guid, CalendarUpgrade.CalendarUpgradeStatus>((long)this.GetHashCode(), "MarkMailboxForUpgrade MailboxGuid:{0} UpgradeStatus:{1}", mbxSession.MailboxGuid, calendarUpgradeStatus);
				logEntry.UpgradeStatus = calendarUpgradeStatus.ToString();
			}
			catch (LocalizedException ex)
			{
				CalendarRepairAssistant.Tracer.TraceError<Guid, LocalizedException>((long)this.GetHashCode(), "MarkMailboxForUpgrade MailboxGuid:{0} Exception:{1}", mbxSession.MailboxGuid, ex);
				logEntry.AddExceptionToLog(ex);
			}
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00055398 File Offset: 0x00053598
		private static bool IsCRAReliabilityLoggerEnabled(MailboxSession mbxSession)
		{
			return mbxSession.MailboxOwner.GetConfiguration().MailboxAssistants.CalendarRepairAssistantReliabilityLogger.Enabled;
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x000553EC File Offset: 0x000535EC
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x000553F4 File Offset: 0x000535F4
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x000553FC File Offset: 0x000535FC
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000914 RID: 2324
		private static TimeSpan cvsPopulationTimeout = CalendarVersionStoreQueryPolicy.DefaultWaitTimeForPopulation;

		// Token: 0x04000915 RID: 2325
		private Action<long> incrementTotalItemsInspected;

		// Token: 0x04000916 RID: 2326
		private Action<long> incrementTotalItemsRepaired;

		// Token: 0x04000917 RID: 2327
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarRepair.ExTraceGlobals.CalendarRepairAssistantTracer;

		// Token: 0x04000918 RID: 2328
		private static readonly Microsoft.Exchange.Diagnostics.Trace TracerPfd = Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarRepair.ExTraceGlobals.PFDTracer;

		// Token: 0x04000919 RID: 2329
		private static readonly Microsoft.Exchange.Diagnostics.Trace CachedStateTracer = Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Calendar.ExTraceGlobals.CachedStateTracer;
	}
}
