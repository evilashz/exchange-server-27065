using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000515 RID: 1301
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class OfflineAddressBook : ADLegacyVersionableObject
	{
		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x06003955 RID: 14677 RVA: 0x000DDC16 File Offset: 0x000DBE16
		internal override ADObjectSchema Schema
		{
			get
			{
				return OfflineAddressBook.schema;
			}
		}

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x06003956 RID: 14678 RVA: 0x000DDC1D File Offset: 0x000DBE1D
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchOAB";
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x06003957 RID: 14679 RVA: 0x000DDC24 File Offset: 0x000DBE24
		internal override ADObjectId ParentPath
		{
			get
			{
				return OfflineAddressBook.ParentPathInternal;
			}
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x000DDC2C File Offset: 0x000DBE2C
		internal static object DiffRetentionPeriodGetter(IPropertyBag propertyBag)
		{
			Unlimited<int> unlimited = (Unlimited<int>)propertyBag[OfflineAddressBookSchema.RawDiffRetentionPeriod];
			if (unlimited.IsUnlimited)
			{
				ExchangeObjectVersion exchangeObjectVersion = (ExchangeObjectVersion)propertyBag[ADObjectSchema.ExchangeVersion];
				if (exchangeObjectVersion.IsOlderThan(ExchangeObjectVersion.Exchange2007))
				{
					return null;
				}
			}
			return unlimited;
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x000DDC79 File Offset: 0x000DBE79
		internal static void DiffRetentionPeriodSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[OfflineAddressBookSchema.RawDiffRetentionPeriod] = (value ?? Unlimited<int>.UnlimitedValue);
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x000DDC98 File Offset: 0x000DBE98
		internal static object VersionsGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<OfflineAddressBookVersion> multiValuedProperty = new MultiValuedProperty<OfflineAddressBookVersion>();
			object obj = propertyBag[OfflineAddressBookSchema.RawVersion];
			int num = (int)(obj ?? OfflineAddressBookSchema.RawVersion.DefaultValue);
			if ((1 & num) != 0)
			{
				multiValuedProperty.Add(OfflineAddressBookVersion.Version1);
			}
			num = ~num;
			if ((2 & num) != 0)
			{
				multiValuedProperty.Add(OfflineAddressBookVersion.Version2);
			}
			if ((4 & num) != 0)
			{
				multiValuedProperty.Add(OfflineAddressBookVersion.Version3);
			}
			if ((8 & num) != 0)
			{
				multiValuedProperty.Add(OfflineAddressBookVersion.Version4);
			}
			return multiValuedProperty;
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x000DDD00 File Offset: 0x000DBF00
		internal static void VersionsSetter(object value, IPropertyBag propertyBag)
		{
			MultiValuedProperty<OfflineAddressBookVersion> multiValuedProperty = value as MultiValuedProperty<OfflineAddressBookVersion>;
			if (multiValuedProperty != null && 0 < multiValuedProperty.Count)
			{
				object obj = propertyBag[OfflineAddressBookSchema.RawVersion];
				int num = (int)(obj ?? OfflineAddressBookSchema.RawVersion.DefaultValue);
				num |= 14;
				num &= -2;
				foreach (OfflineAddressBookVersion offlineAddressBookVersion in multiValuedProperty)
				{
					if (offlineAddressBookVersion == OfflineAddressBookVersion.Version1)
					{
						num |= 1;
					}
					else
					{
						num &= (int)(~(int)offlineAddressBookVersion);
					}
				}
				propertyBag[OfflineAddressBookSchema.RawVersion] = num;
				return;
			}
			throw new OfflineAddressBookVersionsNullException();
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x000DDDB0 File Offset: 0x000DBFB0
		internal static void ScheduleSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[OfflineAddressBookSchema.ScheduleBitmaps] = value;
			if (value == null)
			{
				propertyBag[OfflineAddressBookSchema.ScheduleMode] = ScheduleMode.Never;
				return;
			}
			propertyBag[OfflineAddressBookSchema.ScheduleMode] = ((Schedule)value).Mode;
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x000DDDF0 File Offset: 0x000DBFF0
		internal static object WebDistributionEnabledGetter(IPropertyBag propertyBag)
		{
			object obj = propertyBag[OfflineAddressBookSchema.VirtualDirectories];
			object obj2 = propertyBag[OfflineAddressBookSchema.OabFlags];
			return ((MultiValuedProperty<ADObjectId>)obj).Count != 0 || ((int)obj2 & 2) != 0;
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x000DDE38 File Offset: 0x000DC038
		internal void ResolveConfiguredAttributes()
		{
			MultiValuedProperty<int> multiValuedProperty = (MultiValuedProperty<int>)this.propertyBag[OfflineAddressBookSchema.ANRProperties];
			MultiValuedProperty<int> multiValuedProperty2 = (MultiValuedProperty<int>)this.propertyBag[OfflineAddressBookSchema.DetailsProperties];
			MultiValuedProperty<int> multiValuedProperty3 = (MultiValuedProperty<int>)this.propertyBag[OfflineAddressBookSchema.TruncatedProperties];
			MultiValuedProperty<OfflineAddressBookMapiProperty> multiValuedProperty4 = (MultiValuedProperty<OfflineAddressBookMapiProperty>)this.propertyBag[OfflineAddressBookSchema.ConfiguredAttributes];
			bool isReadOnly = multiValuedProperty4.IsReadOnly;
			if (isReadOnly)
			{
				multiValuedProperty4 = new MultiValuedProperty<OfflineAddressBookMapiProperty>();
			}
			foreach (int num in multiValuedProperty)
			{
				if (num != 0)
				{
					OfflineAddressBookMapiProperty oabmapiProperty = OfflineAddressBookMapiProperty.GetOABMapiProperty((uint)num, OfflineAddressBookMapiPropertyOption.ANR);
					multiValuedProperty4.Add(oabmapiProperty);
				}
			}
			foreach (int num2 in multiValuedProperty2)
			{
				if (num2 != 0)
				{
					OfflineAddressBookMapiProperty oabmapiProperty = OfflineAddressBookMapiProperty.GetOABMapiProperty((uint)num2, OfflineAddressBookMapiPropertyOption.Value);
					if (!multiValuedProperty4.Contains(oabmapiProperty))
					{
						multiValuedProperty4.Add(oabmapiProperty);
					}
				}
			}
			foreach (int num3 in multiValuedProperty3)
			{
				if (num3 != 0)
				{
					OfflineAddressBookMapiProperty oabmapiProperty = OfflineAddressBookMapiProperty.GetOABMapiProperty((uint)num3, OfflineAddressBookMapiPropertyOption.Indicator);
					if (!multiValuedProperty4.Contains(oabmapiProperty))
					{
						multiValuedProperty4.Add(oabmapiProperty);
					}
				}
			}
			multiValuedProperty4.ResetChangeTracking();
			if (isReadOnly)
			{
				this.propertyBag.SetField(OfflineAddressBookSchema.ConfiguredAttributes, multiValuedProperty4);
			}
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x000DDFD0 File Offset: 0x000DC1D0
		internal void UpdateRawMapiAttributes(bool movingToPreE14Server)
		{
			MultiValuedProperty<OfflineAddressBookMapiProperty> multiValuedProperty = (MultiValuedProperty<OfflineAddressBookMapiProperty>)this.propertyBag[OfflineAddressBookSchema.ConfiguredAttributes];
			MultiValuedProperty<OfflineAddressBookMapiProperty> multiValuedProperty2 = new MultiValuedProperty<OfflineAddressBookMapiProperty>();
			foreach (OfflineAddressBookMapiProperty offlineAddressBookMapiProperty in multiValuedProperty)
			{
				offlineAddressBookMapiProperty.ResolveMapiPropTag();
				if (multiValuedProperty2.Contains(offlineAddressBookMapiProperty))
				{
					throw new ArgumentException(DirectoryStrings.ErrorDuplicateMapiIdsInConfiguredAttributes);
				}
				multiValuedProperty2.Add(offlineAddressBookMapiProperty);
			}
			MultiValuedProperty<int> multiValuedProperty3 = new MultiValuedProperty<int>();
			MultiValuedProperty<int> multiValuedProperty4 = new MultiValuedProperty<int>();
			MultiValuedProperty<int> multiValuedProperty5 = new MultiValuedProperty<int>();
			foreach (OfflineAddressBookMapiProperty offlineAddressBookMapiProperty2 in multiValuedProperty2)
			{
				switch (offlineAddressBookMapiProperty2.Type)
				{
				case OfflineAddressBookMapiPropertyOption.ANR:
					multiValuedProperty3.Add((int)offlineAddressBookMapiProperty2.PropertyTag);
					break;
				case OfflineAddressBookMapiPropertyOption.Value:
					multiValuedProperty4.Add((int)offlineAddressBookMapiProperty2.PropertyTag);
					break;
				case OfflineAddressBookMapiPropertyOption.Indicator:
					multiValuedProperty5.Add((int)offlineAddressBookMapiProperty2.PropertyTag);
					break;
				}
			}
			if (multiValuedProperty3.Count == 0 && !movingToPreE14Server)
			{
				multiValuedProperty3.Add(0);
			}
			if (multiValuedProperty4.Count == 0 && !movingToPreE14Server)
			{
				multiValuedProperty4.Add(0);
			}
			if (multiValuedProperty5.Count == 0 && !movingToPreE14Server)
			{
				multiValuedProperty5.Add(0);
			}
			this.propertyBag[OfflineAddressBookSchema.ANRProperties] = multiValuedProperty3;
			this.propertyBag[OfflineAddressBookSchema.DetailsProperties] = multiValuedProperty4;
			this.propertyBag[OfflineAddressBookSchema.TruncatedProperties] = multiValuedProperty5;
			if (multiValuedProperty.IsReadOnly)
			{
				this.propertyBag.SetField(OfflineAddressBookSchema.ConfiguredAttributes, new MultiValuedProperty<OfflineAddressBookMapiProperty>());
				return;
			}
			multiValuedProperty.Clear();
			multiValuedProperty.ResetChangeTracking();
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x06003960 RID: 14688 RVA: 0x000DE18C File Offset: 0x000DC38C
		// (set) Token: 0x06003961 RID: 14689 RVA: 0x000DE19E File Offset: 0x000DC39E
		public ADObjectId GeneratingMailbox
		{
			get
			{
				return (ADObjectId)this[OfflineAddressBookSchema.GeneratingMailbox];
			}
			set
			{
				this[OfflineAddressBookSchema.GeneratingMailbox] = value;
			}
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x06003962 RID: 14690 RVA: 0x000DE1AC File Offset: 0x000DC3AC
		// (set) Token: 0x06003963 RID: 14691 RVA: 0x000DE1BE File Offset: 0x000DC3BE
		internal ADObjectId Server
		{
			get
			{
				return (ADObjectId)this[OfflineAddressBookSchema.Server];
			}
			set
			{
				this[OfflineAddressBookSchema.Server] = value;
			}
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x06003964 RID: 14692 RVA: 0x000DE1CC File Offset: 0x000DC3CC
		// (set) Token: 0x06003965 RID: 14693 RVA: 0x000DE1DE File Offset: 0x000DC3DE
		public MultiValuedProperty<ADObjectId> AddressLists
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[OfflineAddressBookSchema.AddressLists];
			}
			set
			{
				this[OfflineAddressBookSchema.AddressLists] = value;
			}
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x06003966 RID: 14694 RVA: 0x000DE1EC File Offset: 0x000DC3EC
		// (set) Token: 0x06003967 RID: 14695 RVA: 0x000DE1FE File Offset: 0x000DC3FE
		[Parameter]
		[ValidateNotNullOrEmpty]
		public MultiValuedProperty<OfflineAddressBookVersion> Versions
		{
			get
			{
				return (MultiValuedProperty<OfflineAddressBookVersion>)this[OfflineAddressBookSchema.Versions];
			}
			set
			{
				this[OfflineAddressBookSchema.Versions] = value;
			}
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x06003968 RID: 14696 RVA: 0x000DE20C File Offset: 0x000DC40C
		// (set) Token: 0x06003969 RID: 14697 RVA: 0x000DE21E File Offset: 0x000DC41E
		[Parameter]
		public bool IsDefault
		{
			get
			{
				return (bool)this[OfflineAddressBookSchema.IsDefault];
			}
			set
			{
				this[OfflineAddressBookSchema.IsDefault] = value;
			}
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x0600396A RID: 14698 RVA: 0x000DE231 File Offset: 0x000DC431
		// (set) Token: 0x0600396B RID: 14699 RVA: 0x000DE243 File Offset: 0x000DC443
		public ADObjectId PublicFolderDatabase
		{
			get
			{
				return (ADObjectId)this[OfflineAddressBookSchema.PublicFolderDatabase];
			}
			internal set
			{
				this[OfflineAddressBookSchema.PublicFolderDatabase] = value;
			}
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x0600396C RID: 14700 RVA: 0x000DE251 File Offset: 0x000DC451
		// (set) Token: 0x0600396D RID: 14701 RVA: 0x000DE263 File Offset: 0x000DC463
		[Parameter]
		public bool PublicFolderDistributionEnabled
		{
			get
			{
				return (bool)this[OfflineAddressBookSchema.PublicFolderDistributionEnabled];
			}
			set
			{
				this[OfflineAddressBookSchema.PublicFolderDistributionEnabled] = value;
			}
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x0600396E RID: 14702 RVA: 0x000DE276 File Offset: 0x000DC476
		// (set) Token: 0x0600396F RID: 14703 RVA: 0x000DE288 File Offset: 0x000DC488
		[Parameter]
		public bool GlobalWebDistributionEnabled
		{
			get
			{
				return (bool)this[OfflineAddressBookSchema.GlobalWebDistributionEnabled];
			}
			set
			{
				this[OfflineAddressBookSchema.GlobalWebDistributionEnabled] = value;
			}
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06003970 RID: 14704 RVA: 0x000DE29B File Offset: 0x000DC49B
		public bool WebDistributionEnabled
		{
			get
			{
				return (bool)this[OfflineAddressBookSchema.WebDistributionEnabled];
			}
		}

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06003971 RID: 14705 RVA: 0x000DE2AD File Offset: 0x000DC4AD
		// (set) Token: 0x06003972 RID: 14706 RVA: 0x000DE2BF File Offset: 0x000DC4BF
		[Parameter]
		public bool ShadowMailboxDistributionEnabled
		{
			get
			{
				return (bool)this[OfflineAddressBookSchema.ShadowMailboxDistributionEnabled];
			}
			set
			{
				this[OfflineAddressBookSchema.ShadowMailboxDistributionEnabled] = value;
			}
		}

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06003973 RID: 14707 RVA: 0x000DE2D2 File Offset: 0x000DC4D2
		// (set) Token: 0x06003974 RID: 14708 RVA: 0x000DE2E4 File Offset: 0x000DC4E4
		public DateTime? LastTouchedTime
		{
			get
			{
				return (DateTime?)this[OfflineAddressBookSchema.LastTouchedTime];
			}
			internal set
			{
				this[OfflineAddressBookSchema.LastTouchedTime] = value;
			}
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x06003975 RID: 14709 RVA: 0x000DE2F7 File Offset: 0x000DC4F7
		// (set) Token: 0x06003976 RID: 14710 RVA: 0x000DE309 File Offset: 0x000DC509
		public DateTime? LastRequestedTime
		{
			get
			{
				return (DateTime?)this[OfflineAddressBookSchema.LastRequestedTime];
			}
			internal set
			{
				this[OfflineAddressBookSchema.LastRequestedTime] = value;
			}
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06003977 RID: 14711 RVA: 0x000DE31C File Offset: 0x000DC51C
		// (set) Token: 0x06003978 RID: 14712 RVA: 0x000DE32E File Offset: 0x000DC52E
		public DateTime? LastFailedTime
		{
			get
			{
				return (DateTime?)this[OfflineAddressBookSchema.LastFailedTime];
			}
			internal set
			{
				this[OfflineAddressBookSchema.LastFailedTime] = value;
			}
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06003979 RID: 14713 RVA: 0x000DE341 File Offset: 0x000DC541
		// (set) Token: 0x0600397A RID: 14714 RVA: 0x000DE353 File Offset: 0x000DC553
		public int? LastNumberOfRecords
		{
			get
			{
				return (int?)this[OfflineAddressBookSchema.LastNumberOfRecords];
			}
			internal set
			{
				this[OfflineAddressBookSchema.LastNumberOfRecords] = value;
			}
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x0600397B RID: 14715 RVA: 0x000DE366 File Offset: 0x000DC566
		// (set) Token: 0x0600397C RID: 14716 RVA: 0x000DE378 File Offset: 0x000DC578
		public OfflineAddressBookLastGeneratingData LastGeneratingData
		{
			get
			{
				return (OfflineAddressBookLastGeneratingData)this[OfflineAddressBookSchema.LastGeneratingData];
			}
			internal set
			{
				this[OfflineAddressBookSchema.LastGeneratingData] = value;
			}
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x0600397D RID: 14717 RVA: 0x000DE386 File Offset: 0x000DC586
		// (set) Token: 0x0600397E RID: 14718 RVA: 0x000DE398 File Offset: 0x000DC598
		[Parameter]
		public int MaxBinaryPropertySize
		{
			get
			{
				return (int)this[OfflineAddressBookSchema.MaxBinaryPropertySize];
			}
			set
			{
				this[OfflineAddressBookSchema.MaxBinaryPropertySize] = value;
			}
		}

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x0600397F RID: 14719 RVA: 0x000DE3AB File Offset: 0x000DC5AB
		// (set) Token: 0x06003980 RID: 14720 RVA: 0x000DE3BD File Offset: 0x000DC5BD
		[Parameter]
		public int MaxMultivaluedBinaryPropertySize
		{
			get
			{
				return (int)this[OfflineAddressBookSchema.MaxMultivaluedBinaryPropertySize];
			}
			set
			{
				this[OfflineAddressBookSchema.MaxMultivaluedBinaryPropertySize] = value;
			}
		}

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06003981 RID: 14721 RVA: 0x000DE3D0 File Offset: 0x000DC5D0
		// (set) Token: 0x06003982 RID: 14722 RVA: 0x000DE3E2 File Offset: 0x000DC5E2
		[Parameter]
		public int MaxStringPropertySize
		{
			get
			{
				return (int)this[OfflineAddressBookSchema.MaxStringPropertySize];
			}
			set
			{
				this[OfflineAddressBookSchema.MaxStringPropertySize] = value;
			}
		}

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06003983 RID: 14723 RVA: 0x000DE3F5 File Offset: 0x000DC5F5
		// (set) Token: 0x06003984 RID: 14724 RVA: 0x000DE407 File Offset: 0x000DC607
		[Parameter]
		public int MaxMultivaluedStringPropertySize
		{
			get
			{
				return (int)this[OfflineAddressBookSchema.MaxMultivaluedStringPropertySize];
			}
			set
			{
				this[OfflineAddressBookSchema.MaxMultivaluedStringPropertySize] = value;
			}
		}

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06003985 RID: 14725 RVA: 0x000DE41A File Offset: 0x000DC61A
		// (set) Token: 0x06003986 RID: 14726 RVA: 0x000DE42C File Offset: 0x000DC62C
		[Parameter]
		public MultiValuedProperty<OfflineAddressBookMapiProperty> ConfiguredAttributes
		{
			get
			{
				return (MultiValuedProperty<OfflineAddressBookMapiProperty>)this[OfflineAddressBookSchema.ConfiguredAttributes];
			}
			set
			{
				this[OfflineAddressBookSchema.ConfiguredAttributes] = value;
			}
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06003987 RID: 14727 RVA: 0x000DE43A File Offset: 0x000DC63A
		// (set) Token: 0x06003988 RID: 14728 RVA: 0x000DE44C File Offset: 0x000DC64C
		[Parameter]
		public Unlimited<int>? DiffRetentionPeriod
		{
			get
			{
				return (Unlimited<int>?)this[OfflineAddressBookSchema.DiffRetentionPeriod];
			}
			set
			{
				this[OfflineAddressBookSchema.DiffRetentionPeriod] = value;
			}
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06003989 RID: 14729 RVA: 0x000DE45F File Offset: 0x000DC65F
		// (set) Token: 0x0600398A RID: 14730 RVA: 0x000DE471 File Offset: 0x000DC671
		[Parameter]
		public Schedule Schedule
		{
			get
			{
				return (Schedule)this[OfflineAddressBookSchema.Schedule];
			}
			set
			{
				this[OfflineAddressBookSchema.Schedule] = value;
			}
		}

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x0600398B RID: 14731 RVA: 0x000DE47F File Offset: 0x000DC67F
		// (set) Token: 0x0600398C RID: 14732 RVA: 0x000DE491 File Offset: 0x000DC691
		public MultiValuedProperty<ADObjectId> VirtualDirectories
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[OfflineAddressBookSchema.VirtualDirectories];
			}
			set
			{
				this[OfflineAddressBookSchema.VirtualDirectories] = value;
			}
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x0600398D RID: 14733 RVA: 0x000DE49F File Offset: 0x000DC69F
		// (set) Token: 0x0600398E RID: 14734 RVA: 0x000DE4B1 File Offset: 0x000DC6B1
		public new ExchangeObjectVersion ExchangeVersion
		{
			get
			{
				return (ExchangeObjectVersion)this[ADObjectSchema.ExchangeVersion];
			}
			internal set
			{
				base.SetExchangeVersion(value);
			}
		}

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x0600398F RID: 14735 RVA: 0x000DE4BA File Offset: 0x000DC6BA
		// (set) Token: 0x06003990 RID: 14736 RVA: 0x000DE4CC File Offset: 0x000DC6CC
		internal OfflineAddressBookManifestVersion ManifestVersion
		{
			get
			{
				return (OfflineAddressBookManifestVersion)this[OfflineAddressBookSchema.ManifestVersion];
			}
			set
			{
				this[OfflineAddressBookSchema.ManifestVersion] = value;
			}
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x000DE4DA File Offset: 0x000DC6DA
		internal bool IsE15OrLater()
		{
			return ExchangeObjectVersion.Exchange2012.CompareTo(this.ExchangeVersion) <= 0;
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06003992 RID: 14738 RVA: 0x000DE4F2 File Offset: 0x000DC6F2
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06003993 RID: 14739 RVA: 0x000DE4F9 File Offset: 0x000DC6F9
		internal override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06003994 RID: 14740 RVA: 0x000DE4FC File Offset: 0x000DC6FC
		internal override SystemFlagsEnum SystemFlags
		{
			get
			{
				return (SystemFlagsEnum)this[OfflineAddressBookSchema.SystemFlags];
			}
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x000DE510 File Offset: 0x000DC710
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(OfflineAddressBookSchema.OfflineAddressBookFolder))
			{
				PropertyDefinition offlineAddressBookFolder = OfflineAddressBookSchema.OfflineAddressBookFolder;
				byte[] value = new byte[1];
				this[offlineAddressBookFolder] = value;
			}
			if (!base.IsModified(OfflineAddressBookSchema.SiteFolderGuid))
			{
				this[OfflineAddressBookSchema.SiteFolderGuid] = Guid.NewGuid().ToByteArray();
			}
			if (!base.IsModified(OfflineAddressBookSchema.Schedule))
			{
				this.Schedule = Schedule.Daily5AM;
			}
			if (!base.IsModified(OfflineAddressBookSchema.DiffRetentionPeriod))
			{
				this.DiffRetentionPeriod = new Unlimited<int>?(OfflineAddressBook.DefaultDiffRetentionPeriod);
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x000DE5A4 File Offset: 0x000DC7A4
		private bool DuplicateConfiguredAttributesDefined()
		{
			if (this.ConfiguredAttributes.Count > 0)
			{
				return false;
			}
			MultiValuedProperty<int> multiValuedProperty = (MultiValuedProperty<int>)this.propertyBag[OfflineAddressBookSchema.ANRProperties];
			MultiValuedProperty<int> multiValuedProperty2 = (MultiValuedProperty<int>)this.propertyBag[OfflineAddressBookSchema.DetailsProperties];
			MultiValuedProperty<int> multiValuedProperty3 = (MultiValuedProperty<int>)this.propertyBag[OfflineAddressBookSchema.TruncatedProperties];
			Dictionary<int, OfflineAddressBookMapiProperty> dictionary = new Dictionary<int, OfflineAddressBookMapiProperty>();
			foreach (int num in multiValuedProperty)
			{
				if (num != 0)
				{
					OfflineAddressBookMapiProperty oabmapiProperty = OfflineAddressBookMapiProperty.GetOABMapiProperty((uint)num, OfflineAddressBookMapiPropertyOption.ANR);
					if (dictionary.ContainsKey(oabmapiProperty.MapiID))
					{
						return true;
					}
					dictionary.Add(oabmapiProperty.MapiID, oabmapiProperty);
				}
			}
			foreach (int num2 in multiValuedProperty2)
			{
				if (num2 != 0)
				{
					OfflineAddressBookMapiProperty oabmapiProperty = OfflineAddressBookMapiProperty.GetOABMapiProperty((uint)num2, OfflineAddressBookMapiPropertyOption.Value);
					if (dictionary.ContainsKey(oabmapiProperty.MapiID))
					{
						return true;
					}
					dictionary.Add(oabmapiProperty.MapiID, oabmapiProperty);
				}
			}
			foreach (int num3 in multiValuedProperty3)
			{
				if (num3 != 0)
				{
					OfflineAddressBookMapiProperty oabmapiProperty = OfflineAddressBookMapiProperty.GetOABMapiProperty((uint)num3, OfflineAddressBookMapiPropertyOption.Indicator);
					if (dictionary.ContainsKey(oabmapiProperty.MapiID))
					{
						return true;
					}
					dictionary.Add(oabmapiProperty.MapiID, oabmapiProperty);
				}
			}
			return false;
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x000DE758 File Offset: 0x000DC958
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			if (this.DuplicateConfiguredAttributesDefined())
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorDuplicateMapiIdsInConfiguredAttributes, this.Identity, string.Empty));
			}
			if (this.GlobalWebDistributionEnabled && this.VirtualDirectories != null && this.VirtualDirectories.Count > 0)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorGlobalWebDistributionAndVDirsSet(this.Identity.ToString()), this.Identity, string.Empty));
			}
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x000DE7D4 File Offset: 0x000DC9D4
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (!this.PublicFolderDistributionEnabled && (this.Versions.Contains(OfflineAddressBookVersion.Version1) || this.Versions.Contains(OfflineAddressBookVersion.Version2) || this.Versions.Contains(OfflineAddressBookVersion.Version3)))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorLegacyVersionOfflineAddressBookWithoutPublicFolderDatabase(this.Identity.ToString()), this.Identity, string.Empty));
			}
			if (this.WebDistributionEnabled && !this.Versions.Contains(OfflineAddressBookVersion.Version4))
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorWebDistributionEnabledWithoutVersion4(this.Identity.ToString()), this.Identity, string.Empty));
			}
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x000DE87C File Offset: 0x000DCA7C
		internal bool CheckForAssociatedAddressBookPolicies()
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.DistinguishedName, base.Id.DistinguishedName),
				new ExistsFilter(OfflineAddressBookSchema.AssociatedAddressBookPolicies)
			});
			if (base.Session != null)
			{
				OfflineAddressBook[] array = base.Session.Find<OfflineAddressBook>(null, QueryScope.SubTree, filter, null, 1);
				return array != null && array.Length > 0;
			}
			return true;
		}

		// Token: 0x0400275F RID: 10079
		public const string MostDerivedClass = "msExchOAB";

		// Token: 0x04002760 RID: 10080
		private static OfflineAddressBookSchema schema = ObjectSchema.GetInstance<OfflineAddressBookSchema>();

		// Token: 0x04002761 RID: 10081
		internal static readonly ADObjectId RdnContainer = new ADObjectId("CN=Offline Address Lists,CN=Address Lists Container");

		// Token: 0x04002762 RID: 10082
		public static readonly ADObjectId ParentPathInternal = new ADObjectId("CN=Offline Address Lists,CN=Address Lists Container");

		// Token: 0x04002763 RID: 10083
		public static readonly string DefaultName = DirectoryStrings.DefaultOabName;

		// Token: 0x04002764 RID: 10084
		internal static readonly int DefaultDiffRetentionPeriod = 30;
	}
}
