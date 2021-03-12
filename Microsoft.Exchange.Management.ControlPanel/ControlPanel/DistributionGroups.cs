using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004F3 RID: 1267
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class DistributionGroups : DistributionGroupServiceBase, IDistributionGroups, IDataSourceService<DistributionGroupFilter, DistributionGroupRow, DistributionGroup, SetDistributionGroup, NewDistributionGroupParameters>, IDataSourceService<DistributionGroupFilter, DistributionGroupRow, DistributionGroup, SetDistributionGroup, NewDistributionGroupParameters, BaseWebServiceParameters>, IEditListService<DistributionGroupFilter, DistributionGroupRow, DistributionGroup, NewDistributionGroupParameters, BaseWebServiceParameters>, IGetListService<DistributionGroupFilter, DistributionGroupRow>, INewObjectService<DistributionGroupRow, NewDistributionGroupParameters>, IRemoveObjectsService<BaseWebServiceParameters>, IEditObjectForListService<DistributionGroup, SetDistributionGroup, DistributionGroupRow>, IGetObjectService<DistributionGroup>, IGetObjectForListService<DistributionGroupRow>
	{
		// Token: 0x06003D57 RID: 15703 RVA: 0x000B865F File Offset: 0x000B685F
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties@R:Organization")]
		public PowerShellResults<DistributionGroupRow> GetList(DistributionGroupFilter filter, SortOptions sort)
		{
			return base.GetList<DistributionGroupRow, DistributionGroupFilter>("Get-Recipient", filter, sort);
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x000B866E File Offset: 0x000B686E
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-Recipient?Identity@R:Organization")]
		public PowerShellResults<DistributionGroupRow> GetObjectForList(Identity identity)
		{
			return base.GetObjectForList<DistributionGroupRow>("Get-Recipient", identity);
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x000B867C File Offset: 0x000B687C
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-DistributionGroup?Identity@R:Organization+Get-Group?Identity@R:Organization")]
		public PowerShellResults<DistributionGroup> GetObject(Identity identity)
		{
			return base.GetDistributionGroup<DistributionGroup>(identity);
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x000B8688 File Offset: 0x000B6888
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+New-DistributionGroup?Name&Alias&PrimarySmtpAddress@W:MyDistributionGroups|Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Enterprise+New-DistributionGroup?Name&Alias@W:MyDistributionGroups|Organization")]
		public PowerShellResults<DistributionGroupRow> NewObject(NewDistributionGroupParameters properties)
		{
			if (RbacPrincipal.Current.IsInRole("New-DistributionGroup?IgnoreNamingPolicy"))
			{
				properties.IgnoreNamingPolicy = true;
			}
			PowerShellResults<DistributionGroupRow> powerShellResults = base.NewObject<DistributionGroupRow, NewDistributionGroupParameters>("New-DistributionGroup", properties);
			if (powerShellResults.SucceededWithValue && string.Compare(powerShellResults.Value.DisplayName, properties.Name) != 0)
			{
				string text = Strings.GroupNameWithNamingPolciy(powerShellResults.Value.DisplayName);
				powerShellResults.Informations = new string[]
				{
					text
				};
			}
			return powerShellResults;
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x000B8705 File Offset: 0x000B6905
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-DistributionGroup?Identity@W:Organization")]
		public PowerShellResults RemoveObjects(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.RemoveObjects("Remove-DistributionGroup", identities, parameters);
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x000B8714 File Offset: 0x000B6914
		[PrincipalPermission(SecurityAction.Demand, Role = "MultiTenant+Get-DistributionGroup?Identity@R:Organization+Get-Group?Identity@R:Organization+MultiTenant+Add-RecipientPermission?Identity@W:Organization+Remove-RecipientPermission?Identity@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Enterprise+Get-DistributionGroup?Identity@R:Organization+Get-Group?Identity@R:Organization+Enterprise+Get-ADPermission?Identity@R:Organization+Add-ADPermission?Identity@W:Organization+Remove-ADPermission?Identity@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-DistributionGroup?Identity@R:Organization+Get-Group?Identity@R:Organization+Set-DistributionGroup?Identity@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-DistributionGroup?Identity@R:Organization+Get-Group?Identity@R:Organization+Update-DistributionGroupMember?Identity@W:Organization")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-DistributionGroup?Identity@R:Organization+Get-Group?Identity@R:Organization+Set-Group?Identity@W:Organization")]
		public PowerShellResults<DistributionGroupRow> SetObject(Identity identity, SetDistributionGroup properties)
		{
			if (RbacPrincipal.Current.IsInRole("Set-DistributionGroup?IgnoreNamingPolicy"))
			{
				properties.IgnoreNamingPolicy = true;
			}
			return base.SetDistributionGroup<SetDistributionGroup, SetGroup, UpdateDistributionGroupMember>(identity, properties);
		}

		// Token: 0x040027F0 RID: 10224
		internal const string ReadScope = "@R:Organization";

		// Token: 0x040027F1 RID: 10225
		internal const string WriteScope = "@W:Organization";

		// Token: 0x040027F2 RID: 10226
		private const string GetListRole = "Get-Recipient?ResultSize&Filter&RecipientTypeDetails&Properties@R:Organization";

		// Token: 0x040027F3 RID: 10227
		private const string GetObjectForListRole = "Get-Recipient?Identity@R:Organization";

		// Token: 0x040027F4 RID: 10228
		private const string GetObjectRole = "Get-DistributionGroup?Identity@R:Organization+Get-Group?Identity@R:Organization";

		// Token: 0x040027F5 RID: 10229
		internal const string NewDGScope = "@W:MyDistributionGroups|Organization";

		// Token: 0x040027F6 RID: 10230
		private const string NewObjectRole_Enterprise = "Enterprise+New-DistributionGroup?Name&Alias@W:MyDistributionGroups|Organization";

		// Token: 0x040027F7 RID: 10231
		private const string NewObjectRole_MultiTenant = "MultiTenant+New-DistributionGroup?Name&Alias&PrimarySmtpAddress@W:MyDistributionGroups|Organization";

		// Token: 0x040027F8 RID: 10232
		private const string RemoveObjectsRole = "Remove-DistributionGroup?Identity@W:Organization";

		// Token: 0x040027F9 RID: 10233
		private const string SetObjectRole_SetGroup = "Get-DistributionGroup?Identity@R:Organization+Get-Group?Identity@R:Organization+Set-Group?Identity@W:Organization";

		// Token: 0x040027FA RID: 10234
		private const string SetObjectRole_UpdateMember = "Get-DistributionGroup?Identity@R:Organization+Get-Group?Identity@R:Organization+Update-DistributionGroupMember?Identity@W:Organization";

		// Token: 0x040027FB RID: 10235
		private const string SetObjectRole_SetDistributionGroup = "Get-DistributionGroup?Identity@R:Organization+Get-Group?Identity@R:Organization+Set-DistributionGroup?Identity@W:Organization";

		// Token: 0x040027FC RID: 10236
		private const string SetObjectRole_DelegatePermissionEnt = "Enterprise+Get-DistributionGroup?Identity@R:Organization+Get-Group?Identity@R:Organization+Enterprise+Get-ADPermission?Identity@R:Organization+Add-ADPermission?Identity@W:Organization+Remove-ADPermission?Identity@W:Organization";

		// Token: 0x040027FD RID: 10237
		private const string SetObjectRole_DelegatePermissionDC = "MultiTenant+Get-DistributionGroup?Identity@R:Organization+Get-Group?Identity@R:Organization+MultiTenant+Add-RecipientPermission?Identity@W:Organization+Remove-RecipientPermission?Identity@W:Organization";

		// Token: 0x040027FE RID: 10238
		internal const string ConfigureDelegateEnt = "Enterprise+Get-ADPermission?Identity@R:Organization+Add-ADPermission?Identity@W:Organization+Remove-ADPermission?Identity@W:Organization";

		// Token: 0x040027FF RID: 10239
		internal const string ConfigureDelegateDC = "MultiTenant+Add-RecipientPermission?Identity@W:Organization+Remove-RecipientPermission?Identity@W:Organization";
	}
}
