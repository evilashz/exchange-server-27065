using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000420 RID: 1056
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class MessageCategories : DataSourceService, IMessageCategories, IGetListService<MessageCategoryFilter, MessageCategory>
	{
		// Token: 0x06003550 RID: 13648 RVA: 0x000A59AD File Offset: 0x000A3BAD
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MessageCategory@R:Self")]
		public PowerShellResults<MessageCategory> GetList(MessageCategoryFilter filter, SortOptions sort)
		{
			return base.GetList<MessageCategory, MessageCategoryFilter>("Get-MessageCategory", filter, sort);
		}

		// Token: 0x04002581 RID: 9601
		private const string Noun = "MessageCategory";

		// Token: 0x04002582 RID: 9602
		internal const string GetCmdlet = "Get-MessageCategory";

		// Token: 0x04002583 RID: 9603
		internal const string ReadScope = "@R:Self";

		// Token: 0x04002584 RID: 9604
		private const string GetListRole = "Get-MessageCategory@R:Self";
	}
}
