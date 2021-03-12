using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000026 RID: 38
	internal static class Tracer
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000055F0 File Offset: 0x000037F0
		// (set) Token: 0x0600015B RID: 347 RVA: 0x000055F7 File Offset: 0x000037F7
		public static ITracer TracerInstance { get; set; } = new Tracer.DefaultTracer();

		// Token: 0x0600015C RID: 348 RVA: 0x000055FF File Offset: 0x000037FF
		public static void TraceError(string format, params object[] args)
		{
			Tracer.TracerInstance.TraceError(format, args);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000560D File Offset: 0x0000380D
		public static void TraceWarning(string format, params object[] args)
		{
			Tracer.TracerInstance.TraceWarning(format, args);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000561B File Offset: 0x0000381B
		public static void TraceInformation(string format, params object[] args)
		{
			Tracer.TracerInstance.TraceInformation(format, args);
		}

		// Token: 0x02000028 RID: 40
		private class DefaultTracer : ITracer
		{
			// Token: 0x06000162 RID: 354 RVA: 0x00005629 File Offset: 0x00003829
			public void TraceError(string format, params object[] args)
			{
			}

			// Token: 0x06000163 RID: 355 RVA: 0x0000562B File Offset: 0x0000382B
			public void TraceWarning(string format, params object[] args)
			{
			}

			// Token: 0x06000164 RID: 356 RVA: 0x0000562D File Offset: 0x0000382D
			public void TraceInformation(string format, params object[] args)
			{
			}
		}
	}
}
