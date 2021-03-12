using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E98 RID: 3736
	internal class OtherAddressPropertyProvider : PhysicalAddressPropertyProvider
	{
		// Token: 0x06006148 RID: 24904 RVA: 0x0012F57D File Offset: 0x0012D77D
		public OtherAddressPropertyProvider() : base(OtherAddressPropertyProvider.OtherAddressPropertyInformationList)
		{
			base.IsMultiValueProperty = true;
		}

		// Token: 0x06006149 RID: 24905 RVA: 0x0012F594 File Offset: 0x0012D794
		protected override void GetProperty(Entity entity, PropertyDefinition property, ServiceObject ewsObject)
		{
			PhysicalAddress value = new PhysicalAddress
			{
				Street = ewsObject.GetValueOrDefault<string>(ContactSchema.PhysicalAddressOtherStreet),
				State = ewsObject.GetValueOrDefault<string>(ContactSchema.PhysicalAddressOtherState),
				City = ewsObject.GetValueOrDefault<string>(ContactSchema.PhysicalAddressOtherCity),
				CountryOrRegion = ewsObject.GetValueOrDefault<string>(ContactSchema.PhysicalAddressOtherCountryOrRegion),
				PostalCode = ewsObject.GetValueOrDefault<string>(ContactSchema.PhysicalAddressOtherPostalCode)
			};
			entity[property] = value;
		}

		// Token: 0x040034A8 RID: 13480
		internal static readonly ReadOnlyCollection<PropertyInformation> OtherAddressPropertyInformationList = new ReadOnlyCollection<PropertyInformation>(new List<PropertyInformation>
		{
			ContactSchema.PhysicalAddressOtherStreet,
			ContactSchema.PhysicalAddressOtherState,
			ContactSchema.PhysicalAddressOtherCity,
			ContactSchema.PhysicalAddressOtherCountryOrRegion,
			ContactSchema.PhysicalAddressOtherPostalCode
		});
	}
}
