using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.DirectoryServices
{
	// Token: 0x02000003 RID: 3
	public interface IDirectory
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4
		// (set) Token: 0x06000005 RID: 5
		bool BypassContextADAccessValidation { get; set; }

		// Token: 0x06000006 RID: 6
		ErrorCode PrimeDirectoryCaches(IExecutionContext context);

		// Token: 0x06000007 RID: 7
		ServerInfo GetServerInfo(IExecutionContext context);

		// Token: 0x06000008 RID: 8
		void RefreshServerInfo(IExecutionContext context);

		// Token: 0x06000009 RID: 9
		void RefreshDatabaseInfo(IExecutionContext context, Guid databaseGuid);

		// Token: 0x0600000A RID: 10
		void RefreshMailboxInfo(IExecutionContext context, Guid mailboxGuid);

		// Token: 0x0600000B RID: 11
		void RefreshOrganizationContainer(IExecutionContext context, Guid organizationGuid);

		// Token: 0x0600000C RID: 12
		DatabaseInfo GetDatabaseInfo(IExecutionContext context, Guid databaseGuid);

		// Token: 0x0600000D RID: 13
		MailboxInfo GetMailboxInfo(IExecutionContext context, TenantHint tenantHint, Guid mailboxGuid, GetMailboxInfoFlags flags);

		// Token: 0x0600000E RID: 14
		MailboxInfo GetMailboxInfo(IExecutionContext context, TenantHint tenantHint, string legacyDN);

		// Token: 0x0600000F RID: 15
		AddressInfo GetAddressInfoByMailboxGuid(IExecutionContext context, TenantHint tenantHint, Guid mailboxGuid, GetAddressInfoFlags flags);

		// Token: 0x06000010 RID: 16
		AddressInfo GetAddressInfoByObjectId(IExecutionContext context, TenantHint tenantHint, Guid objectId);

		// Token: 0x06000011 RID: 17
		AddressInfo GetAddressInfo(IExecutionContext context, TenantHint tenantHint, string legacyDN, bool loadPublicDelegates);

		// Token: 0x06000012 RID: 18
		TenantHint ResolveTenantHint(IExecutionContext context, byte[] tenantHintBlob);

		// Token: 0x06000013 RID: 19
		void PrePopulateCachesForMailbox(IExecutionContext context, TenantHint tenantHint, Guid mailboxGuid, string domainController);

		// Token: 0x06000014 RID: 20
		bool IsMemberOfDistributionList(IExecutionContext context, TenantHint tenantHint, AddressInfo addressInfo, Guid distributionListObjectId);
	}
}
