using System;
using System.Management.Automation;
using System.Security.Permissions;
using System.ServiceModel.Activation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000087 RID: 135
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public sealed class MailboxFolderSharings : DataSourceService, IMailboxFolderSharings, IGetListService<MailboxFolderPermissionFilter, MailboxFolderPermissionRow>, IEditObjectService<UserMailboxFolderPermission, SetUserMailboxFolderPermission>, IGetObjectService<UserMailboxFolderPermission>, IRemoveObjectsService, IRemoveObjectsService<BaseWebServiceParameters>
	{
		// Token: 0x06001B8E RID: 7054 RVA: 0x000573A4 File Offset: 0x000555A4
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxFolderPermission?Identity@R:Self")]
		public PowerShellResults<MailboxFolderPermissionRow> GetList(MailboxFolderPermissionFilter filter, SortOptions sort)
		{
			Identity originalFolderId = filter.Identity;
			filter.Identity = (Identity)filter.Identity.ToMailboxFolderIdParameter();
			PowerShellResults<MailboxFolderPermissionRow> list = base.GetList<MailboxFolderPermissionRow, MailboxFolderPermissionFilter>("Get-MailboxFolderPermission", filter, sort);
			list.Output = Array.FindAll<MailboxFolderPermissionRow>(list.Output, (MailboxFolderPermissionRow x) => !x.IsAnonymousOrDefault);
			Array.ForEach<MailboxFolderPermissionRow>(list.Output, delegate(MailboxFolderPermissionRow x)
			{
				x.MailboxFolderId = originalFolderId;
			});
			return list;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x00057430 File Offset: 0x00055630
		[PrincipalPermission(SecurityAction.Demand, Role = "Remove-MailboxFolderPermission?Identity&User@W:Self")]
		public PowerShellResults RemoveObjects(Identity[] identities, BaseWebServiceParameters parameters)
		{
			Identity identity = identities.IsNullOrEmpty() ? null : ((Identity)((MailboxFolderPermissionIdentity)identities[0]).MailboxFolderId.ToMailboxFolderIdParameter());
			return base.RemoveObjects("Remove-MailboxFolderPermission", identity, identities, "User", parameters);
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00057474 File Offset: 0x00055674
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxFolderPermission?Identity&User@R:Self")]
		public PowerShellResults<UserMailboxFolderPermission> GetObject(Identity identity)
		{
			MailboxFolderPermissionIdentity mailboxFolderPermissionIdentity = identity.ToMailboxFolderPermissionIdentity();
			PSCommand pscommand = new PSCommand().AddCommand("Get-MailboxFolderPermission");
			pscommand.AddParameter("User", mailboxFolderPermissionIdentity.RawIdentity);
			return base.GetObject<UserMailboxFolderPermission>(pscommand, (Identity)mailboxFolderPermissionIdentity.MailboxFolderId.ToMailboxFolderIdParameter());
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x000574C4 File Offset: 0x000556C4
		[PrincipalPermission(SecurityAction.Demand, Role = "Get-MailboxFolderPermission?Identity&User@R:Self+Set-MailboxFolderPermission?Identity&User@W:Self")]
		public PowerShellResults<UserMailboxFolderPermission> SetObject(Identity identity, SetUserMailboxFolderPermission properties)
		{
			properties.FaultIfNull();
			MailboxFolderPermissionIdentity mailboxFolderPermissionIdentity = identity.ToMailboxFolderPermissionIdentity();
			properties.User = mailboxFolderPermissionIdentity.RawIdentity;
			properties.ReturnObjectType = ReturnObjectTypes.PartialForList;
			return base.SetObject<UserMailboxFolderPermission, SetUserMailboxFolderPermission>("Set-MailboxFolderPermission", (Identity)mailboxFolderPermissionIdentity.MailboxFolderId.ToMailboxFolderIdParameter(), properties);
		}

		// Token: 0x04001B5F RID: 7007
		internal const string GetCmdlet = "Get-MailboxFolderPermission";

		// Token: 0x04001B60 RID: 7008
		internal const string SetCmdlet = "Set-MailboxFolderPermission";

		// Token: 0x04001B61 RID: 7009
		internal const string RemoveCmdlet = "Remove-MailboxFolderPermission";

		// Token: 0x04001B62 RID: 7010
		internal const string ReadScope = "@R:Self";

		// Token: 0x04001B63 RID: 7011
		internal const string WriteScope = "@W:Self";

		// Token: 0x04001B64 RID: 7012
		private const string GetListRole = "Get-MailboxFolderPermission?Identity@R:Self";

		// Token: 0x04001B65 RID: 7013
		private const string GetObjectRole = "Get-MailboxFolderPermission?Identity&User@R:Self";

		// Token: 0x04001B66 RID: 7014
		private const string SetObjectRole = "Get-MailboxFolderPermission?Identity&User@R:Self+Set-MailboxFolderPermission?Identity&User@W:Self";

		// Token: 0x04001B67 RID: 7015
		private const string RemoveObjectRole = "Remove-MailboxFolderPermission?Identity&User@W:Self";
	}
}
