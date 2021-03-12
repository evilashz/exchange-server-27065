using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security.AntiXss;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000031 RID: 49
	public static class Utilities
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000B415 File Offset: 0x00009615
		public static string ApplicationVersion
		{
			get
			{
				return Utilities.applicationVersion;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000B41C File Offset: 0x0000961C
		public static string ImagesPath
		{
			get
			{
				return Utilities.imagesPath;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000B423 File Offset: 0x00009623
		public static string HtmlEncode(string s)
		{
			return AntiXssEncoder.HtmlEncode(s, false);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000B42C File Offset: 0x0000962C
		public static void HtmlEncode(string s, TextWriter writer)
		{
			if (s == null || s.Length == 0)
			{
				return;
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write(AntiXssEncoder.HtmlEncode(s, false));
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000B458 File Offset: 0x00009658
		internal static void HandleException(HttpContext httpContext, Exception exception, bool shouldSend440Response = false)
		{
			try
			{
				ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceError<Exception>(0L, "Live ID Auth Module Error: {0}.", exception);
				Type type = exception.GetType();
				httpContext.Response.AppendToLog(string.Format("&LiveIdError={0}", type.Name));
				if (shouldSend440Response && Utilities.Need440Response(httpContext.Request))
				{
					Utilities.Render440TimeoutResponse(httpContext.Response, httpContext.Request.HttpMethod, httpContext);
				}
				else
				{
					ErrorInformation exceptionHandlingInformation = Utilities.GetExceptionHandlingInformation(httpContext, exception);
					StringBuilder stringBuilder = new StringBuilder("/owa/auth/errorfe.aspx");
					stringBuilder.AppendFormat("?{0}={1}", "httpCode", 500);
					stringBuilder.AppendFormat("&{0}={1}", "ts", DateTime.UtcNow.ToFileTimeUtc());
					if (!AuthCommon.IsFrontEnd)
					{
						stringBuilder.AppendFormat("&{0}={1}", "be", Environment.MachineName);
					}
					if (exceptionHandlingInformation.Exception != null)
					{
						stringBuilder.AppendFormat("&{0}={1}", "authError", exceptionHandlingInformation.Exception.GetType().Name);
					}
					if (exceptionHandlingInformation.MessageId != null)
					{
						stringBuilder.AppendFormat("&{0}={1}", "msg", exceptionHandlingInformation.MessageId.Value);
						exceptionHandlingInformation.AppendMessageParametersToUrl(stringBuilder);
					}
					if (!string.IsNullOrEmpty(exceptionHandlingInformation.CustomParameterName) && !string.IsNullOrEmpty(exceptionHandlingInformation.CustomParameterValue))
					{
						stringBuilder.AppendFormat("&{0}={1}", exceptionHandlingInformation.CustomParameterName, exceptionHandlingInformation.CustomParameterValue);
					}
					if (exceptionHandlingInformation.Mode != null)
					{
						stringBuilder.AppendFormat("&{0}={1}", "m", exceptionHandlingInformation.Mode.Value);
					}
					httpContext.Response.Headers.Add("X-Auth-Error", type.FullName);
					httpContext.Response.Redirect(stringBuilder.ToString());
				}
			}
			catch (HttpException arg)
			{
				ExTraceGlobals.LiveIdAuthenticationModuleTracer.TraceError<HttpException>(0L, "Could not handle auth error: {0}.", arg);
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000B664 File Offset: 0x00009864
		internal static bool Need440Response(HttpRequest request)
		{
			return request.IsNotGetOrOehRequest();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000B66C File Offset: 0x0000986C
		internal static void Render440TimeoutResponse(HttpResponse response, string httpMethod, HttpContext httpContext)
		{
			string body = httpMethod.Equals("GET", StringComparison.OrdinalIgnoreCase) ? "<HTML><BODY>440 Login Timeout</BODY></HTML>" : "<HTML><SCRIPT>if (parent.navbar != null) parent.location = self.location;else self.location = self.location;</SCRIPT><BODY>440 Login Timeout</BODY></HTML>";
			response.TrySkipIisCustomErrors = true;
			Utilities.RenderErrorPage(response, 440, "440 Login Timeout", body, httpContext);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000B6B0 File Offset: 0x000098B0
		internal static void RenderErrorPage(HttpResponse response, int statusCode, string status, string body, HttpContext httpContext)
		{
			response.ClearHeaders();
			response.ClearContent();
			response.ContentType = "text/html";
			response.StatusCode = statusCode;
			response.Status = status;
			if (response.StatusCode == 440 && httpContext != null)
			{
				Utilities.SetCookie(httpContext, "lastResponse", 440.ToString(), null);
			}
			response.Write(body);
			response.End();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000B71C File Offset: 0x0000991C
		internal static void SetCookie(HttpContext httpContext, string cookieName, string cookieValue, string cookieDomain)
		{
			HttpCookie httpCookie = new HttpCookie(cookieName);
			httpCookie.HttpOnly = true;
			httpCookie.Path = "/";
			httpCookie.Value = cookieValue;
			if (cookieDomain != null)
			{
				httpCookie.Domain = cookieDomain;
			}
			httpContext.Response.Cookies.Add(httpCookie);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000B764 File Offset: 0x00009964
		internal static void ExecutePageAndCompleteRequest(HttpContext httpContext, string page)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			try
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder))
				{
					httpContext.Server.Execute(page, stringWriter);
				}
				httpContext.Response.Write(stringBuilder);
				httpContext.Response.StatusCode = 200;
				httpContext.Response.AppendHeader("Content-Length", httpContext.Response.Output.Encoding.GetByteCount(stringBuilder.ToString()).ToString());
				httpContext.Response.Flush();
				httpContext.ApplicationInstance.CompleteRequest();
			}
			finally
			{
				httpContext.Response.End();
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000B82C File Offset: 0x00009A2C
		private static ErrorInformation GetExceptionHandlingInformation(HttpContext httpContext, Exception exception)
		{
			ErrorInformation errorInformation = new ErrorInformation
			{
				Exception = exception
			};
			if (exception is OrgIdMailboxRecentlyCreatedException)
			{
				OrgIdMailboxRecentlyCreatedException ex = exception as OrgIdMailboxRecentlyCreatedException;
				errorInformation.Message = ex.Message;
				errorInformation.MessageId = new Strings.IDs?(ex.ErrorMessageStringId);
				errorInformation.AddMessageParameter(ex.UserName);
				errorInformation.AddMessageParameter(ex.HoursBetweenAccountCreationAndNow.ToString());
				errorInformation.Mode = ex.ErrorMode;
			}
			else if (exception is OrgIdMailboxNotFoundException)
			{
				OrgIdMailboxNotFoundException ex2 = exception as OrgIdMailboxNotFoundException;
				errorInformation.Message = ex2.Message;
				errorInformation.MessageId = new Strings.IDs?(ex2.ErrorMessageStringId);
				errorInformation.AddMessageParameter(ex2.UserName);
				errorInformation.Mode = ex2.ErrorMode;
			}
			else if (exception is OrgIdLogonException)
			{
				OrgIdLogonException ex3 = exception as OrgIdLogonException;
				errorInformation.Message = ex3.Message;
				errorInformation.MessageId = new Strings.IDs?(ex3.ErrorMessageStringId);
				errorInformation.MessageParameter = ex3.UserName;
			}
			else if (exception is AppPasswordAccessException)
			{
				AppPasswordAccessException ex4 = exception as AppPasswordAccessException;
				errorInformation.Message = ex4.Message;
				errorInformation.MessageId = new Strings.IDs?(ex4.ErrorMessageStringId);
			}
			else if (exception is LiveClientException || exception is LiveConfigurationException || exception is LiveTransientException || exception is LiveOperationException)
			{
				errorInformation.Message = exception.Message;
				errorInformation.MessageId = new Strings.IDs?(1317300008);
				string text = httpContext.Request.QueryString["realm"];
				if (!string.IsNullOrEmpty(text))
				{
					errorInformation.AddMessageParameter(text);
				}
			}
			else if (exception is AccountTerminationException)
			{
				AccountTerminationException ex5 = exception as AccountTerminationException;
				errorInformation.Message = ex5.Message;
				errorInformation.MessageId = new Strings.IDs?(ex5.ErrorMessageStringId);
				errorInformation.MessageParameter = ex5.AccountState.ToString();
			}
			return errorInformation;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000BA10 File Offset: 0x00009C10
		public static string GetImagesPath()
		{
			return Utilities.imagesPath;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000BA18 File Offset: 0x00009C18
		public static string GetUserDomain(string user)
		{
			string result = string.Empty;
			if (!string.IsNullOrEmpty(user))
			{
				int num = user.IndexOf("@");
				if (num != -1)
				{
					result = user.Substring(num + 1).Trim();
				}
			}
			return result;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000BA54 File Offset: 0x00009C54
		public static string GetOutlookDotComDomain(string hostName)
		{
			if (string.IsNullOrEmpty(hostName))
			{
				return string.Empty;
			}
			int num = hostName.LastIndexOf(".", StringComparison.InvariantCultureIgnoreCase);
			if (num < 0)
			{
				return hostName;
			}
			int num2 = hostName.LastIndexOf(".", num - 1, StringComparison.InvariantCultureIgnoreCase);
			if (num2 < 0)
			{
				return hostName;
			}
			return hostName.Substring(num2 + 1);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000BAA4 File Offset: 0x00009CA4
		public static string AppendUrlParameter(Uri uri, string name, string value)
		{
			StringBuilder stringBuilder = new StringBuilder(uri.ToString());
			if (string.IsNullOrEmpty(uri.Query))
			{
				stringBuilder.Append("?");
			}
			else
			{
				stringBuilder.Append("&");
			}
			stringBuilder.Append(name);
			stringBuilder.Append("=");
			stringBuilder.Append(HttpUtility.UrlEncode(value));
			return stringBuilder.ToString();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000BB0C File Offset: 0x00009D0C
		public static void JavascriptEncode(string s, TextWriter writer, bool escapeNonAscii)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int i = 0;
			while (i < s.Length)
			{
				char c = s[i];
				if (c <= '"')
				{
					if (c != '\n')
					{
						if (c != '\r')
						{
							switch (c)
							{
							case '!':
							case '"':
								goto IL_78;
							default:
								goto IL_B3;
							}
						}
						else
						{
							writer.Write('\\');
							writer.Write('r');
						}
					}
					else
					{
						writer.Write('\\');
						writer.Write('n');
					}
				}
				else if (c <= '/')
				{
					if (c != '\'' && c != '/')
					{
						goto IL_B3;
					}
					goto IL_78;
				}
				else
				{
					switch (c)
					{
					case '<':
					case '>':
						goto IL_78;
					case '=':
						goto IL_B3;
					default:
						if (c == '\\')
						{
							goto IL_78;
						}
						goto IL_B3;
					}
				}
				IL_E7:
				i++;
				continue;
				IL_78:
				writer.Write('\\');
				writer.Write(s[i]);
				goto IL_E7;
				IL_B3:
				if (escapeNonAscii && s[i] > '\u007f')
				{
					writer.Write("\\u{0:x4}", (ushort)s[i]);
					goto IL_E7;
				}
				writer.Write(s[i]);
				goto IL_E7;
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000BC10 File Offset: 0x00009E10
		public static void JavascriptEncode(string s, TextWriter writer)
		{
			Utilities.JavascriptEncode(s, writer, false);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000BC1C File Offset: 0x00009E1C
		public static CultureInfo GetSupportedBrowserLanguage(string language)
		{
			List<CultureInfo> list = new List<CultureInfo>(LanguagePackInfo.GetInstalledLanguagePackSpecificCultures(LanguagePackType.Client));
			string[] array = new string[list.Count];
			int num = 0;
			foreach (CultureInfo cultureInfo in list)
			{
				array[num++] = cultureInfo.Name;
			}
			Array.Sort<string>(array, StringComparer.OrdinalIgnoreCase);
			if (Array.BinarySearch<string>(array, language, StringComparer.OrdinalIgnoreCase) >= 0)
			{
				return CultureInfo.GetCultureInfo(language);
			}
			return null;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000BCB0 File Offset: 0x00009EB0
		public static string ValidateLanguageTag(string tag)
		{
			if (tag.Length < 1 || tag.Length > 44)
			{
				return null;
			}
			int num = 0;
			while (num < tag.Length && char.IsWhiteSpace(tag[num]))
			{
				num++;
			}
			if (num == tag.Length)
			{
				return null;
			}
			int num2 = num;
			for (int i = 0; i < 3; i++)
			{
				int num3 = 0;
				while (num3 < 8 && num2 < tag.Length && ((tag[num2] >= 'a' && tag[num2] <= 'z') || (tag[num2] >= 'A' && tag[num2] <= 'Z')))
				{
					num3++;
					num2++;
				}
				if (num2 == tag.Length || tag[num2] != '-')
				{
					break;
				}
				num2++;
			}
			if (num2 != tag.Length && tag[num2] != ';' && !char.IsWhiteSpace(tag[num2]))
			{
				return null;
			}
			return tag.Substring(num, num2 - num);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000BD98 File Offset: 0x00009F98
		public static string GetAccessURLFromHostnameAndRealm(string hostName, string realm, bool isVanityDomain)
		{
			string text = Uri.UriSchemeHttps;
			if (isVanityDomain)
			{
				text = Uri.UriSchemeHttp;
			}
			if (string.IsNullOrEmpty(realm))
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}://{1}/owa/", new object[]
				{
					text,
					HttpUtility.UrlEncode(hostName)
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}://{1}/owa/?realm={2}", new object[]
			{
				text,
				HttpUtility.UrlEncode(hostName),
				HttpUtility.UrlEncode(realm)
			});
		}

		// Token: 0x04000186 RID: 390
		public const string VanityDomainQueryStringParameterName = "vd";

		// Token: 0x04000187 RID: 391
		private const string HttpMethodGet = "GET";

		// Token: 0x04000188 RID: 392
		private const int ResponseStatusCode440 = 440;

		// Token: 0x04000189 RID: 393
		private const string ResponseStatus440 = "440 Login Timeout";

		// Token: 0x0400018A RID: 394
		private const string ResponseBody440Get = "<HTML><BODY>440 Login Timeout</BODY></HTML>";

		// Token: 0x0400018B RID: 395
		private const string ResponseBody440Post = "<HTML><SCRIPT>if (parent.navbar != null) parent.location = self.location;else self.location = self.location;</SCRIPT><BODY>440 Login Timeout</BODY></HTML>";

		// Token: 0x0400018C RID: 396
		internal const string LastResponseStatusCodeCookieName = "lastResponse";

		// Token: 0x0400018D RID: 397
		private const string ResponseContentTypeTextHtml = "text/html";

		// Token: 0x0400018E RID: 398
		private static readonly string applicationVersion = typeof(Utilities).GetApplicationVersion();

		// Token: 0x0400018F RID: 399
		private static readonly string imagesPath = "/LIDAuth/" + Utilities.ApplicationVersion + "/images/";

		// Token: 0x04000190 RID: 400
		public static readonly string LiveIdUrlParameter = "liveId";

		// Token: 0x04000191 RID: 401
		public static readonly string EducationUrlParameter = "newurl";

		// Token: 0x04000192 RID: 402
		public static readonly string DestinationUrlParameter = "destination";

		// Token: 0x04000193 RID: 403
		public static readonly string UserNameParameter = "username";
	}
}
