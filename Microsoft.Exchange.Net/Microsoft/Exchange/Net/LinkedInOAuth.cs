using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x0200074D RID: 1869
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LinkedInOAuth
	{
		// Token: 0x0600247C RID: 9340 RVA: 0x0004C4FC File Offset: 0x0004A6FC
		public LinkedInOAuth(ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.tracer = tracer;
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x0004C518 File Offset: 0x0004A718
		public string GetAuthorizationHeader(string url, string httpMethod, NameValueCollection queryParameters, string accessToken, string accessSecret, string appId, string appSecret)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("url", url);
			ArgumentValidator.ThrowIfNullOrEmpty("httpMethod", httpMethod);
			ArgumentValidator.ThrowIfNullOrEmpty("appId", appId);
			ArgumentValidator.ThrowIfNullOrEmpty("appSecret", appSecret);
			Uri url2 = new Uri(url);
			NameValueCollection oauthParameters = this.GetOAuthParameters(url2, queryParameters, httpMethod, appId, appSecret, null, accessToken, accessSecret, null);
			return this.BuildOAuthHeader(oauthParameters, string.Empty);
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x0004C57C File Offset: 0x0004A77C
		private static string NormalizeRequestParameters(NameValueCollection parameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<LinkedInOAuth.QueryParameter> list = new List<LinkedInOAuth.QueryParameter>();
			foreach (string name in parameters.AllKeys)
			{
				LinkedInOAuth.QueryParameter item = new LinkedInOAuth.QueryParameter
				{
					Name = name,
					Value = parameters[name]
				};
				list.Add(item);
			}
			list.Sort(new LinkedInOAuth.QueryParameterComparer());
			for (int j = 0; j < list.Count; j++)
			{
				LinkedInOAuth.QueryParameter queryParameter = list[j];
				stringBuilder.AppendFormat("{0}={1}", queryParameter.Name, queryParameter.Value);
				if (j < list.Count - 1)
				{
					stringBuilder.Append("&");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x0004C640 File Offset: 0x0004A840
		private static string UrlEncode(string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in value)
			{
				if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~".IndexOf(c) != -1)
				{
					stringBuilder.Append(c);
				}
				else
				{
					stringBuilder.Append('%');
					stringBuilder.AppendFormat("{0:X2}", (int)c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x0004C6A8 File Offset: 0x0004A8A8
		private string CreateSignature(Uri uri, string httpMethod, NameValueCollection requestParameters, string consumerSecret, string tokenSecret)
		{
			string value = LinkedInOAuth.NormalizeUrl(uri);
			string value2 = LinkedInOAuth.NormalizeRequestParameters(requestParameters);
			string text = string.Format("{0}&{1}&{2}", httpMethod.ToUpperInvariant(), LinkedInOAuth.UrlEncode(value), LinkedInOAuth.UrlEncode(value2));
			this.tracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "LinkedIn OAuth: signature base: {0} with consumer secret (hash): {1:X8}; token secret (hash): {2:X8}", text, (consumerSecret != null) ? consumerSecret.GetHashCode() : 0, (tokenSecret != null) ? tokenSecret.GetHashCode() : 0);
			string result;
			using (HMACSHA1 hmacsha = new HMACSHA1())
			{
				hmacsha.Key = Encoding.ASCII.GetBytes(string.Format("{0}&{1}", LinkedInOAuth.UrlEncode(consumerSecret), string.IsNullOrEmpty(tokenSecret) ? "" : LinkedInOAuth.UrlEncode(tokenSecret)));
				byte[] bytes = Encoding.ASCII.GetBytes(text);
				byte[] inArray = hmacsha.ComputeHash(bytes);
				result = Convert.ToBase64String(inArray);
			}
			return result;
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x0004C790 File Offset: 0x0004A990
		private static string GetTimeStamp()
		{
			return Convert.ToInt64((DateTime.UtcNow - LinkedInOAuth.EpochUtc).TotalSeconds).ToString();
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x0004C7C4 File Offset: 0x0004A9C4
		private static string GetNonce()
		{
			Random random = new Random();
			return random.Next().ToString();
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x0004C7E8 File Offset: 0x0004A9E8
		private static string NormalizeUrl(Uri url)
		{
			string text = string.Format("{0}://{1}", url.Scheme, url.Host);
			if ((!string.Equals(url.Scheme, "http", StringComparison.OrdinalIgnoreCase) || url.Port != 80) && (!string.Equals(url.Scheme, "https", StringComparison.OrdinalIgnoreCase) || url.Port != 443))
			{
				text = text + ":" + url.Port;
			}
			return text + url.AbsolutePath;
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x0004C870 File Offset: 0x0004AA70
		internal NameValueCollection GetOAuthParameters(Uri url, NameValueCollection queryParameters, string httpMethod, string consumerKey, string consumerSecret, string tokenVerifier, string token, string tokenSecret, string callbackEndPoint)
		{
			if (url == null)
			{
				throw new ArgumentException("url");
			}
			if (string.IsNullOrEmpty(httpMethod))
			{
				throw new ArgumentNullException("httpMethod");
			}
			if (string.IsNullOrEmpty(consumerKey))
			{
				throw new ArgumentNullException("consumerKey");
			}
			if (string.IsNullOrEmpty(consumerSecret))
			{
				throw new ArgumentNullException("consumerSecret");
			}
			new NameValueCollection();
			string timeStamp = LinkedInOAuth.GetTimeStamp();
			string nonce = LinkedInOAuth.GetNonce();
			NameValueCollection nameValueCollection;
			if (queryParameters != null)
			{
				nameValueCollection = new NameValueCollection(queryParameters);
			}
			else
			{
				nameValueCollection = new NameValueCollection();
			}
			nameValueCollection.Add("oauth_timestamp", timeStamp);
			nameValueCollection.Add("oauth_nonce", nonce);
			nameValueCollection.Add("oauth_version", "1.0");
			nameValueCollection.Add("oauth_signature_method", "HMAC-SHA1");
			nameValueCollection.Add("oauth_consumer_key", consumerKey);
			if (!string.IsNullOrEmpty(tokenVerifier))
			{
				nameValueCollection.Add("oauth_verifier", tokenVerifier);
			}
			if (!string.IsNullOrEmpty(token))
			{
				nameValueCollection.Add("oauth_token", token);
			}
			if (!string.IsNullOrEmpty(callbackEndPoint))
			{
				string value = LinkedInOAuth.UrlEncode(callbackEndPoint);
				nameValueCollection.Add("oauth_callback", value);
			}
			string text = this.CreateSignature(url, httpMethod, nameValueCollection, consumerSecret, tokenSecret);
			this.tracer.TraceDebug<string>((long)this.GetHashCode(), "LinkedIn OAuth: signature computed BEFORE URL-encoding: {0}", text);
			text = LinkedInOAuth.UrlEncode(text);
			this.tracer.TraceDebug<string>((long)this.GetHashCode(), "LinkedIn OAuth: signature AFTER URL-encoding: {0}", text);
			nameValueCollection.Add("oauth_signature", text);
			return nameValueCollection;
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x0004C9D8 File Offset: 0x0004ABD8
		internal string BuildOAuthHeader(NameValueCollection oauthParameters, string realm)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("OAuth ");
			if (!string.IsNullOrEmpty(realm))
			{
				stringBuilder.Append("realm=\"" + realm + "\" ");
			}
			for (int i = 0; i < oauthParameters.Count; i++)
			{
				string key = oauthParameters.GetKey(i);
				if (key.StartsWith("oauth_"))
				{
					string value = key + "=\"" + oauthParameters[key] + "\"";
					stringBuilder.Append(value);
					if (i < oauthParameters.Count - 1)
					{
						stringBuilder.Append(",");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002227 RID: 8743
		private const string OAuthVersion = "1.0";

		// Token: 0x04002228 RID: 8744
		private const string OAuthParameterPrefix = "oauth_";

		// Token: 0x04002229 RID: 8745
		private const string OAuthConsumerKeyKey = "oauth_consumer_key";

		// Token: 0x0400222A RID: 8746
		private const string OAuthVersionKey = "oauth_version";

		// Token: 0x0400222B RID: 8747
		private const string OAuthSignatureMethodKey = "oauth_signature_method";

		// Token: 0x0400222C RID: 8748
		private const string OAuthSignatureKey = "oauth_signature";

		// Token: 0x0400222D RID: 8749
		private const string OAuthTimestampKey = "oauth_timestamp";

		// Token: 0x0400222E RID: 8750
		private const string OAuthNonceKey = "oauth_nonce";

		// Token: 0x0400222F RID: 8751
		private const string OAuthTokenKey = "oauth_token";

		// Token: 0x04002230 RID: 8752
		private const string OAuthTokenVerifier = "oauth_verifier";

		// Token: 0x04002231 RID: 8753
		private const string OAuthCallback = "oauth_callback";

		// Token: 0x04002232 RID: 8754
		private const string HMACSHA1SignatureType = "HMAC-SHA1";

		// Token: 0x04002233 RID: 8755
		private const string UnreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

		// Token: 0x04002234 RID: 8756
		private readonly ITracer tracer;

		// Token: 0x04002235 RID: 8757
		private static readonly DateTime EpochUtc = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x0200074E RID: 1870
		private class QueryParameter
		{
			// Token: 0x1700098D RID: 2445
			// (get) Token: 0x06002487 RID: 9351 RVA: 0x0004CA91 File Offset: 0x0004AC91
			// (set) Token: 0x06002488 RID: 9352 RVA: 0x0004CA99 File Offset: 0x0004AC99
			public string Name { get; set; }

			// Token: 0x1700098E RID: 2446
			// (get) Token: 0x06002489 RID: 9353 RVA: 0x0004CAA2 File Offset: 0x0004ACA2
			// (set) Token: 0x0600248A RID: 9354 RVA: 0x0004CAAA File Offset: 0x0004ACAA
			public string Value { get; set; }
		}

		// Token: 0x0200074F RID: 1871
		private class QueryParameterComparer : IComparer<LinkedInOAuth.QueryParameter>
		{
			// Token: 0x0600248C RID: 9356 RVA: 0x0004CABB File Offset: 0x0004ACBB
			public int Compare(LinkedInOAuth.QueryParameter x, LinkedInOAuth.QueryParameter y)
			{
				if (x.Name == y.Name)
				{
					return string.Compare(x.Value, y.Value);
				}
				return string.Compare(x.Name, y.Name);
			}
		}
	}
}
