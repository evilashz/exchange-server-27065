using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000010 RID: 16
	internal class RpcServerCache
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x0000165C File Offset: 0x00000A5C
		public RpcServerCache()
		{
			this.cache = new Dictionary<Guid, RpcServerBase>();
			this.cacheLock = new ReaderWriterLockSlim();
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00050938 File Offset: 0x0004FD38
		public void Add(Guid guid, RpcServerBase server)
		{
			if (guid == Guid.Empty)
			{
				throw new ArgumentNullException("guid");
			}
			if (server == null)
			{
				throw new ArgumentNullException("server");
			}
			try
			{
				this.cacheLock.EnterWriteLock();
				this.cache.Add(guid, server);
			}
			finally
			{
				try
				{
					this.cacheLock.ExitWriteLock();
				}
				catch (SynchronizationLockException ex)
				{
				}
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000509CC File Offset: 0x0004FDCC
		public void Remove(Guid guid)
		{
			if (guid == Guid.Empty)
			{
				throw new ArgumentNullException("guid");
			}
			try
			{
				this.cacheLock.EnterWriteLock();
				this.cache.Remove(guid);
			}
			finally
			{
				try
				{
					this.cacheLock.ExitWriteLock();
				}
				catch (SynchronizationLockException ex)
				{
				}
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00050A54 File Offset: 0x0004FE54
		public RpcServerBase Lookup(Guid guid)
		{
			RpcServerBase result = null;
			try
			{
				this.cacheLock.EnterReadLock();
				if (!this.cache.TryGetValue(guid, out result))
				{
					result = null;
				}
			}
			finally
			{
				try
				{
					this.cacheLock.ExitReadLock();
				}
				catch (SynchronizationLockException ex)
				{
				}
			}
			return result;
		}

		// Token: 0x0400082A RID: 2090
		private readonly Dictionary<Guid, RpcServerBase> cache;

		// Token: 0x0400082B RID: 2091
		private readonly ReaderWriterLockSlim cacheLock;
	}
}
