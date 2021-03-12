using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Provisioning.LoadBalancing;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200018D RID: 397
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class XO1ProvisioningDataFactory
	{
		// Token: 0x06001276 RID: 4726 RVA: 0x0004E228 File Offset: 0x0004C428
		internal static ProvisioningData GetProvisioningData(string identity, ProvisioningDataStorageBase storage)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(identity, "identity");
			MigrationUtil.ThrowOnNullArgument(storage, "storage");
			XO1ProvisioningDataStorage xo1ProvisioningDataStorage = storage as XO1ProvisioningDataStorage;
			MigrationUtil.ThrowOnNullArgument(xo1ProvisioningDataStorage, "xo1Storage");
			ADObjectId adobjectId = PhysicalResourceLoadBalancing.FindDatabase(null);
			XO1UserProvisioningData xo1UserProvisioningData = XO1UserProvisioningData.Create(identity, xo1ProvisioningDataStorage.Puid, xo1ProvisioningDataStorage.TimeZone, xo1ProvisioningDataStorage.LocaleId, adobjectId.Name, xo1ProvisioningDataStorage.FirstName, xo1ProvisioningDataStorage.LastName, xo1ProvisioningDataStorage.EmailAddresses);
			xo1UserProvisioningData.Action = ProvisioningAction.CreateNewOrUpdateExisting;
			xo1UserProvisioningData.Component = ProvisioningComponent.XO1;
			return xo1UserProvisioningData;
		}
	}
}
