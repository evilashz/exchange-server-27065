using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003DA RID: 986
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MeetingMessageInstance : MeetingMessage, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06002CB9 RID: 11449 RVA: 0x000B6974 File Offset: 0x000B4B74
		internal MeetingMessageInstance(ICoreItem coreItem) : base(coreItem)
		{
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06002CBA RID: 11450 RVA: 0x000B697D File Offset: 0x000B4B7D
		// (set) Token: 0x06002CBB RID: 11451 RVA: 0x000B6995 File Offset: 0x000B4B95
		public override bool IsProcessed
		{
			get
			{
				this.CheckDisposed("IsProcessed::get");
				return base.GetValueOrDefault<bool>(MeetingMessageInstanceSchema.IsProcessed);
			}
			set
			{
				this.CheckDisposed("IsProcessed::set");
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(36469U);
				this[MeetingMessageInstanceSchema.IsProcessed] = value;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06002CBC RID: 11452 RVA: 0x000B69C4 File Offset: 0x000B4BC4
		public override GlobalObjectId GlobalObjectId
		{
			get
			{
				if (this.cachedGlobalObjectId != null)
				{
					return this.cachedGlobalObjectId;
				}
				byte[] valueOrDefault = base.GetValueOrDefault<byte[]>(InternalSchema.GlobalObjectId);
				this.cachedGlobalObjectId = new GlobalObjectId(valueOrDefault);
				return this.cachedGlobalObjectId;
			}
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x000B69FE File Offset: 0x000B4BFE
		public new static MeetingMessageInstance Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn = null)
		{
			return ItemBuilder.ItemBind<MeetingMessageInstance>(session, storeId, MeetingMessageInstanceSchema.Instance, propsToReturn);
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x000B6A10 File Offset: 0x000B4C10
		public override CalendarItemBase GetCorrelatedItem()
		{
			IEnumerable<VersionedId> enumerable;
			return this.GetCorrelatedItem(false, out enumerable);
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x000B6A28 File Offset: 0x000B4C28
		public override CalendarItemBase GetCorrelatedItem(bool shouldDetectDuplicateIds, out IEnumerable<VersionedId> detectedDuplicatesIds)
		{
			this.CheckDisposed("GetCorrelatedItem");
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.GetCorrelatedItem: GOID={0}", this.GlobalObjectId);
			if (this.externalCorrelatedCalendarItem != null && !this.externalCorrelatedCalendarItem.IsDisposed)
			{
				throw new CalendarProcessingException(ServerStrings.ExCannotOpenMultipleCorrelatedItems);
			}
			this.externalCorrelatedCalendarItem = this.GetCorrelatedItemInternal(false, shouldDetectDuplicateIds, out detectedDuplicatesIds);
			if (this.externalCorrelatedCalendarItem != null && !this.ValidateCorrelatedItem(this.externalCorrelatedCalendarItem))
			{
				this.externalCorrelatedCalendarItem.Dispose();
				this.externalCorrelatedCalendarItem = null;
				throw new CorrelationFailedException(ServerStrings.ExCorrelationFailedForOccurrence(this.Subject));
			}
			if (this.externalCorrelatedCalendarItem != null)
			{
				this.externalCorrelatedCalendarItem.IsCorrelated = true;
			}
			else
			{
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.GetCorrelatedItem: GOID={0}. There is no correlated item", this.GlobalObjectId);
			}
			this.getCorrelatedItemCalled = true;
			return this.externalCorrelatedCalendarItem;
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x000B6B04 File Offset: 0x000B4D04
		public override CalendarItemBase TryGetCorrelatedItemFromHintId(MailboxSession session, StoreObjectId hintId)
		{
			if (hintId != null)
			{
				try
				{
					CalendarItemBase calendarItemBase = CalendarItemBase.Bind(session, hintId);
					if (!this.ValidateCorrelatedItem(calendarItemBase))
					{
						calendarItemBase.Dispose();
						throw new CorrelationFailedException(ServerStrings.ExCorrelationFailedForOccurrence(this.Subject));
					}
					calendarItemBase.OpenAsReadWrite();
					calendarItemBase.IsCorrelated = true;
					this.correlatedItemId = calendarItemBase.Id;
					calendarItemBase.AssociatedItemId = base.Id;
					this.externalCorrelatedCalendarItem = calendarItemBase;
					this.getCorrelatedItemCalled = true;
					return calendarItemBase;
				}
				catch (Exception arg)
				{
					ExTraceGlobals.MeetingMessageTracer.TraceError<string, Exception>((long)this.GetHashCode(), "Exception thrown when trying to bind to calendar item id hint passed from meeting series message ordering agent. Mailbox = {0}, exception = {1}.", session.MailboxOwnerLegacyDN, arg);
				}
			}
			return null;
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x000B6BA8 File Offset: 0x000B4DA8
		public override CalendarItemBase GetCachedCorrelatedItem()
		{
			return this.GetCorrelatedItemInternal(true);
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000B6C54 File Offset: 0x000B4E54
		public override CalendarItemBase GetEmbeddedItem()
		{
			this.CheckDisposed("GetEmbeddedItem");
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.GetEmbeddedItem: GOID={0}", this.GlobalObjectId);
			CalendarItemBase calendarItemBase = null;
			bool flag = false;
			CalendarItemBase result;
			try
			{
				ItemCreateInfo calendarItemInfo = ItemCreateInfo.CalendarItemInfo;
				calendarItemBase = ItemBuilder.ConstructItem<CalendarItem>(base.Session, base.StoreObjectId, (base.StoreObjectId != null) ? base.Id.ChangeKeyAsByteArray() : null, calendarItemInfo.Schema.AutoloadProperties, delegate
				{
					AcrPropertyBag acrPropertyBag = null;
					PersistablePropertyBag persistablePropertyBag = null;
					bool flag2 = false;
					StoreObjectId storeObjectId = base.StoreObjectId;
					PersistablePropertyBag result2;
					try
					{
						if (storeObjectId != null && !storeObjectId.IsFakeId)
						{
							persistablePropertyBag = StoreObjectPropertyBag.CreatePropertyBag(base.Session, storeObjectId, InternalSchema.ContentConversionProperties);
							acrPropertyBag = new AcrPropertyBag(persistablePropertyBag, AcrProfile.AppointmentProfile, storeObjectId, new ItemBagFactory(base.Session, base.StoreObjectId), null);
							flag2 = true;
							result2 = acrPropertyBag;
						}
						else
						{
							persistablePropertyBag = new StoreObjectPropertyBag(base.Session, base.MapiProp, InternalSchema.ContentConversionProperties, false);
							flag2 = true;
							result2 = persistablePropertyBag;
						}
					}
					finally
					{
						if (!flag2)
						{
							Util.DisposeIfPresent(persistablePropertyBag);
							Util.DisposeIfPresent(acrPropertyBag);
						}
					}
					return result2;
				}, calendarItemInfo.Creator, base.CoreObject.Origin, base.CoreObject.ItemLevel);
				flag = true;
				result = calendarItemBase;
			}
			finally
			{
				if (!flag && calendarItemBase != null)
				{
					calendarItemBase.Dispose();
					calendarItemBase = null;
				}
			}
			return result;
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x000B6D1C File Offset: 0x000B4F1C
		public override CalendarItemBase GetCachedEmbeddedItem()
		{
			if (this.cachedEmbeddedItem == null)
			{
				this.cachedEmbeddedItem = this.GetEmbeddedItem();
			}
			return this.cachedEmbeddedItem;
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000B6D38 File Offset: 0x000B4F38
		public override bool TryUpdateCalendarItem(ref CalendarItemBase originalCalendarItem, bool canUpdatePrincipalCalendar)
		{
			this.CheckDisposed("MeetingRequest::TryUpdateCalendarItem");
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, int>((long)this.GetHashCode(), "Storage.MeetingMessage.TryUpdateCalendarItem: GOID={0}; originalCalendarItem={1}", this.GlobalObjectId, (originalCalendarItem == null) ? -1 : originalCalendarItem.GetHashCode());
			object obj = base.TryGetProperty(InternalSchema.OriginalMeetingType);
			if (obj is int)
			{
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(61557U);
				this[InternalSchema.MeetingRequestType] = (MeetingMessageType)obj;
			}
			base.CheckPreConditionForDelegatedMeeting(canUpdatePrincipalCalendar);
			if (!this.fetchCorrelatedItemIdCalled && !this.getCorrelatedItemCalled)
			{
				throw new CalendarProcessingException(ServerStrings.ExInvalidCallToTryUpdateCalendarItem);
			}
			return this.TryUpdateCalendarItemInternal(ref originalCalendarItem, false, canUpdatePrincipalCalendar);
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x000B6DE0 File Offset: 0x000B4FE0
		public override CalendarItemBase UpdateCalendarItem(bool canUpdatePrincipalCalendar)
		{
			this.CheckDisposed("UpdateCalendarItem");
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.UpdateCalendarItem: GOID={0}", this.GlobalObjectId);
			object obj = base.TryGetProperty(InternalSchema.OriginalMeetingType);
			if (obj is int)
			{
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(36981U);
				this[InternalSchema.MeetingRequestType] = (MeetingMessageType)obj;
			}
			base.CheckPreConditionForDelegatedMeeting(canUpdatePrincipalCalendar);
			CalendarItemBase calendarItemBase = this.GetCorrelatedItem();
			bool flag = calendarItemBase == null;
			bool flag2 = false;
			try
			{
				this.TryUpdateCalendarItemInternal(ref calendarItemBase, true, canUpdatePrincipalCalendar);
				flag2 = true;
			}
			finally
			{
				if (!flag2 && calendarItemBase != null)
				{
					calendarItemBase.Dispose();
					calendarItemBase = null;
				}
			}
			if (flag && calendarItemBase != null)
			{
				calendarItemBase.AssociatedItemId = base.Id;
			}
			return calendarItemBase;
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06002CC6 RID: 11462 RVA: 0x000B6EA4 File Offset: 0x000B50A4
		public override bool IsRecurringMaster
		{
			get
			{
				this.CheckDisposed("IsRecurringMaster::get");
				return InternalRecurrence.HasRecurrenceBlob(base.PropertyBag);
			}
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x000B6EBC File Offset: 0x000B50BC
		public override CalendarItemBase PreProcess(bool createNewItem, bool processExternal, int defaultReminderMinutes)
		{
			this.CheckDisposed("PreProcess");
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(44661U, LastChangeAction.PreProcessMeetingMessage);
			bool canUpdatePrincipalCalendar = true;
			CalendarItemBase calendarItemBase = null;
			bool flag = false;
			if (base.GetValueOrDefault<MeetingMessageType>(InternalSchema.MeetingRequestType) == MeetingMessageType.Outdated)
			{
				return null;
			}
			try
			{
				calendarItemBase = this.GetCorrelatedItem();
				if (base.IsOutOfDate(calendarItemBase))
				{
					base.MarkAsOutOfDate();
					base.LocationIdentifierHelperInstance.SetLocationIdentifier(59381U, LastChangeAction.MeetingMessageOutdated);
					return null;
				}
				if (base.SkipMessage(processExternal, calendarItemBase))
				{
					ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, bool, bool>((long)this.GetHashCode(), "Storage.MeetingMessage.PreProcess: GOID={0}. Skipping meeting message processing. (ProcessExternal: {1}; IsRepairUpdateMessage: {2})", this.GlobalObjectId, processExternal, base.IsRepairUpdateMessage);
					return null;
				}
				CalendarProcessingSteps valueOrDefault = base.GetValueOrDefault<CalendarProcessingSteps>(InternalSchema.CalendarProcessingSteps);
				bool flag2 = (valueOrDefault & CalendarProcessingSteps.CreatedOnPrincipal) == CalendarProcessingSteps.CreatedOnPrincipal;
				bool flag3 = (valueOrDefault & CalendarProcessingSteps.UpdatedCalItem) == CalendarProcessingSteps.UpdatedCalItem;
				flag = (flag2 || flag3);
				if (flag)
				{
					ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.PreProcess: GOID={0}. Skipping meeting message processing because it was already created.", this.GlobalObjectId);
				}
				else if (createNewItem || calendarItemBase != null)
				{
					MeetingRequest meetingRequest = this as MeetingRequest;
					if (meetingRequest != null)
					{
						flag = meetingRequest.TryUpdateCalendarItem(ref calendarItemBase, defaultReminderMinutes, canUpdatePrincipalCalendar);
					}
					else
					{
						MeetingCancellation meetingCancellation = this as MeetingCancellation;
						if (meetingCancellation != null)
						{
							flag = meetingCancellation.TryUpdateCalendarItem(ref calendarItemBase, canUpdatePrincipalCalendar);
						}
					}
				}
				else
				{
					ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.PreProcess: GOID={0}. Skipping new meeting request.", this.GlobalObjectId);
				}
			}
			finally
			{
				if (!flag && calendarItemBase != null)
				{
					ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.PreProcess: GOID={0}. Disposing calendar item; preconditions failed.", this.GlobalObjectId);
					calendarItemBase.Dispose();
					calendarItemBase = null;
				}
			}
			return calendarItemBase;
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x000B7058 File Offset: 0x000B5258
		public override void SetCalendarProcessingSteps(CalendarProcessingSteps stepComplete)
		{
			this.CheckDisposed("SetCalendarProcessingSteps");
			EnumValidator.ThrowIfInvalid<CalendarProcessingSteps>(stepComplete, "stepComplete");
			CalendarProcessingSteps? valueAsNullable = base.GetValueAsNullable<CalendarProcessingSteps>(InternalSchema.CalendarProcessingSteps);
			CalendarProcessingSteps calendarProcessingSteps = stepComplete;
			if (valueAsNullable != null)
			{
				if ((valueAsNullable.Value & stepComplete) == stepComplete)
				{
					return;
				}
				calendarProcessingSteps = (valueAsNullable.Value | stepComplete);
			}
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(60021U, LastChangeAction.SetCalendarProcessingSteps);
			this[InternalSchema.CalendarProcessingSteps] = calendarProcessingSteps;
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x000B70CC File Offset: 0x000B52CC
		public VersionedId FetchCorrelatedItemId(CalendarFolder calendarFolder, bool shouldDetectDuplicateIds, out IEnumerable<VersionedId> detectedDuplicatesIds)
		{
			if (!shouldDetectDuplicateIds && this.correlatedItemId != null)
			{
				detectedDuplicatesIds = null;
				return this.correlatedItemId;
			}
			byte[] array = base.TryGetProperty(InternalSchema.GlobalObjectId) as byte[];
			if (array == null)
			{
				throw new CorruptDataException(ServerStrings.ExInvalidGlobalObjectId);
			}
			if (!(calendarFolder.Session is MailboxSession))
			{
				throw new NotSupportedException();
			}
			this.correlatedItemId = calendarFolder.GetCalendarItemId(array, shouldDetectDuplicateIds, out detectedDuplicatesIds);
			this.fetchCorrelatedItemIdCalled = true;
			return this.correlatedItemId;
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x000B7140 File Offset: 0x000B5340
		internal override CalendarItemBase GetCorrelatedItemInternal(bool cache)
		{
			IEnumerable<VersionedId> enumerable;
			return this.GetCorrelatedItemInternal(cache, false, out enumerable);
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x000B7158 File Offset: 0x000B5358
		internal override CalendarItemBase GetCorrelatedItemInternal(bool cache, bool shouldDetectDuplicateIds, out IEnumerable<VersionedId> detectedDuplicatesIds)
		{
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, bool>((long)this.GetHashCode(), "Storage.MeetingMessage.GetCorrelatedItem: GOID={0}; cache={1}", this.GlobalObjectId, cache);
			if (!(base.Session is MailboxSession))
			{
				detectedDuplicatesIds = null;
				return null;
			}
			if (this.cachedCorrelatedItem != null)
			{
				if (!shouldDetectDuplicateIds)
				{
					ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.GetCorrelatedItemInternal: GOID={0}. Returning cached correlated item.", this.GlobalObjectId);
					CalendarItemBase result = this.cachedCorrelatedItem;
					if (!cache)
					{
						this.cachedCorrelatedItem.SetDisposeTrackerStacktraceToCurrentLocation();
						this.cachedCorrelatedItem = null;
					}
					detectedDuplicatesIds = Array<VersionedId>.Empty;
					return result;
				}
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.GetCorrelatedItemInternal: GOID={0}. Clearing cached item.", this.GlobalObjectId);
				this.cachedCorrelatedItem.Dispose();
				this.cachedCorrelatedItem = null;
			}
			MailboxSession calendarMailboxSession;
			try
			{
				calendarMailboxSession = MeetingMessage.GetCalendarMailboxSession(this);
			}
			catch (NotSupportedWithServerVersionException ex)
			{
				ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "MeetingMessage::GetCorrelatedItemInternal. Calendar mailbox session failed to open due to not supported server version.");
				throw new CorrelationFailedException(ex.LocalizedString, ex);
			}
			catch (AdUserNotFoundException ex2)
			{
				ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "MeetingMessage::GetCorrelatedItemInternal. Calendar mailbox session failed to open due to not being able to find the owner's mailbox.");
				throw new CorrelationFailedException(ex2.LocalizedString, ex2);
			}
			StoreObjectId defaultFolderId = calendarMailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar);
			if (defaultFolderId == null)
			{
				ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "MeetingMessage::GetCorrelatedItemInternal. Calendar folder is not found. We treat it as correlated calendar item has been deleted.");
				detectedDuplicatesIds = null;
				return null;
			}
			CalendarItemBase calendarItemBase = null;
			bool flag = false;
			try
			{
				using (CalendarFolder calendarFolder = CalendarFolder.Bind(calendarMailboxSession, defaultFolderId))
				{
					int i = 0;
					while (i < 2)
					{
						ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, int>((long)this.GetHashCode(), "Storage.MeetingMessage.GetCorrelatedItemInternal: GOID={0}. Retrying fetch count={1}", this.GlobalObjectId, i);
						this.FetchCorrelatedItemId(calendarFolder, shouldDetectDuplicateIds, out detectedDuplicatesIds);
						if (this.correlatedItemId != null)
						{
							try
							{
								calendarItemBase = CalendarItemBase.Bind(calendarMailboxSession, this.correlatedItemId.ObjectId);
							}
							catch (OccurrenceNotFoundException)
							{
								ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.GetCorrelatedItemInternal: GOID={0}. Occurrence not found.", this.GlobalObjectId);
								this.possibleDeletedOccurrenceId = this.correlatedItemId.ObjectId;
								this.correlatedItemId = null;
								break;
							}
							catch (ObjectNotFoundException)
							{
								ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.GetCorrelatedItemInternal: GOID={0}. Calendar item id was found but calendar item got deleted.", this.GlobalObjectId);
								this.correlatedItemId = null;
								goto IL_259;
							}
							catch (WrongObjectTypeException innerException)
							{
								throw new CorruptDataException(ServerStrings.ExNonCalendarItemReturned("Unknown"), innerException);
							}
							catch (ArgumentException)
							{
								throw new CorruptDataException(ServerStrings.ExNonCalendarItemReturned("Unknown"));
							}
							goto IL_20D;
							IL_259:
							i++;
							continue;
							IL_20D:
							calendarItemBase.OpenAsReadWrite();
							if (base.Id != null)
							{
								calendarItemBase.AssociatedItemId = base.Id;
							}
							if (cache)
							{
								ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.GetCorrelatedItemInternal: GOID={0}. Caching correlated calendar item.", this.GlobalObjectId);
								this.cachedCorrelatedItem = calendarItemBase;
							}
							flag = true;
							return calendarItemBase;
						}
						break;
					}
				}
			}
			finally
			{
				if (!flag)
				{
					Util.DisposeIfPresent(calendarItemBase);
				}
			}
			detectedDuplicatesIds = null;
			return null;
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x000B74B4 File Offset: 0x000B56B4
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				Util.DisposeIfPresent(this.cachedCorrelatedItem);
				this.cachedCorrelatedItem = null;
				Util.DisposeIfPresent(this.cachedEmbeddedItem);
				this.cachedEmbeddedItem = null;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000B74E4 File Offset: 0x000B56E4
		protected internal override CalendarItemOccurrence RecoverDeletedOccurrence()
		{
			if (this.possibleDeletedOccurrenceId != null)
			{
				MailboxSession calendarMailboxSession = MeetingMessage.GetCalendarMailboxSession(this);
				for (int i = 0; i < 2; i++)
				{
					CalendarItem calendarItem = CalendarItem.Bind(calendarMailboxSession, StoreObjectId.FromProviderSpecificId(this.possibleDeletedOccurrenceId.ProviderLevelItemId, StoreObjectType.CalendarItem));
					try
					{
						calendarItem.OpenAsReadWrite();
						if (calendarItem.Recurrence != null)
						{
							InternalRecurrence internalRecurrence = (InternalRecurrence)calendarItem.Recurrence;
							ExDateTime occurrenceId = ((OccurrenceStoreObjectId)this.possibleDeletedOccurrenceId).OccurrenceId;
							if (internalRecurrence.IsValidOccurrenceId(occurrenceId))
							{
								if (internalRecurrence.IsOccurrenceDeleted(occurrenceId))
								{
									base.LocationIdentifierHelperInstance.SetLocationIdentifier(39541U, LastChangeAction.RecoverDeletedOccurance);
									calendarItem.RecoverDeletedOccurrence(this.possibleDeletedOccurrenceId);
									if (calendarItem.Save(SaveMode.ResolveConflicts).SaveStatus == SaveResult.IrresolvableConflict)
									{
										goto IL_136;
									}
								}
								CalendarItemOccurrence calendarItemOccurrence = CalendarItemOccurrence.Bind(calendarMailboxSession, this.possibleDeletedOccurrenceId, MeetingMessageSchema.Instance.AutoloadProperties);
								calendarItemOccurrence.OpenAsReadWrite();
								ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.RecoverDeletedOccurrence: GOID={0}; occurrence recovered.", this.GlobalObjectId);
								return calendarItemOccurrence;
							}
							ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.RecoverDeletedOccurrence: GOID={0}; occurrence id is invalid.", this.GlobalObjectId);
							return null;
						}
					}
					catch (OccurrenceNotFoundException)
					{
						ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.RecoverDeletedOccurrence: GOID={0}; occurrence not found.", this.GlobalObjectId);
						return null;
					}
					finally
					{
						calendarItem.Dispose();
					}
					IL_136:;
				}
			}
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.RecoverDeletedOccurrence: GOID={0}; occurrence not recovered.", this.GlobalObjectId);
			return null;
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x000B7670 File Offset: 0x000B5870
		private bool ValidateCorrelatedItem(CalendarItemBase calendarItem)
		{
			byte[] largeBinaryProperty = calendarItem.PropertyBag.GetLargeBinaryProperty(InternalSchema.AppointmentRecurrenceBlob);
			bool valueOrDefault = calendarItem.GetValueOrDefault<bool>(InternalSchema.AppointmentRecurring);
			bool flag = this is MeetingResponse;
			if (flag && valueOrDefault)
			{
				bool valueOrDefault2 = base.GetValueOrDefault<bool>(InternalSchema.AppointmentCounterProposal);
				if (valueOrDefault2)
				{
					return false;
				}
			}
			if (largeBinaryProperty == null || !valueOrDefault)
			{
				return true;
			}
			bool valueOrDefault3 = base.GetValueOrDefault<bool>(InternalSchema.IsException);
			if (valueOrDefault3)
			{
				return false;
			}
			int valueOrDefault4 = base.GetValueOrDefault<int>(InternalSchema.AppointmentSequenceNumber);
			int valueOrDefault5 = calendarItem.GetValueOrDefault<int>(InternalSchema.AppointmentSequenceNumber);
			if (valueOrDefault4 > valueOrDefault5)
			{
				return true;
			}
			bool flag2 = InternalRecurrence.HasRecurrenceBlob(base.PropertyBag);
			bool valueOrDefault6 = base.GetValueOrDefault<bool>(InternalSchema.IsRecurring);
			bool? valueAsNullable = base.GetValueAsNullable<bool>(InternalSchema.AppointmentRecurring);
			bool flag3 = flag2 && valueOrDefault6 && (valueAsNullable == null || valueAsNullable.Value);
			if (flag3)
			{
				return true;
			}
			bool flag4 = this is MeetingForwardNotification;
			bool flag5 = this is MeetingCancellation && new GlobalObjectId(this).IsForeignUid();
			return flag || flag5 || flag4;
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x000B7778 File Offset: 0x000B5978
		protected override bool CheckPreConditions(CalendarItemBase originalCalendarItem, bool shouldThrow, bool canUpdatePrincipalCalendar)
		{
			string text = null;
			if (base.IsDelegated() && !canUpdatePrincipalCalendar)
			{
				text = "Not supported meeting message operation for delegated message.";
			}
			if (base.IsOutOfDate(originalCalendarItem))
			{
				base.MarkAsOutOfDate();
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(42997U, LastChangeAction.MeetingMessageOutdated);
				text = "Cannot update calendar item with out of date message.";
			}
			if (shouldThrow && text != null)
			{
				throw new NotSupportedException(text);
			}
			return text == null;
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x000B77D0 File Offset: 0x000B59D0
		protected internal override int CompareToCalendarItem(CalendarItemBase correlatedCalendarItem)
		{
			base.LocationIdentifierHelperInstance.SetLocationIdentifier(61429U, LastChangeAction.CompareToCalendarItem);
			int num;
			if (correlatedCalendarItem != null && correlatedCalendarItem.Id != null)
			{
				int? valueAsNullable = base.PropertyBag.GetValueAsNullable<int>(InternalSchema.AppointmentSequenceNumber);
				int? valueAsNullable2 = correlatedCalendarItem.PropertyBag.GetValueAsNullable<int>(InternalSchema.AppointmentSequenceNumber);
				if (valueAsNullable == null || valueAsNullable2 == null || valueAsNullable == valueAsNullable2)
				{
					bool? valueAsNullable3 = base.PropertyBag.GetValueAsNullable<bool>(InternalSchema.AppointmentRecurring);
					bool? valueAsNullable4 = correlatedCalendarItem.PropertyBag.GetValueAsNullable<bool>(InternalSchema.AppointmentRecurring);
					if (valueAsNullable3 != null && valueAsNullable3 == true && (valueAsNullable4 == null || valueAsNullable4 == false))
					{
						base.LocationIdentifierHelperInstance.SetLocationIdentifier(53749U, LastChangeAction.CompareToCalendarItem);
						num = 1;
					}
					else
					{
						ExDateTime? valueAsNullable5 = base.PropertyBag.GetValueAsNullable<ExDateTime>(InternalSchema.OwnerCriticalChangeTime);
						ExDateTime? valueAsNullable6 = correlatedCalendarItem.PropertyBag.GetValueAsNullable<ExDateTime>(InternalSchema.OwnerCriticalChangeTime);
						if (valueAsNullable5 != null)
						{
							if (valueAsNullable6 == null)
							{
								base.LocationIdentifierHelperInstance.SetLocationIdentifier(36853U, LastChangeAction.CompareToCalendarItem);
								num = 0;
							}
							else
							{
								base.LocationIdentifierHelperInstance.SetLocationIdentifier(53237U, LastChangeAction.CompareToCalendarItem);
								num = ExDateTime.Compare(valueAsNullable5.GetValueOrDefault(), valueAsNullable6.GetValueOrDefault(), Util.DateTimeComparisonRange);
								if (num == 0 && base.PropertyBag.GetValueOrDefault<bool>(InternalSchema.IsException))
								{
									valueAsNullable5 = base.PropertyBag.GetValueAsNullable<ExDateTime>(InternalSchema.AttendeeCriticalChangeTime);
									if (this is MeetingResponse)
									{
										Attendee attendee = base.FindAttendee(correlatedCalendarItem);
										if (attendee == null)
										{
											valueAsNullable6 = new ExDateTime?(ExDateTime.MinValue);
										}
										else
										{
											valueAsNullable6 = new ExDateTime?(attendee.ReplyTime);
										}
									}
									else
									{
										valueAsNullable6 = new ExDateTime?(correlatedCalendarItem.PropertyBag.GetValueOrDefault<ExDateTime>(InternalSchema.AttendeeCriticalChangeTime, ExDateTime.MinValue));
									}
									if (valueAsNullable6.GetValueOrDefault() == CalendarItemBase.OutlookRtmNone)
									{
										valueAsNullable6 = new ExDateTime?(ExDateTime.MinValue);
									}
									if (valueAsNullable5 != null)
									{
										base.LocationIdentifierHelperInstance.SetLocationIdentifier(47093U, LastChangeAction.CompareToCalendarItem);
										num = ExDateTime.Compare(valueAsNullable5.GetValueOrDefault(), valueAsNullable6.GetValueOrDefault(), Util.DateTimeComparisonRange);
									}
								}
							}
						}
						else
						{
							base.LocationIdentifierHelperInstance.SetLocationIdentifier(63477U, LastChangeAction.CompareToCalendarItem);
							num = 1;
						}
					}
				}
				else
				{
					base.LocationIdentifierHelperInstance.SetLocationIdentifier(38901U, LastChangeAction.CompareToCalendarItem);
					num = valueAsNullable.Value - valueAsNullable2.Value;
				}
			}
			else
			{
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(55285U, LastChangeAction.CompareToCalendarItem);
				num = 1;
			}
			if (num < 0)
			{
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.IsOutOfDate: GOID={0}; isOutOfDate=true", this.GlobalObjectId);
			}
			return num;
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x000B7AA4 File Offset: 0x000B5CA4
		private static void SetVersionInfo(CalendarItemBase originalCalendarItem)
		{
			if (originalCalendarItem != null && originalCalendarItem.IsDirty)
			{
				originalCalendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(49269U, LastChangeAction.SetVersion);
				ExDateTime now = ExDateTime.GetNow(originalCalendarItem.PropertyBag.ExTimeZone);
				originalCalendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(48757U);
				originalCalendarItem[CalendarItemBaseSchema.AppointmentExtractVersion] = StorageGlobals.BuildVersion;
				originalCalendarItem[CalendarItemBaseSchema.AppointmentExtractTime] = now;
				ExTraceGlobals.MeetingMessageTracer.Information<long, ExDateTime>((long)originalCalendarItem.GetHashCode(), "Storage.MeetingMessage.SetVersionInfo: ExtractVersion={0}; ExtractTime={1}.", StorageGlobals.BuildVersion, now);
			}
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000B7B34 File Offset: 0x000B5D34
		private bool TryUpdateCalendarItemInternal(ref CalendarItemBase originalCalendarItem, bool shouldThrow, bool canUpdatePrincipalCalendar)
		{
			this.CheckDisposed("MeetingRequest::TryUpdateCalendarItem");
			bool flag = originalCalendarItem == null;
			CalendarProcessingSteps calendarProcessingSteps = CalendarProcessingSteps.None;
			ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.TryUpdateCalendarItemInternal: GOID={0}", this.GlobalObjectId);
			base.CheckPreConditionForDelegatedMeeting(canUpdatePrincipalCalendar);
			if (originalCalendarItem != this.externalCorrelatedCalendarItem)
			{
				throw new CalendarProcessingException(ServerStrings.ExInvalidCallToTryUpdateCalendarItem);
			}
			if (!this.CheckPreConditions(originalCalendarItem, shouldThrow, canUpdatePrincipalCalendar))
			{
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId>((long)this.GetHashCode(), "Storage.MeetingMessage.TryUpdateCalendarItemInternal: GOID={0}; failed pre-conditions", this.GlobalObjectId);
				return false;
			}
			if (originalCalendarItem != null)
			{
				originalCalendarItem.IsCorrelated = false;
			}
			calendarProcessingSteps |= CalendarProcessingSteps.PropsCheck;
			DelegateRuleType? delegateRuleType;
			if (MeetingMessage.TryGetDelegateRuleTypeFromSession(base.Session as MailboxSession, out delegateRuleType))
			{
				if (delegateRuleType == DelegateRuleType.ForwardAndSetAsInformationalUpdate)
				{
					ExTraceGlobals.MeetingMessageTracer.Information((long)this.GetHashCode(), "Storage.MeetingMessage.TryUpdateCalendarItemInternal: Setting PrincipalWantsCopy on meetingmessage.");
					MeetingMessageType valueOrDefault = base.GetValueOrDefault<MeetingMessageType>(InternalSchema.MeetingRequestType, MeetingMessageType.NewMeetingRequest);
					base.LocationIdentifierHelperInstance.SetLocationIdentifier(53365U);
					this[InternalSchema.OriginalMeetingType] = valueOrDefault;
					this[InternalSchema.MeetingRequestType] = MeetingMessageType.PrincipalWantsCopy;
				}
				calendarProcessingSteps |= CalendarProcessingSteps.PrincipalWantsCopyChecked;
			}
			int num = flag ? 1 : this.CompareToCalendarItem(originalCalendarItem);
			calendarProcessingSteps |= CalendarProcessingSteps.LookedForOutOfDate;
			if ((!base.CalendarProcessed && num == 0) || num > 0)
			{
				base.LocationIdentifierHelperInstance.SetLocationIdentifier(57461U, LastChangeAction.UpdateCalendarItem);
				this.UpdateCalendarItemInternal(ref originalCalendarItem);
				base.CalendarProcessed = true;
				if (originalCalendarItem != null)
				{
					originalCalendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(32885U, LastChangeAction.UpdateCalendarItem);
				}
				MeetingMessageInstance.SetVersionInfo(originalCalendarItem);
				calendarProcessingSteps |= CalendarProcessingSteps.UpdatedCalItem;
			}
			else
			{
				ExTraceGlobals.MeetingMessageTracer.Information<GlobalObjectId, bool, int>((long)this.GetHashCode(), "Storage.MeetingMessage.TryUpdateCalendarItemInternal: GOID={0}; NOOP: calendarprocessed = {1}, originalCalendarItem={2}", this.GlobalObjectId, base.CalendarProcessed, (originalCalendarItem == null) ? -1 : originalCalendarItem.GetHashCode());
			}
			if (flag && originalCalendarItem != null)
			{
				this.externalCorrelatedCalendarItem = originalCalendarItem;
				calendarProcessingSteps |= CalendarProcessingSteps.CreatedOnPrincipal;
			}
			if (originalCalendarItem != null)
			{
				originalCalendarItem.CharsetDetector.DetectionOptions = base.CharsetDetector.DetectionOptions;
			}
			this.SetCalendarProcessingSteps(calendarProcessingSteps);
			return true;
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000B7D28 File Offset: 0x000B5F28
		public override string GenerateWhen(CultureInfo preferedCulture, ExTimeZone preferredTimeZone)
		{
			return CalendarItem.InternalWhen(this, null, false, preferredTimeZone).ToString(preferedCulture);
		}

		// Token: 0x040018E2 RID: 6370
		private StoreObjectId possibleDeletedOccurrenceId;

		// Token: 0x040018E3 RID: 6371
		private VersionedId correlatedItemId;

		// Token: 0x040018E4 RID: 6372
		private CalendarItemBase cachedCorrelatedItem;

		// Token: 0x040018E5 RID: 6373
		private CalendarItemBase cachedEmbeddedItem;

		// Token: 0x040018E6 RID: 6374
		private CalendarItemBase externalCorrelatedCalendarItem;

		// Token: 0x040018E7 RID: 6375
		private bool getCorrelatedItemCalled;

		// Token: 0x040018E8 RID: 6376
		private bool fetchCorrelatedItemIdCalled;

		// Token: 0x040018E9 RID: 6377
		private GlobalObjectId cachedGlobalObjectId;
	}
}
