using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.SyncHealthLog
{
	// Token: 0x020000F4 RID: 244
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SyncHealthEventsStrings
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0002141C File Offset: 0x0001F61C
		public static Dictionary<SyncHealthEventsStrings.SyncHealthEvents, string> StringMap
		{
			get
			{
				if (SyncHealthEventsStrings.stringMap == null)
				{
					lock (SyncHealthEventsStrings.initLock)
					{
						if (SyncHealthEventsStrings.stringMap == null)
						{
							Type typeFromHandle = typeof(SyncHealthEventsStrings.SyncHealthEvents);
							Array values = Enum.GetValues(typeFromHandle);
							SyncHealthEventsStrings.stringMap = new Dictionary<SyncHealthEventsStrings.SyncHealthEvents, string>(values.Length);
							for (int i = 0; i < values.Length; i++)
							{
								SyncHealthEventsStrings.SyncHealthEvents syncHealthEvents = (SyncHealthEventsStrings.SyncHealthEvents)values.GetValue(i);
								string name = Enum.GetName(typeFromHandle, syncHealthEvents);
								SyncHealthEventsStrings.stringMap.Add(syncHealthEvents, name);
							}
						}
					}
				}
				return SyncHealthEventsStrings.stringMap;
			}
		}

		// Token: 0x040003E4 RID: 996
		private static object initLock = new object();

		// Token: 0x040003E5 RID: 997
		private static Dictionary<SyncHealthEventsStrings.SyncHealthEvents, string> stringMap;

		// Token: 0x020000F5 RID: 245
		internal enum SyncHealthEvents
		{
			// Token: 0x040003E7 RID: 999
			Sync,
			// Token: 0x040003E8 RID: 1000
			PolicyInducedSubscriptionDeletion,
			// Token: 0x040003E9 RID: 1001
			SubscriptionDispatch,
			// Token: 0x040003EA RID: 1002
			SubscriptionCompletion,
			// Token: 0x040003EB RID: 1003
			SubscriptionCreation,
			// Token: 0x040003EC RID: 1004
			SubscriptionDeletion,
			// Token: 0x040003ED RID: 1005
			RemoteServerHealth,
			// Token: 0x040003EE RID: 1006
			DatabaseDiscovery,
			// Token: 0x040003EF RID: 1007
			WorkTypeBudgets,
			// Token: 0x040003F0 RID: 1008
			MailboxNotification
		}
	}
}
