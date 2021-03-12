using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Web;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200004C RID: 76
	public static class UserAgentSerializer
	{
		// Token: 0x0600063B RID: 1595 RVA: 0x00017334 File Offset: 0x00015534
		static UserAgentSerializer()
		{
			string arg = "NA";
			try
			{
				arg = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
			}
			catch
			{
			}
			UserAgentSerializer.header = string.Format("Exchange\\{0}\\EDiscovery\\EWS\\", arg);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00017394 File Offset: 0x00015594
		public static string ToUserAgent(IEnumerable<KeyValuePair<string, string>> values)
		{
			string arg = string.Empty;
			StringBuilder stringBuilder = new StringBuilder(UserAgentSerializer.header);
			if (values != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in values)
				{
					stringBuilder.AppendFormat("{0}{1}={2}", arg, keyValuePair.Key, Uri.EscapeDataString(keyValuePair.Value));
					arg = "&";
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00017418 File Offset: 0x00015618
		public static IEnumerable<KeyValuePair<string, string>> FromUserAgent(string userAgent)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (!string.IsNullOrEmpty(userAgent))
			{
				string[] array = userAgent.Split(UserAgentSerializer.separator, 5);
				if (array.Length > 1)
				{
					string query = array[array.Length - 1];
					NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(query);
					for (int i = 0; i < nameValueCollection.Count; i++)
					{
						if (nameValueCollection.AllKeys[i] != null)
						{
							dictionary[nameValueCollection.AllKeys[i]] = nameValueCollection[i];
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x040001C6 RID: 454
		private const string HeaderFormat = "Exchange\\{0}\\EDiscovery\\EWS\\";

		// Token: 0x040001C7 RID: 455
		private const string Ampersand = "&";

		// Token: 0x040001C8 RID: 456
		private const int PartCount = 5;

		// Token: 0x040001C9 RID: 457
		private static readonly string header;

		// Token: 0x040001CA RID: 458
		private static char[] separator = new char[]
		{
			'\\'
		};
	}
}
