using System;
using System.Configuration;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000106 RID: 262
	public class KillBitTimer
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0002D2EE File Offset: 0x0002B4EE
		public static KillBitTimer Singleton
		{
			get
			{
				return KillBitTimer.killBitTimer;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0002D2F5 File Offset: 0x0002B4F5
		public bool IsStarted
		{
			get
			{
				return this.isStarted;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0002D2FD File Offset: 0x0002B4FD
		// (set) Token: 0x06000B31 RID: 2865 RVA: 0x0002D305 File Offset: 0x0002B505
		public int DelayedCheckTimeInSeconds
		{
			get
			{
				return this.delayedCheckTimeInSeconds;
			}
			set
			{
				this.delayedCheckTimeInSeconds = value;
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002D310 File Offset: 0x0002B510
		public void Start()
		{
			if (!this.IsStarted)
			{
				this.internalTimer = new Timer(new TimerCallback(KillBitHelper.DownloadKillBitList));
				string text = ConfigurationManager.AppSettings["KillBitRefreshTimeInSeconds"];
				int num = 0;
				int num2;
				DateTime value;
				long num4;
				if (KillBitHelper.TryReadKillBitFile(out num2, out value))
				{
					long num3 = (long)(num2 * this.DelayedCheckTimeInSeconds * 1000);
					KillBitTimer.Tracer.TraceInformation(0, 0L, string.Format("This refresh rate is {0} milliseconds.", num3));
					TimeSpan timeSpan = DateTime.UtcNow.Subtract(value);
					if ((double)num3 < timeSpan.TotalMilliseconds)
					{
						num4 = (long)this.DelayedCheckTimeInSeconds * 1000L;
					}
					else
					{
						num4 = num3;
					}
				}
				else
				{
					num4 = (long)this.DelayedCheckTimeInSeconds * 1000L;
				}
				if (!string.IsNullOrWhiteSpace(text) && int.TryParse(text, out num) && num > 0)
				{
					this.refreshRateInMillionSecondsFromConfig = num * 1000;
					num4 = (long)this.refreshRateInMillionSecondsFromConfig;
					KillBitTimer.Tracer.TraceInformation(0, 0L, string.Format("This test refresh rate setting is {0} seconds.", num));
				}
				KillBitTimer.Tracer.TraceInformation(0, 0L, string.Format("This time to next download is {0} milliseconds.", num4));
				this.internalTimer.Change(num4, num4);
				this.isStarted = true;
			}
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002D44C File Offset: 0x0002B64C
		public void UpdateTimerWithRefreshRate(int refreshRate)
		{
			if (refreshRate <= 0)
			{
				throw new ArgumentException("The refreshRate cannot be equal to or less than zero.");
			}
			if (!this.isStarted || this.internalTimer == null)
			{
				return;
			}
			long num = (long)((this.refreshRateInMillionSecondsFromConfig > 0) ? this.refreshRateInMillionSecondsFromConfig : (refreshRate * this.DelayedCheckTimeInSeconds * 1000));
			KillBitTimer.Tracer.TraceInformation(0, 0L, string.Format("This time to next download is {0} milliseconds.", num));
			this.internalTimer.Change(num, num);
		}

		// Token: 0x040005A0 RID: 1440
		private const string KillBitRefreshTimeInSecondsKey = "KillBitRefreshTimeInSeconds";

		// Token: 0x040005A1 RID: 1441
		private const int MilliSecondsInSeconds = 1000;

		// Token: 0x040005A2 RID: 1442
		private static readonly Trace Tracer = ExTraceGlobals.ExtensionTracer;

		// Token: 0x040005A3 RID: 1443
		private int delayedCheckTimeInSeconds = 3600;

		// Token: 0x040005A4 RID: 1444
		private int refreshRateInMillionSecondsFromConfig;

		// Token: 0x040005A5 RID: 1445
		private static KillBitTimer killBitTimer = new KillBitTimer();

		// Token: 0x040005A6 RID: 1446
		private Timer internalTimer;

		// Token: 0x040005A7 RID: 1447
		private bool isStarted;
	}
}
