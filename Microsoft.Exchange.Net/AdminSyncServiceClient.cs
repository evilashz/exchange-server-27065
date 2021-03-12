using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync;

// Token: 0x0200086C RID: 2156
[GeneratedCode("System.ServiceModel", "3.0.0.0")]
[DebuggerStepThrough]
internal class AdminSyncServiceClient : ClientBase<IAdminSyncService>, IAdminSyncService
{
	// Token: 0x06002E20 RID: 11808 RVA: 0x00066305 File Offset: 0x00064505
	public AdminSyncServiceClient()
	{
	}

	// Token: 0x06002E21 RID: 11809 RVA: 0x0006630D File Offset: 0x0006450D
	public AdminSyncServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
	{
	}

	// Token: 0x06002E22 RID: 11810 RVA: 0x00066316 File Offset: 0x00064516
	public AdminSyncServiceClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06002E23 RID: 11811 RVA: 0x00066320 File Offset: 0x00064520
	public AdminSyncServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06002E24 RID: 11812 RVA: 0x0006632A File Offset: 0x0006452A
	public AdminSyncServiceClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
	{
	}

	// Token: 0x06002E25 RID: 11813 RVA: 0x00066334 File Offset: 0x00064534
	public void Ping()
	{
		base.Channel.Ping();
	}

	// Token: 0x06002E26 RID: 11814 RVA: 0x00066341 File Offset: 0x00064541
	public FailedAdminAccounts SyncAdminAccounts(CompanyAdministrators companyAdministrators)
	{
		return base.Channel.SyncAdminAccounts(companyAdministrators);
	}

	// Token: 0x06002E27 RID: 11815 RVA: 0x0006634F File Offset: 0x0006454F
	public Dictionary<string, ErrorInfo> SyncGroupUsers(int companyId, Guid groupId, string[] users)
	{
		return base.Channel.SyncGroupUsers(companyId, groupId, users);
	}

	// Token: 0x06002E28 RID: 11816 RVA: 0x0006635F File Offset: 0x0006455F
	public Dictionary<Guid, RemoveGroupErrorInfo> RemoveGroups(Guid[] groupIds)
	{
		return base.Channel.RemoveGroups(groupIds);
	}

	// Token: 0x06002E29 RID: 11817 RVA: 0x0006636D File Offset: 0x0006456D
	public string[] GetGroupMembers(Guid groupId)
	{
		return base.Channel.GetGroupMembers(groupId);
	}

	// Token: 0x06002E2A RID: 11818 RVA: 0x0006637B File Offset: 0x0006457B
	public CompanyAdministrators GetAdminAccounts(int companyId)
	{
		return base.Channel.GetAdminAccounts(companyId);
	}

	// Token: 0x06002E2B RID: 11819 RVA: 0x00066389 File Offset: 0x00064589
	public Dictionary<int, Role[]> GetUserPermissions(string userEmailAddress)
	{
		return base.Channel.GetUserPermissions(userEmailAddress);
	}
}
