using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.FolderUpdate
{
	// Token: 0x02000043 RID: 67
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FolderUpdateCommand : EasServerCommand<FolderUpdateRequest, FolderUpdateResponse, FolderUpdateStatus>
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00004A92 File Offset: 0x00002C92
		internal FolderUpdateCommand(EasConnectionSettings easConnectionSettings) : base(Command.FolderUpdate, easConnectionSettings)
		{
		}
	}
}
