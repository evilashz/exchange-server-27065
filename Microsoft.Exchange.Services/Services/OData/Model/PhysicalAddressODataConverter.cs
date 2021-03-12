using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E7E RID: 3710
	internal class PhysicalAddressODataConverter : IODataPropertyValueConverter
	{
		// Token: 0x0600607C RID: 24700 RVA: 0x0012D285 File Offset: 0x0012B485
		public object FromODataPropertyValue(object odataPropertyValue)
		{
			return PhysicalAddressODataConverter.ODataValueToPhysicalAddress((ODataComplexValue)odataPropertyValue);
		}

		// Token: 0x0600607D RID: 24701 RVA: 0x0012D292 File Offset: 0x0012B492
		public object ToODataPropertyValue(object rawValue)
		{
			return PhysicalAddressODataConverter.PhysicalAddressToODataValue((PhysicalAddress)rawValue);
		}

		// Token: 0x0600607E RID: 24702 RVA: 0x0012D2A0 File Offset: 0x0012B4A0
		internal static ODataValue PhysicalAddressToODataValue(PhysicalAddress address)
		{
			if (address == null)
			{
				return null;
			}
			ODataComplexValue odataComplexValue = new ODataComplexValue();
			odataComplexValue.TypeName = typeof(PhysicalAddress).FullName;
			List<ODataProperty> properties = new List<ODataProperty>
			{
				new ODataProperty
				{
					Name = PhysicalAddressFields.Street.ToString(),
					Value = address.Street
				},
				new ODataProperty
				{
					Name = PhysicalAddressFields.City.ToString(),
					Value = address.City
				},
				new ODataProperty
				{
					Name = PhysicalAddressFields.State.ToString(),
					Value = address.State
				},
				new ODataProperty
				{
					Name = PhysicalAddressFields.CountryOrRegion.ToString(),
					Value = address.CountryOrRegion
				},
				new ODataProperty
				{
					Name = PhysicalAddressFields.PostalCode.ToString(),
					Value = address.PostalCode
				}
			};
			odataComplexValue.Properties = properties;
			return odataComplexValue;
		}

		// Token: 0x0600607F RID: 24703 RVA: 0x0012D438 File Offset: 0x0012B638
		internal static PhysicalAddress ODataValueToPhysicalAddress(ODataComplexValue complexValue)
		{
			if (complexValue == null)
			{
				return null;
			}
			PhysicalAddress physicalAddress = new PhysicalAddress();
			physicalAddress.Street = complexValue.GetPropertyValue(PhysicalAddressFields.Street.ToString(), null);
			physicalAddress.City = complexValue.GetPropertyValue(PhysicalAddressFields.City.ToString(), null);
			physicalAddress.State = complexValue.GetPropertyValue(PhysicalAddressFields.State.ToString(), null);
			physicalAddress.CountryOrRegion = complexValue.GetPropertyValue(PhysicalAddressFields.CountryOrRegion.ToString(), null);
			physicalAddress.PostalCode = complexValue.GetPropertyValue(PhysicalAddressFields.PostalCode.ToString(), null);
			if (complexValue.Properties.Any((ODataProperty x) => x.Name.Equals(PhysicalAddressFields.Street.ToString())))
			{
				physicalAddress.Properties.Add(PhysicalAddressFields.Street);
			}
			if (complexValue.Properties.Any((ODataProperty x) => x.Name.Equals(PhysicalAddressFields.City.ToString())))
			{
				physicalAddress.Properties.Add(PhysicalAddressFields.City);
			}
			if (complexValue.Properties.Any((ODataProperty x) => x.Name.Equals(PhysicalAddressFields.State.ToString())))
			{
				physicalAddress.Properties.Add(PhysicalAddressFields.State);
			}
			if (complexValue.Properties.Any((ODataProperty x) => x.Name.Equals(PhysicalAddressFields.CountryOrRegion.ToString())))
			{
				physicalAddress.Properties.Add(PhysicalAddressFields.CountryOrRegion);
			}
			if (complexValue.Properties.Any((ODataProperty x) => x.Name.Equals(PhysicalAddressFields.PostalCode.ToString())))
			{
				physicalAddress.Properties.Add(PhysicalAddressFields.PostalCode);
			}
			return physicalAddress;
		}
	}
}
