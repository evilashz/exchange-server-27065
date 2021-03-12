using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CBA RID: 3258
	[Cmdlet("Get", "PublicFolderMailboxMigrationRequestStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetPublicFolderMailboxMigrationRequestStatistics : GetRequestStatistics<PublicFolderMailboxMigrationRequestIdParameter, PublicFolderMailboxMigrationRequestStatistics>
	{
	}
}
