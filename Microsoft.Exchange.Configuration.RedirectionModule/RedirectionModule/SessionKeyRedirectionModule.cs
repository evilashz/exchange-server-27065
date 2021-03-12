using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Web;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.RedirectionModule
{
	// Token: 0x0200000A RID: 10
	public class SessionKeyRedirectionModule : IHttpModule
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00003A7A File Offset: 0x00001C7A
		void IHttpModule.Init(HttpApplication application)
		{
			application.PostAuthenticateRequest += SessionKeyRedirectionModule.OnPostAuthenticateRequestHandler;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003A87 File Offset: 0x00001C87
		void IHttpModule.Dispose()
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003A8C File Offset: 0x00001C8C
		private static void OnPostAuthenticateRequest(object source, EventArgs args)
		{
			HttpApplication httpApplication = (HttpApplication)source;
			HttpContext context = httpApplication.Context;
			if (!context.Request.IsAuthenticated)
			{
				Logger.LogWarning(SessionKeyRedirectionModule.traceSrc, "OnPostAuthenticateRequest was called on a not Authenticated Request!");
				return;
			}
			string tenantName = SessionKeyRedirectionModule.ResolveTenantName();
			Uri uri = RedirectionHelper.RemovePropertiesFromOriginalUri(context.Request.Url, RedirectionConfig.RedirectionUriFilterProperties);
			if (SessionKeyRedirectionModule.ShouldAddSessionKey(uri, tenantName))
			{
				SessionKeyRedirectionModule.AddSessionKey(ref uri);
				Logger.LogVerbose(SessionKeyRedirectionModule.traceSrc, "Redirecting user to {0}.", new object[]
				{
					uri
				});
				context.Response.Redirect(uri.ToString());
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003B20 File Offset: 0x00001D20
		private static string ResolveTenantName()
		{
			string text = (string)HttpContext.Current.Items["Cert-MemberOrg"];
			if (string.IsNullOrEmpty(text))
			{
				string text2 = (string)HttpContext.Current.Items["WLID-MemberName"];
				if (!string.IsNullOrEmpty(text2) && SmtpAddress.IsValidSmtpAddress(text2))
				{
					text = SmtpAddress.Parse(text2).Domain;
				}
			}
			return text;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003B8C File Offset: 0x00001D8C
		private static void AddSessionKey(ref Uri uri)
		{
			string text = uri.ToString();
			int random = SessionKeyRedirectionModule.GetRandom();
			text = string.Format("{0};{1}={2}", text, "sessionKey", random);
			UriBuilder uriBuilder = new UriBuilder(text);
			uri = uriBuilder.Uri;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003BD0 File Offset: 0x00001DD0
		private static bool ShouldAddSessionKey(Uri originalUri, string tenantName)
		{
			if (RedirectionConfig.SessionKeyCreationStatus == RedirectionConfig.SessionKeyCreation.Disable)
			{
				return false;
			}
			if (!HttpContext.Current.Request.IsSecureConnection)
			{
				return false;
			}
			NameValueCollection urlProperties = RedirectionHelper.GetUrlProperties(originalUri);
			return urlProperties["sessionKey"] == null && (RedirectionConfig.SessionKeyCreationStatus != RedirectionConfig.SessionKeyCreation.Partner || string.IsNullOrEmpty(tenantName));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003C24 File Offset: 0x00001E24
		private static int GetRandom()
		{
			byte[] array = new byte[4];
			SessionKeyRedirectionModule.randomInstance.GetBytes(array);
			return Math.Abs(BitConverter.ToInt32(array, 0)) % 999;
		}

		// Token: 0x0400002F RID: 47
		public const string SessionKeyName = "sessionKey";

		// Token: 0x04000030 RID: 48
		private static readonly EventHandler OnPostAuthenticateRequestHandler = new EventHandler(SessionKeyRedirectionModule.OnPostAuthenticateRequest);

		// Token: 0x04000031 RID: 49
		private static readonly TraceSource traceSrc = new TraceSource("SessionKeyRedirectionModule");

		// Token: 0x04000032 RID: 50
		private static RNGCryptoServiceProvider randomInstance = new RNGCryptoServiceProvider();
	}
}
