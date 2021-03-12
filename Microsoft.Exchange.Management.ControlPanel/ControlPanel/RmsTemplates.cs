using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000429 RID: 1065
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class RmsTemplates : DataSourceService, IRmsTemplates, IGetListService<RmsTemplateFilter, RmsTemplate>
	{
		// Token: 0x06003563 RID: 13667 RVA: 0x000A5F10 File Offset: 0x000A4110
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-RMSTemplate@R:Organization")]
		public PowerShellResults<RmsTemplate> GetList(RmsTemplateFilter filter, SortOptions sort)
		{
			return base.GetList<RmsTemplate, RmsTemplateFilter>("Get-RMSTemplate", filter, sort);
		}

		// Token: 0x0400258C RID: 9612
		private const string Noun = "RMSTemplate";

		// Token: 0x0400258D RID: 9613
		internal const string GetCmdlet = "Get-RMSTemplate";

		// Token: 0x0400258E RID: 9614
		internal const string ReadScope = "@R:Organization";

		// Token: 0x0400258F RID: 9615
		internal const string GetListRole = "Get-RMSTemplate@R:Organization";
	}
}
