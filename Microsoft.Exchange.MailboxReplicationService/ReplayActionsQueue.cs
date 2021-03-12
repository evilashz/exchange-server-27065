using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200005E RID: 94
	internal class ReplayActionsQueue
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x0001D15D File Offset: 0x0001B35D
		internal ReplayActionsQueue(int capacity)
		{
			this.queue = new Queue<ReplayAction>(capacity);
			this.optimizeMap = new EntryIdMap<List<ReplayAction>>(capacity);
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0001D17D File Offset: 0x0001B37D
		internal int Count
		{
			get
			{
				return this.queue.Count;
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001D18C File Offset: 0x0001B38C
		internal void Enqueue(ReplayAction action)
		{
			MrsTracer.Provider.Function("ReplayActionsQueue.Enqueue", new object[0]);
			byte[] itemId = action.ItemId;
			List<ReplayAction> list;
			if (!this.optimizeMap.TryGetValue(itemId, out list))
			{
				this.optimizeMap.Add(itemId, new List<ReplayAction>
				{
					action
				});
			}
			else
			{
				foreach (ReplayAction replayAction in list)
				{
					ActionUpdateGroup actionUpdateGroup;
					ActionUpdateGroup actionUpdateGroup2;
					if (!replayAction.Ignored && ReplayActionsQueue.IsActionUpdate(replayAction, out actionUpdateGroup) && ReplayActionsQueue.IsActionUpdate(action, out actionUpdateGroup2) && actionUpdateGroup == actionUpdateGroup2)
					{
						MrsTracer.Service.Debug("Action {0} already existed in the batch. Replacing with action: {1}", new object[]
						{
							replayAction,
							action
						});
						replayAction.Ignored = true;
					}
				}
				list.Add(action);
			}
			this.queue.Enqueue(action);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001D27C File Offset: 0x0001B47C
		internal bool TryQueue(out ReplayAction action)
		{
			if (this.queue.Count > 0)
			{
				action = this.queue.Dequeue();
				return true;
			}
			action = null;
			return false;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001D2A0 File Offset: 0x0001B4A0
		private static bool IsActionUpdate(ReplayAction action, out ActionUpdateGroup updateGroup)
		{
			updateGroup = ActionUpdateGroup.None;
			switch (action.Id)
			{
			case ActionId.MarkAsRead:
			case ActionId.MarkAsUnRead:
				updateGroup = ActionUpdateGroup.Read;
				return true;
			case ActionId.Flag:
			case ActionId.FlagClear:
			case ActionId.FlagComplete:
				updateGroup = ActionUpdateGroup.Flag;
				return true;
			case ActionId.UpdateCalendarEvent:
				updateGroup = ActionUpdateGroup.CalendarEvent;
				return true;
			}
			return false;
		}

		// Token: 0x04000209 RID: 521
		private readonly Queue<ReplayAction> queue;

		// Token: 0x0400020A RID: 522
		private readonly EntryIdMap<List<ReplayAction>> optimizeMap;
	}
}
