using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Entities.People.Utilities
{
	// Token: 0x02000007 RID: 7
	public interface IContactDeltaCalculator<T, K>
	{
		// Token: 0x0600002B RID: 43
		ICollection<Tuple<K, object>> CalculateDelta(T objectSource, T objectTarget);
	}
}
