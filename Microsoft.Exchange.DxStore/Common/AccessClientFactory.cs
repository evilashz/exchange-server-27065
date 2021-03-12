using System;
using System.Collections.Concurrent;
using System.ServiceModel.Description;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200004C RID: 76
	public class AccessClientFactory
	{
		// Token: 0x06000279 RID: 633 RVA: 0x000046BD File Offset: 0x000028BD
		public AccessClientFactory(InstanceGroupConfig groupCfg, WcfTimeout timeout = null)
		{
			this.FactoryByTarget = new ConcurrentDictionary<string, Tuple<CachedChannelFactory<IDxStoreAccess>, IDxStoreAccessClient>>();
			this.groupCfg = groupCfg;
			this.DefaultTimeout = timeout;
			if (groupCfg.Settings.IsUseHttpTransportForClientCommunication)
			{
				HttpConfiguration.Configure(this.groupCfg);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600027A RID: 634 RVA: 0x000046F6 File Offset: 0x000028F6
		// (set) Token: 0x0600027B RID: 635 RVA: 0x000046FE File Offset: 0x000028FE
		public ConcurrentDictionary<string, Tuple<CachedChannelFactory<IDxStoreAccess>, IDxStoreAccessClient>> FactoryByTarget { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00004707 File Offset: 0x00002907
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000470F File Offset: 0x0000290F
		public WcfTimeout DefaultTimeout { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00004718 File Offset: 0x00002918
		public IDxStoreAccessClient LocalClient
		{
			get
			{
				return this.GetClient(null);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000047BC File Offset: 0x000029BC
		public IDxStoreAccessClient GetClient(string target)
		{
			if (string.IsNullOrEmpty(target))
			{
				target = this.groupCfg.Self;
			}
			Tuple<CachedChannelFactory<IDxStoreAccess>, IDxStoreAccessClient> orAdd = this.FactoryByTarget.GetOrAdd(target, delegate(string server)
			{
				CachedChannelFactory<IDxStoreAccess> cachedChannelFactory = null;
				IDxStoreAccessClient item;
				if (this.groupCfg.Settings.IsUseHttpTransportForClientCommunication)
				{
					item = new HttpStoreAccessClient(this.groupCfg.Self, HttpClient.TargetInfo.BuildFromNode(server, this.groupCfg), this.groupCfg.Settings.StoreAccessHttpTimeoutInMSec);
				}
				else
				{
					ServiceEndpoint storeAccessEndpoint = this.groupCfg.GetStoreAccessEndpoint(server, false, false, this.DefaultTimeout);
					cachedChannelFactory = new CachedChannelFactory<IDxStoreAccess>(storeAccessEndpoint);
					item = new WcfStoreAccessClient(cachedChannelFactory, (this.DefaultTimeout != null) ? this.DefaultTimeout.Operation : null);
				}
				return Tuple.Create<CachedChannelFactory<IDxStoreAccess>, IDxStoreAccessClient>(cachedChannelFactory, item);
			});
			return orAdd.Item2;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000047FD File Offset: 0x000029FD
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00004814 File Offset: 0x00002A14
		protected void Dispose(bool disposing)
		{
			lock (this)
			{
				if (!this.isDisposed)
				{
					if (disposing)
					{
						foreach (Tuple<CachedChannelFactory<IDxStoreAccess>, IDxStoreAccessClient> tuple in this.FactoryByTarget.Values)
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

		// Token: 0x0400016E RID: 366
		private readonly InstanceGroupConfig groupCfg;

		// Token: 0x0400016F RID: 367
		private bool isDisposed;
	}
}
