using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200064F RID: 1615
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MemoryPropertyBag : PropertyBag, IDictionary<PropertyDefinition, object>, ICollection<KeyValuePair<PropertyDefinition, object>>, IEnumerable<KeyValuePair<PropertyDefinition, object>>, IEnumerable
	{
		// Token: 0x060042FA RID: 17146 RVA: 0x0011CB5B File Offset: 0x0011AD5B
		public MemoryPropertyBag()
		{
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x0011CB70 File Offset: 0x0011AD70
		internal MemoryPropertyBag(MemoryPropertyBag propertyBag) : base(propertyBag)
		{
			this.ExTimeZone = propertyBag.ExTimeZone;
			this.HasAllPropertiesLoaded = propertyBag.HasAllPropertiesLoaded;
			if (propertyBag.propertyValues != null && propertyBag.propertyValues.Count > 0)
			{
				this.propertyValues = new Dictionary<PropertyDefinition, object>(propertyBag.propertyValues);
			}
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x0011CBCE File Offset: 0x0011ADCE
		private void EnsureInternalDataStructuresAllocated(int capacity)
		{
			if (this.propertyValues == null)
			{
				this.propertyValues = new Dictionary<PropertyDefinition, object>(capacity);
			}
		}

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x060042FD RID: 17149 RVA: 0x0011CBE4 File Offset: 0x0011ADE4
		public override bool IsDirty
		{
			get
			{
				return this.changedProperties != null && this.changedProperties.Count != 0;
			}
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x0011CC04 File Offset: 0x0011AE04
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

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x060042FF RID: 17151 RVA: 0x0011CC53 File Offset: 0x0011AE53
		public int Count
		{
			get
			{
				if (this.propertyValues != null)
				{
					return this.propertyValues.Count;
				}
				return 0;
			}
		}

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x06004300 RID: 17152 RVA: 0x0011CC6A File Offset: 0x0011AE6A
		bool ICollection<KeyValuePair<PropertyDefinition, object>>.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x06004301 RID: 17153 RVA: 0x0011CC6D File Offset: 0x0011AE6D
		public ICollection<PropertyDefinition> Keys
		{
			get
			{
				return this.PropertyValues.Keys;
			}
		}

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x06004302 RID: 17154 RVA: 0x0011CC7A File Offset: 0x0011AE7A
		ICollection<object> IDictionary<PropertyDefinition, object>.Values
		{
			get
			{
				return this.PropertyValues.Values;
			}
		}

		// Token: 0x06004303 RID: 17155 RVA: 0x0011CC87 File Offset: 0x0011AE87
		void ICollection<KeyValuePair<PropertyDefinition, object>>.Add(KeyValuePair<PropertyDefinition, object> keyValuePair)
		{
			throw new InvalidOperationException("Readonly ICollection implementation");
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x0011CC93 File Offset: 0x0011AE93
		bool ICollection<KeyValuePair<PropertyDefinition, object>>.Remove(KeyValuePair<PropertyDefinition, object> keyValuePair)
		{
			throw new InvalidOperationException("Readonly ICollection implementation");
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x0011CC9F File Offset: 0x0011AE9F
		bool ICollection<KeyValuePair<PropertyDefinition, object>>.Contains(KeyValuePair<PropertyDefinition, object> keyValuePair)
		{
			return this.PropertyValues.Contains(keyValuePair);
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x0011CCAD File Offset: 0x0011AEAD
		void IDictionary<PropertyDefinition, object>.Add(PropertyDefinition key, object value)
		{
			throw new InvalidOperationException("Readonly IDictionary implementation");
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x0011CCB9 File Offset: 0x0011AEB9
		bool IDictionary<PropertyDefinition, object>.Remove(PropertyDefinition key)
		{
			throw new InvalidOperationException("Readonly IDictionary implementation");
		}

		// Token: 0x06004308 RID: 17160 RVA: 0x0011CCC5 File Offset: 0x0011AEC5
		bool IDictionary<PropertyDefinition, object>.TryGetValue(PropertyDefinition key, out object value)
		{
			return this.PropertyValues.TryGetValue(key, out value);
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x0011CCD4 File Offset: 0x0011AED4
		public bool ContainsKey(PropertyDefinition key)
		{
			return this.PropertyValues.ContainsKey(key);
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x0011CCE2 File Offset: 0x0011AEE2
		IEnumerator<KeyValuePair<PropertyDefinition, object>> IEnumerable<KeyValuePair<PropertyDefinition, object>>.GetEnumerator()
		{
			return this.PropertyValues.GetEnumerator();
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x0011CCEF File Offset: 0x0011AEEF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.PropertyValues.GetEnumerator();
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x0011CCFC File Offset: 0x0011AEFC
		void ICollection<KeyValuePair<PropertyDefinition, object>>.CopyTo(KeyValuePair<PropertyDefinition, object>[] array, int index)
		{
			this.PropertyValues.CopyTo(array, index);
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x0011CD1E File Offset: 0x0011AF1E
		public override void Load(ICollection<PropertyDefinition> properties)
		{
			if (properties == null)
			{
				return;
			}
			this.EnsureInternalDataStructuresAllocated(properties.Count);
			StorePropertyDefinition.PerformActionOnNativePropertyDefinitions<PropertyDefinition>(PropertyDependencyType.AllRead, properties, delegate(NativeStorePropertyDefinition nativeProperty)
			{
				if (!this.IsLoaded(nativeProperty))
				{
					this.MarkAsNotFound(nativeProperty);
				}
			});
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x0011CD44 File Offset: 0x0011AF44
		public void LoadFromStorePropertyBag(IStorePropertyBag storePropertyBag, ICollection<PropertyDefinition> properties)
		{
			ArgumentValidator.ThrowIfNull("storePropertyBag", storePropertyBag);
			ArgumentValidator.ThrowIfNull("properties", properties);
			this.EnsureInternalDataStructuresAllocated(properties.Count);
			foreach (PropertyDefinition propertyDefinition in properties)
			{
				object value = storePropertyBag.TryGetProperty(propertyDefinition);
				this.InternalSetValidatedStoreProperty(propertyDefinition, value);
			}
		}

		// Token: 0x0600430F RID: 17167 RVA: 0x0011CDB8 File Offset: 0x0011AFB8
		protected override void SetValidatedStoreProperty(StorePropertyDefinition propertyDefinition, object propertyValue)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException(ServerStrings.ExNullParameter("propertyDefinition", 1));
			}
			if (propertyValue == null)
			{
				throw new ArgumentNullException(ServerStrings.ExNullParameter("propertyValue", 2));
			}
			this.InternalSetValidatedStoreProperty(propertyDefinition, propertyValue);
		}

		// Token: 0x06004310 RID: 17168 RVA: 0x0011CDF4 File Offset: 0x0011AFF4
		protected override object TryGetStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			object obj;
			if (this.propertyValues == null || !this.propertyValues.TryGetValue(propertyDefinition, out obj))
			{
				if (!this.HasAllPropertiesLoaded)
				{
					throw new NotInBagPropertyErrorException(propertyDefinition);
				}
				return this.MarkAsNotFound(propertyDefinition);
			}
			else
			{
				if (obj is ExDateTime)
				{
					return this.ExTimeZone.ConvertDateTime((ExDateTime)obj);
				}
				return obj;
			}
		}

		// Token: 0x06004311 RID: 17169 RVA: 0x0011CE50 File Offset: 0x0011B050
		protected override void DeleteStoreProperty(StorePropertyDefinition propertyDefinition)
		{
			this.EnsureInternalDataStructuresAllocated(8);
			if (!this.DeletedProperties.Contains(propertyDefinition))
			{
				this.DeletedProperties.Add(propertyDefinition);
			}
			this.ChangedProperties.TryAdd(propertyDefinition);
			this.AddTrackingInformation(propertyDefinition, PropertyTrackingInformation.Deleted, null);
			this.propertyValues[propertyDefinition] = new PropertyError(propertyDefinition, PropertyErrorCode.NotFound);
		}

		// Token: 0x06004312 RID: 17170 RVA: 0x0011CEA7 File Offset: 0x0011B0A7
		protected override bool InternalIsPropertyDirty(AtomicStorePropertyDefinition propertyDefinition)
		{
			return this.changedProperties != null && this.changedProperties.Contains(propertyDefinition);
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x06004313 RID: 17171 RVA: 0x0011CEBF File Offset: 0x0011B0BF
		internal ICollection<NativeStorePropertyDefinition> AllNativeProperties
		{
			get
			{
				if (this.propertyValues == null || this.propertyValues.Count == 0)
				{
					return MemoryPropertyBag.emptyNativeList;
				}
				return new MemoryPropertyBag.NativeFilter(this.propertyValues, new Action(this.MarkAsReadOnly), new Action(this.UnmarkAsReadOnly));
			}
		}

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x06004314 RID: 17172 RVA: 0x0011CEFF File Offset: 0x0011B0FF
		internal ICollection<PropertyDefinition> DeleteList
		{
			get
			{
				if (this.deletedProperties == null)
				{
					return MemoryPropertyBag.emptyList;
				}
				return new ReadOnlyCollection<PropertyDefinition>(this.deletedProperties);
			}
		}

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x06004315 RID: 17173 RVA: 0x0011CF1A File Offset: 0x0011B11A
		internal ICollection<PropertyDefinition> ChangeList
		{
			get
			{
				return this.changedProperties ?? MemoryPropertyBag.emptyList;
			}
		}

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x06004316 RID: 17174 RVA: 0x0011CF2B File Offset: 0x0011B12B
		private IDictionary<PropertyDefinition, object> PropertyValues
		{
			get
			{
				this.EnsureInternalDataStructuresAllocated(8);
				return this.propertyValues;
			}
		}

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x06004317 RID: 17175 RVA: 0x0011CF3A File Offset: 0x0011B13A
		// (set) Token: 0x06004318 RID: 17176 RVA: 0x0011CF42 File Offset: 0x0011B142
		internal override ExTimeZone ExTimeZone
		{
			get
			{
				return this.exTimeZone;
			}
			set
			{
				this.exTimeZone = value;
			}
		}

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x06004319 RID: 17177 RVA: 0x0011CF4B File Offset: 0x0011B14B
		public ICollection<PropertyDefinition> AllFoundProperties
		{
			get
			{
				if (this.propertyValues == null || this.propertyValues.Count == 0)
				{
					return MemoryPropertyBag.emptyList;
				}
				return new MemoryPropertyBag.FoundFilter(this.propertyValues, new Action(this.MarkAsReadOnly), new Action(this.UnmarkAsReadOnly));
			}
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x0011CF8C File Offset: 0x0011B18C
		public void PreLoadStoreProperty<T>(ICollection<T> properties, object[] values) where T : StorePropertyDefinition
		{
			Util.ThrowOnNullArgument(properties, "properties");
			Util.ThrowOnNullArgument(values, "values");
			if (properties.Count != values.Length)
			{
				throw new ArgumentException("properties and values have mismatched elements");
			}
			ICollection<PropValue> collection = new ComputedElementCollection<T, object, PropValue>(new Func<T, object, PropValue>(PropValue.CreatePropValue<T>), properties, values, values.Length);
			this.PreLoadStoreProperties(collection);
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x0011CFE4 File Offset: 0x0011B1E4
		public void PreLoadStoreProperties(ICollection<PropValue> propertyValues)
		{
			Util.ThrowOnNullArgument(propertyValues, "propertyValues");
			this.EnsureInternalDataStructuresAllocated(propertyValues.Count);
			foreach (PropValue propValue in propertyValues)
			{
				if (!this.IsDirty || !this.ChangedProperties.Contains(propValue.Property))
				{
					this.PropertyValues[propValue.Property] = propValue.Value;
				}
			}
		}

		// Token: 0x0600431C RID: 17180 RVA: 0x0011D074 File Offset: 0x0011B274
		internal void PreLoadStoreProperty(PropertyDefinition prop, object propertyValue, int approxCount)
		{
			this.EnsureInternalDataStructuresAllocated(approxCount);
			if (!this.IsDirty || !this.ChangedProperties.Contains(prop))
			{
				this.PropertyValues[prop] = propertyValue;
			}
		}

		// Token: 0x0600431D RID: 17181 RVA: 0x0011D0A0 File Offset: 0x0011B2A0
		protected override bool IsLoaded(NativeStorePropertyDefinition propertyDefinition)
		{
			return this.CheckIsLoaded(propertyDefinition, false);
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x0011D0AA File Offset: 0x0011B2AA
		internal bool CheckIsLoaded(PropertyDefinition propertyDefinition, bool allowCalculatedProperty)
		{
			return this.propertyValues != null && this.propertyValues.ContainsKey(propertyDefinition);
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x0011D0C4 File Offset: 0x0011B2C4
		public void Clear()
		{
			if (this.propertyValues != null)
			{
				this.propertyValues.Clear();
			}
			this.HasAllPropertiesLoaded = false;
			this.ClearChangeInfo();
		}

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x06004320 RID: 17184 RVA: 0x0011D0E6 File Offset: 0x0011B2E6
		// (set) Token: 0x06004321 RID: 17185 RVA: 0x0011D0EE File Offset: 0x0011B2EE
		internal bool HasAllPropertiesLoaded
		{
			get
			{
				return this.hasLoadedAllPossibleProperties;
			}
			private set
			{
				this.hasLoadedAllPossibleProperties = value;
			}
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x0011D0F7 File Offset: 0x0011B2F7
		public void SetAllPropertiesLoaded()
		{
			this.HasAllPropertiesLoaded = true;
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x0011D100 File Offset: 0x0011B300
		public void ClearChangeInfo()
		{
			this.changedProperties = null;
			this.deletedProperties = null;
			this.trackedPropertyInformation = null;
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x0011D118 File Offset: 0x0011B318
		internal void ClearChangeInfo(PropertyDefinition propertyDefinition)
		{
			if (this.changedProperties != null)
			{
				this.ChangedProperties.Remove(propertyDefinition);
			}
			if (this.deletedProperties != null)
			{
				this.DeletedProperties.Remove(propertyDefinition);
			}
			if (this.trackedPropertyInformation != null)
			{
				this.TrackedPropertyInformation.Remove(propertyDefinition);
			}
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x0011D164 File Offset: 0x0011B364
		internal void MarkPropertyAsRequireStreamed(PropertyDefinition propertyDefinition)
		{
			this.InternalSetValidatedStoreProperty(propertyDefinition, new PropertyError(propertyDefinition, PropertyErrorCode.RequireStreamed));
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x0011D175 File Offset: 0x0011B375
		internal void Unload(PropertyDefinition propertyDefinition)
		{
			this.ClearChangeInfo(propertyDefinition);
			if (this.propertyValues != null)
			{
				this.PropertyValues.Remove(propertyDefinition);
			}
		}

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x06004327 RID: 17191 RVA: 0x0011D194 File Offset: 0x0011B394
		private HashSet<PropertyDefinition> ChangedProperties
		{
			get
			{
				if (this.changedProperties == null)
				{
					int num = (this.propertyValues != null) ? this.propertyValues.Count : 8;
					this.changedProperties = new HashSet<PropertyDefinition>(num >> 1);
				}
				return this.changedProperties;
			}
		}

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x06004328 RID: 17192 RVA: 0x0011D1D4 File Offset: 0x0011B3D4
		private List<PropertyDefinition> DeletedProperties
		{
			get
			{
				if (this.deletedProperties == null)
				{
					this.deletedProperties = new List<PropertyDefinition>();
				}
				return this.deletedProperties;
			}
		}

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x06004329 RID: 17193 RVA: 0x0011D1EF File Offset: 0x0011B3EF
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

		// Token: 0x0600432A RID: 17194 RVA: 0x0011D20C File Offset: 0x0011B40C
		private PropertyError MarkAsNotFound(StorePropertyDefinition propertyDefinition)
		{
			PropertyError propertyError = new PropertyError(propertyDefinition, PropertyErrorCode.NotFound);
			this.PropertyValues[propertyDefinition] = propertyError;
			return propertyError;
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x0011D230 File Offset: 0x0011B430
		private void AddTrackingInformation(StorePropertyDefinition propertyDefinition, PropertyTrackingInformation changeType, object originalValue)
		{
			if ((propertyDefinition.PropertyFlags & PropertyFlags.TrackChange) == PropertyFlags.TrackChange && !this.TrackedPropertyInformation.ContainsKey(propertyDefinition))
			{
				PropertyValueTrackingData value = new PropertyValueTrackingData(changeType, originalValue);
				this.TrackedPropertyInformation.Add(propertyDefinition, value);
			}
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x0011D270 File Offset: 0x0011B470
		private void InternalSetValidatedStoreProperty(PropertyDefinition propertyDefinition, object value)
		{
			this.EnsureInternalDataStructuresAllocated(8);
			Array array = value as Array;
			if (array != null)
			{
				value = MemoryPropertyBag.ClonePropertyValue<Array>(array);
			}
			else if (value is DateTime)
			{
				ExTimeZoneHelperForMigrationOnly.CheckValidationLevel(false, ExTimeZoneHelperForMigrationOnly.ValidationLevel.Low, "MemoryPropertyBag.InternalSetValidatedStoreProperty: System.DateTime", new object[0]);
				value = new ExDateTime(this.ExTimeZone, (DateTime)value);
			}
			else if (value is ExDateTime)
			{
				((ExDateTime)value).CheckExpectedTimeZone(this.ExTimeZone, ExTimeZoneHelperForMigrationOnly.ValidationLevel.High);
				value = this.ExTimeZone.ConvertDateTime((ExDateTime)value);
			}
			object originalValue = null;
			StorePropertyDefinition storePropertyDefinition = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			bool flag = (storePropertyDefinition.PropertyFlags & PropertyFlags.TrackChange) == PropertyFlags.TrackChange;
			if (!(value is PropertyError) && flag && this.propertyValues.ContainsKey(propertyDefinition))
			{
				originalValue = this.propertyValues[propertyDefinition];
			}
			this.propertyValues[propertyDefinition] = value;
			if (this.deletedProperties != null)
			{
				this.deletedProperties.Remove(propertyDefinition);
			}
			this.ChangedProperties.TryAdd(propertyDefinition);
			this.AddTrackingInformation(storePropertyDefinition, PropertyTrackingInformation.Modified, originalValue);
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x0011D37C File Offset: 0x0011B57C
		private void MarkAsReadOnly()
		{
			this.activeEnumeratorCount++;
			if (!(this.propertyValues is ReadOnlyDictionary<PropertyDefinition, object>))
			{
				this.propertyValues = new ReadOnlyDictionary<PropertyDefinition, object>(this.propertyValues);
			}
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x0011D3B8 File Offset: 0x0011B5B8
		private void UnmarkAsReadOnly()
		{
			if (this.activeEnumeratorCount > 0)
			{
				this.activeEnumeratorCount--;
			}
			ReadOnlyDictionary<PropertyDefinition, object> readOnlyDictionary = this.propertyValues as ReadOnlyDictionary<PropertyDefinition, object>;
			if (readOnlyDictionary != null && this.activeEnumeratorCount == 0)
			{
				this.propertyValues = readOnlyDictionary.WrappedDictionary;
			}
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x0011D3FF File Offset: 0x0011B5FF
		private static object ClonePropertyValue<T>(T propertyValue) where T : ICloneable
		{
			if (propertyValue == null)
			{
				return propertyValue;
			}
			return propertyValue.Clone();
		}

		// Token: 0x040024A1 RID: 9377
		private static readonly NativeStorePropertyDefinition[] emptyNativeList = Array<NativeStorePropertyDefinition>.Empty;

		// Token: 0x040024A2 RID: 9378
		private static readonly ICollection<PropertyDefinition> emptyList = new ReadOnlyCollection<PropertyDefinition>(Array<PropertyDefinition>.Empty);

		// Token: 0x040024A3 RID: 9379
		private bool hasLoadedAllPossibleProperties;

		// Token: 0x040024A4 RID: 9380
		private IDictionary<PropertyDefinition, object> propertyValues;

		// Token: 0x040024A5 RID: 9381
		private int activeEnumeratorCount;

		// Token: 0x040024A6 RID: 9382
		private List<PropertyDefinition> deletedProperties;

		// Token: 0x040024A7 RID: 9383
		private HashSet<PropertyDefinition> changedProperties;

		// Token: 0x040024A8 RID: 9384
		private Dictionary<PropertyDefinition, PropertyValueTrackingData> trackedPropertyInformation;

		// Token: 0x040024A9 RID: 9385
		private ExTimeZone exTimeZone = ExTimeZone.UtcTimeZone;

		// Token: 0x02000650 RID: 1616
		private class NativeFilter : ICollection<NativeStorePropertyDefinition>, IEnumerable<NativeStorePropertyDefinition>, IEnumerable
		{
			// Token: 0x06004332 RID: 17202 RVA: 0x0011D438 File Offset: 0x0011B638
			public NativeFilter(IDictionary<PropertyDefinition, object> properties, Action onEnumeratorCreate, Action onEnumeratorDispose)
			{
				ArgumentValidator.ThrowIfNull("properties", properties);
				ArgumentValidator.ThrowIfNull("onEnumeratorCreate", onEnumeratorCreate);
				ArgumentValidator.ThrowIfNull("onEnumeratorDispose", onEnumeratorDispose);
				this.properties = properties;
				this.onEnumeratorCreate = onEnumeratorCreate;
				this.onEnumeratorDispose = onEnumeratorDispose;
			}

			// Token: 0x170013C0 RID: 5056
			// (get) Token: 0x06004333 RID: 17203 RVA: 0x0011D488 File Offset: 0x0011B688
			public int Count
			{
				get
				{
					if (this.count == -1)
					{
						this.count = 0;
						foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in this)
						{
							this.count++;
						}
					}
					return this.count;
				}
			}

			// Token: 0x170013C1 RID: 5057
			// (get) Token: 0x06004334 RID: 17204 RVA: 0x0011D4F0 File Offset: 0x0011B6F0
			bool ICollection<NativeStorePropertyDefinition>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06004335 RID: 17205 RVA: 0x0011D4F3 File Offset: 0x0011B6F3
			public void Add(NativeStorePropertyDefinition item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06004336 RID: 17206 RVA: 0x0011D4FA File Offset: 0x0011B6FA
			public void Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06004337 RID: 17207 RVA: 0x0011D501 File Offset: 0x0011B701
			public bool Remove(NativeStorePropertyDefinition item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06004338 RID: 17208 RVA: 0x0011D508 File Offset: 0x0011B708
			public bool Contains(NativeStorePropertyDefinition item)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				return this.properties.ContainsKey(item);
			}

			// Token: 0x06004339 RID: 17209 RVA: 0x0011D524 File Offset: 0x0011B724
			public void CopyTo(NativeStorePropertyDefinition[] array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0 || index > array.Length - this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				foreach (KeyValuePair<PropertyDefinition, object> keyValuePair in this.properties)
				{
					NativeStorePropertyDefinition nativeStorePropertyDefinition = keyValuePair.Key as NativeStorePropertyDefinition;
					if (nativeStorePropertyDefinition != null)
					{
						array[index++] = nativeStorePropertyDefinition;
					}
				}
			}

			// Token: 0x0600433A RID: 17210 RVA: 0x0011D5B0 File Offset: 0x0011B7B0
			public MemoryPropertyBag.NativeFilter.Enumerator GetEnumerator()
			{
				MemoryPropertyBag.NativeFilter.Enumerator result = new MemoryPropertyBag.NativeFilter.Enumerator(this.properties.GetEnumerator(), this.onEnumeratorDispose);
				this.onEnumeratorCreate();
				return result;
			}

			// Token: 0x0600433B RID: 17211 RVA: 0x0011D5E0 File Offset: 0x0011B7E0
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600433C RID: 17212 RVA: 0x0011D5E8 File Offset: 0x0011B7E8
			IEnumerator<NativeStorePropertyDefinition> IEnumerable<NativeStorePropertyDefinition>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040024AA RID: 9386
			private readonly IDictionary<PropertyDefinition, object> properties;

			// Token: 0x040024AB RID: 9387
			private readonly Action onEnumeratorCreate;

			// Token: 0x040024AC RID: 9388
			private readonly Action onEnumeratorDispose;

			// Token: 0x040024AD RID: 9389
			private int count = -1;

			// Token: 0x02000651 RID: 1617
			[Serializable]
			public class Enumerator : DisposableObject, IEnumerator<NativeStorePropertyDefinition>, IDisposable, IEnumerator
			{
				// Token: 0x0600433D RID: 17213 RVA: 0x0011D5F0 File Offset: 0x0011B7F0
				internal Enumerator(IEnumerator<KeyValuePair<PropertyDefinition, object>> parent, Action onDispose)
				{
					this.parentDictionaryEnumerator = parent;
					this.currentItem = null;
					this.onDispose = onDispose;
				}

				// Token: 0x170013C2 RID: 5058
				// (get) Token: 0x0600433E RID: 17214 RVA: 0x0011D60D File Offset: 0x0011B80D
				public NativeStorePropertyDefinition Current
				{
					get
					{
						return this.currentItem;
					}
				}

				// Token: 0x170013C3 RID: 5059
				// (get) Token: 0x0600433F RID: 17215 RVA: 0x0011D615 File Offset: 0x0011B815
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06004340 RID: 17216 RVA: 0x0011D620 File Offset: 0x0011B820
				public bool MoveNext()
				{
					while (this.parentDictionaryEnumerator.MoveNext())
					{
						KeyValuePair<PropertyDefinition, object> keyValuePair = this.parentDictionaryEnumerator.Current;
						this.currentItem = (keyValuePair.Key as NativeStorePropertyDefinition);
						if (this.currentItem != null)
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x06004341 RID: 17217 RVA: 0x0011D665 File Offset: 0x0011B865
				protected override void InternalDispose(bool disposing)
				{
					if (disposing)
					{
						this.onDispose();
					}
					base.InternalDispose(disposing);
				}

				// Token: 0x06004342 RID: 17218 RVA: 0x0011D67C File Offset: 0x0011B87C
				protected override DisposeTracker GetDisposeTracker()
				{
					return DisposeTracker.Get<MemoryPropertyBag.NativeFilter.Enumerator>(this);
				}

				// Token: 0x06004343 RID: 17219 RVA: 0x0011D684 File Offset: 0x0011B884
				void IEnumerator.Reset()
				{
					this.parentDictionaryEnumerator.Reset();
					this.currentItem = null;
				}

				// Token: 0x040024AE RID: 9390
				private readonly IEnumerator<KeyValuePair<PropertyDefinition, object>> parentDictionaryEnumerator;

				// Token: 0x040024AF RID: 9391
				private readonly Action onDispose;

				// Token: 0x040024B0 RID: 9392
				private NativeStorePropertyDefinition currentItem;
			}
		}

		// Token: 0x02000652 RID: 1618
		private class FoundFilter : ICollection<PropertyDefinition>, IEnumerable<PropertyDefinition>, IEnumerable
		{
			// Token: 0x06004344 RID: 17220 RVA: 0x0011D698 File Offset: 0x0011B898
			public FoundFilter(IDictionary<PropertyDefinition, object> properties, Action onEnumeratorCreate, Action onEnumeratorDispose)
			{
				ArgumentValidator.ThrowIfNull("properties", properties);
				ArgumentValidator.ThrowIfNull("onEnumeratorCreate", onEnumeratorCreate);
				ArgumentValidator.ThrowIfNull("onEnumeratorDispose", onEnumeratorDispose);
				this.properties = properties;
				this.onEnumeratorCreate = onEnumeratorCreate;
				this.onEnumeratorDispose = onEnumeratorDispose;
			}

			// Token: 0x170013C4 RID: 5060
			// (get) Token: 0x06004345 RID: 17221 RVA: 0x0011D6E8 File Offset: 0x0011B8E8
			public int Count
			{
				get
				{
					if (this.count == -1)
					{
						this.count = 0;
						foreach (PropertyDefinition propertyDefinition in this)
						{
							this.count++;
						}
					}
					return this.count;
				}
			}

			// Token: 0x170013C5 RID: 5061
			// (get) Token: 0x06004346 RID: 17222 RVA: 0x0011D750 File Offset: 0x0011B950
			bool ICollection<PropertyDefinition>.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06004347 RID: 17223 RVA: 0x0011D753 File Offset: 0x0011B953
			public void Add(PropertyDefinition item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06004348 RID: 17224 RVA: 0x0011D75A File Offset: 0x0011B95A
			public void Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06004349 RID: 17225 RVA: 0x0011D761 File Offset: 0x0011B961
			public bool Remove(PropertyDefinition item)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600434A RID: 17226 RVA: 0x0011D768 File Offset: 0x0011B968
			public bool Contains(PropertyDefinition item)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				return this.properties.ContainsKey(item);
			}

			// Token: 0x0600434B RID: 17227 RVA: 0x0011D784 File Offset: 0x0011B984
			public void CopyTo(PropertyDefinition[] array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0 || index > array.Length - this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				foreach (PropertyDefinition propertyDefinition in this)
				{
					array[index++] = propertyDefinition;
				}
			}

			// Token: 0x0600434C RID: 17228 RVA: 0x0011D7F8 File Offset: 0x0011B9F8
			public MemoryPropertyBag.FoundFilter.Enumerator GetEnumerator()
			{
				MemoryPropertyBag.FoundFilter.Enumerator result = new MemoryPropertyBag.FoundFilter.Enumerator(this.properties.GetEnumerator(), this.onEnumeratorDispose);
				this.onEnumeratorCreate();
				return result;
			}

			// Token: 0x0600434D RID: 17229 RVA: 0x0011D828 File Offset: 0x0011BA28
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600434E RID: 17230 RVA: 0x0011D830 File Offset: 0x0011BA30
			IEnumerator<PropertyDefinition> IEnumerable<PropertyDefinition>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040024B1 RID: 9393
			private readonly IDictionary<PropertyDefinition, object> properties;

			// Token: 0x040024B2 RID: 9394
			private readonly Action onEnumeratorCreate;

			// Token: 0x040024B3 RID: 9395
			private readonly Action onEnumeratorDispose;

			// Token: 0x040024B4 RID: 9396
			private int count = -1;

			// Token: 0x02000653 RID: 1619
			[Serializable]
			public class Enumerator : DisposableObject, IEnumerator<PropertyDefinition>, IDisposable, IEnumerator
			{
				// Token: 0x0600434F RID: 17231 RVA: 0x0011D838 File Offset: 0x0011BA38
				internal Enumerator(IEnumerator<KeyValuePair<PropertyDefinition, object>> parent, Action onDispose)
				{
					this.parentDictionaryEnumerator = parent;
					this.currentItem = null;
					this.onDispose = onDispose;
				}

				// Token: 0x170013C6 RID: 5062
				// (get) Token: 0x06004350 RID: 17232 RVA: 0x0011D855 File Offset: 0x0011BA55
				public PropertyDefinition Current
				{
					get
					{
						return this.currentItem;
					}
				}

				// Token: 0x170013C7 RID: 5063
				// (get) Token: 0x06004351 RID: 17233 RVA: 0x0011D85D File Offset: 0x0011BA5D
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06004352 RID: 17234 RVA: 0x0011D868 File Offset: 0x0011BA68
				public bool MoveNext()
				{
					while (this.parentDictionaryEnumerator.MoveNext())
					{
						KeyValuePair<PropertyDefinition, object> keyValuePair = this.parentDictionaryEnumerator.Current;
						if (!PropertyError.IsPropertyNotFound(keyValuePair.Value))
						{
							KeyValuePair<PropertyDefinition, object> keyValuePair2 = this.parentDictionaryEnumerator.Current;
							this.currentItem = keyValuePair2.Key;
							return true;
						}
					}
					return false;
				}

				// Token: 0x06004353 RID: 17235 RVA: 0x0011D8BA File Offset: 0x0011BABA
				protected override void InternalDispose(bool disposing)
				{
					if (disposing)
					{
						this.onDispose();
					}
					base.InternalDispose(disposing);
				}

				// Token: 0x06004354 RID: 17236 RVA: 0x0011D8D1 File Offset: 0x0011BAD1
				protected override DisposeTracker GetDisposeTracker()
				{
					return DisposeTracker.Get<MemoryPropertyBag.FoundFilter.Enumerator>(this);
				}

				// Token: 0x06004355 RID: 17237 RVA: 0x0011D8D9 File Offset: 0x0011BAD9
				void IEnumerator.Reset()
				{
					this.parentDictionaryEnumerator.Reset();
					this.currentItem = null;
				}

				// Token: 0x040024B5 RID: 9397
				private readonly IEnumerator<KeyValuePair<PropertyDefinition, object>> parentDictionaryEnumerator;

				// Token: 0x040024B6 RID: 9398
				private readonly Action onDispose;

				// Token: 0x040024B7 RID: 9399
				private PropertyDefinition currentItem;
			}
		}
	}
}
