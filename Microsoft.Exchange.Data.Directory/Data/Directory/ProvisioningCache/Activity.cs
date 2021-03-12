using System;
using System.Threading;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ProvisioningCache
{
	// Token: 0x0200079B RID: 1947
	internal abstract class Activity : DisposeTrackableBase
	{
		// Token: 0x170022B5 RID: 8885
		// (get) Token: 0x060060F6 RID: 24822 RVA: 0x00149817 File Offset: 0x00147A17
		// (set) Token: 0x060060F7 RID: 24823 RVA: 0x0014981F File Offset: 0x00147A1F
		private protected ProvisioningCache ProvisioningCache { protected get; private set; }

		// Token: 0x170022B6 RID: 8886
		// (get) Token: 0x060060F8 RID: 24824 RVA: 0x00149828 File Offset: 0x00147A28
		// (set) Token: 0x060060F9 RID: 24825 RVA: 0x00149830 File Offset: 0x00147A30
		internal bool GotStopSignalFromTestCode { get; private set; }

		// Token: 0x060060FA RID: 24826 RVA: 0x00149839 File Offset: 0x00147A39
		protected Activity(ProvisioningCache cache)
		{
			if (cache == null)
			{
				throw new ArgumentNullException("cache");
			}
			this.ProvisioningCache = cache;
			this.GotStopSignalFromTestCode = false;
		}

		// Token: 0x170022B7 RID: 8887
		// (get) Token: 0x060060FB RID: 24827
		public abstract string Name { get; }

		// Token: 0x170022B8 RID: 8888
		// (get) Token: 0x060060FC RID: 24828 RVA: 0x0014985D File Offset: 0x00147A5D
		// (set) Token: 0x060060FD RID: 24829 RVA: 0x00149865 File Offset: 0x00147A65
		private protected Thread AsyncThread { protected get; private set; }

		// Token: 0x060060FE RID: 24830 RVA: 0x00149870 File Offset: 0x00147A70
		public Thread ExecuteAsync(Action<Activity, Exception> callback)
		{
			Thread thread = new Thread(new ParameterizedThreadStart(this.ExecuteAsyncEntryPoint));
			thread.IsBackground = true;
			thread.Start(callback);
			this.AsyncThread = thread;
			return thread;
		}

		// Token: 0x060060FF RID: 24831 RVA: 0x001498A5 File Offset: 0x00147AA5
		internal virtual void StopExecute()
		{
			this.GotStopSignalFromTestCode = true;
		}

		// Token: 0x06006100 RID: 24832 RVA: 0x001498B0 File Offset: 0x00147AB0
		protected void Execute()
		{
			try
			{
				this.InternalExecute();
			}
			catch (ThreadAbortException ex)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCActivityExit, this.Name.ToString(), new object[]
				{
					this.Name.ToString(),
					ex.ToString()
				});
			}
		}

		// Token: 0x06006101 RID: 24833
		protected abstract void InternalExecute();

		// Token: 0x06006102 RID: 24834 RVA: 0x00149910 File Offset: 0x00147B10
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Activity>(this);
		}

		// Token: 0x06006103 RID: 24835 RVA: 0x00149928 File Offset: 0x00147B28
		private void ExecuteAsyncEntryPoint(object parameter)
		{
			Exception arg = null;
			try
			{
				ExWatson.SendReportOnUnhandledException(new ExWatson.MethodDelegate(this.Execute), (object ex) => !(ex is ThreadAbortException));
			}
			catch (Exception ex)
			{
				Exception ex2;
				arg = ex2;
			}
			finally
			{
				Action<Activity, Exception> action = (Action<Activity, Exception>)parameter;
				if (action != null)
				{
					action(this, arg);
				}
			}
		}
	}
}
