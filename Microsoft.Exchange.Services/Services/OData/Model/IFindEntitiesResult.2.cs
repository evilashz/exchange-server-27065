using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EC2 RID: 3778
	public interface IFindEntitiesResult<out T> : IFindEntitiesResult, IEnumerable<T>, IEnumerable
	{
	}
}
