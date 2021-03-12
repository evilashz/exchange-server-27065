using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000E2 RID: 226
	[Serializable]
	internal class PersonalContactInfo : ContactInfo
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x0001D1F4 File Offset: 0x0001B3F4
		internal PersonalContactInfo(Guid userMailboxGuid, Contact contact)
		{
			this.title = PersonalContactInfo.GetProperty(contact, ContactSchema.Title);
			this.fullName = (PersonalContactInfo.GetProperty(contact, StoreObjectSchema.DisplayName) ?? (PersonalContactInfo.GetProperty(contact, ContactSchema.GivenName) + " " + PersonalContactInfo.GetProperty(contact, ContactSchema.Surname)));
			this.firstName = PersonalContactInfo.GetProperty(contact, ContactSchema.GivenName);
			this.lastName = PersonalContactInfo.GetProperty(contact, ContactSchema.Surname);
			this.companyName = PersonalContactInfo.GetProperty(contact, ContactSchema.CompanyName);
			this.workAddressCity = PersonalContactInfo.GetProperty(contact, ContactSchema.WorkAddressCity);
			this.workAddressCountry = PersonalContactInfo.GetProperty(contact, ContactSchema.WorkAddressCountry);
			this.businessPhone1 = this.GetPhoneNumber(contact, ContactSchema.BusinessPhoneNumber);
			this.businessPhone2 = this.GetPhoneNumber(contact, ContactSchema.BusinessPhoneNumber2);
			this.businessPhone = (this.businessPhone1 ?? this.businessPhone2);
			this.homePhone1 = this.GetPhoneNumber(contact, ContactSchema.HomePhone);
			this.homePhone2 = this.GetPhoneNumber(contact, ContactSchema.HomePhone2);
			this.homePhone = (this.homePhone1 ?? this.homePhone2);
			this.mobilePhone = this.GetPhoneNumber(contact, ContactSchema.MobilePhone);
			this.mobilePhone2 = this.GetPhoneNumber(contact, ContactSchema.MobilePhone2);
			this.mobilePhone = (this.mobilePhone ?? this.mobilePhone2);
			this.businessFax = PersonalContactInfo.GetProperty(contact, ContactSchema.WorkFax);
			IDictionary<EmailAddressIndex, Participant> emailAddresses = contact.EmailAddresses;
			this.emailAddress = PersonalContactInfo.GetSmtpAddress(emailAddresses.Values);
			this.instantMessageAddress = PersonalContactInfo.GetProperty(contact, ContactSchema.IMAddress);
			this.storeObjectId = contact.Id.ObjectId;
			this.ewsId = StoreId.StoreIdToEwsId(userMailboxGuid, this.storeObjectId);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001D3BC File Offset: 0x0001B5BC
		internal PersonalContactInfo(Guid userMailboxGuid, object[] result, FoundByType foundBy, bool loadPhoneNumbersAndDisplayNameOnly)
		{
			StorePropertyDefinition[] propertyDefinitions = loadPhoneNumbersAndDisplayNameOnly ? PersonalContactInfo.contactResolutionPropertyDefinitions : PersonalContactInfo.contactPropertyDefinitions;
			this.foundBy = foundBy;
			if (!loadPhoneNumbersAndDisplayNameOnly)
			{
				this.title = XsoUtil.GetProperty(result, ContactSchema.Title, propertyDefinitions);
				this.firstName = XsoUtil.GetProperty(result, ContactSchema.GivenName, propertyDefinitions);
				this.lastName = XsoUtil.GetProperty(result, ContactSchema.Surname, propertyDefinitions);
				this.companyName = XsoUtil.GetProperty(result, ContactSchema.CompanyName, propertyDefinitions);
				this.workAddressCity = XsoUtil.GetProperty(result, ContactSchema.WorkAddressCity, propertyDefinitions);
				this.workAddressCountry = XsoUtil.GetProperty(result, ContactSchema.WorkAddressCountry, propertyDefinitions);
				this.instantMessageAddress = XsoUtil.GetProperty(result, ContactSchema.IMAddress, propertyDefinitions);
				this.emailAddress = PersonalContactInfo.GetSmtpAddress(new Participant[]
				{
					XsoUtil.GetProperty<Participant>(result, ContactSchema.Email1, propertyDefinitions),
					XsoUtil.GetProperty<Participant>(result, ContactSchema.Email2, propertyDefinitions),
					XsoUtil.GetProperty<Participant>(result, ContactSchema.Email3, propertyDefinitions)
				});
			}
			this.fullName = XsoUtil.GetProperty(result, StoreObjectSchema.DisplayName, propertyDefinitions);
			this.homePhone1 = this.GetPhoneNumber(result, ContactSchema.HomePhone, propertyDefinitions);
			this.homePhone2 = this.GetPhoneNumber(result, ContactSchema.HomePhone2, propertyDefinitions);
			this.homePhone = (this.homePhone1 ?? this.homePhone2);
			this.mobilePhone = this.GetPhoneNumber(result, ContactSchema.MobilePhone, propertyDefinitions);
			this.mobilePhone2 = this.GetPhoneNumber(result, ContactSchema.MobilePhone2, propertyDefinitions);
			this.mobilePhone = (this.mobilePhone ?? this.mobilePhone2);
			this.businessFax = XsoUtil.GetProperty(result, ContactSchema.WorkFax, propertyDefinitions);
			this.businessPhone1 = this.GetPhoneNumber(result, ContactSchema.BusinessPhoneNumber, propertyDefinitions);
			this.businessPhone2 = this.GetPhoneNumber(result, ContactSchema.BusinessPhoneNumber2, propertyDefinitions);
			this.businessPhone = (this.businessPhone1 ?? this.businessPhone2);
			this.personId = XsoUtil.GetProperty<PersonId>(result, ContactSchema.PersonId, propertyDefinitions);
			this.partnerNetworkId = XsoUtil.GetProperty<string>(result, ContactSchema.PartnerNetworkId, propertyDefinitions);
			VersionedId property = XsoUtil.GetProperty<VersionedId>(result, ItemSchema.Id, propertyDefinitions);
			this.storeObjectId = property.ObjectId;
			this.ewsId = StoreId.StoreIdToEwsId(userMailboxGuid, this.storeObjectId);
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001D5E0 File Offset: 0x0001B7E0
		internal static StorePropertyDefinition[] ContactPropertyDefinitions
		{
			get
			{
				return PersonalContactInfo.contactPropertyDefinitions;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001D5E7 File Offset: 0x0001B7E7
		internal static StorePropertyDefinition[] ContactPhonePropertyDefinitions
		{
			get
			{
				return PersonalContactInfo.contactResolutionPropertyDefinitions;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001D5EE File Offset: 0x0001B7EE
		internal string BusinessPhone1
		{
			get
			{
				return this.businessPhone1;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x0001D5F6 File Offset: 0x0001B7F6
		internal string BusinessPhone2
		{
			get
			{
				return this.businessPhone2;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0001D5FE File Offset: 0x0001B7FE
		internal string HomePhone1
		{
			get
			{
				return this.homePhone1;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x0001D606 File Offset: 0x0001B806
		internal string HomePhone2
		{
			get
			{
				return this.homePhone2;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0001D60E File Offset: 0x0001B80E
		internal string MobilePhone2
		{
			get
			{
				return this.mobilePhone2;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x0001D616 File Offset: 0x0001B816
		internal override string Title
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0001D61E File Offset: 0x0001B81E
		internal override string Company
		{
			get
			{
				return this.companyName;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0001D626 File Offset: 0x0001B826
		internal override string DisplayName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0001D62E File Offset: 0x0001B82E
		internal override string FirstName
		{
			get
			{
				return this.firstName;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x0001D636 File Offset: 0x0001B836
		internal override string LastName
		{
			get
			{
				return this.lastName;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0001D63E File Offset: 0x0001B83E
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x0001D646 File Offset: 0x0001B846
		internal override string BusinessPhone
		{
			get
			{
				return this.businessPhone;
			}
			set
			{
				this.businessPhone = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0001D64F File Offset: 0x0001B84F
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x0001D657 File Offset: 0x0001B857
		internal override string MobilePhone
		{
			get
			{
				return this.mobilePhone;
			}
			set
			{
				this.mobilePhone = value;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0001D660 File Offset: 0x0001B860
		internal override string FaxNumber
		{
			get
			{
				return this.businessFax;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0001D668 File Offset: 0x0001B868
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x0001D670 File Offset: 0x0001B870
		internal override string HomePhone
		{
			get
			{
				return this.homePhone;
			}
			set
			{
				this.homePhone = value;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0001D679 File Offset: 0x0001B879
		internal override string IMAddress
		{
			get
			{
				return this.instantMessageAddress;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x0001D681 File Offset: 0x0001B881
		internal override string EMailAddress
		{
			get
			{
				return this.emailAddress;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0001D689 File Offset: 0x0001B889
		internal override FoundByType FoundBy
		{
			get
			{
				return this.foundBy;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x0001D691 File Offset: 0x0001B891
		internal override string Id
		{
			get
			{
				return this.storeObjectId.ToBase64String();
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x0001D69E File Offset: 0x0001B89E
		internal override string EwsId
		{
			get
			{
				return this.ewsId;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0001D6A6 File Offset: 0x0001B8A6
		internal override string EwsType
		{
			get
			{
				return "Contact";
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x0001D6AD File Offset: 0x0001B8AD
		internal override string City
		{
			get
			{
				return this.workAddressCity;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0001D6B5 File Offset: 0x0001B8B5
		internal override string Country
		{
			get
			{
				return this.workAddressCountry;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x0001D6BD File Offset: 0x0001B8BD
		internal override ICollection<string> SanitizedPhoneNumbers
		{
			get
			{
				return this.sanitizedNumbers;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x0001D6C5 File Offset: 0x0001B8C5
		internal PersonId PersonId
		{
			get
			{
				return this.personId;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x0001D6CD File Offset: 0x0001B8CD
		internal string PartnerNetworkId
		{
			get
			{
				return this.partnerNetworkId;
			}
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001D6D8 File Offset: 0x0001B8D8
		internal new static ContactInfo FindContactByCallerId(UMSubscriber calledUser, PhoneNumber callerId)
		{
			if (calledUser == null)
			{
				return null;
			}
			List<PersonalContactInfo> uniqueMatches = PersonalContactInfo.GetUniqueMatches(PersonalContactInfo.FindMatchingContacts(calledUser, callerId, false, true));
			PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, calledUser.DisplayName);
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, data, "User _UserDisplayName Matched Contacts = {0}. If there is more than one match, then it will be treated as no matches", new object[]
			{
				uniqueMatches.Count
			});
			if (uniqueMatches.Count == 1)
			{
				return uniqueMatches[0];
			}
			if (uniqueMatches.Count > 1)
			{
				return new MultipleResolvedContactInfo(uniqueMatches, callerId, calledUser.MessageSubmissionCulture);
			}
			return null;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001D758 File Offset: 0x0001B958
		internal static List<PersonalContactInfo> FindAllMatchingContacts(UMSubscriber calledUser, PhoneNumber callerId)
		{
			return PersonalContactInfo.FindMatchingContacts(calledUser, callerId, true, true);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001D764 File Offset: 0x0001B964
		private static List<PersonalContactInfo> FindMatchingContacts(UMSubscriber calledUser, PhoneNumber callerId, bool loadPhoneNumbersOnly, bool findAllMatchingContacts)
		{
			List<PersonalContactInfo> list = new List<PersonalContactInfo>();
			if (calledUser == null)
			{
				return list;
			}
			if (!calledUser.HasContactsFolder)
			{
				PIIMessage data = PIIMessage.Create(PIIType._UserDisplayName, calledUser.DisplayName);
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, data, "User _UserDisplayName does not have a contacts folder.", new object[0]);
				return list;
			}
			StorePropertyDefinition[] contactPhonePropertyDefinitions = PersonalContactInfo.ContactPropertyDefinitions;
			new SortBy(StoreObjectSchema.DisplayName, SortOrder.Ascending);
			if (loadPhoneNumbersOnly)
			{
				contactPhonePropertyDefinitions = PersonalContactInfo.ContactPhonePropertyDefinitions;
			}
			List<KeyValuePair<PhoneNumber, List<string>>> list2 = new List<KeyValuePair<PhoneNumber, List<string>>>(2);
			list2.Add(new KeyValuePair<PhoneNumber, List<string>>(callerId, callerId.GetOptionalPrefixes(calledUser.DialPlan)));
			PhoneNumber phoneNumber = callerId.Extend(calledUser.DialPlan);
			if (!phoneNumber.Equals(callerId))
			{
				list2.Add(new KeyValuePair<PhoneNumber, List<string>>(phoneNumber, phoneNumber.GetOptionalPrefixes(calledUser.DialPlan)));
			}
			FoundByType foundByType = FoundByType.NotSpecified;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = calledUser.CreateSessionLock())
			{
				DefaultFolderType defaultFolderType = DefaultFolderType.AllContacts;
				if (mailboxSessionLock.Session.GetDefaultFolderId(defaultFolderType) == null)
				{
					defaultFolderType = DefaultFolderType.Contacts;
				}
				using (Folder folder = Folder.Bind(mailboxSessionLock.Session, defaultFolderType))
				{
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, contactPhonePropertyDefinitions))
					{
						object[][] rows = queryResult.GetRows(100);
						while (rows != null && rows.Length > 0)
						{
							foreach (object[] result in rows)
							{
								foundByType = FoundByType.NotSpecified;
								string property = XsoUtil.GetProperty<string>(result, ContactSchema.BusinessPhoneNumber, contactPhonePropertyDefinitions);
								string property2 = XsoUtil.GetProperty<string>(result, ContactSchema.BusinessPhoneNumber2, contactPhonePropertyDefinitions);
								string property3 = XsoUtil.GetProperty<string>(result, ContactSchema.HomePhone, contactPhonePropertyDefinitions);
								string property4 = XsoUtil.GetProperty<string>(result, ContactSchema.HomePhone2, contactPhonePropertyDefinitions);
								string property5 = XsoUtil.GetProperty<string>(result, ContactSchema.MobilePhone, contactPhonePropertyDefinitions);
								string property6 = XsoUtil.GetProperty<string>(result, ContactSchema.MobilePhone2, contactPhonePropertyDefinitions);
								foreach (KeyValuePair<PhoneNumber, List<string>> keyValuePair in list2)
								{
									if (keyValuePair.Key.IsMatch(property, keyValuePair.Value) || keyValuePair.Key.IsMatch(property2, keyValuePair.Value))
									{
										foundByType = FoundByType.BusinessPhone;
										break;
									}
									if (keyValuePair.Key.IsMatch(property3, keyValuePair.Value) || keyValuePair.Key.IsMatch(property4, keyValuePair.Value))
									{
										foundByType = FoundByType.HomePhone;
										break;
									}
									if (keyValuePair.Key.IsMatch(property5, keyValuePair.Value) || keyValuePair.Key.IsMatch(property6, keyValuePair.Value))
									{
										foundByType = FoundByType.MobilePhone;
										break;
									}
								}
								if (foundByType != FoundByType.NotSpecified)
								{
									list.Add(new PersonalContactInfo(mailboxSessionLock.Session.MailboxOwner.MailboxInfo.MailboxGuid, result, foundByType, loadPhoneNumbersOnly));
									if (!findAllMatchingContacts)
									{
										break;
									}
								}
							}
							rows = queryResult.GetRows(100);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001DA84 File Offset: 0x0001BC84
		private static string GetProperty(Contact contact, StorePropertyDefinition propertyId)
		{
			object obj = contact.TryGetProperty(propertyId);
			if (obj == null || obj is PropertyError)
			{
				return null;
			}
			string input = (string)obj;
			return Utils.TrimSpaces(input);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001DAB4 File Offset: 0x0001BCB4
		private static string GetSmtpAddress(ICollection<Participant> emailAddrList)
		{
			string text = null;
			string text2 = null;
			foreach (Participant participant in emailAddrList)
			{
				if (!(participant == null))
				{
					text = (participant.TryGetProperty(ParticipantSchema.SmtpAddress) as string);
					if (text != null)
					{
						break;
					}
					if (text2 == null)
					{
						string text3 = participant.TryGetProperty(ParticipantSchema.EmailAddressForDisplay) as string;
						if (text3 != null && SmtpAddress.IsValidSmtpAddress(text3))
						{
							text2 = text3;
						}
					}
				}
			}
			return text ?? text2;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001DB44 File Offset: 0x0001BD44
		private static List<PersonalContactInfo> GetUniqueMatches(List<PersonalContactInfo> matches)
		{
			if (matches == null || matches.Count <= 1)
			{
				return matches;
			}
			Dictionary<string, PersonalContactInfo> dictionary = new Dictionary<string, PersonalContactInfo>();
			foreach (PersonalContactInfo personalContactInfo in matches)
			{
				if (personalContactInfo.PersonId == null)
				{
					string key = Guid.NewGuid().ToString();
					dictionary.Add(key, personalContactInfo);
				}
				else
				{
					string key = personalContactInfo.PersonId.ToBase64String();
					if (!dictionary.ContainsKey(key))
					{
						dictionary.Add(key, personalContactInfo);
					}
					else if (dictionary[key].PartnerNetworkId != null)
					{
						dictionary[key] = personalContactInfo;
					}
				}
			}
			List<PersonalContactInfo> list = new List<PersonalContactInfo>();
			foreach (KeyValuePair<string, PersonalContactInfo> keyValuePair in dictionary)
			{
				list.Add(keyValuePair.Value);
			}
			return list;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001DC48 File Offset: 0x0001BE48
		private string GetPhoneNumber(Contact contact, StorePropertyDefinition propertyDef)
		{
			string property = PersonalContactInfo.GetProperty(contact, propertyDef);
			this.AddToSanitizedNumbersListIfNecessary(property);
			return property;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001DC68 File Offset: 0x0001BE68
		private string GetPhoneNumber(object[] result, StorePropertyDefinition propId, StorePropertyDefinition[] propertyDefinitions)
		{
			string property = XsoUtil.GetProperty(result, propId, propertyDefinitions);
			this.AddToSanitizedNumbersListIfNecessary(property);
			return property;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001DC86 File Offset: 0x0001BE86
		private void AddToSanitizedNumbersListIfNecessary(string number)
		{
			number = DtmfString.SanitizePhoneNumber(number);
			if (!string.IsNullOrEmpty(number) && !this.sanitizedNumbers.Contains(number))
			{
				this.sanitizedNumbers.Add(number);
			}
		}

		// Token: 0x0400043F RID: 1087
		private static StorePropertyDefinition[] contactPropertyDefinitions = new StorePropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			ContactSchema.GivenName,
			ContactSchema.Surname,
			StoreObjectSchema.DisplayName,
			ContactSchema.Title,
			ContactSchema.OfficeLocation,
			ContactSchema.CompanyName,
			ContactSchema.BusinessPhoneNumber,
			ContactSchema.BusinessPhoneNumber2,
			ContactSchema.MobilePhone,
			ContactSchema.MobilePhone2,
			ContactSchema.HomePhone,
			ContactSchema.HomePhone2,
			ContactSchema.WorkFax,
			ContactSchema.Email1,
			ContactSchema.Email1EmailAddress,
			ContactSchema.Email1DisplayName,
			ContactSchema.Email1AddrType,
			ContactSchema.Email2,
			ContactSchema.Email2EmailAddress,
			ContactSchema.Email2AddrType,
			ContactSchema.Email3,
			ContactSchema.Email3EmailAddress,
			ContactSchema.Email3AddrType,
			ContactSchema.IMAddress,
			ContactSchema.HomeAddress,
			ContactSchema.BusinessAddress,
			ContactSchema.OtherAddress,
			ContactSchema.WorkAddressCity,
			ContactSchema.WorkAddressCountry,
			ContactSchema.PartnerNetworkId,
			ContactSchema.PersonId
		};

		// Token: 0x04000440 RID: 1088
		private static StorePropertyDefinition[] contactResolutionPropertyDefinitions = new StorePropertyDefinition[]
		{
			ContactSchema.BusinessPhoneNumber,
			ContactSchema.BusinessPhoneNumber2,
			ContactSchema.MobilePhone,
			ContactSchema.MobilePhone2,
			ContactSchema.HomePhone,
			ContactSchema.HomePhone2,
			ContactSchema.WorkFax,
			ItemSchema.Id,
			StoreObjectSchema.DisplayName,
			ContactSchema.PartnerNetworkId,
			ContactSchema.PersonId
		};

		// Token: 0x04000441 RID: 1089
		private readonly string partnerNetworkId;

		// Token: 0x04000442 RID: 1090
		private string title;

		// Token: 0x04000443 RID: 1091
		private string companyName;

		// Token: 0x04000444 RID: 1092
		private string fullName;

		// Token: 0x04000445 RID: 1093
		private string firstName;

		// Token: 0x04000446 RID: 1094
		private string lastName;

		// Token: 0x04000447 RID: 1095
		private string businessPhone;

		// Token: 0x04000448 RID: 1096
		private string businessPhone1;

		// Token: 0x04000449 RID: 1097
		private string businessPhone2;

		// Token: 0x0400044A RID: 1098
		private string workAddressCity;

		// Token: 0x0400044B RID: 1099
		private string workAddressCountry;

		// Token: 0x0400044C RID: 1100
		private string mobilePhone;

		// Token: 0x0400044D RID: 1101
		private string mobilePhone2;

		// Token: 0x0400044E RID: 1102
		private string homePhone;

		// Token: 0x0400044F RID: 1103
		private string homePhone1;

		// Token: 0x04000450 RID: 1104
		private string homePhone2;

		// Token: 0x04000451 RID: 1105
		private string businessFax;

		// Token: 0x04000452 RID: 1106
		private string instantMessageAddress;

		// Token: 0x04000453 RID: 1107
		private string emailAddress;

		// Token: 0x04000454 RID: 1108
		private PersonId personId;

		// Token: 0x04000455 RID: 1109
		private FoundByType foundBy;

		// Token: 0x04000456 RID: 1110
		private ICollection<string> sanitizedNumbers = new List<string>(10);

		// Token: 0x04000457 RID: 1111
		private StoreObjectId storeObjectId;

		// Token: 0x04000458 RID: 1112
		private string ewsId;
	}
}
