using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001A0 RID: 416
	internal sealed class ContactSchema : Schema
	{
		// Token: 0x06000B86 RID: 2950 RVA: 0x00037F8C File Offset: 0x0003618C
		static ContactSchema()
		{
			XmlElementInformation[] xmlElements = new XmlElementInformation[]
			{
				ContactSchema.FileAs,
				ContactSchema.FileAsMapping,
				ContactSchema.DisplayName,
				ContactSchema.GivenName,
				ContactSchema.Initials,
				ContactSchema.MiddleName,
				ContactSchema.Nickname,
				ContactSchema.CompleteName,
				ContactSchema.CompanyName,
				ContactSchema.EmailAddressesContainer,
				ContactSchema.EmailAddressEmailAddress1,
				ContactSchema.EmailAddressEmailAddress2,
				ContactSchema.EmailAddressEmailAddress3,
				ContactSchema.PhysicalAddressesContainer,
				ContactSchema.PhysicalAddressesBusinessContainer,
				ContactSchema.PhysicalAddressBusinessStreet,
				ContactSchema.PhysicalAddressBusinessCity,
				ContactSchema.PhysicalAddressBusinessState,
				ContactSchema.PhysicalAddressBusinessCountryOrRegion,
				ContactSchema.PhysicalAddressBusinessPostalCode,
				ContactSchema.PhysicalAddressesHomeContainer,
				ContactSchema.PhysicalAddressHomeStreet,
				ContactSchema.PhysicalAddressHomeCity,
				ContactSchema.PhysicalAddressHomeState,
				ContactSchema.PhysicalAddressHomeCountryOrRegion,
				ContactSchema.PhysicalAddressHomePostalCode,
				ContactSchema.PhysicalAddressesOtherContainer,
				ContactSchema.PhysicalAddressOtherStreet,
				ContactSchema.PhysicalAddressOtherCity,
				ContactSchema.PhysicalAddressOtherState,
				ContactSchema.PhysicalAddressOtherCountryOrRegion,
				ContactSchema.PhysicalAddressOtherPostalCode,
				ContactSchema.PhoneNumbersContainer,
				ContactSchema.PhoneNumberAssistantPhone,
				ContactSchema.PhoneNumberBusinessFax,
				ContactSchema.PhoneNumberBusinessPhone,
				ContactSchema.PhoneNumberBusinessPhone2,
				ContactSchema.PhoneNumberCallback,
				ContactSchema.PhoneNumberCarPhone,
				ContactSchema.PhoneNumberCompanyMainPhone,
				ContactSchema.PhoneNumberHomeFax,
				ContactSchema.PhoneNumberHomePhone,
				ContactSchema.PhoneNumberHomePhone2,
				ContactSchema.PhoneNumberIsdn,
				ContactSchema.PhoneNumberMobilePhone,
				ContactSchema.PhoneNumberOtherFax,
				ContactSchema.PhoneNumberOtherTelephone,
				ContactSchema.PhoneNumberPager,
				ContactSchema.PhoneNumberPrimaryPhone,
				ContactSchema.PhoneNumberRadioPhone,
				ContactSchema.PhoneNumberTelex,
				ContactSchema.PhoneNumberTtyTddPhone,
				ContactSchema.AssistantName,
				ContactSchema.Birthday,
				ContactSchema.BirthdayLocal,
				ContactSchema.BusinessHomePage,
				ContactSchema.Children,
				ContactSchema.Companies,
				ContactSchema.Department,
				ContactSchema.Generation,
				ContactSchema.ImAddressesContainer,
				ContactSchema.ImAddressImAddress1,
				ContactSchema.ImAddressImAddress2,
				ContactSchema.ImAddressImAddress3,
				ContactSchema.JobTitle,
				ContactSchema.Manager,
				ContactSchema.Mileage,
				ContactSchema.OfficeLocation,
				ContactSchema.PostalAddressIndex,
				ContactSchema.Profession,
				ContactSchema.SpouseName,
				ContactSchema.Surname,
				ContactSchema.WeddingAnniversary,
				ContactSchema.WeddingAnniversaryLocal,
				ContactSchema.HasPicture,
				ContactSchema.PersonaType
			};
			ContactSchema.schema = new ContactSchema(xmlElements);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00038D7E File Offset: 0x00036F7E
		private ContactSchema(XmlElementInformation[] xmlElements) : base(xmlElements)
		{
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00038D87 File Offset: 0x00036F87
		public static Schema GetSchema()
		{
			return ContactSchema.schema;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00038D8E File Offset: 0x00036F8E
		private static string DictionaryEntryPath(string keyValue)
		{
			if (string.IsNullOrEmpty(keyValue))
			{
				return null;
			}
			return "/Entry[@Key='" + keyValue + "']";
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00038DAA File Offset: 0x00036FAA
		private static ContainerInformation CreateDictionaryEntryContainerInformation(PropertyUriEnum propertyUriEnum, string attributeKeyValue, ExchangeVersion effectiveVersion)
		{
			return new ContainerInformation(propertyUriEnum.ToString() + ContactSchema.DictionaryEntryPath(attributeKeyValue), ServiceXml.GetFullyQualifiedName(propertyUriEnum.ToString() + ContactSchema.DictionaryEntryPath(attributeKeyValue)), effectiveVersion);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00038DE4 File Offset: 0x00036FE4
		private static PropertyInformation CreateDictionaryEntryPropertyInformation(DictionaryUriEnum dictionaryUriEnum, PropertyUriEnum propertyUriEnum, string attributeKeyValue, ExchangeVersion effectiveVersion, PropertyDefinition[] propertyDefinitions, PropertyCommand.CreatePropertyCommand createPropertyCommand)
		{
			return new PropertyInformation(propertyUriEnum.ToString(), ServiceXml.GetFullyQualifiedName(propertyUriEnum.ToString() + ContactSchema.DictionaryEntryPath(attributeKeyValue)), ServiceXml.DefaultNamespaceUri, effectiveVersion, propertyDefinitions, new DictionaryPropertyUri(dictionaryUriEnum, attributeKeyValue), createPropertyCommand, PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00038E34 File Offset: 0x00037034
		private static PropertyInformation CreatePhysicalAddressPropertyInformation(string attributeKeyValue, string xmlElementName, DictionaryUriEnum dictionaryUriEnum, ExchangeVersion effectiveVersion, PropertyDefinition propertyDefinition, PropertyCommand.CreatePropertyCommand createPropertyCommand)
		{
			return new PropertyInformation(PropertyUriEnum.PhysicalAddresses.ToString(), ServiceXml.GetFullyQualifiedName(PropertyUriEnum.PhysicalAddresses.ToString() + ContactSchema.DictionaryEntryPath(attributeKeyValue) + "/" + xmlElementName), ServiceXml.DefaultNamespaceUri, effectiveVersion, new PropertyDefinition[]
			{
				propertyDefinition
			}, new DictionaryPropertyUri(dictionaryUriEnum, attributeKeyValue), createPropertyCommand, PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00038E9C File Offset: 0x0003709C
		private static PropertyInformation CreatePhonePropertyInformation(string attributeKeyValue, ExchangeVersion effectiveVersion, PropertyDefinition propertyDefinition)
		{
			return ContactSchema.CreateDictionaryEntryPropertyInformation(DictionaryUriEnum.PhoneNumber, PropertyUriEnum.PhoneNumbers, attributeKeyValue, effectiveVersion, new PropertyDefinition[]
			{
				propertyDefinition
			}, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryProperty.CreateCommandForPhoneNumbers));
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00038ED0 File Offset: 0x000370D0
		private static PropertyInformation CreateImAddressPropertyInformation(string attributeKeyValue, ExchangeVersion effectiveVersion, PropertyDefinition propertyDefinition)
		{
			return ContactSchema.CreateDictionaryEntryPropertyInformation(DictionaryUriEnum.ImAddress, PropertyUriEnum.ImAddresses, attributeKeyValue, effectiveVersion, new PropertyDefinition[]
			{
				propertyDefinition
			}, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryProperty.CreateCommandForImAddresses));
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00038F04 File Offset: 0x00037104
		private static PropertyInformation CreateEmailAddressPropertyInformation(string attributeKeyValue, ExchangeVersion effectiveVersion, PropertyDefinition propertyDefinition)
		{
			return ContactSchema.CreateDictionaryEntryPropertyInformation(DictionaryUriEnum.EmailAddress, PropertyUriEnum.EmailAddresses, attributeKeyValue, effectiveVersion, new PropertyDefinition[]
			{
				propertyDefinition
			}, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryProperty.CreateCommandForEmailAddresses));
		}

		// Token: 0x04000877 RID: 2167
		private const string BusinessValue = "Business";

		// Token: 0x04000878 RID: 2168
		private const string HomeValue = "Home";

		// Token: 0x04000879 RID: 2169
		private const string OtherValue = "Other";

		// Token: 0x0400087A RID: 2170
		private const string StreetValue = "Street";

		// Token: 0x0400087B RID: 2171
		private const string CityValue = "City";

		// Token: 0x0400087C RID: 2172
		private const string CountryOrRegionValue = "CountryOrRegion";

		// Token: 0x0400087D RID: 2173
		private const string StateValue = "State";

		// Token: 0x0400087E RID: 2174
		private const string PostalCodeValue = "PostalCode";

		// Token: 0x0400087F RID: 2175
		private static Schema schema;

		// Token: 0x04000880 RID: 2176
		public static readonly PropertyInformation AssistantName = new PropertyInformation(PropertyUriEnum.AssistantName, ExchangeVersion.Exchange2007, ContactSchema.AssistantName, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000881 RID: 2177
		public static readonly PropertyInformation Birthday = new PropertyInformation(PropertyUriEnum.Birthday, ExchangeVersion.Exchange2007, ContactSchema.Birthday, new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000882 RID: 2178
		public static readonly PropertyInformation BirthdayLocal = new PropertyInformation(PropertyUriEnum.BirthdayLocal, ExchangeVersion.Exchange2012, ContactSchema.BirthdayLocal, new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000883 RID: 2179
		public static readonly PropertyInformation BusinessHomePage = new PropertyInformation(PropertyUriEnum.BusinessHomePage, ExchangeVersion.Exchange2007, ContactSchema.BusinessHomePage, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000884 RID: 2180
		public static readonly PropertyInformation Children = new ArrayPropertyInformation(PropertyUriEnum.Children.ToString(), ExchangeVersion.Exchange2007, "String", ContactSchema.Children, new PropertyUri(PropertyUriEnum.Children), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x04000885 RID: 2181
		public static readonly PropertyInformation Companies = new ArrayPropertyInformation(PropertyUriEnum.Companies.ToString(), ExchangeVersion.Exchange2007, "String", ContactSchema.Companies, new PropertyUri(PropertyUriEnum.Companies), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x04000886 RID: 2182
		public static readonly PropertyInformation CompanyName = new PropertyInformation(PropertyUriEnum.CompanyName, ExchangeVersion.Exchange2007, ContactSchema.CompanyName, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000887 RID: 2183
		public static readonly PropertyInformation CompleteName = new PropertyInformation(PropertyUriEnum.CompleteName.ToString(), ServiceXml.GetFullyQualifiedName(PropertyUriEnum.CompleteName.ToString()), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2007, new PropertyDefinition[]
		{
			ContactSchema.DisplayNamePrefix,
			ContactSchema.GivenName,
			ContactSchema.MiddleName,
			ContactSchema.Surname,
			ContactSchema.Generation,
			ContactSchema.Initials,
			StoreObjectSchema.DisplayName,
			ContactSchema.Nickname,
			ContactSchema.YomiFirstName,
			ContactSchema.YomiLastName
		}, new PropertyUri(PropertyUriEnum.CompleteName), new PropertyCommand.CreatePropertyCommand(CompleteNameProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000888 RID: 2184
		public static readonly PropertyInformation DisplayName = new PropertyInformation(PropertyUriEnum.DisplayName, ExchangeVersion.Exchange2007, StoreObjectSchema.DisplayName, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000889 RID: 2185
		public static readonly PropertyInformation Department = new PropertyInformation(PropertyUriEnum.Department, ExchangeVersion.Exchange2007, ContactSchema.Department, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x0400088A RID: 2186
		public static readonly ContainerInformation EmailAddressesContainer = ContactSchema.CreateDictionaryEntryContainerInformation(PropertyUriEnum.EmailAddresses, null, ExchangeVersion.Exchange2007);

		// Token: 0x0400088B RID: 2187
		public static readonly PropertyInformation EmailAddressEmailAddress1 = ContactSchema.CreateEmailAddressPropertyInformation("EmailAddress1", ExchangeVersion.Exchange2007, ContactSchema.Email1EmailAddress);

		// Token: 0x0400088C RID: 2188
		public static readonly PropertyInformation EmailAddressEmailAddress2 = ContactSchema.CreateEmailAddressPropertyInformation("EmailAddress2", ExchangeVersion.Exchange2007, ContactSchema.Email2EmailAddress);

		// Token: 0x0400088D RID: 2189
		public static readonly PropertyInformation EmailAddressEmailAddress3 = ContactSchema.CreateEmailAddressPropertyInformation("EmailAddress3", ExchangeVersion.Exchange2007, ContactSchema.Email3EmailAddress);

		// Token: 0x0400088E RID: 2190
		public static readonly PropertyInformation FileAs = new PropertyInformation(PropertyUriEnum.FileAs, ExchangeVersion.Exchange2007, ContactBaseSchema.FileAs, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x0400088F RID: 2191
		public static readonly PropertyInformation FileAsMapping = new PropertyInformation(PropertyUriEnum.FileAsMapping, ExchangeVersion.Exchange2007, ContactSchema.FileAsId, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommandForDoNonReturnNonRepresentableProperty), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000890 RID: 2192
		public static readonly PropertyInformation Generation = new PropertyInformation(PropertyUriEnum.Generation, ExchangeVersion.Exchange2007, ContactSchema.Generation, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000891 RID: 2193
		public static readonly PropertyInformation GivenName = new PropertyInformation(PropertyUriEnum.GivenName, ExchangeVersion.Exchange2007, ContactSchema.GivenName, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000892 RID: 2194
		public static readonly ContainerInformation ImAddressesContainer = ContactSchema.CreateDictionaryEntryContainerInformation(PropertyUriEnum.ImAddresses, null, ExchangeVersion.Exchange2007);

		// Token: 0x04000893 RID: 2195
		public static readonly PropertyInformation ImAddressImAddress1 = ContactSchema.CreateImAddressPropertyInformation("ImAddress1", ExchangeVersion.Exchange2007, ContactSchema.IMAddress);

		// Token: 0x04000894 RID: 2196
		public static readonly PropertyInformation ImAddressImAddress2 = ContactSchema.CreateImAddressPropertyInformation("ImAddress2", ExchangeVersion.Exchange2007, ContactSchema.IMAddress2);

		// Token: 0x04000895 RID: 2197
		public static readonly PropertyInformation ImAddressImAddress3 = ContactSchema.CreateImAddressPropertyInformation("ImAddress3", ExchangeVersion.Exchange2007, ContactSchema.IMAddress3);

		// Token: 0x04000896 RID: 2198
		public static readonly PropertyInformation Initials = new PropertyInformation(PropertyUriEnum.Initials, ExchangeVersion.Exchange2007, ContactSchema.Initials, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000897 RID: 2199
		public static readonly PropertyInformation Mileage = new PropertyInformation(PropertyUriEnum.Mileage, ExchangeVersion.Exchange2007, ContactSchema.Mileage, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000898 RID: 2200
		public static readonly PropertyInformation JobTitle = new PropertyInformation(PropertyUriEnum.JobTitle, ExchangeVersion.Exchange2007, ContactSchema.Title, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000899 RID: 2201
		public static readonly PropertyInformation PostalAddressIndex = new PropertyInformation(PropertyUriEnum.PostalAddressIndex, ExchangeVersion.Exchange2007, ContactSchema.PostalAddressId, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x0400089A RID: 2202
		public static readonly PropertyInformation Manager = new PropertyInformation(PropertyUriEnum.Manager, ExchangeVersion.Exchange2007, ContactSchema.Manager, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x0400089B RID: 2203
		public static readonly PropertyInformation MiddleName = new PropertyInformation(PropertyUriEnum.MiddleName, ExchangeVersion.Exchange2007, ContactSchema.MiddleName, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x0400089C RID: 2204
		public static readonly PropertyInformation Nickname = new PropertyInformation(PropertyUriEnum.Nickname, ExchangeVersion.Exchange2007, ContactSchema.Nickname, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x0400089D RID: 2205
		public static readonly PropertyInformation OfficeLocation = new PropertyInformation(PropertyUriEnum.OfficeLocation, ExchangeVersion.Exchange2007, ContactSchema.OfficeLocation, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x0400089E RID: 2206
		public static readonly PropertyInformation PersonaType = new PropertyInformation(PropertyUriEnum.PersonaType, ExchangeVersion.Exchange2012, ContactSchema.PersonType, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400089F RID: 2207
		public static readonly ContainerInformation PhoneNumbersContainer = ContactSchema.CreateDictionaryEntryContainerInformation(PropertyUriEnum.PhoneNumbers, null, ExchangeVersion.Exchange2007);

		// Token: 0x040008A0 RID: 2208
		public static readonly PropertyInformation PhoneNumberAssistantPhone = ContactSchema.CreatePhonePropertyInformation("AssistantPhone", ExchangeVersion.Exchange2007, ContactSchema.AssistantPhoneNumber);

		// Token: 0x040008A1 RID: 2209
		public static readonly PropertyInformation PhoneNumberBusinessFax = ContactSchema.CreatePhonePropertyInformation("BusinessFax", ExchangeVersion.Exchange2007, ContactSchema.WorkFax);

		// Token: 0x040008A2 RID: 2210
		public static readonly PropertyInformation PhoneNumberBusinessPhone = ContactSchema.CreatePhonePropertyInformation("BusinessPhone", ExchangeVersion.Exchange2007, ContactSchema.BusinessPhoneNumber);

		// Token: 0x040008A3 RID: 2211
		public static readonly PropertyInformation PhoneNumberBusinessPhone2 = ContactSchema.CreatePhonePropertyInformation("BusinessPhone2", ExchangeVersion.Exchange2007, ContactSchema.BusinessPhoneNumber2);

		// Token: 0x040008A4 RID: 2212
		public static readonly PropertyInformation PhoneNumberCallback = ContactSchema.CreatePhonePropertyInformation("Callback", ExchangeVersion.Exchange2007, ContactSchema.CallbackPhone);

		// Token: 0x040008A5 RID: 2213
		public static readonly PropertyInformation PhoneNumberCarPhone = ContactSchema.CreatePhonePropertyInformation("CarPhone", ExchangeVersion.Exchange2007, ContactSchema.CarPhone);

		// Token: 0x040008A6 RID: 2214
		public static readonly PropertyInformation PhoneNumberCompanyMainPhone = ContactSchema.CreatePhonePropertyInformation("CompanyMainPhone", ExchangeVersion.Exchange2007, ContactSchema.OrganizationMainPhone);

		// Token: 0x040008A7 RID: 2215
		public static readonly PropertyInformation PhoneNumberHomeFax = ContactSchema.CreatePhonePropertyInformation("HomeFax", ExchangeVersion.Exchange2007, ContactSchema.HomeFax);

		// Token: 0x040008A8 RID: 2216
		public static readonly PropertyInformation PhoneNumberHomePhone = ContactSchema.CreatePhonePropertyInformation("HomePhone", ExchangeVersion.Exchange2007, ContactSchema.HomePhone);

		// Token: 0x040008A9 RID: 2217
		public static readonly PropertyInformation PhoneNumberHomePhone2 = ContactSchema.CreatePhonePropertyInformation("HomePhone2", ExchangeVersion.Exchange2007, ContactSchema.HomePhone2);

		// Token: 0x040008AA RID: 2218
		public static readonly PropertyInformation PhoneNumberIsdn = ContactSchema.CreatePhonePropertyInformation("Isdn", ExchangeVersion.Exchange2007, ContactSchema.InternationalIsdnNumber);

		// Token: 0x040008AB RID: 2219
		public static readonly PropertyInformation PhoneNumberMobilePhone = ContactSchema.CreatePhonePropertyInformation("MobilePhone", ExchangeVersion.Exchange2007, ContactSchema.MobilePhone);

		// Token: 0x040008AC RID: 2220
		public static readonly PropertyInformation PhoneNumberOtherTelephone = ContactSchema.CreatePhonePropertyInformation("OtherTelephone", ExchangeVersion.Exchange2007, ContactSchema.OtherTelephone);

		// Token: 0x040008AD RID: 2221
		public static readonly PropertyInformation PhoneNumberOtherFax = ContactSchema.CreatePhonePropertyInformation("OtherFax", ExchangeVersion.Exchange2007, ContactSchema.OtherFax);

		// Token: 0x040008AE RID: 2222
		public static readonly PropertyInformation PhoneNumberPager = ContactSchema.CreatePhonePropertyInformation("Pager", ExchangeVersion.Exchange2007, ContactSchema.Pager);

		// Token: 0x040008AF RID: 2223
		public static readonly PropertyInformation PhoneNumberPrimaryPhone = ContactSchema.CreatePhonePropertyInformation("PrimaryPhone", ExchangeVersion.Exchange2007, ContactSchema.PrimaryTelephoneNumber);

		// Token: 0x040008B0 RID: 2224
		public static readonly PropertyInformation PhoneNumberRadioPhone = ContactSchema.CreatePhonePropertyInformation("RadioPhone", ExchangeVersion.Exchange2007, ContactSchema.RadioPhone);

		// Token: 0x040008B1 RID: 2225
		public static readonly PropertyInformation PhoneNumberTelex = ContactSchema.CreatePhonePropertyInformation("Telex", ExchangeVersion.Exchange2007, ContactSchema.TelexNumber);

		// Token: 0x040008B2 RID: 2226
		public static readonly PropertyInformation PhoneNumberTtyTddPhone = ContactSchema.CreatePhonePropertyInformation("TtyTddPhone", ExchangeVersion.Exchange2007, ContactSchema.TtyTddPhoneNumber);

		// Token: 0x040008B3 RID: 2227
		public static readonly ContainerInformation PhysicalAddressesContainer = ContactSchema.CreateDictionaryEntryContainerInformation(PropertyUriEnum.PhysicalAddresses, null, ExchangeVersion.Exchange2007);

		// Token: 0x040008B4 RID: 2228
		public static readonly ContainerInformation PhysicalAddressesBusinessContainer = ContactSchema.CreateDictionaryEntryContainerInformation(PropertyUriEnum.PhysicalAddresses, "Business", ExchangeVersion.Exchange2007);

		// Token: 0x040008B5 RID: 2229
		public static readonly PropertyInformation PhysicalAddressBusinessStreet = ContactSchema.CreatePhysicalAddressPropertyInformation("Business", "Street", DictionaryUriEnum.PhysicalAddressStreet, ExchangeVersion.Exchange2007, ContactSchema.WorkAddressStreet, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryNestedProperty.CreateCommandForStreet));

		// Token: 0x040008B6 RID: 2230
		public static readonly PropertyInformation PhysicalAddressBusinessCity = ContactSchema.CreatePhysicalAddressPropertyInformation("Business", "City", DictionaryUriEnum.PhysicalAddressCity, ExchangeVersion.Exchange2007, ContactSchema.WorkAddressCity, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryNestedProperty.CreateCommandForCity));

		// Token: 0x040008B7 RID: 2231
		public static readonly PropertyInformation PhysicalAddressBusinessState = ContactSchema.CreatePhysicalAddressPropertyInformation("Business", "State", DictionaryUriEnum.PhysicalAddressState, ExchangeVersion.Exchange2007, ContactSchema.WorkAddressState, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryNestedProperty.CreateCommandForState));

		// Token: 0x040008B8 RID: 2232
		public static readonly PropertyInformation PhysicalAddressBusinessCountryOrRegion = ContactSchema.CreatePhysicalAddressPropertyInformation("Business", "CountryOrRegion", DictionaryUriEnum.PhysicalAddressCountryOrRegion, ExchangeVersion.Exchange2007, ContactSchema.WorkAddressCountry, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryNestedProperty.CreateCommandForCountryOrRegion));

		// Token: 0x040008B9 RID: 2233
		public static readonly PropertyInformation PhysicalAddressBusinessPostalCode = ContactSchema.CreatePhysicalAddressPropertyInformation("Business", "PostalCode", DictionaryUriEnum.PhysicalAddressPostalCode, ExchangeVersion.Exchange2007, ContactSchema.WorkAddressPostalCode, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryNestedProperty.CreateCommandForPostalCode));

		// Token: 0x040008BA RID: 2234
		public static readonly ContainerInformation PhysicalAddressesHomeContainer = ContactSchema.CreateDictionaryEntryContainerInformation(PropertyUriEnum.PhysicalAddresses, "Home", ExchangeVersion.Exchange2007);

		// Token: 0x040008BB RID: 2235
		public static readonly PropertyInformation PhysicalAddressHomeStreet = ContactSchema.CreatePhysicalAddressPropertyInformation("Home", "Street", DictionaryUriEnum.PhysicalAddressStreet, ExchangeVersion.Exchange2007, ContactSchema.HomeStreet, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryNestedProperty.CreateCommandForStreet));

		// Token: 0x040008BC RID: 2236
		public static readonly PropertyInformation PhysicalAddressHomeCity = ContactSchema.CreatePhysicalAddressPropertyInformation("Home", "City", DictionaryUriEnum.PhysicalAddressCity, ExchangeVersion.Exchange2007, ContactSchema.HomeCity, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryNestedProperty.CreateCommandForCity));

		// Token: 0x040008BD RID: 2237
		public static readonly PropertyInformation PhysicalAddressHomeState = ContactSchema.CreatePhysicalAddressPropertyInformation("Home", "State", DictionaryUriEnum.PhysicalAddressState, ExchangeVersion.Exchange2007, ContactSchema.HomeState, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryNestedProperty.CreateCommandForState));

		// Token: 0x040008BE RID: 2238
		public static readonly PropertyInformation PhysicalAddressHomeCountryOrRegion = ContactSchema.CreatePhysicalAddressPropertyInformation("Home", "CountryOrRegion", DictionaryUriEnum.PhysicalAddressCountryOrRegion, ExchangeVersion.Exchange2007, ContactSchema.HomeCountry, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryNestedProperty.CreateCommandForCountryOrRegion));

		// Token: 0x040008BF RID: 2239
		public static readonly PropertyInformation PhysicalAddressHomePostalCode = ContactSchema.CreatePhysicalAddressPropertyInformation("Home", "PostalCode", DictionaryUriEnum.PhysicalAddressPostalCode, ExchangeVersion.Exchange2007, ContactSchema.HomePostalCode, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryNestedProperty.CreateCommandForPostalCode));

		// Token: 0x040008C0 RID: 2240
		public static readonly ContainerInformation PhysicalAddressesOtherContainer = ContactSchema.CreateDictionaryEntryContainerInformation(PropertyUriEnum.PhysicalAddresses, "Other", ExchangeVersion.Exchange2007);

		// Token: 0x040008C1 RID: 2241
		public static readonly PropertyInformation PhysicalAddressOtherStreet = ContactSchema.CreatePhysicalAddressPropertyInformation("Other", "Street", DictionaryUriEnum.PhysicalAddressStreet, ExchangeVersion.Exchange2007, ContactSchema.OtherStreet, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryNestedProperty.CreateCommandForStreet));

		// Token: 0x040008C2 RID: 2242
		public static readonly PropertyInformation PhysicalAddressOtherCity = ContactSchema.CreatePhysicalAddressPropertyInformation("Other", "City", DictionaryUriEnum.PhysicalAddressCity, ExchangeVersion.Exchange2007, ContactSchema.OtherCity, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryNestedProperty.CreateCommandForCity));

		// Token: 0x040008C3 RID: 2243
		public static readonly PropertyInformation PhysicalAddressOtherState = ContactSchema.CreatePhysicalAddressPropertyInformation("Other", "State", DictionaryUriEnum.PhysicalAddressState, ExchangeVersion.Exchange2007, ContactSchema.OtherState, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryNestedProperty.CreateCommandForState));

		// Token: 0x040008C4 RID: 2244
		public static readonly PropertyInformation PhysicalAddressOtherCountryOrRegion = ContactSchema.CreatePhysicalAddressPropertyInformation("Other", "CountryOrRegion", DictionaryUriEnum.PhysicalAddressCountryOrRegion, ExchangeVersion.Exchange2007, ContactSchema.OtherCountry, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryNestedProperty.CreateCommandForCountryOrRegion));

		// Token: 0x040008C5 RID: 2245
		public static readonly PropertyInformation PhysicalAddressOtherPostalCode = ContactSchema.CreatePhysicalAddressPropertyInformation("Other", "PostalCode", DictionaryUriEnum.PhysicalAddressPostalCode, ExchangeVersion.Exchange2007, ContactSchema.OtherPostalCode, new PropertyCommand.CreatePropertyCommand(ContactDictionaryEntryNestedProperty.CreateCommandForPostalCode));

		// Token: 0x040008C6 RID: 2246
		public static readonly PropertyInformation Profession = new PropertyInformation(PropertyUriEnum.Profession, ExchangeVersion.Exchange2007, ContactSchema.Profession, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x040008C7 RID: 2247
		public static readonly PropertyInformation SpouseName = new PropertyInformation(PropertyUriEnum.SpouseName, ExchangeVersion.Exchange2007, ContactSchema.SpouseName, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x040008C8 RID: 2248
		public static readonly PropertyInformation Surname = new PropertyInformation(PropertyUriEnum.Surname, ExchangeVersion.Exchange2007, ContactSchema.Surname, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x040008C9 RID: 2249
		public static readonly PropertyInformation WeddingAnniversary = new PropertyInformation(PropertyUriEnum.WeddingAnniversary, ExchangeVersion.Exchange2007, ContactSchema.WeddingAnniversary, new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x040008CA RID: 2250
		public static readonly PropertyInformation WeddingAnniversaryLocal = new PropertyInformation(PropertyUriEnum.WeddingAnniversaryLocal, ExchangeVersion.Exchange2012, ContactSchema.WeddingAnniversaryLocal, new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x040008CB RID: 2251
		public static PropertyDefinition HasPicturePropertyDef = GuidIdPropertyDefinition.CreateCustom("hasPicture", typeof(bool), new Guid("{00062004-0000-0000-C000-000000000046}"), 32789, PropertyFlags.None);

		// Token: 0x040008CC RID: 2252
		public static readonly PropertyInformation HasPicture = new PropertyInformation(PropertyUriEnum.HasPicture, ExchangeVersion.Exchange2010, ContactSchema.HasPicturePropertyDef, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);
	}
}
