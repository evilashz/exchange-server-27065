using System;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002FD RID: 765
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class ActiveSyncPolicies : DataSourceService, IActiveSyncPolicies, IDataSourceService<ActiveSyncMailboxPolicyFilter, ActiveSyncMailboxPolicyRow, ActiveSyncMailboxPolicyObject, SetActiveSyncMailboxPolicyParams, NewActiveSyncMailboxPolicyParams>, IDataSourceService<ActiveSyncMailboxPolicyFilter, ActiveSyncMailboxPolicyRow, ActiveSyncMailboxPolicyObject, SetActiveSyncMailboxPolicyParams, NewActiveSyncMailboxPolicyParams, BaseWebServiceParameters>, IEditListService<ActiveSyncMailboxPolicyFilter, ActiveSyncMailboxPolicyRow, ActiveSyncMailboxPolicyObject, NewActiveSyncMailboxPolicyParams, BaseWebServiceParameters>, IGetListService<ActiveSyncMailboxPolicyFilter, ActiveSyncMailboxPolicyRow>, INewObjectService<ActiveSyncMailboxPolicyRow, NewActiveSyncMailboxPolicyParams>, IRemoveObjectsService<BaseWebServiceParameters>, IEditObjectForListService<ActiveSyncMailboxPolicyObject, SetActiveSyncMailboxPolicyParams, ActiveSyncMailboxPolicyRow>, IGetObjectService<ActiveSyncMailboxPolicyObject>, IGetObjectForListService<ActiveSyncMailboxPolicyRow>
	{
		// Token: 0x06002E0D RID: 11789 RVA: 0x0008C3B5 File Offset: 0x0008A5B5
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MobileMailboxPolicy?Identity@R:Organization")]
		public PowerShellResults<ActiveSyncMailboxPolicyObject> GetObject(Identity identity)
		{
			return base.GetObject<ActiveSyncMailboxPolicyObject>("Get-MobileMailboxPolicy", identity);
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x0008C3C3 File Offset: 0x0008A5C3
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MobileMailboxPolicy@R:Organization")]
		public PowerShellResults<ActiveSyncMailboxPolicyRow> GetList(ActiveSyncMailboxPolicyFilter filter, SortOptions sort)
		{
			return base.GetList<ActiveSyncMailboxPolicyRow, ActiveSyncMailboxPolicyFilter>("Get-MobileMailboxPolicy", filter, sort);
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x0008C3D2 File Offset: 0x0008A5D2
		[PrincipalPermission(SecurityAction.Demand, Role = "New-MobileMailboxPolicy@W:Organization")]
		public PowerShellResults<ActiveSyncMailboxPolicyRow> NewObject(NewActiveSyncMailboxPolicyParams properties)
		{
			return base.NewObject<ActiveSyncMailboxPolicyRow, NewActiveSyncMailboxPolicyParams>("New-MobileMailboxPolicy", properties);
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x0008C3E0 File Offset: 0x0008A5E0
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-MobileMailboxPolicy?Identity@W:Organization")]
		public PowerShellResults RemoveObjects(Identity[] identities, BaseWebServiceParameters parameters)
		{
			return base.RemoveObjects("Remove-MobileMailboxPolicy", identities, parameters);
		}

		// Token: 0x06002E11 RID: 11793 RVA: 0x0008C3F0 File Offset: 0x0008A5F0
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MobileMailboxPolicy?Identity@R:Organization+Set-MobileMailboxPolicy?Identity@W:Organization")]
		public PowerShellResults<ActiveSyncMailboxPolicyRow> SetObject(Identity identity, SetActiveSyncMailboxPolicyParams properties)
		{
			PowerShellResults<ActiveSyncMailboxPolicyObject> @object = this.GetObject(identity);
			if (@object.Failed)
			{
				PowerShellResults<ActiveSyncMailboxPolicyRow> powerShellResults = new PowerShellResults<ActiveSyncMailboxPolicyRow>();
				powerShellResults.MergeErrors<ActiveSyncMailboxPolicyObject>(@object);
				return powerShellResults;
			}
			properties.FaultIfNull();
			properties.ProcessPolicyParams(@object.Value);
			if (properties.Name != null)
			{
				return base.SetObject<ActiveSyncMailboxPolicyObject, SetActiveSyncMailboxPolicyParams, ActiveSyncMailboxPolicyRow>("Set-MobileMailboxPolicy", identity, properties, new Identity(properties.Name, properties.Name));
			}
			return base.SetObject<ActiveSyncMailboxPolicyObject, SetActiveSyncMailboxPolicyParams, ActiveSyncMailboxPolicyRow>("Set-MobileMailboxPolicy", identity, properties);
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x0008C463 File Offset: 0x0008A663
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MobileMailboxPolicy?Identity@R:Organization")]
		public PowerShellResults<ActiveSyncMailboxPolicyRow> GetObjectForList(Identity identity)
		{
			return base.GetObjectForList<ActiveSyncMailboxPolicyRow>("Get-MobileMailboxPolicy", identity);
		}

		// Token: 0x04002270 RID: 8816
		internal const string GetCmdlet = "Get-MobileMailboxPolicy";

		// Token: 0x04002271 RID: 8817
		internal const string SetCmdlet = "Set-MobileMailboxPolicy";

		// Token: 0x04002272 RID: 8818
		internal const string NewCmdlet = "New-MobileMailboxPolicy";

		// Token: 0x04002273 RID: 8819
		internal const string RemoveCmdlet = "Remove-MobileMailboxPolicy";

		// Token: 0x04002274 RID: 8820
		private const string GetListRole = "Get-MobileMailboxPolicy@R:Organization";

		// Token: 0x04002275 RID: 8821
		private const string GetObjectRole = "Get-MobileMailboxPolicy?Identity@R:Organization";

		// Token: 0x04002276 RID: 8822
		private const string SetObjectRole = "Get-MobileMailboxPolicy?Identity@R:Organization+Set-MobileMailboxPolicy?Identity@W:Organization";

		// Token: 0x04002277 RID: 8823
		private const string NewObjectRole = "New-MobileMailboxPolicy@W:Organization";

		// Token: 0x04002278 RID: 8824
		private const string RemoveObjectRole = "Remove-MobileMailboxPolicy?Identity@W:Organization";
	}
}
