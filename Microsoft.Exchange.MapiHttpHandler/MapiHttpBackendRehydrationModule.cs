using System;
using System.Web;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200001A RID: 26
	public class MapiHttpBackendRehydrationModule : BackendRehydrationModule
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000751F File Offset: 0x0000571F
		protected override bool UseAuthIdentifierCache
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007524 File Offset: 0x00005724
		protected override bool NeedTokenRehydration(HttpContext context)
		{
			if (string.Compare(context.Request.RequestType, "POST", true) == 0)
			{
				string contentType = context.Request.ContentType;
				if (!string.IsNullOrEmpty(contentType) && (string.Compare(contentType, "application/mapi-http", true) == 0 || string.Compare(contentType, "application/octet-stream", true) == 0))
				{
					string[] values = context.Request.Headers.GetValues("X-RequestType");
					if (values != null && values.Length == 1)
					{
						return MapiHttpHandler.NeedTokenRehydration(values[0].Trim());
					}
				}
			}
			return true;
		}
	}
}
