using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002CC RID: 716
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IRecipientBaseCollection<ITEM_TYPE> : IList<ITEM_TYPE>, ICollection<ITEM_TYPE>, IEnumerable<!0>, IEnumerable where ITEM_TYPE : IRecipientBase
	{
		// Token: 0x06001EAC RID: 7852
		ITEM_TYPE Add(Participant participant);

		// Token: 0x170009B9 RID: 2489
		ITEM_TYPE this[RecipientId id]
		{
			get;
		}

		// Token: 0x06001EAE RID: 7854
		void Remove(RecipientId id);
	}
}
