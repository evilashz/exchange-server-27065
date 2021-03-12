using System;

namespace Microsoft.Exchange.Diagnostics.Components.Entities.BirthdayCalendar
{
	// Token: 0x02000401 RID: 1025
	public static class ExTraceGlobals
	{
		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x0005CB82 File Offset: 0x0005AD82
		public static Trace CreateBirthdayEventForContactTracer
		{
			get
			{
				if (ExTraceGlobals.createBirthdayEventForContactTracer == null)
				{
					ExTraceGlobals.createBirthdayEventForContactTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.createBirthdayEventForContactTracer;
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x0005CBA0 File Offset: 0x0005ADA0
		public static Trace BirthdayCalendarReferenceTracer
		{
			get
			{
				if (ExTraceGlobals.birthdayCalendarReferenceTracer == null)
				{
					ExTraceGlobals.birthdayCalendarReferenceTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.birthdayCalendarReferenceTracer;
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x0005CBBE File Offset: 0x0005ADBE
		public static Trace BirthdayAssistantBusinessLogicTracer
		{
			get
			{
				if (ExTraceGlobals.birthdayAssistantBusinessLogicTracer == null)
				{
					ExTraceGlobals.birthdayAssistantBusinessLogicTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.birthdayAssistantBusinessLogicTracer;
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x0005CBDC File Offset: 0x0005ADDC
		public static Trace EnableBirthdayCalendarTracer
		{
			get
			{
				if (ExTraceGlobals.enableBirthdayCalendarTracer == null)
				{
					ExTraceGlobals.enableBirthdayCalendarTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.enableBirthdayCalendarTracer;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x0005CBFA File Offset: 0x0005ADFA
		public static Trace BirthdayCalendarsTracer
		{
			get
			{
				if (ExTraceGlobals.birthdayCalendarsTracer == null)
				{
					ExTraceGlobals.birthdayCalendarsTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.birthdayCalendarsTracer;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x0005CC18 File Offset: 0x0005AE18
		public static Trace BirthdayEventDataProviderTracer
		{
			get
			{
				if (ExTraceGlobals.birthdayEventDataProviderTracer == null)
				{
					ExTraceGlobals.birthdayEventDataProviderTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.birthdayEventDataProviderTracer;
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x0005CC36 File Offset: 0x0005AE36
		public static Trace BirthdayContactDataProviderTracer
		{
			get
			{
				if (ExTraceGlobals.birthdayContactDataProviderTracer == null)
				{
					ExTraceGlobals.birthdayContactDataProviderTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.birthdayContactDataProviderTracer;
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x0005CC54 File Offset: 0x0005AE54
		public static Trace UpdateBirthdaysForLinkedContactsTracer
		{
			get
			{
				if (ExTraceGlobals.updateBirthdaysForLinkedContactsTracer == null)
				{
					ExTraceGlobals.updateBirthdaysForLinkedContactsTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.updateBirthdaysForLinkedContactsTracer;
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x0005CC72 File Offset: 0x0005AE72
		public static Trace UpdateBirthdayEventForContactTracer
		{
			get
			{
				if (ExTraceGlobals.updateBirthdayEventForContactTracer == null)
				{
					ExTraceGlobals.updateBirthdayEventForContactTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.updateBirthdayEventForContactTracer;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x0005CC90 File Offset: 0x0005AE90
		public static Trace DeleteBirthdayEventForContactTracer
		{
			get
			{
				if (ExTraceGlobals.deleteBirthdayEventForContactTracer == null)
				{
					ExTraceGlobals.deleteBirthdayEventForContactTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.deleteBirthdayEventForContactTracer;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x0005CCAF File Offset: 0x0005AEAF
		public static Trace GetBirthdayCalendarViewTracer
		{
			get
			{
				if (ExTraceGlobals.getBirthdayCalendarViewTracer == null)
				{
					ExTraceGlobals.getBirthdayCalendarViewTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.getBirthdayCalendarViewTracer;
			}
		}

		// Token: 0x04001D69 RID: 7529
		private static Guid componentGuid = new Guid("F89B9EF1-6D5A-48BF-85C8-445007D8B947");

		// Token: 0x04001D6A RID: 7530
		private static Trace createBirthdayEventForContactTracer = null;

		// Token: 0x04001D6B RID: 7531
		private static Trace birthdayCalendarReferenceTracer = null;

		// Token: 0x04001D6C RID: 7532
		private static Trace birthdayAssistantBusinessLogicTracer = null;

		// Token: 0x04001D6D RID: 7533
		private static Trace enableBirthdayCalendarTracer = null;

		// Token: 0x04001D6E RID: 7534
		private static Trace birthdayCalendarsTracer = null;

		// Token: 0x04001D6F RID: 7535
		private static Trace birthdayEventDataProviderTracer = null;

		// Token: 0x04001D70 RID: 7536
		private static Trace birthdayContactDataProviderTracer = null;

		// Token: 0x04001D71 RID: 7537
		private static Trace updateBirthdaysForLinkedContactsTracer = null;

		// Token: 0x04001D72 RID: 7538
		private static Trace updateBirthdayEventForContactTracer = null;

		// Token: 0x04001D73 RID: 7539
		private static Trace deleteBirthdayEventForContactTracer = null;

		// Token: 0x04001D74 RID: 7540
		private static Trace getBirthdayCalendarViewTracer = null;
	}
}
