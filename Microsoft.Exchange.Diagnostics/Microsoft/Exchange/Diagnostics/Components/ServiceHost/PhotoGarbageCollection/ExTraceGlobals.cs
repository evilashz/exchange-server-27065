using System;

namespace Microsoft.Exchange.Diagnostics.Components.ServiceHost.PhotoGarbageCollection
{
	// Token: 0x020003AF RID: 943
	public static class ExTraceGlobals
	{
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x00058A8F File Offset: 0x00056C8F
		public static Trace GarbageCollectionTracer
		{
			get
			{
				if (ExTraceGlobals.garbageCollectionTracer == null)
				{
					ExTraceGlobals.garbageCollectionTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.garbageCollectionTracer;
			}
		}

		// Token: 0x04001B78 RID: 7032
		private static Guid componentGuid = new Guid("D741A9E4-436A-4266-80F4-E6B7EC1E8611");

		// Token: 0x04001B79 RID: 7033
		private static Trace garbageCollectionTracer = null;
	}
}
