using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000151 RID: 337
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class SafeInstanceTable<T> where T : IIdentityGuid
	{
		// Token: 0x06000D17 RID: 3351 RVA: 0x00039A94 File Offset: 0x00037C94
		internal void AddInstance(T instance)
		{
			this.m_rwLock.AcquireWriterLock(-1);
			try
			{
				this.m_instances.Add(instance.Identity, instance);
			}
			finally
			{
				this.m_rwLock.ReleaseWriterLock();
			}
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00039AE4 File Offset: 0x00037CE4
		internal virtual void RemoveInstance(T instance)
		{
			this.m_rwLock.AcquireWriterLock(-1);
			try
			{
				this.m_instances.Remove(instance.Identity);
			}
			finally
			{
				this.m_rwLock.ReleaseWriterLock();
			}
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00039B34 File Offset: 0x00037D34
		internal void UpdateInstance(T oldInstance, T newInstance)
		{
			this.m_rwLock.AcquireWriterLock(-1);
			try
			{
				this.m_instances.Remove(oldInstance.Identity);
				this.m_instances.Add(newInstance.Identity, newInstance);
			}
			finally
			{
				this.m_rwLock.ReleaseWriterLock();
			}
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00039BA0 File Offset: 0x00037DA0
		internal bool TryGetInstance(string identity, out T instance)
		{
			this.m_rwLock.AcquireReaderLock(-1);
			bool result;
			try
			{
				if (this.m_instances.ContainsKey(identity))
				{
					instance = this.m_instances[identity];
					result = true;
				}
				else
				{
					instance = default(T);
					result = false;
				}
			}
			finally
			{
				this.m_rwLock.ReleaseReaderLock();
			}
			return result;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00039C08 File Offset: 0x00037E08
		internal bool TryGetInstance(Guid guid, out T instance)
		{
			string identityFromGuid = SafeInstanceTable<T>.GetIdentityFromGuid(guid);
			return this.TryGetInstance(identityFromGuid, out instance);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00039C24 File Offset: 0x00037E24
		internal T[] GetAllInstances()
		{
			this.m_rwLock.AcquireReaderLock(-1);
			T[] result;
			try
			{
				T[] array = new T[this.m_instances.Count];
				this.m_instances.Values.CopyTo(array, 0);
				result = array;
			}
			finally
			{
				this.m_rwLock.ReleaseReaderLock();
			}
			return result;
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00039C84 File Offset: 0x00037E84
		internal ICollection<T> GetAllInstancesUnsafe()
		{
			this.m_rwLock.AcquireReaderLock(-1);
			ICollection<T> values;
			try
			{
				values = this.m_instances.Values;
			}
			finally
			{
				this.m_rwLock.ReleaseReaderLock();
			}
			return values;
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x00039CC8 File Offset: 0x00037EC8
		internal int Count
		{
			get
			{
				this.m_rwLock.AcquireReaderLock(-1);
				int count;
				try
				{
					count = this.m_instances.Count;
				}
				finally
				{
					this.m_rwLock.ReleaseReaderLock();
				}
				return count;
			}
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00039D0C File Offset: 0x00037F0C
		public static string GetIdentityFromGuid(Guid guid)
		{
			return guid.ToString().ToLowerInvariant();
		}

		// Token: 0x0400058D RID: 1421
		protected ReaderWriterLock m_rwLock = new ReaderWriterLock();

		// Token: 0x0400058E RID: 1422
		protected Dictionary<string, T> m_instances = new Dictionary<string, T>();
	}
}
