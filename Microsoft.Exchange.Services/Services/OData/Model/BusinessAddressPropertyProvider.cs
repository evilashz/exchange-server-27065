using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E97 RID: 3735
	internal class BusinessAddressPropertyProvider : PhysicalAddressPropertyProvider
	{
		// Token: 0x06006145 RID: 24901 RVA: 0x0012F49D File Offset: 0x0012D69D
		public BusinessAddressPropertyProvider() : base(BusinessAddressPropertyProvider.BusinessAddressPropertyInformationList)
		{
			base.IsMultiValueProperty = true;
		}

		// Token: 0x06006146 RID: 24902 RVA: 0x0012F4B4 File Offset: 0x0012D6B4
		protected override void GetProperty(Entity entity, PropertyDefinition property, ServiceObject ewsObject)
		{
			PhysicalAddress value = new PhysicalAddress
			{
				Street = ewsObject.GetValueOrDefault<string>(ContactSchema.PhysicalAddressBusinessStreet),
				State = ewsObject.GetValueOrDefault<string>(ContactSchema.PhysicalAddressBusinessState),
				City = ewsObject.GetValueOrDefault<string>(ContactSchema.PhysicalAddressBusinessCity),
				CountryOrRegion = ewsObject.GetValueOrDefault<string>(ContactSchema.PhysicalAddressBusinessCountryOrRegion),
				PostalCode = ewsObject.GetValueOrDefault<string>(ContactSchema.PhysicalAddressBusinessPostalCode)
			};
			entity[property] = value;
		}

		// Token: 0x040034A7 RID: 13479
		private static readonly ReadOnlyCollection<PropertyInformation> BusinessAddressPropertyInformationList = new ReadOnlyCollection<PropertyInformation>(new List<PropertyInformation>
		{
			ContactSchema.PhysicalAddressBusinessStreet,
			ContactSchema.PhysicalAddressBusinessState,
			ContactSchema.PhysicalAddressBusinessCity,
			ContactSchema.PhysicalAddressBusinessCountryOrRegion,
			ContactSchema.PhysicalAddressBusinessPostalCode
		});
	}
}
