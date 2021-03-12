using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.MailboxAssistants.Assistants;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.CalendarSync
{
	// Token: 0x020000BE RID: 190
	internal sealed class CalendarSyncAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x000396F8 File Offset: 0x000378F8
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.CalendarSyncAssistant;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x000396FB File Offset: 0x000378FB
		public LocalizedString Name
		{
			get
			{
				return Strings.descCalendarSyncAssistantName;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00039702 File Offset: 0x00037902
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.SharingSyncWorkCycle.Read();
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x0003970E File Offset: 0x0003790E
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.SharingSyncWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0003971A File Offset: 0x0003791A
		public string NonLocalizedName
		{
			get
			{
				return "CalendarSyncAssistant";
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00039721 File Offset: 0x00037921
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.CalendarSyncAssistant;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00039724 File Offset: 0x00037924
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00039727 File Offset: 0x00037927
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForCalendarSyncAssistant;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0003972E File Offset: 0x0003792E
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return CalendarSyncAssistantHelper.MailboxTableExtendedProperties;
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00039735 File Offset: 0x00037935
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			return this.CalendarSyncAssistantHelper.IsMailboxInteresting(mailboxInformation);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00039743 File Offset: 0x00037943
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new CalendarSyncAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00039757 File Offset: 0x00037957
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00039759 File Offset: 0x00037959
		private CalendarSyncAssistantHelper CalendarSyncAssistantHelper
		{
			get
			{
				return CalendarSyncAssistantType.calendarSyncAssistantHelper;
			}
		}

		// Token: 0x040005C2 RID: 1474
		internal const string AssistantName = "CalendarSyncAssistant";

		// Token: 0x040005C3 RID: 1475
		private static CalendarSyncAssistantHelper calendarSyncAssistantHelper = new CalendarSyncAssistantHelper();
	}
}
