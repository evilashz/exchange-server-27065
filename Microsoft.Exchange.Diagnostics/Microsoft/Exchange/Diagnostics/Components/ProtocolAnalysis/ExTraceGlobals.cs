using System;

namespace Microsoft.Exchange.Diagnostics.Components.ProtocolAnalysis
{
	// Token: 0x02000389 RID: 905
	public static class ExTraceGlobals
	{
		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x00055CD7 File Offset: 0x00053ED7
		public static Trace FactoryTracer
		{
			get
			{
				if (ExTraceGlobals.factoryTracer == null)
				{
					ExTraceGlobals.factoryTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.factoryTracer;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x00055CF5 File Offset: 0x00053EF5
		public static Trace DatabaseTracer
		{
			get
			{
				if (ExTraceGlobals.databaseTracer == null)
				{
					ExTraceGlobals.databaseTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.databaseTracer;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x00055D13 File Offset: 0x00053F13
		public static Trace CalculateSrlTracer
		{
			get
			{
				if (ExTraceGlobals.calculateSrlTracer == null)
				{
					ExTraceGlobals.calculateSrlTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.calculateSrlTracer;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00055D31 File Offset: 0x00053F31
		public static Trace OnMailFromTracer
		{
			get
			{
				if (ExTraceGlobals.onMailFromTracer == null)
				{
					ExTraceGlobals.onMailFromTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.onMailFromTracer;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x00055D4F File Offset: 0x00053F4F
		public static Trace OnRcptToTracer
		{
			get
			{
				if (ExTraceGlobals.onRcptToTracer == null)
				{
					ExTraceGlobals.onRcptToTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.onRcptToTracer;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x00055D6D File Offset: 0x00053F6D
		public static Trace OnEODTracer
		{
			get
			{
				if (ExTraceGlobals.onEODTracer == null)
				{
					ExTraceGlobals.onEODTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.onEODTracer;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x00055D8B File Offset: 0x00053F8B
		public static Trace RejectTracer
		{
			get
			{
				if (ExTraceGlobals.rejectTracer == null)
				{
					ExTraceGlobals.rejectTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.rejectTracer;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x00055DA9 File Offset: 0x00053FA9
		public static Trace DisconnectTracer
		{
			get
			{
				if (ExTraceGlobals.disconnectTracer == null)
				{
					ExTraceGlobals.disconnectTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.disconnectTracer;
			}
		}

		// Token: 0x04001A29 RID: 6697
		private static Guid componentGuid = new Guid("A0F3DC2A-7FD4-491E-C176-4857EAF2D7EF");

		// Token: 0x04001A2A RID: 6698
		private static Trace factoryTracer = null;

		// Token: 0x04001A2B RID: 6699
		private static Trace databaseTracer = null;

		// Token: 0x04001A2C RID: 6700
		private static Trace calculateSrlTracer = null;

		// Token: 0x04001A2D RID: 6701
		private static Trace onMailFromTracer = null;

		// Token: 0x04001A2E RID: 6702
		private static Trace onRcptToTracer = null;

		// Token: 0x04001A2F RID: 6703
		private static Trace onEODTracer = null;

		// Token: 0x04001A30 RID: 6704
		private static Trace rejectTracer = null;

		// Token: 0x04001A31 RID: 6705
		private static Trace disconnectTracer = null;
	}
}
