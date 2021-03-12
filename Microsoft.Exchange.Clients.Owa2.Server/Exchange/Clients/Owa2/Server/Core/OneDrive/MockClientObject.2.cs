using System;
using System.Linq.Expressions;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000009 RID: 9
	public abstract class MockClientObject<T> : MockClientObject, IClientObject<T>
	{
		// Token: 0x0600003D RID: 61 RVA: 0x0000260D File Offset: 0x0000080D
		public void Load(IClientContext context, params Expression<Func<T, object>>[] retrievals)
		{
			((MockClientContext)context).Load(this);
		}
	}
}
