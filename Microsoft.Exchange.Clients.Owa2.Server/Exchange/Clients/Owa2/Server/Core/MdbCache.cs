using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000048 RID: 72
	public class MdbCache
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x00007B0D File Offset: 0x00005D0D
		private MdbCache(TimeSpan updateInterval, MdbCache.IQuery query)
		{
			this.updateInterval = updateInterval;
			this.query = query;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00007B4D File Offset: 0x00005D4D
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00007B55 File Offset: 0x00005D55
		public Action<IList<string>> ExecuteAfterAsyncUpdate
		{
			get
			{
				return this.executeAfterAysncUpdate;
			}
			set
			{
				this.executeAfterAysncUpdate = value;
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007B60 File Offset: 0x00005D60
		public static MdbCache GetInstance()
		{
			if (MdbCache.singleton == null)
			{
				lock (MdbCache.creationLock)
				{
					if (MdbCache.singleton == null)
					{
						MdbCache.singleton = new MdbCache(WacConfiguration.Instance.MdbCacheInterval, new MdbCacheQuery());
					}
				}
			}
			return MdbCache.singleton;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007BC8 File Offset: 0x00005DC8
		internal static MdbCache GetTestInstance(TimeSpan updateInterval, MdbCache.IQuery query)
		{
			return new MdbCache(updateInterval, query);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007C26 File Offset: 0x00005E26
		public void BeginAsyncUpdate()
		{
			ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				this.OptionalCacheUpdate();
				if (this.executeAfterAysncUpdate != null && this.databasePaths != null)
				{
					string[] array = new string[this.databasePaths.Count];
					this.databasePaths.Values.CopyTo(array, 0);
					this.executeAfterAysncUpdate(array);
				}
			});
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007C3C File Offset: 0x00005E3C
		public string GetPath(Guid mdb)
		{
			this.EnsureCacheIsLoaded();
			string result;
			if (this.databasePaths.TryGetValue(mdb, out result))
			{
				return result;
			}
			this.MandatoryCacheUpdate();
			if (this.databasePaths.TryGetValue(mdb, out result))
			{
				return result;
			}
			throw new OwaInvalidRequestException(string.Format("MDB {0} not found on this server.", mdb));
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007C8E File Offset: 0x00005E8E
		private void EnsureCacheIsLoaded()
		{
			if (this.databasePaths == null)
			{
				this.MandatoryCacheUpdate();
				if (this.databasePaths == null)
				{
					throw new InvalidOperationException("Unable to acquire MDB information.");
				}
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007CB4 File Offset: 0x00005EB4
		private void OptionalCacheUpdate()
		{
			try
			{
				if (Monitor.TryEnter(this.updateLock))
				{
					if (this.UpdateIntervalElapsed())
					{
						this.UpdateWithinLock();
					}
				}
			}
			finally
			{
				if (Monitor.IsEntered(this.updateLock))
				{
					Monitor.Exit(this.updateLock);
				}
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00007D0C File Offset: 0x00005F0C
		private void MandatoryCacheUpdate()
		{
			try
			{
				if (!Monitor.TryEnter(this.updateLock))
				{
					lock (this.updateLock)
					{
					}
				}
				else
				{
					this.UpdateWithinLock();
				}
			}
			finally
			{
				if (Monitor.IsEntered(this.updateLock))
				{
					Monitor.Exit(this.updateLock);
				}
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00007D84 File Offset: 0x00005F84
		private void UpdateWithinLock()
		{
			if (!Monitor.IsEntered(this.updateLock))
			{
				throw new InvalidOperationException("MdbCache.UpdateWithinLock must only be called after acquiring the lock.");
			}
			Dictionary<Guid, string> dictionary;
			if (this.query.TryGetDatabasePaths(out dictionary))
			{
				this.databasePaths = dictionary;
			}
			this.lastUpdate = ExDateTime.Now;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007DCA File Offset: 0x00005FCA
		private bool UpdateIntervalElapsed()
		{
			return this.lastUpdate + this.updateInterval < ExDateTime.Now;
		}

		// Token: 0x040000C6 RID: 198
		private static readonly object creationLock = new object();

		// Token: 0x040000C7 RID: 199
		private static MdbCache singleton;

		// Token: 0x040000C8 RID: 200
		private readonly object updateLock = new object();

		// Token: 0x040000C9 RID: 201
		private readonly TimeSpan updateInterval = TimeSpan.FromMinutes(30.0);

		// Token: 0x040000CA RID: 202
		private ExDateTime lastUpdate = ExDateTime.MinValue;

		// Token: 0x040000CB RID: 203
		private Dictionary<Guid, string> databasePaths;

		// Token: 0x040000CC RID: 204
		private readonly MdbCache.IQuery query;

		// Token: 0x040000CD RID: 205
		private Action<IList<string>> executeAfterAysncUpdate;

		// Token: 0x02000049 RID: 73
		public interface IQuery
		{
			// Token: 0x06000201 RID: 513
			bool TryGetDatabasePaths(out Dictionary<Guid, string> newPaths);
		}
	}
}
