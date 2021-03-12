using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200006C RID: 108
	public class InTransitInfo : IComponentData
	{
		// Token: 0x06000803 RID: 2051 RVA: 0x00045F43 File Offset: 0x00044143
		private InTransitInfo(InTransitStatus inTransitStatus, List<object> clientHandles)
		{
			this.inTransitStatus = inTransitStatus;
			this.clientHandles = clientHandles;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00045F59 File Offset: 0x00044159
		internal static void Initialize()
		{
			if (InTransitInfo.inTransitStatusListSlot == -1)
			{
				InTransitInfo.inTransitStatusListSlot = MailboxState.AllocateComponentDataSlot(true);
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00045F6E File Offset: 0x0004416E
		public static bool IsMoveUser(InTransitStatus inTransitStatus)
		{
			return (inTransitStatus & InTransitStatus.DirectionMask) != InTransitStatus.NotInTransit;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00045F7A File Offset: 0x0004417A
		public static bool IsMoveDestination(InTransitStatus inTransitStatus)
		{
			return (inTransitStatus & InTransitStatus.DirectionMask) == InTransitStatus.DestinationOfMove;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00045F83 File Offset: 0x00044183
		public static bool IsMoveSource(InTransitStatus inTransitStatus)
		{
			return (inTransitStatus & InTransitStatus.DirectionMask) == InTransitStatus.SourceOfMove;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00045F8C File Offset: 0x0004418C
		public static bool IsOnlineMove(InTransitStatus inTransitStatus)
		{
			return (inTransitStatus & InTransitStatus.OnlineMove) == InTransitStatus.OnlineMove;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00045F96 File Offset: 0x00044196
		public static bool IsForPublicFolderMove(InTransitStatus inTransitStatus)
		{
			return (inTransitStatus & InTransitStatus.ForPublicFolderMove) == InTransitStatus.ForPublicFolderMove;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00045FA0 File Offset: 0x000441A0
		public static bool IsPureOnlineSourceOfMove(InTransitStatus inTransitStatus)
		{
			return inTransitStatus == (InTransitStatus.SourceOfMove | InTransitStatus.OnlineMove);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00045FA8 File Offset: 0x000441A8
		public static ErrorCode SetInTransitState(MailboxState mailboxState, InTransitStatus newStatus, object clientHandle)
		{
			InTransitInfo inTransitInfo = (InTransitInfo)mailboxState.GetComponentData(InTransitInfo.inTransitStatusListSlot);
			if (ConfigurationSchema.MultipleSyncSourceClientsForPublicFolderMailbox.Value)
			{
				if (inTransitInfo != null)
				{
					List<object> list = inTransitInfo.clientHandles;
					if (mailboxState.IsPublicFolderMailbox && clientHandle != null && InTransitInfo.IsPureOnlineSourceOfMove(inTransitInfo.inTransitStatus) && !InTransitInfo.IsOnlineMove(newStatus))
					{
						list = new List<object>
						{
							clientHandle
						};
					}
					else if (mailboxState.IsPublicFolderMailbox && clientHandle != null && InTransitInfo.IsPureOnlineSourceOfMove(inTransitInfo.inTransitStatus) && InTransitInfo.IsPureOnlineSourceOfMove(newStatus))
					{
						if (!list.Contains(clientHandle))
						{
							list.Add(clientHandle);
						}
					}
					else
					{
						if (list.Count > 0 && clientHandle == null)
						{
							return ErrorCode.CreateNoAccess((LID)58916U);
						}
						if (list.Count > 0 && !list.Contains(clientHandle))
						{
							return ErrorCode.CreateNoAccess((LID)47168U);
						}
						if (list.Count > 1 && inTransitInfo.inTransitStatus != newStatus)
						{
							return ErrorCode.CreateNoAccess((LID)34340U);
						}
						if (clientHandle != null && list.Count == 0)
						{
							list.Add(clientHandle);
						}
					}
					if (mailboxState.IsPublicFolderMailbox)
					{
						InTransitInfo.IsPureOnlineSourceOfMove(newStatus);
					}
					mailboxState.SetComponentData(InTransitInfo.inTransitStatusListSlot, new InTransitInfo(newStatus, list));
				}
				else
				{
					List<object> list2 = new List<object>();
					if (clientHandle != null)
					{
						list2.Add(clientHandle);
					}
					mailboxState.SetComponentData(InTransitInfo.inTransitStatusListSlot, new InTransitInfo(newStatus, list2));
				}
			}
			else
			{
				if (inTransitInfo != null && inTransitInfo.clientHandles.Count > 0 && (clientHandle == null || !inTransitInfo.clientHandles.Contains(clientHandle)))
				{
					return ErrorCode.CreateNoAccess((LID)29684U);
				}
				List<object> list3 = new List<object>();
				if (clientHandle != null)
				{
					list3.Add(clientHandle);
				}
				mailboxState.SetComponentData(InTransitInfo.inTransitStatusListSlot, new InTransitInfo(newStatus, list3));
			}
			if ((newStatus & InTransitStatus.OnlineMove) != InTransitStatus.OnlineMove && (newStatus & InTransitStatus.DirectionMask) == InTransitStatus.SourceOfMove)
			{
				mailboxState.InvalidateLogons();
				UserInformation.LockUserEntryForModification(mailboxState.MailboxGuid);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00046180 File Offset: 0x00044380
		public static ErrorCode RemoveInTransitState(MailboxState mailboxState, object clientHandle)
		{
			InTransitInfo inTransitInfo = (InTransitInfo)mailboxState.GetComponentData(InTransitInfo.inTransitStatusListSlot);
			if (inTransitInfo != null)
			{
				if (clientHandle != null)
				{
					if (!inTransitInfo.clientHandles.Contains(clientHandle))
					{
						return ErrorCode.CreateNoAccess((LID)29688U);
					}
					inTransitInfo.clientHandles.Remove(clientHandle);
				}
				mailboxState.SetComponentData(InTransitInfo.inTransitStatusListSlot, (inTransitInfo.clientHandles.Count != 0) ? inTransitInfo : null);
				if (mailboxState.SupportsPerUserFeatures && InTransitInfo.IsMoveDestination(inTransitInfo.inTransitStatus))
				{
					SearchQueue.DrainSearchQueueTask.Launch(mailboxState);
				}
				UserInformation.UnlockUserEntryForModification(mailboxState.MailboxGuid);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00046218 File Offset: 0x00044418
		public static InTransitStatus GetInTransitStatusForClient(MailboxState mailboxState, object clientHandle)
		{
			InTransitInfo inTransitInfo = (InTransitInfo)mailboxState.GetComponentData(InTransitInfo.inTransitStatusListSlot);
			if (inTransitInfo != null && inTransitInfo.clientHandles.Contains(clientHandle))
			{
				return inTransitInfo.inTransitStatus;
			}
			return InTransitStatus.NotInTransit;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00046250 File Offset: 0x00044450
		public static InTransitStatus GetInTransitStatus(MailboxState mailboxState)
		{
			InTransitInfo inTransitInfo = (InTransitInfo)mailboxState.GetComponentData(InTransitInfo.inTransitStatusListSlot);
			if (inTransitInfo != null)
			{
				return inTransitInfo.inTransitStatus;
			}
			return InTransitStatus.NotInTransit;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0004627C File Offset: 0x0004447C
		public static List<object> GetInTransitClientHandles(MailboxState mailboxState)
		{
			InTransitInfo inTransitInfo = (InTransitInfo)mailboxState.GetComponentData(InTransitInfo.inTransitStatusListSlot);
			if (inTransitInfo != null)
			{
				return inTransitInfo.clientHandles;
			}
			return null;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x000462A8 File Offset: 0x000444A8
		public static bool IsClientNotAllowedToLogIn(MailboxState mailboxState, bool clientIsNotMigrationOrContentIndexing, object clientHandle)
		{
			InTransitInfo inTransitInfo = (InTransitInfo)mailboxState.GetComponentData(InTransitInfo.inTransitStatusListSlot);
			if (inTransitInfo == null)
			{
				return false;
			}
			if ((inTransitInfo.inTransitStatus & InTransitStatus.OnlineMove) == InTransitStatus.OnlineMove && (inTransitInfo.inTransitStatus & InTransitStatus.DirectionMask) == InTransitStatus.DestinationOfMove && clientIsNotMigrationOrContentIndexing)
			{
				return !inTransitInfo.clientHandles.Contains(clientHandle);
			}
			return (inTransitInfo.inTransitStatus & InTransitStatus.OnlineMove) != InTransitStatus.OnlineMove && !inTransitInfo.clientHandles.Contains(clientHandle);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00046315 File Offset: 0x00044515
		bool IComponentData.DoCleanup(Context context)
		{
			return false;
		}

		// Token: 0x04000405 RID: 1029
		private static int inTransitStatusListSlot = -1;

		// Token: 0x04000406 RID: 1030
		private InTransitStatus inTransitStatus;

		// Token: 0x04000407 RID: 1031
		private List<object> clientHandles;
	}
}
