using System;
using System.Linq.Expressions;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000006 RID: 6
	public interface IClientObject<T>
	{
		// Token: 0x06000038 RID: 56
		void Load(IClientContext context, params Expression<Func<T, object>>[] retrievals);
	}
}
