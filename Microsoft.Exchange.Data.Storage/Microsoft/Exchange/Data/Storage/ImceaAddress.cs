using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000597 RID: 1431
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ImceaAddress
	{
		// Token: 0x06003A99 RID: 15001 RVA: 0x000F0E9C File Offset: 0x000EF09C
		public static string Encode(string type, string address, string domain)
		{
			Util.ThrowOnNullArgument(type, "type");
			Util.ThrowOnNullArgument(address, "address");
			if (address.StartsWith(type + ':', StringComparison.OrdinalIgnoreCase))
			{
				address = address.Substring(type.Length + 1).Trim();
			}
			SmtpProxyAddress smtpProxyAddress;
			if (SmtpProxyAddress.TryEncapsulate(type, address, domain, out smtpProxyAddress))
			{
				return smtpProxyAddress.SmtpAddress;
			}
			return null;
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000F0F00 File Offset: 0x000EF100
		internal static bool Decode(ref string addressType, ref string address, string imceaResolvableDomain)
		{
			if (imceaResolvableDomain == null)
			{
				return false;
			}
			int num = ImceaAddress.FindImceaDashPosition(address);
			if (num <= "IMCEA".Length)
			{
				return false;
			}
			string text = address.Substring("IMCEA".Length, num - "IMCEA".Length);
			int num2 = address.LastIndexOf('@');
			if (num2 < 0 || num2 == address.Length - 1)
			{
				return false;
			}
			string strA = address.Substring(num2 + 1, address.Length - num2 - 1);
			if (string.Compare(strA, imceaResolvableDomain, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			ProxyAddress proxyAddress = null;
			if (!SmtpProxyAddress.TryDeencapsulate(address, out proxyAddress))
			{
				return false;
			}
			addressType = text.ToUpper();
			address = proxyAddress.AddressString;
			return true;
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x000F0FA7 File Offset: 0x000EF1A7
		private static int FindImceaDashPosition(string address)
		{
			if (address == null)
			{
				return -1;
			}
			if (!address.StartsWith("IMCEA", StringComparison.OrdinalIgnoreCase))
			{
				return -1;
			}
			return address.IndexOf('-', "IMCEA".Length, address.Length - "IMCEA".Length);
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x000F0FE1 File Offset: 0x000EF1E1
		public static bool IsImceaAddress(string address)
		{
			return ImceaAddress.FindImceaDashPosition(address) >= "IMCEA".Length;
		}
	}
}
