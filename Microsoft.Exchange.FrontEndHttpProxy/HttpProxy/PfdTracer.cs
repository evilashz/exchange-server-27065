using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000043 RID: 67
	internal class PfdTracer
	{
		// Token: 0x06000201 RID: 513 RVA: 0x0000B130 File Offset: 0x00009330
		static PfdTracer()
		{
			PfdTracer.NotInterestedCookies.Add("ASP.NET_SessionId");
			PfdTracer.NotInterestedCookies.Add("cadata");
			PfdTracer.NotInterestedCookies.Add("cadataIV");
			PfdTracer.NotInterestedCookies.Add("cadataKey");
			PfdTracer.NotInterestedCookies.Add("cadataSig");
			PfdTracer.NotInterestedCookies.Add("cadataTTL");
			PfdTracer.NotInterestedCookies.Add("PBack");
			PfdTracer.NotInterestedHeaders.Add("Accept");
			PfdTracer.NotInterestedHeaders.Add("Accept-Encoding");
			PfdTracer.NotInterestedHeaders.Add("Accept-Language");
			PfdTracer.NotInterestedHeaders.Add("Connection");
			PfdTracer.NotInterestedHeaders.Add("Content-Length");
			PfdTracer.NotInterestedHeaders.Add("Content-Type");
			PfdTracer.NotInterestedHeaders.Add("Cookie");
			PfdTracer.NotInterestedHeaders.Add("Expect");
			PfdTracer.NotInterestedHeaders.Add("Host");
			PfdTracer.NotInterestedHeaders.Add("If-Modified-Since");
			PfdTracer.NotInterestedHeaders.Add("Proxy-Connection");
			PfdTracer.NotInterestedHeaders.Add("Range");
			PfdTracer.NotInterestedHeaders.Add("Referer");
			PfdTracer.NotInterestedHeaders.Add("Transfer-Encoding");
			PfdTracer.NotInterestedHeaders.Add("User-Agent");
			PfdTracer.NotInterestedHeaders.Add("Accept-Ranges");
			PfdTracer.NotInterestedHeaders.Add("Cache-Control");
			PfdTracer.NotInterestedHeaders.Add("ETag");
			PfdTracer.NotInterestedHeaders.Add("Last-Modified");
			PfdTracer.NotInterestedHeaders.Add("Server");
			PfdTracer.NotInterestedHeaders.Add("X-AspNet-Version");
			PfdTracer.NotInterestedHeaders.Add("X-Powered-By");
			PfdTracer.NotInterestedHeaders.Add("X-UA-Compatible");
			if (PfdTracer.PfdTraceToFile.Value)
			{
				PfdTracer.traceDirectory = Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\HttpProxy");
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000B39C File Offset: 0x0000959C
		public PfdTracer(int traceContext, int hashCode)
		{
			this.traceContext = traceContext;
			this.hashCode = hashCode;
			string text = HttpRuntime.AppDomainAppVirtualPath;
			if (string.IsNullOrEmpty(text))
			{
				text = "unknown";
			}
			else
			{
				text = text.Replace("\\", string.Empty).Replace("/", string.Empty);
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.vdir = text;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000B402 File Offset: 0x00009602
		private static bool IsTraceDisabled
		{
			get
			{
				return !PfdTracer.PfdTraceToDebugger.Value && !PfdTracer.PfdTraceToFile.Value && (!PfdTracer.traceToEtl || !ExTraceGlobals.BriefTracer.IsTraceEnabled(TraceType.PfdTrace));
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000B435 File Offset: 0x00009635
		private string TraceFilePath
		{
			get
			{
				if (this.traceFilePath == null)
				{
					this.traceFilePath = Path.Combine(PfdTracer.traceDirectory, "trace-" + this.vdir + ".log");
				}
				return this.traceFilePath;
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000B46C File Offset: 0x0000966C
		public void TraceRequest(string stage, HttpRequest request)
		{
			if (PfdTracer.IsTraceDisabled)
			{
				return;
			}
			string s = string.Format("{0}: {1}: {2} {3}", new object[]
			{
				this.traceContext,
				stage,
				request.HttpMethod,
				request.Url.ToString()
			});
			this.Write(s);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000B4C4 File Offset: 0x000096C4
		public void TraceRequest(string stage, HttpWebRequest request)
		{
			if (PfdTracer.IsTraceDisabled)
			{
				return;
			}
			string s = string.Format("{0}: {1}: {2} {3}", new object[]
			{
				this.traceContext,
				stage,
				request.Method,
				request.RequestUri.ToString()
			});
			this.Write(s);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000B51C File Offset: 0x0000971C
		public void TraceResponse(string stage, HttpResponse response)
		{
			if (PfdTracer.IsTraceDisabled)
			{
				return;
			}
			int statusCode = response.StatusCode;
			string s;
			if (statusCode == 301 || statusCode == 302 || statusCode == 303 || statusCode == 305 || statusCode == 307)
			{
				string text = response.Headers["Location"];
				s = string.Format("{0}: {1}: redirected {2} to {3}", new object[]
				{
					this.traceContext,
					stage,
					response.StatusCode,
					text ?? "null"
				});
			}
			else
			{
				s = string.Format("{0}: {1}: responds {2} {3}", new object[]
				{
					this.traceContext,
					stage,
					response.StatusCode,
					response.StatusDescription
				});
			}
			this.Write(s);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000B5FC File Offset: 0x000097FC
		public void TraceResponse(string stage, HttpWebResponse response)
		{
			if (PfdTracer.IsTraceDisabled)
			{
				return;
			}
			int statusCode = (int)response.StatusCode;
			string s;
			if (statusCode == 301 || statusCode == 302 || statusCode == 303 || statusCode == 305 || statusCode == 307)
			{
				s = string.Format("{0}: {1}: {2} redirected {3} to {4}", new object[]
				{
					this.traceContext,
					stage,
					response.Server,
					response.StatusCode,
					response.GetResponseHeader("Location")
				});
			}
			else
			{
				s = string.Format("{0}: {1}: {2} responds {3} {4}", new object[]
				{
					this.traceContext,
					stage,
					response.Server,
					response.StatusCode,
					response.StatusDescription
				});
			}
			this.Write(s);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000B6D8 File Offset: 0x000098D8
		public void TraceProxyTarget(AnchoredRoutingTarget anchor)
		{
			if (PfdTracer.IsTraceDisabled)
			{
				return;
			}
			string s = string.Format("{0}: {1}: {2}", this.traceContext, "AnchoredRoutingTarget", anchor.ToString());
			this.Write(s);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000B718 File Offset: 0x00009918
		public void TraceProxyTarget(string key, string fqdn)
		{
			if (PfdTracer.IsTraceDisabled)
			{
				return;
			}
			string s = string.Format("{0}: {1}: select BE server {2} based on {3}", new object[]
			{
				this.traceContext,
				"Cookie",
				fqdn,
				key
			});
			this.Write(s);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000B768 File Offset: 0x00009968
		public void TraceRedirect(string stage, string url)
		{
			if (PfdTracer.IsTraceDisabled)
			{
				return;
			}
			string s = string.Format("{0}: {1}: force redirect to {2}", this.traceContext, stage, url);
			this.Write(s);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000B79C File Offset: 0x0000999C
		public void TraceHeaders(string stage, WebHeaderCollection originalHeaders, WebHeaderCollection newHeaders)
		{
			if (PfdTracer.IsTraceDisabled)
			{
				return;
			}
			if (originalHeaders == null || newHeaders == null)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(20 * originalHeaders.Count);
			stringBuilder.Append(string.Format("{0}: {1}: ", this.traceContext, stage));
			PfdTracer.TraceDiffs(originalHeaders, newHeaders, PfdTracer.NotInterestedHeaders, stringBuilder);
			this.Write(stringBuilder.ToString());
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000B800 File Offset: 0x00009A00
		public void TraceHeaders(string stage, NameValueCollection originalHeaders, NameValueCollection newHeaders)
		{
			if (PfdTracer.IsTraceDisabled)
			{
				return;
			}
			if (originalHeaders == null || newHeaders == null)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(20 * originalHeaders.Count);
			stringBuilder.Append(string.Format("{0}: {1}: ", this.traceContext, stage));
			PfdTracer.TraceDiffs(originalHeaders, newHeaders, PfdTracer.NotInterestedHeaders, stringBuilder);
			this.Write(stringBuilder.ToString());
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000B864 File Offset: 0x00009A64
		public void TraceCookies(string stage, HttpCookieCollection originalCookies, CookieContainer newCookies)
		{
			if (PfdTracer.IsTraceDisabled)
			{
				return;
			}
			if (originalCookies == null || newCookies == null)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(20 * originalCookies.Count);
			stringBuilder.Append(string.Format("{0}: {1}: ", this.traceContext, stage));
			PfdTracer.TraceDiffs(originalCookies, PfdTracer.CopyCookies(newCookies), PfdTracer.NotInterestedCookies, stringBuilder);
			this.Write(stringBuilder.ToString());
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000B8CC File Offset: 0x00009ACC
		public void TraceCookies(string stage, CookieCollection originalCookies, HttpCookieCollection newCookies)
		{
			if (PfdTracer.IsTraceDisabled)
			{
				return;
			}
			if (originalCookies == null || newCookies == null)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(20 * originalCookies.Count);
			stringBuilder.Append(string.Format("{0}: {1}: ", this.traceContext, stage));
			PfdTracer.TraceDiffs(PfdTracer.CopyCookies(originalCookies), newCookies, PfdTracer.NotInterestedCookies, stringBuilder);
			this.Write(stringBuilder.ToString());
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000B934 File Offset: 0x00009B34
		private static NameValueCollection CopyCookies(CookieCollection cookies)
		{
			NameValueCollection nameValueCollection = new NameValueCollection(cookies.Count, StringComparer.OrdinalIgnoreCase);
			foreach (object obj in cookies)
			{
				Cookie cookie = (Cookie)obj;
				nameValueCollection.Add(cookie.Name, cookie.Value);
			}
			return nameValueCollection;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000B9A8 File Offset: 0x00009BA8
		private static NameValueCollection CopyCookies(CookieContainer cookies)
		{
			NameValueCollection nameValueCollection = new NameValueCollection(cookies.Count, StringComparer.OrdinalIgnoreCase);
			BindingFlags invokeAttr = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField;
			try
			{
				Hashtable hashtable = (Hashtable)cookies.GetType().InvokeMember("m_domainTable", invokeAttr, null, cookies, new object[0]);
				foreach (object obj in hashtable.Values)
				{
					SortedList sortedList = (SortedList)obj.GetType().InvokeMember("m_list", invokeAttr, null, obj, new object[0]);
					foreach (object obj2 in sortedList.Values)
					{
						CookieCollection cookieCollection = (CookieCollection)obj2;
						foreach (object obj3 in cookieCollection)
						{
							Cookie cookie = (Cookie)obj3;
							nameValueCollection.Add(cookie.Name, cookie.Value);
						}
					}
				}
			}
			catch (Exception)
			{
			}
			return nameValueCollection;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000BB40 File Offset: 0x00009D40
		private static string GetValue(object o, string key)
		{
			NameValueCollection nameValueCollection = o as NameValueCollection;
			if (nameValueCollection != null)
			{
				return nameValueCollection[key];
			}
			CookieCollection cookieCollection = o as CookieCollection;
			if (cookieCollection != null)
			{
				Cookie cookie = cookieCollection[key];
				if (cookie != null)
				{
					return cookie.Value;
				}
				return null;
			}
			else
			{
				HttpCookieCollection httpCookieCollection = o as HttpCookieCollection;
				if (httpCookieCollection == null)
				{
					return null;
				}
				HttpCookie httpCookie = httpCookieCollection[key];
				if (httpCookie != null)
				{
					return httpCookie.Value;
				}
				return null;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000BBA0 File Offset: 0x00009DA0
		private static void TraceDiffs(NameObjectCollectionBase original, NameObjectCollectionBase revised, HashSet<string> notInterestingNames, StringBuilder result)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
			foreach (object obj in original)
			{
				string text = (string)obj;
				if (!notInterestingNames.Contains(text))
				{
					string value = PfdTracer.GetValue(revised, text);
					string value2 = PfdTracer.GetValue(original, text);
					if (value == null)
					{
						result.Append("-" + text + ",");
					}
					else
					{
						hashSet.Add(text);
						if (string.Compare(value2, value, StringComparison.OrdinalIgnoreCase) != 0)
						{
							result.Append("*" + text + ",");
						}
					}
				}
			}
			foreach (object obj2 in revised)
			{
				string text2 = (string)obj2;
				if (!notInterestingNames.Contains(text2) && !hashSet.Contains(text2))
				{
					result.Append("+" + text2 + ",");
				}
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		private void Write(string s)
		{
			if (PfdTracer.PfdTraceToDebugger.Value)
			{
				bool isAttached = Debugger.IsAttached;
			}
			if (PfdTracer.traceToEtl)
			{
				ExTraceGlobals.BriefTracer.TracePfd((long)this.hashCode, s);
			}
			if (PfdTracer.PfdTraceToFile.Value)
			{
				using (StreamWriter streamWriter = new StreamWriter(this.TraceFilePath, true))
				{
					streamWriter.WriteLine(s);
				}
			}
		}

		// Token: 0x04000103 RID: 259
		public const string ClientRequest = "ClientRequest";

		// Token: 0x04000104 RID: 260
		public const string ProxyRequest = "ProxyRequest";

		// Token: 0x04000105 RID: 261
		public const string ProxyLogonRequest = "ProxyLogonRequest";

		// Token: 0x04000106 RID: 262
		public const string ClientResponse = "ClientResponse";

		// Token: 0x04000107 RID: 263
		public const string ProxyResponse = "ProxyResponse";

		// Token: 0x04000108 RID: 264
		public const string ProxyLogonResponse = "ProxyLogonResponse";

		// Token: 0x04000109 RID: 265
		public const string NeedLanguage = "EcpOwa442NeedLanguage";

		// Token: 0x0400010A RID: 266
		public const string FbaAuth = "FbaAuth";

		// Token: 0x0400010B RID: 267
		public static readonly BoolAppSettingsEntry PfdTraceToFile = new BoolAppSettingsEntry(HttpProxySettings.Prefix("PfdTraceToFile"), false, ExTraceGlobals.VerboseTracer);

		// Token: 0x0400010C RID: 268
		public static readonly BoolAppSettingsEntry PfdTraceToDebugger = new BoolAppSettingsEntry(HttpProxySettings.Prefix("PfdTraceToDebugger"), false, ExTraceGlobals.VerboseTracer);

		// Token: 0x0400010D RID: 269
		private static readonly HashSet<string> NotInterestedHeaders = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x0400010E RID: 270
		private static readonly HashSet<string> NotInterestedCookies = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x0400010F RID: 271
		private static bool traceToEtl = true;

		// Token: 0x04000110 RID: 272
		private static string traceDirectory = null;

		// Token: 0x04000111 RID: 273
		private readonly int traceContext;

		// Token: 0x04000112 RID: 274
		private readonly int hashCode;

		// Token: 0x04000113 RID: 275
		private readonly string vdir;

		// Token: 0x04000114 RID: 276
		private string traceFilePath;
	}
}
