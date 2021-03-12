using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000260 RID: 608
	internal static class ReplayExtensions
	{
		// Token: 0x06001EE9 RID: 7913 RVA: 0x0003FEA4 File Offset: 0x0003E0A4
		public static List<ReplayActionResult> Replay(this IReplayProvider provider, IReadOnlyCollection<ReplayAction> actions)
		{
			List<ReplayActionResult> list = new List<ReplayActionResult>(actions.Count);
			foreach (ReplayAction replayAction in actions)
			{
				switch (replayAction.Id)
				{
				case ActionId.MarkAsRead:
					provider.MarkAsRead(new MarkAsReadAction[]
					{
						(MarkAsReadAction)replayAction
					});
					list.Add(null);
					break;
				case ActionId.MarkAsUnRead:
					provider.MarkAsUnRead(new MarkAsUnReadAction[]
					{
						(MarkAsUnReadAction)replayAction
					});
					list.Add(null);
					break;
				case ActionId.Move:
				{
					IReadOnlyCollection<MoveActionResult> collection = provider.Move(new MoveAction[]
					{
						(MoveAction)replayAction
					});
					list.AddRange(collection);
					break;
				}
				case ActionId.Send:
					provider.Send((SendAction)replayAction);
					list.Add(null);
					break;
				case ActionId.Delete:
					provider.Delete(new DeleteAction[]
					{
						(DeleteAction)replayAction
					});
					list.Add(null);
					break;
				case ActionId.Flag:
					provider.Flag(new FlagAction[]
					{
						(FlagAction)replayAction
					});
					list.Add(null);
					break;
				case ActionId.FlagClear:
					provider.FlagClear(new FlagClearAction[]
					{
						(FlagClearAction)replayAction
					});
					list.Add(null);
					break;
				case ActionId.FlagComplete:
					provider.FlagComplete(new FlagCompleteAction[]
					{
						(FlagCompleteAction)replayAction
					});
					list.Add(null);
					break;
				case ActionId.CreateCalendarEvent:
				{
					IReadOnlyCollection<CreateCalendarEventActionResult> collection2 = provider.CreateCalendarEvent(new CreateCalendarEventAction[]
					{
						(CreateCalendarEventAction)replayAction
					});
					list.AddRange(collection2);
					break;
				}
				case ActionId.UpdateCalendarEvent:
					provider.UpdateCalendarEvent(new UpdateCalendarEventAction[]
					{
						(UpdateCalendarEventAction)replayAction
					});
					list.Add(null);
					break;
				default:
					throw new NotSupportedException();
				}
			}
			return list;
		}
	}
}
