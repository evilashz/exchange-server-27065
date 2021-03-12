using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A04 RID: 2564
	[Serializable]
	public sealed class MailboxCalendarFolder : ConfigurableObject
	{
		// Token: 0x06005E08 RID: 24072 RVA: 0x0018D818 File Offset: 0x0018BA18
		internal static object PublishedUrlGetter(IPropertyBag propertyBag, SimpleProviderPropertyDefinition propertyDefinition)
		{
			string text = (string)propertyBag[propertyDefinition];
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			PublishingUrl publishingUrl = PublishingUrl.Create(text);
			ObscureUrl obscureUrl = publishingUrl as ObscureUrl;
			if (obscureUrl == null)
			{
				return text;
			}
			return obscureUrl.ChangeToKind(ObscureKind.Normal).ToString();
		}

		// Token: 0x06005E09 RID: 24073 RVA: 0x0018D85C File Offset: 0x0018BA5C
		internal static void PublishedUrlSetter(object value, IPropertyBag propertyBag, SimpleProviderPropertyDefinition propertyDefinition)
		{
			string text = (string)value;
			if (string.IsNullOrEmpty(text))
			{
				propertyBag[propertyDefinition] = null;
				return;
			}
			PublishingUrl publishingUrl = PublishingUrl.Create(text);
			ObscureUrl obscureUrl = publishingUrl as ObscureUrl;
			if (obscureUrl == null)
			{
				propertyBag[propertyDefinition] = text;
				return;
			}
			propertyBag[propertyDefinition] = obscureUrl.ChangeToKind(ObscureKind.Restricted).ToString();
		}

		// Token: 0x06005E0A RID: 24074 RVA: 0x0018D8AE File Offset: 0x0018BAAE
		private T EnumGetter<T>(SimpleProviderPropertyDefinition propertyDefinition)
		{
			if (!Enum.IsDefined(typeof(T), this[propertyDefinition]))
			{
				return (T)((object)propertyDefinition.DefaultValue);
			}
			return (T)((object)this[propertyDefinition]);
		}

		// Token: 0x06005E0B RID: 24075 RVA: 0x0018D8E0 File Offset: 0x0018BAE0
		private void EnumSetter<T>(T value, SimpleProviderPropertyDefinition propertyDefinition)
		{
			if (!Enum.IsDefined(typeof(T), value))
			{
				throw new ArgumentOutOfRangeException();
			}
			this[propertyDefinition] = value;
		}

		// Token: 0x06005E0C RID: 24076 RVA: 0x0018D90C File Offset: 0x0018BB0C
		public MailboxCalendarFolder() : base(new SimplePropertyBag(MailboxCalendarFolderSchema.MailboxFolderId, UserConfigurationObjectSchema.ObjectState, UserConfigurationObjectSchema.ExchangeVersion))
		{
		}

		// Token: 0x170019D0 RID: 6608
		// (get) Token: 0x06005E0D RID: 24077 RVA: 0x0018D928 File Offset: 0x0018BB28
		internal sealed override ObjectSchema ObjectSchema
		{
			get
			{
				return MailboxCalendarFolder.schema;
			}
		}

		// Token: 0x170019D1 RID: 6609
		// (get) Token: 0x06005E0E RID: 24078 RVA: 0x0018D92F File Offset: 0x0018BB2F
		public sealed override ObjectId Identity
		{
			get
			{
				return this.MailboxFolderId;
			}
		}

		// Token: 0x170019D2 RID: 6610
		// (get) Token: 0x06005E0F RID: 24079 RVA: 0x0018D937 File Offset: 0x0018BB37
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x170019D3 RID: 6611
		// (get) Token: 0x06005E10 RID: 24080 RVA: 0x0018D93E File Offset: 0x0018BB3E
		// (set) Token: 0x06005E11 RID: 24081 RVA: 0x0018D950 File Offset: 0x0018BB50
		internal MailboxFolderId MailboxFolderId
		{
			get
			{
				return (MailboxFolderId)this[MailboxCalendarFolderSchema.MailboxFolderId];
			}
			set
			{
				this[MailboxCalendarFolderSchema.MailboxFolderId] = value;
			}
		}

		// Token: 0x170019D4 RID: 6612
		// (get) Token: 0x06005E12 RID: 24082 RVA: 0x0018D95E File Offset: 0x0018BB5E
		// (set) Token: 0x06005E13 RID: 24083 RVA: 0x0018D970 File Offset: 0x0018BB70
		[Parameter(Mandatory = false)]
		public bool PublishEnabled
		{
			get
			{
				return (bool)this[MailboxCalendarFolderSchema.PublishEnabled];
			}
			set
			{
				this[MailboxCalendarFolderSchema.PublishEnabled] = value;
			}
		}

		// Token: 0x170019D5 RID: 6613
		// (get) Token: 0x06005E14 RID: 24084 RVA: 0x0018D983 File Offset: 0x0018BB83
		// (set) Token: 0x06005E15 RID: 24085 RVA: 0x0018D990 File Offset: 0x0018BB90
		[Parameter(Mandatory = false)]
		public DateRangeEnumType PublishDateRangeFrom
		{
			get
			{
				return this.EnumGetter<DateRangeEnumType>(MailboxCalendarFolderSchema.PublishDateRangeFrom);
			}
			set
			{
				this.EnumSetter<DateRangeEnumType>(value, MailboxCalendarFolderSchema.PublishDateRangeFrom);
			}
		}

		// Token: 0x170019D6 RID: 6614
		// (get) Token: 0x06005E16 RID: 24086 RVA: 0x0018D99E File Offset: 0x0018BB9E
		// (set) Token: 0x06005E17 RID: 24087 RVA: 0x0018D9AB File Offset: 0x0018BBAB
		[Parameter(Mandatory = false)]
		public DateRangeEnumType PublishDateRangeTo
		{
			get
			{
				return this.EnumGetter<DateRangeEnumType>(MailboxCalendarFolderSchema.PublishDateRangeTo);
			}
			set
			{
				this.EnumSetter<DateRangeEnumType>(value, MailboxCalendarFolderSchema.PublishDateRangeTo);
			}
		}

		// Token: 0x170019D7 RID: 6615
		// (get) Token: 0x06005E18 RID: 24088 RVA: 0x0018D9B9 File Offset: 0x0018BBB9
		// (set) Token: 0x06005E19 RID: 24089 RVA: 0x0018D9C6 File Offset: 0x0018BBC6
		[Parameter(Mandatory = false)]
		public DetailLevelEnumType DetailLevel
		{
			get
			{
				return this.EnumGetter<DetailLevelEnumType>(MailboxCalendarFolderSchema.DetailLevel);
			}
			set
			{
				this.EnumSetter<DetailLevelEnumType>(value, MailboxCalendarFolderSchema.DetailLevel);
			}
		}

		// Token: 0x170019D8 RID: 6616
		// (get) Token: 0x06005E1A RID: 24090 RVA: 0x0018D9D4 File Offset: 0x0018BBD4
		// (set) Token: 0x06005E1B RID: 24091 RVA: 0x0018D9E6 File Offset: 0x0018BBE6
		[Parameter(Mandatory = false)]
		public bool SearchableUrlEnabled
		{
			get
			{
				return (bool)this[MailboxCalendarFolderSchema.SearchableUrlEnabled];
			}
			set
			{
				this[MailboxCalendarFolderSchema.SearchableUrlEnabled] = value;
			}
		}

		// Token: 0x170019D9 RID: 6617
		// (get) Token: 0x06005E1C RID: 24092 RVA: 0x0018D9F9 File Offset: 0x0018BBF9
		// (set) Token: 0x06005E1D RID: 24093 RVA: 0x0018DA0B File Offset: 0x0018BC0B
		public string PublishedCalendarUrl
		{
			get
			{
				return (string)this[MailboxCalendarFolderSchema.PublishedCalendarUrlCalculated];
			}
			internal set
			{
				this[MailboxCalendarFolderSchema.PublishedCalendarUrlCalculated] = value;
			}
		}

		// Token: 0x170019DA RID: 6618
		// (get) Token: 0x06005E1E RID: 24094 RVA: 0x0018DA19 File Offset: 0x0018BC19
		// (set) Token: 0x06005E1F RID: 24095 RVA: 0x0018DA2B File Offset: 0x0018BC2B
		public string PublishedICalUrl
		{
			get
			{
				return (string)this[MailboxCalendarFolderSchema.PublishedICalUrlCalculated];
			}
			internal set
			{
				this[MailboxCalendarFolderSchema.PublishedICalUrlCalculated] = value;
			}
		}

		// Token: 0x170019DB RID: 6619
		// (get) Token: 0x06005E20 RID: 24096 RVA: 0x0018DA39 File Offset: 0x0018BC39
		internal string PublishedCalendarUrlRaw
		{
			get
			{
				return (string)this[MailboxCalendarFolderSchema.PublishedCalendarUrl];
			}
		}

		// Token: 0x170019DC RID: 6620
		// (get) Token: 0x06005E21 RID: 24097 RVA: 0x0018DA4B File Offset: 0x0018BC4B
		internal string PublishedICalUrlRaw
		{
			get
			{
				return (string)this[MailboxCalendarFolderSchema.PublishedICalUrl];
			}
		}

		// Token: 0x04003483 RID: 13443
		private static readonly MailboxCalendarFolderSchema schema = ObjectSchema.GetInstance<MailboxCalendarFolderSchema>();

		// Token: 0x04003484 RID: 13444
		internal static readonly SimpleProviderPropertyDefinition[] CalendarFolderConfigurationProperties = new SimpleProviderPropertyDefinition[]
		{
			MailboxCalendarFolderSchema.PublishEnabled,
			MailboxCalendarFolderSchema.PublishDateRangeFrom,
			MailboxCalendarFolderSchema.PublishDateRangeTo,
			MailboxCalendarFolderSchema.DetailLevel,
			MailboxCalendarFolderSchema.SearchableUrlEnabled,
			MailboxCalendarFolderSchema.PublishedCalendarUrl,
			MailboxCalendarFolderSchema.PublishedICalUrl
		};
	}
}
