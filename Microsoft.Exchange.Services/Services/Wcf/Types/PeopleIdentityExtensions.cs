using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009F7 RID: 2551
	public static class PeopleIdentityExtensions
	{
		// Token: 0x0600480A RID: 18442 RVA: 0x00100DC7 File Offset: 0x000FEFC7
		public static INamedIdentity[] ToIdParameters(this IEnumerable<PeopleIdentity> identities)
		{
			if (identities == null || !identities.Any<PeopleIdentity>())
			{
				return null;
			}
			return (INamedIdentity[])identities.ToArray<PeopleIdentity>();
		}

		// Token: 0x0600480B RID: 18443 RVA: 0x00100DE4 File Offset: 0x000FEFE4
		public static PeopleIdentity ToPeopleIdentity(this ADRecipient entry)
		{
			if (entry != null && entry.Id != null)
			{
				return new PeopleIdentity(entry.DisplayName, entry.LegacyExchangeDN, entry.PrimarySmtpAddress.ToString(), 2, "EX");
			}
			return null;
		}

		// Token: 0x0600480C RID: 18444 RVA: 0x00100E29 File Offset: 0x000FF029
		public static PeopleIdentity ToPeopleIdentity(this ADRecipientOrAddress entry)
		{
			if (entry != null)
			{
				return new PeopleIdentity(entry.DisplayName, entry.Address, entry.Address, (entry.RoutingType == "EX") ? 2 : 3, entry.RoutingType);
			}
			return null;
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x00100E84 File Offset: 0x000FF084
		public static RecipientIdParameter[] ToRecipientIdParameters(this IEnumerable<PeopleIdentity> identities)
		{
			if (identities == null || !identities.Any<PeopleIdentity>())
			{
				return null;
			}
			return identities.Select(delegate(PeopleIdentity e)
			{
				if (!string.IsNullOrEmpty(e.SmtpAddress))
				{
					return new RecipientIdParameter(e.SmtpAddress);
				}
				return new RecipientIdParameter(e);
			}).ToArray<RecipientIdParameter>();
		}

		// Token: 0x0600480E RID: 18446 RVA: 0x00100EC3 File Offset: 0x000FF0C3
		public static IEnumerable<string> ToSmtpAddressArray(this IEnumerable<PeopleIdentity> identities)
		{
			if (identities != null && identities.Any<PeopleIdentity>())
			{
				return (from identity in identities
				select identity.SmtpAddress).ToArray<string>().Distinct<string>();
			}
			return null;
		}

		// Token: 0x0600480F RID: 18447 RVA: 0x00100F00 File Offset: 0x000FF100
		public static PeopleIdentity ToPeopleIdentity(this SmtpAddress? address, string displayName)
		{
			if (address == null)
			{
				return null;
			}
			Participant participant = new Participant(displayName, address.Value.ToString(), "SMTP");
			return new ADRecipientOrAddress(participant).ToPeopleIdentity();
		}

		// Token: 0x06004810 RID: 18448 RVA: 0x00100F4C File Offset: 0x000FF14C
		public static PeopleIdentity[] ToPeopleIdentityArray(this IEnumerable<ADObjectId> addresses)
		{
			if (addresses == null || !addresses.Any<ADObjectId>())
			{
				return null;
			}
			return (from r in RecipientObjectResolver.Instance.ResolveObjects(addresses)
			select r.ToPeopleIdentity()).ToArray<PeopleIdentity>();
		}

		// Token: 0x06004811 RID: 18449 RVA: 0x00100FD0 File Offset: 0x000FF1D0
		public static PeopleIdentity[] ToPeopleIdentityArray(this IEnumerable<ADRecipientOrAddress> addresses)
		{
			List<PeopleIdentity> list = new List<PeopleIdentity>();
			if (addresses == null || !addresses.Any<ADRecipientOrAddress>())
			{
				return null;
			}
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
			return list.ToArray();
		}
	}
}
