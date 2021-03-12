using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B2C RID: 2860
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public abstract class WebApplication
	{
		// Token: 0x06003DAF RID: 15791 RVA: 0x000A09DC File Offset: 0x0009EBDC
		protected WebApplication(string virtualDirectory, WebSession webSession)
		{
			virtualDirectory = (virtualDirectory.EndsWith("/") ? virtualDirectory : (virtualDirectory + '/'));
			Uri baseUri = new Uri(webSession.ServiceAuthority, "/");
			this.BaseUri = new Uri(baseUri, virtualDirectory);
			this.webSession = webSession;
		}

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06003DB0 RID: 15792 RVA: 0x000A0A33 File Offset: 0x0009EC33
		// (set) Token: 0x06003DB1 RID: 15793 RVA: 0x000A0A3B File Offset: 0x0009EC3B
		public Uri BaseUri { get; private set; }

		// Token: 0x06003DB2 RID: 15794 RVA: 0x000A0A44 File Offset: 0x0009EC44
		public Cookie GetCookie(string name)
		{
			return this.GetCookies()[name];
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x000A0A52 File Offset: 0x0009EC52
		public CookieCollection GetCookies()
		{
			return this.webSession.GetCookies(this.BaseUri);
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x000A0A68 File Offset: 0x0009EC68
		public void Get<T>(string relativeUrl, Action<T> successCallback, Action<Exception> failedCallback) where T : WebApplicationResponse, new()
		{
			Uri requestUri = new Uri(this.BaseUri, relativeUrl);
			this.webSession.Get<T>(requestUri, new Func<HttpWebResponse, T>(WebApplication.GetPage<T>), successCallback, failedCallback);
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x000A0A9C File Offset: 0x0009EC9C
		public T Get<T>(string relativeUrl) where T : WebApplicationResponse, new()
		{
			Uri requestUri = new Uri(this.BaseUri, relativeUrl);
			return this.webSession.Get<T>(requestUri, new Func<HttpWebResponse, T>(WebApplication.GetPage<T>));
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x000A0AD0 File Offset: 0x0009ECD0
		public void Post<T>(string relativeUrl, RequestBody requestBody, Action<T> successCallback, Action<Exception> failedCallback) where T : WebApplicationResponse, new()
		{
			Uri requestUri = new Uri(this.BaseUri, relativeUrl);
			this.webSession.Post<T>(requestUri, requestBody, new Func<HttpWebResponse, T>(WebApplication.GetPage<T>), successCallback, failedCallback);
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x000A0B08 File Offset: 0x0009ED08
		public T Post<T>(string relativeUrl, RequestBody requestBody) where T : WebApplicationResponse, new()
		{
			Uri requestUri = new Uri(this.BaseUri, relativeUrl);
			return this.webSession.Post<T>(requestUri, requestBody, new Func<HttpWebResponse, T>(WebApplication.GetPage<T>));
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x000A0B3C File Offset: 0x0009ED3C
		private static T GetPage<T>(HttpWebResponse response) where T : WebApplicationResponse, new()
		{
			T result = Activator.CreateInstance<T>();
			result.SetResponse(response);
			return result;
		}

		// Token: 0x06003DB9 RID: 15801
		public abstract bool ValidateLogin();

		// Token: 0x040035AA RID: 13738
		private WebSession webSession;
	}
}
