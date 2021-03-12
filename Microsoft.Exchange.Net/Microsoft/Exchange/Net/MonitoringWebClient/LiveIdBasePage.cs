using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000784 RID: 1924
	internal class LiveIdBasePage
	{
		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x000503F2 File Offset: 0x0004E5F2
		// (set) Token: 0x06002635 RID: 9781 RVA: 0x000503FA File Offset: 0x0004E5FA
		public string PostUrl { get; private set; }

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x00050403 File Offset: 0x0004E603
		// (set) Token: 0x06002637 RID: 9783 RVA: 0x0005040B File Offset: 0x0004E60B
		public Uri PostUri { get; protected set; }

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002638 RID: 9784 RVA: 0x00050414 File Offset: 0x0004E614
		// (set) Token: 0x06002639 RID: 9785 RVA: 0x0005041C File Offset: 0x0004E61C
		public Dictionary<string, string> HiddenFields { get; protected set; }

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x0600263A RID: 9786 RVA: 0x00050428 File Offset: 0x0004E628
		public string HiddenFieldsString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in this.HiddenFields.Keys)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append("&");
					}
					stringBuilder.AppendFormat("{0}={1}", HttpUtility.UrlEncode(text), HttpUtility.UrlEncode(this.HiddenFields[text]));
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x000504BC File Offset: 0x0004E6BC
		protected static string GetRedirectionLocation(HttpWebResponseWrapper response)
		{
			string result = null;
			if (response != null)
			{
				result = response.Headers["Location"];
			}
			return result;
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x000504E0 File Offset: 0x0004E6E0
		protected void SetPostUrl(Uri uri, HttpWebRequestWrapper request)
		{
			this.PostUrl = uri.ToString();
			this.PostUri = uri;
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x000504F8 File Offset: 0x0004E6F8
		protected void SetPostUrl(string newValue, HttpWebRequestWrapper request)
		{
			this.PostUrl = newValue;
			Uri postUri;
			if (!Uri.TryCreate(newValue, UriKind.Absolute, out postUri))
			{
				postUri = new Uri(string.Format("https://{0}{1}", request.RequestUri.Host, newValue));
			}
			this.PostUri = postUri;
		}
	}
}
