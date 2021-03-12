using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000091 RID: 145
	internal interface ITenantRecipientSession : IRecipientSession, IDirectorySession, IConfigDataProvider
	{
		// Token: 0x0600079C RID: 1948
		ADRawEntry ChooseBetweenAmbiguousUsers(ADRawEntry[] entries);

		// Token: 0x0600079D RID: 1949
		ADObjectId ChooseBetweenAmbiguousUsers(ADObjectId user1Id, ADObjectId user2Id);

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600079E RID: 1950
		DirectoryBackendType DirectoryBackendType { get; }

		// Token: 0x0600079F RID: 1951
		Result<ADRawEntry>[] FindByExternalDirectoryObjectIds(string[] externalDirectoryObjectIds, params PropertyDefinition[] properties);

		// Token: 0x060007A0 RID: 1952
		Result<ADRawEntry>[] FindByExternalDirectoryObjectIds(string[] externalDirectoryObjectIds, bool includeDeletedObjects, params PropertyDefinition[] properties);

		// Token: 0x060007A1 RID: 1953
		ADRawEntry[] FindByNetID(string netID, string organizationContext, params PropertyDefinition[] properties);

		// Token: 0x060007A2 RID: 1954
		ADRawEntry[] FindByNetID(string netID, params PropertyDefinition[] properties);

		// Token: 0x060007A3 RID: 1955
		MiniRecipient FindRecipientByNetID(NetID netId);

		// Token: 0x060007A4 RID: 1956
		ADRawEntry FindUniqueEntryByNetID(string netID, string organizationContext, params PropertyDefinition[] properties);

		// Token: 0x060007A5 RID: 1957
		ADRawEntry FindUniqueEntryByNetID(string netID, params PropertyDefinition[] properties);

		// Token: 0x060007A6 RID: 1958
		ADRawEntry FindByLiveIdMemberName(string liveIdMemberName, params PropertyDefinition[] properties);

		// Token: 0x060007A7 RID: 1959
		Result<ADRawEntry>[] ReadMultipleByLinkedPartnerId(LinkedPartnerGroupInformation[] entryIds, params PropertyDefinition[] properties);
	}
}
