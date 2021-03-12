using System;
using System.Collections.Concurrent;
using System.ServiceModel.Description;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200005F RID: 95
	public class InstanceClientFactory : IDisposable
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x0000AAB2 File Offset: 0x00008CB2
		public InstanceClientFactory(InstanceGroupConfig groupCfg, WcfTimeout timeout = null)
		{
			this.FactoryByTarget = new ConcurrentDictionary<string, Tuple<CachedChannelFactory<IDxStoreInstance>, DxStoreInstanceClient>>();
			this.groupCfg = groupCfg;
			this.DefaultTimeout = timeout;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000AAD3 File Offset: 0x00008CD3
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000AADB File Offset: 0x00008CDB
		public ConcurrentDictionary<string, Tuple<CachedChannelFactory<IDxStoreInstance>, DxStoreInstanceClient>> FactoryByTarget { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000AAE4 File Offset: 0x00008CE4
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x0000AAEC File Offset: 0x00008CEC
		public WcfTimeout DefaultTimeout { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000AAF5 File Offset: 0x00008CF5
		public DxStoreInstanceClient LocalClient
		{
			get
			{
				return this.GetClient(null);
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000AB58 File Offset: 0x00008D58
		public DxStoreInstanceClient GetClient(string target)
		{
			if (string.IsNullOrEmpty(target))
			{
				target = this.groupCfg.Self;
			}
			Tuple<CachedChannelFactory<IDxStoreInstance>, DxStoreInstanceClient> orAdd = this.FactoryByTarget.GetOrAdd(target, delegate(string server)
			{
				ServiceEndpoint storeInstanceEndpoint = this.groupCfg.GetStoreInstanceEndpoint(server, false, false, this.DefaultTimeout);
				CachedChannelFactory<IDxStoreInstance> cachedChannelFactory = new CachedChannelFactory<IDxStoreInstance>(storeInstanceEndpoint);
				DxStoreInstanceClient item = new DxStoreInstanceClient(cachedChannelFactory, (this.DefaultTimeout != null) ? this.DefaultTimeout.Operation : null);
				return Tuple.Create<CachedChannelFactory<IDxStoreInstance>, DxStoreInstanceClient>(cachedChannelFactory, item);
			});
			return orAdd.Item2;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000AB99 File Offset: 0x00008D99
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000ABB0 File Offset: 0x00008DB0
		protected void Dispose(bool disposing)
		{
			lock (this)
			{
				if (!this.isDisposed)
				{
					if (disposing)
					{
						foreach (Tuple<CachedChannelFactory<IDxStoreInstance>, DxStoreInstanceClient> tuple in this.FactoryByTarget.Values)
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

		// Token: 0x040001E7 RID: 487
		private readonly InstanceGroupConfig groupCfg;

		// Token: 0x040001E8 RID: 488
		private bool isDisposed;
	}
}
