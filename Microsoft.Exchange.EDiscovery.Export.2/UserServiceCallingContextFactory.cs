using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy;
using Microsoft.Exchange.EDiscovery.Export.EwsProxy;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200004B RID: 75
	internal class UserServiceCallingContextFactory : IServiceCallingContextFactory
	{
		// Token: 0x06000636 RID: 1590 RVA: 0x0001728E File Offset: 0x0001548E
		public UserServiceCallingContextFactory(ICredentialHandler credentialHandler = null)
		{
			this.CredentialHandler = credentialHandler;
			this.cachedCredentials = new Dictionary<string, ICredentials>();
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x000172A8 File Offset: 0x000154A8
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x000172B0 File Offset: 0x000154B0
		public ICredentialHandler CredentialHandler { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x000172B9 File Offset: 0x000154B9
		public IServiceCallingContext<DefaultBinding_Autodiscover> AutoDiscoverCallingContext
		{
			get
			{
				if (this.CredentialHandler == null)
				{
					throw new ArgumentNullException("CredentialHandler");
				}
				if (this.autoDiscoverCallingContext == null)
				{
					this.autoDiscoverCallingContext = new UserAutoDiscoverCallingContext(this.CredentialHandler, this.cachedCredentials);
				}
				return this.autoDiscoverCallingContext;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x000172F3 File Offset: 0x000154F3
		public IServiceCallingContext<ExchangeServiceBinding> EwsCallingContext
		{
			get
			{
				if (this.CredentialHandler == null)
				{
					throw new ArgumentNullException("CredentialHandler");
				}
				if (this.ewsCallingContext == null)
				{
					this.ewsCallingContext = new UserEwsCallingContext(this.CredentialHandler, "MailboxSearch", this.cachedCredentials);
				}
				return this.ewsCallingContext;
			}
		}

		// Token: 0x040001C2 RID: 450
		private readonly Dictionary<string, ICredentials> cachedCredentials;

		// Token: 0x040001C3 RID: 451
		private IServiceCallingContext<DefaultBinding_Autodiscover> autoDiscoverCallingContext;

		// Token: 0x040001C4 RID: 452
		private IServiceCallingContext<ExchangeServiceBinding> ewsCallingContext;
	}
}
