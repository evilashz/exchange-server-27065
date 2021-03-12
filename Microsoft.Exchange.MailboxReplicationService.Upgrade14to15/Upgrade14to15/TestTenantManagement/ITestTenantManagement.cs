using System;
using System.CodeDom.Compiler;
using System.ServiceModel;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15.TestTenantManagement
{
	// Token: 0x020000B3 RID: 179
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[ServiceContract(ConfigurationName = "Microsoft.Exchange.MailboxReplicationService.Upgrade14to15.TestTenantManagement.ITestTenantManagement")]
	public interface ITestTenantManagement
	{
		// Token: 0x06000520 RID: 1312
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/ITestTenantManagement/QueryTenantsToPopulateAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts.Common")]
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/ITestTenantManagement/QueryTenantsToPopulateArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts.Common")]
		[OperationContract(Action = "http://tempuri.org/ITestTenantManagement/QueryTenantsToPopulate", ReplyAction = "http://tempuri.org/ITestTenantManagement/QueryTenantsToPopulateResponse")]
		Tenant[] QueryTenantsToPopulate(PopulationStatus status);

		// Token: 0x06000521 RID: 1313
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/ITestTenantManagement/QueryTenantsToPopulate", ReplyAction = "http://tempuri.org/ITestTenantManagement/QueryTenantsToPopulateResponse")]
		IAsyncResult BeginQueryTenantsToPopulate(PopulationStatus status, AsyncCallback callback, object asyncState);

		// Token: 0x06000522 RID: 1314
		Tenant[] EndQueryTenantsToPopulate(IAsyncResult result);

		// Token: 0x06000523 RID: 1315
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/ITestTenantManagement/QueryTenantsToValidateArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts.Common")]
		[OperationContract(Action = "http://tempuri.org/ITestTenantManagement/QueryTenantsToValidate", ReplyAction = "http://tempuri.org/ITestTenantManagement/QueryTenantsToValidateResponse")]
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/ITestTenantManagement/QueryTenantsToValidateAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts.Common")]
		Tenant[] QueryTenantsToValidate(ValidationStatus status);

		// Token: 0x06000524 RID: 1316
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/ITestTenantManagement/QueryTenantsToValidate", ReplyAction = "http://tempuri.org/ITestTenantManagement/QueryTenantsToValidateResponse")]
		IAsyncResult BeginQueryTenantsToValidate(ValidationStatus status, AsyncCallback callback, object asyncState);

		// Token: 0x06000525 RID: 1317
		Tenant[] EndQueryTenantsToValidate(IAsyncResult result);

		// Token: 0x06000526 RID: 1318
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/ITestTenantManagement/QueryTenantsToValidateByScenarioArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts.Common")]
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/ITestTenantManagement/QueryTenantsToValidateByScenarioAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts.Common")]
		[OperationContract(Action = "http://tempuri.org/ITestTenantManagement/QueryTenantsToValidateByScenario", ReplyAction = "http://tempuri.org/ITestTenantManagement/QueryTenantsToValidateByScenarioResponse")]
		Tenant[] QueryTenantsToValidateByScenario(ValidationStatus status, string scenario);

		// Token: 0x06000527 RID: 1319
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/ITestTenantManagement/QueryTenantsToValidateByScenario", ReplyAction = "http://tempuri.org/ITestTenantManagement/QueryTenantsToValidateByScenarioResponse")]
		IAsyncResult BeginQueryTenantsToValidateByScenario(ValidationStatus status, string scenario, AsyncCallback callback, object asyncState);

		// Token: 0x06000528 RID: 1320
		Tenant[] EndQueryTenantsToValidateByScenario(IAsyncResult result);

		// Token: 0x06000529 RID: 1321
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/ITestTenantManagement/UpdateTenantPopulationStatusArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts.Common")]
		[OperationContract(Action = "http://tempuri.org/ITestTenantManagement/UpdateTenantPopulationStatus", ReplyAction = "http://tempuri.org/ITestTenantManagement/UpdateTenantPopulationStatusResponse")]
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/ITestTenantManagement/UpdateTenantPopulationStatusAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts.Common")]
		void UpdateTenantPopulationStatus(Guid tenantId, PopulationStatus status);

		// Token: 0x0600052A RID: 1322
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/ITestTenantManagement/UpdateTenantPopulationStatus", ReplyAction = "http://tempuri.org/ITestTenantManagement/UpdateTenantPopulationStatusResponse")]
		IAsyncResult BeginUpdateTenantPopulationStatus(Guid tenantId, PopulationStatus status, AsyncCallback callback, object asyncState);

		// Token: 0x0600052B RID: 1323
		void EndUpdateTenantPopulationStatus(IAsyncResult result);

		// Token: 0x0600052C RID: 1324
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/ITestTenantManagement/UpdateTenantPopulationStatusWithScenarioAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts.Common")]
		[OperationContract(Action = "http://tempuri.org/ITestTenantManagement/UpdateTenantPopulationStatusWithScenario", ReplyAction = "http://tempuri.org/ITestTenantManagement/UpdateTenantPopulationStatusWithScenarioResponse")]
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/ITestTenantManagement/UpdateTenantPopulationStatusWithScenarioArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts.Common")]
		void UpdateTenantPopulationStatusWithScenario(Guid tenantId, PopulationStatus status, string scenario, string comment);

		// Token: 0x0600052D RID: 1325
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/ITestTenantManagement/UpdateTenantPopulationStatusWithScenario", ReplyAction = "http://tempuri.org/ITestTenantManagement/UpdateTenantPopulationStatusWithScenarioResponse")]
		IAsyncResult BeginUpdateTenantPopulationStatusWithScenario(Guid tenantId, PopulationStatus status, string scenario, string comment, AsyncCallback callback, object asyncState);

		// Token: 0x0600052E RID: 1326
		void EndUpdateTenantPopulationStatusWithScenario(IAsyncResult result);

		// Token: 0x0600052F RID: 1327
		[OperationContract(Action = "http://tempuri.org/ITestTenantManagement/UpdateTenantValidationStatus", ReplyAction = "http://tempuri.org/ITestTenantManagement/UpdateTenantValidationStatusResponse")]
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/ITestTenantManagement/UpdateTenantValidationStatusAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts.Common")]
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/ITestTenantManagement/UpdateTenantValidationStatusArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts.Common")]
		void UpdateTenantValidationStatus(Guid tenantId, ValidationStatus status, int? office15BugId);

		// Token: 0x06000530 RID: 1328
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/ITestTenantManagement/UpdateTenantValidationStatus", ReplyAction = "http://tempuri.org/ITestTenantManagement/UpdateTenantValidationStatusResponse")]
		IAsyncResult BeginUpdateTenantValidationStatus(Guid tenantId, ValidationStatus status, int? office15BugId, AsyncCallback callback, object asyncState);

		// Token: 0x06000531 RID: 1329
		void EndUpdateTenantValidationStatus(IAsyncResult result);

		// Token: 0x06000532 RID: 1330
		[OperationContract(Action = "http://tempuri.org/ITestTenantManagement/UpdateTenantValidationStatusWithReason", ReplyAction = "http://tempuri.org/ITestTenantManagement/UpdateTenantValidationStatusWithReasonResponse")]
		[FaultContract(typeof(AccessDeniedFault), Action = "http://tempuri.org/ITestTenantManagement/UpdateTenantValidationStatusWithReasonAccessDeniedFaultFault", Name = "AccessDeniedFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts.Common")]
		[FaultContract(typeof(ArgumentFault), Action = "http://tempuri.org/ITestTenantManagement/UpdateTenantValidationStatusWithReasonArgumentFaultFault", Name = "ArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts.Common")]
		void UpdateTenantValidationStatusWithReason(Guid tenantId, ValidationStatus status, string failureReason);

		// Token: 0x06000533 RID: 1331
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/ITestTenantManagement/UpdateTenantValidationStatusWithReason", ReplyAction = "http://tempuri.org/ITestTenantManagement/UpdateTenantValidationStatusWithReasonResponse")]
		IAsyncResult BeginUpdateTenantValidationStatusWithReason(Guid tenantId, ValidationStatus status, string failureReason, AsyncCallback callback, object asyncState);

		// Token: 0x06000534 RID: 1332
		void EndUpdateTenantValidationStatusWithReason(IAsyncResult result);
	}
}
