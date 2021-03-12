using System;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive
{
	// Token: 0x0200000C RID: 12
	public class ClientResultWrapper<T> : IClientResult<T>
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002623 File Offset: 0x00000823
		public T Value
		{
			get
			{
				return this.backingClientResult.Value;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002630 File Offset: 0x00000830
		public ClientResultWrapper(ClientResult<T> result)
		{
			this.backingClientResult = result;
		}

		// Token: 0x04000010 RID: 16
		private ClientResult<T> backingClientResult;
	}
}
