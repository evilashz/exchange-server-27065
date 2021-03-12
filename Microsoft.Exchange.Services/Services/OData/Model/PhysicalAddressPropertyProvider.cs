﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E95 RID: 3733
	internal class PhysicalAddressPropertyProvider : SimpleEwsPropertyProvider
	{
		// Token: 0x0600613E RID: 24894 RVA: 0x0012F1AF File Offset: 0x0012D3AF
		public PhysicalAddressPropertyProvider(ReadOnlyCollection<PropertyInformation> physicalAddressPropertyInformationList) : base(physicalAddressPropertyInformationList)
		{
			base.IsMultiValueProperty = true;
		}

		// Token: 0x0600613F RID: 24895 RVA: 0x0012F1C0 File Offset: 0x0012D3C0
		public override List<PropertyUpdate> GetPropertyUpdateList(Entity entity, PropertyDefinition property, object value)
		{
			PhysicalAddress physicalAddress = entity[property] as PhysicalAddress;
			List<PropertyUpdate> list = new List<PropertyUpdate>();
			ArgumentValidator.ThrowIfNull("GetPropertyUpdateList:address", physicalAddress);
			foreach (PropertyInformation propertyInformation in base.PropertyInformationList)
			{
				string text;
				if (this.TryGetComplexPropertyValue(propertyInformation.PropertyPath, physicalAddress, out text))
				{
					ContactItemType contactItemType = EwsServiceObjectFactory.CreateServiceObject<ContactItemType>(entity);
					contactItemType[propertyInformation] = text;
					PropertyUpdate propertyUpdate;
					if (text != null)
					{
						propertyUpdate = EwsPropertyProvider.SetItemPropertyUpdateDelegate(contactItemType);
					}
					else
					{
						propertyUpdate = EwsPropertyProvider.DeleteItemPropertyUpdateDelegate(contactItemType);
					}
					propertyUpdate.PropertyPath = propertyInformation.PropertyPath;
					list.Add(propertyUpdate);
				}
			}
			return list;
		}

		// Token: 0x06006140 RID: 24896 RVA: 0x0012F284 File Offset: 0x0012D484
		protected override void SetProperty(Entity entity, PropertyDefinition property, ServiceObject ewsObject)
		{
			PhysicalAddress physicalAddress = entity[property] as PhysicalAddress;
			ArgumentValidator.ThrowIfNull("SetProperty:address", physicalAddress);
			foreach (PropertyInformation propertyInformation in base.PropertyInformationList)
			{
				string text;
				if (this.TryGetComplexPropertyValue(propertyInformation.PropertyPath, physicalAddress, out text) && text != null)
				{
					ewsObject[propertyInformation] = text;
				}
			}
		}

		// Token: 0x06006141 RID: 24897 RVA: 0x0012F300 File Offset: 0x0012D500
		private bool TryGetComplexPropertyValue(PropertyPath propertypath, PhysicalAddress address, out string propertyValue)
		{
			DictionaryPropertyUriBase dictionaryPropertyUriBase = propertypath as DictionaryPropertyUriBase;
			propertyValue = null;
			if (dictionaryPropertyUriBase == null)
			{
				return false;
			}
			switch (dictionaryPropertyUriBase.FieldUri)
			{
			case DictionaryUriEnum.PhysicalAddressStreet:
				if (address.Properties.Contains(PhysicalAddressFields.Street))
				{
					propertyValue = address.Street;
					return true;
				}
				break;
			case DictionaryUriEnum.PhysicalAddressCity:
				if (address.Properties.Contains(PhysicalAddressFields.City))
				{
					propertyValue = address.City;
					return true;
				}
				break;
			case DictionaryUriEnum.PhysicalAddressState:
				if (address.Properties.Contains(PhysicalAddressFields.State))
				{
					propertyValue = address.State;
					return true;
				}
				break;
			case DictionaryUriEnum.PhysicalAddressCountryOrRegion:
				if (address.Properties.Contains(PhysicalAddressFields.CountryOrRegion))
				{
					propertyValue = address.CountryOrRegion;
					return true;
				}
				break;
			case DictionaryUriEnum.PhysicalAddressPostalCode:
				if (address.Properties.Contains(PhysicalAddressFields.PostalCode))
				{
					propertyValue = address.PostalCode;
					return true;
				}
				break;
			default:
				return false;
			}
			return false;
		}
	}
}
