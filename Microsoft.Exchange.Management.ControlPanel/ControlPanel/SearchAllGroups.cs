using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000240 RID: 576
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class SearchAllGroups : DistributionGroupServiceBase, ISearchAllGroups, IGetListService<SearchAllGroupFilter, GroupRecipientRow>, IGetObjectService<ViewDistributionGroupData>
	{
		// Token: 0x06002832 RID: 10290 RVA: 0x0007D86C File Offset: 0x0007BA6C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties@R:MyGAL")]
		public PowerShellResults<GroupRecipientRow> GetList(SearchAllGroupFilter filter, SortOptions sort)
		{
			return base.GetList<GroupRecipientRow, SearchAllGroupFilter>("Get-Recipient", filter, sort);
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x0007D87B File Offset: 0x0007BA7B
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-DistributionGroup?Identity@R:MyGAL+Get-Group?Identity@R:MyGAL")]
		public PowerShellResults<ViewDistributionGroupData> GetObject(Identity identity)
		{
			return base.GetDistributionGroup<ViewDistributionGroupData>(identity);
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x0007D884 File Offset: 0x0007BA84
		[PrincipalPermission(SecurityAction.Demand, Role = "Add-DistributionGroupMember?Identity@W:MyGAL|MyDistributionGroups")]
		public PowerShellResults JoinGroups(Identity[] identities)
		{
			identities.FaultIfNullOrEmpty();
			Identity groupIdentityForTranslation = DistributionGroupServiceBase.GetGroupIdentityForTranslation(identities);
			PowerShellResults powerShellResults = new PowerShellResults();
			int num = 0;
			int num2 = -1;
			for (int i = 0; i < identities.Length; i++)
			{
				PSCommand psCommand = new PSCommand().AddCommand("Add-DistributionGroupMember").AddParameter("Identity", identities[i]);
				PowerShellResults powerShellResults2 = base.Invoke(psCommand, groupIdentityForTranslation, null);
				if (powerShellResults2.SucceededWithoutWarnings)
				{
					num++;
					if (num == 1)
					{
						num2 = i;
					}
				}
				powerShellResults.MergeErrors(powerShellResults2);
			}
			if (num > 0)
			{
				string text = (num == 1) ? OwaOptionStrings.JoinDlSuccess(identities[num2].DisplayName) : ((num == identities.Length) ? OwaOptionStrings.JoinDlsSuccess(num) : OwaOptionStrings.JoinOtherDlsSuccess(num));
				powerShellResults.Informations = new string[]
				{
					text
				};
			}
			return powerShellResults;
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x0007D94B File Offset: 0x0007BB4B
		[PrincipalPermission(SecurityAction.Demand, Role = "Add-DistributionGroupMember?Identity@W:MyGAL|MyDistributionGroups")]
		public PowerShellResults<ViewDistributionGroupData> JoinGroup(Identity identity)
		{
			return this.JoinOrLeaveGroup(identity, "Add-DistributionGroupMember");
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x0007D959 File Offset: 0x0007BB59
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-DistributionGroupMember?Identity@W:MyGAL|MyDistributionGroups")]
		public PowerShellResults<ViewDistributionGroupData> LeaveGroup(Identity identity)
		{
			return this.JoinOrLeaveGroup(identity, "Remove-DistributionGroupMember");
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x0007D968 File Offset: 0x0007BB68
		private PowerShellResults<ViewDistributionGroupData> JoinOrLeaveGroup(Identity identity, string cmdlet)
		{
			PowerShellResults<ViewDistributionGroupData> powerShellResults = new PowerShellResults<ViewDistributionGroupData>();
			PSCommand psCommand = new PSCommand().AddCommand(cmdlet).AddParameter("Identity", identity);
			powerShellResults.MergeErrors(base.Invoke(psCommand, identity, null));
			return powerShellResults;
		}

		// Token: 0x04002038 RID: 8248
		internal const string ReadScope = "@R:MyGAL";

		// Token: 0x04002039 RID: 8249
		internal const string WriteScope = "@W:MyGAL|MyDistributionGroups";

		// Token: 0x0400203A RID: 8250
		private const string GetListRole = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties@R:MyGAL";

		// Token: 0x0400203B RID: 8251
		private const string GetObjectRole = "Get-DistributionGroup?Identity@R:MyGAL+Get-Group?Identity@R:MyGAL";

		// Token: 0x0400203C RID: 8252
		private const string JoinGroupRole = "Add-DistributionGroupMember?Identity@W:MyGAL|MyDistributionGroups";

		// Token: 0x0400203D RID: 8253
		private const string LeaveGroupRole = "Remove-DistributionGroupMember?Identity@W:MyGAL|MyDistributionGroups";
	}
}
