using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarRepair;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarRepair
{
	// Token: 0x0200015A RID: 346
	internal sealed class CalendarRepairAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x00055404 File Offset: 0x00053604
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.CalendarRepairAssistant;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00055407 File Offset: 0x00053607
		public LocalizedString Name
		{
			get
			{
				return Strings.calRepairName;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0005540E File Offset: 0x0005360E
		public string NonLocalizedName
		{
			get
			{
				return "CalendarRepairAssistant";
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x00055415 File Offset: 0x00053615
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.CalendarRepairWorkCycle.Read();
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x00055421 File Offset: 0x00053621
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.CalendarRepairWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x0005542D File Offset: 0x0005362D
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.CalendarRepairAssistant;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x00055430 File Offset: 0x00053630
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x00055433 File Offset: 0x00053633
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForCalendarRepairAssistant;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0005543A File Offset: 0x0005363A
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return new PropertyTagPropertyDefinition[0];
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00055444 File Offset: 0x00053644
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			if (!mailboxInformation.IsUserMailbox() && !mailboxInformation.IsGroupMailbox())
			{
				CalendarRepairAssistantType.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "CalendarRepairAssistant Mailbox guid {0}: Not group or user mailbox", mailboxInformation.MailboxGuid);
				return false;
			}
			if (!CalendarUpgrade.IsMailboxActive(new ExDateTime?(new ExDateTime(ExTimeZone.UtcTimeZone, mailboxInformation.LastLogonTime.ToUniversalTime()))))
			{
				CalendarRepairAssistantType.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "CalendarRepairAssistant Mailbox guid {0}: Inactive mailbox", mailboxInformation.MailboxGuid);
				return false;
			}
			CalendarRepairAssistantType.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "CalendarRepairAssistant Mailbox guid {0}: Interesting mailbox", mailboxInformation.MailboxGuid);
			return true;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x000554DE File Offset: 0x000536DE
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new CalendarRepairAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x000554F4 File Offset: 0x000536F4
		public void OnWorkCycleCheckpoint()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange Assistants\\Parameters"))
			{
				object value = registryKey.GetValue("CvsPopulationTimeout");
				if (value is int)
				{
					CalendarRepairAssistant.CvsPopulationTimeout = TimeSpan.FromMilliseconds((double)((int)value));
				}
			}
		}

		// Token: 0x0400091D RID: 2333
		internal const string AssistantName = "CalendarRepairAssistant";

		// Token: 0x0400091E RID: 2334
		private static readonly Trace Tracer = ExTraceGlobals.CalendarRepairAssistantTracer;
	}
}
