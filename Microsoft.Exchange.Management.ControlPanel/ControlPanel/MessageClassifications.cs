using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000424 RID: 1060
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class MessageClassifications : DataSourceService, IMessageClassifications, IGetListService<MessageClassificationFilter, MessageClassification>
	{
		// Token: 0x06003558 RID: 13656 RVA: 0x000A5A2C File Offset: 0x000A3C2C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MessageClassification@R:Self")]
		public PowerShellResults<MessageClassification> GetList(MessageClassificationFilter filter, SortOptions sort)
		{
			PowerShellResults<MessageClassification> list = base.GetList<MessageClassification, MessageClassificationFilter>("Get-MessageClassification", filter, sort);
			if (list.Output != null && list.Output.Length > 0)
			{
				list.Output = Array.FindAll<MessageClassification>(list.Output, (MessageClassification x) => x.PermissionMenuVisible);
			}
			return list;
		}

		// Token: 0x04002586 RID: 9606
		private const string Noun = "MessageClassification";

		// Token: 0x04002587 RID: 9607
		internal const string GetCmdlet = "Get-MessageClassification";

		// Token: 0x04002588 RID: 9608
		internal const string ReadScope = "@R:Self";

		// Token: 0x04002589 RID: 9609
		internal const string GetListRole = "Get-MessageClassification@R:Self";
	}
}
