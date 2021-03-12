using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.Diagnostics.Components.Infoworker.MeetingValidator;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x0200003A RID: 58
	internal static class Globals
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x0000C53E File Offset: 0x0000A73E
		internal static void Initialize()
		{
		}

		// Token: 0x0400013E RID: 318
		internal const string CalendarRepairEventSource = "MSExchange CalendarRepairAssistant";

		// Token: 0x0400013F RID: 319
		internal static readonly Trace ValidatorTracer = Microsoft.Exchange.Diagnostics.Components.Infoworker.MeetingValidator.ExTraceGlobals.ValidatorTracer;

		// Token: 0x04000140 RID: 320
		internal static readonly Trace ConsistencyChecksTracer = Microsoft.Exchange.Diagnostics.Components.Infoworker.MeetingValidator.ExTraceGlobals.ConsistencyChecksTracer;

		// Token: 0x04000141 RID: 321
		internal static readonly Trace CompareToAttendeeTracer = Microsoft.Exchange.Diagnostics.Components.Infoworker.MeetingValidator.ExTraceGlobals.CompareToAttendeeTracer;

		// Token: 0x04000142 RID: 322
		internal static readonly Trace CompareToOrganizerTracer = Microsoft.Exchange.Diagnostics.Components.Infoworker.MeetingValidator.ExTraceGlobals.CompareToOrganizerTracer;

		// Token: 0x04000143 RID: 323
		internal static readonly Trace FixupTracer = Microsoft.Exchange.Diagnostics.Components.Infoworker.MeetingValidator.ExTraceGlobals.FixupTracer;

		// Token: 0x04000144 RID: 324
		internal static readonly Trace TracerPfd = Microsoft.Exchange.Diagnostics.Components.Infoworker.MeetingValidator.ExTraceGlobals.PFDTracer;

		// Token: 0x04000145 RID: 325
		internal static ExEventLog CalendarRepairLogger = new ExEventLog(Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common.ExTraceGlobals.SingleInstanceItemTracer.Category, "MSExchange CalendarRepairAssistant");
	}
}
