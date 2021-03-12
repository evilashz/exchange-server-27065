using System;
using System.Threading;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000099 RID: 153
	internal class FileSystemWatcherTimer : IDisposable
	{
		// Token: 0x06000396 RID: 918 RVA: 0x0000DCE4 File Offset: 0x0000BEE4
		internal FileSystemWatcherTimer(string filePath, Action notifyHandler)
		{
			if (notifyHandler == null)
			{
				throw new ArgumentNullException("notifyHandler");
			}
			this.filePath = filePath;
			this.notifyHandler = notifyHandler;
			SharedTimer.Instance.RegisterCallback(new TimerCallback(this.Callback));
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000DD1E File Offset: 0x0000BF1E
		public void Dispose()
		{
			SharedTimer.Instance.UnRegisterCallback(new TimerCallback(this.Callback));
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000DD38 File Offset: 0x0000BF38
		private void Callback(object arg)
		{
			ConfigFileFingerPrint configFileFingerPrint = new ConfigFileFingerPrint(this.filePath);
			if (!configFileFingerPrint.Equals(this.lastFingerPrint))
			{
				InternalBypassTrace.TracingConfigurationTracer.TraceDebug(0, (long)this.GetHashCode(), "File changed, old attributes=\"{0}\", new attributes=\"{1}\"", new object[]
				{
					this.lastFingerPrint,
					configFileFingerPrint
				});
				this.lastFingerPrint = configFileFingerPrint;
				this.notifyHandler();
			}
		}

		// Token: 0x0400031B RID: 795
		private string filePath;

		// Token: 0x0400031C RID: 796
		private Action notifyHandler;

		// Token: 0x0400031D RID: 797
		private ConfigFileFingerPrint lastFingerPrint;
	}
}
