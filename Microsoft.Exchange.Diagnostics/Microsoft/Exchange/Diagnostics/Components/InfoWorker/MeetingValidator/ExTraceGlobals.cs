using System;

namespace Microsoft.Exchange.Diagnostics.Components.Infoworker.MeetingValidator
{
	// Token: 0x02000354 RID: 852
	public static class ExTraceGlobals
	{
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x0005219C File Offset: 0x0005039C
		public static Trace ValidatorTracer
		{
			get
			{
				if (ExTraceGlobals.validatorTracer == null)
				{
					ExTraceGlobals.validatorTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.validatorTracer;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x000521BA File Offset: 0x000503BA
		public static Trace ConsistencyChecksTracer
		{
			get
			{
				if (ExTraceGlobals.consistencyChecksTracer == null)
				{
					ExTraceGlobals.consistencyChecksTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.consistencyChecksTracer;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x000521D8 File Offset: 0x000503D8
		public static Trace CompareToAttendeeTracer
		{
			get
			{
				if (ExTraceGlobals.compareToAttendeeTracer == null)
				{
					ExTraceGlobals.compareToAttendeeTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.compareToAttendeeTracer;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x000521F6 File Offset: 0x000503F6
		public static Trace CompareToOrganizerTracer
		{
			get
			{
				if (ExTraceGlobals.compareToOrganizerTracer == null)
				{
					ExTraceGlobals.compareToOrganizerTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.compareToOrganizerTracer;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x00052214 File Offset: 0x00050414
		public static Trace FixupTracer
		{
			get
			{
				if (ExTraceGlobals.fixupTracer == null)
				{
					ExTraceGlobals.fixupTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.fixupTracer;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x00052232 File Offset: 0x00050432
		public static Trace PFDTracer
		{
			get
			{
				if (ExTraceGlobals.pFDTracer == null)
				{
					ExTraceGlobals.pFDTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.pFDTracer;
			}
		}

		// Token: 0x04001870 RID: 6256
		private static Guid componentGuid = new Guid("7CCC3078-AE21-4CF6-B241-3EE7A8439681");

		// Token: 0x04001871 RID: 6257
		private static Trace validatorTracer = null;

		// Token: 0x04001872 RID: 6258
		private static Trace consistencyChecksTracer = null;

		// Token: 0x04001873 RID: 6259
		private static Trace compareToAttendeeTracer = null;

		// Token: 0x04001874 RID: 6260
		private static Trace compareToOrganizerTracer = null;

		// Token: 0x04001875 RID: 6261
		private static Trace fixupTracer = null;

		// Token: 0x04001876 RID: 6262
		private static Trace pFDTracer = null;
	}
}
