using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxReplicationService
{
	// Token: 0x0200037A RID: 890
	public static class ExTraceGlobals
	{
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x00054E5C File Offset: 0x0005305C
		public static Trace MailboxReplicationServiceTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxReplicationServiceTracer == null)
				{
					ExTraceGlobals.mailboxReplicationServiceTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.mailboxReplicationServiceTracer;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x00054E7A File Offset: 0x0005307A
		public static Trace MailboxReplicationServiceProviderTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxReplicationServiceProviderTracer == null)
				{
					ExTraceGlobals.mailboxReplicationServiceProviderTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.mailboxReplicationServiceProviderTracer;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x00054E98 File Offset: 0x00053098
		public static Trace MailboxReplicationProxyClientTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxReplicationProxyClientTracer == null)
				{
					ExTraceGlobals.mailboxReplicationProxyClientTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.mailboxReplicationProxyClientTracer;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x00054EB6 File Offset: 0x000530B6
		public static Trace MailboxReplicationProxyServiceTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxReplicationProxyServiceTracer == null)
				{
					ExTraceGlobals.mailboxReplicationProxyServiceTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.mailboxReplicationProxyServiceTracer;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x00054ED4 File Offset: 0x000530D4
		public static Trace MailboxReplicationCmdletTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxReplicationCmdletTracer == null)
				{
					ExTraceGlobals.mailboxReplicationCmdletTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.mailboxReplicationCmdletTracer;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x00054EF2 File Offset: 0x000530F2
		public static Trace MailboxReplicationUpdateMovedMailboxTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxReplicationUpdateMovedMailboxTracer == null)
				{
					ExTraceGlobals.mailboxReplicationUpdateMovedMailboxTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.mailboxReplicationUpdateMovedMailboxTracer;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x00054F10 File Offset: 0x00053110
		public static Trace MailboxReplicationServiceThrottlingTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxReplicationServiceThrottlingTracer == null)
				{
					ExTraceGlobals.mailboxReplicationServiceThrottlingTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.mailboxReplicationServiceThrottlingTracer;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x00054F2E File Offset: 0x0005312E
		public static Trace MailboxReplicationAuthorizationTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxReplicationAuthorizationTracer == null)
				{
					ExTraceGlobals.mailboxReplicationAuthorizationTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.mailboxReplicationAuthorizationTracer;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x00054F4C File Offset: 0x0005314C
		public static Trace MailboxReplicationCommonTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxReplicationCommonTracer == null)
				{
					ExTraceGlobals.mailboxReplicationCommonTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.mailboxReplicationCommonTracer;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x00054F6A File Offset: 0x0005316A
		public static Trace MailboxReplicationResourceHealthTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxReplicationResourceHealthTracer == null)
				{
					ExTraceGlobals.mailboxReplicationResourceHealthTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.mailboxReplicationResourceHealthTracer;
			}
		}

		// Token: 0x040019BC RID: 6588
		private static Guid componentGuid = new Guid("1141b405-c15a-48ef-a440-ab2f44a9cdac");

		// Token: 0x040019BD RID: 6589
		private static Trace mailboxReplicationServiceTracer = null;

		// Token: 0x040019BE RID: 6590
		private static Trace mailboxReplicationServiceProviderTracer = null;

		// Token: 0x040019BF RID: 6591
		private static Trace mailboxReplicationProxyClientTracer = null;

		// Token: 0x040019C0 RID: 6592
		private static Trace mailboxReplicationProxyServiceTracer = null;

		// Token: 0x040019C1 RID: 6593
		private static Trace mailboxReplicationCmdletTracer = null;

		// Token: 0x040019C2 RID: 6594
		private static Trace mailboxReplicationUpdateMovedMailboxTracer = null;

		// Token: 0x040019C3 RID: 6595
		private static Trace mailboxReplicationServiceThrottlingTracer = null;

		// Token: 0x040019C4 RID: 6596
		private static Trace mailboxReplicationAuthorizationTracer = null;

		// Token: 0x040019C5 RID: 6597
		private static Trace mailboxReplicationCommonTracer = null;

		// Token: 0x040019C6 RID: 6598
		private static Trace mailboxReplicationResourceHealthTracer = null;
	}
}
