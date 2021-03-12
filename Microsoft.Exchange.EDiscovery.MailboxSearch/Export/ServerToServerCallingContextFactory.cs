using System;
using System.Collections.Generic;
using Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200000D RID: 13
	internal class ServerToServerCallingContextFactory : IServiceCallingContextFactory
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00006323 File Offset: 0x00004523
		public ServerToServerCallingContextFactory(IDictionary<Uri, string> remoteUrls)
		{
			this.remoteUrls = (remoteUrls ?? new Dictionary<Uri, string>());
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000633B File Offset: 0x0000453B
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000633E File Offset: 0x0000453E
		public ICredentialHandler CredentialHandler
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotSupportedException("ServerToServerCallingContextFactory shouldn't need the credential handler.");
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000634A File Offset: 0x0000454A
		public IServiceCallingContext<DefaultBinding_Autodiscover> AutoDiscoverCallingContext
		{
			get
			{
				if (this.autoDiscoveryCallingContext == null)
				{
					this.autoDiscoveryCallingContext = new ServerToServerAutoDiscoveryCallingContext(this.remoteUrls);
				}
				return this.autoDiscoveryCallingContext;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000636B File Offset: 0x0000456B
		public IServiceCallingContext<ExchangeServiceBinding> EwsCallingContext
		{
			get
			{
				if (this.ewsCallingContext == null)
				{
					this.ewsCallingContext = new ServerToServerEwsCallingContext(this.remoteUrls);
				}
				return this.ewsCallingContext;
			}
		}

		// Token: 0x04000060 RID: 96
		private readonly IDictionary<Uri, string> remoteUrls;

		// Token: 0x04000061 RID: 97
		private IServiceCallingContext<ExchangeServiceBinding> ewsCallingContext;

		// Token: 0x04000062 RID: 98
		private IServiceCallingContext<DefaultBinding_Autodiscover> autoDiscoveryCallingContext;
	}
}
