using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000E8 RID: 232
	internal class MessageTypeTable
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0000CD7E File Offset: 0x0000AF7E
		internal static IEnumerable<MessageTypeEntry> Table
		{
			get
			{
				return MessageTypeTable.table;
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000CED4 File Offset: 0x0000B0D4
		internal static MessageFlags GetMessageFlags(MessageType type)
		{
			MessageTypeEntry messageTypeEntry = MessageTypeTable.table[(int)type];
			return messageTypeEntry.MessageFlags;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
		internal static MessageSecurityType GetMessageSecurityType(MessageType type)
		{
			MessageTypeEntry messageTypeEntry = MessageTypeTable.table[(int)type];
			return messageTypeEntry.MessageSecurityType;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000CF0C File Offset: 0x0000B10C
		internal static MessageTypeEntry GetMessageTypeEntry(MessageType type)
		{
			return MessageTypeTable.table[(int)type];
		}

		// Token: 0x04000394 RID: 916
		private static MessageTypeEntry[] table = new MessageTypeEntry[]
		{
			new MessageTypeEntry(MessageType.Undefined, MessageFlags.None),
			new MessageTypeEntry(MessageType.Unknown, MessageFlags.KnownApplication),
			new MessageTypeEntry(MessageType.SingleAttachment, MessageFlags.Normal),
			new MessageTypeEntry(MessageType.MultipleAttachments, MessageFlags.Normal),
			new MessageTypeEntry(MessageType.Normal, MessageFlags.Normal),
			new MessageTypeEntry(MessageType.NormalWithRegularAttachments, MessageFlags.Normal),
			new MessageTypeEntry(MessageType.SummaryTnef, MessageFlags.Normal | MessageFlags.Tnef),
			new MessageTypeEntry(MessageType.LegacyTnef, MessageFlags.Normal | MessageFlags.Tnef),
			new MessageTypeEntry(MessageType.SuperLegacyTnef, MessageFlags.Normal | MessageFlags.Tnef),
			new MessageTypeEntry(MessageType.SuperLegacyTnefWithRegularAttachments, MessageFlags.Normal | MessageFlags.Tnef),
			new MessageTypeEntry(MessageType.Voice, MessageFlags.Normal | MessageFlags.KnownApplication),
			new MessageTypeEntry(MessageType.Fax, MessageFlags.KnownApplication),
			new MessageTypeEntry(MessageType.Journal, MessageFlags.System),
			new MessageTypeEntry(MessageType.Dsn, MessageFlags.System),
			new MessageTypeEntry(MessageType.Mdn, MessageFlags.System),
			new MessageTypeEntry(MessageType.MsRightsProtected, MessageFlags.KnownApplication, MessageSecurityType.Encrypted),
			new MessageTypeEntry(MessageType.Quota, MessageFlags.System),
			new MessageTypeEntry(MessageType.AdReplicationMessage, MessageFlags.System),
			new MessageTypeEntry(MessageType.PgpEncrypted, MessageFlags.KnownApplication, MessageSecurityType.Encrypted),
			new MessageTypeEntry(MessageType.SmimeSignedNormal, MessageFlags.KnownApplication, MessageSecurityType.ClearSigned),
			new MessageTypeEntry(MessageType.SmimeSignedUnknown, MessageFlags.KnownApplication, MessageSecurityType.ClearSigned),
			new MessageTypeEntry(MessageType.SmimeSignedEncrypted, MessageFlags.KnownApplication, MessageSecurityType.Encrypted),
			new MessageTypeEntry(MessageType.SmimeOpaqueSigned, MessageFlags.KnownApplication, MessageSecurityType.OpaqueSigned),
			new MessageTypeEntry(MessageType.SmimeEncrypted, MessageFlags.KnownApplication, MessageSecurityType.Encrypted),
			new MessageTypeEntry(MessageType.ApprovalInitiation, MessageFlags.System),
			new MessageTypeEntry(MessageType.UMPartner, MessageFlags.KnownApplication)
		};
	}
}
