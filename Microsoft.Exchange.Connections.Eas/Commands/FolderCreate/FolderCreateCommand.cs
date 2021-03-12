using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.FolderCreate
{
	// Token: 0x02000031 RID: 49
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FolderCreateCommand : EasServerCommand<FolderCreateRequest, FolderCreateResponse, FolderCreateStatus>
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00004664 File Offset: 0x00002864
		internal FolderCreateCommand(EasConnectionSettings easConnectionSettings) : base(Command.FolderCreate, easConnectionSettings)
		{
		}
	}
}
