using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C5F RID: 3167
	internal static class NetworkTimer
	{
		// Token: 0x06004637 RID: 17975 RVA: 0x000BB844 File Offset: 0x000B9A44
		public static void Add(NetworkConnection nc)
		{
			lock (NetworkTimer.networkConnections)
			{
				NetworkTimer.networkConnections.Add(nc.ConnectionId, new WeakReference(nc));
			}
		}

		// Token: 0x06004638 RID: 17976 RVA: 0x000BB894 File Offset: 0x000B9A94
		public static void Remove(NetworkConnection nc)
		{
			lock (NetworkTimer.networkConnections)
			{
				NetworkTimer.networkConnections.Remove(nc.ConnectionId);
			}
		}

		// Token: 0x06004639 RID: 17977 RVA: 0x000BB8E0 File Offset: 0x000B9AE0
		private static void CheckTimeouts(object ignored)
		{
			if (NetworkTimer.networkConnections.Count == 0)
			{
				return;
			}
			try
			{
				if (Monitor.TryEnter(NetworkTimer.timeoutTimer))
				{
					NetworkTimer.timeoutTimer.Change(-1, -1);
					lock (NetworkTimer.networkConnections)
					{
						List<long> list = null;
						long ticks = DateTime.UtcNow.Ticks;
						foreach (KeyValuePair<long, WeakReference> keyValuePair in NetworkTimer.networkConnections)
						{
							NetworkConnection networkConnection = (NetworkConnection)keyValuePair.Value.Target;
							if (networkConnection != null)
							{
								networkConnection.CheckForTimeouts(ticks);
							}
							else
							{
								if (list == null)
								{
									list = new List<long>();
								}
								list.Add(keyValuePair.Key);
							}
						}
						if (list != null)
						{
							for (int i = 0; i < list.Count; i++)
							{
								NetworkTimer.networkConnections.Remove(list[i]);
							}
						}
					}
				}
			}
			finally
			{
				if (Monitor.IsEntered(NetworkTimer.timeoutTimer))
				{
					Monitor.Exit(NetworkTimer.timeoutTimer);
					NetworkTimer.timeoutTimer.Change(1000, 1000);
				}
			}
		}

		// Token: 0x04003A9E RID: 15006
		private const int TimeoutFrequency = 1000;

		// Token: 0x04003A9F RID: 15007
		private static Dictionary<long, WeakReference> networkConnections = new Dictionary<long, WeakReference>();

		// Token: 0x04003AA0 RID: 15008
		private static Timer timeoutTimer = new Timer(new TimerCallback(NetworkTimer.CheckTimeouts), null, 1000, 1000);
	}
}
