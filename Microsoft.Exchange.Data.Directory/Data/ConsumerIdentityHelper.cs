using System;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000DC RID: 220
	internal static class ConsumerIdentityHelper
	{
		// Token: 0x06000AF7 RID: 2807 RVA: 0x0003208B File Offset: 0x0003028B
		public static bool IsConsumerDomain(SmtpDomain domainName)
		{
			return domainName != null && Globals.IsDatacenter && ConsumerIdentityHelper.IsConsumerMailbox("@" + domainName.Domain);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x000320AE File Offset: 0x000302AE
		public static bool IsConsumerMailbox(string memberName)
		{
			return !string.IsNullOrEmpty(memberName) && Globals.IsDatacenter && ConsumerIdentityHelper.consumerDomainRegex.IsMatch(memberName);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000320CC File Offset: 0x000302CC
		public static bool IsConsumerMailbox(ADObjectId id)
		{
			if (id == null || id.DistinguishedName == null || !Globals.IsDatacenter)
			{
				return false;
			}
			Match match = ConsumerIdentityHelper.RDNRegex.Match(id.DistinguishedName);
			if (match.Success)
			{
				string[] array = id.DistinguishedName.Split(ConsumerIdentityHelper.OUSplitter, StringSplitOptions.None);
				return array.Length >= 2 && TemplateTenantConfiguration.IsTemplateTenantName(array[1]);
			}
			return false;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0003212A File Offset: 0x0003032A
		public static bool IsPuidBasedSecurityIdentifier(SecurityIdentifier sid)
		{
			return !(sid == null) && Globals.IsDatacenter && ConsumerIdentityHelper.SidRegex.Match(sid.ToString()).Success;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00032153 File Offset: 0x00030353
		internal static bool IsPuidBasedLegacyExchangeDN(string legacyExchangeDN)
		{
			return legacyExchangeDN != null && Globals.IsDatacenter && ConsumerIdentityHelper.LegacyExchangeDNRegex.Match(legacyExchangeDN).Success;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00032171 File Offset: 0x00030371
		internal static bool IsPuidBasedCanonicalName(string canonicalName)
		{
			return canonicalName != null && Globals.IsDatacenter && ConsumerIdentityHelper.CanonicalNameRegex.Match(canonicalName).Success;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0003218F File Offset: 0x0003038F
		public static bool IsMigratedConsumerMailbox(ADRawEntry userEntry)
		{
			return userEntry != null && Globals.IsDatacenter && ConsumerIdentityHelper.IsConsumerMailbox(userEntry.Id) && (PrimaryMailboxSourceType)userEntry[ADUserSchema.PrimaryMailboxSource] == PrimaryMailboxSourceType.Exo;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000321C0 File Offset: 0x000303C0
		public static SecurityIdentifier GetSecurityIdentifierFromPuid(ulong puid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			uint num = (uint)(puid >> 32);
			uint num2 = (uint)(puid << 32 >> 32);
			stringBuilder.AppendFormat("S-1-{0}-{1}-{2}", 2827L, num, num2);
			return new SecurityIdentifier(stringBuilder.ToString());
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00032210 File Offset: 0x00030410
		public static Guid GetExchangeGuidFromPuid(ulong puid)
		{
			uint a = (uint)(puid >> 32);
			ushort b = (ushort)(puid << 32 >> 48);
			ushort c = (ushort)puid;
			Guid result = new Guid(a, b, c, 0, 0, 0, 0, 0, 0, 0, 0);
			return result;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00032244 File Offset: 0x00030444
		public static string GetLegacyExchangeDNFromPuid(ulong puid)
		{
			string arg = string.Format("{0:X16}", puid);
			return string.Format("{0}{1}", ConsumerIdentityHelper.LegacyExchangeDNPrefix, arg);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00032274 File Offset: 0x00030474
		internal static string GetCommonNameFromPuid(ulong puid)
		{
			string arg = string.Format("{0:X16}", puid);
			return string.Format("{0}{1}", ConsumerIdentityHelper.CommonNamePrefix, arg);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000322A4 File Offset: 0x000304A4
		public static string GetDistinguishedNameFromPuid(ulong puid)
		{
			ADObjectId organizationalUnit = ADSessionSettings.FromConsumerOrganization().CurrentOrganizationId.OrganizationalUnit;
			return organizationalUnit.GetChildId(ConsumerIdentityHelper.GetCommonNameFromPuid(puid)).DistinguishedName;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000322D4 File Offset: 0x000304D4
		public static string GetExternalDirectoryObjectIdFromPuid(ulong puid)
		{
			return ConsumerIdentityHelper.GetExchangeGuidFromPuid(puid).ToString();
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000322F8 File Offset: 0x000304F8
		public static ADObjectId GetADObjectIdFromPuid(ulong puid)
		{
			Guid exchangeGuidFromPuid = ConsumerIdentityHelper.GetExchangeGuidFromPuid(puid);
			return new ADObjectId(ConsumerIdentityHelper.GetDistinguishedNameFromPuid(puid), exchangeGuidFromPuid);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00032318 File Offset: 0x00030518
		public static ADObjectId GetADObjectIdFromSmtpAddress(SmtpAddress address)
		{
			ADObjectId organizationalUnitRoot = TemplateTenantConfiguration.GetLocalTemplateTenant().OrganizationalUnitRoot;
			return new ADObjectId(organizationalUnitRoot.GetChildId(address.ToString()).DistinguishedName);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00032350 File Offset: 0x00030550
		public static bool TryGetPuidFromSecurityIdentifier(SecurityIdentifier sid, out ulong puid)
		{
			if (sid == null || !Globals.IsDatacenter)
			{
				puid = 0UL;
				return false;
			}
			string input = sid.ToString();
			Match match = ConsumerIdentityHelper.SidRegex.Match(input);
			if (!match.Success)
			{
				puid = 0UL;
				return false;
			}
			uint num = uint.Parse(match.Result("${hi}"));
			uint num2 = uint.Parse(match.Result("${lo}"));
			puid = ((ulong)num << 32) + (ulong)num2;
			return true;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x000323C4 File Offset: 0x000305C4
		public static bool TryGetPuidFromGuid(Guid guid, out ulong puid)
		{
			string input = string.Format("{0:X}", guid);
			Match match = ConsumerIdentityHelper.GuidRegex.Match(input);
			if (!match.Success || !Globals.IsDatacenter)
			{
				puid = 0UL;
				return false;
			}
			ulong num = (ulong)uint.Parse(match.Result("${a}"), NumberStyles.HexNumber);
			ulong num2 = (ulong)ushort.Parse(match.Result("${b}"), NumberStyles.HexNumber);
			ulong num3 = (ulong)ushort.Parse(match.Result("${c}"), NumberStyles.HexNumber);
			puid = (num << 32) + (num2 << 16) + num3;
			return true;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00032458 File Offset: 0x00030658
		public static bool TryGetPuidFromLegacyExchangeDN(string legacyExchangeDN, out ulong puid)
		{
			if (!string.IsNullOrEmpty(legacyExchangeDN) && Globals.IsDatacenter)
			{
				Match match = ConsumerIdentityHelper.LegacyExchangeDNRegex.Match(legacyExchangeDN);
				if (match.Success)
				{
					puid = ulong.Parse(match.Result("${a}"), NumberStyles.HexNumber);
					return true;
				}
			}
			puid = 0UL;
			return false;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x000324A8 File Offset: 0x000306A8
		public static bool TryGetPuidFromCanonicalName(string canonicalName, out ulong puid)
		{
			if (!string.IsNullOrEmpty(canonicalName) && Globals.IsDatacenter)
			{
				Match match = ConsumerIdentityHelper.CanonicalNameRegex.Match(canonicalName);
				if (match.Success)
				{
					puid = ulong.Parse(match.Result("${a}"), NumberStyles.HexNumber);
					return true;
				}
			}
			puid = 0UL;
			return false;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000324F8 File Offset: 0x000306F8
		public static bool TryGetPuidFromDN(string distinguishedName, out ulong puid)
		{
			if (!string.IsNullOrEmpty(distinguishedName) && Globals.IsDatacenter)
			{
				Match match = ConsumerIdentityHelper.RDNRegex.Match(distinguishedName);
				if (match.Success)
				{
					puid = ulong.Parse(match.Result("${a}"), NumberStyles.HexNumber);
					return true;
				}
			}
			puid = 0UL;
			return false;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00032548 File Offset: 0x00030748
		public static bool TryGetPuidFromADObjectId(ADObjectId objectId, out ulong puid)
		{
			if (objectId != null && Globals.IsDatacenter)
			{
				if (!string.IsNullOrEmpty(objectId.DistinguishedName) && ConsumerIdentityHelper.TryGetPuidFromDN(objectId.DistinguishedName, out puid))
				{
					return true;
				}
				if (objectId.ObjectGuid != Guid.Empty && ConsumerIdentityHelper.TryGetPuidFromGuid(objectId.ObjectGuid, out puid))
				{
					return true;
				}
			}
			puid = 0UL;
			return false;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000325A3 File Offset: 0x000307A3
		public static bool TryGetPuidByExternalDirectoryObjectId(string guidString, out ulong puid)
		{
			if (string.IsNullOrEmpty(guidString) || !Globals.IsDatacenter)
			{
				puid = 0UL;
				return false;
			}
			return ConsumerIdentityHelper.TryGetPuidFromGuid(new Guid(guidString), out puid);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x000325C8 File Offset: 0x000307C8
		public static bool TryGetDistinguishedNameFromPuidBasedCanonicalName(string canonicalName, out string distinguishedName)
		{
			distinguishedName = null;
			if (string.IsNullOrEmpty(canonicalName) || !Globals.IsDatacenter)
			{
				return false;
			}
			Match match = ConsumerIdentityHelper.CanonicalNameRegex.Match(canonicalName);
			if (!match.Success)
			{
				return false;
			}
			string canonicalName2 = match.Result("${cn}");
			string arg = match.Result("${a}");
			string arg2 = NativeHelpers.DistinguishedNameFromCanonicalName(canonicalName2);
			distinguishedName = string.Format("CN={0}{1},{2}", ConsumerIdentityHelper.CommonNamePrefix, arg, arg2);
			return true;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00032632 File Offset: 0x00030832
		public static ulong ConvertPuidStringToPuidNumber(string puid)
		{
			return ulong.Parse(puid, NumberStyles.HexNumber);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0003263F File Offset: 0x0003083F
		public static string ConvertPuidNumberToPuidString(ulong puidNum)
		{
			return new NetID((long)puidNum).ToString();
		}

		// Token: 0x0400044E RID: 1102
		private const long IdentifierAuthority = 2827L;

		// Token: 0x0400044F RID: 1103
		private static readonly string LegacyExchangeDNPrefix = "/o=First Organization/ou=Exchange Administrative Group(FYDIBOHF23SPDLT)/cn=Recipients/cn=";

		// Token: 0x04000450 RID: 1104
		public static readonly Regex LegacyExchangeDNRegex = new Regex("/o=First Organization/ou=Exchange Administrative Group\\(FYDIBOHF23SPDLT\\)/cn=Recipients/cn=(?<a>[0-9A-F]{16})$", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(1.0));

		// Token: 0x04000451 RID: 1105
		private static readonly Regex SidRegex = new Regex(string.Format("^S-1-{0}-(?<hi>\\d+)-(?<lo>\\d+)", 2827L), RegexOptions.Compiled, TimeSpan.FromSeconds(1.0));

		// Token: 0x04000452 RID: 1106
		private static readonly Regex CanonicalNameRegex = new Regex("^(?<cn>.*/.*/.*\\.templateTenant)/puid-(?<a>[0-9A-F]{16})$", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(1.0));

		// Token: 0x04000453 RID: 1107
		public static readonly Regex GuidRegex = new Regex("^\\{0x(?<a>[0-9a-f]{8}),0x(?<b>[0-9a-f]{4}),0x(?<c>[0-9a-f]{4}),\\{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00\\}\\}", RegexOptions.Compiled, TimeSpan.FromSeconds(1.0));

		// Token: 0x04000454 RID: 1108
		public static readonly string CommonNamePrefix = "puid-";

		// Token: 0x04000455 RID: 1109
		private static readonly Regex RDNRegex = new Regex("^CN=puid-(?<a>[0-9A-F]{16})", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(1.0));

		// Token: 0x04000456 RID: 1110
		private static readonly string[] OUSplitter = new string[]
		{
			",OU=",
			",ou="
		};

		// Token: 0x04000457 RID: 1111
		private static readonly Regex consumerDomainRegex = new Regex("^.*@(outlook\\.com|hotmail\\.com|live\\.com|outlook-int\\.com|hotmail-int\\.com|live-int\\.com)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}
}
