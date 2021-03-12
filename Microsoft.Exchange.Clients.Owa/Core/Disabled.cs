using System;
using System.Net;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000103 RID: 259
	public class Disabled : OwaPage
	{
		// Token: 0x0600088E RID: 2190 RVA: 0x0003F2D1 File Offset: 0x0003D4D1
		protected override void OnLoad(EventArgs e)
		{
			Utilities.EndResponse(this.Context, HttpStatusCode.Forbidden);
		}
	}
}
