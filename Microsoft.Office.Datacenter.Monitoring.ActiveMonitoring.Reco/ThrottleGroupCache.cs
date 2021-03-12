using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000048 RID: 72
	internal class ThrottleGroupCache
	{
		// Token: 0x06000300 RID: 768 RVA: 0x0000A6D7 File Offset: 0x000088D7
		private ThrottleGroupCache()
		{
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000A716 File Offset: 0x00008916
		internal static ThrottleGroupCache Instance
		{
			get
			{
				return ThrottleGroupCache.lazy.Value;
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000A72C File Offset: 0x0000892C
		internal void StartPeriodicTimer()
		{
			GlobalTunables tunables = Dependencies.ThrottleHelper.Tunables;
			if (this.periodicTimer == null)
			{
				lock (this.timerLock)
				{
					if (this.periodicTimer == null)
					{
						this.periodicTimer = new Timer(delegate(object o)
						{
							this.UpdateCache();
						}, null, tunables.ThrottleGroupCacheRefreshStartDelay, tunables.ThrottleGroupCacheRefreshFrequency);
					}
				}
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000A7AC File Offset: 0x000089AC
		internal void StopTimer()
		{
			lock (this.timerLock)
			{
				if (this.periodicTimer != null)
				{
					this.periodicTimer.Change(-1, -1);
					this.periodicTimer.Dispose();
				}
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000A808 File Offset: 0x00008A08
		internal void AddGroup(string groupName)
		{
			if (!string.IsNullOrEmpty(groupName))
			{
				this.UpdateGroupMembership(groupName, false);
				this.StartPeriodicTimer();
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000A820 File Offset: 0x00008A20
		internal void AddServers(string[] servers)
		{
			if (servers != null)
			{
				this.UpdateServersInfo(servers, false);
				this.StartPeriodicTimer();
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000A834 File Offset: 0x00008A34
		internal string[] GetServersInGroup(string groupName, bool isForceRefresh)
		{
			if (string.IsNullOrEmpty(groupName))
			{
				return null;
			}
			string[] array = null;
			lock (this.locker)
			{
				this.groupServersMap.TryGetValue(groupName, out array);
				if (isForceRefresh || array == null)
				{
					this.UpdateGroupMembership(groupName, false);
					this.groupServersMap.TryGetValue(groupName, out array);
				}
			}
			return array;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000A8A8 File Offset: 0x00008AA8
		internal bool IsAllServersInGroupSupportVersion(string groupName, int requiredVersion, out int totalServersInGroup, out int totalServersInCompatibleVersion, bool isForceRefresh = false)
		{
			totalServersInGroup = 0;
			totalServersInCompatibleVersion = 0;
			this.UpdateGroupMembership(groupName, false);
			string[] serversInGroup = this.GetServersInGroup(groupName, isForceRefresh);
			return this.IsAllServersSupportVersion(serversInGroup, requiredVersion, out totalServersInGroup, out totalServersInCompatibleVersion, isForceRefresh);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000A8DC File Offset: 0x00008ADC
		internal bool IsAllServersSupportVersion(string[] servers, int requiredVersion, out int totalServersInGroup, out int totalServersInCompatibleVersion, bool isForceRefresh = false)
		{
			bool result = false;
			totalServersInGroup = ((servers != null) ? servers.Length : 0);
			totalServersInCompatibleVersion = 0;
			lock (this.locker)
			{
				if (servers != null)
				{
					this.UpdateServersInfo(servers, isForceRefresh);
					foreach (string key in servers)
					{
						ThrottleGroupCache.ServerMiniInfo serverMiniInfo = null;
						this.serverInfoMap.TryGetValue(key, out serverMiniInfo);
						if (serverMiniInfo != null && serverMiniInfo.Version >= requiredVersion)
						{
							totalServersInCompatibleVersion++;
						}
					}
					if (totalServersInCompatibleVersion == totalServersInGroup)
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000A980 File Offset: 0x00008B80
		internal void ClearCacheForTesting()
		{
			this.groupServersMap.Clear();
			this.serverInfoMap.Clear();
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000A998 File Offset: 0x00008B98
		private void UpdateCache()
		{
			try
			{
				if (Interlocked.Increment(ref this.timerCounter) <= 1)
				{
					this.UpdateCacheInternal();
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.timerCounter);
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000A9DC File Offset: 0x00008BDC
		private string[] ResolveGroupNameToServers(string groupName)
		{
			string[] result = null;
			try
			{
				result = Dependencies.ThrottleHelper.Settings.GetServersInGroup(groupName);
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError<string, string>(ExTraceGlobals.RecoveryActionTracer, this.traceContext, "ResolveGroupNameToServers:GetServersInGroup() failed for group {0} with error {1}", groupName, ex.ToString(), null, "ResolveGroupNameToServers", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\ThrottleGroupCache.cs", 296);
			}
			return result;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000AA40 File Offset: 0x00008C40
		private void UpdateCacheInternal()
		{
			lock (this.locker)
			{
				string[] array = this.groupServersMap.Keys.ToArray<string>();
				foreach (string text in array)
				{
					string[] array3 = this.ResolveGroupNameToServers(text);
					if (array3 != null)
					{
						this.groupServersMap[text] = array3;
						foreach (string key in array3)
						{
							ThrottleGroupCache.ServerMiniInfo serverMiniInfo = null;
							if (!this.serverInfoMap.TryGetValue(key, out serverMiniInfo))
							{
								this.serverInfoMap[key] = null;
							}
						}
					}
				}
				string[] array5 = this.serverInfoMap.Keys.ToArray<string>();
				foreach (string server in array5)
				{
					this.UpdateSingleServer(server);
				}
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000AB38 File Offset: 0x00008D38
		private void UpdateGroupMembership(string groupName, bool isForceRefresh = false)
		{
			if (groupName == null)
			{
				return;
			}
			lock (this.locker)
			{
				string[] array = null;
				this.groupServersMap.TryGetValue(groupName, out array);
				if (array == null || isForceRefresh)
				{
					array = this.ResolveGroupNameToServers(groupName);
					this.groupServersMap[groupName] = array;
					this.UpdateServersInfo(array, isForceRefresh);
				}
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000ABAC File Offset: 0x00008DAC
		private void UpdateServersInfo(string[] servers, bool isForceRefresh = false)
		{
			if (servers == null || servers.Length == 0)
			{
				return;
			}
			lock (this.locker)
			{
				if (servers != null)
				{
					foreach (string text in servers)
					{
						ThrottleGroupCache.ServerMiniInfo serverMiniInfo = null;
						bool flag2 = isForceRefresh;
						if (!this.serverInfoMap.TryGetValue(text, out serverMiniInfo))
						{
							this.serverInfoMap[text] = null;
							flag2 = true;
						}
						if (flag2)
						{
							this.UpdateSingleServer(text);
						}
					}
				}
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000AC40 File Offset: 0x00008E40
		private void UpdateSingleServer(string server)
		{
			try
			{
				ThrottleGroupCache.ServerMiniInfo value = new ThrottleGroupCache.ServerMiniInfo
				{
					Version = Dependencies.ThrottleHelper.GetServerVersion(server)
				};
				this.serverInfoMap[server] = value;
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError<string, string>(ExTraceGlobals.RecoveryActionTracer, this.traceContext, "UpdateSingleServer() failed for server {0} with error {1}", server, ex.ToString(), null, "UpdateSingleServer", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Recovery\\ThrottleGroupCache.cs", 425);
			}
		}

		// Token: 0x040001D9 RID: 473
		private static readonly Lazy<ThrottleGroupCache> lazy = new Lazy<ThrottleGroupCache>(() => new ThrottleGroupCache());

		// Token: 0x040001DA RID: 474
		private readonly object locker = new object();

		// Token: 0x040001DB RID: 475
		private readonly object timerLock = new object();

		// Token: 0x040001DC RID: 476
		private Dictionary<string, string[]> groupServersMap = new Dictionary<string, string[]>();

		// Token: 0x040001DD RID: 477
		private Dictionary<string, ThrottleGroupCache.ServerMiniInfo> serverInfoMap = new Dictionary<string, ThrottleGroupCache.ServerMiniInfo>();

		// Token: 0x040001DE RID: 478
		private Timer periodicTimer;

		// Token: 0x040001DF RID: 479
		private TracingContext traceContext = TracingContext.Default;

		// Token: 0x040001E0 RID: 480
		private int timerCounter;

		// Token: 0x02000049 RID: 73
		internal class ServerMiniInfo
		{
			// Token: 0x17000107 RID: 263
			// (get) Token: 0x06000313 RID: 787 RVA: 0x0000ACE4 File Offset: 0x00008EE4
			// (set) Token: 0x06000314 RID: 788 RVA: 0x0000ACEC File Offset: 0x00008EEC
			internal int Version { get; set; }
		}
	}
}
