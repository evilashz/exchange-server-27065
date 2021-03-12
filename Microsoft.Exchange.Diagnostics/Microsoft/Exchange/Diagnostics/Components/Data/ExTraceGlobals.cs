using System;

namespace Microsoft.Exchange.Diagnostics.Components.Data
{
	// Token: 0x0200033A RID: 826
	public static class ExTraceGlobals
	{
		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x00050362 File Offset: 0x0004E562
		public static Trace PropertyBagTracer
		{
			get
			{
				if (ExTraceGlobals.propertyBagTracer == null)
				{
					ExTraceGlobals.propertyBagTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.propertyBagTracer;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x00050380 File Offset: 0x0004E580
		public static Trace ValidationTracer
		{
			get
			{
				if (ExTraceGlobals.validationTracer == null)
				{
					ExTraceGlobals.validationTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.validationTracer;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x0005039E File Offset: 0x0004E59E
		public static Trace SerializationTracer
		{
			get
			{
				if (ExTraceGlobals.serializationTracer == null)
				{
					ExTraceGlobals.serializationTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.serializationTracer;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x000503BC File Offset: 0x0004E5BC
		public static Trace ValueConvertorTracer
		{
			get
			{
				if (ExTraceGlobals.valueConvertorTracer == null)
				{
					ExTraceGlobals.valueConvertorTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.valueConvertorTracer;
			}
		}

		// Token: 0x04001790 RID: 6032
		private static Guid componentGuid = new Guid("E7FE6E6D-7B3D-4942-B672-BBFD89AC4DC5");

		// Token: 0x04001791 RID: 6033
		private static Trace propertyBagTracer = null;

		// Token: 0x04001792 RID: 6034
		private static Trace validationTracer = null;

		// Token: 0x04001793 RID: 6035
		private static Trace serializationTracer = null;

		// Token: 0x04001794 RID: 6036
		private static Trace valueConvertorTracer = null;
	}
}
