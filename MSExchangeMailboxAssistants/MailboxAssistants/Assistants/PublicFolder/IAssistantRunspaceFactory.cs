using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000162 RID: 354
	internal interface IAssistantRunspaceFactory : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000E66 RID: 3686
		IAssistantRunspaceProxy CreateRunspaceForDatacenterAdmin(OrganizationId organizationId);
	}
}
