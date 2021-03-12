using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Common
{
	// Token: 0x02000004 RID: 4
	internal class ConnectionsTracker
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000246C File Offset: 0x0000066C
		public ConnectionsTracker(ConnectionsTracker.GetExPerfCounterDelegate getConnectionsCurrent, ConnectionsTracker.GetExPerfCounterDelegate getConnectionsTotal)
		{
			ArgumentValidator.ThrowIfNull("GetConnectionsCurrent", getConnectionsCurrent);
			ArgumentValidator.ThrowIfNull("getConnectionsTotal", getConnectionsTotal);
			this.getConnectionsCurrent = getConnectionsCurrent;
			this.getConnectionsTotal = getConnectionsTotal;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024C4 File Offset: 0x000006C4
		public void IncrementProxyCount(string forest)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("forest", forest);
			ConnectionsTracker.Connections orAdd = this.proxyConnectionsDictionary.GetOrAdd(forest, (string c) => new ConnectionsTracker.Connections());
			orAdd.Increment();
			this.getConnectionsCurrent(forest).Increment();
			this.getConnectionsTotal(forest).Increment();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002530 File Offset: 0x00000730
		public void DecrementProxyCount(string forest)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("forest", forest);
			ConnectionsTracker.Connections connections;
			if (this.proxyConnectionsDictionary.TryGetValue(forest, out connections))
			{
				connections.Decrement();
				this.getConnectionsCurrent(forest).Decrement();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002570 File Offset: 0x00000770
		public int GetUsage(string forest)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("forest", forest);
			int result = 0;
			ConnectionsTracker.Connections connections;
			if (this.proxyConnectionsDictionary.TryGetValue(forest, out connections))
			{
				result = connections.Count;
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025A4 File Offset: 0x000007A4
		public IEnumerable<XElement> GetDiagnosticInfo(string xmlNodeName)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("xmlNodeName", xmlNodeName);
			List<XElement> list = new List<XElement>();
			foreach (KeyValuePair<string, ConnectionsTracker.Connections> keyValuePair in this.proxyConnectionsDictionary)
			{
				XElement xelement = new XElement(xmlNodeName);
				xelement.SetAttributeValue("Name", keyValuePair.Key);
				xelement.SetAttributeValue("Connections", keyValuePair.Value.Count);
				list.Add(xelement);
			}
			return list;
		}

		// Token: 0x04000003 RID: 3
		private readonly ConcurrentDictionary<string, ConnectionsTracker.Connections> proxyConnectionsDictionary = new ConcurrentDictionary<string, ConnectionsTracker.Connections>(Environment.ProcessorCount, 25, StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x04000004 RID: 4
		private readonly ConnectionsTracker.GetExPerfCounterDelegate getConnectionsCurrent;

		// Token: 0x04000005 RID: 5
		private readonly ConnectionsTracker.GetExPerfCounterDelegate getConnectionsTotal;

		// Token: 0x02000005 RID: 5
		// (Invoke) Token: 0x0600001F RID: 31
		public delegate ExPerformanceCounter GetExPerfCounterDelegate(string instanceName);

		// Token: 0x02000006 RID: 6
		private class Connections
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000022 RID: 34 RVA: 0x00002648 File Offset: 0x00000848
			public int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x06000023 RID: 35 RVA: 0x00002650 File Offset: 0x00000850
			public void Increment()
			{
				Interlocked.Increment(ref this.count);
			}

			// Token: 0x06000024 RID: 36 RVA: 0x0000265E File Offset: 0x0000085E
			public void Decrement()
			{
				Interlocked.Decrement(ref this.count);
			}

			// Token: 0x04000007 RID: 7
			private int count;
		}
	}
}
