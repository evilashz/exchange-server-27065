using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000083 RID: 131
	public class FolderDeletedNotificationEvent : ObjectNotificationEvent
	{
		// Token: 0x060008D6 RID: 2262 RVA: 0x0004C654 File Offset: 0x0004A854
		public FolderDeletedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExtendedEventFlags extendedEventFlags, ExchangeId fid, ExchangeId parentFid, string containerClass) : base(database, mailboxNumber, EventType.ObjectDeleted, userIdentity, clientType, eventFlags, extendedEventFlags, fid, ExchangeId.Null, parentFid, null, null, containerClass)
		{
			Statistics.NotificationTypes.FolderDeleted.Bump();
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0004C698 File Offset: 0x0004A898
		public override NotificationEvent.RedundancyStatus GetRedundancyStatus(NotificationEvent oldNev)
		{
			ObjectNotificationEvent objectNotificationEvent = oldNev as ObjectNotificationEvent;
			if (objectNotificationEvent != null)
			{
				if (base.IsSameObject(objectNotificationEvent))
				{
					if (objectNotificationEvent.EventType == EventType.ObjectModified)
					{
						return NotificationEvent.RedundancyStatus.DropOldAndStop;
					}
					if (objectNotificationEvent.EventType == EventType.ObjectCreated)
					{
						return NotificationEvent.RedundancyStatus.DropBothAndStop;
					}
					return NotificationEvent.RedundancyStatus.FlagStopSearch;
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

		// Token: 0x060008D8 RID: 2264 RVA: 0x0004C70B File Offset: 0x0004A90B
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("FolderDeletedNotificationEvent");
		}
	}
}
