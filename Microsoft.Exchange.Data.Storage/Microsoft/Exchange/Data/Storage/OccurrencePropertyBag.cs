using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200009A RID: 154
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OccurrencePropertyBag : PersistablePropertyBag
	{
		// Token: 0x06000AB0 RID: 2736 RVA: 0x00049D4E File Offset: 0x00047F4E
		internal OccurrencePropertyBag(StoreSession storeSession, PersistablePropertyBag masterPropertyBag, OccurrenceInfo occurrenceInfo, ICollection<PropertyDefinition> additionalPrefetchProperties) : this(storeSession, masterPropertyBag, null, occurrenceInfo, additionalPrefetchProperties)
		{
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00049D5C File Offset: 0x00047F5C
		private OccurrencePropertyBag(StoreSession storeSession, PersistablePropertyBag masterPropertyBag, InternalRecurrence masterRecurrence, OccurrenceInfo occurrenceInfo, ICollection<PropertyDefinition> additionalPrefetchProperties)
		{
			this.masterRecurrence = masterRecurrence;
			this.additionalPrefetchProperties = additionalPrefetchProperties;
			this.storeSession = storeSession;
			this.masterPropertyBag = masterPropertyBag;
			this.itemId = (OccurrenceStoreObjectId)occurrenceInfo.VersionedId.ObjectId;
			this.organizerTimeZone = (TimeZoneHelper.GetRecurringTimeZoneFromPropertyBag(masterPropertyBag) ?? masterPropertyBag.ExTimeZone);
			this.ownMasterMessage = true;
			this.originalStartTime = occurrenceInfo.OriginalStartTime;
			this.propertyCache = new MemoryPropertyBag();
			this.propertyCache.ExTimeZone = this.masterPropertyBag.ExTimeZone;
			this.InitializeDefaultProperties(occurrenceInfo);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00049DF6 File Offset: 0x00047FF6
		internal OccurrencePropertyBag(StoreSession storeSession, CalendarItem masterCalendarItem, InternalRecurrence masterRecurrence, OccurrenceInfo occurrenceInfo, ICollection<PropertyDefinition> additionalPrefetchProperties) : this(storeSession, masterCalendarItem.PropertyBag, masterRecurrence, occurrenceInfo, additionalPrefetchProperties)
		{
			this.masterCalendarItem = masterCalendarItem;
			this.ownMasterMessage = false;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00049E18 File Offset: 0x00048018
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<OccurrencePropertyBag>(this);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00049E20 File Offset: 0x00048020
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
			if (disposing)
			{
				if (this.embeddedMessage != null)
				{
					this.embeddedMessage.Dispose();
					this.embeddedMessage = null;
				}
				if (this.exceptionAttachment != null)
				{
					this.exceptionAttachment.Dispose();
					this.exceptionAttachment = null;
				}
				if (this.ownMasterMessage)
				{
					if (this.masterPropertyBag != null)
					{
						this.masterPropertyBag.Dispose();
						this.masterPropertyBag = null;
					}
					if (this.masterCalendarItem != null)
					{
						this.masterCalendarItem.Dispose();
						this.masterCalendarItem = null;
					}
				}
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00049EA7 File Offset: 0x000480A7
		protected override void SetValidatedStoreProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
		{
			this.InternalSetValidatedStoreProperty(propertyDefinition, propertyValue);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00049EB1 File Offset: 0x000480B1
		protected override object TryGetStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			return this.InternalTryGetStoreProperty(propertyDefinition);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00049EBA File Offset: 0x000480BA
		protected override void DeleteStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			if (this.embeddedMessage != null)
			{
				this.embeddedMessage.Delete(propertyDefinition);
			}
			if (this.propertyCache.ContainsKey(propertyDefinition))
			{
				this.propertyCache.Delete(propertyDefinition);
			}
			this.AddTrackingInformation(propertyDefinition, PropertyTrackingInformation.Deleted, null);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00049EF4 File Offset: 0x000480F4
		public override void Load(ICollection<PropertyDefinition> properties)
		{
			base.CheckDisposed("GetProperty");
			if (!this.isPropertyCacheLoaded)
			{
				this.masterCalendarItem.Load(properties);
				this.InitializeDefaultProperties(this.MasterRecurrence.GetOccurrenceInfoByDateId(this.itemId.OccurrenceId));
			}
			else
			{
				this.masterPropertyBag.Load(properties);
			}
			this.isEmbeddedMessageLoaded = false;
			this.FetchException();
			if (this.embeddedMessage != null)
			{
				this.embeddedMessage.Load(properties);
				this.exceptionAttachment.Load(null);
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00049F78 File Offset: 0x00048178
		public override void Clear()
		{
			base.CheckDisposed("Clear");
			if (this.masterCalendarItem != null)
			{
				this.masterCalendarItem.PropertyBag.Clear();
			}
			if (this.masterPropertyBag != null)
			{
				this.masterPropertyBag.Clear();
			}
			if (this.embeddedMessage != null)
			{
				this.embeddedMessage.PropertyBag.Clear();
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00049FD3 File Offset: 0x000481D3
		public override bool CanIgnoreUnchangedProperties
		{
			get
			{
				base.CheckDisposed("CanIgnoreUnchangedProperties::get");
				return false;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00049FE4 File Offset: 0x000481E4
		internal override ICollection<NativeStorePropertyDefinition> AllNativeProperties
		{
			get
			{
				this.FetchException();
				ICollection<NativeStorePropertyDefinition> allNativeProperties = this.masterCalendarItem.AllNativeProperties;
				if (this.IsException && this.embeddedMessage != null)
				{
					return InternalSchema.Combine<NativeStorePropertyDefinition>(allNativeProperties, this.embeddedMessage.AllNativeProperties);
				}
				return allNativeProperties;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x0004A028 File Offset: 0x00048228
		public override bool HasAllPropertiesLoaded
		{
			get
			{
				base.CheckDisposed("HasAllPropertiesLoaded::get");
				if (this.IsException && this.embeddedMessage != null)
				{
					return this.masterCalendarItem.HasAllPropertiesLoaded && this.embeddedMessage.HasAllPropertiesLoaded;
				}
				return this.masterCalendarItem.HasAllPropertiesLoaded;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0004A076 File Offset: 0x00048276
		internal override MapiProp MapiProp
		{
			get
			{
				base.CheckDisposed("MapiProp::get");
				this.FetchException();
				if (this.embeddedMessage == null)
				{
					return this.masterPropertyBag.MapiProp;
				}
				return this.embeddedMessage.MapiProp;
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0004A0A8 File Offset: 0x000482A8
		public override Stream OpenPropertyStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode)
		{
			base.CheckDisposed("OpenPropertyStream");
			EnumValidator.ThrowIfInvalid<PropertyOpenMode>(openMode, "openMode");
			NativeStorePropertyDefinition nativeStorePropertyDefinition = propertyDefinition as NativeStorePropertyDefinition;
			if (nativeStorePropertyDefinition == null)
			{
				throw new NotImplementedException(ServerStrings.ExCalculatedPropertyStreamAccessNotSupported(propertyDefinition.Name));
			}
			Stream stream = null;
			if (openMode == PropertyOpenMode.Create)
			{
				this.MakeException();
				stream = this.embeddedMessage.OpenPropertyStream(nativeStorePropertyDefinition, openMode);
			}
			else if (openMode == PropertyOpenMode.Modify)
			{
				bool isException = this.IsException;
				this.MakeException();
				stream = this.OpenPropertyStreamFromEmbeddedMessage(nativeStorePropertyDefinition, openMode);
				if (stream == null)
				{
					PropTag propTag = PropertyTagCache.Cache.PropTagFromPropertyDefinition(this.masterCalendarItem.MapiProp, this.storeSession, nativeStorePropertyDefinition);
					PropProblem[] array = null;
					StoreSession storeSession = this.storeSession;
					bool flag = false;
					try
					{
						if (storeSession != null)
						{
							storeSession.BeginMapiCall();
							storeSession.BeginServerHealthCall();
							flag = true;
						}
						if (StorageGlobals.MapiTestHookBeforeCall != null)
						{
							StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
						}
						array = this.masterCalendarItem.MapiMessage.CopyProps(this.embeddedMessage.MapiMessage, new PropTag[]
						{
							propTag
						});
					}
					catch (MapiPermanentException ex)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.ExUnableToGetStreamProperty(nativeStorePropertyDefinition.Name), ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("OccurrencePropertyBag::OpenPropertyStream.", new object[0]),
							ex
						});
					}
					catch (MapiRetryableException ex2)
					{
						throw StorageGlobals.TranslateMapiException(ServerStrings.ExUnableToGetStreamProperty(nativeStorePropertyDefinition.Name), ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
						{
							string.Format("OccurrencePropertyBag::OpenPropertyStream.", new object[0]),
							ex2
						});
					}
					finally
					{
						try
						{
							if (storeSession != null)
							{
								storeSession.EndMapiCall();
								if (flag)
								{
									storeSession.EndServerHealthCall();
								}
							}
						}
						finally
						{
							if (StorageGlobals.MapiTestHookAfterCall != null)
							{
								StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
							}
						}
					}
					if (array != null)
					{
						ExTraceGlobals.StorageTracer.TraceError<int>((long)this.GetHashCode(), "OccurrencePropertyBag::OpenPropertyStream: .CopyProps returned scode = {0}", array[0].Scode);
						throw PropertyError.ToException(ServerStrings.ExUnableToGetStreamProperty(nativeStorePropertyDefinition.Name), StoreObjectPropertyBag.MapiPropProblemsToPropertyErrors(new PropertyDefinition[]
						{
							nativeStorePropertyDefinition
						}, array));
					}
					stream = this.embeddedMessage.OpenPropertyStream(nativeStorePropertyDefinition, openMode);
				}
			}
			else if (openMode == PropertyOpenMode.ReadOnly)
			{
				this.FetchException();
				stream = this.OpenPropertyStreamFromEmbeddedMessage(nativeStorePropertyDefinition, openMode);
				if (stream == null)
				{
					stream = this.masterCalendarItem.OpenPropertyStream(nativeStorePropertyDefinition, openMode);
				}
			}
			if ((openMode == PropertyOpenMode.Modify || openMode == PropertyOpenMode.Create) && (nativeStorePropertyDefinition == InternalSchema.HtmlBody || nativeStorePropertyDefinition == InternalSchema.RtfBody || nativeStorePropertyDefinition == InternalSchema.TextBody))
			{
				this.hasExceptionalBody = true;
			}
			if ((openMode == PropertyOpenMode.Modify || openMode == PropertyOpenMode.Create) && nativeStorePropertyDefinition == InternalSchema.EventTimeBasedInboxReminders)
			{
				this.MasterCalendarItem.HasExceptionalInboxReminders = true;
			}
			return stream;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0004A364 File Offset: 0x00048564
		internal override void FlushChanges()
		{
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0004A368 File Offset: 0x00048568
		internal override void SaveChanges(bool force)
		{
			base.CheckDisposed("SaveChanges");
			this.MakeException();
			this.UpdateExceptionAttachmentTimes();
			foreach (KeyValuePair<PropertyDefinition, object> keyValuePair in ((IEnumerable<KeyValuePair<PropertyDefinition, object>>)this.propertyCache))
			{
				PropertyDefinition key = keyValuePair.Key;
				if (!OccurrencePropertyBag.PropertyNotInEmbeddedMessage.Contains(key) && !OccurrencePropertyBag.StoreComputedExceptionProperty.Contains(key) && !PropertyError.IsPropertyError(keyValuePair.Value))
				{
					this.embeddedMessage[key] = keyValuePair.Value;
				}
			}
			if (this.embeddedMessage != null)
			{
				this.embeddedMessage[CalendarItemOccurrenceSchema.ExceptionReplaceTime] = this.OriginalStartTime;
				this.CopyLocationToEmbeddedMessage();
			}
			this.embeddedMessage.LocationIdentifierHelperInstance.SetLocationIdentifier(56181U);
			ConflictResolutionResult conflictResolutionResult = this.embeddedMessage.Save(SaveMode.ResolveConflicts);
			if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				ExTraceGlobals.StorageTracer.TraceError<ConflictResolutionResult>((long)this.GetHashCode(), "OccurrencePropertyBag::SaveChanges. Cannot resolve conflicts on saving an embedded message. SaveStatus = {0}.", conflictResolutionResult);
				throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(this.embeddedMessage.InternalObjectId), conflictResolutionResult);
			}
			this.attachmentIdStartTime = this.StartTimeId;
			this.attachmentIdEndTime = this.EndTimeId;
			this.embeddedMessage.Load(new PropertyDefinition[]
			{
				InternalSchema.MapiHasAttachment
			});
			if (this.embeddedMessage.PropertyBag.GetValueOrDefault<bool>(InternalSchema.MapiHasAttachment))
			{
				this.propertyCache[InternalSchema.MapiHasAttachment] = true;
				this.propertyCache[InternalSchema.AllAttachmentsHidden] = false;
			}
			else if (this.propertyCache.GetValueOrDefault<bool>(InternalSchema.MapiHasAttachment))
			{
				this.propertyCache[InternalSchema.MapiHasAttachment] = false;
			}
			this.UpdateMasterRecurrence();
			InternalRecurrence internalRecurrence = this.MasterRecurrence;
			if (this.masterCalendarItem.Recurrence == internalRecurrence)
			{
				this.masterPropertyBag[InternalSchema.AppointmentRecurrenceBlob] = internalRecurrence.ToByteArray();
			}
			this.exceptionAttachment.Save();
			if (this.ownMasterMessage)
			{
				if (this.embeddedMessage != null)
				{
					this.embeddedMessage.Dispose();
					this.embeddedMessage = null;
				}
				if (this.exceptionAttachment != null)
				{
					this.exceptionAttachment.Dispose();
					this.exceptionAttachment = null;
				}
				ExTraceGlobals.FaultInjectionTracer.TraceTest(3561368893U);
				this.masterCalendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(39797U);
				this.masterCalendarItem.Save(SaveMode.NoConflictResolution);
			}
			this.propertyCache.Clear();
			this.isPropertyCacheLoaded = false;
			this.IsException = true;
			this.isDirty = false;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0004A5FC File Offset: 0x000487FC
		private static bool IsAttendeeAcceptingOccurrence(StorePropertyDefinition propertyDefinition, object propertyValue)
		{
			bool result = false;
			if (propertyDefinition == InternalSchema.MapiResponseType)
			{
				ResponseType responseType = (ResponseType)propertyValue;
				result = (responseType == ResponseType.Accept || responseType == ResponseType.Tentative);
			}
			return result;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0004A628 File Offset: 0x00048828
		private void CopyLocationToEmbeddedMessage()
		{
			object obj = this.embeddedMessage.TryGetProperty(InternalSchema.LocationUri);
			bool flag = !(obj is PropertyError);
			bool flag2 = this.embeddedMessage.IsPropertyDirty(InternalSchema.LocationAnnotation);
			bool flag3 = false;
			for (int i = 0; i < CalendarItemProperties.EnhancedLocationPropertyDefinitions.Length; i++)
			{
				if (InternalSchema.LocationAnnotation != CalendarItemProperties.EnhancedLocationPropertyDefinitions[i] && this.embeddedMessage.IsPropertyDirty(CalendarItemProperties.EnhancedLocationPropertyDefinitions[i]))
				{
					ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "OccurrencePropertyBag::SaveChanges. Found at least one enhanced location property dirty.");
					flag3 = true;
					break;
				}
			}
			if (flag3 || (flag2 && !flag))
			{
				foreach (Tuple<StorePropertyDefinition, object> tuple in CalendarItemProperties.EnhancedLocationPropertiesWithDefaultValues)
				{
					this.CopyFromMasterToOccurrenceIfNotDirty(tuple.Item1, tuple.Item2);
				}
			}
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0004A6F8 File Offset: 0x000488F8
		private void CopyFromMasterToOccurrenceIfNotDirty(PropertyDefinition propertyDefinition, object defaultValue)
		{
			if (!this.embeddedMessage.IsPropertyDirty(propertyDefinition))
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string>((long)this.GetHashCode(), "OccurrencePropertyBag::CopyFromMasterToOccurrence. Copying property {0} to the embedded message.", propertyDefinition.Name);
				this.embeddedMessage[propertyDefinition] = this.masterPropertyBag.GetValueOrDefault<object>(propertyDefinition, defaultValue);
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0004A748 File Offset: 0x00048948
		private Stream OpenPropertyStreamFromEmbeddedMessage(NativeStorePropertyDefinition nativePropertyDefinition, PropertyOpenMode openMode)
		{
			Stream result = null;
			bool flag = false;
			if (!OccurrencePropertyBag.PropertyNotInEmbeddedMessage.Contains(nativePropertyDefinition) && this.embeddedMessage != null)
			{
				if (((IDirectPropertyBag)this.embeddedMessage.PropertyBag).IsLoaded(nativePropertyDefinition))
				{
					PropertyError propertyError = this.embeddedMessage.TryGetProperty(nativePropertyDefinition) as PropertyError;
					if (propertyError == null || PropertyError.IsPropertyValueTooBig(propertyError))
					{
						flag = true;
					}
				}
				if (((IDirectPropertyBag)this.embeddedMessage.PropertyBag).IsLoaded(nativePropertyDefinition))
				{
					if (!flag)
					{
						return result;
					}
				}
				try
				{
					result = this.embeddedMessage.OpenPropertyStream(nativePropertyDefinition, openMode);
				}
				catch (ObjectNotFoundException)
				{
				}
			}
			return result;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0004A7DC File Offset: 0x000489DC
		private object InternalTryGetStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			base.CheckDisposed("GetProperty");
			if (!this.isPropertyCacheLoaded)
			{
				return this.propertyCache.TryGetProperty(propertyDefinition);
			}
			object obj = null;
			if (this.propertyCache.CheckIsLoaded(propertyDefinition, true))
			{
				obj = this.propertyCache.TryGetProperty(propertyDefinition);
				PropertyError propertyError = obj as PropertyError;
				if (propertyError != null)
				{
					obj = null;
				}
			}
			bool flag = OccurrencePropertyBag.PropertyNotInMasterPropertyBag.Contains(propertyDefinition);
			if (obj == null)
			{
				if (OccurrencePropertyBag.PropertyNotInEmbeddedMessage.Contains(propertyDefinition))
				{
					return this.masterPropertyBag.TryGetProperty(propertyDefinition);
				}
				if (!this.hasExceptionalBody && (Body.BodyPropSet.Contains(propertyDefinition) || propertyDefinition == InternalSchema.NativeBodyInfo || propertyDefinition == InternalSchema.Preview))
				{
					return this.masterPropertyBag.TryGetProperty(propertyDefinition);
				}
				this.FetchException();
				if (this.embeddedMessage != null)
				{
					obj = this.embeddedMessage.TryGetProperty(propertyDefinition);
					PropertyError propertyError2 = obj as PropertyError;
					if (propertyError2 != null && !flag)
					{
						obj = null;
					}
				}
			}
			if (obj == null && !flag)
			{
				obj = this.masterPropertyBag.TryGetProperty(propertyDefinition);
			}
			if (obj == null)
			{
				obj = new PropertyError(propertyDefinition, PropertyErrorCode.NotFound);
			}
			return obj;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0004A8D8 File Offset: 0x00048AD8
		private void AddTrackingInformation(StorePropertyDefinition propertyDefinition, PropertyTrackingInformation changeType, object originalValue)
		{
			if ((propertyDefinition.PropertyFlags & PropertyFlags.TrackChange) == PropertyFlags.TrackChange && !this.TrackedPropertyInformation.ContainsKey(propertyDefinition))
			{
				PropertyValueTrackingData value = new PropertyValueTrackingData(changeType, originalValue);
				this.TrackedPropertyInformation.Add(propertyDefinition, value);
			}
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0004A918 File Offset: 0x00048B18
		private void InternalSetValidatedStoreProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
		{
			base.CheckDisposed("SetProperty");
			this.isDirty = true;
			if (OccurrencePropertyBag.NotOccurrenceProperties.Contains(propertyDefinition))
			{
				ExTraceGlobals.StorageTracer.TraceError<string>((long)this.GetHashCode(), "OccurrencePropertyBag::SetProperties. Property is not allowed on occurrences: {0}.", propertyDefinition.Name);
				throw new InvalidOperationException(ServerStrings.ExPropertyNotValidOnOccurrence(propertyDefinition.Name));
			}
			if (OccurrencePropertyBag.PropertyNotInEmbeddedMessage.Contains(propertyDefinition))
			{
				return;
			}
			if ((propertyDefinition.PropertyFlags & PropertyFlags.ReadOnly) == PropertyFlags.ReadOnly)
			{
				return;
			}
			bool flag = false;
			object obj = null;
			if (this.IsLoaded(propertyDefinition as NativeStorePropertyDefinition))
			{
				obj = this.InternalTryGetStoreProperty(propertyDefinition);
				if ((propertyDefinition.PropertyFlags & PropertyFlags.SetIfNotChanged) != PropertyFlags.SetIfNotChanged)
				{
					if (obj != null)
					{
						flag = obj.Equals(propertyValue);
					}
					else
					{
						flag = (propertyValue == null);
					}
				}
			}
			if (RecurrenceManager.CanPropertyBeInExceptionData(propertyDefinition))
			{
				((IDirectPropertyBag)this.propertyCache).SetValue(propertyDefinition, propertyValue);
			}
			if (!flag)
			{
				this.MakeException();
				IDirectPropertyBag propertyBag = this.embeddedMessage.PropertyBag;
				propertyBag.SetValue(propertyDefinition, propertyValue);
				if (!(propertyValue is PropertyError))
				{
					StorePropertyDefinition propertyDefinition2 = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
					this.AddTrackingInformation(propertyDefinition2, PropertyTrackingInformation.Modified, obj);
				}
				if (propertyDefinition == InternalSchema.HtmlBody || propertyDefinition == InternalSchema.RtfBody || propertyDefinition == InternalSchema.TextBody)
				{
					this.hasExceptionalBody = true;
				}
				if (propertyDefinition == InternalSchema.EventTimeBasedInboxReminders)
				{
					this.MasterCalendarItem.HasExceptionalInboxReminders = true;
				}
				if (OccurrencePropertyBag.IsAttendeeAcceptingOccurrence(propertyDefinition, propertyValue))
				{
					ResponseType valueOrDefault = this.masterPropertyBag.GetValueOrDefault<ResponseType>(CalendarItemBaseSchema.ResponseType, ResponseType.NotResponded);
					if (valueOrDefault == ResponseType.NotResponded)
					{
						this.masterPropertyBag[CalendarItemBaseSchema.ResponseType] = ResponseType.Tentative;
					}
				}
			}
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0004AA84 File Offset: 0x00048C84
		private void InitializeDefaultProperties(OccurrenceInfo occurrenceInfo)
		{
			ExceptionInfo exceptionInfo = occurrenceInfo as ExceptionInfo;
			if (exceptionInfo != null)
			{
				foreach (PropertyDefinition propertyDefinition in RecurrenceManager.PropertiesInTheBlobO11)
				{
					NativeStorePropertyDefinition nativeStorePropertyDefinition = propertyDefinition as NativeStorePropertyDefinition;
					if (nativeStorePropertyDefinition != null && !((IDirectPropertyBag)this.propertyCache).IsLoaded(nativeStorePropertyDefinition) && ((IDirectPropertyBag)exceptionInfo.PropertyBag).IsLoaded(nativeStorePropertyDefinition))
					{
						((IDirectPropertyBag)this.propertyCache).SetValue(nativeStorePropertyDefinition, exceptionInfo.PropertyBag.TryGetProperty(nativeStorePropertyDefinition));
					}
				}
				int valueOrDefault = this.masterPropertyBag.GetValueOrDefault<int>(InternalSchema.AppointmentStateInternal);
				int valueOrDefault2 = this.propertyCache.GetValueOrDefault<int>(InternalSchema.AppointmentStateInternal);
				int num = (valueOrDefault2 & -3) | valueOrDefault;
				if (valueOrDefault2 != num)
				{
					this.propertyCache[InternalSchema.AppointmentStateInternal] = num;
				}
				this.IsException = true;
				this.hasExceptionalBody = ((exceptionInfo.ModificationType & ModificationType.Body) == ModificationType.Body);
			}
			else
			{
				ExDateTime startTime = occurrenceInfo.StartTime;
				ExDateTime endTime = occurrenceInfo.EndTime;
				this.propertyCache[InternalSchema.MapiStartTime] = startTime;
				this.propertyCache[InternalSchema.MapiEndTime] = endTime;
				this.IsException = false;
			}
			if (!this.IsException && this.organizerTimeZone != null && this.organizerTimeZone != ExTimeZone.UtcTimeZone)
			{
				this.propertyCache[InternalSchema.TimeZoneDefinitionStart] = O12TimeZoneFormatter.GetTimeZoneBlob(this.organizerTimeZone);
			}
			object obj = this.masterPropertyBag.TryGetProperty(InternalSchema.CleanGlobalObjectId);
			byte[] array = obj as byte[];
			if (array != null)
			{
				GlobalObjectId globalObjectId = new GlobalObjectId(array);
				globalObjectId.Date = this.itemId.OccurrenceId.Date;
				this.propertyCache[InternalSchema.GlobalObjectId] = globalObjectId.Bytes;
			}
			else
			{
				PropertyError propertyError = obj as PropertyError;
				((IDirectPropertyBag)this.propertyCache).SetValue(InternalSchema.GlobalObjectId, new PropertyError(InternalSchema.GlobalObjectId, propertyError.PropertyErrorCode, propertyError.PropertyErrorDescription));
			}
			this.propertyCache[InternalSchema.StartRecurDate] = Util.ConvertDateTimeToSCDDate(occurrenceInfo.OccurrenceDateId);
			this.propertyCache[InternalSchema.StartRecurTime] = Util.ConvertTimeSpanToSCDTime(this.organizerTimeZone.ConvertDateTime(this.originalStartTime).TimeOfDay);
			this.propertyCache[InternalSchema.AppointmentRecurring] = false;
			this.propertyCache.Load(RecurrenceManager.PropertiesInTheBlobO11);
			this.propertyCache[InternalSchema.IsException] = this.IsException;
			this.propertyCache[InternalSchema.ItemClass] = (this.IsException ? "IPM.OLE.CLASS.{00061055-0000-0000-C000-000000000046}" : "IPM.Appointment.Occurrence");
			this.propertyCache[InternalSchema.IsRecurring] = true;
			this.attachmentIdStartTime = this.StartTimeId;
			this.attachmentIdEndTime = this.EndTimeId;
			this.isPropertyCacheLoaded = true;
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0004AD5F File Offset: 0x00048F5F
		public override bool IsDirty
		{
			get
			{
				base.CheckDisposed("IsDirty::get");
				return this.isDirty;
			}
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0004AD72 File Offset: 0x00048F72
		protected override bool InternalIsPropertyDirty(AtomicStorePropertyDefinition propertyDefinition)
		{
			base.CheckDisposed("InternalIsPropertyDirty");
			return this.IsException && this.embeddedMessage != null && this.embeddedMessage.IsPropertyDirty(propertyDefinition);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0004ADA0 File Offset: 0x00048FA0
		protected override bool IsLoaded(NativeStorePropertyDefinition propertyDefinition)
		{
			base.CheckDisposed("IsLoaded");
			if (propertyDefinition == null)
			{
				return false;
			}
			if (!this.isPropertyCacheLoaded)
			{
				return ((IDirectPropertyBag)this.propertyCache).IsLoaded(propertyDefinition);
			}
			if (((IDirectPropertyBag)this.propertyCache).IsLoaded(propertyDefinition))
			{
				return true;
			}
			if (!OccurrencePropertyBag.PropertyNotInEmbeddedMessage.Contains(propertyDefinition))
			{
				this.FetchException();
				if (this.embeddedMessage != null)
				{
					return ((IDirectPropertyBag)this.embeddedMessage.PropertyBag).IsLoaded(propertyDefinition);
				}
			}
			return ((IDirectPropertyBag)this.masterPropertyBag).IsLoaded(propertyDefinition);
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x0004AE1B File Offset: 0x0004901B
		public override ICollection<PropertyDefinition> AllFoundProperties
		{
			get
			{
				base.CheckDisposed("AllFoundProperties::get");
				throw new NotSupportedException(string.Format("This object does not support GetPropertyList. Type = {0}.", base.GetType()));
			}
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0004AE40 File Offset: 0x00049040
		public override PropertyValueTrackingData GetOriginalPropertyInformation(PropertyDefinition propertyDefinition)
		{
			StorePropertyDefinition storePropertyDefinition = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			if ((storePropertyDefinition.PropertyFlags & PropertyFlags.TrackChange) != PropertyFlags.TrackChange)
			{
				return PropertyValueTrackingData.PropertyValueTrackDataNotTracked;
			}
			if (this.TrackedPropertyInformation != null && this.TrackedPropertyInformation.ContainsKey(propertyDefinition))
			{
				return this.TrackedPropertyInformation[propertyDefinition];
			}
			return PropertyValueTrackingData.PropertyValueTrackDataUnchanged;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x0004AE8F File Offset: 0x0004908F
		public CalendarItem MasterCalendarItem
		{
			get
			{
				base.CheckDisposed("MasterCalendarItem::get");
				this.FetchException();
				return this.masterCalendarItem;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0004AEA8 File Offset: 0x000490A8
		internal Item ExceptionMessage
		{
			get
			{
				this.FetchException();
				return this.embeddedMessage;
			}
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0004AEB8 File Offset: 0x000490B8
		public PropertyChangeMetadata ComputeChangeMetadataBasedOnLoadedProperties()
		{
			PropertyChangeMetadata propertyChangeMetadata = new PropertyChangeMetadata();
			if (this.IsException && this.ExceptionMessage != null)
			{
				ICollection<NativeStorePropertyDefinition> allNativeProperties = this.embeddedMessage.AllNativeProperties;
				foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in propertyChangeMetadata.GetTrackedNonOverrideNativeStorePropertyDefinitions())
				{
					if (allNativeProperties.Contains(nativeStorePropertyDefinition))
					{
						object obj = this.embeddedMessage.TryGetProperty(nativeStorePropertyDefinition);
						if (!(obj is PropertyError))
						{
							propertyChangeMetadata.MarkAsMasterPropertyOverride(nativeStorePropertyDefinition.Name);
						}
					}
				}
			}
			return propertyChangeMetadata;
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0004AF50 File Offset: 0x00049150
		internal void MakeException()
		{
			if (!this.IsException)
			{
				AcrPropertyBag acrPropertyBag = this.MasterCalendarItem.PropertyBag as AcrPropertyBag;
				if (acrPropertyBag != null && acrPropertyBag.IsReadOnly)
				{
					ExTraceGlobals.StorageTracer.TraceError<int>((long)this.GetHashCode(), "OccurrecePropertyBag::MakeException{0}, SetProperties called for readonly AcrPropertyBag", this.GetHashCode());
					throw new AccessDeniedException(ServerStrings.ExItemIsOpenedInReadOnlyMode);
				}
				this.MasterCalendarItem.LocationIdentifierHelperInstance.SetLocationIdentifier(34293U, LastChangeAction.MakeModifiedOccurrence);
			}
			this.IsException = true;
			this.propertyCache[InternalSchema.IsException] = true;
			this.propertyCache[InternalSchema.ItemClass] = "IPM.OLE.CLASS.{00061055-0000-0000-C000-000000000046}";
			this.FetchException();
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0004AFF8 File Offset: 0x000491F8
		private void CreateExceptionAttachment(ICollection<PropertyDefinition> propertiesToLoad)
		{
			ExDateTime exDateTime = TimeZoneHelper.AssignLocalTimeToUtc(this.StartTimeId);
			ExDateTime exDateTime2 = TimeZoneHelper.AssignLocalTimeToUtc(this.EndTimeId);
			this.exceptionAttachment = (ItemAttachment)this.masterCalendarItem.AttachmentCollection.Create(AttachmentType.EmbeddedMessage);
			this.masterCalendarItem.ClearExceptionSummaryList();
			this.embeddedMessage = this.exceptionAttachment.GetItem(propertiesToLoad);
			object[] array = new object[]
			{
				exDateTime,
				exDateTime2,
				true,
				AttachMethods.EmbeddedMessage,
				2,
				0,
				"Untitled",
				-1,
				OccurrencePropertyBag.zeroLengthByteArray
			};
			for (int i = 0; i < array.Length; i++)
			{
				this.exceptionAttachment.PropertyBag[OccurrencePropertyBag.exceptionProperties[i]] = array[i];
			}
			this.propertyCache[InternalSchema.ItemClass] = "IPM.OLE.CLASS.{00061055-0000-0000-C000-000000000046}";
			this.propertyCache[InternalSchema.IsRecurring] = true;
			this.propertyCache[InternalSchema.IsException] = true;
			this.propertyCache[InternalSchema.AppointmentRecurring] = false;
			bool valueOrDefault = this.masterCalendarItem.GetValueOrDefault<bool>(InternalSchema.MeetingRequestWasSent);
			this.propertyCache[InternalSchema.MeetingRequestWasSent] = valueOrDefault;
			int? valueAsNullable = this.masterCalendarItem.GetValueAsNullable<int>(InternalSchema.Codepage);
			if (valueAsNullable != null)
			{
				this.propertyCache[InternalSchema.Codepage] = valueAsNullable.Value;
			}
			int? valueAsNullable2 = this.masterCalendarItem.GetValueAsNullable<int>(ItemSchema.InternetCpid);
			if (valueAsNullable2 != null)
			{
				this.propertyCache[InternalSchema.InternetCpid] = valueAsNullable2;
			}
			MailboxSession mailboxSession = this.storeSession as MailboxSession;
			if (mailboxSession != null)
			{
				string valueOrDefault2 = this.masterCalendarItem.GetValueOrDefault<string>(CalendarItemBaseSchema.CalendarOriginatorId);
				if (!string.IsNullOrEmpty(valueOrDefault2))
				{
					this.propertyCache[CalendarItemBaseSchema.CalendarOriginatorId] = valueOrDefault2;
				}
			}
			this.masterCalendarItem.ReplaceAttachments(this.embeddedMessage);
			this.IsException = true;
			this.isDirty = true;
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0004B228 File Offset: 0x00049428
		// (set) Token: 0x06000AD4 RID: 2772 RVA: 0x0004B230 File Offset: 0x00049430
		internal bool IsException { get; private set; }

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0004B23C File Offset: 0x0004943C
		private void UpdateExceptionAttachmentTimes()
		{
			if (this.StartTimeId != this.attachmentIdStartTime || this.EndTimeId != this.attachmentIdEndTime)
			{
				ExTraceGlobals.StorageTracer.Information((long)this.GetHashCode(), "OccurrencePropertyBag::SaveChanges. HashCode = {0}. Updating start and end time of exception from start={1} end={2} to start={3} end={4}", new object[]
				{
					this.GetHashCode(),
					this.attachmentIdStartTime,
					this.attachmentIdEndTime,
					this.StartTimeId,
					this.EndTimeId
				});
				ExDateTime exDateTime = TimeZoneHelper.AssignLocalTimeToUtc(this.StartTimeId);
				ExDateTime exDateTime2 = TimeZoneHelper.AssignLocalTimeToUtc(this.EndTimeId);
				this.exceptionAttachment.PropertyBag[InternalSchema.AppointmentExceptionStartTime] = exDateTime;
				this.exceptionAttachment.PropertyBag[InternalSchema.AppointmentExceptionEndTime] = exDateTime2;
				this.isDirty = true;
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0004B32C File Offset: 0x0004952C
		internal bool IsModifiedProperty(PropertyDefinition propertyDefinition)
		{
			if (!this.IsException || this.ExceptionMessage == null)
			{
				return false;
			}
			PropertyError propertyError = this.ExceptionMessage.TryGetProperty(propertyDefinition) as PropertyError;
			if (propertyError == null || propertyError.PropertyErrorCode != PropertyErrorCode.NotFound)
			{
				return true;
			}
			if (((IDirectPropertyBag)this.propertyCache).IsLoaded((NativeStorePropertyDefinition)propertyDefinition))
			{
				propertyError = (this.propertyCache.TryGetProperty(propertyDefinition) as PropertyError);
				return propertyError == null || propertyError.PropertyErrorCode != PropertyErrorCode.NotFound;
			}
			return false;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0004B3A3 File Offset: 0x000495A3
		internal override void SetUpdateImapIdFlag()
		{
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0004B3A5 File Offset: 0x000495A5
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x0004B3C0 File Offset: 0x000495C0
		internal override PropertyBagSaveFlags SaveFlags
		{
			get
			{
				base.CheckDisposed("OccurrencePropertyBag.SaveFlags.get");
				return this.masterPropertyBag.SaveFlags;
			}
			set
			{
				base.CheckDisposed("OccurrencePropertyBag.SaveFlags.set");
				EnumValidator.ThrowIfInvalid<PropertyBagSaveFlags>(value, "value");
				this.MakeException();
				this.UpdateExceptionAttachmentTimes();
				foreach (KeyValuePair<PropertyDefinition, object> keyValuePair in ((IEnumerable<KeyValuePair<PropertyDefinition, object>>)this.propertyCache))
				{
					PropertyDefinition key = keyValuePair.Key;
					if (!OccurrencePropertyBag.PropertyNotInEmbeddedMessage.Contains(key))
					{
						this.embeddedMessage[key] = keyValuePair.Value;
						this.embeddedMessage.SaveFlags = value;
					}
				}
				this.exceptionAttachment.PropertyBag.SaveFlags = value;
				this.attachmentIdStartTime = this.StartTimeId;
				this.attachmentIdEndTime = this.EndTimeId;
				this.UpdateMasterRecurrence();
				InternalRecurrence internalRecurrence = this.MasterRecurrence;
				if (internalRecurrence == this.masterCalendarItem.Recurrence)
				{
					this.masterPropertyBag[InternalSchema.AppointmentRecurrenceBlob] = internalRecurrence.ToByteArray();
				}
				this.masterPropertyBag.SaveFlags = value;
				this.IsException = true;
				this.isDirty = false;
			}
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0004B4D0 File Offset: 0x000496D0
		private void FetchException()
		{
			if (this.masterCalendarItem == null)
			{
				CoreItem coreItem = null;
				bool flag = false;
				try
				{
					ICollection<PropertyDefinition> prefetchProperties = InternalSchema.Combine<PropertyDefinition>(CalendarItemBaseSchema.Instance.AutoloadProperties, this.additionalPrefetchProperties);
					StoreObjectId masterStoreObjectId = this.itemId.GetMasterStoreObjectId();
					coreItem = new CoreItem(this.storeSession, this.masterPropertyBag, masterStoreObjectId, null, Origin.Existing, ItemLevel.TopLevel, prefetchProperties, ItemBindOption.None);
					this.masterCalendarItem = new CalendarItem(coreItem);
					this.masterCalendarItem.OpenAsReadWrite();
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						if (this.masterCalendarItem != null)
						{
							this.masterCalendarItem.Dispose();
							this.masterCalendarItem = null;
						}
						if (coreItem != null)
						{
							coreItem.Dispose();
							coreItem = null;
						}
					}
				}
			}
			if (this.IsException)
			{
				if (this.embeddedMessage != null)
				{
					return;
				}
				ICollection<PropertyDefinition> collection = InternalSchema.Combine<PropertyDefinition>(MessageItemSchema.Instance.AutoloadProperties, InternalSchema.Combine<PropertyDefinition>(CalendarItemOccurrenceSchema.Instance.AutoloadProperties, this.additionalPrefetchProperties));
				if (!this.isEmbeddedMessageLoaded)
				{
					this.embeddedMessage = RecurrenceManager.OpenEmbeddedMessageAndAttachment(this.masterCalendarItem.AttachmentCollection, TimeZoneHelper.GetExTimeZoneFromItem(this.masterCalendarItem), (ExDateTime)this.propertyCache[InternalSchema.MapiStartTime], (ExDateTime)this.propertyCache[InternalSchema.MapiEndTime], out this.exceptionAttachment, collection);
					this.isEmbeddedMessageLoaded = true;
				}
				if (this.embeddedMessage == null && !this.masterCalendarItem.AttachmentCollection.IsReadOnly)
				{
					this.CreateExceptionAttachment(collection);
				}
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0004B638 File Offset: 0x00049838
		internal void UpdateMasterRecurrence()
		{
			InternalRecurrence internalRecurrence = this.MasterRecurrence;
			OccurrenceInfo occurrenceInfoByDateId = internalRecurrence.GetOccurrenceInfoByDateId(this.itemId.OccurrenceId);
			ExceptionInfo exceptionInfo = new ExceptionInfo(occurrenceInfoByDateId.VersionedId, occurrenceInfoByDateId.OccurrenceDateId, occurrenceInfoByDateId.OriginalStartTime, (ExDateTime)this.propertyCache[InternalSchema.MapiStartTime], (ExDateTime)this.propertyCache[InternalSchema.MapiEndTime], this.hasExceptionalBody ? ModificationType.Body : ((ModificationType)0), this.propertyCache);
			internalRecurrence.ModifyOccurrence(exceptionInfo);
			if (this.embeddedMessage != null)
			{
				this.embeddedMessage[CalendarItemOccurrenceSchema.ExceptionReplaceTime] = exceptionInfo.OriginalStartTime;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0004B6E0 File Offset: 0x000498E0
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x0004B6ED File Offset: 0x000498ED
		internal override ExTimeZone ExTimeZone
		{
			get
			{
				return this.masterPropertyBag.ExTimeZone;
			}
			set
			{
				this.originalStartTime = value.ConvertDateTime(this.originalStartTime);
				this.masterPropertyBag.ExTimeZone = value;
				this.propertyCache.ExTimeZone = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0004B719 File Offset: 0x00049919
		internal ExDateTime OriginalStartTime
		{
			get
			{
				return this.originalStartTime;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0004B721 File Offset: 0x00049921
		private InternalRecurrence MasterRecurrence
		{
			get
			{
				return this.masterRecurrence ?? ((InternalRecurrence)this.masterCalendarItem.Recurrence);
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0004B73D File Offset: 0x0004993D
		private ExDateTime StartTimeId
		{
			get
			{
				return this.organizerTimeZone.ConvertDateTime((ExDateTime)this.propertyCache[InternalSchema.MapiStartTime]);
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0004B75F File Offset: 0x0004995F
		private ExDateTime EndTimeId
		{
			get
			{
				return this.organizerTimeZone.ConvertDateTime((ExDateTime)this.propertyCache[InternalSchema.MapiEndTime]);
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0004B781 File Offset: 0x00049981
		private Dictionary<PropertyDefinition, PropertyValueTrackingData> TrackedPropertyInformation
		{
			get
			{
				if (this.trackedPropertyInformation == null)
				{
					this.trackedPropertyInformation = new Dictionary<PropertyDefinition, PropertyValueTrackingData>(2);
				}
				return this.trackedPropertyInformation;
			}
		}

		// Token: 0x040002A8 RID: 680
		private static readonly ICollection<PropertyDefinition> NotOccurrenceProperties = new PropertyDefinition[]
		{
			InternalSchema.AppointmentRecurrenceBlob,
			InternalSchema.AppointmentLastSequenceNumber
		};

		// Token: 0x040002A9 RID: 681
		private static readonly byte[] zeroLengthByteArray = Array<byte>.Empty;

		// Token: 0x040002AA RID: 682
		private static readonly PropertyDefinition[] exceptionProperties = new PropertyDefinition[]
		{
			InternalSchema.AppointmentExceptionStartTime,
			InternalSchema.AppointmentExceptionEndTime,
			InternalSchema.AttachCalendarHidden,
			InternalSchema.AttachMethod,
			InternalSchema.AttachCalendarFlags,
			InternalSchema.AttachCalendarLinkId,
			InternalSchema.DisplayName,
			InternalSchema.RenderingPosition,
			InternalSchema.AttachEncoding
		};

		// Token: 0x040002AB RID: 683
		private static readonly HashSet<PropertyDefinition> PartialPropertiesNotInEmbeddedMessage = new HashSet<PropertyDefinition>(new PropertyDefinition[]
		{
			InternalSchema.ItemId,
			InternalSchema.GlobalObjectId,
			InternalSchema.MapiHasAttachment,
			InternalSchema.CleanGlobalObjectId,
			InternalSchema.StartRecurDate,
			InternalSchema.StartRecurTime,
			InternalSchema.EntryId,
			InternalSchema.MapiSensitivity,
			InternalSchema.ReminderDueBy,
			InternalSchema.ReminderDueByInternal,
			InternalSchema.AppointmentRecurrenceBlob,
			InternalSchema.EffectiveRights
		});

		// Token: 0x040002AC RID: 684
		private static readonly HashSet<PropertyDefinition> OrganizerProperties = new HashSet<PropertyDefinition>(StorePropertyDefinition.GetNativePropertyDefinitions<PropertyDefinition>(PropertyDependencyType.AllRead, new PropertyDefinition[]
		{
			InternalSchema.From
		}));

		// Token: 0x040002AD RID: 685
		private static readonly PropertyDefinition[] PropertyNotInEmbeddedMessage = OccurrencePropertyBag.PartialPropertiesNotInEmbeddedMessage.Concat(OccurrencePropertyBag.OrganizerProperties).ToArray<PropertyDefinition>();

		// Token: 0x040002AE RID: 686
		private static readonly HashSet<PropertyDefinition> PropertyNotInMasterPropertyBag = new HashSet<PropertyDefinition>
		{
			InternalSchema.PropertyChangeMetadataRaw
		};

		// Token: 0x040002AF RID: 687
		private static readonly HashSet<PropertyDefinition> StoreComputedExceptionProperty = new HashSet<PropertyDefinition>(new PropertyDefinition[]
		{
			InternalSchema.NativeBodyInfo
		});

		// Token: 0x040002B0 RID: 688
		private readonly ExTimeZone organizerTimeZone;

		// Token: 0x040002B1 RID: 689
		private ExDateTime attachmentIdStartTime;

		// Token: 0x040002B2 RID: 690
		private ExDateTime attachmentIdEndTime;

		// Token: 0x040002B3 RID: 691
		private ExDateTime originalStartTime;

		// Token: 0x040002B4 RID: 692
		private PersistablePropertyBag masterPropertyBag;

		// Token: 0x040002B5 RID: 693
		private CalendarItem masterCalendarItem;

		// Token: 0x040002B6 RID: 694
		private InternalRecurrence masterRecurrence;

		// Token: 0x040002B7 RID: 695
		private bool ownMasterMessage;

		// Token: 0x040002B8 RID: 696
		private Item embeddedMessage;

		// Token: 0x040002B9 RID: 697
		private ItemAttachment exceptionAttachment;

		// Token: 0x040002BA RID: 698
		private MemoryPropertyBag propertyCache;

		// Token: 0x040002BB RID: 699
		private bool isPropertyCacheLoaded;

		// Token: 0x040002BC RID: 700
		private bool isEmbeddedMessageLoaded;

		// Token: 0x040002BD RID: 701
		private OccurrenceStoreObjectId itemId;

		// Token: 0x040002BE RID: 702
		private StoreSession storeSession;

		// Token: 0x040002BF RID: 703
		private ICollection<PropertyDefinition> additionalPrefetchProperties;

		// Token: 0x040002C0 RID: 704
		private bool isDirty;

		// Token: 0x040002C1 RID: 705
		private bool hasExceptionalBody;

		// Token: 0x040002C2 RID: 706
		private Dictionary<PropertyDefinition, PropertyValueTrackingData> trackedPropertyInformation;
	}
}
