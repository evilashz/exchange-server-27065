﻿using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Search.Common
{
	// Token: 0x02000D06 RID: 3334
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Globals
	{
		// Token: 0x060072CA RID: 29386 RVA: 0x001FC72C File Offset: 0x001FA92C
		static Globals()
		{
			Globals.propertyKeywordToDefinitionMap = new Dictionary<PropertyKeyword, PropertyDefinition[]>
			{
				{
					PropertyKeyword.From,
					new PropertyDefinition[]
					{
						ItemSchema.SearchSender
					}
				},
				{
					PropertyKeyword.To,
					new PropertyDefinition[]
					{
						ItemSchema.SearchRecipientsTo
					}
				},
				{
					PropertyKeyword.Bcc,
					new PropertyDefinition[]
					{
						ItemSchema.SearchRecipientsBcc
					}
				},
				{
					PropertyKeyword.Cc,
					new PropertyDefinition[]
					{
						ItemSchema.SearchRecipientsCc
					}
				},
				{
					PropertyKeyword.Recipients,
					new PropertyDefinition[]
					{
						ItemSchema.SearchRecipients
					}
				},
				{
					PropertyKeyword.Participants,
					new PropertyDefinition[]
					{
						ItemSchema.SearchRecipients,
						ItemSchema.SearchSender
					}
				},
				{
					PropertyKeyword.Subject,
					new PropertyDefinition[]
					{
						ItemSchema.Subject
					}
				},
				{
					PropertyKeyword.Body,
					new PropertyDefinition[]
					{
						ItemSchema.TextBody
					}
				},
				{
					PropertyKeyword.Sent,
					new PropertyDefinition[]
					{
						ItemSchema.SentTime
					}
				},
				{
					PropertyKeyword.Received,
					new PropertyDefinition[]
					{
						ItemSchema.ReceivedTime
					}
				},
				{
					PropertyKeyword.Attachment,
					new PropertyDefinition[]
					{
						ItemSchema.AttachmentContent
					}
				},
				{
					PropertyKeyword.Kind,
					new PropertyDefinition[]
					{
						StoreObjectSchema.ItemClass
					}
				},
				{
					PropertyKeyword.PolicyTag,
					new PropertyDefinition[]
					{
						StoreObjectSchema.PolicyTag
					}
				},
				{
					PropertyKeyword.Expires,
					new PropertyDefinition[]
					{
						ItemSchema.RetentionDate
					}
				},
				{
					PropertyKeyword.IsFlagged,
					new PropertyDefinition[]
					{
						ItemSchema.FlagStatus
					}
				},
				{
					PropertyKeyword.IsRead,
					new PropertyDefinition[]
					{
						MessageItemSchema.IsRead
					}
				},
				{
					PropertyKeyword.Category,
					new PropertyDefinition[]
					{
						ItemSchema.Categories
					}
				},
				{
					PropertyKeyword.Importance,
					new PropertyDefinition[]
					{
						ItemSchema.Importance
					}
				},
				{
					PropertyKeyword.Size,
					new PropertyDefinition[]
					{
						ItemSchema.Size
					}
				},
				{
					PropertyKeyword.HasAttachment,
					new PropertyDefinition[]
					{
						ItemSchema.HasAttachment
					}
				},
				{
					PropertyKeyword.AttachmentNames,
					new PropertyDefinition[]
					{
						AttachmentSchema.AttachLongFileName
					}
				},
				{
					PropertyKeyword.All,
					new PropertyDefinition[]
					{
						ItemSchema.SearchAllIndexedProps
					}
				}
			};
			Globals.kindItemClasses = new Dictionary<KindKeyword, string[]>
			{
				{
					KindKeyword.email,
					new string[]
					{
						"IPM.Note"
					}
				},
				{
					KindKeyword.meetings,
					new string[]
					{
						"IPM.Schedule",
						"IPM.Appointment"
					}
				},
				{
					KindKeyword.tasks,
					new string[]
					{
						"IPM.Task"
					}
				},
				{
					KindKeyword.notes,
					new string[]
					{
						"IPM.StickyNote"
					}
				},
				{
					KindKeyword.docs,
					new string[]
					{
						"IPM.Document"
					}
				},
				{
					KindKeyword.journals,
					new string[]
					{
						"IPM.Activity"
					}
				},
				{
					KindKeyword.contacts,
					new string[]
					{
						"IPM.Contact"
					}
				},
				{
					KindKeyword.im,
					new string[]
					{
						"IPM.Note.Microsoft.Conversation",
						"IPM.Note.Microsoft.Missed",
						"IPM.Note.Microsoft.Conversation.Voice",
						"IPM.Note.Microsoft.Missed.Voice"
					}
				},
				{
					KindKeyword.voicemail,
					new string[]
					{
						"IPM.Note.Microsoft.Voicemail"
					}
				},
				{
					KindKeyword.faxes,
					new string[]
					{
						"IPM.Note.Microsoft.Fax"
					}
				},
				{
					KindKeyword.posts,
					new string[]
					{
						"IPM.Post"
					}
				},
				{
					KindKeyword.rssfeeds,
					new string[]
					{
						"IPM.Post.RSS"
					}
				}
			};
		}

		// Token: 0x17001E98 RID: 7832
		// (get) Token: 0x060072CB RID: 29387 RVA: 0x001FCB2C File Offset: 0x001FAD2C
		public static Dictionary<PropertyKeyword, PropertyDefinition[]> PropertyKeywordToDefinitionMap
		{
			get
			{
				return Globals.propertyKeywordToDefinitionMap;
			}
		}

		// Token: 0x17001E99 RID: 7833
		// (get) Token: 0x060072CC RID: 29388 RVA: 0x001FCB33 File Offset: 0x001FAD33
		public static IReadOnlyDictionary<KindKeyword, string[]> KindKeywordToClassMap
		{
			get
			{
				return Globals.kindItemClasses;
			}
		}

		// Token: 0x17001E9A RID: 7834
		// (get) Token: 0x060072CD RID: 29389 RVA: 0x001FCB3A File Offset: 0x001FAD3A
		public static PropertyDefinition[] AttachmentSubMapping
		{
			get
			{
				return Globals.attachmentSubMapping;
			}
		}

		// Token: 0x0400502B RID: 20523
		private static Dictionary<PropertyKeyword, PropertyDefinition[]> propertyKeywordToDefinitionMap;

		// Token: 0x0400502C RID: 20524
		private static Dictionary<KindKeyword, string[]> kindItemClasses;

		// Token: 0x0400502D RID: 20525
		private static PropertyDefinition[] attachmentSubMapping = new PropertyDefinition[]
		{
			AttachmentSchema.AttachFileName,
			AttachmentSchema.AttachLongFileName,
			AttachmentSchema.AttachExtension,
			AttachmentSchema.DisplayName
		};
	}
}
