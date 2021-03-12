using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.FfoAuthorization
{
	// Token: 0x020003DE RID: 990
	public static class ExTraceGlobals
	{
		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x0005A147 File Offset: 0x00058347
		public static Trace FfoRunspaceTracer
		{
			get
			{
				if (ExTraceGlobals.ffoRunspaceTracer == null)
				{
					ExTraceGlobals.ffoRunspaceTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.ffoRunspaceTracer;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x0005A165 File Offset: 0x00058365
		public static Trace PartnerConfigTracer
		{
			get
			{
				if (ExTraceGlobals.partnerConfigTracer == null)
				{
					ExTraceGlobals.partnerConfigTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.partnerConfigTracer;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x0005A183 File Offset: 0x00058383
		public static Trace FfoRpsTracer
		{
			get
			{
				if (ExTraceGlobals.ffoRpsTracer == null)
				{
					ExTraceGlobals.ffoRpsTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.ffoRpsTracer;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x0005A1A1 File Offset: 0x000583A1
		public static Trace FfoRpsBudgetTracer
		{
			get
			{
				if (ExTraceGlobals.ffoRpsBudgetTracer == null)
				{
					ExTraceGlobals.ffoRpsBudgetTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.ffoRpsBudgetTracer;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x0005A1BF File Offset: 0x000583BF
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x0005A1DD File Offset: 0x000583DD
		public static Trace FfoServicePlansTracer
		{
			get
			{
				if (ExTraceGlobals.ffoServicePlansTracer == null)
				{
					ExTraceGlobals.ffoServicePlansTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.ffoServicePlansTracer;
			}
		}

		// Token: 0x04001C31 RID: 7217
		private static Guid componentGuid = new Guid("2AEBD40A-8FA5-4159-A644-54F41B37D965");

		// Token: 0x04001C32 RID: 7218
		private static Trace ffoRunspaceTracer = null;

		// Token: 0x04001C33 RID: 7219
		private static Trace partnerConfigTracer = null;

		// Token: 0x04001C34 RID: 7220
		private static Trace ffoRpsTracer = null;

		// Token: 0x04001C35 RID: 7221
		private static Trace ffoRpsBudgetTracer = null;

		// Token: 0x04001C36 RID: 7222
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001C37 RID: 7223
		private static Trace ffoServicePlansTracer = null;
	}
}
