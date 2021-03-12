using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004E3 RID: 1251
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class EmailAddressProperties
	{
		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x060036B2 RID: 14002 RVA: 0x000DCF94 File Offset: 0x000DB194
		public static NativeStorePropertyDefinition[] AllProperties
		{
			get
			{
				if (EmailAddressProperties.allProperties == null)
				{
					List<NativeStorePropertyDefinition> list = new List<NativeStorePropertyDefinition>(EmailAddressProperties.PropertySets.Length * 3);
					foreach (EmailAddressProperties emailAddressProperties in EmailAddressProperties.PropertySets)
					{
						list.Add(emailAddressProperties.RoutingType);
						list.Add(emailAddressProperties.Address);
						list.Add(emailAddressProperties.DisplayName);
						list.Add(emailAddressProperties.OriginalDisplayName);
						list.Add(emailAddressProperties.OriginalEntryId);
					}
					EmailAddressProperties.allProperties = list.ToArray();
				}
				return EmailAddressProperties.allProperties;
			}
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x000DD01B File Offset: 0x000DB21B
		private EmailAddressProperties()
		{
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x060036B4 RID: 14004 RVA: 0x000DD023 File Offset: 0x000DB223
		// (set) Token: 0x060036B5 RID: 14005 RVA: 0x000DD02B File Offset: 0x000DB22B
		public NativeStorePropertyDefinition RoutingType { get; private set; }

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x060036B6 RID: 14006 RVA: 0x000DD034 File Offset: 0x000DB234
		// (set) Token: 0x060036B7 RID: 14007 RVA: 0x000DD03C File Offset: 0x000DB23C
		public NativeStorePropertyDefinition Address { get; private set; }

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x060036B8 RID: 14008 RVA: 0x000DD045 File Offset: 0x000DB245
		// (set) Token: 0x060036B9 RID: 14009 RVA: 0x000DD04D File Offset: 0x000DB24D
		public NativeStorePropertyDefinition DisplayName { get; private set; }

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x060036BA RID: 14010 RVA: 0x000DD056 File Offset: 0x000DB256
		// (set) Token: 0x060036BB RID: 14011 RVA: 0x000DD05E File Offset: 0x000DB25E
		public NativeStorePropertyDefinition OriginalDisplayName { get; private set; }

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x060036BC RID: 14012 RVA: 0x000DD067 File Offset: 0x000DB267
		// (set) Token: 0x060036BD RID: 14013 RVA: 0x000DD06F File Offset: 0x000DB26F
		public NativeStorePropertyDefinition OriginalEntryId { get; private set; }

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x060036BE RID: 14014 RVA: 0x000DD078 File Offset: 0x000DB278
		// (set) Token: 0x060036BF RID: 14015 RVA: 0x000DD080 File Offset: 0x000DB280
		public EmailAddressIndex EmailAddressIndex { get; private set; }

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x060036C0 RID: 14016 RVA: 0x000DD08C File Offset: 0x000DB28C
		public NativeStorePropertyDefinition[] Properties
		{
			get
			{
				return new NativeStorePropertyDefinition[]
				{
					this.RoutingType,
					this.Address,
					this.DisplayName,
					this.OriginalDisplayName,
					this.OriginalEntryId
				};
			}
		}

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x060036C1 RID: 14017 RVA: 0x000DD0D0 File Offset: 0x000DB2D0
		public SortBy[] AscendingSortBy
		{
			get
			{
				if (this.ascendingSortBy == null)
				{
					this.ascendingSortBy = new SortBy[]
					{
						new SortBy(this.Address, SortOrder.Ascending)
					};
				}
				return this.ascendingSortBy;
			}
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x000DD108 File Offset: 0x000DB308
		public EmailAddress GetFrom(IStorePropertyBag propertyBag)
		{
			string valueOrDefault = propertyBag.GetValueOrDefault<string>(this.Address, null);
			string valueOrDefault2 = propertyBag.GetValueOrDefault<string>(this.DisplayName, null);
			string valueOrDefault3 = propertyBag.GetValueOrDefault<string>(this.OriginalDisplayName, null);
			string text = propertyBag.GetValueOrDefault<string>(this.RoutingType, null);
			if (!string.IsNullOrEmpty(valueOrDefault) && string.IsNullOrEmpty(text))
			{
				Participant participant = null;
				if (Participant.TryParse(valueOrDefault, out participant))
				{
					text = participant.RoutingType;
				}
			}
			return new EmailAddress
			{
				RoutingType = text,
				Address = valueOrDefault,
				Name = valueOrDefault2,
				OriginalDisplayName = valueOrDefault3
			};
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x000DD19C File Offset: 0x000DB39C
		public void SetTo(IStorePropertyBag propertyBag, EmailAddress emailAddress)
		{
			EmailAddressProperties.SetOrDeleteValue(propertyBag, this.RoutingType, emailAddress.RoutingType);
			EmailAddressProperties.SetOrDeleteValue(propertyBag, this.Address, emailAddress.Address);
			EmailAddressProperties.SetOrDeleteValue(propertyBag, this.DisplayName, emailAddress.Name);
			EmailAddressProperties.SetOrDeleteValue(propertyBag, this.OriginalDisplayName, emailAddress.OriginalDisplayName);
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x000DD1F1 File Offset: 0x000DB3F1
		private static void SetOrDeleteValue(IStorePropertyBag propertyBag, NativeStorePropertyDefinition property, string value)
		{
			if (value == null)
			{
				propertyBag.Delete(property);
				return;
			}
			propertyBag[property] = value;
		}

		// Token: 0x04001D36 RID: 7478
		private static NativeStorePropertyDefinition[] allProperties;

		// Token: 0x04001D37 RID: 7479
		public static readonly EmailAddressProperties Email1 = new EmailAddressProperties
		{
			RoutingType = InternalSchema.Email1AddrType,
			Address = InternalSchema.Email1EmailAddress,
			DisplayName = InternalSchema.Email1DisplayName,
			OriginalDisplayName = InternalSchema.Email1OriginalDisplayName,
			EmailAddressIndex = EmailAddressIndex.Email1,
			OriginalEntryId = InternalSchema.Email1OriginalEntryID
		};

		// Token: 0x04001D38 RID: 7480
		public static readonly EmailAddressProperties Email2 = new EmailAddressProperties
		{
			RoutingType = InternalSchema.Email2AddrType,
			Address = InternalSchema.Email2EmailAddress,
			DisplayName = InternalSchema.Email2DisplayName,
			OriginalDisplayName = InternalSchema.Email2OriginalDisplayName,
			EmailAddressIndex = EmailAddressIndex.Email2,
			OriginalEntryId = InternalSchema.Email2OriginalEntryID
		};

		// Token: 0x04001D39 RID: 7481
		public static readonly EmailAddressProperties Email3 = new EmailAddressProperties
		{
			RoutingType = InternalSchema.Email3AddrType,
			Address = InternalSchema.Email3EmailAddress,
			DisplayName = InternalSchema.Email3DisplayName,
			OriginalDisplayName = InternalSchema.Email3OriginalDisplayName,
			EmailAddressIndex = EmailAddressIndex.Email3,
			OriginalEntryId = InternalSchema.Email3OriginalEntryID
		};

		// Token: 0x04001D3A RID: 7482
		public static readonly EmailAddressProperties[] PropertySets = new EmailAddressProperties[]
		{
			EmailAddressProperties.Email1,
			EmailAddressProperties.Email2,
			EmailAddressProperties.Email3
		};

		// Token: 0x04001D3B RID: 7483
		private SortBy[] ascendingSortBy;
	}
}
