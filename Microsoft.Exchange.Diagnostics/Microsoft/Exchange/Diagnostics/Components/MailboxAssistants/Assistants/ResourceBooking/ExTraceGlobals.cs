using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ResourceBooking
{
	// Token: 0x02000345 RID: 837
	public static class ExTraceGlobals
	{
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001370 RID: 4976 RVA: 0x000516FF File Offset: 0x0004F8FF
		public static Trace ResourceBookingAssistantTracer
		{
			get
			{
				if (ExTraceGlobals.resourceBookingAssistantTracer == null)
				{
					ExTraceGlobals.resourceBookingAssistantTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.resourceBookingAssistantTracer;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x0005171D File Offset: 0x0004F91D
		public static Trace ResourceBookingProcessingTracer
		{
			get
			{
				if (ExTraceGlobals.resourceBookingProcessingTracer == null)
				{
					ExTraceGlobals.resourceBookingProcessingTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.resourceBookingProcessingTracer;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x0005173B File Offset: 0x0004F93B
		public static Trace BookingPolicyTracer
		{
			get
			{
				if (ExTraceGlobals.bookingPolicyTracer == null)
				{
					ExTraceGlobals.bookingPolicyTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.bookingPolicyTracer;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x00051759 File Offset: 0x0004F959
		public static Trace ResourceCheckTracer
		{
			get
			{
				if (ExTraceGlobals.resourceCheckTracer == null)
				{
					ExTraceGlobals.resourceCheckTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.resourceCheckTracer;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x00051777 File Offset: 0x0004F977
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

		// Token: 0x0400181E RID: 6174
		private static Guid componentGuid = new Guid("CAC67919-4E0C-4416-B371-9AC12F9B1AED");

		// Token: 0x0400181F RID: 6175
		private static Trace resourceBookingAssistantTracer = null;

		// Token: 0x04001820 RID: 6176
		private static Trace resourceBookingProcessingTracer = null;

		// Token: 0x04001821 RID: 6177
		private static Trace bookingPolicyTracer = null;

		// Token: 0x04001822 RID: 6178
		private static Trace resourceCheckTracer = null;

		// Token: 0x04001823 RID: 6179
		private static Trace pFDTracer = null;
	}
}
