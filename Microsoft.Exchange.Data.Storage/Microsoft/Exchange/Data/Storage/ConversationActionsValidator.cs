using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000679 RID: 1657
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ConversationActionsValidator : DefaultFolderValidator
	{
		// Token: 0x0600444D RID: 17485 RVA: 0x001232E0 File Offset: 0x001214E0
		internal ConversationActionsValidator() : base(new IValidator[]
		{
			new MatchIsHidden(true),
			new MatchMapiFolderType(FolderType.Generic),
			new MatchContainerClass("IPF.Configuration")
		})
		{
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x0012331A File Offset: 0x0012151A
		protected override void SetPropertiesInternal(DefaultFolderContext context, Folder folder)
		{
			base.SetPropertiesInternal(context, folder);
			folder[FolderSchema.IsHidden] = true;
			folder[StoreObjectSchema.ContainerClass] = "IPF.Configuration";
		}
	}
}
