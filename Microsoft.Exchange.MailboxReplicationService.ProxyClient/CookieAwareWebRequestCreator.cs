using System;
using System.Collections.Generic;
using System.Net;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000004 RID: 4
	internal class CookieAwareWebRequestCreator : IWebRequestCreate
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000251C File Offset: 0x0000071C
		public CookieAwareWebRequestCreator()
		{
			string text = TestIntegration.Instance.RoutingCookie;
			if (string.IsNullOrEmpty(text))
			{
				text = "exchangecookie";
			}
			else if (text.Equals("Disabled", StringComparison.InvariantCultureIgnoreCase))
			{
				text = null;
			}
			if (text != null)
			{
				this.routingCookie = new Cookie(text, Guid.NewGuid().ToString("N"));
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000257B File Offset: 0x0000077B
		public bool IsDisabled
		{
			get
			{
				return this.routingCookie == null;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002588 File Offset: 0x00000788
		public WebRequest Create(Uri uri)
		{
			uri == null;
			string input = uri.AbsolutePath.Remove(0, 1);
			MRSProxyRequestContext mrsproxyRequestContext = null;
			Guid id;
			if (Guid.TryParse(input, out id))
			{
				mrsproxyRequestContext = MRSProxyRequestContext.Find(id);
			}
			if (mrsproxyRequestContext == null)
			{
				return (HttpWebRequest)WebRequest.CreateDefault(uri);
			}
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.CreateDefault(mrsproxyRequestContext.EndpointUri);
			if (!this.IsDisabled)
			{
				httpWebRequest.CookieContainer = new CookieContainer();
				httpWebRequest.CookieContainer.Add(httpWebRequest.RequestUri, this.routingCookie);
				if (mrsproxyRequestContext.BackendCookie != null)
				{
					httpWebRequest.CookieContainer.Add(httpWebRequest.RequestUri, mrsproxyRequestContext.BackendCookie);
				}
			}
			foreach (KeyValuePair<string, string> keyValuePair in mrsproxyRequestContext.HttpHeaders)
			{
				httpWebRequest.Headers.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return httpWebRequest;
		}

		// Token: 0x0400000B RID: 11
		private const string DefaultCookieName = "exchangecookie";

		// Token: 0x0400000C RID: 12
		private Cookie routingCookie;
	}
}
