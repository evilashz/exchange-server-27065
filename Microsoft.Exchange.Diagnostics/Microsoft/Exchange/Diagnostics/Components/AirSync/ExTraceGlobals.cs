using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.AirSync
{
	// Token: 0x02000315 RID: 789
	public static class ExTraceGlobals
	{
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x0004AA9C File Offset: 0x00048C9C
		public static Trace RequestsTracer
		{
			get
			{
				if (ExTraceGlobals.requestsTracer == null)
				{
					ExTraceGlobals.requestsTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.requestsTracer;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x0004AABA File Offset: 0x00048CBA
		public static Trace WbxmlTracer
		{
			get
			{
				if (ExTraceGlobals.wbxmlTracer == null)
				{
					ExTraceGlobals.wbxmlTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.wbxmlTracer;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x0004AAD8 File Offset: 0x00048CD8
		public static Trace XsoTracer
		{
			get
			{
				if (ExTraceGlobals.xsoTracer == null)
				{
					ExTraceGlobals.xsoTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.xsoTracer;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x0004AAF6 File Offset: 0x00048CF6
		public static Trace AlgorithmTracer
		{
			get
			{
				if (ExTraceGlobals.algorithmTracer == null)
				{
					ExTraceGlobals.algorithmTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.algorithmTracer;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x0004AB14 File Offset: 0x00048D14
		public static Trace ProtocolTracer
		{
			get
			{
				if (ExTraceGlobals.protocolTracer == null)
				{
					ExTraceGlobals.protocolTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.protocolTracer;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x0004AB32 File Offset: 0x00048D32
		public static Trace ConversionTracer
		{
			get
			{
				if (ExTraceGlobals.conversionTracer == null)
				{
					ExTraceGlobals.conversionTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.conversionTracer;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x0004AB50 File Offset: 0x00048D50
		public static Trace ThreadPoolTracer
		{
			get
			{
				if (ExTraceGlobals.threadPoolTracer == null)
				{
					ExTraceGlobals.threadPoolTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.threadPoolTracer;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0004AB6E File Offset: 0x00048D6E
		public static Trace RawBodyBytesTracer
		{
			get
			{
				if (ExTraceGlobals.rawBodyBytesTracer == null)
				{
					ExTraceGlobals.rawBodyBytesTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.rawBodyBytesTracer;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x0004AB8C File Offset: 0x00048D8C
		public static Trace MethodEnterExitTracer
		{
			get
			{
				if (ExTraceGlobals.methodEnterExitTracer == null)
				{
					ExTraceGlobals.methodEnterExitTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.methodEnterExitTracer;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0004ABAA File Offset: 0x00048DAA
		public static Trace TiUpgradeTracer
		{
			get
			{
				if (ExTraceGlobals.tiUpgradeTracer == null)
				{
					ExTraceGlobals.tiUpgradeTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.tiUpgradeTracer;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x0004ABC9 File Offset: 0x00048DC9
		public static Trace ValidationTracer
		{
			get
			{
				if (ExTraceGlobals.validationTracer == null)
				{
					ExTraceGlobals.validationTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.validationTracer;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0004ABE8 File Offset: 0x00048DE8
		public static Trace PfdInitTraceTracer
		{
			get
			{
				if (ExTraceGlobals.pfdInitTraceTracer == null)
				{
					ExTraceGlobals.pfdInitTraceTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.pfdInitTraceTracer;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x0004AC07 File Offset: 0x00048E07
		public static Trace CorruptItemTracer
		{
			get
			{
				if (ExTraceGlobals.corruptItemTracer == null)
				{
					ExTraceGlobals.corruptItemTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.corruptItemTracer;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x0004AC26 File Offset: 0x00048E26
		public static Trace ThreadingTracer
		{
			get
			{
				if (ExTraceGlobals.threadingTracer == null)
				{
					ExTraceGlobals.threadingTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.threadingTracer;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x0004AC45 File Offset: 0x00048E45
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x0004AC64 File Offset: 0x00048E64
		public static Trace BodyTracer
		{
			get
			{
				if (ExTraceGlobals.bodyTracer == null)
				{
					ExTraceGlobals.bodyTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.bodyTracer;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x0004AC83 File Offset: 0x00048E83
		public static Trace DiagnosticsTracer
		{
			get
			{
				if (ExTraceGlobals.diagnosticsTracer == null)
				{
					ExTraceGlobals.diagnosticsTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.diagnosticsTracer;
			}
		}

		// Token: 0x04001517 RID: 5399
		private static Guid componentGuid = new Guid("5e88fb2c-0a36-41f2-a710-c911bfe18e44");

		// Token: 0x04001518 RID: 5400
		private static Trace requestsTracer = null;

		// Token: 0x04001519 RID: 5401
		private static Trace wbxmlTracer = null;

		// Token: 0x0400151A RID: 5402
		private static Trace xsoTracer = null;

		// Token: 0x0400151B RID: 5403
		private static Trace algorithmTracer = null;

		// Token: 0x0400151C RID: 5404
		private static Trace protocolTracer = null;

		// Token: 0x0400151D RID: 5405
		private static Trace conversionTracer = null;

		// Token: 0x0400151E RID: 5406
		private static Trace threadPoolTracer = null;

		// Token: 0x0400151F RID: 5407
		private static Trace rawBodyBytesTracer = null;

		// Token: 0x04001520 RID: 5408
		private static Trace methodEnterExitTracer = null;

		// Token: 0x04001521 RID: 5409
		private static Trace tiUpgradeTracer = null;

		// Token: 0x04001522 RID: 5410
		private static Trace validationTracer = null;

		// Token: 0x04001523 RID: 5411
		private static Trace pfdInitTraceTracer = null;

		// Token: 0x04001524 RID: 5412
		private static Trace corruptItemTracer = null;

		// Token: 0x04001525 RID: 5413
		private static Trace threadingTracer = null;

		// Token: 0x04001526 RID: 5414
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001527 RID: 5415
		private static Trace bodyTracer = null;

		// Token: 0x04001528 RID: 5416
		private static Trace diagnosticsTracer = null;
	}
}
