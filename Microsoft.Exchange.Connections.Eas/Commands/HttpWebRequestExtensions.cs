using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class HttpWebRequestExtensions
	{
		// Token: 0x06000091 RID: 145 RVA: 0x000033C4 File Offset: 0x000015C4
		public static HttpWebResponse TryGetResponse(this HttpWebRequest request)
		{
			return (HttpWebResponse)request.GetResponse();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000033D4 File Offset: 0x000015D4
		public static HttpWebResponse GetHttpResponse(this HttpWebRequest request, List<int> expectedHttpStatusCodes)
		{
			HttpWebResponse result;
			try
			{
				result = request.TryGetResponse();
			}
			catch (WebException ex)
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
				if (httpWebResponse == null || !expectedHttpStatusCodes.Contains((int)httpWebResponse.StatusCode))
				{
					throw;
				}
				result = httpWebResponse;
			}
			return result;
		}
	}
}
