using System;
using System.Linq.Expressions;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x02000007 RID: 7
	public abstract class ClientObjectWrapper<T> : IClientObject<T> where T : ClientObject
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000025DD File Offset: 0x000007DD
		protected ClientObjectWrapper(T clientObject)
		{
			this.backingClientObject = clientObject;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000025EC File Offset: 0x000007EC
		public void Load(IClientContext context, params Expression<Func<T, object>>[] retrievals)
		{
			((ClientContextWrapper)context).BackingClientContext.Load<T>(this.backingClientObject, retrievals);
		}

		// Token: 0x0400000F RID: 15
		private T backingClientObject;
	}
}
