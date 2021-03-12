using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001C2 RID: 450
	internal sealed class PersonaSchema : Schema
	{
		// Token: 0x06000C54 RID: 3156 RVA: 0x0003E9AC File Offset: 0x0003CBAC
		static PersonaSchema()
		{
			XmlElementInformation[] xmlElements = new XmlElementInformation[]
			{
				PersonaSchema.PersonaId,
				PersonaSchema.PersonaType,
				PersonaSchema.CreationTime,
				PersonaSchema.IsFavorite,
				PersonaSchema.DisplayNameFirstLastSortKey,
				PersonaSchema.DisplayNameLastFirstSortKey,
				PersonaSchema.CompanyNameSortKey,
				PersonaSchema.HomeCitySortKey,
				PersonaSchema.WorkCitySortKey,
				PersonaSchema.DisplayNameFirstLastHeader,
				PersonaSchema.DisplayNameLastFirstHeader,
				PersonaSchema.DisplayName,
				PersonaSchema.DisplayNameFirstLast,
				PersonaSchema.DisplayNameLastFirst,
				PersonaSchema.FileAs,
				PersonaSchema.FileAsId,
				PersonaSchema.DisplayNamePrefix,
				PersonaSchema.GivenName,
				PersonaSchema.MiddleName,
				PersonaSchema.Surname,
				PersonaSchema.Generation,
				PersonaSchema.Nickname,
				PersonaSchema.YomiCompanyName,
				PersonaSchema.YomiFirstName,
				PersonaSchema.YomiLastName,
				PersonaSchema.Title,
				PersonaSchema.Department,
				PersonaSchema.CompanyName,
				PersonaSchema.Location,
				PersonaSchema.EmailAddress,
				PersonaSchema.EmailAddresses,
				PersonaSchema.PhoneNumber,
				PersonaSchema.ImAddress,
				PersonaSchema.HomeCity,
				PersonaSchema.WorkCity,
				PersonaSchema.Alias,
				PersonaSchema.RelevanceScore,
				PersonaSchema.FolderIds,
				PersonaSchema.Attributions,
				PersonaSchema.Members,
				PersonaSchema.DisplayNames,
				PersonaSchema.FileAses,
				PersonaSchema.FileAsIds,
				PersonaSchema.DisplayNamePrefixes,
				PersonaSchema.GivenNames,
				PersonaSchema.MiddleNames,
				PersonaSchema.Surnames,
				PersonaSchema.Generations,
				PersonaSchema.Nicknames,
				PersonaSchema.Initials,
				PersonaSchema.YomiCompanyNames,
				PersonaSchema.YomiFirstNames,
				PersonaSchema.YomiLastNames,
				PersonaSchema.BusinessPhoneNumbers,
				PersonaSchema.BusinessPhoneNumbers2,
				PersonaSchema.HomePhones,
				PersonaSchema.HomePhones2,
				PersonaSchema.MobilePhones,
				PersonaSchema.MobilePhones2,
				PersonaSchema.AssistantPhoneNumbers,
				PersonaSchema.CallbackPhones,
				PersonaSchema.CarPhones,
				PersonaSchema.HomeFaxes,
				PersonaSchema.OrganizationMainPhones,
				PersonaSchema.OtherFaxes,
				PersonaSchema.OtherTelephones,
				PersonaSchema.OtherPhones2,
				PersonaSchema.Pagers,
				PersonaSchema.RadioPhones,
				PersonaSchema.TelexNumbers,
				PersonaSchema.TTYTDDPhoneNumbers,
				PersonaSchema.WorkFaxes,
				PersonaSchema.Emails1,
				PersonaSchema.Emails2,
				PersonaSchema.Emails3,
				PersonaSchema.BusinessHomePages,
				PersonaSchema.PersonalHomePages,
				PersonaSchema.OfficeLocations,
				PersonaSchema.ImAddresses,
				PersonaSchema.ImAddresses2,
				PersonaSchema.ImAddresses3,
				PersonaSchema.BusinessAddresses,
				PersonaSchema.HomeAddresses,
				PersonaSchema.OtherAddresses,
				PersonaSchema.Titles,
				PersonaSchema.Departments,
				PersonaSchema.CompanyNames,
				PersonaSchema.Managers,
				PersonaSchema.AssistantNames,
				PersonaSchema.Professions,
				PersonaSchema.SpouseNames,
				PersonaSchema.Children,
				PersonaSchema.Hobbies,
				PersonaSchema.WeddingAnniversaries,
				PersonaSchema.Birthdays,
				PersonaSchema.WeddingAnniversariesLocal,
				PersonaSchema.BirthdaysLocal,
				PersonaSchema.Locations,
				PersonaSchema.ExtendedProperties,
				PersonaSchema.Schools,
				PersonaSchema.ThirdPartyPhotoUrls,
				PersonaSchema.UnreadCount,
				PersonaSchema.ADObjectId
			};
			PersonaSchema.EwsCalculatedProperties = new HashSet<PropertyPath>
			{
				PersonaSchema.DisplayNameFirstLastSortKey.PropertyPath,
				PersonaSchema.DisplayNameLastFirstSortKey.PropertyPath,
				PersonaSchema.CompanyNameSortKey.PropertyPath,
				PersonaSchema.HomeCitySortKey.PropertyPath,
				PersonaSchema.WorkCitySortKey.PropertyPath,
				PersonaSchema.DisplayNameFirstLastHeader.PropertyPath,
				PersonaSchema.DisplayNameLastFirstHeader.PropertyPath,
				PersonaSchema.Bodies.PropertyPath,
				PersonaSchema.ExtendedProperties.PropertyPath,
				PersonaSchema.UnreadCount.PropertyPath
			};
			PersonaSchema.AllPropertiesExclusionList = new HashSet<PropertyInformation>
			{
				PersonaSchema.DisplayNameFirstLastSortKey,
				PersonaSchema.DisplayNameLastFirstSortKey,
				PersonaSchema.CompanyNameSortKey,
				PersonaSchema.HomeCitySortKey,
				PersonaSchema.WorkCitySortKey,
				PersonaSchema.DisplayNameFirstLastHeader,
				PersonaSchema.DisplayNameLastFirstHeader,
				PersonaSchema.FolderIds,
				PersonaSchema.Members,
				PersonaSchema.ThirdPartyPhotoUrls,
				PersonaSchema.Alias,
				PersonaSchema.UnreadCount
			};
			PersonaSchema.schema = new PersonaSchema(xmlElements, PersonaSchema.PersonaId);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00040318 File Offset: 0x0003E518
		private PersonaSchema(XmlElementInformation[] xmlElements, PropertyInformation personaIdPropertyInformation) : base(xmlElements, personaIdPropertyInformation)
		{
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00040322 File Offset: 0x0003E522
		public static Schema GetSchema()
		{
			return PersonaSchema.schema;
		}

		// Token: 0x040009AE RID: 2478
		private static Schema schema;

		// Token: 0x040009AF RID: 2479
		public static readonly HashSet<PropertyInformation> AllPropertiesExclusionList;

		// Token: 0x040009B0 RID: 2480
		public static readonly HashSet<PropertyPath> EwsCalculatedProperties;

		// Token: 0x040009B1 RID: 2481
		public static readonly PropertyInformation PersonaId = new PropertyInformation("PersonaId", ServiceXml.GetFullyQualifiedName("PersonaId"), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2012, new PropertyDefinition[]
		{
			PersonSchema.Id,
			PersonSchema.GALLinkID
		}, new PropertyUri(PropertyUriEnum.PersonaId), new PropertyCommand.CreatePropertyCommand(PersonaIdProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009B2 RID: 2482
		public static readonly PropertyInformation PersonaType = new PropertyInformation("PersonaType", ExchangeVersion.Exchange2012, PersonSchema.PersonType, new PropertyUri(PropertyUriEnum.PersonaType), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009B3 RID: 2483
		public static readonly PropertyInformation ADObjectId = new PropertyInformation("ADObjectId", ExchangeVersion.Exchange2012, PersonSchema.GALLinkID, new PropertyUri(PropertyUriEnum.PersonaADObjectId), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009B4 RID: 2484
		public static readonly PropertyInformation CreationTime = new PropertyInformation("CreationTime", ExchangeVersion.Exchange2012, PersonSchema.CreationTime, new PropertyUri(PropertyUriEnum.PersonaCreationTime), new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009B5 RID: 2485
		public static readonly PropertyInformation IsFavorite = new PropertyInformation("IsFavorite", ExchangeVersion.Exchange2012, PersonSchema.IsFavorite, new PropertyUri(PropertyUriEnum.PersonaIsFavorite), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009B6 RID: 2486
		public static readonly PropertyInformation DisplayName = new PropertyInformation(PropertyUriEnum.PersonaDisplayName, ExchangeVersion.Exchange2012, PersonSchema.DisplayName, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009B7 RID: 2487
		public static readonly PropertyInformation DisplayNameFirstLast = new PropertyInformation(PropertyUriEnum.PersonaDisplayNameFirstLast, ExchangeVersion.Exchange2012, PersonSchema.DisplayNameFirstLast, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009B8 RID: 2488
		public static readonly PropertyInformation DisplayNameLastFirst = new PropertyInformation(PropertyUriEnum.PersonaDisplayNameLastFirst, ExchangeVersion.Exchange2012, PersonSchema.DisplayNameLastFirst, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009B9 RID: 2489
		public static readonly PropertyInformation DisplayNameFirstLastSortKey = new PropertyInformation(PropertyUriEnum.PersonaDisplayNameFirstLastSortKey, ExchangeVersion.Exchange2012, PersonSchema.DisplayNameFirstLastSortKey, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009BA RID: 2490
		public static readonly PropertyInformation DisplayNameLastFirstSortKey = new PropertyInformation(PropertyUriEnum.PersonaDisplayNameLastFirstSortKey, ExchangeVersion.Exchange2012, PersonSchema.DisplayNameLastFirstSortKey, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009BB RID: 2491
		public static readonly PropertyInformation CompanyNameSortKey = new PropertyInformation(PropertyUriEnum.PersonaCompanyNameSortKey, ExchangeVersion.Exchange2012, PersonSchema.CompanyNameSortKey, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009BC RID: 2492
		public static readonly PropertyInformation HomeCitySortKey = new PropertyInformation(PropertyUriEnum.PersonaHomeCitySortKey, ExchangeVersion.Exchange2012, PersonSchema.HomeCitySortKey, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009BD RID: 2493
		public static readonly PropertyInformation WorkCitySortKey = new PropertyInformation(PropertyUriEnum.PersonaWorkCitySortKey, ExchangeVersion.Exchange2012, PersonSchema.WorkCitySortKey, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009BE RID: 2494
		public static readonly PropertyInformation DisplayNameFirstLastHeader = new PropertyInformation(PropertyUriEnum.PersonaDisplayNameFirstLastHeader, ExchangeVersion.Exchange2012, PersonSchema.DisplayNameFirstLastHeader, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009BF RID: 2495
		public static readonly PropertyInformation DisplayNameLastFirstHeader = new PropertyInformation(PropertyUriEnum.PersonaDisplayNameLastFirstHeader, ExchangeVersion.Exchange2012, PersonSchema.DisplayNameLastFirstHeader, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009C0 RID: 2496
		public static readonly PropertyInformation FileAs = new PropertyInformation(PropertyUriEnum.PersonaFileAs, ExchangeVersion.Exchange2012, PersonSchema.FileAs, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009C1 RID: 2497
		public static readonly PropertyInformation FileAsId = new PropertyInformation(PropertyUriEnum.PersonaFileAsId, ExchangeVersion.Exchange2012, PersonSchema.FileAsId, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009C2 RID: 2498
		public static readonly PropertyInformation DisplayNamePrefix = new PropertyInformation(PropertyUriEnum.PersonaDisplayNamePrefix, ExchangeVersion.Exchange2012, PersonSchema.DisplayNamePrefix, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009C3 RID: 2499
		public static readonly PropertyInformation GivenName = new PropertyInformation(PropertyUriEnum.PersonaGivenName, ExchangeVersion.Exchange2012, PersonSchema.GivenName, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009C4 RID: 2500
		public static readonly PropertyInformation MiddleName = new PropertyInformation(PropertyUriEnum.PersonaMiddleName, ExchangeVersion.Exchange2012, PersonSchema.MiddleName, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009C5 RID: 2501
		public static readonly PropertyInformation Surname = new PropertyInformation(PropertyUriEnum.PersonaSurname, ExchangeVersion.Exchange2012, PersonSchema.Surname, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009C6 RID: 2502
		public static readonly PropertyInformation Generation = new PropertyInformation(PropertyUriEnum.PersonaGeneration, ExchangeVersion.Exchange2012, PersonSchema.Generation, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009C7 RID: 2503
		public static readonly PropertyInformation Nickname = new PropertyInformation(PropertyUriEnum.PersonaNickname, ExchangeVersion.Exchange2012, PersonSchema.Nickname, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009C8 RID: 2504
		public static readonly PropertyInformation Alias = new PropertyInformation(PropertyUriEnum.PersonaAlias, ExchangeVersion.Exchange2012, PersonSchema.Alias, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009C9 RID: 2505
		public static readonly PropertyInformation YomiFirstName = new PropertyInformation(PropertyUriEnum.PersonaYomiFirstName, ExchangeVersion.Exchange2012, PersonSchema.YomiFirstName, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009CA RID: 2506
		public static readonly PropertyInformation YomiCompanyName = new PropertyInformation(PropertyUriEnum.PersonaYomiCompanyName, ExchangeVersion.Exchange2012, PersonSchema.YomiCompanyName, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009CB RID: 2507
		public static readonly PropertyInformation YomiLastName = new PropertyInformation(PropertyUriEnum.PersonaYomiLastName, ExchangeVersion.Exchange2012, PersonSchema.YomiLastName, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009CC RID: 2508
		public static readonly PropertyInformation Title = new PropertyInformation(PropertyUriEnum.PersonaTitle, ExchangeVersion.Exchange2012, PersonSchema.Title, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009CD RID: 2509
		public static readonly PropertyInformation Department = new PropertyInformation(PropertyUriEnum.PersonaDepartment, ExchangeVersion.Exchange2012, PersonSchema.Department, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009CE RID: 2510
		public static readonly PropertyInformation CompanyName = new PropertyInformation(PropertyUriEnum.PersonaCompanyName, ExchangeVersion.Exchange2012, PersonSchema.CompanyName, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009CF RID: 2511
		public static readonly PropertyInformation Location = new PropertyInformation(PropertyUriEnum.PersonaLocation, ExchangeVersion.Exchange2012, PersonSchema.Location, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009D0 RID: 2512
		public static readonly PropertyInformation EmailAddress = new PropertyInformation(PropertyUriEnum.PersonaEmailAddress, ExchangeVersion.Exchange2012, PersonSchema.EmailAddress, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009D1 RID: 2513
		public static readonly PropertyInformation EmailAddresses = new ArrayPropertyInformation("EmailAddresses", ExchangeVersion.Exchange2012, "EmailAddress", PersonSchema.EmailAddresses, new PropertyUri(PropertyUriEnum.EmailAddresses), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x040009D2 RID: 2514
		public static readonly PropertyInformation PhoneNumber = new PropertyInformation(PropertyUriEnum.PersonaPhoneNumber, ExchangeVersion.Exchange2012, PersonSchema.PhoneNumber, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009D3 RID: 2515
		public static readonly PropertyInformation ImAddress = new PropertyInformation(PropertyUriEnum.PersonaImAddress, ExchangeVersion.Exchange2012, PersonSchema.IMAddress, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009D4 RID: 2516
		public static readonly PropertyInformation HomeCity = new PropertyInformation(PropertyUriEnum.PersonaHomeCity, ExchangeVersion.Exchange2012, PersonSchema.HomeCity, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009D5 RID: 2517
		public static readonly PropertyInformation WorkCity = new PropertyInformation(PropertyUriEnum.PersonaWorkCity, ExchangeVersion.Exchange2012, PersonSchema.WorkCity, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009D6 RID: 2518
		public static readonly PropertyInformation RelevanceScore = new PropertyInformation(PropertyUriEnum.PersonaRelevanceScore, ExchangeVersion.Exchange2012, PersonSchema.RelevanceScore, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009D7 RID: 2519
		public static readonly PropertyInformation UnreadCount = new PropertyInformation(PropertyUriEnum.PersonaUnreadCount, ExchangeVersion.Exchange2012, null, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x040009D8 RID: 2520
		public static readonly PropertyInformation Bodies = new ArrayPropertyInformation("PersonaBodies", ExchangeVersion.Exchange2012, "BodyContentAttributedValue", PersonSchema.Bodies, new PropertyUri(PropertyUriEnum.PersonaBodies), new PropertyCommand.CreatePropertyCommand(PersonaBodiesProperty.CreateCommand));

		// Token: 0x040009D9 RID: 2521
		public static readonly PropertyInformation FolderIds = new ArrayPropertyInformation("FolderIds", ExchangeVersion.Exchange2012, "FolderId", PersonSchema.FolderIds, new PropertyUri(PropertyUriEnum.PersonaFolderIds), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x040009DA RID: 2522
		public static readonly PropertyInformation Attributions = new ArrayPropertyInformation("Attributions", ExchangeVersion.Exchange2012, "Attribution", PersonSchema.Attributions, new PropertyUri(PropertyUriEnum.PersonaAttributions), new PropertyCommand.CreatePropertyCommand(AttributionProperty.CreateCommand));

		// Token: 0x040009DB RID: 2523
		public static readonly PropertyInformation Members = new ArrayPropertyInformation("Members", ExchangeVersion.Exchange2012, "EmailAddressWrapper", PersonSchema.Members, new PropertyUri(PropertyUriEnum.PersonaMembers), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x040009DC RID: 2524
		public static readonly PropertyInformation DisplayNames = new ArrayPropertyInformation("DisplayNames", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.DisplayNames, new PropertyUri(PropertyUriEnum.PersonaDisplayNames), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x040009DD RID: 2525
		public static readonly PropertyInformation FileAses = new ArrayPropertyInformation("FileAses", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.FileAses, new PropertyUri(PropertyUriEnum.PersonaFileAses), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x040009DE RID: 2526
		public static readonly PropertyInformation FileAsIds = new ArrayPropertyInformation("FileAsIds", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.FileAsIds, new PropertyUri(PropertyUriEnum.PersonaFileAsIds), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x040009DF RID: 2527
		public static readonly PropertyInformation DisplayNamePrefixes = new ArrayPropertyInformation("DisplayNamePrefixes", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.DisplayNamePrefixes, new PropertyUri(PropertyUriEnum.PersonaDisplayNamePrefixes), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x040009E0 RID: 2528
		public static readonly PropertyInformation GivenNames = new ArrayPropertyInformation("GivenNames", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.GivenNames, new PropertyUri(PropertyUriEnum.PersonaGivenNames), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x040009E1 RID: 2529
		public static readonly PropertyInformation MiddleNames = new ArrayPropertyInformation("MiddleNames", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.MiddleNames, new PropertyUri(PropertyUriEnum.PersonaMiddleNames), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x040009E2 RID: 2530
		public static readonly PropertyInformation Surnames = new ArrayPropertyInformation("Surnames", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.Surnames, new PropertyUri(PropertyUriEnum.PersonaSurnames), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x040009E3 RID: 2531
		public static readonly PropertyInformation Generations = new ArrayPropertyInformation("Generations", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.Generations, new PropertyUri(PropertyUriEnum.PersonaGenerations), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x040009E4 RID: 2532
		public static readonly PropertyInformation Nicknames = new ArrayPropertyInformation("Nicknames", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.Nicknames, new PropertyUri(PropertyUriEnum.PersonaNicknames), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x040009E5 RID: 2533
		public static readonly PropertyInformation Initials = new ArrayPropertyInformation("Initials", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.Initials, new PropertyUri(PropertyUriEnum.PersonaInitials), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x040009E6 RID: 2534
		public static readonly PropertyInformation YomiCompanyNames = new ArrayPropertyInformation("YomiCompanyNames", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.YomiCompanyNames, new PropertyUri(PropertyUriEnum.PersonaYomiCompanyNames), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x040009E7 RID: 2535
		public static readonly PropertyInformation YomiFirstNames = new ArrayPropertyInformation("YomiFirstNames", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.YomiFirstNames, new PropertyUri(PropertyUriEnum.PersonaYomiFirstNames), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x040009E8 RID: 2536
		public static readonly PropertyInformation YomiLastNames = new ArrayPropertyInformation("YomiLastNames", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.YomiLastNames, new PropertyUri(PropertyUriEnum.PersonaYomiLastNames), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x040009E9 RID: 2537
		public static readonly PropertyInformation BusinessPhoneNumbers = new ArrayPropertyInformation("BusinessPhoneNumbers", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.BusinessPhoneNumbers, new PropertyUri(PropertyUriEnum.PersonaBusinessPhoneNumbers), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009EA RID: 2538
		public static readonly PropertyInformation BusinessPhoneNumbers2 = new ArrayPropertyInformation("BusinessPhoneNumbers2", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.BusinessPhoneNumbers2, new PropertyUri(PropertyUriEnum.PersonaBusinessPhoneNumbers2), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009EB RID: 2539
		public static readonly PropertyInformation HomePhones = new ArrayPropertyInformation("HomePhones", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.HomePhones, new PropertyUri(PropertyUriEnum.PersonaHomePhones), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009EC RID: 2540
		public static readonly PropertyInformation HomePhones2 = new ArrayPropertyInformation("HomePhones2", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.HomePhones2, new PropertyUri(PropertyUriEnum.PersonaHomePhones2), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009ED RID: 2541
		public static readonly PropertyInformation MobilePhones = new ArrayPropertyInformation("MobilePhones", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.MobilePhones, new PropertyUri(PropertyUriEnum.PersonaMobilePhones), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009EE RID: 2542
		public static readonly PropertyInformation MobilePhones2 = new ArrayPropertyInformation("MobilePhones2", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.MobilePhones2, new PropertyUri(PropertyUriEnum.PersonaMobilePhones2), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009EF RID: 2543
		public static readonly PropertyInformation AssistantPhoneNumbers = new ArrayPropertyInformation("AssistantPhoneNumbers", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.AssistantPhoneNumbers, new PropertyUri(PropertyUriEnum.PersonaAssistantPhoneNumbers), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009F0 RID: 2544
		public static readonly PropertyInformation CallbackPhones = new ArrayPropertyInformation("CallbackPhones", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.CallbackPhones, new PropertyUri(PropertyUriEnum.PersonaCallbackPhones), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009F1 RID: 2545
		public static readonly PropertyInformation CarPhones = new ArrayPropertyInformation("CarPhones", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.CarPhones, new PropertyUri(PropertyUriEnum.PersonaCarPhones), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009F2 RID: 2546
		public static readonly PropertyInformation HomeFaxes = new ArrayPropertyInformation("HomeFaxes", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.HomeFaxes, new PropertyUri(PropertyUriEnum.PersonaHomeFaxes), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009F3 RID: 2547
		public static readonly PropertyInformation OrganizationMainPhones = new ArrayPropertyInformation("OrganizationMainPhones", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.OrganizationMainPhones, new PropertyUri(PropertyUriEnum.PersonaOrganizationMainPhones), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009F4 RID: 2548
		public static readonly PropertyInformation OtherFaxes = new ArrayPropertyInformation("OtherFaxes", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.OtherFaxes, new PropertyUri(PropertyUriEnum.PersonaOtherFaxes), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009F5 RID: 2549
		public static readonly PropertyInformation OtherTelephones = new ArrayPropertyInformation("OtherTelephones", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.OtherTelephones, new PropertyUri(PropertyUriEnum.PersonaOtherTelephones), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009F6 RID: 2550
		public static readonly PropertyInformation OtherPhones2 = new ArrayPropertyInformation("OtherPhones2", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.OtherPhones2, new PropertyUri(PropertyUriEnum.PersonaOtherPhones2), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009F7 RID: 2551
		public static readonly PropertyInformation Pagers = new ArrayPropertyInformation("Pagers", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.Pagers, new PropertyUri(PropertyUriEnum.PersonaPagers), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009F8 RID: 2552
		public static readonly PropertyInformation RadioPhones = new ArrayPropertyInformation("RadioPhones", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.RadioPhones, new PropertyUri(PropertyUriEnum.PersonaRadioPhones), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009F9 RID: 2553
		public static readonly PropertyInformation TelexNumbers = new ArrayPropertyInformation("TelexNumbers", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.TelexNumbers, new PropertyUri(PropertyUriEnum.PersonaTelexNumbers), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009FA RID: 2554
		public static readonly PropertyInformation TTYTDDPhoneNumbers = new ArrayPropertyInformation("TTYTDDPhoneNumbers", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.TtyTddPhoneNumbers, new PropertyUri(PropertyUriEnum.PersonaTTYTDDPhoneNumbers), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009FB RID: 2555
		public static readonly PropertyInformation WorkFaxes = new ArrayPropertyInformation("WorkFaxes", ExchangeVersion.Exchange2012, "PhoneNumberAttributedValue", PersonSchema.WorkFaxes, new PropertyUri(PropertyUriEnum.PersonaWorkFaxes), new PropertyCommand.CreatePropertyCommand(PhoneNumberAttributedValueProperty.CreateCommand));

		// Token: 0x040009FC RID: 2556
		public static readonly PropertyInformation Emails1 = new ArrayPropertyInformation("Emails1", ExchangeVersion.Exchange2012, "EmailAddressAttributedValue", PersonSchema.Emails1, new PropertyUri(PropertyUriEnum.PersonaEmails1), new PropertyCommand.CreatePropertyCommand(EmailAddressAttributedValueProperty.CreateCommand));

		// Token: 0x040009FD RID: 2557
		public static readonly PropertyInformation Emails2 = new ArrayPropertyInformation("Emails2", ExchangeVersion.Exchange2012, "EmailAddressAttributedValue", PersonSchema.Emails2, new PropertyUri(PropertyUriEnum.PersonaEmails2), new PropertyCommand.CreatePropertyCommand(EmailAddressAttributedValueProperty.CreateCommand));

		// Token: 0x040009FE RID: 2558
		public static readonly PropertyInformation Emails3 = new ArrayPropertyInformation("Emails3", ExchangeVersion.Exchange2012, "EmailAddressAttributedValue", PersonSchema.Emails3, new PropertyUri(PropertyUriEnum.PersonaEmails3), new PropertyCommand.CreatePropertyCommand(EmailAddressAttributedValueProperty.CreateCommand));

		// Token: 0x040009FF RID: 2559
		public static readonly PropertyInformation BusinessHomePages = new ArrayPropertyInformation("BusinessHomePages", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.BusinessHomePages, new PropertyUri(PropertyUriEnum.PersonaBusinessHomePages), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A00 RID: 2560
		public static readonly PropertyInformation Schools = new ArrayPropertyInformation("Schools", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.Schools, new PropertyUri(PropertyUriEnum.PersonaSchools), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A01 RID: 2561
		public static readonly PropertyInformation PersonalHomePages = new ArrayPropertyInformation("PersonalHomePages", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.PersonalHomePages, new PropertyUri(PropertyUriEnum.PersonaPersonalHomePages), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A02 RID: 2562
		public static readonly PropertyInformation OfficeLocations = new ArrayPropertyInformation("OfficeLocations", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.OfficeLocations, new PropertyUri(PropertyUriEnum.PersonaOfficeLocations), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A03 RID: 2563
		public static readonly PropertyInformation ImAddresses = new ArrayPropertyInformation("ImAddresses", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.IMAddresses, new PropertyUri(PropertyUriEnum.PersonaImAddresses), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A04 RID: 2564
		public static readonly PropertyInformation ImAddresses2 = new ArrayPropertyInformation("ImAddresses2", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.IMAddresses2, new PropertyUri(PropertyUriEnum.PersonaImAddresses2), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A05 RID: 2565
		public static readonly PropertyInformation ImAddresses3 = new ArrayPropertyInformation("ImAddresses3", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.IMAddresses3, new PropertyUri(PropertyUriEnum.PersonaImAddresses3), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A06 RID: 2566
		public static readonly PropertyInformation BusinessAddresses = new ArrayPropertyInformation("BusinessAddresses", ExchangeVersion.Exchange2012, "PostalAddressAttributedValue", PersonSchema.BusinessAddresses, new PropertyUri(PropertyUriEnum.PersonaBusinessAddresses), new PropertyCommand.CreatePropertyCommand(PostalAddressAttributedValueProperty.CreateCommand));

		// Token: 0x04000A07 RID: 2567
		public static readonly PropertyInformation HomeAddresses = new ArrayPropertyInformation("HomeAddresses", ExchangeVersion.Exchange2012, "PostalAddressAttributedValue", PersonSchema.HomeAddresses, new PropertyUri(PropertyUriEnum.PersonaHomeAddresses), new PropertyCommand.CreatePropertyCommand(PostalAddressAttributedValueProperty.CreateCommand));

		// Token: 0x04000A08 RID: 2568
		public static readonly PropertyInformation OtherAddresses = new ArrayPropertyInformation("OtherAddresses", ExchangeVersion.Exchange2012, "PostalAddressAttributedValue", PersonSchema.OtherAddresses, new PropertyUri(PropertyUriEnum.PersonaOtherAddresses), new PropertyCommand.CreatePropertyCommand(PostalAddressAttributedValueProperty.CreateCommand));

		// Token: 0x04000A09 RID: 2569
		public static readonly PropertyInformation Titles = new ArrayPropertyInformation("Titles", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.Titles, new PropertyUri(PropertyUriEnum.PersonaTitles), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A0A RID: 2570
		public static readonly PropertyInformation Departments = new ArrayPropertyInformation("Departments", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.Departments, new PropertyUri(PropertyUriEnum.PersonaDepartments), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A0B RID: 2571
		public static readonly PropertyInformation CompanyNames = new ArrayPropertyInformation("CompanyNames", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.CompanyNames, new PropertyUri(PropertyUriEnum.PersonaCompanyNames), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A0C RID: 2572
		public static readonly PropertyInformation Managers = new ArrayPropertyInformation("Managers", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.Managers, new PropertyUri(PropertyUriEnum.PersonaManagers), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A0D RID: 2573
		public static readonly PropertyInformation AssistantNames = new ArrayPropertyInformation("AssistantNames", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.AssistantNames, new PropertyUri(PropertyUriEnum.PersonaAssistantNames), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A0E RID: 2574
		public static readonly PropertyInformation Professions = new ArrayPropertyInformation("Professions", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.Professions, new PropertyUri(PropertyUriEnum.PersonaProfessions), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A0F RID: 2575
		public static readonly PropertyInformation SpouseNames = new ArrayPropertyInformation("SpouseNames", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.SpouseNames, new PropertyUri(PropertyUriEnum.PersonaSpouseNames), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A10 RID: 2576
		public static readonly PropertyInformation Children = new ArrayPropertyInformation("Children", ExchangeVersion.Exchange2012, "StringArrayAttributedValue", PersonSchema.Children, new PropertyUri(PropertyUriEnum.PersonaChildren), new PropertyCommand.CreatePropertyCommand(StringArrayAttributedValueProperty.CreateCommand));

		// Token: 0x04000A11 RID: 2577
		public static readonly PropertyInformation Hobbies = new ArrayPropertyInformation("Hobbies", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.Hobbies, new PropertyUri(PropertyUriEnum.PersonaHobbies), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A12 RID: 2578
		public static readonly PropertyInformation WeddingAnniversaries = new ArrayPropertyInformation("WeddingAnniversaries", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.WeddingAnniversaries, new PropertyUri(PropertyUriEnum.PersonaWeddingAnniversaries), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A13 RID: 2579
		public static readonly PropertyInformation WeddingAnniversariesLocal = new ArrayPropertyInformation("WeddingAnniversariesLocal", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.WeddingAnniversariesLocal, new PropertyUri(PropertyUriEnum.PersonaWeddingAnniversariesLocal), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A14 RID: 2580
		public static readonly PropertyInformation Birthdays = new ArrayPropertyInformation("Birthdays", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.Birthdays, new PropertyUri(PropertyUriEnum.PersonaBirthdays), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A15 RID: 2581
		public static readonly PropertyInformation BirthdaysLocal = new ArrayPropertyInformation("BirthdaysLocal", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.BirthdaysLocal, new PropertyUri(PropertyUriEnum.PersonaBirthdaysLocal), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A16 RID: 2582
		public static readonly PropertyInformation Locations = new ArrayPropertyInformation("Locations", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.Locations, new PropertyUri(PropertyUriEnum.PersonaLocations), new PropertyCommand.CreatePropertyCommand(StringAttributedValueProperty.CreateCommand));

		// Token: 0x04000A17 RID: 2583
		public static readonly PropertyInformation ExtendedProperties = new ArrayPropertyInformation("ExtendedProperties", ExchangeVersion.Exchange2012, "ExtendedPropertyAttributedValue", PersonSchema.ExtendedProperties, new PropertyUri(PropertyUriEnum.PersonaExtendedProperties), new PropertyCommand.CreatePropertyCommand(ExtendedPropertyAttributedValueProperty.CreateCommand));

		// Token: 0x04000A18 RID: 2584
		public static readonly PropertyInformation ThirdPartyPhotoUrls = new ArrayPropertyInformation("ThirdPartyPhotoUrls", ExchangeVersion.Exchange2012, "StringAttributedValue", PersonSchema.ThirdPartyPhotoUrls, new PropertyUri(PropertyUriEnum.PersonaThirdPartyPhotoUrls), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));
	}
}
