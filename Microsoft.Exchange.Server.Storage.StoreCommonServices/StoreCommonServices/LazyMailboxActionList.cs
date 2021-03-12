using System;
using System.Threading;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200008E RID: 142
	internal class LazyMailboxActionList : IComponentData
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x0001D9AE File Offset: 0x0001BBAE
		private LazyMailboxActionList()
		{
			this.actions = new Action<Context, Mailbox>[LazyMailboxActionList.nextAvailableSlot];
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0001D9C6 File Offset: 0x0001BBC6
		internal static int LazyActionSlotForTest
		{
			get
			{
				return LazyMailboxActionList.lazyActionSlotForTest;
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001D9CD File Offset: 0x0001BBCD
		internal static LazyMailboxActionList GetCachedForMailbox(MailboxState mailboxState)
		{
			return (LazyMailboxActionList)mailboxState.GetComponentData(LazyMailboxActionList.mailboxStateSlot);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001D9DF File Offset: 0x0001BBDF
		internal static void Initialize()
		{
			if (LazyMailboxActionList.mailboxStateSlot == -1)
			{
				LazyMailboxActionList.mailboxStateSlot = MailboxState.AllocateComponentDataSlot(true);
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001D9F4 File Offset: 0x0001BBF4
		internal static void InitializeActionSlotForTest()
		{
			if (LazyMailboxActionList.lazyActionSlotForTest == -1)
			{
				LazyMailboxActionList.lazyActionSlotForTest = LazyMailboxActionList.AllocateSlot();
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001DA08 File Offset: 0x0001BC08
		public static int AllocateSlot()
		{
			return Interlocked.Increment(ref LazyMailboxActionList.nextAvailableSlot) - 1;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001DA16 File Offset: 0x0001BC16
		internal static void SetMailboxAction(int slot, MailboxState mailboxState, Action<Context, Mailbox> mailboxActionDelegate)
		{
			LazyMailboxActionList.SetMailboxAction(slot, mailboxState, mailboxActionDelegate, false);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001DA21 File Offset: 0x0001BC21
		internal static void AppendMailboxAction(int slot, MailboxState mailboxState, Action<Context, Mailbox> mailboxActionDelegate)
		{
			LazyMailboxActionList.SetMailboxAction(slot, mailboxState, mailboxActionDelegate, true);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001DA60 File Offset: 0x0001BC60
		private static void SetMailboxAction(int slot, MailboxState mailboxState, Action<Context, Mailbox> mailboxActionDelegate, bool appendToSlotList)
		{
			LazyMailboxActionList lazyMailboxActionList = (LazyMailboxActionList)mailboxState.GetComponentData(LazyMailboxActionList.mailboxStateSlot);
			if (lazyMailboxActionList == null)
			{
				lazyMailboxActionList = new LazyMailboxActionList();
				LazyMailboxActionList lazyMailboxActionList2 = (LazyMailboxActionList)mailboxState.CompareExchangeComponentData(LazyMailboxActionList.mailboxStateSlot, null, lazyMailboxActionList);
				if (lazyMailboxActionList2 != null)
				{
					lazyMailboxActionList = lazyMailboxActionList2;
				}
			}
			using (LockManager.Lock(lazyMailboxActionList.actions, LockManager.LockType.LeafMonitorLock))
			{
				if (lazyMailboxActionList.actions[slot] != null && appendToSlotList)
				{
					Action<Context, Mailbox> existingAction = lazyMailboxActionList.actions[slot];
					lazyMailboxActionList.actions[slot] = delegate(Context context, Mailbox mailbox)
					{
						existingAction(context, mailbox);
						mailboxActionDelegate(context, mailbox);
					};
				}
				else
				{
					lazyMailboxActionList.actions[slot] = mailboxActionDelegate;
				}
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001DB28 File Offset: 0x0001BD28
		internal static void ExecuteMailboxActions(Context context, Mailbox storeMailbox)
		{
			LazyMailboxActionList lazyMailboxActionList = (LazyMailboxActionList)storeMailbox.SharedState.GetComponentData(LazyMailboxActionList.mailboxStateSlot);
			if (lazyMailboxActionList != null)
			{
				for (int i = 0; i < lazyMailboxActionList.actions.Length; i++)
				{
					if (lazyMailboxActionList.actions[i] != null)
					{
						try
						{
							lazyMailboxActionList.actions[i](context, storeMailbox);
						}
						finally
						{
							lazyMailboxActionList.actions[i] = null;
						}
					}
				}
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001DB98 File Offset: 0x0001BD98
		bool IComponentData.DoCleanup(Context context)
		{
			return true;
		}

		// Token: 0x040003C5 RID: 965
		private static int mailboxStateSlot = -1;

		// Token: 0x040003C6 RID: 966
		private static int lazyActionSlotForTest = -1;

		// Token: 0x040003C7 RID: 967
		private static int nextAvailableSlot;

		// Token: 0x040003C8 RID: 968
		private Action<Context, Mailbox>[] actions;
	}
}
