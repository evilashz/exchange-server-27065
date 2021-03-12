using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ManagedStore.FastTransfer
{
	// Token: 0x02000393 RID: 915
	public static class ExTraceGlobals
	{
		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060015BA RID: 5562 RVA: 0x00056514 File Offset: 0x00054714
		public static Trace SourceSendTracer
		{
			get
			{
				if (ExTraceGlobals.sourceSendTracer == null)
				{
					ExTraceGlobals.sourceSendTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.sourceSendTracer;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060015BB RID: 5563 RVA: 0x00056532 File Offset: 0x00054732
		public static Trace IcsDownloadTracer
		{
			get
			{
				if (ExTraceGlobals.icsDownloadTracer == null)
				{
					ExTraceGlobals.icsDownloadTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.icsDownloadTracer;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x00056550 File Offset: 0x00054750
		public static Trace IcsDownloadStateTracer
		{
			get
			{
				if (ExTraceGlobals.icsDownloadStateTracer == null)
				{
					ExTraceGlobals.icsDownloadStateTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.icsDownloadStateTracer;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x0005656E File Offset: 0x0005476E
		public static Trace IcsUploadStateTracer
		{
			get
			{
				if (ExTraceGlobals.icsUploadStateTracer == null)
				{
					ExTraceGlobals.icsUploadStateTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.icsUploadStateTracer;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x0005658C File Offset: 0x0005478C
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x04001A68 RID: 6760
		private static Guid componentGuid = new Guid("e8d090ac-ab71-4752-b432-0b86b6e380e6");

		// Token: 0x04001A69 RID: 6761
		private static Trace sourceSendTracer = null;

		// Token: 0x04001A6A RID: 6762
		private static Trace icsDownloadTracer = null;

		// Token: 0x04001A6B RID: 6763
		private static Trace icsDownloadStateTracer = null;

		// Token: 0x04001A6C RID: 6764
		private static Trace icsUploadStateTracer = null;

		// Token: 0x04001A6D RID: 6765
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
