using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAnchorADProvider
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28
		string TenantOrganizationName { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29
		MicrosoftExchangeRecipient PrimaryExchangeRecipient { get; }

		// Token: 0x0600001E RID: 30
		ADRecipient GetADRecipientByObjectId(ADObjectId objectId);

		// Token: 0x0600001F RID: 31
		void AddCapability(ADObjectId objectId, OrganizationCapability capability);

		// Token: 0x06000020 RID: 32
		void RemoveCapability(ADObjectId objectId, OrganizationCapability capability);

		// Token: 0x06000021 RID: 33
		string GetDatabaseServerFqdn(Guid mdbGuid, bool forceRediscovery);

		// Token: 0x06000022 RID: 34
		string GetMailboxServerFqdn(ADUser user, bool forceRefresh);

		// Token: 0x06000023 RID: 35
		void EnsureLocalMailbox(ADUser user, bool forceRefresh);

		// Token: 0x06000024 RID: 36
		string GetPreferredDomainController();

		// Token: 0x06000025 RID: 37
		ADRecipient GetADRecipientByProxyAddress(string userEmail);

		// Token: 0x06000026 RID: 38
		IEnumerable<ADUser> GetOrganizationMailboxesByCapability(OrganizationCapability capability);

		// Token: 0x06000027 RID: 39
		ADPagedReader<TEntry> FindPagedMiniRecipient<TEntry>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties) where TEntry : MiniRecipient, new();

		// Token: 0x06000028 RID: 40
		ADPagedReader<ADRawEntry> FindPagedADRawEntry(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int pageSize, IEnumerable<PropertyDefinition> properties);
	}
}
