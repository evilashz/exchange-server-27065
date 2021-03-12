using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000008 RID: 8
	public static class Globals
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002DBC File Offset: 0x00000FBC
		public static void Initialize()
		{
			Globals.spoolerLockSlot = MailboxState.AllocateComponentDataSlot(false);
			TimedEventDispatcher.RegisterHandler(MapiTimedEvents.EventSource, new MapiTimedEventHandler());
			MapiStreamLock.Initialize();
			MailboxLogonList.Initialize();
			ActiveObjectLimits.Initialize();
			MapiSessionPerServiceCounter.Initialize();
			MapiSessionPerUserCounter.Initialize();
			SecurityContextManager.Initialize();
			MapiLogon.Initialize();
			MapiFolder.Initialize();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002E0B File Offset: 0x0000100B
		public static void Terminate()
		{
			TimedEventDispatcher.UnregisterHandler(MapiTimedEvents.EventSource);
			ActiveObjectLimits.Terminate();
			SecurityContextManager.Terminate();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002E21 File Offset: 0x00001021
		public static void DatabaseMounting(Context context, StoreDatabase database)
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002E23 File Offset: 0x00001023
		public static void DatabaseMounted(Context context, StoreDatabase database)
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002E25 File Offset: 0x00001025
		public static void DatabaseDismounting(Context context, StoreDatabase database)
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002E27 File Offset: 0x00001027
		internal static HashSet<ExchangeId> GetSpoolerLockList(Mailbox mailbox)
		{
			return mailbox.SharedState.GetComponentData(Globals.spoolerLockSlot) as HashSet<ExchangeId>;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002E3E File Offset: 0x0000103E
		internal static void SetSpoolerLockList(Mailbox mailbox, HashSet<ExchangeId> list)
		{
			mailbox.SharedState.SetComponentData(Globals.spoolerLockSlot, list);
		}

		// Token: 0x0400003E RID: 62
		internal const uint HsotNone = 4294967295U;

		// Token: 0x0400003F RID: 63
		internal const int MapiUnicode = -2147483648;

		// Token: 0x04000040 RID: 64
		private static int spoolerLockSlot;
	}
}
