using System;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x0200004D RID: 77
	internal class WorkerProcessRepository
	{
		// Token: 0x06000338 RID: 824 RVA: 0x0000B70F File Offset: 0x0000990F
		private WorkerProcessRepository()
		{
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000B718 File Offset: 0x00009918
		public static WorkerProcessRepository Instance
		{
			get
			{
				if (WorkerProcessRepository.instance == null)
				{
					lock (WorkerProcessRepository.initLock)
					{
						if (WorkerProcessRepository.instance == null)
						{
							WorkerProcessRepository.instance = new WorkerProcessRepository();
						}
					}
				}
				return WorkerProcessRepository.instance;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000B770 File Offset: 0x00009970
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0000B778 File Offset: 0x00009978
		internal RpcSetWorkerProcessInfoImpl.WorkerProcessInfo WorkerProcessInfo { get; set; }

		// Token: 0x0600033C RID: 828 RVA: 0x0000B784 File Offset: 0x00009984
		internal DateTime GetWorkerProcessStartTime()
		{
			DateTime result = DateTime.MinValue;
			if (this.WorkerProcessInfo != null)
			{
				result = this.WorkerProcessInfo.ProcessStartTime;
			}
			return result;
		}

		// Token: 0x040001F6 RID: 502
		private static readonly object initLock = new object();

		// Token: 0x040001F7 RID: 503
		private static WorkerProcessRepository instance = null;
	}
}
