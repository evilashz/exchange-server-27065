using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Approval
{
	// Token: 0x02000359 RID: 857
	public static class ExTraceGlobals
	{
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x000523C5 File Offset: 0x000505C5
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

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x000523E3 File Offset: 0x000505E3
		public static Trace CachedStateTracer
		{
			get
			{
				if (ExTraceGlobals.cachedStateTracer == null)
				{
					ExTraceGlobals.cachedStateTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.cachedStateTracer;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x00052401 File Offset: 0x00050601
		public static Trace PFDTracer
		{
			get
			{
				if (ExTraceGlobals.pFDTracer == null)
				{
					ExTraceGlobals.pFDTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.pFDTracer;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0005241F File Offset: 0x0005061F
		public static Trace AutoGroupTracer
		{
			get
			{
				if (ExTraceGlobals.autoGroupTracer == null)
				{
					ExTraceGlobals.autoGroupTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.autoGroupTracer;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x0005243D File Offset: 0x0005063D
		public static Trace ModeratedTransportTracer
		{
			get
			{
				if (ExTraceGlobals.moderatedTransportTracer == null)
				{
					ExTraceGlobals.moderatedTransportTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.moderatedTransportTracer;
			}
		}

		// Token: 0x04001882 RID: 6274
		private static Guid componentGuid = new Guid("B37B1146-EBE5-4078-9F5D-4B08C52F73DE");

		// Token: 0x04001883 RID: 6275
		private static Trace generalTracer = null;

		// Token: 0x04001884 RID: 6276
		private static Trace cachedStateTracer = null;

		// Token: 0x04001885 RID: 6277
		private static Trace pFDTracer = null;

		// Token: 0x04001886 RID: 6278
		private static Trace autoGroupTracer = null;

		// Token: 0x04001887 RID: 6279
		private static Trace moderatedTransportTracer = null;
	}
}
