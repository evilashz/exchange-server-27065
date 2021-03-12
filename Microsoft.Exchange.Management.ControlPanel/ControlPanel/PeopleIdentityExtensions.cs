using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006B6 RID: 1718
	public static class PeopleIdentityExtensions
	{
		// Token: 0x06004917 RID: 18711 RVA: 0x000DF758 File Offset: 0x000DD958
		public static INamedIdentity[] ToIdParameters(this IEnumerable<PeopleIdentity> identities)
		{
			if (identities == null || !identities.Any<PeopleIdentity>())
			{
				return null;
			}
			return (INamedIdentity[])identities.ToArray<PeopleIdentity>();
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x000DF774 File Offset: 0x000DD974
		public static PeopleIdentity ToPeopleIdentity(this ADRecipient entry)
		{
			if (entry != null && entry.Id != null)
			{
				return new PeopleIdentity(entry.DisplayName, entry.LegacyExchangeDN, entry.PrimarySmtpAddress.ToString(), 2, RbacPrincipal.Current.IsInRole("FFO") ? "SMTP" : "EX", (int)entry.RecipientType.ToRecipientAddressFlag());
			}
			return null;
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x000DF7DC File Offset: 0x000DD9DC
		public static PeopleIdentity ToPeopleIdentity(this PeopleRecipientObject entry)
		{
			if (entry != null)
			{
				return new PeopleIdentity(entry.DisplayName, entry.LegacyExchangeDN, entry.PrimarySmtpAddress, 2, "EX", (int)entry.RecipientType.ToRecipientAddressFlag());
			}
			return null;
		}

		// Token: 0x0600491A RID: 18714 RVA: 0x000DF80B File Offset: 0x000DDA0B
		public static PeopleIdentity ToPeopleIdentity(this ADRecipientOrAddress entry)
		{
			if (entry != null)
			{
				return new PeopleIdentity(entry.DisplayName, entry.Address, entry.Address, (entry.RoutingType == "EX") ? 2 : 3, entry.RoutingType, 0);
			}
			return null;
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x000DF84E File Offset: 0x000DDA4E
		public static IEnumerable<string> ToSMTPAddressArray(this IEnumerable<PeopleIdentity> identities)
		{
			if (identities != null && identities.Any<PeopleIdentity>())
			{
				return (from identity in identities
				select identity.SMTPAddress).ToArray<string>().Distinct<string>();
			}
			return null;
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x000DF892 File Offset: 0x000DDA92
		public static PeopleIdentity[] ToPeopleIdentityArray(this IEnumerable<ADRecipient> identities)
		{
			if (identities != null && identities.Any<ADRecipient>())
			{
				return (from identity in identities
				select identity.ToPeopleIdentity()).ToArray<PeopleIdentity>();
			}
			return null;
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x000DF8CC File Offset: 0x000DDACC
		public static RecipientAddressFlags ToRecipientAddressFlag(this RecipientType type)
		{
			RecipientAddressFlags result = RecipientAddressFlags.None;
			if (type == RecipientType.MailUniversalDistributionGroup || type == RecipientType.MailUniversalSecurityGroup || type == RecipientType.MailNonUniversalGroup || type == RecipientType.DynamicDistributionGroup)
			{
				result = RecipientAddressFlags.DistributionList;
			}
			return result;
		}

		// Token: 0x0600491E RID: 18718 RVA: 0x000DF8F0 File Offset: 0x000DDAF0
		public static PeopleIdentity[] ToPeopleIdentityArray(this IEnumerable<ADIdParameter> identities)
		{
			IEnumerable<ADRecipientOrAddress> enumerable = identities.ToADRecipientOrAddresses();
			if (enumerable != null && enumerable.Any<ADRecipientOrAddress>())
			{
				return enumerable.ToArray<ADRecipientOrAddress>().ToPeopleIdentityArray();
			}
			return null;
		}

		// Token: 0x0600491F RID: 18719 RVA: 0x000DF91C File Offset: 0x000DDB1C
		public static PeopleIdentity ToPeopleIdentity(this SmtpAddress? address)
		{
			ADRecipientOrAddress adrecipientOrAddress = address.ToADRecipientOrAddress();
			if (adrecipientOrAddress != null)
			{
				return adrecipientOrAddress.ToPeopleIdentity();
			}
			return null;
		}

		// Token: 0x06004920 RID: 18720 RVA: 0x000DF943 File Offset: 0x000DDB43
		public static PeopleIdentity[] ToPeopleIdentityArray(this IEnumerable<PeopleRecipientObject> identities)
		{
			if (identities != null && identities.Any<PeopleRecipientObject>())
			{
				return (from identity in identities
				select identity.ToPeopleIdentity()).ToArray<PeopleIdentity>();
			}
			return null;
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x000DF9B0 File Offset: 0x000DDBB0
		public static PeopleIdentity[] ToPeopleIdentityArray(this ADRecipientOrAddress[] addresses)
		{
			List<PeopleIdentity> list = new List<PeopleIdentity>();
			if (addresses != null && addresses.Any<ADRecipientOrAddress>())
			{
				Dictionary<string, ADRecipient> dictionary = new Dictionary<string, ADRecipient>(StringComparer.OrdinalIgnoreCase);
				IEnumerable<string> legacyDNs = from address in addresses
				where address.RoutingType == "EX"
				select address.Address;
				IEnumerable<ADRecipient> enumerable = RecipientObjectResolver.Instance.ResolveLegacyDNs(legacyDNs);
				if (enumerable != null && enumerable.Any<ADRecipient>())
				{
					foreach (ADRecipient adrecipient in enumerable)
					{
						if (!dictionary.ContainsKey(adrecipient.LegacyExchangeDN))
						{
							dictionary.Add(adrecipient.LegacyExchangeDN, adrecipient);
						}
					}
				}
				IEnumerable<string> addresses2 = from address in addresses
				where address.RoutingType == "SMTP"
				select address.Address;
				enumerable = RecipientObjectResolver.Instance.ResolveSmtpAddress(addresses2);
				if (enumerable != null && enumerable.Any<ADRecipient>())
				{
					foreach (ADRecipient adrecipient2 in enumerable)
					{
						string key = adrecipient2.PrimarySmtpAddress.ToString();
						if (!dictionary.ContainsKey(key))
						{
							dictionary.Add(key, adrecipient2);
						}
					}
				}
				foreach (ADRecipientOrAddress adrecipientOrAddress in addresses)
				{
					ADRecipient entry = null;
					if (dictionary.TryGetValue(adrecipientOrAddress.Address, out entry))
					{
						list.Add(entry.ToPeopleIdentity());
					}
					else
					{
						list.Add(adrecipientOrAddress.ToPeopleIdentity());
					}
				}
			}
			if (!list.Any<PeopleIdentity>())
			{
				return null;
			}
			return list.ToArray();
		}
	}
}
