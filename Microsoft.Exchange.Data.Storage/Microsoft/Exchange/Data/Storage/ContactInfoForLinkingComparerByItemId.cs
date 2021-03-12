using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004AB RID: 1195
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ContactInfoForLinkingComparerByItemId : IEqualityComparer<ContactInfoForLinking>
	{
		// Token: 0x06003518 RID: 13592 RVA: 0x000D6935 File Offset: 0x000D4B35
		private ContactInfoForLinkingComparerByItemId()
		{
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x000D693D File Offset: 0x000D4B3D
		public bool Equals(ContactInfoForLinking x, ContactInfoForLinking y)
		{
			return object.ReferenceEquals(x, y) || (x != null && y != null && ContactInfoForLinkingComparerByItemId.IsSameItemId(x, y));
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x000D6959 File Offset: 0x000D4B59
		public int GetHashCode(ContactInfoForLinking contact)
		{
			if (contact.ItemId == null)
			{
				return 0;
			}
			return contact.ItemId.GetHashCode();
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x000D6970 File Offset: 0x000D4B70
		private static bool IsSameItemId(ContactInfoForLinking contact1, ContactInfoForLinking contact2)
		{
			return contact1.ItemId != null && contact1.ItemId.ObjectId != null && contact2.ItemId != null && contact2.ItemId.ObjectId != null && contact1.ItemId.ObjectId.Equals(contact2.ItemId.ObjectId);
		}

		// Token: 0x04001C3D RID: 7229
		public static readonly IEqualityComparer<ContactInfoForLinking> Instance = new ContactInfoForLinkingComparerByItemId();
	}
}
