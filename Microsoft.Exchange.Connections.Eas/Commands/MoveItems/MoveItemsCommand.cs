using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.MoveItems
{
	// Token: 0x02000055 RID: 85
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MoveItemsCommand : EasServerCommand<MoveItemsRequest, MoveItemsResponse, MoveItemsStatus>
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00004FEB File Offset: 0x000031EB
		internal MoveItemsCommand(EasConnectionSettings easConnectionSettings) : base(Command.MoveItems, easConnectionSettings)
		{
		}
	}
}
