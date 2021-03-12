using System;

namespace Microsoft.Exchange.Diagnostics.Components.Configuration.Core
{
	// Token: 0x0200036F RID: 879
	public static class ExTraceGlobals
	{
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x00053E83 File Offset: 0x00052083
		public static Trace HttpModuleTracer
		{
			get
			{
				if (ExTraceGlobals.httpModuleTracer == null)
				{
					ExTraceGlobals.httpModuleTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.httpModuleTracer;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x00053EA1 File Offset: 0x000520A1
		public static Trace InstrumentationTracer
		{
			get
			{
				if (ExTraceGlobals.instrumentationTracer == null)
				{
					ExTraceGlobals.instrumentationTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.instrumentationTracer;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x00053EBF File Offset: 0x000520BF
		public static Trace UserTokenTracer
		{
			get
			{
				if (ExTraceGlobals.userTokenTracer == null)
				{
					ExTraceGlobals.userTokenTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.userTokenTracer;
			}
		}

		// Token: 0x04001948 RID: 6472
		private static Guid componentGuid = new Guid("0377C8B8-BA48-4B10-9D07-F1F3E5636ED4");

		// Token: 0x04001949 RID: 6473
		private static Trace httpModuleTracer = null;

		// Token: 0x0400194A RID: 6474
		private static Trace instrumentationTracer = null;

		// Token: 0x0400194B RID: 6475
		private static Trace userTokenTracer = null;
	}
}
