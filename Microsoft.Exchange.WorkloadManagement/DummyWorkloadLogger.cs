using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000007 RID: 7
	internal sealed class DummyWorkloadLogger : IWorkloadLogger
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002420 File Offset: 0x00000620
		private DummyWorkloadLogger()
		{
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002428 File Offset: 0x00000628
		internal static DummyWorkloadLogger Instance
		{
			get
			{
				if (DummyWorkloadLogger.instance == null)
				{
					DummyWorkloadLogger.instance = new DummyWorkloadLogger();
				}
				return DummyWorkloadLogger.instance;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002440 File Offset: 0x00000640
		public void LogActivityEvent(IActivityScope activityScope, ActivityEventType eventType)
		{
		}

		// Token: 0x04000026 RID: 38
		private static DummyWorkloadLogger instance;
	}
}
