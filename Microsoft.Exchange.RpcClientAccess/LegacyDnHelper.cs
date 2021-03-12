using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200001D RID: 29
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class LegacyDnHelper
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x000039A4 File Offset: 0x00001BA4
		public static string ConvertToLegacyDn(string address, OrganizationId organizationId, bool forcePrimaryLegacyDn)
		{
			if (address.StartsWith("/o=", StringComparison.InvariantCultureIgnoreCase))
			{
				return address;
			}
			if (address.StartsWith("EX:", StringComparison.InvariantCultureIgnoreCase))
			{
				return address.Substring("EX:".Length).Trim();
			}
			ADSessionSettings adSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId);
			ExchangePrincipal exchangePrincipal = null;
			if (address.StartsWith("SMTP:", StringComparison.InvariantCultureIgnoreCase))
			{
				Guid empty = Guid.Empty;
				string address2 = address.Substring("SMTP:".Length).Trim();
				if (SmtpAddress.IsValidSmtpAddress(address2) && SmtpProxyAddress.TryDeencapsulateExchangeGuid(address2, out empty))
				{
					exchangePrincipal = ExchangePrincipal.FromMailboxGuid(adSettings, empty, null);
				}
			}
			if (exchangePrincipal == null)
			{
				exchangePrincipal = ExchangePrincipal.FromProxyAddress(adSettings, address);
			}
			if (!forcePrimaryLegacyDn && exchangePrincipal.MailboxInfo.IsArchive)
			{
				return exchangePrincipal.LegacyDn + "/guid=" + exchangePrincipal.MailboxInfo.MailboxGuid.ToString();
			}
			return exchangePrincipal.LegacyDn;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003A80 File Offset: 0x00001C80
		public static string GetDomainAndLegacyDnFromAddress(string address, out string legacyDn)
		{
			legacyDn = null;
			if (address.StartsWith("DomDn:", StringComparison.InvariantCultureIgnoreCase))
			{
				string text = address.Substring("DomDn:".Length).Trim();
				int num = text.IndexOf(";");
				int num2 = text.Length - (num + 1);
				if (num > 0 && num2 > 0)
				{
					legacyDn = text.Substring(num + 1, num2);
					string text2 = text.Substring(0, num);
					if (SmtpAddress.IsValidDomain(text2))
					{
						return text2;
					}
				}
			}
			return null;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003AF3 File Offset: 0x00001CF3
		public static bool IsFederatedSystemAttendant(string legacyDn)
		{
			return !string.IsNullOrEmpty(legacyDn) && string.Compare(legacyDn, 0, "*/cn=Microsoft Federated System Attendant", 0, "*/cn=Microsoft Federated System Attendant".Length, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003B1A File Offset: 0x00001D1A
		public static bool IsValidClientFederatedSystemAttendant(string legacyDn)
		{
			return LegacyDnHelper.IsFederatedSystemAttendant(legacyDn) && legacyDn.Length == "*/cn=Microsoft Federated System Attendant".Length;
		}

		// Token: 0x04000047 RID: 71
		private const string LegacyDnPrefix = "/o=";

		// Token: 0x04000048 RID: 72
		private const string ExchangeLegacyDnPrefix = "EX:";

		// Token: 0x04000049 RID: 73
		private const string SmtpPrefix = "SMTP:";

		// Token: 0x0400004A RID: 74
		private const string FederatedSystemAttendantLegacyDn = "*/cn=Microsoft Federated System Attendant";

		// Token: 0x0400004B RID: 75
		public const string DomainAndLegacyDnPrefix = "DomDn:";
	}
}
