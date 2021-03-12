using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000040 RID: 64
	internal class PropertyIndexHolder
	{
		// Token: 0x0600024E RID: 590 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
		internal PropertyIndexHolder(PropertyDefinition[] propColumns)
		{
			this.idIndex = Array.IndexOf<PropertyDefinition>(propColumns, ItemSchema.Id);
			this.itemClassIndex = Array.IndexOf<PropertyDefinition>(propColumns, StoreObjectSchema.ItemClass);
			this.receivedTimeIndex = Array.IndexOf<PropertyDefinition>(propColumns, ItemSchema.ReceivedTime);
			this.moveDateIndex = Array.IndexOf<PropertyDefinition>(propColumns, ItemSchema.ElcMoveDate);
			this.expiryTimeIndex = Array.IndexOf<PropertyDefinition>(propColumns, MessageItemSchema.ExpiryTime);
			this.creationTimeIndex = Array.IndexOf<PropertyDefinition>(propColumns, StoreObjectSchema.CreationTime);
			this.parentItemIdIndex = Array.IndexOf<PropertyDefinition>(propColumns, StoreObjectSchema.ParentItemId);
			this.lastModifiedTimeIndex = Array.IndexOf<PropertyDefinition>(propColumns, StoreObjectSchema.LastModifiedTime);
			this.calendarTypeIndex = Array.IndexOf<PropertyDefinition>(propColumns, CalendarItemBaseSchema.CalendarItemType);
			this.endDateIndex = Array.IndexOf<PropertyDefinition>(propColumns, CalendarItemInstanceSchema.EndTime);
			this.isTaskRecurringIndex = Array.IndexOf<PropertyDefinition>(propColumns, TaskSchema.IsTaskRecurring);
			this.messageSubjectIndex = Array.IndexOf<PropertyDefinition>(propColumns, ItemSchema.Subject);
			this.messageSentRepresentingIndex = Array.IndexOf<PropertyDefinition>(propColumns, ItemSchema.SentRepresentingEmailAddress);
			this.messageSenderIndex = Array.IndexOf<PropertyDefinition>(propColumns, MessageItemSchema.SenderEmailAddress);
			this.messageSenderDisplayNameIndex = Array.IndexOf<PropertyDefinition>(propColumns, MessageItemSchema.SenderDisplayName);
			this.messageInternetIdIndex = Array.IndexOf<PropertyDefinition>(propColumns, ItemSchema.InternetMessageId);
			this.parentDisplayNameIndex = Array.IndexOf<PropertyDefinition>(propColumns, ItemSchema.ParentDisplayName);
			this.messageToMeIndex = Array.IndexOf<PropertyDefinition>(propColumns, MessageItemSchema.MessageToMe);
			this.messageCcMeIndex = Array.IndexOf<PropertyDefinition>(propColumns, MessageItemSchema.MessageCcMe);
			this.conversationTopicIndex = Array.IndexOf<PropertyDefinition>(propColumns, ItemSchema.ConversationTopic);
			this.conversationIdIndex = Array.IndexOf<PropertyDefinition>(propColumns, ItemSchema.ConversationId);
			this.autoCopiedIndex = Array.IndexOf<PropertyDefinition>(propColumns, ItemSchema.ElcAutoCopyTag);
			this.policyTagIndex = Array.IndexOf<PropertyDefinition>(propColumns, StoreObjectSchema.PolicyTag);
			this.retentionPeriodIndex = Array.IndexOf<PropertyDefinition>(propColumns, StoreObjectSchema.RetentionPeriod);
			this.startDateEtcIndex = Array.IndexOf<PropertyDefinition>(propColumns, ItemSchema.StartDateEtc);
			this.retentionDateIndex = Array.IndexOf<PropertyDefinition>(propColumns, ItemSchema.RetentionDate);
			this.retentionFlagsIndex = Array.IndexOf<PropertyDefinition>(propColumns, StoreObjectSchema.RetentionFlags);
			this.archiveTagIndex = Array.IndexOf<PropertyDefinition>(propColumns, StoreObjectSchema.ArchiveTag);
			this.archiveDateIndex = Array.IndexOf<PropertyDefinition>(propColumns, ItemSchema.ArchiveDate);
			this.archivePeriodIndex = Array.IndexOf<PropertyDefinition>(propColumns, StoreObjectSchema.ArchivePeriod);
			this.sizeIndex = Array.IndexOf<PropertyDefinition>(propColumns, ItemSchema.Size);
			this.explicitPolicyTagIndex = Array.IndexOf<PropertyDefinition>(propColumns, StoreObjectSchema.ExplicitPolicyTag);
			this.explicitArchiveTagIndex = Array.IndexOf<PropertyDefinition>(propColumns, StoreObjectSchema.ExplicitArchiveTag);
			this.ehaMigrationExpiryDateIndex = Array.IndexOf<PropertyDefinition>(propColumns, ItemSchema.EHAMigrationExpiryDate);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000E908 File Offset: 0x0000CB08
		internal PropertyIndexHolder(StoreObject storeObject, out object[] properties)
		{
			properties = new object[PropertyIndexHolder.tagItemProperties.Length];
			int num = 0;
			while (num != PropertyIndexHolder.tagItemProperties.Length)
			{
				if (PropertyIndexHolder.tagItemProperties[num] != null)
				{
					try
					{
						properties[num] = storeObject.TryGetProperty(PropertyIndexHolder.tagItemProperties[num]);
						goto IL_6F;
					}
					catch (NotInBagPropertyErrorException)
					{
						PropertyIndexHolder.Tracer.TraceDebug<string, VersionedId, string>((long)this.GetHashCode(), "Unable to load property {0} for {1} of type {2}.", PropertyIndexHolder.tagItemProperties[num].Name, storeObject.Id, storeObject.ClassName);
						properties[num] = null;
						goto IL_6F;
					}
					goto IL_6A;
				}
				goto IL_6A;
				IL_6F:
				num++;
				continue;
				IL_6A:
				properties[num] = null;
				goto IL_6F;
			}
			this.idIndex = 0;
			this.itemClassIndex = 1;
			this.receivedTimeIndex = 2;
			this.moveDateIndex = 3;
			this.expiryTimeIndex = 4;
			this.creationTimeIndex = 5;
			this.calendarTypeIndex = 6;
			this.endDateIndex = 7;
			this.isTaskRecurringIndex = 8;
			this.messageSubjectIndex = 9;
			this.messageSentRepresentingIndex = 10;
			this.messageSenderIndex = 11;
			this.messageInternetIdIndex = 12;
			this.autoCopiedIndex = 13;
			this.policyTagIndex = 14;
			this.retentionPeriodIndex = 15;
			this.startDateEtcIndex = 16;
			this.retentionDateIndex = 17;
			this.retentionFlagsIndex = 18;
			this.archiveTagIndex = 19;
			this.archiveDateIndex = 20;
			this.archivePeriodIndex = 21;
			this.sizeIndex = 22;
			this.explicitPolicyTagIndex = 23;
			this.explicitArchiveTagIndex = 24;
			this.messageSenderDisplayNameIndex = 25;
			this.lastModifiedTimeIndex = 26;
			this.parentDisplayNameIndex = 27;
			this.messageToMeIndex = 28;
			this.messageCcMeIndex = 29;
			this.conversationTopicIndex = 30;
			this.conversationIdIndex = 31;
			this.ehaMigrationExpiryDateIndex = 32;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
		internal int IdIndex
		{
			get
			{
				return this.idIndex;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000EAAC File Offset: 0x0000CCAC
		internal int ItemClassIndex
		{
			get
			{
				return this.itemClassIndex;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000EAB4 File Offset: 0x0000CCB4
		internal int ReceivedTimeIndex
		{
			get
			{
				return this.receivedTimeIndex;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000EABC File Offset: 0x0000CCBC
		internal int MoveDateIndex
		{
			get
			{
				return this.moveDateIndex;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000EAC4 File Offset: 0x0000CCC4
		internal int ExpiryTimeIndex
		{
			get
			{
				return this.expiryTimeIndex;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000EACC File Offset: 0x0000CCCC
		internal int CreationTimeIndex
		{
			get
			{
				return this.creationTimeIndex;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000EAD4 File Offset: 0x0000CCD4
		internal int ParentItemIdIndex
		{
			get
			{
				return this.parentItemIdIndex;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000EADC File Offset: 0x0000CCDC
		internal int CalendarTypeIndex
		{
			get
			{
				return this.calendarTypeIndex;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000EAE4 File Offset: 0x0000CCE4
		internal int IsTaskRecurringIndex
		{
			get
			{
				return this.isTaskRecurringIndex;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		internal int EndDateIndex
		{
			get
			{
				return this.endDateIndex;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000EAF4 File Offset: 0x0000CCF4
		internal int MessageSubjectIndex
		{
			get
			{
				return this.messageSubjectIndex;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000EAFC File Offset: 0x0000CCFC
		internal int MessageSentRepresentingIndex
		{
			get
			{
				return this.messageSentRepresentingIndex;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000EB04 File Offset: 0x0000CD04
		internal int MessageSenderIndex
		{
			get
			{
				return this.messageSenderIndex;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000EB0C File Offset: 0x0000CD0C
		internal int MessageSenderDisplayName
		{
			get
			{
				return this.messageSenderDisplayNameIndex;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000EB14 File Offset: 0x0000CD14
		internal int MessageInternetIdIndex
		{
			get
			{
				return this.messageInternetIdIndex;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000EB1C File Offset: 0x0000CD1C
		internal int AutoCopiedIndex
		{
			get
			{
				return this.autoCopiedIndex;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000EB24 File Offset: 0x0000CD24
		internal int PolicyTagIndex
		{
			get
			{
				return this.policyTagIndex;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000EB2C File Offset: 0x0000CD2C
		internal int RetentionPeriodIndex
		{
			get
			{
				return this.retentionPeriodIndex;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000EB34 File Offset: 0x0000CD34
		internal int StartDateEtcIndex
		{
			get
			{
				return this.startDateEtcIndex;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000EB3C File Offset: 0x0000CD3C
		internal int RetentionDateIndex
		{
			get
			{
				return this.retentionDateIndex;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000EB44 File Offset: 0x0000CD44
		internal int RetentionFlagsIndex
		{
			get
			{
				return this.retentionFlagsIndex;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000EB4C File Offset: 0x0000CD4C
		internal int ArchiveTagIndex
		{
			get
			{
				return this.archiveTagIndex;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000EB54 File Offset: 0x0000CD54
		internal int ArchiveDateIndex
		{
			get
			{
				return this.archiveDateIndex;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000EB5C File Offset: 0x0000CD5C
		internal int ArchivePeriodIndex
		{
			get
			{
				return this.archivePeriodIndex;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000EB64 File Offset: 0x0000CD64
		internal int SizeIndex
		{
			get
			{
				return this.sizeIndex;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000EB6C File Offset: 0x0000CD6C
		internal int ExplicitPolicyTagIndex
		{
			get
			{
				return this.explicitPolicyTagIndex;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000EB74 File Offset: 0x0000CD74
		internal int ExplicitArchiveTagIndex
		{
			get
			{
				return this.explicitArchiveTagIndex;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000EB7C File Offset: 0x0000CD7C
		internal int LastModifiedTime
		{
			get
			{
				return this.lastModifiedTimeIndex;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000EB84 File Offset: 0x0000CD84
		internal int ParentDisplayName
		{
			get
			{
				return this.parentDisplayNameIndex;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000EB8C File Offset: 0x0000CD8C
		internal int MessageToMe
		{
			get
			{
				return this.messageToMeIndex;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000EB94 File Offset: 0x0000CD94
		internal int MessageCcMe
		{
			get
			{
				return this.messageCcMeIndex;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000EB9C File Offset: 0x0000CD9C
		internal int ConversationTopic
		{
			get
			{
				return this.conversationTopicIndex;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000EBA4 File Offset: 0x0000CDA4
		internal int ConversationId
		{
			get
			{
				return this.conversationIdIndex;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000EBAC File Offset: 0x0000CDAC
		internal int EHAMigrationExpiryDateIndex
		{
			get
			{
				return this.ehaMigrationExpiryDateIndex;
			}
		}

		// Token: 0x040001D9 RID: 473
		private static readonly Trace Tracer = ExTraceGlobals.ELCAssistantTracer;

		// Token: 0x040001DA RID: 474
		private static PropertyDefinition[] tagItemProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			ItemSchema.ReceivedTime,
			null,
			null,
			StoreObjectSchema.CreationTime,
			CalendarItemBaseSchema.CalendarItemType,
			CalendarItemInstanceSchema.EndTime,
			TaskSchema.IsTaskRecurring,
			ItemSchema.Subject,
			ItemSchema.SentRepresentingEmailAddress,
			MessageItemSchema.SenderEmailAddress,
			ItemSchema.InternetMessageId,
			ItemSchema.ElcAutoCopyTag,
			StoreObjectSchema.PolicyTag,
			StoreObjectSchema.RetentionPeriod,
			ItemSchema.StartDateEtc,
			ItemSchema.RetentionDate,
			StoreObjectSchema.RetentionFlags,
			StoreObjectSchema.ArchiveTag,
			ItemSchema.ArchiveDate,
			StoreObjectSchema.ArchivePeriod,
			ItemSchema.Size,
			StoreObjectSchema.ExplicitPolicyTag,
			StoreObjectSchema.ExplicitArchiveTag,
			MessageItemSchema.SenderDisplayName,
			StoreObjectSchema.LastModifiedTime,
			ItemSchema.ParentDisplayName,
			MessageItemSchema.MessageToMe,
			MessageItemSchema.MessageCcMe,
			ItemSchema.ConversationTopic,
			ItemSchema.ConversationId,
			ItemSchema.EHAMigrationExpiryDate
		};

		// Token: 0x040001DB RID: 475
		private readonly int idIndex;

		// Token: 0x040001DC RID: 476
		private readonly int itemClassIndex;

		// Token: 0x040001DD RID: 477
		private readonly int receivedTimeIndex;

		// Token: 0x040001DE RID: 478
		private readonly int moveDateIndex;

		// Token: 0x040001DF RID: 479
		private readonly int expiryTimeIndex;

		// Token: 0x040001E0 RID: 480
		private readonly int creationTimeIndex;

		// Token: 0x040001E1 RID: 481
		private readonly int parentItemIdIndex;

		// Token: 0x040001E2 RID: 482
		private readonly int calendarTypeIndex;

		// Token: 0x040001E3 RID: 483
		private readonly int isTaskRecurringIndex;

		// Token: 0x040001E4 RID: 484
		private readonly int endDateIndex;

		// Token: 0x040001E5 RID: 485
		private readonly int messageSubjectIndex;

		// Token: 0x040001E6 RID: 486
		private readonly int messageSentRepresentingIndex;

		// Token: 0x040001E7 RID: 487
		private readonly int messageSenderIndex;

		// Token: 0x040001E8 RID: 488
		private readonly int messageSenderDisplayNameIndex;

		// Token: 0x040001E9 RID: 489
		private readonly int messageInternetIdIndex;

		// Token: 0x040001EA RID: 490
		private readonly int autoCopiedIndex;

		// Token: 0x040001EB RID: 491
		private readonly int policyTagIndex;

		// Token: 0x040001EC RID: 492
		private readonly int retentionPeriodIndex;

		// Token: 0x040001ED RID: 493
		private readonly int startDateEtcIndex;

		// Token: 0x040001EE RID: 494
		private readonly int retentionDateIndex;

		// Token: 0x040001EF RID: 495
		private readonly int retentionFlagsIndex;

		// Token: 0x040001F0 RID: 496
		private readonly int archiveTagIndex;

		// Token: 0x040001F1 RID: 497
		private readonly int archiveDateIndex;

		// Token: 0x040001F2 RID: 498
		private readonly int archivePeriodIndex;

		// Token: 0x040001F3 RID: 499
		private readonly int sizeIndex;

		// Token: 0x040001F4 RID: 500
		private readonly int explicitPolicyTagIndex;

		// Token: 0x040001F5 RID: 501
		private readonly int explicitArchiveTagIndex;

		// Token: 0x040001F6 RID: 502
		private readonly int ehaMigrationExpiryDateIndex;

		// Token: 0x040001F7 RID: 503
		private readonly int lastModifiedTimeIndex;

		// Token: 0x040001F8 RID: 504
		private readonly int parentDisplayNameIndex;

		// Token: 0x040001F9 RID: 505
		private readonly int messageToMeIndex;

		// Token: 0x040001FA RID: 506
		private readonly int messageCcMeIndex;

		// Token: 0x040001FB RID: 507
		private readonly int conversationTopicIndex;

		// Token: 0x040001FC RID: 508
		private readonly int conversationIdIndex;
	}
}
