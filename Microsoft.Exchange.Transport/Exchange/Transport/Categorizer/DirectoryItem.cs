using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001CB RID: 459
	internal abstract class DirectoryItem : RecipientItem
	{
		// Token: 0x060014F5 RID: 5365 RVA: 0x00053EDE File Offset: 0x000520DE
		public DirectoryItem(MailRecipient recipient) : base(recipient)
		{
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x00053EE8 File Offset: 0x000520E8
		public static CachedProperty[] AllCachedProperties
		{
			get
			{
				if (DirectoryItem.allCachedProperties == null)
				{
					List<CachedProperty> list = new List<CachedProperty>();
					list.AddRange(DirectoryItemSchema.CachedProperties);
					list.AddRange(RestrictedItemSchema.CachedProperties);
					list.AddRange(DeliverableItemSchema.CachedProperties);
					list.AddRange(MailboxItemSchema.CachedProperties);
					list.AddRange(ContactItemSchema.CachedProperties);
					list.AddRange(GroupItemSchema.CachedProperties);
					list.AddRange(ForwardableItemSchema.CachedProperties);
					list.AddRange(PublicFolderItemSchema.CachedProperties);
					list.AddRange(PublicDatabaseItemSchema.CachedProperties);
					list.AddRange(ReroutableItemSchema.CachedProperties);
					list.AddRange(SenderSchema.CachedProperties);
					DirectoryItem.allCachedProperties = list.ToArray();
				}
				return DirectoryItem.allCachedProperties;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00053F8E File Offset: 0x0005218E
		public RecipientType RecipientType
		{
			get
			{
				return base.GetProperty<RecipientType>("Microsoft.Exchange.Transport.DirectoryData.RecipientType");
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x00053F9C File Offset: 0x0005219C
		public RecipientTypeDetails? RecipientTypeDetails
		{
			get
			{
				long? property = base.GetProperty<long?>("Microsoft.Exchange.Transport.DirectoryData.RecipientTypeDetailsRaw");
				if (property != null && Enum.IsDefined(typeof(RecipientTypeDetails), property))
				{
					return new RecipientTypeDetails?((RecipientTypeDetails)property.Value);
				}
				return null;
			}
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00053FEC File Offset: 0x000521EC
		public new static DirectoryItem Create(MailRecipient recipient)
		{
			RecipientType recipientType;
			if (!recipient.ExtendedProperties.TryGetValue<RecipientType>("Microsoft.Exchange.Transport.DirectoryData.RecipientType", out recipientType))
			{
				return null;
			}
			switch (recipientType)
			{
			case RecipientType.Invalid:
				return new InvalidItem(recipient);
			case RecipientType.User:
			case RecipientType.UserMailbox:
			case RecipientType.MailUser:
			{
				string text;
				if (ContactItem.TryGetTargetAddress(recipient, out text))
				{
					return new ContactItem(recipient);
				}
				return new ForwardableItem(recipient);
			}
			case RecipientType.Contact:
			case RecipientType.MailContact:
				return new ContactItem(recipient);
			case RecipientType.Group:
			case RecipientType.MailUniversalDistributionGroup:
			case RecipientType.MailUniversalSecurityGroup:
			case RecipientType.MailNonUniversalGroup:
			case RecipientType.DynamicDistributionGroup:
			{
				string text;
				if (VariantConfiguration.InvariantNoFlightingSnapshot.Transport.TargetAddressRoutingForRemoteGroupMailbox.Enabled && ContactItem.TryGetTargetAddress(recipient, out text) && DirectoryItem.IsRecipientRemoteUnifiedGroup(recipient))
				{
					return new ContactItem(recipient);
				}
				return new GroupItem(recipient);
			}
			case RecipientType.PublicFolder:
			{
				bool flag = false;
				if (recipient.ExtendedProperties.TryGetValue<bool>("Microsoft.Exchange.Transport.IsRemoteRecipient", out flag) && flag)
				{
					return new ContactItem(recipient);
				}
				bool flag2 = false;
				if (recipient.ExtendedProperties.TryGetValue<bool>("Microsoft.Exchange.Transport.IsOneOffRecipient", out flag2) && flag2)
				{
					return null;
				}
				return new PublicFolderItem(recipient);
			}
			case RecipientType.PublicDatabase:
				return new PublicDatabaseItem(recipient);
			case RecipientType.SystemAttendantMailbox:
			case RecipientType.SystemMailbox:
				return new MailboxItem(recipient);
			case RecipientType.MicrosoftExchange:
				return new MicrosoftExchangeItem(recipient);
			default:
				throw new InvalidOperationException("Unknown recipient type: " + recipientType);
			}
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0005412C File Offset: 0x0005232C
		public static void Set(ADRawEntry entry, MailRecipient recipient)
		{
			RecipientType recipientType = (RecipientType)entry[ADRecipientSchema.RecipientType];
			switch (recipientType)
			{
			case RecipientType.Invalid:
				DirectoryItemSchema.Set(entry, recipient);
				return;
			case RecipientType.User:
			case RecipientType.UserMailbox:
			case RecipientType.MailUser:
				if (entry[ADRecipientSchema.ExternalEmailAddress] != null)
				{
					ContactItemSchema.Set(entry, recipient);
					return;
				}
				ForwardableItemSchema.Set(entry, recipient);
				return;
			case RecipientType.Contact:
			case RecipientType.MailContact:
				ContactItemSchema.Set(entry, recipient);
				return;
			case RecipientType.Group:
			case RecipientType.MailUniversalDistributionGroup:
			case RecipientType.MailUniversalSecurityGroup:
			case RecipientType.MailNonUniversalGroup:
			case RecipientType.DynamicDistributionGroup:
				if (VariantConfiguration.InvariantNoFlightingSnapshot.Transport.TargetAddressRoutingForRemoteGroupMailbox.Enabled && entry[ADRecipientSchema.ExternalEmailAddress] != null && (RecipientTypeDetails)entry[ADRecipientSchema.RecipientTypeDetailsRaw] == Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.RemoteGroupMailbox)
				{
					ContactItemSchema.Set(entry, recipient);
					return;
				}
				GroupItemSchema.Set(entry, recipient);
				return;
			case RecipientType.PublicFolder:
				PublicFolderItemSchema.Set(entry, recipient);
				return;
			case RecipientType.PublicDatabase:
				PublicDatabaseItemSchema.Set(entry, recipient);
				return;
			case RecipientType.SystemAttendantMailbox:
			case RecipientType.SystemMailbox:
				MailboxItemSchema.Set(entry, recipient);
				return;
			case RecipientType.MicrosoftExchange:
				ForwardableItemSchema.Set(entry, recipient);
				return;
			default:
				throw new InvalidOperationException("Unknown recipient type: " + recipientType);
			}
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0005424C File Offset: 0x0005244C
		public static string GetPrimarySmtpAddress(TransportMiniRecipient entry)
		{
			SmtpAddress primarySmtpAddress = entry.PrimarySmtpAddress;
			if (primarySmtpAddress == SmtpAddress.Empty)
			{
				ExTraceGlobals.ResolverTracer.TraceDebug(0L, "no primary SMTP address");
				return null;
			}
			if (!primarySmtpAddress.IsValidAddress)
			{
				ExTraceGlobals.ResolverTracer.TraceDebug<string>(0L, "Invalid Smtp Address {0}", primarySmtpAddress.ToString());
				return null;
			}
			return primarySmtpAddress.ToString();
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x000542B6 File Offset: 0x000524B6
		public override void PostProcess(Expansion expansion)
		{
			OofRestriction.InternalUserOofCheck(expansion, base.Recipient);
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x000542C4 File Offset: 0x000524C4
		protected Guid ObjectGuid
		{
			get
			{
				return base.GetProperty<Guid>("Microsoft.Exchange.Transport.DirectoryData.ObjectGuid", Guid.Empty);
			}
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x000542D8 File Offset: 0x000524D8
		private static bool IsRecipientRemoteUnifiedGroup(MailRecipient recipient)
		{
			long? value = recipient.ExtendedProperties.GetValue<long?>("Microsoft.Exchange.Transport.DirectoryData.RecipientTypeDetailsRaw", null);
			return value != null && Enum.IsDefined(typeof(RecipientTypeDetails), value) && value.Value == 8796093022208L;
		}

		// Token: 0x04000A8A RID: 2698
		private static CachedProperty[] allCachedProperties;
	}
}
