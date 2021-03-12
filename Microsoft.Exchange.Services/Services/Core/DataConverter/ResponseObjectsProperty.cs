using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000172 RID: 370
	internal sealed class ResponseObjectsProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x06000A9B RID: 2715 RVA: 0x000336E6 File Offset: 0x000318E6
		public ResponseObjectsProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x000336EF File Offset: 0x000318EF
		public static ResponseObjectsProperty CreateCommand(CommandContext commandContext)
		{
			return new ResponseObjectsProperty(commandContext);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x000336F7 File Offset: 0x000318F7
		public void ToXml()
		{
			throw new InvalidOperationException("ResponseObjectsProperty.ToXml should not be called.");
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x00033703 File Offset: 0x00031903
		public override bool ToServiceObjectRequiresMailboxAccess
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00033708 File Offset: 0x00031908
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			this.serviceObject = commandSettings.ServiceObject;
			this.responseObjects = new List<ResponseObjectType>();
			CalendarItemBase calendarItemBase = null;
			MeetingMessage meetingMessage = null;
			MessageItem messageItem = null;
			PostItem postItem = null;
			if (XsoDataConverter.TryGetStoreObject<CalendarItemBase>(storeObject, out calendarItemBase))
			{
				this.CreateCalendarItemBaseResponseObjects(calendarItemBase);
			}
			else if (XsoDataConverter.TryGetStoreObject<MeetingMessage>(storeObject, out meetingMessage))
			{
				this.CreateMeetingMessageResponseObjects(meetingMessage);
			}
			else if (XsoDataConverter.TryGetStoreObject<MessageItem>(storeObject, out messageItem))
			{
				this.CreateMessageResponseObjects(messageItem);
			}
			else if (XsoDataConverter.TryGetStoreObject<PostItem>(storeObject, out postItem) && ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1))
			{
				this.CreatePostItemResponseObjects();
			}
			if (this.responseObjects.Count > 0)
			{
				this.serviceObject[this.commandContext.PropertyInformation] = this.responseObjects.ToArray();
			}
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x000337D0 File Offset: 0x000319D0
		private static bool IsMessageInJunkEmailFolder(MessageItem messageItem)
		{
			StoreObjectId parentId;
			try
			{
				parentId = messageItem.ParentId;
			}
			catch (InvalidOperationException)
			{
				return false;
			}
			MailboxSession mailboxSession = messageItem.Session as MailboxSession;
			return mailboxSession != null && mailboxSession.IsDefaultFolderType(parentId) == DefaultFolderType.JunkEmail;
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00033818 File Offset: 0x00031A18
		private void CreateMessageResponseObjects(MessageItem messageItem)
		{
			if ((!Shape.IsGenericMessageOnly(messageItem) || ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1)) && !messageItem.ClassName.StartsWith("rpmsg.message", StringComparison.OrdinalIgnoreCase))
			{
				this.CreateForwardReplyResponseObjects(messageItem);
				this.CreateSuppressReadReceiptResponseObject(messageItem);
			}
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00033854 File Offset: 0x00031A54
		private void CreateForwardReplyResponseObjects(MessageItem messageItem)
		{
			if (!messageItem.IsDraft && messageItem.IsReplyAllowed)
			{
				this.responseObjects.Add(new ReplyToItemType());
				this.responseObjects.Add(new ReplyAllToItemType());
			}
			this.responseObjects.Add(new ForwardItemType());
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000338A1 File Offset: 0x00031AA1
		private void CreateSuppressReadReceiptResponseObject(MessageItem messageItem)
		{
			if ((bool)messageItem[this.propertyDefinitions[0]] && messageItem.Session is MailboxSession && !ResponseObjectsProperty.IsMessageInJunkEmailFolder(messageItem))
			{
				this.responseObjects.Add(new SuppressReadReceiptType());
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000338E0 File Offset: 0x00031AE0
		private void CreateCalendarItemBaseResponseObjects(CalendarItemBase calendarItemBase)
		{
			if (calendarItemBase.IsOrganizer())
			{
				if (calendarItemBase.IsMeeting && calendarItemBase.Session is MailboxSession)
				{
					this.responseObjects.Add(new CancelCalendarItemType());
				}
			}
			else
			{
				MailboxSession mailboxSession = calendarItemBase.Session as MailboxSession;
				bool flag = mailboxSession != null && mailboxSession.IsGroupMailbox();
				if (flag)
				{
					if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012))
					{
						this.CreateGroupMailboxResponseObjects(calendarItemBase.IsCancelled);
					}
				}
				else
				{
					if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012) || !calendarItemBase.IsCancelled)
					{
						this.CreateAcceptTentativeDeclineResponseObjects(mailboxSession);
					}
					this.CreateProposeNewTimeResponseObject(calendarItemBase);
				}
			}
			if ((ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012) && calendarItemBase.IsMeeting) || !calendarItemBase.IsOrganizer())
			{
				this.responseObjects.Add(new ReplyToItemType());
				this.responseObjects.Add(new ReplyAllToItemType());
			}
			this.responseObjects.Add(new ForwardItemType());
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000339D4 File Offset: 0x00031BD4
		private void CreateMeetingMessageResponseObjects(MeetingMessage meetingMessage)
		{
			MailboxSession mailboxSession = meetingMessage.Session as MailboxSession;
			bool flag = mailboxSession != null && mailboxSession.IsGroupMailbox();
			if (meetingMessage is MeetingCancellation && meetingMessage.Session is MailboxSession)
			{
				if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012) || !meetingMessage.IsOrganizer())
				{
					this.responseObjects.Add(new RemoveItemType());
				}
			}
			else if (meetingMessage is MeetingRequest)
			{
				if (flag)
				{
					if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012))
					{
						this.CreateGroupMailboxResponseObjects(meetingMessage.IsOutOfDate());
					}
				}
				else
				{
					if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012) || (!meetingMessage.IsOrganizer() && !meetingMessage.IsOutOfDate()))
					{
						this.CreateAcceptTentativeDeclineResponseObjects(mailboxSession);
					}
					this.CreateProposeNewTimeResponseObject((MeetingRequest)meetingMessage);
				}
			}
			this.CreateForwardReplyResponseObjects(meetingMessage);
			if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012) || !flag)
			{
				this.CreateSuppressReadReceiptResponseObject(meetingMessage);
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00033ABA File Offset: 0x00031CBA
		private void CreateGroupMailboxResponseObjects(bool isMeetingOutOfDateOrCancelled)
		{
			if (!isMeetingOutOfDateOrCancelled)
			{
				this.responseObjects.Add(new AddItemToMyCalendarType());
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00033ACF File Offset: 0x00031CCF
		private void CreateAcceptTentativeDeclineResponseObjects(MailboxSession mailboxSession)
		{
			if (mailboxSession != null)
			{
				this.responseObjects.Add(new AcceptItemType());
				this.responseObjects.Add(new TentativelyAcceptItemType());
				this.responseObjects.Add(new DeclineItemType());
			}
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00033B04 File Offset: 0x00031D04
		private void CreateProposeNewTimeResponseObject(MeetingRequest meetingRequest)
		{
			if (meetingRequest != null && meetingRequest.Session is MailboxSession && ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2013_SP1) && !meetingRequest.IsOrganizer() && !meetingRequest.IsOutOfDate() && meetingRequest.AllowNewTimeProposal && !meetingRequest.IsRecurringMaster)
			{
				this.responseObjects.Add(new ProposeNewTimeType());
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00033B64 File Offset: 0x00031D64
		private void CreateProposeNewTimeResponseObject(CalendarItemBase calendarItem)
		{
			if (calendarItem != null && calendarItem.Session is MailboxSession && ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2013_SP1) && !calendarItem.IsOrganizer() && !calendarItem.IsCancelled && calendarItem.AllowNewTimeProposal && calendarItem.CalendarItemType != CalendarItemType.RecurringMaster)
			{
				this.responseObjects.Add(new ProposeNewTimeType());
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00033BC4 File Offset: 0x00031DC4
		private void CreatePostItemResponseObjects()
		{
			this.responseObjects.Add(new PostReplyItemType());
			this.responseObjects.Add(new ReplyToItemType());
			this.responseObjects.Add(new ReplyAllToItemType());
			this.responseObjects.Add(new ForwardItemType());
		}

		// Token: 0x040007CE RID: 1998
		private ServiceObject serviceObject;

		// Token: 0x040007CF RID: 1999
		private List<ResponseObjectType> responseObjects;
	}
}
