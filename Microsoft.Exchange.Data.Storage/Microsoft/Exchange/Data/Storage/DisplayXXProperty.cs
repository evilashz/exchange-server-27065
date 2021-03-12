using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C0D RID: 3085
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class DisplayXXProperty : AggregateRecipientProperty
	{
		// Token: 0x06006E16 RID: 28182 RVA: 0x001D8E30 File Offset: 0x001D7030
		internal DisplayXXProperty(string displayName) : this(displayName, null, null)
		{
		}

		// Token: 0x06006E17 RID: 28183 RVA: 0x001D8E4E File Offset: 0x001D704E
		internal DisplayXXProperty(string displayName, NativeStorePropertyDefinition storeComputedProperty, RecipientItemType? recipientItemType) : base(displayName, storeComputedProperty, InternalSchema.DisplayName)
		{
			this.recipientItemType = recipientItemType;
		}

		// Token: 0x06006E18 RID: 28184 RVA: 0x001D8E70 File Offset: 0x001D7070
		protected override bool IsRecipientIncluded(RecipientBase recipientBase)
		{
			return this.recipientItemType == null || this.recipientItemType == recipientBase.RecipientItemType;
		}

		// Token: 0x04003EF6 RID: 16118
		private readonly RecipientItemType? recipientItemType = null;
	}
}
