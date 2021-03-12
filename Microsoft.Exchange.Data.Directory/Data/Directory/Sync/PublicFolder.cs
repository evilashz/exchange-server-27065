using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000951 RID: 2385
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class PublicFolder : DirectoryObject
	{
		// Token: 0x170027ED RID: 10221
		// (get) Token: 0x0600703D RID: 28733 RVA: 0x001772A7 File Offset: 0x001754A7
		// (set) Token: 0x0600703E RID: 28734 RVA: 0x001772AF File Offset: 0x001754AF
		[XmlElement(Order = 0)]
		public DirectoryPropertyBooleanSingle DirSyncEnabled
		{
			get
			{
				return this.dirSyncEnabledField;
			}
			set
			{
				this.dirSyncEnabledField = value;
			}
		}

		// Token: 0x170027EE RID: 10222
		// (get) Token: 0x0600703F RID: 28735 RVA: 0x001772B8 File Offset: 0x001754B8
		// (set) Token: 0x06007040 RID: 28736 RVA: 0x001772C0 File Offset: 0x001754C0
		[XmlElement(Order = 1)]
		public DirectoryPropertyStringSingleLength1To256 DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x170027EF RID: 10223
		// (get) Token: 0x06007041 RID: 28737 RVA: 0x001772C9 File Offset: 0x001754C9
		// (set) Token: 0x06007042 RID: 28738 RVA: 0x001772D1 File Offset: 0x001754D1
		[XmlElement(Order = 2)]
		public DirectoryPropertyStringSingleLength1To256 Mail
		{
			get
			{
				return this.mailField;
			}
			set
			{
				this.mailField = value;
			}
		}

		// Token: 0x170027F0 RID: 10224
		// (get) Token: 0x06007043 RID: 28739 RVA: 0x001772DA File Offset: 0x001754DA
		// (set) Token: 0x06007044 RID: 28740 RVA: 0x001772E2 File Offset: 0x001754E2
		[XmlElement(Order = 3)]
		public DirectoryPropertyStringSingleMailNickname MailNickname
		{
			get
			{
				return this.mailNicknameField;
			}
			set
			{
				this.mailNicknameField = value;
			}
		}

		// Token: 0x170027F1 RID: 10225
		// (get) Token: 0x06007045 RID: 28741 RVA: 0x001772EB File Offset: 0x001754EB
		// (set) Token: 0x06007046 RID: 28742 RVA: 0x001772F3 File Offset: 0x001754F3
		[XmlElement(Order = 4)]
		public DirectoryPropertyBooleanSingle MSExchBypassAudit
		{
			get
			{
				return this.mSExchBypassAuditField;
			}
			set
			{
				this.mSExchBypassAuditField = value;
			}
		}

		// Token: 0x170027F2 RID: 10226
		// (get) Token: 0x06007047 RID: 28743 RVA: 0x001772FC File Offset: 0x001754FC
		// (set) Token: 0x06007048 RID: 28744 RVA: 0x00177304 File Offset: 0x00175504
		[XmlElement(Order = 5)]
		public DirectoryPropertyBooleanSingle MSExchMailboxAuditEnable
		{
			get
			{
				return this.mSExchMailboxAuditEnableField;
			}
			set
			{
				this.mSExchMailboxAuditEnableField = value;
			}
		}

		// Token: 0x170027F3 RID: 10227
		// (get) Token: 0x06007049 RID: 28745 RVA: 0x0017730D File Offset: 0x0017550D
		// (set) Token: 0x0600704A RID: 28746 RVA: 0x00177315 File Offset: 0x00175515
		[XmlElement(Order = 6)]
		public DirectoryPropertyInt32Single MSExchMailboxAuditLogAgeLimit
		{
			get
			{
				return this.mSExchMailboxAuditLogAgeLimitField;
			}
			set
			{
				this.mSExchMailboxAuditLogAgeLimitField = value;
			}
		}

		// Token: 0x170027F4 RID: 10228
		// (get) Token: 0x0600704B RID: 28747 RVA: 0x0017731E File Offset: 0x0017551E
		// (set) Token: 0x0600704C RID: 28748 RVA: 0x00177326 File Offset: 0x00175526
		[XmlElement(Order = 7)]
		public DirectoryPropertyInt32Single MSExchModerationFlags
		{
			get
			{
				return this.mSExchModerationFlagsField;
			}
			set
			{
				this.mSExchModerationFlagsField = value;
			}
		}

		// Token: 0x170027F5 RID: 10229
		// (get) Token: 0x0600704D RID: 28749 RVA: 0x0017732F File Offset: 0x0017552F
		// (set) Token: 0x0600704E RID: 28750 RVA: 0x00177337 File Offset: 0x00175537
		[XmlElement(Order = 8)]
		public DirectoryPropertyInt64Single MSExchRecipientTypeDetails
		{
			get
			{
				return this.mSExchRecipientTypeDetailsField;
			}
			set
			{
				this.mSExchRecipientTypeDetailsField = value;
			}
		}

		// Token: 0x170027F6 RID: 10230
		// (get) Token: 0x0600704F RID: 28751 RVA: 0x00177340 File Offset: 0x00175540
		// (set) Token: 0x06007050 RID: 28752 RVA: 0x00177348 File Offset: 0x00175548
		[XmlElement(Order = 9)]
		public DirectoryPropertyInt32Single MSExchRecipientSoftDeletedStatus
		{
			get
			{
				return this.mSExchRecipientSoftDeletedStatusField;
			}
			set
			{
				this.mSExchRecipientSoftDeletedStatusField = value;
			}
		}

		// Token: 0x170027F7 RID: 10231
		// (get) Token: 0x06007051 RID: 28753 RVA: 0x00177351 File Offset: 0x00175551
		// (set) Token: 0x06007052 RID: 28754 RVA: 0x00177359 File Offset: 0x00175559
		[XmlElement(Order = 10)]
		public DirectoryPropertyInt32Single MSExchTransportRecipientSettingsFlags
		{
			get
			{
				return this.mSExchTransportRecipientSettingsFlagsField;
			}
			set
			{
				this.mSExchTransportRecipientSettingsFlagsField = value;
			}
		}

		// Token: 0x170027F8 RID: 10232
		// (get) Token: 0x06007053 RID: 28755 RVA: 0x00177362 File Offset: 0x00175562
		// (set) Token: 0x06007054 RID: 28756 RVA: 0x0017736A File Offset: 0x0017556A
		[XmlElement(Order = 11)]
		public DirectoryPropertyProxyAddresses ProxyAddresses
		{
			get
			{
				return this.proxyAddressesField;
			}
			set
			{
				this.proxyAddressesField = value;
			}
		}

		// Token: 0x170027F9 RID: 10233
		// (get) Token: 0x06007055 RID: 28757 RVA: 0x00177373 File Offset: 0x00175573
		// (set) Token: 0x06007056 RID: 28758 RVA: 0x0017737B File Offset: 0x0017557B
		[XmlElement(Order = 12)]
		public DirectoryPropertyStringLength1To1123 ShadowProxyAddresses
		{
			get
			{
				return this.shadowProxyAddressesField;
			}
			set
			{
				this.shadowProxyAddressesField = value;
			}
		}

		// Token: 0x170027FA RID: 10234
		// (get) Token: 0x06007057 RID: 28759 RVA: 0x00177384 File Offset: 0x00175584
		// (set) Token: 0x06007058 RID: 28760 RVA: 0x0017738C File Offset: 0x0017558C
		[XmlElement(Order = 13)]
		public DirectoryPropertyStringSingleLength1To256 SourceAnchor
		{
			get
			{
				return this.sourceAnchorField;
			}
			set
			{
				this.sourceAnchorField = value;
			}
		}

		// Token: 0x170027FB RID: 10235
		// (get) Token: 0x06007059 RID: 28761 RVA: 0x00177395 File Offset: 0x00175595
		// (set) Token: 0x0600705A RID: 28762 RVA: 0x0017739D File Offset: 0x0017559D
		[XmlElement(Order = 14)]
		public DirectoryPropertyTargetAddress TargetAddress
		{
			get
			{
				return this.targetAddressField;
			}
			set
			{
				this.targetAddressField = value;
			}
		}

		// Token: 0x170027FC RID: 10236
		// (get) Token: 0x0600705B RID: 28763 RVA: 0x001773A6 File Offset: 0x001755A6
		// (set) Token: 0x0600705C RID: 28764 RVA: 0x001773AE File Offset: 0x001755AE
		[XmlArrayItem("AttributeSet", IsNullable = false)]
		[XmlArray(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/metadata/2010/01", Order = 15)]
		public AttributeSet[] SingleAuthorityMetadata
		{
			get
			{
				return this.singleAuthorityMetadataField;
			}
			set
			{
				this.singleAuthorityMetadataField = value;
			}
		}

		// Token: 0x170027FD RID: 10237
		// (get) Token: 0x0600705D RID: 28765 RVA: 0x001773B7 File Offset: 0x001755B7
		// (set) Token: 0x0600705E RID: 28766 RVA: 0x001773BF File Offset: 0x001755BF
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
			}
		}

		// Token: 0x0600705F RID: 28767 RVA: 0x001773C8 File Offset: 0x001755C8
		internal override void ForEachProperty(IPropertyProcessor processor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040048D7 RID: 18647
		private DirectoryPropertyBooleanSingle dirSyncEnabledField;

		// Token: 0x040048D8 RID: 18648
		private DirectoryPropertyStringSingleLength1To256 displayNameField;

		// Token: 0x040048D9 RID: 18649
		private DirectoryPropertyStringSingleLength1To256 mailField;

		// Token: 0x040048DA RID: 18650
		private DirectoryPropertyStringSingleMailNickname mailNicknameField;

		// Token: 0x040048DB RID: 18651
		private DirectoryPropertyBooleanSingle mSExchBypassAuditField;

		// Token: 0x040048DC RID: 18652
		private DirectoryPropertyBooleanSingle mSExchMailboxAuditEnableField;

		// Token: 0x040048DD RID: 18653
		private DirectoryPropertyInt32Single mSExchMailboxAuditLogAgeLimitField;

		// Token: 0x040048DE RID: 18654
		private DirectoryPropertyInt32Single mSExchModerationFlagsField;

		// Token: 0x040048DF RID: 18655
		private DirectoryPropertyInt64Single mSExchRecipientTypeDetailsField;

		// Token: 0x040048E0 RID: 18656
		private DirectoryPropertyInt32Single mSExchRecipientSoftDeletedStatusField;

		// Token: 0x040048E1 RID: 18657
		private DirectoryPropertyInt32Single mSExchTransportRecipientSettingsFlagsField;

		// Token: 0x040048E2 RID: 18658
		private DirectoryPropertyProxyAddresses proxyAddressesField;

		// Token: 0x040048E3 RID: 18659
		private DirectoryPropertyStringLength1To1123 shadowProxyAddressesField;

		// Token: 0x040048E4 RID: 18660
		private DirectoryPropertyStringSingleLength1To256 sourceAnchorField;

		// Token: 0x040048E5 RID: 18661
		private DirectoryPropertyTargetAddress targetAddressField;

		// Token: 0x040048E6 RID: 18662
		private AttributeSet[] singleAuthorityMetadataField;

		// Token: 0x040048E7 RID: 18663
		private XmlAttribute[] anyAttrField;
	}
}
