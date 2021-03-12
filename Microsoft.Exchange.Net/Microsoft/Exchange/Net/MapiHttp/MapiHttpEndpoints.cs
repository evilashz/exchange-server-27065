using System;
using System.Collections.Specialized;
using System.Web;

namespace Microsoft.Exchange.Net.MapiHttp
{
	// Token: 0x02000882 RID: 2178
	public static class MapiHttpEndpoints
	{
		// Token: 0x06002E5B RID: 11867 RVA: 0x00066524 File Offset: 0x00064724
		public static string GetMailboxUrl(string fqdn, string mailboxId)
		{
			return string.Format("https://{0}{1}?{2}={3}", new object[]
			{
				fqdn,
				MapiHttpEndpoints.VdirPathEmsmdb,
				"MailboxId",
				mailboxId
			});
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x0006655B File Offset: 0x0006475B
		public static string GetMailboxUrl(string fqdn)
		{
			return string.Format("https://{0}{1}", fqdn, MapiHttpEndpoints.VdirPathEmsmdb);
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x00066570 File Offset: 0x00064770
		public static string GetAddressBookUrl(string fqdn, string mailboxId)
		{
			return string.Format("https://{0}{1}?{2}={3}", new object[]
			{
				fqdn,
				MapiHttpEndpoints.VdirPathNspi,
				"MailboxId",
				mailboxId
			});
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x000665A7 File Offset: 0x000647A7
		public static string GetAddressBookUrl(string fqdn)
		{
			return string.Format("https://{0}{1}", fqdn, MapiHttpEndpoints.VdirPathNspi);
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x000665B9 File Offset: 0x000647B9
		public static string GetClientRequestInfo(HttpContext context)
		{
			return MapiHttpEndpoints.GetClientRequestInfo(context.Request.Headers);
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x000665CC File Offset: 0x000647CC
		public static string GetClientRequestInfo(NameValueCollection headers)
		{
			return string.Concat(new string[]
			{
				"R:",
				headers.GetHeaderValue("X-RequestId"),
				";CI:",
				headers.GetHeaderValue("X-ClientInfo"),
				";RT:",
				headers.GetHeaderValue("X-RequestType")
			});
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x00066628 File Offset: 0x00064828
		private static string GetHeaderValue(this NameValueCollection headers, string header)
		{
			string text = headers[header];
			if (text == null)
			{
				return "<null>";
			}
			if (string.IsNullOrEmpty(text))
			{
				return "<empty>";
			}
			return text;
		}

		// Token: 0x0400285F RID: 10335
		private const string HeaderRequestId = "X-RequestId";

		// Token: 0x04002860 RID: 10336
		private const string HeaderClientInfo = "X-ClientInfo";

		// Token: 0x04002861 RID: 10337
		private const string HeaderRequestType = "X-RequestType";

		// Token: 0x04002862 RID: 10338
		internal const string VdirPathMapi = "/mapi/";

		// Token: 0x04002863 RID: 10339
		internal const string ParameterMailboxId = "MailboxId";

		// Token: 0x04002864 RID: 10340
		internal const string ParameterShowDebug = "ShowDebug";

		// Token: 0x04002865 RID: 10341
		internal const string ParameterShowDebugActivationValue = "yes";

		// Token: 0x04002866 RID: 10342
		internal static readonly string VdirPathEmsmdb = "/mapi/emsmdb/";

		// Token: 0x04002867 RID: 10343
		internal static readonly string VdirPathNspi = "/mapi/nspi/";
	}
}
