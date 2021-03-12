using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Exchange.Win32;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Server.Storage.MapiDisp
{
	// Token: 0x0200000C RID: 12
	public static class Globals
	{
		// Token: 0x0600023A RID: 570 RVA: 0x00036234 File Offset: 0x00034434
		public static void Initialize()
		{
			if (Microsoft.Exchange.Server.Storage.MapiDisp.Globals.notificationCompletionPort == null)
			{
				Microsoft.Exchange.Server.Storage.MapiDisp.Globals.notificationCompletionPort = NativeMethods.CreateIoCompletionPort(new SafeFileHandle(new IntPtr(-1), true), IoCompletionPort.InvalidHandle, new UIntPtr(0U), 0U);
				NotificationContext.AssignCompletionPort(Microsoft.Exchange.Server.Storage.MapiDisp.Globals.notificationCompletionPort, 1U);
			}
			if (Microsoft.Exchange.Server.Storage.MapiDisp.Globals.notificationPumpTask == null)
			{
				Microsoft.Exchange.Server.Storage.MapiDisp.Globals.notificationPumpTask = new CompletionNotificationTask<object>(new CompletionNotificationTask<object>.CompletionNotificationCallback(MapiRpc.PumpNotificationsTask), null, Microsoft.Exchange.Server.Storage.MapiDisp.Globals.notificationCompletionPort, (uint)Microsoft.Exchange.Server.Storage.MapiDisp.Globals.NotificationPumpingInterval.TotalMilliseconds, false);
			}
			MailboxCleanup.Initialize();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000362AC File Offset: 0x000344AC
		public static void Terminate()
		{
			PoolRpcServer.Terminate();
			MapiRpc.Terminate();
			if (Microsoft.Exchange.Server.Storage.MapiDisp.Globals.notificationPumpTask != null)
			{
				Microsoft.Exchange.Server.Storage.MapiDisp.Globals.notificationPumpTask.WaitForCompletion();
				Microsoft.Exchange.Server.Storage.MapiDisp.Globals.notificationPumpTask.Dispose();
				Microsoft.Exchange.Server.Storage.MapiDisp.Globals.notificationPumpTask = null;
			}
			if (Microsoft.Exchange.Server.Storage.MapiDisp.Globals.notificationCompletionPort != null)
			{
				Microsoft.Exchange.Server.Storage.MapiDisp.Globals.notificationCompletionPort.Dispose();
				Microsoft.Exchange.Server.Storage.MapiDisp.Globals.notificationCompletionPort = null;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000362FC File Offset: 0x000344FC
		public static void StartAllTasks()
		{
			Microsoft.Exchange.Server.Storage.MapiDisp.Globals.notificationPumpTask.Start();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00036308 File Offset: 0x00034508
		public static void DisableTokenSingleInstancingForTest()
		{
			Microsoft.Exchange.Server.Storage.MapiDisp.Globals.IsTokenSingleInstancingEnabled = false;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00036310 File Offset: 0x00034510
		public static void EnableTokenSingleInstancingForTest()
		{
			Microsoft.Exchange.Server.Storage.MapiDisp.Globals.IsTokenSingleInstancingEnabled = true;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00036318 File Offset: 0x00034518
		public static void SignalStopToAllTasks()
		{
			NotificationContext.AssignCompletionPort(null, 0U);
			if (Microsoft.Exchange.Server.Storage.MapiDisp.Globals.notificationPumpTask != null)
			{
				Microsoft.Exchange.Server.Storage.MapiDisp.Globals.notificationPumpTask.Stop();
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00036332 File Offset: 0x00034532
		public static void DatabaseMounting(Context context, StoreDatabase database)
		{
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00036334 File Offset: 0x00034534
		public static void DatabaseMounted(Context context, StoreDatabase database)
		{
			if (!database.IsReadOnly)
			{
				MailboxCleanup.MountedEventHandler(context, database);
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00036355 File Offset: 0x00034555
		public static void DatabaseDismounting(Context context, StoreDatabase database)
		{
			Microsoft.Exchange.Server.Storage.MapiDisp.Globals.ForEachSession(delegate(MapiSession session, Func<bool> shouldCallbackContinue)
			{
				if (!session.IsDisposed)
				{
					session.RequestClose();
				}
			});
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0003637C File Offset: 0x0003457C
		public static void ForEachSession(Task<MapiSession>.Callback enumCallback)
		{
			Microsoft.Exchange.Server.Storage.MapiDisp.Globals.ForEachSession(enumCallback, () => true);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000363A4 File Offset: 0x000345A4
		public static void ForEachSession(Task<MapiSession>.Callback enumCallback, Func<bool> shouldCallbackContinue)
		{
			if (MapiRpc.Instance != null)
			{
				IEnumerable<MapiSession> sessionListSnapshot = MapiRpc.Instance.GetSessionListSnapshot();
				foreach (MapiSession mapiSession in sessionListSnapshot)
				{
					mapiSession.LockSession(false);
					try
					{
						if (mapiSession.IsValid)
						{
							enumCallback(mapiSession, shouldCallbackContinue);
						}
					}
					finally
					{
						mapiSession.UnlockSession();
					}
				}
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00036424 File Offset: 0x00034624
		public static void DeregisterAllSessionssOfMailbox(MapiContext context, Guid mdbGuid, Guid mailboxGuid)
		{
			IList<MapiSession> list = null;
			StoreDatabase storeDatabase = Storage.FindDatabase(mdbGuid);
			if (storeDatabase == null)
			{
				throw new StoreException((LID)48592U, ErrorCodeValue.NotFound);
			}
			using (context.AssociateWithDatabase(storeDatabase))
			{
				if (!storeDatabase.IsOnlineActive)
				{
					throw new StoreException((LID)64976U, ErrorCodeValue.MdbNotInitialized);
				}
				list = MailboxLogonList.GetSessionListOfMailbox(context, mailboxGuid);
			}
			if (list == null)
			{
				return;
			}
			for (int i = 0; i < list.Count; i++)
			{
				MapiSession mapiSession = list[i];
				if (mapiSession != null)
				{
					mapiSession.LockSession(false);
					try
					{
						if (mapiSession.IsValid && !mapiSession.IsDisposed)
						{
							mapiSession.RequestClose();
						}
					}
					finally
					{
						mapiSession.UnlockSession();
					}
				}
			}
		}

		// Token: 0x0400014D RID: 333
		private static readonly TimeSpan NotificationPumpingInterval = TimeSpan.FromSeconds(10.0);

		// Token: 0x0400014E RID: 334
		private static CompletionNotificationTask<object> notificationPumpTask;

		// Token: 0x0400014F RID: 335
		private static IoCompletionPort notificationCompletionPort;

		// Token: 0x04000150 RID: 336
		internal static bool IsTokenSingleInstancingEnabled = true;
	}
}
