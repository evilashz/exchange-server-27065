using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AE9 RID: 2793
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class AcrPropertyBag : PersistablePropertyBag
	{
		// Token: 0x06006544 RID: 25924 RVA: 0x001AE99C File Offset: 0x001ACB9C
		internal AcrPropertyBag(PersistablePropertyBag propertyBag, AcrProfile profile, StoreObjectId itemId, IPropertyBagFactory propertyBagFactory, byte[] openChangeKey)
		{
			this.propertyBag = propertyBag;
			base.PrefetchPropertyArray = this.propertyBag.PrefetchPropertyArray;
			this.profile = profile;
			this.itemId = itemId;
			this.propertyBagFactory = propertyBagFactory;
			this.openChangeKey = openChangeKey;
			if (propertyBag.DisposeTracker != null)
			{
				propertyBag.DisposeTracker.AddExtraDataWithStackTrace("AcrPropertyBag owns PersistablePropertyBag propertyBag at");
			}
		}

		// Token: 0x06006545 RID: 25925 RVA: 0x001AEA14 File Offset: 0x001ACC14
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
			if (this.propertyBag != null && this.propertyBag.DisposeTracker != null)
			{
				this.propertyBag.DisposeTracker.AddExtraDataWithStackTrace(string.Format(CultureInfo.InvariantCulture, "AcrPropertyBag.InternalDispose({0}) called with stack", new object[]
				{
					disposing
				}));
			}
			if (disposing)
			{
				this.propertyBag.Dispose();
			}
			this.propertyBag = null;
			this.propertyTrackingCache = null;
			this.propertiesWrittenAsStream = null;
		}

		// Token: 0x06006546 RID: 25926 RVA: 0x001AEA90 File Offset: 0x001ACC90
		public void OpenAsReadWrite()
		{
			base.CheckDisposed("PropertyBag::OpenAsReadWrite()");
			ExTraceGlobals.StorageTracer.Information<int>((long)this.GetHashCode(), "AcrPropertyBag::OpenAsReadWrite HashCode = {0}.", this.GetHashCode());
			if (this.Mode != AcrPropertyBag.AcrMode.ReadOnly)
			{
				ExTraceGlobals.StorageTracer.Information<int>((long)this.GetHashCode(), "AcrPropertyBag::Reopen HashCode = {0}. Reopen called when Item was already opened with ItemId.", this.GetHashCode());
				return;
			}
			object obj = this.propertyBag.TryGetProperty(InternalSchema.ChangeKey);
			if (PropertyError.IsPropertyNotFound(obj))
			{
				obj = new byte[]
				{
					1
				};
			}
			if (obj is byte[])
			{
				this.openChangeKey = (this.currentChangeKey = (byte[])obj);
				this.acrModeHint = AcrPropertyBag.AcrMode.Passive;
			}
		}

		// Token: 0x06006547 RID: 25927 RVA: 0x001AEB34 File Offset: 0x001ACD34
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AcrPropertyBag>(this);
		}

		// Token: 0x06006548 RID: 25928 RVA: 0x001AEB3C File Offset: 0x001ACD3C
		protected override void SetValidatedStoreProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
		{
			base.CheckDisposed("SetValidatedStoreProperty");
			if (propertyValue == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<int>((long)this.GetHashCode(), "AcrPropertyBag::SetProperties {0}, propertyValues passed as null", this.GetHashCode());
				throw new ArgumentNullException(ServerStrings.ExNullParameter("propertyValue", 2));
			}
			switch (this.Mode)
			{
			case AcrPropertyBag.AcrMode.ReadOnly:
				ExTraceGlobals.StorageTracer.TraceError<int>((long)this.GetHashCode(), "AcrPropertyBag::SetProperties {0}, SetProperties called for readonly AcrPropertyBag", this.GetHashCode());
				throw new AccessDeniedException(ServerStrings.ExItemIsOpenedInReadOnlyMode);
			case AcrPropertyBag.AcrMode.Active:
			case AcrPropertyBag.AcrMode.Passive:
				this.ModifyPropertyWithBookKeeping(propertyDefinition, propertyValue);
				return;
			case (AcrPropertyBag.AcrMode)4:
				return;
			case AcrPropertyBag.AcrMode.NewItem:
				((IDirectPropertyBag)this.propertyBag).SetValue(propertyDefinition, propertyValue);
				return;
			default:
				return;
			}
		}

		// Token: 0x06006549 RID: 25929 RVA: 0x001AEBEB File Offset: 0x001ACDEB
		protected override object TryGetStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			return ((IDirectPropertyBag)this.propertyBag).GetValue(propertyDefinition);
		}

		// Token: 0x0600654A RID: 25930 RVA: 0x001AEBF9 File Offset: 0x001ACDF9
		public override void Load(ICollection<PropertyDefinition> properties)
		{
			this.propertyBag.Load(properties);
		}

		// Token: 0x0600654B RID: 25931 RVA: 0x001AEC07 File Offset: 0x001ACE07
		public override void Clear()
		{
			this.propertyBag.Clear();
		}

		// Token: 0x17001BEF RID: 7151
		// (get) Token: 0x0600654C RID: 25932 RVA: 0x001AEC14 File Offset: 0x001ACE14
		internal override ICollection<NativeStorePropertyDefinition> AllNativeProperties
		{
			get
			{
				return this.propertyBag.AllNativeProperties;
			}
		}

		// Token: 0x17001BF0 RID: 7152
		// (get) Token: 0x0600654D RID: 25933 RVA: 0x001AEC21 File Offset: 0x001ACE21
		public override bool HasAllPropertiesLoaded
		{
			get
			{
				base.CheckDisposed("HasAllPropertiesLoaded::get");
				return this.propertyBag.HasAllPropertiesLoaded;
			}
		}

		// Token: 0x17001BF1 RID: 7153
		// (get) Token: 0x0600654E RID: 25934 RVA: 0x001AEC39 File Offset: 0x001ACE39
		public override bool CanIgnoreUnchangedProperties
		{
			get
			{
				base.CheckDisposed("CanIgnoreUnchangedProperties::get");
				return this.propertyBag.CanIgnoreUnchangedProperties;
			}
		}

		// Token: 0x0600654F RID: 25935 RVA: 0x001AEC54 File Offset: 0x001ACE54
		private void SetOriginalProperty(PropertyDefinition propertyDefinition)
		{
			Dictionary<PropertyDefinition, PropertyDefinition> dictionary = new Dictionary<PropertyDefinition, PropertyDefinition>();
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			if (this.profile[propertyDefinition] != null && (this.profile[propertyDefinition].RequireChangeTracking || this.Mode == AcrPropertyBag.AcrMode.Active))
			{
				foreach (PropertyDefinition propertyDefinition2 in this.profile[propertyDefinition].AllProperties)
				{
					if ((!this.propertyTrackingCache.ContainsKey(propertyDefinition2) || this.propertyTrackingCache[propertyDefinition2].OriginalValue == null) && !dictionary.ContainsKey(propertyDefinition2))
					{
						dictionary.Add(propertyDefinition2, propertyDefinition2);
						list.Add(propertyDefinition2);
					}
				}
			}
			else if (this.Mode == AcrPropertyBag.AcrMode.Active && !dictionary.ContainsKey(propertyDefinition))
			{
				dictionary.Add(propertyDefinition, propertyDefinition);
				list.Add(propertyDefinition);
			}
			if (list.Count > 0)
			{
				this.propertyBag.Load(list);
				foreach (PropertyDefinition propertyDefinition3 in list)
				{
					object obj = this.propertyBag.TryGetProperty(propertyDefinition3);
					switch (this.Mode)
					{
					case AcrPropertyBag.AcrMode.Active:
						if (!this.propertyTrackingCache.ContainsKey(propertyDefinition3))
						{
							this.propertyTrackingCache.Add(propertyDefinition3, new AcrPropertyBag.TrackingInfo(false, obj, null));
						}
						break;
					case AcrPropertyBag.AcrMode.Passive:
						if (!this.propertyTrackingCache.ContainsKey(propertyDefinition3))
						{
							this.propertyTrackingCache.Add(propertyDefinition3, new AcrPropertyBag.TrackingInfo(false, null, obj));
						}
						break;
					}
				}
			}
		}

		// Token: 0x06006550 RID: 25936 RVA: 0x001AEDEC File Offset: 0x001ACFEC
		private void MarkPropertyAsDirty(PropertyDefinition propertyDefinition)
		{
			if (this.propertyTrackingCache.ContainsKey(propertyDefinition))
			{
				this.propertyTrackingCache[propertyDefinition].Dirty = true;
				return;
			}
			this.propertyTrackingCache.Add(propertyDefinition, new AcrPropertyBag.TrackingInfo(true));
		}

		// Token: 0x06006551 RID: 25937 RVA: 0x001AEE21 File Offset: 0x001AD021
		private void ModifyPropertyWithBookKeeping(StorePropertyDefinition propertyDefinition, object propertyValue)
		{
			if (!this.propertiesWrittenAsStream.ContainsKey(propertyDefinition))
			{
				this.SetOriginalProperty(propertyDefinition);
			}
			if (propertyValue == null)
			{
				this.propertyBag.Delete(propertyDefinition);
			}
			else
			{
				((IDirectPropertyBag)this.propertyBag).SetValue(propertyDefinition, propertyValue);
			}
			this.MarkPropertyAsDirty(propertyDefinition);
		}

		// Token: 0x06006552 RID: 25938 RVA: 0x001AEE60 File Offset: 0x001AD060
		protected override void DeleteStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			base.CheckDisposed("DeleteStoreProperty");
			if (propertyDefinition == null)
			{
				ExTraceGlobals.StorageTracer.TraceError<int>((long)this.GetHashCode(), "AcrPropertyBag::Delete {0}, propertyDefinition passed as null", this.GetHashCode());
				throw new ArgumentNullException(ServerStrings.ExNullParameter("propertyDefinition", 1));
			}
			switch (this.Mode)
			{
			case AcrPropertyBag.AcrMode.ReadOnly:
				ExTraceGlobals.StorageTracer.TraceError<int>((long)this.GetHashCode(), "AcrPropertyBag::Delete {0}, Delete called for readonly AcrPropertyBag", this.GetHashCode());
				throw new AccessDeniedException(ServerStrings.ExItemIsOpenedInReadOnlyMode);
			case AcrPropertyBag.AcrMode.Active:
			case AcrPropertyBag.AcrMode.Passive:
				this.ModifyPropertyWithBookKeeping(propertyDefinition, null);
				return;
			case (AcrPropertyBag.AcrMode)4:
				return;
			case AcrPropertyBag.AcrMode.NewItem:
				((IDirectPropertyBag)this.propertyBag).Delete(propertyDefinition);
				return;
			default:
				return;
			}
		}

		// Token: 0x06006553 RID: 25939 RVA: 0x001AEF10 File Offset: 0x001AD110
		public override Stream OpenPropertyStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode)
		{
			base.CheckDisposed("AcrPropertyBag::OpenPropertyStream");
			EnumValidator.AssertValid<PropertyOpenMode>(openMode);
			if (openMode != PropertyOpenMode.ReadOnly)
			{
				if (this.Mode == AcrPropertyBag.AcrMode.ReadOnly)
				{
					ExTraceGlobals.StorageTracer.TraceError<int, PropertyOpenMode>((long)this.GetHashCode(), "AcrPropertyBag::SetProperties {0}, OpenPropertyStream called for readonly AcrPropertyBag with opemMode = {1}", this.GetHashCode(), openMode);
					throw new AccessDeniedException(ServerStrings.ExItemIsOpenedInReadOnlyMode);
				}
				if (!this.propertiesWrittenAsStream.ContainsKey(propertyDefinition))
				{
					if (this.propertyTrackingCache.ContainsKey(propertyDefinition))
					{
						ExTraceGlobals.StorageTracer.Information<int, string>((long)this.GetHashCode(), "AcrPropertyBag::OpenPropertyStream HashCode[{0}]: PropertyRemoved from acr {1}", this.GetHashCode(), propertyDefinition.Name);
						this.propertyTrackingCache.Remove(propertyDefinition);
					}
					this.propertiesWrittenAsStream.Add(propertyDefinition, propertyDefinition);
				}
			}
			return this.propertyBag.OpenPropertyStream(propertyDefinition, openMode);
		}

		// Token: 0x06006554 RID: 25940 RVA: 0x001AEFCA File Offset: 0x001AD1CA
		public override PropertyValueTrackingData GetOriginalPropertyInformation(PropertyDefinition propertyDefinition)
		{
			return this.propertyBag.GetOriginalPropertyInformation(propertyDefinition);
		}

		// Token: 0x06006555 RID: 25941 RVA: 0x001AEFD8 File Offset: 0x001AD1D8
		internal void SetIrresolvableChange()
		{
			this.irresolvableChanges = true;
		}

		// Token: 0x17001BF2 RID: 7154
		// (get) Token: 0x06006556 RID: 25942 RVA: 0x001AEFE1 File Offset: 0x001AD1E1
		public override ICollection<PropertyDefinition> AllFoundProperties
		{
			get
			{
				base.CheckDisposed("AcrPropertyBag::AllFoundProperties");
				return this.propertyBag.AllFoundProperties;
			}
		}

		// Token: 0x06006557 RID: 25943 RVA: 0x001AEFF9 File Offset: 0x001AD1F9
		public override void Reload()
		{
			this.propertyBag.Reload();
			this.propertyTrackingCache.Clear();
			this.propertiesWrittenAsStream.Clear();
		}

		// Token: 0x06006558 RID: 25944 RVA: 0x001AF01C File Offset: 0x001AD21C
		private Dictionary<PropertyDefinition, AcrPropertyProfile.ValuesToResolve> GetValuesToResolve(PropertyBag acrPropertyBag)
		{
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>(this.propertyTrackingCache.Count);
			foreach (KeyValuePair<PropertyDefinition, AcrPropertyBag.TrackingInfo> keyValuePair in this.propertyTrackingCache)
			{
				if (keyValuePair.Value.Dirty)
				{
					foreach (PropertyDefinition item in this.profile.GetPropertiesNeededForResolution(keyValuePair.Key))
					{
						hashSet.TryAdd(item);
					}
				}
			}
			Dictionary<PropertyDefinition, AcrPropertyProfile.ValuesToResolve> dictionary = new Dictionary<PropertyDefinition, AcrPropertyProfile.ValuesToResolve>();
			if (hashSet.Count > 0)
			{
				this.propertyBag.Load(hashSet);
				acrPropertyBag.Load(hashSet);
				foreach (PropertyDefinition propertyDefinition in hashSet)
				{
					AcrPropertyBag.TrackingInfo trackingInfo;
					this.propertyTrackingCache.TryGetValue(propertyDefinition, out trackingInfo);
					object clientValue = this.propertyBag.TryGetProperty(propertyDefinition);
					if (acrPropertyBag == this.propertyBag)
					{
						dictionary.Add(propertyDefinition, new AcrPropertyProfile.ValuesToResolve(clientValue, trackingInfo.ServerValue, trackingInfo.OriginalValue));
					}
					else
					{
						object serverValue = acrPropertyBag.TryGetProperty(propertyDefinition);
						dictionary.Add(propertyDefinition, new AcrPropertyProfile.ValuesToResolve(clientValue, serverValue, (trackingInfo != null) ? trackingInfo.OriginalValue : null));
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06006559 RID: 25945 RVA: 0x001AF1A8 File Offset: 0x001AD3A8
		private ConflictResolutionResult ApplyAcr(PropertyBag acrPropBag, SaveMode saveMode)
		{
			Dictionary<PropertyDefinition, AcrPropertyProfile.ValuesToResolve> valuesToResolve = this.GetValuesToResolve(acrPropBag);
			string valueOrDefault = this.PropertyBag.GetValueOrDefault<string>(InternalSchema.ItemClass, string.Empty);
			if (ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(valueOrDefault) || ObjectClass.IsMeetingMessage(valueOrDefault))
			{
				LocationIdentifierHelper locationIdentifierHelper = new LocationIdentifierHelper();
				AcrPropertyProfile.ValuesToResolve valuesToResolve2;
				object serverValue;
				if (valuesToResolve.TryGetValue(InternalSchema.ChangeList, out valuesToResolve2))
				{
					locationIdentifierHelper.ChangeBuffer = (byte[])valuesToResolve2.ClientValue;
					serverValue = valuesToResolve2.ServerValue;
				}
				else
				{
					serverValue = new PropertyError(InternalSchema.ChangeList, PropertyErrorCode.NotFound);
				}
				locationIdentifierHelper.SetLocationIdentifier(53909U, LastChangeAction.AcrPerformed);
				valuesToResolve2 = new AcrPropertyProfile.ValuesToResolve(locationIdentifierHelper.ChangeBuffer, serverValue, null);
				valuesToResolve[InternalSchema.ChangeList] = valuesToResolve2;
			}
			ConflictResolutionResult conflictResolutionResult = this.profile.ResolveConflicts(valuesToResolve);
			if (this.propertiesWrittenAsStream.Count > 0)
			{
				List<PropertyConflict> list = new List<PropertyConflict>(conflictResolutionResult.PropertyConflicts);
				foreach (PropertyDefinition propertyDefinition in this.propertiesWrittenAsStream.Keys)
				{
					PropertyConflict item = new PropertyConflict(propertyDefinition, null, null, null, null, false);
					list.Add(item);
				}
				conflictResolutionResult = new ConflictResolutionResult(SaveResult.IrresolvableConflict, list.ToArray());
			}
			if (this.irresolvableChanges || saveMode == SaveMode.FailOnAnyConflict)
			{
				conflictResolutionResult = new ConflictResolutionResult(SaveResult.IrresolvableConflict, conflictResolutionResult.PropertyConflicts);
			}
			if (conflictResolutionResult.SaveStatus != SaveResult.IrresolvableConflict)
			{
				List<PropertyDefinition> list2 = new List<PropertyDefinition>();
				List<PropertyDefinition> list3 = new List<PropertyDefinition>();
				List<object> list4 = new List<object>();
				if (this.propertyBag == acrPropBag)
				{
					foreach (PropertyConflict propertyConflict in conflictResolutionResult.PropertyConflicts)
					{
						if (propertyConflict.ResolvedValue is PropertyError)
						{
							if (PropertyError.IsPropertyNotFound(propertyConflict.ResolvedValue) && (!PropertyError.IsPropertyError(propertyConflict.ClientValue) || !PropertyError.IsPropertyNotFound(propertyConflict.ClientValue)))
							{
								list2.Add(propertyConflict.PropertyDefinition);
							}
						}
						else if (propertyConflict.ResolvedValue != propertyConflict.ClientValue)
						{
							list3.Add(propertyConflict.PropertyDefinition);
							list4.Add(propertyConflict.ResolvedValue);
						}
					}
				}
				else
				{
					foreach (PropertyConflict propertyConflict2 in conflictResolutionResult.PropertyConflicts)
					{
						if (propertyConflict2.ResolvedValue is PropertyError)
						{
							if (PropertyError.IsPropertyNotFound(propertyConflict2.ResolvedValue))
							{
								list2.Add(propertyConflict2.PropertyDefinition);
							}
						}
						else if (propertyConflict2.ServerValue != propertyConflict2.ResolvedValue)
						{
							list3.Add(propertyConflict2.PropertyDefinition);
							list4.Add(propertyConflict2.ResolvedValue);
						}
					}
				}
				for (int k = 0; k < list2.Count; k++)
				{
					acrPropBag.Delete(list2[k]);
				}
				for (int l = 0; l < list3.Count; l++)
				{
					acrPropBag[list3[l]] = list4[l];
				}
			}
			return conflictResolutionResult;
		}

		// Token: 0x0600655A RID: 25946 RVA: 0x001AF498 File Offset: 0x001AD698
		private bool CanSaveBeNoOp()
		{
			return !this.IsDirty && this.Mode != AcrPropertyBag.AcrMode.NewItem && this.Mode != AcrPropertyBag.AcrMode.ReadOnly;
		}

		// Token: 0x0600655B RID: 25947 RVA: 0x001AF4B8 File Offset: 0x001AD6B8
		internal ConflictResolutionResult FlushChangesWithAcr(SaveMode saveMode)
		{
			base.CheckDisposed("AcrPropertyBag::FlushChangesWithAcr");
			ConflictResolutionResult conflictResolutionResult = null;
			if (this.CanSaveBeNoOp())
			{
				return ConflictResolutionResult.Success;
			}
			switch (this.Mode)
			{
			case AcrPropertyBag.AcrMode.ReadOnly:
				throw new AccessDeniedException(ServerStrings.ExItemIsOpenedInReadOnlyMode);
			case AcrPropertyBag.AcrMode.Active:
				conflictResolutionResult = this.ApplyAcr(this.propertyBag, saveMode);
				if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
				{
					return conflictResolutionResult;
				}
				break;
			}
			this.propertyBag.FlushChanges();
			this.uncommitted = true;
			if (conflictResolutionResult == null)
			{
				conflictResolutionResult = ConflictResolutionResult.Success;
			}
			return conflictResolutionResult;
		}

		// Token: 0x0600655C RID: 25948 RVA: 0x001AF53C File Offset: 0x001AD73C
		internal ConflictResolutionResult SaveChangesWithAcr(SaveMode saveMode)
		{
			base.CheckDisposed("AcrPropertyBag::SaveChangesWithAcr");
			ConflictResolutionResult conflictResolutionResult = null;
			if (this.CanSaveBeNoOp() && !this.uncommitted)
			{
				return ConflictResolutionResult.Success;
			}
			try
			{
				this.propertyBag.SaveChanges(false);
				this.uncommitted = false;
			}
			catch (SaveConflictException)
			{
				PersistablePropertyBag persistablePropertyBag = this.propertyBagFactory.CreateStorePropertyBag(this.propertyBag, this.PrefetchPropertyArray);
				persistablePropertyBag.Context.Copy(this.propertyBag.Context);
				try
				{
					conflictResolutionResult = this.ApplyAcr(persistablePropertyBag, saveMode);
					if (conflictResolutionResult.SaveStatus == SaveResult.Success || conflictResolutionResult.SaveStatus == SaveResult.SuccessWithConflictResolution)
					{
						persistablePropertyBag.FlushChanges();
						persistablePropertyBag.SaveChanges(false);
						this.PropertyBag = persistablePropertyBag;
						persistablePropertyBag = null;
						this.uncommitted = false;
					}
				}
				finally
				{
					if (persistablePropertyBag != null)
					{
						persistablePropertyBag.Dispose();
					}
				}
			}
			if (conflictResolutionResult == null)
			{
				conflictResolutionResult = ConflictResolutionResult.Success;
			}
			if (conflictResolutionResult.SaveStatus != SaveResult.IrresolvableConflict)
			{
				this.RefreshCacheAfterSave(conflictResolutionResult);
			}
			return conflictResolutionResult;
		}

		// Token: 0x0600655D RID: 25949 RVA: 0x001AF62C File Offset: 0x001AD82C
		private void RefreshCacheAfterSave(ConflictResolutionResult resolutionResults)
		{
			if (this.Mode != AcrPropertyBag.AcrMode.NewItem)
			{
				this.propertyTrackingCache.Clear();
			}
			this.currentChangeKey = (this.openChangeKey = Guid.NewGuid().ToByteArray());
			this.acrModeHint = AcrPropertyBag.AcrMode.Passive;
			this.propertiesWrittenAsStream.Clear();
			this.irresolvableChanges = false;
		}

		// Token: 0x0600655E RID: 25950 RVA: 0x001AF682 File Offset: 0x001AD882
		internal override void FlushChanges()
		{
			base.CheckDisposed("AcrPropertyBag::FlushChanges");
			if (this.Mode == AcrPropertyBag.AcrMode.ReadOnly)
			{
				throw new AccessDeniedException(ServerStrings.ExItemIsOpenedInReadOnlyMode);
			}
			if (this.CanSaveBeNoOp())
			{
				return;
			}
			this.propertyBag.FlushChanges();
			this.uncommitted = true;
		}

		// Token: 0x0600655F RID: 25951 RVA: 0x001AF6C0 File Offset: 0x001AD8C0
		internal override void SaveChanges(bool force)
		{
			base.CheckDisposed("AcrPropertyBag::SaveChanges");
			if (this.Mode == AcrPropertyBag.AcrMode.ReadOnly)
			{
				throw new AccessDeniedException(ServerStrings.ExItemIsOpenedInReadOnlyMode);
			}
			if (this.CanSaveBeNoOp() && !this.uncommitted)
			{
				return;
			}
			this.propertyBag.SaveChanges(force);
			this.uncommitted = false;
			this.RefreshCacheAfterSave(null);
		}

		// Token: 0x17001BF3 RID: 7155
		// (get) Token: 0x06006560 RID: 25952 RVA: 0x001AF717 File Offset: 0x001AD917
		internal override MapiProp MapiProp
		{
			get
			{
				return this.propertyBag.MapiProp;
			}
		}

		// Token: 0x17001BF4 RID: 7156
		// (get) Token: 0x06006561 RID: 25953 RVA: 0x001AF724 File Offset: 0x001AD924
		// (set) Token: 0x06006562 RID: 25954 RVA: 0x001AF72C File Offset: 0x001AD92C
		internal PersistablePropertyBag PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
			private set
			{
				if (this.propertyBag != null)
				{
					this.propertyBag.Dispose();
				}
				this.propertyBag = value;
				if (this.propertyBag != null)
				{
					this.propertyBag.PrefetchPropertyArray = this.PrefetchPropertyArray;
				}
			}
		}

		// Token: 0x17001BF5 RID: 7157
		// (get) Token: 0x06006563 RID: 25955 RVA: 0x001AF761 File Offset: 0x001AD961
		// (set) Token: 0x06006564 RID: 25956 RVA: 0x001AF76C File Offset: 0x001AD96C
		internal override ICollection<PropertyDefinition> PrefetchPropertyArray
		{
			get
			{
				return base.PrefetchPropertyArray;
			}
			set
			{
				PersistablePropertyBag persistablePropertyBag = this.propertyBag;
				base.PrefetchPropertyArray = value;
				persistablePropertyBag.PrefetchPropertyArray = value;
			}
		}

		// Token: 0x17001BF6 RID: 7158
		// (get) Token: 0x06006565 RID: 25957 RVA: 0x001AF78E File Offset: 0x001AD98E
		public override bool IsDirty
		{
			get
			{
				base.CheckDisposed("IsDirty::get");
				return this.propertyBag.IsDirty || this.propertyTrackingCache.Count != 0 || this.propertiesWrittenAsStream.Count != 0 || this.irresolvableChanges;
			}
		}

		// Token: 0x17001BF7 RID: 7159
		// (get) Token: 0x06006566 RID: 25958 RVA: 0x001AF7CA File Offset: 0x001AD9CA
		internal bool IsReadOnly
		{
			get
			{
				return this.Mode == AcrPropertyBag.AcrMode.ReadOnly;
			}
		}

		// Token: 0x17001BF8 RID: 7160
		// (get) Token: 0x06006567 RID: 25959 RVA: 0x001AF7D8 File Offset: 0x001AD9D8
		private AcrPropertyBag.AcrMode Mode
		{
			get
			{
				if (this.acrModeHint != AcrPropertyBag.AcrMode.Unknown)
				{
					return this.acrModeHint;
				}
				if (this.itemId == null)
				{
					this.acrModeHint = AcrPropertyBag.AcrMode.NewItem;
				}
				else if (this.openChangeKey == null)
				{
					this.acrModeHint = AcrPropertyBag.AcrMode.ReadOnly;
				}
				else if (this.currentChangeKey == null)
				{
					object obj = this.propertyBag.TryGetProperty(InternalSchema.ChangeKey);
					if (obj is byte[])
					{
						this.currentChangeKey = (byte[])obj;
						if (!ArrayComparer<byte>.Comparer.Equals(this.currentChangeKey, this.openChangeKey))
						{
							this.acrModeHint = AcrPropertyBag.AcrMode.Active;
						}
						else
						{
							this.acrModeHint = AcrPropertyBag.AcrMode.Passive;
						}
					}
					else
					{
						this.currentChangeKey = this.openChangeKey;
						this.acrModeHint = AcrPropertyBag.AcrMode.Passive;
					}
				}
				return this.acrModeHint;
			}
		}

		// Token: 0x17001BF9 RID: 7161
		// (get) Token: 0x06006568 RID: 25960 RVA: 0x001AF886 File Offset: 0x001ADA86
		// (set) Token: 0x06006569 RID: 25961 RVA: 0x001AF893 File Offset: 0x001ADA93
		internal override ExTimeZone ExTimeZone
		{
			get
			{
				return this.PropertyBag.ExTimeZone;
			}
			set
			{
				this.PropertyBag.ExTimeZone = value;
			}
		}

		// Token: 0x17001BFA RID: 7162
		// (get) Token: 0x0600656A RID: 25962 RVA: 0x001AF8A1 File Offset: 0x001ADAA1
		// (set) Token: 0x0600656B RID: 25963 RVA: 0x001AF8B9 File Offset: 0x001ADAB9
		internal override PropertyBagSaveFlags SaveFlags
		{
			get
			{
				base.CheckDisposed("AcrPropertyBag.SaveFlags.get");
				return this.propertyBag.SaveFlags;
			}
			set
			{
				base.CheckDisposed("AcrPropertyBag.SaveFlags.set");
				EnumValidator.ThrowIfInvalid<PropertyBagSaveFlags>(value, "value");
				this.propertyBag.SaveFlags = value;
			}
		}

		// Token: 0x0600656C RID: 25964 RVA: 0x001AF8DD File Offset: 0x001ADADD
		internal override void SetUpdateImapIdFlag()
		{
			base.CheckDisposed("AcrPropertyBag::SetUpdateImapIdFlag");
			this.propertyBag.SetUpdateImapIdFlag();
		}

		// Token: 0x0600656D RID: 25965 RVA: 0x001AF8F5 File Offset: 0x001ADAF5
		protected override bool InternalIsPropertyDirty(AtomicStorePropertyDefinition propertyDefinition)
		{
			base.CheckDisposed("InternalIsPropertyDirty");
			return ((IDirectPropertyBag)this.propertyBag).IsDirty(propertyDefinition);
		}

		// Token: 0x0600656E RID: 25966 RVA: 0x001AF90E File Offset: 0x001ADB0E
		protected override bool IsLoaded(NativeStorePropertyDefinition propertyDefinition)
		{
			return ((IDirectPropertyBag)this.PropertyBag).IsLoaded(propertyDefinition);
		}

		// Token: 0x17001BFB RID: 7163
		// (get) Token: 0x0600656F RID: 25967 RVA: 0x001AF91C File Offset: 0x001ADB1C
		internal override PropertyBagContext Context
		{
			get
			{
				return this.propertyBag.Context;
			}
		}

		// Token: 0x040039B1 RID: 14769
		private AcrPropertyBag.AcrMode acrModeHint;

		// Token: 0x040039B2 RID: 14770
		private PersistablePropertyBag propertyBag;

		// Token: 0x040039B3 RID: 14771
		private readonly AcrProfile profile;

		// Token: 0x040039B4 RID: 14772
		private readonly IPropertyBagFactory propertyBagFactory;

		// Token: 0x040039B5 RID: 14773
		private StoreObjectId itemId;

		// Token: 0x040039B6 RID: 14774
		private byte[] openChangeKey;

		// Token: 0x040039B7 RID: 14775
		private byte[] currentChangeKey;

		// Token: 0x040039B8 RID: 14776
		private bool irresolvableChanges;

		// Token: 0x040039B9 RID: 14777
		private Dictionary<PropertyDefinition, PropertyDefinition> propertiesWrittenAsStream = new Dictionary<PropertyDefinition, PropertyDefinition>();

		// Token: 0x040039BA RID: 14778
		private Dictionary<PropertyDefinition, AcrPropertyBag.TrackingInfo> propertyTrackingCache = new Dictionary<PropertyDefinition, AcrPropertyBag.TrackingInfo>();

		// Token: 0x040039BB RID: 14779
		private bool uncommitted;

		// Token: 0x02000AEA RID: 2794
		private enum AcrMode
		{
			// Token: 0x040039BD RID: 14781
			Unknown,
			// Token: 0x040039BE RID: 14782
			ReadOnly,
			// Token: 0x040039BF RID: 14783
			Active,
			// Token: 0x040039C0 RID: 14784
			Passive,
			// Token: 0x040039C1 RID: 14785
			NewItem = 5
		}

		// Token: 0x02000AEB RID: 2795
		private class TrackingInfo
		{
			// Token: 0x06006570 RID: 25968 RVA: 0x001AF929 File Offset: 0x001ADB29
			public TrackingInfo(bool dirty) : this(dirty, null, null)
			{
			}

			// Token: 0x06006571 RID: 25969 RVA: 0x001AF934 File Offset: 0x001ADB34
			public TrackingInfo(bool dirty, object serverValue, object originalValue)
			{
				this.Dirty = dirty;
				this.ServerValue = serverValue;
				this.OriginalValue = originalValue;
			}

			// Token: 0x040039C2 RID: 14786
			public object OriginalValue;

			// Token: 0x040039C3 RID: 14787
			public object ServerValue;

			// Token: 0x040039C4 RID: 14788
			public bool Dirty;
		}
	}
}
