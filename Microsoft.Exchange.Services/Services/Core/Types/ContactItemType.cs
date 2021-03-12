using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005AF RID: 1455
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Contact")]
	[Serializable]
	public class ContactItemType : ItemType
	{
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06002B08 RID: 11016 RVA: 0x000AEDDE File Offset: 0x000ACFDE
		// (set) Token: 0x06002B09 RID: 11017 RVA: 0x000AEDF0 File Offset: 0x000ACFF0
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string FileAs
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.FileAs);
			}
			set
			{
				base.PropertyBag[ContactSchema.FileAs] = value;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06002B0A RID: 11018 RVA: 0x000AEE03 File Offset: 0x000AD003
		// (set) Token: 0x06002B0B RID: 11019 RVA: 0x000AEE15 File Offset: 0x000AD015
		[DataMember(Name = "FileAsMapping", EmitDefaultValue = false, Order = 2)]
		[XmlElement("FileAsMapping")]
		public string FileAsMapping
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.FileAsMapping);
			}
			set
			{
				base.PropertyBag[ContactSchema.FileAsMapping] = value;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002B0C RID: 11020 RVA: 0x000AEE28 File Offset: 0x000AD028
		// (set) Token: 0x06002B0D RID: 11021 RVA: 0x000AEE3A File Offset: 0x000AD03A
		[IgnoreDataMember]
		[XmlIgnore]
		public bool FileAsMappingSpecified
		{
			get
			{
				return base.PropertyBag.Contains(ContactSchema.FileAsMapping);
			}
			set
			{
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002B0E RID: 11022 RVA: 0x000AEE3C File Offset: 0x000AD03C
		// (set) Token: 0x06002B0F RID: 11023 RVA: 0x000AEE4E File Offset: 0x000AD04E
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public string DisplayName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.DisplayName);
			}
			set
			{
				base.PropertyBag[ContactSchema.DisplayName] = value;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x000AEE61 File Offset: 0x000AD061
		// (set) Token: 0x06002B11 RID: 11025 RVA: 0x000AEE73 File Offset: 0x000AD073
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public string GivenName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.GivenName);
			}
			set
			{
				base.PropertyBag[ContactSchema.GivenName] = value;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002B12 RID: 11026 RVA: 0x000AEE86 File Offset: 0x000AD086
		// (set) Token: 0x06002B13 RID: 11027 RVA: 0x000AEE98 File Offset: 0x000AD098
		[DataMember(EmitDefaultValue = false, Order = 5)]
		public string Initials
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.Initials);
			}
			set
			{
				base.PropertyBag[ContactSchema.Initials] = value;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06002B14 RID: 11028 RVA: 0x000AEEAB File Offset: 0x000AD0AB
		// (set) Token: 0x06002B15 RID: 11029 RVA: 0x000AEEBD File Offset: 0x000AD0BD
		[DataMember(EmitDefaultValue = false, Order = 6)]
		public string MiddleName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.MiddleName);
			}
			set
			{
				base.PropertyBag[ContactSchema.MiddleName] = value;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002B16 RID: 11030 RVA: 0x000AEED0 File Offset: 0x000AD0D0
		// (set) Token: 0x06002B17 RID: 11031 RVA: 0x000AEEE2 File Offset: 0x000AD0E2
		[DataMember(EmitDefaultValue = false, Order = 7)]
		public string Nickname
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.Nickname);
			}
			set
			{
				base.PropertyBag[ContactSchema.Nickname] = value;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06002B18 RID: 11032 RVA: 0x000AEEF5 File Offset: 0x000AD0F5
		// (set) Token: 0x06002B19 RID: 11033 RVA: 0x000AEF07 File Offset: 0x000AD107
		[DataMember(EmitDefaultValue = false, Order = 8)]
		public CompleteNameType CompleteName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<CompleteNameType>(ContactSchema.CompleteName);
			}
			set
			{
				base.PropertyBag[ContactSchema.CompleteName] = value;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002B1A RID: 11034 RVA: 0x000AEF1A File Offset: 0x000AD11A
		// (set) Token: 0x06002B1B RID: 11035 RVA: 0x000AEF2C File Offset: 0x000AD12C
		[DataMember(EmitDefaultValue = false, Order = 9)]
		public string CompanyName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.CompanyName);
			}
			set
			{
				base.PropertyBag[ContactSchema.CompanyName] = value;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002B1C RID: 11036 RVA: 0x000AEF3F File Offset: 0x000AD13F
		// (set) Token: 0x06002B1D RID: 11037 RVA: 0x000AEF47 File Offset: 0x000AD147
		[DataMember(EmitDefaultValue = false, Order = 10)]
		[XmlArrayItem("Entry", IsNullable = false)]
		public EmailAddressDictionaryEntryType[] EmailAddresses
		{
			get
			{
				return this.GetEmailAddresses();
			}
			set
			{
				this.SetEmailAddresses(value);
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06002B1E RID: 11038 RVA: 0x000AEF50 File Offset: 0x000AD150
		// (set) Token: 0x06002B1F RID: 11039 RVA: 0x000AEF58 File Offset: 0x000AD158
		[DataMember(EmitDefaultValue = false, Order = 11)]
		[XmlArrayItem("Entry", IsNullable = false)]
		public PhysicalAddressDictionaryEntryType[] PhysicalAddresses
		{
			get
			{
				return this.GetPhysicalAddresses();
			}
			set
			{
				this.SetPhysicalAddresses(value);
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06002B20 RID: 11040 RVA: 0x000AEF6A File Offset: 0x000AD16A
		// (set) Token: 0x06002B21 RID: 11041 RVA: 0x000AEF94 File Offset: 0x000AD194
		[XmlArrayItem("Entry", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 12)]
		public PhoneNumberDictionaryEntryType[] PhoneNumbers
		{
			get
			{
				return this.GetIndexedProperties<PhoneNumberDictionaryEntryType, PhoneNumberKeyType>(ContactItemType.phoneNumberToPropInfoMap, (PhoneNumberKeyType k, string v) => new PhoneNumberDictionaryEntryType(k, v));
			}
			set
			{
				if (value != null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						PhoneNumberDictionaryEntryType phoneNumberDictionaryEntryType = value[i];
						PropertyInformation property = ContactItemType.phoneNumberToPropInfoMap[phoneNumberDictionaryEntryType.Key];
						this[property] = phoneNumberDictionaryEntryType.Value;
					}
				}
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06002B22 RID: 11042 RVA: 0x000AEFD6 File Offset: 0x000AD1D6
		// (set) Token: 0x06002B23 RID: 11043 RVA: 0x000AEFE8 File Offset: 0x000AD1E8
		[DataMember(EmitDefaultValue = false, Order = 13)]
		public string AssistantName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.AssistantName);
			}
			set
			{
				base.PropertyBag[ContactSchema.AssistantName] = value;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x000AEFFB File Offset: 0x000AD1FB
		// (set) Token: 0x06002B25 RID: 11045 RVA: 0x000AF00D File Offset: 0x000AD20D
		[DataMember(EmitDefaultValue = false, Order = 14)]
		public string Birthday
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.Birthday);
			}
			set
			{
				base.PropertyBag[ContactSchema.Birthday] = value;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x000AF020 File Offset: 0x000AD220
		// (set) Token: 0x06002B27 RID: 11047 RVA: 0x000AF032 File Offset: 0x000AD232
		[IgnoreDataMember]
		[XmlIgnore]
		public bool BirthdaySpecified
		{
			get
			{
				return base.PropertyBag.Contains(ContactSchema.Birthday);
			}
			set
			{
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x000AF034 File Offset: 0x000AD234
		// (set) Token: 0x06002B29 RID: 11049 RVA: 0x000AF046 File Offset: 0x000AD246
		[XmlElement(DataType = "anyURI")]
		[DataMember(EmitDefaultValue = false, Order = 15)]
		public string BusinessHomePage
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.BusinessHomePage);
			}
			set
			{
				base.PropertyBag[ContactSchema.BusinessHomePage] = value;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06002B2A RID: 11050 RVA: 0x000AF059 File Offset: 0x000AD259
		// (set) Token: 0x06002B2B RID: 11051 RVA: 0x000AF06B File Offset: 0x000AD26B
		[XmlArrayItem("String", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 16)]
		public string[] Children
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string[]>(ContactSchema.Children);
			}
			set
			{
				base.PropertyBag[ContactSchema.Children] = value;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002B2C RID: 11052 RVA: 0x000AF07E File Offset: 0x000AD27E
		// (set) Token: 0x06002B2D RID: 11053 RVA: 0x000AF090 File Offset: 0x000AD290
		[DataMember(EmitDefaultValue = false, Order = 17)]
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Companies
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string[]>(ContactSchema.Companies);
			}
			set
			{
				base.PropertyBag[ContactSchema.Companies] = value;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06002B2E RID: 11054 RVA: 0x000AF0A3 File Offset: 0x000AD2A3
		// (set) Token: 0x06002B2F RID: 11055 RVA: 0x000AF0AB File Offset: 0x000AD2AB
		[XmlElement]
		[IgnoreDataMember]
		public ContactSourceType ContactSource
		{
			get
			{
				return this.contactSource;
			}
			set
			{
				this.contactSource = value;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06002B30 RID: 11056 RVA: 0x000AF0B4 File Offset: 0x000AD2B4
		// (set) Token: 0x06002B31 RID: 11057 RVA: 0x000AF0CB File Offset: 0x000AD2CB
		[DataMember(Name = "ContactSource", EmitDefaultValue = false, Order = 18)]
		[XmlIgnore]
		public string ContactSourceString
		{
			get
			{
				if (!this.contactSourceSpecified)
				{
					return null;
				}
				return EnumUtilities.ToString<ContactSourceType>(this.contactSource);
			}
			set
			{
				this.contactSource = EnumUtilities.Parse<ContactSourceType>(value);
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06002B32 RID: 11058 RVA: 0x000AF0D9 File Offset: 0x000AD2D9
		// (set) Token: 0x06002B33 RID: 11059 RVA: 0x000AF0E1 File Offset: 0x000AD2E1
		[IgnoreDataMember]
		[XmlIgnore]
		public bool ContactSourceSpecified
		{
			get
			{
				return this.contactSourceSpecified;
			}
			set
			{
				this.contactSourceSpecified = value;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002B34 RID: 11060 RVA: 0x000AF0EA File Offset: 0x000AD2EA
		// (set) Token: 0x06002B35 RID: 11061 RVA: 0x000AF0FC File Offset: 0x000AD2FC
		[DataMember(EmitDefaultValue = false, Order = 19)]
		public string Department
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.Department);
			}
			set
			{
				base.PropertyBag[ContactSchema.Department] = value;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x000AF10F File Offset: 0x000AD30F
		// (set) Token: 0x06002B37 RID: 11063 RVA: 0x000AF121 File Offset: 0x000AD321
		[DataMember(EmitDefaultValue = false, Order = 20)]
		public string Generation
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.Generation);
			}
			set
			{
				base.PropertyBag[ContactSchema.Generation] = value;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x000AF13D File Offset: 0x000AD33D
		// (set) Token: 0x06002B39 RID: 11065 RVA: 0x000AF168 File Offset: 0x000AD368
		[DataMember(EmitDefaultValue = false, Order = 21)]
		[XmlArrayItem("Entry", IsNullable = false)]
		public ImAddressDictionaryEntryType[] ImAddresses
		{
			get
			{
				return this.GetIndexedProperties<ImAddressDictionaryEntryType, ImAddressKeyType>(ContactItemType.imAddressToPropInfoMap, (ImAddressKeyType k, string v) => new ImAddressDictionaryEntryType(k, v));
			}
			set
			{
				if (value != null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						ImAddressDictionaryEntryType imAddressDictionaryEntryType = value[i];
						PropertyInformation property = ContactItemType.imAddressToPropInfoMap[imAddressDictionaryEntryType.Key];
						this[property] = imAddressDictionaryEntryType.Value;
					}
				}
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x000AF1AA File Offset: 0x000AD3AA
		// (set) Token: 0x06002B3B RID: 11067 RVA: 0x000AF1BC File Offset: 0x000AD3BC
		[DataMember(EmitDefaultValue = false, Order = 22)]
		public string JobTitle
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.JobTitle);
			}
			set
			{
				base.PropertyBag[ContactSchema.JobTitle] = value;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x000AF1CF File Offset: 0x000AD3CF
		// (set) Token: 0x06002B3D RID: 11069 RVA: 0x000AF1E1 File Offset: 0x000AD3E1
		[DataMember(EmitDefaultValue = false, Order = 23)]
		public string Manager
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.Manager);
			}
			set
			{
				base.PropertyBag[ContactSchema.Manager] = value;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x000AF1F4 File Offset: 0x000AD3F4
		// (set) Token: 0x06002B3F RID: 11071 RVA: 0x000AF206 File Offset: 0x000AD406
		[DataMember(EmitDefaultValue = false, Order = 24)]
		public string Mileage
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.Mileage);
			}
			set
			{
				base.PropertyBag[ContactSchema.Mileage] = value;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x000AF219 File Offset: 0x000AD419
		// (set) Token: 0x06002B41 RID: 11073 RVA: 0x000AF22B File Offset: 0x000AD42B
		[DataMember(EmitDefaultValue = false, Order = 25)]
		public string OfficeLocation
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.OfficeLocation);
			}
			set
			{
				base.PropertyBag[ContactSchema.OfficeLocation] = value;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002B42 RID: 11074 RVA: 0x000AF23E File Offset: 0x000AD43E
		// (set) Token: 0x06002B43 RID: 11075 RVA: 0x000AF250 File Offset: 0x000AD450
		[IgnoreDataMember]
		[XmlElement]
		public PhysicalAddressIndexType PostalAddressIndex
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<PhysicalAddressIndexType>(ContactSchema.PostalAddressIndex);
			}
			set
			{
				base.PropertyBag[ContactSchema.PostalAddressIndex] = value;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002B44 RID: 11076 RVA: 0x000AF268 File Offset: 0x000AD468
		// (set) Token: 0x06002B45 RID: 11077 RVA: 0x000AF27F File Offset: 0x000AD47F
		[DataMember(Name = "PostalAddressIndex", EmitDefaultValue = false, Order = 26)]
		[XmlIgnore]
		public string PostalAddressIndexString
		{
			get
			{
				if (!this.PostalAddressIndexSpecified)
				{
					return null;
				}
				return EnumUtilities.ToString<PhysicalAddressIndexType>(this.PostalAddressIndex);
			}
			set
			{
				this.PostalAddressIndex = EnumUtilities.Parse<PhysicalAddressIndexType>(value);
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002B46 RID: 11078 RVA: 0x000AF28D File Offset: 0x000AD48D
		// (set) Token: 0x06002B47 RID: 11079 RVA: 0x000AF29F File Offset: 0x000AD49F
		[XmlIgnore]
		[IgnoreDataMember]
		public bool PostalAddressIndexSpecified
		{
			get
			{
				return base.PropertyBag.Contains(ContactSchema.PostalAddressIndex);
			}
			set
			{
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06002B48 RID: 11080 RVA: 0x000AF2A1 File Offset: 0x000AD4A1
		// (set) Token: 0x06002B49 RID: 11081 RVA: 0x000AF2B3 File Offset: 0x000AD4B3
		[DataMember(EmitDefaultValue = false, Order = 27)]
		public string Profession
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.Profession);
			}
			set
			{
				base.PropertyBag[ContactSchema.Profession] = value;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002B4A RID: 11082 RVA: 0x000AF2C6 File Offset: 0x000AD4C6
		// (set) Token: 0x06002B4B RID: 11083 RVA: 0x000AF2D8 File Offset: 0x000AD4D8
		[DataMember(EmitDefaultValue = false, Order = 28)]
		public string SpouseName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.SpouseName);
			}
			set
			{
				base.PropertyBag[ContactSchema.SpouseName] = value;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002B4C RID: 11084 RVA: 0x000AF2EB File Offset: 0x000AD4EB
		// (set) Token: 0x06002B4D RID: 11085 RVA: 0x000AF2FD File Offset: 0x000AD4FD
		[DataMember(EmitDefaultValue = false, Order = 29)]
		public string Surname
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.Surname);
			}
			set
			{
				base.PropertyBag[ContactSchema.Surname] = value;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002B4E RID: 11086 RVA: 0x000AF310 File Offset: 0x000AD510
		// (set) Token: 0x06002B4F RID: 11087 RVA: 0x000AF322 File Offset: 0x000AD522
		[DataMember(EmitDefaultValue = false, Order = 30)]
		public string WeddingAnniversary
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.WeddingAnniversary);
			}
			set
			{
				base.PropertyBag[ContactSchema.WeddingAnniversary] = value;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002B50 RID: 11088 RVA: 0x000AF335 File Offset: 0x000AD535
		// (set) Token: 0x06002B51 RID: 11089 RVA: 0x000AF347 File Offset: 0x000AD547
		[XmlIgnore]
		[IgnoreDataMember]
		public bool WeddingAnniversarySpecified
		{
			get
			{
				return base.PropertyBag.Contains(ContactSchema.WeddingAnniversary);
			}
			set
			{
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002B52 RID: 11090 RVA: 0x000AF349 File Offset: 0x000AD549
		// (set) Token: 0x06002B53 RID: 11091 RVA: 0x000AF35B File Offset: 0x000AD55B
		[DataMember(EmitDefaultValue = false, Order = 31)]
		public bool HasPicture
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<bool>(ContactSchema.HasPicture);
			}
			set
			{
				base.PropertyBag[ContactSchema.HasPicture] = value;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06002B54 RID: 11092 RVA: 0x000AF373 File Offset: 0x000AD573
		// (set) Token: 0x06002B55 RID: 11093 RVA: 0x000AF385 File Offset: 0x000AD585
		[XmlIgnore]
		[IgnoreDataMember]
		public bool HasPictureSpecified
		{
			get
			{
				return base.PropertyBag.Contains(ContactSchema.HasPicture);
			}
			set
			{
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06002B56 RID: 11094 RVA: 0x000AF387 File Offset: 0x000AD587
		// (set) Token: 0x06002B57 RID: 11095 RVA: 0x000AF399 File Offset: 0x000AD599
		[DataMember(EmitDefaultValue = false, Order = 32)]
		public string PhoneticFullName
		{
			get
			{
				return this.GetDirectoryProperty("PhoneticFullName") as string;
			}
			set
			{
				this.SetDirectoryProperty("PhoneticFullName", value);
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06002B58 RID: 11096 RVA: 0x000AF3A7 File Offset: 0x000AD5A7
		// (set) Token: 0x06002B59 RID: 11097 RVA: 0x000AF3B9 File Offset: 0x000AD5B9
		[DataMember(EmitDefaultValue = false, Order = 33)]
		public string PhoneticFirstName
		{
			get
			{
				return this.GetDirectoryProperty("PhoneticFirstName") as string;
			}
			set
			{
				this.SetDirectoryProperty("PhoneticFirstName", value);
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06002B5A RID: 11098 RVA: 0x000AF3C7 File Offset: 0x000AD5C7
		// (set) Token: 0x06002B5B RID: 11099 RVA: 0x000AF3D9 File Offset: 0x000AD5D9
		[DataMember(EmitDefaultValue = false, Order = 34)]
		public string PhoneticLastName
		{
			get
			{
				return this.GetDirectoryProperty("PhoneticLastName") as string;
			}
			set
			{
				this.SetDirectoryProperty("PhoneticLastName", value);
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002B5C RID: 11100 RVA: 0x000AF3E7 File Offset: 0x000AD5E7
		// (set) Token: 0x06002B5D RID: 11101 RVA: 0x000AF3F9 File Offset: 0x000AD5F9
		[DataMember(EmitDefaultValue = false, Order = 35)]
		public string Alias
		{
			get
			{
				return this.GetDirectoryProperty("Alias") as string;
			}
			set
			{
				this.SetDirectoryProperty("Alias", value);
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002B5E RID: 11102 RVA: 0x000AF407 File Offset: 0x000AD607
		// (set) Token: 0x06002B5F RID: 11103 RVA: 0x000AF419 File Offset: 0x000AD619
		[DataMember(EmitDefaultValue = false, Order = 36)]
		public string Notes
		{
			get
			{
				return this.GetDirectoryProperty("Notes") as string;
			}
			set
			{
				this.SetDirectoryProperty("Notes", value);
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002B60 RID: 11104 RVA: 0x000AF427 File Offset: 0x000AD627
		// (set) Token: 0x06002B61 RID: 11105 RVA: 0x000AF439 File Offset: 0x000AD639
		[XmlElement(DataType = "base64Binary")]
		[IgnoreDataMember]
		public byte[] Photo
		{
			get
			{
				return this.GetDirectoryProperty("Photo") as byte[];
			}
			set
			{
				this.SetDirectoryProperty("Photo", value);
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x000AF448 File Offset: 0x000AD648
		// (set) Token: 0x06002B63 RID: 11107 RVA: 0x000AF471 File Offset: 0x000AD671
		[XmlIgnore]
		[DataMember(Name = "Photo", EmitDefaultValue = false, Order = 37, IsRequired = false)]
		public string PhotoString
		{
			get
			{
				byte[] array = this.GetDirectoryProperty("Photo") as byte[];
				if (array != null)
				{
					return Convert.ToBase64String(array);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.SetDirectoryProperty("Photo", Convert.FromBase64String(value));
					return;
				}
				this.SetDirectoryProperty("Photo", value);
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x000AF494 File Offset: 0x000AD694
		// (set) Token: 0x06002B65 RID: 11109 RVA: 0x000AF4A6 File Offset: 0x000AD6A6
		[XmlArrayItem("Base64Binary", DataType = "base64Binary", Type = typeof(byte[]))]
		[IgnoreDataMember]
		[XmlArray]
		public byte[][] UserSMIMECertificate
		{
			get
			{
				return this.GetDirectoryProperty("UserSMIMECertificate") as byte[][];
			}
			set
			{
				this.SetDirectoryProperty("UserSMIMECertificate", value);
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002B66 RID: 11110 RVA: 0x000AF4B4 File Offset: 0x000AD6B4
		// (set) Token: 0x06002B67 RID: 11111 RVA: 0x000AF4CB File Offset: 0x000AD6CB
		[DataMember(Name = "UserSMIMECertificate", EmitDefaultValue = false, Order = 38, IsRequired = false)]
		[XmlIgnore]
		public string[] UserSMIMECertificateString
		{
			get
			{
				return ContactItemType.Base64StringArrayFromJaggedByteArrayArray(this.GetDirectoryProperty("UserSMIMECertificate") as byte[][]);
			}
			set
			{
				this.SetDirectoryProperty("UserSMIMECertificate", ContactItemType.JaggedByteArrayArrayFromBase64StringArray(value));
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06002B68 RID: 11112 RVA: 0x000AF4DE File Offset: 0x000AD6DE
		// (set) Token: 0x06002B69 RID: 11113 RVA: 0x000AF4F0 File Offset: 0x000AD6F0
		[XmlArray]
		[XmlArrayItem("Base64Binary", DataType = "base64Binary", Type = typeof(byte[]))]
		[IgnoreDataMember]
		public byte[][] MSExchangeCertificate
		{
			get
			{
				return this.GetDirectoryProperty("MSExchangeCertificate") as byte[][];
			}
			set
			{
				this.SetDirectoryProperty("MSExchangeCertificate", value);
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06002B6A RID: 11114 RVA: 0x000AF4FE File Offset: 0x000AD6FE
		// (set) Token: 0x06002B6B RID: 11115 RVA: 0x000AF515 File Offset: 0x000AD715
		[XmlIgnore]
		[DataMember(Name = "MSExchangeCertificate", EmitDefaultValue = false, Order = 39, IsRequired = false)]
		public string[] MSExchangeCertificateString
		{
			get
			{
				return ContactItemType.Base64StringArrayFromJaggedByteArrayArray(this.GetDirectoryProperty("MSExchangeCertificate") as byte[][]);
			}
			set
			{
				this.SetDirectoryProperty("MSExchangeCertificate", ContactItemType.JaggedByteArrayArrayFromBase64StringArray(value));
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x000AF528 File Offset: 0x000AD728
		// (set) Token: 0x06002B6D RID: 11117 RVA: 0x000AF53A File Offset: 0x000AD73A
		[DataMember(EmitDefaultValue = false, Order = 40)]
		public string DirectoryId
		{
			get
			{
				return this.GetDirectoryProperty("DirectoryId") as string;
			}
			set
			{
				this.SetDirectoryProperty("DirectoryId", value);
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x000AF548 File Offset: 0x000AD748
		// (set) Token: 0x06002B6F RID: 11119 RVA: 0x000AF55A File Offset: 0x000AD75A
		[DataMember(EmitDefaultValue = false, Order = 41)]
		public SingleRecipientType ManagerMailbox
		{
			get
			{
				return this.GetDirectoryProperty("ManagerMailbox") as SingleRecipientType;
			}
			set
			{
				this.SetDirectoryProperty("ManagerMailbox", value);
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06002B70 RID: 11120 RVA: 0x000AF568 File Offset: 0x000AD768
		// (set) Token: 0x06002B71 RID: 11121 RVA: 0x000AF57A File Offset: 0x000AD77A
		[DataMember(EmitDefaultValue = false, Order = 42)]
		[XmlArrayItem("Mailbox", Type = typeof(EmailAddressWrapper))]
		public EmailAddressWrapper[] DirectReports
		{
			get
			{
				return this.GetDirectoryProperty("DirectReports") as EmailAddressWrapper[];
			}
			set
			{
				this.SetDirectoryProperty("DirectReports", value);
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06002B72 RID: 11122 RVA: 0x000AF588 File Offset: 0x000AD788
		// (set) Token: 0x06002B73 RID: 11123 RVA: 0x000AF59A File Offset: 0x000AD79A
		[DataMember(EmitDefaultValue = false, Order = 43)]
		public string BirthdayLocal
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.BirthdayLocal);
			}
			set
			{
				base.PropertyBag[ContactSchema.BirthdayLocal] = value;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002B74 RID: 11124 RVA: 0x000AF5AD File Offset: 0x000AD7AD
		// (set) Token: 0x06002B75 RID: 11125 RVA: 0x000AF5BF File Offset: 0x000AD7BF
		[DataMember(EmitDefaultValue = false, Order = 44)]
		public string WeddingAnniversaryLocal
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ContactSchema.WeddingAnniversaryLocal);
			}
			set
			{
				base.PropertyBag[ContactSchema.WeddingAnniversaryLocal] = value;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06002B76 RID: 11126 RVA: 0x000AF5D2 File Offset: 0x000AD7D2
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.Contact;
			}
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x000AF5D8 File Offset: 0x000AD7D8
		private static byte[][] JaggedByteArrayArrayFromBase64StringArray(string[] inArray)
		{
			if (inArray != null)
			{
				List<byte[]> list = new List<byte[]>(inArray.Length);
				foreach (string text in inArray)
				{
					if (text != null)
					{
						list.Add(Convert.FromBase64String(text));
					}
					else
					{
						list.Add(null);
					}
				}
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x000AF628 File Offset: 0x000AD828
		private static string[] Base64StringArrayFromJaggedByteArrayArray(byte[][] inArray)
		{
			if (inArray != null)
			{
				List<string> list = new List<string>(inArray.Length);
				foreach (byte[] array in inArray)
				{
					if (array != null)
					{
						list.Add(Convert.ToBase64String(array));
					}
					else
					{
						list.Add(null);
					}
				}
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x000AF678 File Offset: 0x000AD878
		private object GetDirectoryProperty(string key)
		{
			object result;
			if (this.directoryProperties.TryGetValue(key, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000AF698 File Offset: 0x000AD898
		private void SetDirectoryProperty(string key, object value)
		{
			if (this.directoryProperties.ContainsKey(key))
			{
				this.directoryProperties[key] = value;
				return;
			}
			this.directoryProperties.Add(key, value);
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x000AF6C4 File Offset: 0x000AD8C4
		private TEntry[] GetIndexedProperties<TEntry, TKey>(Dictionary<TKey, PropertyInformation> map, Func<TKey, string, TEntry> createEntryFunc)
		{
			List<TEntry> list = new List<TEntry>();
			foreach (KeyValuePair<TKey, PropertyInformation> keyValuePair in map)
			{
				string valueOrDefault = base.GetValueOrDefault<string>(keyValuePair.Value);
				if (valueOrDefault != null)
				{
					TEntry item = createEntryFunc(keyValuePair.Key, valueOrDefault);
					list.Add(item);
				}
			}
			if (list.Count > 0)
			{
				return list.ToArray();
			}
			return null;
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000AF74C File Offset: 0x000AD94C
		private void SetPropertyIfValueIsSet(PropertyInformation propInfo, string value)
		{
			if (value != null)
			{
				this[propInfo] = value;
			}
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x000AF75C File Offset: 0x000AD95C
		private EmailAddressDictionaryEntryType[] GetEmailAddresses()
		{
			List<EmailAddressDictionaryEntryType> list = null;
			foreach (PropertyInformation propertyInfo in ContactItemType.emailAddressProps)
			{
				EmailAddressDictionaryEntryType valueOrDefault = base.GetValueOrDefault<EmailAddressDictionaryEntryType>(propertyInfo);
				if (valueOrDefault != null)
				{
					if (list == null)
					{
						list = new List<EmailAddressDictionaryEntryType>();
					}
					list.Add(valueOrDefault);
				}
			}
			if (list == null)
			{
				return null;
			}
			return list.ToArray();
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x000AF7B0 File Offset: 0x000AD9B0
		private void SetEmailAddresses(EmailAddressDictionaryEntryType[] value)
		{
			if (value != null)
			{
				foreach (EmailAddressDictionaryEntryType emailAddressDictionaryEntryType in value)
				{
					PropertyInformation property = ContactItemType.emailAddressToPropInfoMap[emailAddressDictionaryEntryType.Key];
					this[property] = emailAddressDictionaryEntryType;
				}
			}
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x000AF7F0 File Offset: 0x000AD9F0
		private PhysicalAddressDictionaryEntryType[] GetPhysicalAddresses()
		{
			List<PhysicalAddressDictionaryEntryType> list = null;
			list = this.AddPhysicalAddressIfNeeded(list, PhysicalAddressKeyType.Business, ContactSchema.PhysicalAddressBusinessStreet, ContactSchema.PhysicalAddressBusinessCity, ContactSchema.PhysicalAddressBusinessState, ContactSchema.PhysicalAddressBusinessCountryOrRegion, ContactSchema.PhysicalAddressBusinessPostalCode);
			list = this.AddPhysicalAddressIfNeeded(list, PhysicalAddressKeyType.Home, ContactSchema.PhysicalAddressHomeStreet, ContactSchema.PhysicalAddressHomeCity, ContactSchema.PhysicalAddressHomeState, ContactSchema.PhysicalAddressHomeCountryOrRegion, ContactSchema.PhysicalAddressHomePostalCode);
			list = this.AddPhysicalAddressIfNeeded(list, PhysicalAddressKeyType.Other, ContactSchema.PhysicalAddressOtherStreet, ContactSchema.PhysicalAddressOtherCity, ContactSchema.PhysicalAddressOtherState, ContactSchema.PhysicalAddressOtherCountryOrRegion, ContactSchema.PhysicalAddressOtherPostalCode);
			if (list == null)
			{
				return null;
			}
			return list.ToArray();
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x000AF870 File Offset: 0x000ADA70
		private List<PhysicalAddressDictionaryEntryType> AddPhysicalAddressIfNeeded(List<PhysicalAddressDictionaryEntryType> addressList, PhysicalAddressKeyType key, PropertyInformation streetProp, PropertyInformation cityProp, PropertyInformation stateProp, PropertyInformation countryOrRegionProp, PropertyInformation postalCodeProp)
		{
			if (base.IsSet(streetProp) || base.IsSet(cityProp) || base.IsSet(stateProp) || base.IsSet(countryOrRegionProp) || base.IsSet(postalCodeProp))
			{
				PhysicalAddressDictionaryEntryType item = new PhysicalAddressDictionaryEntryType
				{
					Key = key,
					Street = base.GetValueOrDefault<string>(streetProp),
					City = base.GetValueOrDefault<string>(cityProp),
					State = base.GetValueOrDefault<string>(stateProp),
					CountryOrRegion = base.GetValueOrDefault<string>(countryOrRegionProp),
					PostalCode = base.GetValueOrDefault<string>(postalCodeProp)
				};
				if (addressList == null)
				{
					addressList = new List<PhysicalAddressDictionaryEntryType>();
				}
				addressList.Add(item);
			}
			return addressList;
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000AF914 File Offset: 0x000ADB14
		private void SetPhysicalAddresses(PhysicalAddressDictionaryEntryType[] value)
		{
			foreach (PhysicalAddressDictionaryEntryType physicalAddressDictionaryEntryType in value)
			{
				switch (physicalAddressDictionaryEntryType.Key)
				{
				case PhysicalAddressKeyType.Home:
					this.SetPropertyIfValueIsSet(ContactSchema.PhysicalAddressHomeStreet, physicalAddressDictionaryEntryType.Street);
					this.SetPropertyIfValueIsSet(ContactSchema.PhysicalAddressHomeCity, physicalAddressDictionaryEntryType.City);
					this.SetPropertyIfValueIsSet(ContactSchema.PhysicalAddressHomeState, physicalAddressDictionaryEntryType.State);
					this.SetPropertyIfValueIsSet(ContactSchema.PhysicalAddressHomePostalCode, physicalAddressDictionaryEntryType.PostalCode);
					this.SetPropertyIfValueIsSet(ContactSchema.PhysicalAddressHomeCountryOrRegion, physicalAddressDictionaryEntryType.CountryOrRegion);
					break;
				case PhysicalAddressKeyType.Business:
					this.SetPropertyIfValueIsSet(ContactSchema.PhysicalAddressBusinessStreet, physicalAddressDictionaryEntryType.Street);
					this.SetPropertyIfValueIsSet(ContactSchema.PhysicalAddressBusinessCity, physicalAddressDictionaryEntryType.City);
					this.SetPropertyIfValueIsSet(ContactSchema.PhysicalAddressBusinessState, physicalAddressDictionaryEntryType.State);
					this.SetPropertyIfValueIsSet(ContactSchema.PhysicalAddressBusinessPostalCode, physicalAddressDictionaryEntryType.PostalCode);
					this.SetPropertyIfValueIsSet(ContactSchema.PhysicalAddressBusinessCountryOrRegion, physicalAddressDictionaryEntryType.CountryOrRegion);
					break;
				case PhysicalAddressKeyType.Other:
					this.SetPropertyIfValueIsSet(ContactSchema.PhysicalAddressOtherStreet, physicalAddressDictionaryEntryType.Street);
					this.SetPropertyIfValueIsSet(ContactSchema.PhysicalAddressOtherCity, physicalAddressDictionaryEntryType.City);
					this.SetPropertyIfValueIsSet(ContactSchema.PhysicalAddressOtherState, physicalAddressDictionaryEntryType.State);
					this.SetPropertyIfValueIsSet(ContactSchema.PhysicalAddressOtherPostalCode, physicalAddressDictionaryEntryType.PostalCode);
					this.SetPropertyIfValueIsSet(ContactSchema.PhysicalAddressOtherCountryOrRegion, physicalAddressDictionaryEntryType.CountryOrRegion);
					break;
				}
			}
		}

		// Token: 0x04001A32 RID: 6706
		private const string DirId = "DirectoryId";

		// Token: 0x04001A33 RID: 6707
		private const string DirPhoneticFullName = "PhoneticFullName";

		// Token: 0x04001A34 RID: 6708
		private const string DirPhoneticFirstName = "PhoneticFirstName";

		// Token: 0x04001A35 RID: 6709
		private const string DirPhoneticLastName = "PhoneticLastName";

		// Token: 0x04001A36 RID: 6710
		private const string DirAlias = "Alias";

		// Token: 0x04001A37 RID: 6711
		private const string DirNotes = "Notes";

		// Token: 0x04001A38 RID: 6712
		private const string DirPhoto = "Photo";

		// Token: 0x04001A39 RID: 6713
		private const string DirUserSMIMECertificate = "UserSMIMECertificate";

		// Token: 0x04001A3A RID: 6714
		private const string DirMSExchangeCertificate = "MSExchangeCertificate";

		// Token: 0x04001A3B RID: 6715
		private const string DirManagerMailbox = "ManagerMailbox";

		// Token: 0x04001A3C RID: 6716
		private const string DirDirectReports = "DirectReports";

		// Token: 0x04001A3D RID: 6717
		private static Dictionary<ImAddressKeyType, PropertyInformation> imAddressToPropInfoMap = new Dictionary<ImAddressKeyType, PropertyInformation>
		{
			{
				ImAddressKeyType.ImAddress1,
				ContactSchema.ImAddressImAddress1
			},
			{
				ImAddressKeyType.ImAddress2,
				ContactSchema.ImAddressImAddress2
			},
			{
				ImAddressKeyType.ImAddress3,
				ContactSchema.ImAddressImAddress3
			}
		};

		// Token: 0x04001A3E RID: 6718
		private static Dictionary<EmailAddressKeyType, PropertyInformation> emailAddressToPropInfoMap = new Dictionary<EmailAddressKeyType, PropertyInformation>
		{
			{
				EmailAddressKeyType.EmailAddress1,
				ContactSchema.EmailAddressEmailAddress1
			},
			{
				EmailAddressKeyType.EmailAddress2,
				ContactSchema.EmailAddressEmailAddress2
			},
			{
				EmailAddressKeyType.EmailAddress3,
				ContactSchema.EmailAddressEmailAddress3
			}
		};

		// Token: 0x04001A3F RID: 6719
		private static Dictionary<PhoneNumberKeyType, PropertyInformation> phoneNumberToPropInfoMap = new Dictionary<PhoneNumberKeyType, PropertyInformation>
		{
			{
				PhoneNumberKeyType.AssistantPhone,
				ContactSchema.PhoneNumberAssistantPhone
			},
			{
				PhoneNumberKeyType.BusinessFax,
				ContactSchema.PhoneNumberBusinessFax
			},
			{
				PhoneNumberKeyType.BusinessPhone,
				ContactSchema.PhoneNumberBusinessPhone
			},
			{
				PhoneNumberKeyType.BusinessPhone2,
				ContactSchema.PhoneNumberBusinessPhone2
			},
			{
				PhoneNumberKeyType.Callback,
				ContactSchema.PhoneNumberCallback
			},
			{
				PhoneNumberKeyType.CarPhone,
				ContactSchema.PhoneNumberCarPhone
			},
			{
				PhoneNumberKeyType.CompanyMainPhone,
				ContactSchema.PhoneNumberCompanyMainPhone
			},
			{
				PhoneNumberKeyType.HomeFax,
				ContactSchema.PhoneNumberHomeFax
			},
			{
				PhoneNumberKeyType.HomePhone,
				ContactSchema.PhoneNumberHomePhone
			},
			{
				PhoneNumberKeyType.HomePhone2,
				ContactSchema.PhoneNumberHomePhone2
			},
			{
				PhoneNumberKeyType.Isdn,
				ContactSchema.PhoneNumberIsdn
			},
			{
				PhoneNumberKeyType.MobilePhone,
				ContactSchema.PhoneNumberMobilePhone
			},
			{
				PhoneNumberKeyType.OtherFax,
				ContactSchema.PhoneNumberOtherFax
			},
			{
				PhoneNumberKeyType.OtherTelephone,
				ContactSchema.PhoneNumberOtherTelephone
			},
			{
				PhoneNumberKeyType.Pager,
				ContactSchema.PhoneNumberPager
			},
			{
				PhoneNumberKeyType.PrimaryPhone,
				ContactSchema.PhoneNumberPrimaryPhone
			},
			{
				PhoneNumberKeyType.RadioPhone,
				ContactSchema.PhoneNumberRadioPhone
			},
			{
				PhoneNumberKeyType.Telex,
				ContactSchema.PhoneNumberTelex
			},
			{
				PhoneNumberKeyType.TtyTddPhone,
				ContactSchema.PhoneNumberTtyTddPhone
			}
		};

		// Token: 0x04001A40 RID: 6720
		private static PropertyInformation[] emailAddressProps = new PropertyInformation[]
		{
			ContactSchema.EmailAddressEmailAddress1,
			ContactSchema.EmailAddressEmailAddress2,
			ContactSchema.EmailAddressEmailAddress3
		};

		// Token: 0x04001A41 RID: 6721
		private Dictionary<string, object> directoryProperties = new Dictionary<string, object>();

		// Token: 0x04001A42 RID: 6722
		private ContactSourceType contactSource = ContactSourceType.Store;

		// Token: 0x04001A43 RID: 6723
		private bool contactSourceSpecified;
	}
}
