using System;
using System.CodeDom.Compiler;
using System.ServiceModel;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000CD RID: 205
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[ServiceContract(ConfigurationName = "Microsoft.Exchange.MailboxReplicationService.Upgrade14to15.WorkloadService.IUpgradeSchedulingConstraints")]
	public interface IUpgradeSchedulingConstraints
	{
		// Token: 0x0600063B RID: 1595
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateTenantReadinessArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[OperationContract(Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateTenantReadiness", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateTenantReadinessResponse")]
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateTenantReadinessAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		void UpdateTenantReadiness(TenantReadiness[] tenantReadiness);

		// Token: 0x0600063C RID: 1596
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateTenantReadiness", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateTenantReadinessResponse")]
		IAsyncResult BeginUpdateTenantReadiness(TenantReadiness[] tenantReadiness, AsyncCallback callback, object asyncState);

		// Token: 0x0600063D RID: 1597
		void EndUpdateTenantReadiness(IAsyncResult result);

		// Token: 0x0600063E RID: 1598
		[OperationContract(Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateGroup", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateGroupResponse")]
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateGroupAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateGroupArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		void UpdateGroup(Group[] group);

		// Token: 0x0600063F RID: 1599
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateGroup", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateGroupResponse")]
		IAsyncResult BeginUpdateGroup(Group[] group, AsyncCallback callback, object asyncState);

		// Token: 0x06000640 RID: 1600
		void EndUpdateGroup(IAsyncResult result);

		// Token: 0x06000641 RID: 1601
		[OperationContract(Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateCapacity", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateCapacityResponse")]
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateCapacityAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateCapacityArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		void UpdateCapacity(GroupCapacity[] capacities);

		// Token: 0x06000642 RID: 1602
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateCapacity", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateCapacityResponse")]
		IAsyncResult BeginUpdateCapacity(GroupCapacity[] capacities, AsyncCallback callback, object asyncState);

		// Token: 0x06000643 RID: 1603
		void EndUpdateCapacity(IAsyncResult result);

		// Token: 0x06000644 RID: 1604
		[OperationContract(Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateBlackout", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateBlackoutResponse")]
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateBlackoutAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateBlackoutArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		void UpdateBlackout(GroupBlackout[] blackouts);

		// Token: 0x06000645 RID: 1605
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateBlackout", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateBlackoutResponse")]
		IAsyncResult BeginUpdateBlackout(GroupBlackout[] blackouts, AsyncCallback callback, object asyncState);

		// Token: 0x06000646 RID: 1606
		void EndUpdateBlackout(IAsyncResult result);

		// Token: 0x06000647 RID: 1607
		[OperationContract(Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateConstraint", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateConstraintResponse")]
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateConstraintAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateConstraintArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		void UpdateConstraint(Constraint[] constraint);

		// Token: 0x06000648 RID: 1608
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateConstraint", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/UpdateConstraintResponse")]
		IAsyncResult BeginUpdateConstraint(Constraint[] constraint, AsyncCallback callback, object asyncState);

		// Token: 0x06000649 RID: 1609
		void EndUpdateConstraint(IAsyncResult result);

		// Token: 0x0600064A RID: 1610
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryTenantReadinessAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[OperationContract(Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryTenantReadiness", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryTenantReadinessResponse")]
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryTenantReadinessArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		TenantReadiness[] QueryTenantReadiness(Guid[] tenantIds);

		// Token: 0x0600064B RID: 1611
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryTenantReadiness", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryTenantReadinessResponse")]
		IAsyncResult BeginQueryTenantReadiness(Guid[] tenantIds, AsyncCallback callback, object asyncState);

		// Token: 0x0600064C RID: 1612
		TenantReadiness[] EndQueryTenantReadiness(IAsyncResult result);

		// Token: 0x0600064D RID: 1613
		[OperationContract(Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryGroup", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryGroupResponse")]
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryGroupArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryGroupAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		Group[] QueryGroup(string[] groupNames);

		// Token: 0x0600064E RID: 1614
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryGroup", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryGroupResponse")]
		IAsyncResult BeginQueryGroup(string[] groupNames, AsyncCallback callback, object asyncState);

		// Token: 0x0600064F RID: 1615
		Group[] EndQueryGroup(IAsyncResult result);

		// Token: 0x06000650 RID: 1616
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryCapacityArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryCapacityAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[OperationContract(Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryCapacity", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryCapacityResponse")]
		GroupCapacity[] QueryCapacity(string[] groupNames);

		// Token: 0x06000651 RID: 1617
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryCapacity", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryCapacityResponse")]
		IAsyncResult BeginQueryCapacity(string[] groupNames, AsyncCallback callback, object asyncState);

		// Token: 0x06000652 RID: 1618
		GroupCapacity[] EndQueryCapacity(IAsyncResult result);

		// Token: 0x06000653 RID: 1619
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryBlackoutArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryBlackoutAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[OperationContract(Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryBlackout", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryBlackoutResponse")]
		GroupBlackout[] QueryBlackout(string[] groupNames);

		// Token: 0x06000654 RID: 1620
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryBlackout", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryBlackoutResponse")]
		IAsyncResult BeginQueryBlackout(string[] groupNames, AsyncCallback callback, object asyncState);

		// Token: 0x06000655 RID: 1621
		GroupBlackout[] EndQueryBlackout(IAsyncResult result);

		// Token: 0x06000656 RID: 1622
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryConstraintArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryConstraintAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.Common")]
		[OperationContract(Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryConstraint", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryConstraintResponse")]
		Constraint[] QueryConstraint(string[] constraintName);

		// Token: 0x06000657 RID: 1623
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryConstraint", ReplyAction = "http://tempuri.org/IUpgradeSchedulingConstraints/QueryConstraintResponse")]
		IAsyncResult BeginQueryConstraint(string[] constraintName, AsyncCallback callback, object asyncState);

		// Token: 0x06000658 RID: 1624
		Constraint[] EndQueryConstraint(IAsyncResult result);
	}
}
