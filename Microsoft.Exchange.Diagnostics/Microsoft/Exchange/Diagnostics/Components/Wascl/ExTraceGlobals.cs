using System;

namespace Microsoft.Exchange.Diagnostics.Components.Wascl
{
	// Token: 0x0200040B RID: 1035
	public static class ExTraceGlobals
	{
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x0005D53D File Offset: 0x0005B73D
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

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x0005D55B File Offset: 0x0005B75B
		public static Trace CoreTracer
		{
			get
			{
				if (ExTraceGlobals.coreTracer == null)
				{
					ExTraceGlobals.coreTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.coreTracer;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x0005D579 File Offset: 0x0005B779
		public static Trace VerdictTracer
		{
			get
			{
				if (ExTraceGlobals.verdictTracer == null)
				{
					ExTraceGlobals.verdictTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.verdictTracer;
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x0005D597 File Offset: 0x0005B797
		public static Trace ExternalCallTracer
		{
			get
			{
				if (ExTraceGlobals.externalCallTracer == null)
				{
					ExTraceGlobals.externalCallTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.externalCallTracer;
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06001908 RID: 6408 RVA: 0x0005D5B5 File Offset: 0x0005B7B5
		public static Trace APITracer
		{
			get
			{
				if (ExTraceGlobals.aPITracer == null)
				{
					ExTraceGlobals.aPITracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.aPITracer;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x0005D5D3 File Offset: 0x0005B7D3
		public static Trace CryptoHelperTracer
		{
			get
			{
				if (ExTraceGlobals.cryptoHelperTracer == null)
				{
					ExTraceGlobals.cryptoHelperTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.cryptoHelperTracer;
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600190A RID: 6410 RVA: 0x0005D5F1 File Offset: 0x0005B7F1
		public static Trace OSETracer
		{
			get
			{
				if (ExTraceGlobals.oSETracer == null)
				{
					ExTraceGlobals.oSETracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.oSETracer;
			}
		}

		// Token: 0x04001DB2 RID: 7602
		private static Guid componentGuid = new Guid("48076FB3-30FE-460B-975D-934742F529EA");

		// Token: 0x04001DB3 RID: 7603
		private static Trace generalTracer = null;

		// Token: 0x04001DB4 RID: 7604
		private static Trace coreTracer = null;

		// Token: 0x04001DB5 RID: 7605
		private static Trace verdictTracer = null;

		// Token: 0x04001DB6 RID: 7606
		private static Trace externalCallTracer = null;

		// Token: 0x04001DB7 RID: 7607
		private static Trace aPITracer = null;

		// Token: 0x04001DB8 RID: 7608
		private static Trace cryptoHelperTracer = null;

		// Token: 0x04001DB9 RID: 7609
		private static Trace oSETracer = null;
	}
}
