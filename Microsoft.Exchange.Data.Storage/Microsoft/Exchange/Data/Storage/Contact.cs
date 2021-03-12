using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200049D RID: 1181
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Contact : ContactBase, IContact, IContactBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06003436 RID: 13366 RVA: 0x000D3C4F File Offset: 0x000D1E4F
		internal Contact(ICoreItem coreItem) : base(coreItem)
		{
			if (base.IsNew)
			{
				this.Initialize();
			}
			this.completeName = new Contact.CallbackCompleteName(this);
			this.emailAddresses = new Contact.EmailAddressDictionary(this);
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x000D3C89 File Offset: 0x000D1E89
		public static Contact Create(StoreSession session, StoreId contactFolderId)
		{
			return ItemBuilder.CreateNewItem<Contact>(session, contactFolderId, ItemCreateInfo.ContactInfo);
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x000D3C97 File Offset: 0x000D1E97
		public new static Contact Bind(StoreSession session, StoreId storeId, params PropertyDefinition[] propsToReturn)
		{
			return Contact.Bind(session, storeId, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x000D3CA6 File Offset: 0x000D1EA6
		public new static Contact Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<Contact>(session, storeId, ContactSchema.Instance, propsToReturn);
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x000D3CB8 File Offset: 0x000D1EB8
		public static void ImportVCard(Contact contact, Stream dataStream, Encoding encoding, InboundConversionOptions options)
		{
			Util.ThrowOnNullArgument(contact, "contact");
			Util.ThrowOnNullArgument(dataStream, "dataStream");
			Util.ThrowOnNullArgument(encoding, "encoding");
			Util.ThrowOnNullArgument(options, "options");
			if (!options.IgnoreImceaDomain)
			{
				Util.ThrowOnNullOrEmptyArgument(options.ImceaEncapsulationDomain, "options.ImceaEncapsulationDomain");
			}
			contact.Load(InternalSchema.ContentConversionProperties);
			InboundVCardConverter.Convert(dataStream, encoding, contact, options);
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x000D3D20 File Offset: 0x000D1F20
		public static void ExportVCard(Contact contact, Stream dataStream, OutboundConversionOptions options)
		{
			Util.ThrowOnNullArgument(dataStream, "dataStream");
			Util.ThrowOnNullArgument(contact, "contact");
			Util.ThrowOnNullArgument(options, "options");
			Util.ThrowOnNullOrEmptyArgument(options.ImceaEncapsulationDomain, "options.ImceaEncapsulationDomain");
			if (!contact.HasAllPropertiesLoaded)
			{
				throw new ArgumentException("Properties not loaded for contact");
			}
			OutboundVCardConverter.Convert(dataStream, Encoding.UTF8, contact, options, new ConversionLimitsTracker(options.Limits));
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x000D3D89 File Offset: 0x000D1F89
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<Contact>(this);
		}

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x0600343D RID: 13373 RVA: 0x000D3D91 File Offset: 0x000D1F91
		// (set) Token: 0x0600343E RID: 13374 RVA: 0x000D3DAE File Offset: 0x000D1FAE
		public string ImAddress
		{
			get
			{
				this.CheckDisposed("ImAddress::get");
				return base.GetValueOrDefault<string>(ContactSchema.IMAddress, string.Empty);
			}
			set
			{
				this.CheckDisposed("ImAddress::set");
				this[ContactSchema.IMAddress] = value;
			}
		}

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x0600343F RID: 13375 RVA: 0x000D3DC7 File Offset: 0x000D1FC7
		public CompleteName CompleteName
		{
			get
			{
				this.CheckDisposed("CompleteName::get");
				return this.completeName;
			}
		}

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x06003440 RID: 13376 RVA: 0x000D3DDA File Offset: 0x000D1FDA
		public IDictionary<PhysicalAddressType, PhysicalAddress> PhysicalAddresses
		{
			get
			{
				this.CheckDisposed("PhysicalAddresses::get");
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x06003441 RID: 13377 RVA: 0x000D3DEC File Offset: 0x000D1FEC
		public IDictionary<EmailAddressIndex, Participant> EmailAddresses
		{
			get
			{
				this.CheckDisposed("EmailAddresses::get");
				this.EnsureParticipantsLoaded();
				return this.emailAddresses;
			}
		}

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x06003442 RID: 13378 RVA: 0x000D3E05 File Offset: 0x000D2005
		// (set) Token: 0x06003443 RID: 13379 RVA: 0x000D3E22 File Offset: 0x000D2022
		public string JobTitle
		{
			get
			{
				this.CheckDisposed("JobTitle::get");
				return base.GetValueOrDefault<string>(ContactSchema.Title, string.Empty);
			}
			set
			{
				this.CheckDisposed("JobTitle::set");
				this[ContactSchema.Title] = value;
			}
		}

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06003444 RID: 13380 RVA: 0x000D3E3B File Offset: 0x000D203B
		// (set) Token: 0x06003445 RID: 13381 RVA: 0x000D3E58 File Offset: 0x000D2058
		public string Company
		{
			get
			{
				this.CheckDisposed("Company::get");
				return base.GetValueOrDefault<string>(ContactSchema.CompanyName, string.Empty);
			}
			set
			{
				this.CheckDisposed("Company::set");
				this[ContactSchema.CompanyName] = value;
			}
		}

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x06003446 RID: 13382 RVA: 0x000D3E71 File Offset: 0x000D2071
		// (set) Token: 0x06003447 RID: 13383 RVA: 0x000D3E8E File Offset: 0x000D208E
		public string Department
		{
			get
			{
				this.CheckDisposed("Department::get");
				return base.GetValueOrDefault<string>(ContactSchema.Department, string.Empty);
			}
			set
			{
				this.CheckDisposed("Department::set");
				this[ContactSchema.Department] = value;
			}
		}

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x06003448 RID: 13384 RVA: 0x000D3EA7 File Offset: 0x000D20A7
		// (set) Token: 0x06003449 RID: 13385 RVA: 0x000D3EC0 File Offset: 0x000D20C0
		public PersonType PersonType
		{
			get
			{
				this.CheckDisposed("PersonType::get");
				return base.GetValueOrDefault<PersonType>(ContactSchema.PersonType, PersonType.Unknown);
			}
			set
			{
				this.CheckDisposed("PersonType::set");
				this[ContactSchema.PersonType] = value;
			}
		}

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x0600344A RID: 13386 RVA: 0x000D3EDE File Offset: 0x000D20DE
		// (set) Token: 0x0600344B RID: 13387 RVA: 0x000D3EF7 File Offset: 0x000D20F7
		public bool IsFavorite
		{
			get
			{
				this.CheckDisposed("IsFavorite::get");
				return base.GetValueOrDefault<bool>(ContactSchema.IsFavorite, false);
			}
			set
			{
				this.CheckDisposed("IsFavorite::set");
				this[ContactSchema.IsFavorite] = value;
			}
		}

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x0600344C RID: 13388 RVA: 0x000D3F15 File Offset: 0x000D2115
		// (set) Token: 0x0600344D RID: 13389 RVA: 0x000D3F2E File Offset: 0x000D212E
		public bool IsPromotedContact
		{
			get
			{
				this.CheckDisposed("IsPromotedContact::get");
				return base.GetValueOrDefault<bool>(ContactSchema.IsPromotedContact, false);
			}
			set
			{
				this.CheckDisposed("IsPromotedContact::set");
				this[ContactSchema.IsPromotedContact] = value;
			}
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x0600344E RID: 13390 RVA: 0x000D3F4C File Offset: 0x000D214C
		// (set) Token: 0x0600344F RID: 13391 RVA: 0x000D3F69 File Offset: 0x000D2169
		public int RelevanceScore
		{
			get
			{
				this.CheckDisposed("RelevanceScore::get");
				return base.GetValueOrDefault<int>(ContactSchema.RelevanceScore, int.MaxValue);
			}
			set
			{
				this.CheckDisposed("RelevanceScore::set");
				this[ContactSchema.RelevanceScore] = value;
			}
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x06003450 RID: 13392 RVA: 0x000D3F87 File Offset: 0x000D2187
		// (set) Token: 0x06003451 RID: 13393 RVA: 0x000D3FA4 File Offset: 0x000D21A4
		public string PartnerNetworkId
		{
			get
			{
				this.CheckDisposed("PartnerNetworkId::get");
				return base.GetValueOrDefault<string>(ContactSchema.PartnerNetworkId, string.Empty);
			}
			set
			{
				this.CheckDisposed("PartnerNetworkId::set");
				this[ContactSchema.PartnerNetworkId] = value;
			}
		}

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x06003452 RID: 13394 RVA: 0x000D3FBD File Offset: 0x000D21BD
		// (set) Token: 0x06003453 RID: 13395 RVA: 0x000D3FDA File Offset: 0x000D21DA
		public string OfficeLocation
		{
			get
			{
				this.CheckDisposed("OfficeLocation::get");
				return base.GetValueOrDefault<string>(ContactSchema.OfficeLocation, string.Empty);
			}
			set
			{
				this.CheckDisposed("OfficeLocation::set");
				this[ContactSchema.OfficeLocation] = value;
			}
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x06003454 RID: 13396 RVA: 0x000D3FF3 File Offset: 0x000D21F3
		// (set) Token: 0x06003455 RID: 13397 RVA: 0x000D4010 File Offset: 0x000D2210
		public string AssistantName
		{
			get
			{
				this.CheckDisposed("AssistantName::get");
				return base.GetValueOrDefault<string>(ContactSchema.AssistantName, string.Empty);
			}
			set
			{
				this.CheckDisposed("AssistantName::set");
				this[ContactSchema.AssistantName] = value;
			}
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x06003456 RID: 13398 RVA: 0x000D4029 File Offset: 0x000D2229
		// (set) Token: 0x06003457 RID: 13399 RVA: 0x000D4037 File Offset: 0x000D2237
		public FileAsMapping FileAs
		{
			get
			{
				return base.GetValueOrDefault<FileAsMapping>(ContactSchema.FileAsId, FileAsMapping.None);
			}
			set
			{
				this.CheckDisposed("FileAs::set");
				EnumValidator.ThrowIfInvalid<FileAsMapping>(value, "value");
				this[ContactSchema.FileAsId] = (int)value;
			}
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x06003458 RID: 13400 RVA: 0x000D4060 File Offset: 0x000D2260
		// (set) Token: 0x06003459 RID: 13401 RVA: 0x000D4072 File Offset: 0x000D2272
		public CultureInfo Culture
		{
			get
			{
				this.CheckDisposed("Culture::get");
				throw new NotImplementedException();
			}
			set
			{
				this.CheckDisposed("Culture::set");
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x0600345A RID: 13402 RVA: 0x000D4084 File Offset: 0x000D2284
		// (set) Token: 0x0600345B RID: 13403 RVA: 0x000D409D File Offset: 0x000D229D
		public LocationSource HomeLocationSource
		{
			get
			{
				this.CheckDisposed("HomeLocationSource::get");
				return (LocationSource)base.GetValueOrDefault<int>(ContactSchema.HomeLocationSource, 0);
			}
			set
			{
				this.CheckDisposed("HomeLocationSource::set");
				EnumValidator.ThrowIfInvalid<LocationSource>(value, "HomeLocationSource");
				this[ContactSchema.HomeLocationSource] = value;
			}
		}

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x0600345C RID: 13404 RVA: 0x000D40C6 File Offset: 0x000D22C6
		// (set) Token: 0x0600345D RID: 13405 RVA: 0x000D40DF File Offset: 0x000D22DF
		public string HomeLocationUri
		{
			get
			{
				this.CheckDisposed("HomeLocationUri::get");
				return base.GetValueOrDefault<string>(ContactSchema.HomeLocationUri, null);
			}
			set
			{
				this.CheckDisposed("HomeLocationUri::set");
				base.SetOrDeleteProperty(ContactSchema.HomeLocationUri, value);
			}
		}

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x0600345E RID: 13406 RVA: 0x000D40F8 File Offset: 0x000D22F8
		// (set) Token: 0x0600345F RID: 13407 RVA: 0x000D4124 File Offset: 0x000D2324
		public double? HomeLatitude
		{
			get
			{
				this.CheckDisposed("HomeLatitude::get");
				return base.GetValueOrDefault<double?>(ContactSchema.HomeLatitude, null);
			}
			set
			{
				this.CheckDisposed("HomeLatitude::set");
				if (value.Equals(base.GetValueAsNullable<double>(ContactSchema.HomeLatitude)))
				{
					return;
				}
				base.SetOrDeleteProperty(ContactSchema.HomeLatitude, value);
			}
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x06003460 RID: 13408 RVA: 0x000D4164 File Offset: 0x000D2364
		// (set) Token: 0x06003461 RID: 13409 RVA: 0x000D4190 File Offset: 0x000D2390
		public double? HomeLongitude
		{
			get
			{
				this.CheckDisposed("HomeLongitude::get");
				return base.GetValueOrDefault<double?>(ContactSchema.HomeLongitude, null);
			}
			set
			{
				this.CheckDisposed("HomeLongitude::set");
				if (value.Equals(base.GetValueAsNullable<double>(ContactSchema.HomeLongitude)))
				{
					return;
				}
				base.SetOrDeleteProperty(ContactSchema.HomeLongitude, value);
			}
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x06003462 RID: 13410 RVA: 0x000D41D0 File Offset: 0x000D23D0
		// (set) Token: 0x06003463 RID: 13411 RVA: 0x000D41FC File Offset: 0x000D23FC
		public double? HomeAltitude
		{
			get
			{
				this.CheckDisposed("HomeAltitude::get");
				return base.GetValueOrDefault<double?>(ContactSchema.HomeAltitude, null);
			}
			set
			{
				this.CheckDisposed("HomeAltitude::set");
				if (value.Equals(base.GetValueAsNullable<double>(ContactSchema.HomeAltitude)))
				{
					return;
				}
				base.SetOrDeleteProperty(ContactSchema.HomeAltitude, value);
			}
		}

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x06003464 RID: 13412 RVA: 0x000D423C File Offset: 0x000D243C
		// (set) Token: 0x06003465 RID: 13413 RVA: 0x000D4268 File Offset: 0x000D2468
		public double? HomeAccuracy
		{
			get
			{
				this.CheckDisposed("HomeAccuracy::get");
				return base.GetValueOrDefault<double?>(ContactSchema.HomeAccuracy, null);
			}
			set
			{
				this.CheckDisposed("HomeAccuracy::set");
				if (value.Equals(base.GetValueAsNullable<double>(ContactSchema.HomeAccuracy)))
				{
					return;
				}
				base.SetOrDeleteProperty(ContactSchema.HomeAccuracy, value);
			}
		}

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x06003466 RID: 13414 RVA: 0x000D42A8 File Offset: 0x000D24A8
		// (set) Token: 0x06003467 RID: 13415 RVA: 0x000D42D4 File Offset: 0x000D24D4
		public double? HomeAltitudeAccuracy
		{
			get
			{
				this.CheckDisposed("HomeAltitudeAccuracy::get");
				return base.GetValueOrDefault<double?>(ContactSchema.HomeAltitudeAccuracy, null);
			}
			set
			{
				this.CheckDisposed("HomeAltitudeAccuracy::set");
				if (value.Equals(base.GetValueAsNullable<double>(ContactSchema.HomeAltitudeAccuracy)))
				{
					return;
				}
				base.SetOrDeleteProperty(ContactSchema.HomeAltitudeAccuracy, value);
			}
		}

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x06003468 RID: 13416 RVA: 0x000D4312 File Offset: 0x000D2512
		// (set) Token: 0x06003469 RID: 13417 RVA: 0x000D432B File Offset: 0x000D252B
		public LocationSource WorkLocationSource
		{
			get
			{
				this.CheckDisposed("WorkLocationSource::get");
				return (LocationSource)base.GetValueOrDefault<int>(ContactSchema.WorkLocationSource, 0);
			}
			set
			{
				this.CheckDisposed("WorkLocationSource::set");
				EnumValidator.ThrowIfInvalid<LocationSource>(value, "WorkLocationSource");
				base.SetOrDeleteProperty(ContactSchema.WorkLocationSource, value);
			}
		}

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x0600346A RID: 13418 RVA: 0x000D4354 File Offset: 0x000D2554
		// (set) Token: 0x0600346B RID: 13419 RVA: 0x000D436D File Offset: 0x000D256D
		public string WorkLocationUri
		{
			get
			{
				this.CheckDisposed("WorkLocationUri::get");
				return base.GetValueOrDefault<string>(ContactSchema.WorkLocationUri, null);
			}
			set
			{
				this.CheckDisposed("WorkLocationUri::set");
				base.SetOrDeleteProperty(ContactSchema.WorkLocationUri, value);
			}
		}

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x0600346C RID: 13420 RVA: 0x000D4388 File Offset: 0x000D2588
		// (set) Token: 0x0600346D RID: 13421 RVA: 0x000D43B4 File Offset: 0x000D25B4
		public double? WorkLatitude
		{
			get
			{
				this.CheckDisposed("WorkLatitude::get");
				return base.GetValueOrDefault<double?>(ContactSchema.WorkLatitude, null);
			}
			set
			{
				this.CheckDisposed("WorkLatitude::set");
				if (value.Equals(base.GetValueAsNullable<double>(ContactSchema.WorkLatitude)))
				{
					return;
				}
				base.SetOrDeleteProperty(ContactSchema.WorkLatitude, value);
			}
		}

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x0600346E RID: 13422 RVA: 0x000D43F4 File Offset: 0x000D25F4
		// (set) Token: 0x0600346F RID: 13423 RVA: 0x000D4420 File Offset: 0x000D2620
		public double? WorkLongitude
		{
			get
			{
				this.CheckDisposed("WorkLongitude::get");
				return base.GetValueOrDefault<double?>(ContactSchema.WorkLongitude, null);
			}
			set
			{
				this.CheckDisposed("WorkLongitude::set");
				if (value.Equals(base.GetValueAsNullable<double>(ContactSchema.WorkLongitude)))
				{
					return;
				}
				base.SetOrDeleteProperty(ContactSchema.WorkLongitude, value);
			}
		}

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x06003470 RID: 13424 RVA: 0x000D4460 File Offset: 0x000D2660
		// (set) Token: 0x06003471 RID: 13425 RVA: 0x000D448C File Offset: 0x000D268C
		public double? WorkAltitude
		{
			get
			{
				this.CheckDisposed("WorkAltitude::get");
				return base.GetValueOrDefault<double?>(ContactSchema.WorkAltitude, null);
			}
			set
			{
				this.CheckDisposed("WorkAltitude::set");
				if (value.Equals(base.GetValueAsNullable<double>(ContactSchema.WorkAltitude)))
				{
					return;
				}
				base.SetOrDeleteProperty(ContactSchema.WorkAltitude, value);
			}
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x06003472 RID: 13426 RVA: 0x000D44CC File Offset: 0x000D26CC
		// (set) Token: 0x06003473 RID: 13427 RVA: 0x000D44F8 File Offset: 0x000D26F8
		public double? WorkAccuracy
		{
			get
			{
				this.CheckDisposed("WorkAccuracy::get");
				return base.GetValueOrDefault<double?>(ContactSchema.WorkAccuracy, null);
			}
			set
			{
				this.CheckDisposed("WorkAccuracy::set");
				if (value.Equals(base.GetValueAsNullable<double>(ContactSchema.WorkAccuracy)))
				{
					return;
				}
				base.SetOrDeleteProperty(ContactSchema.WorkAccuracy, value);
			}
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x06003474 RID: 13428 RVA: 0x000D4538 File Offset: 0x000D2738
		// (set) Token: 0x06003475 RID: 13429 RVA: 0x000D4564 File Offset: 0x000D2764
		public double? WorkAltitudeAccuracy
		{
			get
			{
				this.CheckDisposed("WorkAltitudeAccuracy::get");
				return base.GetValueOrDefault<double?>(ContactSchema.WorkAltitudeAccuracy, null);
			}
			set
			{
				this.CheckDisposed("WorkAltitudeAccuracy::set");
				if (value.Equals(base.GetValueAsNullable<double>(ContactSchema.WorkAltitudeAccuracy)))
				{
					return;
				}
				base.SetOrDeleteProperty(ContactSchema.WorkAltitudeAccuracy, value);
			}
		}

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x06003476 RID: 13430 RVA: 0x000D45A2 File Offset: 0x000D27A2
		// (set) Token: 0x06003477 RID: 13431 RVA: 0x000D45BB File Offset: 0x000D27BB
		public LocationSource OtherLocationSource
		{
			get
			{
				this.CheckDisposed("OtherLocationSource::get");
				return (LocationSource)base.GetValueOrDefault<int>(ContactSchema.OtherLocationSource, 0);
			}
			set
			{
				this.CheckDisposed("OtherLocationSource::set");
				EnumValidator.ThrowIfInvalid<LocationSource>(value, "OtherLocationSource");
				this[ContactSchema.OtherLocationSource] = value;
			}
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x06003478 RID: 13432 RVA: 0x000D45E4 File Offset: 0x000D27E4
		// (set) Token: 0x06003479 RID: 13433 RVA: 0x000D45FD File Offset: 0x000D27FD
		public string OtherLocationUri
		{
			get
			{
				this.CheckDisposed("OtherLocationUri::get");
				return base.GetValueOrDefault<string>(ContactSchema.OtherLocationUri, null);
			}
			set
			{
				this.CheckDisposed("OtherLocationUri::set");
				base.SetOrDeleteProperty(ContactSchema.OtherLocationUri, value);
			}
		}

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x0600347A RID: 13434 RVA: 0x000D4618 File Offset: 0x000D2818
		// (set) Token: 0x0600347B RID: 13435 RVA: 0x000D4644 File Offset: 0x000D2844
		public double? OtherLatitude
		{
			get
			{
				this.CheckDisposed("OtherLatitude::get");
				return base.GetValueOrDefault<double?>(ContactSchema.OtherLatitude, null);
			}
			set
			{
				this.CheckDisposed("OtherLatitude::set");
				if (value.Equals(base.GetValueAsNullable<double>(ContactSchema.OtherLatitude)))
				{
					return;
				}
				base.SetOrDeleteProperty(ContactSchema.OtherLatitude, value);
			}
		}

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x0600347C RID: 13436 RVA: 0x000D4684 File Offset: 0x000D2884
		// (set) Token: 0x0600347D RID: 13437 RVA: 0x000D46B0 File Offset: 0x000D28B0
		public double? OtherLongitude
		{
			get
			{
				this.CheckDisposed("OtherLongitude::get");
				return base.GetValueOrDefault<double?>(ContactSchema.OtherLongitude, null);
			}
			set
			{
				this.CheckDisposed("OtherLongitude::set");
				if (value.Equals(base.GetValueAsNullable<double>(ContactSchema.OtherLongitude)))
				{
					return;
				}
				base.SetOrDeleteProperty(ContactSchema.OtherLongitude, value);
			}
		}

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000D46F0 File Offset: 0x000D28F0
		// (set) Token: 0x0600347F RID: 13439 RVA: 0x000D471C File Offset: 0x000D291C
		public double? OtherAltitude
		{
			get
			{
				this.CheckDisposed("OtherAltitude::get");
				return base.GetValueOrDefault<double?>(ContactSchema.OtherAltitude, null);
			}
			set
			{
				this.CheckDisposed("OtherAltitude::set");
				if (value.Equals(base.GetValueAsNullable<double>(ContactSchema.OtherAltitude)))
				{
					return;
				}
				base.SetOrDeleteProperty(ContactSchema.OtherAltitude, value);
			}
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06003480 RID: 13440 RVA: 0x000D475C File Offset: 0x000D295C
		// (set) Token: 0x06003481 RID: 13441 RVA: 0x000D4788 File Offset: 0x000D2988
		public double? OtherAccuracy
		{
			get
			{
				this.CheckDisposed("OtherAccuracy::get");
				return base.GetValueOrDefault<double?>(ContactSchema.OtherAccuracy, null);
			}
			set
			{
				this.CheckDisposed("OtherAccuracy::set");
				if (value.Equals(base.GetValueAsNullable<double>(ContactSchema.OtherAccuracy)))
				{
					return;
				}
				base.SetOrDeleteProperty(ContactSchema.OtherAccuracy, value);
			}
		}

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x000D47C8 File Offset: 0x000D29C8
		// (set) Token: 0x06003483 RID: 13443 RVA: 0x000D47F4 File Offset: 0x000D29F4
		public double? OtherAltitudeAccuracy
		{
			get
			{
				this.CheckDisposed("OtherAltitudeAccuracy::get");
				return base.GetValueOrDefault<double?>(ContactSchema.OtherAltitudeAccuracy, null);
			}
			set
			{
				this.CheckDisposed("OtherAltitudeAccuracy::set");
				if (value.Equals(base.GetValueAsNullable<double>(ContactSchema.OtherAltitudeAccuracy)))
				{
					return;
				}
				base.SetOrDeleteProperty(ContactSchema.OtherAltitudeAccuracy, value);
			}
		}

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x000D4832 File Offset: 0x000D2A32
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return ContactSchema.Instance;
			}
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x000D4844 File Offset: 0x000D2A44
		public Stream GetPhotoStream()
		{
			foreach (AttachmentHandle handle in base.AttachmentCollection)
			{
				using (Attachment attachment = base.AttachmentCollection.Open(handle, null))
				{
					if (attachment.IsContactPhoto)
					{
						StreamAttachment streamAttachment = attachment as StreamAttachment;
						if (streamAttachment != null)
						{
							return streamAttachment.TryGetContentStream(PropertyOpenMode.ReadOnly);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x000D48D8 File Offset: 0x000D2AD8
		private void EnsureParticipantsLoaded()
		{
			if (!this.areEmailAddressesLoaded)
			{
				foreach (KeyValuePair<EmailAddressIndex, ContactEmailSlotParticipantProperty> keyValuePair in ContactEmailSlotParticipantProperty.AllInstances)
				{
					this.emailAddresses.InternalSet(keyValuePair.Key, base.GetValueOrDefault<Participant>(keyValuePair.Value));
				}
				this.areEmailAddressesLoaded = true;
			}
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x000D4954 File Offset: 0x000D2B54
		private void OnEmailAddressChanged(EmailAddressIndex emailAddressIndex)
		{
			base.SetOrDeleteProperty(ContactEmailSlotParticipantProperty.AllInstances[emailAddressIndex], this.emailAddresses[emailAddressIndex]);
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x000D4974 File Offset: 0x000D2B74
		private void Initialize()
		{
			base.Load(new PropertyDefinition[]
			{
				InternalSchema.ItemClass
			});
			string itemClass = base.TryGetProperty(InternalSchema.ItemClass) as string;
			if (!ObjectClass.IsContact(itemClass))
			{
				this[InternalSchema.ItemClass] = "IPM.Contact";
			}
			this[ContactSchema.FileAsId] = -1;
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x000D49D4 File Offset: 0x000D2BD4
		protected override void OnBeforeSave()
		{
			base.OnBeforeSave();
			this.junkEmailUpdates.Clear();
			foreach (ContactEmailSlotParticipantProperty contactEmailSlotParticipantProperty in ContactEmailSlotParticipantProperty.AllInstances.Values)
			{
				if (base.PropertyBag.IsPropertyDirty(contactEmailSlotParticipantProperty.EmailAddressPropertyDefinition) || base.PropertyBag.IsPropertyDirty(contactEmailSlotParticipantProperty.RoutingTypePropertyDefinition))
				{
					Participant valueOrDefault = base.GetValueOrDefault<Participant>(contactEmailSlotParticipantProperty);
					if (valueOrDefault != null && valueOrDefault.RoutingType == "SMTP")
					{
						this.junkEmailUpdates.Add(valueOrDefault.EmailAddress);
					}
				}
			}
			this.OnBeforeSaveUpdateInteropValues();
			SideEffects sideEffects = base.GetValueOrDefault<SideEffects>(ContactSchema.SideEffects);
			sideEffects |= SideEffects.CoerceToInbox;
			this[ContactSchema.SideEffects] = sideEffects;
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000D4AB8 File Offset: 0x000D2CB8
		protected override void OnAfterSave(ConflictResolutionResult acrResults)
		{
			base.OnAfterSave(acrResults);
			if (acrResults.SaveStatus != SaveResult.IrresolvableConflict)
			{
				this.areEmailAddressesLoaded = false;
				this.UpdateJunkEmailContacts();
			}
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000D4AD8 File Offset: 0x000D2CD8
		private void OnBeforeSaveUpdateInteropValues()
		{
			this.OnBeforeSaveUpdateFaxParticipant(ContactSchema.WorkFax, EmailAddressIndex.BusinessFax);
			this.OnBeforeSaveUpdateFaxParticipant(ContactSchema.HomeFax, EmailAddressIndex.HomeFax);
			this.OnBeforeSaveUpdateFaxParticipant(ContactSchema.OtherFax, EmailAddressIndex.OtherFax);
			EmailListType emailListType = EmailListType.None;
			List<int> list = new List<int>(6);
			EmailAddressIndex[] array = new EmailAddressIndex[]
			{
				EmailAddressIndex.Email1,
				EmailAddressIndex.Email2,
				EmailAddressIndex.Email3,
				EmailAddressIndex.BusinessFax,
				EmailAddressIndex.HomeFax,
				EmailAddressIndex.OtherFax
			};
			int i = 0;
			int num = 1;
			while (i < array.Length)
			{
				Participant participant = this.EmailAddresses[array[i]];
				if (participant != null && participant.RoutingType != null)
				{
					emailListType |= (EmailListType)num;
					list.Add(i);
				}
				i++;
				num <<= 1;
			}
			this[InternalSchema.EmailListType] = (int)emailListType;
			if (list.Count > 0)
			{
				this[InternalSchema.EmailList] = list.ToArray();
			}
			else
			{
				base.DeleteProperties(new PropertyDefinition[]
				{
					InternalSchema.EmailList
				});
			}
			base.SetOrDeleteProperty(ContactSchema.LegacyWebPage, base.TryGetProperty(ContactSchema.BusinessHomePage));
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x000D4BE4 File Offset: 0x000D2DE4
		private void OnBeforeSaveUpdateFaxParticipant(StorePropertyDefinition prop, EmailAddressIndex index)
		{
			if (base.IsNew || base.PropertyBag.IsPropertyDirty(prop))
			{
				string valueOrDefault = base.GetValueOrDefault<string>(prop);
				if (string.IsNullOrEmpty(valueOrDefault))
				{
					this.EmailAddresses.Remove(index);
					return;
				}
				Participant participant = this.EmailAddresses[index];
				if (participant == null || participant.EmailAddress != valueOrDefault || participant.RoutingType != "FAX")
				{
					string valueOrDefault2 = base.GetValueOrDefault<string>(StoreObjectSchema.DisplayName);
					Participant value = new Participant(valueOrDefault2, valueOrDefault, "FAX");
					this.EmailAddresses[index] = value;
				}
			}
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x000D4C84 File Offset: 0x000D2E84
		internal static void CoreObjectUpdateFileAs(CoreItem coreItem)
		{
			PersistablePropertyBag persistablePropertyBag = Microsoft.Exchange.Data.Storage.CoreObject.GetPersistablePropertyBag(coreItem);
			InternalSchema.FileAsString.UpdateCompositePropertyValue(persistablePropertyBag);
			InternalSchema.FileAsString.UpdateFullNameAndSubject(persistablePropertyBag);
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000D4CB0 File Offset: 0x000D2EB0
		internal static void CoreObjectUpdatePhysicalAddresses(CoreItem coreItem)
		{
			PersistablePropertyBag persistablePropertyBag = Microsoft.Exchange.Data.Storage.CoreObject.GetPersistablePropertyBag(coreItem);
			InternalSchema.HomeAddress.UpdateCompositePropertyValue(persistablePropertyBag);
			InternalSchema.BusinessAddress.UpdateCompositePropertyValue(persistablePropertyBag);
			InternalSchema.OtherAddress.UpdateCompositePropertyValue(persistablePropertyBag);
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000D4CE5 File Offset: 0x000D2EE5
		internal new static void CoreObjectUpdateAllAttachmentsHidden(CoreItem coreItem)
		{
			Microsoft.Exchange.Data.Storage.Item.EnsureAllAttachmentsHiddenValue(coreItem, true);
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000D4CF0 File Offset: 0x000D2EF0
		private void UpdateJunkEmailContacts()
		{
			if (this.junkEmailUpdates.Count > 0)
			{
				MailboxSession mailboxSession = base.Session as MailboxSession;
				if (mailboxSession != null && mailboxSession.LogonType != LogonType.Delegated && mailboxSession.Capabilities.CanHaveJunkEmailRule && !mailboxSession.MailboxOwner.ObjectId.IsNullOrEmpty())
				{
					JunkEmailRule junkEmailRule = mailboxSession.JunkEmailRule;
					if (junkEmailRule.IsContactsFolderTrusted)
					{
						try
						{
							junkEmailRule.SynchronizeContactsCache();
							junkEmailRule.Save();
						}
						catch (JunkEmailValidationException)
						{
						}
						catch (DataSourceOperationException ex)
						{
							throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex, this, "Contact.UpdateJunkEmailContacts. Failed due to directory exception {0}.", new object[]
							{
								ex
							});
						}
						catch (DataSourceTransientException ex2)
						{
							throw StorageGlobals.TranslateDirectoryException(ServerStrings.ADException, ex2, this, "Contact.UpdateJunkEmailContacts. Failed due to directory exception {0}.", new object[]
							{
								ex2
							});
						}
					}
				}
			}
		}

		// Token: 0x04001C05 RID: 7173
		private bool areEmailAddressesLoaded;

		// Token: 0x04001C06 RID: 7174
		private IList<string> junkEmailUpdates = new List<string>();

		// Token: 0x04001C07 RID: 7175
		private readonly CompleteName completeName;

		// Token: 0x04001C08 RID: 7176
		private readonly Contact.EmailAddressDictionary emailAddresses;

		// Token: 0x0200049E RID: 1182
		// (Invoke) Token: 0x06003492 RID: 13458
		private delegate void ModifyDistributionListsDelegate(DistributionList dl, DistributionListMember member);

		// Token: 0x0200049F RID: 1183
		private class CallbackCompleteName : CompleteName
		{
			// Token: 0x06003495 RID: 13461 RVA: 0x000D4DD8 File Offset: 0x000D2FD8
			public CallbackCompleteName(Contact contact)
			{
				this.contact = contact;
			}

			// Token: 0x17001062 RID: 4194
			// (get) Token: 0x06003496 RID: 13462 RVA: 0x000D4DE7 File Offset: 0x000D2FE7
			// (set) Token: 0x06003497 RID: 13463 RVA: 0x000D4E0E File Offset: 0x000D300E
			public override string FirstName
			{
				get
				{
					this.contact.CheckDisposed("CompleteName::FirstName::get");
					return this.contact.GetValueOrDefault<string>(ContactSchema.GivenName, string.Empty);
				}
				set
				{
					this.contact.CheckDisposed("CompleteName::FirstName::set");
					this.contact[ContactSchema.GivenName] = value;
				}
			}

			// Token: 0x17001063 RID: 4195
			// (get) Token: 0x06003498 RID: 13464 RVA: 0x000D4E31 File Offset: 0x000D3031
			// (set) Token: 0x06003499 RID: 13465 RVA: 0x000D4E58 File Offset: 0x000D3058
			public override string FullName
			{
				get
				{
					this.contact.CheckDisposed("CompleteName::FullName::get");
					return this.contact.GetValueOrDefault<string>(ContactSchema.FullName, string.Empty);
				}
				set
				{
					this.contact.CheckDisposed("CompleteName::FullName::set");
					this.contact[ContactSchema.FullName] = value;
				}
			}

			// Token: 0x17001064 RID: 4196
			// (get) Token: 0x0600349A RID: 13466 RVA: 0x000D4E7B File Offset: 0x000D307B
			// (set) Token: 0x0600349B RID: 13467 RVA: 0x000D4EA2 File Offset: 0x000D30A2
			public override string Initials
			{
				get
				{
					this.contact.CheckDisposed("CompleteName::Initials::get");
					return this.contact.GetValueOrDefault<string>(ContactSchema.Initials, string.Empty);
				}
				set
				{
					this.contact.CheckDisposed("CompleteName::Initials::set");
					this.contact[ContactSchema.Initials] = value;
				}
			}

			// Token: 0x17001065 RID: 4197
			// (get) Token: 0x0600349C RID: 13468 RVA: 0x000D4EC5 File Offset: 0x000D30C5
			// (set) Token: 0x0600349D RID: 13469 RVA: 0x000D4EEC File Offset: 0x000D30EC
			public override string LastName
			{
				get
				{
					this.contact.CheckDisposed("CompleteName::LastName::get");
					return this.contact.GetValueOrDefault<string>(ContactSchema.Surname, string.Empty);
				}
				set
				{
					this.contact.CheckDisposed("CompleteName::LastName::set");
					this.contact[ContactSchema.Surname] = value;
				}
			}

			// Token: 0x17001066 RID: 4198
			// (get) Token: 0x0600349E RID: 13470 RVA: 0x000D4F0F File Offset: 0x000D310F
			// (set) Token: 0x0600349F RID: 13471 RVA: 0x000D4F36 File Offset: 0x000D3136
			public override string MiddleName
			{
				get
				{
					this.contact.CheckDisposed("CompleteName::MiddleName::get");
					return this.contact.GetValueOrDefault<string>(ContactSchema.MiddleName, string.Empty);
				}
				set
				{
					this.contact.CheckDisposed("CompleteName::MiddleName::set");
					this.contact[ContactSchema.MiddleName] = value;
				}
			}

			// Token: 0x17001067 RID: 4199
			// (get) Token: 0x060034A0 RID: 13472 RVA: 0x000D4F59 File Offset: 0x000D3159
			// (set) Token: 0x060034A1 RID: 13473 RVA: 0x000D4F80 File Offset: 0x000D3180
			public override string Nickname
			{
				get
				{
					this.contact.CheckDisposed("CompleteName::Nickname::get");
					return this.contact.GetValueOrDefault<string>(ContactSchema.Nickname, string.Empty);
				}
				set
				{
					this.contact.CheckDisposed("CompleteName::Nickname::set");
					this.contact[ContactSchema.Nickname] = value;
				}
			}

			// Token: 0x17001068 RID: 4200
			// (get) Token: 0x060034A2 RID: 13474 RVA: 0x000D4FA3 File Offset: 0x000D31A3
			// (set) Token: 0x060034A3 RID: 13475 RVA: 0x000D4FCA File Offset: 0x000D31CA
			public override string Suffix
			{
				get
				{
					this.contact.CheckDisposed("CompleteName::Suffix::get");
					return this.contact.GetValueOrDefault<string>(ContactSchema.Generation, string.Empty);
				}
				set
				{
					this.contact.CheckDisposed("CompleteName::Suffix::set");
					this.contact[ContactSchema.Generation] = value;
				}
			}

			// Token: 0x17001069 RID: 4201
			// (get) Token: 0x060034A4 RID: 13476 RVA: 0x000D4FED File Offset: 0x000D31ED
			// (set) Token: 0x060034A5 RID: 13477 RVA: 0x000D5014 File Offset: 0x000D3214
			public override string Title
			{
				get
				{
					this.contact.CheckDisposed("CompleteName::Title::get");
					return this.contact.GetValueOrDefault<string>(ContactSchema.DisplayNamePrefix, string.Empty);
				}
				set
				{
					this.contact.CheckDisposed("CompleteName::Title::set");
					this.contact[ContactSchema.DisplayNamePrefix] = value;
				}
			}

			// Token: 0x1700106A RID: 4202
			// (get) Token: 0x060034A6 RID: 13478 RVA: 0x000D5037 File Offset: 0x000D3237
			// (set) Token: 0x060034A7 RID: 13479 RVA: 0x000D505E File Offset: 0x000D325E
			public override string YomiCompany
			{
				get
				{
					this.contact.CheckDisposed("CompleteName::YomiCompany::get");
					return this.contact.GetValueOrDefault<string>(ContactSchema.YomiCompany, string.Empty);
				}
				set
				{
					this.contact.CheckDisposed("CompleteName::YomiCompany::set");
					this.contact[ContactSchema.YomiCompany] = value;
				}
			}

			// Token: 0x1700106B RID: 4203
			// (get) Token: 0x060034A8 RID: 13480 RVA: 0x000D5081 File Offset: 0x000D3281
			// (set) Token: 0x060034A9 RID: 13481 RVA: 0x000D50A8 File Offset: 0x000D32A8
			public override string YomiFirstName
			{
				get
				{
					this.contact.CheckDisposed("CompleteName::YomiFirstName::get");
					return this.contact.GetValueOrDefault<string>(ContactSchema.YomiFirstName, string.Empty);
				}
				set
				{
					this.contact.CheckDisposed("CompleteName::YomiFirstName::set");
					this.contact[ContactSchema.YomiFirstName] = value;
				}
			}

			// Token: 0x1700106C RID: 4204
			// (get) Token: 0x060034AA RID: 13482 RVA: 0x000D50CB File Offset: 0x000D32CB
			// (set) Token: 0x060034AB RID: 13483 RVA: 0x000D50F2 File Offset: 0x000D32F2
			public override string YomiLastName
			{
				get
				{
					this.contact.CheckDisposed("CompleteName::YomiLastName::get");
					return this.contact.GetValueOrDefault<string>(ContactSchema.YomiLastName, string.Empty);
				}
				set
				{
					this.contact.CheckDisposed("CompleteName::YomiLastName::set");
					this.contact[ContactSchema.YomiLastName] = value;
				}
			}

			// Token: 0x04001C09 RID: 7177
			private readonly Contact contact;
		}

		// Token: 0x020004A0 RID: 1184
		private class EmailAddressDictionary : Dictionary<EmailAddressIndex, Participant>, IDictionary<EmailAddressIndex, Participant>, ICollection<KeyValuePair<EmailAddressIndex, Participant>>, IEnumerable<KeyValuePair<EmailAddressIndex, Participant>>, IEnumerable
		{
			// Token: 0x060034AC RID: 13484 RVA: 0x000D5115 File Offset: 0x000D3315
			public EmailAddressDictionary(Contact contact)
			{
				this.contact = contact;
			}

			// Token: 0x1700106D RID: 4205
			public new Participant this[EmailAddressIndex key]
			{
				get
				{
					EnumValidator.ThrowIfInvalid<EmailAddressIndex>(key, "key");
					if (base.ContainsKey(key))
					{
						return base[key];
					}
					return null;
				}
				set
				{
					EnumValidator.ThrowIfInvalid<EmailAddressIndex>(key, "key");
					this.InternalSet(key, value);
					this.contact.OnEmailAddressChanged(key);
				}
			}

			// Token: 0x060034AF RID: 13487 RVA: 0x000D5164 File Offset: 0x000D3364
			public new void Add(EmailAddressIndex key, Participant value)
			{
				EnumValidator.ThrowIfInvalid<EmailAddressIndex>(key, "key");
				Util.ThrowOnNullArgument(value, "value");
				Contact.EmailAddressDictionary.Check(key);
				base.Add(key, value);
				this.contact.OnEmailAddressChanged(key);
			}

			// Token: 0x060034B0 RID: 13488 RVA: 0x000D5198 File Offset: 0x000D3398
			public new bool Remove(EmailAddressIndex key)
			{
				EnumValidator.ThrowIfInvalid<EmailAddressIndex>(key, "key");
				bool result = base.Remove(key);
				this.contact.OnEmailAddressChanged(key);
				return result;
			}

			// Token: 0x060034B1 RID: 13489 RVA: 0x000D51C5 File Offset: 0x000D33C5
			void ICollection<KeyValuePair<EmailAddressIndex, Participant>>.Add(KeyValuePair<EmailAddressIndex, Participant> item)
			{
				this.Add(item.Key, item.Value);
			}

			// Token: 0x060034B2 RID: 13490 RVA: 0x000D51DC File Offset: 0x000D33DC
			public new void Clear()
			{
				List<EmailAddressIndex> list = new List<EmailAddressIndex>(base.Keys);
				base.Clear();
				foreach (EmailAddressIndex emailAddressIndex in list)
				{
					this.contact.OnEmailAddressChanged(emailAddressIndex);
				}
			}

			// Token: 0x060034B3 RID: 13491 RVA: 0x000D5244 File Offset: 0x000D3444
			public bool Remove(KeyValuePair<EmailAddressIndex, Participant> item)
			{
				return this.Remove(item.Key);
			}

			// Token: 0x060034B4 RID: 13492 RVA: 0x000D5253 File Offset: 0x000D3453
			private static void Check(EmailAddressIndex emailAddressIndex)
			{
				EnumValidator.ThrowIfInvalid<EmailAddressIndex>(emailAddressIndex, Contact.EmailAddressDictionary.validSet);
			}

			// Token: 0x060034B5 RID: 13493 RVA: 0x000D5260 File Offset: 0x000D3460
			internal void InternalSet(EmailAddressIndex key, Participant value)
			{
				EnumValidator.AssertValid<EmailAddressIndex>(key);
				Contact.EmailAddressDictionary.Check(key);
				if (value != null)
				{
					base[key] = value;
					return;
				}
				base.Remove(key);
			}

			// Token: 0x04001C0A RID: 7178
			private readonly Contact contact;

			// Token: 0x04001C0B RID: 7179
			private static EmailAddressIndex[] validSet = new EmailAddressIndex[]
			{
				EmailAddressIndex.Email1,
				EmailAddressIndex.Email2,
				EmailAddressIndex.Email3,
				EmailAddressIndex.BusinessFax,
				EmailAddressIndex.HomeFax,
				EmailAddressIndex.OtherFax
			};
		}
	}
}
