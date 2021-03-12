using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200041B RID: 1051
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class MailRoutingDomains : DataSourceService, IMailRoutingDomains, IGetListService<MailRoutingDomainFilter, MailRoutingDomain>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>, IEditObjectService<MailRoutingDomain, SetMailRoutingDomain>, IGetObjectService<MailRoutingDomain>
	{
		// Token: 0x06003540 RID: 13632 RVA: 0x000A589B File Offset: 0x000A3A9B
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-AcceptedDomain@C:OrganizationConfig")]
		public PowerShellResults<MailRoutingDomain> GetList(MailRoutingDomainFilter filter, SortOptions sort)
		{
			return base.GetList<MailRoutingDomain, MailRoutingDomainFilter>("Get-AcceptedDomain", filter, sort);
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x000A58AA File Offset: 0x000A3AAA
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-AcceptedDomain?Identity@C:OrganizationConfig")]
		public PowerShellResults RemoveObjects(Identity[] identities, BaseWebServiceParameters wsParameters)
		{
			return base.RemoveObjects("Remove-AcceptedDomain", identities, wsParameters);
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x000A58B9 File Offset: 0x000A3AB9
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-AcceptedDomain?Identity@C:OrganizationConfig+Set-AcceptedDomain?Identity@C:OrganizationConfig")]
		public PowerShellResults<MailRoutingDomain> SetObject(Identity identity, SetMailRoutingDomain properties)
		{
			return base.SetObject<MailRoutingDomain, SetMailRoutingDomain>("Set-AcceptedDomain", identity, properties);
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000A58C8 File Offset: 0x000A3AC8
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-AcceptedDomain?Identity@C:OrganizationConfig")]
		public PowerShellResults<MailRoutingDomain> GetObject(Identity identity)
		{
			return base.GetObject<MailRoutingDomain>("Get-AcceptedDomain", identity);
		}

		// Token: 0x04002573 RID: 9587
		private const string Noun = "AcceptedDomain";

		// Token: 0x04002574 RID: 9588
		internal const string GetCmdlet = "Get-AcceptedDomain";

		// Token: 0x04002575 RID: 9589
		internal const string SetCmdlet = "Set-AcceptedDomain";

		// Token: 0x04002576 RID: 9590
		private const string RemoveCmdlet = "Remove-AcceptedDomain";

		// Token: 0x04002577 RID: 9591
		internal const string ReadScope = "@C:OrganizationConfig";

		// Token: 0x04002578 RID: 9592
		private const string WriteScope = "@C:OrganizationConfig";

		// Token: 0x04002579 RID: 9593
		private const string GetListRole = "Get-AcceptedDomain@C:OrganizationConfig";

		// Token: 0x0400257A RID: 9594
		private const string RemoveObjectsRole = "Remove-AcceptedDomain?Identity@C:OrganizationConfig";

		// Token: 0x0400257B RID: 9595
		private const string SetObjectRole = "Get-AcceptedDomain?Identity@C:OrganizationConfig+Set-AcceptedDomain?Identity@C:OrganizationConfig";

		// Token: 0x0400257C RID: 9596
		private const string GetObjectRole = "Get-AcceptedDomain?Identity@C:OrganizationConfig";
	}
}
