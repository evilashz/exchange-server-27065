using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyTranslationRules;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.Translators;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.Calendaring.CustomActions;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.EntitySets;
using Microsoft.Exchange.Entities.Serialization;
using Microsoft.Exchange.Entities.TypeConversion.Converters;
using Microsoft.Exchange.Entities.TypeConversion.Translators;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.Calendaring.DataProviders
{
	// Token: 0x02000020 RID: 32
	internal class EventDataProvider : StorageItemDataProvider<IStoreSession, Event, ICalendarItemBase>
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00003760 File Offset: 0x00001960
		public EventDataProvider(IStorageEntitySetScope<IStoreSession> scope, StoreId calendarFolderId) : base(scope, calendarFolderId, ExTraceGlobals.EventDataProviderTracer)
		{
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000376F File Offset: 0x0000196F
		protected override IStorageTranslator<ICalendarItemBase, Event> Translator
		{
			get
			{
				return EventTranslator.Instance;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003776 File Offset: 0x00001976
		private static Trace InstanceQueryTrace
		{
			get
			{
				return ExTraceGlobals.InstancesQueryTracer;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003780 File Offset: 0x00001980
		public static ExDateTime EnforceMidnightTime(ExDateTime time, MidnightEnforcementOption option = MidnightEnforcementOption.Throw)
		{
			if (!(time != time.Date))
			{
				return time;
			}
			switch (option)
			{
			case MidnightEnforcementOption.Throw:
				throw new InvalidRequestException(CalendaringStrings.ErrorAllDayTimesMustBeMidnight);
			case MidnightEnforcementOption.MoveBackward:
				return time.Date;
			case MidnightEnforcementOption.MoveForward:
				return time.Date.AddDays(1.0);
			default:
				throw new ArgumentOutOfRangeException("option");
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000037EC File Offset: 0x000019EC
		public virtual IEnumerable<Event> GetCalendarView(ExDateTime startTime, ExDateTime endTime, bool includeSeriesMasters, params Microsoft.Exchange.Data.PropertyDefinition[] propertiesToLoad)
		{
			IEnumerable<Event> result;
			using (ICalendarFolder calendarFolder = base.XsoFactory.BindToCalendarFolder(base.Session, base.ContainerFolderId))
			{
				Dictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices = this.GetPropertyIndices(propertiesToLoad);
				object[][] syncView = calendarFolder.GetSyncView(startTime, endTime, CalendarViewBatchingStrategy.CreateNoneBatchingInstance(), propertiesToLoad, true);
				IEnumerable<Event> enumerable = this.ReadQueryResults(syncView, propertyIndices);
				result = (includeSeriesMasters ? enumerable : this.FilterOutType(enumerable, EventType.SeriesMaster));
			}
			return result;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003864 File Offset: 0x00001A64
		public virtual void RespondToEvent(StoreId id, RespondToEventParameters parameters, Event updateToEvent = null)
		{
			if (parameters == null)
			{
				throw new InvalidRequestException(CalendaringStrings.ErrorMissingRequiredRespondParameter);
			}
			MeetingResponse meetingResponse = null;
			try
			{
				try
				{
					using (ICalendarItemBase calendarItemBase = this.Bind(id))
					{
						calendarItemBase.OpenAsReadWrite();
						if (updateToEvent != null)
						{
							EventTranslator.Instance.SetPropertiesFromEntityOnStorageObject(updateToEvent, calendarItemBase);
						}
						meetingResponse = calendarItemBase.RespondToMeetingRequest(default(ResponseTypeConverter).Convert(parameters.Response), true, true, parameters.ProposedStartTime, parameters.ProposedEndTime);
					}
				}
				catch (ObjectNotFoundException innerException)
				{
					throw new AccessDeniedException(Strings.ErrorAccessDenied, innerException);
				}
				if (parameters.SendResponse)
				{
					EventWorkflowParametersTranslator<RespondToEventParameters, RespondToEventParametersSchema>.Instance.SetPropertiesFromEntityOnStorageObject(parameters, meetingResponse);
					MeetingMessage.SendLocalOrRemote(meetingResponse, true, true);
				}
			}
			finally
			{
				if (meetingResponse != null)
				{
					meetingResponse.Dispose();
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003938 File Offset: 0x00001B38
		public override Event ConvertToEntity(ICalendarItemBase storeObject)
		{
			ICalendarItem calendarItem = storeObject as ICalendarItem;
			if (calendarItem == null || string.IsNullOrEmpty(storeObject.SeriesId))
			{
				return base.ConvertToEntity(storeObject);
			}
			Event result;
			using (ICalendarItemBase calendarItemBase = this.BindToMasterFromInstance(calendarItem))
			{
				result = ((calendarItemBase != null) ? this.Translator.ConvertToEntity(calendarItemBase, storeObject) : base.ConvertToEntity(storeObject));
			}
			return result;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000039A4 File Offset: 0x00001BA4
		public void CancelEvent(StoreId id, CancelEventParameters parameters, int? seriesSequenceNumber = null, bool deleteAfterCancelling = true, Event updateToEvent = null, byte[] masterGoid = null)
		{
			using (ICalendarItemBase calendarItemBase = this.Bind(id))
			{
				if (calendarItemBase.IsMeeting && !calendarItemBase.IsOrganizer())
				{
					throw new InvalidRequestException(CalendaringStrings.ErrorNotAuthorizedToCancel);
				}
				calendarItemBase.OpenAsReadWrite();
				if (updateToEvent != null)
				{
					EventTranslator.Instance.SetPropertiesFromEntityOnStorageObject(updateToEvent, calendarItemBase);
				}
				if (calendarItemBase.IsMeeting && calendarItemBase.AttendeeCollection != null && calendarItemBase.AttendeeCollection.Count > 0)
				{
					using (MeetingCancellation meetingCancellation = ((CalendarItemBase)calendarItemBase).CancelMeeting(seriesSequenceNumber, masterGoid))
					{
						if (parameters != null)
						{
							EventWorkflowParametersTranslator<CancelEventParameters, CancelEventParametersSchema>.Instance.SetPropertiesFromEntityOnStorageObject(parameters, meetingCancellation);
						}
						MeetingMessage.SendLocalOrRemote(meetingCancellation, true, true);
						goto IL_95;
					}
				}
				if (updateToEvent != null)
				{
					calendarItemBase.SaveWithConflictCheck(SaveMode.ResolveConflicts);
				}
				IL_95:;
			}
			if (deleteAfterCancelling)
			{
				this.Delete(id, DeleteItemFlags.MoveToDeletedItems | DeleteItemFlags.CancelCalendarItem);
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003A80 File Offset: 0x00001C80
		public bool ForEachSeriesItem(Event theEvent, Func<Event, bool> instanceAction, Func<IStorePropertyBag, Event> convertToEvent, Action<Event> seriesAction = null, SortBy sortOrder = null, Microsoft.Exchange.Data.PropertyDefinition identityProperty = null, params Microsoft.Exchange.Data.PropertyDefinition[] additionalPropertiesToQuery)
		{
			if (theEvent.Type != EventType.SeriesMaster)
			{
				throw new InvalidOperationException("You can only call this method for a series master");
			}
			foreach (Event @event in this.GetSeriesEventsData(theEvent, identityProperty, convertToEvent, sortOrder, additionalPropertiesToQuery))
			{
				if (@event.Type == EventType.SeriesMaster)
				{
					if (seriesAction != null)
					{
						seriesAction(@event);
					}
				}
				else if (instanceAction != null && !instanceAction(@event))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003B24 File Offset: 0x00001D24
		public string CreateOccurrenceViewPropertiesBlob(Event master)
		{
			List<Event> occurrencesWithViewProperties = new List<Event>();
			this.ForEachSeriesItem(master, delegate(Event instance)
			{
				occurrencesWithViewProperties.Add(instance);
				return true;
			}, new Func<IStorePropertyBag, Event>(this.GetOccurrenceWithViewProperties), null, null, CalendarItemBaseSchema.SeriesId, new Microsoft.Exchange.Data.PropertyDefinition[]
			{
				CalendarItemInstanceSchema.StartTime,
				CalendarItemInstanceSchema.EndTime,
				ItemSchema.Subject,
				CalendarItemBaseSchema.Location,
				ItemSchema.Sensitivity,
				CalendarItemBaseSchema.IsAllDayEvent,
				CalendarItemBaseSchema.GlobalObjectId
			});
			return EntitySerializer.Serialize<List<Event>>(occurrencesWithViewProperties);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003BB8 File Offset: 0x00001DB8
		public void ForwardEvent(StoreId id, ForwardEventParameters parameters, Event updateToEvent = null, int? seriesSequenceNumber = null, string occurrencesViewPropertiesBlob = null, CommandContext commandContext = null)
		{
			using (ICalendarItemBase calendarItemBase = this.Bind(id))
			{
				if (updateToEvent != null)
				{
					calendarItemBase.OpenAsReadWrite();
					this.UpdateOnly(updateToEvent, calendarItemBase, base.GetSaveMode(updateToEvent.ChangeKey, commandContext));
				}
				CalendarItemBase calendarItemBase2 = (CalendarItemBase)calendarItemBase;
				BodyFormat targetFormat = BodyFormat.TextPlain;
				if (parameters != null && parameters.Notes != null)
				{
					targetFormat = parameters.Notes.ContentType.ToStorageType();
				}
				ReplyForwardConfiguration replyForwardParameters = new ReplyForwardConfiguration(targetFormat)
				{
					ShouldSuppressReadReceipt = false
				};
				MailboxSession mailboxSession = base.Session as MailboxSession;
				using (MessageItem messageItem = calendarItemBase2.CreateForward(mailboxSession, CalendarItemBase.GetDraftsFolderIdOrThrow(mailboxSession), replyForwardParameters, seriesSequenceNumber, occurrencesViewPropertiesBlob))
				{
					EventWorkflowParametersTranslator<ForwardEventParameters, ForwardEventParametersSchema>.Instance.SetPropertiesFromEntityOnStorageObject(parameters, messageItem);
					foreach (Recipient<RecipientSchema> recipient in parameters.Forwardees)
					{
						messageItem.Recipients.Add(new Participant(recipient.Name, recipient.EmailAddress, "SMTP"));
					}
					MeetingMessage.SendLocalOrRemote(messageItem, true, true);
				}
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public override void Validate(Event entity, bool isNew)
		{
			if (entity.IsPropertySet(entity.Schema.PopupReminderSettingsProperty))
			{
				this.ValidatePopupReminderSettings(entity, isNew);
			}
			base.Validate(entity, isNew);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003D1C File Offset: 0x00001F1C
		public virtual ICalendarItemBase BindToMasterFromSeriesId(string seriesId)
		{
			StoreId seriesMasterIdFromSeriesId = this.GetSeriesMasterIdFromSeriesId(seriesId);
			if (seriesMasterIdFromSeriesId == null)
			{
				return null;
			}
			return this.Bind(seriesMasterIdFromSeriesId);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003D40 File Offset: 0x00001F40
		public override void Delete(StoreId id, DeleteItemFlags flags)
		{
			base.Delete(id, flags);
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(id);
			if (storeObjectId is OccurrenceStoreObjectId)
			{
				this.TryLogCalendarEventActivity(ActivityId.UpdateCalendarEvent, storeObjectId);
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003D6D File Offset: 0x00001F6D
		internal void TryLogCalendarEventActivity(ActivityId activityId, StoreObjectId itemId)
		{
			if (base.Session.ActivitySession != null && this.ShouldLogActivityForAggregation())
			{
				base.Session.ActivitySession.CaptureCalendarEventActivity(activityId, itemId);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003D98 File Offset: 0x00001F98
		internal bool TryGetPropertyFromPropertyIndices(IDictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices, IList values, Microsoft.Exchange.Data.PropertyDefinition property, out object value)
		{
			int index;
			object obj;
			if (propertyIndices.TryGetValue(property, out index) && (obj = values[index]) != null && obj.GetType() == property.Type)
			{
				value = obj;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003DD9 File Offset: 0x00001FD9
		protected internal override ICalendarItemBase BindToStoreObject(StoreId id)
		{
			return base.XsoFactory.BindToCalendarItemBase(base.Session, id, null);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003DEE File Offset: 0x00001FEE
		protected virtual bool ShouldLogActivityForAggregation()
		{
			return base.Session.MailboxOwner.MailboxInfo.IsAggregated;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003E08 File Offset: 0x00002008
		protected virtual ICalendarItemBase BindToMasterFromInstance(ICalendarItem instance)
		{
			ICalendarItemBase result;
			if (!this.TryBindToMasterFromSeriesMasterId(instance, out result))
			{
				result = this.BindToMasterFromSeriesId(instance.SeriesId);
			}
			return result;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003E4C File Offset: 0x0000204C
		protected virtual IEnumerable<Event> FilterOutType(IEnumerable<Event> events, EventType type)
		{
			return from theEvent in events
			where theEvent.Type != type
			select theEvent;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003E78 File Offset: 0x00002078
		protected virtual bool TryBindToMasterFromSeriesMasterId(ICalendarItem instance, out ICalendarItemBase master)
		{
			master = null;
			StoreId id;
			if (!this.TryGetSeriesMasterId(instance, out id))
			{
				return false;
			}
			bool result;
			try
			{
				master = this.Bind(id);
				result = true;
			}
			catch (ObjectNotFoundException arg)
			{
				base.Trace.TraceError<ObjectNotFoundException>((long)this.GetHashCode(), "Error while binding master based on SeriesMasterId. {0}", arg);
				result = false;
			}
			return result;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003ED0 File Offset: 0x000020D0
		protected virtual bool TryGetSeriesMasterId(ICalendarItem item, out StoreId seriesMasterId)
		{
			string text;
			if (EventTranslator.SeriesMasterIdRule.TryGetValue(item, out text) && !string.IsNullOrEmpty(text))
			{
				seriesMasterId = base.IdConverter.ToStoreObjectId(text);
				return true;
			}
			seriesMasterId = null;
			return false;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003F08 File Offset: 0x00002108
		protected override ICalendarItemBase CreateNewStoreObject()
		{
			return base.XsoFactory.CreateCalendarItem(base.Session, base.ContainerFolderId);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003F40 File Offset: 0x00002140
		protected virtual StoreId GetSeriesMasterIdFromSeriesId(string seriesId)
		{
			Event theEvent = new Event
			{
				SeriesId = seriesId,
				Type = EventType.SeriesMaster
			};
			Event masterFromStore = null;
			this.ForEachSeriesItem(theEvent, null, (IStorePropertyBag bag) => EventExtensions.GetBasicSeriesEventData(bag, base.Scope), delegate(Event master)
			{
				masterFromStore = master;
			}, null, null, new Microsoft.Exchange.Data.PropertyDefinition[0]);
			if (masterFromStore == null)
			{
				return null;
			}
			return base.IdConverter.ToStoreObjectId(masterFromStore.Id);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003FC0 File Offset: 0x000021C0
		protected override void ValidateChanges(Event sourceEntity, ICalendarItemBase targetStoreObject)
		{
			bool flag;
			if (sourceEntity.IsPropertySet(sourceEntity.Schema.IsAllDayProperty))
			{
				flag = sourceEntity.IsAllDay;
				if (flag != targetStoreObject.IsAllDayEvent)
				{
					sourceEntity.ThrowIfPropertyNotSet(sourceEntity.Schema.StartProperty);
					sourceEntity.ThrowIfPropertyNotSet(sourceEntity.Schema.EndProperty);
				}
			}
			else
			{
				flag = targetStoreObject.IsAllDayEvent;
			}
			if (flag)
			{
				if (sourceEntity.IsPropertySet(sourceEntity.Schema.StartProperty))
				{
					EventDataProvider.EnforceMidnightTime(sourceEntity.Start, MidnightEnforcementOption.Throw);
					ExTimeZone exTimeZone;
					if (sourceEntity.IsPropertySet(sourceEntity.Schema.EndProperty))
					{
						EventDataProvider.EnforceMidnightTime(sourceEntity.End, MidnightEnforcementOption.Throw);
						exTimeZone = sourceEntity.End.TimeZone;
					}
					else
					{
						exTimeZone = targetStoreObject.EndTimeZone;
					}
					if (sourceEntity.Start.TimeZone != exTimeZone)
					{
						throw new InvalidRequestException(CalendaringStrings.ErrorAllDayTimeZoneMismatch);
					}
				}
				else if (sourceEntity.IsPropertySet(sourceEntity.Schema.EndProperty))
				{
					EventDataProvider.EnforceMidnightTime(sourceEntity.End, MidnightEnforcementOption.Throw);
					if (sourceEntity.End.TimeZone != targetStoreObject.StartTimeZone)
					{
						throw new InvalidRequestException(CalendaringStrings.ErrorAllDayTimeZoneMismatch);
					}
				}
			}
			base.ValidateChanges(sourceEntity, targetStoreObject);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004108 File Offset: 0x00002308
		protected override IEnumerable<Event> ReadQueryResults(object[][] rows, Dictionary<Microsoft.Exchange.Data.PropertyDefinition, int> propertyIndices)
		{
			List<Event> list = new List<Event>();
			Dictionary<string, Event> dictionary = new Dictionary<string, Event>();
			foreach (object[] values in rows)
			{
				Event @event = this.Translator.ConvertToEntity(propertyIndices, values, base.Session);
				list.Add(@event);
				object obj;
				if (@event.Type == EventType.SeriesMaster && this.TryGetPropertyFromPropertyIndices(propertyIndices, values, CalendarItemBaseSchema.SeriesId, out obj) && !dictionary.ContainsKey(@event.SeriesId))
				{
					dictionary.Add(@event.SeriesId, @event);
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				Event event2 = list[j];
				Event master2;
				if (event2.Type == EventType.Exception && dictionary.TryGetValue(event2.SeriesId, out master2))
				{
					IList<string> overridenProperties = new List<string>();
					object obj2;
					if (this.TryGetPropertyFromPropertyIndices(propertyIndices, rows[j], CalendarItemInstanceSchema.PropertyChangeMetadataRaw, out obj2))
					{
						PropertyChangeMetadata metadata = PropertyChangeMetadata.Parse((byte[])obj2);
						overridenProperties = EventTranslator.Instance.GetOverridenProperties(metadata);
					}
					master2.CopyMasterPropertiesTo(event2, false, (Event occurrence, Event master, Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition property) => master.IsPropertySet(property) && !overridenProperties.Contains(property.Name), false);
				}
			}
			return list;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004230 File Offset: 0x00002430
		private static ComparisonFilter CreateComparisonFilterForSeriesSearch(Event entity, Microsoft.Exchange.Data.PropertyDefinition identityProperty)
		{
			ComparisonFilter result;
			if (identityProperty.Equals(CalendarItemBaseSchema.SeriesId))
			{
				result = new ComparisonFilter(ComparisonOperator.Equal, CalendarItemBaseSchema.SeriesId, entity.SeriesId);
			}
			else
			{
				if (!identityProperty.Equals(CalendarItemBaseSchema.EventClientId))
				{
					throw new ArgumentOutOfRangeException("identityProperty");
				}
				result = new ComparisonFilter(ComparisonOperator.Equal, CalendarItemBaseSchema.EventClientId, entity.ClientId);
			}
			return result;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000428C File Offset: 0x0000248C
		private static SortBy[] CreateSortOrderForSeriesSearch(Microsoft.Exchange.Data.PropertyDefinition identityProperty, SortBy additionalSortBy)
		{
			List<SortBy> list = new List<SortBy>(2)
			{
				new SortBy(identityProperty, SortOrder.Ascending)
			};
			if (additionalSortBy != null)
			{
				list.Add(additionalSortBy);
			}
			return list.ToArray();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000042BF File Offset: 0x000024BF
		private static bool IsSeriesIdMatching(string seriesId, IStorePropertyBag rowPropertyBag, Microsoft.Exchange.Data.PropertyDefinition identityProperty)
		{
			return string.Equals(rowPropertyBag.GetValueOrDefault<string>(identityProperty, null), seriesId, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000042D0 File Offset: 0x000024D0
		private void ValidatePopupReminderSettings(Event entity, bool isNew)
		{
			IList<EventPopupReminderSetting> popupReminderSettings = entity.PopupReminderSettings;
			if (popupReminderSettings == null)
			{
				throw new NullPopupReminderSettingsException();
			}
			if (popupReminderSettings.Count != 1)
			{
				throw new InvalidPopupReminderSettingsCountException(popupReminderSettings.Count);
			}
			EventPopupReminderSetting eventPopupReminderSetting = popupReminderSettings[0];
			if (eventPopupReminderSetting == null)
			{
				throw new NullPopupReminderSettingsException();
			}
			if (isNew)
			{
				if (!string.IsNullOrEmpty(eventPopupReminderSetting.Id))
				{
					throw new InvalidNewReminderSettingIdException();
				}
			}
			else if (eventPopupReminderSetting.Id != EventPopupReminderSettingsRules.GetDefaultPopupReminderSettingId(entity))
			{
				throw new InvalidReminderSettingIdException();
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000047AC File Offset: 0x000029AC
		private IEnumerable<Event> GetSeriesEventsData(Event theEvent, Microsoft.Exchange.Data.PropertyDefinition identityProperty, Func<IStorePropertyBag, Event> convertToEvent, SortBy sortOrder, params Microsoft.Exchange.Data.PropertyDefinition[] additionalPropertiesToQuery)
		{
			if (identityProperty == null)
			{
				identityProperty = CalendarItemBaseSchema.SeriesId;
			}
			ComparisonFilter condition = EventDataProvider.CreateComparisonFilterForSeriesSearch(theEvent, identityProperty);
			string identityBeingSearched = condition.PropertyValue.ToString();
			SortBy[] internalSortOrder = EventDataProvider.CreateSortOrderForSeriesSearch(condition.Property, sortOrder);
			ICollection<Microsoft.Exchange.Data.PropertyDefinition> propertiesToQuery = new List<Microsoft.Exchange.Data.PropertyDefinition>(EventDataProvider.InstanceRequiredPropertiesToQuery);
			propertiesToQuery.Add(identityProperty);
			if (additionalPropertiesToQuery != null)
			{
				propertiesToQuery = propertiesToQuery.Union(additionalPropertiesToQuery);
			}
			using (ICalendarFolder calendarFolder = base.XsoFactory.BindToCalendarFolder(base.Session, base.ContainerFolderId))
			{
				using (IQueryResult queryResult = calendarFolder.IItemQuery(ItemQueryType.None, null, internalSortOrder, propertiesToQuery))
				{
					queryResult.SeekToCondition(SeekReference.OriginBeginning, condition);
					for (;;)
					{
						IStorePropertyBag[] rows = queryResult.GetPropertyBags(50);
						EventDataProvider.InstanceQueryTrace.TraceDebug<int, string>((long)theEvent.GetHashCode(), "EventExtension::GetDataForSeries retrieved {0} instances for series {1}.", rows.Length, identityBeingSearched);
						foreach (IStorePropertyBag rowPropertyBag in rows)
						{
							string itemClass = rowPropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, null);
							if (ObjectClass.IsCalendarItem(itemClass) || ObjectClass.IsCalendarItemSeries(itemClass))
							{
								if (!EventDataProvider.IsSeriesIdMatching(identityBeingSearched, rowPropertyBag, condition.Property))
								{
									goto IL_211;
								}
								yield return convertToEvent(rowPropertyBag);
							}
							else
							{
								EventDataProvider.InstanceQueryTrace.TraceDebug<string>((long)theEvent.GetHashCode(), "EventExtension::GetDataForSeries will skip item with class {0}.", itemClass);
							}
						}
						if (rows.Length <= 0)
						{
							goto Block_10;
						}
					}
					IL_211:
					EventDataProvider.InstanceQueryTrace.TraceDebug<string>((long)theEvent.GetHashCode(), "EventExtension::GetDataForSeries found all the items related to series {0} and will return.", identityBeingSearched);
					yield break;
					Block_10:;
				}
			}
			yield break;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000047F0 File Offset: 0x000029F0
		private Event GetOccurrenceWithViewProperties(IStorePropertyBag propertyBag)
		{
			Event basicSeriesEventData = EventExtensions.GetBasicSeriesEventData(propertyBag, base.Scope);
			basicSeriesEventData.Start = propertyBag.GetValueOrDefault<ExDateTime>(CalendarItemInstanceSchema.StartTime, ExDateTime.MinValue);
			basicSeriesEventData.End = propertyBag.GetValueOrDefault<ExDateTime>(CalendarItemInstanceSchema.EndTime, ExDateTime.MaxValue);
			basicSeriesEventData.Subject = propertyBag.GetValueOrDefault<string>(ItemSchema.Subject, string.Empty);
			basicSeriesEventData.Location = new Location
			{
				DisplayName = propertyBag.GetValueOrDefault<string>(CalendarItemBaseSchema.Location, string.Empty)
			};
			basicSeriesEventData.Sensitivity = default(SensitivityConverter).StorageToEntitiesConverter.Convert(propertyBag.GetValueOrDefault<Microsoft.Exchange.Data.Storage.Sensitivity>(ItemSchema.Sensitivity, Microsoft.Exchange.Data.Storage.Sensitivity.Normal));
			basicSeriesEventData.IsAllDay = propertyBag.GetValueOrDefault<bool>(CalendarItemBaseSchema.IsAllDayEvent, false);
			IEventInternal eventInternal = basicSeriesEventData;
			eventInternal.GlobalObjectId = new GlobalObjectId(propertyBag.GetValueOrDefault<byte[]>(CalendarItemBaseSchema.GlobalObjectId, null)).ToString();
			return basicSeriesEventData;
		}

		// Token: 0x0400004D RID: 77
		internal const DeleteItemFlags DeleteFlags = DeleteItemFlags.MoveToDeletedItems | DeleteItemFlags.CancelCalendarItem;

		// Token: 0x0400004E RID: 78
		private static readonly Microsoft.Exchange.Data.PropertyDefinition[] InstanceRequiredPropertiesToQuery = new Microsoft.Exchange.Data.PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			CalendarItemBaseSchema.SeriesId
		};
	}
}
