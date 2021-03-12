using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Entities.People.Utilities
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class StoreContactPersonIdComparer : IComparer<IStorePropertyBag>
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00003188 File Offset: 0x00001388
		private StoreContactPersonIdComparer()
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003190 File Offset: 0x00001390
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
			PersonId valueOrDefault = x.GetValueOrDefault<PersonId>(ContactSchema.PersonId, PersonId.Create(Guid.Empty.ToByteArray()));
			PersonId valueOrDefault2 = y.GetValueOrDefault<PersonId>(ContactSchema.PersonId, PersonId.Create(Guid.Empty.ToByteArray()));
			return valueOrDefault.ToBase64String().CompareTo(valueOrDefault2.ToBase64String());
		}

		// Token: 0x0400001E RID: 30
		internal static readonly StoreContactPersonIdComparer Instance = new StoreContactPersonIdComparer();
	}
}
