using System;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E58 RID: 3672
	internal class Message : Item
	{
		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x06005EB5 RID: 24245 RVA: 0x001276EA File Offset: 0x001258EA
		// (set) Token: 0x06005EB6 RID: 24246 RVA: 0x001276FC File Offset: 0x001258FC
		public string ParentFolderId
		{
			get
			{
				return (string)base[MessageSchema.ParentFolderId];
			}
			set
			{
				base[MessageSchema.ParentFolderId] = value;
			}
		}

		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x06005EB7 RID: 24247 RVA: 0x0012770A File Offset: 0x0012590A
		// (set) Token: 0x06005EB8 RID: 24248 RVA: 0x0012771C File Offset: 0x0012591C
		public string ConversationId
		{
			get
			{
				return (string)base[MessageSchema.ConversationId];
			}
			set
			{
				base[MessageSchema.ConversationId] = value;
			}
		}

		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x06005EB9 RID: 24249 RVA: 0x0012772A File Offset: 0x0012592A
		// (set) Token: 0x06005EBA RID: 24250 RVA: 0x0012773C File Offset: 0x0012593C
		public ItemBody UniqueBody
		{
			get
			{
				return (ItemBody)base[MessageSchema.UniqueBody];
			}
			set
			{
				base[MessageSchema.UniqueBody] = value;
			}
		}

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x06005EBB RID: 24251 RVA: 0x0012774A File Offset: 0x0012594A
		// (set) Token: 0x06005EBC RID: 24252 RVA: 0x0012775C File Offset: 0x0012595C
		public Recipient[] ToRecipients
		{
			get
			{
				return (Recipient[])base[MessageSchema.ToRecipients];
			}
			set
			{
				base[MessageSchema.ToRecipients] = value;
			}
		}

		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x06005EBD RID: 24253 RVA: 0x0012776A File Offset: 0x0012596A
		// (set) Token: 0x06005EBE RID: 24254 RVA: 0x0012777C File Offset: 0x0012597C
		public Recipient[] CcRecipients
		{
			get
			{
				return (Recipient[])base[MessageSchema.CcRecipients];
			}
			set
			{
				base[MessageSchema.CcRecipients] = value;
			}
		}

		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x06005EBF RID: 24255 RVA: 0x0012778A File Offset: 0x0012598A
		// (set) Token: 0x06005EC0 RID: 24256 RVA: 0x0012779C File Offset: 0x0012599C
		public Recipient[] BccRecipients
		{
			get
			{
				return (Recipient[])base[MessageSchema.BccRecipients];
			}
			set
			{
				base[MessageSchema.BccRecipients] = value;
			}
		}

		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x06005EC1 RID: 24257 RVA: 0x001277AA File Offset: 0x001259AA
		// (set) Token: 0x06005EC2 RID: 24258 RVA: 0x001277BC File Offset: 0x001259BC
		public Recipient[] ReplyTo
		{
			get
			{
				return (Recipient[])base[MessageSchema.ReplyTo];
			}
			set
			{
				base[MessageSchema.ReplyTo] = value;
			}
		}

		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x06005EC3 RID: 24259 RVA: 0x001277CA File Offset: 0x001259CA
		// (set) Token: 0x06005EC4 RID: 24260 RVA: 0x001277DC File Offset: 0x001259DC
		public Recipient Sender
		{
			get
			{
				return (Recipient)base[MessageSchema.Sender];
			}
			set
			{
				base[MessageSchema.Sender] = value;
			}
		}

		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x06005EC5 RID: 24261 RVA: 0x001277EA File Offset: 0x001259EA
		// (set) Token: 0x06005EC6 RID: 24262 RVA: 0x001277FC File Offset: 0x001259FC
		public Recipient From
		{
			get
			{
				return (Recipient)base[MessageSchema.From];
			}
			set
			{
				base[MessageSchema.From] = value;
			}
		}

		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x06005EC7 RID: 24263 RVA: 0x0012780A File Offset: 0x00125A0A
		// (set) Token: 0x06005EC8 RID: 24264 RVA: 0x0012781C File Offset: 0x00125A1C
		public bool IsDeliveryReceiptRequested
		{
			get
			{
				return (bool)base[MessageSchema.IsDeliveryReceiptRequested];
			}
			set
			{
				base[MessageSchema.IsDeliveryReceiptRequested] = value;
			}
		}

		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x06005EC9 RID: 24265 RVA: 0x0012782F File Offset: 0x00125A2F
		// (set) Token: 0x06005ECA RID: 24266 RVA: 0x00127841 File Offset: 0x00125A41
		public bool IsReadReceiptRequested
		{
			get
			{
				return (bool)base[MessageSchema.IsReadReceiptRequested];
			}
			set
			{
				base[MessageSchema.IsReadReceiptRequested] = value;
			}
		}

		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x06005ECB RID: 24267 RVA: 0x00127854 File Offset: 0x00125A54
		// (set) Token: 0x06005ECC RID: 24268 RVA: 0x00127866 File Offset: 0x00125A66
		public bool IsRead
		{
			get
			{
				return (bool)base[MessageSchema.IsRead];
			}
			set
			{
				base[MessageSchema.IsRead] = value;
			}
		}

		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x06005ECD RID: 24269 RVA: 0x00127879 File Offset: 0x00125A79
		// (set) Token: 0x06005ECE RID: 24270 RVA: 0x0012788B File Offset: 0x00125A8B
		public bool IsDraft
		{
			get
			{
				return (bool)base[MessageSchema.IsDraft];
			}
			set
			{
				base[MessageSchema.IsDraft] = value;
			}
		}

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x06005ECF RID: 24271 RVA: 0x0012789E File Offset: 0x00125A9E
		// (set) Token: 0x06005ED0 RID: 24272 RVA: 0x001278B0 File Offset: 0x00125AB0
		public DateTimeOffset DateTimeReceived
		{
			get
			{
				return (DateTimeOffset)base[MessageSchema.DateTimeReceived];
			}
			set
			{
				base[MessageSchema.DateTimeReceived] = value;
			}
		}

		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x06005ED1 RID: 24273 RVA: 0x001278C3 File Offset: 0x00125AC3
		// (set) Token: 0x06005ED2 RID: 24274 RVA: 0x001278D5 File Offset: 0x00125AD5
		public DateTimeOffset DateTimeSent
		{
			get
			{
				return (DateTimeOffset)base[MessageSchema.DateTimeSent];
			}
			set
			{
				base[MessageSchema.DateTimeSent] = value;
			}
		}

		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x06005ED3 RID: 24275 RVA: 0x001278E8 File Offset: 0x00125AE8
		// (set) Token: 0x06005ED4 RID: 24276 RVA: 0x001278FA File Offset: 0x00125AFA
		public string EventId
		{
			get
			{
				return (string)base[MessageSchema.EventId];
			}
			set
			{
				base[MessageSchema.EventId] = value;
			}
		}

		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x06005ED5 RID: 24277 RVA: 0x00127908 File Offset: 0x00125B08
		// (set) Token: 0x06005ED6 RID: 24278 RVA: 0x0012791A File Offset: 0x00125B1A
		public MeetingMessageType MeetingMessageType
		{
			get
			{
				return (MeetingMessageType)base[MessageSchema.MeetingMessageType];
			}
			set
			{
				base[MessageSchema.MeetingMessageType] = value;
			}
		}

		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x06005ED7 RID: 24279 RVA: 0x0012792D File Offset: 0x00125B2D
		internal override EntitySchema Schema
		{
			get
			{
				return MessageSchema.SchemaInstance;
			}
		}

		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x06005ED8 RID: 24280 RVA: 0x00127934 File Offset: 0x00125B34
		protected override string UserRootNavigationName
		{
			get
			{
				return UserSchema.Messages.Name;
			}
		}

		// Token: 0x04003351 RID: 13137
		internal new static readonly EdmEntityType EdmEntityType = new EdmEntityType(typeof(Message).Namespace, typeof(Message).Name, Microsoft.Exchange.Services.OData.Model.Item.EdmEntityType);
	}
}
