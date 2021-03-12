using System;
using System.Collections.Generic;
using Microsoft.Exchange.AddressBook.Service;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AddressBook.Service;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.AddressBook.Nspi.Client
{
	// Token: 0x02000030 RID: 48
	internal class NspiConnectionPool
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x0000949F File Offset: 0x0000769F
		internal NspiConnectionPool(string server)
		{
			this.server = server;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x000094B9 File Offset: 0x000076B9
		internal string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000094C4 File Offset: 0x000076C4
		internal static NspiConnection GetConnection(string server, PartitionId partitionId)
		{
			NspiConnectionPool.NspiConnectionTracer.TraceDebug<string>(0L, "NspiConnectionPool.GetConnection: {0}", server ?? "(null)");
			if (string.IsNullOrEmpty(server))
			{
				if (!string.IsNullOrEmpty(Configuration.NspiTestServer))
				{
					server = Configuration.NspiTestServer;
					NspiConnectionPool.NspiConnectionTracer.TraceDebug<string>(0L, "Using test server: {0}", server ?? "(null)");
				}
				else
				{
					ADRunspaceServerSettingsProvider instance = ADRunspaceServerSettingsProvider.GetInstance();
					bool flag;
					server = instance.GetGcFromToken(partitionId.ForestFQDN, null, out flag, false).Fqdn;
					NspiConnectionPool.NspiConnectionTracer.TraceDebug<string>(0L, "Using GC: {0}", server ?? "(null)");
				}
			}
			NspiConnectionPool nspiConnectionPool;
			lock (NspiConnectionPool.pools)
			{
				if (!NspiConnectionPool.pools.TryGetValue(server, out nspiConnectionPool))
				{
					nspiConnectionPool = new NspiConnectionPool(server);
					NspiConnectionPool.pools[server] = nspiConnectionPool;
				}
			}
			return nspiConnectionPool.GetConnectionFromPool();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000095B4 File Offset: 0x000077B4
		private NspiConnection GetConnectionFromPool()
		{
			NspiConnection nspiConnection = null;
			bool flag = false;
			NspiConnection result;
			try
			{
				lock (this.connections)
				{
					int num = this.connectionsInUse;
					this.connectionsInUse++;
					if (this.connections.Count > 0)
					{
						int index = this.connections.Count - 1;
						nspiConnection = this.connections[index];
						this.connections.RemoveAt(index);
						flag = true;
						return nspiConnection;
					}
				}
				nspiConnection = new NspiConnection(this);
				if (nspiConnection.Connect() != NspiStatus.Success)
				{
					result = null;
				}
				else
				{
					flag = true;
					result = nspiConnection;
				}
			}
			finally
			{
				if (!flag && nspiConnection != null)
				{
					nspiConnection.Dispose();
					nspiConnection = null;
				}
			}
			return result;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00009680 File Offset: 0x00007880
		internal void ReturnToPool(NspiConnection connection)
		{
			lock (this.connections)
			{
				this.connectionsInUse--;
				if (this.connections.Count == 10)
				{
					this.connections[0].Dispose();
					this.connections.RemoveAt(0);
				}
				this.connections.Add(connection);
			}
		}

		// Token: 0x04000103 RID: 259
		private const int MaxPoolSize = 10;

		// Token: 0x04000104 RID: 260
		private const int MaxConnectionsInUse = 25;

		// Token: 0x04000105 RID: 261
		internal static readonly Trace NspiConnectionTracer = ExTraceGlobals.NspiConnectionTracer;

		// Token: 0x04000106 RID: 262
		private static readonly TimeSpan expireOlderThan = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000107 RID: 263
		private static readonly Dictionary<string, NspiConnectionPool> pools = new Dictionary<string, NspiConnectionPool>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000108 RID: 264
		private readonly string server;

		// Token: 0x04000109 RID: 265
		private List<NspiConnection> connections = new List<NspiConnection>();

		// Token: 0x0400010A RID: 266
		private int connectionsInUse;
	}
}
