using System;

namespace Microsoft.Exchange.Diagnostics.Components.ServiceHost.PublicFolder
{
	// Token: 0x020003B5 RID: 949
	public static class ExTraceGlobals
	{
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x00058CC9 File Offset: 0x00056EC9
		public static Trace PublicFolderSynchronizerTracer
		{
			get
			{
				if (ExTraceGlobals.publicFolderSynchronizerTracer == null)
				{
					ExTraceGlobals.publicFolderSynchronizerTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.publicFolderSynchronizerTracer;
			}
		}

		// Token: 0x04001B8B RID: 7051
		private static Guid componentGuid = new Guid("59193765-72d1-4ff4-a52d-0e3de811073a");

		// Token: 0x04001B8C RID: 7052
		private static Trace publicFolderSynchronizerTracer = null;
	}
}
