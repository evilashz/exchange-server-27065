using System;
using System.CodeDom.Compiler;
using System.ServiceModel;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200009A RID: 154
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[ServiceContract(ConfigurationName = "Microsoft.Exchange.MailboxReplicationService.Upgrade14to15.SuiteService.ISuiteHandler")]
	public interface ISuiteHandler
	{
		// Token: 0x060003E3 RID: 995
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/ISuiteHandler/AddPilotUsersArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[FaultContract(typeof(InvalidOperationFault), Action = "http://tempuri.org/ISuiteHandler/AddPilotUsersInvalidOperationFaultFault", Name = "InvalidOperationFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[OperationContract(Action = "http://tempuri.org/ISuiteHandler/AddPilotUsers", ReplyAction = "http://tempuri.org/ISuiteHandler/AddPilotUsersResponse")]
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/ISuiteHandler/AddPilotUsersAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		int AddPilotUsers(Guid tenantId, UserId[] users);

		// Token: 0x060003E4 RID: 996
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/ISuiteHandler/AddPilotUsers", ReplyAction = "http://tempuri.org/ISuiteHandler/AddPilotUsersResponse")]
		IAsyncResult BeginAddPilotUsers(Guid tenantId, UserId[] users, AsyncCallback callback, object asyncState);

		// Token: 0x060003E5 RID: 997
		int EndAddPilotUsers(IAsyncResult result);

		// Token: 0x060003E6 RID: 998
		[OperationContract(Action = "http://tempuri.org/ISuiteHandler/GetPilotUsers", ReplyAction = "http://tempuri.org/ISuiteHandler/GetPilotUsersResponse")]
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/ISuiteHandler/GetPilotUsersAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/ISuiteHandler/GetPilotUsersArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		UserWorkloadStatusInfo[] GetPilotUsers(Guid tenantId);

		// Token: 0x060003E7 RID: 999
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/ISuiteHandler/GetPilotUsers", ReplyAction = "http://tempuri.org/ISuiteHandler/GetPilotUsersResponse")]
		IAsyncResult BeginGetPilotUsers(Guid tenantId, AsyncCallback callback, object asyncState);

		// Token: 0x060003E8 RID: 1000
		UserWorkloadStatusInfo[] EndGetPilotUsers(IAsyncResult result);

		// Token: 0x060003E9 RID: 1001
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/ISuiteHandler/PostponeTenantUpgradeAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[OperationContract(Action = "http://tempuri.org/ISuiteHandler/PostponeTenantUpgrade", ReplyAction = "http://tempuri.org/ISuiteHandler/PostponeTenantUpgradeResponse")]
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/ISuiteHandler/PostponeTenantUpgradeArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[FaultContract(typeof(InvalidOperationFault), Action = "http://tempuri.org/ISuiteHandler/PostponeTenantUpgradeInvalidOperationFaultFault", Name = "InvalidOperationFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		void PostponeTenantUpgrade(Guid tenantId, string userUpn);

		// Token: 0x060003EA RID: 1002
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/ISuiteHandler/PostponeTenantUpgrade", ReplyAction = "http://tempuri.org/ISuiteHandler/PostponeTenantUpgradeResponse")]
		IAsyncResult BeginPostponeTenantUpgrade(Guid tenantId, string userUpn, AsyncCallback callback, object asyncState);

		// Token: 0x060003EB RID: 1003
		void EndPostponeTenantUpgrade(IAsyncResult result);
	}
}
