using System;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000186 RID: 390
	internal abstract class UMRPCComponentBase : IUMAsyncComponent
	{
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x0002DAF5 File Offset: 0x0002BCF5
		public AutoResetEvent StoppedEvent
		{
			get
			{
				return this.controlEvent;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0002DAFD File Offset: 0x0002BCFD
		public bool IsInitialized
		{
			get
			{
				return this.initialized;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0002DB05 File Offset: 0x0002BD05
		public string Name
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0002DB12 File Offset: 0x0002BD12
		public static void HandleException(Exception ex)
		{
			if (ex is LocalizedException)
			{
				return;
			}
			if (GrayException.IsGrayException(ex))
			{
				ExWatson.SendReport(ex);
				return;
			}
			CrashProcess.Instance.CrashThisProcess(ex);
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0002DB38 File Offset: 0x0002BD38
		public void StartNow(StartupStage stage)
		{
			if (stage == StartupStage.WPActivation)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "{0} starting in stage {1}", new object[]
				{
					this.Name,
					stage
				});
				this.activeRequestCount = 0;
				this.shutdownInProgress = false;
				this.RegisterServer();
				this.initialized = true;
			}
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0002DB94 File Offset: 0x0002BD94
		public void StopAsync()
		{
			this.controlEvent.Reset();
			lock (this.lockObj)
			{
				this.shutdownInProgress = true;
				if (this.activeRequestCount <= 0)
				{
					this.controlEvent.Set();
				}
			}
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0002DBF8 File Offset: 0x0002BDF8
		public void CleanupAfterStopped()
		{
			this.controlEvent.Close();
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0002DC08 File Offset: 0x0002BE08
		internal bool GuardBeforeExecution()
		{
			bool result;
			lock (this.lockObj)
			{
				if (this.shutdownInProgress)
				{
					result = false;
				}
				else
				{
					this.activeRequestCount++;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0002DC60 File Offset: 0x0002BE60
		internal void GuardAfterExecution()
		{
			lock (this.lockObj)
			{
				this.activeRequestCount--;
				if (this.shutdownInProgress && this.activeRequestCount <= 0)
				{
					this.controlEvent.Set();
				}
			}
		}

		// Token: 0x06000C7B RID: 3195
		internal abstract void RegisterServer();

		// Token: 0x040006B7 RID: 1719
		private AutoResetEvent controlEvent = new AutoResetEvent(false);

		// Token: 0x040006B8 RID: 1720
		private object lockObj = new object();

		// Token: 0x040006B9 RID: 1721
		private bool shutdownInProgress;

		// Token: 0x040006BA RID: 1722
		private bool initialized;

		// Token: 0x040006BB RID: 1723
		private int activeRequestCount;
	}
}
