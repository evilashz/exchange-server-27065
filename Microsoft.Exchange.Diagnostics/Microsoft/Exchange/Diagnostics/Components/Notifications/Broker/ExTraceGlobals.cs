using System;

namespace Microsoft.Exchange.Diagnostics.Components.Notifications.Broker
{
	// Token: 0x02000406 RID: 1030
	public static class ExTraceGlobals
	{
		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0005CE26 File Offset: 0x0005B026
		public static Trace ClientTracer
		{
			get
			{
				if (ExTraceGlobals.clientTracer == null)
				{
					ExTraceGlobals.clientTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.clientTracer;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x0005CE44 File Offset: 0x0005B044
		public static Trace ServiceTracer
		{
			get
			{
				if (ExTraceGlobals.serviceTracer == null)
				{
					ExTraceGlobals.serviceTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.serviceTracer;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0005CE62 File Offset: 0x0005B062
		public static Trace MailboxChangeTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxChangeTracer == null)
				{
					ExTraceGlobals.mailboxChangeTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.mailboxChangeTracer;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x0005CE80 File Offset: 0x0005B080
		public static Trace SubscriptionsTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionsTracer == null)
				{
					ExTraceGlobals.subscriptionsTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.subscriptionsTracer;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0005CE9E File Offset: 0x0005B09E
		public static Trace RemoteConduitTracer
		{
			get
			{
				if (ExTraceGlobals.remoteConduitTracer == null)
				{
					ExTraceGlobals.remoteConduitTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.remoteConduitTracer;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x0005CEBC File Offset: 0x0005B0BC
		public static Trace GeneratorTracer
		{
			get
			{
				if (ExTraceGlobals.generatorTracer == null)
				{
					ExTraceGlobals.generatorTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.generatorTracer;
			}
		}

		// Token: 0x04001D7E RID: 7550
		private static Guid componentGuid = new Guid("f16b990e-bd72-4a46-b231-b1ed417eaa17");

		// Token: 0x04001D7F RID: 7551
		private static Trace clientTracer = null;

		// Token: 0x04001D80 RID: 7552
		private static Trace serviceTracer = null;

		// Token: 0x04001D81 RID: 7553
		private static Trace mailboxChangeTracer = null;

		// Token: 0x04001D82 RID: 7554
		private static Trace subscriptionsTracer = null;

		// Token: 0x04001D83 RID: 7555
		private static Trace remoteConduitTracer = null;

		// Token: 0x04001D84 RID: 7556
		private static Trace generatorTracer = null;
	}
}
