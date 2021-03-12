using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x0200003F RID: 63
	internal sealed class ProxyLoopException : LocalizedException
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x0000879A File Offset: 0x0000699A
		internal ProxyLoopException(string redirectServer) : base(new LocalizedString(redirectServer))
		{
			this.RedirectServer = redirectServer;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000087AF File Offset: 0x000069AF
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x000087B7 File Offset: 0x000069B7
		internal string RedirectServer { get; private set; }
	}
}
