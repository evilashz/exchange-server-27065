using System;

namespace Microsoft.Exchange.Diagnostics.Components.IPListGenerators
{
	// Token: 0x020003CD RID: 973
	public static class ExTraceGlobals
	{
		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x00059C12 File Offset: 0x00057E12
		public static Trace CommonTracer
		{
			get
			{
				if (ExTraceGlobals.commonTracer == null)
				{
					ExTraceGlobals.commonTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.commonTracer;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x00059C30 File Offset: 0x00057E30
		public static Trace IPListGeneratorTracer
		{
			get
			{
				if (ExTraceGlobals.iPListGeneratorTracer == null)
				{
					ExTraceGlobals.iPListGeneratorTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.iPListGeneratorTracer;
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x00059C4E File Offset: 0x00057E4E
		public static Trace RWBLListGeneratorTracer
		{
			get
			{
				if (ExTraceGlobals.rWBLListGeneratorTracer == null)
				{
					ExTraceGlobals.rWBLListGeneratorTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.rWBLListGeneratorTracer;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x00059C6C File Offset: 0x00057E6C
		public static Trace TBLListGeneratorTracer
		{
			get
			{
				if (ExTraceGlobals.tBLListGeneratorTracer == null)
				{
					ExTraceGlobals.tBLListGeneratorTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.tBLListGeneratorTracer;
			}
		}

		// Token: 0x04001C03 RID: 7171
		private static Guid componentGuid = new Guid("4A1C4EB6-CEAC-42f3-A708-3FF1536B0DD7");

		// Token: 0x04001C04 RID: 7172
		private static Trace commonTracer = null;

		// Token: 0x04001C05 RID: 7173
		private static Trace iPListGeneratorTracer = null;

		// Token: 0x04001C06 RID: 7174
		private static Trace rWBLListGeneratorTracer = null;

		// Token: 0x04001C07 RID: 7175
		private static Trace tBLListGeneratorTracer = null;
	}
}
