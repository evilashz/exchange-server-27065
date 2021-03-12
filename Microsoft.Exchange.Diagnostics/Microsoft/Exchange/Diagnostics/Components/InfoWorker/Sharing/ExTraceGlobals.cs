using System;

namespace Microsoft.Exchange.Diagnostics.Components.InfoWorker.Sharing
{
	// Token: 0x0200035F RID: 863
	public static class ExTraceGlobals
	{
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x00052647 File Offset: 0x00050847
		public static Trace SharingEngineTracer
		{
			get
			{
				if (ExTraceGlobals.sharingEngineTracer == null)
				{
					ExTraceGlobals.sharingEngineTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.sharingEngineTracer;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x00052665 File Offset: 0x00050865
		public static Trace AppointmentTranslatorTracer
		{
			get
			{
				if (ExTraceGlobals.appointmentTranslatorTracer == null)
				{
					ExTraceGlobals.appointmentTranslatorTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.appointmentTranslatorTracer;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x00052683 File Offset: 0x00050883
		public static Trace ExchangeServiceTracer
		{
			get
			{
				if (ExTraceGlobals.exchangeServiceTracer == null)
				{
					ExTraceGlobals.exchangeServiceTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.exchangeServiceTracer;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x000526A1 File Offset: 0x000508A1
		public static Trace LocalFolderTracer
		{
			get
			{
				if (ExTraceGlobals.localFolderTracer == null)
				{
					ExTraceGlobals.localFolderTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.localFolderTracer;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x000526BF File Offset: 0x000508BF
		public static Trace SharingKeyHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.sharingKeyHandlerTracer == null)
				{
					ExTraceGlobals.sharingKeyHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.sharingKeyHandlerTracer;
			}
		}

		// Token: 0x04001897 RID: 6295
		private static Guid componentGuid = new Guid("A15553C6-31A1-4a7a-8526-8FABE6841235");

		// Token: 0x04001898 RID: 6296
		private static Trace sharingEngineTracer = null;

		// Token: 0x04001899 RID: 6297
		private static Trace appointmentTranslatorTracer = null;

		// Token: 0x0400189A RID: 6298
		private static Trace exchangeServiceTracer = null;

		// Token: 0x0400189B RID: 6299
		private static Trace localFolderTracer = null;

		// Token: 0x0400189C RID: 6300
		private static Trace sharingKeyHandlerTracer = null;
	}
}
