using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services
{
	// Token: 0x02000016 RID: 22
	internal class EwsCpuBasedSleeper : IDisposable
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000061F0 File Offset: 0x000043F0
		public static EwsCpuBasedSleeper Singleton
		{
			get
			{
				if (EwsCpuBasedSleeper.singleton == null)
				{
					lock (EwsCpuBasedSleeper.lockObj)
					{
						if (EwsCpuBasedSleeper.singleton == null)
						{
							if (Global.GetAppSettingAsBool("EwsCpuBasedDelayEnabled", false))
							{
								EwsCpuBasedSleeper.singleton = new EwsCpuBasedSleeper.EwsSleeper();
							}
							else
							{
								EwsCpuBasedSleeper.singleton = new EwsCpuBasedSleeper();
							}
						}
					}
				}
				return EwsCpuBasedSleeper.singleton;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006260 File Offset: 0x00004460
		public virtual double GetDelayTime()
		{
			return 0.0;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000626B File Offset: 0x0000446B
		public virtual void Dispose()
		{
		}

		// Token: 0x040000E2 RID: 226
		private const string AppSettingEwsCpuBasedDelayEnabled = "EwsCpuBasedDelayEnabled";

		// Token: 0x040000E3 RID: 227
		private static EwsCpuBasedSleeper singleton;

		// Token: 0x040000E4 RID: 228
		private static object lockObj = new object();

		// Token: 0x02000017 RID: 23
		private class EwsSleeper : EwsCpuBasedSleeper
		{
			// Token: 0x06000121 RID: 289 RVA: 0x00006284 File Offset: 0x00004484
			internal EwsSleeper()
			{
				this.MaxDelayOnHighSystemCpu = (double)Global.GetAppSettingAsInt("MaxDelayOnHighSystemCpu", 8000);
				this.MaxDelayOnHighProcessCpu = (double)Global.GetAppSettingAsInt("MaxDelayOnHighProcessCpu", 4000);
				this.callsThreshold = Global.GetAppSettingAsInt("CallsThresholdOnHighCpu", 20);
				this.processCpuPerfCounter = new ProcessCPURunningAveragePerfCounterReader();
				this.processCpuThreshold = (float)Global.GetAppSettingAsInt("HighProcessCpuThreshold", 80);
				if (this.processCpuThreshold >= 100f || this.processCpuThreshold <= 0f)
				{
					this.processCpuThreshold = 80f;
				}
				this.systemCpuThreshold = (float)Global.GetAppSettingAsInt("HighSystemCpuThreshold", 80);
				if (this.systemCpuThreshold >= 100f || this.systemCpuThreshold <= 0f)
				{
					this.systemCpuThreshold = 80f;
				}
				this.timer = new System.Timers.Timer(60000.0);
				this.timer.Elapsed += this.UpdateCallHistory;
				this.timer.Start();
				this.newCopy = new Dictionary<string, int>(2000);
			}

			// Token: 0x06000122 RID: 290 RVA: 0x000063AC File Offset: 0x000045AC
			private void UpdateCallHistory(object sender, ElapsedEventArgs e)
			{
				this.oldCopy = this.newCopy;
				Interlocked.Exchange<Dictionary<string, int>>(ref this.newCopy, new Dictionary<string, int>(2000));
				lock (this.oldCopyLock)
				{
					foreach (string key in this.oldCopy.Keys)
					{
						int num;
						if (this.oldCopy.TryGetValue(key, out num) && num > 10)
						{
							int num2 = num / 10;
							int num3 = num2;
							lock (this.newCopyLock)
							{
								int num4 = 0;
								if (this.newCopy.TryGetValue(key, out num4))
								{
									num3 += num4;
								}
								this.newCopy[key] = num3;
							}
						}
					}
				}
			}

			// Token: 0x06000123 RID: 291 RVA: 0x000064BC File Offset: 0x000046BC
			public override void Dispose()
			{
				if (this.timer != null)
				{
					this.timer.Dispose();
					this.timer = null;
				}
			}

			// Token: 0x06000124 RID: 292 RVA: 0x000064D8 File Offset: 0x000046D8
			public override double GetDelayTime()
			{
				double num = ServerCPUUsage.GetCurrentUsagePercentage();
				double num2 = (double)(this.processCpuPerfCounter.GetValue() * (float)Environment.ProcessorCount);
				if (HttpContext.Current == null || HttpContext.Current.Request == null)
				{
					return 0.0;
				}
				HttpRequest request = HttpContext.Current.Request;
				if (!request.IsAuthenticated && request.UserAgent != null && !request.UserAgent.StartsWith("ExchangeWebServicesProxy", StringComparison.OrdinalIgnoreCase) && !request.UserAgent.StartsWith("ASProxy", StringComparison.OrdinalIgnoreCase) && !request.UserAgent.StartsWith("OwaProxy", StringComparison.OrdinalIgnoreCase) && num >= (double)this.systemCpuThreshold)
				{
					RequestDetailsLogger.Current.AppendGenericInfo("BoxCpu", num);
					return (num - (double)this.systemCpuThreshold) / (100.0 - (double)this.systemCpuThreshold) * this.MaxDelayOnHighSystemCpu;
				}
				CallContext callContext = CallContext.Current;
				if (callContext == null)
				{
					return 0.0;
				}
				if (callContext.AccessingPrincipal == null)
				{
					RequestDetailsLogger.Current.AppendGenericInfo("BadKey", callContext.OriginalCallerContext.IdentifierString);
					return 0.0;
				}
				string text = string.Format("{0}:{1}", callContext.OriginalCallerContext.IdentifierString, callContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress);
				int num3 = 1;
				lock (this.newCopyLock)
				{
					int num4;
					if (this.newCopy.TryGetValue(text, out num4))
					{
						num3 += num4;
					}
					this.newCopy[text] = num3;
				}
				if (num2 >= (double)this.processCpuThreshold && num3 > this.callsThreshold)
				{
					RequestDetailsLogger.Current.AppendGenericInfo("Key", text);
					RequestDetailsLogger.Current.AppendGenericInfo("BoxCpu", num.ToString("F0"));
					RequestDetailsLogger.Current.AppendGenericInfo("EwsCpu", num2.ToString("F0"));
					RequestDetailsLogger.Current.AppendGenericInfo("Calls", num3);
					double val = (num - (double)this.systemCpuThreshold) / (100.0 - (double)this.systemCpuThreshold) * this.MaxDelayOnHighSystemCpu;
					double val2 = Math.Min((num2 - (double)this.processCpuThreshold) / (100.0 - (double)this.processCpuThreshold), 2.0) * this.MaxDelayOnHighProcessCpu;
					return Math.Max(val2, val);
				}
				return 0.0;
			}

			// Token: 0x040000E5 RID: 229
			private const int DefaultHistorySize = 2000;

			// Token: 0x040000E6 RID: 230
			private const int RefreshInterval = 60000;

			// Token: 0x040000E7 RID: 231
			private readonly double MaxDelayOnHighSystemCpu;

			// Token: 0x040000E8 RID: 232
			private readonly double MaxDelayOnHighProcessCpu;

			// Token: 0x040000E9 RID: 233
			private readonly ProcessCPURunningAveragePerfCounterReader processCpuPerfCounter;

			// Token: 0x040000EA RID: 234
			private readonly int callsThreshold;

			// Token: 0x040000EB RID: 235
			private readonly float systemCpuThreshold;

			// Token: 0x040000EC RID: 236
			private readonly float processCpuThreshold;

			// Token: 0x040000ED RID: 237
			private readonly object oldCopyLock = new object();

			// Token: 0x040000EE RID: 238
			private readonly object newCopyLock = new object();

			// Token: 0x040000EF RID: 239
			private Dictionary<string, int> oldCopy;

			// Token: 0x040000F0 RID: 240
			private Dictionary<string, int> newCopy;

			// Token: 0x040000F1 RID: 241
			private System.Timers.Timer timer;
		}
	}
}
