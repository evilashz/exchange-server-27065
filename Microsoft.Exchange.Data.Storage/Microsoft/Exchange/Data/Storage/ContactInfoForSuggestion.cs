using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004B0 RID: 1200
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ContactInfoForSuggestion
	{
		// Token: 0x0600353C RID: 13628 RVA: 0x000D6F89 File Offset: 0x000D5189
		internal ContactInfoForSuggestion()
		{
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x0600353D RID: 13629 RVA: 0x000D6F91 File Offset: 0x000D5191
		// (set) Token: 0x0600353E RID: 13630 RVA: 0x000D6F99 File Offset: 0x000D5199
		internal PersonId PersonId { get; set; }

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x0600353F RID: 13631 RVA: 0x000D6FA2 File Offset: 0x000D51A2
		// (set) Token: 0x06003540 RID: 13632 RVA: 0x000D6FAA File Offset: 0x000D51AA
		internal StoreId ItemId { get; set; }

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06003541 RID: 13633 RVA: 0x000D6FB3 File Offset: 0x000D51B3
		// (set) Token: 0x06003542 RID: 13634 RVA: 0x000D6FBB File Offset: 0x000D51BB
		internal PersonId[] LinkRejectHistory { get; set; }

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x06003543 RID: 13635 RVA: 0x000D6FC4 File Offset: 0x000D51C4
		// (set) Token: 0x06003544 RID: 13636 RVA: 0x000D6FCC File Offset: 0x000D51CC
		internal string GivenName { get; set; }

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x06003545 RID: 13637 RVA: 0x000D6FD5 File Offset: 0x000D51D5
		// (set) Token: 0x06003546 RID: 13638 RVA: 0x000D6FDD File Offset: 0x000D51DD
		internal string Surname { get; set; }

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x06003547 RID: 13639 RVA: 0x000D6FE6 File Offset: 0x000D51E6
		// (set) Token: 0x06003548 RID: 13640 RVA: 0x000D6FEE File Offset: 0x000D51EE
		internal HashSet<string> EmailAddresses { get; set; }

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x06003549 RID: 13641 RVA: 0x000D6FF7 File Offset: 0x000D51F7
		// (set) Token: 0x0600354A RID: 13642 RVA: 0x000D6FFF File Offset: 0x000D51FF
		internal HashSet<string> AliasOfEmailAddresses { get; set; }

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x0600354B RID: 13643 RVA: 0x000D7008 File Offset: 0x000D5208
		// (set) Token: 0x0600354C RID: 13644 RVA: 0x000D7010 File Offset: 0x000D5210
		internal HashSet<string> PhoneNumbers { get; set; }

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x0600354D RID: 13645 RVA: 0x000D7019 File Offset: 0x000D5219
		// (set) Token: 0x0600354E RID: 13646 RVA: 0x000D7021 File Offset: 0x000D5221
		internal string PartnerNetworkId { get; set; }

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x0600354F RID: 13647 RVA: 0x000D702A File Offset: 0x000D522A
		// (set) Token: 0x06003550 RID: 13648 RVA: 0x000D7032 File Offset: 0x000D5232
		internal string ParentDisplayName { get; set; }

		// Token: 0x06003551 RID: 13649 RVA: 0x000D703C File Offset: 0x000D523C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("PersonId=");
			stringBuilder.Append(this.PersonId.ToString());
			stringBuilder.Append(", ItemId=");
			stringBuilder.Append(this.ItemId.ToString());
			if (!string.IsNullOrEmpty(this.GivenName))
			{
				stringBuilder.Append(", GivenName=");
				stringBuilder.Append(this.GivenName);
			}
			if (!string.IsNullOrEmpty(this.Surname))
			{
				stringBuilder.Append(", Surname=");
				stringBuilder.Append(this.Surname);
			}
			if (this.EmailAddresses.Count > 0)
			{
				stringBuilder.Append(", EmailAddreses={");
				foreach (string value in this.EmailAddresses)
				{
					stringBuilder.Append(value);
					stringBuilder.Append(",");
				}
				stringBuilder.Append("}");
			}
			if (this.PhoneNumbers.Count > 0)
			{
				stringBuilder.Append(", PhoneNumbers={");
				foreach (string value2 in this.PhoneNumbers)
				{
					stringBuilder.Append(value2);
					stringBuilder.Append(",");
				}
				stringBuilder.Append("}");
			}
			if (!string.IsNullOrEmpty(this.PartnerNetworkId))
			{
				stringBuilder.Append(", PartnerNetworkId=");
				stringBuilder.Append(this.PartnerNetworkId);
			}
			if (!string.IsNullOrEmpty(this.ParentDisplayName))
			{
				stringBuilder.Append(", ParentDisplayName=");
				stringBuilder.Append(this.ParentDisplayName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000D721C File Offset: 0x000D541C
		internal static IList<ContactInfoForSuggestion> ConvertAll(IList<IStorePropertyBag> inputContacts)
		{
			Util.ThrowOnNullArgument(inputContacts, "inputContacts");
			List<ContactInfoForSuggestion> list = new List<ContactInfoForSuggestion>(inputContacts.Count);
			foreach (IStorePropertyBag propertyBag in inputContacts)
			{
				list.Add(ContactInfoForSuggestion.Create(propertyBag));
			}
			return list;
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000D7284 File Offset: 0x000D5484
		internal static IEnumerable<ContactInfoForSuggestion> GetContactsEnumerator(MailboxSession mailboxSession)
		{
			return ContactsEnumerator<ContactInfoForSuggestion>.CreateContactsOnlyEnumerator(mailboxSession, DefaultFolderType.AllContacts, ContactInfoForSuggestion.Properties, new Func<IStorePropertyBag, ContactInfoForSuggestion>(ContactInfoForSuggestion.Create), new XSOFactory());
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000D72A4 File Offset: 0x000D54A4
		internal static ContactInfoForSuggestion Create(IStorePropertyBag propertyBag)
		{
			Util.ThrowOnNullArgument(propertyBag, "propertyBag");
			StoreId valueOrDefault = propertyBag.GetValueOrDefault<StoreId>(ItemSchema.Id, null);
			PersonId valueOrDefault2 = propertyBag.GetValueOrDefault<PersonId>(ContactSchema.PersonId, null);
			PersonId[] valueOrDefault3 = propertyBag.GetValueOrDefault<PersonId[]>(ContactSchema.LinkRejectHistory, null);
			string @string = ContactInfoForSuggestion.GetString(new Func<string, string>(ContactInfoForSuggestion.NormalizeName), propertyBag, ContactSchema.GivenName);
			string string2 = ContactInfoForSuggestion.GetString(new Func<string, string>(ContactInfoForSuggestion.NormalizeName), propertyBag, ContactSchema.Surname);
			string valueOrDefault4 = propertyBag.GetValueOrDefault<string>(ContactSchema.PartnerNetworkId, null);
			string valueOrDefault5 = propertyBag.GetValueOrDefault<string>(ItemSchema.ParentDisplayName, null);
			HashSet<string> stringSet = ContactInfoForSuggestion.GetStringSet(new Func<string, string>(ContactInfoForSuggestion.NormalizeEmailAddress), propertyBag, new PropertyDefinition[]
			{
				ContactSchema.Email1EmailAddress,
				ContactSchema.Email2EmailAddress,
				ContactSchema.Email3EmailAddress,
				ContactSchema.PrimarySmtpAddress
			});
			HashSet<string> aliasOfEmailAddresses = ContactInfoForSuggestion.GetAliasOfEmailAddresses(stringSet);
			HashSet<string> stringSet2 = ContactInfoForSuggestion.GetStringSet(new Func<string, string>(Util.NormalizePhoneNumber), propertyBag, new PropertyDefinition[]
			{
				ContactSchema.BusinessPhoneNumber,
				ContactSchema.BusinessPhoneNumber2,
				ContactSchema.HomePhone,
				ContactSchema.MobilePhone,
				ContactSchema.OtherTelephone
			});
			return new ContactInfoForSuggestion
			{
				ItemId = valueOrDefault,
				PersonId = valueOrDefault2,
				LinkRejectHistory = valueOrDefault3,
				GivenName = @string,
				Surname = string2,
				EmailAddresses = stringSet,
				AliasOfEmailAddresses = aliasOfEmailAddresses,
				PhoneNumbers = stringSet2,
				PartnerNetworkId = valueOrDefault4,
				ParentDisplayName = valueOrDefault5
			};
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000D7428 File Offset: 0x000D5628
		private static HashSet<string> GetStringSet(Func<string, string> normalize, IStorePropertyBag propertyBag, params PropertyDefinition[] properties)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (PropertyDefinition property in properties)
			{
				string @string = ContactInfoForSuggestion.GetString(normalize, propertyBag, property);
				if (@string != null)
				{
					hashSet.Add(@string);
				}
			}
			return hashSet;
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000D7470 File Offset: 0x000D5670
		private static string GetString(Func<string, string> normalize, IStorePropertyBag propertyBag, PropertyDefinition property)
		{
			string text = normalize(propertyBag.GetValueOrDefault<string>(property, string.Empty));
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x000D749C File Offset: 0x000D569C
		private static HashSet<string> GetAliasOfEmailAddresses(HashSet<string> emailAddresses)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (string text in emailAddresses)
			{
				int num = text.IndexOf("@");
				if (num > 1)
				{
					hashSet.Add(text.Substring(0, num));
				}
			}
			return hashSet;
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x000D7510 File Offset: 0x000D5710
		private static string NormalizeEmailAddress(string emailAddress)
		{
			return emailAddress.Trim();
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x000D7518 File Offset: 0x000D5718
		private static string NormalizeName(string name)
		{
			string text = name.Trim();
			do
			{
				name = text;
				text = name.Replace("  ", " ");
			}
			while (!StringComparer.OrdinalIgnoreCase.Equals(text, name));
			return text;
		}

		// Token: 0x04001C49 RID: 7241
		internal static readonly PropertyDefinition[] Properties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ContactSchema.PersonId,
			ContactSchema.LinkRejectHistory,
			ContactSchema.GivenName,
			ContactSchema.Surname,
			StoreObjectSchema.DisplayName,
			ContactSchema.Email1EmailAddress,
			ContactSchema.Email2EmailAddress,
			ContactSchema.Email3EmailAddress,
			ContactSchema.PrimarySmtpAddress,
			ContactSchema.BusinessPhoneNumber,
			ContactSchema.BusinessPhoneNumber2,
			ContactSchema.HomePhone,
			ContactSchema.MobilePhone,
			ContactSchema.OtherTelephone,
			ContactSchema.PartnerNetworkId,
			ItemSchema.ParentDisplayName
		};
	}
}
