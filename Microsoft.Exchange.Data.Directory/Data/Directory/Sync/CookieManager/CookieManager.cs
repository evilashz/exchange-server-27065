using System;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007CC RID: 1996
	internal abstract class CookieManager
	{
		// Token: 0x06006349 RID: 25417
		public abstract byte[] ReadCookie();

		// Token: 0x0600634A RID: 25418
		public abstract void WriteCookie(byte[] cookie, DateTime timestamp);

		// Token: 0x1700234B RID: 9035
		// (get) Token: 0x0600634B RID: 25419
		public abstract string DomainController { get; }

		// Token: 0x0600634C RID: 25420 RVA: 0x00158A20 File Offset: 0x00156C20
		public virtual DateTime? GetMostRecentCookieTimestamp()
		{
			return null;
		}
	}
}
