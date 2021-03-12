using System;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001DC RID: 476
	internal class InvalidItem : DirectoryItem
	{
		// Token: 0x06001596 RID: 5526 RVA: 0x000574B0 File Offset: 0x000556B0
		public InvalidItem(MailRecipient recipient) : base(recipient)
		{
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x000574B9 File Offset: 0x000556B9
		public override void PreProcess(Expansion expansion)
		{
			base.FailRecipient(AckReason.InvalidDirectoryObject);
		}
	}
}
