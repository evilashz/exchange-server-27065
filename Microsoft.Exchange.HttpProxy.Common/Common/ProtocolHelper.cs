using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x02000009 RID: 9
	internal static class ProtocolHelper
	{
		// Token: 0x06000017 RID: 23 RVA: 0x0000298C File Offset: 0x00000B8C
		public static string GetExplicitLogonNode(string applicationPath, string filePath, ExplicitLogonNode node, out bool selectedNodeIsLast)
		{
			ArgumentValidator.ThrowIfNull("applicationPath", applicationPath);
			ArgumentValidator.ThrowIfNull("filePath", filePath);
			selectedNodeIsLast = false;
			string text = null;
			bool flag = false;
			int num = applicationPath.Length;
			if (num < filePath.Length && filePath[num] == '/')
			{
				num++;
			}
			int num2 = filePath.IndexOf('/', num);
			string text2 = (num2 == -1) ? filePath.Substring(num) : filePath.Substring(num, num2 - num);
			bool flag2 = num2 == -1;
			if (!flag2 && node == ExplicitLogonNode.Third)
			{
				int num3 = filePath.IndexOf('/', num2 + 1);
				text = ((num3 == -1) ? filePath.Substring(num2 + 1) : filePath.Substring(num2 + 1, num3 - num2 - 1));
				flag = (num3 == -1);
			}
			string result;
			switch (node)
			{
			case ExplicitLogonNode.Second:
				result = text2;
				selectedNodeIsLast = flag2;
				break;
			case ExplicitLogonNode.Third:
				result = text;
				selectedNodeIsLast = flag;
				break;
			default:
				throw new InvalidOperationException("somebody expanded ExplicitLogonNode and didn't tell TryGetExplicitLogonNode!");
			}
			return result;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002A8E File Offset: 0x00000C8E
		public static bool TryGetValidNormalizedExplicitLogonAddress(string explicitLogonAddress, out string normalizedAddress)
		{
			return ProtocolHelper.TryGetValidNormalizedExplicitLogonAddress(explicitLogonAddress, false, out normalizedAddress);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002A98 File Offset: 0x00000C98
		public static bool TryGetValidNormalizedExplicitLogonAddress(string explicitLogonAddress, bool selectedNodeIsLast, out string normalizedAddress)
		{
			normalizedAddress = null;
			if (string.IsNullOrEmpty(explicitLogonAddress))
			{
				return false;
			}
			if (selectedNodeIsLast)
			{
				return false;
			}
			normalizedAddress = explicitLogonAddress.Replace("...", ".@").Replace("..", "@");
			return SmtpAddress.IsValidSmtpAddress(normalizedAddress);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public static Uri GetClientUrlForProxy(Uri url, string explicitLogonAddress)
		{
			ArgumentValidator.ThrowIfNull("url", url);
			ArgumentValidator.ThrowIfNull("explicitLogonAddress", explicitLogonAddress);
			string text = "/" + explicitLogonAddress;
			string text2 = url.ToString();
			int num = text2.IndexOf(text);
			if (num != -1)
			{
				text2 = text2.Substring(0, num) + text2.Substring(num + text.Length);
			}
			return new Uri(text2);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002B48 File Offset: 0x00000D48
		public static bool IsODataRequest(string path)
		{
			ArgumentValidator.ThrowIfNull("path", path);
			return path.StartsWith("/odata/", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002B61 File Offset: 0x00000D61
		public static bool IsOAuthMetadataRequest(string path)
		{
			ArgumentValidator.ThrowIfNull("path", path);
			return path.IndexOf("/metadata/", StringComparison.OrdinalIgnoreCase) >= 0;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002B80 File Offset: 0x00000D80
		public static bool IsAutodiscoverV2Request(string path)
		{
			ArgumentValidator.ThrowIfNull("path", path);
			return ProtocolHelper.IsAutodiscoverV2Version1Request(path) || ProtocolHelper.IsAutodiscoverV2PreviewRequest(path);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public static bool IsAutodiscoverV2Version1Request(string path)
		{
			ArgumentValidator.ThrowIfNull("path", path);
			return path.IndexOf("/autodiscover.json/v1.0", StringComparison.OrdinalIgnoreCase) >= 0;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002BBF File Offset: 0x00000DBF
		public static bool IsAutodiscoverV2PreviewRequest(string path)
		{
			ArgumentValidator.ThrowIfNull("path", path);
			return path.IndexOf("/autodiscover.json", StringComparison.OrdinalIgnoreCase) >= 0 && path.IndexOf("/autodiscover.json/v1.0", StringComparison.OrdinalIgnoreCase) == -1;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002BED File Offset: 0x00000DED
		public static bool IsEwsODataRequest(string path)
		{
			ArgumentValidator.ThrowIfNull("path", path);
			return path.StartsWith("/ews/odata/", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002C06 File Offset: 0x00000E06
		public static bool IsEwsGetUserPhotoRequest(string path)
		{
			ArgumentValidator.ThrowIfNull("path", path);
			return path.StartsWith("/ews/exchange.asmx/s/GetUserPhoto", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002C1F File Offset: 0x00000E1F
		public static bool IsAnyWsSecurityRequest(string path)
		{
			ArgumentValidator.ThrowIfNull("path", path);
			return ProtocolHelper.IsWsSecurityRequest(path) || ProtocolHelper.IsPartnerAuthRequest(path) || ProtocolHelper.IsX509CertAuthRequest(path);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002C44 File Offset: 0x00000E44
		public static bool IsWsSecurityRequest(string path)
		{
			ArgumentValidator.ThrowIfNull("path", path);
			return path.EndsWith("wssecurity", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002C5D File Offset: 0x00000E5D
		public static bool IsPartnerAuthRequest(string path)
		{
			ArgumentValidator.ThrowIfNull("path", path);
			return path.EndsWith("wssecurity/symmetrickey", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002C76 File Offset: 0x00000E76
		public static bool IsX509CertAuthRequest(string path)
		{
			ArgumentValidator.ThrowIfNull("path", path);
			return path.EndsWith("wssecurity/x509cert", StringComparison.OrdinalIgnoreCase);
		}
	}
}
