using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000921 RID: 2337
	internal class GetModernGroupDomainCommand : ServiceCommand<GetModernGroupDomainResponse>
	{
		// Token: 0x060043BF RID: 17343 RVA: 0x000E603A File Offset: 0x000E423A
		public GetModernGroupDomainCommand(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x000E6044 File Offset: 0x000E4244
		protected override GetModernGroupDomainResponse InternalExecute()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(base.CallContext.AccessingPrincipal.MailboxInfo.OrganizationId);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, sessionSettings, 36, "InternalExecute", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\jsonservice\\servicecommands\\GetModernGroupDomainCommand.cs");
			return new GetModernGroupDomainResponse(tenantOrTopologyConfigurationSession.GetDefaultAcceptedDomain().DomainName.Domain);
		}
	}
}
