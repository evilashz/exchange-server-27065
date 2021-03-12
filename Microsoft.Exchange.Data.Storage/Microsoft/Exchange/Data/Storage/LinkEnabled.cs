using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C7B RID: 3195
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class LinkEnabled : SmartPropertyDefinition
	{
		// Token: 0x06007025 RID: 28709 RVA: 0x001F0920 File Offset: 0x001EEB20
		public LinkEnabled() : base("LinkEnabled", typeof(bool), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.OutlookPhishingStamp, PropertyDependencyType.AllRead),
			new PropertyDependency(InternalSchema.OutlookSpoofingStamp, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06007026 RID: 28710 RVA: 0x001F096C File Offset: 0x001EEB6C
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			int valueOrDefault = propertyBag.GetValueOrDefault<int>(InternalSchema.OutlookPhishingStamp);
			int valueOrDefault2 = propertyBag.GetValueOrDefault<int>(InternalSchema.OutlookSpoofingStamp);
			if ((valueOrDefault & 268435455) != 0 || (valueOrDefault2 & 268435455) != 0)
			{
				return (valueOrDefault & 268435456) != 0 || (valueOrDefault2 & 268435456) != 0;
			}
			return new PropertyError(this, PropertyErrorCode.NotFound);
		}

		// Token: 0x06007027 RID: 28711 RVA: 0x001F09D4 File Offset: 0x001EEBD4
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			int num = propertyBag.GetValueOrDefault<int>(InternalSchema.OutlookPhishingStamp);
			if (num <= 0)
			{
				byte[] array = LinkEnabled.GetMovingStamp(propertyBag.Context.Session);
				if (array.Length != 4)
				{
					array = new byte[]
					{
						1,
						2,
						3,
						4
					};
				}
				num = BitConverter.ToInt32(array, 0);
			}
			if ((bool)value)
			{
				num |= 268435456;
			}
			else
			{
				num &= -268435457;
			}
			propertyBag.SetValueWithFixup(InternalSchema.OutlookPhishingStamp, num);
		}

		// Token: 0x06007028 RID: 28712 RVA: 0x001F0A50 File Offset: 0x001EEC50
		private static byte[] GetMovingStamp(StoreSession storeSession)
		{
			MailboxSession mailboxSession = storeSession as MailboxSession;
			if (mailboxSession == null)
			{
				return Array<byte>.Empty;
			}
			using (Folder folder = Folder.Bind(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox), new PropertyDefinition[]
			{
				InternalSchema.AdditionalRenEntryIds
			}))
			{
				int num = 5;
				byte[][] array = folder.TryGetProperty(InternalSchema.AdditionalRenEntryIds) as byte[][];
				if (array != null && array.Length > num)
				{
					return array[num];
				}
			}
			return Array<byte>.Empty;
		}

		// Token: 0x04004C80 RID: 19584
		private const int PhishingStampMask = 268435455;

		// Token: 0x04004C81 RID: 19585
		private const int PhishingEnabledMask = 268435456;
	}
}
