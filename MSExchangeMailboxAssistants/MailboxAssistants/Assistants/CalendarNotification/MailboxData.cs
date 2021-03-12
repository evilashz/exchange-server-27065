using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarNotification;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000E6 RID: 230
	internal sealed class MailboxData : DisposeTrackableBase
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x000407F8 File Offset: 0x0003E9F8
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x00040800 File Offset: 0x0003EA00
		internal UserSettings Settings { get; set; }

		// Token: 0x060009A0 RID: 2464 RVA: 0x0004080C File Offset: 0x0003EA0C
		public static MailboxData GetFromCache(Guid mailboxGuid)
		{
			MailboxData result;
			using (MailboxData.CachedStateReader cachedStateReader = new MailboxData.CachedStateReader(mailboxGuid))
			{
				result = cachedStateReader.Get();
			}
			return result;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00040844 File Offset: 0x0003EA44
		public static MailboxData UpdateCache(ref MailboxData mailboxData)
		{
			if (mailboxData == null)
			{
				throw new ArgumentNullException("mailboxData");
			}
			MailboxData mailboxData2 = null;
			using (MailboxData.CachedStateWriter cachedStateWriter = new MailboxData.CachedStateWriter(mailboxData.MailboxGuid))
			{
				mailboxData2 = cachedStateWriter.Get();
				if (mailboxData2 == null)
				{
					mailboxData2 = mailboxData;
					mailboxData = null;
				}
				else
				{
					using (mailboxData.CreateReadLock())
					{
						using (mailboxData2.CreateWriteLock())
						{
							mailboxData2.CopyExcludingActionsFrom(mailboxData);
						}
					}
				}
				cachedStateWriter.Set(mailboxData2);
			}
			return mailboxData2;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x000408EC File Offset: 0x0003EAEC
		public MailboxData(MailboxSession session)
		{
			this.Settings = new UserSettings(session);
			this.databaseGuid = session.MailboxOwner.MailboxInfo.GetDatabaseGuid();
			this.mailboxGuid = session.MailboxOwner.MailboxInfo.MailboxGuid;
			this.defaultCalendarFolderId = session.GetDefaultFolderId(DefaultFolderType.Calendar);
			this.defaultDeletedItemsFolderId = session.GetDefaultFolderId(DefaultFolderType.DeletedItems);
			this.defaultJunkEmailFolderId = session.GetDefaultFolderId(DefaultFolderType.JunkEmail);
			this.defaultOutboxFolderId = session.GetDefaultFolderId(DefaultFolderType.Outbox);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00040984 File Offset: 0x0003EB84
		public MailboxData(InfoFromUserMailboxSession info)
		{
			this.Settings = new UserSettings(info);
			this.databaseGuid = info.DatabaseGuid;
			this.mailboxGuid = info.MailboxGuid;
			this.defaultCalendarFolderId = info.DefaultCalendarFolderId;
			this.defaultDeletedItemsFolderId = info.DefaultDeletedItemsFolderId;
			this.defaultJunkEmailFolderId = info.DefaultJunkEmailFolderId;
			this.defaultOutboxFolderId = info.DefaultOutboxFolderId;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00040A08 File Offset: 0x0003EC08
		public static MailboxData CreateFromUserSettings(UserSettings settings)
		{
			MailboxData result;
			try
			{
				MailboxData mailboxData = null;
				ExchangePrincipal mailboxOwner = ExchangePrincipal.FromLegacyDN(settings.GetADSettings(), settings.LegacyDN);
				using (MailboxSession mailboxSession = MailboxSession.OpenAsSystemService(mailboxOwner, CultureInfo.InvariantCulture, "Client=TBA;Action=GetInitialState"))
				{
					mailboxData = new MailboxData(mailboxSession);
				}
				mailboxData.Settings = settings;
				result = mailboxData;
			}
			catch (AdUserNotFoundException arg)
			{
				ExTraceGlobals.AssistantTracer.TraceDebug<string, AdUserNotFoundException>((long)typeof(MailboxData).GetHashCode(), "cannot find AD user: {0}", settings.LegacyDN, arg);
				result = null;
			}
			return result;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00040AA4 File Offset: 0x0003ECA4
		public static MailboxData CreateFromUserSettings(UserSettings settings, InfoFromUserMailboxSession info)
		{
			return new MailboxData(info)
			{
				Settings = settings
			};
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00040AC0 File Offset: 0x0003ECC0
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00040AC8 File Offset: 0x0003ECC8
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00040AD0 File Offset: 0x0003ECD0
		public StoreObjectId DefaultCalendarFolderId
		{
			get
			{
				return this.defaultCalendarFolderId;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00040AD8 File Offset: 0x0003ECD8
		public StoreObjectId DefaultDeletedItemsFolderId
		{
			get
			{
				return this.defaultDeletedItemsFolderId;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00040AE0 File Offset: 0x0003ECE0
		public StoreObjectId DefaultJunkEmailFolderId
		{
			get
			{
				return this.defaultJunkEmailFolderId;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00040AE8 File Offset: 0x0003ECE8
		public StoreObjectId DefaultOutboxFolderId
		{
			get
			{
				return this.defaultOutboxFolderId;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00040AF0 File Offset: 0x0003ECF0
		public ThreadSafeCollection<CalendarNotificationAction> Actions
		{
			get
			{
				if (this.actions == null)
				{
					lock (this.actionsCreationSyncObj)
					{
						if (this.actions == null)
						{
							this.actions = new ThreadSafeCollection<CalendarNotificationAction>(10, null, null);
						}
					}
				}
				return this.actions;
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00040B50 File Offset: 0x0003ED50
		public IDisposable CreateReadLock()
		{
			return this.syncObj.CreateReadLock();
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00040B5D File Offset: 0x0003ED5D
		public IDisposable CreateUpgradeableReadLock()
		{
			return this.syncObj.CreateUpgradeableReadLock();
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00040B6A File Offset: 0x0003ED6A
		public IDisposable CreateWriteLock()
		{
			return this.syncObj.CreateWriteLock();
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00040B78 File Offset: 0x0003ED78
		public void CopyExcludingActionsFrom(MailboxData mailboxData)
		{
			if (mailboxData == null)
			{
				throw new ArgumentNullException("mailboxData");
			}
			this.databaseGuid = mailboxData.DatabaseGuid;
			this.mailboxGuid = mailboxData.MailboxGuid;
			this.defaultCalendarFolderId = mailboxData.DefaultCalendarFolderId;
			this.defaultDeletedItemsFolderId = mailboxData.DefaultDeletedItemsFolderId;
			this.defaultJunkEmailFolderId = mailboxData.DefaultJunkEmailFolderId;
			this.defaultOutboxFolderId = mailboxData.DefaultOutboxFolderId;
			this.Settings = mailboxData.Settings;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00040BE7 File Offset: 0x0003EDE7
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.Settings.LegacyDN))
			{
				return this.Settings.LegacyDN;
			}
			return string.Format("{0}\\{1}", this.DatabaseGuid, this.MailboxGuid);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00040C40 File Offset: 0x0003EE40
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.actions != null)
				{
					List<CalendarNotificationAction> actionList = new List<CalendarNotificationAction>(this.Actions.Count);
					this.actions.Remove(delegate(CalendarNotificationAction action)
					{
						actionList.Add(action);
						return true;
					});
					this.actions.Dispose();
					this.actions = null;
					foreach (CalendarNotificationAction calendarNotificationAction in actionList)
					{
						calendarNotificationAction.Dispose();
					}
				}
				this.syncObj.Dispose();
				this.syncObj = null;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00040D00 File Offset: 0x0003EF00
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxData>(this);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00040D08 File Offset: 0x0003EF08
		private void ThrowIfGetterUnsafe()
		{
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00040D0A File Offset: 0x0003EF0A
		private void ThrowIfSetterUnsafe()
		{
		}

		// Token: 0x04000671 RID: 1649
		private const int InitialActionCapacity = 10;

		// Token: 0x04000672 RID: 1650
		private SynchronizationObject syncObj = new SynchronizationObject();

		// Token: 0x04000673 RID: 1651
		private Guid databaseGuid;

		// Token: 0x04000674 RID: 1652
		private Guid mailboxGuid;

		// Token: 0x04000675 RID: 1653
		private StoreObjectId defaultCalendarFolderId;

		// Token: 0x04000676 RID: 1654
		private StoreObjectId defaultDeletedItemsFolderId;

		// Token: 0x04000677 RID: 1655
		private StoreObjectId defaultJunkEmailFolderId;

		// Token: 0x04000678 RID: 1656
		private StoreObjectId defaultOutboxFolderId;

		// Token: 0x04000679 RID: 1657
		private object actionsCreationSyncObj = new object();

		// Token: 0x0400067A RID: 1658
		private ThreadSafeCollection<CalendarNotificationAction> actions;

		// Token: 0x020000E7 RID: 231
		public class CachedStateReader : DisposeTrackableBase
		{
			// Token: 0x060009B6 RID: 2486 RVA: 0x00040D0C File Offset: 0x0003EF0C
			public CachedStateReader(Guid mailboxGuid)
			{
				this.cachedState = AssistantsService.CachedObjectsList.GetCachedState(mailboxGuid);
				this.cachedState.LockForRead();
			}

			// Token: 0x060009B7 RID: 2487 RVA: 0x00040D30 File Offset: 0x0003EF30
			public MailboxData Get()
			{
				return this.cachedState.State[8] as MailboxData;
			}

			// Token: 0x060009B8 RID: 2488 RVA: 0x00040D44 File Offset: 0x0003EF44
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<MailboxData.CachedStateReader>(this);
			}

			// Token: 0x060009B9 RID: 2489 RVA: 0x00040D4C File Offset: 0x0003EF4C
			protected override void InternalDispose(bool disposing)
			{
				if (disposing)
				{
					this.cachedState.ReleaseReaderLock();
				}
			}

			// Token: 0x0400067C RID: 1660
			private CachedState cachedState;
		}

		// Token: 0x020000E8 RID: 232
		public class CachedStateWriter : DisposeTrackableBase
		{
			// Token: 0x060009BA RID: 2490 RVA: 0x00040D5C File Offset: 0x0003EF5C
			public CachedStateWriter(Guid mailboxGuid)
			{
				this.cachedState = AssistantsService.CachedObjectsList.GetCachedState(mailboxGuid);
				this.cachedState.LockForWrite();
			}

			// Token: 0x060009BB RID: 2491 RVA: 0x00040D80 File Offset: 0x0003EF80
			public MailboxData Get()
			{
				return this.cachedState.State[8] as MailboxData;
			}

			// Token: 0x060009BC RID: 2492 RVA: 0x00040D94 File Offset: 0x0003EF94
			public void Set(MailboxData mailboxData)
			{
				this.cachedState.State[8] = mailboxData;
			}

			// Token: 0x060009BD RID: 2493 RVA: 0x00040DA4 File Offset: 0x0003EFA4
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<MailboxData.CachedStateWriter>(this);
			}

			// Token: 0x060009BE RID: 2494 RVA: 0x00040DAC File Offset: 0x0003EFAC
			protected override void InternalDispose(bool disposing)
			{
				if (disposing)
				{
					this.cachedState.ReleaseWriterLock();
				}
			}

			// Token: 0x0400067D RID: 1661
			private CachedState cachedState;
		}
	}
}
