using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000714 RID: 1812
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FacebookAuthenticationWebClient : IFacebookAuthenticationWebClient
	{
		// Token: 0x06002233 RID: 8755 RVA: 0x00046348 File Offset: 0x00044548
		public AuthenticateApplicationResponse AuthenticateApplication(Uri accessTokenEndpoint, TimeSpan requestTimeout)
		{
			WebRequest webRequest = WebRequest.Create(accessTokenEndpoint);
			webRequest.UseDefaultCredentials = false;
			webRequest.Timeout = (int)requestTimeout.TotalMilliseconds;
			AuthenticateApplicationResponse result;
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse())
			{
				if (httpWebResponse.ContentLength > 4096L)
				{
					throw new FacebookAuthenticationException(NetServerException.AppAuthenticationResponseTooLarge(httpWebResponse.ContentLength));
				}
				if (!"UTF-8".Equals(httpWebResponse.CharacterSet, StringComparison.OrdinalIgnoreCase))
				{
					throw new FacebookAuthenticationException(NetServerException.UnexpectedCharSetInAppAuthenticationResponse(httpWebResponse.CharacterSet));
				}
				using (Stream responseStream = httpWebResponse.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
					{
						result = new AuthenticateApplicationResponse
						{
							Code = httpWebResponse.StatusCode,
							Body = streamReader.ReadToEnd()
						};
					}
				}
			}
			return result;
		}

		// Token: 0x040020BF RID: 8383
		private const int MaxResponseLength = 4096;

		// Token: 0x040020C0 RID: 8384
		private const string AuthenticateResponseCharSet = "UTF-8";
	}
}
