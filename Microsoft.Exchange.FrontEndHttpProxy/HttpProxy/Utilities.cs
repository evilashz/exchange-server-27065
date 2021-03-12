using System;
using System.Globalization;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000099 RID: 153
	public static class Utilities
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0001A2EC File Offset: 0x000184EC
		public static bool IsPartnerHostedOnly
		{
			get
			{
				return HttpProxyGlobals.IsPartnerHostedOnly;
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001A2F4 File Offset: 0x000184F4
		public static BrowserType GetBrowserType(string userAgent)
		{
			if (userAgent == null)
			{
				return BrowserType.Other;
			}
			string a = null;
			string text = null;
			UserAgentParser.UserAgentVersion userAgentVersion;
			UserAgentParser.Parse(userAgent, out a, out userAgentVersion, out text);
			if (string.Equals(a, "MSIE", StringComparison.OrdinalIgnoreCase))
			{
				return BrowserType.IE;
			}
			if (string.Equals(a, "Opera", StringComparison.OrdinalIgnoreCase))
			{
				return BrowserType.Opera;
			}
			if (string.Equals(a, "Safari", StringComparison.OrdinalIgnoreCase))
			{
				return BrowserType.Safari;
			}
			if (string.Equals(a, "Firefox", StringComparison.OrdinalIgnoreCase))
			{
				return BrowserType.Firefox;
			}
			if (string.Equals(a, "Chrome", StringComparison.OrdinalIgnoreCase))
			{
				return BrowserType.Chrome;
			}
			return BrowserType.Other;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001A368 File Offset: 0x00018568
		public static bool IsViet()
		{
			CultureInfo userCulture = Culture.GetUserCulture();
			return Utilities.IsViet(userCulture);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0001A381 File Offset: 0x00018581
		public static bool IsViet(CultureInfo userCulture)
		{
			if (userCulture == null)
			{
				throw new ArgumentNullException("userCulture");
			}
			return userCulture.LCID == 1066;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001A3A0 File Offset: 0x000185A0
		internal static SidAndAttributesType[] SidStringAndAttributesConverter(SidStringAndAttributes[] sidStringAndAttributesArray)
		{
			if (sidStringAndAttributesArray == null)
			{
				return null;
			}
			SidAndAttributesType[] array = new SidAndAttributesType[sidStringAndAttributesArray.Length];
			for (int i = 0; i < sidStringAndAttributesArray.Length; i++)
			{
				array[i] = new SidAndAttributesType
				{
					SecurityIdentifier = sidStringAndAttributesArray[i].SecurityIdentifier,
					Attributes = sidStringAndAttributesArray[i].Attributes
				};
			}
			return array;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001A3F0 File Offset: 0x000185F0
		internal static string FormatServerVersion(int serverVersion)
		{
			ServerVersion serverVersion2 = new ServerVersion(serverVersion);
			return string.Format(CultureInfo.InvariantCulture, "{0:d}.{1:d2}.{2:d4}.{3:d3}", new object[]
			{
				serverVersion2.Major,
				serverVersion2.Minor,
				serverVersion2.Build,
				serverVersion2.Revision
			});
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001A454 File Offset: 0x00018654
		internal static string NormalizeExchClientVer(string version)
		{
			if (string.IsNullOrWhiteSpace(version))
			{
				return version;
			}
			string[] array = version.Split(new char[]
			{
				'.'
			});
			return string.Join(".", new string[]
			{
				array[0],
				(array.Length > 1) ? array[1] : "0",
				(array.Length > 2) ? array[2] : "1",
				(array.Length > 3) ? array[3] : "0"
			});
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001A4D0 File Offset: 0x000186D0
		internal static string GetTruncatedString(string inputString, int maxLength)
		{
			if (string.IsNullOrEmpty(inputString) || maxLength <= 0)
			{
				return inputString;
			}
			if (inputString.Length <= maxLength)
			{
				return inputString;
			}
			return inputString.Substring(0, maxLength);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001A4F4 File Offset: 0x000186F4
		internal static bool TryParseDBMountedOnServerHeader(string headerValue, out Guid mdbGuid, out Fqdn serverFqdn, out int serverVersion)
		{
			mdbGuid = default(Guid);
			serverFqdn = null;
			serverVersion = 0;
			if (string.IsNullOrEmpty(headerValue))
			{
				return false;
			}
			string[] array = headerValue.Split(new char[]
			{
				'~'
			});
			return array.Length == 3 && Guid.TryParse(array[0], out mdbGuid) && Fqdn.TryParse(array[1], out serverFqdn) && int.TryParse(array[2], out serverVersion) && serverVersion != 0;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001A55C File Offset: 0x0001875C
		internal static bool TryGetSiteNameFromServerFqdn(string serverFqdn, out string siteName)
		{
			siteName = string.Empty;
			if (string.IsNullOrEmpty(serverFqdn))
			{
				throw new ArgumentNullException("serverFqdn");
			}
			string[] array = serverFqdn.Split(new char[]
			{
				'.'
			});
			if ((Utilities.IsPartnerHostedOnly || VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.SiteNameFromServerFqdnTranslation.Enabled) && array[0].Length > 5)
			{
				siteName = array[0].Substring(0, array[0].Length - 5);
				return true;
			}
			siteName = array[0];
			return true;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001A5E8 File Offset: 0x000187E8
		internal static string GetForestFqdnFromServerFqdn(string serverFqdn)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("serverFqdn", serverFqdn);
			int num = serverFqdn.IndexOf('.');
			return serverFqdn.Substring(num + 1, serverFqdn.Length - num - 1);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001A61C File Offset: 0x0001881C
		internal static ServerVersion ConvertToServerVersion(string version)
		{
			if (string.IsNullOrEmpty(version))
			{
				return null;
			}
			Version version2 = Version.Parse(version);
			return new ServerVersion(version2.Major, version2.Minor, version2.Build, version2.Revision);
		}
	}
}
