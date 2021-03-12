using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000455 RID: 1109
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class TransportConfigs : DataSourceService, ITransportConfigs, IGetListService<TransportConfigFilter, SupervisionTag>
	{
		// Token: 0x0600364C RID: 13900 RVA: 0x000A7CC0 File Offset: 0x000A5EC0
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-TransportConfig@C:OrganizationConfig")]
		public PowerShellResults<SupervisionTag> GetList(TransportConfigFilter filter, SortOptions sort)
		{
			PowerShellResults<SupervisionTag> powerShellResults = new PowerShellResults<SupervisionTag>();
			PowerShellResults<TransportConfigContainer> powerShellResults2 = powerShellResults.MergeErrors<TransportConfigContainer>(base.GetList<TransportConfigContainer, TransportConfigFilter>("Get-TransportConfig", filter, sort));
			if (powerShellResults2.HasValue)
			{
				string[] array = powerShellResults2.Value.SupervisionTags.ToArray();
				if (array != null && array.Length > 0)
				{
					SupervisionTag[] array2 = new SupervisionTag[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = new SupervisionTag(array[i]);
					}
					powerShellResults.Output = array2;
				}
			}
			return powerShellResults;
		}

		// Token: 0x040025DA RID: 9690
		private const string Noun = "TransportConfig";

		// Token: 0x040025DB RID: 9691
		internal const string GetCmdlet = "Get-TransportConfig";

		// Token: 0x040025DC RID: 9692
		internal const string ReadScope = "@C:OrganizationConfig";

		// Token: 0x040025DD RID: 9693
		internal const string GetListRole = "Get-TransportConfig@C:OrganizationConfig";
	}
}
