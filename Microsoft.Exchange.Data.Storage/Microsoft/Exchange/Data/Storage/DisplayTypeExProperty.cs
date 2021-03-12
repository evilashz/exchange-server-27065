using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C94 RID: 3220
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class DisplayTypeExProperty : SmartPropertyDefinition
	{
		// Token: 0x0600708C RID: 28812 RVA: 0x001F2950 File Offset: 0x001F0B50
		internal DisplayTypeExProperty() : base("DisplayTypeEx", typeof(RecipientDisplayType), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.DisplayTypeExInternal, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x0600708D RID: 28813 RVA: 0x001F2990 File Offset: 0x001F0B90
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object value = propertyBag.GetValue(InternalSchema.DisplayTypeExInternal);
			if (PropertyError.IsPropertyError(value))
			{
				return value;
			}
			return (RecipientDisplayType)((int)value);
		}

		// Token: 0x0600708E RID: 28814 RVA: 0x001F29C0 File Offset: 0x001F0BC0
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			RecipientDisplayType recipientDisplayType = (RecipientDisplayType)value;
			propertyBag.SetValueWithFixup(InternalSchema.DisplayTypeExInternal, (int)recipientDisplayType);
			propertyBag.SetValueWithFixup(InternalSchema.DisplayType, (int)DisplayTypeExProperty.GetLegacyRecipientDisplayType(recipientDisplayType));
		}

		// Token: 0x0600708F RID: 28815 RVA: 0x001F29FD File Offset: 0x001F0BFD
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.Delete(InternalSchema.DisplayTypeExInternal);
			propertyBag.Delete(InternalSchema.DisplayType);
		}

		// Token: 0x06007090 RID: 28816 RVA: 0x001F2A17 File Offset: 0x001F0C17
		internal static bool IsDL(LegacyRecipientDisplayType legacyRecipientDisplayType)
		{
			return legacyRecipientDisplayType == LegacyRecipientDisplayType.DistributionList || legacyRecipientDisplayType == LegacyRecipientDisplayType.DynamicDistributionList || legacyRecipientDisplayType == LegacyRecipientDisplayType.PersonalDistributionList;
		}

		// Token: 0x06007091 RID: 28817 RVA: 0x001F2A28 File Offset: 0x001F0C28
		internal static bool IsDL(RecipientDisplayType recipientDisplayType)
		{
			RecipientDisplayType recipientDisplayTypeInNativeForest = DisplayTypeExProperty.GetRecipientDisplayTypeInNativeForest(recipientDisplayType);
			return recipientDisplayTypeInNativeForest == RecipientDisplayType.DistributionGroup || recipientDisplayTypeInNativeForest == RecipientDisplayType.DynamicDistributionGroup || recipientDisplayTypeInNativeForest == RecipientDisplayType.SecurityDistributionGroup || recipientDisplayTypeInNativeForest == RecipientDisplayType.PrivateDistributionList;
		}

		// Token: 0x06007092 RID: 28818 RVA: 0x001F2A52 File Offset: 0x001F0C52
		internal static bool IsMailboxUser(LegacyRecipientDisplayType legacyRecipientDisplayType)
		{
			return legacyRecipientDisplayType == LegacyRecipientDisplayType.MailUser || legacyRecipientDisplayType == LegacyRecipientDisplayType.RemoteMailUser;
		}

		// Token: 0x06007093 RID: 28819 RVA: 0x001F2A60 File Offset: 0x001F0C60
		internal static bool IsMailboxUser(RecipientDisplayType recipientDisplayType)
		{
			RecipientDisplayType recipientDisplayTypeInNativeForest = DisplayTypeExProperty.GetRecipientDisplayTypeInNativeForest(recipientDisplayType);
			return recipientDisplayTypeInNativeForest == RecipientDisplayType.MailboxUser || recipientDisplayTypeInNativeForest == RecipientDisplayType.RemoteMailUser || recipientDisplayTypeInNativeForest == RecipientDisplayType.ACLableMailboxUser || recipientDisplayTypeInNativeForest == RecipientDisplayType.ACLableRemoteMailUser || recipientDisplayTypeInNativeForest == RecipientDisplayType.ACLableSyncedMailboxUser || recipientDisplayTypeInNativeForest == RecipientDisplayType.ACLableSyncedRemoteMailUser || recipientDisplayTypeInNativeForest == RecipientDisplayType.LinkedUser || recipientDisplayTypeInNativeForest == RecipientDisplayType.SyncedMailboxUser || recipientDisplayTypeInNativeForest == RecipientDisplayType.SyncedRemoteMailUser;
		}

		// Token: 0x06007094 RID: 28820 RVA: 0x001F2AB4 File Offset: 0x001F0CB4
		internal static bool IsRoom(RecipientDisplayType recipientDisplayType)
		{
			RecipientDisplayType recipientDisplayTypeInNativeForest = DisplayTypeExProperty.GetRecipientDisplayTypeInNativeForest(recipientDisplayType);
			return recipientDisplayTypeInNativeForest == RecipientDisplayType.ConferenceRoomMailbox;
		}

		// Token: 0x06007095 RID: 28821 RVA: 0x001F2ACC File Offset: 0x001F0CCC
		internal static bool IsResource(RecipientDisplayType recipientDisplayType)
		{
			RecipientDisplayType recipientDisplayTypeInNativeForest = DisplayTypeExProperty.GetRecipientDisplayTypeInNativeForest(recipientDisplayType);
			return recipientDisplayTypeInNativeForest == RecipientDisplayType.EquipmentMailbox;
		}

		// Token: 0x06007096 RID: 28822 RVA: 0x001F2AE4 File Offset: 0x001F0CE4
		internal static bool IsGroupMailbox(RecipientDisplayType recipientDisplayType)
		{
			RecipientDisplayType recipientDisplayTypeInNativeForest = DisplayTypeExProperty.GetRecipientDisplayTypeInNativeForest(recipientDisplayType);
			return recipientDisplayTypeInNativeForest == RecipientDisplayType.GroupMailboxUser;
		}

		// Token: 0x06007097 RID: 28823 RVA: 0x001F2AFD File Offset: 0x001F0CFD
		private static bool IsLocalForestRecipient(RecipientDisplayType recipientDisplayType)
		{
			return (recipientDisplayType & (RecipientDisplayType)(-2147483648)) == RecipientDisplayType.MailboxUser;
		}

		// Token: 0x06007098 RID: 28824 RVA: 0x001F2B09 File Offset: 0x001F0D09
		private static RecipientDisplayType GetRecipientDisplayTypeInNativeForest(RecipientDisplayType recipientDisplayType)
		{
			if (!DisplayTypeExProperty.IsLocalForestRecipient(recipientDisplayType))
			{
				return DisplayTypeExProperty.GetRecipientDisplayTypeInForeignForest(recipientDisplayType);
			}
			return DisplayTypeExProperty.GetRecipientDisplayTypeInLocalForest(recipientDisplayType);
		}

		// Token: 0x06007099 RID: 28825 RVA: 0x001F2B20 File Offset: 0x001F0D20
		private static RecipientDisplayType GetRecipientDisplayTypeInLocalForest(RecipientDisplayType recipientDisplayType)
		{
			return DisplayTypeExProperty.FixRecipientDisplayType(recipientDisplayType & (RecipientDisplayType)1073742079);
		}

		// Token: 0x0600709A RID: 28826 RVA: 0x001F2B2E File Offset: 0x001F0D2E
		private static RecipientDisplayType GetRecipientDisplayTypeInForeignForest(RecipientDisplayType recipientDisplayType)
		{
			return DisplayTypeExProperty.FixRecipientDisplayType((recipientDisplayType & (RecipientDisplayType)65280) >> 8);
		}

		// Token: 0x0600709B RID: 28827 RVA: 0x001F2B3E File Offset: 0x001F0D3E
		private static RecipientDisplayType FixRecipientDisplayType(RecipientDisplayType recipientDisplayType)
		{
			if (!EnumValidator.IsValidValue<RecipientDisplayType>(recipientDisplayType) && EnumValidator.IsValidValue<RecipientDisplayType>(recipientDisplayType | RecipientDisplayType.ACLableMailboxUser))
			{
				recipientDisplayType |= RecipientDisplayType.ACLableMailboxUser;
			}
			return recipientDisplayType;
		}

		// Token: 0x0600709C RID: 28828 RVA: 0x001F2B60 File Offset: 0x001F0D60
		private static LegacyRecipientDisplayType GetLegacyRecipientDisplayType(RecipientDisplayType recipientDisplayType)
		{
			LegacyRecipientDisplayType legacyRecipientDisplayType = (LegacyRecipientDisplayType)(DisplayTypeExProperty.GetRecipientDisplayTypeInLocalForest(recipientDisplayType) & (RecipientDisplayType)255);
			if (EnumValidator.IsValidValue<LegacyRecipientDisplayType>(legacyRecipientDisplayType))
			{
				return legacyRecipientDisplayType;
			}
			if (legacyRecipientDisplayType != (LegacyRecipientDisplayType)9)
			{
				return LegacyRecipientDisplayType.MailUser;
			}
			return LegacyRecipientDisplayType.DistributionList;
		}

		// Token: 0x04004DB6 RID: 19894
		private const RecipientDisplayType RdtForeignMask = (RecipientDisplayType)(-2147483648);

		// Token: 0x04004DB7 RID: 19895
		private const RecipientDisplayType RdtAclMask = RecipientDisplayType.ACLableMailboxUser;

		// Token: 0x04004DB8 RID: 19896
		private const RecipientDisplayType RdtRemoteMask = (RecipientDisplayType)65280;

		// Token: 0x04004DB9 RID: 19897
		private const RecipientDisplayType RdtLocalMask = (RecipientDisplayType)255;
	}
}
