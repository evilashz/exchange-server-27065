using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using Microsoft.Exchange.Autodiscover;

namespace Microsoft.Exchange.AutoDiscoverV2
{
	// Token: 0x02000007 RID: 7
	internal class AutoDiscoverV2Handler : AutoDiscoverV2HandlerBase, IHttpHandler
	{
		// Token: 0x0600002B RID: 43 RVA: 0x0000293E File Offset: 0x00000B3E
		public AutoDiscoverV2Handler(RequestDetailsLogger logger)
		{
			base.Logger = logger;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000294D File Offset: 0x00000B4D
		[ExcludeFromCodeCoverage]
		public AutoDiscoverV2Handler()
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002955 File Offset: 0x00000B55
		public override string GetEmailAddressFromUrl(HttpContextBase context)
		{
			return context.Request.Params["Email"];
		}
	}
}
