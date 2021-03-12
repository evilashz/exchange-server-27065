using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CA7 RID: 3239
	[Cmdlet("Get", "PublicFolderMoveRequestStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetPublicFolderMoveRequestStatistics : GetRequestStatistics<PublicFolderMoveRequestIdParameter, PublicFolderMoveRequestStatistics>
	{
	}
}
