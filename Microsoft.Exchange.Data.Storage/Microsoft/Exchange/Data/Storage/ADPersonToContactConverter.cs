using System;
using System.Collections;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200046D RID: 1133
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ADPersonToContactConverter
	{
		// Token: 0x060032E4 RID: 13028 RVA: 0x000CE96E File Offset: 0x000CCB6E
		protected ADPersonToContactConverter(PropertyDefinition contactProperty, ADPropertyDefinition[] adProperties)
		{
			this.contactProperty = contactProperty;
			this.adProperties = adProperties;
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x060032E5 RID: 13029 RVA: 0x000CE984 File Offset: 0x000CCB84
		public PropertyDefinition ContactProperty
		{
			get
			{
				return this.contactProperty;
			}
		}

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x060032E6 RID: 13030 RVA: 0x000CE98C File Offset: 0x000CCB8C
		public ADPropertyDefinition[] ADProperties
		{
			get
			{
				return this.adProperties;
			}
		}

		// Token: 0x060032E7 RID: 13031
		public abstract void Convert(ADRawEntry adObject, IStorePropertyBag contact);

		// Token: 0x060032E8 RID: 13032 RVA: 0x000CE994 File Offset: 0x000CCB94
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder((this.adProperties.Length + 1) * 30);
			stringBuilder.Append("Type=");
			stringBuilder.Append(base.GetType().Name);
			stringBuilder.Append(";ContactProperty=");
			stringBuilder.Append(this.ContactProperty.Name);
			if (this.adProperties.Length > 0)
			{
				stringBuilder.Append(";ADProperties=");
				stringBuilder.Append(this.adProperties[0].Name);
				for (int i = 1; i < this.adProperties.Length; i++)
				{
					stringBuilder.Append(",");
					stringBuilder.Append(this.adProperties[i].Name);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000CEA54 File Offset: 0x000CCC54
		public static string GetSipUri(ADRawEntry adEntry)
		{
			string text = null;
			object obj;
			if (adEntry.TryGetValueWithoutDefault(ADUserSchema.RTCSIPPrimaryUserAddress, out obj))
			{
				text = (obj as string);
			}
			object obj2;
			if (string.IsNullOrWhiteSpace(text) && adEntry.TryGetValueWithoutDefault(ADRecipientSchema.EmailAddresses, out obj2))
			{
				ProxyAddressCollection proxyAddressCollection = obj2 as ProxyAddressCollection;
				if (proxyAddressCollection != null)
				{
					text = proxyAddressCollection.GetSipUri();
				}
			}
			return text;
		}

		// Token: 0x04001B5E RID: 7006
		public static readonly ADPersonToContactConverter PersonType = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.PersonType, ADRecipientSchema.RecipientPersonType);

		// Token: 0x04001B5F RID: 7007
		public static readonly ADPersonToContactConverter DisplayName = new ADPersonToContactConverter.DisplayNameConverter();

		// Token: 0x04001B60 RID: 7008
		public static readonly ADPersonToContactConverter Surname = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.Surname, ADOrgPersonSchema.LastName);

		// Token: 0x04001B61 RID: 7009
		public static readonly ADPersonToContactConverter GivenName = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.GivenName, ADOrgPersonSchema.FirstName);

		// Token: 0x04001B62 RID: 7010
		public static readonly ADPersonToContactConverter FileAs = new ADPersonToContactConverter.DirectPropertyConverter(ContactBaseSchema.FileAs, ADRecipientSchema.SimpleDisplayName);

		// Token: 0x04001B63 RID: 7011
		public static readonly ADPersonToContactConverter EmailAddress = new ADPersonToContactConverter.SmtpAddressConverter(ContactSchema.Email1EmailAddress);

		// Token: 0x04001B64 RID: 7012
		public static readonly ADPersonToContactConverter BusinessHomePage = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.BusinessHomePage, ADRecipientSchema.WebPage);

		// Token: 0x04001B65 RID: 7013
		public static readonly ADPersonToContactConverter BusinessPhoneNumber = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.BusinessPhoneNumber, ADOrgPersonSchema.Phone);

		// Token: 0x04001B66 RID: 7014
		public static readonly ADPersonToContactConverter BusinessPhoneNumber2 = new ADPersonToContactConverter.MultiValueToSigleValuePropertyConverter(ContactSchema.BusinessPhoneNumber2, ADOrgPersonSchema.OtherTelephone);

		// Token: 0x04001B67 RID: 7015
		public static readonly ADPersonToContactConverter MobilePhone = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.MobilePhone, ADOrgPersonSchema.MobilePhone);

		// Token: 0x04001B68 RID: 7016
		public static readonly ADPersonToContactConverter HomePhone = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.HomePhone, ADOrgPersonSchema.HomePhone);

		// Token: 0x04001B69 RID: 7017
		public static readonly ADPersonToContactConverter HomePhone2 = new ADPersonToContactConverter.MultiValueToSigleValuePropertyConverter(ContactSchema.HomePhone2, ADOrgPersonSchema.OtherHomePhone);

		// Token: 0x04001B6A RID: 7018
		public static readonly ADPersonToContactConverter CompanyName = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.CompanyName, ADOrgPersonSchema.Company);

		// Token: 0x04001B6B RID: 7019
		public static readonly ADPersonToContactConverter Title = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.Title, ADOrgPersonSchema.Title);

		// Token: 0x04001B6C RID: 7020
		public static readonly ADPersonToContactConverter Department = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.Department, ADOrgPersonSchema.Department);

		// Token: 0x04001B6D RID: 7021
		public static readonly ADPersonToContactConverter OfficeLocation = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.OfficeLocation, ADOrgPersonSchema.Office);

		// Token: 0x04001B6E RID: 7022
		public static readonly ADPersonToContactConverter PostOfficeBox = new ADPersonToContactConverter.MultiValueToSigleValuePropertyConverter(ContactSchema.WorkPostOfficeBox, ADOrgPersonSchema.PostOfficeBox);

		// Token: 0x04001B6F RID: 7023
		public static readonly ADPersonToContactConverter Street = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.WorkAddressStreet, ADOrgPersonSchema.StreetAddress);

		// Token: 0x04001B70 RID: 7024
		public static readonly ADPersonToContactConverter Country = new ADPersonToContactConverter.CountryValueConverter();

		// Token: 0x04001B71 RID: 7025
		public static readonly ADPersonToContactConverter PostalCode = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.WorkAddressPostalCode, ADOrgPersonSchema.PostalCode);

		// Token: 0x04001B72 RID: 7026
		public static readonly ADPersonToContactConverter State = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.WorkAddressState, ADOrgPersonSchema.StateOrProvince);

		// Token: 0x04001B73 RID: 7027
		public static readonly ADPersonToContactConverter City = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.WorkAddressCity, ADOrgPersonSchema.City);

		// Token: 0x04001B74 RID: 7028
		public static readonly ADPersonToContactConverter Fax = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.WorkFax, ADOrgPersonSchema.Fax);

		// Token: 0x04001B75 RID: 7029
		public static readonly ADPersonToContactConverter AssistantName = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.AssistantName, ADRecipientSchema.AssistantName);

		// Token: 0x04001B76 RID: 7030
		public static readonly ADPersonToContactConverter YomiCompany = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.YomiCompany, ADRecipientSchema.PhoneticCompany);

		// Token: 0x04001B77 RID: 7031
		public static readonly ADPersonToContactConverter YomiFirstName = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.YomiFirstName, ADRecipientSchema.PhoneticFirstName);

		// Token: 0x04001B78 RID: 7032
		public static readonly ADPersonToContactConverter YomiLastName = new ADPersonToContactConverter.DirectPropertyConverter(ContactSchema.YomiLastName, ADRecipientSchema.PhoneticLastName);

		// Token: 0x04001B79 RID: 7033
		public static readonly ADPersonToContactConverter IMAddress = new ADPersonToContactConverter.IMAddressConverter();

		// Token: 0x04001B7A RID: 7034
		public static readonly ADPersonToContactConverter HomeLocationSource = new ADPersonToContactConverter.GeoCoordinatesConverter();

		// Token: 0x04001B7B RID: 7035
		public static readonly ADPersonToContactConverter RecipientType = new ADPersonToContactConverter.RecipientTypeConverter();

		// Token: 0x04001B7C RID: 7036
		public static readonly ADPersonToContactConverter[] AllConverters = new ADPersonToContactConverter[]
		{
			ADPersonToContactConverter.PersonType,
			ADPersonToContactConverter.DisplayName,
			ADPersonToContactConverter.Surname,
			ADPersonToContactConverter.GivenName,
			ADPersonToContactConverter.FileAs,
			ADPersonToContactConverter.EmailAddress,
			ADPersonToContactConverter.IMAddress,
			ADPersonToContactConverter.BusinessHomePage,
			ADPersonToContactConverter.BusinessPhoneNumber,
			ADPersonToContactConverter.BusinessPhoneNumber2,
			ADPersonToContactConverter.MobilePhone,
			ADPersonToContactConverter.HomePhone,
			ADPersonToContactConverter.HomePhone2,
			ADPersonToContactConverter.CompanyName,
			ADPersonToContactConverter.Title,
			ADPersonToContactConverter.Department,
			ADPersonToContactConverter.OfficeLocation,
			ADPersonToContactConverter.PostOfficeBox,
			ADPersonToContactConverter.Street,
			ADPersonToContactConverter.Country,
			ADPersonToContactConverter.PostalCode,
			ADPersonToContactConverter.State,
			ADPersonToContactConverter.City,
			ADPersonToContactConverter.Fax,
			ADPersonToContactConverter.AssistantName,
			ADPersonToContactConverter.YomiCompany,
			ADPersonToContactConverter.YomiFirstName,
			ADPersonToContactConverter.YomiLastName,
			ADPersonToContactConverter.HomeLocationSource,
			ADPersonToContactConverter.RecipientType
		};

		// Token: 0x04001B7D RID: 7037
		private static readonly Trace Tracer = ExTraceGlobals.ContactLinkingTracer;

		// Token: 0x04001B7E RID: 7038
		private readonly PropertyDefinition contactProperty;

		// Token: 0x04001B7F RID: 7039
		private readonly ADPropertyDefinition[] adProperties;

		// Token: 0x0200046E RID: 1134
		private abstract class SinglePropertyConverter : ADPersonToContactConverter
		{
			// Token: 0x060032EB RID: 13035 RVA: 0x000CEDF0 File Offset: 0x000CCFF0
			public SinglePropertyConverter(PropertyDefinition contactProperty, ADPropertyDefinition adProperty) : base(contactProperty, new ADPropertyDefinition[]
			{
				adProperty
			})
			{
			}

			// Token: 0x17001006 RID: 4102
			// (get) Token: 0x060032EC RID: 13036 RVA: 0x000CEE10 File Offset: 0x000CD010
			public ADPropertyDefinition ADProperty
			{
				get
				{
					return this.adProperties[0];
				}
			}
		}

		// Token: 0x0200046F RID: 1135
		private sealed class DirectPropertyConverter : ADPersonToContactConverter.SinglePropertyConverter
		{
			// Token: 0x060032ED RID: 13037 RVA: 0x000CEE1A File Offset: 0x000CD01A
			public DirectPropertyConverter(PropertyDefinition contactProperty, ADPropertyDefinition adProperty) : base(contactProperty, adProperty)
			{
			}

			// Token: 0x060032EE RID: 13038 RVA: 0x000CEE24 File Offset: 0x000CD024
			public override void Convert(ADRawEntry adObject, IStorePropertyBag contact)
			{
				Util.ThrowOnNullArgument(adObject, "adObject");
				Util.ThrowOnNullArgument(contact, "contact");
				object obj;
				if (adObject.TryGetValueWithoutDefault(base.ADProperty, out obj))
				{
					contact[base.ContactProperty] = obj;
					ADPersonToContactConverter.Tracer.TraceDebug<string, string, object>(0L, "Setting contact property {0} with value from AD property {1}. Value: {2}", base.ADProperty.Name, base.ContactProperty.Name, obj);
					return;
				}
				ADPersonToContactConverter.Tracer.TraceDebug<string, string>(0L, "Deleting contact property {0} since AD property {1} not found.", base.ContactProperty.Name, base.ADProperty.Name);
				contact.Delete(base.ContactProperty);
			}
		}

		// Token: 0x02000470 RID: 1136
		private sealed class DisplayNameConverter : ADPersonToContactConverter.SinglePropertyConverter
		{
			// Token: 0x060032EF RID: 13039 RVA: 0x000CEEC0 File Offset: 0x000CD0C0
			public DisplayNameConverter() : base(StoreObjectSchema.DisplayName, ADRecipientSchema.DisplayName)
			{
			}

			// Token: 0x060032F0 RID: 13040 RVA: 0x000CEED4 File Offset: 0x000CD0D4
			public override void Convert(ADRawEntry adObject, IStorePropertyBag contact)
			{
				Util.ThrowOnNullArgument(adObject, "adObject");
				Util.ThrowOnNullArgument(contact, "contact");
				object value;
				if (adObject.TryGetValueWithoutDefault(base.ADProperty, out value))
				{
					contact[StoreObjectSchema.DisplayName] = (contact[ContactBaseSchema.DisplayNameFirstLast] = (contact[ContactBaseSchema.DisplayNameLastFirst] = value));
					return;
				}
				ADPersonToContactConverter.Tracer.TraceDebug<string>(0L, "Deleting contact DisplayName properties since AD property {0} not found.", base.ADProperty.Name);
				contact.Delete(ContactBaseSchema.DisplayNameFirstLast);
				contact.Delete(ContactBaseSchema.DisplayNameLastFirst);
				contact.Delete(StoreObjectSchema.DisplayName);
			}
		}

		// Token: 0x02000471 RID: 1137
		private sealed class SmtpAddressConverter : ADPersonToContactConverter.SinglePropertyConverter
		{
			// Token: 0x060032F1 RID: 13041 RVA: 0x000CEF6D File Offset: 0x000CD16D
			public SmtpAddressConverter(PropertyDefinition contactProperty) : base(contactProperty, ADRecipientSchema.EmailAddresses)
			{
			}

			// Token: 0x060032F2 RID: 13042 RVA: 0x000CEF7C File Offset: 0x000CD17C
			public override void Convert(ADRawEntry adObject, IStorePropertyBag contact)
			{
				Util.ThrowOnNullArgument(adObject, "adObject");
				Util.ThrowOnNullArgument(contact, "contact");
				string text = null;
				object obj;
				if (adObject.TryGetValueWithoutDefault(ADRecipientSchema.EmailAddresses, out obj))
				{
					ProxyAddressCollection proxyAddressCollection = obj as ProxyAddressCollection;
					if (proxyAddressCollection != null)
					{
						foreach (ProxyAddress proxyAddress in proxyAddressCollection)
						{
							SmtpProxyAddress smtpProxyAddress = proxyAddress as SmtpProxyAddress;
							if (smtpProxyAddress != null)
							{
								if (smtpProxyAddress.IsPrimaryAddress)
								{
									text = smtpProxyAddress.SmtpAddress;
									break;
								}
								if (text == null)
								{
									text = smtpProxyAddress.SmtpAddress;
								}
							}
						}
					}
				}
				if (text != null)
				{
					contact[base.ContactProperty] = text;
					return;
				}
				ADPersonToContactConverter.Tracer.TraceDebug<string>(0L, "Deleting contact property {0} since AD Object has no email addresses.", base.ContactProperty.Name);
				contact.Delete(base.ContactProperty);
			}
		}

		// Token: 0x02000472 RID: 1138
		private sealed class IMAddressConverter : ADPersonToContactConverter
		{
			// Token: 0x060032F3 RID: 13043 RVA: 0x000CF060 File Offset: 0x000CD260
			public IMAddressConverter() : base(ContactSchema.IMAddress, ADPersonToContactConverter.IMAddressConverter.requiredAdProperties)
			{
			}

			// Token: 0x060032F4 RID: 13044 RVA: 0x000CF074 File Offset: 0x000CD274
			public override void Convert(ADRawEntry adObject, IStorePropertyBag contact)
			{
				Util.ThrowOnNullArgument(adObject, "adObject");
				Util.ThrowOnNullArgument(contact, "contact");
				string sipUri = ADPersonToContactConverter.GetSipUri(adObject);
				if (sipUri != null)
				{
					contact[base.ContactProperty] = sipUri;
					return;
				}
				ADPersonToContactConverter.Tracer.TraceDebug<string>(0L, "Deleting contact property {0} since AD Object has no SIP addresses.", base.ContactProperty.Name);
				contact.Delete(base.ContactProperty);
			}

			// Token: 0x04001B80 RID: 7040
			private static readonly ADPropertyDefinition[] requiredAdProperties = new ADPropertyDefinition[]
			{
				ADUserSchema.RTCSIPPrimaryUserAddress,
				ADRecipientSchema.EmailAddresses
			};
		}

		// Token: 0x02000473 RID: 1139
		private sealed class RecipientTypeConverter : ADPersonToContactConverter.SinglePropertyConverter
		{
			// Token: 0x060032F6 RID: 13046 RVA: 0x000CF102 File Offset: 0x000CD302
			public RecipientTypeConverter() : base(InternalSchema.InternalPersonType, ADRecipientSchema.RecipientPersonType)
			{
			}

			// Token: 0x060032F7 RID: 13047 RVA: 0x000CF114 File Offset: 0x000CD314
			public override void Convert(ADRawEntry adObject, IStorePropertyBag contact)
			{
				Util.ThrowOnNullArgument(adObject, "adObject");
				Util.ThrowOnNullArgument(contact, "contact");
				PersonType personType = (PersonType)adObject[ADRecipientSchema.RecipientPersonType];
				contact[InternalSchema.InternalPersonType] = (int)personType;
			}
		}

		// Token: 0x02000474 RID: 1140
		private sealed class GeoCoordinatesConverter : ADPersonToContactConverter
		{
			// Token: 0x060032F8 RID: 13048 RVA: 0x000CF159 File Offset: 0x000CD359
			public GeoCoordinatesConverter() : base(ContactSchema.HomeLocationSource, ADPersonToContactConverter.GeoCoordinatesConverter.RequiredAdProperties)
			{
			}

			// Token: 0x060032F9 RID: 13049 RVA: 0x000CF16C File Offset: 0x000CD36C
			public override void Convert(ADRawEntry adObject, IStorePropertyBag contact)
			{
				Util.ThrowOnNullArgument(adObject, "adObject");
				Util.ThrowOnNullArgument(contact, "contact");
				object obj;
				if (adObject.TryGetValueWithoutDefault(ADRecipientSchema.GeoCoordinates, out obj))
				{
					GeoCoordinates geoCoordinates = (GeoCoordinates)obj;
					contact[ContactSchema.HomeLatitude] = geoCoordinates.Latitude;
					contact[ContactSchema.HomeLongitude] = geoCoordinates.Longitude;
					contact[ContactSchema.HomeLocationSource] = LocationSource.Contact;
					if (geoCoordinates.Altitude != null)
					{
						contact[ContactSchema.HomeAltitude] = geoCoordinates.Altitude;
					}
					else
					{
						ADPersonToContactConverter.Tracer.TraceDebug(0L, "Deleting contact HomeAltitude property it is not found on AD GeoCoordinates property.");
						contact.Delete(ContactSchema.HomeAltitude);
					}
					ADPersonToContactConverter.GeoCoordinatesConverter.locationUriConverter.Convert(adObject, contact);
					return;
				}
				ADPersonToContactConverter.Tracer.TraceDebug(0L, "Deleting contact location properties since AD GeoCoordinates property not found.");
				contact.Delete(ContactSchema.HomeLatitude);
				contact.Delete(ContactSchema.HomeLongitude);
				contact.Delete(ContactSchema.HomeLocationSource);
				contact.Delete(ContactSchema.HomeAltitude);
				ADPersonToContactConverter.GeoCoordinatesConverter.locationUriConverter.Convert(adObject, contact);
			}

			// Token: 0x04001B81 RID: 7041
			private static readonly ADPropertyDefinition[] RequiredAdProperties = new ADPropertyDefinition[]
			{
				ADRecipientSchema.Latitude,
				ADRecipientSchema.Longitude,
				ADRecipientSchema.Altitude,
				ADRecipientSchema.PrimarySmtpAddress
			};

			// Token: 0x04001B82 RID: 7042
			private static readonly ADPersonToContactConverter.SmtpAddressConverter locationUriConverter = new ADPersonToContactConverter.SmtpAddressConverter(ContactSchema.HomeLocationUri);
		}

		// Token: 0x02000475 RID: 1141
		private sealed class MultiValueToSigleValuePropertyConverter : ADPersonToContactConverter.SinglePropertyConverter
		{
			// Token: 0x060032FB RID: 13051 RVA: 0x000CF2C9 File Offset: 0x000CD4C9
			public MultiValueToSigleValuePropertyConverter(PropertyDefinition contactProperty, ADPropertyDefinition adProperty) : base(contactProperty, adProperty)
			{
			}

			// Token: 0x060032FC RID: 13052 RVA: 0x000CF2D4 File Offset: 0x000CD4D4
			public override void Convert(ADRawEntry adObject, IStorePropertyBag contact)
			{
				Util.ThrowOnNullArgument(adObject, "adObject");
				Util.ThrowOnNullArgument(contact, "contact");
				object obj = null;
				object obj2;
				if (adObject.TryGetValueWithoutDefault(base.ADProperty, out obj2))
				{
					IList list = obj2 as IList;
					if (list != null && list.Count > 0)
					{
						obj = list[0];
					}
				}
				if (obj != null)
				{
					contact[base.ContactProperty] = obj;
					ADPersonToContactConverter.Tracer.TraceDebug<string, string, object>(0L, "Setting contact property {0} with value from AD property {1}. Value: {2}", base.ADProperty.Name, base.ContactProperty.Name, obj);
					return;
				}
				ADPersonToContactConverter.Tracer.TraceDebug<string, string>(0L, "Deleting contact property {0} since AD property {1} not found.", base.ContactProperty.Name, base.ADProperty.Name);
				contact.Delete(base.ContactProperty);
			}
		}

		// Token: 0x02000476 RID: 1142
		private sealed class CountryValueConverter : ADPersonToContactConverter.SinglePropertyConverter
		{
			// Token: 0x060032FD RID: 13053 RVA: 0x000CF390 File Offset: 0x000CD590
			public CountryValueConverter() : base(ContactSchema.WorkAddressCountry, ADOrgPersonSchema.CountryOrRegion)
			{
			}

			// Token: 0x060032FE RID: 13054 RVA: 0x000CF3A4 File Offset: 0x000CD5A4
			public override void Convert(ADRawEntry adObject, IStorePropertyBag contact)
			{
				object obj;
				if (adObject.TryGetValueWithoutDefault(base.ADProperty, out obj))
				{
					CountryInfo countryInfo = (CountryInfo)obj;
					contact[base.ContactProperty] = countryInfo.LocalizedDisplayName.ToString();
					return;
				}
				contact.Delete(base.ContactProperty);
			}
		}
	}
}
