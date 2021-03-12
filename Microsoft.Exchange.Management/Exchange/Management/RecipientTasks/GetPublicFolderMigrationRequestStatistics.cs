using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CAE RID: 3246
	[Cmdlet("Get", "PublicFolderMigrationRequestStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetPublicFolderMigrationRequestStatistics : GetRequestStatistics<PublicFolderMigrationRequestIdParameter, PublicFolderMigrationRequestStatistics>
	{
	}
}
