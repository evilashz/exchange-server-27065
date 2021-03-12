using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200000B RID: 11
	public class MailboxLogonList : LinkedList<MapiLogon>, IComponentData
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002EEA File Offset: 0x000010EA
		internal MailboxLogonList()
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002EF2 File Offset: 0x000010F2
		internal static void Initialize()
		{
			if (MailboxLogonList.mapilongListSlot == -1)
			{
				MailboxLogonList.mapilongListSlot = MailboxState.AllocateComponentDataSlot(false);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002F08 File Offset: 0x00001108
		internal static LinkedListNode<MapiLogon> AddLogon(MapiLogon logon)
		{
			MailboxLogonList cacheForMailbox = MailboxLogonList.GetCacheForMailbox(logon.MapiMailbox.SharedState);
			LinkedListNode<MapiLogon> result;
			using (LockManager.Lock(cacheForMailbox))
			{
				result = cacheForMailbox.AddLast(logon);
			}
			return result;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002F58 File Offset: 0x00001158
		internal static void RemoveLogon(MapiLogon logon)
		{
			MailboxLogonList cacheForMailbox = MailboxLogonList.GetCacheForMailbox(logon.MapiMailbox.SharedState);
			using (LockManager.Lock(cacheForMailbox))
			{
				cacheForMailbox.Remove(logon.NodeOfMailboxStateLogonList);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002FD4 File Offset: 0x000011D4
		internal static IList<MapiSession> GetSessionListOfMailbox(Context context, Guid mailboxGuid)
		{
			List<MapiSession> result = null;
			bool flag;
			MailboxState mailboxState;
			if (MailboxStateCache.TryGetByGuidLocked(context, mailboxGuid, MailboxCreation.DontAllow, false, false, (MailboxState state) => Context.GetMailboxLockTimeout(state, MapiContext.MailboxLockTimeout), context.Diagnostics, out flag, out mailboxState))
			{
				try
				{
					MailboxLogonList mailboxLogonList = (MailboxLogonList)mailboxState.GetComponentData(MailboxLogonList.mapilongListSlot);
					if (mailboxLogonList != null)
					{
						result = mailboxLogonList.Select(delegate(MapiLogon logon)
						{
							if (!logon.IsValid || logon.IsDisposed)
							{
								return null;
							}
							return logon.Session;
						}).ToList<MapiSession>();
					}
				}
				finally
				{
					mailboxState.ReleaseMailboxLock(false);
				}
				return result;
			}
			if (flag)
			{
				throw new StoreException((LID)38748U, ErrorCodeValue.Timeout);
			}
			throw new StoreException((LID)40400U, ErrorCodeValue.NotFound);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000030A4 File Offset: 0x000012A4
		bool IComponentData.DoCleanup(Context context)
		{
			bool result;
			using (LockManager.Lock(this, context.Diagnostics))
			{
				result = (base.Count == 0);
			}
			return result;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000030EC File Offset: 0x000012EC
		private static MailboxLogonList GetCacheForMailbox(MailboxState mailboxState)
		{
			MailboxLogonList mailboxLogonList = (MailboxLogonList)mailboxState.GetComponentData(MailboxLogonList.mapilongListSlot);
			if (mailboxLogonList == null)
			{
				mailboxLogonList = new MailboxLogonList();
				MailboxLogonList mailboxLogonList2 = (MailboxLogonList)mailboxState.CompareExchangeComponentData(MailboxLogonList.mapilongListSlot, null, mailboxLogonList);
				if (mailboxLogonList2 != null)
				{
					mailboxLogonList = mailboxLogonList2;
				}
			}
			return mailboxLogonList;
		}

		// Token: 0x04000042 RID: 66
		private static int mapilongListSlot = -1;
	}
}
