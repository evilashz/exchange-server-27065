using System;

namespace Microsoft.Exchange.Diagnostics.Components.InfoWorker.MultiMailboxSearch
{
	// Token: 0x02000341 RID: 833
	public static class ExTraceGlobals
	{
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x000512FE File Offset: 0x0004F4FE
		public static Trace LocalSearchTracer
		{
			get
			{
				if (ExTraceGlobals.localSearchTracer == null)
				{
					ExTraceGlobals.localSearchTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.localSearchTracer;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x0005131C File Offset: 0x0004F51C
		public static Trace MailboxGroupGeneratorTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxGroupGeneratorTracer == null)
				{
					ExTraceGlobals.mailboxGroupGeneratorTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.mailboxGroupGeneratorTracer;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x0005133A File Offset: 0x0004F53A
		public static Trace GeneralTracer
		{
			get
			{
				if (ExTraceGlobals.generalTracer == null)
				{
					ExTraceGlobals.generalTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.generalTracer;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x00051358 File Offset: 0x0004F558
		public static Trace AutoDiscoverTracer
		{
			get
			{
				if (ExTraceGlobals.autoDiscoverTracer == null)
				{
					ExTraceGlobals.autoDiscoverTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.autoDiscoverTracer;
			}
		}

		// Token: 0x04001800 RID: 6144
		private static Guid componentGuid = new Guid("6a7f7e5b-18a1-4e29-b0c0-2514adb49e41");

		// Token: 0x04001801 RID: 6145
		private static Trace localSearchTracer = null;

		// Token: 0x04001802 RID: 6146
		private static Trace mailboxGroupGeneratorTracer = null;

		// Token: 0x04001803 RID: 6147
		private static Trace generalTracer = null;

		// Token: 0x04001804 RID: 6148
		private static Trace autoDiscoverTracer = null;
	}
}
