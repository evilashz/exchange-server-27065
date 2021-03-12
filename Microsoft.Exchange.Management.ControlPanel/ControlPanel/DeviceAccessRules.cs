using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200030B RID: 779
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class DeviceAccessRules : DataSourceService, IDeviceAccessRules, IDataSourceService<DeviceAccessRuleFilter, DeviceAccessRuleRow, DeviceAccessRule, SetDeviceAccessRule, NewDeviceAccessRule>, IDataSourceService<DeviceAccessRuleFilter, DeviceAccessRuleRow, DeviceAccessRule, SetDeviceAccessRule, NewDeviceAccessRule, BaseWebServiceParameters>, IEditListService<DeviceAccessRuleFilter, DeviceAccessRuleRow, DeviceAccessRule, NewDeviceAccessRule, BaseWebServiceParameters>, IGetListService<DeviceAccessRuleFilter, DeviceAccessRuleRow>, IRemoveObjectsService<BaseWebServiceParameters>, IEditObjectForListService<DeviceAccessRule, SetDeviceAccessRule, DeviceAccessRuleRow>, IGetObjectService<DeviceAccessRule>, IGetObjectForListService<DeviceAccessRuleRow>, INewGetObjectService<DeviceAccessRuleRow, NewDeviceAccessRule, NewDeviceAccessRuleData>, INewObjectService<DeviceAccessRuleRow, NewDeviceAccessRule>
	{
		// Token: 0x06002E5A RID: 11866 RVA: 0x0008CAA8 File Offset: 0x0008ACA8
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-ActiveSyncDeviceAccessRule@C:OrganizationConfig")]
		public PowerShellResults<DeviceAccessRuleRow> GetList(DeviceAccessRuleFilter filter, SortOptions sort)
		{
			return base.GetList<DeviceAccessRuleRow, DeviceAccessRuleFilter>("Get-ActiveSyncDeviceAccessRule", filter, sort);
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x0008CAB7 File Offset: 0x0008ACB7
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-ActiveSyncDeviceAccessRule?Identity@C:OrganizationConfig")]
		public PowerShellResults<DeviceAccessRule> GetObject(Identity identity)
		{
			return base.GetObject<DeviceAccessRule>("Get-ActiveSyncDeviceAccessRule", identity);
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x0008CAC5 File Offset: 0x0008ACC5
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-ActiveSyncDeviceAccessRule?Identity@C:OrganizationConfig")]
		public PowerShellResults<DeviceAccessRuleRow> GetObjectForList(Identity identity)
		{
			return base.GetObjectForList<DeviceAccessRuleRow>("Get-ActiveSyncDeviceAccessRule", identity);
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x0008CAD3 File Offset: 0x0008ACD3
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MobileDevice?Identity@R:Organization")]
		public PowerShellResults<NewDeviceAccessRuleData> GetObjectForNew(Identity identity)
		{
			if (identity == null)
			{
				return null;
			}
			return base.GetObject<NewDeviceAccessRuleData>("Get-MobileDevice", identity);
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x0008CAEC File Offset: 0x0008ACEC
		[PrincipalPermission(SecurityAction.Demand, Role = "New-ActiveSyncDeviceAccessRule@C:OrganizationConfig")]
		public PowerShellResults<DeviceAccessRuleRow> NewObject(NewDeviceAccessRule properties)
		{
			if (properties.DeviceTypeQueryString == null)
			{
				throw new FaultException(Strings.DeviceTypeRequired);
			}
			if (properties.DeviceModelQueryString == null)
			{
				throw new FaultException(Strings.DeviceModelRequired);
			}
			PSCommand pscommand = new PSCommand().AddCommand("New-ActiveSyncDeviceAccessRule").AddParameters(properties);
			if (!properties.DeviceTypeQueryString.IsWildcard && properties.DeviceModelQueryString.IsWildcard)
			{
				pscommand.AddParameter(ActiveSyncDeviceAccessRuleSchema.Characteristic.Name, DeviceAccessCharacteristic.DeviceType);
				pscommand.AddParameter(ActiveSyncDeviceAccessRuleSchema.QueryString.Name, properties.DeviceTypeQueryString.QueryString);
			}
			else
			{
				if (!properties.DeviceTypeQueryString.IsWildcard || properties.DeviceModelQueryString.IsWildcard)
				{
					throw new FaultException(Strings.InvalidDeviceAccessCharacteristic);
				}
				pscommand.AddParameter(ActiveSyncDeviceAccessRuleSchema.Characteristic.Name, DeviceAccessCharacteristic.DeviceModel);
				pscommand.AddParameter(ActiveSyncDeviceAccessRuleSchema.QueryString.Name, properties.DeviceModelQueryString.QueryString);
			}
			return base.Invoke<DeviceAccessRuleRow>(pscommand);
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x0008CBF4 File Offset: 0x0008ADF4
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-ActiveSyncDeviceAccessRule?Identity@C:OrganizationConfig")]
		public PowerShellResults RemoveObjects(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.RemoveObjects("Remove-ActiveSyncDeviceAccessRule", identities, parameters);
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x0008CC03 File Offset: 0x0008AE03
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-ActiveSyncDeviceAccessRule?Identity@C:OrganizationConfig+Set-ActiveSyncDeviceAccessRule?Identity@C:OrganizationConfig")]
		public PowerShellResults<DeviceAccessRuleRow> SetObject(Identity identity, SetDeviceAccessRule properties)
		{
			return base.SetObject<DeviceAccessRule, SetDeviceAccessRule, DeviceAccessRuleRow>("Set-ActiveSyncDeviceAccessRule", identity, properties);
		}

		// Token: 0x04002288 RID: 8840
		private const string Noun = "ActiveSyncDeviceAccessRule";

		// Token: 0x04002289 RID: 8841
		internal const string GetCmdlet = "Get-ActiveSyncDeviceAccessRule";

		// Token: 0x0400228A RID: 8842
		internal const string NewCmdlet = "New-ActiveSyncDeviceAccessRule";

		// Token: 0x0400228B RID: 8843
		internal const string RemoveCmdlet = "Remove-ActiveSyncDeviceAccessRule";

		// Token: 0x0400228C RID: 8844
		internal const string SetCmdlet = "Set-ActiveSyncDeviceAccessRule";

		// Token: 0x0400228D RID: 8845
		internal const string ReadScope = "@C:OrganizationConfig";

		// Token: 0x0400228E RID: 8846
		internal const string WriteScope = "@C:OrganizationConfig";

		// Token: 0x0400228F RID: 8847
		internal const string GetDeviceCmdlet = "Get-MobileDevice";

		// Token: 0x04002290 RID: 8848
		internal const string DeviceReadScope = "@R:Organization";

		// Token: 0x04002291 RID: 8849
		private const string GetListRole = "Get-ActiveSyncDeviceAccessRule@C:OrganizationConfig";

		// Token: 0x04002292 RID: 8850
		private const string GetObjectRole = "Get-ActiveSyncDeviceAccessRule?Identity@C:OrganizationConfig";

		// Token: 0x04002293 RID: 8851
		private const string GetObjectForNewRole = "Get-MobileDevice?Identity@R:Organization";

		// Token: 0x04002294 RID: 8852
		private const string NewObjectRole = "New-ActiveSyncDeviceAccessRule@C:OrganizationConfig";

		// Token: 0x04002295 RID: 8853
		private const string RemoveObjectsRole = "Remove-ActiveSyncDeviceAccessRule?Identity@C:OrganizationConfig";

		// Token: 0x04002296 RID: 8854
		private const string SetObjectRole = "Get-ActiveSyncDeviceAccessRule?Identity@C:OrganizationConfig+Set-ActiveSyncDeviceAccessRule?Identity@C:OrganizationConfig";
	}
}
