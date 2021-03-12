using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000341 RID: 833
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class OptionalRPTsPicker : DataSourceService, IOptionalRPTsPicker, IGetListService<AllRPTsFilter, OptionalRetentionPolicyTagRow>
	{
		// Token: 0x06002F49 RID: 12105 RVA: 0x00090428 File Offset: 0x0008E628
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-RetentionPolicyTag?Types@C:OrganizationConfig")]
		public PowerShellResults<OptionalRetentionPolicyTagRow> GetList(AllRPTsFilter filter, SortOptions sort)
		{
			PowerShellResults<OptionalRetentionPolicyTagRow> powerShellResults = new PowerShellResults<OptionalRetentionPolicyTagRow>();
			PowerShellResults<OptionalRetentionPolicyTagRow> list = base.GetList<OptionalRetentionPolicyTagRow, AllRPTsFilter>("Get-RetentionPolicyTag", filter, sort, "DisplayName");
			PowerShellResults<OptionalRetentionPolicyTagRow> powerShellResults2 = list.MergeErrors<OptionalRetentionPolicyTagRow>(base.GetList<OptionalRetentionPolicyTagRow, AllAssociatedRPTsFilter>("Get-RetentionPolicyTag", new AllAssociatedRPTsFilter(), null));
			if (list.Succeeded && powerShellResults2.Succeeded)
			{
				powerShellResults.Output = list.Output.Except(powerShellResults2.Output).ToArray<OptionalRetentionPolicyTagRow>();
				if (RbacPrincipal.Current.ExecutingUserId != null)
				{
					Accounts accounts = new Accounts();
					if (!RetentionUtils.UserHasArchive(accounts.GetRecipientObject(null)))
					{
						List<OptionalRetentionPolicyTagRow> list2 = new List<OptionalRetentionPolicyTagRow>();
						foreach (OptionalRetentionPolicyTagRow optionalRetentionPolicyTagRow in powerShellResults.Output)
						{
							if (optionalRetentionPolicyTagRow.RetentionPolicyTag.RetentionAction != RetentionActionType.MoveToArchive)
							{
								list2.Add(optionalRetentionPolicyTagRow);
							}
						}
						powerShellResults.Output = list2.ToArray();
					}
				}
				if (sort != null)
				{
					sort.PropertyName = RetentionPolicyTagBaseRow.GetSortProperty(sort.PropertyName);
					Func<OptionalRetentionPolicyTagRow[], OptionalRetentionPolicyTagRow[]> sortFunction = sort.GetSortFunction<OptionalRetentionPolicyTagRow>();
					if (sortFunction != null)
					{
						powerShellResults.Output = sortFunction(powerShellResults.Output);
					}
				}
			}
			return powerShellResults;
		}

		// Token: 0x04002300 RID: 8960
		private const string GetListRole = "Get-RetentionPolicyTag?Types@C:OrganizationConfig";
	}
}
