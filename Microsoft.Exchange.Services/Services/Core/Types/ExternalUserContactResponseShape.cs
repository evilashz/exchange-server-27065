using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200077C RID: 1916
	internal class ExternalUserContactResponseShape : ExternalUserResponseShape
	{
		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x0600393D RID: 14653 RVA: 0x000CA805 File Offset: 0x000C8A05
		protected override List<PropertyPath> PropertiesAllowedForReadAccess
		{
			get
			{
				return ExternalUserContactResponseShape.properties;
			}
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x000CA80C File Offset: 0x000C8A0C
		public ExternalUserContactResponseShape(Permission permissionGranted)
		{
			base.Permissions = permissionGranted;
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000CA81B File Offset: 0x000C8A1B
		protected override PropertyPath[] GetPropertiesForCustomPermissions(ItemResponseShape requestedResponseShape)
		{
			return null;
		}

		// Token: 0x04001FEE RID: 8174
		private static List<PropertyPath> properties = new List<PropertyPath>
		{
			ItemSchema.Body.PropertyPath,
			ItemSchema.Categories.PropertyPath,
			ItemSchema.ItemId.PropertyPath,
			ItemSchema.Sensitivity.PropertyPath,
			ContactSchema.AssistantName.PropertyPath,
			ContactSchema.Birthday.PropertyPath,
			ContactSchema.BusinessHomePage.PropertyPath,
			ContactSchema.Children.PropertyPath,
			ContactSchema.CompanyName.PropertyPath,
			ContactSchema.CompleteName.PropertyPath,
			ContactSchema.Department.PropertyPath,
			ContactSchema.DisplayName.PropertyPath,
			ContactSchema.EmailAddressEmailAddress1.PropertyPath,
			ContactSchema.EmailAddressEmailAddress2.PropertyPath,
			ContactSchema.EmailAddressEmailAddress3.PropertyPath,
			ContactSchema.FileAs.PropertyPath,
			ContactSchema.FileAsMapping.PropertyPath,
			ContactSchema.Generation.PropertyPath,
			ContactSchema.GivenName.PropertyPath,
			ContactSchema.ImAddressImAddress1.PropertyPath,
			ContactSchema.Initials.PropertyPath,
			ContactSchema.JobTitle.PropertyPath,
			ContactSchema.Manager.PropertyPath,
			ContactSchema.MiddleName.PropertyPath,
			ContactSchema.Mileage.PropertyPath,
			ContactSchema.Nickname.PropertyPath,
			ContactSchema.OfficeLocation.PropertyPath,
			ContactSchema.PhoneNumberAssistantPhone.PropertyPath,
			ContactSchema.PhoneNumberBusinessFax.PropertyPath,
			ContactSchema.PhoneNumberBusinessPhone.PropertyPath,
			ContactSchema.PhoneNumberBusinessPhone2.PropertyPath,
			ContactSchema.PhoneNumberCallback.PropertyPath,
			ContactSchema.PhoneNumberCarPhone.PropertyPath,
			ContactSchema.PhoneNumberCompanyMainPhone.PropertyPath,
			ContactSchema.PhoneNumberHomeFax.PropertyPath,
			ContactSchema.PhoneNumberHomePhone.PropertyPath,
			ContactSchema.PhoneNumberHomePhone2.PropertyPath,
			ContactSchema.PhoneNumberIsdn.PropertyPath,
			ContactSchema.PhoneNumberMobilePhone.PropertyPath,
			ContactSchema.PhoneNumberOtherFax.PropertyPath,
			ContactSchema.PhoneNumberOtherTelephone.PropertyPath,
			ContactSchema.PhoneNumberPager.PropertyPath,
			ContactSchema.PhoneNumberPrimaryPhone.PropertyPath,
			ContactSchema.PhoneNumberRadioPhone.PropertyPath,
			ContactSchema.PhoneNumberTelex.PropertyPath,
			ContactSchema.PhoneNumberTtyTddPhone.PropertyPath,
			ContactSchema.PhysicalAddressBusinessCity.PropertyPath,
			ContactSchema.PhysicalAddressBusinessCountryOrRegion.PropertyPath,
			ContactSchema.PhysicalAddressBusinessPostalCode.PropertyPath,
			ContactSchema.PhysicalAddressBusinessState.PropertyPath,
			ContactSchema.PhysicalAddressBusinessStreet.PropertyPath,
			ContactSchema.PhysicalAddressHomeCity.PropertyPath,
			ContactSchema.PhysicalAddressHomeCountryOrRegion.PropertyPath,
			ContactSchema.PhysicalAddressHomePostalCode.PropertyPath,
			ContactSchema.PhysicalAddressHomeState.PropertyPath,
			ContactSchema.PhysicalAddressHomeStreet.PropertyPath,
			ContactSchema.PhysicalAddressOtherCity.PropertyPath,
			ContactSchema.PhysicalAddressOtherCountryOrRegion.PropertyPath,
			ContactSchema.PhysicalAddressOtherPostalCode.PropertyPath,
			ContactSchema.PhysicalAddressOtherState.PropertyPath,
			ContactSchema.PhysicalAddressOtherStreet.PropertyPath,
			ContactSchema.PostalAddressIndex.PropertyPath,
			ContactSchema.Profession.PropertyPath,
			ContactSchema.SpouseName.PropertyPath,
			ContactSchema.Surname.PropertyPath,
			ContactSchema.WeddingAnniversary.PropertyPath,
			ContactSchema.HasPicture.PropertyPath
		};

		// Token: 0x04001FEF RID: 8175
		public static List<PropertyPath> PrivateItemProperties = new List<PropertyPath>
		{
			ItemSchema.ItemId.PropertyPath,
			ItemSchema.Sensitivity.PropertyPath
		};
	}
}
