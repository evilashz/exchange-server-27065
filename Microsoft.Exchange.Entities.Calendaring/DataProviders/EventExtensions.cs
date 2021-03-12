using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.Calendaring.TypeConversion.PropertyTranslationRules;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.EntitySets;
using Microsoft.Exchange.Entities.TypeConversion.Converters;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.Calendaring.DataProviders
{
	// Token: 0x02000021 RID: 33
	internal static class EventExtensions
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x000048FC File Offset: 0x00002AFC
		internal static void CopyMasterPropertiesTo(this Event master, Event occurrence, bool isNew = false, Func<Event, Event, PropertyDefinition, bool> shouldCopyProperty = null, bool isSeriesToInstancePropagation = true)
		{
			occurrence.MergeMasterAndInstanceProperties(master, isNew, shouldCopyProperty ?? new Func<Event, Event, PropertyDefinition, bool>(EventExtensions.ShouldCopyProperty));
			occurrence.SeriesMasterId = master.Id;
			occurrence.Type = EventType.Exception;
			if (isSeriesToInstancePropagation)
			{
				occurrence.SeriesMaster = master;
				((IEventInternal)occurrence).SeriesToInstancePropagation = true;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000494C File Offset: 0x00002B4C
		internal static void MergeMasterAndInstanceProperties(this Event occurrence, Event master, bool isNew, Func<Event, Event, PropertyDefinition, bool> shouldCopyProperty)
		{
			if (shouldCopyProperty(master, occurrence, master.Schema.AttachmentsProperty))
			{
				occurrence.Attachments = master.Attachments;
			}
			if (shouldCopyProperty(master, occurrence, master.Schema.AttendeesProperty))
			{
				occurrence.Attendees = master.Attendees;
			}
			if (shouldCopyProperty(master, occurrence, master.Schema.BodyProperty))
			{
				occurrence.Body = master.Body;
			}
			if (shouldCopyProperty(master, occurrence, master.Schema.CalendarProperty))
			{
				occurrence.Calendar = master.Calendar;
			}
			if (shouldCopyProperty(master, occurrence, master.Schema.CategoriesProperty))
			{
				occurrence.Categories = master.Categories;
			}
			if (shouldCopyProperty(master, occurrence, master.Schema.HasAttachmentsProperty))
			{
				occurrence.HasAttachments = master.HasAttachments;
			}
			if (shouldCopyProperty(master, occurrence, master.Schema.IntendedEndTimeZoneIdProperty))
			{
				occurrence.IntendedEndTimeZoneId = master.IntendedEndTimeZoneId;
			}
			if (shouldCopyProperty(master, occurrence, master.Schema.IntendedStartTimeZoneIdProperty))
			{
				occurrence.IntendedStartTimeZoneId = master.IntendedStartTimeZoneId;
			}
			if (shouldCopyProperty(master, occurrence, master.Schema.ImportanceProperty))
			{
				occurrence.Importance = master.Importance;
			}
			if (shouldCopyProperty(master, occurrence, master.Schema.LocationProperty))
			{
				occurrence.Location = master.Location;
			}
			if (shouldCopyProperty(master, occurrence, master.Schema.PreviewProperty))
			{
				occurrence.Preview = master.Preview;
			}
			if (shouldCopyProperty(master, occurrence, master.Schema.ResponseRequestedProperty))
			{
				occurrence.ResponseRequested = master.ResponseRequested;
			}
			if (shouldCopyProperty(master, occurrence, master.Schema.SensitivityProperty))
			{
				occurrence.Sensitivity = master.Sensitivity;
			}
			if (shouldCopyProperty(master, occurrence, master.Schema.SeriesIdProperty))
			{
				occurrence.SeriesId = master.SeriesId;
			}
			if (shouldCopyProperty(master, occurrence, master.Schema.ShowAsProperty))
			{
				occurrence.ShowAs = master.ShowAs;
			}
			if (shouldCopyProperty(master, occurrence, master.Schema.SubjectProperty))
			{
				occurrence.Subject = master.Subject;
			}
			if (isNew)
			{
				occurrence.ClientId = master.ClientId;
			}
			if (shouldCopyProperty(master, occurrence, master.Schema.InternalIsReceived))
			{
				((IEventInternal)occurrence).IsReceived = ((IEventInternal)master).IsReceived;
			}
			if (shouldCopyProperty(master, occurrence, master.Schema.PopupReminderSettingsProperty) && master.PopupReminderSettings != null && master.PopupReminderSettings.Count == 1 && master.PopupReminderSettings[0] != null)
			{
				EventPopupReminderSetting eventPopupReminderSetting = master.PopupReminderSettings[0];
				occurrence.PopupReminderSettings = new List<EventPopupReminderSetting>
				{
					new EventPopupReminderSetting
					{
						Id = (isNew ? null : EventPopupReminderSettingsRules.GetDefaultPopupReminderSettingId(occurrence)),
						IsReminderSet = eventPopupReminderSetting.IsReminderSet,
						ReminderMinutesBeforeStart = eventPopupReminderSetting.ReminderMinutesBeforeStart
					}
				};
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004C28 File Offset: 0x00002E28
		internal static void DeleteRelatedMessage(this IEventInternal entity, string messageId, DeleteItemFlags flags, IXSOFactory xsoFactory, IdConverter idConverter, IStoreSession session, bool markAsReadBeforeDelete)
		{
			StoreObjectId storeObjectId = idConverter.ToStoreObjectId(messageId);
			using (IMeetingMessage meetingMessage = xsoFactory.BindToMeetingMessage(session, storeObjectId))
			{
				if (meetingMessage == null || meetingMessage.GlobalObjectId == null || meetingMessage.GlobalObjectId.ToString() != entity.GlobalObjectId)
				{
					throw new InvalidRequestException(CalendaringStrings.ErrorMeetingMessageNotFoundOrCantBeUsed);
				}
				if (markAsReadBeforeDelete)
				{
					meetingMessage.OpenAsReadWrite();
					meetingMessage.IsRead = true;
					meetingMessage.Save(SaveMode.NoConflictResolutionForceSave);
				}
			}
			session.Delete(flags, new StoreId[]
			{
				storeObjectId
			});
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004CC0 File Offset: 0x00002EC0
		internal static bool AdjustSeriesStartAndEndTimes(this Event series, ICollection<Event> occurrencesBeingAdded)
		{
			bool result = false;
			if (occurrencesBeingAdded == null || occurrencesBeingAdded.Count == 0)
			{
				return false;
			}
			if (!series.IsPropertySet(series.Schema.StartProperty))
			{
				throw new InvalidOperationException("The Start property needs to be specified");
			}
			if (!series.IsPropertySet(series.Schema.EndProperty))
			{
				throw new InvalidOperationException("The End property needs to be specified");
			}
			ExDateTime start = series.Start;
			ExDateTime end = series.End;
			foreach (Event @event in occurrencesBeingAdded)
			{
				if (@event.Start < start)
				{
					start = @event.Start;
					result = true;
				}
				if (@event.End > end)
				{
					end = @event.End;
					result = true;
				}
			}
			series.Start = start;
			series.End = end;
			return result;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004D9C File Offset: 0x00002F9C
		internal static Event GetBasicSeriesEventData(IStorePropertyBag propertyBag, IStorageEntitySetScope<IStoreSession> scope)
		{
			VersionedId valueOrDefault = propertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
			Event @event = scope.IdConverter.CreateBasicEntity<Event>(valueOrDefault, scope.StoreSession);
			string valueOrDefault2 = propertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, null);
			if (!string.IsNullOrEmpty(valueOrDefault2))
			{
				@event.Type = (ObjectClass.IsCalendarItemSeries(valueOrDefault2) ? EventType.SeriesMaster : EventType.Exception);
			}
			@event.StoreId = valueOrDefault;
			return @event;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004DF8 File Offset: 0x00002FF8
		internal static bool ShouldCopyProperty(Event master, Event occurrence, PropertyDefinition propertyDefinition)
		{
			return master.IsPropertySet(propertyDefinition) && !occurrence.IsPropertySet(propertyDefinition);
		}

		// Token: 0x0400004F RID: 79
		internal const uint MaximumInstancesInSeries = 50U;
	}
}
