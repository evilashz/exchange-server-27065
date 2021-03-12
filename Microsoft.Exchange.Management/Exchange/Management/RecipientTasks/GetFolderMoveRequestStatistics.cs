using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C61 RID: 3169
	[Cmdlet("Get", "FolderMoveRequestStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetFolderMoveRequestStatistics : GetRequestStatistics<FolderMoveRequestIdParameter, FolderMoveRequestStatistics>
	{
	}
}
