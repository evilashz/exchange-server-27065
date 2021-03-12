using System;
using Microsoft.Exchange.HttpProxy.Routing.Providers;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingLookups
{
	// Token: 0x02000038 RID: 56
	internal class ExternalDirectoryObjectIdRoutingLookup : MailboxRoutingLookupBase<ExternalDirectoryObjectIdRoutingKey>
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00004118 File Offset: 0x00002318
		public ExternalDirectoryObjectIdRoutingLookup(IUserProvider userProvider) : base(userProvider)
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004121 File Offset: 0x00002321
		protected override User FindUser(ExternalDirectoryObjectIdRoutingKey externalDirectoryObjectIdRoutingKey, IRoutingDiagnostics diagnostics)
		{
			return base.UserProvider.FindByExternalDirectoryObjectId(externalDirectoryObjectIdRoutingKey.UserGuid, externalDirectoryObjectIdRoutingKey.TenantGuid, diagnostics);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000413B File Offset: 0x0000233B
		protected override string GetDomainName(ExternalDirectoryObjectIdRoutingKey routingKey)
		{
			return string.Empty;
		}
	}
}
