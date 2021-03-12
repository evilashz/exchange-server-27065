using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x0200000A RID: 10
	public interface IClientObjectCollection<T, T2> : IClientObject<T2>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x17000011 RID: 17
		T this[int index]
		{
			get;
		}

		// Token: 0x06000040 RID: 64
		int Count();
	}
}
