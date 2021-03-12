using System;
using System.ServiceModel;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000E9 RID: 233
	[ServiceKnownType(typeof(AssociationConfiguration))]
	[ServiceKnownType(typeof(RuleConfiguration))]
	[ServiceKnownType(typeof(BindingConfiguration))]
	[ServiceKnownType(typeof(PolicyConfiguration))]
	[ServiceContract]
	public interface IPolicySyncWebserviceClient : IDisposable
	{
		// Token: 0x0600064E RID: 1614
		[OperationContract]
		[FaultContract(typeof(PolicySyncTransientFault))]
		[FaultContract(typeof(PolicySyncPermanentFault))]
		PolicyChange GetSingleTenantChanges(TenantCookie tenantCookie);

		// Token: 0x0600064F RID: 1615
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetSingleTenantChanges(TenantCookie tenantCookie, AsyncCallback userCallback, object stateObject);

		// Token: 0x06000650 RID: 1616
		PolicyChange EndGetSingleTenantChanges(IAsyncResult asyncResult);

		// Token: 0x06000651 RID: 1617
		[FaultContract(typeof(PolicySyncTransientFault))]
		[FaultContract(typeof(PolicySyncPermanentFault))]
		[OperationContract(Action = "http://tempuri.org/IPolicySyncWebservice/GetChanges", ReplyAction = "http://tempuri.org/IPolicySyncWebservice/GetChangesResponse")]
		PolicyChangeBatch GetChanges(GetChangesRequest request);

		// Token: 0x06000652 RID: 1618
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IPolicySyncWebservice/GetChanges", ReplyAction = "http://tempuri.org/IPolicySyncWebservice/GetChangesResponse")]
		IAsyncResult BeginGetChanges(GetChangesRequest request, AsyncCallback userCallback, object stateObject);

		// Token: 0x06000653 RID: 1619
		PolicyChangeBatch EndGetChanges(IAsyncResult asyncResult);

		// Token: 0x06000654 RID: 1620
		[OperationContract(Action = "http://tempuri.org/IPolicySyncWebservice/GetObject", ReplyAction = "http://tempuri.org/IPolicySyncWebservice/GetObjectResponse")]
		[FaultContract(typeof(PolicySyncTransientFault))]
		[FaultContract(typeof(PolicySyncPermanentFault))]
		PolicyConfigurationBase GetObject(SyncCallerContext callerContext, Guid tenantId, ConfigurationObjectType objectType, Guid objectId, bool includeDeletedObjects);

		// Token: 0x06000655 RID: 1621
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IPolicySyncWebservice/GetObject", ReplyAction = "http://tempuri.org/IPolicySyncWebservice/GetObjectResponse")]
		IAsyncResult BeginGetObject(SyncCallerContext callerContext, Guid tenantId, ConfigurationObjectType objectType, Guid objectId, bool includeDeletedObjects, AsyncCallback userCallback, object stateObject);

		// Token: 0x06000656 RID: 1622
		PolicyConfigurationBase EndGetObject(IAsyncResult asyncResult);

		// Token: 0x06000657 RID: 1623
		[FaultContract(typeof(PolicySyncPermanentFault))]
		[OperationContract(Action = "http://tempuri.org/IPolicySyncWebservice/PublishStatus", ReplyAction = "http://tempuri.org/IPolicySyncWebservice/PublishStatusResponse")]
		[FaultContract(typeof(PolicySyncTransientFault))]
		void PublishStatus(PublishStatusRequest request);

		// Token: 0x06000658 RID: 1624
		[OperationContract(AsyncPattern = true, Action = "http://tempuri.org/IPolicySyncWebservice/PublishStatus", ReplyAction = "http://tempuri.org/IPolicySyncWebservice/PublishStatusResponse")]
		IAsyncResult BeginPublishStatus(PublishStatusRequest request, AsyncCallback userCallback, object stateObject);

		// Token: 0x06000659 RID: 1625
		void EndPublishStatus(IAsyncResult asyncResult);
	}
}
