using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.ABProviderFramework;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000A7 RID: 167
	internal sealed class ADABContact : ABContact
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x000367A0 File Offset: 0x000349A0
		public ADABContact(ABSession ownerSession, ADRecipient recipient) : base(ownerSession)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			if (recipient.Id == null)
			{
				throw new ArgumentException("recipient.Id can't be null.", "recipient.Id");
			}
			switch (recipient.RecipientType)
			{
			case RecipientType.MailUniversalDistributionGroup:
			case RecipientType.MailUniversalSecurityGroup:
			case RecipientType.MailNonUniversalGroup:
			case RecipientType.DynamicDistributionGroup:
				throw new ArgumentException("RecipientType " + recipient.RecipientType.ToString() + " shouldn't be wrapped in an ADABContact.", "recipient");
			default:
				this.recipient = recipient;
				return;
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0003682C File Offset: 0x00034A2C
		protected override ABObjectId GetId()
		{
			ADABObjectId adabobjectId;
			if (this.id == null && ADABUtils.GetId(this.recipient, out adabobjectId))
			{
				this.id = adabobjectId;
			}
			return this.id;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0003685D File Offset: 0x00034A5D
		protected override string GetLegacyExchangeDN()
		{
			return this.recipient.LegacyExchangeDN;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0003686A File Offset: 0x00034A6A
		protected override bool GetCanEmail()
		{
			return ADABUtils.CanEmailRecipientType(this.recipient.RecipientType);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0003687C File Offset: 0x00034A7C
		protected override string GetAlias()
		{
			return this.recipient.Alias;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0003688C File Offset: 0x00034A8C
		protected override string GetEmailAddress()
		{
			string result;
			if (ADABUtils.GetEmailAddress(this.recipient, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x000368AB File Offset: 0x00034AAB
		protected override string GetDisplayName()
		{
			return this.recipient.DisplayName;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000368B8 File Offset: 0x00034AB8
		protected override string GetBusinessPhoneNumber()
		{
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return null;
			}
			return iadorgPerson.Phone;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000368DC File Offset: 0x00034ADC
		protected override string GetCompanyName()
		{
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return null;
			}
			return iadorgPerson.Company;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00036900 File Offset: 0x00034B00
		protected override string GetDepartmentName()
		{
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return null;
			}
			return iadorgPerson.Department;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00036924 File Offset: 0x00034B24
		protected override string GetBusinessFaxNumber()
		{
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return null;
			}
			return iadorgPerson.Fax;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00036948 File Offset: 0x00034B48
		protected override string GetGivenName()
		{
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return null;
			}
			return iadorgPerson.FirstName;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0003696C File Offset: 0x00034B6C
		protected override string GetHomePhoneNumber()
		{
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return null;
			}
			return iadorgPerson.HomePhone;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00036990 File Offset: 0x00034B90
		protected override string GetInitials()
		{
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return null;
			}
			return iadorgPerson.Initials;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x000369B4 File Offset: 0x00034BB4
		protected override string GetMobilePhoneNumber()
		{
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return null;
			}
			return iadorgPerson.MobilePhone;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x000369D8 File Offset: 0x00034BD8
		protected override string GetOfficeLocation()
		{
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return null;
			}
			return iadorgPerson.Office;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000369FC File Offset: 0x00034BFC
		protected override string GetSurname()
		{
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return null;
			}
			return iadorgPerson.LastName;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00036A20 File Offset: 0x00034C20
		protected override string GetTitle()
		{
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return null;
			}
			return iadorgPerson.Title;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00036A44 File Offset: 0x00034C44
		protected override string GetWorkAddressPostOfficeBox()
		{
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return null;
			}
			return ADABContact.GetFirstValue(iadorgPerson.PostOfficeBox);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00036A70 File Offset: 0x00034C70
		protected override string GetWorkAddressStreet()
		{
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return null;
			}
			return iadorgPerson.StreetAddress;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00036A94 File Offset: 0x00034C94
		protected override string GetWorkAddressCity()
		{
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return null;
			}
			return iadorgPerson.City;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00036AB8 File Offset: 0x00034CB8
		protected override string GetWorkAddressState()
		{
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return null;
			}
			return iadorgPerson.StateOrProvince;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00036ADC File Offset: 0x00034CDC
		protected override string GetWorkAddressPostalCode()
		{
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return null;
			}
			return iadorgPerson.PostalCode;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00036B00 File Offset: 0x00034D00
		protected override string GetWorkAddressCountry()
		{
			IADOrgPerson iadorgPerson = this.recipient as IADOrgPerson;
			if (iadorgPerson == null)
			{
				return null;
			}
			return iadorgPerson.CountryOrRegionDisplayName;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00036B24 File Offset: 0x00034D24
		protected override Uri GetWebPage()
		{
			Uri result;
			if (!ADABUtils.GetWebPage(this.recipient, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00036B43 File Offset: 0x00034D43
		protected override byte[] GetPicture()
		{
			return this.recipient.ThumbnailPhoto;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00036B50 File Offset: 0x00034D50
		private static string GetFirstValue(MultiValuedProperty<string> mvp)
		{
			if (mvp == null || mvp.Count == 0)
			{
				return null;
			}
			return mvp[0];
		}

		// Token: 0x040005D6 RID: 1494
		private ADRecipient recipient;

		// Token: 0x040005D7 RID: 1495
		private ADABObjectId id;
	}
}
