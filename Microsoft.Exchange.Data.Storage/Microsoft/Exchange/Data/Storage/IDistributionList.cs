using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002CD RID: 717
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IDistributionList : IContactBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable, IRecipientBaseCollection<DistributionListMember>, IList<DistributionListMember>, ICollection<DistributionListMember>, IEnumerable<DistributionListMember>, IEnumerable
	{
		// Token: 0x06001EAF RID: 7855
		bool Remove(IDistributionListMember item);

		// Token: 0x06001EB0 RID: 7856
		IEnumerator<IDistributionListMember> IGetEnumerator();
	}
}
