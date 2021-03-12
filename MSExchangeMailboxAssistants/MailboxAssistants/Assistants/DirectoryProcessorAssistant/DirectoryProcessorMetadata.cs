using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant
{
	// Token: 0x020001AC RID: 428
	internal enum DirectoryProcessorMetadata
	{
		// Token: 0x04000A8F RID: 2703
		[DisplayName("DirectoryProcessor.TaskName")]
		TaskName,
		// Token: 0x04000A90 RID: 2704
		[DisplayName("DirectoryProcessor.RecipientType")]
		RecipientType,
		// Token: 0x04000A91 RID: 2705
		[DisplayName("DirectoryProcessor.ChunkId")]
		ChunkId,
		// Token: 0x04000A92 RID: 2706
		[DisplayName("DirectoryProcessor.MailboxGuid")]
		MailboxGuid,
		// Token: 0x04000A93 RID: 2707
		[DisplayName("DirectoryProcessor.TenantGuid")]
		TenantGuid
	}
}
