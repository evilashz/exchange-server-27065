using System;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;

namespace Microsoft.Exchange.Transport.RecipientAPI
{
	// Token: 0x02000531 RID: 1329
	internal class IsMemberOfResolverComponent<GroupKeyType> : ITransportComponent
	{
		// Token: 0x06003DF5 RID: 15861 RVA: 0x0010435B File Offset: 0x0010255B
		public IsMemberOfResolverComponent(string componentName, TransportAppConfig.IsMemberOfResolverConfiguration appConfig, IsMemberOfResolverADAdapter<GroupKeyType> adAdapter)
		{
			this.componentName = componentName;
			this.configuration = new TransportIsMemberOfResolverConfig(appConfig);
			this.adAdapter = adAdapter;
		}

		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x06003DF6 RID: 15862 RVA: 0x0010437D File Offset: 0x0010257D
		public IsMemberOfResolver<GroupKeyType> IsMemberOfResolver
		{
			get
			{
				return this.memberOfResolver;
			}
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x00104388 File Offset: 0x00102588
		public void Load()
		{
			IsMemberOfResolverPerformanceCounters perfCounters = new IsMemberOfResolverPerformanceCounters(this.componentName);
			this.memberOfResolver = new IsMemberOfResolver<GroupKeyType>(this.configuration, perfCounters, this.adAdapter);
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x001043B9 File Offset: 0x001025B9
		public void Unload()
		{
			if (this.memberOfResolver == null)
			{
				throw new InvalidOperationException("IsMemberOfResolverComponent is not loaded");
			}
			this.memberOfResolver.Dispose();
			this.memberOfResolver = null;
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x001043E0 File Offset: 0x001025E0
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x001043E3 File Offset: 0x001025E3
		public virtual void ClearCache()
		{
			if (this.memberOfResolver != null)
			{
				this.memberOfResolver.ClearCache();
			}
		}

		// Token: 0x04001F99 RID: 8089
		private string componentName;

		// Token: 0x04001F9A RID: 8090
		private TransportIsMemberOfResolverConfig configuration;

		// Token: 0x04001F9B RID: 8091
		private IsMemberOfResolverADAdapter<GroupKeyType> adAdapter;

		// Token: 0x04001F9C RID: 8092
		private IsMemberOfResolver<GroupKeyType> memberOfResolver;
	}
}
