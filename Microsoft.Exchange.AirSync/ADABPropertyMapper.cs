using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ABProviderFramework;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000AA RID: 170
	internal static class ADABPropertyMapper
	{
		// Token: 0x0600095D RID: 2397 RVA: 0x00036D04 File Offset: 0x00034F04
		public static ADPropertyDefinition GetADPropertyDefinition(ABPropertyDefinition addressBookProperty)
		{
			ADPropertyDefinition result;
			if (ADABPropertyMapper.propertyMap.TryGetValue(addressBookProperty, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00036D24 File Offset: 0x00034F24
		internal static ADPropertyDefinition[] ConvertToADProperties(ABPropertyDefinitionCollection properties)
		{
			object nativePropertyCollection = properties.GetNativePropertyCollection("AD");
			if (nativePropertyCollection != null)
			{
				return (ADPropertyDefinition[])nativePropertyCollection;
			}
			List<ADPropertyDefinition> list = new List<ADPropertyDefinition>(properties.Count);
			foreach (ABPropertyDefinition addressBookProperty in properties)
			{
				ADABPropertyMapper.AddCorrespondingADPropertyToList(list, addressBookProperty);
			}
			ADPropertyDefinition[] array = list.ToArray();
			properties.SetNativePropertyCollection("AD", array);
			return array;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00036DA8 File Offset: 0x00034FA8
		private static void AddCorrespondingADPropertyToList(List<ADPropertyDefinition> activeDirectoryProperties, ABPropertyDefinition addressBookProperty)
		{
			ADPropertyDefinition adpropertyDefinition = ADABPropertyMapper.GetADPropertyDefinition(addressBookProperty);
			if (adpropertyDefinition != null)
			{
				activeDirectoryProperties.Add(adpropertyDefinition);
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00036DC8 File Offset: 0x00034FC8
		private static Dictionary<ABPropertyDefinition, ADPropertyDefinition> CreateMappingDictionary()
		{
			Dictionary<ABPropertyDefinition, ADPropertyDefinition> dictionary = new Dictionary<ABPropertyDefinition, ADPropertyDefinition>();
			foreach (ADABPropertyMapper.ABPropertyMapEntry abpropertyMapEntry in ADABPropertyMapper.propertyMapArray)
			{
				dictionary.Add(abpropertyMapEntry.AddressBookProperty, abpropertyMapEntry.ActiveDirectoryProperty);
			}
			return dictionary;
		}

		// Token: 0x040005DE RID: 1502
		private static ADABPropertyMapper.ABPropertyMapEntry[] propertyMapArray = new ADABPropertyMapper.ABPropertyMapEntry[]
		{
			new ADABPropertyMapper.ABPropertyMapEntry(ABObjectSchema.Alias, ADRecipientSchema.Alias),
			new ADABPropertyMapper.ABPropertyMapEntry(ABObjectSchema.DisplayName, ADRecipientSchema.DisplayName),
			new ADABPropertyMapper.ABPropertyMapEntry(ABObjectSchema.LegacyExchangeDN, ADRecipientSchema.LegacyExchangeDN),
			new ADABPropertyMapper.ABPropertyMapEntry(ABObjectSchema.CanEmail, ADRecipientSchema.RecipientType),
			new ADABPropertyMapper.ABPropertyMapEntry(ABObjectSchema.Id, ADObjectSchema.Id),
			new ADABPropertyMapper.ABPropertyMapEntry(ABObjectSchema.EmailAddress, ADRecipientSchema.PrimarySmtpAddress),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.GivenName, ADOrgPersonSchema.FirstName),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.Surname, ADOrgPersonSchema.LastName),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.Initials, ADOrgPersonSchema.Initials),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.BusinessPhoneNumber, ADOrgPersonSchema.Phone),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.BusinessFaxNumber, ADOrgPersonSchema.Fax),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.OfficeLocation, ADOrgPersonSchema.Office),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.CompanyName, ADOrgPersonSchema.Company),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.DepartmentName, ADOrgPersonSchema.Department),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.HomePhoneNumber, ADOrgPersonSchema.HomePhone),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.MobilePhoneNumber, ADOrgPersonSchema.MobilePhone),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.Title, ADOrgPersonSchema.Title),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.WorkAddressCity, ADOrgPersonSchema.City),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.WorkAddressCountry, ADOrgPersonSchema.Co),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.WorkAddressPostalCode, ADOrgPersonSchema.PostalCode),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.WorkAddressPostOfficeBox, ADOrgPersonSchema.PostOfficeBox),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.WorkAddressState, ADOrgPersonSchema.StateOrProvince),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.WorkAddressStreet, ADOrgPersonSchema.StreetAddress),
			new ADABPropertyMapper.ABPropertyMapEntry(ABContactSchema.WebPage, ADRecipientSchema.WebPage),
			new ADABPropertyMapper.ABPropertyMapEntry(ABGroupSchema.HiddenMembership, ADGroupSchema.HiddenGroupMembershipEnabled),
			new ADABPropertyMapper.ABPropertyMapEntry(ABGroupSchema.OwnerId, ADGroupSchema.ManagedBy)
		};

		// Token: 0x040005DF RID: 1503
		private static Dictionary<ABPropertyDefinition, ADPropertyDefinition> propertyMap = ADABPropertyMapper.CreateMappingDictionary();

		// Token: 0x020000AB RID: 171
		private struct ABPropertyMapEntry
		{
			// Token: 0x06000962 RID: 2402 RVA: 0x00037108 File Offset: 0x00035308
			public ABPropertyMapEntry(ABPropertyDefinition addressBookProperty, ADPropertyDefinition activeDirectoryProperty)
			{
				if (addressBookProperty == null)
				{
					throw new ArgumentNullException("addressBookProperty");
				}
				if (activeDirectoryProperty == null)
				{
					throw new ArgumentNullException("activeDirectoryProperty");
				}
				this.addressBookProperty = addressBookProperty;
				this.activeDirectoryProperty = activeDirectoryProperty;
			}

			// Token: 0x17000372 RID: 882
			// (get) Token: 0x06000963 RID: 2403 RVA: 0x00037134 File Offset: 0x00035334
			public ABPropertyDefinition AddressBookProperty
			{
				get
				{
					return this.addressBookProperty;
				}
			}

			// Token: 0x17000373 RID: 883
			// (get) Token: 0x06000964 RID: 2404 RVA: 0x0003713C File Offset: 0x0003533C
			public ADPropertyDefinition ActiveDirectoryProperty
			{
				get
				{
					return this.activeDirectoryProperty;
				}
			}

			// Token: 0x040005E0 RID: 1504
			private ABPropertyDefinition addressBookProperty;

			// Token: 0x040005E1 RID: 1505
			private ADPropertyDefinition activeDirectoryProperty;
		}
	}
}
