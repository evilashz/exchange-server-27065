using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x0200080A RID: 2058
	internal class IPConnectionTable
	{
		// Token: 0x06002B32 RID: 11058 RVA: 0x0005E9E4 File Offset: 0x0005CBE4
		public IPConnectionTable(int connectionRate)
		{
			this.ipTableSweeper = new Timer(new TimerCallback(this.CleanupIPTable), null, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(2.0));
			this.connectionRateLimit = connectionRate;
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06002B33 RID: 11059 RVA: 0x0005EA3D File Offset: 0x0005CC3D
		// (set) Token: 0x06002B34 RID: 11060 RVA: 0x0005EA45 File Offset: 0x0005CC45
		public int ConnectionRateLimit
		{
			get
			{
				return this.connectionRateLimit;
			}
			set
			{
				this.connectionRateLimit = value;
			}
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x0005EA50 File Offset: 0x0005CC50
		public bool CanAcceptConnection(IPAddress ipAddress)
		{
			object syncRoot = ((ICollection)this.ipTable).SyncRoot;
			bool result;
			try
			{
				if (!Monitor.TryEnter(syncRoot))
				{
					result = true;
				}
				else
				{
					TokenRateLimiter tokenRateLimiter;
					if (!this.ipTable.TryGetValue(ipAddress, out tokenRateLimiter))
					{
						tokenRateLimiter = new TokenRateLimiter();
						this.ipTable.Add(ipAddress, tokenRateLimiter);
					}
					result = tokenRateLimiter.TryFetchToken(this.connectionRateLimit);
				}
			}
			finally
			{
				if (Monitor.IsEntered(syncRoot))
				{
					Monitor.Exit(syncRoot);
				}
			}
			return result;
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x0005EAC8 File Offset: 0x0005CCC8
		private void CleanupIPTable(object state)
		{
			DateTime utcNow = DateTime.UtcNow;
			lock (((ICollection)this.ipTable).SyncRoot)
			{
				List<IPAddress> list = new List<IPAddress>();
				foreach (KeyValuePair<IPAddress, TokenRateLimiter> keyValuePair in this.ipTable)
				{
					if (keyValuePair.Value.IsIdle(utcNow))
					{
						list.Add(keyValuePair.Key);
					}
				}
				foreach (IPAddress key in list)
				{
					this.ipTable.Remove(key);
				}
			}
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x0005EBB4 File Offset: 0x0005CDB4
		public void Close()
		{
			if (this.ipTableSweeper != null)
			{
				this.ipTableSweeper.Dispose();
				this.ipTableSweeper = null;
			}
		}

		// Token: 0x04002599 RID: 9625
		private Dictionary<IPAddress, TokenRateLimiter> ipTable = new Dictionary<IPAddress, TokenRateLimiter>();

		// Token: 0x0400259A RID: 9626
		private Timer ipTableSweeper;

		// Token: 0x0400259B RID: 9627
		private int connectionRateLimit;
	}
}
