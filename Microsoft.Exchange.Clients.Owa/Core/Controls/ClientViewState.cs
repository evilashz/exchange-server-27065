using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x02000010 RID: 16
	public abstract class ClientViewState
	{
		// Token: 0x06000077 RID: 119
		public abstract PreFormActionResponse ToPreFormActionResponse();

		// Token: 0x06000078 RID: 120 RVA: 0x00004EFE File Offset: 0x000030FE
		public string ToQueryString()
		{
			return "?" + this.ToPreFormActionResponse().GetUrl();
		}
	}
}
