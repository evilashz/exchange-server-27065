using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C9F RID: 3231
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PersonSchema : Schema
	{
		// Token: 0x17001E3C RID: 7740
		// (get) Token: 0x060070C2 RID: 28866 RVA: 0x001F34AE File Offset: 0x001F16AE
		public new static PersonSchema Instance
		{
			get
			{
				return PersonSchema.instance;
			}
		}

		// Token: 0x04004DF9 RID: 19961
		public static readonly StorePropertyDefinition Id = new ApplicationAggregatedProperty("Id", typeof(PersonId), PropertyFlags.None, PersonPropertyAggregationStrategy.PersonIdProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004DFA RID: 19962
		public static readonly ApplicationAggregatedProperty CreationTime = new ApplicationAggregatedProperty("CreationTime", typeof(ExDateTime), PropertyFlags.None, PersonPropertyAggregationStrategy.CreationTimeProperty, SortByAndFilterStrategy.CreateSimpleSort(InternalSchema.InternalPersonCreationTime));

		// Token: 0x04004DFB RID: 19963
		public static readonly ApplicationAggregatedProperty IsFavorite = new ApplicationAggregatedProperty("IsFavorite", typeof(bool), PropertyFlags.None, PersonPropertyAggregationStrategy.IsFavoriteProperty, SortByAndFilterStrategy.CreateSimpleSort(InternalSchema.IsFavorite));

		// Token: 0x04004DFC RID: 19964
		public static readonly ApplicationAggregatedProperty DisplayName = new ApplicationAggregatedProperty("DisplayName", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreateNameProperty(InternalSchema.DisplayName), SortByAndFilterStrategy.CreateSimpleSort(InternalSchema.InternalPersonDisplayName));

		// Token: 0x04004DFD RID: 19965
		public static readonly ApplicationAggregatedProperty FileAs = new ApplicationAggregatedProperty("FileAs", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreateNameProperty(InternalSchema.FileAsString), SortByAndFilterStrategy.CreateSimpleSort(InternalSchema.InternalPersonFileAs));

		// Token: 0x04004DFE RID: 19966
		public static readonly ApplicationAggregatedProperty FileAsId = new ApplicationAggregatedProperty("FileAsId", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.FileAsIdProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004DFF RID: 19967
		public static readonly ApplicationAggregatedProperty Nickname = new ApplicationAggregatedProperty("Nickname", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreateNameProperty(InternalSchema.Nickname), SortByAndFilterStrategy.None);

		// Token: 0x04004E00 RID: 19968
		public static readonly ApplicationAggregatedProperty DisplayNamePrefix = new ApplicationAggregatedProperty("DisplayNamePrefix", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreateNameProperty(InternalSchema.DisplayNamePrefix), SortByAndFilterStrategy.None);

		// Token: 0x04004E01 RID: 19969
		public static readonly ApplicationAggregatedProperty GivenName = new ApplicationAggregatedProperty("GivenName", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreateNameProperty(InternalSchema.GivenName), SortByAndFilterStrategy.CreateSimpleSort(InternalSchema.InternalPersonGivenName));

		// Token: 0x04004E02 RID: 19970
		public static readonly ApplicationAggregatedProperty MiddleName = new ApplicationAggregatedProperty("MiddleName", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreateNameProperty(InternalSchema.MiddleName), SortByAndFilterStrategy.None);

		// Token: 0x04004E03 RID: 19971
		public static readonly ApplicationAggregatedProperty Surname = new ApplicationAggregatedProperty("Surname", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreateNameProperty(InternalSchema.Surname), SortByAndFilterStrategy.CreateSimpleSort(InternalSchema.InternalPersonSurname));

		// Token: 0x04004E04 RID: 19972
		public static readonly ApplicationAggregatedProperty Generation = new ApplicationAggregatedProperty("Generation", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreateNameProperty(InternalSchema.Generation), SortByAndFilterStrategy.None);

		// Token: 0x04004E05 RID: 19973
		public static readonly ApplicationAggregatedProperty YomiFirstName = new ApplicationAggregatedProperty("YomiFirstName", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreateNameProperty(InternalSchema.YomiFirstName), SortByAndFilterStrategy.None);

		// Token: 0x04004E06 RID: 19974
		public static readonly ApplicationAggregatedProperty YomiLastName = new ApplicationAggregatedProperty("YomiLastName", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreateNameProperty(InternalSchema.YomiLastName), SortByAndFilterStrategy.None);

		// Token: 0x04004E07 RID: 19975
		public static readonly ApplicationAggregatedProperty YomiCompanyName = new ApplicationAggregatedProperty("YomiCompanyName", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreateNameProperty(InternalSchema.YomiCompany), SortByAndFilterStrategy.None);

		// Token: 0x04004E08 RID: 19976
		public static readonly ApplicationAggregatedProperty Title = new ApplicationAggregatedProperty("Title", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreatePriorityPropertyAggregation(InternalSchema.Title), SortByAndFilterStrategy.None);

		// Token: 0x04004E09 RID: 19977
		public static readonly ApplicationAggregatedProperty Department = new ApplicationAggregatedProperty("Department", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreatePriorityPropertyAggregation(InternalSchema.Department), SortByAndFilterStrategy.None);

		// Token: 0x04004E0A RID: 19978
		public static readonly ApplicationAggregatedProperty CompanyName = new ApplicationAggregatedProperty("CompanyName", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreatePriorityPropertyAggregation(InternalSchema.CompanyName), SortByAndFilterStrategy.CreateSimpleSort(InternalSchema.InternalPersonCompanyName));

		// Token: 0x04004E0B RID: 19979
		public static readonly ApplicationAggregatedProperty Alias = new ApplicationAggregatedProperty("Alias", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreatePriorityPropertyAggregation(InternalSchema.Account), SortByAndFilterStrategy.None);

		// Token: 0x04004E0C RID: 19980
		public static readonly ApplicationAggregatedProperty Location = new ApplicationAggregatedProperty("Location", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreatePriorityPropertyAggregation(InternalSchema.Location), SortByAndFilterStrategy.None);

		// Token: 0x04004E0D RID: 19981
		public static readonly ApplicationAggregatedProperty IMAddress = new ApplicationAggregatedProperty("IMAddress", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.IMAddressProperty, SortByAndFilterStrategy.SimpleCanQuery);

		// Token: 0x04004E0E RID: 19982
		public static readonly ApplicationAggregatedProperty HomeCity = new ApplicationAggregatedProperty("HomeCity", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreatePriorityPropertyAggregation(InternalSchema.HomeCity), SortByAndFilterStrategy.CreateSimpleSort(InternalSchema.InternalPersonHomeCity));

		// Token: 0x04004E0F RID: 19983
		public static readonly ApplicationAggregatedProperty EmailAddresses = new ApplicationAggregatedProperty("EmailAddresses", typeof(Participant[]), PropertyFlags.Multivalued, PersonPropertyAggregationStrategy.EmailAddressesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E10 RID: 19984
		public static readonly ApplicationAggregatedProperty PostalAddresses = new ApplicationAggregatedProperty("PostalAddresses", typeof(PostalAddress[]), PropertyFlags.Multivalued, PersonPropertyAggregationStrategy.PostalAddressesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E11 RID: 19985
		public static readonly ApplicationAggregatedProperty PostalAddressesWithDetails = new ApplicationAggregatedProperty("PostalAddressesWithDetails", typeof(PostalAddress[]), PropertyFlags.Multivalued, PersonPropertyAggregationStrategy.PostalAddressesWithDetailsProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E12 RID: 19986
		public static readonly PropertyDefinition ContactItemIds = new ApplicationAggregatedProperty("ContactItemIds", typeof(StoreObjectId[]), PropertyFlags.Multivalued, PropertyAggregationStrategy.EntryIdsProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E13 RID: 19987
		public static readonly PropertyDefinition RelevanceScore = new ApplicationAggregatedProperty("RelevanceScore", typeof(int), PropertyFlags.None, PersonPropertyAggregationStrategy.RelevanceScoreProperty, SortByAndFilterStrategy.CreateSimpleSort(InternalSchema.InternalPersonRelevanceScore));

		// Token: 0x04004E14 RID: 19988
		public static readonly PropertyDefinition GALLinkID = new ApplicationAggregatedProperty("GALLinkID", typeof(Guid), PropertyFlags.None, PersonPropertyAggregationStrategy.GALLinkIDProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E15 RID: 19989
		public static readonly PropertyDefinition PhotoContactEntryId = new ApplicationAggregatedProperty("PhotoContactEntryId", typeof(StoreObjectId), PropertyFlags.None, PersonPropertyAggregationStrategy.PhotoContactEntryIdProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E16 RID: 19990
		public static readonly ApplicationAggregatedProperty ThirdPartyPhotoUrls = new ApplicationAggregatedProperty("ThirdPartyPhotoUrls", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedThirdPartyPhotoUrlsProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E17 RID: 19991
		public static readonly ApplicationAggregatedProperty WorkCity = new ApplicationAggregatedProperty("WorkCity", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreatePriorityPropertyAggregation(InternalSchema.WorkAddressCity), SortByAndFilterStrategy.CreateSimpleSort(InternalSchema.InternalPersonWorkCity));

		// Token: 0x04004E18 RID: 19992
		public static readonly ApplicationAggregatedProperty DisplayNameFirstLast = new ApplicationAggregatedProperty("DisplayNameFirstLast", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreateNameProperty(InternalSchema.DisplayNameFirstLast), SortByAndFilterStrategy.CreateSimpleSort(InternalSchema.InternalPersonDisplayNameFirstLast));

		// Token: 0x04004E19 RID: 19993
		public static readonly ApplicationAggregatedProperty DisplayNameLastFirst = new ApplicationAggregatedProperty("DisplayNameLastFirst", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.CreateNameProperty(InternalSchema.DisplayNameLastFirst), SortByAndFilterStrategy.CreateSimpleSort(InternalSchema.InternalPersonDisplayNameLastFirst));

		// Token: 0x04004E1A RID: 19994
		public static readonly ApplicationAggregatedProperty EmailAddress = new ApplicationAggregatedProperty("EmailAddress", typeof(Participant), PropertyFlags.None, PersonPropertyAggregationStrategy.EmailAddressProperty, SortByAndFilterStrategy.SimpleCanQuery);

		// Token: 0x04004E1B RID: 19995
		public static readonly ApplicationAggregatedProperty PostalAddress = new ApplicationAggregatedProperty("PostalAddress", typeof(string), PropertyFlags.None, PersonPropertyAggregationStrategy.PostalAddressProperty, SortByAndFilterStrategy.SimpleCanQuery);

		// Token: 0x04004E1C RID: 19996
		public static readonly ApplicationAggregatedProperty PersonType = new ApplicationAggregatedProperty("PersonType", typeof(PersonType), PropertyFlags.None, PersonPropertyAggregationStrategy.PersonTypeProperty, SortByAndFilterStrategy.PersonType);

		// Token: 0x04004E1D RID: 19997
		public static readonly ApplicationAggregatedProperty Bodies = new ApplicationAggregatedProperty("Body", typeof(IEnumerable<AttributedValue<PersonNotes>>), PropertyFlags.None, PropertyAggregationStrategy.None, SortByAndFilterStrategy.None);

		// Token: 0x04004E1E RID: 19998
		public static readonly ApplicationAggregatedProperty FolderIds = new ApplicationAggregatedProperty("FolderIds", typeof(IEnumerable<StoreObjectId>), PropertyFlags.None, PersonPropertyAggregationStrategy.FolderIdsProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E1F RID: 19999
		public static readonly ApplicationAggregatedProperty DisplayNameFirstLastSortKey = new ApplicationAggregatedProperty("DisplayNameFirstLastSortKey", typeof(string), PropertyFlags.None, PropertyAggregationStrategy.None, SortByAndFilterStrategy.None);

		// Token: 0x04004E20 RID: 20000
		public static readonly ApplicationAggregatedProperty DisplayNameLastFirstSortKey = new ApplicationAggregatedProperty("DisplayNameLastFirstSortKey", typeof(string), PropertyFlags.None, PropertyAggregationStrategy.None, SortByAndFilterStrategy.None);

		// Token: 0x04004E21 RID: 20001
		public static readonly ApplicationAggregatedProperty CompanyNameSortKey = new ApplicationAggregatedProperty("CompanyNameSortKey", typeof(string), PropertyFlags.None, PropertyAggregationStrategy.None, SortByAndFilterStrategy.None);

		// Token: 0x04004E22 RID: 20002
		public static readonly ApplicationAggregatedProperty HomeCitySortKey = new ApplicationAggregatedProperty("HomeCitySortKey", typeof(string), PropertyFlags.None, PropertyAggregationStrategy.None, SortByAndFilterStrategy.None);

		// Token: 0x04004E23 RID: 20003
		public static readonly ApplicationAggregatedProperty WorkCitySortKey = new ApplicationAggregatedProperty("WorkCitySortKey", typeof(string), PropertyFlags.None, PropertyAggregationStrategy.None, SortByAndFilterStrategy.None);

		// Token: 0x04004E24 RID: 20004
		public static readonly ApplicationAggregatedProperty DisplayNameFirstLastHeader = new ApplicationAggregatedProperty("DisplayNameFirstLastHeader", typeof(string), PropertyFlags.None, PropertyAggregationStrategy.None, SortByAndFilterStrategy.None);

		// Token: 0x04004E25 RID: 20005
		public static readonly ApplicationAggregatedProperty DisplayNameLastFirstHeader = new ApplicationAggregatedProperty("DisplayNameLastFirstHeader", typeof(string), PropertyFlags.None, PropertyAggregationStrategy.None, SortByAndFilterStrategy.None);

		// Token: 0x04004E26 RID: 20006
		public static readonly ApplicationAggregatedProperty Attributions = new ApplicationAggregatedProperty("Attributions", typeof(IEnumerable<Attribution>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributionsProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E27 RID: 20007
		public static readonly ApplicationAggregatedProperty Members = new ApplicationAggregatedProperty("Members", typeof(Participant[]), PropertyFlags.None, PropertyAggregationStrategy.None, SortByAndFilterStrategy.None);

		// Token: 0x04004E28 RID: 20008
		public static readonly ApplicationAggregatedProperty PhoneNumber = new ApplicationAggregatedProperty("PhoneNumber", typeof(PhoneNumber), PropertyFlags.None, PropertyAggregationStrategy.None, SortByAndFilterStrategy.None);

		// Token: 0x04004E29 RID: 20009
		public static readonly ApplicationAggregatedProperty DisplayNames = new ApplicationAggregatedProperty("DisplayNames", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedDisplayNamesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E2A RID: 20010
		public static readonly ApplicationAggregatedProperty FileAses = new ApplicationAggregatedProperty("FileAses", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedFileAsesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E2B RID: 20011
		public static readonly ApplicationAggregatedProperty FileAsIds = new ApplicationAggregatedProperty("FileAsIds", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedFileAsIdsProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E2C RID: 20012
		public static readonly ApplicationAggregatedProperty DisplayNamePrefixes = new ApplicationAggregatedProperty("DisplayNamePrefixes", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedDisplayNamePrefixesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E2D RID: 20013
		public static readonly ApplicationAggregatedProperty GivenNames = new ApplicationAggregatedProperty("GivenNames", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedGivenNamesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E2E RID: 20014
		public static readonly ApplicationAggregatedProperty MiddleNames = new ApplicationAggregatedProperty("MiddleNames", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedMiddleNamesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E2F RID: 20015
		public static readonly ApplicationAggregatedProperty Surnames = new ApplicationAggregatedProperty("Surnames", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedSurnamesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E30 RID: 20016
		public static readonly ApplicationAggregatedProperty Generations = new ApplicationAggregatedProperty("Generations", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedGenerationsProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E31 RID: 20017
		public static readonly ApplicationAggregatedProperty Nicknames = new ApplicationAggregatedProperty("Nicknames", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedNicknamesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E32 RID: 20018
		public static readonly ApplicationAggregatedProperty Initials = new ApplicationAggregatedProperty("Initials", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedInitialsProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E33 RID: 20019
		public static readonly ApplicationAggregatedProperty YomiCompanyNames = new ApplicationAggregatedProperty("YomiCompanyNames", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedYomiCompanyNamesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E34 RID: 20020
		public static readonly ApplicationAggregatedProperty YomiFirstNames = new ApplicationAggregatedProperty("YomiFirstNames", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedYomiFirstNamesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E35 RID: 20021
		public static readonly ApplicationAggregatedProperty YomiLastNames = new ApplicationAggregatedProperty("YomiLastNames", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedYomiLastNamesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E36 RID: 20022
		public static readonly ApplicationAggregatedProperty BusinessPhoneNumbers = new ApplicationAggregatedProperty("BusinessPhoneNumbers", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedBusinessPhoneNumbersProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E37 RID: 20023
		public static readonly ApplicationAggregatedProperty BusinessPhoneNumbers2 = new ApplicationAggregatedProperty("BusinessPhoneNumbers2", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedBusinessPhoneNumbers2Property, SortByAndFilterStrategy.None);

		// Token: 0x04004E38 RID: 20024
		public static readonly ApplicationAggregatedProperty HomePhones = new ApplicationAggregatedProperty("HomePhones", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedHomePhonesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E39 RID: 20025
		public static readonly ApplicationAggregatedProperty HomePhones2 = new ApplicationAggregatedProperty("HomePhones2", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedHomePhones2Property, SortByAndFilterStrategy.None);

		// Token: 0x04004E3A RID: 20026
		public static readonly ApplicationAggregatedProperty MobilePhones = new ApplicationAggregatedProperty("MobilePhones", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedMobilePhonesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E3B RID: 20027
		public static readonly ApplicationAggregatedProperty MobilePhones2 = new ApplicationAggregatedProperty("MobilePhones2", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedMobilePhones2Property, SortByAndFilterStrategy.None);

		// Token: 0x04004E3C RID: 20028
		public static readonly ApplicationAggregatedProperty AssistantPhoneNumbers = new ApplicationAggregatedProperty("AssistantPhoneNumbers", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedAssistantPhoneNumbersProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E3D RID: 20029
		public static readonly ApplicationAggregatedProperty CallbackPhones = new ApplicationAggregatedProperty("CallbackPhones", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedCallbackPhonesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E3E RID: 20030
		public static readonly ApplicationAggregatedProperty CarPhones = new ApplicationAggregatedProperty("CarPhones", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedCarPhonesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E3F RID: 20031
		public static readonly ApplicationAggregatedProperty HomeFaxes = new ApplicationAggregatedProperty("HomeFaxes", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedHomeFaxesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E40 RID: 20032
		public static readonly ApplicationAggregatedProperty OrganizationMainPhones = new ApplicationAggregatedProperty("OrganizationMainPhones", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedOrganizationMainPhonesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E41 RID: 20033
		public static readonly ApplicationAggregatedProperty OtherFaxes = new ApplicationAggregatedProperty("OtherFaxes", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedOtherFaxesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E42 RID: 20034
		public static readonly ApplicationAggregatedProperty OtherTelephones = new ApplicationAggregatedProperty("OtherTelephones", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedOtherTelephonesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E43 RID: 20035
		public static readonly ApplicationAggregatedProperty OtherPhones2 = new ApplicationAggregatedProperty("OtherPhones2", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedOtherPhones2Property, SortByAndFilterStrategy.None);

		// Token: 0x04004E44 RID: 20036
		public static readonly ApplicationAggregatedProperty Pagers = new ApplicationAggregatedProperty("Pagers", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedPagersProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E45 RID: 20037
		public static readonly ApplicationAggregatedProperty RadioPhones = new ApplicationAggregatedProperty("RadioPhones", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedRadioPhonesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E46 RID: 20038
		public static readonly ApplicationAggregatedProperty TelexNumbers = new ApplicationAggregatedProperty("TelexNumbers", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedTelexNumbersProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E47 RID: 20039
		public static readonly ApplicationAggregatedProperty TtyTddPhoneNumbers = new ApplicationAggregatedProperty("TtyTddPhoneNumbers", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedTtyTddPhoneNumbersProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E48 RID: 20040
		public static readonly ApplicationAggregatedProperty WorkFaxes = new ApplicationAggregatedProperty("WorkFaxes", typeof(IEnumerable<AttributedValue<PhoneNumber>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedWorkFaxesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E49 RID: 20041
		public static readonly ApplicationAggregatedProperty Emails1 = new ApplicationAggregatedProperty("Emails1", typeof(IEnumerable<AttributedValue<Participant>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedEmails1Property, SortByAndFilterStrategy.None);

		// Token: 0x04004E4A RID: 20042
		public static readonly ApplicationAggregatedProperty Emails2 = new ApplicationAggregatedProperty("Emails2", typeof(IEnumerable<AttributedValue<Participant>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedEmails2Property, SortByAndFilterStrategy.None);

		// Token: 0x04004E4B RID: 20043
		public static readonly ApplicationAggregatedProperty Emails3 = new ApplicationAggregatedProperty("Emails3", typeof(IEnumerable<AttributedValue<Participant>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedEmails3Property, SortByAndFilterStrategy.None);

		// Token: 0x04004E4C RID: 20044
		public static readonly ApplicationAggregatedProperty BusinessHomePages = new ApplicationAggregatedProperty("BusinessHomePages", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedBusinessHomePagesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E4D RID: 20045
		public static readonly ApplicationAggregatedProperty Schools = new ApplicationAggregatedProperty("Schools", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedSchoolsProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E4E RID: 20046
		public static readonly ApplicationAggregatedProperty PersonalHomePages = new ApplicationAggregatedProperty("PersonalHomePages", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedPersonalHomePagesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E4F RID: 20047
		public static readonly ApplicationAggregatedProperty OfficeLocations = new ApplicationAggregatedProperty("OfficeLocations", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedOfficeLocationsProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E50 RID: 20048
		public static readonly ApplicationAggregatedProperty IMAddresses = new ApplicationAggregatedProperty("IMAddresses", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedIMAddressesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E51 RID: 20049
		public static readonly ApplicationAggregatedProperty IMAddresses2 = new ApplicationAggregatedProperty("IMAddresses2", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedIMAddresses2Property, SortByAndFilterStrategy.None);

		// Token: 0x04004E52 RID: 20050
		public static readonly ApplicationAggregatedProperty IMAddresses3 = new ApplicationAggregatedProperty("IMAddresses3", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedIMAddresses3Property, SortByAndFilterStrategy.None);

		// Token: 0x04004E53 RID: 20051
		public static readonly ApplicationAggregatedProperty Titles = new ApplicationAggregatedProperty("Titles", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedTitlesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E54 RID: 20052
		public static readonly ApplicationAggregatedProperty Departments = new ApplicationAggregatedProperty("Departments", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedDepartmentsProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E55 RID: 20053
		public static readonly ApplicationAggregatedProperty CompanyNames = new ApplicationAggregatedProperty("CompanyNames", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedCompanyNamesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E56 RID: 20054
		public static readonly ApplicationAggregatedProperty Managers = new ApplicationAggregatedProperty("Managers", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedManagersProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E57 RID: 20055
		public static readonly ApplicationAggregatedProperty AssistantNames = new ApplicationAggregatedProperty("AssistantNames", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedAssistantNamesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E58 RID: 20056
		public static readonly ApplicationAggregatedProperty Professions = new ApplicationAggregatedProperty("Professions", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedProfessionsProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E59 RID: 20057
		public static readonly ApplicationAggregatedProperty SpouseNames = new ApplicationAggregatedProperty("SpouseNames", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedSpouseNamesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E5A RID: 20058
		public static readonly ApplicationAggregatedProperty Children = new ApplicationAggregatedProperty("Children", typeof(IEnumerable<AttributedValue<string[]>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedChildrenProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E5B RID: 20059
		public static readonly ApplicationAggregatedProperty Hobbies = new ApplicationAggregatedProperty("Hobbies", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedHobbiesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E5C RID: 20060
		public static readonly ApplicationAggregatedProperty WeddingAnniversaries = new ApplicationAggregatedProperty("WeddingAnniversaries", typeof(IEnumerable<AttributedValue<ExDateTime>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedWeddingAnniversariesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E5D RID: 20061
		public static readonly ApplicationAggregatedProperty Birthdays = new ApplicationAggregatedProperty("Birthdays", typeof(IEnumerable<AttributedValue<ExDateTime>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedBirthdaysProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E5E RID: 20062
		public static readonly ApplicationAggregatedProperty WeddingAnniversariesLocal = new ApplicationAggregatedProperty("WeddingAnniversariesLocal", typeof(IEnumerable<AttributedValue<ExDateTime>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedWeddingAnniversariesLocalProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E5F RID: 20063
		public static readonly ApplicationAggregatedProperty BirthdaysLocal = new ApplicationAggregatedProperty("BirthdaysLocal", typeof(IEnumerable<AttributedValue<ExDateTime>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedBirthdaysLocalProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E60 RID: 20064
		public static readonly ApplicationAggregatedProperty Locations = new ApplicationAggregatedProperty("Locations", typeof(IEnumerable<AttributedValue<string>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedLocationsProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E61 RID: 20065
		public static readonly ApplicationAggregatedProperty ExtendedProperties = new ApplicationAggregatedProperty("ExtendedProperties", typeof(IEnumerable<AttributedValue<ContactExtendedPropertyData>>), PropertyFlags.None, PropertyAggregationStrategy.None, SortByAndFilterStrategy.None);

		// Token: 0x04004E62 RID: 20066
		public static readonly ApplicationAggregatedProperty HomeAddresses = new ApplicationAggregatedProperty("HomeAddresses", typeof(IEnumerable<AttributedValue<PostalAddress>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedHomeAddressesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E63 RID: 20067
		public static readonly ApplicationAggregatedProperty BusinessAddresses = new ApplicationAggregatedProperty("BusinessAddresses", typeof(IEnumerable<AttributedValue<PostalAddress>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedWorkAddressesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E64 RID: 20068
		public static readonly ApplicationAggregatedProperty OtherAddresses = new ApplicationAggregatedProperty("OtherAddresses", typeof(IEnumerable<AttributedValue<PostalAddress>>), PropertyFlags.None, PersonPropertyAggregationStrategy.AttributedOtherAddressesProperty, SortByAndFilterStrategy.None);

		// Token: 0x04004E65 RID: 20069
		private static readonly PersonSchema instance = new PersonSchema();
	}
}
