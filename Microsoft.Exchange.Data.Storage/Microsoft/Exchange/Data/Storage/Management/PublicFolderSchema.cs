using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A66 RID: 2662
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderSchema : MailboxFolderSchema
	{
		// Token: 0x04003729 RID: 14121
		public static readonly XsoDriverPropertyDefinition ProxyGuid = new XsoDriverPropertyDefinition(FolderSchema.ProxyGuid, "ProxyGuid", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400372A RID: 14122
		public static readonly XsoDriverPropertyDefinition MailEnabledValue = new XsoDriverPropertyDefinition(FolderSchema.MailEnabled, "MailEnabledValue", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400372B RID: 14123
		public static readonly XsoDriverPropertyDefinition DisablePerUserReadValue = new XsoDriverPropertyDefinition(FolderSchema.DisablePerUserRead, "DisablePerUserReadValue", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400372C RID: 14124
		public static readonly XsoDriverPropertyDefinition EformsLocaleIdValue = new XsoDriverPropertyDefinition(FolderSchema.EformsLocaleId, "EformsLocaleIdValue", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400372D RID: 14125
		public new static readonly XsoDriverPropertyDefinition Name = new XsoDriverPropertyDefinition(FolderSchema.DisplayName, "Name", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, string.Empty, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400372E RID: 14126
		internal static readonly XsoDriverPropertyDefinition ReplicaListBinary = new XsoDriverPropertyDefinition(InternalSchema.ReplicaListBinary, "ReplicaListBinary", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400372F RID: 14127
		public static readonly XsoDriverPropertyDefinition OverallAgeLimit = new XsoDriverPropertyDefinition(FolderSchema.OverallAgeLimit, "OverallAgeLimit", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003730 RID: 14128
		public static readonly XsoDriverPropertyDefinition RetentionAgeLimit = new XsoDriverPropertyDefinition(FolderSchema.RetentionAgeLimit, "RetentionAgeLimit", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003731 RID: 14129
		public static readonly XsoDriverPropertyDefinition PfQuotaStyle = new XsoDriverPropertyDefinition(FolderSchema.PfQuotaStyle, "PfQuotaStyle", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003732 RID: 14130
		public static readonly XsoDriverPropertyDefinition PfOverHardQuotaLimit = new XsoDriverPropertyDefinition(FolderSchema.PfOverHardQuotaLimit, "PfOverHardQuotaLimit", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003733 RID: 14131
		public static readonly XsoDriverPropertyDefinition LastMovedTimeStamp = new XsoDriverPropertyDefinition(FolderSchema.LastMovedTimeStamp, "LastMovedTimeStamp", ExchangeObjectVersion.Exchange2012, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003734 RID: 14132
		public static readonly XsoDriverPropertyDefinition PfStorageQuota = new XsoDriverPropertyDefinition(FolderSchema.PfStorageQuota, "PfStorageQuota", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003735 RID: 14133
		public static readonly XsoDriverPropertyDefinition PfMsgSizeLimit = new XsoDriverPropertyDefinition(FolderSchema.PfMsgSizeLimit, "PfMsgSizeLimit", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003736 RID: 14134
		public new static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2003, typeof(PublicFolderId), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated | PropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailboxFolderSchema.FolderPath,
			MailboxFolderSchema.InternalFolderIdentity
		}, null, new GetterDelegate(PublicFolder.IdentityGetter), null);

		// Token: 0x04003737 RID: 14135
		public new static readonly SimpleProviderPropertyDefinition ParentFolder = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2003, typeof(PublicFolderId), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailboxFolderSchema.FolderPath,
			MailboxFolderSchema.InternalFolderIdentity,
			MailboxFolderSchema.InternalParentFolderIdentity
		}, null, new GetterDelegate(PublicFolder.ParentFolderGetter), null);

		// Token: 0x04003738 RID: 14136
		public static readonly SimpleProviderPropertyDefinition MailRecipientGuid = new SimpleProviderPropertyDefinition("MailRecipientGuid", ExchangeObjectVersion.Exchange2003, typeof(Guid?), PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PublicFolderSchema.ProxyGuid
		}, null, new GetterDelegate(PublicFolder.MailRecipientGuidGetter), new SetterDelegate(PublicFolder.MailRecipientGuidSetter));

		// Token: 0x04003739 RID: 14137
		public static readonly SimpleProviderPropertyDefinition PerUserReadStateEnabled = new SimpleProviderPropertyDefinition("PerUserReadStateEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool?), PropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PublicFolderSchema.DisablePerUserReadValue
		}, null, new GetterDelegate(PublicFolder.PerUserReadStateEnabledGetter), new SetterDelegate(PublicFolder.PerUserReadStateEnabledSetter));

		// Token: 0x0400373A RID: 14138
		public static readonly SimpleProviderPropertyDefinition EformsLocaleId = new SimpleProviderPropertyDefinition("EformsLocaleId", ExchangeObjectVersion.Exchange2003, typeof(CultureInfo), PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PublicFolderSchema.EformsLocaleIdValue
		}, null, new GetterDelegate(PublicFolder.EformsLocaleIdGetter), new SetterDelegate(PublicFolder.EformsLocaleIdSetter));

		// Token: 0x0400373B RID: 14139
		public static readonly SimpleProviderPropertyDefinition MailEnabled = new SimpleProviderPropertyDefinition("MailEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool?), PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PublicFolderSchema.MailEnabledValue
		}, null, new GetterDelegate(PublicFolder.MailEnabledGetter), new SetterDelegate(PublicFolder.MailEnabledSetter));

		// Token: 0x0400373C RID: 14140
		public static readonly SimpleProviderPropertyDefinition EntryId = new SimpleProviderPropertyDefinition("EntryId", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailboxFolderSchema.InternalFolderIdentity
		}, null, new GetterDelegate(PublicFolder.EntryIdGetter), null);

		// Token: 0x0400373D RID: 14141
		public static readonly SimpleProviderPropertyDefinition ContentMailboxInfo = new SimpleProviderPropertyDefinition("ContentMailboxInfo", ExchangeObjectVersion.Exchange2003, typeof(PublicFolderContentMailboxInfo), PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PublicFolderSchema.ReplicaListBinary
		}, null, new GetterDelegate(PublicFolder.ContentMailboxInfoGetter), new SetterDelegate(PublicFolder.ContentMailboxInfoSetter));

		// Token: 0x0400373E RID: 14142
		public static readonly SimpleProviderPropertyDefinition AgeLimit = new SimpleProviderPropertyDefinition("AgeLimit", ExchangeObjectVersion.Exchange2003, typeof(EnhancedTimeSpan?), PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneDay, EnhancedTimeSpan.FromDays(24855.0)),
			new NullableEnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneDay)
		}, new ProviderPropertyDefinition[]
		{
			PublicFolderSchema.OverallAgeLimit
		}, null, new GetterDelegate(PublicFolder.AgeLimitGetter), new SetterDelegate(PublicFolder.AgeLimitSetter));

		// Token: 0x0400373F RID: 14143
		public static readonly SimpleProviderPropertyDefinition RetainDeletedItemsFor = new SimpleProviderPropertyDefinition("RetainDeletedItemsFor", ExchangeObjectVersion.Exchange2003, typeof(EnhancedTimeSpan?), PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromDays(24855.0)),
			new NullableEnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneDay)
		}, new ProviderPropertyDefinition[]
		{
			PublicFolderSchema.RetentionAgeLimit
		}, null, new GetterDelegate(PublicFolder.RetainDeletedItemsForGetter), new SetterDelegate(PublicFolder.RetainDeletedItemsForSetter));

		// Token: 0x04003740 RID: 14144
		public static readonly SimpleProviderPropertyDefinition ProhibitPostQuota = new SimpleProviderPropertyDefinition("ProhibitPostQuota", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<ByteQuantifiedSize>?), PropertyDefinitionFlags.Calculated, Unlimited<ByteQuantifiedSize>.UnlimitedValue, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedNullableUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(1UL), ByteQuantifiedSize.FromKB(2147483647UL))
		}, new ProviderPropertyDefinition[]
		{
			PublicFolderSchema.PfOverHardQuotaLimit
		}, null, new GetterDelegate(PublicFolder.ProhibitPostQuotaGetter), new SetterDelegate(PublicFolder.ProhibitPostQuotaSetter));

		// Token: 0x04003741 RID: 14145
		public static readonly SimpleProviderPropertyDefinition LastMovedTime = new SimpleProviderPropertyDefinition("LastMovedTime", ExchangeObjectVersion.Exchange2012, typeof(ExDateTime?), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			PublicFolderSchema.LastMovedTimeStamp
		}, null, new GetterDelegate(PublicFolder.LastMovedTimeGetter), null);

		// Token: 0x04003742 RID: 14146
		public static readonly SimpleProviderPropertyDefinition IssueWarningQuota = new SimpleProviderPropertyDefinition("IssueWarningQuota", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<ByteQuantifiedSize>?), PropertyDefinitionFlags.Calculated, Unlimited<ByteQuantifiedSize>.UnlimitedValue, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedNullableUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2147483647UL))
		}, new ProviderPropertyDefinition[]
		{
			PublicFolderSchema.PfStorageQuota
		}, null, new GetterDelegate(PublicFolder.IssueWarningQuotaGetter), new SetterDelegate(PublicFolder.IssueWarningQuotaSetter));

		// Token: 0x04003743 RID: 14147
		public static readonly SimpleProviderPropertyDefinition MaxItemSize = new SimpleProviderPropertyDefinition("MaxItemSize", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<ByteQuantifiedSize>?), PropertyDefinitionFlags.Calculated, Unlimited<ByteQuantifiedSize>.UnlimitedValue, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedNullableUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2097151UL))
		}, new ProviderPropertyDefinition[]
		{
			PublicFolderSchema.PfMsgSizeLimit
		}, null, new GetterDelegate(PublicFolder.MaxItemSizeGetter), new SetterDelegate(PublicFolder.MaxItemSizeSetter));
	}
}
