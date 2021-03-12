using System;
using System.Threading;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ResourceBooking;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ResourceBooking
{
	// Token: 0x0200011C RID: 284
	internal static class ResourceCheck
	{
		// Token: 0x06000B76 RID: 2934 RVA: 0x0004A35E File Offset: 0x0004855E
		public static bool IsEventConfigChange(MapiEvent mapiEvent)
		{
			return (mapiEvent.EventFlags & MapiEventFlags.FolderAssociated) != MapiEventFlags.None && CalendarConfigurationDataProvider.IsCalendarConfigurationClass(mapiEvent.ObjectClass);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0004A378 File Offset: 0x00048578
		public static bool? QuickCheckForAutomatedBooking(MapiEvent mapiEvent, CachedState cachedState)
		{
			bool? result = new bool?(false);
			cachedState.LockForRead();
			CalendarConfiguration calendarConfiguration;
			try
			{
				calendarConfiguration = (cachedState.State[0] as CalendarConfiguration);
			}
			finally
			{
				cachedState.ReleaseReaderLock();
			}
			if (calendarConfiguration == null || ResourceCheck.IsEventConfigChange(mapiEvent))
			{
				result = null;
			}
			else
			{
				result = new bool?(calendarConfiguration.AutomateProcessing == CalendarProcessingFlags.AutoAccept);
			}
			return result;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0004A3E0 File Offset: 0x000485E0
		public static bool DetailedCheckForAutomatedBooking(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, CachedState cachedState)
		{
			bool isEventForConfigObject = CalendarConfigurationDataProvider.IsCalendarConfigurationClass(mapiEvent.ObjectClass) && item != null;
			return ResourceCheck.InternalCheckForAutomaticBooking(isEventForConfigObject, itemStore, cachedState);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0004A40D File Offset: 0x0004860D
		public static bool DetailedCheckForAutomatedBooking(MailboxSession itemStore, CachedState cachedState)
		{
			return ResourceCheck.InternalCheckForAutomaticBooking(false, itemStore, cachedState);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0004A418 File Offset: 0x00048618
		private static bool InternalCheckForAutomaticBooking(bool isEventForConfigObject, MailboxSession itemStore, CachedState cachedState)
		{
			cachedState.LockForRead();
			CalendarConfiguration calendarConfiguration;
			try
			{
				calendarConfiguration = (cachedState.State[0] as CalendarConfiguration);
				if (calendarConfiguration == null || isEventForConfigObject)
				{
					LockCookie lockCookie = cachedState.UpgradeToWriterLock();
					try
					{
						using (CalendarConfigurationDataProvider calendarConfigurationDataProvider = new CalendarConfigurationDataProvider(itemStore))
						{
							calendarConfiguration = (CalendarConfiguration)calendarConfigurationDataProvider.Read<CalendarConfiguration>(null);
							if (calendarConfiguration == null)
							{
								Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_CorruptCalendarConfiguration, null, new object[]
								{
									itemStore.MailboxOwner.LegacyDn
								});
								calendarConfigurationDataProvider.Delete(new CalendarConfiguration());
								calendarConfiguration = new CalendarConfiguration();
							}
						}
						if (calendarConfiguration.AutomateProcessing == CalendarProcessingFlags.AutoAccept)
						{
							calendarConfiguration.AddNewRequestsTentatively = true;
						}
						cachedState.State[0] = calendarConfiguration;
						ResourceCheck.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: The calendar settings object was new or has been changed - (re)reading contents.", new object[]
						{
							TraceContext.Get()
						});
					}
					catch (ObjectNotFoundException innerException)
					{
						ResourceCheck.Tracer.TraceError((long)itemStore.GetHashCode(), "{0}: The calendar configuration was deleted while we were looking at it. Back off and retry.", new object[]
						{
							TraceContext.Get()
						});
						throw new TransientMailboxException(innerException);
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
			ResourceCheck.Tracer.TraceDebug<object, CalendarProcessingFlags>((long)itemStore.GetHashCode(), "{0}: MailboxType {1}", TraceContext.Get(), calendarConfiguration.AutomateProcessing);
			ResourceCheck.TracerPfd.TracePfd<int, object, CalendarProcessingFlags>((long)itemStore.GetHashCode(), "PFD IWR {0} {1}: ResourceMailbox: {2}", 19223, TraceContext.Get(), calendarConfiguration.AutomateProcessing);
			return calendarConfiguration.AutomateProcessing == CalendarProcessingFlags.AutoAccept;
		}

		// Token: 0x04000721 RID: 1825
		private static readonly Trace Tracer = ExTraceGlobals.ResourceCheckTracer;

		// Token: 0x04000722 RID: 1826
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;
	}
}
