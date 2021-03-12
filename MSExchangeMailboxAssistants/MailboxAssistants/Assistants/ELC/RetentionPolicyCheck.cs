using System;
using System.Threading;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000084 RID: 132
	internal static class RetentionPolicyCheck
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x0002570C File Offset: 0x0002390C
		internal static void UpdateStateForPendingFaiEvent(long eventCounter, CachedState cachedState)
		{
			cachedState.LockForWrite();
			try
			{
				UserRetentionPolicyCache userRetentionPolicyCache = cachedState.State[5] as UserRetentionPolicyCache;
				if (userRetentionPolicyCache == null || userRetentionPolicyCache.ReadOnlyCache)
				{
					userRetentionPolicyCache = new UserRetentionPolicyCache();
				}
				userRetentionPolicyCache.HasPendingFaiEvent = true;
				userRetentionPolicyCache.PendingFaiEventCounter = eventCounter;
				cachedState.State[5] = userRetentionPolicyCache;
			}
			finally
			{
				cachedState.ReleaseWriterLock();
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00025770 File Offset: 0x00023970
		internal static bool IsEventConfigChange(MapiEvent mapiEvent)
		{
			return (mapiEvent.EventFlags & MapiEventFlags.FolderAssociated) != MapiEventFlags.None && string.Compare(mapiEvent.ObjectClass, "IPM.Configuration.MRM", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00025792 File Offset: 0x00023992
		internal static bool IsAutoTagFai(MapiEvent mapiEvent)
		{
			return mapiEvent.ClientType != MapiEventClientTypes.EventBasedAssistants && (mapiEvent.EventFlags & MapiEventFlags.FolderAssociated) != MapiEventFlags.None && (string.Compare(mapiEvent.ObjectClass, "IPM.Configuration.MRM.AutoTag.Model", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(mapiEvent.ObjectClass, "IPM.Configuration.MRM.AutoTag.Setting", StringComparison.OrdinalIgnoreCase) == 0);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000257D4 File Offset: 0x000239D4
		internal static UserRetentionPolicyCache QuickCheckForRetentionPolicy(MapiEvent mapiEvent, CachedState cachedState)
		{
			UserRetentionPolicyCache userRetentionPolicyCache = null;
			cachedState.LockForRead();
			try
			{
				userRetentionPolicyCache = (cachedState.State[5] as UserRetentionPolicyCache);
			}
			finally
			{
				cachedState.ReleaseReaderLock();
			}
			if (userRetentionPolicyCache == null || RetentionPolicyCheck.IsEventConfigChange(mapiEvent) || userRetentionPolicyCache.HasPendingFaiEvent)
			{
				userRetentionPolicyCache = null;
			}
			return userRetentionPolicyCache;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00025828 File Offset: 0x00023A28
		internal static UserRetentionPolicyCache DetailedCheckForRetentionPolicy(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, CachedState cachedState)
		{
			UserRetentionPolicyCache userRetentionPolicyCache = null;
			cachedState.LockForRead();
			try
			{
				userRetentionPolicyCache = (cachedState.State[5] as UserRetentionPolicyCache);
				if (userRetentionPolicyCache == null || (item != null && RetentionPolicyCheck.IsEventConfigChange(mapiEvent)) || (userRetentionPolicyCache.HasPendingFaiEvent && userRetentionPolicyCache.PendingFaiEventCounter <= mapiEvent.EventCounter))
				{
					LockCookie lockCookie = cachedState.UpgradeToWriterLock();
					try
					{
						if (userRetentionPolicyCache == null || !userRetentionPolicyCache.UnderRetentionPolicy)
						{
							userRetentionPolicyCache = new UserRetentionPolicyCache();
						}
						userRetentionPolicyCache.LoadStoreTagDataFromStore(itemStore);
						if (!userRetentionPolicyCache.UnderRetentionPolicy)
						{
							userRetentionPolicyCache = RetentionPolicyCheck.CachedOffState;
						}
						cachedState.State[5] = userRetentionPolicyCache;
						RetentionPolicyCheck.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: The MRM settings object was new or has been changed - (re)reading contents.", new object[]
						{
							TraceContext.Get()
						});
					}
					finally
					{
						cachedState.DowngradeFromWriterLock(ref lockCookie);
					}
				}
			}
			finally
			{
				cachedState.ReleaseReaderLock();
			}
			RetentionPolicyCheck.Tracer.TraceDebug<object, bool>((long)itemStore.GetHashCode(), "{0}: Mailbox under retention policy: {1}", TraceContext.Get(), userRetentionPolicyCache.UnderRetentionPolicy);
			RetentionPolicyCheck.TracerPfd.TracePfd<int, object, bool>((long)itemStore.GetHashCode(), "PFD IWR {0} {1}: Mailbox under retention policy: {2}", 19223, TraceContext.Get(), userRetentionPolicyCache.UnderRetentionPolicy);
			return userRetentionPolicyCache;
		}

		// Token: 0x040003C2 RID: 962
		private static readonly Trace Tracer = ExTraceGlobals.EventBasedAssistantTracer;

		// Token: 0x040003C3 RID: 963
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x040003C4 RID: 964
		private static UserRetentionPolicyCache CachedOffState = UserRetentionPolicyCache.GetCacheThatIsOff();
	}
}
