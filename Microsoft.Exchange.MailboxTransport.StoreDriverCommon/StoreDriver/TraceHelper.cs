using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriver;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission;
using Microsoft.Exchange.Diagnostics.Components.SubmissionService;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x02000034 RID: 52
	internal static class TraceHelper
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00009CC1 File Offset: 0x00007EC1
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00009CC8 File Offset: 0x00007EC8
		public static Guid MessageProbeActivityId
		{
			get
			{
				return TraceHelper.messageProbeActivityId;
			}
			internal set
			{
				TraceHelper.messageProbeActivityId = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00009CD0 File Offset: 0x00007ED0
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00009CD7 File Offset: 0x00007ED7
		public static TraceHelper.LamNotificationIdParts? MessageProbeLamNotificationIdParts
		{
			get
			{
				return TraceHelper.messageProbeLamNotificationIdParts;
			}
			internal set
			{
				TraceHelper.messageProbeLamNotificationIdParts = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00009CDF File Offset: 0x00007EDF
		public static bool IsMessageAMapiSubmitSystemProbe
		{
			get
			{
				return TraceHelper.messageProbeActivityId != Guid.Empty;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00009CF0 File Offset: 0x00007EF0
		public static bool IsMessageAMapiSubmitLAMProbe
		{
			get
			{
				return TraceHelper.messageProbeLamNotificationIdParts != null;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00009CFC File Offset: 0x00007EFC
		public static SystemProbeTrace ServiceTracer
		{
			get
			{
				return TraceHelper.serviceTracer;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00009D03 File Offset: 0x00007F03
		public static SystemProbeTrace ModeratedTransportTracer
		{
			get
			{
				return TraceHelper.moderatedTransportTracer;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00009D0A File Offset: 0x00007F0A
		public static SystemProbeTrace MeetingForwardNotificationTracer
		{
			get
			{
				return TraceHelper.meetingForwardNotificationTracer;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00009D11 File Offset: 0x00007F11
		public static SystemProbeTrace ParkedItemSubmitterAgentTracer
		{
			get
			{
				return TraceHelper.parkedItemSubmitterAgentTracer;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00009D18 File Offset: 0x00007F18
		public static SystemProbeTrace SubmissionConnectionTracer
		{
			get
			{
				return TraceHelper.submissionConnectionTracer;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00009D1F File Offset: 0x00007F1F
		public static SystemProbeTrace SubmissionConnectionPoolTracer
		{
			get
			{
				return TraceHelper.submissionConnectionPoolTracer;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00009D26 File Offset: 0x00007F26
		public static SystemProbeTrace ExtensibilityTracer
		{
			get
			{
				return TraceHelper.extensibilityTracer;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00009D2D File Offset: 0x00007F2D
		public static SystemProbeTrace MapiStoreDriverSubmissionTracer
		{
			get
			{
				return TraceHelper.mapiStoreDriverSubmissionTracer;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00009D34 File Offset: 0x00007F34
		public static SystemProbeTrace StoreDriverSubmissionTracer
		{
			get
			{
				return TraceHelper.storeDriverSubmissionTracer;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00009D3B File Offset: 0x00007F3B
		public static SystemProbeTrace StoreDriverDeliveryTracer
		{
			get
			{
				return TraceHelper.storeDriverDeliveryTracer;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00009D42 File Offset: 0x00007F42
		public static SystemProbeTrace SmtpSendTracer
		{
			get
			{
				return TraceHelper.smtpSendTracer;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00009D49 File Offset: 0x00007F49
		public static SystemProbeTrace StoreDriverTracer
		{
			get
			{
				return TraceHelper.storeDriverTracer;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00009D50 File Offset: 0x00007F50
		public static SystemProbeTrace GeneralTracer
		{
			get
			{
				return TraceHelper.generalTracer;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00009D58 File Offset: 0x00007F58
		public static void TracePass(Trace tracer, long etlTraceId, string formatString, params object[] args)
		{
			SystemProbeTrace systemProbeTrace = new SystemProbeTrace(tracer, tracer.GetType().ToString());
			systemProbeTrace.TracePass(TraceHelper.MessageProbeActivityId, etlTraceId, formatString, args);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00009D88 File Offset: 0x00007F88
		public static void TraceFail(Trace tracer, long etlTraceId, string formatString, params object[] args)
		{
			SystemProbeTrace systemProbeTrace = new SystemProbeTrace(tracer, tracer.GetType().ToString());
			systemProbeTrace.TraceFail(TraceHelper.MessageProbeActivityId, etlTraceId, formatString, args);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009DB8 File Offset: 0x00007FB8
		public static void TraceFail(Trace tracer, long etlTraceId, string message)
		{
			SystemProbeTrace systemProbeTrace = new SystemProbeTrace(tracer, tracer.GetType().ToString());
			systemProbeTrace.TraceFail(TraceHelper.MessageProbeActivityId, etlTraceId, message);
		}

		// Token: 0x040000BB RID: 187
		[ThreadStatic]
		private static Guid messageProbeActivityId;

		// Token: 0x040000BC RID: 188
		[ThreadStatic]
		private static TraceHelper.LamNotificationIdParts? messageProbeLamNotificationIdParts;

		// Token: 0x040000BD RID: 189
		private static SystemProbeTrace serviceTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.SubmissionService.ExTraceGlobals.ServiceTracer, "MailboxTransportSubmissionService");

		// Token: 0x040000BE RID: 190
		private static SystemProbeTrace moderatedTransportTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission.ExTraceGlobals.ModeratedTransportTracer, "ModeratedTransport");

		// Token: 0x040000BF RID: 191
		private static SystemProbeTrace meetingForwardNotificationTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission.ExTraceGlobals.MeetingForwardNotificationTracer, "MeetingForwardNotification");

		// Token: 0x040000C0 RID: 192
		private static SystemProbeTrace parkedItemSubmitterAgentTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission.ExTraceGlobals.ParkedItemSubmitterAgentTracer, "ParkedItemSubmitterAgent");

		// Token: 0x040000C1 RID: 193
		private static SystemProbeTrace submissionConnectionTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission.ExTraceGlobals.SubmissionConnectionTracer, "SubmissionConnection");

		// Token: 0x040000C2 RID: 194
		private static SystemProbeTrace submissionConnectionPoolTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission.ExTraceGlobals.SubmissionConnectionPoolTracer, "SubmissionConnectionPool");

		// Token: 0x040000C3 RID: 195
		private static SystemProbeTrace extensibilityTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.Transport.ExTraceGlobals.ExtensibilityTracer, "Extensibility");

		// Token: 0x040000C4 RID: 196
		private static SystemProbeTrace mapiStoreDriverSubmissionTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission.ExTraceGlobals.MapiStoreDriverSubmissionTracer, "MapiStoreDriverSubmission");

		// Token: 0x040000C5 RID: 197
		private static SystemProbeTrace storeDriverSubmissionTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission.ExTraceGlobals.StoreDriverSubmissionTracer, "StoreDriverSubmission");

		// Token: 0x040000C6 RID: 198
		private static SystemProbeTrace storeDriverDeliveryTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery.ExTraceGlobals.StoreDriverDeliveryTracer, "StoreDriverDelivery");

		// Token: 0x040000C7 RID: 199
		private static SystemProbeTrace storeDriverTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.StoreDriver.ExTraceGlobals.StoreDriverTracer, "StoreDriver");

		// Token: 0x040000C8 RID: 200
		private static SystemProbeTrace smtpSendTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.Transport.ExTraceGlobals.SmtpSendTracer, "SmtpSend");

		// Token: 0x040000C9 RID: 201
		private static SystemProbeTrace generalTracer = new SystemProbeTrace(Microsoft.Exchange.Diagnostics.Components.Transport.ExTraceGlobals.GeneralTracer, "General");

		// Token: 0x02000035 RID: 53
		internal struct LamNotificationIdParts
		{
			// Token: 0x060001C2 RID: 450 RVA: 0x00009EF5 File Offset: 0x000080F5
			private LamNotificationIdParts(string lamNotificationId, string serviceName, string componentName, Guid lamNotificationIdGuid, long lamNotificationSequenceNumber)
			{
				this.lamNotificationId = lamNotificationId;
				this.serviceName = serviceName;
				this.componentName = componentName;
				this.lamNotificationIdGuid = lamNotificationIdGuid;
				this.lamNotificationSequenceNumber = lamNotificationSequenceNumber;
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x060001C3 RID: 451 RVA: 0x00009F1C File Offset: 0x0000811C
			public string ServiceName
			{
				get
				{
					return this.serviceName;
				}
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x060001C4 RID: 452 RVA: 0x00009F24 File Offset: 0x00008124
			public string ComponentName
			{
				get
				{
					return this.componentName;
				}
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x060001C5 RID: 453 RVA: 0x00009F2C File Offset: 0x0000812C
			public Guid LamNotificationIdGuid
			{
				get
				{
					return this.lamNotificationIdGuid;
				}
			}

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x060001C6 RID: 454 RVA: 0x00009F34 File Offset: 0x00008134
			public long LamNotificationSequenceNumber
			{
				get
				{
					return this.lamNotificationSequenceNumber;
				}
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x060001C7 RID: 455 RVA: 0x00009F3C File Offset: 0x0000813C
			public string LamNotificationId
			{
				get
				{
					return this.lamNotificationId;
				}
			}

			// Token: 0x060001C8 RID: 456 RVA: 0x00009F44 File Offset: 0x00008144
			public static TraceHelper.LamNotificationIdParts Parse(string lamNotificationId)
			{
				if (string.IsNullOrEmpty(lamNotificationId))
				{
					throw new ArgumentException("lamNotificationId cannot be null or empty");
				}
				string[] array = lamNotificationId.Split(new char[]
				{
					'/'
				});
				if (array.Length != 3)
				{
					throw new ArgumentException(string.Format("lamNotificationId: {0} does not have 2 '/'s.", lamNotificationId));
				}
				string text = array[0];
				string text2 = array[1];
				Guid empty = Guid.Empty;
				long num = -1L;
				if (!Guid.TryParse(array[2], out empty))
				{
					throw new ArgumentException(string.Format("lamNotificationId: {0} does not have a Guid in the last part, after the 2nd '/'.", lamNotificationId));
				}
				string[] array2 = array[2].Split(new char[]
				{
					'-'
				});
				if (array2.Length != 5)
				{
					throw new ArgumentException(string.Format("lamNotificationId: {0} does not have 4 '-'s in the probeIdSeqNum part.", lamNotificationId));
				}
				if (!long.TryParse(array2[4], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num))
				{
					throw new ArgumentException(string.Format("lamNotificationId: {0} does not have an integer in the SeqNum part.", lamNotificationId));
				}
				return new TraceHelper.LamNotificationIdParts(lamNotificationId, text, text2, empty, num);
			}

			// Token: 0x040000CA RID: 202
			private readonly string serviceName;

			// Token: 0x040000CB RID: 203
			private readonly string componentName;

			// Token: 0x040000CC RID: 204
			private readonly Guid lamNotificationIdGuid;

			// Token: 0x040000CD RID: 205
			private readonly long lamNotificationSequenceNumber;

			// Token: 0x040000CE RID: 206
			private readonly string lamNotificationId;
		}
	}
}
