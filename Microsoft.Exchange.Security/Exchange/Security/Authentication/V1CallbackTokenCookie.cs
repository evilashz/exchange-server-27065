using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000111 RID: 273
	internal class V1CallbackTokenCookie
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0003A284 File Offset: 0x00038484
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x0003A28B File Offset: 0x0003848B
		private static ExactTimeoutCache<string, byte[]> KeyCache { get; set; } = new ExactTimeoutCache<string, byte[]>(null, null, null, 25000, false);

		// Token: 0x060008EB RID: 2283 RVA: 0x0003A293 File Offset: 0x00038493
		private V1CallbackTokenCookie(HttpRequest request)
		{
			this.InitializeFromHttpRequest(request);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0003A2A4 File Offset: 0x000384A4
		private V1CallbackTokenCookie(string rawCookieValue)
		{
			this.RawCookieValue = rawCookieValue;
			using (AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider())
			{
				aesCryptoServiceProvider.GenerateKey();
				aesCryptoServiceProvider.GenerateIV();
				this.symKey = aesCryptoServiceProvider.Key;
				this.symIV = aesCryptoServiceProvider.IV;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x0003A32C File Offset: 0x0003852C
		// (set) Token: 0x060008EF RID: 2287 RVA: 0x0003A334 File Offset: 0x00038534
		public string RawCookieValue { get; private set; }

		// Token: 0x060008F0 RID: 2288 RVA: 0x0003A340 File Offset: 0x00038540
		public static bool TryGetCookieFromHttpRequest(HttpRequest request, out V1CallbackTokenCookie cookie)
		{
			cookie = null;
			bool result = false;
			try
			{
				cookie = new V1CallbackTokenCookie(request);
				result = true;
			}
			catch (ArgumentException ex)
			{
				ExTraceGlobals.OAuthTracer.TraceError<string>(0L, "[V1CallbackTokenCookie::TryGetCookieFromHttpRequest]: Failed to create V1CallbackTokenCookie Data: {0}.", ex.Message);
			}
			return result;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0003A38C File Offset: 0x0003858C
		public static void DeleteCookies(HttpRequest request, HttpResponse response)
		{
			Utility.DeleteCookie(request, response, "ExCallbackV1", null, false);
			Utility.DeleteCookie(request, response, "Key", null, false);
			Utility.DeleteCookie(request, response, "IV", null, false);
			Utility.DeleteCookie(request, response, "Sig", null, false);
			V1CallbackTokenCookie.InvalidateKeyCacheEntry(request);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0003A3CC File Offset: 0x000385CC
		public static void AddCookieToResponse(HttpContext httpContext, TokenResult tokenResult)
		{
			HttpRequest request = httpContext.Request;
			HttpResponse response = httpContext.Response;
			string text = string.Format("{0} {1}", Constants.BearerAuthenticationType, tokenResult.TokenString);
			V1CallbackTokenCookie v1CallbackTokenCookie = new V1CallbackTokenCookie(text);
			using (AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider())
			{
				aesCryptoServiceProvider.Key = v1CallbackTokenCookie.symKey;
				aesCryptoServiceProvider.IV = v1CallbackTokenCookie.symIV;
				using (ICryptoTransform cryptoTransform = aesCryptoServiceProvider.CreateEncryptor())
				{
					byte[] bytes = Encoding.Unicode.GetBytes(text);
					byte[] inArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
					V1CallbackTokenCookie.CreateAndAddHttpCookieToResponse(request, response, "ExCallbackV1", Convert.ToBase64String(inArray), tokenResult.ExpirationDate);
				}
			}
			X509Certificate2[] certificates = Utility.GetCertificates();
			RSACryptoServiceProvider rsacryptoServiceProvider = certificates[0].PublicKey.Key as RSACryptoServiceProvider;
			byte[] inArray2 = rsacryptoServiceProvider.Encrypt(v1CallbackTokenCookie.symKey, true);
			string text2 = Convert.ToBase64String(inArray2);
			byte[] inArray3 = rsacryptoServiceProvider.Encrypt(v1CallbackTokenCookie.symIV, true);
			string text3 = Convert.ToBase64String(inArray3);
			V1CallbackTokenCookie.CreateAndAddHttpCookieToResponse(request, response, "Key", text2, tokenResult.ExpirationDate);
			V1CallbackTokenCookie.CreateAndAddHttpCookieToResponse(request, response, "IV", text3, tokenResult.ExpirationDate);
			V1CallbackTokenCookie.KeyCache.TryInsertSliding(text2, v1CallbackTokenCookie.symKey, V1CallbackTokenCookie.CookieLifeTimeSpan);
			V1CallbackTokenCookie.KeyCache.TryInsertSliding(text3, v1CallbackTokenCookie.symIV, V1CallbackTokenCookie.CookieLifeTimeSpan);
			byte[] bytes2 = Encoding.Unicode.GetBytes("CallbackSignature!");
			byte[] inArray4 = rsacryptoServiceProvider.Encrypt(bytes2, true);
			V1CallbackTokenCookie.CreateAndAddHttpCookieToResponse(request, response, "Sig", Convert.ToBase64String(inArray4), tokenResult.ExpirationDate);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0003A578 File Offset: 0x00038778
		public static DateTime TryGetCookieExpiryFromContext(HttpContext httpContext)
		{
			DateTime result = DateTime.UtcNow + V1CallbackTokenCookie.CookieLifeTimeSpan;
			HttpCookie httpCookie = httpContext.Response.Cookies["ExCallbackV1"];
			if (httpCookie == null)
			{
				httpCookie = httpContext.Request.Cookies["ExCallbackV1"];
			}
			if (httpCookie != null)
			{
				result = httpCookie.Expires;
			}
			return result;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0003A5D0 File Offset: 0x000387D0
		private static void CreateAndAddHttpCookieToResponse(HttpRequest request, HttpResponse response, string name, string value, DateTime expires)
		{
			HttpCookie httpCookie = new HttpCookie(name, value);
			httpCookie.HttpOnly = true;
			httpCookie.Secure = request.IsSecureConnection;
			httpCookie.Expires = expires;
			response.Cookies.Add(httpCookie);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0003A60C File Offset: 0x0003880C
		private static void InvalidateKeyCacheEntry(HttpRequest request)
		{
			string text = null;
			if (request.Cookies["Key"] != null && request.Cookies["Key"].Value != null)
			{
				text = request.Cookies["Key"].Value;
			}
			if (!string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.OAuthTracer.TraceDebug<string>(0L, "[V1CallbackTokenCookie::Validate] Removing key cache entry with key {0}", text);
				V1CallbackTokenCookie.KeyCache.Remove(text);
			}
			string text2 = null;
			if (request.Cookies["IV"] != null && request.Cookies["IV"].Value != null)
			{
				text2 = request.Cookies["IV"].Value;
			}
			if (!string.IsNullOrEmpty(text2))
			{
				ExTraceGlobals.OAuthTracer.TraceDebug<string>(0L, "[V1CallbackTokenCookie::Validate] Removing key cache entry with key {0}", text2);
				V1CallbackTokenCookie.KeyCache.Remove(text2);
			}
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0003A6E8 File Offset: 0x000388E8
		private void InitializeFromHttpRequest(HttpRequest request)
		{
			try
			{
				string text = null;
				if (request.Cookies["ExCallbackV1"] != null && request.Cookies["ExCallbackV1"].Value != null)
				{
					text = request.Cookies["ExCallbackV1"].Value;
				}
				string text2 = null;
				if (request.Cookies["Key"] != null && request.Cookies["Key"].Value != null)
				{
					text2 = request.Cookies["Key"].Value;
				}
				string text3 = null;
				if (request.Cookies["IV"] != null && request.Cookies["IV"].Value != null)
				{
					text3 = request.Cookies["IV"].Value;
				}
				string text4 = null;
				if (request.Cookies["Sig"] != null && request.Cookies["Sig"].Value != null)
				{
					text4 = request.Cookies["Sig"].Value;
				}
				if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2) || string.IsNullOrEmpty(text3) || string.IsNullOrEmpty(text4))
				{
					ExTraceGlobals.OAuthTracer.TraceError(0L, "[V1CallbackTokenCookie::InitializeFromHttpRequest]: Unable to find all the required callback token cookies in collection");
					throw new ArgumentException("Missing cookies in collection");
				}
				byte[] array = null;
				byte[] array2 = null;
				V1CallbackTokenCookie.KeyCache.TryGetValue(text2, out array);
				V1CallbackTokenCookie.KeyCache.TryGetValue(text3, out array2);
				if (array == null || array2 == null)
				{
					bool flag = true;
					RSACryptoServiceProvider rsacryptoServiceProvider = null;
					X509Certificate2[] certificates = Utility.GetCertificates();
					foreach (X509Certificate2 x509Certificate in certificates)
					{
						try
						{
							rsacryptoServiceProvider = (x509Certificate.PrivateKey as RSACryptoServiceProvider);
							byte[] rgb = Convert.FromBase64String(text4);
							byte[] bytes = rsacryptoServiceProvider.Decrypt(rgb, true);
							string @string = Encoding.Unicode.GetString(bytes);
							if (string.Compare(@string, "CallbackSignature!", StringComparison.Ordinal) == 0)
							{
								flag = false;
								break;
							}
						}
						catch (CryptographicException arg)
						{
							ExTraceGlobals.OAuthTracer.TraceDebug<CryptographicException>(0L, "[V1CallbackTokenCookie::InitializeFromHttpRequest] Received CryptographicException {0} decrypting CookieSignature", arg);
						}
					}
					if (flag)
					{
						string text5 = "Failed to decrypt cookie using all possible certificates.";
						ExTraceGlobals.OAuthTracer.TraceError<string>(0L, "[V1CallbackTokenCookie:InitializeFromHttpRequest] - {0}", text5);
						throw new ArgumentException(text5);
					}
					byte[] rgb2 = Convert.FromBase64String(text2);
					byte[] rgb3 = Convert.FromBase64String(text3);
					try
					{
						array = rsacryptoServiceProvider.Decrypt(rgb2, true);
						array2 = rsacryptoServiceProvider.Decrypt(rgb3, true);
					}
					catch (CryptographicException ex)
					{
						ExTraceGlobals.OAuthTracer.TraceDebug<CryptographicException>(0L, "[V1CallbackTokenCookie::InitializeFromHttpRequest] Received CryptographicException {0} decrypting symKey/symIV", ex);
						throw new ArgumentException("Bad cookie value", ex);
					}
					V1CallbackTokenCookie.KeyCache.TryInsertSliding(text2, array, V1CallbackTokenCookie.CookieLifeTimeSpan);
					V1CallbackTokenCookie.KeyCache.TryInsertSliding(text3, array2, V1CallbackTokenCookie.CookieLifeTimeSpan);
				}
				byte[] bytes2 = null;
				using (AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider())
				{
					aesCryptoServiceProvider.Key = array;
					aesCryptoServiceProvider.IV = array2;
					using (ICryptoTransform cryptoTransform = aesCryptoServiceProvider.CreateDecryptor())
					{
						byte[] array4 = Convert.FromBase64String(text);
						try
						{
							bytes2 = cryptoTransform.TransformFinalBlock(array4, 0, array4.Length);
						}
						catch (CryptographicException ex2)
						{
							ExTraceGlobals.OAuthTracer.TraceDebug<CryptographicException>(0L, "[V1CallbackTokenCookie::InitializeFromHttpRequest] Received CryptographicException {0} transforming time string", ex2);
							throw new ArgumentException("Bad cookie value", ex2);
						}
					}
				}
				this.RawCookieValue = Encoding.Unicode.GetString(bytes2);
				this.symKey = array;
				this.symIV = array2;
			}
			catch (InvalidOperationException ex3)
			{
				ExTraceGlobals.OAuthTracer.TraceError<InvalidOperationException>(0L, "[V1CallbackTokenCookie::ParseHttpCookies]: Unable to parse cookies: {0}.", ex3);
				throw new ArgumentException("Bad cookie value", ex3);
			}
			catch (FormatException ex4)
			{
				ExTraceGlobals.OAuthTracer.TraceError<FormatException>(0L, "[V1CallbackTokenCookie::ParseHttpCookies]: Unable to parse cookies: {0}.", ex4);
				throw new ArgumentException("Bad cookie value", ex4);
			}
		}

		// Token: 0x0400081C RID: 2076
		private const string CookieSignature = "CallbackSignature!";

		// Token: 0x0400081D RID: 2077
		public const string CookieName = "ExCallbackV1";

		// Token: 0x0400081E RID: 2078
		public const string KeyCookieName = "Key";

		// Token: 0x0400081F RID: 2079
		public const string IVCookieName = "IV";

		// Token: 0x04000820 RID: 2080
		public const string SignatureCookieName = "Sig";

		// Token: 0x04000821 RID: 2081
		public const int KeyCacheSizeLimit = 25000;

		// Token: 0x04000822 RID: 2082
		private static readonly TimeSpan CookieLifeTimeSpan = TimeSpan.FromHours(23.0);

		// Token: 0x04000823 RID: 2083
		private byte[] symKey;

		// Token: 0x04000824 RID: 2084
		private byte[] symIV;
	}
}
