using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004C7 RID: 1223
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ContactLinkingSuggestion
	{
		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x060035AC RID: 13740 RVA: 0x000D8171 File Offset: 0x000D6371
		// (set) Token: 0x060035AD RID: 13741 RVA: 0x000D8179 File Offset: 0x000D6379
		internal PersonId PersonId { get; private set; }

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x060035AE RID: 13742 RVA: 0x000D8182 File Offset: 0x000D6382
		// (set) Token: 0x060035AF RID: 13743 RVA: 0x000D818A File Offset: 0x000D638A
		internal bool SurnameMatch { get; private set; }

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x060035B0 RID: 13744 RVA: 0x000D8193 File Offset: 0x000D6393
		// (set) Token: 0x060035B1 RID: 13745 RVA: 0x000D819B File Offset: 0x000D639B
		internal bool GivenNameMatch { get; private set; }

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x060035B2 RID: 13746 RVA: 0x000D81A4 File Offset: 0x000D63A4
		// (set) Token: 0x060035B3 RID: 13747 RVA: 0x000D81AC File Offset: 0x000D63AC
		internal bool AliasOfEmailAddressMatch { get; private set; }

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x060035B4 RID: 13748 RVA: 0x000D81B5 File Offset: 0x000D63B5
		// (set) Token: 0x060035B5 RID: 13749 RVA: 0x000D81BD File Offset: 0x000D63BD
		internal bool PhoneNumberMatch { get; private set; }

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x060035B6 RID: 13750 RVA: 0x000D81C6 File Offset: 0x000D63C6
		// (set) Token: 0x060035B7 RID: 13751 RVA: 0x000D81CE File Offset: 0x000D63CE
		internal int PartialSurnameMatch { get; private set; }

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x060035B8 RID: 13752 RVA: 0x000D81D7 File Offset: 0x000D63D7
		// (set) Token: 0x060035B9 RID: 13753 RVA: 0x000D81DF File Offset: 0x000D63DF
		internal int PartialGivenNameMatch { get; private set; }

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x060035BA RID: 13754 RVA: 0x000D81E8 File Offset: 0x000D63E8
		// (set) Token: 0x060035BB RID: 13755 RVA: 0x000D81F0 File Offset: 0x000D63F0
		internal int PartialAliasOfEmailAddressMatch { get; private set; }

		// Token: 0x060035BC RID: 13756 RVA: 0x000D81FC File Offset: 0x000D63FC
		public override string ToString()
		{
			return string.Format("Suggestion for {0}: SurnameMatch={1}, GivenNameMatch={2}, AliasOfEmailAddressMatch={3}, PhoneNumberMatch={4}, PartialSurnameMatch={5}, PartialGivenNameMatch={6}, PartialAliasOfEmailAddressMatch={7}", new object[]
			{
				this.PersonId,
				this.SurnameMatch,
				this.GivenNameMatch,
				this.AliasOfEmailAddressMatch,
				this.PhoneNumberMatch,
				this.PartialSurnameMatch,
				this.PartialGivenNameMatch,
				this.PartialAliasOfEmailAddressMatch
			});
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000D8288 File Offset: 0x000D6488
		internal static int Compare(ContactLinkingSuggestion a, ContactLinkingSuggestion b)
		{
			Util.ThrowOnNullArgument(a, "a");
			Util.ThrowOnNullArgument(b, "b");
			int num = ContactLinkingSuggestion.Compare(a.SurnameMatch && a.GivenNameMatch, b.SurnameMatch && b.GivenNameMatch);
			if (num != 0)
			{
				return num;
			}
			num = ContactLinkingSuggestion.Compare(a.AliasOfEmailAddressMatch, b.AliasOfEmailAddressMatch);
			if (num != 0)
			{
				return num;
			}
			num = ContactLinkingSuggestion.Compare(a.PhoneNumberMatch, b.PhoneNumberMatch);
			if (num != 0)
			{
				return num;
			}
			num = ContactLinkingSuggestion.Compare(a.PartialSurnameMatch, b.PartialSurnameMatch);
			if (num != 0)
			{
				return num;
			}
			num = ContactLinkingSuggestion.Compare(a.PartialGivenNameMatch, b.PartialGivenNameMatch);
			if (num != 0)
			{
				return num;
			}
			num = ContactLinkingSuggestion.Compare(a.PartialAliasOfEmailAddressMatch, b.PartialAliasOfEmailAddressMatch);
			if (num != 0)
			{
				return num;
			}
			return 0;
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000D834C File Offset: 0x000D654C
		internal static IList<ContactLinkingSuggestion> GetSuggestions(CultureInfo culture, IList<ContactInfoForSuggestion> personContacts, IEnumerable<ContactInfoForSuggestion> otherContacts)
		{
			Dictionary<PersonId, ContactLinkingSuggestion> dictionary = new Dictionary<PersonId, ContactLinkingSuggestion>();
			foreach (ContactInfoForSuggestion contactInfoForSuggestion in otherContacts)
			{
				if (!WellKnownNetworkNames.IsHiddenSourceNetworkName(contactInfoForSuggestion.PartnerNetworkId, contactInfoForSuggestion.ParentDisplayName))
				{
					foreach (ContactInfoForSuggestion personContact in personContacts)
					{
						ContactLinkingSuggestion contactLinkingSuggestion = ContactLinkingSuggestion.Create(culture, personContact, contactInfoForSuggestion);
						if (contactLinkingSuggestion != null)
						{
							ContactLinkingSuggestion b;
							if (dictionary.TryGetValue(contactLinkingSuggestion.PersonId, out b))
							{
								if (ContactLinkingSuggestion.Compare(contactLinkingSuggestion, b) > 0)
								{
									dictionary[contactLinkingSuggestion.PersonId] = contactLinkingSuggestion;
								}
							}
							else
							{
								dictionary.Add(contactLinkingSuggestion.PersonId, contactLinkingSuggestion);
							}
						}
					}
				}
			}
			List<ContactLinkingSuggestion> list = new List<ContactLinkingSuggestion>(dictionary.Values);
			list.Sort(new Comparison<ContactLinkingSuggestion>(ContactLinkingSuggestion.Compare));
			if (list.Count > ContactLinkingSuggestion.MaximumSuggestions.Value)
			{
				list.RemoveRange(ContactLinkingSuggestion.MaximumSuggestions.Value, list.Count - ContactLinkingSuggestion.MaximumSuggestions.Value);
			}
			return list;
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000D84B4 File Offset: 0x000D66B4
		internal static ContactLinkingSuggestion Create(CultureInfo culture, ContactInfoForSuggestion personContact, ContactInfoForSuggestion otherContact)
		{
			Util.ThrowOnNullArgument(culture, "culture");
			Util.ThrowOnNullArgument(personContact, "personContact");
			Util.ThrowOnNullArgument(otherContact, "otherContact");
			if (otherContact.PersonId == null)
			{
				ContactLinkingSuggestion.Tracer.TraceError<StoreId>(0L, "Cannot use contact without PersonId: {0}", otherContact.ItemId);
				return null;
			}
			if (otherContact.PersonId.Equals(personContact.PersonId))
			{
				ContactLinkingSuggestion.Tracer.TraceDebug<StoreId>(0L, "Ignoring contact {0} because it has same PersonId of the person we are looking suggestions for", otherContact.ItemId);
				return null;
			}
			if (otherContact.LinkRejectHistory != null && Array.Exists<PersonId>(otherContact.LinkRejectHistory, (PersonId otherContactLinkReject) => personContact.PersonId.Equals(otherContactLinkReject)))
			{
				ContactLinkingSuggestion.Tracer.TraceDebug<StoreId, PersonId>(0L, "Ignoring contact {0} because its LinkRejectHistory has PersonId of the person we are looking suggestions for: {1}", otherContact.ItemId, personContact.PersonId);
				return null;
			}
			if (personContact.LinkRejectHistory != null && Array.Exists<PersonId>(personContact.LinkRejectHistory, (PersonId personPersonId) => personPersonId.Equals(otherContact.PersonId)))
			{
				ContactLinkingSuggestion.Tracer.TraceDebug<StoreId, PersonId>(0L, "Ignoring contact {0} because its PersonId is present in LinkRejectHistory of the person we are looking suggestions for: {1}", otherContact.ItemId, personContact.PersonId);
				return null;
			}
			ContactLinkingSuggestion.NameCompareResult nameCompareResult = ContactLinkingSuggestion.CompareNames(culture, personContact.GivenName, personContact.Surname, otherContact.GivenName, otherContact.Surname);
			ContactLinkingSuggestion.NameCompareResult nameCompareResult2 = ContactLinkingSuggestion.CompareNames(culture, personContact.GivenName, personContact.Surname, otherContact.Surname, otherContact.GivenName);
			if (nameCompareResult.IsFullMatch || nameCompareResult2.IsFullMatch)
			{
				return new ContactLinkingSuggestion
				{
					PersonId = otherContact.PersonId,
					SurnameMatch = true,
					GivenNameMatch = true
				};
			}
			if (ContactLinkingSuggestion.IsAliasOfEmailAddressMatch(personContact, otherContact))
			{
				return new ContactLinkingSuggestion
				{
					PersonId = otherContact.PersonId,
					AliasOfEmailAddressMatch = true
				};
			}
			if (ContactLinkingSuggestion.IsPhoneNumberMatch(personContact, otherContact))
			{
				return new ContactLinkingSuggestion
				{
					PersonId = otherContact.PersonId,
					PhoneNumberMatch = true
				};
			}
			if (nameCompareResult.IsPartialMatch)
			{
				return new ContactLinkingSuggestion
				{
					PersonId = otherContact.PersonId,
					GivenNameMatch = nameCompareResult.FullGivenName,
					SurnameMatch = nameCompareResult.FullSurname,
					PartialSurnameMatch = nameCompareResult.PartialSurname,
					PartialGivenNameMatch = nameCompareResult.PartialGivenName
				};
			}
			if (nameCompareResult2.IsPartialMatch)
			{
				return new ContactLinkingSuggestion
				{
					PersonId = otherContact.PersonId,
					GivenNameMatch = nameCompareResult2.FullGivenName,
					SurnameMatch = nameCompareResult2.FullSurname,
					PartialSurnameMatch = nameCompareResult2.PartialSurname,
					PartialGivenNameMatch = nameCompareResult2.PartialGivenName
				};
			}
			int partialAliasOfEmailAddressMatch = ContactLinkingSuggestion.GetPartialAliasOfEmailAddressMatch(personContact, otherContact);
			if (partialAliasOfEmailAddressMatch > 0)
			{
				return new ContactLinkingSuggestion
				{
					PersonId = otherContact.PersonId,
					PartialAliasOfEmailAddressMatch = partialAliasOfEmailAddressMatch
				};
			}
			ContactLinkingSuggestion.Tracer.TraceDebug<StoreId>(0L, "Ignoring contact {0} because its doesn't match any criteria for suggestion.", otherContact.ItemId);
			return null;
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x000D8848 File Offset: 0x000D6A48
		private static int Compare(bool a, bool b)
		{
			return (a ? 1 : 0) - (b ? 1 : 0);
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000D8859 File Offset: 0x000D6A59
		private static int Compare(int a, int b)
		{
			return Math.Sign(a - b);
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000D8864 File Offset: 0x000D6A64
		private static int GetPartialAliasOfEmailAddressMatch(ContactInfoForSuggestion a, ContactInfoForSuggestion b)
		{
			int num = 0;
			if (a.AliasOfEmailAddresses != null && b.AliasOfEmailAddresses != null)
			{
				foreach (string a2 in a.AliasOfEmailAddresses)
				{
					foreach (string b2 in b.AliasOfEmailAddresses)
					{
						num = Math.Max(num, ContactLinkingSuggestionMatching.AliasOrEmailAddress.GetPartialMatchCount(CultureInfo.InvariantCulture, a2, b2));
					}
				}
			}
			return num;
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000D8918 File Offset: 0x000D6B18
		private static bool IsAliasOfEmailAddressMatch(ContactInfoForSuggestion a, ContactInfoForSuggestion b)
		{
			return a.AliasOfEmailAddresses != null && b.AliasOfEmailAddresses != null && a.AliasOfEmailAddresses.Overlaps(b.AliasOfEmailAddresses);
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000D893D File Offset: 0x000D6B3D
		private static bool IsPhoneNumberMatch(ContactInfoForSuggestion a, ContactInfoForSuggestion b)
		{
			return a.PhoneNumbers != null && b.PhoneNumbers != null && a.PhoneNumbers.Overlaps(b.PhoneNumbers);
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000D8964 File Offset: 0x000D6B64
		private static ContactLinkingSuggestion.NameCompareResult CompareNames(CultureInfo culture, string aGivenName, string aSurname, string bGivenName, string bSurname)
		{
			bool flag = ContactLinkingSuggestionMatching.FirstOrLastName.IsFullMatch(culture, aGivenName, bGivenName);
			bool flag2 = ContactLinkingSuggestionMatching.FirstOrLastName.IsFullMatch(culture, aSurname, bSurname);
			if (flag && flag2)
			{
				return new ContactLinkingSuggestion.NameCompareResult
				{
					FullGivenName = true,
					FullSurname = true
				};
			}
			int partialGivenName = 0;
			if (!flag)
			{
				partialGivenName = ContactLinkingSuggestionMatching.FirstOrLastName.GetPartialMatchCount(culture, aGivenName, bGivenName);
			}
			int partialSurname = 0;
			if (!flag2)
			{
				partialSurname = ContactLinkingSuggestionMatching.FirstOrLastName.GetPartialMatchCount(culture, aSurname, bSurname);
			}
			return new ContactLinkingSuggestion.NameCompareResult
			{
				FullGivenName = flag,
				FullSurname = flag2,
				PartialGivenName = partialGivenName,
				PartialSurname = partialSurname
			};
		}

		// Token: 0x04001CCC RID: 7372
		private static readonly Trace Tracer = ExTraceGlobals.ContactLinkingTracer;

		// Token: 0x04001CCD RID: 7373
		internal static readonly IntAppSettingsEntry MaximumSuggestions = new IntAppSettingsEntry("MaximumContactSuggestions", 3, ContactLinkingSuggestion.Tracer);

		// Token: 0x020004C8 RID: 1224
		private sealed class NameCompareResult
		{
			// Token: 0x170010C6 RID: 4294
			// (get) Token: 0x060035C8 RID: 13768 RVA: 0x000D8A26 File Offset: 0x000D6C26
			// (set) Token: 0x060035C9 RID: 13769 RVA: 0x000D8A2E File Offset: 0x000D6C2E
			public bool FullGivenName { get; set; }

			// Token: 0x170010C7 RID: 4295
			// (get) Token: 0x060035CA RID: 13770 RVA: 0x000D8A37 File Offset: 0x000D6C37
			// (set) Token: 0x060035CB RID: 13771 RVA: 0x000D8A3F File Offset: 0x000D6C3F
			public bool FullSurname { get; set; }

			// Token: 0x170010C8 RID: 4296
			// (get) Token: 0x060035CC RID: 13772 RVA: 0x000D8A48 File Offset: 0x000D6C48
			// (set) Token: 0x060035CD RID: 13773 RVA: 0x000D8A50 File Offset: 0x000D6C50
			public int PartialGivenName { get; set; }

			// Token: 0x170010C9 RID: 4297
			// (get) Token: 0x060035CE RID: 13774 RVA: 0x000D8A59 File Offset: 0x000D6C59
			// (set) Token: 0x060035CF RID: 13775 RVA: 0x000D8A61 File Offset: 0x000D6C61
			public int PartialSurname { get; set; }

			// Token: 0x170010CA RID: 4298
			// (get) Token: 0x060035D0 RID: 13776 RVA: 0x000D8A6A File Offset: 0x000D6C6A
			public bool IsFullMatch
			{
				get
				{
					return this.FullGivenName && this.FullSurname;
				}
			}

			// Token: 0x170010CB RID: 4299
			// (get) Token: 0x060035D1 RID: 13777 RVA: 0x000D8A7C File Offset: 0x000D6C7C
			public bool IsPartialMatch
			{
				get
				{
					return (this.FullGivenName && this.PartialSurname > 0) || (this.FullSurname && this.PartialGivenName > 0);
				}
			}
		}
	}
}
