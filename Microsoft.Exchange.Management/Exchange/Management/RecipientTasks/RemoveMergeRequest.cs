using System;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C8A RID: 3210
	[Cmdlet("Remove", "MergeRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class RemoveMergeRequest : RemoveRequest<MergeRequestIdParameter>
	{
		// Token: 0x06007B76 RID: 31606 RVA: 0x001FA62B File Offset: 0x001F882B
		internal override string GenerateIndexEntryString(IRequestIndexEntry entry)
		{
			return new MergeRequest(entry).ToString();
		}
	}
}
