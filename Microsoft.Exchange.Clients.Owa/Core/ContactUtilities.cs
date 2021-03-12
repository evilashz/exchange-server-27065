using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core.Directory;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020002BA RID: 698
	internal static class ContactUtilities
	{
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x0009BA62 File Offset: 0x00099C62
		public static PropertyDefinition[] PrefetchProperties
		{
			get
			{
				return ContactUtilities.prefetchProperties;
			}
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0009BA6C File Offset: 0x00099C6C
		private static Dictionary<string, ContactPropertyInfo> CreateIdToInfoMap()
		{
			Dictionary<string, ContactPropertyInfo> dictionary = new Dictionary<string, ContactPropertyInfo>();
			for (int i = 0; i < ContactUtilities.AllContactProperties.Length; i++)
			{
				ContactPropertyInfo contactPropertyInfo = ContactUtilities.AllContactProperties[i];
				dictionary.Add(contactPropertyInfo.Id, contactPropertyInfo);
			}
			return dictionary;
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x0009BAA8 File Offset: 0x00099CA8
		private static PropertyDefinition[] CreateContactProperties()
		{
			PropertyDefinition[] array = new PropertyDefinition[ContactUtilities.AllContactProperties.Length];
			for (int i = 0; i < ContactUtilities.AllContactProperties.Length; i++)
			{
				array[i] = ContactUtilities.AllContactProperties[i].PropertyDefinition;
			}
			return array;
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0009BAE4 File Offset: 0x00099CE4
		public static ContactPropertyInfo GetPropertyInfo(string propertyId)
		{
			return ContactUtilities.idToInfoMap[propertyId];
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0009BAF1 File Offset: 0x00099CF1
		public static FileAsMapping GetDefaultFileAs()
		{
			return ContactUtils.GetDefaultFileAs(Culture.GetUserCulture().LCID);
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0009BB04 File Offset: 0x00099D04
		public static FileAsMapping GetFileAs(Item item)
		{
			FileAsMapping fileAsMapping = FileAsMapping.None;
			if (item == null)
			{
				fileAsMapping = ContactUtilities.GetDefaultFileAs();
			}
			else
			{
				object obj = item.TryGetProperty(ContactSchema.FileAsId);
				if (obj is int)
				{
					fileAsMapping = (FileAsMapping)obj;
				}
				if (fileAsMapping == FileAsMapping.None || !ContactUtilities.fileAsEnumToStringMap.ContainsKey(fileAsMapping))
				{
					fileAsMapping = ContactUtilities.GetDefaultFileAs();
				}
			}
			return fileAsMapping;
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0009BB51 File Offset: 0x00099D51
		public static string GetFileAsString(FileAsMapping enumValue)
		{
			return LocalizedStrings.GetNonEncoded(ContactUtilities.fileAsEnumToStringMap[enumValue]);
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0009BB63 File Offset: 0x00099D63
		public static ICollection<FileAsMapping> GetSupportedFileAsMappings()
		{
			return ContactUtilities.fileAsEnumToStringMap.Keys;
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0009BB6F File Offset: 0x00099D6F
		public static string GetPhysicalAddressString(PhysicalAddressType enumValue)
		{
			return LocalizedStrings.GetNonEncoded(ContactUtilities.physicalAddressEnumToStringMap[enumValue]);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0009BB81 File Offset: 0x00099D81
		public static EmailAddressIndex GetEmailPropertyIndex(ContactPropertyInfo propertyInfo)
		{
			if (propertyInfo == ContactUtilities.Email1EmailAddress || propertyInfo == ContactUtilities.Email1DisplayName)
			{
				return EmailAddressIndex.Email1;
			}
			if (propertyInfo == ContactUtilities.Email2EmailAddress || propertyInfo == ContactUtilities.Email2DisplayName)
			{
				return EmailAddressIndex.Email2;
			}
			if (propertyInfo == ContactUtilities.Email3EmailAddress || propertyInfo == ContactUtilities.Email3DisplayName)
			{
				return EmailAddressIndex.Email3;
			}
			return EmailAddressIndex.None;
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0009BBBA File Offset: 0x00099DBA
		public static ContactPropertyInfo GetEmailDisplayAsProperty(ContactPropertyInfo propertyInfo)
		{
			if (propertyInfo == ContactUtilities.Email1EmailAddress)
			{
				return ContactUtilities.Email1DisplayName;
			}
			if (propertyInfo == ContactUtilities.Email2EmailAddress)
			{
				return ContactUtilities.Email2DisplayName;
			}
			if (propertyInfo == ContactUtilities.Email3EmailAddress)
			{
				return ContactUtilities.Email3DisplayName;
			}
			return null;
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0009BBE8 File Offset: 0x00099DE8
		public static PropertyDefinition GetPropertyDefinitionFromPhoneNumberType(PhoneNumberType type)
		{
			switch (type)
			{
			case PhoneNumberType.AssistantPhone:
				return ContactSchema.AssistantPhoneNumber;
			case PhoneNumberType.BusinessPhone:
				return ContactSchema.BusinessPhoneNumber;
			case PhoneNumberType.BusinessPhone2:
				return ContactSchema.BusinessPhoneNumber2;
			case PhoneNumberType.BusinessFax:
				return ContactSchema.WorkFax;
			case PhoneNumberType.Callback:
				return ContactSchema.CallbackPhone;
			case PhoneNumberType.CarPhone:
				return ContactSchema.CarPhone;
			case PhoneNumberType.CompanyMainPhone:
				return ContactSchema.OrganizationMainPhone;
			case PhoneNumberType.HomeFax:
				return ContactSchema.HomeFax;
			case PhoneNumberType.HomePhone:
				return ContactSchema.HomePhone;
			case PhoneNumberType.HomePhone2:
				return ContactSchema.HomePhone2;
			case PhoneNumberType.Isdn:
				return ContactSchema.InternationalIsdnNumber;
			case PhoneNumberType.MobilePhone:
				return ContactSchema.MobilePhone;
			case PhoneNumberType.OtherFax:
				return ContactSchema.OtherFax;
			case PhoneNumberType.OtherPhone:
				return ContactSchema.OtherTelephone;
			case PhoneNumberType.Pager:
				return ContactSchema.Pager;
			case PhoneNumberType.PrimaryPhone:
				return ContactSchema.PrimaryTelephoneNumber;
			case PhoneNumberType.RadioPhone:
				return ContactSchema.RadioPhone;
			case PhoneNumberType.Telex:
				return ContactSchema.TelexNumber;
			case PhoneNumberType.TtyTddPhone:
				return ContactSchema.TtyTddPhoneNumber;
			default:
				throw new ArgumentOutOfRangeException("type", "Phone number type is invalid");
			}
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0009BCCC File Offset: 0x00099ECC
		public static string GetPropertyStringFromPhoneNumberType(PhoneNumberType type)
		{
			switch (type)
			{
			case PhoneNumberType.AssistantPhone:
				return LocalizedStrings.GetNonEncoded(-1816252206);
			case PhoneNumberType.BusinessPhone:
				return LocalizedStrings.GetNonEncoded(346027136);
			case PhoneNumberType.BusinessPhone2:
				return LocalizedStrings.GetNonEncoded(873918106);
			case PhoneNumberType.BusinessFax:
				return LocalizedStrings.GetNonEncoded(-11305699);
			case PhoneNumberType.Callback:
				return LocalizedStrings.GetNonEncoded(-646524091);
			case PhoneNumberType.CarPhone:
				return LocalizedStrings.GetNonEncoded(159631176);
			case PhoneNumberType.CompanyMainPhone:
				return LocalizedStrings.GetNonEncoded(-1918812500);
			case PhoneNumberType.HomeFax:
				return LocalizedStrings.GetNonEncoded(1180016964);
			case PhoneNumberType.HomePhone:
				return LocalizedStrings.GetNonEncoded(-1844864953);
			case PhoneNumberType.HomePhone2:
				return LocalizedStrings.GetNonEncoded(1714659233);
			case PhoneNumberType.Isdn:
				return LocalizedStrings.GetNonEncoded(57098496);
			case PhoneNumberType.MobilePhone:
				return LocalizedStrings.GetNonEncoded(1158653436);
			case PhoneNumberType.OtherFax:
				return LocalizedStrings.GetNonEncoded(-679895069);
			case PhoneNumberType.OtherPhone:
				return LocalizedStrings.GetNonEncoded(-582599340);
			case PhoneNumberType.Pager:
				return LocalizedStrings.GetNonEncoded(-1779142331);
			case PhoneNumberType.PrimaryPhone:
				return LocalizedStrings.GetNonEncoded(1442239260);
			case PhoneNumberType.RadioPhone:
				return LocalizedStrings.GetNonEncoded(-166006211);
			case PhoneNumberType.Telex:
				return LocalizedStrings.GetNonEncoded(1096044911);
			case PhoneNumberType.TtyTddPhone:
				return LocalizedStrings.GetNonEncoded(-1028516975);
			default:
				throw new ArgumentOutOfRangeException("type", "Phone number type is invalid");
			}
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0009BE14 File Offset: 0x0009A014
		public static ContactBase AddADRecipientToContacts(UserContext userContext, ADRecipient adRecipient)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (adRecipient == null)
			{
				throw new ArgumentNullException("adRecipient");
			}
			Participant primaryParticipant = ContactUtilities.GetPrimaryParticipant(adRecipient);
			if (primaryParticipant == null)
			{
				return null;
			}
			if (adRecipient is IADDistributionList)
			{
				DistributionList distributionList = DistributionList.Create(userContext.MailboxSession, userContext.ContactsFolderId);
				distributionList.Add(new Participant(adRecipient));
				distributionList.DisplayName = (string)adRecipient[ADRecipientSchema.DisplayName];
				distributionList.Save(SaveMode.ResolveConflicts);
				return distributionList;
			}
			Contact contact = Contact.Create(userContext.MailboxSession, userContext.ContactsFolderId);
			ContactUtilities.AddContactProperties(userContext, contact, adRecipient, primaryParticipant);
			contact.Save(SaveMode.ResolveConflicts);
			return contact;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0009BEBC File Offset: 0x0009A0BC
		private static Participant GetPrimaryParticipant(ADRecipient adRecipient)
		{
			Participant result = null;
			if (adRecipient.RecipientType == RecipientType.UserMailbox || adRecipient.RecipientType == RecipientType.MailUniversalDistributionGroup || adRecipient.RecipientType == RecipientType.MailUniversalSecurityGroup || adRecipient.RecipientType == RecipientType.MailNonUniversalGroup || adRecipient.RecipientType == RecipientType.MailUser || adRecipient.RecipientType == RecipientType.MailContact || adRecipient.RecipientType == RecipientType.DynamicDistributionGroup || adRecipient.RecipientType == RecipientType.PublicFolder)
			{
				result = new Participant(adRecipient.DisplayName, adRecipient.PrimarySmtpAddress.ToString(), "SMTP");
			}
			return result;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x0009BF3C File Offset: 0x0009A13C
		private static void AddContactProperties(UserContext userContext, Contact contact, ADRecipient adRecipient, Participant participant)
		{
			IRecipientSession recipientSession = Utilities.CreateADRecipientSession(ConsistencyMode.IgnoreInvalid, userContext);
			contact.JobTitle = (string)adRecipient[ADOrgPersonSchema.Title];
			contact.Company = (string)adRecipient[ADOrgPersonSchema.Company];
			contact.Department = (string)adRecipient[ADOrgPersonSchema.Department];
			contact.OfficeLocation = (string)adRecipient[ADOrgPersonSchema.Office];
			if (adRecipient[ADOrgPersonSchema.Manager] != null)
			{
				ADRecipient adrecipient = recipientSession.Read((ADObjectId)adRecipient[ADOrgPersonSchema.Manager]);
				if (adrecipient != null && !string.IsNullOrEmpty(adrecipient.DisplayName))
				{
					contact[ContactSchema.Manager] = adrecipient.DisplayName;
				}
			}
			contact[ContactSchema.AssistantName] = (string)adRecipient[ADRecipientSchema.AssistantName];
			contact.DisplayName = (string)adRecipient[ADRecipientSchema.DisplayName];
			contact[ContactSchema.GivenName] = (string)adRecipient[ADOrgPersonSchema.FirstName];
			contact[ContactSchema.Surname] = (string)adRecipient[ADOrgPersonSchema.LastName];
			contact[ContactSchema.YomiFirstName] = (string)adRecipient[ADRecipientSchema.PhoneticFirstName];
			contact[ContactSchema.YomiLastName] = (string)adRecipient[ADRecipientSchema.PhoneticLastName];
			contact[ContactSchema.YomiCompany] = (string)adRecipient[ADRecipientSchema.PhoneticCompany];
			contact[ContactSchema.FileAsId] = ContactUtilities.GetDefaultFileAs();
			contact.EmailAddresses[EmailAddressIndex.Email1] = participant;
			contact[ContactSchema.PrimaryTelephoneNumber] = (string)adRecipient[ADOrgPersonSchema.Phone];
			contact[ContactSchema.BusinessPhoneNumber] = (string)adRecipient[ADOrgPersonSchema.Phone];
			contact[ContactSchema.HomePhone] = (string)adRecipient[ADOrgPersonSchema.HomePhone];
			contact[ContactSchema.HomePhone2] = DirectoryAssistance.GetFirstResource((MultiValuedProperty<string>)adRecipient[ADOrgPersonSchema.OtherHomePhone]);
			contact[ContactSchema.WorkFax] = (string)adRecipient[ADOrgPersonSchema.Fax];
			contact[ContactSchema.OtherFax] = DirectoryAssistance.GetFirstResource((MultiValuedProperty<string>)adRecipient[ADOrgPersonSchema.OtherFax]);
			contact[ContactSchema.Pager] = (string)adRecipient[ADOrgPersonSchema.Pager];
			contact[ContactSchema.MobilePhone] = (string)adRecipient[ADOrgPersonSchema.MobilePhone];
			contact[ContactSchema.WorkAddressStreet] = (string)adRecipient[ADOrgPersonSchema.StreetAddress];
			contact[ContactSchema.WorkAddressState] = (string)adRecipient[ADOrgPersonSchema.StateOrProvince];
			contact[ContactSchema.WorkAddressPostalCode] = (string)adRecipient[ADOrgPersonSchema.PostalCode];
			contact[ContactSchema.WorkAddressCity] = (string)adRecipient[ADOrgPersonSchema.City];
			contact[ContactSchema.WorkAddressCountry] = (string)adRecipient[ADOrgPersonSchema.Co];
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0009C234 File Offset: 0x0009A434
		private static ContactPropertyInfo LookupAddressProperty(AddressFormatTable.AddressPart addressPart, PhysicalAddressType type)
		{
			ContactPropertyInfo[] array;
			if (ContactUtilities.addressPropertyTable.TryGetValue(type, out array))
			{
				return array[(int)addressPart];
			}
			return null;
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0009C258 File Offset: 0x0009A458
		private static Dictionary<PhysicalAddressType, ContactPropertyInfo[]> LoadAddressPropertyTable()
		{
			Dictionary<PhysicalAddressType, ContactPropertyInfo[]> dictionary = new Dictionary<PhysicalAddressType, ContactPropertyInfo[]>();
			dictionary[PhysicalAddressType.Business] = ContactUtilities.BusinessAddressParts;
			dictionary[PhysicalAddressType.Home] = ContactUtilities.HomeAddressParts;
			dictionary[PhysicalAddressType.Other] = ContactUtilities.OtherAddressParts;
			return dictionary;
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0009C290 File Offset: 0x0009A490
		public static List<ContactPropertyInfo> GetAddressInfo(PhysicalAddressType type)
		{
			if (type != PhysicalAddressType.Business && type != PhysicalAddressType.Home && type != PhysicalAddressType.Other)
			{
				throw new ArgumentOutOfRangeException("type", "type must be Business, Home or Other");
			}
			List<ContactPropertyInfo> list = new List<ContactPropertyInfo>();
			AddressFormatTable.AddressPart[] cultureAddressMap = AddressFormatTable.GetCultureAddressMap(Culture.GetUserCulture().LCID);
			foreach (AddressFormatTable.AddressPart addressPart in cultureAddressMap)
			{
				ContactPropertyInfo item = ContactUtilities.LookupAddressProperty(addressPart, type);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0009C2FC File Offset: 0x0009A4FC
		private static string GetADOrgPersonStringPropertyValue(IADOrgPerson orgPerson, PropertyDefinition property)
		{
			string text = orgPerson[property] as string;
			if (text != null)
			{
				text = text.Trim();
			}
			return text;
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0009C324 File Offset: 0x0009A524
		private static AddressComponent ProcessAddressPartInternal(string addressPartValue, AddressFormatTable.AddressPart addressPart)
		{
			AddressComponent addressComponent = new AddressComponent();
			addressComponent.Value = addressPartValue;
			string label = string.Empty;
			switch (addressPart)
			{
			case AddressFormatTable.AddressPart.Street:
				label = LocalizedStrings.GetHtmlEncoded(-883163903);
				break;
			case AddressFormatTable.AddressPart.City:
				label = LocalizedStrings.GetHtmlEncoded(775690683);
				break;
			case AddressFormatTable.AddressPart.State:
				label = LocalizedStrings.GetHtmlEncoded(2035807370);
				break;
			case AddressFormatTable.AddressPart.PostalCode:
				label = LocalizedStrings.GetHtmlEncoded(-1694515752);
				break;
			case AddressFormatTable.AddressPart.Country:
				label = LocalizedStrings.GetHtmlEncoded(-383027171);
				break;
			}
			addressComponent.Label = label;
			return addressComponent;
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0009C3AC File Offset: 0x0009A5AC
		public static IDictionary<AddressFormatTable.AddressPart, AddressComponent> GetAddressInfo(IADOrgPerson orgPerson)
		{
			if (orgPerson == null)
			{
				throw new ArgumentNullException("orgPerson");
			}
			IDictionary<AddressFormatTable.AddressPart, AddressComponent> dictionary = new Dictionary<AddressFormatTable.AddressPart, AddressComponent>();
			AddressFormatTable.AddressPart[] cultureAddressMap = AddressFormatTable.GetCultureAddressMap(Culture.GetUserCulture().LCID);
			foreach (AddressFormatTable.AddressPart addressPart in cultureAddressMap)
			{
				PropertyDefinition property = AddressFormatTable.LookupAddressPropertyAd(addressPart);
				string adorgPersonStringPropertyValue = ContactUtilities.GetADOrgPersonStringPropertyValue(orgPerson, property);
				if (!string.IsNullOrEmpty(adorgPersonStringPropertyValue))
				{
					AddressComponent value = ContactUtilities.ProcessAddressPartInternal(adorgPersonStringPropertyValue, addressPart);
					dictionary[addressPart] = value;
				}
			}
			return dictionary;
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0009C428 File Offset: 0x0009A628
		public static IDictionary<AddressFormatTable.AddressPart, AddressComponent> GetAddressInfo(Item item, PhysicalAddressType type)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (type != PhysicalAddressType.Business && type != PhysicalAddressType.Home && type != PhysicalAddressType.Other)
			{
				throw new ArgumentOutOfRangeException("type", "type must be Business, Home or Other");
			}
			IDictionary<AddressFormatTable.AddressPart, AddressComponent> dictionary = new Dictionary<AddressFormatTable.AddressPart, AddressComponent>();
			AddressFormatTable.AddressPart[] cultureAddressMap = AddressFormatTable.GetCultureAddressMap(Culture.GetUserCulture().LCID);
			foreach (AddressFormatTable.AddressPart addressPart in cultureAddressMap)
			{
				PropertyDefinition propertyDefinition = AddressFormatTable.LookupAddressProperty(addressPart, type);
				string property = ItemUtility.GetProperty<string>(item, propertyDefinition, string.Empty);
				if (property.Length > 0)
				{
					AddressComponent value = ContactUtilities.ProcessAddressPartInternal(property, addressPart);
					dictionary[addressPart] = value;
				}
			}
			return dictionary;
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x0009C4C8 File Offset: 0x0009A6C8
		internal static void GetParticipantEmailAddress(Participant participant, out string email, out string displayName, bool appendRoutingType)
		{
			displayName = string.Empty;
			email = string.Empty;
			if (participant == null)
			{
				return;
			}
			string text;
			if ((text = participant.GetValueOrDefault<string>(ParticipantSchema.EmailAddressForDisplay)) == null)
			{
				text = (participant.EmailAddress ?? string.Empty);
			}
			email = text;
			displayName = (participant.DisplayName ?? string.Empty);
			if (appendRoutingType && participant.RoutingType != null && participant.EmailAddress != null && !participant.RoutingType.Equals("SMTP") && !participant.RoutingType.Equals("EX"))
			{
				email = string.Concat(new string[]
				{
					"[",
					participant.RoutingType,
					": ",
					email,
					"]"
				});
			}
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x0009C58A File Offset: 0x0009A78A
		internal static void GetParticipantEmailAddress(Participant participant, out string email, out string displayName)
		{
			ContactUtilities.GetParticipantEmailAddress(participant, out email, out displayName, true);
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x0009C598 File Offset: 0x0009A798
		internal static void GetContactEmailAddress(Contact contact, EmailAddressIndex emailIndex, out string email, out string displayName, bool appendRoutingType)
		{
			Participant participant = contact.EmailAddresses[emailIndex];
			ContactUtilities.GetParticipantEmailAddress(participant, out email, out displayName, appendRoutingType);
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x0009C5BC File Offset: 0x0009A7BC
		internal static void GetContactEmailAddress(Contact contact, EmailAddressIndex emailIndex, out string email, out string displayName)
		{
			ContactUtilities.GetContactEmailAddress(contact, emailIndex, out email, out displayName, true);
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x0009C5C8 File Offset: 0x0009A7C8
		internal static void SetContactEmailAddress(Contact contact, EmailAddressIndex emailIndex, string email, string displayName)
		{
			if (Utilities.WhiteSpaceOnlyOrNullEmpty(email))
			{
				email = null;
			}
			if (Utilities.WhiteSpaceOnlyOrNullEmpty(displayName))
			{
				displayName = null;
			}
			Participant copyFrom = contact.EmailAddresses[emailIndex];
			string strB = null;
			string text = null;
			ContactUtilities.GetContactEmailAddress(contact, emailIndex, out strB, out text);
			Participant.Builder builder;
			if (email != null && string.CompareOrdinal(email, strB) == 0)
			{
				builder = new Participant.Builder(copyFrom);
			}
			else if (email != null)
			{
				builder = new Participant.Builder(Participant.Parse(email));
				if (builder.RoutingType == null)
				{
					builder[ParticipantSchema.EmailAddressForDisplay] = email;
				}
			}
			else
			{
				builder = new Participant.Builder();
			}
			builder.DisplayName = displayName;
			Participant value = builder.ToParticipant();
			contact.EmailAddresses[emailIndex] = value;
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x0009C66C File Offset: 0x0009A86C
		internal static string GetContactRecipientIMAddress(string email, UserContext userContext, bool addSipPrefix)
		{
			if (email == null)
			{
				throw new ArgumentNullException("email");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			string text = null;
			using (ContactsFolder contactsFolder = ContactsFolder.Bind(userContext.MailboxSession, DefaultFolderType.Contacts))
			{
				using (FindInfo<Contact> findInfo = contactsFolder.FindByEmailAddress(email, new PropertyDefinition[0]))
				{
					if (findInfo.FindStatus == FindStatus.Found)
					{
						Contact result = findInfo.Result;
						result.Load(new PropertyDefinition[]
						{
							ContactSchema.IMAddress
						});
						text = (result.TryGetProperty(ContactSchema.IMAddress) as string);
					}
				}
			}
			if (addSipPrefix && !string.IsNullOrEmpty(text) && text.IndexOf("sip:", StringComparison.OrdinalIgnoreCase) != 0)
			{
				text = "sip:" + text;
			}
			return text;
		}

		// Token: 0x0400139D RID: 5021
		public static readonly ContactPropertyInfo AssistantName = new ContactPropertyInfo(ContactSchema.AssistantName, "an", 425094986);

		// Token: 0x0400139E RID: 5022
		public static readonly ContactPropertyInfo AssistantPhoneNumber = new ContactPropertyInfo(ContactSchema.AssistantPhoneNumber, "apn", -1816252206);

		// Token: 0x0400139F RID: 5023
		public static readonly ContactPropertyInfo BusinessPhoneNumber = new ContactPropertyInfo(ContactSchema.BusinessPhoneNumber, "bpn", 346027136);

		// Token: 0x040013A0 RID: 5024
		public static readonly ContactPropertyInfo BusinessPhoneNumber2 = new ContactPropertyInfo(ContactSchema.BusinessPhoneNumber2, "bpn2", 873918106);

		// Token: 0x040013A1 RID: 5025
		public static readonly ContactPropertyInfo CompanyName = new ContactPropertyInfo(ContactSchema.CompanyName, "cn", 642177943);

		// Token: 0x040013A2 RID: 5026
		public static readonly ContactPropertyInfo CallbackPhone = new ContactPropertyInfo(ContactSchema.CallbackPhone, "cbp", -646524091);

		// Token: 0x040013A3 RID: 5027
		public static readonly ContactPropertyInfo CarPhone = new ContactPropertyInfo(ContactSchema.CarPhone, "cp", 159631176);

		// Token: 0x040013A4 RID: 5028
		public static readonly ContactPropertyInfo CompanyYomi = new ContactPropertyInfo(ContactSchema.YomiCompany, "cy", 1805298069);

		// Token: 0x040013A5 RID: 5029
		public static readonly ContactPropertyInfo Department = new ContactPropertyInfo(ContactSchema.Department, "d", 1855823700);

		// Token: 0x040013A6 RID: 5030
		public static readonly ContactPropertyInfo Email1EmailAddress = new ContactPropertyInfo(ContactSchema.Email1EmailAddress, "em1", 1111077458);

		// Token: 0x040013A7 RID: 5031
		public static readonly ContactPropertyInfo Email2EmailAddress = new ContactPropertyInfo(ContactSchema.Email2EmailAddress, "em2", 1405549740);

		// Token: 0x040013A8 RID: 5032
		public static readonly ContactPropertyInfo Email3EmailAddress = new ContactPropertyInfo(ContactSchema.Email3EmailAddress, "em3", -160534201);

		// Token: 0x040013A9 RID: 5033
		public static readonly ContactPropertyInfo Email1DisplayName = new ContactPropertyInfo(ContactSchema.Email1DisplayName, "em1dn", 0);

		// Token: 0x040013AA RID: 5034
		public static readonly ContactPropertyInfo Email2DisplayName = new ContactPropertyInfo(ContactSchema.Email2DisplayName, "em2dn", 0);

		// Token: 0x040013AB RID: 5035
		public static readonly ContactPropertyInfo Email3DisplayName = new ContactPropertyInfo(ContactSchema.Email3DisplayName, "em3dn", 0);

		// Token: 0x040013AC RID: 5036
		public static readonly ContactPropertyInfo FileAsId = new ContactPropertyInfo(ContactSchema.FileAsId, "fa", 412578096);

		// Token: 0x040013AD RID: 5037
		public static readonly ContactPropertyInfo FaxNumber = new ContactPropertyInfo(ContactSchema.WorkFax, "fn", -11305699);

		// Token: 0x040013AE RID: 5038
		public static readonly ContactPropertyInfo GivenName = new ContactPropertyInfo(ContactSchema.GivenName, "gn", -1134283443);

		// Token: 0x040013AF RID: 5039
		public static readonly ContactPropertyInfo IMAddress = new ContactPropertyInfo(ContactSchema.IMAddress, "im", -859851584);

		// Token: 0x040013B0 RID: 5040
		public static readonly ContactPropertyInfo InternationalIsdnNumber = new ContactPropertyInfo(ContactSchema.InternationalIsdnNumber, "iin", 57098496);

		// Token: 0x040013B1 RID: 5041
		public static readonly ContactPropertyInfo HomeFax = new ContactPropertyInfo(ContactSchema.HomeFax, "hf", 1180016964);

		// Token: 0x040013B2 RID: 5042
		public static readonly ContactPropertyInfo HomePhone = new ContactPropertyInfo(ContactSchema.HomePhone, "hp", -1844864953);

		// Token: 0x040013B3 RID: 5043
		public static readonly ContactPropertyInfo HomePhone2 = new ContactPropertyInfo(ContactSchema.HomePhone2, "hp2", 1714659233);

		// Token: 0x040013B4 RID: 5044
		public static readonly ContactPropertyInfo HomeStreet = new ContactPropertyInfo(ContactSchema.HomeStreet, "hs", -883163903);

		// Token: 0x040013B5 RID: 5045
		public static readonly ContactPropertyInfo HomeCity = new ContactPropertyInfo(ContactSchema.HomeCity, "hc", 775690683);

		// Token: 0x040013B6 RID: 5046
		public static readonly ContactPropertyInfo HomeState = new ContactPropertyInfo(ContactSchema.HomeState, "hst", 2035807370);

		// Token: 0x040013B7 RID: 5047
		public static readonly ContactPropertyInfo HomePostalCode = new ContactPropertyInfo(ContactSchema.HomePostalCode, "hpc", -1694515752);

		// Token: 0x040013B8 RID: 5048
		public static readonly ContactPropertyInfo HomeCountry = new ContactPropertyInfo(ContactSchema.HomeCountry, "hct", -383027171);

		// Token: 0x040013B9 RID: 5049
		public static readonly ContactPropertyInfo Manager = new ContactPropertyInfo(ContactSchema.Manager, "m", -128712621);

		// Token: 0x040013BA RID: 5050
		public static readonly ContactPropertyInfo MiddleName = new ContactPropertyInfo(ContactSchema.MiddleName, "mn", -252056336);

		// Token: 0x040013BB RID: 5051
		public static readonly ContactPropertyInfo MobilePhone = new ContactPropertyInfo(ContactSchema.MobilePhone, "mp", 1158653436);

		// Token: 0x040013BC RID: 5052
		public static readonly ContactPropertyInfo SurName = new ContactPropertyInfo(ContactSchema.Surname, "sn", -991618307);

		// Token: 0x040013BD RID: 5053
		public static readonly ContactPropertyInfo Title = new ContactPropertyInfo(ContactSchema.Title, "t", 587115635);

		// Token: 0x040013BE RID: 5054
		public static readonly ContactPropertyInfo OtherFax = new ContactPropertyInfo(ContactSchema.OtherFax, "of", -679895069);

		// Token: 0x040013BF RID: 5055
		public static readonly ContactPropertyInfo OfficeLocation = new ContactPropertyInfo(ContactSchema.OfficeLocation, "ol", 275231482);

		// Token: 0x040013C0 RID: 5056
		public static readonly ContactPropertyInfo OtherTelephone = new ContactPropertyInfo(ContactSchema.OtherTelephone, "ot", -582599340);

		// Token: 0x040013C1 RID: 5057
		public static readonly ContactPropertyInfo OtherStreet = new ContactPropertyInfo(ContactSchema.OtherStreet, "os", -883163903);

		// Token: 0x040013C2 RID: 5058
		public static readonly ContactPropertyInfo OtherCity = new ContactPropertyInfo(ContactSchema.OtherCity, "oc", 775690683);

		// Token: 0x040013C3 RID: 5059
		public static readonly ContactPropertyInfo OtherState = new ContactPropertyInfo(ContactSchema.OtherState, "ost", 2035807370);

		// Token: 0x040013C4 RID: 5060
		public static readonly ContactPropertyInfo OtherPostalCode = new ContactPropertyInfo(ContactSchema.OtherPostalCode, "opc", -1694515752);

		// Token: 0x040013C5 RID: 5061
		public static readonly ContactPropertyInfo OtherCountry = new ContactPropertyInfo(ContactSchema.OtherCountry, "oct", -383027171);

		// Token: 0x040013C6 RID: 5062
		public static readonly ContactPropertyInfo OrganizationMainPhone = new ContactPropertyInfo(ContactSchema.OrganizationMainPhone, "omp", -1918812500);

		// Token: 0x040013C7 RID: 5063
		public static readonly ContactPropertyInfo Pager = new ContactPropertyInfo(ContactSchema.Pager, "p", 410048973);

		// Token: 0x040013C8 RID: 5064
		public static readonly ContactPropertyInfo PostalAddressId = new ContactPropertyInfo(ContactSchema.PostalAddressId, "pa", 1912536019);

		// Token: 0x040013C9 RID: 5065
		public static readonly ContactPropertyInfo PrimaryTelephoneNumber = new ContactPropertyInfo(ContactSchema.PrimaryTelephoneNumber, "ptn", 1442239260);

		// Token: 0x040013CA RID: 5066
		public static readonly ContactPropertyInfo RadioPhone = new ContactPropertyInfo(ContactSchema.RadioPhone, "rp", -166006211);

		// Token: 0x040013CB RID: 5067
		public static readonly ContactPropertyInfo TelexNumber = new ContactPropertyInfo(ContactSchema.TelexNumber, "tn", 1096044911);

		// Token: 0x040013CC RID: 5068
		public static readonly ContactPropertyInfo TtyTddPhoneNumber = new ContactPropertyInfo(ContactSchema.TtyTddPhoneNumber, "ttpn", -1028516975);

		// Token: 0x040013CD RID: 5069
		public static readonly ContactPropertyInfo YomiFirstName = new ContactPropertyInfo(ContactSchema.YomiFirstName, "yf", 415319213);

		// Token: 0x040013CE RID: 5070
		public static readonly ContactPropertyInfo YomiLastName = new ContactPropertyInfo(ContactSchema.YomiLastName, "yl", 333370761);

		// Token: 0x040013CF RID: 5071
		public static readonly ContactPropertyInfo WebPage = new ContactPropertyInfo(ContactSchema.BusinessHomePage, "wp", 521829799);

		// Token: 0x040013D0 RID: 5072
		public static readonly ContactPropertyInfo WorkAddressStreet = new ContactPropertyInfo(ContactSchema.WorkAddressStreet, "was", -883163903);

		// Token: 0x040013D1 RID: 5073
		public static readonly ContactPropertyInfo WorkAddressCity = new ContactPropertyInfo(ContactSchema.WorkAddressCity, "wac", 775690683);

		// Token: 0x040013D2 RID: 5074
		public static readonly ContactPropertyInfo WorkAddressState = new ContactPropertyInfo(ContactSchema.WorkAddressState, "wast", 2035807370);

		// Token: 0x040013D3 RID: 5075
		public static readonly ContactPropertyInfo WorkAddressPostalCode = new ContactPropertyInfo(ContactSchema.WorkAddressPostalCode, "wapc", -1694515752);

		// Token: 0x040013D4 RID: 5076
		public static readonly ContactPropertyInfo WorkAddressCountry = new ContactPropertyInfo(ContactSchema.WorkAddressCountry, "wact", -383027171);

		// Token: 0x040013D5 RID: 5077
		public static readonly ContactPropertyInfo DefaultPhoneNumber = new ContactPropertyInfo(ContactSchema.SelectedPreferredPhoneNumber, "dpn", -746050732);

		// Token: 0x040013D6 RID: 5078
		public static readonly ContactPropertyInfo[] AllContactProperties = new ContactPropertyInfo[]
		{
			ContactUtilities.AssistantName,
			ContactUtilities.AssistantPhoneNumber,
			ContactUtilities.BusinessPhoneNumber,
			ContactUtilities.BusinessPhoneNumber2,
			ContactUtilities.CompanyName,
			ContactUtilities.CallbackPhone,
			ContactUtilities.CarPhone,
			ContactUtilities.CompanyYomi,
			ContactUtilities.Department,
			ContactUtilities.FileAsId,
			ContactUtilities.FaxNumber,
			ContactUtilities.GivenName,
			ContactUtilities.IMAddress,
			ContactUtilities.InternationalIsdnNumber,
			ContactUtilities.HomeFax,
			ContactUtilities.HomePhone,
			ContactUtilities.HomePhone2,
			ContactUtilities.HomeStreet,
			ContactUtilities.HomeCity,
			ContactUtilities.HomeState,
			ContactUtilities.HomePostalCode,
			ContactUtilities.HomeCountry,
			ContactUtilities.Manager,
			ContactUtilities.MiddleName,
			ContactUtilities.MobilePhone,
			ContactUtilities.SurName,
			ContactUtilities.Title,
			ContactUtilities.OtherFax,
			ContactUtilities.OfficeLocation,
			ContactUtilities.OtherTelephone,
			ContactUtilities.OtherStreet,
			ContactUtilities.OtherCity,
			ContactUtilities.OtherState,
			ContactUtilities.OtherPostalCode,
			ContactUtilities.OtherCountry,
			ContactUtilities.OrganizationMainPhone,
			ContactUtilities.Pager,
			ContactUtilities.PostalAddressId,
			ContactUtilities.PrimaryTelephoneNumber,
			ContactUtilities.RadioPhone,
			ContactUtilities.TelexNumber,
			ContactUtilities.TtyTddPhoneNumber,
			ContactUtilities.YomiFirstName,
			ContactUtilities.YomiLastName,
			ContactUtilities.WebPage,
			ContactUtilities.WorkAddressStreet,
			ContactUtilities.WorkAddressCity,
			ContactUtilities.WorkAddressState,
			ContactUtilities.WorkAddressPostalCode,
			ContactUtilities.WorkAddressCountry,
			ContactUtilities.DefaultPhoneNumber
		};

		// Token: 0x040013D7 RID: 5079
		public static readonly ContactPropertyInfo[] PhoneNumberProperties = new ContactPropertyInfo[]
		{
			ContactUtilities.AssistantPhoneNumber,
			ContactUtilities.BusinessPhoneNumber2,
			ContactUtilities.FaxNumber,
			ContactUtilities.CallbackPhone,
			ContactUtilities.CarPhone,
			ContactUtilities.OrganizationMainPhone,
			ContactUtilities.HomePhone2,
			ContactUtilities.HomeFax,
			ContactUtilities.InternationalIsdnNumber,
			ContactUtilities.OtherTelephone,
			ContactUtilities.OtherFax,
			ContactUtilities.Pager,
			ContactUtilities.PrimaryTelephoneNumber,
			ContactUtilities.RadioPhone,
			ContactUtilities.TelexNumber,
			ContactUtilities.TtyTddPhoneNumber
		};

		// Token: 0x040013D8 RID: 5080
		public static readonly ContactPropertyInfo[] EmailAddressProperties = new ContactPropertyInfo[]
		{
			ContactUtilities.Email1EmailAddress,
			ContactUtilities.Email2EmailAddress,
			ContactUtilities.Email3EmailAddress
		};

		// Token: 0x040013D9 RID: 5081
		public static readonly EmailAddressIndex[] EmailAddressIndexesToRead = new EmailAddressIndex[]
		{
			EmailAddressIndex.Email1,
			EmailAddressIndex.Email2,
			EmailAddressIndex.Email3
		};

		// Token: 0x040013DA RID: 5082
		public static readonly EnumInfo<FileAsMapping>[] FileAsInfoTable = new EnumInfo<FileAsMapping>[]
		{
			new EnumInfo<FileAsMapping>(875365327, FileAsMapping.Company),
			new EnumInfo<FileAsMapping>(1688844756, FileAsMapping.CompanyLastCommaFirst),
			new EnumInfo<FileAsMapping>(1199067323, FileAsMapping.CompanyLastFirst),
			new EnumInfo<FileAsMapping>(-637514149, FileAsMapping.CompanyLastSpaceFirst),
			new EnumInfo<FileAsMapping>(-2034194004, FileAsMapping.FirstSpaceLast),
			new EnumInfo<FileAsMapping>(-45900891, FileAsMapping.LastCommaFirst),
			new EnumInfo<FileAsMapping>(-1429783810, FileAsMapping.LastCommaFirstCompany),
			new EnumInfo<FileAsMapping>(1262463098, FileAsMapping.LastFirst),
			new EnumInfo<FileAsMapping>(2123543599, FileAsMapping.LastFirstCompany),
			new EnumInfo<FileAsMapping>(1469096423, FileAsMapping.LastFirstSuffix),
			new EnumInfo<FileAsMapping>(1196310056, FileAsMapping.LastSpaceFirst),
			new EnumInfo<FileAsMapping>(-1994911189, FileAsMapping.LastSpaceFirstCompany)
		};

		// Token: 0x040013DB RID: 5083
		public static readonly EnumInfo<PhysicalAddressType>[] PhysicalAddressInfoTable = new EnumInfo<PhysicalAddressType>[]
		{
			new EnumInfo<PhysicalAddressType>(1414246128, PhysicalAddressType.None),
			new EnumInfo<PhysicalAddressType>(1414246315, PhysicalAddressType.Home),
			new EnumInfo<PhysicalAddressType>(-765825260, PhysicalAddressType.Business),
			new EnumInfo<PhysicalAddressType>(-582599340, PhysicalAddressType.Other)
		};

		// Token: 0x040013DC RID: 5084
		private static readonly ContactPropertyInfo[] HomeAddressParts = new ContactPropertyInfo[]
		{
			ContactUtilities.HomeStreet,
			ContactUtilities.HomeCity,
			ContactUtilities.HomeState,
			ContactUtilities.HomePostalCode,
			ContactUtilities.HomeCountry
		};

		// Token: 0x040013DD RID: 5085
		private static readonly ContactPropertyInfo[] BusinessAddressParts = new ContactPropertyInfo[]
		{
			ContactUtilities.WorkAddressStreet,
			ContactUtilities.WorkAddressCity,
			ContactUtilities.WorkAddressState,
			ContactUtilities.WorkAddressPostalCode,
			ContactUtilities.WorkAddressCountry
		};

		// Token: 0x040013DE RID: 5086
		private static readonly ContactPropertyInfo[] OtherAddressParts = new ContactPropertyInfo[]
		{
			ContactUtilities.OtherStreet,
			ContactUtilities.OtherCity,
			ContactUtilities.OtherState,
			ContactUtilities.OtherPostalCode,
			ContactUtilities.OtherCountry
		};

		// Token: 0x040013DF RID: 5087
		private static Dictionary<string, ContactPropertyInfo> idToInfoMap = ContactUtilities.CreateIdToInfoMap();

		// Token: 0x040013E0 RID: 5088
		private static Dictionary<FileAsMapping, Strings.IDs> fileAsEnumToStringMap = Utilities.CreateEnumLocalizedStringMap<FileAsMapping>(ContactUtilities.FileAsInfoTable);

		// Token: 0x040013E1 RID: 5089
		private static Dictionary<PhysicalAddressType, Strings.IDs> physicalAddressEnumToStringMap = Utilities.CreateEnumLocalizedStringMap<PhysicalAddressType>(ContactUtilities.PhysicalAddressInfoTable);

		// Token: 0x040013E2 RID: 5090
		private static PropertyDefinition[] prefetchProperties = ContactUtilities.CreateContactProperties();

		// Token: 0x040013E3 RID: 5091
		private static Dictionary<PhysicalAddressType, ContactPropertyInfo[]> addressPropertyTable = ContactUtilities.LoadAddressPropertyTable();
	}
}
