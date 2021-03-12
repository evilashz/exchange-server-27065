using System;
using System.Threading;
using Microsoft.Win32;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001D0 RID: 464
	internal class RegistryKeyChangeWatcher : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000D0B RID: 3339 RVA: 0x00036E08 File Offset: 0x00035008
		public RegistryKeyChangeWatcher(string key, string value, Action<string> successCallback, Action<string> errorCallback)
		{
			this.key = key;
			this.value = value;
			this.successCallback = successCallback;
			this.errorCallback = errorCallback;
			this.timer = new Timer(new TimerCallback(this.ReadRegistryKey), null, 0, -1);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00036E5E File Offset: 0x0003505E
		public void Dispose()
		{
			if (this.timer != null)
			{
				this.timer.Dispose();
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00036E73 File Offset: 0x00035073
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTrackerFactory.Get(this);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00036E7B File Offset: 0x0003507B
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00036E90 File Offset: 0x00035090
		private void ReadRegistryKey(object state)
		{
			try
			{
				using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
				{
					if (registryKey != null)
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey(this.key))
						{
							if (registryKey2 != null)
							{
								object objA = registryKey2.GetValue(this.value);
								if (this.successCallback != null && !object.Equals(objA, this.registryKeyCurrentValue))
								{
									this.registryKeyCurrentValue = objA;
									this.successCallback(this.registryKeyCurrentValue.ToString());
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.errorCallback(ex.Message);
			}
			this.timer.Change(RegistryKeyChangeWatcher.timerElapsedPeriod, -1);
		}

		// Token: 0x0400099D RID: 2461
		private static readonly int timerElapsedPeriod = 300000;

		// Token: 0x0400099E RID: 2462
		private readonly Timer timer;

		// Token: 0x0400099F RID: 2463
		private readonly string key;

		// Token: 0x040009A0 RID: 2464
		private readonly Action<string> successCallback;

		// Token: 0x040009A1 RID: 2465
		private readonly Action<string> errorCallback;

		// Token: 0x040009A2 RID: 2466
		private readonly string value;

		// Token: 0x040009A3 RID: 2467
		private object registryKeyCurrentValue;

		// Token: 0x040009A4 RID: 2468
		private DisposeTracker disposeTracker;
	}
}
