using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C49 RID: 3145
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ContactSchema : ContactBaseSchema
	{
		// Token: 0x17001E06 RID: 7686
		// (get) Token: 0x06006F38 RID: 28472 RVA: 0x001DE662 File Offset: 0x001DC862
		public new static ContactSchema Instance
		{
			get
			{
				if (ContactSchema.instance == null)
				{
					ContactSchema.instance = new ContactSchema();
				}
				return ContactSchema.instance;
			}
		}

		// Token: 0x17001E07 RID: 7687
		// (get) Token: 0x06006F39 RID: 28473 RVA: 0x001DE67A File Offset: 0x001DC87A
		protected override ICollection<PropertyRule> PropertyRules
		{
			get
			{
				if (this.propertyRulesCache == null)
				{
					this.propertyRulesCache = base.PropertyRules.Concat(ContactSchema.ContactSchemaPropertyRules);
				}
				return this.propertyRulesCache;
			}
		}

		// Token: 0x06006F3A RID: 28474 RVA: 0x001DE6A0 File Offset: 0x001DC8A0
		internal override void CoreObjectUpdate(CoreItem coreItem, CoreItemOperation operation)
		{
			Contact.CoreObjectUpdateFileAs(coreItem);
			base.CoreObjectUpdate(coreItem, operation);
			Contact.CoreObjectUpdatePhysicalAddresses(coreItem);
		}

		// Token: 0x06006F3C RID: 28476 RVA: 0x001DE6C8 File Offset: 0x001DC8C8
		// Note: this type is marked as 'beforefieldinit'.
		static ContactSchema()
		{
			PropertyRule[] array = new PropertyRule[4];
			array[0] = PropertyRuleLibrary.PersonIdRule;
			array[1] = PropertyRuleLibrary.EnhancedLocation;
			array[2] = new SequenceCompositePropertyRule(string.Empty, delegate(ILocationIdentifierSetter lidSetter)
			{
				lidSetter.SetLocationIdentifier(34080U, LastChangeAction.SequenceCompositePropertyRuleApplied);
			}, new PropertyRule[]
			{
				PropertyRuleLibrary.EmailAddressUpdateRule,
				PropertyRuleLibrary.ContactDisplayNameRule
			});
			array[3] = PropertyRuleLibrary.OscContactSourcesForContactRule;
			ContactSchema.ContactSchemaPropertyRules = array;
			ContactSchema.instance = null;
		}

		// Token: 0x04004251 RID: 16977
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition AssistantName = InternalSchema.AssistantName;

		// Token: 0x04004252 RID: 16978
		[LegalTracking]
		public static readonly StorePropertyDefinition AssistantPhoneNumber = InternalSchema.AssistantPhoneNumber;

		// Token: 0x04004253 RID: 16979
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition BillingInformation = InternalSchema.BillingInformation;

		// Token: 0x04004254 RID: 16980
		[LegalTracking]
		public static readonly StorePropertyDefinition Birthday = InternalSchema.Birthday;

		// Token: 0x04004255 RID: 16981
		[LegalTracking]
		public static readonly StorePropertyDefinition BirthdayLocal = InternalSchema.BirthdayLocal;

		// Token: 0x04004256 RID: 16982
		[LegalTracking]
		public static readonly StorePropertyDefinition NotInBirthdayCalendar = InternalSchema.NotInBirthdayCalendar;

		// Token: 0x04004257 RID: 16983
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition BusinessHomePage = InternalSchema.BusinessHomePage;

		// Token: 0x04004258 RID: 16984
		[LegalTracking]
		public static readonly StorePropertyDefinition LegacyWebPage = InternalSchema.LegacyWebPage;

		// Token: 0x04004259 RID: 16985
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition BusinessPhoneNumber = InternalSchema.BusinessPhoneNumber;

		// Token: 0x0400425A RID: 16986
		[LegalTracking]
		public static readonly StorePropertyDefinition BusinessPhoneNumber2 = InternalSchema.BusinessPhoneNumber2;

		// Token: 0x0400425B RID: 16987
		[LegalTracking]
		public static readonly StorePropertyDefinition CallbackPhone = InternalSchema.CallbackPhone;

		// Token: 0x0400425C RID: 16988
		[LegalTracking]
		public static readonly StorePropertyDefinition CarPhone = InternalSchema.CarPhone;

		// Token: 0x0400425D RID: 16989
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition Children = InternalSchema.Children;

		// Token: 0x0400425E RID: 16990
		[LegalTracking]
		public static readonly StorePropertyDefinition Companies = InternalSchema.Companies;

		// Token: 0x0400425F RID: 16991
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition CompanyName = InternalSchema.CompanyName;

		// Token: 0x04004260 RID: 16992
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition Department = InternalSchema.Department;

		// Token: 0x04004261 RID: 16993
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition DisplayNamePrefix = InternalSchema.DisplayNamePrefix;

		// Token: 0x04004262 RID: 16994
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition Email1 = InternalSchema.ContactEmail1;

		// Token: 0x04004263 RID: 16995
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition Email2 = InternalSchema.ContactEmail2;

		// Token: 0x04004264 RID: 16996
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition Email3 = InternalSchema.ContactEmail3;

		// Token: 0x04004265 RID: 16997
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition Email1AddrType = InternalSchema.Email1AddrType;

		// Token: 0x04004266 RID: 16998
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition Email1DisplayName = InternalSchema.Email1DisplayName;

		// Token: 0x04004267 RID: 16999
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition Email1EmailAddress = InternalSchema.Email1EmailAddress;

		// Token: 0x04004268 RID: 17000
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition Email1OriginalDisplayName = InternalSchema.Email1OriginalDisplayName;

		// Token: 0x04004269 RID: 17001
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition Email1OriginalEntryID = InternalSchema.Email1OriginalEntryID;

		// Token: 0x0400426A RID: 17002
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition Email2AddrType = InternalSchema.Email2AddrType;

		// Token: 0x0400426B RID: 17003
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition Email2DisplayName = InternalSchema.Email2DisplayName;

		// Token: 0x0400426C RID: 17004
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition Email2EmailAddress = InternalSchema.Email2EmailAddress;

		// Token: 0x0400426D RID: 17005
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition Email2OriginalDisplayName = InternalSchema.Email2OriginalDisplayName;

		// Token: 0x0400426E RID: 17006
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition Email2OriginalEntryID = InternalSchema.Email2OriginalEntryID;

		// Token: 0x0400426F RID: 17007
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition Email3AddrType = InternalSchema.Email3AddrType;

		// Token: 0x04004270 RID: 17008
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition Email3DisplayName = InternalSchema.Email3DisplayName;

		// Token: 0x04004271 RID: 17009
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition Email3EmailAddress = InternalSchema.Email3EmailAddress;

		// Token: 0x04004272 RID: 17010
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition Email3OriginalDisplayName = InternalSchema.Email3OriginalDisplayName;

		// Token: 0x04004273 RID: 17011
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition Email3OriginalEntryID = InternalSchema.Email3OriginalEntryID;

		// Token: 0x04004274 RID: 17012
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition ContactBusinessFax = InternalSchema.ContactBusinessFax;

		// Token: 0x04004275 RID: 17013
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition ContactHomeFax = InternalSchema.ContactHomeFax;

		// Token: 0x04004276 RID: 17014
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition ContactOtherFax = InternalSchema.ContactOtherFax;

		// Token: 0x04004277 RID: 17015
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition FileAsId = InternalSchema.FileAsId;

		// Token: 0x04004278 RID: 17016
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition GALContactType = InternalSchema.GALContactType;

		// Token: 0x04004279 RID: 17017
		[DetectCodepage]
		public static readonly StorePropertyDefinition GALUMDialplanId = InternalSchema.GALUMDialplanId;

		// Token: 0x0400427A RID: 17018
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition Generation = InternalSchema.Generation;

		// Token: 0x0400427B RID: 17019
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition GivenName = InternalSchema.GivenName;

		// Token: 0x0400427C RID: 17020
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition HasPicture = InternalSchema.HasPicture;

		// Token: 0x0400427D RID: 17021
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition HomePostOfficeBox = InternalSchema.HomePostOfficeBox;

		// Token: 0x0400427E RID: 17022
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition HomeCity = InternalSchema.HomeCity;

		// Token: 0x0400427F RID: 17023
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition HomeCountry = InternalSchema.HomeCountry;

		// Token: 0x04004280 RID: 17024
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition HomeAddressInternal = InternalSchema.HomeAddressInternal;

		// Token: 0x04004281 RID: 17025
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition HomeLatitude = InternalSchema.HomeLatitude;

		// Token: 0x04004282 RID: 17026
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition HomeLongitude = InternalSchema.HomeLongitude;

		// Token: 0x04004283 RID: 17027
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition HomeAltitude = InternalSchema.HomeAltitude;

		// Token: 0x04004284 RID: 17028
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition HomeAccuracy = InternalSchema.HomeAccuracy;

		// Token: 0x04004285 RID: 17029
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition HomeAltitudeAccuracy = InternalSchema.HomeAltitudeAccuracy;

		// Token: 0x04004286 RID: 17030
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition HomeLocationUri = InternalSchema.HomeLocationUri;

		// Token: 0x04004287 RID: 17031
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition HomeLocationSource = InternalSchema.HomeLocationSource;

		// Token: 0x04004288 RID: 17032
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition HomeFax = InternalSchema.HomeFax;

		// Token: 0x04004289 RID: 17033
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition SelectedPreferredPhoneNumber = InternalSchema.SelectedPreferredPhoneNumber;

		// Token: 0x0400428A RID: 17034
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition HomePhone = InternalSchema.HomePhone;

		// Token: 0x0400428B RID: 17035
		[LegalTracking]
		public static readonly StorePropertyDefinition HomePhone2 = InternalSchema.HomePhone2;

		// Token: 0x0400428C RID: 17036
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition HomePostalCode = InternalSchema.HomePostalCode;

		// Token: 0x0400428D RID: 17037
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition HomeState = InternalSchema.HomeState;

		// Token: 0x0400428E RID: 17038
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition HomeStreet = InternalSchema.HomeStreet;

		// Token: 0x0400428F RID: 17039
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition IMAddress = InternalSchema.IMAddress;

		// Token: 0x04004290 RID: 17040
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition IMAddress2 = InternalSchema.IMAddress2;

		// Token: 0x04004291 RID: 17041
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition IMAddress3 = InternalSchema.IMAddress3;

		// Token: 0x04004292 RID: 17042
		[LegalTracking]
		public static readonly StorePropertyDefinition MMS = InternalSchema.MMS;

		// Token: 0x04004293 RID: 17043
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition Initials = InternalSchema.Initials;

		// Token: 0x04004294 RID: 17044
		[LegalTracking]
		public static readonly StorePropertyDefinition InternationalIsdnNumber = InternalSchema.InternationalIsdnNumber;

		// Token: 0x04004295 RID: 17045
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition IsFavorite = InternalSchema.IsFavorite;

		// Token: 0x04004296 RID: 17046
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition IsPromotedContact = InternalSchema.IsPromotedContact;

		// Token: 0x04004297 RID: 17047
		[Autoload]
		public static readonly StorePropertyDefinition Linked = InternalSchema.Linked;

		// Token: 0x04004298 RID: 17048
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition Manager = InternalSchema.Manager;

		// Token: 0x04004299 RID: 17049
		[LegalTracking]
		[Autoload]
		[DetectCodepage]
		public static readonly StorePropertyDefinition MiddleName = InternalSchema.MiddleName;

		// Token: 0x0400429A RID: 17050
		[LegalTracking]
		public static readonly StorePropertyDefinition Mileage = InternalSchema.Mileage;

		// Token: 0x0400429B RID: 17051
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition MobilePhone = InternalSchema.MobilePhone;

		// Token: 0x0400429C RID: 17052
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition Nickname = InternalSchema.Nickname;

		// Token: 0x0400429D RID: 17053
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition OfficeLocation = InternalSchema.OfficeLocation;

		// Token: 0x0400429E RID: 17054
		public static readonly StorePropertyDefinition PersonType = new PersonTypeProperty();

		// Token: 0x0400429F RID: 17055
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition Location = InternalSchema.Location;

		// Token: 0x040042A0 RID: 17056
		[LegalTracking]
		public static readonly StorePropertyDefinition OrganizationMainPhone = InternalSchema.OrganizationMainPhone;

		// Token: 0x040042A1 RID: 17057
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition OtherPostOfficeBox = InternalSchema.OtherPostOfficeBox;

		// Token: 0x040042A2 RID: 17058
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition OtherCity = InternalSchema.OtherCity;

		// Token: 0x040042A3 RID: 17059
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition OtherCountry = InternalSchema.OtherCountry;

		// Token: 0x040042A4 RID: 17060
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition OtherLatitude = InternalSchema.OtherLatitude;

		// Token: 0x040042A5 RID: 17061
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition OtherLongitude = InternalSchema.OtherLongitude;

		// Token: 0x040042A6 RID: 17062
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition OtherAltitude = InternalSchema.OtherAltitude;

		// Token: 0x040042A7 RID: 17063
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition OtherAccuracy = InternalSchema.OtherAccuracy;

		// Token: 0x040042A8 RID: 17064
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition OtherAltitudeAccuracy = InternalSchema.OtherAltitudeAccuracy;

		// Token: 0x040042A9 RID: 17065
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition OtherLocationUri = InternalSchema.OtherLocationUri;

		// Token: 0x040042AA RID: 17066
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition OtherLocationSource = InternalSchema.OtherLocationSource;

		// Token: 0x040042AB RID: 17067
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition OtherFax = InternalSchema.OtherFax;

		// Token: 0x040042AC RID: 17068
		[LegalTracking]
		public static readonly StorePropertyDefinition OtherMobile = InternalSchema.CarPhone;

		// Token: 0x040042AD RID: 17069
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition OtherPostalCode = InternalSchema.OtherPostalCode;

		// Token: 0x040042AE RID: 17070
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition OtherState = InternalSchema.OtherState;

		// Token: 0x040042AF RID: 17071
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition OtherStreet = InternalSchema.OtherStreet;

		// Token: 0x040042B0 RID: 17072
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition OtherTelephone = InternalSchema.OtherTelephone;

		// Token: 0x040042B1 RID: 17073
		[LegalTracking]
		public static readonly StorePropertyDefinition Pager = InternalSchema.Pager;

		// Token: 0x040042B2 RID: 17074
		[LegalTracking]
		public static readonly StorePropertyDefinition PartnerNetworkId = InternalSchema.PartnerNetworkId;

		// Token: 0x040042B3 RID: 17075
		[LegalTracking]
		public static readonly StorePropertyDefinition PartnerNetworkUserId = InternalSchema.PartnerNetworkUserId;

		// Token: 0x040042B4 RID: 17076
		[LegalTracking]
		public static readonly StorePropertyDefinition PartnerNetworkThumbnailPhotoUrl = InternalSchema.PartnerNetworkThumbnailPhotoUrl;

		// Token: 0x040042B5 RID: 17077
		[LegalTracking]
		public static readonly StorePropertyDefinition PartnerNetworkProfilePhotoUrl = InternalSchema.PartnerNetworkProfilePhotoUrl;

		// Token: 0x040042B6 RID: 17078
		public static readonly StorePropertyDefinition PartnerNetworkContactType = InternalSchema.PartnerNetworkContactType;

		// Token: 0x040042B7 RID: 17079
		[LegalTracking]
		public static readonly StorePropertyDefinition PostalAddressId = InternalSchema.PostalAddressId;

		// Token: 0x040042B8 RID: 17080
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition Profession = InternalSchema.Profession;

		// Token: 0x040042B9 RID: 17081
		[LegalTracking]
		public static readonly StorePropertyDefinition RadioPhone = InternalSchema.RadioPhone;

		// Token: 0x040042BA RID: 17082
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition RelevanceScore = InternalSchema.RelevanceScore;

		// Token: 0x040042BB RID: 17083
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition Schools = InternalSchema.Schools;

		// Token: 0x040042BC RID: 17084
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition SpouseName = InternalSchema.SpouseName;

		// Token: 0x040042BD RID: 17085
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition Surname = InternalSchema.Surname;

		// Token: 0x040042BE RID: 17086
		public static readonly StorePropertyDefinition TelUri = InternalSchema.TelUri;

		// Token: 0x040042BF RID: 17087
		public static readonly StorePropertyDefinition ImContactSipUriAddress = InternalSchema.ImContactSipUriAddress;

		// Token: 0x040042C0 RID: 17088
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition Title = InternalSchema.Title;

		// Token: 0x040042C1 RID: 17089
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition FullName = InternalSchema.DisplayName;

		// Token: 0x040042C2 RID: 17090
		public static readonly StorePropertyDefinition WeddingAnniversary = InternalSchema.WeddingAnniversary;

		// Token: 0x040042C3 RID: 17091
		public static readonly StorePropertyDefinition WeddingAnniversaryLocal = InternalSchema.WeddingAnniversaryLocal;

		// Token: 0x040042C4 RID: 17092
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition WorkPostOfficeBox = InternalSchema.WorkPostOfficeBox;

		// Token: 0x040042C5 RID: 17093
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition WorkAddressCity = InternalSchema.WorkAddressCity;

		// Token: 0x040042C6 RID: 17094
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition WorkAddressCountry = InternalSchema.WorkAddressCountry;

		// Token: 0x040042C7 RID: 17095
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition WorkLatitude = InternalSchema.WorkLatitude;

		// Token: 0x040042C8 RID: 17096
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition WorkLongitude = InternalSchema.WorkLongitude;

		// Token: 0x040042C9 RID: 17097
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition WorkAltitude = InternalSchema.WorkAltitude;

		// Token: 0x040042CA RID: 17098
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition WorkAccuracy = InternalSchema.WorkAccuracy;

		// Token: 0x040042CB RID: 17099
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition WorkAltitudeAccuracy = InternalSchema.WorkAltitudeAccuracy;

		// Token: 0x040042CC RID: 17100
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition WorkLocationUri = InternalSchema.WorkLocationUri;

		// Token: 0x040042CD RID: 17101
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition WorkLocationSource = InternalSchema.WorkLocationSource;

		// Token: 0x040042CE RID: 17102
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition WorkAddressPostalCode = InternalSchema.WorkAddressPostalCode;

		// Token: 0x040042CF RID: 17103
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition WorkAddressState = InternalSchema.WorkAddressState;

		// Token: 0x040042D0 RID: 17104
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition WorkAddressStreet = InternalSchema.WorkAddressStreet;

		// Token: 0x040042D1 RID: 17105
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition WorkFax = InternalSchema.FaxNumber;

		// Token: 0x040042D2 RID: 17106
		[LegalTracking]
		public static readonly StorePropertyDefinition PersonalHomePage = InternalSchema.PersonalHomePage;

		// Token: 0x040042D3 RID: 17107
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition YomiFirstName = InternalSchema.YomiFirstName;

		// Token: 0x040042D4 RID: 17108
		[Autoload]
		[LegalTracking]
		public static readonly StorePropertyDefinition YomiLastName = InternalSchema.YomiLastName;

		// Token: 0x040042D5 RID: 17109
		[LegalTracking]
		[Autoload]
		public static readonly StorePropertyDefinition YomiCompany = InternalSchema.YomiCompany;

		// Token: 0x040042D6 RID: 17110
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition HomeAddress = InternalSchema.HomeAddress;

		// Token: 0x040042D7 RID: 17111
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition BusinessAddress = InternalSchema.BusinessAddress;

		// Token: 0x040042D8 RID: 17112
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition OtherAddress = InternalSchema.OtherAddress;

		// Token: 0x040042D9 RID: 17113
		[LegalTracking]
		public static readonly StorePropertyDefinition PrimaryTelephoneNumber = InternalSchema.PrimaryTelephoneNumber;

		// Token: 0x040042DA RID: 17114
		[LegalTracking]
		public static readonly StorePropertyDefinition TtyTddPhoneNumber = InternalSchema.TtyTddPhoneNumber;

		// Token: 0x040042DB RID: 17115
		[LegalTracking]
		public static readonly StorePropertyDefinition TelexNumber = InternalSchema.TelexNumber;

		// Token: 0x040042DC RID: 17116
		[LegalTracking]
		public static readonly StorePropertyDefinition CustomerId = InternalSchema.CustomerId;

		// Token: 0x040042DD RID: 17117
		[LegalTracking]
		public static readonly StorePropertyDefinition GovernmentIdNumber = InternalSchema.GovernmentIdNumber;

		// Token: 0x040042DE RID: 17118
		[LegalTracking]
		public static readonly StorePropertyDefinition Account = InternalSchema.Account;

		// Token: 0x040042DF RID: 17119
		[LegalTracking]
		public static readonly StorePropertyDefinition UserX509Certificates = InternalSchema.UserX509Certificates;

		// Token: 0x040042E0 RID: 17120
		[LegalTracking]
		public static readonly StorePropertyDefinition OutlookCardDesign = InternalSchema.OutlookCardDesign;

		// Token: 0x040042E1 RID: 17121
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition UserText1 = InternalSchema.UserText1;

		// Token: 0x040042E2 RID: 17122
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition UserText2 = InternalSchema.UserText2;

		// Token: 0x040042E3 RID: 17123
		[DetectCodepage]
		[LegalTracking]
		public static readonly StorePropertyDefinition UserText3 = InternalSchema.UserText3;

		// Token: 0x040042E4 RID: 17124
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition UserText4 = InternalSchema.UserText4;

		// Token: 0x040042E5 RID: 17125
		public static readonly StorePropertyDefinition FreeBusyUrl = InternalSchema.FreeBusyUrl;

		// Token: 0x040042E6 RID: 17126
		[LegalTracking]
		[DetectCodepage]
		public static readonly StorePropertyDefinition Hobbies = InternalSchema.Hobbies;

		// Token: 0x040042E7 RID: 17127
		[LegalTracking]
		public static readonly StorePropertyDefinition MobilePhone2 = InternalSchema.MobilePhone2;

		// Token: 0x040042E8 RID: 17128
		[LegalTracking]
		public static readonly StorePropertyDefinition OtherPhone2 = InternalSchema.OtherPhone2;

		// Token: 0x040042E9 RID: 17129
		[LegalTracking]
		public static readonly StorePropertyDefinition HomePhoneAttributes = InternalSchema.HomePhoneAttributes;

		// Token: 0x040042EA RID: 17130
		[LegalTracking]
		public static readonly StorePropertyDefinition WorkPhoneAttributes = InternalSchema.WorkPhoneAttributes;

		// Token: 0x040042EB RID: 17131
		[LegalTracking]
		public static readonly StorePropertyDefinition MobilePhoneAttributes = InternalSchema.MobilePhoneAttributes;

		// Token: 0x040042EC RID: 17132
		[LegalTracking]
		public static readonly StorePropertyDefinition OtherPhoneAttributes = InternalSchema.OtherPhoneAttributes;

		// Token: 0x040042ED RID: 17133
		[LegalTracking]
		public static readonly StorePropertyDefinition PrimarySmtpAddress = InternalSchema.PrimarySmtpAddress;

		// Token: 0x040042EE RID: 17134
		[Autoload]
		public static readonly StorePropertyDefinition SideEffects = InternalSchema.SideEffects;

		// Token: 0x040042EF RID: 17135
		public static readonly StorePropertyDefinition LinkRejectHistory = new LinkRejectHistoryProperty();

		// Token: 0x040042F0 RID: 17136
		public static readonly StorePropertyDefinition[] EmailAddressProperties = new StorePropertyDefinition[]
		{
			ContactSchema.Email1EmailAddress,
			ContactSchema.Email2EmailAddress,
			ContactSchema.Email3EmailAddress,
			ContactProtectedPropertiesSchema.ProtectedEmailAddress
		};

		// Token: 0x040042F1 RID: 17137
		public static readonly StorePropertyDefinition GALLinkID = InternalSchema.GALLinkID;

		// Token: 0x040042F2 RID: 17138
		public static readonly StorePropertyDefinition SmtpAddressCache = InternalSchema.SmtpAddressCache;

		// Token: 0x040042F3 RID: 17139
		public static readonly StorePropertyDefinition GALLinkState = InternalSchema.GALLinkState;

		// Token: 0x040042F4 RID: 17140
		public static readonly StorePropertyDefinition UserApprovedLink = InternalSchema.UserApprovedLink;

		// Token: 0x040042F5 RID: 17141
		public static readonly StorePropertyDefinition LinkChangeHistory = InternalSchema.LinkChangeHistory;

		// Token: 0x040042F6 RID: 17142
		public static readonly StorePropertyDefinition OscContactSourcesForContact = InternalSchema.OscContactSourcesForContact;

		// Token: 0x040042F7 RID: 17143
		public static readonly StorePropertyDefinition PeopleConnectionCreationTime = InternalSchema.PeopleConnectionCreationTime;

		// Token: 0x040042F8 RID: 17144
		public static readonly StorePropertyDefinition AddressBookEntryId = InternalSchema.AddressBookEntryId;

		// Token: 0x040042F9 RID: 17145
		public static readonly StorePropertyDefinition PersonId = InternalSchema.PersonId;

		// Token: 0x040042FA RID: 17146
		public static readonly StorePropertyDefinition AttributionDisplayName = InternalSchema.AttributionDisplayName;

		// Token: 0x040042FB RID: 17147
		public static readonly StorePropertyDefinition IsWritable = InternalSchema.IsContactWritable;

		// Token: 0x040042FC RID: 17148
		private static readonly PropertyRule[] ContactSchemaPropertyRules;

		// Token: 0x040042FD RID: 17149
		private static ContactSchema instance;

		// Token: 0x040042FE RID: 17150
		private ICollection<PropertyRule> propertyRulesCache;
	}
}
