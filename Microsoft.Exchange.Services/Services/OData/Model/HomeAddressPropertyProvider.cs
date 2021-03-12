using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E96 RID: 3734
	internal class HomeAddressPropertyProvider : PhysicalAddressPropertyProvider
	{
		// Token: 0x06006142 RID: 24898 RVA: 0x0012F3C0 File Offset: 0x0012D5C0
		public HomeAddressPropertyProvider() : base(HomeAddressPropertyProvider.HomeAddressPropertyInformationList)
		{
			base.IsMultiValueProperty = true;
		}

		// Token: 0x06006143 RID: 24899 RVA: 0x0012F3D4 File Offset: 0x0012D5D4
		protected override void GetProperty(Entity entity, PropertyDefinition property, ServiceObject ewsObject)
		{
			PhysicalAddress value = new PhysicalAddress
			{
				Street = ewsObject.GetValueOrDefault<string>(ContactSchema.PhysicalAddressHomeStreet),
				State = ewsObject.GetValueOrDefault<string>(ContactSchema.PhysicalAddressHomeState),
				City = ewsObject.GetValueOrDefault<string>(ContactSchema.PhysicalAddressHomeCity),
				CountryOrRegion = ewsObject.GetValueOrDefault<string>(ContactSchema.PhysicalAddressHomeCountryOrRegion),
				PostalCode = ewsObject.GetValueOrDefault<string>(ContactSchema.PhysicalAddressHomePostalCode)
			};
			entity[property] = value;
		}

		// Token: 0x040034A6 RID: 13478
		internal static readonly ReadOnlyCollection<PropertyInformation> HomeAddressPropertyInformationList = new ReadOnlyCollection<PropertyInformation>(new List<PropertyInformation>
		{
			ContactSchema.PhysicalAddressHomeStreet,
			ContactSchema.PhysicalAddressHomeState,
			ContactSchema.PhysicalAddressHomeCity,
			ContactSchema.PhysicalAddressHomeCountryOrRegion,
			ContactSchema.PhysicalAddressHomePostalCode
		});
	}
}
