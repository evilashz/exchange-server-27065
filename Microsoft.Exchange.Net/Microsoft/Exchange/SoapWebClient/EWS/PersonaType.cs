using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001C9 RID: 457
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PersonaType
	{
		// Token: 0x04000BD5 RID: 3029
		public ItemIdType PersonaId;

		// Token: 0x04000BD6 RID: 3030
		[XmlElement("PersonaType")]
		public string PersonaType1;

		// Token: 0x04000BD7 RID: 3031
		public string PersonaObjectStatus;

		// Token: 0x04000BD8 RID: 3032
		public DateTime CreationTime;

		// Token: 0x04000BD9 RID: 3033
		[XmlIgnore]
		public bool CreationTimeSpecified;

		// Token: 0x04000BDA RID: 3034
		[XmlArrayItem("BodyContentAttributedValue", IsNullable = false)]
		public BodyContentAttributedValueType[] Bodies;

		// Token: 0x04000BDB RID: 3035
		public string DisplayNameFirstLastSortKey;

		// Token: 0x04000BDC RID: 3036
		public string DisplayNameLastFirstSortKey;

		// Token: 0x04000BDD RID: 3037
		public string CompanyNameSortKey;

		// Token: 0x04000BDE RID: 3038
		public string HomeCitySortKey;

		// Token: 0x04000BDF RID: 3039
		public string WorkCitySortKey;

		// Token: 0x04000BE0 RID: 3040
		public string DisplayNameFirstLastHeader;

		// Token: 0x04000BE1 RID: 3041
		public string DisplayNameLastFirstHeader;

		// Token: 0x04000BE2 RID: 3042
		public string DisplayName;

		// Token: 0x04000BE3 RID: 3043
		public string DisplayNameFirstLast;

		// Token: 0x04000BE4 RID: 3044
		public string DisplayNameLastFirst;

		// Token: 0x04000BE5 RID: 3045
		public string FileAs;

		// Token: 0x04000BE6 RID: 3046
		public string FileAsId;

		// Token: 0x04000BE7 RID: 3047
		public string DisplayNamePrefix;

		// Token: 0x04000BE8 RID: 3048
		public string GivenName;

		// Token: 0x04000BE9 RID: 3049
		public string MiddleName;

		// Token: 0x04000BEA RID: 3050
		public string Surname;

		// Token: 0x04000BEB RID: 3051
		public string Generation;

		// Token: 0x04000BEC RID: 3052
		public string Nickname;

		// Token: 0x04000BED RID: 3053
		public string YomiCompanyName;

		// Token: 0x04000BEE RID: 3054
		public string YomiFirstName;

		// Token: 0x04000BEF RID: 3055
		public string YomiLastName;

		// Token: 0x04000BF0 RID: 3056
		public string Title;

		// Token: 0x04000BF1 RID: 3057
		public string Department;

		// Token: 0x04000BF2 RID: 3058
		public string CompanyName;

		// Token: 0x04000BF3 RID: 3059
		public string Location;

		// Token: 0x04000BF4 RID: 3060
		public EmailAddressType EmailAddress;

		// Token: 0x04000BF5 RID: 3061
		[XmlArrayItem("Address", IsNullable = false)]
		public EmailAddressType[] EmailAddresses;

		// Token: 0x04000BF6 RID: 3062
		public PersonaPhoneNumberType PhoneNumber;

		// Token: 0x04000BF7 RID: 3063
		public string ImAddress;

		// Token: 0x04000BF8 RID: 3064
		public string HomeCity;

		// Token: 0x04000BF9 RID: 3065
		public string WorkCity;

		// Token: 0x04000BFA RID: 3066
		public int RelevanceScore;

		// Token: 0x04000BFB RID: 3067
		[XmlIgnore]
		public bool RelevanceScoreSpecified;

		// Token: 0x04000BFC RID: 3068
		[XmlArrayItem("FolderId", IsNullable = false)]
		public FolderIdType[] FolderIds;

		// Token: 0x04000BFD RID: 3069
		[XmlArrayItem("Attribution", IsNullable = false)]
		public PersonaAttributionType[] Attributions;

		// Token: 0x04000BFE RID: 3070
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] DisplayNames;

		// Token: 0x04000BFF RID: 3071
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] FileAses;

		// Token: 0x04000C00 RID: 3072
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] FileAsIds;

		// Token: 0x04000C01 RID: 3073
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] DisplayNamePrefixes;

		// Token: 0x04000C02 RID: 3074
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] GivenNames;

		// Token: 0x04000C03 RID: 3075
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] MiddleNames;

		// Token: 0x04000C04 RID: 3076
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Surnames;

		// Token: 0x04000C05 RID: 3077
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Generations;

		// Token: 0x04000C06 RID: 3078
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Nicknames;

		// Token: 0x04000C07 RID: 3079
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Initials;

		// Token: 0x04000C08 RID: 3080
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] YomiCompanyNames;

		// Token: 0x04000C09 RID: 3081
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] YomiFirstNames;

		// Token: 0x04000C0A RID: 3082
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] YomiLastNames;

		// Token: 0x04000C0B RID: 3083
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] BusinessPhoneNumbers;

		// Token: 0x04000C0C RID: 3084
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] BusinessPhoneNumbers2;

		// Token: 0x04000C0D RID: 3085
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] HomePhones;

		// Token: 0x04000C0E RID: 3086
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] HomePhones2;

		// Token: 0x04000C0F RID: 3087
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] MobilePhones;

		// Token: 0x04000C10 RID: 3088
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] MobilePhones2;

		// Token: 0x04000C11 RID: 3089
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] AssistantPhoneNumbers;

		// Token: 0x04000C12 RID: 3090
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] CallbackPhones;

		// Token: 0x04000C13 RID: 3091
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] CarPhones;

		// Token: 0x04000C14 RID: 3092
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] HomeFaxes;

		// Token: 0x04000C15 RID: 3093
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] OrganizationMainPhones;

		// Token: 0x04000C16 RID: 3094
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] OtherFaxes;

		// Token: 0x04000C17 RID: 3095
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] OtherTelephones;

		// Token: 0x04000C18 RID: 3096
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] OtherPhones2;

		// Token: 0x04000C19 RID: 3097
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] Pagers;

		// Token: 0x04000C1A RID: 3098
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] RadioPhones;

		// Token: 0x04000C1B RID: 3099
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] TelexNumbers;

		// Token: 0x04000C1C RID: 3100
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] TTYTDDPhoneNumbers;

		// Token: 0x04000C1D RID: 3101
		[XmlArrayItem("PhoneNumberAttributedValue", IsNullable = false)]
		public PhoneNumberAttributedValueType[] WorkFaxes;

		// Token: 0x04000C1E RID: 3102
		[XmlArrayItem("EmailAddressAttributedValue", IsNullable = false)]
		public EmailAddressAttributedValueType[] Emails1;

		// Token: 0x04000C1F RID: 3103
		[XmlArrayItem("EmailAddressAttributedValue", IsNullable = false)]
		public EmailAddressAttributedValueType[] Emails2;

		// Token: 0x04000C20 RID: 3104
		[XmlArrayItem("EmailAddressAttributedValue", IsNullable = false)]
		public EmailAddressAttributedValueType[] Emails3;

		// Token: 0x04000C21 RID: 3105
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] BusinessHomePages;

		// Token: 0x04000C22 RID: 3106
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] PersonalHomePages;

		// Token: 0x04000C23 RID: 3107
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] OfficeLocations;

		// Token: 0x04000C24 RID: 3108
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] ImAddresses;

		// Token: 0x04000C25 RID: 3109
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] ImAddresses2;

		// Token: 0x04000C26 RID: 3110
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] ImAddresses3;

		// Token: 0x04000C27 RID: 3111
		[XmlArrayItem("PostalAddressAttributedValue", IsNullable = false)]
		public PostalAddressAttributedValueType[] BusinessAddresses;

		// Token: 0x04000C28 RID: 3112
		[XmlArrayItem("PostalAddressAttributedValue", IsNullable = false)]
		public PostalAddressAttributedValueType[] HomeAddresses;

		// Token: 0x04000C29 RID: 3113
		[XmlArrayItem("PostalAddressAttributedValue", IsNullable = false)]
		public PostalAddressAttributedValueType[] OtherAddresses;

		// Token: 0x04000C2A RID: 3114
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Titles;

		// Token: 0x04000C2B RID: 3115
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Departments;

		// Token: 0x04000C2C RID: 3116
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] CompanyNames;

		// Token: 0x04000C2D RID: 3117
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Managers;

		// Token: 0x04000C2E RID: 3118
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] AssistantNames;

		// Token: 0x04000C2F RID: 3119
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Professions;

		// Token: 0x04000C30 RID: 3120
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] SpouseNames;

		// Token: 0x04000C31 RID: 3121
		[XmlArrayItem("StringArrayAttributedValue", IsNullable = false)]
		public StringArrayAttributedValueType[] Children;

		// Token: 0x04000C32 RID: 3122
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Schools;

		// Token: 0x04000C33 RID: 3123
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Hobbies;

		// Token: 0x04000C34 RID: 3124
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] WeddingAnniversaries;

		// Token: 0x04000C35 RID: 3125
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Birthdays;

		// Token: 0x04000C36 RID: 3126
		[XmlArrayItem("StringAttributedValue", IsNullable = false)]
		public StringAttributedValueType[] Locations;

		// Token: 0x04000C37 RID: 3127
		[XmlArrayItem("ExtendedPropertyAttributedValue", IsNullable = false)]
		public ExtendedPropertyAttributedValueType[] ExtendedProperties;
	}
}
