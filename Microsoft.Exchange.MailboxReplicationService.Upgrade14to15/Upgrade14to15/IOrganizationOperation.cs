using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000007 RID: 7
	internal interface IOrganizationOperation
	{
		// Token: 0x06000013 RID: 19
		TenantOrganizationPresentationObjectWrapper GetOrganization(string tenantId);

		// Token: 0x06000014 RID: 20
		List<TenantOrganizationPresentationObjectWrapper> GetAllOrganizations(bool checkAllPartitions);

		// Token: 0x06000015 RID: 21
		void SetOrganization(TenantOrganizationPresentationObjectWrapper tenant, UpgradeStatusTypes status, UpgradeRequestTypes request, string message, string details, UpgradeStage? upgradeStage = 0, int e14MbxCountForCurrentStage = -1, int nonUpgradeMoveRequestCount = -1);

		// Token: 0x06000016 RID: 22
		RecipientWrapper GetUser(string organizationId, string userId);

		// Token: 0x06000017 RID: 23
		void SetUser(RecipientWrapper user, UpgradeStatusTypes status, UpgradeRequestTypes request, string message, string details, UpgradeStage? stage);

		// Token: 0x06000018 RID: 24
		void InvokeOrganizationCmdlet(string organizationId, string cmdlet, bool configOnly);

		// Token: 0x06000019 RID: 25
		bool TryGetAnchorMailbox(string tenantId, out RecipientWrapper anchorMailbox);

		// Token: 0x0600001A RID: 26
		void CreateAnchorMailbox(string tenantId);

		// Token: 0x0600001B RID: 27
		void SetTenantUpgradeCapability(string identity, bool tenantUpgradeCapabilityEnabled);

		// Token: 0x0600001C RID: 28
		bool TryRemoveMoveRequest(string identity);
	}
}
