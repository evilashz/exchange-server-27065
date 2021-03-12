using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000093 RID: 147
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class StoreObjectPropertyBag : PersistablePropertyBag
	{
		// Token: 0x06000A40 RID: 2624 RVA: 0x00046B64 File Offset: 0x00044D64
		internal StoreObjectPropertyBag(StoreSession session, MapiProp mapiProp, ICollection<PropertyDefinition> autoloadProperties) : this(session, mapiProp, autoloadProperties, true)
		{
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00046B70 File Offset: 0x00044D70
		internal StoreObjectPropertyBag(StoreSession session, MapiProp mapiProp, ICollection<PropertyDefinition> autoloadProperties, bool canSaveOrDisposeMapiProp)
		{
			this.trackingPropertyExistence = autoloadProperties.Contains(InternalSchema.PropertyExistenceTracker);
			bool flag = false;
			try
			{
				this.canSaveOrDisposeMapiProp = canSaveOrDisposeMapiProp;
				if (mapiProp != null)
				{
					this.MapiPropertyBag = new MapiPropertyBag(session, mapiProp);
					if (this.mapiPropertyBag != null && this.mapiPropertyBag.DisposeTracker != null)
					{
						this.mapiPropertyBag.DisposeTracker.AddExtraDataWithStackTrace("StoreObjectPropertyBag owns mapiPropertyBag at");
					}
				}
				this.ExTimeZone = session.ExTimeZone;
				this.PrefetchPropertyArray = autoloadProperties;
				this.Load(null);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					base.Dispose();
				}
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00046C30 File Offset: 0x00044E30
		internal override ICollection<NativeStorePropertyDefinition> AllNativeProperties
		{
			get
			{
				return this.memoryPropertyBag.AllNativeProperties;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00046C3D File Offset: 0x00044E3D
		public override bool HasAllPropertiesLoaded
		{
			get
			{
				base.CheckDisposed("HasAllPropertiesLoaded::get");
				return this.memoryPropertyBag.HasAllPropertiesLoaded;
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00046C55 File Offset: 0x00044E55
		protected override object TryGetStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException(ServerStrings.ExNullParameter("propertyDefinition", 1));
			}
			return ((IDirectPropertyBag)this.memoryPropertyBag).GetValue(propertyDefinition);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00046C7C File Offset: 0x00044E7C
		protected override void SetValidatedStoreProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException(ServerStrings.ExNullParameter("propertyDefinition", 1));
			}
			if (propertyValue == null)
			{
				throw PropertyError.ToException(new PropertyError[]
				{
					new PropertyError(propertyDefinition, PropertyErrorCode.NullValue)
				});
			}
			((IDirectPropertyBag)this.memoryPropertyBag).SetValue(propertyDefinition, propertyValue);
			this.TrackProperty(propertyDefinition, true);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00046CD2 File Offset: 0x00044ED2
		protected override void DeleteStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			((IDirectPropertyBag)this.MemoryPropertyBag).Delete(propertyDefinition);
			this.TrackProperty(propertyDefinition, false);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00046CE8 File Offset: 0x00044EE8
		public override void Load(ICollection<PropertyDefinition> properties)
		{
			this.BindToMapiPropertyBag();
			if (this.HasAllPropertiesLoaded)
			{
				return;
			}
			if (properties == InternalSchema.ContentConversionProperties || this.PrefetchPropertyArray == InternalSchema.ContentConversionProperties)
			{
				this.InternalLoadAllPropertiesOnItemForContentConversion();
				return;
			}
			this.InternalLoad(properties);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00046D1C File Offset: 0x00044F1C
		public override void Reload()
		{
			this.Clear();
			this.InternalLoad(null);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00046D2C File Offset: 0x00044F2C
		internal void ForceReload(ICollection<PropertyDefinition> propsToLoad)
		{
			if (base.IsNew)
			{
				throw new InvalidOperationException(ServerStrings.NoServerValueAvailable);
			}
			foreach (PropertyDefinition propertyDefinition in propsToLoad)
			{
				this.MemoryPropertyBag.Unload(propertyDefinition);
			}
			this.InternalLoad(propsToLoad);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00046D98 File Offset: 0x00044F98
		public override void Clear()
		{
			this.memoryPropertyBag.Clear();
			this.propertyExistenceBitMap = null;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00046DC4 File Offset: 0x00044FC4
		private void InternalLoad(ICollection<PropertyDefinition> extraProperties)
		{
			ICollection<PropertyDefinition> collection;
			if (!this.IsCacheValid)
			{
				collection = this.PrefetchPropertyArray.Concat(extraProperties);
			}
			else
			{
				collection = extraProperties;
			}
			if (collection == null || collection.Count == 0)
			{
				return;
			}
			if (this.mapiPropertyBag != null)
			{
				IList<NativeStorePropertyDefinition> nativePropertyDefinitions = StorePropertyDefinition.GetNativePropertyDefinitions<PropertyDefinition>(PropertyDependencyType.AllRead, collection, (NativeStorePropertyDefinition native) => !((IDirectPropertyBag)this.MemoryPropertyBag).IsLoaded(native));
				if (nativePropertyDefinitions.Count > 0)
				{
					object[] properties = this.MapiPropertyBag.GetProperties(nativePropertyDefinitions);
					this.MemoryPropertyBag.PreLoadStoreProperty<NativeStorePropertyDefinition>(nativePropertyDefinitions, properties);
					return;
				}
			}
			else
			{
				this.MemoryPropertyBag.Load(collection);
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00046E4C File Offset: 0x0004504C
		private void InternalLoadAllPropertiesOnItemForContentConversion()
		{
			if (this.mapiPropertyBag != null)
			{
				ICollection<PropValue> allProperties = this.MapiPropertyBag.GetAllProperties();
				if (allProperties.Count > 0)
				{
					this.MemoryPropertyBag.PreLoadStoreProperties(allProperties);
				}
			}
			this.MemoryPropertyBag.SetAllPropertiesLoaded();
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00046E8D File Offset: 0x0004508D
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<StoreObjectPropertyBag>(this);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00046E98 File Offset: 0x00045098
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
			if (this.mapiPropertyBag != null && this.mapiPropertyBag.DisposeTracker != null)
			{
				this.mapiPropertyBag.DisposeTracker.AddExtraDataWithStackTrace(string.Format(CultureInfo.InvariantCulture, "StoreObjectPropertyBag.InternalDispose({0}) called with stack", new object[]
				{
					disposing
				}));
			}
			if (disposing)
			{
				if (this.mapiPropertyBag != null)
				{
					if (!this.canSaveOrDisposeMapiProp)
					{
						this.MapiPropertyBag.DetachMapiProp();
					}
					this.MapiPropertyBag.Dispose();
				}
				foreach (StoreObjectStream storeObjectStream in this.listOfStreams)
				{
					storeObjectStream.DetachPropertyBag();
				}
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00046F60 File Offset: 0x00045160
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x00046F68 File Offset: 0x00045168
		internal override ICollection<PropertyDefinition> PrefetchPropertyArray
		{
			get
			{
				return base.PrefetchPropertyArray;
			}
			set
			{
				base.PrefetchPropertyArray = (value ?? ((ICollection<PropertyDefinition>)Array<PropertyDefinition>.Empty));
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00046F7F File Offset: 0x0004517F
		internal override MapiProp MapiProp
		{
			get
			{
				return this.MapiPropertyBag.MapiProp;
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00046F8C File Offset: 0x0004518C
		public override Stream OpenPropertyStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode)
		{
			base.CheckDisposed("OpenPropertyStream");
			EnumValidator.AssertValid<PropertyOpenMode>(openMode);
			NativeStorePropertyDefinition nativeStorePropertyDefinition = propertyDefinition as NativeStorePropertyDefinition;
			if (nativeStorePropertyDefinition == null)
			{
				throw new InvalidOperationException(ServerStrings.ExPropertyNotStreamable(propertyDefinition.ToString()));
			}
			Stream result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				StoreObjectStream storeObjectStream = new StoreObjectStream(this, nativeStorePropertyDefinition, openMode);
				disposeGuard.Add<StoreObjectStream>(storeObjectStream);
				this.listOfStreams.Add(storeObjectStream);
				disposeGuard.Success();
				if (openMode == PropertyOpenMode.Create || openMode == PropertyOpenMode.Modify)
				{
					this.TrackProperty(nativeStorePropertyDefinition, true);
				}
				result = storeObjectStream;
			}
			return result;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0004702C File Offset: 0x0004522C
		internal override void FlushChanges()
		{
			base.CheckDisposed("FlushChanges");
			List<PropertyError> list = new List<PropertyError>();
			this.BindToMapiPropertyBag();
			this.MapiPropertyBag.SaveFlags = this.saveFlags;
			foreach (StoreObjectStream storeObjectStream in this.listOfStreams)
			{
				storeObjectStream.Flush();
			}
			list.AddRange(this.FlushDeleteProperties());
			list.AddRange(this.FlushSetProperties());
			if ((this.saveFlags & PropertyBagSaveFlags.IgnoreMapiComputedErrors) == PropertyBagSaveFlags.IgnoreMapiComputedErrors)
			{
				for (int i = list.Count - 1; i >= 0; i--)
				{
					if (list[i].PropertyErrorCode == PropertyErrorCode.SetStoreComputedPropertyError)
					{
						list.RemoveAt(i);
					}
				}
			}
			if ((this.saveFlags & PropertyBagSaveFlags.IgnoreAccessDeniedErrors) == PropertyBagSaveFlags.IgnoreAccessDeniedErrors)
			{
				for (int j = list.Count - 1; j >= 0; j--)
				{
					if (list[j].PropertyErrorCode == PropertyErrorCode.AccessDenied)
					{
						list.RemoveAt(j);
					}
				}
			}
			if (list.Count > 0)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<int>((long)this.GetHashCode(), "StoreObjectPropertyBag::FlushChanges. Property was not set or deleted successfully. Error Count = {0}.", list.Count);
				throw PropertyError.ToException(list.ToArray());
			}
			this.memoryPropertyBag.ClearChangeInfo();
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00047164 File Offset: 0x00045364
		internal override void SaveChanges(bool force)
		{
			base.CheckDisposed("SaveChanges");
			if (this.canSaveOrDisposeMapiProp)
			{
				this.mapiPropertyBag.SaveChanges(force);
			}
			this.Clear();
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0004718B File Offset: 0x0004538B
		protected override bool InternalIsPropertyDirty(AtomicStorePropertyDefinition propertyDefinition)
		{
			base.CheckDisposed("InternalIsPropertyDirty");
			return ((IDirectPropertyBag)this.memoryPropertyBag).IsDirty(propertyDefinition);
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x000471A4 File Offset: 0x000453A4
		public override bool IsDirty
		{
			get
			{
				base.CheckDisposed("IsDirty::get");
				return this.canSaveOrDisposeMapiProp && this.memoryPropertyBag.IsDirty;
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000471C6 File Offset: 0x000453C6
		public override PropertyValueTrackingData GetOriginalPropertyInformation(PropertyDefinition propertyDefinition)
		{
			return this.memoryPropertyBag.GetOriginalPropertyInformation(propertyDefinition);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000471D4 File Offset: 0x000453D4
		protected override bool IsLoaded(NativeStorePropertyDefinition propertyDefinition)
		{
			return ((IDirectPropertyBag)this.memoryPropertyBag).IsLoaded(propertyDefinition);
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x000471E4 File Offset: 0x000453E4
		public override ICollection<PropertyDefinition> AllFoundProperties
		{
			get
			{
				if (this.Context.CoreState.Origin == Origin.Existing && !this.HasAllPropertiesLoaded)
				{
					return InternalSchema.Combine<PropertyDefinition>(this.MapiPropertyBag.GetAllFoundProperties(), this.MemoryPropertyBag.AllFoundProperties);
				}
				return this.MemoryPropertyBag.AllFoundProperties;
			}
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00047234 File Offset: 0x00045434
		protected PropertyError[] FlushSetProperties()
		{
			this.BindToMapiPropertyBag();
			if (this.memoryPropertyBag.ChangeList.Count > 0)
			{
				List<PropertyDefinition> list = new List<PropertyDefinition>();
				List<object> list2 = new List<object>();
				List<PropertyError> list3 = new List<PropertyError>();
				this.AddSubjectPropertiesToList(list, list2);
				foreach (PropertyDefinition propertyDefinition in this.memoryPropertyBag.ChangeList)
				{
					if (!propertyDefinition.Equals(InternalSchema.MapiSubject) && !propertyDefinition.Equals(InternalSchema.SubjectPrefix) && !propertyDefinition.Equals(InternalSchema.NormalizedSubjectInternal))
					{
						object obj = this.memoryPropertyBag.TryGetProperty(propertyDefinition);
						if (!(obj is PropertyError))
						{
							PropertyErrorCode? propertyErrorCode;
							if (this.ShouldSkipProperty(propertyDefinition, out propertyErrorCode))
							{
								list3.Add(new PropertyError(propertyDefinition, (propertyErrorCode != null) ? propertyErrorCode.Value : PropertyErrorCode.NotFound));
							}
							else
							{
								list.Add(propertyDefinition);
								list2.Add(obj);
							}
						}
					}
				}
				if (list2.Count > 0)
				{
					if ((this.saveFlags & PropertyBagSaveFlags.SaveFolderPropertyBagConditional) == PropertyBagSaveFlags.SaveFolderPropertyBagConditional)
					{
						list3.AddRange(this.MapiPropertyBag.SetPropertiesWithChangeKeyCheck(base.TryGetProperty(InternalSchema.ChangeKey) as byte[], list.ToArray(), list2.ToArray()));
					}
					else
					{
						list3.AddRange(this.MapiPropertyBag.SetProperties(list.ToArray(), list2.ToArray()));
					}
				}
				return list3.ToArray();
			}
			return MapiPropertyBag.EmptyPropertyErrorArray;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x000473A8 File Offset: 0x000455A8
		protected virtual bool ShouldSkipProperty(PropertyDefinition property, out PropertyErrorCode? error)
		{
			error = null;
			return false;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x000473B4 File Offset: 0x000455B4
		private void AddSubjectPropertiesToList(List<PropertyDefinition> propertyDefinitions, List<object> propertyValues)
		{
			object changedProperty = this.GetChangedProperty(InternalSchema.MapiSubject);
			object changedProperty2 = this.GetChangedProperty(InternalSchema.SubjectPrefixInternal);
			object changedProperty3 = this.GetChangedProperty(InternalSchema.NormalizedSubjectInternal);
			if (changedProperty2 != null)
			{
				propertyDefinitions.Add(InternalSchema.SubjectPrefixInternal);
				propertyValues.Add(changedProperty2);
			}
			if (changedProperty3 != null)
			{
				propertyDefinitions.Add(InternalSchema.NormalizedSubjectInternal);
				propertyValues.Add(changedProperty3);
			}
			if (changedProperty != null && (changedProperty2 == null || changedProperty3 == null))
			{
				propertyDefinitions.Add(InternalSchema.MapiSubject);
				propertyValues.Add(changedProperty);
			}
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0004742C File Offset: 0x0004562C
		protected PropertyError[] FlushDeleteProperties()
		{
			this.BindToMapiPropertyBag();
			if (this.memoryPropertyBag.DeleteList.Count > 0)
			{
				List<PropertyDefinition> list = new List<PropertyDefinition>();
				List<PropertyError> list2 = new List<PropertyError>();
				foreach (PropertyDefinition propertyDefinition in this.memoryPropertyBag.DeleteList)
				{
					PropertyErrorCode? propertyErrorCode;
					if (this.ShouldSkipProperty(propertyDefinition, out propertyErrorCode))
					{
						list2.Add(new PropertyError(propertyDefinition, (propertyErrorCode != null) ? propertyErrorCode.Value : PropertyErrorCode.NotFound));
					}
					else
					{
						list.Add(propertyDefinition);
					}
				}
				if (list.Count > 0)
				{
					list2.AddRange(this.MapiPropertyBag.DeleteProperties(list));
				}
				return list2.ToArray();
			}
			return MapiPropertyBag.EmptyPropertyErrorArray;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00047500 File Offset: 0x00045700
		protected virtual void LazyCreateMapiPropertyBag()
		{
			throw new InvalidOperationException("The property bag is not bound to a MapiProp object");
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0004750C File Offset: 0x0004570C
		protected void BindToMapiPropertyBag()
		{
			if (this.mapiPropertyBag == null)
			{
				this.LazyCreateMapiPropertyBag();
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x0004751C File Offset: 0x0004571C
		private bool IsCacheValid
		{
			get
			{
				return this.MemoryPropertyBag.HasAllPropertiesLoaded || this.MemoryPropertyBag.Count > 0;
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0004753C File Offset: 0x0004573C
		private object GetChangedProperty(PropertyDefinition property)
		{
			if (!this.memoryPropertyBag.ChangeList.Contains(property))
			{
				return null;
			}
			object obj = this.memoryPropertyBag.TryGetProperty(property);
			if (obj is PropertyError)
			{
				return null;
			}
			return obj;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00047576 File Offset: 0x00045776
		private void SyncPropertyBagParameters()
		{
			if (this.mapiPropertyBag != null)
			{
				this.MapiPropertyBag.ExTimeZone = this.memoryPropertyBag.ExTimeZone;
			}
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00047598 File Offset: 0x00045798
		private void TrackProperty(StorePropertyDefinition propertyDefinition, bool isAdded)
		{
			if (!this.trackingPropertyExistence)
			{
				return;
			}
			long bitFlag = PropertyExistenceTracker.GetBitFlag(propertyDefinition);
			if (bitFlag == -1L)
			{
				return;
			}
			if (this.propertyExistenceBitMap == null)
			{
				object obj = base.TryGetProperty(InternalSchema.PropertyExistenceTracker);
				if (obj is long)
				{
					this.propertyExistenceBitMap = new long?((long)obj);
				}
				else
				{
					this.propertyExistenceBitMap = new long?(0L);
				}
			}
			if (isAdded)
			{
				this.propertyExistenceBitMap = new long?(this.propertyExistenceBitMap.Value | bitFlag);
			}
			else
			{
				this.propertyExistenceBitMap &= ~bitFlag;
			}
			if (this.propertyExistenceBitMap.Value != 0L)
			{
				((IDirectPropertyBag)this.memoryPropertyBag).SetValue(InternalSchema.PropertyExistenceTracker, this.propertyExistenceBitMap.Value);
				return;
			}
			((IDirectPropertyBag)this.memoryPropertyBag).Delete(InternalSchema.PropertyExistenceTracker);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0004768C File Offset: 0x0004588C
		internal static StoreObjectPropertyBag CreatePropertyBag(StoreSession storeSession, StoreObjectId id, ICollection<PropertyDefinition> prefetchPropertyArray)
		{
			Util.ThrowOnNullArgument(id, "id");
			MapiProp mapiProp = null;
			StoreObjectPropertyBag storeObjectPropertyBag = null;
			bool flag = false;
			StoreObjectPropertyBag result;
			try
			{
				mapiProp = storeSession.GetMapiProp(id);
				storeObjectPropertyBag = new StoreObjectPropertyBag(storeSession, mapiProp, prefetchPropertyArray);
				flag = true;
				result = storeObjectPropertyBag;
			}
			finally
			{
				if (!flag)
				{
					Util.DisposeIfPresent(storeObjectPropertyBag);
					Util.DisposeIfPresent(mapiProp);
				}
			}
			return result;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x000476E4 File Offset: 0x000458E4
		internal static PropertyError[] MapiPropProblemsToPropertyErrors(PropertyDefinition[] propertyDefinitions, PropProblem[] problems)
		{
			PropertyError[] array = new PropertyError[problems.Length];
			for (int i = 0; i < problems.Length; i++)
			{
				string errorDescription;
				PropertyErrorCode error = MapiPropertyHelper.MapiErrorToXsoError(problems[i].Scode, out errorDescription);
				array[i] = new PropertyError(propertyDefinitions[problems[i].Index], error, errorDescription);
			}
			return array;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00047738 File Offset: 0x00045938
		internal static PropertyError[] MapiPropProblemsToPropertyErrors(StoreSession storeSession, MapiProp mapiProp, PropProblem[] problems)
		{
			PropTag[] array = new PropTag[problems.Length];
			PropProblem[] array2 = new PropProblem[problems.Length];
			for (int i = 0; i < problems.Length; i++)
			{
				array[i] = problems[i].PropTag;
				array2[i] = new PropProblem(i, problems[i].PropTag, problems[i].Scode);
			}
			NativeStorePropertyDefinition[] propertyDefinitions = PropertyTagCache.Cache.PropertyDefinitionsFromPropTags(NativeStorePropertyDefinition.TypeCheckingFlag.DisableTypeCheck, mapiProp, storeSession, array);
			return StoreObjectPropertyBag.MapiPropProblemsToPropertyErrors(propertyDefinitions, array2);
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x000477B4 File Offset: 0x000459B4
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x000477C7 File Offset: 0x000459C7
		internal override PropertyBagSaveFlags SaveFlags
		{
			get
			{
				base.CheckDisposed("StoreObjectPropertyBag.SaveFlags.get");
				return this.saveFlags;
			}
			set
			{
				base.CheckDisposed("StoreObjectPropertyBag.SaveFlags.set");
				EnumValidator.ThrowIfInvalid<PropertyBagSaveFlags>(value, "value");
				this.saveFlags = value;
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x000477E6 File Offset: 0x000459E6
		internal override void SetUpdateImapIdFlag()
		{
			base.CheckDisposed("SetUpdateImapIdFlag");
			this.MapiPropertyBag.SetUpdateImapIdFlag();
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000477FE File Offset: 0x000459FE
		[Conditional("DEBUG")]
		internal static void CheckNativePropertyDefinition(PropertyDefinition propertyDefinition)
		{
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00047800 File Offset: 0x00045A00
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x0004780D File Offset: 0x00045A0D
		internal override ExTimeZone ExTimeZone
		{
			get
			{
				return this.memoryPropertyBag.ExTimeZone;
			}
			set
			{
				this.memoryPropertyBag.ExTimeZone = value;
				this.SyncPropertyBagParameters();
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00047821 File Offset: 0x00045A21
		internal void OnStreamClose(StoreObjectStream stream)
		{
			this.listOfStreams.Remove(stream);
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00047830 File Offset: 0x00045A30
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x0004784B File Offset: 0x00045A4B
		internal MapiPropertyBag MapiPropertyBag
		{
			get
			{
				if (this.mapiPropertyBag == null)
				{
					throw new InvalidOperationException("The property bag is not bound to a MapiProp object");
				}
				return this.mapiPropertyBag;
			}
			set
			{
				Util.ThrowOnNullArgument(value, "value");
				if (this.mapiPropertyBag != null)
				{
					throw new InvalidOperationException("MapiPropertyBag has been already assigned");
				}
				this.mapiPropertyBag = value;
				this.SyncPropertyBagParameters();
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x00047878 File Offset: 0x00045A78
		internal MemoryPropertyBag MemoryPropertyBag
		{
			get
			{
				return this.memoryPropertyBag;
			}
		}

		// Token: 0x04000290 RID: 656
		private MapiPropertyBag mapiPropertyBag;

		// Token: 0x04000291 RID: 657
		private readonly MemoryPropertyBag memoryPropertyBag = new MemoryPropertyBag();

		// Token: 0x04000292 RID: 658
		private readonly bool canSaveOrDisposeMapiProp = true;

		// Token: 0x04000293 RID: 659
		private PropertyBagSaveFlags saveFlags;

		// Token: 0x04000294 RID: 660
		private readonly List<StoreObjectStream> listOfStreams = new List<StoreObjectStream>();

		// Token: 0x04000295 RID: 661
		private readonly bool trackingPropertyExistence;

		// Token: 0x04000296 RID: 662
		private long? propertyExistenceBitMap;

		// Token: 0x02000094 RID: 148
		internal enum PropertyAccessMode
		{
			// Token: 0x04000298 RID: 664
			Stream,
			// Token: 0x04000299 RID: 665
			Value
		}
	}
}
