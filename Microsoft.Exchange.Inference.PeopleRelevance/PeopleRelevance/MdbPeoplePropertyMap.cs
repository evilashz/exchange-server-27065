using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.Mdb;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x0200000D RID: 13
	internal class MdbPeoplePropertyMap : MdbPropertyMap
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002E27 File Offset: 0x00001027
		public static MdbPeoplePropertyMap Instance
		{
			get
			{
				if (MdbPeoplePropertyMap.instance == null)
				{
					MdbPeoplePropertyMap.instance = new MdbPeoplePropertyMap();
				}
				return MdbPeoplePropertyMap.instance;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002E3F File Offset: 0x0000103F
		private static object GetRecipientsTo(IItem item, StorePropertyDefinition propertyDefinition, IMdbPropertyMappingContext context)
		{
			return MdbPeoplePropertyMap.GetRecipients(item, RecipientItemType.To);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002E48 File Offset: 0x00001048
		private static object GetRecipientsCc(IItem item, StorePropertyDefinition propertyDefinition, IMdbPropertyMappingContext context)
		{
			return MdbPeoplePropertyMap.GetRecipients(item, RecipientItemType.Cc);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002E54 File Offset: 0x00001054
		private static object GetRecipients(IItem item, RecipientItemType recipientType)
		{
			MessageItem messageItem = item as MessageItem;
			List<IMessageRecipient> list = null;
			if (messageItem != null)
			{
				list = new List<IMessageRecipient>(16);
				foreach (Recipient recipient in messageItem.Recipients)
				{
					if (recipient.RecipientItemType == recipientType)
					{
						if (!string.IsNullOrEmpty(recipient.Participant.GetValueOrDefault<string>(ParticipantSchema.SmtpAddress)))
						{
							list.Add(new MdbRecipient(recipient));
						}
						else
						{
							ExTraceGlobals.CoreDocumentModelTracer.TraceError<string, RecipientItemType, StoreObjectId>(0L, "Recipient {0} (type {1}) contains a bad email address in message {2}", recipient.Participant.DisplayName, recipientType, messageItem.StoreObjectId);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002F04 File Offset: 0x00001104
		private static object GetIsReply(PropertyDefinition genericPropertyDefinition, StorePropertyDefinition storePropertyDefinition, object underlyingPropertyValue)
		{
			if (underlyingPropertyValue == null || underlyingPropertyValue is PropertyError)
			{
				return false;
			}
			return true;
		}

		// Token: 0x04000028 RID: 40
		[PropertyMapping]
		public static readonly MdbPropertyMapping SentTime = new MdbOneToOneSimplePropertyMapping(PeopleRelevanceSchema.SentTime, ItemSchema.SentTime);

		// Token: 0x04000029 RID: 41
		[PropertyMapping]
		public static readonly MdbPropertyMapping RecipientsTo = new MdbOneToOneTransformPropertyMapping(PeopleRelevanceSchema.RecipientsTo, ItemSchema.DisplayTo, new MdbOneToOnePropertyMapping.ItemGetterDelegate(MdbPeoplePropertyMap.GetRecipientsTo), null);

		// Token: 0x0400002A RID: 42
		[PropertyMapping]
		public static readonly MdbPropertyMapping RecipientsCc = new MdbOneToOneTransformPropertyMapping(PeopleRelevanceSchema.RecipientsCc, ItemSchema.DisplayCc, new MdbOneToOnePropertyMapping.ItemGetterDelegate(MdbPeoplePropertyMap.GetRecipientsCc), null);

		// Token: 0x0400002B RID: 43
		[PropertyMapping]
		public static readonly MdbPropertyMapping IsReply = new MdbOneToOneTransformPropertyMapping(PeopleRelevanceSchema.IsReply, ItemSchema.InReplyTo, new MdbOneToOneTransformPropertyMapping.TransformDelegate(MdbPeoplePropertyMap.GetIsReply), null);

		// Token: 0x0400002C RID: 44
		private static MdbPeoplePropertyMap instance = null;
	}
}
