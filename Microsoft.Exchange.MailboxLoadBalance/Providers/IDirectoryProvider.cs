using System;
using System.Collections.Generic;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;

namespace Microsoft.Exchange.MailboxLoadBalance.Providers
{
	// Token: 0x0200007D RID: 125
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IDirectoryProvider
	{
		// Token: 0x06000452 RID: 1106
		IEnumerable<DirectoryDatabaseAvailabilityGroup> GetDatabaseAvailabilityGroups();

		// Token: 0x06000453 RID: 1107
		DirectoryServer GetServer(Guid serverGuid);

		// Token: 0x06000454 RID: 1108
		DirectoryDatabase GetDatabase(Guid guid);

		// Token: 0x06000455 RID: 1109
		DirectoryForest GetLocalForest();

		// Token: 0x06000456 RID: 1110
		IEnumerable<DirectoryServer> GetServers(DirectoryIdentity dagIdentity);

		// Token: 0x06000457 RID: 1111
		IEnumerable<DirectoryServer> GetServers();

		// Token: 0x06000458 RID: 1112
		IEnumerable<DirectoryDatabase> GetDatabasesOwnedByServer(DirectoryServer server);

		// Token: 0x06000459 RID: 1113
		DirectoryServer GetLocalServer();

		// Token: 0x0600045A RID: 1114
		IEnumerable<DirectoryServer> GetActivationPreferenceForDatabase(DirectoryDatabase database);

		// Token: 0x0600045B RID: 1115
		DirectoryDatabase GetDatabaseForMailbox(DirectoryIdentity identity);

		// Token: 0x0600045C RID: 1116
		IEnumerable<DirectoryMailbox> GetMailboxesForDatabase(DirectoryDatabase database);

		// Token: 0x0600045D RID: 1117
		IEnumerable<NonConnectedMailbox> GetDisconnectedMailboxesForDatabase(DirectoryDatabase database);

		// Token: 0x0600045E RID: 1118
		DirectoryObject GetDirectoryObject(DirectoryIdentity directoryObjectIdentity);

		// Token: 0x0600045F RID: 1119
		IRequest CreateRequestToMove(DirectoryMailbox directoryMailbox, DirectoryIdentity targetIdentity, string batchName, ILogger logger);

		// Token: 0x06000460 RID: 1120
		DirectoryServer GetServerByFqdn(Fqdn fqdn);

		// Token: 0x06000461 RID: 1121
		IEnumerable<DirectoryDatabase> GetCachedDatabasesForProvisioning();
	}
}
