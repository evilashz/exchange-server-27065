using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200014D RID: 333
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ContactItemType : ItemType
	{
		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x000228EB File Offset: 0x00020AEB
		// (set) Token: 0x06000E5D RID: 3677 RVA: 0x000228F3 File Offset: 0x00020AF3
		public string FileAs
		{
			get
			{
				return this.fileAsField;
			}
			set
			{
				this.fileAsField = value;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x000228FC File Offset: 0x00020AFC
		// (set) Token: 0x06000E5F RID: 3679 RVA: 0x00022904 File Offset: 0x00020B04
		public FileAsMappingType FileAsMapping
		{
			get
			{
				return this.fileAsMappingField;
			}
			set
			{
				this.fileAsMappingField = value;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0002290D File Offset: 0x00020B0D
		// (set) Token: 0x06000E61 RID: 3681 RVA: 0x00022915 File Offset: 0x00020B15
		[XmlIgnore]
		public bool FileAsMappingSpecified
		{
			get
			{
				return this.fileAsMappingFieldSpecified;
			}
			set
			{
				this.fileAsMappingFieldSpecified = value;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x0002291E File Offset: 0x00020B1E
		// (set) Token: 0x06000E63 RID: 3683 RVA: 0x00022926 File Offset: 0x00020B26
		public string DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x0002292F File Offset: 0x00020B2F
		// (set) Token: 0x06000E65 RID: 3685 RVA: 0x00022937 File Offset: 0x00020B37
		public string GivenName
		{
			get
			{
				return this.givenNameField;
			}
			set
			{
				this.givenNameField = value;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x00022940 File Offset: 0x00020B40
		// (set) Token: 0x06000E67 RID: 3687 RVA: 0x00022948 File Offset: 0x00020B48
		public string Initials
		{
			get
			{
				return this.initialsField;
			}
			set
			{
				this.initialsField = value;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00022951 File Offset: 0x00020B51
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x00022959 File Offset: 0x00020B59
		public string MiddleName
		{
			get
			{
				return this.middleNameField;
			}
			set
			{
				this.middleNameField = value;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00022962 File Offset: 0x00020B62
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x0002296A File Offset: 0x00020B6A
		public string Nickname
		{
			get
			{
				return this.nicknameField;
			}
			set
			{
				this.nicknameField = value;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00022973 File Offset: 0x00020B73
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x0002297B File Offset: 0x00020B7B
		public CompleteNameType CompleteName
		{
			get
			{
				return this.completeNameField;
			}
			set
			{
				this.completeNameField = value;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00022984 File Offset: 0x00020B84
		// (set) Token: 0x06000E6F RID: 3695 RVA: 0x0002298C File Offset: 0x00020B8C
		public string CompanyName
		{
			get
			{
				return this.companyNameField;
			}
			set
			{
				this.companyNameField = value;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00022995 File Offset: 0x00020B95
		// (set) Token: 0x06000E71 RID: 3697 RVA: 0x0002299D File Offset: 0x00020B9D
		[XmlArrayItem("Entry", IsNullable = false)]
		public EmailAddressDictionaryEntryType[] EmailAddresses
		{
			get
			{
				return this.emailAddressesField;
			}
			set
			{
				this.emailAddressesField = value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x000229A6 File Offset: 0x00020BA6
		// (set) Token: 0x06000E73 RID: 3699 RVA: 0x000229AE File Offset: 0x00020BAE
		[XmlArrayItem("Entry", IsNullable = false)]
		public PhysicalAddressDictionaryEntryType[] PhysicalAddresses
		{
			get
			{
				return this.physicalAddressesField;
			}
			set
			{
				this.physicalAddressesField = value;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x000229B7 File Offset: 0x00020BB7
		// (set) Token: 0x06000E75 RID: 3701 RVA: 0x000229BF File Offset: 0x00020BBF
		[XmlArrayItem("Entry", IsNullable = false)]
		public PhoneNumberDictionaryEntryType[] PhoneNumbers
		{
			get
			{
				return this.phoneNumbersField;
			}
			set
			{
				this.phoneNumbersField = value;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x000229C8 File Offset: 0x00020BC8
		// (set) Token: 0x06000E77 RID: 3703 RVA: 0x000229D0 File Offset: 0x00020BD0
		public string AssistantName
		{
			get
			{
				return this.assistantNameField;
			}
			set
			{
				this.assistantNameField = value;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x000229D9 File Offset: 0x00020BD9
		// (set) Token: 0x06000E79 RID: 3705 RVA: 0x000229E1 File Offset: 0x00020BE1
		public DateTime Birthday
		{
			get
			{
				return this.birthdayField;
			}
			set
			{
				this.birthdayField = value;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x000229EA File Offset: 0x00020BEA
		// (set) Token: 0x06000E7B RID: 3707 RVA: 0x000229F2 File Offset: 0x00020BF2
		[XmlIgnore]
		public bool BirthdaySpecified
		{
			get
			{
				return this.birthdayFieldSpecified;
			}
			set
			{
				this.birthdayFieldSpecified = value;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x000229FB File Offset: 0x00020BFB
		// (set) Token: 0x06000E7D RID: 3709 RVA: 0x00022A03 File Offset: 0x00020C03
		[XmlElement(DataType = "anyURI")]
		public string BusinessHomePage
		{
			get
			{
				return this.businessHomePageField;
			}
			set
			{
				this.businessHomePageField = value;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x00022A0C File Offset: 0x00020C0C
		// (set) Token: 0x06000E7F RID: 3711 RVA: 0x00022A14 File Offset: 0x00020C14
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Children
		{
			get
			{
				return this.childrenField;
			}
			set
			{
				this.childrenField = value;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x00022A1D File Offset: 0x00020C1D
		// (set) Token: 0x06000E81 RID: 3713 RVA: 0x00022A25 File Offset: 0x00020C25
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Companies
		{
			get
			{
				return this.companiesField;
			}
			set
			{
				this.companiesField = value;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x00022A2E File Offset: 0x00020C2E
		// (set) Token: 0x06000E83 RID: 3715 RVA: 0x00022A36 File Offset: 0x00020C36
		public ContactSourceType ContactSource
		{
			get
			{
				return this.contactSourceField;
			}
			set
			{
				this.contactSourceField = value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00022A3F File Offset: 0x00020C3F
		// (set) Token: 0x06000E85 RID: 3717 RVA: 0x00022A47 File Offset: 0x00020C47
		[XmlIgnore]
		public bool ContactSourceSpecified
		{
			get
			{
				return this.contactSourceFieldSpecified;
			}
			set
			{
				this.contactSourceFieldSpecified = value;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x00022A50 File Offset: 0x00020C50
		// (set) Token: 0x06000E87 RID: 3719 RVA: 0x00022A58 File Offset: 0x00020C58
		public string Department
		{
			get
			{
				return this.departmentField;
			}
			set
			{
				this.departmentField = value;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x00022A61 File Offset: 0x00020C61
		// (set) Token: 0x06000E89 RID: 3721 RVA: 0x00022A69 File Offset: 0x00020C69
		public string Generation
		{
			get
			{
				return this.generationField;
			}
			set
			{
				this.generationField = value;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x00022A72 File Offset: 0x00020C72
		// (set) Token: 0x06000E8B RID: 3723 RVA: 0x00022A7A File Offset: 0x00020C7A
		[XmlArrayItem("Entry", IsNullable = false)]
		public ImAddressDictionaryEntryType[] ImAddresses
		{
			get
			{
				return this.imAddressesField;
			}
			set
			{
				this.imAddressesField = value;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x00022A83 File Offset: 0x00020C83
		// (set) Token: 0x06000E8D RID: 3725 RVA: 0x00022A8B File Offset: 0x00020C8B
		public string JobTitle
		{
			get
			{
				return this.jobTitleField;
			}
			set
			{
				this.jobTitleField = value;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x00022A94 File Offset: 0x00020C94
		// (set) Token: 0x06000E8F RID: 3727 RVA: 0x00022A9C File Offset: 0x00020C9C
		public string Manager
		{
			get
			{
				return this.managerField;
			}
			set
			{
				this.managerField = value;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x00022AA5 File Offset: 0x00020CA5
		// (set) Token: 0x06000E91 RID: 3729 RVA: 0x00022AAD File Offset: 0x00020CAD
		public string Mileage
		{
			get
			{
				return this.mileageField;
			}
			set
			{
				this.mileageField = value;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x00022AB6 File Offset: 0x00020CB6
		// (set) Token: 0x06000E93 RID: 3731 RVA: 0x00022ABE File Offset: 0x00020CBE
		public string OfficeLocation
		{
			get
			{
				return this.officeLocationField;
			}
			set
			{
				this.officeLocationField = value;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x00022AC7 File Offset: 0x00020CC7
		// (set) Token: 0x06000E95 RID: 3733 RVA: 0x00022ACF File Offset: 0x00020CCF
		public PhysicalAddressIndexType PostalAddressIndex
		{
			get
			{
				return this.postalAddressIndexField;
			}
			set
			{
				this.postalAddressIndexField = value;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x00022AD8 File Offset: 0x00020CD8
		// (set) Token: 0x06000E97 RID: 3735 RVA: 0x00022AE0 File Offset: 0x00020CE0
		[XmlIgnore]
		public bool PostalAddressIndexSpecified
		{
			get
			{
				return this.postalAddressIndexFieldSpecified;
			}
			set
			{
				this.postalAddressIndexFieldSpecified = value;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x00022AE9 File Offset: 0x00020CE9
		// (set) Token: 0x06000E99 RID: 3737 RVA: 0x00022AF1 File Offset: 0x00020CF1
		public string Profession
		{
			get
			{
				return this.professionField;
			}
			set
			{
				this.professionField = value;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x00022AFA File Offset: 0x00020CFA
		// (set) Token: 0x06000E9B RID: 3739 RVA: 0x00022B02 File Offset: 0x00020D02
		public string SpouseName
		{
			get
			{
				return this.spouseNameField;
			}
			set
			{
				this.spouseNameField = value;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x00022B0B File Offset: 0x00020D0B
		// (set) Token: 0x06000E9D RID: 3741 RVA: 0x00022B13 File Offset: 0x00020D13
		public string Surname
		{
			get
			{
				return this.surnameField;
			}
			set
			{
				this.surnameField = value;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00022B1C File Offset: 0x00020D1C
		// (set) Token: 0x06000E9F RID: 3743 RVA: 0x00022B24 File Offset: 0x00020D24
		public DateTime WeddingAnniversary
		{
			get
			{
				return this.weddingAnniversaryField;
			}
			set
			{
				this.weddingAnniversaryField = value;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x00022B2D File Offset: 0x00020D2D
		// (set) Token: 0x06000EA1 RID: 3745 RVA: 0x00022B35 File Offset: 0x00020D35
		[XmlIgnore]
		public bool WeddingAnniversarySpecified
		{
			get
			{
				return this.weddingAnniversaryFieldSpecified;
			}
			set
			{
				this.weddingAnniversaryFieldSpecified = value;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x00022B3E File Offset: 0x00020D3E
		// (set) Token: 0x06000EA3 RID: 3747 RVA: 0x00022B46 File Offset: 0x00020D46
		public bool HasPicture
		{
			get
			{
				return this.hasPictureField;
			}
			set
			{
				this.hasPictureField = value;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00022B4F File Offset: 0x00020D4F
		// (set) Token: 0x06000EA5 RID: 3749 RVA: 0x00022B57 File Offset: 0x00020D57
		[XmlIgnore]
		public bool HasPictureSpecified
		{
			get
			{
				return this.hasPictureFieldSpecified;
			}
			set
			{
				this.hasPictureFieldSpecified = value;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x00022B60 File Offset: 0x00020D60
		// (set) Token: 0x06000EA7 RID: 3751 RVA: 0x00022B68 File Offset: 0x00020D68
		public string PhoneticFullName
		{
			get
			{
				return this.phoneticFullNameField;
			}
			set
			{
				this.phoneticFullNameField = value;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00022B71 File Offset: 0x00020D71
		// (set) Token: 0x06000EA9 RID: 3753 RVA: 0x00022B79 File Offset: 0x00020D79
		public string PhoneticFirstName
		{
			get
			{
				return this.phoneticFirstNameField;
			}
			set
			{
				this.phoneticFirstNameField = value;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x00022B82 File Offset: 0x00020D82
		// (set) Token: 0x06000EAB RID: 3755 RVA: 0x00022B8A File Offset: 0x00020D8A
		public string PhoneticLastName
		{
			get
			{
				return this.phoneticLastNameField;
			}
			set
			{
				this.phoneticLastNameField = value;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x00022B93 File Offset: 0x00020D93
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x00022B9B File Offset: 0x00020D9B
		public string Alias
		{
			get
			{
				return this.aliasField;
			}
			set
			{
				this.aliasField = value;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x00022BA4 File Offset: 0x00020DA4
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x00022BAC File Offset: 0x00020DAC
		public string Notes
		{
			get
			{
				return this.notesField;
			}
			set
			{
				this.notesField = value;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x00022BB5 File Offset: 0x00020DB5
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x00022BBD File Offset: 0x00020DBD
		[XmlElement(DataType = "base64Binary")]
		public byte[] Photo
		{
			get
			{
				return this.photoField;
			}
			set
			{
				this.photoField = value;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x00022BC6 File Offset: 0x00020DC6
		// (set) Token: 0x06000EB3 RID: 3763 RVA: 0x00022BCE File Offset: 0x00020DCE
		[XmlArrayItem("Base64Binary", DataType = "base64Binary", IsNullable = false)]
		public byte[][] UserSMIMECertificate
		{
			get
			{
				return this.userSMIMECertificateField;
			}
			set
			{
				this.userSMIMECertificateField = value;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x00022BD7 File Offset: 0x00020DD7
		// (set) Token: 0x06000EB5 RID: 3765 RVA: 0x00022BDF File Offset: 0x00020DDF
		[XmlArrayItem("Base64Binary", DataType = "base64Binary", IsNullable = false)]
		public byte[][] MSExchangeCertificate
		{
			get
			{
				return this.mSExchangeCertificateField;
			}
			set
			{
				this.mSExchangeCertificateField = value;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x00022BE8 File Offset: 0x00020DE8
		// (set) Token: 0x06000EB7 RID: 3767 RVA: 0x00022BF0 File Offset: 0x00020DF0
		public string DirectoryId
		{
			get
			{
				return this.directoryIdField;
			}
			set
			{
				this.directoryIdField = value;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x00022BF9 File Offset: 0x00020DF9
		// (set) Token: 0x06000EB9 RID: 3769 RVA: 0x00022C01 File Offset: 0x00020E01
		public SingleRecipientType ManagerMailbox
		{
			get
			{
				return this.managerMailboxField;
			}
			set
			{
				this.managerMailboxField = value;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x00022C0A File Offset: 0x00020E0A
		// (set) Token: 0x06000EBB RID: 3771 RVA: 0x00022C12 File Offset: 0x00020E12
		[XmlArrayItem("Mailbox", IsNullable = false)]
		public EmailAddressType[] DirectReports
		{
			get
			{
				return this.directReportsField;
			}
			set
			{
				this.directReportsField = value;
			}
		}

		// Token: 0x040009EC RID: 2540
		private string fileAsField;

		// Token: 0x040009ED RID: 2541
		private FileAsMappingType fileAsMappingField;

		// Token: 0x040009EE RID: 2542
		private bool fileAsMappingFieldSpecified;

		// Token: 0x040009EF RID: 2543
		private string displayNameField;

		// Token: 0x040009F0 RID: 2544
		private string givenNameField;

		// Token: 0x040009F1 RID: 2545
		private string initialsField;

		// Token: 0x040009F2 RID: 2546
		private string middleNameField;

		// Token: 0x040009F3 RID: 2547
		private string nicknameField;

		// Token: 0x040009F4 RID: 2548
		private CompleteNameType completeNameField;

		// Token: 0x040009F5 RID: 2549
		private string companyNameField;

		// Token: 0x040009F6 RID: 2550
		private EmailAddressDictionaryEntryType[] emailAddressesField;

		// Token: 0x040009F7 RID: 2551
		private PhysicalAddressDictionaryEntryType[] physicalAddressesField;

		// Token: 0x040009F8 RID: 2552
		private PhoneNumberDictionaryEntryType[] phoneNumbersField;

		// Token: 0x040009F9 RID: 2553
		private string assistantNameField;

		// Token: 0x040009FA RID: 2554
		private DateTime birthdayField;

		// Token: 0x040009FB RID: 2555
		private bool birthdayFieldSpecified;

		// Token: 0x040009FC RID: 2556
		private string businessHomePageField;

		// Token: 0x040009FD RID: 2557
		private string[] childrenField;

		// Token: 0x040009FE RID: 2558
		private string[] companiesField;

		// Token: 0x040009FF RID: 2559
		private ContactSourceType contactSourceField;

		// Token: 0x04000A00 RID: 2560
		private bool contactSourceFieldSpecified;

		// Token: 0x04000A01 RID: 2561
		private string departmentField;

		// Token: 0x04000A02 RID: 2562
		private string generationField;

		// Token: 0x04000A03 RID: 2563
		private ImAddressDictionaryEntryType[] imAddressesField;

		// Token: 0x04000A04 RID: 2564
		private string jobTitleField;

		// Token: 0x04000A05 RID: 2565
		private string managerField;

		// Token: 0x04000A06 RID: 2566
		private string mileageField;

		// Token: 0x04000A07 RID: 2567
		private string officeLocationField;

		// Token: 0x04000A08 RID: 2568
		private PhysicalAddressIndexType postalAddressIndexField;

		// Token: 0x04000A09 RID: 2569
		private bool postalAddressIndexFieldSpecified;

		// Token: 0x04000A0A RID: 2570
		private string professionField;

		// Token: 0x04000A0B RID: 2571
		private string spouseNameField;

		// Token: 0x04000A0C RID: 2572
		private string surnameField;

		// Token: 0x04000A0D RID: 2573
		private DateTime weddingAnniversaryField;

		// Token: 0x04000A0E RID: 2574
		private bool weddingAnniversaryFieldSpecified;

		// Token: 0x04000A0F RID: 2575
		private bool hasPictureField;

		// Token: 0x04000A10 RID: 2576
		private bool hasPictureFieldSpecified;

		// Token: 0x04000A11 RID: 2577
		private string phoneticFullNameField;

		// Token: 0x04000A12 RID: 2578
		private string phoneticFirstNameField;

		// Token: 0x04000A13 RID: 2579
		private string phoneticLastNameField;

		// Token: 0x04000A14 RID: 2580
		private string aliasField;

		// Token: 0x04000A15 RID: 2581
		private string notesField;

		// Token: 0x04000A16 RID: 2582
		private byte[] photoField;

		// Token: 0x04000A17 RID: 2583
		private byte[][] userSMIMECertificateField;

		// Token: 0x04000A18 RID: 2584
		private byte[][] mSExchangeCertificateField;

		// Token: 0x04000A19 RID: 2585
		private string directoryIdField;

		// Token: 0x04000A1A RID: 2586
		private SingleRecipientType managerMailboxField;

		// Token: 0x04000A1B RID: 2587
		private EmailAddressType[] directReportsField;
	}
}
