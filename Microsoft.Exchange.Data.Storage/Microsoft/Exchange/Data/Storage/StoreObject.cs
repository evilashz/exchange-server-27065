using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000015 RID: 21
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class StoreObject : IDisposeTrackable, ILocationIdentifierController, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06000239 RID: 569 RVA: 0x00013738 File Offset: 0x00011938
		internal StoreObject(ICoreObject coreObject, bool shallowDispose = false)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				StorageGlobals.TraceConstructIDisposable(this);
				this.disposeTracker = this.GetDisposeTracker();
				this.shallowDispose = shallowDispose;
				DisposableObject disposableObject = coreObject as DisposableObject;
				if (disposableObject != null && disposableObject.DisposeTracker != null)
				{
					disposableObject.DisposeTracker.AddExtraDataWithStackTrace("StoreObject (e.g. MessageItem) owns coreObject (e.g. CoreItem) at");
				}
				this.AttachCoreObject(coreObject);
				((IDirectPropertyBag)this.PropertyBag).Context.StoreObject = this;
				disposeGuard.Success();
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000137CC File Offset: 0x000119CC
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
		{
			this.CheckDisposed("GetProperties");
			if (propertyDefinitionArray == null || propertyDefinitionArray.Count == 0)
			{
				return Array<object>.Empty;
			}
			object[] array = new object[propertyDefinitionArray.Count];
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in propertyDefinitionArray)
			{
				array[num++] = this.TryGetProperty(propertyDefinition);
			}
			return array;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00013848 File Offset: 0x00011A48
		public void SetProperties(ICollection<PropertyDefinition> propertyDefinitions, object[] propertyValues)
		{
			this.CheckDisposed("SetProperties");
			if (propertyDefinitions == null)
			{
				return;
			}
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in propertyDefinitions)
			{
				this[propertyDefinition] = propertyValues[num++];
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600023C RID: 572 RVA: 0x000138A8 File Offset: 0x00011AA8
		public virtual bool IsDirty
		{
			get
			{
				return this.coreObject != null && this.coreObject.IsDirty;
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000138BF File Offset: 0x00011ABF
		public bool IsPropertyDirty(PropertyDefinition propertyDefinition)
		{
			this.CheckDisposed("IsPropertyDirty");
			return this.PropertyBag.IsPropertyDirty(propertyDefinition);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000138D8 File Offset: 0x00011AD8
		public void Load()
		{
			this.Load(null);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000138E1 File Offset: 0x00011AE1
		public void Load(params PropertyDefinition[] properties)
		{
			this.Load((ICollection<PropertyDefinition>)properties);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000138EF File Offset: 0x00011AEF
		public void Load(ICollection<PropertyDefinition> properties)
		{
			this.CheckDisposed("Load");
			ExTraceGlobals.StorageTracer.Information<int>((long)this.GetHashCode(), "StoreObject:Load. Hashcode = {0}.", this.GetHashCode());
			this.PropertyBag.Load(properties);
		}

		// Token: 0x1700009A RID: 154
		public virtual object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				this.CheckDisposed("this::get[PropertyDefinition]");
				return this.GetProperty(propertyDefinition);
			}
			set
			{
				this.CheckDisposed("this::set[PropertyDefinition]");
				this.SetProperty(propertyDefinition, value);
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0001394D File Offset: 0x00011B4D
		public object TryGetProperty(PropertyDefinition propertyDefinition)
		{
			this.CheckDisposed("TryGetProperty");
			if (propertyDefinition is SimpleVirtualPropertyDefinition)
			{
				return new PropertyError(propertyDefinition, PropertyErrorCode.NotFound);
			}
			return this.PropertyBag.TryGetProperty(propertyDefinition);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00013976 File Offset: 0x00011B76
		public object TryGetProperty(StorePropertyDefinition propertyDefinition)
		{
			this.CheckDisposed("TryGetProperty");
			if (propertyDefinition is SimpleVirtualPropertyDefinition)
			{
				return new PropertyError(propertyDefinition, PropertyErrorCode.NotFound);
			}
			return this.PropertyBag.TryGetProperty(propertyDefinition);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0001399F File Offset: 0x00011B9F
		public void Delete(PropertyDefinition propertyDefinition)
		{
			this.CheckDisposed("Delete");
			this.PropertyBag.Delete(propertyDefinition);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000139B8 File Offset: 0x00011BB8
		public Stream OpenPropertyStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode)
		{
			this.CheckDisposed("OpenPropertyStream");
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			EnumValidator.ThrowIfInvalid<PropertyOpenMode>(openMode, "openMode");
			InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			if (!(propertyDefinition is NativeStorePropertyDefinition))
			{
				throw new NotImplementedException(ServerStrings.ExCalculatedPropertyStreamAccessNotSupported(propertyDefinition.Name));
			}
			return this.PropertyBag.OpenPropertyStream(propertyDefinition, openMode);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00013A1B File Offset: 0x00011C1B
		protected virtual void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (this.coreObject == null)
			{
				throw new InvalidOperationException("The core object is null");
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00013A50 File Offset: 0x00011C50
		public void SetDisposeTrackerStacktraceToCurrentLocation()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.SetReportedStacktraceToCurrentLocation();
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00013A65 File Offset: 0x00011C65
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00013A74 File Offset: 0x00011C74
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00013A9C File Offset: 0x00011C9C
		protected virtual void InternalDispose(bool disposing)
		{
			if (this.shallowDispose)
			{
				this.coreObject = null;
			}
			if (this.coreObject != null)
			{
				DisposableObject disposableObject = this.coreObject as DisposableObject;
				if (disposableObject != null && disposableObject.DisposeTracker != null)
				{
					disposableObject.DisposeTracker.AddExtraDataWithStackTrace(string.Format(CultureInfo.InvariantCulture, "StoreObject.InternalDispose({0}) called with stack", new object[]
					{
						disposing
					}));
				}
			}
			if (disposing)
			{
				Util.DisposeIfPresent(this.coreObject);
				this.coreObject = null;
				Util.DisposeIfPresent(this.disposeTracker);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00013B22 File Offset: 0x00011D22
		internal bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		// Token: 0x0600024D RID: 589
		public abstract DisposeTracker GetDisposeTracker();

		// Token: 0x0600024E RID: 590 RVA: 0x00013B2A File Offset: 0x00011D2A
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600024F RID: 591
		// (set) Token: 0x06000250 RID: 592
		public abstract string ClassName { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00013B3F File Offset: 0x00011D3F
		public virtual Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return StoreObjectSchema.Instance;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00013B51 File Offset: 0x00011D51
		public StoreSession Session
		{
			get
			{
				this.CheckDisposed("Session::get");
				return this.CoreObject.Session;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00013B69 File Offset: 0x00011D69
		IStoreSession IStoreObject.Session
		{
			get
			{
				return this.Session;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00013B71 File Offset: 0x00011D71
		public VersionedId Id
		{
			get
			{
				this.CheckDisposed("Id::get");
				return this.CoreObject.Id;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00013B89 File Offset: 0x00011D89
		public byte[] RecordKey
		{
			get
			{
				return this.GetValueOrDefault<byte[]>(StoreObjectSchema.RecordKey);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00013B98 File Offset: 0x00011D98
		public StoreObjectId ParentId
		{
			get
			{
				this.CheckDisposed("ParentId::get");
				StoreObjectId valueOrDefault = this.GetValueOrDefault<StoreObjectId>(InternalSchema.ParentItemId);
				if (valueOrDefault == null)
				{
					ExTraceGlobals.StorageTracer.TraceError<LocalizedString>((long)this.GetHashCode(), "StoreObject::ParentId::get. Error: {0}", ServerStrings.ExItemNoParentId);
					throw new InvalidOperationException(ServerStrings.ExItemNoParentId);
				}
				return valueOrDefault;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00013BEB File Offset: 0x00011DEB
		protected bool IsInMemoryObject
		{
			get
			{
				return this.Session == null;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00013BF6 File Offset: 0x00011DF6
		public virtual ObjectState ObjectState
		{
			get
			{
				if (this.IsNew)
				{
					return ObjectState.New;
				}
				if (this.IsDirty)
				{
					return ObjectState.Changed;
				}
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00013C0D File Offset: 0x00011E0D
		public ExDateTime CreationTime
		{
			get
			{
				this.CheckDisposed("CreationTime::get");
				return this.GetValueOrDefault<ExDateTime>(InternalSchema.CreationTime, ExDateTime.MinValue);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00013C2A File Offset: 0x00011E2A
		public ExDateTime LastModifiedTime
		{
			get
			{
				this.CheckDisposed("LastModifiedTime::get");
				return this.GetValueOrDefault<ExDateTime>(InternalSchema.LastModifiedTime, ExDateTime.MinValue);
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00013C48 File Offset: 0x00011E48
		public StoreObjectValidationError[] Validate()
		{
			this.CheckDisposed("Validate");
			ValidationContext context = new ValidationContext(this.Session);
			return Validation.CreateStoreObjectValiationErrorArray(this.CoreObject, context);
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00013C78 File Offset: 0x00011E78
		// (set) Token: 0x0600025D RID: 605 RVA: 0x00013C90 File Offset: 0x00011E90
		public bool EnableFullValidation
		{
			get
			{
				this.CheckDisposed("EnableFullValidation::get");
				return this.CoreObject.ValidateAllProperties;
			}
			set
			{
				this.CheckDisposed("EnableFullValidation::set");
				this.CoreObject.SetEnableFullValidation(value);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00013CA9 File Offset: 0x00011EA9
		public DisposeTracker DisposeTracker
		{
			get
			{
				this.CheckDisposed("DisposeTracker::get");
				return this.disposeTracker;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00013CBC File Offset: 0x00011EBC
		public StoreObjectId InternalObjectId
		{
			get
			{
				this.CheckDisposed("InternalObjectId::get");
				return this.CoreObject.InternalStoreObjectId;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00013CD4 File Offset: 0x00011ED4
		public StoreObjectId StoreObjectId
		{
			get
			{
				this.CheckDisposed("StoreObjectId::get");
				return this.CoreObject.StoreObjectId;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00013CEC File Offset: 0x00011EEC
		public ICollection<PropertyDefinition> PrefetchPropertyArray
		{
			get
			{
				this.CheckDisposed("PrefetchProperties::get");
				return this.PropertyBag.PrefetchPropertyArray;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00013D04 File Offset: 0x00011F04
		public bool IsNew
		{
			get
			{
				return this.CoreObject.Origin == Origin.New;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00013D14 File Offset: 0x00011F14
		internal ICollection<NativeStorePropertyDefinition> AllNativeProperties
		{
			get
			{
				return this.PropertyBag.AllNativeProperties;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00013D21 File Offset: 0x00011F21
		internal bool IsAttached
		{
			get
			{
				return this.CoreObject.ItemLevel == ItemLevel.Attached;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00013D31 File Offset: 0x00011F31
		private void AttachCoreObject(ICoreObject coreObject)
		{
			this.DetachCoreObject();
			this.coreObject = coreObject;
			if (this.coreObject != null)
			{
				this.OnCoreObjectAttached();
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00013D50 File Offset: 0x00011F50
		private ICoreObject DetachCoreObject()
		{
			ICoreObject coreObject = this.coreObject;
			if (coreObject != null)
			{
				this.OnCoreObjectDetaching();
				this.coreObject = null;
			}
			return coreObject;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00013D78 File Offset: 0x00011F78
		public void DeleteProperties(params PropertyDefinition[] propertyDefinitions)
		{
			this.CheckDisposed("DeleteProperties");
			if (propertyDefinitions == null)
			{
				throw new ArgumentNullException("propertyDefinitions");
			}
			foreach (PropertyDefinition propertyDefinition in propertyDefinitions)
			{
				this.Delete(propertyDefinition);
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00013DB9 File Offset: 0x00011FB9
		private void SetProperty(PropertyDefinition propertyDefinition, object propertyValue)
		{
			this.CheckDisposed("SetProperty");
			this.PropertyBag.SetProperty(propertyDefinition, propertyValue);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00013DD3 File Offset: 0x00011FD3
		private object GetProperty(PropertyDefinition propertyDefinition)
		{
			this.CheckDisposed("GetProperty");
			if (propertyDefinition is SimpleVirtualPropertyDefinition)
			{
				return new PropertyError(propertyDefinition, PropertyErrorCode.NotFound);
			}
			return this.PropertyBag[propertyDefinition];
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00013DFC File Offset: 0x00011FFC
		public void SetOrDeleteProperty(PropertyDefinition propertyDefinition, object propertyValue)
		{
			this.CheckDisposed("SetOrDeleteProperty");
			this.PropertyBag.SetOrDeleteProperty(propertyDefinition, propertyValue);
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00013E16 File Offset: 0x00012016
		protected virtual bool CanDoObjectUpdate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00013E19 File Offset: 0x00012019
		public PersistablePropertyBag PropertyBag
		{
			get
			{
				this.CheckDisposed("PropertyBag::get");
				return Microsoft.Exchange.Data.Storage.CoreObject.GetPersistablePropertyBag(this.CoreObject);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00013E31 File Offset: 0x00012031
		// (set) Token: 0x0600026E RID: 622 RVA: 0x00013E44 File Offset: 0x00012044
		public ICoreObject CoreObject
		{
			get
			{
				this.CheckDisposed("CoreObject::get");
				return this.coreObject;
			}
			set
			{
				this.CheckDisposed("CoreObject::set");
				this.AttachCoreObject(value);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00013E58 File Offset: 0x00012058
		internal MapiProp MapiProp
		{
			get
			{
				this.CheckDisposed("MapiProp::get");
				return this.PropertyBag.MapiProp;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00013E70 File Offset: 0x00012070
		public T GetValueOrDefault<T>(PropertyDefinition propertyDefinition)
		{
			StorePropertyDefinition propertyDefinition2 = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			return this.GetValueOrDefault<T>(propertyDefinition2);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00013E8C File Offset: 0x0001208C
		public T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition)
		{
			return this.GetValueOrDefault<T>(propertyDefinition, default(T));
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00013EAC File Offset: 0x000120AC
		public T GetValueOrDefault<T>(PropertyDefinition propertyDefinition, T defaultValue)
		{
			StorePropertyDefinition propertyDefinition2 = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			return this.GetValueOrDefault<T>(propertyDefinition2, defaultValue);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00013EC8 File Offset: 0x000120C8
		public T GetValueOrDefault<T>(StorePropertyDefinition propertyDefinition, T defaultValue)
		{
			this.CheckDisposed("GetValueOrDefault");
			return Microsoft.Exchange.Data.Storage.PropertyBag.CheckPropertyValue<T>(propertyDefinition, this.TryGetProperty(propertyDefinition), defaultValue);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00013EE4 File Offset: 0x000120E4
		public T? GetValueAsNullable<T>(PropertyDefinition propertyDefinition) where T : struct
		{
			StorePropertyDefinition propertyDefinition2 = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			return this.GetValueAsNullable<T>(propertyDefinition2);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00013EFF File Offset: 0x000120FF
		public T? GetValueAsNullable<T>(StorePropertyDefinition propertyDefinition) where T : struct
		{
			this.CheckDisposed("GetValueAsNullable");
			return Microsoft.Exchange.Data.Storage.PropertyBag.CheckNullablePropertyValue<T>(propertyDefinition, this.TryGetProperty(propertyDefinition));
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00013F19 File Offset: 0x00012119
		public void SafeSetProperty(PropertyDefinition propertyDefinition, object propValue)
		{
			this.CheckDisposed("SafeSetProperty");
			if (propValue != null && !(propValue is PropertyError))
			{
				this.SetProperty(propertyDefinition, propValue);
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00013F39 File Offset: 0x00012139
		internal static object SafePropertyValue(object propValue, Type type, object defaultValue)
		{
			if (propValue != null && propValue.GetType() == type)
			{
				return propValue;
			}
			return defaultValue;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00013F50 File Offset: 0x00012150
		internal T DownCastStoreObject<T>() where T : StoreObject
		{
			T t = this as T;
			if (t == null)
			{
				throw new WrongObjectTypeException(ServerStrings.BindToWrongObjectType(this.ClassName, typeof(T).ToString()));
			}
			return t;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00013F92 File Offset: 0x00012192
		protected virtual void OnCoreObjectAttached()
		{
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00013F94 File Offset: 0x00012194
		protected virtual void OnCoreObjectDetaching()
		{
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00013F96 File Offset: 0x00012196
		public LocationIdentifierHelper LocationIdentifierHelperInstance
		{
			get
			{
				this.CheckDisposed("get_LocationIdentifierHelperInstance");
				if (this.coreObject == null || !(this.coreObject is ILocationIdentifierController))
				{
					return null;
				}
				return ((ILocationIdentifierController)this.coreObject).LocationIdentifierHelperInstance;
			}
		}

		// Token: 0x040000A4 RID: 164
		private readonly DisposeTracker disposeTracker;

		// Token: 0x040000A5 RID: 165
		private readonly bool shallowDispose;

		// Token: 0x040000A6 RID: 166
		private ICoreObject coreObject;

		// Token: 0x040000A7 RID: 167
		private bool isDisposed;
	}
}
