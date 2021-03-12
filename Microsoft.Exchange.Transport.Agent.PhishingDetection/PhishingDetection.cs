using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Microsoft.Exchange.Transport.Agent.PhishingDetection
{
	// Token: 0x02000002 RID: 2
	internal static class PhishingDetection
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static PhishingDetection()
		{
			PhishingDetection.LoadConfiguration();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F8 File Offset: 0x000002F8
		public static bool TenantHasPhishingEnabled(Guid tenantId)
		{
			return tenantId == Guid.Empty || PhishingDetection.TenantIds.Any((Guid id) => id.Equals(tenantId));
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002154 File Offset: 0x00000354
		public static List<KeyValuePair<string, string>> ExtractPhishingUrlsFromContent(string mailBody)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Match match = Regex.Match(mailBody, "(?is)<a.*?href\\s*=\\s*((?:\".*?\")|(?:'.*?')|[^>\\s]+).*?>(.*?)<\\/a>");
			while (match.Success)
			{
				int num = 3072 - stringBuilder.Length;
				if (match.Groups.Count >= 3)
				{
					string link = HttpUtility.UrlDecode(match.Groups[1].Value);
					string text = HttpUtility.UrlDecode(match.Groups[2].Value);
					if (link.Length + text.Length <= num && link.Length <= 2083 && text.Length <= 2083)
					{
						int num2 = text.IndexOf("<", StringComparison.InvariantCultureIgnoreCase);
						while (num2 != -1)
						{
							int num3 = text.IndexOf(">", num2, StringComparison.InvariantCultureIgnoreCase);
							if (num3 != -1)
							{
								text = text.Remove(num2, num3 - num2 + 1);
								num2 = text.IndexOf("<", StringComparison.InvariantCultureIgnoreCase);
							}
							else
							{
								IL_10D:
								while (link.StartsWith("u=", StringComparison.OrdinalIgnoreCase))
								{
									link = link.Substring(2);
								}
								if (link.StartsWith("file:///", StringComparison.OrdinalIgnoreCase))
								{
									link = link.Substring(8);
								}
								if (link.EndsWith("/", StringComparison.OrdinalIgnoreCase) || link.EndsWith(";", StringComparison.OrdinalIgnoreCase))
								{
									link = link.Substring(0, link.Length - 1);
								}
								if (text.EndsWith("/", StringComparison.OrdinalIgnoreCase) || text.EndsWith(";", StringComparison.OrdinalIgnoreCase))
								{
									text = text.Substring(0, text.Length - 1);
								}
								link = link.Replace("\"", string.Empty).Replace("'", string.Empty);
								text = text.Replace("\"", string.Empty).Replace("'", string.Empty);
								if (string.IsNullOrWhiteSpace(link) || string.IsNullOrWhiteSpace(text))
								{
									goto IL_336;
								}
								link = link.Replace("\r\n", string.Empty);
								text = text.Replace("\r\n", string.Empty);
								link = link.Replace(" ", string.Empty);
								text = text.Replace(" ", string.Empty);
								List<string> source = new List<string>
								{
									"mailto:",
									"tel:",
									"sip:",
									"mid:"
								};
								if (!source.Any((string startValue) => link.StartsWith(startValue, StringComparison.OrdinalIgnoreCase)) && !link.Equals(text, StringComparison.OrdinalIgnoreCase) && !text.StartsWith("<img", StringComparison.OrdinalIgnoreCase) && (text.IndexOf(".", StringComparison.InvariantCultureIgnoreCase) >= 0 || text.IndexOf("://", StringComparison.InvariantCultureIgnoreCase) >= 0))
								{
									stringBuilder.Append(link + "|" + text + "; ");
									goto IL_336;
								}
								goto IL_336;
							}
						}
						goto IL_10D;
					}
				}
				IL_336:
				match = match.NextMatch();
			}
			if (stringBuilder.Length == 0)
			{
				return null;
			}
			string text2 = stringBuilder.ToString();
			int num4 = PhishingDetection.KeyNames.LogKeyLength + text2.Length;
			if (num4 > 3072)
			{
				text2 = text2.Substring(0, 3072 - "...".Length) + "...";
			}
			return new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("u", text2)
			};
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002513 File Offset: 0x00000713
		public static void LogWarning(string warning)
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002518 File Offset: 0x00000718
		private static void LoadConfiguration()
		{
			string location = Assembly.GetExecutingAssembly().Location;
			string text = null;
			try
			{
				Configuration configuration = ConfigurationManager.OpenExeConfiguration(location);
				text = ((configuration.AppSettings.Settings["PhishingDetectionEnabledTenantIds"] == null) ? null : configuration.AppSettings.Settings["PhishingDetectionEnabledTenantIds"].Value);
			}
			catch (ConfigurationErrorsException)
			{
				PhishingDetection.LogWarning(string.Format(CultureInfo.InvariantCulture, "No special Tenant configuration found. Defaulting to :'{0}'.", new object[]
				{
					"5660bb4b-f6c5-47e5-af49-c13b55185dff;4171b533-24dd-48c3-9388-7e4df49fd947"
				}));
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "5660bb4b-f6c5-47e5-af49-c13b55185dff;4171b533-24dd-48c3-9388-7e4df49fd947";
			}
			string[] array = text.Split(new char[]
			{
				';'
			});
			foreach (string text2 in array)
			{
				Guid item;
				if (Guid.TryParse(text2, out item))
				{
					PhishingDetection.TenantIds.Add(item);
				}
				else
				{
					PhishingDetection.LogWarning(string.Format(CultureInfo.InvariantCulture, "Error parsing GUID:'{0}'. Please fix it on configuration file.", new object[]
					{
						text2
					}));
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private const string RegexPattern = "(?is)<a.*?href\\s*=\\s*((?:\".*?\")|(?:'.*?')|[^>\\s]+).*?>(.*?)<\\/a>";

		// Token: 0x04000002 RID: 2
		private const string PhishingDetectionEnabledTenantIdsConfig = "PhishingDetectionEnabledTenantIds";

		// Token: 0x04000003 RID: 3
		private const string DefaultPhishingDetectionEnabledTenantIds = "5660bb4b-f6c5-47e5-af49-c13b55185dff;4171b533-24dd-48c3-9388-7e4df49fd947";

		// Token: 0x04000004 RID: 4
		private const int MaxFormattedLength = 3072;

		// Token: 0x04000005 RID: 5
		private const int MaxLinkLength = 2083;

		// Token: 0x04000006 RID: 6
		private static readonly IList<Guid> TenantIds = new List<Guid>();

		// Token: 0x02000003 RID: 3
		private static class KeyNames
		{
			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000006 RID: 6 RVA: 0x0000262C File Offset: 0x0000082C
			public static int LogKeyLength
			{
				get
				{
					return "u".Length;
				}
			}

			// Token: 0x04000007 RID: 7
			public const string Url = "u";
		}
	}
}
