using System;
using System.Collections.Concurrent;
using System.ServiceModel.Description;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000064 RID: 100
	public class ManagerClientFactory : IDisposable
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x0000C450 File Offset: 0x0000A650
		public ManagerClientFactory(InstanceManagerConfig managerConfig, WcfTimeout timeout = null)
		{
			this.FactoryByTarget = new ConcurrentDictionary<string, Tuple<CachedChannelFactory<IDxStoreManager>, DxStoreManagerClient>>();
			this.managerConfig = managerConfig;
			this.DefaultTimeout = timeout;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000C471 File Offset: 0x0000A671
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x0000C479 File Offset: 0x0000A679
		public ConcurrentDictionary<string, Tuple<CachedChannelFactory<IDxStoreManager>, DxStoreManagerClient>> FactoryByTarget { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000C482 File Offset: 0x0000A682
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x0000C48A File Offset: 0x0000A68A
		public WcfTimeout DefaultTimeout { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000C493 File Offset: 0x0000A693
		public DxStoreManagerClient LocalClient
		{
			get
			{
				return this.GetClient(null);
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		public DxStoreManagerClient GetClient(string target)
		{
			if (string.IsNullOrEmpty(target))
			{
				target = this.managerConfig.Self;
			}
			Tuple<CachedChannelFactory<IDxStoreManager>, DxStoreManagerClient> orAdd = this.FactoryByTarget.GetOrAdd(target, delegate(string server)
			{
				ServiceEndpoint endpoint = this.managerConfig.GetEndpoint(server, false, this.DefaultTimeout);
				CachedChannelFactory<IDxStoreManager> cachedChannelFactory = new CachedChannelFactory<IDxStoreManager>(endpoint);
				DxStoreManagerClient item = new DxStoreManagerClient(cachedChannelFactory, (this.DefaultTimeout != null) ? this.DefaultTimeout.Operation : null);
				return Tuple.Create<CachedChannelFactory<IDxStoreManager>, DxStoreManagerClient>(cachedChannelFactory, item);
			});
			return orAdd.Item2;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000C531 File Offset: 0x0000A731
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000C548 File Offset: 0x0000A748
		protected void Dispose(bool disposing)
		{
			lock (this)
			{
				if (!this.isDisposed)
				{
					if (disposing)
					{
						foreach (Tuple<CachedChannelFactory<IDxStoreManager>, DxStoreManagerClient> tuple in this.FactoryByTarget.Values)
						{
							if (tuple != null && tuple.Item1 != null)
							{
								tuple.Item1.Dispose();
							}
						}
					}
					this.isDisposed = true;
				}
			}
		}

		// Token: 0x04000204 RID: 516
		private readonly InstanceManagerConfig managerConfig;

		// Token: 0x04000205 RID: 517
		private bool isDisposed;
	}
}
