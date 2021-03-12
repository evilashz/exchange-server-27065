using System;

namespace Microsoft.Exchange.Diagnostics.Components.SystemLogging
{
	// Token: 0x02000314 RID: 788
	public static class ExTraceGlobals
	{
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0004A916 File Offset: 0x00048B16
		public static Trace SystemNetTracer
		{
			get
			{
				if (ExTraceGlobals.systemNetTracer == null)
				{
					ExTraceGlobals.systemNetTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.systemNetTracer;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x0004A934 File Offset: 0x00048B34
		public static Trace SystemNetSocketTracer
		{
			get
			{
				if (ExTraceGlobals.systemNetSocketTracer == null)
				{
					ExTraceGlobals.systemNetSocketTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.systemNetSocketTracer;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x0004A952 File Offset: 0x00048B52
		public static Trace SystemNetHttpListenerTracer
		{
			get
			{
				if (ExTraceGlobals.systemNetHttpListenerTracer == null)
				{
					ExTraceGlobals.systemNetHttpListenerTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.systemNetHttpListenerTracer;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x0004A970 File Offset: 0x00048B70
		public static Trace SystemIdentityModelTraceTracer
		{
			get
			{
				if (ExTraceGlobals.systemIdentityModelTraceTracer == null)
				{
					ExTraceGlobals.systemIdentityModelTraceTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.systemIdentityModelTraceTracer;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0004A98E File Offset: 0x00048B8E
		public static Trace SystemServiceModelTraceTracer
		{
			get
			{
				if (ExTraceGlobals.systemServiceModelTraceTracer == null)
				{
					ExTraceGlobals.systemServiceModelTraceTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.systemServiceModelTraceTracer;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x0004A9AC File Offset: 0x00048BAC
		public static Trace SystemServiceModelMessageLoggingTracer
		{
			get
			{
				if (ExTraceGlobals.systemServiceModelMessageLoggingTracer == null)
				{
					ExTraceGlobals.systemServiceModelMessageLoggingTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.systemServiceModelMessageLoggingTracer;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0004A9CA File Offset: 0x00048BCA
		public static Trace SystemServiceModelMessageLogging_LogMalformedMessagesTracer
		{
			get
			{
				if (ExTraceGlobals.systemServiceModelMessageLogging_LogMalformedMessagesTracer == null)
				{
					ExTraceGlobals.systemServiceModelMessageLogging_LogMalformedMessagesTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.systemServiceModelMessageLogging_LogMalformedMessagesTracer;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0004A9E8 File Offset: 0x00048BE8
		public static Trace SystemServiceModelMessageLogging_LogMessagesAtServiceLevelTracer
		{
			get
			{
				if (ExTraceGlobals.systemServiceModelMessageLogging_LogMessagesAtServiceLevelTracer == null)
				{
					ExTraceGlobals.systemServiceModelMessageLogging_LogMessagesAtServiceLevelTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.systemServiceModelMessageLogging_LogMessagesAtServiceLevelTracer;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0004AA06 File Offset: 0x00048C06
		public static Trace SystemServiceModelMessageLogging_LogMessagesAtTransportLevelTracer
		{
			get
			{
				if (ExTraceGlobals.systemServiceModelMessageLogging_LogMessagesAtTransportLevelTracer == null)
				{
					ExTraceGlobals.systemServiceModelMessageLogging_LogMessagesAtTransportLevelTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.systemServiceModelMessageLogging_LogMessagesAtTransportLevelTracer;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x0004AA24 File Offset: 0x00048C24
		public static Trace SystemServiceModelMessageLogging_LogMessageBodyTracer
		{
			get
			{
				if (ExTraceGlobals.systemServiceModelMessageLogging_LogMessageBodyTracer == null)
				{
					ExTraceGlobals.systemServiceModelMessageLogging_LogMessageBodyTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.systemServiceModelMessageLogging_LogMessageBodyTracer;
			}
		}

		// Token: 0x0400150C RID: 5388
		private static Guid componentGuid = new Guid("F21F1E57-9689-46E5-BE7D-A84C9BCE0D94");

		// Token: 0x0400150D RID: 5389
		private static Trace systemNetTracer = null;

		// Token: 0x0400150E RID: 5390
		private static Trace systemNetSocketTracer = null;

		// Token: 0x0400150F RID: 5391
		private static Trace systemNetHttpListenerTracer = null;

		// Token: 0x04001510 RID: 5392
		private static Trace systemIdentityModelTraceTracer = null;

		// Token: 0x04001511 RID: 5393
		private static Trace systemServiceModelTraceTracer = null;

		// Token: 0x04001512 RID: 5394
		private static Trace systemServiceModelMessageLoggingTracer = null;

		// Token: 0x04001513 RID: 5395
		private static Trace systemServiceModelMessageLogging_LogMalformedMessagesTracer = null;

		// Token: 0x04001514 RID: 5396
		private static Trace systemServiceModelMessageLogging_LogMessagesAtServiceLevelTracer = null;

		// Token: 0x04001515 RID: 5397
		private static Trace systemServiceModelMessageLogging_LogMessagesAtTransportLevelTracer = null;

		// Token: 0x04001516 RID: 5398
		private static Trace systemServiceModelMessageLogging_LogMessageBodyTracer = null;
	}
}
