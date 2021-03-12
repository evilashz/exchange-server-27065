using System;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000365 RID: 869
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class UMDialingRuleGroupPicker : DataSourceService, IUMDialingRuleGroupPicker, IGetListService<UMDialPlanFilterWithIdentity, UMDialingRuleGroupRow>
	{
		// Token: 0x06002FE5 RID: 12261 RVA: 0x00091D9C File Offset: 0x0008FF9C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMDialPlan?Identity@R:Organization")]
		public PowerShellResults<UMDialingRuleGroupRow> GetList(UMDialPlanFilterWithIdentity filter, SortOptions sort)
		{
			PowerShellResults<UMDialingRuleGroupRow> powerShellResults = new PowerShellResults<UMDialingRuleGroupRow>();
			filter.FaultIfNull();
			filter.DialPlanIdentity.FaultIfNull();
			PowerShellResults<UMDialPlanObjectWithGroupList> @object = base.GetObject<UMDialPlanObjectWithGroupList>("Get-UMDialPlan", filter.DialPlanIdentity);
			powerShellResults.MergeErrors<UMDialPlanObjectWithGroupList>(@object);
			if (@object.SucceededWithValue)
			{
				if (filter.IsInternational)
				{
					powerShellResults.Output = (from dialGroupName in @object.Value.ConfiguredInternationalGroupNameList
					select new UMDialingRuleGroupRow(dialGroupName)).ToArray<UMDialingRuleGroupRow>();
				}
				else
				{
					powerShellResults.Output = (from dialGroupName in @object.Value.ConfiguredInCountryOrRegionGroupNameList
					select new UMDialingRuleGroupRow(dialGroupName)).ToArray<UMDialingRuleGroupRow>();
				}
			}
			return powerShellResults;
		}

		// Token: 0x04002323 RID: 8995
		private const string GetUMDialPlanRole = "Get-UMDialPlan?Identity@R:Organization";
	}
}
