using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.FolderDelete
{
	// Token: 0x02000037 RID: 55
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FolderDeleteCommand : EasServerCommand<FolderDeleteRequest, FolderDeleteResponse, FolderDeleteStatus>
	{
		// Token: 0x06000132 RID: 306 RVA: 0x000047C5 File Offset: 0x000029C5
		internal FolderDeleteCommand(EasConnectionSettings easConnectionSettings) : base(Command.FolderDelete, easConnectionSettings)
		{
		}
	}
}
