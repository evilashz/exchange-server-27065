using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000108 RID: 264
	[Serializable]
	public class OwaCanaryException : OwaPermanentException
	{
		// Token: 0x06000994 RID: 2452 RVA: 0x000229C5 File Offset: 0x00020BC5
		public OwaCanaryException(string cookieName, string cookieValue) : base(LocalizedString.Empty)
		{
			this.CanaryCookieName = cookieName;
			this.CanaryCookieValue = cookieValue;
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x000229E5 File Offset: 0x00020BE5
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x000229ED File Offset: 0x00020BED
		public string CanaryCookieValue { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x000229F6 File Offset: 0x00020BF6
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x000229FE File Offset: 0x00020BFE
		public string CanaryCookieName { get; set; }
	}
}
