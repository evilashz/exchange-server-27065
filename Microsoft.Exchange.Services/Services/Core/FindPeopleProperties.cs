using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.MailboxOperators;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002F2 RID: 754
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class FindPeopleProperties
	{
		// Token: 0x06001545 RID: 5445 RVA: 0x0006CB3C File Offset: 0x0006AD3C
		private static HashSet<PropertyPath> GetSupportedRequestProperties()
		{
			HashSet<PropertyPath> properties = Persona.DefaultPersonaPropertyList.GetProperties();
			foreach (PropertyPath item in FindPeopleProperties.AdditionalProperties)
			{
				properties.Add(item);
			}
			return properties;
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0006CCB8 File Offset: 0x0006AEB8
		private static HashSet<PropertyDefinition> GetSupportedRestrictionProperties()
		{
			var array = new <>f__AnonymousType1<StorePropertyDefinition, FastPropertyDefinition>[]
			{
				new
				{
					Property = ContactBaseSchema.DisplayNameFirstLast,
					FastProperty = FastDocumentSchema.DisplayName
				},
				new
				{
					Property = ContactSchema.Surname,
					FastProperty = FastDocumentSchema.LastName
				},
				new
				{
					Property = ContactSchema.GivenName,
					FastProperty = FastDocumentSchema.FirstName
				},
				new
				{
					Property = ContactBaseSchema.FileAs,
					FastProperty = FastDocumentSchema.FileAs
				},
				new
				{
					Property = ContactSchema.Email1EmailAddress,
					FastProperty = FastDocumentSchema.EmailAddress
				},
				new
				{
					Property = ContactSchema.Email2EmailAddress,
					FastProperty = FastDocumentSchema.EmailAddress
				},
				new
				{
					Property = ContactSchema.Email3EmailAddress,
					FastProperty = FastDocumentSchema.EmailAddress
				},
				new
				{
					Property = ContactSchema.IMAddress,
					FastProperty = FastDocumentSchema.IMAddress
				},
				new
				{
					Property = ContactSchema.IMAddress2,
					FastProperty = FastDocumentSchema.IMAddress
				},
				new
				{
					Property = ContactSchema.IMAddress3,
					FastProperty = FastDocumentSchema.IMAddress
				},
				new
				{
					Property = ContactSchema.BusinessPhoneNumber,
					FastProperty = FastDocumentSchema.BusinessPhoneNumber
				},
				new
				{
					Property = ContactSchema.BusinessPhoneNumber2,
					FastProperty = FastDocumentSchema.BusinessPhoneNumber
				},
				new
				{
					Property = ContactSchema.OrganizationMainPhone,
					FastProperty = FastDocumentSchema.BusinessMainPhone
				},
				new
				{
					Property = ContactSchema.CarPhone,
					FastProperty = FastDocumentSchema.CarPhoneNumber
				},
				new
				{
					Property = ContactSchema.MobilePhone,
					FastProperty = FastDocumentSchema.MobilePhoneNumber
				},
				new
				{
					Property = ContactSchema.MobilePhone2,
					FastProperty = FastDocumentSchema.MobilePhoneNumber
				},
				new
				{
					Property = ContactSchema.HomePhone,
					FastProperty = FastDocumentSchema.HomePhone
				},
				new
				{
					Property = ContactSchema.HomePhone2,
					FastProperty = FastDocumentSchema.HomePhone
				},
				new
				{
					Property = ContactSchema.PrimaryTelephoneNumber,
					FastProperty = FastDocumentSchema.PrimaryTelephoneNumber
				},
				new
				{
					Property = ContactSchema.CompanyName,
					FastProperty = FastDocumentSchema.CompanyName
				},
				new
				{
					Property = ContactSchema.Title,
					FastProperty = FastDocumentSchema.Title
				},
				new
				{
					Property = ContactSchema.Department,
					FastProperty = FastDocumentSchema.DepartmentName
				},
				new
				{
					Property = ContactSchema.OfficeLocation,
					FastProperty = FastDocumentSchema.OfficeLocation
				},
				new
				{
					Property = ContactSchema.HomePostOfficeBox,
					FastProperty = FastDocumentSchema.HomeAddress
				},
				new
				{
					Property = ContactSchema.HomeStreet,
					FastProperty = FastDocumentSchema.HomeAddress
				},
				new
				{
					Property = ContactSchema.HomeCountry,
					FastProperty = FastDocumentSchema.HomeAddress
				},
				new
				{
					Property = ContactSchema.HomePostalCode,
					FastProperty = FastDocumentSchema.HomeAddress
				},
				new
				{
					Property = ContactSchema.HomeState,
					FastProperty = FastDocumentSchema.HomeAddress
				},
				new
				{
					Property = ContactSchema.HomeCity,
					FastProperty = FastDocumentSchema.HomeAddress
				},
				new
				{
					Property = ContactSchema.WorkPostOfficeBox,
					FastProperty = FastDocumentSchema.BusinessAddress
				},
				new
				{
					Property = ContactSchema.WorkAddressCity,
					FastProperty = FastDocumentSchema.BusinessAddress
				},
				new
				{
					Property = ContactSchema.WorkAddressCountry,
					FastProperty = FastDocumentSchema.BusinessAddress
				},
				new
				{
					Property = ContactSchema.WorkAddressPostalCode,
					FastProperty = FastDocumentSchema.BusinessAddress
				},
				new
				{
					Property = ContactSchema.WorkAddressState,
					FastProperty = FastDocumentSchema.BusinessAddress
				},
				new
				{
					Property = ContactSchema.WorkAddressStreet,
					FastProperty = FastDocumentSchema.BusinessAddress
				},
				new
				{
					Property = ContactSchema.OtherPostOfficeBox,
					FastProperty = FastDocumentSchema.OtherAddress
				},
				new
				{
					Property = ContactSchema.OtherCity,
					FastProperty = FastDocumentSchema.OtherAddress
				},
				new
				{
					Property = ContactSchema.OtherCountry,
					FastProperty = FastDocumentSchema.OtherAddress
				},
				new
				{
					Property = ContactSchema.OtherPostalCode,
					FastProperty = FastDocumentSchema.OtherAddress
				},
				new
				{
					Property = ContactSchema.OtherState,
					FastProperty = FastDocumentSchema.OtherAddress
				},
				new
				{
					Property = ContactSchema.OtherStreet,
					FastProperty = FastDocumentSchema.OtherAddress
				},
				new
				{
					Property = ContactSchema.MiddleName,
					FastProperty = FastDocumentSchema.MiddleName
				},
				new
				{
					Property = ContactSchema.Nickname,
					FastProperty = FastDocumentSchema.Nickname
				},
				new
				{
					Property = ContactSchema.YomiCompany,
					FastProperty = FastDocumentSchema.YomiCompanyName
				},
				new
				{
					Property = ContactSchema.YomiFirstName,
					FastProperty = FastDocumentSchema.YomiFirstName
				},
				new
				{
					Property = ContactSchema.YomiLastName,
					FastProperty = FastDocumentSchema.YomiLastName
				}
			};
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			var array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				var <>f__AnonymousType = array2[i];
				PropertyDefinition property;
				if (!FindPeopleProperties.CompositeProperties.TryGetValue(<>f__AnonymousType.Property, out property))
				{
					property = <>f__AnonymousType.Property;
				}
				hashSet.Add(<>f__AnonymousType.Property);
			}
			return hashSet;
		}

		// Token: 0x04000E72 RID: 3698
		public static readonly Dictionary<PropertyDefinition, PropertyDefinition> CompositeProperties = new Dictionary<PropertyDefinition, PropertyDefinition>
		{
			{
				ContactSchema.WorkPostOfficeBox,
				ContactSchema.BusinessAddress
			},
			{
				ContactSchema.WorkAddressCity,
				ContactSchema.BusinessAddress
			},
			{
				ContactSchema.WorkAddressCountry,
				ContactSchema.BusinessAddress
			},
			{
				ContactSchema.WorkAddressPostalCode,
				ContactSchema.BusinessAddress
			},
			{
				ContactSchema.WorkAddressState,
				ContactSchema.BusinessAddress
			},
			{
				ContactSchema.WorkAddressStreet,
				ContactSchema.BusinessAddress
			},
			{
				ContactSchema.HomePostOfficeBox,
				ContactSchema.HomeAddress
			},
			{
				ContactSchema.HomeCity,
				ContactSchema.HomeAddress
			},
			{
				ContactSchema.HomeCountry,
				ContactSchema.HomeAddress
			},
			{
				ContactSchema.HomePostalCode,
				ContactSchema.HomeAddress
			},
			{
				ContactSchema.HomeState,
				ContactSchema.HomeAddress
			},
			{
				ContactSchema.HomeStreet,
				ContactSchema.HomeAddress
			},
			{
				ContactSchema.OtherPostOfficeBox,
				ContactSchema.OtherAddress
			},
			{
				ContactSchema.OtherCity,
				ContactSchema.OtherAddress
			},
			{
				ContactSchema.OtherCountry,
				ContactSchema.OtherAddress
			},
			{
				ContactSchema.OtherPostalCode,
				ContactSchema.OtherAddress
			},
			{
				ContactSchema.OtherState,
				ContactSchema.OtherAddress
			},
			{
				ContactSchema.OtherStreet,
				ContactSchema.OtherAddress
			}
		};

		// Token: 0x04000E73 RID: 3699
		private static readonly HashSet<PropertyPath> AdditionalProperties = new HashSet<PropertyPath>
		{
			PersonaSchema.Title.PropertyPath,
			PersonaSchema.ImAddresses.PropertyPath,
			PersonaSchema.Departments.PropertyPath,
			PersonaSchema.OfficeLocations.PropertyPath,
			PersonaSchema.BusinessAddresses.PropertyPath,
			PersonaSchema.HomeAddresses.PropertyPath,
			PersonaSchema.OtherAddresses.PropertyPath,
			PersonaSchema.Attributions.PropertyPath,
			PersonaSchema.Alias.PropertyPath
		};

		// Token: 0x04000E74 RID: 3700
		public static readonly HashSet<PropertyDefinition> SupportedRestrictionProperties = FindPeopleProperties.GetSupportedRestrictionProperties();

		// Token: 0x04000E75 RID: 3701
		public static readonly HashSet<PropertyPath> SupportedRequestProperties = FindPeopleProperties.GetSupportedRequestProperties();
	}
}
