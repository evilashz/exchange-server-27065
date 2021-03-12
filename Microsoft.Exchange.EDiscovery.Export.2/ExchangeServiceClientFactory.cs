using System;
using System.Threading;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000043 RID: 67
	internal sealed class ExchangeServiceClientFactory : IServiceClientFactory
	{
		// Token: 0x06000613 RID: 1555 RVA: 0x00016C62 File Offset: 0x00014E62
		public ExchangeServiceClientFactory(IServiceCallingContextFactory serviceCallingContextFactory)
		{
			if (serviceCallingContextFactory == null)
			{
				throw new ArgumentNullException("serviceCallingContextFactory");
			}
			this.serviceCallingContextFactory = serviceCallingContextFactory;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00016C7F File Offset: 0x00014E7F
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x00016C8C File Offset: 0x00014E8C
		public ICredentialHandler CredentialHandler
		{
			get
			{
				return this.serviceCallingContextFactory.CredentialHandler;
			}
			set
			{
				this.serviceCallingContextFactory.CredentialHandler = value;
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00016C9C File Offset: 0x00014E9C
		public IServiceClient<ISourceDataProvider> CreateSourceDataProvider(Uri serviceEndpoint, CancellationToken cancellationToken)
		{
			EwsClient ewsClient = new EwsClient(serviceEndpoint, this.serviceCallingContextFactory.EwsCallingContext, cancellationToken);
			if (ConstantProvider.RebindWithAutoDiscoveryEnabled && ConstantProvider.RebindAutoDiscoveryUrl != null && this.serviceCallingContextFactory.AutoDiscoverCallingContext != null)
			{
				AutoDiscoverClient autoDiscoverClient = (AutoDiscoverClient)this.CreateAutoDiscoverClient(ConstantProvider.RebindAutoDiscoveryUrl, cancellationToken);
				autoDiscoverClient.AutoDiscoverInternalUrlOnly = ConstantProvider.RebindAutoDiscoveryInternalUrlOnly;
				ewsClient.AutoDiscoverInterface = autoDiscoverClient.FunctionalInterface;
			}
			return ewsClient;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00016D07 File Offset: 0x00014F07
		public IServiceClient<IAutoDiscoverClient> CreateAutoDiscoverClient(Uri serviceEndpoint, CancellationToken cancellationToken)
		{
			return new AutoDiscoverClient(serviceEndpoint, this.serviceCallingContextFactory.AutoDiscoverCallingContext, cancellationToken);
		}

		// Token: 0x040001B6 RID: 438
		private readonly IServiceCallingContextFactory serviceCallingContextFactory;
	}
}
