using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common.Properties.XSO;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema
{
	// Token: 0x02000230 RID: 560
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ContactPropertyManager : XSOPropertyManager
	{
		// Token: 0x0600141D RID: 5149 RVA: 0x0004979C File Offset: 0x0004799C
		private ContactPropertyManager()
		{
			this.birthDate = new XSOProperty<ExDateTime?>(this, ContactSchema.Birthday);
			this.birthDateLocal = new XSOProperty<ExDateTime?>(this, ContactSchema.BirthdayLocal);
			this.businessAddressCity = new XSOProperty<string>(this, ContactSchema.WorkAddressCity);
			this.businessAddressCountry = new XSOProperty<string>(this, ContactSchema.WorkAddressCountry);
			this.businessAddressPostalCode = new XSOProperty<string>(this, ContactSchema.WorkAddressPostalCode);
			this.businessAddressState = new XSOProperty<string>(this, ContactSchema.WorkAddressState);
			this.businessAddressStreet = new XSOProperty<string>(this, ContactSchema.WorkAddressStreet);
			this.businessFaxNumber = new XSOProperty<string>(this, ContactSchema.WorkFax);
			this.businessTelephoneNumber = new XSOProperty<string>(this, ContactSchema.BusinessPhoneNumber);
			this.companyName = new XSOProperty<string>(this, ContactSchema.CompanyName);
			this.displayName = new XSOProperty<string>(this, ContactSchema.FullName);
			this.email1Address = new EmailProperty(this, ContactSchema.Email1, ContactSchema.Email1EmailAddress);
			this.email2Address = new EmailProperty(this, ContactSchema.Email2, ContactSchema.Email2EmailAddress);
			this.email3Address = new EmailProperty(this, ContactSchema.Email3, ContactSchema.Email3EmailAddress);
			this.fileAs = new FileAsProperty(this);
			this.firstName = new XSOProperty<string>(this, ContactSchema.GivenName);
			this.hobbies = new XSOProperty<string>(this, ContactSchema.Hobbies);
			this.homeAddressCity = new XSOProperty<string>(this, ContactSchema.HomeCity);
			this.homeAddressCountry = new XSOProperty<string>(this, ContactSchema.HomeCountry);
			this.homeAddressPostalCode = new XSOProperty<string>(this, ContactSchema.HomePostalCode);
			this.homeAddressState = new XSOProperty<string>(this, ContactSchema.HomeState);
			this.homeAddressStreet = new XSOProperty<string>(this, ContactSchema.HomeStreet);
			this.homeTelephoneNumber = new XSOProperty<string>(this, ContactSchema.HomePhone);
			this.iMAddress = new XSOProperty<string>(this, ContactSchema.IMAddress);
			this.jobTile = new XSOProperty<string>(this, ContactSchema.Title);
			this.lastModifiedTime = new XSOProperty<ExDateTime?>(this, StoreObjectSchema.LastModifiedTime);
			this.lastName = new XSOProperty<string>(this, ContactSchema.Surname);
			this.location = new XSOProperty<string>(this, ContactSchema.Location);
			this.middleName = new XSOProperty<string>(this, ContactSchema.MiddleName);
			this.mobileTelephoneNumber = new XSOProperty<string>(this, ContactSchema.MobilePhone);
			this.oscContactSources = new XSOProperty<byte[]>(this, ContactSchema.OscContactSourcesForContact);
			this.otherTelephoneNumber = new XSOProperty<string>(this, ContactSchema.OtherTelephone);
			this.partnerNetworkContactType = new XSOProperty<string>(this, ContactSchema.PartnerNetworkContactType);
			this.partnerNetworkId = new XSOProperty<string>(this, ContactSchema.PartnerNetworkId);
			this.partnerNetworkProfilePhotoUrl = new XSOProperty<string>(this, ContactSchema.PartnerNetworkProfilePhotoUrl);
			this.partnerNetworkThumbnailPhotoUrl = new XSOProperty<string>(this, ContactSchema.PartnerNetworkThumbnailPhotoUrl);
			this.partnerNetworkUserId = new XSOProperty<string>(this, ContactSchema.PartnerNetworkUserId);
			this.peopleConnectionCreationTime = new XSOProperty<ExDateTime?>(this, ContactSchema.PeopleConnectionCreationTime);
			this.protectedEmailAddress = new XSOProperty<string>(this, ContactProtectedPropertiesSchema.ProtectedEmailAddress);
			this.protectedPhoneNumber = new XSOProperty<string>(this, ContactProtectedPropertiesSchema.ProtectedPhoneNumber);
			this.schools = new XSOProperty<string>(this, ContactSchema.Schools);
			this.webPage = new XSOProperty<string>(this, ContactSchema.BusinessHomePage);
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x00049A83 File Offset: 0x00047C83
		internal static ContactPropertyManager Instance
		{
			get
			{
				return ContactPropertyManager.instance;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x00049A8A File Offset: 0x00047C8A
		internal XSOProperty<ExDateTime?> BirthDate
		{
			get
			{
				return this.birthDate;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x00049A92 File Offset: 0x00047C92
		internal XSOProperty<ExDateTime?> BirthDateLocal
		{
			get
			{
				return this.birthDateLocal;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x00049A9A File Offset: 0x00047C9A
		internal XSOProperty<string> BusinessAddressCity
		{
			get
			{
				return this.businessAddressCity;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x00049AA2 File Offset: 0x00047CA2
		internal XSOProperty<string> BusinessAddressCountry
		{
			get
			{
				return this.businessAddressCountry;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x00049AAA File Offset: 0x00047CAA
		internal XSOProperty<string> BusinessAddressPostalCode
		{
			get
			{
				return this.businessAddressPostalCode;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x00049AB2 File Offset: 0x00047CB2
		internal XSOProperty<string> BusinessAddressState
		{
			get
			{
				return this.businessAddressState;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x00049ABA File Offset: 0x00047CBA
		internal XSOProperty<string> BusinessAddressStreet
		{
			get
			{
				return this.businessAddressStreet;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x00049AC2 File Offset: 0x00047CC2
		internal XSOProperty<string> BusinessFaxNumber
		{
			get
			{
				return this.businessFaxNumber;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x00049ACA File Offset: 0x00047CCA
		internal XSOProperty<string> BusinessTelephoneNumber
		{
			get
			{
				return this.businessTelephoneNumber;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x00049AD2 File Offset: 0x00047CD2
		internal XSOProperty<string> CompanyName
		{
			get
			{
				return this.companyName;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x00049ADA File Offset: 0x00047CDA
		internal XSOProperty<string> DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x00049AE2 File Offset: 0x00047CE2
		internal EmailProperty Email1Address
		{
			get
			{
				return this.email1Address;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x00049AEA File Offset: 0x00047CEA
		internal EmailProperty Email2Address
		{
			get
			{
				return this.email2Address;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x00049AF2 File Offset: 0x00047CF2
		internal EmailProperty Email3Address
		{
			get
			{
				return this.email3Address;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x00049AFA File Offset: 0x00047CFA
		internal FileAsProperty FileAs
		{
			get
			{
				return this.fileAs;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x00049B02 File Offset: 0x00047D02
		internal XSOProperty<string> FirstName
		{
			get
			{
				return this.firstName;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x00049B0A File Offset: 0x00047D0A
		internal XSOProperty<string> Hobbies
		{
			get
			{
				return this.hobbies;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x00049B12 File Offset: 0x00047D12
		internal XSOProperty<string> HomeAddressCity
		{
			get
			{
				return this.homeAddressCity;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x00049B1A File Offset: 0x00047D1A
		internal XSOProperty<string> HomeAddressCountry
		{
			get
			{
				return this.homeAddressCountry;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x00049B22 File Offset: 0x00047D22
		internal XSOProperty<string> HomeAddressPostalCode
		{
			get
			{
				return this.homeAddressPostalCode;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x00049B2A File Offset: 0x00047D2A
		internal XSOProperty<string> HomeAddressState
		{
			get
			{
				return this.homeAddressState;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x00049B32 File Offset: 0x00047D32
		internal XSOProperty<string> HomeAddressStreet
		{
			get
			{
				return this.homeAddressStreet;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x00049B3A File Offset: 0x00047D3A
		internal XSOProperty<string> HomeTelephoneNumber
		{
			get
			{
				return this.homeTelephoneNumber;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x00049B42 File Offset: 0x00047D42
		internal XSOProperty<string> IMAddress
		{
			get
			{
				return this.iMAddress;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x00049B4A File Offset: 0x00047D4A
		internal XSOProperty<string> JobTile
		{
			get
			{
				return this.jobTile;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x00049B52 File Offset: 0x00047D52
		internal XSOProperty<string> LastName
		{
			get
			{
				return this.lastName;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x00049B5A File Offset: 0x00047D5A
		internal XSOProperty<ExDateTime?> LastModifiedTime
		{
			get
			{
				return this.lastModifiedTime;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x00049B62 File Offset: 0x00047D62
		internal XSOProperty<string> Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x00049B6A File Offset: 0x00047D6A
		internal XSOProperty<string> MiddleName
		{
			get
			{
				return this.middleName;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00049B72 File Offset: 0x00047D72
		internal XSOProperty<string> MobileTelephoneNumber
		{
			get
			{
				return this.mobileTelephoneNumber;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x00049B7A File Offset: 0x00047D7A
		internal XSOProperty<string> OtherTelephoneNumber
		{
			get
			{
				return this.otherTelephoneNumber;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x00049B82 File Offset: 0x00047D82
		internal XSOProperty<byte[]> OscContactSources
		{
			get
			{
				return this.oscContactSources;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x00049B8A File Offset: 0x00047D8A
		internal XSOProperty<string> PartnerNetworkContactType
		{
			get
			{
				return this.partnerNetworkContactType;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x00049B92 File Offset: 0x00047D92
		internal XSOProperty<string> PartnerNetworkId
		{
			get
			{
				return this.partnerNetworkId;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x00049B9A File Offset: 0x00047D9A
		internal XSOProperty<string> PartnerNetworkProfilePhotoUrl
		{
			get
			{
				return this.partnerNetworkProfilePhotoUrl;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00049BA2 File Offset: 0x00047DA2
		internal XSOProperty<string> PartnerNetworkThumbnailPhotoUrl
		{
			get
			{
				return this.partnerNetworkThumbnailPhotoUrl;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x00049BAA File Offset: 0x00047DAA
		internal XSOProperty<string> PartnerNetworkUserId
		{
			get
			{
				return this.partnerNetworkUserId;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00049BB2 File Offset: 0x00047DB2
		internal XSOProperty<ExDateTime?> PeopleConnectionCreationTime
		{
			get
			{
				return this.peopleConnectionCreationTime;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x00049BBA File Offset: 0x00047DBA
		internal XSOProperty<string> ProtectedEmailAddress
		{
			get
			{
				return this.protectedEmailAddress;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x00049BC2 File Offset: 0x00047DC2
		internal XSOProperty<string> ProtectedPhoneNumber
		{
			get
			{
				return this.protectedPhoneNumber;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x00049BCA File Offset: 0x00047DCA
		internal XSOProperty<string> Schools
		{
			get
			{
				return this.schools;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x00049BD2 File Offset: 0x00047DD2
		internal XSOProperty<string> WebPage
		{
			get
			{
				return this.webPage;
			}
		}

		// Token: 0x04000A8A RID: 2698
		private static readonly ContactPropertyManager instance = new ContactPropertyManager();

		// Token: 0x04000A8B RID: 2699
		private readonly XSOProperty<ExDateTime?> birthDate;

		// Token: 0x04000A8C RID: 2700
		private readonly XSOProperty<ExDateTime?> birthDateLocal;

		// Token: 0x04000A8D RID: 2701
		private readonly XSOProperty<string> businessAddressCity;

		// Token: 0x04000A8E RID: 2702
		private readonly XSOProperty<string> businessAddressCountry;

		// Token: 0x04000A8F RID: 2703
		private readonly XSOProperty<string> businessAddressPostalCode;

		// Token: 0x04000A90 RID: 2704
		private readonly XSOProperty<string> businessAddressState;

		// Token: 0x04000A91 RID: 2705
		private readonly XSOProperty<string> businessAddressStreet;

		// Token: 0x04000A92 RID: 2706
		private readonly XSOProperty<string> businessFaxNumber;

		// Token: 0x04000A93 RID: 2707
		private readonly XSOProperty<string> businessTelephoneNumber;

		// Token: 0x04000A94 RID: 2708
		private readonly XSOProperty<string> companyName;

		// Token: 0x04000A95 RID: 2709
		private readonly XSOProperty<string> displayName;

		// Token: 0x04000A96 RID: 2710
		private readonly EmailProperty email1Address;

		// Token: 0x04000A97 RID: 2711
		private readonly EmailProperty email2Address;

		// Token: 0x04000A98 RID: 2712
		private readonly EmailProperty email3Address;

		// Token: 0x04000A99 RID: 2713
		private readonly FileAsProperty fileAs;

		// Token: 0x04000A9A RID: 2714
		private readonly XSOProperty<string> firstName;

		// Token: 0x04000A9B RID: 2715
		private readonly XSOProperty<string> homeAddressCity;

		// Token: 0x04000A9C RID: 2716
		private readonly XSOProperty<string> hobbies;

		// Token: 0x04000A9D RID: 2717
		private readonly XSOProperty<string> homeAddressCountry;

		// Token: 0x04000A9E RID: 2718
		private readonly XSOProperty<string> homeAddressPostalCode;

		// Token: 0x04000A9F RID: 2719
		private readonly XSOProperty<string> homeAddressState;

		// Token: 0x04000AA0 RID: 2720
		private readonly XSOProperty<string> homeAddressStreet;

		// Token: 0x04000AA1 RID: 2721
		private readonly XSOProperty<string> homeTelephoneNumber;

		// Token: 0x04000AA2 RID: 2722
		private readonly XSOProperty<string> iMAddress;

		// Token: 0x04000AA3 RID: 2723
		private readonly XSOProperty<string> jobTile;

		// Token: 0x04000AA4 RID: 2724
		private readonly XSOProperty<string> lastName;

		// Token: 0x04000AA5 RID: 2725
		private readonly XSOProperty<ExDateTime?> lastModifiedTime;

		// Token: 0x04000AA6 RID: 2726
		private readonly XSOProperty<string> location;

		// Token: 0x04000AA7 RID: 2727
		private readonly XSOProperty<string> middleName;

		// Token: 0x04000AA8 RID: 2728
		private readonly XSOProperty<string> mobileTelephoneNumber;

		// Token: 0x04000AA9 RID: 2729
		private readonly XSOProperty<byte[]> oscContactSources;

		// Token: 0x04000AAA RID: 2730
		private readonly XSOProperty<string> otherTelephoneNumber;

		// Token: 0x04000AAB RID: 2731
		private readonly XSOProperty<string> partnerNetworkContactType;

		// Token: 0x04000AAC RID: 2732
		private readonly XSOProperty<string> partnerNetworkId;

		// Token: 0x04000AAD RID: 2733
		private readonly XSOProperty<string> partnerNetworkProfilePhotoUrl;

		// Token: 0x04000AAE RID: 2734
		private readonly XSOProperty<string> partnerNetworkThumbnailPhotoUrl;

		// Token: 0x04000AAF RID: 2735
		private readonly XSOProperty<string> partnerNetworkUserId;

		// Token: 0x04000AB0 RID: 2736
		private readonly XSOProperty<ExDateTime?> peopleConnectionCreationTime;

		// Token: 0x04000AB1 RID: 2737
		private readonly XSOProperty<string> protectedEmailAddress;

		// Token: 0x04000AB2 RID: 2738
		private readonly XSOProperty<string> protectedPhoneNumber;

		// Token: 0x04000AB3 RID: 2739
		private readonly XSOProperty<string> schools;

		// Token: 0x04000AB4 RID: 2740
		private readonly XSOProperty<string> webPage;
	}
}
