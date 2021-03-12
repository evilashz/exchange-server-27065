using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarSync;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.MailboxAssistants.CalendarSync
{
	// Token: 0x020000BF RID: 191
	internal abstract class SynchronizableFolderType
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0003976C File Offset: 0x0003796C
		public static IList<SynchronizableFolderType> All
		{
			get
			{
				return SynchronizableFolderType.allSynchronizableFolderTypes;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060007FB RID: 2043
		public abstract string FolderTypeName { get; }

		// Token: 0x060007FC RID: 2044 RVA: 0x00039774 File Offset: 0x00037974
		static SynchronizableFolderType()
		{
			SynchronizableFolderType.Register(new ConsumerSharingCalendarType());
			SynchronizableFolderType.Register(new ExternalSharingCalendarType());
			SynchronizableFolderType.Register(new InternetCalendarType());
			SynchronizableFolderType.Register(new ExternalSharingContactsType());
			WebCalendar.RegisterPrefixes();
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x000397FC File Offset: 0x000379FC
		public static SynchronizableFolderType FromBinding(StoreObject binding)
		{
			binding.Load(SynchronizableFolderType.interestingBindingProperties);
			foreach (SynchronizableFolderType synchronizableFolderType in SynchronizableFolderType.All)
			{
				if (synchronizableFolderType.MatchesBinding(binding))
				{
					return synchronizableFolderType;
				}
			}
			return null;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0003985C File Offset: 0x00037A5C
		public static SynchronizableFolderType FromFolder(Folder folder)
		{
			foreach (SynchronizableFolderType synchronizableFolderType in SynchronizableFolderType.All)
			{
				if (synchronizableFolderType.MatchesContainerClass(folder.ClassName) && synchronizableFolderType.MatchesFolder(folder))
				{
					return synchronizableFolderType;
				}
			}
			return null;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x000398C0 File Offset: 0x00037AC0
		public static SynchronizableFolderType FromCounterProperty(PropertyDefinition propDef)
		{
			foreach (SynchronizableFolderType synchronizableFolderType in SynchronizableFolderType.All)
			{
				if (synchronizableFolderType.CounterProperty.Equals(propDef))
				{
					return synchronizableFolderType;
				}
			}
			return null;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0003991C File Offset: 0x00037B1C
		public static SynchronizableFolderType FromFolderRow(FolderRow folderRow)
		{
			foreach (SynchronizableFolderType synchronizableFolderType in SynchronizableFolderType.All)
			{
				if (synchronizableFolderType.MatchesFolderRow(folderRow))
				{
					return synchronizableFolderType;
				}
			}
			return null;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00039974 File Offset: 0x00037B74
		protected static void Register(SynchronizableFolderType folderType)
		{
			SynchronizableFolderType.allSynchronizableFolderTypes.Add(folderType);
			List<SynchronizableFolderType> list = new List<SynchronizableFolderType>(1);
			list.Add(folderType);
			SynchronizableFolderType.allLists.Add(folderType.GetType(), list);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000399AB File Offset: 0x00037BAB
		protected static void Unregister(SynchronizableFolderType folderType)
		{
			SynchronizableFolderType.allSynchronizableFolderTypes.Remove(folderType);
			SynchronizableFolderType.allLists.Remove(folderType.GetType());
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x000399CA File Offset: 0x00037BCA
		public IList<SynchronizableFolderType> ToList()
		{
			return SynchronizableFolderType.allLists[base.GetType()];
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x000399DC File Offset: 0x00037BDC
		public bool HasSubscription(MailboxSession mailboxSession, StoreObjectId folderId)
		{
			bool result;
			try
			{
				result = this.HasSubscriptionInternal(mailboxSession, folderId);
			}
			catch (ObjectNotFoundException arg)
			{
				SynchronizableFolderType.Tracer.TraceDebug<object, string, ObjectNotFoundException>((long)this.GetHashCode(), "{0}: SynchronizableFolderType.HasSubscription: user {1} doesn't have the default sharing folder. Returning false. Exception: {2}", TraceContext.Get(), mailboxSession.DisplayName, arg);
				result = false;
			}
			return result;
		}

		// Token: 0x06000805 RID: 2053
		public abstract bool Synchronize(MailboxSession mailboxSession, FolderRow folderRow, Deadline deadline, CalendarSyncPerformanceCountersInstance counters, CalendarSyncFolderOperationLogEntry folderOpLogEntry);

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000806 RID: 2054
		public abstract PropertyDefinition CounterProperty { get; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000807 RID: 2055
		public abstract StoreObjectType StoreObjectType { get; }

		// Token: 0x06000808 RID: 2056 RVA: 0x00039A2C File Offset: 0x00037C2C
		protected virtual bool MatchesBinding(StoreObject binding)
		{
			string valueOrDefault = binding.GetValueOrDefault<string>(BindingItemSchema.SharingLocalType, string.Empty);
			return binding.GetValueOrDefault<Guid>(BindingItemSchema.SharingProviderGuid, Guid.Empty).Equals(this.ProviderGuid) && this.MatchesContainerClass(valueOrDefault);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00039A74 File Offset: 0x00037C74
		protected bool MatchesFolderRow(FolderRow folderRow)
		{
			object extendedFolderFlags = folderRow.ExtendedFolderFlags;
			return !(extendedFolderFlags is PropertyError) && this.MatchesContainerClass(folderRow.ContainerClass) && this.MatchesExtendedFolderFlags((int)extendedFolderFlags);
		}

		// Token: 0x0600080A RID: 2058
		protected abstract bool HasSubscriptionInternal(MailboxSession mailboxSession, StoreObjectId folderId);

		// Token: 0x0600080B RID: 2059
		protected abstract bool MatchesContainerClass(string containerClass);

		// Token: 0x0600080C RID: 2060
		protected abstract bool MatchesFolder(Folder folder);

		// Token: 0x0600080D RID: 2061
		protected abstract bool MatchesExtendedFolderFlags(int extendedFolderFlags);

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600080E RID: 2062
		protected abstract Guid ProviderGuid { get; }

		// Token: 0x040005C4 RID: 1476
		protected static readonly Trace Tracer = ExTraceGlobals.CalendarSyncAssistantTracer;

		// Token: 0x040005C5 RID: 1477
		public static readonly TimeSpan MaxSyncTimePerFolder = TimeSpan.FromMinutes(1.0);

		// Token: 0x040005C6 RID: 1478
		private static PropertyDefinition[] interestingBindingProperties = new PropertyDefinition[]
		{
			BindingItemSchema.SharingProviderGuid,
			BindingItemSchema.SharingLocalType
		};

		// Token: 0x040005C7 RID: 1479
		private static List<SynchronizableFolderType> allSynchronizableFolderTypes = new List<SynchronizableFolderType>();

		// Token: 0x040005C8 RID: 1480
		private static Dictionary<Type, List<SynchronizableFolderType>> allLists = new Dictionary<Type, List<SynchronizableFolderType>>();
	}
}
