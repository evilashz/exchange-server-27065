using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000232 RID: 562
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class MemberOfGroups : DataSourceService, IMemberOfGroups, IGetListService<MemberOfGroupFilter, RecipientRow>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>
	{
		// Token: 0x060027C8 RID: 10184 RVA: 0x0007D149 File Offset: 0x0007B349
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties@R:MyGAL")]
		public PowerShellResults<RecipientRow> GetList(MemberOfGroupFilter filter, SortOptions sort)
		{
			return base.GetList<RecipientRow, MemberOfGroupFilter>("Get-Recipient", filter, sort, "DisplayName");
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x0007D160 File Offset: 0x0007B360
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-DistributionGroupMember?Identity@W:MyGAL")]
		public PowerShellResults RemoveObjects(Identity[] identities, BaseWebServiceParameters parameters)
		{
			identities.FaultIfNullOrEmpty();
			Identity groupIdentityForTranslation = DistributionGroupServiceBase.GetGroupIdentityForTranslation(identities);
			PowerShellResults powerShellResults = new PowerShellResults();
			for (int i = 0; i < identities.Length; i++)
			{
				PSCommand psCommand = new PSCommand().AddCommand("Remove-DistributionGroupMember").AddParameter("Identity", identities[i]);
				PowerShellResults results = base.Invoke(psCommand, groupIdentityForTranslation, parameters);
				powerShellResults.MergeErrors(results);
			}
			return powerShellResults;
		}

		// Token: 0x04002023 RID: 8227
		internal const string ReadScope = "@R:MyGAL";

		// Token: 0x04002024 RID: 8228
		internal const string WriteScope = "@W:MyGAL";

		// Token: 0x04002025 RID: 8229
		private const string GetListRole = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties@R:MyGAL";

		// Token: 0x04002026 RID: 8230
		private const string RemoveObjectsRole = "Remove-DistributionGroupMember?Identity@W:MyGAL";
	}
}
