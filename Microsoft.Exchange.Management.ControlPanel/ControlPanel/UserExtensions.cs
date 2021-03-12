using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000471 RID: 1137
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class UserExtensions : DataSourceService, IUserExtensions, IGetListService<UserExtensionsFilter, UMMailboxExtension>
	{
		// Token: 0x06003957 RID: 14679 RVA: 0x000AE698 File Offset: 0x000AC898
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-UMMailbox@R:Self")]
		public PowerShellResults<UMMailboxExtension> GetList(UserExtensionsFilter filter, SortOptions sort)
		{
			PowerShellResults<UMMailboxObject> @object = base.GetObject<UMMailboxObject>("Get-UMMailbox", Identity.FromExecutingUserId());
			if (@object.Output.Length == 1)
			{
				UMMailboxExtension[] array = new UMMailboxExtension[@object.Output[0].CallAnsweringRulesExtensions.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new UMMailboxExtension
					{
						DisplayName = @object.Output[0].CallAnsweringRulesExtensions[i]
					};
				}
				return new PowerShellResults<UMMailboxExtension>
				{
					Output = array
				};
			}
			return new PowerShellResults<UMMailboxExtension>();
		}

		// Token: 0x0400269E RID: 9886
		internal const string ReadScope = "@R:Self";

		// Token: 0x0400269F RID: 9887
		internal const string GetUMMailbox = "Get-UMMailbox";

		// Token: 0x040026A0 RID: 9888
		internal const string GetListRole = "Get-UMMailbox@R:Self";
	}
}
