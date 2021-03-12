using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000419 RID: 1049
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class ElcContentSettings : ADConfigurationObject
	{
		// Token: 0x06002F38 RID: 12088 RVA: 0x000BF2FB File Offset: 0x000BD4FB
		internal ElcContentSettings(IDirectorySession session, ADObjectId elcFolderId, string name)
		{
			this.m_Session = session;
			base.SetId(elcFolderId.GetChildId(name));
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x000BF322 File Offset: 0x000BD522
		public ElcContentSettings()
		{
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x000BF335 File Offset: 0x000BD535
		internal override ADObjectSchema Schema
		{
			get
			{
				return ElcContentSettings.schema;
			}
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x000BF33C File Offset: 0x000BD53C
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ElcContentSettings.mostDerivedClass;
			}
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06002F3C RID: 12092 RVA: 0x000BF343 File Offset: 0x000BD543
		public string MessageClassDisplayName
		{
			get
			{
				return (string)this[ElcContentSettingsSchema.MessageClassDisplayName];
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06002F3D RID: 12093 RVA: 0x000BF355 File Offset: 0x000BD555
		// (set) Token: 0x06002F3E RID: 12094 RVA: 0x000BF367 File Offset: 0x000BD567
		public string MessageClass
		{
			get
			{
				return (string)this[ElcContentSettingsSchema.MessageClass];
			}
			set
			{
				this[ElcContentSettingsSchema.MessageClass] = value;
			}
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06002F3F RID: 12095 RVA: 0x000BF375 File Offset: 0x000BD575
		public string Description
		{
			get
			{
				return DirectoryStrings.ElcContentSettingsDescription;
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06002F40 RID: 12096 RVA: 0x000BF381 File Offset: 0x000BD581
		// (set) Token: 0x06002F41 RID: 12097 RVA: 0x000BF393 File Offset: 0x000BD593
		[Parameter(Mandatory = false)]
		public bool RetentionEnabled
		{
			get
			{
				return (bool)this[ElcContentSettingsSchema.RetentionEnabled];
			}
			set
			{
				this[ElcContentSettingsSchema.RetentionEnabled] = value;
			}
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06002F42 RID: 12098 RVA: 0x000BF3A6 File Offset: 0x000BD5A6
		// (set) Token: 0x06002F43 RID: 12099 RVA: 0x000BF3B8 File Offset: 0x000BD5B8
		[Parameter(Mandatory = false)]
		public RetentionActionType RetentionAction
		{
			get
			{
				return (RetentionActionType)this[ElcContentSettingsSchema.RetentionAction];
			}
			set
			{
				this[ElcContentSettingsSchema.RetentionAction] = value;
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06002F44 RID: 12100 RVA: 0x000BF3CB File Offset: 0x000BD5CB
		// (set) Token: 0x06002F45 RID: 12101 RVA: 0x000BF3DD File Offset: 0x000BD5DD
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan? AgeLimitForRetention
		{
			get
			{
				return (EnhancedTimeSpan?)this[ElcContentSettingsSchema.AgeLimitForRetention];
			}
			set
			{
				this[ElcContentSettingsSchema.AgeLimitForRetention] = value;
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06002F46 RID: 12102 RVA: 0x000BF3F0 File Offset: 0x000BD5F0
		// (set) Token: 0x06002F47 RID: 12103 RVA: 0x000BF402 File Offset: 0x000BD602
		public ADObjectId MoveToDestinationFolder
		{
			get
			{
				return (ADObjectId)this[ElcContentSettingsSchema.MoveToDestinationFolder];
			}
			set
			{
				this[ElcContentSettingsSchema.MoveToDestinationFolder] = value;
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06002F48 RID: 12104 RVA: 0x000BF410 File Offset: 0x000BD610
		// (set) Token: 0x06002F49 RID: 12105 RVA: 0x000BF422 File Offset: 0x000BD622
		[Parameter(Mandatory = false)]
		public RetentionDateType TriggerForRetention
		{
			get
			{
				return (RetentionDateType)this[ElcContentSettingsSchema.TriggerForRetention];
			}
			set
			{
				this[ElcContentSettingsSchema.TriggerForRetention] = value;
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06002F4A RID: 12106 RVA: 0x000BF435 File Offset: 0x000BD635
		// (set) Token: 0x06002F4B RID: 12107 RVA: 0x000BF447 File Offset: 0x000BD647
		[Parameter(Mandatory = false)]
		public JournalingFormat MessageFormatForJournaling
		{
			get
			{
				return (JournalingFormat)this[ElcContentSettingsSchema.MessageFormatForJournaling];
			}
			set
			{
				this[ElcContentSettingsSchema.MessageFormatForJournaling] = value;
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06002F4C RID: 12108 RVA: 0x000BF45A File Offset: 0x000BD65A
		// (set) Token: 0x06002F4D RID: 12109 RVA: 0x000BF46C File Offset: 0x000BD66C
		[Parameter(Mandatory = false)]
		public bool JournalingEnabled
		{
			get
			{
				return (bool)this[ElcContentSettingsSchema.JournalingEnabled];
			}
			set
			{
				this[ElcContentSettingsSchema.JournalingEnabled] = value;
			}
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x06002F4E RID: 12110 RVA: 0x000BF47F File Offset: 0x000BD67F
		// (set) Token: 0x06002F4F RID: 12111 RVA: 0x000BF491 File Offset: 0x000BD691
		public ADObjectId AddressForJournaling
		{
			get
			{
				return (ADObjectId)this[ElcContentSettingsSchema.AddressForJournaling];
			}
			set
			{
				this[ElcContentSettingsSchema.AddressForJournaling] = value;
			}
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06002F50 RID: 12112 RVA: 0x000BF49F File Offset: 0x000BD69F
		// (set) Token: 0x06002F51 RID: 12113 RVA: 0x000BF4B1 File Offset: 0x000BD6B1
		[Parameter(Mandatory = false)]
		public string LabelForJournaling
		{
			get
			{
				return (string)this[ElcContentSettingsSchema.LabelForJournaling];
			}
			set
			{
				this[ElcContentSettingsSchema.LabelForJournaling] = value;
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06002F52 RID: 12114 RVA: 0x000BF4BF File Offset: 0x000BD6BF
		public ADObjectId ManagedFolder
		{
			get
			{
				return (ADObjectId)this[ElcContentSettingsSchema.ManagedFolder];
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06002F53 RID: 12115 RVA: 0x000BF4D1 File Offset: 0x000BD6D1
		public string ManagedFolderName
		{
			get
			{
				return (string)this[ElcContentSettingsSchema.ManagedFolderName];
			}
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x000BF4E4 File Offset: 0x000BD6E4
		internal static object ELCMessageClassGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[ElcContentSettingsSchema.MessageClassString];
			if (ElcMessageClass.IsMultiMessageClassDeputy(text))
			{
				StringBuilder stringBuilder = new StringBuilder(128);
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ElcContentSettingsSchema.MessageClassArray];
				multiValuedProperty.Sort();
				foreach (string value in multiValuedProperty)
				{
					stringBuilder.Append(value);
					stringBuilder.Append(ElcMessageClass.MessageClassDelims[0]);
				}
				stringBuilder.Length--;
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000BF594 File Offset: 0x000BD794
		internal static void ELCMessageClassSetter(object value, IPropertyBag propertyBag)
		{
			string text = (string)value;
			if (ElcMessageClass.IsMultiMessageClass(text))
			{
				string[] array = ((string)value).Split(ElcMessageClass.MessageClassDelims, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length == 0)
				{
					text = string.Empty;
				}
				else if (array.Length == 1)
				{
					text = array[0];
				}
				else
				{
					MultiValuedProperty<string> value2 = new MultiValuedProperty<string>(array);
					propertyBag[ElcContentSettingsSchema.MessageClassArray] = value2;
					text = ElcMessageClass.MultiMessageClassDeputy;
				}
			}
			propertyBag[ElcContentSettingsSchema.MessageClassString] = text;
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000BF600 File Offset: 0x000BD800
		internal static object ELCFolderGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			if (adobjectId != null)
			{
				return adobjectId.Parent;
			}
			return null;
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x000BF62C File Offset: 0x000BD82C
		internal static object ELCFolderNameGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)ElcContentSettings.ELCFolderGetter(propertyBag);
			if (adobjectId != null)
			{
				return adobjectId.Name;
			}
			return string.Empty;
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x000BF654 File Offset: 0x000BD854
		internal static object MessageClassDisplayNameGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[ElcContentSettingsSchema.MessageClass];
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return ElcMessageClass.GetDisplayName(text);
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x000BF684 File Offset: 0x000BD884
		internal static bool GetValueFromFlags(IPropertyBag propertyBag, ElcContentSettingFlags flag)
		{
			object obj = propertyBag[ElcContentSettingsSchema.ElcFlags];
			return flag == ((ElcContentSettingFlags)obj & flag);
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x000BF6AC File Offset: 0x000BD8AC
		internal static void SetFlags(IPropertyBag propertyBag, ElcContentSettingFlags flag, bool value)
		{
			ElcContentSettingFlags elcContentSettingFlags = (ElcContentSettingFlags)propertyBag[ElcContentSettingsSchema.ElcFlags];
			ElcContentSettingFlags elcContentSettingFlags2 = value ? (elcContentSettingFlags | flag) : (elcContentSettingFlags & ~flag);
			propertyBag[ElcContentSettingsSchema.ElcFlags] = elcContentSettingFlags2;
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x000BF6E8 File Offset: 0x000BD8E8
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			this.ValidateMessageClass(errors);
			if (this.RetentionEnabled)
			{
				if (this.RetentionAction == RetentionActionType.MoveToFolder && this.MoveToDestinationFolder == null)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ErrorMoveToDestinationFolderNotDefined, ElcContentSettingsSchema.MoveToDestinationFolder, this));
				}
				if (this.AgeLimitForRetention == null)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ErrorAgeLimitExpiration, ElcContentSettingsSchema.AgeLimitForRetention, this));
				}
			}
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x000BF758 File Offset: 0x000BD958
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			this.ValidateMessageClass(errors);
			if (this.JournalingEnabled && this.AddressForJournaling == null)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorAddressAutoCopy, ElcContentSettingsSchema.AddressForJournaling, this));
			}
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000BF790 File Offset: 0x000BD990
		private void ValidateMessageClass(List<ValidationError> errors)
		{
			bool flag = string.IsNullOrEmpty(this.MessageClass);
			string[] array = null;
			if (!flag)
			{
				array = this.MessageClass.Split(ElcMessageClass.MessageClassDelims, StringSplitOptions.RemoveEmptyEntries);
				flag = (array == null || array.Length == 0);
			}
			if (flag)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorMessageClassEmpty, ElcContentSettingsSchema.MessageClass, this));
				return;
			}
			foreach (string text in array)
			{
				int num = text.IndexOf('*');
				if (num != -1 && num < text.Length - 1)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ErrorMessageClassHasUnsupportedWildcard, ElcContentSettingsSchema.MessageClass, this));
					return;
				}
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06002F5E RID: 12126 RVA: 0x000BF832 File Offset: 0x000BDA32
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return this.objectVersion;
			}
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x000BF83A File Offset: 0x000BDA3A
		internal override void Initialize()
		{
			if (base.ExchangeVersion == RetentionPolicy.E14RetentionPolicyMajorVersion)
			{
				this.propertyBag.SetField(this.propertyBag.ObjectVersionPropertyDefinition, RetentionPolicy.E14RetentionPolicyFullVersion);
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06002F60 RID: 12128 RVA: 0x000BF86C File Offset: 0x000BDA6C
		internal override QueryFilter VersioningFilter
		{
			get
			{
				ExchangeObjectVersion exchange = ExchangeObjectVersion.Exchange2007;
				ExchangeObjectVersion nextMajorVersion = ExchangeObjectVersion.Exchange2010.NextMajorVersion.NextMajorVersion;
				return new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADObjectSchema.ExchangeVersion, exchange),
					new ComparisonFilter(ComparisonOperator.LessThan, ADObjectSchema.ExchangeVersion, nextMajorVersion)
				});
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (set) Token: 0x06002F61 RID: 12129 RVA: 0x000BF8BA File Offset: 0x000BDABA
		internal bool AppliesToFolder
		{
			set
			{
				this.objectVersion = (value ? ExchangeObjectVersion.Exchange2007 : RetentionPolicy.E14RetentionPolicyFullVersion);
				this.propertyBag.SetField(ADObjectSchema.ExchangeVersion, this.objectVersion);
			}
		}

		// Token: 0x04001FE1 RID: 8161
		private static ElcContentSettingsSchema schema = ObjectSchema.GetInstance<ElcContentSettingsSchema>();

		// Token: 0x04001FE2 RID: 8162
		private static string mostDerivedClass = "msExchELCContentSettings";

		// Token: 0x04001FE3 RID: 8163
		private ExchangeObjectVersion objectVersion = RetentionPolicy.E14RetentionPolicyFullVersion;
	}
}
