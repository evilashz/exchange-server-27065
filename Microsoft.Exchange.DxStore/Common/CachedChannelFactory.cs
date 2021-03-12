using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200004D RID: 77
	public class CachedChannelFactory<T> : IDisposable
	{
		// Token: 0x06000283 RID: 643 RVA: 0x000048AC File Offset: 0x00002AAC
		public CachedChannelFactory(ServiceEndpoint endpoint)
		{
			this.endpoint = endpoint;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000284 RID: 644 RVA: 0x000048C8 File Offset: 0x00002AC8
		public ChannelFactory<T> Factory
		{
			get
			{
				ChannelFactory<T> channelFactory = null;
				ChannelFactory<T> result;
				try
				{
					lock (this.gate)
					{
						if (this.factory != null && this.factory.State == CommunicationState.Faulted)
						{
							channelFactory = this.factory;
							this.factory = null;
						}
						result = (this.factory ?? new ChannelFactory<T>(this.endpoint));
					}
				}
				finally
				{
					Utils.AbortBestEffort<T>(channelFactory);
					Utils.DisposeBestEffort(channelFactory);
				}
				return result;
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00004958 File Offset: 0x00002B58
		public void Dispose()
		{
			ChannelFactory<T> disposable;
			lock (this.gate)
			{
				disposable = this.factory;
				this.factory = null;
			}
			Utils.DisposeBestEffort(disposable);
		}

		// Token: 0x04000172 RID: 370
		private readonly object gate = new object();

		// Token: 0x04000173 RID: 371
		private readonly ServiceEndpoint endpoint;

		// Token: 0x04000174 RID: 372
		private ChannelFactory<T> factory;
	}
}
