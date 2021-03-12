using System;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RegisterMigrationBatchArgs
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00003220 File Offset: 0x00001420
		internal RegisterMigrationBatchArgs(Guid mdbGuid, string mailboxOwnerLegacyDN, ADObjectId organizationName, bool refresh)
		{
			SyncUtilities.ThrowIfGuidEmpty("mdbGuid", mdbGuid);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("mailboxOwnerLegacyDN", mailboxOwnerLegacyDN);
			this.mdbGuid = mdbGuid;
			this.mailboxOwnerLegacyDN = mailboxOwnerLegacyDN;
			this.organizationName = (organizationName ?? new ADObjectId());
			this.refresh = refresh;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000060 RID: 96 RVA: 0x0000326F File Offset: 0x0000146F
		internal Guid MdbGuid
		{
			get
			{
				return this.mdbGuid;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003277 File Offset: 0x00001477
		internal string MailboxOwnerLegacyDN
		{
			get
			{
				return this.mailboxOwnerLegacyDN;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000327F File Offset: 0x0000147F
		internal bool Refresh
		{
			get
			{
				return this.refresh;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003287 File Offset: 0x00001487
		internal ADObjectId OrganizationId
		{
			get
			{
				return this.organizationName;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000328F File Offset: 0x0000148F
		internal static RegisterMigrationBatchArgs UnMarshal(MdbefPropertyCollection inputArgs)
		{
			return new RegisterMigrationBatchArgs(MigrationRpcHelper.ReadValue<Guid>(inputArgs, 2688614472U), MigrationRpcHelper.ReadValue<string>(inputArgs, 2688679967U), MigrationRpcHelper.ReadADObjectId(inputArgs, 2688876802U), MigrationRpcHelper.ReadValue<bool>(inputArgs, 2688876555U, true));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000032C4 File Offset: 0x000014C4
		internal MdbefPropertyCollection Marshal()
		{
			MdbefPropertyCollection mdbefPropertyCollection = new MdbefPropertyCollection();
			mdbefPropertyCollection[2688614472U] = this.mdbGuid;
			mdbefPropertyCollection[2688679967U] = this.mailboxOwnerLegacyDN;
			mdbefPropertyCollection[2688876802U] = this.organizationName.GetBytes();
			mdbefPropertyCollection[2688876555U] = this.refresh;
			return mdbefPropertyCollection;
		}

		// Token: 0x04000084 RID: 132
		private readonly Guid mdbGuid;

		// Token: 0x04000085 RID: 133
		private readonly string mailboxOwnerLegacyDN;

		// Token: 0x04000086 RID: 134
		private readonly ADObjectId organizationName;

		// Token: 0x04000087 RID: 135
		private readonly bool refresh;
	}
}
