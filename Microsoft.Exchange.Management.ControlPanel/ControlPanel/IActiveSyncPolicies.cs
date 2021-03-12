﻿using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002FC RID: 764
	[ServiceContract(Namespace = "ECP", Name = "ActiveSyncPolicies")]
	public interface IActiveSyncPolicies : IDataSourceService<ActiveSyncMailboxPolicyFilter, ActiveSyncMailboxPolicyRow, ActiveSyncMailboxPolicyObject, SetActiveSyncMailboxPolicyParams, NewActiveSyncMailboxPolicyParams>, IDataSourceService<ActiveSyncMailboxPolicyFilter, ActiveSyncMailboxPolicyRow, ActiveSyncMailboxPolicyObject, SetActiveSyncMailboxPolicyParams, NewActiveSyncMailboxPolicyParams, BaseWebServiceParameters>, IEditListService<ActiveSyncMailboxPolicyFilter, ActiveSyncMailboxPolicyRow, ActiveSyncMailboxPolicyObject, NewActiveSyncMailboxPolicyParams, BaseWebServiceParameters>, IGetListService<ActiveSyncMailboxPolicyFilter, ActiveSyncMailboxPolicyRow>, INewObjectService<ActiveSyncMailboxPolicyRow, NewActiveSyncMailboxPolicyParams>, IRemoveObjectsService<BaseWebServiceParameters>, IEditObjectForListService<ActiveSyncMailboxPolicyObject, SetActiveSyncMailboxPolicyParams, ActiveSyncMailboxPolicyRow>, IGetObjectService<ActiveSyncMailboxPolicyObject>, IGetObjectForListService<ActiveSyncMailboxPolicyRow>
	{
	}
}
