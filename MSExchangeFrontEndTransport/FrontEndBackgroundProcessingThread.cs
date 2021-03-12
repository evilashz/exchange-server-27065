using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Transport.FrontEnd
{
	// Token: 0x02000002 RID: 2
	internal class FrontEndBackgroundProcessingThread : BackgroundProcessingThreadBase, IStartableTransportComponent, ITransportComponent
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public FrontEndBackgroundProcessingThread(FrontEndBackgroundProcessingThread.ServerComponentStateChangedHandler serverComponentStateChangedHandler) : base(TimeSpan.FromSeconds(1.0))
		{
			this.serverComponentStateChangedHandler = serverComponentStateChangedHandler;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020ED File Offset: 0x000002ED
		public string CurrentState
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020F0 File Offset: 0x000002F0
		public void Load()
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F2 File Offset: 0x000002F2
		public void Unload()
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F4 File Offset: 0x000002F4
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020F7 File Offset: 0x000002F7
		public void Pause()
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F9 File Offset: 0x000002F9
		public void Continue()
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020FC File Offset: 0x000002FC
		protected override void Run()
		{
			DateTime utcNow = DateTime.UtcNow;
			this.lastTimeThrottlingManagerSwept = utcNow;
			this.lastTimeServiceStateChecked = utcNow;
			this.serviceStateHelper = new ServiceStateHelper(Components.Configuration, ServerComponentStates.GetComponentId(ServerComponentEnum.FrontendTransport));
			base.Run();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		protected override void RunMain(DateTime now)
		{
			Components.SmtpInComponent.UpdateTime(now);
			if (now - this.lastTimeThrottlingManagerSwept > FrontEndBackgroundProcessingThread.ipTableScanInterval)
			{
				Components.MessageThrottlingComponent.MessageThrottlingManager.CleanupIdleEntries();
				Components.UnhealthyTargetFilterComponent.CleanupExpiredEntries();
				this.lastTimeThrottlingManagerSwept = now;
			}
			if (now - this.lastTimeServiceStateChecked > FrontEndBackgroundProcessingThread.serviceStateScanInterval)
			{
				bool flag = this.serviceStateHelper.CheckState(this.startupServiceState);
				if (flag && this.serverComponentStateChangedHandler != null)
				{
					ServiceState serviceState = ServiceStateHelper.GetServiceState(Components.Configuration, ServerComponentStates.GetComponentId(ServerComponentEnum.FrontendTransport));
					this.serverComponentStateChangedHandler(serviceState);
				}
				this.lastTimeServiceStateChecked = now;
			}
			if (Components.ResourceManager.IsMonitoringEnabled && now - Components.ResourceManager.LastTimeResourceMonitored > Components.ResourceManager.MonitorInterval)
			{
				Components.ResourceManager.OnMonitorResource(null);
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly TimeSpan ipTableScanInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000002 RID: 2
		private static readonly TimeSpan serviceStateScanInterval = TimeSpan.FromSeconds(30.0);

		// Token: 0x04000003 RID: 3
		private readonly FrontEndBackgroundProcessingThread.ServerComponentStateChangedHandler serverComponentStateChangedHandler;

		// Token: 0x04000004 RID: 4
		private DateTime lastTimeThrottlingManagerSwept;

		// Token: 0x04000005 RID: 5
		private DateTime lastTimeServiceStateChecked;

		// Token: 0x04000006 RID: 6
		private ServiceStateHelper serviceStateHelper;

		// Token: 0x02000003 RID: 3
		// (Invoke) Token: 0x0600000C RID: 12
		public delegate void ServerComponentStateChangedHandler(ServiceState newState);
	}
}
