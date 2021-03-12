using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Mapi.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Mapi;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x0200001D RID: 29
	[Serializable]
	public abstract class MapiObject : ConfigurableObject, IDisposeTrackable, IDisposable
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000050FD File Offset: 0x000032FD
		// (set) Token: 0x06000086 RID: 134 RVA: 0x0000510A File Offset: 0x0000330A
		public MapiObjectId MapiIdentity
		{
			get
			{
				return (MapiObjectId)this.GetIdentity();
			}
			internal set
			{
				this.SetIdentity(value);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005114 File Offset: 0x00003314
		internal static MapiEntryId GetEntryIdentity(MapiProp mapiObject)
		{
			MapiEntryId result;
			try
			{
				if (mapiObject == null)
				{
					throw new ArgumentNullException("mapiObject");
				}
				PropValue prop = mapiObject.GetProp(PropTag.EntryId);
				if (PropType.Error == prop.PropType)
				{
					result = null;
				}
				else
				{
					result = new MapiEntryId(prop.GetBytes());
				}
			}
			catch (MapiPermanentException)
			{
				result = null;
			}
			catch (MapiRetryableException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005180 File Offset: 0x00003380
		internal static string GetName(MapiProp mapiObject)
		{
			string result;
			try
			{
				if (mapiObject == null)
				{
					throw new ArgumentNullException("mapiObject");
				}
				PropValue prop = mapiObject.GetProp(PropTag.DisplayName);
				if (PropType.Error == prop.PropType)
				{
					result = null;
				}
				else
				{
					result = prop.GetString();
				}
			}
			catch (MapiPermanentException)
			{
				result = null;
			}
			catch (MapiRetryableException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000051E8 File Offset: 0x000033E8
		internal void DisposeCheck()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005203 File Offset: 0x00003403
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005212 File Offset: 0x00003412
		internal virtual void ReleaseUnmanagedResources()
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005214 File Offset: 0x00003414
		internal void ResetChangeTrackingAndObjectState()
		{
			base.ResetChangeTracking(true);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000521D File Offset: 0x0000341D
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.ReleaseUnmanagedResources();
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
						this.disposeTracker = null;
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005251 File Offset: 0x00003451
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MapiObject>(this);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005259 File Offset: 0x00003459
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000526E File Offset: 0x0000346E
		protected void EnableDisposeTracking()
		{
			if (this.disposeTracker == null)
			{
				this.disposeTracker = this.GetDisposeTracker();
			}
			this.disposed = false;
		}

		// Token: 0x06000091 RID: 145
		protected abstract ObjectId GetIdentity();

		// Token: 0x06000092 RID: 146 RVA: 0x0000528B File Offset: 0x0000348B
		protected virtual void SetIdentity(ObjectId identity)
		{
			if (identity == null)
			{
				throw new MapiInvalidOperationException(Strings.ExceptionIdentityNull);
			}
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = identity;
		}

		// Token: 0x06000093 RID: 147
		internal abstract void Save(bool keepUnmanagedResources);

		// Token: 0x06000094 RID: 148
		internal abstract void Read(bool keepUnmanagedResources);

		// Token: 0x06000095 RID: 149
		internal abstract void Delete();

		// Token: 0x06000096 RID: 150
		internal abstract T[] Find<T>(QueryFilter filter, MapiObjectId root, QueryScope scope, SortBy sort, int maximumResultsSize) where T : IConfigurable, new();

		// Token: 0x06000097 RID: 151
		internal abstract IEnumerable<T> FindPaged<T>(QueryFilter filter, MapiObjectId root, QueryScope scope, SortBy sort, int pageSize, int maximumResultsSize) where T : IConfigurable, new();

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000098 RID: 152
		internal abstract MapiProp RawMapiEntry { get; }

		// Token: 0x06000099 RID: 153 RVA: 0x000052AD File Offset: 0x000034AD
		internal virtual MapiProp GetRawMapiEntry(out MapiStore store)
		{
			throw new NotImplementedException("MapiObject.GetRawMapiEntry is not implemented.");
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000052B9 File Offset: 0x000034B9
		internal bool IsOriginatingServerRetrieved
		{
			get
			{
				return null != this.originatingServer;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000052C7 File Offset: 0x000034C7
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000052CF File Offset: 0x000034CF
		public Fqdn OriginatingServer
		{
			get
			{
				return this.originatingServer;
			}
			internal set
			{
				this.originatingServer = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000052D8 File Offset: 0x000034D8
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000052E0 File Offset: 0x000034E0
		internal MapiSession MapiSession
		{
			get
			{
				return this.mapiSession;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("MapiSession");
				}
				this.mapiSession = value;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000052F8 File Offset: 0x000034F8
		internal void Instantiate(PropValue[] propertyValues)
		{
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			if (propertyValues.Length == 0)
			{
				return;
			}
			if (!(this.ObjectSchema is MapiObjectSchema))
			{
				throw new MapiInvalidOperationException(Strings.ExceptionSchemaInvalidCast(this.ObjectSchema.GetType().ToString()));
			}
			base.InstantiationErrors.Clear();
			MapiStore mapiStore = null;
			MapiProp mapiProp = null;
			try
			{
				foreach (PropertyDefinition propertyDefinition in this.ObjectSchema.AllProperties)
				{
					MapiPropertyDefinition mapiPropertyDefinition = (MapiPropertyDefinition)propertyDefinition;
					if (!mapiPropertyDefinition.IsCalculated && mapiPropertyDefinition.PropertyTag != PropTag.Null)
					{
						bool flag = false;
						PropValue propValue = new PropValue(PropTag.Null, null);
						foreach (PropValue propValue in propertyValues)
						{
							if (propValue.PropTag.Id() == mapiPropertyDefinition.PropertyTag.Id())
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							if (mapiPropertyDefinition.IsMandatory)
							{
								base.InstantiationErrors.Add(new PropertyValidationError(Strings.ErrorMandatoryPropertyMissing(mapiPropertyDefinition.Name), mapiPropertyDefinition, null));
							}
						}
						else
						{
							if (PropType.Error == propValue.PropType && propValue.GetErrorValue() == -2147024882)
							{
								if (mapiProp == null)
								{
									mapiProp = this.GetRawMapiEntry(out mapiStore);
								}
								propValue = mapiProp.GetProp(mapiPropertyDefinition.PropertyTag);
							}
							if (PropType.Error == propValue.PropType)
							{
								ExTraceGlobals.MapiObjectTracer.TraceError<PropTag>((long)this.GetHashCode(), "Retrieving PropTag '{0}' failed.", mapiPropertyDefinition.PropertyTag);
							}
							else
							{
								try
								{
									object value = mapiPropertyDefinition.Extractor(propValue, mapiPropertyDefinition);
									IList<ValidationError> list = mapiPropertyDefinition.ValidateProperty(value, this.propertyBag, false);
									if (list != null)
									{
										base.InstantiationErrors.AddRange(list);
									}
									this.propertyBag.SetField(mapiPropertyDefinition, value);
								}
								catch (MapiConvertingException ex)
								{
									base.InstantiationErrors.Add(new PropertyConversionError(ex.LocalizedString, mapiPropertyDefinition, propValue, ex));
								}
							}
						}
					}
				}
			}
			finally
			{
				if (mapiProp != null)
				{
					mapiProp.Dispose();
					mapiProp = null;
				}
				if (mapiStore != null)
				{
					mapiStore.Dispose();
					mapiStore = null;
				}
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005548 File Offset: 0x00003748
		internal virtual void AdjustPropertyTagsToRead(List<PropTag> propertyTags)
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000554C File Offset: 0x0000374C
		internal PropTag[] GetPropertyTagsToRead()
		{
			if (!(this.ObjectSchema is MapiObjectSchema))
			{
				throw new MapiInvalidOperationException(Strings.ExceptionSchemaInvalidCast(this.ObjectSchema.GetType().ToString()));
			}
			List<PropTag> list = new List<PropTag>(this.ObjectSchema.AllProperties.Count);
			foreach (PropertyDefinition propertyDefinition in this.ObjectSchema.AllProperties)
			{
				MapiPropertyDefinition mapiPropertyDefinition = (MapiPropertyDefinition)propertyDefinition;
				if (mapiPropertyDefinition.PropertyTag != PropTag.Null && !mapiPropertyDefinition.IsCalculated)
				{
					list.Add(mapiPropertyDefinition.PropertyTag);
				}
			}
			this.AdjustPropertyTagsToRead(list);
			return list.ToArray();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000560C File Offset: 0x0000380C
		internal virtual void AdjustPropertyTagsToDelete(List<PropTag> propertyTags)
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005610 File Offset: 0x00003810
		internal PropTag[] GetPropertyTagsToDelete()
		{
			if (!(this.ObjectSchema is MapiObjectSchema))
			{
				throw new MapiInvalidOperationException(Strings.ExceptionSchemaInvalidCast(this.ObjectSchema.GetType().ToString()));
			}
			List<PropTag> list = new List<PropTag>(this.ObjectSchema.AllProperties.Count);
			foreach (PropertyDefinition propertyDefinition in this.ObjectSchema.AllProperties)
			{
				MapiPropertyDefinition mapiPropertyDefinition = (MapiPropertyDefinition)propertyDefinition;
				if (mapiPropertyDefinition.PropertyTag != PropTag.Null && !mapiPropertyDefinition.IsCalculated && !mapiPropertyDefinition.IsFilterOnly && !mapiPropertyDefinition.IsReadOnly)
				{
					bool flag = false;
					if (this.propertyBag.IsChanged(mapiPropertyDefinition))
					{
						object obj = null;
						if (this.propertyBag.TryGetField(mapiPropertyDefinition, ref obj) && !mapiPropertyDefinition.PersistDefaultValue && mapiPropertyDefinition.DefaultValue == obj)
						{
							list.Add(mapiPropertyDefinition.PropertyTag);
							flag = true;
						}
					}
					if (mapiPropertyDefinition.IsMandatory && flag)
					{
						throw new DataValidationException(new PropertyValidationError(Strings.ErrorMandatoryPropertyMissing(mapiPropertyDefinition.Name), mapiPropertyDefinition, null));
					}
				}
			}
			this.AdjustPropertyTagsToDelete(list);
			return list.ToArray();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000573C File Offset: 0x0000393C
		internal virtual void AdjustPropertyValuesToUpdate(List<PropValue> propertyValues)
		{
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005740 File Offset: 0x00003940
		internal PropValue[] GetPropertyValuesToUpdate()
		{
			if (!(this.ObjectSchema is MapiObjectSchema))
			{
				throw new MapiInvalidOperationException(Strings.ExceptionSchemaInvalidCast(this.ObjectSchema.GetType().ToString()));
			}
			List<PropValue> list = new List<PropValue>(this.ObjectSchema.AllProperties.Count);
			foreach (PropertyDefinition propertyDefinition in this.ObjectSchema.AllProperties)
			{
				MapiPropertyDefinition mapiPropertyDefinition = (MapiPropertyDefinition)propertyDefinition;
				if (mapiPropertyDefinition.PropertyTag != PropTag.Null && !mapiPropertyDefinition.IsCalculated && !mapiPropertyDefinition.IsFilterOnly && !mapiPropertyDefinition.IsReadOnly)
				{
					bool flag = false;
					if (base.ObjectState == ObjectState.New && !this.propertyBag.IsChanged(mapiPropertyDefinition) && (mapiPropertyDefinition.DefaultValue != mapiPropertyDefinition.InitialValue || mapiPropertyDefinition.PersistDefaultValue))
					{
						this.propertyBag[mapiPropertyDefinition] = mapiPropertyDefinition.InitialValue;
					}
					if (((MapiPropertyBag)this.propertyBag).IsChangedOrInitialized(mapiPropertyDefinition))
					{
						object obj = null;
						if (this.propertyBag.TryGetField(mapiPropertyDefinition, ref obj) && (mapiPropertyDefinition.DefaultValue != obj || mapiPropertyDefinition.PersistDefaultValue))
						{
							list.Add(mapiPropertyDefinition.Packer(obj, mapiPropertyDefinition));
							flag = true;
						}
					}
					if (mapiPropertyDefinition.IsMandatory && base.ObjectState == ObjectState.New && !flag)
					{
						throw new DataValidationException(new PropertyValidationError(Strings.ErrorMandatoryPropertyMissing(mapiPropertyDefinition.Name), mapiPropertyDefinition, null));
					}
				}
			}
			this.AdjustPropertyValuesToUpdate(list);
			return list.ToArray();
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000058D8 File Offset: 0x00003AD8
		protected virtual MapiObject.UpdateIdentityFlags UpdateIdentityFlagsForCreating
		{
			get
			{
				return MapiObject.UpdateIdentityFlags.Default;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000058DF File Offset: 0x00003ADF
		protected virtual MapiObject.UpdateIdentityFlags UpdateIdentityFlagsForReading
		{
			get
			{
				return MapiObject.UpdateIdentityFlags.Default;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000058E6 File Offset: 0x00003AE6
		protected virtual MapiObject.UpdateIdentityFlags UpdateIdentityFlagsForFinding
		{
			get
			{
				return MapiObject.UpdateIdentityFlags.Default;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A9 RID: 169
		protected abstract MapiObject.RetrievePropertiesScope RetrievePropertiesScopeForFinding { get; }

		// Token: 0x060000AA RID: 170
		protected abstract void UpdateIdentity(MapiObject.UpdateIdentityFlags flags);

		// Token: 0x060000AB RID: 171 RVA: 0x000058ED File Offset: 0x00003AED
		public MapiObject() : base(new MapiPropertyBag())
		{
			this.disposeTracker = null;
			this.disposed = false;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005908 File Offset: 0x00003B08
		internal MapiObject(MapiObjectId mapiObjectId, MapiSession mapiSession) : base(new MapiPropertyBag())
		{
			this.SetIdentity(mapiObjectId);
			this.MapiSession = mapiSession;
			this.disposeTracker = null;
			this.disposed = false;
		}

		// Token: 0x04000095 RID: 149
		private bool disposed;

		// Token: 0x04000096 RID: 150
		[NonSerialized]
		private MapiSession mapiSession;

		// Token: 0x04000097 RID: 151
		private Fqdn originatingServer;

		// Token: 0x04000098 RID: 152
		[NonSerialized]
		private DisposeTracker disposeTracker;

		// Token: 0x0200001E RID: 30
		[Flags]
		protected enum UpdateIdentityFlags
		{
			// Token: 0x0400009A RID: 154
			Nop = 0,
			// Token: 0x0400009B RID: 155
			EntryIdentity = 1,
			// Token: 0x0400009C RID: 156
			LegacyDistinguishedName = 2,
			// Token: 0x0400009D RID: 157
			FolderPath = 4,
			// Token: 0x0400009E RID: 158
			MailboxGuid = 8,
			// Token: 0x0400009F RID: 159
			All = 255,
			// Token: 0x040000A0 RID: 160
			SkipIfExists = 256,
			// Token: 0x040000A1 RID: 161
			Offline = 512,
			// Token: 0x040000A2 RID: 162
			Default = 1023
		}

		// Token: 0x0200001F RID: 31
		protected enum RetrievePropertiesScope
		{
			// Token: 0x040000A4 RID: 164
			Instance,
			// Token: 0x040000A5 RID: 165
			Hierarchy,
			// Token: 0x040000A6 RID: 166
			Database
		}
	}
}
