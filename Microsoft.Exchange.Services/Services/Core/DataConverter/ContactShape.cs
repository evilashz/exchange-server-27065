using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001A1 RID: 417
	internal sealed class ContactShape : Shape
	{
		// Token: 0x06000B90 RID: 2960 RVA: 0x00038F38 File Offset: 0x00037138
		static ContactShape()
		{
			ContactShape.defaultProperties.Add(ItemSchema.ItemId);
			ContactShape.defaultProperties.Add(ItemSchema.Attachments);
			ContactShape.defaultProperties.Add(ItemSchema.ResponseObjects);
			ContactShape.defaultProperties.Add(ItemSchema.HasAttachments);
			ContactShape.defaultProperties.Add(ItemSchema.Culture);
			ContactShape.defaultProperties.Add(ContactSchema.CompanyName);
			ContactShape.defaultProperties.Add(ContactSchema.CompleteName);
			ContactShape.defaultProperties.Add(ContactSchema.EmailAddressEmailAddress1);
			ContactShape.defaultProperties.Add(ContactSchema.EmailAddressEmailAddress2);
			ContactShape.defaultProperties.Add(ContactSchema.EmailAddressEmailAddress3);
			ContactShape.defaultProperties.Add(ContactSchema.FileAs);
			ContactShape.defaultProperties.Add(ContactSchema.JobTitle);
			ContactShape.defaultProperties.Add(ContactSchema.ImAddressImAddress1);
			ContactShape.defaultProperties.Add(ContactSchema.ImAddressImAddress2);
			ContactShape.defaultProperties.Add(ContactSchema.ImAddressImAddress3);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberAssistantPhone);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberBusinessFax);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberBusinessPhone);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberBusinessPhone2);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberCallback);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberCarPhone);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberCompanyMainPhone);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberHomeFax);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberHomePhone);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberHomePhone2);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberIsdn);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberMobilePhone);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberOtherFax);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberOtherTelephone);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberPager);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberPrimaryPhone);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberRadioPhone);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberTelex);
			ContactShape.defaultProperties.Add(ContactSchema.PhoneNumberTtyTddPhone);
			ContactShape.defaultProperties.Add(ContactSchema.PhysicalAddressBusinessStreet);
			ContactShape.defaultProperties.Add(ContactSchema.PhysicalAddressBusinessCity);
			ContactShape.defaultProperties.Add(ContactSchema.PhysicalAddressBusinessState);
			ContactShape.defaultProperties.Add(ContactSchema.PhysicalAddressBusinessCountryOrRegion);
			ContactShape.defaultProperties.Add(ContactSchema.PhysicalAddressBusinessPostalCode);
			ContactShape.defaultProperties.Add(ContactSchema.PhysicalAddressHomeStreet);
			ContactShape.defaultProperties.Add(ContactSchema.PhysicalAddressHomeCity);
			ContactShape.defaultProperties.Add(ContactSchema.PhysicalAddressHomeState);
			ContactShape.defaultProperties.Add(ContactSchema.PhysicalAddressHomeCountryOrRegion);
			ContactShape.defaultProperties.Add(ContactSchema.PhysicalAddressHomePostalCode);
			ContactShape.defaultProperties.Add(ContactSchema.PhysicalAddressOtherStreet);
			ContactShape.defaultProperties.Add(ContactSchema.PhysicalAddressOtherCity);
			ContactShape.defaultProperties.Add(ContactSchema.PhysicalAddressOtherState);
			ContactShape.defaultProperties.Add(ContactSchema.PhysicalAddressOtherCountryOrRegion);
			ContactShape.defaultProperties.Add(ContactSchema.PhysicalAddressOtherPostalCode);
			ContactShape.defaultProperties.Add(ContactSchema.HasPicture);
			ContactShape.defaultProperties.Add(ContactSchema.GivenName);
			ContactShape.defaultProperties.Add(ContactSchema.Surname);
			ContactShape.defaultProperties.Add(ContactSchema.DisplayName);
			ContactShape.defaultProperties.Add(ContactSchema.Department);
			ContactShape.defaultProperties.Add(ContactSchema.OfficeLocation);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00039288 File Offset: 0x00037488
		private ContactShape() : base(Schema.Contact, ContactSchema.GetSchema(), ItemShape.CreateShape(), ContactShape.defaultProperties)
		{
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000392A4 File Offset: 0x000374A4
		internal static ContactShape CreateShape()
		{
			return new ContactShape();
		}

		// Token: 0x040008CD RID: 2253
		private static List<PropertyInformation> defaultProperties = new List<PropertyInformation>();
	}
}
