using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200023A RID: 570
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class OwnedGroups : DistributionGroupServiceBase, IOwnedGroups, IGetListService<OwnedGroupFilter, DistributionGroupRow>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>, IEditObjectForListService<DistributionGroup, SetMyDistributionGroup, DistributionGroupRow>, IGetObjectService<DistributionGroup>, IGetObjectForListService<DistributionGroupRow>
	{
		// Token: 0x060027D8 RID: 10200 RVA: 0x0007D29C File Offset: 0x0007B49C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties@R:MyGAL")]
		public PowerShellResults<DistributionGroupRow> GetList(OwnedGroupFilter filter, SortOptions sort)
		{
			return base.GetList<DistributionGroupRow, OwnedGroupFilter>("Get-Recipient", filter, sort, "DisplayName");
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x0007D2B0 File Offset: 0x0007B4B0
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?Identity@R:MyGAL")]
		public PowerShellResults<DistributionGroupRow> GetObjectForList(Identity identity)
		{
			return base.GetObjectForList<DistributionGroupRow>("Get-Recipient", identity);
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x0007D2BE File Offset: 0x0007B4BE
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-DistributionGroup?Identity@R:MyGAL+Get-Group?Identity@R:MyGAL")]
		public PowerShellResults<DistributionGroup> GetObject(Identity identity)
		{
			return base.GetDistributionGroup<DistributionGroup>(identity);
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x0007D2C7 File Offset: 0x0007B4C7
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-DistributionGroup?Identity@W:MyDistributionGroups")]
		public PowerShellResults RemoveObjects(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.RemoveObjects("Remove-DistributionGroup", identities, parameters);
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x0007D2D6 File Offset: 0x0007B4D6
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-DistributionGroup?Identity@R:MyGAL+Get-Group?Identity@R:MyGAL+Set-DistributionGroup?Identity@W:MyDistributionGroups")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-DistributionGroup?Identity@R:MyGAL+Get-Group?Identity@R:MyGAL+Update-DistributionGroupMember?Identity@W:MyDistributionGroups")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-DistributionGroup?Identity@R:MyGAL+Get-Group?Identity@R:MyGAL+Set-Group?Identity@W:MyDistributionGroups")]
		public PowerShellResults<DistributionGroupRow> SetObject(Identity identity, SetMyDistributionGroup properties)
		{
			if (RbacPrincipal.Current.IsInRole("Set-DistributionGroup?IgnoreNamingPolicy"))
			{
				properties.IgnoreNamingPolicy = true;
			}
			return base.SetDistributionGroup<SetMyDistributionGroup, SetMyGroup, UpdateMyDistributionGroupMember>(identity, properties);
		}

		// Token: 0x04002027 RID: 8231
		internal const string ReadScope = "@R:MyGAL";

		// Token: 0x04002028 RID: 8232
		internal const string WriteScope = "@W:MyDistributionGroups";

		// Token: 0x04002029 RID: 8233
		private const string GetListRole = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties@R:MyGAL";

		// Token: 0x0400202A RID: 8234
		private const string GetObjectForListRole = "Get-Recipient?Identity@R:MyGAL";

		// Token: 0x0400202B RID: 8235
		private const string GetObjectRole = "Get-DistributionGroup?Identity@R:MyGAL+Get-Group?Identity@R:MyGAL";

		// Token: 0x0400202C RID: 8236
		private const string RemoveObjectsRole = "Remove-DistributionGroup?Identity@W:MyDistributionGroups";

		// Token: 0x0400202D RID: 8237
		private const string SetObjectRole_SetGroup = "Get-DistributionGroup?Identity@R:MyGAL+Get-Group?Identity@R:MyGAL+Set-Group?Identity@W:MyDistributionGroups";

		// Token: 0x0400202E RID: 8238
		private const string SetObjectRole_UpdateMember = "Get-DistributionGroup?Identity@R:MyGAL+Get-Group?Identity@R:MyGAL+Update-DistributionGroupMember?Identity@W:MyDistributionGroups";

		// Token: 0x0400202F RID: 8239
		private const string SetObjectRole_SetDistributionGroup = "Get-DistributionGroup?Identity@R:MyGAL+Get-Group?Identity@R:MyGAL+Set-DistributionGroup?Identity@W:MyDistributionGroups";
	}
}
