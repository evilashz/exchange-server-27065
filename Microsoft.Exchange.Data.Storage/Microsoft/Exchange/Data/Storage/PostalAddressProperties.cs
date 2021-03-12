using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000550 RID: 1360
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class PostalAddressProperties
	{
		// Token: 0x06003975 RID: 14709 RVA: 0x000EBA50 File Offset: 0x000E9C50
		private static NativeStorePropertyDefinition[] GetAllProperties(PostalAddressProperties[] propertySets, Func<PostalAddressProperties, NativeStorePropertyDefinition[]> getProperties)
		{
			List<NativeStorePropertyDefinition> list = new List<NativeStorePropertyDefinition>(propertySets.Length * 13 + PostalAddressProperties.AdditionalProperties.Length);
			foreach (PostalAddressProperties arg in propertySets)
			{
				list.AddRange(getProperties(arg));
			}
			list.AddRange(PostalAddressProperties.AdditionalProperties);
			return list.ToArray();
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x000EBAA3 File Offset: 0x000E9CA3
		private PostalAddressProperties()
		{
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06003977 RID: 14711 RVA: 0x000EBAAB File Offset: 0x000E9CAB
		// (set) Token: 0x06003978 RID: 14712 RVA: 0x000EBAB3 File Offset: 0x000E9CB3
		public NativeStorePropertyDefinition Street { get; private set; }

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06003979 RID: 14713 RVA: 0x000EBABC File Offset: 0x000E9CBC
		// (set) Token: 0x0600397A RID: 14714 RVA: 0x000EBAC4 File Offset: 0x000E9CC4
		public NativeStorePropertyDefinition City { get; private set; }

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x0600397B RID: 14715 RVA: 0x000EBACD File Offset: 0x000E9CCD
		// (set) Token: 0x0600397C RID: 14716 RVA: 0x000EBAD5 File Offset: 0x000E9CD5
		public NativeStorePropertyDefinition State { get; private set; }

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x0600397D RID: 14717 RVA: 0x000EBADE File Offset: 0x000E9CDE
		// (set) Token: 0x0600397E RID: 14718 RVA: 0x000EBAE6 File Offset: 0x000E9CE6
		public NativeStorePropertyDefinition Country { get; private set; }

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x0600397F RID: 14719 RVA: 0x000EBAEF File Offset: 0x000E9CEF
		// (set) Token: 0x06003980 RID: 14720 RVA: 0x000EBAF7 File Offset: 0x000E9CF7
		public NativeStorePropertyDefinition PostalCode { get; private set; }

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x06003981 RID: 14721 RVA: 0x000EBB00 File Offset: 0x000E9D00
		// (set) Token: 0x06003982 RID: 14722 RVA: 0x000EBB08 File Offset: 0x000E9D08
		public NativeStorePropertyDefinition PostOfficeBox { get; private set; }

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x06003983 RID: 14723 RVA: 0x000EBB11 File Offset: 0x000E9D11
		// (set) Token: 0x06003984 RID: 14724 RVA: 0x000EBB19 File Offset: 0x000E9D19
		public NativeStorePropertyDefinition Latitude { get; private set; }

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x06003985 RID: 14725 RVA: 0x000EBB22 File Offset: 0x000E9D22
		// (set) Token: 0x06003986 RID: 14726 RVA: 0x000EBB2A File Offset: 0x000E9D2A
		public NativeStorePropertyDefinition Longitude { get; private set; }

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x06003987 RID: 14727 RVA: 0x000EBB33 File Offset: 0x000E9D33
		// (set) Token: 0x06003988 RID: 14728 RVA: 0x000EBB3B File Offset: 0x000E9D3B
		public NativeStorePropertyDefinition Accuracy { get; private set; }

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x06003989 RID: 14729 RVA: 0x000EBB44 File Offset: 0x000E9D44
		// (set) Token: 0x0600398A RID: 14730 RVA: 0x000EBB4C File Offset: 0x000E9D4C
		public NativeStorePropertyDefinition Altitude { get; private set; }

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x0600398B RID: 14731 RVA: 0x000EBB55 File Offset: 0x000E9D55
		// (set) Token: 0x0600398C RID: 14732 RVA: 0x000EBB5D File Offset: 0x000E9D5D
		public NativeStorePropertyDefinition AltitudeAccuracy { get; private set; }

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x0600398D RID: 14733 RVA: 0x000EBB66 File Offset: 0x000E9D66
		// (set) Token: 0x0600398E RID: 14734 RVA: 0x000EBB6E File Offset: 0x000E9D6E
		public NativeStorePropertyDefinition LocationSource { get; private set; }

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x0600398F RID: 14735 RVA: 0x000EBB77 File Offset: 0x000E9D77
		// (set) Token: 0x06003990 RID: 14736 RVA: 0x000EBB7F File Offset: 0x000E9D7F
		public NativeStorePropertyDefinition LocationUri { get; private set; }

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x06003991 RID: 14737 RVA: 0x000EBB88 File Offset: 0x000E9D88
		// (set) Token: 0x06003992 RID: 14738 RVA: 0x000EBB90 File Offset: 0x000E9D90
		public PostalAddressType PostalAddressType { get; private set; }

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x06003993 RID: 14739 RVA: 0x000EBB9C File Offset: 0x000E9D9C
		public NativeStorePropertyDefinition[] Properties
		{
			get
			{
				return new NativeStorePropertyDefinition[]
				{
					this.Street,
					this.City,
					this.State,
					this.Country,
					this.PostalCode,
					this.PostOfficeBox,
					this.Latitude,
					this.Longitude,
					this.Accuracy,
					this.Altitude,
					this.AltitudeAccuracy,
					this.LocationSource,
					this.LocationUri
				};
			}
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x06003994 RID: 14740 RVA: 0x000EBC2C File Offset: 0x000E9E2C
		private NativeStorePropertyDefinition[] PropertiesForConversationView
		{
			get
			{
				return new NativeStorePropertyDefinition[]
				{
					this.Street,
					this.City,
					this.State,
					this.Country,
					this.PostalCode,
					this.Latitude,
					this.Longitude,
					this.LocationSource,
					this.LocationUri
				};
			}
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x000EBC94 File Offset: 0x000E9E94
		public PostalAddress GetFromAllProperties(IStorePropertyBag propertyBag)
		{
			PostalAddress fromAllPropertiesForConversationViewInternal = this.GetFromAllPropertiesForConversationViewInternal(propertyBag);
			fromAllPropertiesForConversationViewInternal.PostOfficeBox = propertyBag.GetValueOrDefault<string>(this.PostOfficeBox, null);
			fromAllPropertiesForConversationViewInternal.Accuracy = propertyBag.GetValueOrDefault<double?>(this.Accuracy, null);
			fromAllPropertiesForConversationViewInternal.Altitude = propertyBag.GetValueOrDefault<double?>(this.Altitude, null);
			fromAllPropertiesForConversationViewInternal.AltitudeAccuracy = propertyBag.GetValueOrDefault<double?>(this.AltitudeAccuracy, null);
			if (fromAllPropertiesForConversationViewInternal.IsEmpty())
			{
				return null;
			}
			return fromAllPropertiesForConversationViewInternal;
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x000EBD18 File Offset: 0x000E9F18
		public PostalAddress GetFromAllPropertiesForConversationView(IStorePropertyBag propertyBag)
		{
			PostalAddress fromAllPropertiesForConversationViewInternal = this.GetFromAllPropertiesForConversationViewInternal(propertyBag);
			if (fromAllPropertiesForConversationViewInternal.IsEmpty())
			{
				return null;
			}
			return fromAllPropertiesForConversationViewInternal;
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x000EBD38 File Offset: 0x000E9F38
		public void SetTo(IStorePropertyBag propertyBag, PostalAddress postalAddress)
		{
			PostalAddressProperties.SetOrDeleteValue<string>(propertyBag, this.Street, postalAddress.Street);
			PostalAddressProperties.SetOrDeleteValue<string>(propertyBag, this.City, postalAddress.City);
			PostalAddressProperties.SetOrDeleteValue<string>(propertyBag, this.State, postalAddress.State);
			PostalAddressProperties.SetOrDeleteValue<string>(propertyBag, this.Country, postalAddress.Country);
			PostalAddressProperties.SetOrDeleteValue<string>(propertyBag, this.PostalCode, postalAddress.PostalCode);
			PostalAddressProperties.SetOrDeleteValue<double>(propertyBag, this.Latitude, postalAddress.Latitude);
			PostalAddressProperties.SetOrDeleteValue<double>(propertyBag, this.Longitude, postalAddress.Longitude);
			PostalAddressProperties.SetOrDeleteValue<double>(propertyBag, this.Accuracy, postalAddress.Accuracy);
			PostalAddressProperties.SetOrDeleteValue<double>(propertyBag, this.Altitude, postalAddress.Altitude);
			PostalAddressProperties.SetOrDeleteValue<double>(propertyBag, this.AltitudeAccuracy, postalAddress.AltitudeAccuracy);
			PostalAddressProperties.SetOrDeleteValue(propertyBag, this.LocationSource, postalAddress.LocationSource);
			PostalAddressProperties.SetOrDeleteValue<string>(propertyBag, this.LocationUri, postalAddress.LocationUri);
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x000EBE1D File Offset: 0x000EA01D
		private static void SetOrDeleteValue<T>(IStorePropertyBag propertyBag, NativeStorePropertyDefinition property, T value) where T : class
		{
			if (value == null)
			{
				propertyBag.Delete(property);
				return;
			}
			propertyBag[property] = value;
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x000EBE3C File Offset: 0x000EA03C
		private static void SetOrDeleteValue<T>(IStorePropertyBag propertyBag, NativeStorePropertyDefinition property, T? value) where T : struct
		{
			if (value == null)
			{
				propertyBag.Delete(property);
				return;
			}
			propertyBag[property] = value.Value;
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x000EBE62 File Offset: 0x000EA062
		private static void SetOrDeleteValue(IStorePropertyBag propertyBag, NativeStorePropertyDefinition property, LocationSource value)
		{
			if (value == Microsoft.Exchange.Data.Storage.LocationSource.None)
			{
				propertyBag.Delete(property);
				return;
			}
			propertyBag[property] = value;
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x000EBE7C File Offset: 0x000EA07C
		private PostalAddress GetFromAllPropertiesForConversationViewInternal(IStorePropertyBag propertyBag)
		{
			PostalAddress postalAddress = new PostalAddress
			{
				Street = propertyBag.GetValueOrDefault<string>(this.Street, null),
				City = propertyBag.GetValueOrDefault<string>(this.City, null),
				State = propertyBag.GetValueOrDefault<string>(this.State, null),
				Country = propertyBag.GetValueOrDefault<string>(this.Country, null),
				PostalCode = propertyBag.GetValueOrDefault<string>(this.PostalCode, null),
				Latitude = propertyBag.GetValueOrDefault<double?>(this.Latitude, null),
				Longitude = propertyBag.GetValueOrDefault<double?>(this.Longitude, null),
				LocationSource = propertyBag.GetValueOrDefault<LocationSource>(this.LocationSource, Microsoft.Exchange.Data.Storage.LocationSource.None),
				LocationUri = propertyBag.GetValueOrDefault<string>(this.LocationUri, null),
				Type = this.PostalAddressType
			};
			if (!postalAddress.IsEmpty() && postalAddress.LocationSource == Microsoft.Exchange.Data.Storage.LocationSource.None)
			{
				postalAddress.LocationSource = Microsoft.Exchange.Data.Storage.LocationSource.Contact;
				foreach (NativeStorePropertyDefinition propertyDefinition in PostalAddressProperties.AdditionalProperties)
				{
					string valueOrDefault = propertyBag.GetValueOrDefault<string>(propertyDefinition, null);
					if (valueOrDefault != null)
					{
						postalAddress.LocationUri = valueOrDefault;
						break;
					}
				}
			}
			return postalAddress;
		}

		// Token: 0x04001EA7 RID: 7847
		private static readonly NativeStorePropertyDefinition[] AdditionalProperties = new NativeStorePropertyDefinition[]
		{
			InternalSchema.Email1EmailAddress,
			InternalSchema.Email2EmailAddress,
			InternalSchema.Email3EmailAddress
		};

		// Token: 0x04001EA8 RID: 7848
		public static readonly PostalAddressProperties WorkAddress = new PostalAddressProperties
		{
			Street = InternalSchema.WorkAddressStreet,
			City = InternalSchema.WorkAddressCity,
			State = InternalSchema.WorkAddressState,
			Country = InternalSchema.WorkAddressCountry,
			PostalCode = InternalSchema.WorkAddressPostalCode,
			PostOfficeBox = InternalSchema.WorkPostOfficeBox,
			Latitude = InternalSchema.WorkLatitude,
			Longitude = InternalSchema.WorkLongitude,
			Accuracy = InternalSchema.WorkAccuracy,
			Altitude = InternalSchema.WorkAltitude,
			AltitudeAccuracy = InternalSchema.WorkAltitudeAccuracy,
			LocationSource = InternalSchema.WorkLocationSource,
			LocationUri = InternalSchema.WorkLocationUri,
			PostalAddressType = PostalAddressType.Business
		};

		// Token: 0x04001EA9 RID: 7849
		public static readonly PostalAddressProperties HomeAddress = new PostalAddressProperties
		{
			Street = InternalSchema.HomeStreet,
			City = InternalSchema.HomeCity,
			State = InternalSchema.HomeState,
			Country = InternalSchema.HomeCountry,
			PostalCode = InternalSchema.HomePostalCode,
			PostOfficeBox = InternalSchema.HomePostOfficeBox,
			Latitude = InternalSchema.HomeLatitude,
			Longitude = InternalSchema.HomeLongitude,
			Accuracy = InternalSchema.HomeAccuracy,
			Altitude = InternalSchema.HomeAltitude,
			AltitudeAccuracy = InternalSchema.HomeAltitudeAccuracy,
			LocationSource = InternalSchema.HomeLocationSource,
			LocationUri = InternalSchema.HomeLocationUri,
			PostalAddressType = PostalAddressType.Home
		};

		// Token: 0x04001EAA RID: 7850
		public static readonly PostalAddressProperties OtherAddress = new PostalAddressProperties
		{
			Street = InternalSchema.OtherStreet,
			City = InternalSchema.OtherCity,
			State = InternalSchema.OtherState,
			Country = InternalSchema.OtherCountry,
			PostalCode = InternalSchema.OtherPostalCode,
			PostOfficeBox = InternalSchema.OtherPostOfficeBox,
			Latitude = InternalSchema.OtherLatitude,
			Longitude = InternalSchema.OtherLongitude,
			Accuracy = InternalSchema.OtherAccuracy,
			Altitude = InternalSchema.OtherAltitude,
			AltitudeAccuracy = InternalSchema.OtherAltitudeAccuracy,
			LocationSource = InternalSchema.OtherLocationSource,
			LocationUri = InternalSchema.OtherLocationUri,
			PostalAddressType = PostalAddressType.Other
		};

		// Token: 0x04001EAB RID: 7851
		public static readonly PostalAddressProperties[] PropertySets = new PostalAddressProperties[]
		{
			PostalAddressProperties.WorkAddress,
			PostalAddressProperties.HomeAddress,
			PostalAddressProperties.OtherAddress
		};

		// Token: 0x04001EAC RID: 7852
		public static readonly NativeStorePropertyDefinition[] AllProperties = PostalAddressProperties.GetAllProperties(PostalAddressProperties.PropertySets, (PostalAddressProperties propertySet) => propertySet.Properties);

		// Token: 0x04001EAD RID: 7853
		public static NativeStorePropertyDefinition[] AllPropertiesForConversationView = PostalAddressProperties.GetAllProperties(PostalAddressProperties.PropertySets, (PostalAddressProperties propertySet) => propertySet.PropertiesForConversationView);
	}
}
