using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.CalendarDiagnostics;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Calendar;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.MailboxAssistants.Assistants.ResourceBooking;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Calendar
{
	// Token: 0x020000B3 RID: 179
	internal sealed class CalendarAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x000346FC File Offset: 0x000328FC
		public CalendarAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			this.cvsGateway = new CalendarVersionStoreGateway(new CalendarVersionStoreQueryPolicy(TimeSpan.Zero), true);
			this.calProcessor = new CalendarProcessing(false);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00034729 File Offset: 0x00032929
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			if (mapiEvent.ItemType == ObjectType.MAPI_MESSAGE)
			{
				return this.IsMessageInteresting(mapiEvent);
			}
			if (mapiEvent.ItemType == ObjectType.MAPI_FOLDER)
			{
				return this.IsFolderChangeInteresting(mapiEvent);
			}
			return mapiEvent.ItemType == ObjectType.MAPI_STORE && this.IsStoreEventInteresting(mapiEvent);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00034760 File Offset: 0x00032960
		private bool IsMessageInteresting(MapiEvent mapiEvent)
		{
			bool flag = ResourceCheck.IsEventConfigChange(mapiEvent);
			if (ObjectClass.IsMiddleTierRulesMessage(mapiEvent.ObjectClass) && !flag)
			{
				return true;
			}
			if (!CalendarAssistant.ShouldProcessMessageClass(mapiEvent) && !flag)
			{
				return false;
			}
			CachedState cachedState = AssistantsService.CachedObjectsList.GetCachedState(mapiEvent.MailboxGuid);
			bool? flag2 = ResourceCheck.QuickCheckForAutomatedBooking(mapiEvent, cachedState);
			if (flag2 == null)
			{
				return true;
			}
			if (flag2 == false)
			{
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
				return !calendarConfiguration.SkipProcessing;
			}
			return false;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00034808 File Offset: 0x00032A08
		private bool IsFolderChangeInteresting(MapiEvent mapiEvent)
		{
			return (ObjectClass.IsCalendarFolder(mapiEvent.ObjectClass) || mapiEvent.ObjectClass == string.Empty) && (mapiEvent.ExtendedEventFlags & MapiExtendedEventFlags.FolderPermissionChanged) != MapiExtendedEventFlags.None;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0003483D File Offset: 0x00032A3D
		private bool IsStoreEventInteresting(MapiEvent mapiEvent)
		{
			return mapiEvent.EventMask == MapiEventTypeFlags.MailboxMoveSucceeded || mapiEvent.EventMask == MapiEventTypeFlags.MailboxMoveFailed;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0003485C File Offset: 0x00032A5C
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			if (mapiEvent.ItemType == ObjectType.MAPI_MESSAGE)
			{
				if (!ObjectClass.IsMiddleTierRulesMessage(mapiEvent.ObjectClass))
				{
					this.HandleMeetingMessage(mapiEvent, itemStore, item, customDataToLog);
					return;
				}
				if (this.GetDelegatesRuleLoggerStatus(itemStore))
				{
					this.HandleDelegateRuleMessage(mapiEvent, itemStore, item);
					return;
				}
			}
			else
			{
				if (mapiEvent.ItemType == ObjectType.MAPI_FOLDER)
				{
					this.HandlePermissionChange(mapiEvent, itemStore, item, customDataToLog);
					return;
				}
				if (mapiEvent.ItemType == ObjectType.MAPI_STORE)
				{
					this.HandleStoreEvent(mapiEvent, itemStore, item, customDataToLog);
				}
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000348C8 File Offset: 0x00032AC8
		private void HandleDelegateRuleMessage(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item)
		{
			if (mapiEvent.ItemEntryId == null)
			{
				return;
			}
			if (mapiEvent.EventMask == MapiEventTypeFlags.ObjectDeleted)
			{
				DelegateRulesManagementLogger.LogEntry(itemStore, string.Format("Client Type: {0} \nEvent: {1} \nMapiEvent ItemEntryID : {2}\n", mapiEvent.ClientType, mapiEvent.EventMask, mapiEvent.ItemEntryIdString));
				return;
			}
			Rule delegateRule = this.GetDelegateRule(mapiEvent, itemStore);
			if (mapiEvent.EventMask != MapiEventTypeFlags.ObjectDeleted && delegateRule != null)
			{
				try
				{
					DelegateUserCollection delegateUserCollection = new DelegateUserCollection(itemStore);
					DelegateRulesManagementLogger.LogEntry(itemStore, string.Format("Client Type: {0} \nEvent: {1} \nTotal Inbox Rules count: {2} \nDelegate RuleName: Delegate Rule {3}, Delegate RuleProvider: {4} \nDelegates added for {5}: {6} \nDelegate can see Private Items: {7} \nDelegate RuleType for {8}: {9} \nMapiEvent ItemEntryID : {10}\n", new object[]
					{
						mapiEvent.ClientType,
						mapiEvent.EventMask,
						itemStore.AllInboxRules.Count,
						delegateRule.ID,
						delegateRule.Provider,
						itemStore.DisplayName,
						string.Join(",", delegateUserCollection.DelegateRestoreInfo.Names),
						string.Join<int>(",", delegateUserCollection.DelegateRestoreInfo.Flags),
						itemStore.DisplayName,
						delegateUserCollection.DelegateRuleType,
						mapiEvent.ItemEntryIdString
					}));
				}
				catch (DelegateUserNoFreeBusyFolderException)
				{
					CalendarAssistant.GeneralTracer.TraceDebug((long)this.GetHashCode(), "{0}: FreeBusy folder does not exist. Skipping Delegate Rules Logging.", new object[]
					{
						TraceContext.Get()
					});
					CalendarAssistantLog.LogEntry(itemStore, "Could not get FreeBusy folder id", new object[0]);
				}
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00034A3C File Offset: 0x00032C3C
		private void HandleMeetingMessage(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			if (item == null)
			{
				string objectClass = mapiEvent.ObjectClass;
			}
			else
			{
				string className = item.ClassName;
			}
			if (itemStore.GetDefaultFolderId(DefaultFolderType.Calendar) == null)
			{
				CalendarAssistant.GeneralTracer.TraceDebug((long)this.GetHashCode(), "{0}: Calendar folder does not exist. Skipping processing.", new object[]
				{
					TraceContext.Get()
				});
				CalendarAssistantLog.LogEntry(itemStore, "Could not get default folder id", new object[0]);
				return;
			}
			if (item == null)
			{
				return;
			}
			MeetingMessage meetingMessage = item as MeetingMessage;
			if (meetingMessage != null && meetingMessage.IsArchiveMigratedMessage)
			{
				CalendarAssistant.CachedStateTracer.TraceDebug((long)this.GetHashCode(), "{0}: Skipping the processing of the meeting item as this is an EHA migration traffic.", new object[]
				{
					TraceContext.Get()
				});
				return;
			}
			CachedState cachedState = AssistantsService.CachedObjectsList.GetCachedState(mapiEvent.MailboxGuid);
			bool flag = ResourceCheck.DetailedCheckForAutomatedBooking(mapiEvent, itemStore, item, cachedState);
			if (itemStore != null)
			{
				string legacyDn = itemStore.MailboxOwner.LegacyDn;
			}
			if (flag)
			{
				CalendarAssistant.CachedStateTracer.TraceDebug((long)this.GetHashCode(), "{0}: Updated the cachedState object, but this is a resource mailbox.", new object[]
				{
					TraceContext.Get()
				});
				return;
			}
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
			if (calendarConfiguration.SkipProcessing)
			{
				CalendarAssistant.CachedStateTracer.TraceDebug((long)this.GetHashCode(), "{0}: SkipProcessing is set.", new object[]
				{
					TraceContext.Get()
				});
				return;
			}
			if (CalendarAssistant.ShouldIgnoreMessage(itemStore, item.ParentId, meetingMessage))
			{
				return;
			}
			string text = (string)Utils.SafeGetProperty(item, ItemSchema.InternetMessageId, "<null>");
			if (CalendarAssistant.ShouldProcessMessageClass(mapiEvent) && CalendarAssistant.ShouldProcessSeriesMessages(mapiEvent, itemStore))
			{
				bool flag2 = false;
				DateTime utcNow = DateTime.UtcNow;
				if (meetingMessage != null)
				{
					CalendarItemBase calendarItemBase = null;
					try
					{
						if (meetingMessage.IsDelegated())
						{
							this.calProcessor.OldMessageDeletion.PerformCleanUp(itemStore, item, calendarConfiguration, calendarItemBase, null);
						}
						else
						{
							IEnumerable<VersionedId> duplicates;
							bool calendarItem = CalendarAssistant.GetCalendarItem(meetingMessage, CalendarAssistant.UnexpectedPathTracer, ref calendarItemBase, true, out duplicates);
							if (calendarItem)
							{
								this.calProcessor.ProcessMeetingMessage(itemStore, meetingMessage, ref calendarItemBase, calendarConfiguration, duplicates, true, itemStore.MailboxOwner.IsResource.Value);
							}
						}
					}
					catch (OccurrenceCrossingBoundaryException ex)
					{
						CalendarAssistant.ProcessingTracer.TraceDebug<object, VersionedId>((long)meetingMessage.GetHashCode(), "{0}: Found an overlapping occurrence while processing message ID={1}. Cleaning things up and retrying", TraceContext.Get(), meetingMessage.Id);
						StringBuilder stringBuilder = new StringBuilder(1024);
						stringBuilder.AppendFormat("Found an overlapping occurrence while processing message ID={0}.", meetingMessage.Id);
						if (ex.OccurrenceInfo == null)
						{
							CalendarAssistant.UnexpectedPathTracer.TraceError((long)this.GetHashCode(), "{0}: Unexpected: Handling OccurrenceCrossingBoundaryException, the OccurrenceInfo is null.", new object[]
							{
								TraceContext.Get()
							});
							stringBuilder.Append("Unexpected: Handling OccurrenceCrossingBoundaryException, the OccurrenceInfo is null.");
						}
						else
						{
							VersionedId versionedId = ex.OccurrenceInfo.VersionedId;
							AggregateOperationResult aggregateOperationResult = meetingMessage.Session.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
							{
								versionedId
							});
							CalendarAssistant.ProcessingTracer.TraceDebug<object, OperationResult>((long)this.GetHashCode(), "{0}: Deleting the occurrence when handling an OccurrenceCrossingBoundaryException, returned:{1}", TraceContext.Get(), aggregateOperationResult.OperationResult);
							stringBuilder.AppendFormat("Deleting the occurrence when handling an OccurrenceCrossingBoundaryException, returned: {0}", aggregateOperationResult.OperationResult);
						}
						CalendarAssistantLog.LogEntry(itemStore, ex, false, stringBuilder.ToString(), new object[0]);
					}
					catch (CorruptDataException ex2)
					{
						CalendarAssistant.ProcessingTracer.TraceDebug<object, CorruptDataException>((long)this.GetHashCode(), "{0}: Got a corrupt message, exception data:{1}", TraceContext.Get(), ex2);
						CalendarAssistantLog.LogEntry(itemStore, ex2, false, "Got a corrupt message ID = {0}.", new object[]
						{
							meetingMessage.Id
						});
					}
					finally
					{
						if (calendarItemBase != null)
						{
							calendarItemBase.Dispose();
							calendarItemBase = null;
						}
					}
					flag2 = true;
				}
				else if (item is MeetingInquiryMessage)
				{
					MeetingInquiryMessage meetingInquiryMessage = (MeetingInquiryMessage)item;
					try
					{
						this.calProcessor.ProcessMeetingInquiryMessage(this.cvsGateway, itemStore, meetingInquiryMessage);
					}
					catch (CalendarVersionStoreNotPopulatedException ex3)
					{
						CalendarAssistant.ProcessingTracer.TraceWarning<object, CalendarVersionStoreNotPopulatedException>((long)this.GetHashCode(), "{0}: Failed to process an inquiry message, because the CVS is not populated yet. {1}", TraceContext.Get(), ex3);
						CalendarAssistantLog.LogEntry(itemStore, ex3, false, "Failed to process an inquiry message ID = {0}, because the CVS is not populated yet.", new object[]
						{
							meetingInquiryMessage.Id
						});
					}
					this.calProcessor.OldMessageDeletion.DeleteMessage(itemStore, item);
					flag2 = true;
				}
				else
				{
					CalendarAssistant.UnexpectedPathTracer.TraceError((long)this.GetHashCode(), "{0}: Unexpected: Message class matched, but is not the correct object type. Ignoring message.", new object[]
					{
						TraceContext.Get()
					});
					CalendarAssistantLog.LogEntry(itemStore, "Unexpected: Message class matched, but is not the correct object type. Ignoring message ID = {0}.", new object[]
					{
						item.Id
					});
				}
				if (flag2)
				{
					TimeSpan timeSpan = DateTime.UtcNow.Subtract(utcNow);
					CalendarAssistantPerformanceCounters.LastCalAssistantProcessingTime.RawValue = (long)timeSpan.TotalMilliseconds;
					CalendarAssistantPerformanceCounters.AverageCalAssistantProcessingTime.IncrementBy((long)timeSpan.TotalMilliseconds);
					CalendarAssistantPerformanceCounters.AverageCalAssistantProcessingTimeBase.Increment();
					CalendarAssistantPerformanceCounters.MeetingMessagesProcessed.Increment();
				}
				CalendarAssistant.GeneralTracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Finished processing message. InternetMessgeID = {1}", TraceContext.Get(), text);
				CalendarAssistant.TracerPfd.TracePfd<int, object, string>((long)this.GetHashCode(), "PFD IWC {0} {1}: Finished processing message. InternetMessgeID = {2}", 21655, TraceContext.Get(), text);
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00034F74 File Offset: 0x00033174
		private void HandlePermissionChange(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			if (mapiEvent.ItemEntryId == null || itemStore.IsGroupMailbox())
			{
				return;
			}
			StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(mapiEvent.ItemEntryId, StoreObjectType.Folder);
			StoreObjectId defaultFolderId = itemStore.GetDefaultFolderId(DefaultFolderType.Root);
			if (storeObjectId != null && defaultFolderId != null)
			{
				if (!storeObjectId.Equals(defaultFolderId) && !ObjectClass.IsCalendarFolder(mapiEvent.ObjectClass))
				{
					return;
				}
				using (Folder folder = Folder.Bind(itemStore, storeObjectId))
				{
					string text = CalendarLoggingHelper.GetCalendarPermissionsLog(itemStore, folder);
					if (!string.IsNullOrEmpty(text))
					{
						text = string.Format("Client Type: {0} New Permission Table:\n{1}", mapiEvent.ClientType, text);
					}
					else
					{
						text = string.Format("Client Type: {0} Mailbox session or folder was null", mapiEvent.ClientType);
					}
					CalendarPermissionsLog.LogEntry(itemStore, text, new object[0]);
					return;
				}
			}
			if (storeObjectId == null)
			{
				CalendarAssistant.GeneralTracer.TraceError<object, byte[]>((long)this.GetHashCode(), "{0}: Unable to bind to a calendar folder with FolderId {1}", TraceContext.Get(), mapiEvent.ItemEntryId);
				return;
			}
			CalendarAssistant.GeneralTracer.TraceError<object, Guid>((long)this.GetHashCode(), "{0}: Unable to bind to the root folder for Mailbox {1}", TraceContext.Get(), mapiEvent.MailboxGuid);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0003507C File Offset: 0x0003327C
		private void HandleStoreEvent(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			using (Folder folder = Folder.Bind(itemStore, DefaultFolderType.Calendar))
			{
				string text = CalendarLoggingHelper.GetCalendarPermissionsLog(itemStore, folder);
				if (!string.IsNullOrEmpty(text))
				{
					text = string.Format("Client Type: {0} Move Status:{1} Permission Table:\n{2}", mapiEvent.ClientType, mapiEvent.EventMask, text);
				}
				else
				{
					text = string.Format("Client Type: {0} Mailbox session was null", mapiEvent.ClientType);
				}
				CalendarPermissionsLog.LogEntry(itemStore, text, new object[0]);
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00035104 File Offset: 0x00033304
		private bool GetDelegatesRuleLoggerStatus(MailboxSession session)
		{
			return session != null && session.MailboxOwner != null && !session.IsGroupMailbox() && this.IsDelegateRulesLoggerEnabled(session.MailboxOwner);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00035128 File Offset: 0x00033328
		private bool IsDelegateRulesLoggerEnabled(IExchangePrincipal exchangePrincipal)
		{
			VariantConfigurationSnapshot configuration = exchangePrincipal.GetConfiguration();
			return configuration.MailboxAssistants.DelegateRulesLogger.Enabled;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00035150 File Offset: 0x00033350
		private static bool ShouldProcessMessageClass(MapiEvent mapiEvent)
		{
			return mapiEvent.EventMask.Contains(MapiEventTypeFlags.NewMail) && (ObjectClass.IsMeetingMessage(mapiEvent.ObjectClass) || ObjectClass.IsMeetingForwardNotification(mapiEvent.ObjectClass) || ObjectClass.IsMeetingInquiry(mapiEvent.ObjectClass) || ObjectClass.IsMeetingRequestSeries(mapiEvent.ObjectClass) || ObjectClass.IsMeetingCancellationSeries(mapiEvent.ObjectClass));
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x000351B0 File Offset: 0x000333B0
		private static bool ShouldProcessSeriesMessages(MapiEvent mapiEvent, MailboxSession session)
		{
			return !ObjectClass.IsMeetingMessageSeries(mapiEvent.ObjectClass) || (session.MailboxOwner != null && session.MailboxOwner.GetConfiguration().MailboxTransport.OrderSeriesMeetingMessages.Enabled);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000351F4 File Offset: 0x000333F4
		internal static bool GetCalendarItem(MeetingMessage mtgMessage, Trace tracer, ref CalendarItemBase originalCalItem, bool shouldDetectDuplicateIds, out IEnumerable<VersionedId> detectedDuplicatesIds)
		{
			Exception ex;
			return CalendarAssistant.GetCalendarItem(mtgMessage, tracer, ref originalCalItem, shouldDetectDuplicateIds, out detectedDuplicatesIds, out ex);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00035210 File Offset: 0x00033410
		internal static bool GetCalendarItem(MeetingMessage mtgMessage, Trace tracer, ref CalendarItemBase originalCalItem, bool shouldDetectDuplicateIds, out IEnumerable<VersionedId> detectedDuplicatesIds, out Exception exception)
		{
			MailboxSession session = mtgMessage.Session as MailboxSession;
			bool result = true;
			exception = null;
			originalCalItem = null;
			try
			{
				originalCalItem = mtgMessage.GetCorrelatedItem(shouldDetectDuplicateIds, out detectedDuplicatesIds);
			}
			catch (ObjectNotFoundException ex)
			{
				detectedDuplicatesIds = null;
				result = false;
				exception = ex;
			}
			catch (CorruptDataException ex2)
			{
				tracer.TraceError<object, VersionedId>(0L, "{0}: There was an error opening the original CalendarItem associated with this message {1}. This message will be skipped.", TraceContext.Get(), mtgMessage.Id);
				CalendarAssistantLog.LogEntry(session, ex2, false, "There was an error opening the original CalendarItem associated with this message {0}. This message will be skipped.", new object[]
				{
					mtgMessage.Id
				});
				detectedDuplicatesIds = null;
				result = false;
				exception = ex2;
			}
			catch (CorrelationFailedException ex3)
			{
				tracer.TraceError<object, VersionedId>(0L, "{0}: The original CalendarItem associated with this message {1} is a master, but this message is a single occurrence. This message will be skipped.", TraceContext.Get(), mtgMessage.Id);
				CalendarAssistantLog.LogEntry(session, ex3, false, "The original CalendarItem associated with this message {0} is a master, but this message is a single occurrence. This message will be skipped.", new object[]
				{
					mtgMessage.Id
				});
				detectedDuplicatesIds = null;
				result = false;
				exception = ex3;
			}
			catch (VirusDetectedException ex4)
			{
				tracer.TraceError<object, VersionedId>(0L, "{0}: A virus was detected in the CalendarItem associated with this message {1}. This message will be skipped.", TraceContext.Get(), mtgMessage.Id);
				CalendarAssistantLog.LogEntry(session, ex4, false, "A virus was detected in the CalendarItem associated with this message {0}. This message will be skipped.", new object[]
				{
					mtgMessage.Id
				});
				detectedDuplicatesIds = null;
				result = false;
				exception = ex4;
			}
			catch (VirusMessageDeletedException ex5)
			{
				tracer.TraceError<object, VersionedId>(0L, "{0}: A virus was detected in the CalendarItem associated with this message and was deleted {1}. This message will be skipped.", TraceContext.Get(), mtgMessage.Id);
				CalendarAssistantLog.LogEntry(session, ex5, false, "A virus was detected in the CalendarItem associated with this message and was deleted {0}. This message will be skipped.", new object[]
				{
					mtgMessage.Id
				});
				detectedDuplicatesIds = null;
				result = false;
				exception = ex5;
			}
			catch (RecurrenceException ex6)
			{
				tracer.TraceError<object, VersionedId, RecurrenceException>(0L, "{0}: There was a problem with the recurrence blob. Messageid={1}. This message will be skipped. Exception {2}", TraceContext.Get(), mtgMessage.Id, ex6);
				CalendarAssistantLog.LogEntry(session, ex6, false, "There was a problem with the recurrence blob. Messageid={0}. This message will be skipped", new object[]
				{
					mtgMessage.Id
				});
				detectedDuplicatesIds = null;
				result = false;
				exception = ex6;
			}
			if (originalCalItem == null)
			{
				tracer.TraceDebug<object, VersionedId>(0L, "{0}: Original CalendarItem is null for message {1}.", TraceContext.Get(), mtgMessage.Id);
			}
			return result;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00035430 File Offset: 0x00033630
		internal static bool IsDumpsterFolder(MailboxSession itemStore, StoreObjectId folderId)
		{
			bool result;
			using (COWSession cowsession = COWSession.Create(itemStore))
			{
				result = cowsession.IsDumpsterFolder(itemStore, folderId);
			}
			return result;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0003546C File Offset: 0x0003366C
		private static bool ShouldIgnoreMessage(MailboxSession itemStore, StoreObjectId folderId, MeetingMessage mtgMessage)
		{
			foreach (DefaultFolderType defaultFolderType in CalendarAssistant.FoldersToIgnore)
			{
				StoreObjectId defaultFolderId = itemStore.GetDefaultFolderId(defaultFolderType);
				if (defaultFolderId != null && defaultFolderId.Equals(folderId))
				{
					return true;
				}
			}
			StoreObjectId defaultFolderId2 = itemStore.GetDefaultFolderId(DefaultFolderType.DeletedItems);
			if (defaultFolderId2 != null && defaultFolderId2.Equals(folderId) && mtgMessage != null && mtgMessage is MeetingRequest)
			{
				return !OldMessageDeletion.IsSelfForwardedEvent(mtgMessage, itemStore);
			}
			return CalendarAssistant.IsDumpsterFolder(itemStore, folderId);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000354E4 File Offset: 0x000336E4
		private Rule GetDelegateRule(MapiEvent mapiEvent, MailboxSession mailboxsession)
		{
			using (Folder folder = Folder.Bind(mailboxsession, mailboxsession.GetDefaultFolderId(DefaultFolderType.Inbox)))
			{
				if (folder.MapiFolder != null)
				{
					Rule[] rules = folder.MapiFolder.GetRules(new PropTag[0]);
					if (rules != null)
					{
						foreach (Rule rule in rules)
						{
							if (Encoding.Default.GetString(rule.IDx).Equals(Encoding.Default.GetString(mapiEvent.ItemEntryId)) && string.Compare(rule.Provider, DelegateUserCollection.DelegateRuleProvider, StringComparison.OrdinalIgnoreCase) == 0)
							{
								return rule;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x000355FC File Offset: 0x000337FC
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00035604 File Offset: 0x00033804
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0003560C File Offset: 0x0003380C
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000574 RID: 1396
		public const string MeetingMessageClassPrefix = "IPM.Schedule.Meeting";

		// Token: 0x04000575 RID: 1397
		public const string MeetingResponsePrefix = "IPM.Schedule.Meeting.Resp";

		// Token: 0x04000576 RID: 1398
		private const string delegateDeletionLog = "Client Type: {0} \nEvent: {1} \nMapiEvent ItemEntryID : {2}\n";

		// Token: 0x04000577 RID: 1399
		private const string delegateCreationLog = "Client Type: {0} \nEvent: {1} \nTotal Inbox Rules count: {2} \nDelegate RuleName: Delegate Rule {3}, Delegate RuleProvider: {4} \nDelegates added for {5}: {6} \nDelegate can see Private Items: {7} \nDelegate RuleType for {8}: {9} \nMapiEvent ItemEntryID : {10}\n";

		// Token: 0x04000578 RID: 1400
		private static DefaultFolderType[] FoldersToIgnore = new DefaultFolderType[]
		{
			DefaultFolderType.JunkEmail
		};

		// Token: 0x04000579 RID: 1401
		private CalendarProcessing calProcessor;

		// Token: 0x0400057A RID: 1402
		private CalendarVersionStoreGateway cvsGateway;

		// Token: 0x0400057B RID: 1403
		private static readonly Trace GeneralTracer = ExTraceGlobals.GeneralTracer;

		// Token: 0x0400057C RID: 1404
		private static readonly Trace UnexpectedPathTracer = ExTraceGlobals.UnexpectedPathTracer;

		// Token: 0x0400057D RID: 1405
		private static readonly Trace CalendarItemValuesTracer = ExTraceGlobals.CalendarItemValuesTracer;

		// Token: 0x0400057E RID: 1406
		private static readonly Trace ProcessingTracer = ExTraceGlobals.ProcessingTracer;

		// Token: 0x0400057F RID: 1407
		private static readonly Trace ProcessingRequestTracer = ExTraceGlobals.ProcessingRequestTracer;

		// Token: 0x04000580 RID: 1408
		private static readonly Trace CachedStateTracer = ExTraceGlobals.CachedStateTracer;

		// Token: 0x04000581 RID: 1409
		internal static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;
	}
}
