using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000B0 RID: 176
	internal sealed class ConfigProvider
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0002DFD6 File Offset: 0x0002C1D6
		public DateTime LastRefreshDateTime
		{
			get
			{
				return this.lastRefreshDateTime;
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0002DFDE File Offset: 0x0002C1DE
		private ConfigProvider()
		{
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0002DFFC File Offset: 0x0002C1FC
		public static ConfigProvider Instance
		{
			get
			{
				if (ConfigProvider.instance == null)
				{
					lock (ConfigProvider.staticLockObj)
					{
						if (ConfigProvider.instance == null)
						{
							ConfigProvider.instance = new ConfigProvider();
						}
					}
				}
				return ConfigProvider.instance;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0002E054 File Offset: 0x0002C254
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x0002E05C File Offset: 0x0002C25C
		public bool AutoRefresh
		{
			get
			{
				return this.autoRefreshEnabled;
			}
			set
			{
				if (!value)
				{
					this.DisableAutoRefresh();
				}
				this.autoRefreshEnabled = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0002E06E File Offset: 0x0002C26E
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x0002E076 File Offset: 0x0002C276
		public bool LoadTrustedIssuers
		{
			get
			{
				return this.loadTrustedIssuers;
			}
			set
			{
				this.loadTrustedIssuers = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0002E080 File Offset: 0x0002C280
		public LocalConfiguration Configuration
		{
			get
			{
				if (this.autoRefreshEnabled)
				{
					this.EnableAutoRefresh();
				}
				if (this.refreshTimer != null && this.underlyingConfiguration != null)
				{
					ExTraceGlobals.OAuthTracer.TraceDebug(0L, "[ConfigProvider::get_Configuration] returning auto-refreshed instance");
				}
				else
				{
					this.ManualLoadIfNecessary();
					ExTraceGlobals.OAuthTracer.TraceDebug(0L, "[ConfigProvider::get_Configuration] returning manual-loaded instance");
				}
				return this.underlyingConfiguration;
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0002E0DC File Offset: 0x0002C2DC
		private void EnableAutoRefresh()
		{
			if (this.refreshTimer == null)
			{
				lock (this.instanceLockObj)
				{
					if (this.refreshTimer == null)
					{
						ExTraceGlobals.OAuthTracer.TraceDebug(0L, "[ConfigProvider::EnableAutoRefresh] entering");
						TimeSpan startDelay = this.GetStartDelay();
						ExTraceGlobals.OAuthTracer.TraceDebug<DateTime>(0L, "[ConfigProvider::EnableAutoRefresh] refresh timer set, start at ~{0}", DateTime.UtcNow.Add(startDelay));
						this.refreshTimer = new GuardedTimer(new TimerCallback(this.AutoRefreshCallback), null, startDelay, ConfigProvider.refreshInterval);
					}
				}
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0002E17C File Offset: 0x0002C37C
		private TimeSpan GetStartDelay()
		{
			TimeSpan result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				int id = currentProcess.Id;
				Random random = new Random(id);
				result = TimeSpan.FromSeconds((double)random.Next(0, (int)ConfigProvider.refreshInterval.TotalSeconds));
			}
			return result;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0002E1D8 File Offset: 0x0002C3D8
		private void DisableAutoRefresh()
		{
			if (this.refreshTimer != null)
			{
				lock (this.instanceLockObj)
				{
					if (this.refreshTimer != null)
					{
						ExTraceGlobals.OAuthTracer.TraceDebug(0L, "[ConfigProvider::DisableAutoRefresh] clearing the refresh timer.");
						this.refreshTimer.Dispose(true);
						this.refreshTimer = null;
					}
				}
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0002E248 File Offset: 0x0002C448
		private void AutoRefreshCallback(object state)
		{
			ExTraceGlobals.OAuthTracer.TraceDebug(0L, "[ConfigProvider::AutoRefresh] starting refreshing");
			Exception exception = null;
			LocalConfiguration localConfiguration = LocalConfiguration.InternalLoadHelper(null, this.loadTrustedIssuers, out exception);
			if (localConfiguration != null)
			{
				ExTraceGlobals.OAuthTracer.TraceDebug(0L, "[ConfigProvider::AutoRefresh] configuration was updated successfully");
				this.underlyingConfiguration = localConfiguration;
				this.lastRefreshDateTime = DateTime.UtcNow;
			}
			else
			{
				ExTraceGlobals.OAuthTracer.TraceWarning(0L, "[ConfigProvider::AutoRefresh] configuration was not updated.");
				this.LogEventIfNecessary(exception);
			}
			ExTraceGlobals.OAuthTracer.TraceDebug<DateTime>(0L, "[ConfigProvider::AutoRefresh] finishing refresh, next refresh at {0}", DateTime.UtcNow.Add(ConfigProvider.refreshInterval));
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0002E2DC File Offset: 0x0002C4DC
		private void ManualLoadIfNecessary()
		{
			if (DateTime.UtcNow - this.lastRefreshDateTime > ConfigProvider.refreshInterval)
			{
				lock (this.instanceLockObj)
				{
					if (DateTime.UtcNow - this.lastRefreshDateTime > ConfigProvider.refreshInterval)
					{
						ExTraceGlobals.OAuthTracer.TraceDebug(0L, "[ConfigProvider::ManualLoadIfNecessary] loading the configuration");
						Exception exception = null;
						LocalConfiguration localConfiguration = LocalConfiguration.InternalLoadHelper(null, this.loadTrustedIssuers, out exception);
						if (localConfiguration != null)
						{
							ExTraceGlobals.OAuthTracer.TraceDebug(0L, "[ConfigProvider::ManualLoadIfNecessary] configuration was updated successfully");
							this.underlyingConfiguration = localConfiguration;
							this.lastRefreshDateTime = DateTime.UtcNow;
						}
						else
						{
							this.LogEventIfNecessary(exception);
						}
					}
				}
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0002E3A4 File Offset: 0x0002C5A4
		private void LogEventIfNecessary(Exception exception)
		{
			ExTraceGlobals.OAuthTracer.TraceDebug<DateTime>((long)this.GetHashCode(), "[ConfigProvider::LogEventIfNecessary] unable to load the configuration, last successfuly load was at {0}", this.lastRefreshDateTime);
			if (DateTime.UtcNow - this.lastRefreshDateTime > ConfigProvider.alertInterval)
			{
				OAuthCommon.EventLogger.LogEvent(SecurityEventLogConstants.Tuple_OAuthFailToLoadLocalConfiguration, string.Empty, new object[]
				{
					this.lastRefreshDateTime,
					exception
				});
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0002E418 File Offset: 0x0002C618
		public void EnforceReload()
		{
			this.lastRefreshDateTime = DateTime.MinValue;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0002E425 File Offset: 0x0002C625
		public void ManuallyReload()
		{
			this.EnforceReload();
			this.ManualLoadIfNecessary();
		}

		// Token: 0x040005CA RID: 1482
		private static readonly object staticLockObj = new object();

		// Token: 0x040005CB RID: 1483
		internal static readonly TimeSpan refreshInterval = TimeSpan.FromMinutes(30.0);

		// Token: 0x040005CC RID: 1484
		private static readonly TimeSpan alertInterval = TimeSpan.FromMinutes(ConfigProvider.refreshInterval.TotalMinutes * 3.0);

		// Token: 0x040005CD RID: 1485
		private static ConfigProvider instance;

		// Token: 0x040005CE RID: 1486
		private readonly object instanceLockObj = new object();

		// Token: 0x040005CF RID: 1487
		private GuardedTimer refreshTimer;

		// Token: 0x040005D0 RID: 1488
		private DateTime lastRefreshDateTime = DateTime.MinValue;

		// Token: 0x040005D1 RID: 1489
		private LocalConfiguration underlyingConfiguration;

		// Token: 0x040005D2 RID: 1490
		private bool autoRefreshEnabled;

		// Token: 0x040005D3 RID: 1491
		private bool loadTrustedIssuers;
	}
}
