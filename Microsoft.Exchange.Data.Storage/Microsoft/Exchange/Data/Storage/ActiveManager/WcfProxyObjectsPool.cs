using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Microsoft.Exchange.Data.Storage.ServerLocator;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x020002FA RID: 762
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class WcfProxyObjectsPool : IDisposable
	{
		// Token: 0x06002289 RID: 8841 RVA: 0x0008A888 File Offset: 0x00088A88
		internal WcfProxyObjectsPool()
		{
			WcfProxyObjectsPool.s_backgroundThreadSyncPoint = 0;
			this.m_memoryCleanupBackgroundTimer = new System.Timers.Timer();
			this.m_memoryCleanupBackgroundTimer.Elapsed += this.MemoryCleanupBackgroundTimerRun;
			this.m_memoryCleanupBackgroundTimer.Interval = AmRpcClientHelper.WcfCleanupTime.TotalMilliseconds;
			this.m_memoryCleanupBackgroundTimer.AutoReset = true;
			this.m_memoryCleanupBackgroundTimer.Enabled = true;
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x0008A910 File Offset: 0x00088B10
		private void MemoryCleanupBackgroundTimerRun(object sender, ElapsedEventArgs e)
		{
			int num = 0;
			if (Interlocked.CompareExchange(ref WcfProxyObjectsPool.s_backgroundThreadSyncPoint, 1, 0) == 0)
			{
				num = this.Compact();
				WcfProxyObjectsPool.s_backgroundThreadSyncPoint = 0;
			}
			if (AmRpcClientHelper.LogDiagnosticEvents)
			{
				StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_ActiveManagerWCFCleanup, null, new object[]
				{
					num
				});
			}
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x0008A968 File Offset: 0x00088B68
		private ServerLocatorServiceClient TakeClientFromThePool(string serverName, out DateTime lastAccessTimeUtc)
		{
			ServerLocatorServiceClient result;
			lock (this.lockObject)
			{
				WcfProxyObjectsPerServerStack wcfProxyObjectsPerServerStack;
				if (this.m_dictionary.TryGetValue(serverName, out wcfProxyObjectsPerServerStack))
				{
					lastAccessTimeUtc = wcfProxyObjectsPerServerStack.LastAccessTimeUtc;
					if (wcfProxyObjectsPerServerStack.Count > 0)
					{
						result = wcfProxyObjectsPerServerStack.Pop();
					}
					else
					{
						result = null;
					}
				}
				else
				{
					lastAccessTimeUtc = DateTime.MinValue;
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x0008A9E4 File Offset: 0x00088BE4
		internal int Count(string serverName)
		{
			int result;
			lock (this.lockObject)
			{
				WcfProxyObjectsPerServerStack wcfProxyObjectsPerServerStack;
				if (this.m_dictionary.TryGetValue(serverName, out wcfProxyObjectsPerServerStack))
				{
					result = wcfProxyObjectsPerServerStack.Count;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x0008AA3C File Offset: 0x00088C3C
		internal ServerLocatorServiceClient TakeClientFromThePool(string serverName)
		{
			DateTime dateTime;
			return this.TakeClientFromThePool(serverName, out dateTime);
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x0008AA54 File Offset: 0x00088C54
		internal bool ReturnClientIntoThePool(string serverName, ServerLocatorServiceClient client)
		{
			bool result = false;
			lock (this.lockObject)
			{
				WcfProxyObjectsPerServerStack wcfProxyObjectsPerServerStack;
				if (this.m_dictionary.TryGetValue(serverName, out wcfProxyObjectsPerServerStack))
				{
					if (wcfProxyObjectsPerServerStack.Count < WcfProxyObjectsPool.s_maximumSizeOfThePool)
					{
						wcfProxyObjectsPerServerStack.Push(client);
						result = true;
					}
					else
					{
						result = false;
					}
				}
				else
				{
					wcfProxyObjectsPerServerStack = new WcfProxyObjectsPerServerStack();
					wcfProxyObjectsPerServerStack.Push(client);
					this.m_dictionary[serverName] = wcfProxyObjectsPerServerStack;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x0008AADC File Offset: 0x00088CDC
		internal int Compact()
		{
			int num = 0;
			List<string> list = new List<string>(WcfProxyObjectsPool.s_numberOfServersPerDag);
			DateTime t = DateTime.UtcNow - AmRpcClientHelper.WcfCleanupTime;
			lock (this.lockObject)
			{
				foreach (KeyValuePair<string, WcfProxyObjectsPerServerStack> keyValuePair in this.m_dictionary)
				{
					if (keyValuePair.Value.LastAccessTimeUtc <= t)
					{
						list.Add(keyValuePair.Key);
					}
				}
			}
			foreach (string serverName in list)
			{
				DateTime minValue = DateTime.MinValue;
				ServerLocatorServiceClient serverLocatorServiceClient;
				while ((serverLocatorServiceClient = this.TakeClientFromThePool(serverName, out minValue)) != null)
				{
					if (minValue > t)
					{
						this.ReturnClientIntoThePool(serverName, serverLocatorServiceClient);
						break;
					}
					serverLocatorServiceClient.Dispose(true);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x0008AC10 File Offset: 0x00088E10
		internal void Clear()
		{
			while (Interlocked.CompareExchange(ref WcfProxyObjectsPool.s_backgroundThreadSyncPoint, -1, 0) != 0)
			{
				Thread.Yield();
			}
			lock (this.lockObject)
			{
				foreach (WcfProxyObjectsPerServerStack wcfProxyObjectsPerServerStack in this.m_dictionary.Values)
				{
					while (wcfProxyObjectsPerServerStack.Count > 0)
					{
						ServerLocatorServiceClient serverLocatorServiceClient = wcfProxyObjectsPerServerStack.Pop();
						serverLocatorServiceClient.Dispose(true);
					}
				}
				this.m_dictionary.Clear();
			}
			WcfProxyObjectsPool.s_backgroundThreadSyncPoint = 0;
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0008ACD0 File Offset: 0x00088ED0
		public void Dispose()
		{
			this.m_memoryCleanupBackgroundTimer.Stop();
			while (Interlocked.CompareExchange(ref WcfProxyObjectsPool.s_backgroundThreadSyncPoint, -1, 0) != 0)
			{
				Thread.Yield();
			}
			this.m_memoryCleanupBackgroundTimer.Dispose();
			this.m_memoryCleanupBackgroundTimer = null;
		}

		// Token: 0x0400140D RID: 5133
		private readonly Dictionary<string, WcfProxyObjectsPerServerStack> m_dictionary = new Dictionary<string, WcfProxyObjectsPerServerStack>(WcfProxyObjectsPool.s_numberOfServersPerDag);

		// Token: 0x0400140E RID: 5134
		private static int s_numberOfServersPerDag = 16;

		// Token: 0x0400140F RID: 5135
		private static int s_maximumSizeOfThePool = 10;

		// Token: 0x04001410 RID: 5136
		private object lockObject = new object();

		// Token: 0x04001411 RID: 5137
		private System.Timers.Timer m_memoryCleanupBackgroundTimer;

		// Token: 0x04001412 RID: 5138
		private static int s_backgroundThreadSyncPoint = 0;
	}
}
