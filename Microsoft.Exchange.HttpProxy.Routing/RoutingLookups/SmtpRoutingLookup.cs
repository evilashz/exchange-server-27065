using System;
using Microsoft.Exchange.HttpProxy.Routing.Providers;
using Microsoft.Exchange.HttpProxy.Routing.RoutingKeys;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingLookups
{
	// Token: 0x0200003F RID: 63
	internal class SmtpRoutingLookup : MailboxRoutingLookupBase<SmtpRoutingKey>
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00004318 File Offset: 0x00002518
		public SmtpRoutingLookup(IUserProvider userProvider) : base(userProvider)
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004321 File Offset: 0x00002521
		protected override User FindUser(SmtpRoutingKey smtpRoutingKey, IRoutingDiagnostics diagnostics)
		{
			return base.UserProvider.FindBySmtpAddress(smtpRoutingKey.SmtpAddress, diagnostics);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004338 File Offset: 0x00002538
		protected override string GetDomainName(SmtpRoutingKey routingKey)
		{
			return routingKey.SmtpAddress.Domain;
		}
	}
}
