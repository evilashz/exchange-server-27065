using System;

namespace Microsoft.Exchange.Diagnostics.Components.StsUpdate
{
	// Token: 0x0200038B RID: 907
	public static class ExTraceGlobals
	{
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x00055EFD File Offset: 0x000540FD
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

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x00055F1B File Offset: 0x0005411B
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

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x00055F39 File Offset: 0x00054139
		public static Trace AgentTracer
		{
			get
			{
				if (ExTraceGlobals.agentTracer == null)
				{
					ExTraceGlobals.agentTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.agentTracer;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x00055F57 File Offset: 0x00054157
		public static Trace OnDownloadTracer
		{
			get
			{
				if (ExTraceGlobals.onDownloadTracer == null)
				{
					ExTraceGlobals.onDownloadTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.onDownloadTracer;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x00055F75 File Offset: 0x00054175
		public static Trace OnRequestTracer
		{
			get
			{
				if (ExTraceGlobals.onRequestTracer == null)
				{
					ExTraceGlobals.onRequestTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.onRequestTracer;
			}
		}

		// Token: 0x04001A39 RID: 6713
		private static Guid componentGuid = new Guid("C5F72F2A-EF44-4286-9AB2-14D106DFB8F1");

		// Token: 0x04001A3A RID: 6714
		private static Trace factoryTracer = null;

		// Token: 0x04001A3B RID: 6715
		private static Trace databaseTracer = null;

		// Token: 0x04001A3C RID: 6716
		private static Trace agentTracer = null;

		// Token: 0x04001A3D RID: 6717
		private static Trace onDownloadTracer = null;

		// Token: 0x04001A3E RID: 6718
		private static Trace onRequestTracer = null;
	}
}
