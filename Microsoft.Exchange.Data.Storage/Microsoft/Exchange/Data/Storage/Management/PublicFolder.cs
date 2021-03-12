using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A68 RID: 2664
	[Serializable]
	public class PublicFolder : MailboxFolder
	{
		// Token: 0x17001AC4 RID: 6852
		// (get) Token: 0x06006139 RID: 24889 RVA: 0x0019AAE7 File Offset: 0x00198CE7
		internal override XsoMailboxConfigurationObjectSchema Schema
		{
			get
			{
				return PublicFolder.schema;
			}
		}

		// Token: 0x17001AC5 RID: 6853
		// (get) Token: 0x0600613A RID: 24890 RVA: 0x0019AAEE File Offset: 0x00198CEE
		public override ObjectId Identity
		{
			get
			{
				return (PublicFolderId)this[PublicFolderSchema.Identity];
			}
		}

		// Token: 0x17001AC6 RID: 6854
		// (get) Token: 0x0600613B RID: 24891 RVA: 0x0019AB00 File Offset: 0x00198D00
		private new string FolderStoreObjectId
		{
			get
			{
				return (string)this[MailboxFolderSchema.FolderStoreObjectId];
			}
		}

		// Token: 0x17001AC7 RID: 6855
		// (get) Token: 0x0600613C RID: 24892 RVA: 0x0019AB12 File Offset: 0x00198D12
		// (set) Token: 0x0600613D RID: 24893 RVA: 0x0019AB24 File Offset: 0x00198D24
		[Parameter]
		[ValidateNotNullOrEmpty]
		public new string Name
		{
			get
			{
				return (string)this[PublicFolderSchema.Name];
			}
			set
			{
				this[PublicFolderSchema.Name] = value;
			}
		}

		// Token: 0x17001AC8 RID: 6856
		// (get) Token: 0x0600613E RID: 24894 RVA: 0x0019AB32 File Offset: 0x00198D32
		// (set) Token: 0x0600613F RID: 24895 RVA: 0x0019AB44 File Offset: 0x00198D44
		public bool MailEnabled
		{
			get
			{
				return (bool)this[PublicFolderSchema.MailEnabled];
			}
			set
			{
				this[PublicFolderSchema.MailEnabled] = value;
			}
		}

		// Token: 0x17001AC9 RID: 6857
		// (get) Token: 0x06006140 RID: 24896 RVA: 0x0019AB57 File Offset: 0x00198D57
		// (set) Token: 0x06006141 RID: 24897 RVA: 0x0019AB69 File Offset: 0x00198D69
		internal byte[] ProxyGuid
		{
			get
			{
				return (byte[])this[PublicFolderSchema.ProxyGuid];
			}
			set
			{
				this[PublicFolderSchema.ProxyGuid] = value;
			}
		}

		// Token: 0x17001ACA RID: 6858
		// (get) Token: 0x06006142 RID: 24898 RVA: 0x0019AB77 File Offset: 0x00198D77
		// (set) Token: 0x06006143 RID: 24899 RVA: 0x0019AB89 File Offset: 0x00198D89
		public Guid? MailRecipientGuid
		{
			get
			{
				return (Guid?)this[PublicFolderSchema.MailRecipientGuid];
			}
			set
			{
				this[PublicFolderSchema.MailRecipientGuid] = value;
			}
		}

		// Token: 0x17001ACB RID: 6859
		// (get) Token: 0x06006144 RID: 24900 RVA: 0x0019AB9C File Offset: 0x00198D9C
		public string ParentPath
		{
			get
			{
				MapiFolderPath folderPath = base.FolderPath;
				if (folderPath == null || folderPath.IsSubtreeRoot)
				{
					return null;
				}
				return folderPath.Parent.ToString();
			}
		}

		// Token: 0x17001ACC RID: 6860
		// (get) Token: 0x06006145 RID: 24901 RVA: 0x0019ABCE File Offset: 0x00198DCE
		// (set) Token: 0x06006146 RID: 24902 RVA: 0x0019ABD6 File Offset: 0x00198DD6
		public string ContentMailboxName { get; internal set; }

		// Token: 0x17001ACD RID: 6861
		// (get) Token: 0x06006147 RID: 24903 RVA: 0x0019ABDF File Offset: 0x00198DDF
		// (set) Token: 0x06006148 RID: 24904 RVA: 0x0019ABF6 File Offset: 0x00198DF6
		public Guid ContentMailboxGuid
		{
			get
			{
				return ((PublicFolderContentMailboxInfo)this[PublicFolderSchema.ContentMailboxInfo]).MailboxGuid;
			}
			set
			{
				this[PublicFolderSchema.ContentMailboxInfo] = new PublicFolderContentMailboxInfo(value.ToString());
			}
		}

		// Token: 0x17001ACE RID: 6862
		// (get) Token: 0x06006149 RID: 24905 RVA: 0x0019AC15 File Offset: 0x00198E15
		// (set) Token: 0x0600614A RID: 24906 RVA: 0x0019AC27 File Offset: 0x00198E27
		[Parameter]
		public CultureInfo EformsLocaleId
		{
			get
			{
				return (CultureInfo)this[PublicFolderSchema.EformsLocaleId];
			}
			set
			{
				this[PublicFolderSchema.EformsLocaleId] = value;
			}
		}

		// Token: 0x17001ACF RID: 6863
		// (get) Token: 0x0600614B RID: 24907 RVA: 0x0019AC35 File Offset: 0x00198E35
		// (set) Token: 0x0600614C RID: 24908 RVA: 0x0019AC47 File Offset: 0x00198E47
		[Parameter]
		public bool PerUserReadStateEnabled
		{
			get
			{
				return (bool)this[PublicFolderSchema.PerUserReadStateEnabled];
			}
			set
			{
				this[PublicFolderSchema.PerUserReadStateEnabled] = value;
			}
		}

		// Token: 0x17001AD0 RID: 6864
		// (get) Token: 0x0600614D RID: 24909 RVA: 0x0019AC5A File Offset: 0x00198E5A
		public string EntryId
		{
			get
			{
				return (string)this[PublicFolderSchema.EntryId];
			}
		}

		// Token: 0x17001AD1 RID: 6865
		// (get) Token: 0x0600614E RID: 24910 RVA: 0x0019AC6C File Offset: 0x00198E6C
		// (set) Token: 0x0600614F RID: 24911 RVA: 0x0019AC74 File Offset: 0x00198E74
		public string DumpsterEntryId { get; internal set; }

		// Token: 0x17001AD2 RID: 6866
		// (get) Token: 0x06006150 RID: 24912 RVA: 0x0019AC7D File Offset: 0x00198E7D
		public new string ParentFolder
		{
			get
			{
				if (this[PublicFolderSchema.ParentFolder] == null)
				{
					return string.Empty;
				}
				return ((PublicFolderId)this[PublicFolderSchema.ParentFolder]).StoreObjectId.ToHexEntryId();
			}
		}

		// Token: 0x17001AD3 RID: 6867
		// (get) Token: 0x06006151 RID: 24913 RVA: 0x0019ACAC File Offset: 0x00198EAC
		// (set) Token: 0x06006152 RID: 24914 RVA: 0x0019ACB4 File Offset: 0x00198EB4
		public OrganizationId OrganizationId { get; set; }

		// Token: 0x17001AD4 RID: 6868
		// (get) Token: 0x06006153 RID: 24915 RVA: 0x0019ACBD File Offset: 0x00198EBD
		// (set) Token: 0x06006154 RID: 24916 RVA: 0x0019ACCF File Offset: 0x00198ECF
		[Parameter]
		public EnhancedTimeSpan? AgeLimit
		{
			get
			{
				return (EnhancedTimeSpan?)this[PublicFolderSchema.AgeLimit];
			}
			set
			{
				this[PublicFolderSchema.AgeLimit] = value;
			}
		}

		// Token: 0x17001AD5 RID: 6869
		// (get) Token: 0x06006155 RID: 24917 RVA: 0x0019ACE2 File Offset: 0x00198EE2
		// (set) Token: 0x06006156 RID: 24918 RVA: 0x0019ACF4 File Offset: 0x00198EF4
		[Parameter]
		public EnhancedTimeSpan? RetainDeletedItemsFor
		{
			get
			{
				return (EnhancedTimeSpan?)this[PublicFolderSchema.RetainDeletedItemsFor];
			}
			set
			{
				this[PublicFolderSchema.RetainDeletedItemsFor] = value;
			}
		}

		// Token: 0x17001AD6 RID: 6870
		// (get) Token: 0x06006157 RID: 24919 RVA: 0x0019AD07 File Offset: 0x00198F07
		// (set) Token: 0x06006158 RID: 24920 RVA: 0x0019AD19 File Offset: 0x00198F19
		[Parameter]
		public Unlimited<ByteQuantifiedSize>? ProhibitPostQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>?)this[PublicFolderSchema.ProhibitPostQuota];
			}
			set
			{
				this[PublicFolderSchema.ProhibitPostQuota] = value;
			}
		}

		// Token: 0x17001AD7 RID: 6871
		// (get) Token: 0x06006159 RID: 24921 RVA: 0x0019AD2C File Offset: 0x00198F2C
		// (set) Token: 0x0600615A RID: 24922 RVA: 0x0019AD3E File Offset: 0x00198F3E
		[Parameter]
		public Unlimited<ByteQuantifiedSize>? IssueWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>?)this[PublicFolderSchema.IssueWarningQuota];
			}
			set
			{
				this[PublicFolderSchema.IssueWarningQuota] = value;
			}
		}

		// Token: 0x17001AD8 RID: 6872
		// (get) Token: 0x0600615B RID: 24923 RVA: 0x0019AD51 File Offset: 0x00198F51
		// (set) Token: 0x0600615C RID: 24924 RVA: 0x0019AD63 File Offset: 0x00198F63
		[Parameter]
		public Unlimited<ByteQuantifiedSize>? MaxItemSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>?)this[PublicFolderSchema.MaxItemSize];
			}
			set
			{
				this[PublicFolderSchema.MaxItemSize] = value;
			}
		}

		// Token: 0x17001AD9 RID: 6873
		// (get) Token: 0x0600615D RID: 24925 RVA: 0x0019AD76 File Offset: 0x00198F76
		// (set) Token: 0x0600615E RID: 24926 RVA: 0x0019AD88 File Offset: 0x00198F88
		[Parameter]
		public ExDateTime? LastMovedTime
		{
			get
			{
				return (ExDateTime?)this[PublicFolderSchema.LastMovedTime];
			}
			internal set
			{
				this[PublicFolderSchema.LastMovedTime] = value;
			}
		}

		// Token: 0x17001ADA RID: 6874
		// (get) Token: 0x0600615F RID: 24927 RVA: 0x0019AD9B File Offset: 0x00198F9B
		// (set) Token: 0x06006160 RID: 24928 RVA: 0x0019ADAD File Offset: 0x00198FAD
		internal int QuotaStyle
		{
			get
			{
				return (int)this[PublicFolderSchema.PfQuotaStyle];
			}
			set
			{
				this[PublicFolderSchema.PfQuotaStyle] = value;
			}
		}

		// Token: 0x06006161 RID: 24929 RVA: 0x0019ADC0 File Offset: 0x00198FC0
		internal new static object IdentityGetter(IPropertyBag propertyBag)
		{
			VersionedId versionedId = (VersionedId)propertyBag[MailboxFolderSchema.InternalFolderIdentity];
			MapiFolderPath mapiFolderPath = (MapiFolderPath)propertyBag[MailboxFolderSchema.FolderPath];
			if (null != mapiFolderPath || versionedId != null)
			{
				return new PublicFolderId((versionedId == null) ? null : versionedId.ObjectId, mapiFolderPath);
			}
			return null;
		}

		// Token: 0x06006162 RID: 24930 RVA: 0x0019AE10 File Offset: 0x00199010
		internal static void MailRecipientGuidSetter(object value, IPropertyBag propertyBag)
		{
			if (value is Guid?)
			{
				byte[] value2 = ((Guid?)value).Value.ToByteArray();
				propertyBag[PublicFolderSchema.ProxyGuid] = value2;
				return;
			}
			propertyBag[PublicFolderSchema.ProxyGuid] = null;
		}

		// Token: 0x06006163 RID: 24931 RVA: 0x0019AE58 File Offset: 0x00199058
		internal static object MailRecipientGuidGetter(IPropertyBag propertyBag)
		{
			byte[] array = (byte[])propertyBag[PublicFolderSchema.ProxyGuid];
			if (array != null && array.Length == 16)
			{
				return new Guid(array);
			}
			return null;
		}

		// Token: 0x06006164 RID: 24932 RVA: 0x0019AE90 File Offset: 0x00199090
		internal new static object ParentFolderGetter(IPropertyBag propertyBag)
		{
			VersionedId versionedId = (VersionedId)propertyBag[MailboxFolderSchema.InternalFolderIdentity];
			StoreObjectId storeObjectId = (StoreObjectId)propertyBag[MailboxFolderSchema.InternalParentFolderIdentity];
			MapiFolderPath mapiFolderPath = (MapiFolderPath)propertyBag[MailboxFolderSchema.FolderPath];
			if (versionedId != null && versionedId.ObjectId != null && object.Equals(versionedId.ObjectId, storeObjectId))
			{
				return null;
			}
			if ((null != mapiFolderPath && null != mapiFolderPath.Parent) || storeObjectId != null)
			{
				return new PublicFolderId(storeObjectId, (null == mapiFolderPath) ? null : mapiFolderPath.Parent);
			}
			return null;
		}

		// Token: 0x06006165 RID: 24933 RVA: 0x0019AF20 File Offset: 0x00199120
		internal static object EformsLocaleIdGetter(IPropertyBag propertyBag)
		{
			int? num = (int?)propertyBag[PublicFolderSchema.EformsLocaleIdValue];
			if (num == null)
			{
				return null;
			}
			return new CultureInfo(num.Value);
		}

		// Token: 0x06006166 RID: 24934 RVA: 0x0019AF55 File Offset: 0x00199155
		internal static void EformsLocaleIdSetter(object value, IPropertyBag propertyBag)
		{
			if (value != null)
			{
				propertyBag[PublicFolderSchema.EformsLocaleIdValue] = ((CultureInfo)value).LCID;
				return;
			}
			propertyBag[PublicFolderSchema.EformsLocaleIdValue] = null;
		}

		// Token: 0x06006167 RID: 24935 RVA: 0x0019AF82 File Offset: 0x00199182
		internal static void PerUserReadStateEnabledSetter(object value, IPropertyBag propertyBag)
		{
			if (value != null)
			{
				propertyBag[PublicFolderSchema.DisablePerUserReadValue] = !(bool)value;
				return;
			}
			propertyBag[PublicFolderSchema.DisablePerUserReadValue] = null;
		}

		// Token: 0x06006168 RID: 24936 RVA: 0x0019AFB0 File Offset: 0x001991B0
		internal static object PerUserReadStateEnabledGetter(IPropertyBag propertyBag)
		{
			bool? flag = (bool?)propertyBag[PublicFolderSchema.DisablePerUserReadValue];
			return flag == null || !flag.Value;
		}

		// Token: 0x06006169 RID: 24937 RVA: 0x0019AFEC File Offset: 0x001991EC
		internal static object MailEnabledGetter(IPropertyBag propertyBag)
		{
			bool? flag = (bool?)propertyBag[PublicFolderSchema.MailEnabledValue];
			return flag != null && flag.Value;
		}

		// Token: 0x0600616A RID: 24938 RVA: 0x0019B022 File Offset: 0x00199222
		internal static void MailEnabledSetter(object value, IPropertyBag propertyBag)
		{
			if (value != null)
			{
				propertyBag[PublicFolderSchema.MailEnabledValue] = (bool)value;
				return;
			}
			propertyBag[PublicFolderSchema.MailEnabledValue] = null;
		}

		// Token: 0x0600616B RID: 24939 RVA: 0x0019B04C File Offset: 0x0019924C
		internal static string EntryIdGetter(IPropertyBag propertyBag)
		{
			VersionedId versionedId = propertyBag[MailboxFolderSchema.InternalFolderIdentity] as VersionedId;
			if (versionedId == null || versionedId.ObjectId == null)
			{
				return string.Empty;
			}
			return versionedId.ObjectId.ToHexEntryId();
		}

		// Token: 0x0600616C RID: 24940 RVA: 0x0019B088 File Offset: 0x00199288
		internal static PublicFolderContentMailboxInfo ContentMailboxInfoGetter(IPropertyBag propertyBag)
		{
			string contentMailboxInfo = null;
			byte[] array = propertyBag[PublicFolderSchema.ReplicaListBinary] as byte[];
			if (array != null)
			{
				string[] stringArrayFromBytes = ReplicaListProperty.GetStringArrayFromBytes(array);
				if (stringArrayFromBytes.Length > 0)
				{
					contentMailboxInfo = stringArrayFromBytes[0];
				}
			}
			return new PublicFolderContentMailboxInfo(contentMailboxInfo);
		}

		// Token: 0x0600616D RID: 24941 RVA: 0x0019B0C4 File Offset: 0x001992C4
		internal static void ContentMailboxInfoSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[PublicFolderSchema.ReplicaListBinary] = ((value != null) ? ReplicaListProperty.GetBytesFromStringArray(new string[]
			{
				((PublicFolderContentMailboxInfo)value).ToString()
			}) : null);
		}

		// Token: 0x0600616E RID: 24942 RVA: 0x0019B100 File Offset: 0x00199300
		internal static object AgeLimitGetter(IPropertyBag propertyBag)
		{
			int? num = propertyBag[PublicFolderSchema.OverallAgeLimit] as int?;
			if (num != null)
			{
				return EnhancedTimeSpan.FromSeconds((double)num.Value);
			}
			return null;
		}

		// Token: 0x0600616F RID: 24943 RVA: 0x0019B140 File Offset: 0x00199340
		internal static void AgeLimitSetter(object value, IPropertyBag propertyBag)
		{
			EnhancedTimeSpan? enhancedTimeSpan = value as EnhancedTimeSpan?;
			if (enhancedTimeSpan != null)
			{
				propertyBag[PublicFolderSchema.OverallAgeLimit] = (int)enhancedTimeSpan.Value.TotalSeconds;
				return;
			}
			propertyBag[PublicFolderSchema.OverallAgeLimit] = null;
		}

		// Token: 0x06006170 RID: 24944 RVA: 0x0019B190 File Offset: 0x00199390
		internal static object RetainDeletedItemsForGetter(IPropertyBag propertyBag)
		{
			int? num = propertyBag[PublicFolderSchema.RetentionAgeLimit] as int?;
			if (num != null)
			{
				return EnhancedTimeSpan.FromSeconds((double)num.Value);
			}
			return null;
		}

		// Token: 0x06006171 RID: 24945 RVA: 0x0019B1D0 File Offset: 0x001993D0
		internal static void RetainDeletedItemsForSetter(object value, IPropertyBag propertyBag)
		{
			EnhancedTimeSpan? enhancedTimeSpan = value as EnhancedTimeSpan?;
			if (enhancedTimeSpan != null)
			{
				propertyBag[PublicFolderSchema.RetentionAgeLimit] = (int)enhancedTimeSpan.Value.TotalSeconds;
				return;
			}
			propertyBag[PublicFolderSchema.RetentionAgeLimit] = null;
		}

		// Token: 0x06006172 RID: 24946 RVA: 0x0019B220 File Offset: 0x00199420
		internal static object ProhibitPostQuotaGetter(IPropertyBag propertyBag)
		{
			int? num = propertyBag[PublicFolderSchema.PfOverHardQuotaLimit] as int?;
			if (num != null)
			{
				int value = num.Value;
				return (value < 0) ? Unlimited<ByteQuantifiedSize>.UnlimitedValue : new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(checked((ulong)value)));
			}
			return Unlimited<ByteQuantifiedSize>.UnlimitedValue;
		}

		// Token: 0x06006173 RID: 24947 RVA: 0x0019B27C File Offset: 0x0019947C
		internal static void ProhibitPostQuotaSetter(object value, IPropertyBag propertyBag)
		{
			Unlimited<ByteQuantifiedSize>? unlimited = value as Unlimited<ByteQuantifiedSize>?;
			if (unlimited != null)
			{
				Unlimited<ByteQuantifiedSize> value2 = unlimited.Value;
				propertyBag[PublicFolderSchema.PfOverHardQuotaLimit] = (value2.IsUnlimited ? -1 : (propertyBag[PublicFolderSchema.PfOverHardQuotaLimit] = (int)value2.Value.ToKB()));
				return;
			}
			propertyBag[PublicFolderSchema.PfOverHardQuotaLimit] = null;
		}

		// Token: 0x06006174 RID: 24948 RVA: 0x0019B2F4 File Offset: 0x001994F4
		internal static object LastMovedTimeGetter(IPropertyBag propertyBag)
		{
			ExDateTime? exDateTime = propertyBag[PublicFolderSchema.LastMovedTimeStamp] as ExDateTime?;
			if (exDateTime != null)
			{
				return exDateTime;
			}
			return null;
		}

		// Token: 0x06006175 RID: 24949 RVA: 0x0019B328 File Offset: 0x00199528
		internal static object IssueWarningQuotaGetter(IPropertyBag propertyBag)
		{
			int? num = propertyBag[PublicFolderSchema.PfStorageQuota] as int?;
			if (num != null)
			{
				int value = num.Value;
				return (value < 0) ? Unlimited<ByteQuantifiedSize>.UnlimitedValue : new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(checked((ulong)value)));
			}
			return Unlimited<ByteQuantifiedSize>.UnlimitedValue;
		}

		// Token: 0x06006176 RID: 24950 RVA: 0x0019B384 File Offset: 0x00199584
		internal static void IssueWarningQuotaSetter(object value, IPropertyBag propertyBag)
		{
			Unlimited<ByteQuantifiedSize>? unlimited = value as Unlimited<ByteQuantifiedSize>?;
			if (unlimited != null)
			{
				Unlimited<ByteQuantifiedSize> value2 = unlimited.Value;
				propertyBag[PublicFolderSchema.PfStorageQuota] = (value2.IsUnlimited ? -1 : ((int)value2.Value.ToKB()));
				return;
			}
			propertyBag[PublicFolderSchema.PfStorageQuota] = null;
		}

		// Token: 0x06006177 RID: 24951 RVA: 0x0019B3E8 File Offset: 0x001995E8
		internal static object MaxItemSizeGetter(IPropertyBag propertyBag)
		{
			int? num = propertyBag[PublicFolderSchema.PfMsgSizeLimit] as int?;
			if (num != null)
			{
				int value = num.Value;
				return (value < 0) ? Unlimited<ByteQuantifiedSize>.UnlimitedValue : new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(checked((ulong)value)));
			}
			return Unlimited<ByteQuantifiedSize>.UnlimitedValue;
		}

		// Token: 0x06006178 RID: 24952 RVA: 0x0019B444 File Offset: 0x00199644
		internal static void MaxItemSizeSetter(object value, IPropertyBag propertyBag)
		{
			Unlimited<ByteQuantifiedSize>? unlimited = value as Unlimited<ByteQuantifiedSize>?;
			if (unlimited != null)
			{
				Unlimited<ByteQuantifiedSize> value2 = unlimited.Value;
				propertyBag[PublicFolderSchema.PfMsgSizeLimit] = (value2.IsUnlimited ? -1 : ((int)value2.Value.ToKB()));
				return;
			}
			propertyBag[PublicFolderSchema.PfMsgSizeLimit] = null;
		}

		// Token: 0x04003745 RID: 14149
		private static PublicFolderSchema schema = ObjectSchema.GetInstance<PublicFolderSchema>();
	}
}
