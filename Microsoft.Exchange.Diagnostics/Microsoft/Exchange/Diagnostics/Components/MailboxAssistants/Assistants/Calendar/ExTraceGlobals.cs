using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Calendar
{
	// Token: 0x02000346 RID: 838
	public static class ExTraceGlobals
	{
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x000517C4 File Offset: 0x0004F9C4
		public static Trace GeneralTracer
		{
			get
			{
				if (ExTraceGlobals.generalTracer == null)
				{
					ExTraceGlobals.generalTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.generalTracer;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x000517E2 File Offset: 0x0004F9E2
		public static Trace UnexpectedPathTracer
		{
			get
			{
				if (ExTraceGlobals.unexpectedPathTracer == null)
				{
					ExTraceGlobals.unexpectedPathTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.unexpectedPathTracer;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x00051800 File Offset: 0x0004FA00
		public static Trace CalendarItemValuesTracer
		{
			get
			{
				if (ExTraceGlobals.calendarItemValuesTracer == null)
				{
					ExTraceGlobals.calendarItemValuesTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.calendarItemValuesTracer;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0005181E File Offset: 0x0004FA1E
		public static Trace ProcessingTracer
		{
			get
			{
				if (ExTraceGlobals.processingTracer == null)
				{
					ExTraceGlobals.processingTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.processingTracer;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x0005183C File Offset: 0x0004FA3C
		public static Trace ProcessingRequestTracer
		{
			get
			{
				if (ExTraceGlobals.processingRequestTracer == null)
				{
					ExTraceGlobals.processingRequestTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.processingRequestTracer;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x0005185A File Offset: 0x0004FA5A
		public static Trace ProcessingResponseTracer
		{
			get
			{
				if (ExTraceGlobals.processingResponseTracer == null)
				{
					ExTraceGlobals.processingResponseTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.processingResponseTracer;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x00051878 File Offset: 0x0004FA78
		public static Trace ProcessingCancellationTracer
		{
			get
			{
				if (ExTraceGlobals.processingCancellationTracer == null)
				{
					ExTraceGlobals.processingCancellationTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.processingCancellationTracer;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x00051896 File Offset: 0x0004FA96
		public static Trace CachedStateTracer
		{
			get
			{
				if (ExTraceGlobals.cachedStateTracer == null)
				{
					ExTraceGlobals.cachedStateTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.cachedStateTracer;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x000518B4 File Offset: 0x0004FAB4
		public static Trace OldMessageDeletionTracer
		{
			get
			{
				if (ExTraceGlobals.oldMessageDeletionTracer == null)
				{
					ExTraceGlobals.oldMessageDeletionTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.oldMessageDeletionTracer;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x000518D2 File Offset: 0x0004FAD2
		public static Trace PFDTracer
		{
			get
			{
				if (ExTraceGlobals.pFDTracer == null)
				{
					ExTraceGlobals.pFDTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.pFDTracer;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x000518F1 File Offset: 0x0004FAF1
		public static Trace ProcessingMeetingForwardNotificationTracer
		{
			get
			{
				if (ExTraceGlobals.processingMeetingForwardNotificationTracer == null)
				{
					ExTraceGlobals.processingMeetingForwardNotificationTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.processingMeetingForwardNotificationTracer;
			}
		}

		// Token: 0x04001824 RID: 6180
		private static Guid componentGuid = new Guid("57785AFC-670A-4e9e-9AFB-5A6AD9A01AD5");

		// Token: 0x04001825 RID: 6181
		private static Trace generalTracer = null;

		// Token: 0x04001826 RID: 6182
		private static Trace unexpectedPathTracer = null;

		// Token: 0x04001827 RID: 6183
		private static Trace calendarItemValuesTracer = null;

		// Token: 0x04001828 RID: 6184
		private static Trace processingTracer = null;

		// Token: 0x04001829 RID: 6185
		private static Trace processingRequestTracer = null;

		// Token: 0x0400182A RID: 6186
		private static Trace processingResponseTracer = null;

		// Token: 0x0400182B RID: 6187
		private static Trace processingCancellationTracer = null;

		// Token: 0x0400182C RID: 6188
		private static Trace cachedStateTracer = null;

		// Token: 0x0400182D RID: 6189
		private static Trace oldMessageDeletionTracer = null;

		// Token: 0x0400182E RID: 6190
		private static Trace pFDTracer = null;

		// Token: 0x0400182F RID: 6191
		private static Trace processingMeetingForwardNotificationTracer = null;
	}
}
