using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Entities.People.Utilities
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class StoreContactEmailAddressComparer : IComparer<IStorePropertyBag>
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00003124 File Offset: 0x00001324
		private StoreContactEmailAddressComparer()
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000312C File Offset: 0x0000132C
		public int Compare(IStorePropertyBag x, IStorePropertyBag y)
		{
			if (object.ReferenceEquals(x, y))
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			string valueOrDefault = x.GetValueOrDefault<string>(ContactSchema.Email1EmailAddress, string.Empty);
			string valueOrDefault2 = y.GetValueOrDefault<string>(ContactSchema.Email1EmailAddress, string.Empty);
			return StringComparer.OrdinalIgnoreCase.Compare(valueOrDefault, valueOrDefault2);
		}

		// Token: 0x0400001D RID: 29
		internal static readonly StoreContactEmailAddressComparer Instance = new StoreContactEmailAddressComparer();
	}
}
