using System;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007DB RID: 2011
	internal class MsoMainStreamCookieWithIndex
	{
		// Token: 0x060063B6 RID: 25526 RVA: 0x00159E50 File Offset: 0x00158050
		public MsoMainStreamCookieWithIndex(MsoMainStreamCookie cookie, int index)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			this.Cookie = cookie;
			this.Index = index;
		}

		// Token: 0x17002361 RID: 9057
		// (get) Token: 0x060063B7 RID: 25527 RVA: 0x00159E74 File Offset: 0x00158074
		// (set) Token: 0x060063B8 RID: 25528 RVA: 0x00159E7C File Offset: 0x0015807C
		public MsoMainStreamCookie Cookie { get; private set; }

		// Token: 0x17002362 RID: 9058
		// (get) Token: 0x060063B9 RID: 25529 RVA: 0x00159E85 File Offset: 0x00158085
		// (set) Token: 0x060063BA RID: 25530 RVA: 0x00159E8D File Offset: 0x0015808D
		public int Index { get; private set; }
	}
}
