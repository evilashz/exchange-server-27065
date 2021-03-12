using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200022E RID: 558
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ContactItemType : ItemType
	{
		// Token: 0x04000E3E RID: 3646
		public string FileAs;

		// Token: 0x04000E3F RID: 3647
		public FileAsMappingType FileAsMapping;

		// Token: 0x04000E40 RID: 3648
		[XmlIgnore]
		public bool FileAsMappingSpecified;

		// Token: 0x04000E41 RID: 3649
		public string DisplayName;

		// Token: 0x04000E42 RID: 3650
		public string GivenName;

		// Token: 0x04000E43 RID: 3651
		public string Initials;

		// Token: 0x04000E44 RID: 3652
		public string MiddleName;

		// Token: 0x04000E45 RID: 3653
		public string Nickname;

		// Token: 0x04000E46 RID: 3654
		public CompleteNameType CompleteName;

		// Token: 0x04000E47 RID: 3655
		public string CompanyName;

		// Token: 0x04000E48 RID: 3656
		[XmlArrayItem("Entry", IsNullable = false)]
		public EmailAddressDictionaryEntryType[] EmailAddresses;

		// Token: 0x04000E49 RID: 3657
		[XmlArrayItem("Entry", IsNullable = false)]
		public PhysicalAddressDictionaryEntryType[] PhysicalAddresses;

		// Token: 0x04000E4A RID: 3658
		[XmlArrayItem("Entry", IsNullable = false)]
		public PhoneNumberDictionaryEntryType[] PhoneNumbers;

		// Token: 0x04000E4B RID: 3659
		public string AssistantName;

		// Token: 0x04000E4C RID: 3660
		public DateTime Birthday;

		// Token: 0x04000E4D RID: 3661
		[XmlIgnore]
		public bool BirthdaySpecified;

		// Token: 0x04000E4E RID: 3662
		[XmlElement(DataType = "anyURI")]
		public string BusinessHomePage;

		// Token: 0x04000E4F RID: 3663
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Children;

		// Token: 0x04000E50 RID: 3664
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Companies;

		// Token: 0x04000E51 RID: 3665
		public ContactSourceType ContactSource;

		// Token: 0x04000E52 RID: 3666
		[XmlIgnore]
		public bool ContactSourceSpecified;

		// Token: 0x04000E53 RID: 3667
		public string Department;

		// Token: 0x04000E54 RID: 3668
		public string Generation;

		// Token: 0x04000E55 RID: 3669
		[XmlArrayItem("Entry", IsNullable = false)]
		public ImAddressDictionaryEntryType[] ImAddresses;

		// Token: 0x04000E56 RID: 3670
		public string JobTitle;

		// Token: 0x04000E57 RID: 3671
		public string Manager;

		// Token: 0x04000E58 RID: 3672
		public string Mileage;

		// Token: 0x04000E59 RID: 3673
		public string OfficeLocation;

		// Token: 0x04000E5A RID: 3674
		public PhysicalAddressIndexType PostalAddressIndex;

		// Token: 0x04000E5B RID: 3675
		[XmlIgnore]
		public bool PostalAddressIndexSpecified;

		// Token: 0x04000E5C RID: 3676
		public string Profession;

		// Token: 0x04000E5D RID: 3677
		public string SpouseName;

		// Token: 0x04000E5E RID: 3678
		public string Surname;

		// Token: 0x04000E5F RID: 3679
		public DateTime WeddingAnniversary;

		// Token: 0x04000E60 RID: 3680
		[XmlIgnore]
		public bool WeddingAnniversarySpecified;

		// Token: 0x04000E61 RID: 3681
		public bool HasPicture;

		// Token: 0x04000E62 RID: 3682
		[XmlIgnore]
		public bool HasPictureSpecified;

		// Token: 0x04000E63 RID: 3683
		public string PhoneticFullName;

		// Token: 0x04000E64 RID: 3684
		public string PhoneticFirstName;

		// Token: 0x04000E65 RID: 3685
		public string PhoneticLastName;

		// Token: 0x04000E66 RID: 3686
		public string Alias;

		// Token: 0x04000E67 RID: 3687
		public string Notes;

		// Token: 0x04000E68 RID: 3688
		[XmlElement(DataType = "base64Binary")]
		public byte[] Photo;

		// Token: 0x04000E69 RID: 3689
		[XmlArrayItem("Base64Binary", DataType = "base64Binary", IsNullable = false)]
		public byte[][] UserSMIMECertificate;

		// Token: 0x04000E6A RID: 3690
		[XmlArrayItem("Base64Binary", DataType = "base64Binary", IsNullable = false)]
		public byte[][] MSExchangeCertificate;

		// Token: 0x04000E6B RID: 3691
		public string DirectoryId;

		// Token: 0x04000E6C RID: 3692
		public SingleRecipientType ManagerMailbox;

		// Token: 0x04000E6D RID: 3693
		[XmlArrayItem("Mailbox", IsNullable = false)]
		public EmailAddressType[] DirectReports;
	}
}
