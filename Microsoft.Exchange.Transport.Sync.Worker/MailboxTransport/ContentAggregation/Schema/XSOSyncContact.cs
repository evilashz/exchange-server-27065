using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema
{
	// Token: 0x02000233 RID: 563
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class XSOSyncContact : DisposeTrackableBase, ISyncContact, ISyncObject, IDisposeTrackable, IDisposable
	{
		// Token: 0x06001451 RID: 5201 RVA: 0x00049D39 File Offset: 0x00047F39
		internal XSOSyncContact(Item contact) : this(contact, true)
		{
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x00049D43 File Offset: 0x00047F43
		private XSOSyncContact(Item contact, bool ownContact)
		{
			SyncUtilities.ThrowIfArgumentNull("contact", contact);
			this.contact = contact;
			this.ownContact = ownContact;
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x00049D6F File Offset: 0x00047F6F
		// (set) Token: 0x06001454 RID: 5204 RVA: 0x00049D8D File Offset: 0x00047F8D
		public string FirstName
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.FirstName.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.FirstName.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x00049DAC File Offset: 0x00047FAC
		// (set) Token: 0x06001456 RID: 5206 RVA: 0x00049DCA File Offset: 0x00047FCA
		public string Hobbies
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.Hobbies.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.Hobbies.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x00049DE9 File Offset: 0x00047FE9
		// (set) Token: 0x06001458 RID: 5208 RVA: 0x00049E07 File Offset: 0x00048007
		public string MiddleName
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.MiddleName.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.MiddleName.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x00049E26 File Offset: 0x00048026
		// (set) Token: 0x0600145A RID: 5210 RVA: 0x00049E44 File Offset: 0x00048044
		public string LastName
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.LastName.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.LastName.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x00049E63 File Offset: 0x00048063
		// (set) Token: 0x0600145C RID: 5212 RVA: 0x00049E81 File Offset: 0x00048081
		public string JobTitle
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.JobTile.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.JobTile.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x00049EA0 File Offset: 0x000480A0
		// (set) Token: 0x0600145E RID: 5214 RVA: 0x00049EBE File Offset: 0x000480BE
		public string FileAs
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.FileAs.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.FileAs.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x00049EDD File Offset: 0x000480DD
		// (set) Token: 0x06001460 RID: 5216 RVA: 0x00049EFB File Offset: 0x000480FB
		public string BusinessTelephoneNumber
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.BusinessTelephoneNumber.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.BusinessTelephoneNumber.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x00049F1A File Offset: 0x0004811A
		// (set) Token: 0x06001462 RID: 5218 RVA: 0x00049F38 File Offset: 0x00048138
		public string HomeTelephoneNumber
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.HomeTelephoneNumber.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.HomeTelephoneNumber.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x00049F57 File Offset: 0x00048157
		// (set) Token: 0x06001464 RID: 5220 RVA: 0x00049F75 File Offset: 0x00048175
		public string MobileTelephoneNumber
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.MobileTelephoneNumber.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.MobileTelephoneNumber.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x00049F94 File Offset: 0x00048194
		// (set) Token: 0x06001466 RID: 5222 RVA: 0x00049FB2 File Offset: 0x000481B2
		public string BusinessFaxNumber
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.BusinessFaxNumber.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.BusinessFaxNumber.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x00049FD1 File Offset: 0x000481D1
		// (set) Token: 0x06001468 RID: 5224 RVA: 0x00049FEF File Offset: 0x000481EF
		public string OtherTelephoneNumber
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.OtherTelephoneNumber.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.OtherTelephoneNumber.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0004A00E File Offset: 0x0004820E
		// (set) Token: 0x0600146A RID: 5226 RVA: 0x0004A02C File Offset: 0x0004822C
		public string CompanyName
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.CompanyName.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.CompanyName.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x0004A04B File Offset: 0x0004824B
		// (set) Token: 0x0600146C RID: 5228 RVA: 0x0004A069 File Offset: 0x00048269
		public string Email1Address
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.Email1Address.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.Email1Address.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x0004A088 File Offset: 0x00048288
		// (set) Token: 0x0600146E RID: 5230 RVA: 0x0004A0A6 File Offset: 0x000482A6
		public string Email2Address
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.Email2Address.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.Email2Address.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x0004A0C5 File Offset: 0x000482C5
		// (set) Token: 0x06001470 RID: 5232 RVA: 0x0004A0E3 File Offset: 0x000482E3
		public string Email3Address
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.Email3Address.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.Email3Address.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0004A102 File Offset: 0x00048302
		// (set) Token: 0x06001472 RID: 5234 RVA: 0x0004A120 File Offset: 0x00048320
		public string Webpage
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.WebPage.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.WebPage.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0004A13F File Offset: 0x0004833F
		// (set) Token: 0x06001474 RID: 5236 RVA: 0x0004A15D File Offset: 0x0004835D
		public string BusinessAddressStreet
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.BusinessAddressStreet.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.BusinessAddressStreet.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0004A17C File Offset: 0x0004837C
		// (set) Token: 0x06001476 RID: 5238 RVA: 0x0004A19A File Offset: 0x0004839A
		public string BusinessAddressCity
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.BusinessAddressCity.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.BusinessAddressCity.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x0004A1B9 File Offset: 0x000483B9
		// (set) Token: 0x06001478 RID: 5240 RVA: 0x0004A1D7 File Offset: 0x000483D7
		public string BusinessAddressState
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.BusinessAddressState.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.BusinessAddressState.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0004A1F6 File Offset: 0x000483F6
		// (set) Token: 0x0600147A RID: 5242 RVA: 0x0004A214 File Offset: 0x00048414
		public string BusinessAddressPostalCode
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.BusinessAddressPostalCode.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.BusinessAddressPostalCode.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x0004A233 File Offset: 0x00048433
		// (set) Token: 0x0600147C RID: 5244 RVA: 0x0004A251 File Offset: 0x00048451
		public string BusinessAddressCountry
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.BusinessAddressCountry.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.BusinessAddressCountry.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x0004A270 File Offset: 0x00048470
		// (set) Token: 0x0600147E RID: 5246 RVA: 0x0004A28E File Offset: 0x0004848E
		public string HomeAddressStreet
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.HomeAddressStreet.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.HomeAddressStreet.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x0004A2AD File Offset: 0x000484AD
		// (set) Token: 0x06001480 RID: 5248 RVA: 0x0004A2CB File Offset: 0x000484CB
		public string HomeAddressCity
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.HomeAddressCity.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.HomeAddressCity.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x0004A2EA File Offset: 0x000484EA
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x0004A308 File Offset: 0x00048508
		public string HomeAddressState
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.HomeAddressState.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.HomeAddressState.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x0004A327 File Offset: 0x00048527
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x0004A345 File Offset: 0x00048545
		public string HomeAddressPostalCode
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.HomeAddressPostalCode.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.HomeAddressPostalCode.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x0004A364 File Offset: 0x00048564
		// (set) Token: 0x06001486 RID: 5254 RVA: 0x0004A382 File Offset: 0x00048582
		public string HomeAddressCountry
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.HomeAddressCountry.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.HomeAddressCountry.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0004A3A1 File Offset: 0x000485A1
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x0004A3BF File Offset: 0x000485BF
		public string IMAddress
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.IMAddress.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.IMAddress.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0004A3DE File Offset: 0x000485DE
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x0004A3FC File Offset: 0x000485FC
		public ExDateTime? BirthDate
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.BirthDate.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.BirthDate.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x0004A41B File Offset: 0x0004861B
		// (set) Token: 0x0600148C RID: 5260 RVA: 0x0004A439 File Offset: 0x00048639
		public ExDateTime? BirthDateLocal
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.BirthDateLocal.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.BirthDateLocal.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x0004A458 File Offset: 0x00048658
		// (set) Token: 0x0600148E RID: 5262 RVA: 0x0004A476 File Offset: 0x00048676
		public string DisplayName
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.DisplayName.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.DisplayName.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0004A495 File Offset: 0x00048695
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x0004A4B3 File Offset: 0x000486B3
		public string Location
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.Location.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.Location.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0004A4D2 File Offset: 0x000486D2
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x0004A4F0 File Offset: 0x000486F0
		public byte[] OscContactSources
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.OscContactSources.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.OscContactSources.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x0004A50F File Offset: 0x0004870F
		// (set) Token: 0x06001494 RID: 5268 RVA: 0x0004A52D File Offset: 0x0004872D
		public string PartnerNetworkId
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.PartnerNetworkId.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.PartnerNetworkId.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x0004A54C File Offset: 0x0004874C
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x0004A56A File Offset: 0x0004876A
		public string PartnerNetworkUserId
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.PartnerNetworkUserId.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.PartnerNetworkUserId.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x0004A589 File Offset: 0x00048789
		// (set) Token: 0x06001498 RID: 5272 RVA: 0x0004A5A7 File Offset: 0x000487A7
		public string PartnerNetworkThumbnailPhotoUrl
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.PartnerNetworkThumbnailPhotoUrl.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.PartnerNetworkThumbnailPhotoUrl.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x0004A5C6 File Offset: 0x000487C6
		// (set) Token: 0x0600149A RID: 5274 RVA: 0x0004A5E4 File Offset: 0x000487E4
		public string PartnerNetworkProfilePhotoUrl
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.PartnerNetworkProfilePhotoUrl.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.PartnerNetworkProfilePhotoUrl.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x0004A603 File Offset: 0x00048803
		// (set) Token: 0x0600149C RID: 5276 RVA: 0x0004A621 File Offset: 0x00048821
		public string PartnerNetworkContactType
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.PartnerNetworkContactType.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.PartnerNetworkContactType.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x0004A640 File Offset: 0x00048840
		// (set) Token: 0x0600149E RID: 5278 RVA: 0x0004A65E File Offset: 0x0004885E
		public ExDateTime? PeopleConnectionCreationTime
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.PeopleConnectionCreationTime.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.PeopleConnectionCreationTime.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x0004A67D File Offset: 0x0004887D
		// (set) Token: 0x060014A0 RID: 5280 RVA: 0x0004A69B File Offset: 0x0004889B
		public string ProtectedEmailAddress
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.ProtectedEmailAddress.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.ProtectedEmailAddress.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x0004A6BA File Offset: 0x000488BA
		// (set) Token: 0x060014A2 RID: 5282 RVA: 0x0004A6D8 File Offset: 0x000488D8
		public string ProtectedPhoneNumber
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.ProtectedPhoneNumber.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.ProtectedPhoneNumber.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x0004A6F7 File Offset: 0x000488F7
		// (set) Token: 0x060014A4 RID: 5284 RVA: 0x0004A715 File Offset: 0x00048915
		public string Schools
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.Schools.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.Schools.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x0004A734 File Offset: 0x00048934
		// (set) Token: 0x060014A6 RID: 5286 RVA: 0x0004A752 File Offset: 0x00048952
		public ExDateTime? LastModifiedTime
		{
			get
			{
				base.CheckDisposed();
				return this.contactPropertyManager.LastModifiedTime.ReadProperty(this.contact);
			}
			private set
			{
				base.CheckDisposed();
				this.contactPropertyManager.LastModifiedTime.WriteProperty(this.contact, value);
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x0004A771 File Offset: 0x00048971
		public SchemaType Type
		{
			get
			{
				base.CheckDisposed();
				return SchemaType.Contact;
			}
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0004A77C File Offset: 0x0004897C
		public static void CopyPropertiesFromISyncContact(Item contact, ISyncContact syncContact)
		{
			SyncUtilities.ThrowIfArgumentNull("contact", contact);
			SyncUtilities.ThrowIfArgumentNull("syncContact", syncContact);
			using (XSOSyncContact xsosyncContact = new XSOSyncContact(contact, false))
			{
				xsosyncContact.CopyPropertiesFromISyncContact(syncContact);
			}
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0004A7CC File Offset: 0x000489CC
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.ownContact && this.contact != null)
				{
					this.contact.Dispose();
				}
				this.contact = null;
			}
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0004A7F3 File Offset: 0x000489F3
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<XSOSyncContact>(this);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0004A7FC File Offset: 0x000489FC
		private void CopyPropertiesFromISyncContact(ISyncContact syncContact)
		{
			this.BirthDate = syncContact.BirthDate;
			this.BirthDateLocal = syncContact.BirthDateLocal;
			this.BusinessAddressCity = syncContact.BusinessAddressCity;
			this.BusinessAddressCountry = syncContact.BusinessAddressCountry;
			this.BusinessAddressPostalCode = syncContact.BusinessAddressPostalCode;
			this.BusinessAddressState = syncContact.BusinessAddressState;
			this.BusinessAddressStreet = syncContact.BusinessAddressStreet;
			this.BusinessFaxNumber = syncContact.BusinessFaxNumber;
			this.BusinessTelephoneNumber = syncContact.BusinessTelephoneNumber;
			this.CompanyName = syncContact.CompanyName;
			this.DisplayName = syncContact.DisplayName;
			this.Email1Address = syncContact.Email1Address;
			this.Email2Address = syncContact.Email2Address;
			this.Email3Address = syncContact.Email3Address;
			this.FileAs = syncContact.FileAs;
			this.FirstName = syncContact.FirstName;
			this.Hobbies = syncContact.Hobbies;
			this.HomeAddressCity = syncContact.HomeAddressCity;
			this.HomeAddressCountry = syncContact.HomeAddressCountry;
			this.HomeAddressPostalCode = syncContact.HomeAddressPostalCode;
			this.HomeAddressState = syncContact.HomeAddressState;
			this.HomeAddressStreet = syncContact.HomeAddressStreet;
			this.HomeTelephoneNumber = syncContact.HomeTelephoneNumber;
			this.IMAddress = syncContact.IMAddress;
			this.JobTitle = syncContact.JobTitle;
			this.LastName = syncContact.LastName;
			this.LastModifiedTime = syncContact.LastModifiedTime;
			this.Location = syncContact.Location;
			this.MiddleName = syncContact.MiddleName;
			this.MobileTelephoneNumber = syncContact.MobileTelephoneNumber;
			this.OscContactSources = syncContact.OscContactSources;
			this.OtherTelephoneNumber = syncContact.OtherTelephoneNumber;
			this.PartnerNetworkContactType = syncContact.PartnerNetworkContactType;
			this.PartnerNetworkId = syncContact.PartnerNetworkId;
			this.PartnerNetworkProfilePhotoUrl = syncContact.PartnerNetworkProfilePhotoUrl;
			this.PartnerNetworkThumbnailPhotoUrl = syncContact.PartnerNetworkThumbnailPhotoUrl;
			this.PartnerNetworkUserId = syncContact.PartnerNetworkUserId;
			this.PeopleConnectionCreationTime = syncContact.PeopleConnectionCreationTime;
			this.ProtectedEmailAddress = syncContact.ProtectedEmailAddress;
			this.ProtectedPhoneNumber = syncContact.ProtectedPhoneNumber;
			this.Schools = syncContact.Schools;
			this.Webpage = syncContact.Webpage;
		}

		// Token: 0x04000AB9 RID: 2745
		private readonly ContactPropertyManager contactPropertyManager = ContactPropertyManager.Instance;

		// Token: 0x04000ABA RID: 2746
		private Item contact;

		// Token: 0x04000ABB RID: 2747
		private readonly bool ownContact;
	}
}
