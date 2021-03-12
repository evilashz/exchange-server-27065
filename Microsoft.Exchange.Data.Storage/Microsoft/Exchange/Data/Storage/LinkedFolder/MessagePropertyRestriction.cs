using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000982 RID: 2434
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MessagePropertyRestriction : PropertyRestriction
	{
		// Token: 0x060059F3 RID: 23027 RVA: 0x00174DC8 File Offset: 0x00172FC8
		public MessagePropertyRestriction()
		{
			this.BlockBeforeLink.Add(MessageItemSchema.LinkedId);
			this.BlockBeforeLink.Add(MessageItemSchema.LinkedUrl);
			this.BlockBeforeLink.Add(MessageItemSchema.LinkedObjectVersion);
			this.BlockBeforeLink.Add(MessageItemSchema.LinkedSiteUrl);
			this.BlockBeforeLink.Add(MessageItemSchema.LinkedDocumentSize);
			this.BlockAfterLink.Add(MessageItemSchema.LinkedId);
			this.BlockAfterLink.Add(MessageItemSchema.LinkedUrl);
			this.BlockAfterLink.Add(MessageItemSchema.LinkedObjectVersion);
			this.BlockAfterLink.Add(MessageItemSchema.LinkedSiteUrl);
			this.BlockAfterLink.Add(MessageItemSchema.LinkedDocumentSize);
			this.BlockAfterLink.Add(StoreObjectSchema.DisplayName);
			this.BlockAfterLink.Add(StoreObjectSchema.ItemClass);
			this.BlockAfterLink.Add(InternalSchema.MapiSubject);
			this.BlockAfterLink.Add(InternalSchema.NormalizedSubjectInternal);
			this.BlockAfterLink.Add(InternalSchema.SubjectPrefixInternal);
			this.BlockAfterLink.Add(ItemSchema.NormalizedSubject);
			this.BlockAfterLink.Add(ItemSchema.SubjectPrefix);
			this.BlockAfterLink.Add(ItemSchema.LastModifiedBy);
			this.BlockAfterLink.Add(ItemSchema.ReceivedTime);
		}

		// Token: 0x04003175 RID: 12661
		public static MessagePropertyRestriction Instance = new MessagePropertyRestriction();
	}
}
