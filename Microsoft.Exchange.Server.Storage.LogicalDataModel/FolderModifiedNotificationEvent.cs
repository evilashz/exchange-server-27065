using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000082 RID: 130
	public class FolderModifiedNotificationEvent : ObjectCreatedModifiedNotificationEvent
	{
		// Token: 0x060008CF RID: 2255 RVA: 0x0004C468 File Offset: 0x0004A668
		public FolderModifiedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExtendedEventFlags extendedEventFlags, ExchangeId fid, ExchangeId parentFid, StorePropTag[] changedPropTags, string containerClass, int messageCount, int unreadMessageCount) : base(database, mailboxNumber, EventType.ObjectModified, userIdentity, clientType, eventFlags, extendedEventFlags, fid, ExchangeId.Null, parentFid, null, null, changedPropTags, containerClass, null)
		{
			Statistics.NotificationTypes.FolderModified.Bump();
			this.messageCount = messageCount;
			this.unreadMessageCount = unreadMessageCount;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0004C4C8 File Offset: 0x0004A6C8
		public int MessageCount
		{
			get
			{
				return this.messageCount;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x0004C4D0 File Offset: 0x0004A6D0
		public int UnreadMessageCount
		{
			get
			{
				return this.unreadMessageCount;
			}
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0004C4D8 File Offset: 0x0004A6D8
		public override NotificationEvent.RedundancyStatus GetRedundancyStatus(NotificationEvent oldNev)
		{
			ObjectNotificationEvent objectNotificationEvent = oldNev as ObjectNotificationEvent;
			if (objectNotificationEvent != null)
			{
				if (base.IsSameObject(objectNotificationEvent))
				{
					if (objectNotificationEvent.EventType == EventType.ObjectModified)
					{
						FolderModifiedNotificationEvent folderModifiedNotificationEvent = oldNev as FolderModifiedNotificationEvent;
						if (ObjectCreatedModifiedNotificationEvent.PropTagArraysEqual(folderModifiedNotificationEvent.ChangedPropTags, base.ChangedPropTags))
						{
							return NotificationEvent.RedundancyStatus.ReplaceOldAndStop;
						}
						return NotificationEvent.RedundancyStatus.MergeReplaceOldAndStop;
					}
					else
					{
						if (objectNotificationEvent.EventType == EventType.ObjectCreated)
						{
							return NotificationEvent.RedundancyStatus.DropNewAndStop;
						}
						return NotificationEvent.RedundancyStatus.FlagStopSearch;
					}
				}
				else if (objectNotificationEvent.EventType == EventType.ObjectCopied)
				{
					FolderCopiedNotificationEvent folderCopiedNotificationEvent = oldNev as FolderCopiedNotificationEvent;
					if (folderCopiedNotificationEvent != null && base.Fid == folderCopiedNotificationEvent.OldFid)
					{
						return NotificationEvent.RedundancyStatus.FlagStopSearch;
					}
				}
			}
			return NotificationEvent.RedundancyStatus.Continue;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0004C56C File Offset: 0x0004A76C
		public override NotificationEvent MergeWithOldEvent(NotificationEvent oldNev)
		{
			return new FolderModifiedNotificationEvent(base.Database, base.MailboxNumber, base.UserIdentity, base.ClientType, base.EventFlags, (base.ExtendedEventFlags != null) ? base.ExtendedEventFlags.Value : Microsoft.Exchange.Server.Storage.LogicalDataModel.ExtendedEventFlags.None, base.Fid, base.ParentFid, ObjectCreatedModifiedNotificationEvent.MergeChangedPropTagArrays((oldNev as FolderModifiedNotificationEvent).ChangedPropTags, base.ChangedPropTags), base.ObjectClass, this.MessageCount, this.UnreadMessageCount);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0004C5F2 File Offset: 0x0004A7F2
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("FolderModifiedNotificationEvent");
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0004C600 File Offset: 0x0004A800
		protected override void AppendFields(StringBuilder sb)
		{
			base.AppendFields(sb);
			sb.Append(" MessageCount:[");
			sb.Append(this.messageCount);
			sb.Append("] UnreadMessageCount:[");
			sb.Append(this.unreadMessageCount);
			sb.Append("]");
		}

		// Token: 0x04000477 RID: 1143
		private int messageCount;

		// Token: 0x04000478 RID: 1144
		private int unreadMessageCount;
	}
}
