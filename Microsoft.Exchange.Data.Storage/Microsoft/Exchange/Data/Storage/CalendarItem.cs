using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003A7 RID: 935
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarItem : CalendarItemInstance, ICalendarItem, ICalendarItemInstance, ICalendarItemBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06002A5C RID: 10844 RVA: 0x000A84D7 File Offset: 0x000A66D7
		internal CalendarItem(ICoreItem coreItem) : base(coreItem)
		{
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06002A5D RID: 10845 RVA: 0x000A84EB File Offset: 0x000A66EB
		// (set) Token: 0x06002A5E RID: 10846 RVA: 0x000A8503 File Offset: 0x000A6703
		public Guid? LastExecutedCalendarInteropAction
		{
			get
			{
				this.CheckDisposed("LastExecutedCalendarInteropAction::get");
				return base.GetValueOrDefault<Guid?>(CalendarItemSchema.LastExecutedCalendarInteropAction);
			}
			set
			{
				this.CheckDisposed("LastExecutedCalendarInteropAction::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(55836U);
				this[CalendarItemSchema.LastExecutedCalendarInteropAction] = value;
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06002A5F RID: 10847 RVA: 0x000A8531 File Offset: 0x000A6731
		// (set) Token: 0x06002A60 RID: 10848 RVA: 0x000A854A File Offset: 0x000A674A
		public int InstanceCreationIndex
		{
			get
			{
				this.CheckDisposed("InstanceCreationIndex::get");
				return base.GetValueOrDefault<int>(CalendarItemSchema.InstanceCreationIndex, -1);
			}
			set
			{
				this.CheckDisposed("InstanceCreationIndex::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(39036U);
				this[CalendarItemSchema.InstanceCreationIndex] = value;
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06002A61 RID: 10849 RVA: 0x000A8578 File Offset: 0x000A6778
		// (set) Token: 0x06002A62 RID: 10850 RVA: 0x000A8591 File Offset: 0x000A6791
		public bool HasExceptionalInboxReminders
		{
			get
			{
				this.CheckDisposed("HasExceptionalInboxReminders::get");
				return base.GetValueOrDefault<bool>(CalendarItemSchema.HasExceptionalInboxReminders, false);
			}
			set
			{
				this.CheckDisposed("HasExceptionalInboxReminders::set");
				this[CalendarItemSchema.HasExceptionalInboxReminders] = value;
			}
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x000A85AF File Offset: 0x000A67AF
		public static CalendarItem Create(StoreSession session, StoreId parentFolderId)
		{
			return CalendarItem.CreateCalendarItem(session, parentFolderId, true);
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x000A85B9 File Offset: 0x000A67B9
		public new static CalendarItem Bind(StoreSession session, StoreId id)
		{
			return CalendarItem.Bind(session, id, null);
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x000A85C3 File Offset: 0x000A67C3
		public new static CalendarItem Bind(StoreSession session, StoreId id, params PropertyDefinition[] propsToReturn)
		{
			return CalendarItem.Bind(session, id, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x000A85D2 File Offset: 0x000A67D2
		public new static CalendarItem Bind(StoreSession session, StoreId id, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<CalendarItem>(session, id, CalendarItemSchema.Instance, propsToReturn);
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x000A85E1 File Offset: 0x000A67E1
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CalendarItem>(this);
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06002A68 RID: 10856 RVA: 0x000A85E9 File Offset: 0x000A67E9
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return CalendarItemSchema.Instance;
			}
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x000A85FC File Offset: 0x000A67FC
		internal static CalendarItem CreateCalendarItem(StoreSession session, StoreId parentFolderId, bool newItem)
		{
			CalendarItem calendarItem = null;
			bool flag = false;
			CalendarItem result;
			try
			{
				calendarItem = ItemBuilder.CreateNewItem<CalendarItem>(session, parentFolderId, ItemCreateInfo.CalendarItemInfo);
				calendarItem.Initialize(newItem);
				flag = true;
				result = calendarItem;
			}
			finally
			{
				if (!flag && calendarItem != null)
				{
					calendarItem.Dispose();
					calendarItem = null;
				}
			}
			return result;
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000A8648 File Offset: 0x000A6848
		protected override void CopyMeetingRequestProperties(MeetingRequest meetingRequest)
		{
			base.CopyMeetingRequestProperties(meetingRequest);
			CalendarItemBase.CopyPropertiesTo(this, meetingRequest, new PropertyDefinition[]
			{
				InternalSchema.PropertyChangeMetadataRaw
			});
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000A8673 File Offset: 0x000A6873
		protected override AttachmentCollection FetchAttachmentCollection()
		{
			this.CheckDisposed("AttachmentCollection::get");
			if (this.attachmentCollection == null)
			{
				base.CoreItem.OpenAttachmentCollection();
				this.attachmentCollection = new AttachmentCollection(this, true);
				this.ClearExceptionSummaryList();
			}
			return this.attachmentCollection;
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06002A6C RID: 10860 RVA: 0x000A86AC File Offset: 0x000A68AC
		protected override bool IsInThePast
		{
			get
			{
				ExDateTime t = ExDateTime.MaxValue;
				if (base.GetValueOrDefault<bool>(CalendarItemBaseSchema.AppointmentRecurring) && this.Recurrence != null)
				{
					t = this.Recurrence.EndDate + this.Recurrence.EndOffset;
				}
				else
				{
					t = this.EndTime;
				}
				return t < ExDateTime.GetNow(base.PropertyBag.ExTimeZone);
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06002A6D RID: 10861 RVA: 0x000A870F File Offset: 0x000A690F
		// (set) Token: 0x06002A6E RID: 10862 RVA: 0x000A8727 File Offset: 0x000A6927
		public ExTimeZone ExTimeZone
		{
			get
			{
				this.CheckDisposed("ExTimeZone::get");
				return base.PropertyBag.ExTimeZone;
			}
			set
			{
				this.CheckDisposed("ExTimeZone::set");
				if (value == null)
				{
					throw new ArgumentNullException("ExTimeZone");
				}
				this.recurrence = null;
				base.PropertyBag.ExTimeZone = value;
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06002A6F RID: 10863 RVA: 0x000A8755 File Offset: 0x000A6955
		// (set) Token: 0x06002A70 RID: 10864 RVA: 0x000A876D File Offset: 0x000A696D
		public override int AppointmentLastSequenceNumber
		{
			get
			{
				this.CheckDisposed("AppointmentLastSequenceNumber::get");
				return base.GetValueOrDefault<int>(CalendarItemBaseSchema.AppointmentLastSequenceNumber);
			}
			set
			{
				this.CheckDisposed("AppointmentLastSequenceNumber::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(61813U);
				this[CalendarItemBaseSchema.AppointmentLastSequenceNumber] = value;
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06002A71 RID: 10865 RVA: 0x000A879B File Offset: 0x000A699B
		// (set) Token: 0x06002A72 RID: 10866 RVA: 0x000A87BC File Offset: 0x000A69BC
		public Recurrence Recurrence
		{
			get
			{
				this.CheckDisposed("Recurrence::get");
				if (this.recurrence == null)
				{
					this.ReloadRecurrence();
				}
				return this.recurrence;
			}
			set
			{
				this.CheckDisposed("Recurrence::set");
				InternalRecurrence internalRecurrence = null;
				this.ValidateNoOccurrencesOpen();
				try
				{
					internalRecurrence = (this.Recurrence as InternalRecurrence);
				}
				catch (RecurrenceFormatException ex)
				{
					ExTraceGlobals.StorageTracer.TraceDebug<string>((long)this.GetHashCode(), "Parsing recurrence blob failed with message:{0}", ex.Message);
				}
				if (value == null)
				{
					this.recurrence = null;
				}
				else
				{
					InternalRecurrence internalRecurrence2 = value as InternalRecurrence;
					if (internalRecurrence2 != null)
					{
						if (!object.Equals(this, internalRecurrence2.MasterItem))
						{
							throw new ArgumentException();
						}
						this.recurrence = internalRecurrence2;
					}
					else
					{
						ExTimeZoneHelperForMigrationOnly.CheckValidationLevel(value.CreatedExTimeZone != ExTimeZone.UnspecifiedTimeZone, ExTimeZoneHelperForMigrationOnly.ValidationLevel.Mid, "CalendarItem.Recurrence_set", new object[0]);
						ExTimeZone exTimeZone;
						if (value.HasTimeZone && value.CreatedExTimeZone != ExTimeZone.UtcTimeZone && value.CreatedExTimeZone != ExTimeZone.UnspecifiedTimeZone)
						{
							exTimeZone = value.CreatedExTimeZone;
						}
						else
						{
							exTimeZone = base.PropertyBag.ExTimeZone;
						}
						if (exTimeZone == null || exTimeZone == ExTimeZone.UtcTimeZone)
						{
							throw new InvalidOperationException(ServerStrings.ExCannotCreateRecurringMeetingWithoutTimeZone);
						}
						ExDateTime exDateTime = exTimeZone.ConvertDateTime(this.StartTime);
						ExDateTime exDateTime2 = exTimeZone.ConvertDateTime(this.EndTime);
						this.recurrence = new InternalRecurrence(value.Pattern, value.Range, this, exTimeZone, base.PropertyBag.ExTimeZone, exDateTime.LocalTime - exDateTime.LocalTime.Date, exDateTime2.LocalTime - exDateTime.LocalTime.Date);
					}
				}
				if (internalRecurrence != this.recurrence && this.oldRecurrence == null)
				{
					this.oldRecurrence = internalRecurrence;
				}
				bool flag = this.recurrence != null;
				base.PropertyBag[InternalSchema.AppointmentRecurring] = flag;
				base.PropertyBag[InternalSchema.IsException] = false;
				base.PropertyBag[InternalSchema.IsRecurring] = (flag && !base.IsOrganizer());
				if (this.recurrence == null)
				{
					base.Delete(InternalSchema.AppointmentRecurrenceBlob);
					base.Delete(InternalSchema.RecurrencePattern);
					base.Delete(InternalSchema.TimeZoneBlob);
					base.Delete(InternalSchema.ClipStartTime);
					base.Delete(InternalSchema.ClipEndTime);
					base.Delete(InternalSchema.StartRecurDate);
					base.Delete(InternalSchema.StartRecurTime);
					base.Delete(InternalSchema.MapiRecurrenceType);
					base.Delete(InternalSchema.TimeZoneDefinitionRecurring);
					base.Delete(InternalSchema.TimeZone);
				}
				else
				{
					byte[] value2 = this.recurrence.ToByteArray();
					base.PropertyBag[InternalSchema.AppointmentRecurrenceBlob] = value2;
					base.PropertyBag[InternalSchema.RecurrencePattern] = this.recurrence.GenerateWhen(false).ToString(base.Session.InternalPreferedCulture);
					base.PropertyBag[InternalSchema.ClipStartTime] = this.recurrence.Range.StartDate;
					base.PropertyBag[InternalSchema.ClipEndTime] = this.recurrence.EndDate;
					base.PropertyBag[InternalSchema.StartRecurTime] = Util.ConvertTimeSpanToSCDTime(this.recurrence.StartOffset);
					base.PropertyBag[InternalSchema.StartRecurDate] = Util.ConvertDateTimeToSCDDate(this.recurrence.Range.StartDate);
					base.PropertyBag[InternalSchema.MapiRecurrenceType] = this.recurrence.Pattern.MapiRecurrenceType;
					ExTimeZone createdExTimeZone = this.recurrence.CreatedExTimeZone;
					base.PropertyBag[InternalSchema.TimeZone] = createdExTimeZone.LocalizableDisplayName.ToString();
					base.PropertyBag[InternalSchema.TimeZoneBlob] = O11TimeZoneFormatter.GetTimeZoneBlob(createdExTimeZone);
					if (createdExTimeZone.IsCustomTimeZone && string.IsNullOrEmpty(createdExTimeZone.AlternativeId))
					{
						base.PropertyBag.Delete(InternalSchema.TimeZoneDefinitionRecurring);
						base.PropertyBag.Delete(InternalSchema.TimeZoneDefinitionStart);
					}
					else
					{
						byte[] timeZoneBlob = O12TimeZoneFormatter.GetTimeZoneBlob(createdExTimeZone);
						base.PropertyBag[InternalSchema.TimeZoneDefinitionRecurring] = timeZoneBlob;
						base.PropertyBag[InternalSchema.TimeZoneDefinitionStart] = timeZoneBlob;
					}
				}
				base.Reminder.Adjust();
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06002A73 RID: 10867 RVA: 0x000A8BF4 File Offset: 0x000A6DF4
		public string InternetMessageId
		{
			get
			{
				this.CheckDisposed("InternetMessageId::get");
				return base.GetValueOrDefault<string>(InternalSchema.InternetMessageId, string.Empty);
			}
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000A8C11 File Offset: 0x000A6E11
		internal void DeleteOccurrence(StoreObjectId id)
		{
			this.DeleteOccurrence(id, DeleteItemFlags.None);
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000A8C1C File Offset: 0x000A6E1C
		internal void DeleteOccurrence(StoreObjectId id, DeleteItemFlags deleteFlags)
		{
			this.CheckDisposed("DeleteOccurrence");
			OccurrenceStoreObjectId occurrenceStoreObjectId = id as OccurrenceStoreObjectId;
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (occurrenceStoreObjectId == null || !ArrayComparer<byte>.Comparer.Equals(occurrenceStoreObjectId.ProviderLevelItemId, base.StoreObjectId.ProviderLevelItemId))
			{
				throw new ArgumentException(ServerStrings.ExInvalidOccurrenceId);
			}
			this.SetDeleteClientIntent(deleteFlags, true);
			this.DeleteOccurrenceByDateId(occurrenceStoreObjectId.OccurrenceId);
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000A8C90 File Offset: 0x000A6E90
		public void DeleteOccurrenceByOriginalStartTime(ExDateTime originalStartTime)
		{
			this.CheckDisposed("DeleteOccurrenceByOriginalStartTime");
			if (this.Recurrence == null)
			{
				throw new InvalidOperationException(ServerStrings.ExCantAccessOccurrenceFromSingle);
			}
			this.DeleteOccurrenceByDateId(this.recurrence.CreatedExTimeZone.ConvertDateTime(originalStartTime).Date);
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000A8CE0 File Offset: 0x000A6EE0
		public void RecoverDeletedOccurrence(StoreObjectId id)
		{
			this.CheckDisposed("UnDeleteOccurrence");
			OccurrenceStoreObjectId occurrenceStoreObjectId = id as OccurrenceStoreObjectId;
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (occurrenceStoreObjectId == null || !ArrayComparer<byte>.Comparer.Equals(occurrenceStoreObjectId.ProviderLevelItemId, base.StoreObjectId.ProviderLevelItemId))
			{
				throw new ArgumentException(ServerStrings.ExInvalidOccurrenceId);
			}
			this.RecoverDeletedOccurrenceByDateId(occurrenceStoreObjectId.OccurrenceId);
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000A8D4C File Offset: 0x000A6F4C
		public CalendarItemOccurrence OpenOccurrence(StoreObjectId id, params PropertyDefinition[] prefetchPropertyDefinitions)
		{
			this.CheckDisposed("OpenOccurrence");
			OccurrenceStoreObjectId occurrenceStoreObjectId = id as OccurrenceStoreObjectId;
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (occurrenceStoreObjectId == null || !ArrayComparer<byte>.Comparer.Equals(occurrenceStoreObjectId.ProviderLevelItemId, base.StoreObjectId.ProviderLevelItemId))
			{
				throw new ArgumentException(ServerStrings.ExInvalidOccurrenceId);
			}
			return this.OpenOccurrenceByOrganizerOriginalDate(occurrenceStoreObjectId.OccurrenceId, prefetchPropertyDefinitions);
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x000A8DB8 File Offset: 0x000A6FB8
		public CalendarItemOccurrence OpenOccurrenceByOriginalStartTime(ExDateTime originalStartTime, params PropertyDefinition[] prefetchProperties)
		{
			this.CheckDisposed("OpenOccurrenceByOriginalStartTime");
			if (this.Recurrence == null)
			{
				throw new InvalidOperationException(ServerStrings.ExCantAccessOccurrenceFromSingle);
			}
			return this.OpenOccurrenceByOrganizerOriginalDate(this.recurrence.CreatedExTimeZone.ConvertDateTime(originalStartTime).Date, prefetchProperties);
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x000A8E08 File Offset: 0x000A7008
		public override string GenerateWhen()
		{
			if (!base.IsReadOnly)
			{
				this.OnBeforeSaveUpdateRecurrenceBlob();
			}
			LocalizedString localizedString = CalendarItem.InternalWhen(this, this.recurrence, false);
			if (base.Session != null)
			{
				return localizedString.ToString(base.Session.InternalPreferedCulture);
			}
			ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "CalendarItem.GenerateWhen() found the session is null");
			return localizedString.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x000A8E6E File Offset: 0x000A706E
		public void ReloadRecurrence()
		{
			this.recurrence = CalendarItem.GetRecurrenceFromItem(this);
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x000A8E7C File Offset: 0x000A707C
		public override MeetingResponse RespondToMeetingRequest(ResponseType responseType, string subjectPrefix, ExDateTime? proposedStart = null, ExDateTime? proposedEnd = null)
		{
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, ResponseType>((long)this.GetHashCode(), "Storage.CalendarItem.RespondToMeetingRequest: GOID={0}; responseType={1}.", base.GlobalObjectId, responseType);
			return base.RespondToMeetingRequest(responseType, subjectPrefix, proposedStart, proposedEnd);
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x000A8EA8 File Offset: 0x000A70A8
		protected override void OnBeforeSave()
		{
			if (!base.IsInMemoryObject)
			{
				this.OnBeforeSaveUpdateRecurrenceBlob();
				DateTime utcNow = DateTime.UtcNow;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<DateTime>(2672176445U, ref utcNow);
				ExDateTime now = new ExDateTime(this.ExTimeZone, utcNow);
				this.KeepMeetingHistory(now);
				this.FlushOldRecurrenceCache(false, now);
				if (this.globalObjectId == null)
				{
					this.globalObjectId = base.GetValueOrDefault<byte[]>(InternalSchema.GlobalObjectId);
				}
				this.deleteOrphansAfterSave = (base.IsNew && this.Recurrence != null && this.globalObjectId != null);
			}
			base.OnBeforeSave();
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x000A8F3C File Offset: 0x000A713C
		protected override void OnAfterSave(ConflictResolutionResult acrResults)
		{
			base.OnAfterSave(acrResults);
			if (acrResults.SaveStatus == SaveResult.SuccessWithConflictResolution)
			{
				this.attendees = null;
			}
			this.ClearExceptionSummaryList();
			if (!base.IsInMemoryObject)
			{
				if (acrResults.SaveStatus != SaveResult.IrresolvableConflict && this.deleteOrphansAfterSave && base.Session is MailboxSession)
				{
					base.Load();
					VersionedId id = base.Id;
					StoreObjectId valueOrDefault = base.GetValueOrDefault<StoreObjectId>(InternalSchema.ParentItemId);
					if (this.Recurrence != null && valueOrDefault != null)
					{
						ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, InternalSchema.CleanGlobalObjectId, this.globalObjectId);
						using (Folder folder = Folder.Bind(base.Session, base.ParentId))
						{
							using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, new StorePropertyDefinition[]
							{
								InternalSchema.ItemId,
								InternalSchema.CleanGlobalObjectId,
								InternalSchema.GlobalObjectId,
								InternalSchema.AppointmentSequenceNumber
							}))
							{
								queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter);
								bool flag = true;
								int? valueAsNullable = base.GetValueAsNullable<int>(InternalSchema.AppointmentSequenceNumber);
								List<VersionedId> list = new List<VersionedId>();
								bool flag2 = false;
								while (flag)
								{
									object[][] rows = queryResult.GetRows(10000);
									if (rows.Length > 0)
									{
										foreach (object[] array2 in rows)
										{
											object obj = array2[1];
											byte[] array3 = array2[2] as byte[];
											if (array3 != null && GlobalObjectId.CompareCleanGlobalObjectIds(this.globalObjectId, array3))
											{
												ExDateTime date = new GlobalObjectId(array3).Date;
												VersionedId versionedId = Microsoft.Exchange.Data.Storage.PropertyBag.CheckPropertyValue<VersionedId>(InternalSchema.ItemId, array2[0]);
												int? num = (array2[3] is int) ? new int?((int)array2[3]) : null;
												if (!id.Equals(versionedId) && date != ExDateTime.MinValue)
												{
													flag2 = true;
													if (this.recurrence.IsValidOccurrenceId(date) && !this.recurrence.IsOccurrenceDeleted(date))
													{
														using (CalendarItemOccurrence calendarItemOccurrence = this.OpenOccurrenceByOrganizerOriginalDate(date, null))
														{
															using (CalendarItem calendarItem = CalendarItem.Bind(base.Session, versionedId, MeetingMessage.MeetingMessageProperties))
															{
																calendarItemOccurrence.MakeModifiedOccurrence();
																CalendarItemBase.CopyPropertiesTo(calendarItem, calendarItemOccurrence, MeetingMessage.MeetingMessageProperties);
																object obj2 = calendarItem.TryGetProperty(InternalSchema.FreeBusyStatus);
																if (!(obj2 is PropertyError))
																{
																	calendarItemOccurrence[InternalSchema.FreeBusyStatus] = obj2;
																}
																Body.CopyBody(calendarItem, calendarItemOccurrence, false);
																calendarItem.ReplaceAttachments(calendarItemOccurrence);
																IAttendeeCollection attendeeCollection = calendarItemOccurrence.AttendeeCollection;
																attendeeCollection.Clear();
																if (calendarItem.Organizer != null)
																{
																	attendeeCollection.Add(calendarItem.Organizer, AttendeeType.Required, null, null, false).RecipientFlags = (RecipientFlags.Sendable | RecipientFlags.Organizer);
																}
																foreach (Attendee item in calendarItem.AttendeeCollection)
																{
																	attendeeCollection.Add(item);
																}
																calendarItemOccurrence.Reminder.Adjust();
															}
															calendarItemOccurrence[InternalSchema.IsException] = true;
															calendarItemOccurrence[InternalSchema.IsRecurring] = true;
															calendarItemOccurrence[InternalSchema.AppointmentRecurring] = false;
															if (calendarItemOccurrence.Save(SaveMode.FailOnAnyConflict).SaveStatus == SaveResult.Success)
															{
																list.Add(versionedId);
															}
															goto IL_36C;
														}
													}
													if (num != null && valueAsNullable != null && num.Value <= valueAsNullable.Value)
													{
														list.Add(versionedId);
													}
												}
											}
											IL_36C:;
										}
										queryResult.SeekToCondition(SeekReference.OriginCurrent, seekFilter);
									}
									else
									{
										flag = false;
									}
								}
								this.deleteOrphansAfterSave = false;
								if (flag2 && base.Save(SaveMode.ResolveConflicts).SaveStatus != SaveResult.IrresolvableConflict)
								{
									base.Session.Delete(DeleteItemFlags.SoftDelete, list.ToArray());
								}
							}
						}
					}
				}
				this.deleteOrphansAfterSave = false;
			}
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x000A93A8 File Offset: 0x000A75A8
		public override bool DeleteMeeting(DeleteItemFlags deleteFlag)
		{
			if (this.Recurrence != null)
			{
				DateTime utcNow = DateTime.UtcNow;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<DateTime>(3209047357U, ref utcNow);
				ExDateTime exDateTime = new ExDateTime(this.ExTimeZone, utcNow);
				if (this.Recurrence.HasPastOccurrences(exDateTime) && this.Recurrence.HasFutureOccurrences(exDateTime))
				{
					base.OpenAsReadWrite();
					this.MakeMeetingHistory(exDateTime);
					this.MakeAppointment();
					base.ClientIntent |= ClientIntentFlags.MeetingConvertedToHistoryByRemove;
					return base.Save(SaveMode.ResolveConflicts).SaveStatus != SaveResult.IrresolvableConflict;
				}
			}
			return base.DeleteMeeting(deleteFlag);
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x000A943D File Offset: 0x000A763D
		public override MeetingCancellation CancelMeeting(int? seriesSequenceNumber = null, byte[] masterGoid = null)
		{
			this.CheckDisposed("CancelMeeting");
			if (base.Id != null)
			{
				this.CancelOccurrences();
			}
			return base.CancelMeeting(seriesSequenceNumber, masterGoid);
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x000A9460 File Offset: 0x000A7660
		internal void CancelOccurrences()
		{
			this.CheckDisposed("CancelOccurrences");
			ExDateTime now = ExDateTime.GetNow(this.ExTimeZone);
			this.FlushOldRecurrenceCache(true, now);
			if (this.Recurrence != null)
			{
				this.CleanOccurrencesForRecurrenceChange(null, this.recurrence, now, true, false);
			}
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x000A94DC File Offset: 0x000A76DC
		internal override void SendUpdateRums(UpdateRumInfo rumInfo, bool copyToSentItems)
		{
			this.CheckDisposed("SendUpdateRums");
			this.SendMeetingMessages(delegate()
			{
				this.SendBaseUpdateRums(rumInfo, copyToSentItems);
			}, delegate(CalendarItemBase calItem)
			{
				calItem.SendUpdateRums(rumInfo, copyToSentItems);
			});
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x000A9588 File Offset: 0x000A7788
		public override void SendMeetingMessages(bool isToAllAttendees, int? seriesSequenceNumber = null, bool autoCaptureClientIntent = false, bool copyToSentItems = true, string occurrencesViewPropertiesBlob = null, byte[] masterGoid = null)
		{
			this.CheckDisposed("SendMeetingMessages");
			this.SendMeetingMessages(delegate()
			{
				this.SendBaseMeetingMessages(isToAllAttendees, seriesSequenceNumber, autoCaptureClientIntent, copyToSentItems, masterGoid);
			}, delegate(CalendarItemBase calItem)
			{
				calItem.SendMeetingMessages(isToAllAttendees, seriesSequenceNumber, autoCaptureClientIntent, copyToSentItems, null, masterGoid);
			});
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x000A95F0 File Offset: 0x000A77F0
		protected override Reminder CreateReminderObject()
		{
			return new CalendarItem.CustomReminder(this);
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x000A95F8 File Offset: 0x000A77F8
		private static ClientIntentFlags GetDeclineIntent(bool intendToSendResponse)
		{
			if (!intendToSendResponse)
			{
				return ClientIntentFlags.DeletedWithNoResponse;
			}
			return ClientIntentFlags.RespondedDecline;
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x000A9601 File Offset: 0x000A7801
		// (set) Token: 0x06002A87 RID: 10887 RVA: 0x000A9614 File Offset: 0x000A7814
		internal List<RecurrenceManager.ExceptionSummary> ExceptionSummaryList
		{
			get
			{
				this.CheckDisposed("ExceptionSummaryList::get");
				return this.exceptionSummaryList;
			}
			set
			{
				this.CheckDisposed("ExceptionSummaryList::set");
				this.exceptionSummaryList = value;
			}
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x000A9628 File Offset: 0x000A7828
		internal void ClearExceptionSummaryList()
		{
			this.ExceptionSummaryList = null;
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06002A89 RID: 10889 RVA: 0x000A9631 File Offset: 0x000A7831
		// (set) Token: 0x06002A8A RID: 10890 RVA: 0x000A9644 File Offset: 0x000A7844
		internal bool SuppressUpdateRecurrenceTimeOffset
		{
			get
			{
				this.CheckDisposed("SuppressUpdateRecurrenceTimeOffset::get");
				return this.suppressUpdateRecurrenceTimeOffset;
			}
			set
			{
				this.CheckDisposed("SuppressUpdateRecurrenceTimeOffset::set");
				this.suppressUpdateRecurrenceTimeOffset = value;
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06002A8B RID: 10891 RVA: 0x000A9658 File Offset: 0x000A7858
		// (set) Token: 0x06002A8C RID: 10892 RVA: 0x000A9660 File Offset: 0x000A7860
		internal SaveMode SaveModeOnSendMeetingMessages
		{
			get
			{
				return this.saveModeOnSendMeetingMessages;
			}
			set
			{
				this.saveModeOnSendMeetingMessages = value;
			}
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x000A966C File Offset: 0x000A786C
		private static ExDateTime? GetDatePropertyInTimezone(Item item, StorePropertyDefinition propDef, ExTimeZone preferredTimeZone)
		{
			ExDateTime? valueAsNullable = item.GetValueAsNullable<ExDateTime>(propDef);
			if (valueAsNullable != null && preferredTimeZone != null)
			{
				valueAsNullable = new ExDateTime?(preferredTimeZone.ConvertDateTime(valueAsNullable.Value));
			}
			return valueAsNullable;
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000A96A4 File Offset: 0x000A78A4
		public void RecoverDeletedOccurrenceByDateId(ExDateTime dateId)
		{
			this.CheckDisposed("CalendarItem::RecoverDeletedOccurrenceByDateId");
			if (this.Recurrence == null)
			{
				throw new InvalidOperationException(ServerStrings.ExCantAccessOccurrenceFromSingle);
			}
			if (base.Id == null)
			{
				throw new InvalidOperationException(ServerStrings.ExCantAccessOccurrenceFromNewItem);
			}
			if (!this.recurrence.TryUndeleteOccurrence(dateId))
			{
				throw new OccurrenceNotFoundException(ServerStrings.ExCantUndeleteOccurrence);
			}
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(59509U, LastChangeAction.UnDeleteException);
			this[InternalSchema.AppointmentRecurrenceBlob] = this.recurrence.ToByteArray();
			Reminder.Adjust(this);
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000A9760 File Offset: 0x000A7960
		private CalendarItemOccurrence OpenOccurrenceByOrganizerOriginalDate(ExDateTime occurrenceDateId, ICollection<PropertyDefinition> prefetchProperties)
		{
			if (this.Recurrence == null)
			{
				throw new InvalidOperationException(ServerStrings.ExCantAccessOccurrenceFromSingle);
			}
			if (base.Id == null)
			{
				throw new InvalidOperationException(ServerStrings.ExCantAccessOccurrenceFromNewItem);
			}
			if (!this.recurrence.IsValidOccurrenceId(occurrenceDateId))
			{
				throw new OccurrenceNotFoundException(ServerStrings.ExOccurrenceNotPresent(occurrenceDateId));
			}
			this.FlushOldRecurrenceCache(false);
			CalendarItemOccurrence calendarItemOccurrence = null;
			if (this.childOccurrences.TryGetValue(occurrenceDateId, out calendarItemOccurrence) && !calendarItemOccurrence.IsDisposed)
			{
				return calendarItemOccurrence;
			}
			prefetchProperties = CalendarItemOccurrenceSchema.Instance.AutoloadProperties.Union(prefetchProperties);
			OccurrenceInfo occurrenceInfo = this.recurrence.GetOccurrenceInfoByDateId(occurrenceDateId);
			bool flag = false;
			CalendarItemOccurrence result;
			try
			{
				calendarItemOccurrence = ItemBuilder.ConstructItem<CalendarItemOccurrence>(base.Session, occurrenceInfo.VersionedId.ObjectId, null, prefetchProperties, () => new OccurrencePropertyBag(this.Session, this, null, occurrenceInfo, prefetchProperties), ItemCreateInfo.CalendarItemOccurrenceInfo.Creator, Origin.Existing, ItemLevel.TopLevel);
				this.childOccurrences[occurrenceDateId] = calendarItemOccurrence;
				flag = true;
				result = calendarItemOccurrence;
			}
			finally
			{
				if (!flag && calendarItemOccurrence != null)
				{
					calendarItemOccurrence.Dispose();
					calendarItemOccurrence = null;
				}
			}
			return result;
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x000A989C File Offset: 0x000A7A9C
		private void DeleteOccurrenceByDateId(ExDateTime dateId)
		{
			if (this.Recurrence == null)
			{
				throw new InvalidOperationException(ServerStrings.ExCantAccessOccurrenceFromSingle);
			}
			if (base.Id == null)
			{
				throw new InvalidOperationException(ServerStrings.ExCantAccessOccurrenceFromNewItem);
			}
			this.FlushOldRecurrenceCache(false);
			ExceptionInfo exceptionInfo = this.recurrence.GetOccurrenceInfoByDateId(dateId) as ExceptionInfo;
			CalendarItemOccurrence calendarItemOccurrence;
			if (this.childOccurrences.TryGetValue(dateId, out calendarItemOccurrence) && !calendarItemOccurrence.IsDisposed)
			{
				throw new InvalidOperationException(ServerStrings.ExCantDeleteOpenedOccurrence(dateId));
			}
			if (this.recurrence.RemainingOccurrences == 1)
			{
				throw new LastOccurrenceDeletionException(dateId, ServerStrings.ExCantDeleteLastOccurrence);
			}
			this.recurrence.TryDeleteOccurrence(dateId);
			if (exceptionInfo != null)
			{
				RecurrenceManager.DeleteAttachment(this, this.recurrence.CreatedExTimeZone, exceptionInfo.StartTime, exceptionInfo.EndTime);
			}
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(34933U, LastChangeAction.DeleteOccurrence);
			this[CalendarItemBaseSchema.AppointmentRecurrenceBlob] = this.recurrence.ToByteArray();
			Reminder.Adjust(this);
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x000A9998 File Offset: 0x000A7B98
		private void OnBeforeSaveUpdateRecurrenceBlob()
		{
			try
			{
				if (this.Recurrence != null && !this.SuppressUpdateRecurrenceTimeOffset && this.recurrence.IsTimeOffsetOutOfDate())
				{
					this.Recurrence = new Recurrence(this.recurrence.Pattern, this.recurrence.Range, this.recurrence.CreatedExTimeZone, this.recurrence.ReadExTimeZone);
				}
				if (this.recurrence != null && (base.GetValueAsNullable<ExDateTime>(CalendarItemInstanceSchema.StartTime) == null || this.recurrence.CreatedExTimeZone.ConvertDateTime(this.StartTime).Date != this.recurrence.Range.StartDate))
				{
					ExDateTime exDateTime = this.recurrence.CreatedExTimeZone.ConvertDateTime(this.recurrence.Range.StartDate);
					this.StartTime = new ExDateTime(this.recurrence.CreatedExTimeZone, exDateTime.LocalTime + this.recurrence.StartOffset);
					this.EndTime = this.StartTime.Add(this.recurrence.EndOffset - this.recurrence.StartOffset);
				}
			}
			catch (RecurrenceFormatException ex)
			{
				ExTraceGlobals.StorageTracer.TraceError<int, string>((long)this.GetHashCode(), "The recurrence blob of invalid format, Position:{0} Blob:{1}", ex.Position, Convert.ToBase64String(ex.Blob));
			}
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x000A9B14 File Offset: 0x000A7D14
		private void SendBaseUpdateRums(UpdateRumInfo rumInfo, bool copyToSentItems)
		{
			base.SendUpdateRums(rumInfo, copyToSentItems);
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x000A9B20 File Offset: 0x000A7D20
		private void SendMeetingMessages(Action sendBaseMessages, Action<CalendarItemBase> sendOccurrenceMessages)
		{
			bool flag = this.Recurrence != null;
			bool flag2 = false;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(3303419197U, ref flag2);
			DateTime utcNow = DateTime.UtcNow;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<DateTime>(4282789181U, ref utcNow);
			ExDateTime exDateTime = new ExDateTime(this.ExTimeZone, utcNow);
			this.KeepMeetingHistory(exDateTime);
			this.FlushOldRecurrenceCache(true, exDateTime);
			if (!flag2)
			{
				sendBaseMessages();
			}
			if (flag)
			{
				base.Load(null);
				foreach (OccurrenceInfo occurrenceInfo in ((InternalRecurrence)this.Recurrence).GetModifiedOccurrences())
				{
					if (occurrenceInfo.StartTime > exDateTime)
					{
						using (CalendarItemBase calendarItemBase = this.OpenOccurrenceByOriginalStartTime(occurrenceInfo.OriginalStartTime, new PropertyDefinition[0]))
						{
							calendarItemBase.OpenAsReadWrite();
							sendOccurrenceMessages(calendarItemBase);
						}
					}
				}
				base.SaveWithConflictCheck(this.saveModeOnSendMeetingMessages);
			}
			if (flag2)
			{
				base.Load();
				sendBaseMessages();
			}
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x000A9C48 File Offset: 0x000A7E48
		private void KeepMeetingHistory(ExDateTime now)
		{
			if (this.Recurrence == null || this.oldRecurrence == null || this.Recurrence == this.oldRecurrence)
			{
				return;
			}
			if (!this.Recurrence.Pattern.Equals(this.oldRecurrence.Pattern) && this.oldRecurrence.HasPastOccurrences(now) && this.Recurrence.HasFutureOccurrences(now))
			{
				ExTraceGlobals.RecurrenceTracer.TraceDebug<InternalRecurrence, Recurrence>((long)this.GetHashCode(), "It has been decided to clone calendar item. Old recurrence:{0}. New recurrence:{1}.", this.oldRecurrence, this.Recurrence);
				byte[] array = base.GetValueOrDefault<byte[]>(CalendarItemBaseSchema.MeetingUniqueId);
				if (array == null)
				{
					array = default(Guid).ToByteArray();
					this[CalendarItemBaseSchema.MeetingUniqueId] = array;
				}
				using (Item item = Microsoft.Exchange.Data.Storage.Item.CloneItem(base.Session, base.ParentId, this, false, true, null))
				{
					CalendarItem calendarItem = item as CalendarItem;
					if (calendarItem != null)
					{
						if (calendarItem.IsMeeting)
						{
							using (MeetingCancellation meetingCancellation = calendarItem.CancelMeeting(null, null))
							{
								meetingCancellation.Send();
								ExTraceGlobals.RecurrenceTracer.TraceDebug((long)this.GetHashCode(), "Meeting cancellation sent for history.");
							}
						}
						if (this.oldRecurrence.HasFutureOccurrences(now))
						{
							calendarItem.MakeMeetingHistory(now);
						}
						calendarItem.MakeAppointment();
						calendarItem.ClientIntent |= ClientIntentFlags.MeetingHistoryCreatedByPatternChange;
						calendarItem[CalendarItemBaseSchema.MeetingUniqueId] = array;
						calendarItem.SaveWithConflictCheck(SaveMode.ResolveConflicts);
						ExTraceGlobals.RecurrenceTracer.TraceDebug<byte[]>((long)this.GetHashCode(), "CalendarItem::KeepMeetingHistory. Succeeded to save meeting history. MeetingUniqueId:{0}", array);
					}
				}
				if (this.Recurrence.HasPastOccurrences(now))
				{
					OccurrenceInfo occurrenceInfo = this.Recurrence.GetFirstOccurrence();
					int num = 0;
					while (occurrenceInfo.StartTime < now)
					{
						occurrenceInfo = this.Recurrence.GetNextOccurrence(occurrenceInfo);
						num++;
					}
					RecurrenceRange recurrenceRange = null;
					if (this.Recurrence.Range is NumberedRecurrenceRange)
					{
						recurrenceRange = new NumberedRecurrenceRange(occurrenceInfo.OccurrenceDateId, ((NumberedRecurrenceRange)this.Recurrence.Range).NumberOfOccurrences - num);
					}
					else if (this.Recurrence.Range is EndDateRecurrenceRange)
					{
						recurrenceRange = new EndDateRecurrenceRange(occurrenceInfo.OccurrenceDateId, this.Recurrence.EndDate);
					}
					else if (this.Recurrence.Range is NoEndRecurrenceRange)
					{
						recurrenceRange = new NoEndRecurrenceRange(occurrenceInfo.OccurrenceDateId);
					}
					ExTraceGlobals.RecurrenceTracer.TraceDebug<RecurrenceRange, RecurrenceRange>((long)this.GetHashCode(), "Going to truncate new series range. Old range:{0}. New range:{1}.", this.Recurrence.Range, recurrenceRange);
					this.Recurrence = new Recurrence(this.Recurrence.Pattern, recurrenceRange);
				}
				this.AssignNewGlobalObjectId();
			}
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x000A9F00 File Offset: 0x000A8100
		private void SendBaseMeetingMessages(bool isToAllAttendees, int? seriesSequenceNumber, bool autoCaptureClientIntent, bool copyToSentItems, byte[] masterGoid)
		{
			base.SendMeetingMessages(isToAllAttendees, seriesSequenceNumber, autoCaptureClientIntent, copyToSentItems, null, masterGoid);
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x000A9F10 File Offset: 0x000A8110
		internal void AbandonRecipientChanges()
		{
			base.CoreItem.AbandonRecipientChanges();
			this.attendees = null;
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x000A9F24 File Offset: 0x000A8124
		internal override IAttendeeCollection FetchAttendeeCollection(bool forceOpen)
		{
			this.CheckDisposed("FetchAttendeeCollection");
			if (this.attendees == null)
			{
				CoreRecipientCollection recipientCollection = base.CoreItem.GetRecipientCollection(forceOpen);
				if (recipientCollection != null)
				{
					this.attendees = new AttendeeCollection(recipientCollection);
					base.ResetAttendeeCache();
				}
			}
			return this.attendees;
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x000A9F6C File Offset: 0x000A816C
		internal override bool IsAttendeeListDirty
		{
			get
			{
				this.FetchAttendeeCollection(false);
				return this.attendees != null && this.attendees.IsDirty;
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06002A99 RID: 10905 RVA: 0x000A9F8B File Offset: 0x000A818B
		internal override bool IsAttendeeListCreated
		{
			get
			{
				this.FetchAttendeeCollection(false);
				return this.attendees != null;
			}
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000A9FA1 File Offset: 0x000A81A1
		internal static LocalizedString InternalWhen(Item item, InternalRecurrence recurrence, bool addTimeZoneInfo)
		{
			return CalendarItem.InternalWhen(item, recurrence, addTimeZoneInfo, null);
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x000A9FAC File Offset: 0x000A81AC
		internal static LocalizedString InternalWhen(Item item, InternalRecurrence recurrence, bool addTimeZoneInfo, ExTimeZone preferredTimeZone)
		{
			if (recurrence == null)
			{
				bool valueOrDefault = item.GetValueOrDefault<bool>(InternalSchema.AppointmentRecurring);
				bool valueOrDefault2 = item.GetValueOrDefault<bool>(InternalSchema.IsException);
				if (valueOrDefault && !valueOrDefault2)
				{
					recurrence = CalendarItem.GetRecurrenceFromItem(item);
				}
			}
			LocalizedString localizedString;
			if (recurrence != null)
			{
				localizedString = recurrence.GenerateWhen(addTimeZoneInfo);
			}
			else
			{
				preferredTimeZone = (preferredTimeZone ?? item.PropertyBag.ExTimeZone);
				ExDateTime? datePropertyInTimezone = CalendarItem.GetDatePropertyInTimezone(item, CalendarItemInstanceSchema.StartTime, preferredTimeZone);
				ExDateTime? datePropertyInTimezone2 = CalendarItem.GetDatePropertyInTimezone(item, CalendarItemInstanceSchema.EndTime, preferredTimeZone);
				localizedString = CalendarItem.GenerateWhenForSingleInstance(datePropertyInTimezone, datePropertyInTimezone2);
				if (addTimeZoneInfo && preferredTimeZone != null)
				{
					localizedString = ClientStrings.JointStrings(localizedString, preferredTimeZone.LocalizableDisplayName);
				}
			}
			return localizedString;
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x000AA03C File Offset: 0x000A823C
		internal static LocalizedString GenerateWhenForSingleInstance(ExDateTime? start, ExDateTime? end)
		{
			LocalizedString result;
			if (start == null || end == null)
			{
				result = default(LocalizedString);
			}
			else if (start.Value.Date == end.Value.Date)
			{
				result = ClientStrings.WhenStartsEndsSameDay(start.Value, start.Value, end.Value);
			}
			else
			{
				result = ClientStrings.WhenStartsEndsDifferentDay(start.Value, start.Value, end.Value, end.Value);
			}
			return result;
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x000AA0F0 File Offset: 0x000A82F0
		internal static InternalRecurrence GetRecurrenceFromItem(Item item)
		{
			InternalRecurrence result = null;
			byte[] largeBinaryProperty = item.PropertyBag.GetLargeBinaryProperty(InternalSchema.AppointmentRecurrenceBlob);
			if (largeBinaryProperty != null)
			{
				ExTimeZone exTimeZoneFromItem = TimeZoneHelper.GetExTimeZoneFromItem(item);
				int valueOrDefault = item.PropertyBag.GetValueOrDefault<int>(InternalSchema.Codepage, CalendarItem.DefaultCodePage);
				result = InternalRecurrence.InternalParse(largeBinaryProperty, item, exTimeZoneFromItem, item.PropertyBag.ExTimeZone, valueOrDefault);
			}
			return result;
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x000AA148 File Offset: 0x000A8348
		private void FlushOldRecurrenceCache(bool sendMeetingCancellations)
		{
			ExDateTime now = ExDateTime.GetNow(base.PropertyBag.ExTimeZone);
			this.FlushOldRecurrenceCache(sendMeetingCancellations, now);
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x000AA170 File Offset: 0x000A8370
		private void FlushOldRecurrenceCache(bool sendMeetingCancellations, ExDateTime now)
		{
			if (this.oldRecurrence == null)
			{
				return;
			}
			if (this.CleanOccurrencesForRecurrenceChange(this.recurrence, this.oldRecurrence, now, sendMeetingCancellations && base.IsOrganizer(), true))
			{
				this[InternalSchema.AppointmentRecurrenceBlob] = this.recurrence.ToByteArray();
				if (this.Recurrence.StartOffset != this.oldRecurrence.StartOffset)
				{
					foreach (OccurrenceInfo occurrenceInfo in this.recurrence.GetModifiedOccurrences())
					{
						using (CalendarItemOccurrence calendarItemOccurrence = this.OpenOccurrence(occurrenceInfo.VersionedId.ObjectId, new PropertyDefinition[0]))
						{
							calendarItemOccurrence.Save(SaveMode.NoConflictResolution);
						}
					}
				}
			}
			this.oldRecurrence = null;
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x000AA25C File Offset: 0x000A845C
		private void ValidateNoOccurrencesOpen()
		{
			foreach (CalendarItemOccurrence calendarItemOccurrence in this.childOccurrences.Values)
			{
				if (!calendarItemOccurrence.IsDisposed)
				{
					throw new InvalidOperationException();
				}
			}
			this.childOccurrences.Clear();
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x000AA304 File Offset: 0x000A8504
		private void RemoveOccurrenceAttachmentAndSendCancellation(InternalRecurrence recurrence, OccurrenceInfo occurrenceInfo, ExDateTime sendCancellationAfter, bool sendCancellation, bool deleteAttachments)
		{
			if (sendCancellation && occurrenceInfo.StartTime > sendCancellationAfter)
			{
				using (CalendarItemOccurrence calendarItemOccurrence = ItemBuilder.ConstructItem<CalendarItemOccurrence>(base.Session, occurrenceInfo.VersionedId.ObjectId, null, base.PropertyBag.PrefetchPropertyArray, () => new OccurrencePropertyBag(this.Session, this, recurrence, occurrenceInfo, this.PropertyBag.PrefetchPropertyArray), ItemCreateInfo.CalendarItemOccurrenceInfo.Creator, Origin.Existing, ItemLevel.TopLevel))
				{
					if (calendarItemOccurrence.IsMeeting && calendarItemOccurrence.MeetingRequestWasSent && calendarItemOccurrence.AttendeeCollection.Count > 0)
					{
						using (MeetingCancellation meetingCancellation = calendarItemOccurrence.CancelMeeting(null, null))
						{
							if (meetingCancellation.Recipients.Count > 0)
							{
								meetingCancellation.Send();
							}
						}
					}
				}
			}
			if (deleteAttachments)
			{
				RecurrenceManager.DeleteAttachment(this, recurrence.CreatedExTimeZone, occurrenceInfo.StartTime, occurrenceInfo.EndTime);
			}
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x000AA434 File Offset: 0x000A8634
		private bool CleanOccurrencesForRecurrenceChange(InternalRecurrence newRecurrence, InternalRecurrence oldRecurrence, ExDateTime sendCancellationAfter, bool sendCancellations, bool deleteExceptionAttachments)
		{
			bool result = false;
			this.ValidateNoOccurrencesOpen();
			if (oldRecurrence == null || oldRecurrence == newRecurrence)
			{
				return result;
			}
			if (newRecurrence != null && oldRecurrence.StartOffset == newRecurrence.StartOffset && oldRecurrence.EndOffset == newRecurrence.EndOffset && oldRecurrence.Pattern.Equals(newRecurrence.Pattern) && !base.IsNew)
			{
				foreach (ExDateTime exDateTime in oldRecurrence.GetDeletedOccurrences())
				{
					newRecurrence.TryDeleteOccurrence(oldRecurrence.CreatedExTimeZone.ConvertDateTime(exDateTime).Date);
					result = true;
				}
				using (IEnumerator<OccurrenceInfo> enumerator = oldRecurrence.GetModifiedOccurrences().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						OccurrenceInfo occurrenceInfo = enumerator.Current;
						ExceptionInfo exceptionInfo = (ExceptionInfo)occurrenceInfo;
						bool flag = false;
						ExDateTime occurrenceId = ((OccurrenceStoreObjectId)occurrenceInfo.VersionedId.ObjectId).OccurrenceId;
						if (newRecurrence.IsValidOccurrenceId(occurrenceId) && !newRecurrence.IsOccurrenceDeleted(occurrenceId))
						{
							try
							{
								newRecurrence.ModifyOccurrence(exceptionInfo);
								result = true;
								ExTraceGlobals.RecurrenceTracer.Information<ExDateTime>((long)this.GetHashCode(), "Exception on {0} is conserved when recurrence is modified", exceptionInfo.StartTime);
								goto IL_12B;
							}
							catch (OccurrenceCrossingBoundaryException)
							{
								flag = true;
								goto IL_12B;
							}
							goto IL_128;
						}
						goto IL_128;
						IL_12B:
						if (flag)
						{
							this.RemoveOccurrenceAttachmentAndSendCancellation(oldRecurrence, occurrenceInfo, sendCancellationAfter, sendCancellations, deleteExceptionAttachments);
							ExTraceGlobals.RecurrenceTracer.Information<ExDateTime>((long)this.GetHashCode(), "Exception on {0} is deleted when recurrence is modified", exceptionInfo.StartTime);
							continue;
						}
						continue;
						IL_128:
						flag = true;
						goto IL_12B;
					}
					return result;
				}
			}
			foreach (OccurrenceInfo occurrenceInfo2 in oldRecurrence.GetModifiedOccurrences())
			{
				this.RemoveOccurrenceAttachmentAndSendCancellation(oldRecurrence, occurrenceInfo2, sendCancellationAfter, sendCancellations, deleteExceptionAttachments);
				ExTraceGlobals.RecurrenceTracer.Information<ExDateTime>((long)this.GetHashCode(), "Exception on {0} is deleted when recurrence is modified", occurrenceInfo2.StartTime);
			}
			IList<AttachmentHandle> handles = base.AttachmentCollection.GetHandles();
			foreach (AttachmentHandle handle in handles)
			{
				if (CoreAttachmentCollection.IsCalendarException(handle))
				{
					base.AttachmentCollection.Remove(handle);
				}
			}
			return result;
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000AA694 File Offset: 0x000A8894
		protected override void SetDeclineIntent(bool intendToSendResponse)
		{
			this.SetDeclineIntent(intendToSendResponse, false);
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000AA69E File Offset: 0x000A889E
		private void SetDeclineIntent(bool intendToSendResponse, bool isOccurrence)
		{
			base.ClientIntent = (isOccurrence ? CalendarItemOccurrence.GetDeclineIntent(intendToSendResponse) : CalendarItem.GetDeclineIntent(intendToSendResponse));
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000AA6B8 File Offset: 0x000A88B8
		private void SetDeleteClientIntent(DeleteItemFlags deleteFlags, bool isOccurrence)
		{
			if ((deleteFlags & DeleteItemFlags.DeclineCalendarItemWithoutResponse) == DeleteItemFlags.DeclineCalendarItemWithoutResponse)
			{
				this.SetDeclineIntent(false, isOccurrence);
				return;
			}
			if ((deleteFlags & DeleteItemFlags.DeclineCalendarItemWithResponse) == DeleteItemFlags.DeclineCalendarItemWithResponse)
			{
				this.SetDeclineIntent(true, isOccurrence);
				return;
			}
			if ((deleteFlags & DeleteItemFlags.CancelCalendarItem) == DeleteItemFlags.CancelCalendarItem)
			{
				base.ClientIntent = (isOccurrence ? ClientIntentFlags.MeetingExceptionCanceled : ClientIntentFlags.MeetingCanceled);
			}
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000AA718 File Offset: 0x000A8918
		private void MakeMeetingHistory(ExDateTime nowTime)
		{
			InternalRecurrence internalRecurrence = this.Recurrence as InternalRecurrence;
			if (internalRecurrence == null)
			{
				return;
			}
			if (!internalRecurrence.HasPastOccurrences(nowTime))
			{
				return;
			}
			OccurrenceInfo firstOccurrence = internalRecurrence.GetFirstOccurrence();
			OccurrenceInfo occurrenceInfo = firstOccurrence;
			OccurrenceInfo occurrenceInfo2;
			do
			{
				occurrenceInfo2 = occurrenceInfo;
				occurrenceInfo = internalRecurrence.GetNextOccurrence(occurrenceInfo2);
			}
			while (occurrenceInfo.StartTime <= nowTime);
			RecurrenceManager recurrenceManager = new RecurrenceManager(base.Session, internalRecurrence);
			InternalRecurrence truncatedRecurrence = recurrenceManager.GetTruncatedRecurrence(firstOccurrence.OccurrenceDateId, occurrenceInfo2.OccurrenceDateId, false);
			if (truncatedRecurrence != null)
			{
				this.Recurrence = truncatedRecurrence;
				this.oldRecurrence = null;
			}
			AppointmentStateFlags valueOrDefault = base.GetValueOrDefault<AppointmentStateFlags>(CalendarItemBaseSchema.AppointmentState);
			this[CalendarItemBaseSchema.AppointmentState] = (valueOrDefault | AppointmentStateFlags.Cancelled);
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000AA7B8 File Offset: 0x000A89B8
		private void MakeAppointment()
		{
			AppointmentStateFlags valueOrDefault = base.GetValueOrDefault<AppointmentStateFlags>(CalendarItemBaseSchema.AppointmentState);
			this[InternalSchema.AppointmentState] = (valueOrDefault & ~AppointmentStateFlags.Meeting & ~AppointmentStateFlags.Received);
			ExTraceGlobals.RecurrenceTracer.TraceDebug<IAttendeeCollection>((long)this.GetHashCode(), "Cleaning attendee list. Attendee list:{0}", base.AttendeeCollection);
			base.AttendeeCollection.Clear();
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x000AA810 File Offset: 0x000A8A10
		private void AssignNewGlobalObjectId()
		{
			GlobalObjectId globalObjectId = new GlobalObjectId();
			ExTraceGlobals.RecurrenceTracer.TraceDebug<GlobalObjectId, GlobalObjectId>((long)this.GetHashCode(), "CalendarItem::MakeNewGlobalObjectId. Going to assign new GOID. Old GOID:{0}. New GOID:{1}.", base.GlobalObjectId, globalObjectId);
			this[InternalSchema.GlobalObjectId] = globalObjectId.Bytes;
			this[InternalSchema.CleanGlobalObjectId] = globalObjectId.Bytes;
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000AA864 File Offset: 0x000A8A64
		internal new static void CoreObjectUpdateAllAttachmentsHidden(CoreItem coreItem)
		{
			if (((ICoreItem)coreItem).IsAttachmentCollectionLoaded && coreItem.AttachmentCollection.IsDirty)
			{
				bool flag = false;
				bool flag2 = false;
				foreach (AttachmentHandle handle in coreItem.AttachmentCollection.GetAllHandles())
				{
					if (CoreAttachmentCollection.IsCalendarException(handle))
					{
						flag = true;
						if (flag2)
						{
							break;
						}
					}
					else
					{
						flag2 = true;
						if (flag)
						{
							break;
						}
					}
				}
				Microsoft.Exchange.Data.Storage.Item.EnsureAllAttachmentsHiddenValue(coreItem, flag && !flag2);
			}
		}

		// Token: 0x04001810 RID: 6160
		private InternalRecurrence oldRecurrence;

		// Token: 0x04001811 RID: 6161
		private InternalRecurrence recurrence;

		// Token: 0x04001812 RID: 6162
		private bool deleteOrphansAfterSave;

		// Token: 0x04001813 RID: 6163
		private byte[] globalObjectId;

		// Token: 0x04001814 RID: 6164
		private Dictionary<ExDateTime, CalendarItemOccurrence> childOccurrences = new Dictionary<ExDateTime, CalendarItemOccurrence>();

		// Token: 0x04001815 RID: 6165
		private bool suppressUpdateRecurrenceTimeOffset;

		// Token: 0x04001816 RID: 6166
		private SaveMode saveModeOnSendMeetingMessages;

		// Token: 0x04001817 RID: 6167
		internal static int DefaultCodePage = 20127;

		// Token: 0x04001818 RID: 6168
		private List<RecurrenceManager.ExceptionSummary> exceptionSummaryList;

		// Token: 0x04001819 RID: 6169
		private AttendeeCollection attendees;

		// Token: 0x020003AB RID: 939
		internal class CustomReminder : Reminder
		{
			// Token: 0x06002AD9 RID: 10969 RVA: 0x000AB3D2 File Offset: 0x000A95D2
			internal CustomReminder(CalendarItem item) : base(item)
			{
			}

			// Token: 0x17000DFC RID: 3580
			// (get) Token: 0x06002ADA RID: 10970 RVA: 0x000AB3DB File Offset: 0x000A95DB
			// (set) Token: 0x06002ADB RID: 10971 RVA: 0x000AB3E3 File Offset: 0x000A95E3
			public override ExDateTime? DueBy
			{
				get
				{
					return base.DueBy;
				}
				set
				{
					throw base.PropertyNotSupported("DueBy");
				}
			}

			// Token: 0x17000DFD RID: 3581
			// (get) Token: 0x06002ADC RID: 10972 RVA: 0x000AB3F0 File Offset: 0x000A95F0
			// (set) Token: 0x06002ADD RID: 10973 RVA: 0x000AB3F8 File Offset: 0x000A95F8
			public override int MinutesBeforeStart
			{
				get
				{
					return base.MinutesBeforeStart;
				}
				set
				{
					this.Item[InternalSchema.ReminderMinutesBeforeStart] = value;
				}
			}

			// Token: 0x06002ADE RID: 10974 RVA: 0x000AB410 File Offset: 0x000A9610
			protected override void Adjust(ExDateTime actualizationTime)
			{
				if (this.IsItemStateValid)
				{
					if (this.lastSetTo is CalendarItem.CustomReminder.OccurrenceReminderInfo != (this.Item.Recurrence != null))
					{
						this.lastSetTo = null;
					}
					base.Adjust(actualizationTime);
				}
			}

			// Token: 0x06002ADF RID: 10975 RVA: 0x000AB44C File Offset: 0x000A964C
			protected override Reminder.ReminderInfo GetNextPertinentItemInfo(ExDateTime actualizationTime)
			{
				InternalRecurrence internalRecurrence = (InternalRecurrence)this.Item.Recurrence;
				if (internalRecurrence != null)
				{
					Reminder.ReminderInfo reminderInfo = CalendarItem.CustomReminder.OccurrenceToReminderInfo(CalendarItem.CustomReminder.GetNextOccurrence(internalRecurrence, actualizationTime, this.IsSet, this.MinutesBeforeStart), this.MinutesBeforeStart);
					if (reminderInfo == null && this.IsSet)
					{
						reminderInfo = new CalendarItem.CustomReminder.OccurrenceReminderInfo(base.MaxOutlookDate, base.MaxOutlookDate, null);
					}
					return reminderInfo;
				}
				return base.GetNextPertinentItemInfo(actualizationTime);
			}

			// Token: 0x06002AE0 RID: 10976 RVA: 0x000AB4B4 File Offset: 0x000A96B4
			protected override Reminder.ReminderInfo GetPertinentItemInfo(ExDateTime actualizationTime)
			{
				InternalRecurrence internalRecurrence = (InternalRecurrence)this.Item.Recurrence;
				if (internalRecurrence != null)
				{
					return CalendarItem.CustomReminder.OccurrenceToReminderInfo(CalendarItem.CustomReminder.GetMostRecentOccurrence(internalRecurrence, actualizationTime, this.IsSet, this.MinutesBeforeStart), this.MinutesBeforeStart);
				}
				return base.GetPertinentItemInfo(actualizationTime);
			}

			// Token: 0x06002AE1 RID: 10977 RVA: 0x000AB4FB File Offset: 0x000A96FB
			internal static OccurrenceInfo GetMostRecentOccurrence(InternalRecurrence recurrence, ExDateTime actualizationTime, bool defaultIsSet, int defaultMinutesBeforeStart)
			{
				return CalendarItem.CustomReminder.GetAdjacentOccurrence(recurrence, actualizationTime, defaultIsSet, defaultMinutesBeforeStart, false, true);
			}

			// Token: 0x06002AE2 RID: 10978 RVA: 0x000AB508 File Offset: 0x000A9708
			internal static OccurrenceInfo GetNextOccurrence(InternalRecurrence recurrence, ExDateTime actualizationTime, bool defaultIsSet, int defaultMinutesBeforeStart)
			{
				return CalendarItem.CustomReminder.GetAdjacentOccurrence(recurrence, actualizationTime, defaultIsSet, defaultMinutesBeforeStart, true, false);
			}

			// Token: 0x17000DFE RID: 3582
			// (get) Token: 0x06002AE3 RID: 10979 RVA: 0x000AB515 File Offset: 0x000A9715
			private new CalendarItem Item
			{
				get
				{
					return (CalendarItem)base.Item;
				}
			}

			// Token: 0x06002AE4 RID: 10980 RVA: 0x000AB524 File Offset: 0x000A9724
			private static OccurrenceInfo GetAdjacentOccurrence(InternalRecurrence recurrence, ExDateTime actualizationTime, bool defaultIsSet, int defaultMinutesBeforeStart, bool forward, bool inclusive)
			{
				if (!defaultIsSet)
				{
					return null;
				}
				InternalRecurrence.IOccurrenceIterator occurrenceIterator = recurrence.CreateIterator(actualizationTime, !defaultIsSet);
				int num = forward ? 1 : -1;
				int num2 = inclusive ? 0 : num;
				occurrenceIterator = occurrenceIterator.Move(!forward);
				while (occurrenceIterator.Current != null)
				{
					int position = CalendarItem.CustomReminder.GetPosition(occurrenceIterator.Current, actualizationTime, defaultMinutesBeforeStart);
					if (position != num && position != num2)
					{
						break;
					}
					occurrenceIterator = occurrenceIterator.Move(!forward);
				}
				occurrenceIterator = occurrenceIterator.Move(forward);
				while (occurrenceIterator.Current != null)
				{
					int position2 = CalendarItem.CustomReminder.GetPosition(occurrenceIterator.Current, actualizationTime, defaultMinutesBeforeStart);
					if ((position2 == num || position2 == num2) && CalendarItem.CustomReminder.GetOccurrenceProperty<bool>(occurrenceIterator.Current, InternalSchema.ReminderIsSetInternal, defaultIsSet))
					{
						return occurrenceIterator.Current;
					}
					occurrenceIterator = occurrenceIterator.Move(forward);
				}
				return null;
			}

			// Token: 0x06002AE5 RID: 10981 RVA: 0x000AB5DC File Offset: 0x000A97DC
			private static int GetPosition(OccurrenceInfo occurrenceInfo, ExDateTime moment, int defaultMinutesBeforeStart)
			{
				if (occurrenceInfo.StartTime > occurrenceInfo.EndTime)
				{
					ExTraceGlobals.RecurrenceTracer.TraceDebug(0L, "Couldn't calculate position of the {0} [{1}; {2}) against {3}", new object[]
					{
						occurrenceInfo.GetType().Name,
						occurrenceInfo.StartTime,
						occurrenceInfo.EndTime,
						moment
					});
					throw new CorruptDataException(ServerStrings.ExEndDateEarlierThanStartDate);
				}
				if (occurrenceInfo.EndTime < moment)
				{
					return -1;
				}
				Reminder.ReminderInfo reminderInfo = CalendarItem.CustomReminder.OccurrenceToReminderInfo(occurrenceInfo, defaultMinutesBeforeStart);
				if (reminderInfo == null || !(reminderInfo.DefaultReminderNextTime <= moment))
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x06002AE6 RID: 10982 RVA: 0x000AB680 File Offset: 0x000A9880
			private static T GetOccurrenceProperty<T>(OccurrenceInfo occurrence, StorePropertyDefinition propertyDefinition, T valueOnMaster) where T : struct
			{
				ExceptionInfo exceptionInfo = occurrence as ExceptionInfo;
				if (exceptionInfo == null)
				{
					return valueOnMaster;
				}
				return exceptionInfo.PropertyBag.GetValueOrDefault<T>(propertyDefinition, valueOnMaster);
			}

			// Token: 0x17000DFF RID: 3583
			// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x000AB6A8 File Offset: 0x000A98A8
			private bool IsItemStateValid
			{
				get
				{
					ExDateTime? valueAsNullable = this.Item.GetValueAsNullable<ExDateTime>(InternalSchema.StartTime);
					ExDateTime? valueAsNullable2 = this.Item.GetValueAsNullable<ExDateTime>(InternalSchema.EndTime);
					return valueAsNullable != null && valueAsNullable2 != null && valueAsNullable.Value <= valueAsNullable2.Value;
				}
			}

			// Token: 0x06002AE8 RID: 10984 RVA: 0x000AB700 File Offset: 0x000A9900
			private static Reminder.ReminderInfo OccurrenceToReminderInfo(OccurrenceInfo occurrenceInfo, int defaultMinutesBeforeStart)
			{
				if (occurrenceInfo == null)
				{
					return null;
				}
				int occurrenceProperty = CalendarItem.CustomReminder.GetOccurrenceProperty<int>(occurrenceInfo, InternalSchema.ReminderMinutesBeforeStartInternal, defaultMinutesBeforeStart);
				ExDateTime? defaultReminderNextTime = Reminder.GetDefaultReminderNextTime(new ExDateTime?(occurrenceInfo.StartTime), occurrenceProperty);
				if (defaultReminderNextTime == null)
				{
					return null;
				}
				return new CalendarItem.CustomReminder.OccurrenceReminderInfo(occurrenceInfo.StartTime, defaultReminderNextTime.Value, occurrenceInfo);
			}

			// Token: 0x020003AC RID: 940
			private class OccurrenceReminderInfo : Reminder.ReminderInfo
			{
				// Token: 0x06002AE9 RID: 10985 RVA: 0x000AB74F File Offset: 0x000A994F
				public OccurrenceReminderInfo(ExDateTime defaultDueBy, ExDateTime defaultReminderNextTime, OccurrenceInfo occurrenceInfo) : base(defaultDueBy, defaultReminderNextTime, (occurrenceInfo != null) ? occurrenceInfo.VersionedId : null)
				{
					if (occurrenceInfo != null)
					{
						this.originalStartTime = occurrenceInfo.OriginalStartTime;
					}
				}

				// Token: 0x06002AEA RID: 10986 RVA: 0x000AB774 File Offset: 0x000A9974
				protected override bool? IsForSamePertinentItem(Reminder.ReminderInfo reminderInfo)
				{
					CalendarItem.CustomReminder.OccurrenceReminderInfo occurrenceReminderInfo = reminderInfo as CalendarItem.CustomReminder.OccurrenceReminderInfo;
					return new bool?(occurrenceReminderInfo != null && this.originalStartTime == occurrenceReminderInfo.originalStartTime);
				}

				// Token: 0x04001826 RID: 6182
				private readonly ExDateTime originalStartTime;
			}
		}
	}
}
