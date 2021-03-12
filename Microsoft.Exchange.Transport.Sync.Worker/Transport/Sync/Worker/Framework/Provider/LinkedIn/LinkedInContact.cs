using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema;
using Microsoft.Exchange.Net.LinkedIn;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.LinkedIn
{
	// Token: 0x020001E4 RID: 484
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LinkedInContact : DisposeTrackableBase, ISyncContact, ISyncObject, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000FBB RID: 4027 RVA: 0x00030F84 File Offset: 0x0002F184
		internal LinkedInContact(LinkedInPerson contact, ExDateTime peopleConnectionCreationTime)
		{
			SyncUtilities.ThrowIfArgumentNull("contact", contact);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("contact.Id", contact.Id);
			this.contact = contact;
			this.lastModifiedTime = new ExDateTime?(ExDateTime.UtcNow);
			this.peopleConnectionCreationTime = new ExDateTime?(peopleConnectionCreationTime);
			this.SetJobTitle();
			this.SetPhoneNumbers();
			this.SetImAddress();
			this.SetBirthDate();
			this.InitializeOscContactSources();
			this.SetPhotoUrl();
			this.SuppressDisposeTracker();
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x00030FFF File Offset: 0x0002F1FF
		public SchemaType Type
		{
			get
			{
				base.CheckDisposed();
				return SchemaType.Contact;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x00031008 File Offset: 0x0002F208
		public string FirstName
		{
			get
			{
				base.CheckDisposed();
				return this.contact.FirstName;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x0003101B File Offset: 0x0002F21B
		public string Hobbies
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00031024 File Offset: 0x0002F224
		public string MiddleName
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x0003102D File Offset: 0x0002F22D
		public string LastName
		{
			get
			{
				base.CheckDisposed();
				return this.contact.LastName;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x00031040 File Offset: 0x0002F240
		public string JobTitle
		{
			get
			{
				base.CheckDisposed();
				return this.jobTitle;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x0003104E File Offset: 0x0002F24E
		public string FileAs
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00031057 File Offset: 0x0002F257
		public string BusinessTelephoneNumber
		{
			get
			{
				base.CheckDisposed();
				return this.businessPhoneNumber;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00031065 File Offset: 0x0002F265
		public string HomeTelephoneNumber
		{
			get
			{
				base.CheckDisposed();
				return this.homePhoneNumber;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00031073 File Offset: 0x0002F273
		public string MobileTelephoneNumber
		{
			get
			{
				base.CheckDisposed();
				return this.mobilePhoneNumber;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00031081 File Offset: 0x0002F281
		public string BusinessFaxNumber
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0003108A File Offset: 0x0002F28A
		public string OtherTelephoneNumber
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00031094 File Offset: 0x0002F294
		public string CompanyName
		{
			get
			{
				base.CheckDisposed();
				if (this.contact.ThreeCurrentPositions != null && this.contact.ThreeCurrentPositions.Positions != null && this.contact.ThreeCurrentPositions.Positions.Count > 0 && this.contact.ThreeCurrentPositions.Positions[0].Company != null)
				{
					return this.contact.ThreeCurrentPositions.Positions[0].Company.Name;
				}
				return null;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x0003111D File Offset: 0x0002F31D
		public string Email1Address
		{
			get
			{
				base.CheckDisposed();
				return this.contact.EmailAddress;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00031130 File Offset: 0x0002F330
		public string Email2Address
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00031139 File Offset: 0x0002F339
		public string Email3Address
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x00031142 File Offset: 0x0002F342
		public string Webpage
		{
			get
			{
				base.CheckDisposed();
				return this.contact.PublicProfileUrl;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00031155 File Offset: 0x0002F355
		public string BusinessAddressStreet
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x0003115E File Offset: 0x0002F35E
		public string BusinessAddressCity
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00031167 File Offset: 0x0002F367
		public string BusinessAddressState
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x00031170 File Offset: 0x0002F370
		public string BusinessAddressPostalCode
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00031179 File Offset: 0x0002F379
		public string BusinessAddressCountry
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x00031182 File Offset: 0x0002F382
		public string HomeAddressStreet
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0003118B File Offset: 0x0002F38B
		public string HomeAddressCity
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00031194 File Offset: 0x0002F394
		public string HomeAddressState
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0003119D File Offset: 0x0002F39D
		public string HomeAddressPostalCode
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x000311A6 File Offset: 0x0002F3A6
		public string HomeAddressCountry
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x000311AF File Offset: 0x0002F3AF
		public string IMAddress
		{
			get
			{
				base.CheckDisposed();
				return this.imAddress;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x000311BD File Offset: 0x0002F3BD
		public ExDateTime? LastModifiedTime
		{
			get
			{
				base.CheckDisposed();
				return this.lastModifiedTime;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x000311CB File Offset: 0x0002F3CB
		public ExDateTime? BirthDate
		{
			get
			{
				base.CheckDisposed();
				return this.birthDate;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x000311D9 File Offset: 0x0002F3D9
		public ExDateTime? BirthDateLocal
		{
			get
			{
				base.CheckDisposed();
				return this.birthDate;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x000311E7 File Offset: 0x0002F3E7
		public string DisplayName
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x000311F0 File Offset: 0x0002F3F0
		public string Location
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x000311F9 File Offset: 0x0002F3F9
		public byte[] OscContactSources
		{
			get
			{
				base.CheckDisposed();
				return this.oscContactSources;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00031207 File Offset: 0x0002F407
		public string PartnerNetworkId
		{
			get
			{
				base.CheckDisposed();
				return "LinkedIn";
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00031214 File Offset: 0x0002F414
		public string PartnerNetworkUserId
		{
			get
			{
				base.CheckDisposed();
				return this.contact.Id;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00031227 File Offset: 0x0002F427
		public string PartnerNetworkThumbnailPhotoUrl
		{
			get
			{
				base.CheckDisposed();
				return this.photoUrl;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00031235 File Offset: 0x0002F435
		public string PartnerNetworkProfilePhotoUrl
		{
			get
			{
				base.CheckDisposed();
				return this.photoUrl;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x00031243 File Offset: 0x0002F443
		public string PartnerNetworkContactType
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x0003124C File Offset: 0x0002F44C
		public ExDateTime? PeopleConnectionCreationTime
		{
			get
			{
				base.CheckDisposed();
				return this.peopleConnectionCreationTime;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x0003125A File Offset: 0x0002F45A
		public string ProtectedEmailAddress
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x00031263 File Offset: 0x0002F463
		public string ProtectedPhoneNumber
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x0003126C File Offset: 0x0002F46C
		public string Schools
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00031275 File Offset: 0x0002F475
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00031277 File Offset: 0x0002F477
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<LinkedInContact>(this);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00031280 File Offset: 0x0002F480
		private void SetJobTitle()
		{
			if (this.contact.ThreeCurrentPositions != null && this.contact.ThreeCurrentPositions.Positions != null)
			{
				this.jobTitle = this.contact.ThreeCurrentPositions.Positions[0].Title;
				return;
			}
			this.jobTitle = this.contact.Headline;
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x000312E0 File Offset: 0x0002F4E0
		private void SetPhoneNumbers()
		{
			if (this.contact.PhoneNumbers != null && this.contact.PhoneNumbers.Numbers != null)
			{
				foreach (LinkedInPhoneNumber linkedInPhoneNumber in this.contact.PhoneNumbers.Numbers)
				{
					if (StringComparer.InvariantCultureIgnoreCase.Equals(linkedInPhoneNumber.Type, "mobile"))
					{
						this.mobilePhoneNumber = linkedInPhoneNumber.Number;
					}
					else if (StringComparer.InvariantCultureIgnoreCase.Equals(linkedInPhoneNumber.Type, "work"))
					{
						this.businessPhoneNumber = linkedInPhoneNumber.Number;
					}
					else if (StringComparer.InvariantCultureIgnoreCase.Equals(linkedInPhoneNumber.Type, "home"))
					{
						this.homePhoneNumber = linkedInPhoneNumber.Number;
					}
				}
			}
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x000313C8 File Offset: 0x0002F5C8
		private void SetImAddress()
		{
			if (this.contact.IMAccounts != null && this.contact.IMAccounts.Accounts != null && this.contact.IMAccounts.Accounts.Count > 0 && this.contact.IMAccounts.Accounts[0] != null)
			{
				this.imAddress = this.contact.IMAccounts.Accounts[0].IMAccountType + ":" + this.contact.IMAccounts.Accounts[0].IMAccountName;
			}
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00031470 File Offset: 0x0002F670
		private void SetBirthDate()
		{
			if (this.contact.Birthdate != null && this.contact.Birthdate.Month > 0 && this.contact.Birthdate.Day > 0)
			{
				int year = (this.contact.Birthdate.Year > 0) ? this.contact.Birthdate.Year : 1604;
				try
				{
					this.birthDate = new ExDateTime?(new ExDateTime(ExTimeZone.UtcTimeZone, year, this.contact.Birthdate.Month, this.contact.Birthdate.Day));
				}
				catch (ArgumentOutOfRangeException)
				{
				}
			}
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0003152C File Offset: 0x0002F72C
		private void InitializeOscContactSources()
		{
			this.oscContactSources = OscContactSourcesForContactWriter.Instance.Write(OscProviderGuids.LinkedIn, "LinkedIn", this.contact.Id);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00031554 File Offset: 0x0002F754
		private void SetPhotoUrl()
		{
			if (this.contact.PictureUrls != null && this.contact.PictureUrls.Urls != null && this.contact.PictureUrls.Urls.Count > 0)
			{
				this.photoUrl = this.contact.PictureUrls.Urls[0];
			}
		}

		// Token: 0x040008A1 RID: 2209
		private const string LinkedInPartnerNetworkId = "LinkedIn";

		// Token: 0x040008A2 RID: 2210
		private const string HomePhoneNumberType = "home";

		// Token: 0x040008A3 RID: 2211
		private const string MobilePhoneNumberType = "mobile";

		// Token: 0x040008A4 RID: 2212
		private const string WorkPhoneNumberType = "work";

		// Token: 0x040008A5 RID: 2213
		private readonly LinkedInPerson contact;

		// Token: 0x040008A6 RID: 2214
		private readonly ExDateTime? lastModifiedTime;

		// Token: 0x040008A7 RID: 2215
		private string jobTitle;

		// Token: 0x040008A8 RID: 2216
		private string businessPhoneNumber;

		// Token: 0x040008A9 RID: 2217
		private string mobilePhoneNumber;

		// Token: 0x040008AA RID: 2218
		private string homePhoneNumber;

		// Token: 0x040008AB RID: 2219
		private string imAddress;

		// Token: 0x040008AC RID: 2220
		private ExDateTime? birthDate;

		// Token: 0x040008AD RID: 2221
		private byte[] oscContactSources;

		// Token: 0x040008AE RID: 2222
		private readonly ExDateTime? peopleConnectionCreationTime;

		// Token: 0x040008AF RID: 2223
		private string photoUrl;
	}
}
