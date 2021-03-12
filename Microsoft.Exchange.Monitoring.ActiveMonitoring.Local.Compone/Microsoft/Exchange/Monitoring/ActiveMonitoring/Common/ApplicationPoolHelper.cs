using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200011A RID: 282
	internal class ApplicationPoolHelper : DisposeTrackableBase
	{
		// Token: 0x0600086F RID: 2159 RVA: 0x00031D44 File Offset: 0x0002FF44
		public ApplicationPoolHelper(string appPoolName)
		{
			this.appPoolName = appPoolName;
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00031D53 File Offset: 0x0002FF53
		internal ApplicationPool ApplicationPool
		{
			get
			{
				this.Initialize();
				return this.appPool;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00031D64 File Offset: 0x0002FF64
		internal List<int> WorkerProcessIds
		{
			get
			{
				List<int> list = new List<int>();
				this.Refresh();
				WorkerProcessCollection workerProcesses = this.appPool.WorkerProcesses;
				if (workerProcesses != null && workerProcesses.Count > 0)
				{
					foreach (WorkerProcess workerProcess in workerProcesses)
					{
						list.Add(workerProcess.ProcessId);
					}
				}
				return list;
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00031DD8 File Offset: 0x0002FFD8
		internal static ApplicationPool Find(ServerManager serverManager, string appPoolName)
		{
			foreach (ApplicationPool applicationPool in serverManager.ApplicationPools)
			{
				if (string.Equals(appPoolName, applicationPool.Name, StringComparison.OrdinalIgnoreCase))
				{
					return applicationPool;
				}
			}
			return null;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00031E34 File Offset: 0x00030034
		internal static Process[] GetRunningProcessesForAppPool(ServerManager serverManager, string appPoolName)
		{
			ApplicationPoolCollection applicationPools = serverManager.ApplicationPools;
			foreach (ApplicationPool applicationPool in applicationPools)
			{
				if (appPoolName.Equals(applicationPool.Name, StringComparison.OrdinalIgnoreCase))
				{
					WorkerProcessCollection workerProcesses = applicationPool.WorkerProcesses;
					List<Process> list = new List<Process>();
					foreach (WorkerProcess workerProcess in workerProcesses)
					{
						if (workerProcess.State == 1)
						{
							Process processById = Process.GetProcessById(workerProcess.ProcessId);
							if (processById != null)
							{
								list.Add(processById);
							}
						}
					}
					return list.ToArray();
				}
			}
			return new Process[0];
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00031F10 File Offset: 0x00030110
		internal void Initialize()
		{
			if (this.serverManager == null)
			{
				this.serverManager = new ServerManager();
			}
			if (this.appPool == null)
			{
				this.appPool = ApplicationPoolHelper.Find(this.serverManager, this.appPoolName);
			}
			if (this.appPool == null)
			{
				throw new InvalidOperationException("Application pool can't be located");
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00031F62 File Offset: 0x00030162
		internal void Refresh()
		{
			if (this.serverManager != null)
			{
				this.serverManager.Dispose();
				this.serverManager = null;
				this.appPool = null;
			}
			this.Initialize();
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00031F8C File Offset: 0x0003018C
		internal void Recycle()
		{
			this.Initialize();
			bool flag = false;
			ObjectState objectState;
			if (this.appPool.State == 1)
			{
				objectState = this.appPool.Recycle();
			}
			else
			{
				flag = true;
				objectState = this.appPool.Start();
			}
			if (objectState != null && objectState != 1)
			{
				throw new InvalidOperationException(string.Format("Failed to recycle application pool (poolName={0}, state={1}, startAttempted={2})", this.appPoolName, objectState, flag));
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00031FF4 File Offset: 0x000301F4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ApplicationPoolHelper>(this);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00031FFC File Offset: 0x000301FC
		protected override void InternalDispose(bool disposing)
		{
			lock (this)
			{
				if (disposing && this.serverManager != null)
				{
					this.serverManager.Dispose();
				}
			}
		}

		// Token: 0x040005CA RID: 1482
		private readonly string appPoolName;

		// Token: 0x040005CB RID: 1483
		private ServerManager serverManager;

		// Token: 0x040005CC RID: 1484
		private ApplicationPool appPool;
	}
}
