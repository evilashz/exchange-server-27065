using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200004A RID: 74
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Ex12MultivalueEntryIdNoMoveStampStrategy : Ex12MultivalueEntryIdStrategy
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x0002D9B4 File Offset: 0x0002BBB4
		internal Ex12MultivalueEntryIdNoMoveStampStrategy(StorePropertyDefinition property, LocationEntryIdStrategy.GetLocationPropertyBagDelegate getLocationPropertyBag, int index) : base(property, getLocationPropertyBag, index)
		{
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0002D9C0 File Offset: 0x0002BBC0
		internal override void SetEntryId(DefaultFolderContext context, byte[] entryId)
		{
			byte[][] entryIds = this.GetLocationPropertyBag(context).TryGetProperty(this.Property) as byte[][];
			byte[][] propertyValue = Ex12MultivalueEntryIdStrategy.CreateMultiValuedPropertyValue(entryIds, entryId, this.index, 4);
			base.SetEntryValueInternal(context, propertyValue);
		}
	}
}
