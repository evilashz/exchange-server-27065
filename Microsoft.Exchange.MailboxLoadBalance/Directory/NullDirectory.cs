using System;
using System.Collections.Generic;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Providers;
using Microsoft.Exchange.MailboxLoadBalance.QueueProcessing;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x0200007E RID: 126
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NullDirectory : IDirectoryProvider
	{
		// Token: 0x06000462 RID: 1122 RVA: 0x0000B6F0 File Offset: 0x000098F0
		private NullDirectory()
		{
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000B798 File Offset: 0x00009998
		public IEnumerable<DirectoryDatabaseAvailabilityGroup> GetDatabaseAvailabilityGroups()
		{
			yield break;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000B7B5 File Offset: 0x000099B5
		public DirectoryServer GetServer(Guid serverGuid)
		{
			return null;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000B7B8 File Offset: 0x000099B8
		public DirectoryDatabase GetDatabase(Guid guid)
		{
			return null;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000B7BB File Offset: 0x000099BB
		public DirectoryForest GetLocalForest()
		{
			return null;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000B860 File Offset: 0x00009A60
		public IEnumerable<DirectoryServer> GetServers(DirectoryIdentity dagIdentity)
		{
			yield break;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000B920 File Offset: 0x00009B20
		public IEnumerable<DirectoryServer> GetServers()
		{
			yield break;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000B9E0 File Offset: 0x00009BE0
		public IEnumerable<DirectoryDatabase> GetDatabasesOwnedByServer(DirectoryServer server)
		{
			yield break;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000B9FD File Offset: 0x00009BFD
		public DirectoryServer GetLocalServer()
		{
			return null;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000BAA0 File Offset: 0x00009CA0
		public IEnumerable<DirectoryServer> GetActivationPreferenceForDatabase(DirectoryDatabase database)
		{
			yield break;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000BABD File Offset: 0x00009CBD
		public DirectoryDatabase GetDatabaseForMailbox(DirectoryIdentity identity)
		{
			return null;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000BB60 File Offset: 0x00009D60
		public IEnumerable<DirectoryMailbox> GetMailboxesForDatabase(DirectoryDatabase database)
		{
			yield break;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000BC20 File Offset: 0x00009E20
		public IEnumerable<NonConnectedMailbox> GetDisconnectedMailboxesForDatabase(DirectoryDatabase database)
		{
			yield break;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000BC3D File Offset: 0x00009E3D
		public DirectoryObject GetDirectoryObject(DirectoryIdentity directoryObjectIdentity)
		{
			return null;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000BC40 File Offset: 0x00009E40
		public IRequest CreateRequestToMove(DirectoryMailbox directoryMailbox, DirectoryIdentity targetIdentity, string batchName, ILogger logger)
		{
			return new NullDirectory.NullRequest();
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000BC47 File Offset: 0x00009E47
		public DirectoryServer GetServerByFqdn(Fqdn fqdn)
		{
			return null;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000BCEC File Offset: 0x00009EEC
		public IEnumerable<DirectoryDatabase> GetCachedDatabasesForProvisioning()
		{
			yield break;
		}

		// Token: 0x04000163 RID: 355
		public static readonly IDirectoryProvider Instance = new NullDirectory();

		// Token: 0x02000081 RID: 129
		private class NullRequest : BaseRequest
		{
			// Token: 0x06000490 RID: 1168 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
			protected override void ProcessRequest()
			{
			}
		}
	}
}
