using System;

namespace Microsoft.Exchange.Diagnostics.Components.SchemaConverter
{
	// Token: 0x02000316 RID: 790
	public static class ExTraceGlobals
	{
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x0004AD26 File Offset: 0x00048F26
		public static Trace SchemaStateTracer
		{
			get
			{
				if (ExTraceGlobals.schemaStateTracer == null)
				{
					ExTraceGlobals.schemaStateTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.schemaStateTracer;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x0004AD44 File Offset: 0x00048F44
		public static Trace CommonTracer
		{
			get
			{
				if (ExTraceGlobals.commonTracer == null)
				{
					ExTraceGlobals.commonTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.commonTracer;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x0004AD62 File Offset: 0x00048F62
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

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x0004AD80 File Offset: 0x00048F80
		public static Trace AirSyncTracer
		{
			get
			{
				if (ExTraceGlobals.airSyncTracer == null)
				{
					ExTraceGlobals.airSyncTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.airSyncTracer;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x0004AD9E File Offset: 0x00048F9E
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

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x0004ADBC File Offset: 0x00048FBC
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

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x0004ADDA File Offset: 0x00048FDA
		public static Trace MethodEnterExitTracer
		{
			get
			{
				if (ExTraceGlobals.methodEnterExitTracer == null)
				{
					ExTraceGlobals.methodEnterExitTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.methodEnterExitTracer;
			}
		}

		// Token: 0x04001529 RID: 5417
		private static Guid componentGuid = new Guid("{7569BC27-E1CA-11D9-88B7-000D9DFFC66E}");

		// Token: 0x0400152A RID: 5418
		private static Trace schemaStateTracer = null;

		// Token: 0x0400152B RID: 5419
		private static Trace commonTracer = null;

		// Token: 0x0400152C RID: 5420
		private static Trace xsoTracer = null;

		// Token: 0x0400152D RID: 5421
		private static Trace airSyncTracer = null;

		// Token: 0x0400152E RID: 5422
		private static Trace protocolTracer = null;

		// Token: 0x0400152F RID: 5423
		private static Trace conversionTracer = null;

		// Token: 0x04001530 RID: 5424
		private static Trace methodEnterExitTracer = null;
	}
}
