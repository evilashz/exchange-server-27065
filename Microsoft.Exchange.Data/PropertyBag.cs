using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Data;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200020E RID: 526
	[Serializable]
	internal abstract class PropertyBag : ICollection, IEnumerable, IPropertyBag, IReadOnlyPropertyBag, ICloneable
	{
		// Token: 0x0600124D RID: 4685 RVA: 0x0003766D File Offset: 0x0003586D
		public PropertyBag(bool readOnly, int initialSize)
		{
			ProviderPropertyDefinition objectVersionPropertyDefinition = this.ObjectVersionPropertyDefinition;
			this.readOnly = readOnly;
			this.storeValuesOnly = false;
			this.currentValues = new Dictionary<ProviderPropertyDefinition, object>(initialSize);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x000376A8 File Offset: 0x000358A8
		public PropertyBag() : this(false, 16)
		{
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x000376B3 File Offset: 0x000358B3
		internal void SetUpdateErrorsCallback(UpdateErrorsDelegate callback)
		{
			this.updateErrors = callback;
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x000376BC File Offset: 0x000358BC
		public int Count
		{
			get
			{
				return this.currentValues.Count;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x000376C9 File Offset: 0x000358C9
		public bool IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x000376D4 File Offset: 0x000358D4
		public bool Changed
		{
			get
			{
				if (this.IsReadOnly || this.originalValues == null || this.OriginalValues.Count == 0)
				{
					return false;
				}
				foreach (ProviderPropertyDefinition key in this.OriginalValues.Keys)
				{
					if (this.IsChanged(key))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x00037754 File Offset: 0x00035954
		public ICollection Keys
		{
			get
			{
				return this.currentValues.Keys;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x00037761 File Offset: 0x00035961
		public ICollection Values
		{
			get
			{
				return this.currentValues.Values;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x0003776E File Offset: 0x0003596E
		private Dictionary<ProviderPropertyDefinition, object> OriginalValues
		{
			get
			{
				if (this.originalValues == null)
				{
					this.originalValues = new Dictionary<ProviderPropertyDefinition, object>(Math.Min(this.currentValues.Count / 2, 4));
				}
				return this.originalValues;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x0003779C File Offset: 0x0003599C
		internal virtual bool ProcessCalculatedProperies
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001257 RID: 4695
		internal abstract ProviderPropertyDefinition ObjectVersionPropertyDefinition { get; }

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001258 RID: 4696
		internal abstract ProviderPropertyDefinition ObjectStatePropertyDefinition { get; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001259 RID: 4697
		internal abstract ProviderPropertyDefinition ObjectIdentityPropertyDefinition { get; }

		// Token: 0x0600125A RID: 4698 RVA: 0x0003779F File Offset: 0x0003599F
		internal virtual MultiValuedPropertyBase CreateMultiValuedProperty(ProviderPropertyDefinition propertyDefinition, bool createAsReadOnly, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage)
		{
			return ValueConvertor.CreateGenericMultiValuedProperty(propertyDefinition, createAsReadOnly, values, invalidValues, readOnlyErrorMessage);
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x000377AD File Offset: 0x000359AD
		internal bool SaveCalculatedValues
		{
			get
			{
				return this.storeValuesOnly;
			}
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x000377B5 File Offset: 0x000359B5
		internal void EnableSaveCalculatedValues()
		{
			this.storeValuesOnly = true;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x000377C0 File Offset: 0x000359C0
		public object Clone()
		{
			PropertyBag propertyBag = (PropertyBag)CloneHelper.SerializeObj(this);
			List<PropertyDefinition> list = new List<PropertyDefinition>(this.Keys.Count);
			foreach (object obj in this.Keys)
			{
				PropertyDefinition item = (PropertyDefinition)obj;
				list.Add(item);
			}
			propertyBag.SetConfigObjectSchema(list);
			return propertyBag;
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00037844 File Offset: 0x00035A44
		internal bool IsReadOnlyProperty(ProviderPropertyDefinition propertyDefinition)
		{
			LocalizedString? localizedString;
			return this.IsReadOnlyProperty(propertyDefinition, out localizedString);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0003785C File Offset: 0x00035A5C
		private bool IsReadOnlyProperty(ProviderPropertyDefinition propertyDefinition, out LocalizedString? reason)
		{
			reason = null;
			if (this.IsReadOnly && !propertyDefinition.IsTaskPopulated)
			{
				reason = new LocalizedString?(DataStrings.ErrorReadOnlyCauseObject(propertyDefinition.Name));
				return true;
			}
			if (propertyDefinition.IsReadOnly)
			{
				reason = new LocalizedString?(DataStrings.ErrorReadOnlyCauseProperty(propertyDefinition.Name));
				return true;
			}
			if (this.ObjectVersionPropertyDefinition != null)
			{
				if (object.Equals(propertyDefinition, this.ObjectVersionPropertyDefinition))
				{
					reason = new LocalizedString?(DataStrings.ErrorObjectVersionReadOnly(propertyDefinition.Name));
					return true;
				}
				ExchangeObjectVersion exchangeObjectVersion = (ExchangeObjectVersion)this[this.ObjectVersionPropertyDefinition];
				ExchangeObjectVersion versionAdded = propertyDefinition.VersionAdded;
				if (exchangeObjectVersion.IsOlderThan(versionAdded))
				{
					reason = new LocalizedString?(DataStrings.ExceptionPropertyTooNew(propertyDefinition.Name, versionAdded, exchangeObjectVersion));
					return true;
				}
			}
			if (this.ObjectStatePropertyDefinition != null && propertyDefinition.IsWriteOnce && (ObjectState)this[this.ObjectStatePropertyDefinition] != ObjectState.New)
			{
				reason = new LocalizedString?(DataStrings.ExceptionWriteOnceProperty(propertyDefinition.Name));
				return true;
			}
			return false;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0003795E File Offset: 0x00035B5E
		private bool IsCalculatedProperty(ProviderPropertyDefinition propertyDefinition)
		{
			return propertyDefinition.IsCalculated && !this.storeValuesOnly;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00037973 File Offset: 0x00035B73
		internal bool TryGetField(ProviderPropertyDefinition key, ref object value)
		{
			return this.currentValues.TryGetValue(key, out value);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00037984 File Offset: 0x00035B84
		internal void DangerousSetValue(ProviderPropertyDefinition key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IList<ValidationError> list = key.ValidateProperty(value, this, false);
			if (list.Count > 0)
			{
				throw new DataValidationException(list[0]);
			}
			this.SetField(key, value);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x000379C8 File Offset: 0x00035BC8
		internal object SetField(ProviderPropertyDefinition key, object value)
		{
			ExTraceGlobals.PropertyBagTracer.TraceDebug<ProviderPropertyDefinition, object>((long)this.GetHashCode(), "SetField({0}, {1})", key, value ?? "<NULL>");
			if (key.IsMultivalued)
			{
				MultiValuedPropertyBase multiValuedPropertyBase = (MultiValuedPropertyBase)value;
				IntRange valueRange = (multiValuedPropertyBase != null) ? multiValuedPropertyBase.ValueRange : null;
				bool isCompletelyRead = multiValuedPropertyBase == null || multiValuedPropertyBase.IsCompletelyRead;
				LocalizedString? readOnlyErrorMessage;
				bool createAsReadOnly = this.IsReadOnlyProperty(key, out readOnlyErrorMessage);
				multiValuedPropertyBase = this.CreateMultiValuedProperty(key, createAsReadOnly, multiValuedPropertyBase ?? PropertyBag.EmptyArray, null, readOnlyErrorMessage);
				multiValuedPropertyBase.IsCompletelyRead = isCompletelyRead;
				multiValuedPropertyBase.ValueRange = valueRange;
				value = multiValuedPropertyBase;
				object obj = null;
				this.TryGetField(key, ref obj);
				MultiValuedPropertyBase multiValuedPropertyBase2 = (MultiValuedPropertyBase)obj;
				if (multiValuedPropertyBase2 != null)
				{
					multiValuedPropertyBase2.CollectionChanging -= this.MvpChangingHandler;
					multiValuedPropertyBase2.CollectionChanged -= this.MvpChangedHandler;
				}
				multiValuedPropertyBase.CollectionChanging += this.MvpChangingHandler;
				multiValuedPropertyBase.CollectionChanged += this.MvpChangedHandler;
			}
			else if (!this.IsReadOnly)
			{
				object obj2 = null;
				if (!this.OriginalValues.ContainsKey(key))
				{
					this.OriginalValues[key] = (this.TryGetField(key, ref obj2) ? obj2 : null);
				}
			}
			this.currentValues[key] = value;
			return value;
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x00037AE5 File Offset: 0x00035CE5
		private EventHandler MvpChangingHandler
		{
			get
			{
				if (this.mvpChangingHandler == null)
				{
					this.mvpChangingHandler = new EventHandler(this.MvpChanging);
				}
				return this.mvpChangingHandler;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x00037B07 File Offset: 0x00035D07
		private EventHandler MvpChangedHandler
		{
			get
			{
				if (this.mvpChangedHandler == null)
				{
					this.mvpChangedHandler = new EventHandler(this.MvpChanged);
				}
				return this.mvpChangedHandler;
			}
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00037B2C File Offset: 0x00035D2C
		private void MvpChanging(object sender, EventArgs e)
		{
			MultiValuedPropertyBase multiValuedPropertyBase = sender as MultiValuedPropertyBase;
			ProviderPropertyDefinition propertyDefinition = multiValuedPropertyBase.PropertyDefinition;
			object obj = null;
			if (this.TryGetField(propertyDefinition, ref obj) && obj == multiValuedPropertyBase && !this.readOnly && !this.OriginalValues.ContainsKey(propertyDefinition))
			{
				ExTraceGlobals.PropertyBagTracer.TraceDebug<ProviderPropertyDefinition>((long)this.GetHashCode(), "'{0}' MVP is about to change for the first time. Backing up original value.", propertyDefinition);
				this.OriginalValues[propertyDefinition] = this.CreateMultiValuedProperty(propertyDefinition, true, multiValuedPropertyBase, null, new LocalizedString?(DataStrings.ErrorOrignalMultiValuedProperty(propertyDefinition.Name)));
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00037BAC File Offset: 0x00035DAC
		private void MvpChanged(object sender, EventArgs e)
		{
			MultiValuedPropertyBase multiValuedPropertyBase = sender as MultiValuedPropertyBase;
			ProviderPropertyDefinition propertyDefinition = multiValuedPropertyBase.PropertyDefinition;
			object obj = null;
			if (this.TryGetField(propertyDefinition, ref obj) && obj == multiValuedPropertyBase)
			{
				ExTraceGlobals.PropertyBagTracer.TraceDebug<ProviderPropertyDefinition>((long)this.GetHashCode(), "{0} MVP has changed. Updating any dependent properties.", propertyDefinition);
				this.UpdatePropertyDependents(propertyDefinition, multiValuedPropertyBase);
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00037BF8 File Offset: 0x00035DF8
		private void UpdatePropertyDependents(ProviderPropertyDefinition propertyDefinition, object value)
		{
			if (!this.ProcessCalculatedProperies)
			{
				ExTraceGlobals.PropertyBagTracer.TraceDebug<ProviderPropertyDefinition, object>((long)this.GetHashCode(), "Skip Calcualted Properties logic; Property = {0}; Value = {1};", propertyDefinition, value ?? "<NULL>");
				if (!propertyDefinition.IsMultivalued)
				{
					this.SetField(propertyDefinition, value);
				}
				return;
			}
			try
			{
				ExTraceGlobals.PropertyBagTracer.TraceDebug((long)this.GetHashCode(), "Recursion Depth = {0}; Property = {1}; Value = {2}; ShouldCallSetters = {3};", new object[]
				{
					this.recursionDepth,
					propertyDefinition,
					value ?? "<NULL>",
					this.shouldCallSetters
				});
				this.recursionDepth++;
				if (this.shouldCallSetters)
				{
					if (this.IsCalculatedProperty(propertyDefinition) && propertyDefinition.SetterDelegate != null)
					{
						propertyDefinition.SetterDelegate(value, this);
					}
					if (1 == this.recursionDepth)
					{
						this.shouldCallSetters = false;
					}
				}
				if (!propertyDefinition.IsMultivalued)
				{
					this.SetField(propertyDefinition, value);
				}
				if (!this.shouldCallSetters && 1 == this.recursionDepth)
				{
					if (this.IsCalculatedProperty(propertyDefinition))
					{
						HashSet<ProviderPropertyDefinition> hashSet = new HashSet<ProviderPropertyDefinition>();
						hashSet.TryAdd(propertyDefinition);
						using (ReadOnlyCollection<ProviderPropertyDefinition>.Enumerator enumerator = propertyDefinition.SupportingProperties.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ProviderPropertyDefinition propertyDefinition2 = enumerator.Current;
								this.UpdateDependentMvps(propertyDefinition2, hashSet);
							}
							goto IL_1A5;
						}
					}
					if (this.IsReadOnly)
					{
						foreach (ProviderPropertyDefinition providerPropertyDefinition in propertyDefinition.DependentProperties)
						{
							if (!providerPropertyDefinition.IsMultivalued)
							{
								object value2 = providerPropertyDefinition.GetterDelegate(this);
								this.SetField(providerPropertyDefinition, value2);
							}
						}
					}
					this.UpdateDependentMvps(propertyDefinition, null);
				}
				IL_1A5:;
			}
			finally
			{
				this.recursionDepth--;
				if (this.recursionDepth == 0)
				{
					this.shouldCallSetters = true;
				}
			}
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00037E18 File Offset: 0x00036018
		private void UpdateDependentMvps(ProviderPropertyDefinition propertyDefinition, HashSet<ProviderPropertyDefinition> visitedProperties)
		{
			ExTraceGlobals.PropertyBagTracer.TraceDebug<ProviderPropertyDefinition>((long)this.GetHashCode(), "UpdateDependentMvps {0}", propertyDefinition);
			if (!this.IsChanged(propertyDefinition))
			{
				return;
			}
			foreach (ProviderPropertyDefinition providerPropertyDefinition in propertyDefinition.DependentProperties)
			{
				object obj = null;
				if (this.IsCalculatedProperty(providerPropertyDefinition) && providerPropertyDefinition.IsMultivalued && (visitedProperties == null || visitedProperties.TryAdd(providerPropertyDefinition)) && this.TryGetField(providerPropertyDefinition, ref obj))
				{
					ExTraceGlobals.PropertyBagTracer.TraceDebug<ProviderPropertyDefinition>((long)this.GetHashCode(), "Updating dependent MVP {0}", providerPropertyDefinition);
					MultiValuedPropertyBase multiValuedPropertyBase = (MultiValuedPropertyBase)obj;
					MultiValuedPropertyBase newMvp = (MultiValuedPropertyBase)providerPropertyDefinition.GetterDelegate(this);
					multiValuedPropertyBase.UpdateValues(newMvp);
				}
			}
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00037EE8 File Offset: 0x000360E8
		internal void MarkAsChanged(ProviderPropertyDefinition key)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidObjectOperationException(DataStrings.ErrorReadOnlyCauseObject(key.Name));
			}
			object obj;
			if (this.currentValues.TryGetValue(key, out obj))
			{
				MultiValuedPropertyBase multiValuedPropertyBase = obj as MultiValuedPropertyBase;
				if (multiValuedPropertyBase != null)
				{
					multiValuedPropertyBase.MarkAsChanged();
					return;
				}
				this.OriginalValues[key] = PropertyBag.ChangeMarker;
			}
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00037F40 File Offset: 0x00036140
		internal void ResetChangeTracking(ProviderPropertyDefinition key)
		{
			object obj;
			if (this.currentValues.TryGetValue(key, out obj))
			{
				MultiValuedPropertyBase multiValuedPropertyBase = obj as MultiValuedPropertyBase;
				if (multiValuedPropertyBase != null && multiValuedPropertyBase.Changed)
				{
					multiValuedPropertyBase.ResetChangeTracking();
				}
				if (this.originalValues != null)
				{
					this.OriginalValues.Remove(key);
				}
			}
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00037F8C File Offset: 0x0003618C
		internal bool TryGetOriginalValue(ProviderPropertyDefinition key, out object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			bool result = (!this.IsReadOnly && this.originalValues != null && this.OriginalValues.TryGetValue(key, out value)) || this.currentValues.TryGetValue(key, out value);
			if (value == null || PropertyBag.ChangeMarker.Equals(value))
			{
				value = ((!key.IsMultivalued) ? key.DefaultValue : this.CreateMultiValuedProperty(key, true, new object[0], null, new LocalizedString?(DataStrings.ErrorOrignalMultiValuedProperty(key.Name))));
			}
			else
			{
				MultiValuedPropertyBase multiValuedPropertyBase = value as MultiValuedPropertyBase;
				if (multiValuedPropertyBase != null && (key.IsCalculated || !multiValuedPropertyBase.IsReadOnly))
				{
					value = this.CreateMultiValuedProperty(key, true, multiValuedPropertyBase, null, new LocalizedString?(DataStrings.ErrorOrignalMultiValuedProperty(key.Name)));
				}
			}
			return result;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00038054 File Offset: 0x00036254
		internal PropertyBag GetOriginalBag()
		{
			PropertyBag propertyBag = (PropertyBag)Activator.CreateInstance(base.GetType(), new object[]
			{
				true,
				this.Count
			});
			foreach (ProviderPropertyDefinition key in this.currentValues.Keys)
			{
				object value;
				this.TryGetOriginalValue(key, out value);
				propertyBag.SetField(key, value);
			}
			return propertyBag;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x000380EC File Offset: 0x000362EC
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitions)
		{
			if (propertyDefinitions == null)
			{
				throw new ArgumentNullException("propertyDefinitions");
			}
			object[] array = new object[propertyDefinitions.Count];
			int num = 0;
			foreach (PropertyDefinition key in propertyDefinitions)
			{
				array[num++] = this[key];
			}
			return array;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00038158 File Offset: 0x00036358
		public void SetProperties(ICollection<PropertyDefinition> propertyDefinitions, object[] propertyValues)
		{
			if (propertyDefinitions == null)
			{
				throw new ArgumentNullException("propertyDefinitions");
			}
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			int num = 0;
			foreach (PropertyDefinition key in propertyDefinitions)
			{
				this[key] = propertyValues[num++];
			}
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x000381C8 File Offset: 0x000363C8
		internal void SetObjectVersion(ExchangeObjectVersion newVersion)
		{
			ExTraceGlobals.PropertyBagTracer.TraceDebug<ExchangeObjectVersion>((long)this.GetHashCode(), "PropertyBag::SetObjectVersion({0})", newVersion);
			if (this.ObjectVersionPropertyDefinition != null)
			{
				ExchangeObjectVersion objB = (ExchangeObjectVersion)this[this.ObjectVersionPropertyDefinition];
				if (!object.Equals(newVersion, objB))
				{
					ExchangeObjectVersion value = object.Equals(newVersion, this.ObjectVersionPropertyDefinition.DefaultValue) ? null : newVersion;
					this.SetField(this.ObjectVersionPropertyDefinition, value);
					this.UpdateMvpsAsNecessary();
				}
			}
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0003823B File Offset: 0x0003643B
		internal void SetIsReadOnly(bool readOnly)
		{
			ExTraceGlobals.PropertyBagTracer.TraceDebug<bool>((long)this.GetHashCode(), "PropertyBag::SetIsReadOnly({0})", readOnly);
			if (readOnly != this.IsReadOnly)
			{
				this.ResetChangeTracking();
				this.readOnly = readOnly;
				this.originalValues = null;
				this.UpdateMvpsAsNecessary();
			}
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00038278 File Offset: 0x00036478
		private void UpdateMvpsAsNecessary()
		{
			List<ProviderPropertyDefinition> list = new List<ProviderPropertyDefinition>(this.currentValues.Keys);
			foreach (ProviderPropertyDefinition providerPropertyDefinition in list)
			{
				if (!providerPropertyDefinition.IsReadOnly && providerPropertyDefinition.IsMultivalued)
				{
					object obj = null;
					if (this.TryGetField(providerPropertyDefinition, ref obj) && obj != null)
					{
						LocalizedString? readOnlyErrorMessage;
						bool isReadOnly = this.IsReadOnlyProperty(providerPropertyDefinition, out readOnlyErrorMessage);
						MultiValuedPropertyBase multiValuedPropertyBase = (MultiValuedPropertyBase)obj;
						multiValuedPropertyBase.SetIsReadOnly(isReadOnly, readOnlyErrorMessage);
					}
				}
			}
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x00038310 File Offset: 0x00036510
		internal bool IsChanged(ProviderPropertyDefinition key)
		{
			if (!this.IsModified(key))
			{
				return false;
			}
			if (key.IsMultivalued)
			{
				return ((MultiValuedPropertyBase)this.currentValues[key]).Changed;
			}
			return !object.Equals(this.OriginalValues[key], this.currentValues[key]);
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00038368 File Offset: 0x00036568
		internal bool IsModified(ProviderPropertyDefinition key)
		{
			if (this.IsReadOnly)
			{
				return false;
			}
			if (this.originalValues == null)
			{
				return false;
			}
			if (this.OriginalValues.ContainsKey(key))
			{
				return true;
			}
			if (!this.IsCalculatedProperty(key) || (key.IsMultivalued && this.currentValues.ContainsKey(key)) || !this.HasSupportingProperties(key))
			{
				return false;
			}
			if (key.IsMultivalued)
			{
				MultiValuedPropertyBase multiValuedPropertyValue = this.GetMultiValuedPropertyValue(key);
				return multiValuedPropertyValue.Changed;
			}
			foreach (ProviderPropertyDefinition key2 in key.SupportingProperties)
			{
				if (this.IsModified(key2))
				{
					this.SetField(key, key.GetterDelegate(this));
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00038440 File Offset: 0x00036640
		private bool HasSupportingProperties(ProviderPropertyDefinition key)
		{
			if (this.currentValues.ContainsKey(key))
			{
				return true;
			}
			foreach (ProviderPropertyDefinition key2 in key.SupportingProperties)
			{
				if (!this.Contains(key2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x000384AC File Offset: 0x000366AC
		internal void ResetChangeTracking()
		{
			ExTraceGlobals.PropertyBagTracer.TraceDebug((long)this.GetHashCode(), "PropertyBag::ResetChangeTracking()");
			if (this.IsReadOnly)
			{
				return;
			}
			if (!this.storeValuesOnly)
			{
				List<ProviderPropertyDefinition> list = new List<ProviderPropertyDefinition>(this.currentValues.Keys);
				foreach (ProviderPropertyDefinition providerPropertyDefinition in list)
				{
					if (providerPropertyDefinition.IsCalculated && !providerPropertyDefinition.IsMultivalued)
					{
						try
						{
							this.SetField(providerPropertyDefinition, this[providerPropertyDefinition]);
						}
						catch (DataValidationException arg)
						{
							ExTraceGlobals.ValidationTracer.TraceError<ProviderPropertyDefinition, DataValidationException>(0L, "Calculated property {0} getter threw an exception {1}.", providerPropertyDefinition, arg);
						}
					}
				}
			}
			if (this.originalValues != null)
			{
				this.originalValues.Clear();
			}
			foreach (object obj in this.currentValues.Values)
			{
				MultiValuedPropertyBase multiValuedPropertyBase = obj as MultiValuedPropertyBase;
				if (multiValuedPropertyBase != null && multiValuedPropertyBase.Changed)
				{
					multiValuedPropertyBase.ResetChangeTracking();
				}
			}
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x000385E0 File Offset: 0x000367E0
		internal void Remove(ProviderPropertyDefinition key)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidObjectOperationException(DataStrings.ErrorReadOnlyCauseObject(key.Name));
			}
			this.currentValues.Remove(key);
			if (this.originalValues != null)
			{
				this.originalValues.Remove(key);
			}
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00038620 File Offset: 0x00036820
		public void Clear()
		{
			ExTraceGlobals.PropertyBagTracer.TraceDebug((long)this.GetHashCode(), "PropertyBag::Clear()");
			if (this.IsReadOnly)
			{
				throw new InvalidObjectOperationException(DataStrings.ExceptionReadOnlyPropertyBag);
			}
			this.currentValues.Clear();
			if (this.originalValues != null)
			{
				this.originalValues.Clear();
			}
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00038674 File Offset: 0x00036874
		internal bool Contains(ProviderPropertyDefinition key)
		{
			return this.currentValues.ContainsKey(key);
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00038682 File Offset: 0x00036882
		public Dictionary<ProviderPropertyDefinition, object>.Enumerator GetEnumerator()
		{
			return this.currentValues.GetEnumerator();
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00038690 File Offset: 0x00036890
		private object GetCalculatedPropertyValue(ProviderPropertyDefinition propertyDefinition)
		{
			object obj = null;
			if (!this.IsReadOnly || !this.TryGetField(propertyDefinition, ref obj))
			{
				obj = propertyDefinition.GetterDelegate(this);
				if (!this.currentValues.ContainsKey(propertyDefinition))
				{
					lock (this.lockObject)
					{
						this.currentValues[propertyDefinition] = obj;
					}
				}
			}
			return obj;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00038708 File Offset: 0x00036908
		private MultiValuedPropertyBase GetMultiValuedPropertyValue(ProviderPropertyDefinition propertyDefinition)
		{
			return this.GetMultiValuedPropertyValue(propertyDefinition, true);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00038714 File Offset: 0x00036914
		private MultiValuedPropertyBase GetMultiValuedPropertyValue(ProviderPropertyDefinition propertyDefinition, bool createDefaultValueEntry)
		{
			object obj = null;
			if (!this.TryGetField(propertyDefinition, ref obj) || obj == null)
			{
				if (!this.IsCalculatedProperty(propertyDefinition))
				{
					if (!createDefaultValueEntry)
					{
						return null;
					}
					obj = this.SetField(propertyDefinition, null);
				}
				else
				{
					bool flag = false;
					if (this.Changed)
					{
						foreach (ProviderPropertyDefinition providerPropertyDefinition in propertyDefinition.SupportingProperties)
						{
							if (this.IsModified(providerPropertyDefinition))
							{
								ExTraceGlobals.PropertyBagTracer.TracePerformance<ProviderPropertyDefinition, ProviderPropertyDefinition>(0L, "Initializing Calculated MVP '{0}' as modified because supporting property '{1}' is modified. This could degrade performance if it happens frequently.", propertyDefinition, providerPropertyDefinition);
								flag = true;
								break;
							}
						}
					}
					object obj2 = propertyDefinition.DefaultValue;
					if (this.HasSupportingProperties(propertyDefinition))
					{
						IPropertyBag propertyBag = flag ? new PropertyBag.OriginalPropertyBag(this) : this;
						try
						{
							obj2 = propertyDefinition.GetterDelegate(propertyBag);
						}
						catch (DataValidationException arg)
						{
							ExTraceGlobals.ValidationTracer.TraceError<ProviderPropertyDefinition, DataValidationException>(0L, "Calculated property {0} getter threw an exception {1}.", propertyDefinition, arg);
						}
					}
					object obj3 = flag ? propertyDefinition.GetterDelegate(this) : obj2;
					if (obj2 == null && obj3 == null && !createDefaultValueEntry)
					{
						return null;
					}
					obj = this.SetField(propertyDefinition, obj2);
					if (flag)
					{
						MultiValuedPropertyBase multiValuedPropertyBase = (MultiValuedPropertyBase)obj;
						MultiValuedPropertyBase newMvp = (MultiValuedPropertyBase)obj3;
						multiValuedPropertyBase.CollectionChanged -= this.MvpChangedHandler;
						try
						{
							multiValuedPropertyBase.UpdateValues(newMvp);
						}
						finally
						{
							multiValuedPropertyBase.CollectionChanged += this.MvpChangedHandler;
						}
					}
				}
			}
			return obj as MultiValuedPropertyBase;
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00038888 File Offset: 0x00036A88
		public bool TryGetValueWithoutDefault(PropertyDefinition key, out object returnValue)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)key;
			returnValue = null;
			if (providerPropertyDefinition.IsMultivalued)
			{
				returnValue = this.GetMultiValuedPropertyValue(providerPropertyDefinition, false);
				return returnValue != null;
			}
			if (this.IsCalculatedProperty(providerPropertyDefinition))
			{
				returnValue = this.GetCalculatedPropertyValue(providerPropertyDefinition);
				return returnValue != null;
			}
			this.TryGetField(providerPropertyDefinition, ref returnValue);
			return returnValue != null;
		}

		// Token: 0x170005B1 RID: 1457
		public virtual object this[PropertyDefinition key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)key;
				object obj = null;
				if (providerPropertyDefinition.IsMultivalued)
				{
					return this.GetMultiValuedPropertyValue(providerPropertyDefinition);
				}
				if (this.IsCalculatedProperty(providerPropertyDefinition))
				{
					return this.GetCalculatedPropertyValue(providerPropertyDefinition);
				}
				this.TryGetField(providerPropertyDefinition, ref obj);
				return obj ?? providerPropertyDefinition.DefaultValue;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				ExTraceGlobals.PropertyBagTracer.TraceDebug<string, object>((long)this.GetHashCode(), "PropertyBag[{0}]={1}.", key.Name, value ?? "<null>");
				ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)key;
				LocalizedString? localizedString;
				if (this.IsReadOnlyProperty(providerPropertyDefinition, out localizedString))
				{
					throw new InvalidObjectOperationException(localizedString.Value);
				}
				IList<ValidationError> list = providerPropertyDefinition.ValidateProperty(value, this, false);
				if (list.Count > 0)
				{
					throw new DataValidationException(list[0]);
				}
				if (!providerPropertyDefinition.IsMultivalued)
				{
					if (!this.IsCalculatedProperty(providerPropertyDefinition) && !providerPropertyDefinition.PersistDefaultValue && providerPropertyDefinition.DefaultValue != null && value != null)
					{
						if (providerPropertyDefinition.DefaultValue.Equals(value))
						{
							ExTraceGlobals.PropertyBagTracer.TraceDebug<string, object>((long)this.GetHashCode(), "Set[{0}] replaces default {1} with null.", providerPropertyDefinition.Name, value ?? "<null>");
							value = null;
						}
						else if (providerPropertyDefinition.Type == typeof(string) && string.Empty.Equals(value))
						{
							ExTraceGlobals.PropertyBagTracer.TraceDebug<string>((long)this.GetHashCode(), "Setting [{0}] to null.", providerPropertyDefinition.Name);
							value = null;
						}
					}
					this.UpdatePropertyDependents(providerPropertyDefinition, value);
					return;
				}
				MultiValuedPropertyBase multiValuedPropertyBase = (MultiValuedPropertyBase)this[providerPropertyDefinition];
				if (multiValuedPropertyBase == value)
				{
					multiValuedPropertyBase.MarkAsChanged();
					return;
				}
				MultiValuedPropertyBase multiValuedPropertyBase2 = (MultiValuedPropertyBase)value;
				if (multiValuedPropertyBase2 == null || !multiValuedPropertyBase2.IsChangesOnlyCopy)
				{
					multiValuedPropertyBase.UpdateValues(multiValuedPropertyBase2);
					return;
				}
				if (this.storeValuesOnly)
				{
					this.currentValues[providerPropertyDefinition] = multiValuedPropertyBase2;
					this.OriginalValues[providerPropertyDefinition] = multiValuedPropertyBase2;
					return;
				}
				multiValuedPropertyBase.CopyChangesFrom(multiValuedPropertyBase2);
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00038AE2 File Offset: 0x00036CE2
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.currentValues).CopyTo(array, index);
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x00038AF1 File Offset: 0x00036CF1
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)this.currentValues).IsSynchronized;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x00038AFE File Offset: 0x00036CFE
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this.currentValues).SyncRoot;
			}
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00038B0B File Offset: 0x00036D0B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.currentValues.GetEnumerator();
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00038B1D File Offset: 0x00036D1D
		internal void SetConfigObjectSchema(IEnumerable<PropertyDefinition> schema)
		{
			ExTraceGlobals.PropertyBagTracer.TraceDebug<IEnumerable<PropertyDefinition>>((long)this.GetHashCode(), "SetConfigObjectSchema({0})", schema);
			this.fullPropertyDefinitions = schema;
			this.FinalizeDeserialization();
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00038B44 File Offset: 0x00036D44
		private void FinalizeDeserialization()
		{
			ExTraceGlobals.PropertyBagTracer.TraceDebug<bool, IEnumerable<PropertyDefinition>, Dictionary<ProviderPropertyDefinition, object>>((long)this.GetHashCode(), "FinalizeDeserialization(); IsDeserializationComplete = {0}; FullPropertyDefinitions = {1}; CurrentValues = {2}", this.isDeserializationComplete, this.fullPropertyDefinitions, this.currentValues);
			if (this.isDeserializationComplete || this.fullPropertyDefinitions == null || this.currentValues == null || this.currentValues.Count == 0)
			{
				return;
			}
			Dictionary<ProviderPropertyDefinition, object> dictionary = this.currentValues;
			Dictionary<ProviderPropertyDefinition, object> dictionary2 = this.originalValues;
			this.currentValues = new Dictionary<ProviderPropertyDefinition, object>(dictionary.Count);
			this.originalValues = null;
			List<ProviderPropertyDefinition> list = new List<ProviderPropertyDefinition>();
			List<ProviderPropertyDefinition> list2 = new List<ProviderPropertyDefinition>();
			Dictionary<ProviderPropertyDefinition, object> dictionary3 = new Dictionary<ProviderPropertyDefinition, object>();
			foreach (PropertyDefinition propertyDefinition in this.fullPropertyDefinitions)
			{
				ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)propertyDefinition;
				if (providerPropertyDefinition.IsCalculated)
				{
					list.Add(providerPropertyDefinition);
				}
				else
				{
					object obj;
					if (dictionary.TryGetValue(providerPropertyDefinition, out obj))
					{
						MultiValuedPropertyBase multiValuedPropertyBase = obj as MultiValuedPropertyBase;
						if (this.DeserializeMultiValuedProperty(multiValuedPropertyBase, providerPropertyDefinition, this.serializationErrors))
						{
							multiValuedPropertyBase.CollectionChanging += this.MvpChangingHandler;
							multiValuedPropertyBase.CollectionChanged += this.MvpChangedHandler;
						}
						ExTraceGlobals.PropertyBagTracer.TraceDebug<ProviderPropertyDefinition, object>((long)this.GetHashCode(), "Restoring current value for {0}: {1}", providerPropertyDefinition, obj);
						this.currentValues.Add(providerPropertyDefinition, obj);
						dictionary3[providerPropertyDefinition] = obj;
					}
					else
					{
						list2.Add(providerPropertyDefinition);
					}
					object obj2;
					if (!this.IsReadOnly && dictionary2 != null && dictionary2.TryGetValue(providerPropertyDefinition, out obj2))
					{
						this.DeserializeMultiValuedProperty(obj2 as MultiValuedPropertyBase, providerPropertyDefinition, this.serializationErrors);
						ExTraceGlobals.PropertyBagTracer.TraceDebug<ProviderPropertyDefinition, object>((long)this.GetHashCode(), "Restoring original value for {0}: {1}", providerPropertyDefinition, obj2);
						this.OriginalValues.Add(providerPropertyDefinition, obj2);
						if (obj2 is PropertyBag.ChangeMarkerClass)
						{
							dictionary3[providerPropertyDefinition] = providerPropertyDefinition.DefaultValue;
						}
						else
						{
							dictionary3[providerPropertyDefinition] = obj2;
						}
					}
				}
			}
			for (int i = 0; i < list2.Count; i++)
			{
				ProviderPropertyDefinition providerPropertyDefinition2 = list2[i];
				ExTraceGlobals.PropertyBagTracer.TraceDebug<ProviderPropertyDefinition>((long)this.GetHashCode(), "Restoring empty value for {0}", providerPropertyDefinition2);
				if (providerPropertyDefinition2.IsMultivalued)
				{
					this.GetMultiValuedPropertyValue(providerPropertyDefinition2);
				}
				else
				{
					this.currentValues.Add(providerPropertyDefinition2, null);
				}
			}
			HashSet<ProviderPropertyDefinition> hashSet = new HashSet<ProviderPropertyDefinition>();
			for (int j = 0; j < list.Count; j++)
			{
				ProviderPropertyDefinition providerPropertyDefinition3 = list[j];
				ExTraceGlobals.PropertyBagTracer.TraceDebug<ProviderPropertyDefinition>((long)this.GetHashCode(), "Restoring calculated value for {0}", providerPropertyDefinition3);
				try
				{
					if (providerPropertyDefinition3.IsMultivalued)
					{
						hashSet.TryAdd(providerPropertyDefinition3);
						this.GetMultiValuedPropertyValue(providerPropertyDefinition3);
					}
					else
					{
						this.GetCalculatedPropertyValue(providerPropertyDefinition3);
					}
				}
				catch (DataValidationException)
				{
				}
			}
			if (!this.IsReadOnly && this.originalValues != null)
			{
				PropertyBag propertyBag = (PropertyBag)Activator.CreateInstance(base.GetType());
				propertyBag.currentValues = dictionary3;
				List<ProviderPropertyDefinition> list3 = new List<ProviderPropertyDefinition>(this.OriginalValues.Keys);
				foreach (ProviderPropertyDefinition providerPropertyDefinition4 in list3)
				{
					foreach (ProviderPropertyDefinition providerPropertyDefinition5 in providerPropertyDefinition4.DependentProperties)
					{
						if (hashSet.TryAdd(providerPropertyDefinition5) && list.Contains(providerPropertyDefinition5))
						{
							try
							{
								object value = providerPropertyDefinition5.GetterDelegate(propertyBag);
								this.OriginalValues.Add(providerPropertyDefinition5, value);
							}
							catch (DataValidationException)
							{
							}
						}
					}
				}
			}
			if (this.updateErrors != null)
			{
				this.updateErrors(this.serializationErrors.ToArray());
			}
			this.serializationErrors = null;
			this.fullPropertyDefinitions = null;
			this.isDeserializationComplete = true;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00038F70 File Offset: 0x00037170
		private bool DeserializeMultiValuedProperty(MultiValuedPropertyBase mvp, ProviderPropertyDefinition propDef, List<ValidationError> serializationErrors)
		{
			if (mvp != null)
			{
				mvp.UpdatePropertyDefinition(propDef);
				try
				{
					mvp.FinalizeDeserialization();
				}
				catch (DataValidationException ex)
				{
					this.serializationErrors.Add(ex.Error);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x00038FB8 File Offset: 0x000371B8
		private object SerializeDataPrivate(ProviderPropertyDefinition propertyDefinition, object input)
		{
			if (this.NeedsToSuppressPii)
			{
				input = this.InternalSuppressPii(propertyDefinition, input);
			}
			if (SerializationTypeConverter.TypeIsSerializable(propertyDefinition.Type))
			{
				return input;
			}
			object result;
			try
			{
				result = this.SerializeData(propertyDefinition, input);
			}
			catch (FormatException innerException)
			{
				throw new SerializationException(DataStrings.ErrorConversionFailed(propertyDefinition, input), innerException);
			}
			catch (NotImplementedException innerException2)
			{
				throw new SerializationException(DataStrings.ErrorConversionFailed(propertyDefinition, input), innerException2);
			}
			return result;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00039038 File Offset: 0x00037238
		internal virtual object SerializeData(ProviderPropertyDefinition propertyDefinition, object input)
		{
			return ValueConvertor.SerializeData(propertyDefinition, input);
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00039044 File Offset: 0x00037244
		private object DeserializeDataPrivate(ProviderPropertyDefinition propertyDefinition, object input)
		{
			if (SerializationTypeConverter.TypeIsSerializable(propertyDefinition.Type))
			{
				return input;
			}
			object result;
			try
			{
				result = this.DeserializeData(propertyDefinition, input);
			}
			catch (FormatException ex)
			{
				throw new DataValidationException(new PropertyConversionError(DataStrings.ErrorConversionFailed(propertyDefinition, input), propertyDefinition, input, ex), ex);
			}
			return result;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00039094 File Offset: 0x00037294
		internal virtual object DeserializeData(ProviderPropertyDefinition propertyDefinition, object input)
		{
			return ValueConvertor.DeserializeData(propertyDefinition, input);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x000390A0 File Offset: 0x000372A0
		private void SerializeAndAddValuePair(List<PropertyBag.ValuePair> list, ProviderPropertyDefinition key, object value)
		{
			value = this.SerializeDataPrivate(key, value);
			PropertyBag.ValuePair item;
			item.Key = key;
			item.Value = value;
			ExTraceGlobals.PropertyBagTracer.TraceDebug<ProviderPropertyDefinition, object>((long)this.GetHashCode(), "Adding ValuePair ({0}, {1})", key, value);
			list.Add(item);
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x000390E6 File Offset: 0x000372E6
		private bool NeedsToSuppressPii
		{
			get
			{
				return SuppressingPiiContext.NeedPiiSuppression;
			}
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x000390F0 File Offset: 0x000372F0
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			ExTraceGlobals.PropertyBagTracer.TraceDebug<bool, bool>((long)this.GetHashCode(), "PrepareForSerialization(); IsReadOnly = {0}; SaveCalculatedValues = {1}", this.IsReadOnly, this.storeValuesOnly);
			this.serializerAssemblyVersion = ConfigurableObject.ExecutingAssemblyVersion;
			List<PropertyBag.ValuePair> list = new List<PropertyBag.ValuePair>();
			foreach (KeyValuePair<ProviderPropertyDefinition, object> keyValuePair in this.currentValues)
			{
				ProviderPropertyDefinition key = keyValuePair.Key;
				object value = keyValuePair.Value;
				if ((!key.IsCalculated || this.NeedsToSuppressPii) && value != null)
				{
					MultiValuedPropertyBase multiValuedPropertyBase = value as MultiValuedPropertyBase;
					if (multiValuedPropertyBase == null || multiValuedPropertyBase.Changed || multiValuedPropertyBase.Count != 0)
					{
						this.SerializeAndAddValuePair(list, key, value);
					}
				}
			}
			this.serializedCurrentValues = list.ToArray();
			ExTraceGlobals.PropertyBagTracer.TraceDebug((long)this.GetHashCode(), "Serialize Original Values");
			if (!this.IsReadOnly)
			{
				List<PropertyBag.ValuePair> list2 = new List<PropertyBag.ValuePair>();
				if (this.originalValues != null)
				{
					foreach (KeyValuePair<ProviderPropertyDefinition, object> keyValuePair2 in this.OriginalValues)
					{
						if (!keyValuePair2.Key.IsCalculated || this.NeedsToSuppressPii)
						{
							this.SerializeAndAddValuePair(list2, keyValuePair2.Key, keyValuePair2.Value);
						}
					}
				}
				this.serializedOriginalValues = list2.ToArray();
			}
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0003926C File Offset: 0x0003746C
		[OnSerialized]
		private void OnSerialized(StreamingContext context)
		{
			ExTraceGlobals.PropertyBagTracer.TraceDebug((long)this.GetHashCode(), "OnSerialized()");
			this.serializedCurrentValues = null;
			this.serializedOriginalValues = null;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00039294 File Offset: 0x00037494
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			ExTraceGlobals.PropertyBagTracer.TraceDebug((long)this.GetHashCode(), "OnDeserializing()");
			this.lockObject = new object();
			this.isDeserializationComplete = false;
			this.recursionDepth = 0;
			this.shouldCallSetters = true;
			this.serializationErrors = new List<ValidationError>();
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x000392E4 File Offset: 0x000374E4
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			ExTraceGlobals.PropertyBagTracer.TraceDebug<bool, bool>((long)this.GetHashCode(), "DeserializeValues(); IsReadOnly = {0}; SaveCalculatedValues = {1}", this.IsReadOnly, this.storeValuesOnly);
			this.currentValues = new Dictionary<ProviderPropertyDefinition, object>(this.serializedCurrentValues.Length);
			this.originalValues = null;
			foreach (PropertyBag.ValuePair valuePair in this.serializedCurrentValues)
			{
				try
				{
					ProviderPropertyDefinition key = valuePair.Key;
					object value = this.DeserializeDataPrivate(key, valuePair.Value);
					this.currentValues.Add(key, value);
				}
				catch (DataValidationException ex)
				{
					this.serializationErrors.Add(ex.Error);
				}
			}
			this.serializedCurrentValues = null;
			if (this.serializedOriginalValues != null)
			{
				foreach (PropertyBag.ValuePair valuePair2 in this.serializedOriginalValues)
				{
					ProviderPropertyDefinition key2 = valuePair2.Key;
					object value2 = this.DeserializeDataPrivate(key2, valuePair2.Value);
					this.OriginalValues[key2] = value2;
				}
				this.serializedOriginalValues = null;
			}
			this.FinalizeDeserialization();
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00039414 File Offset: 0x00037614
		private bool InternalIsMarshalByRefObject(object value)
		{
			return typeof(MarshalByRefObject).IsInstanceOfType(value);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00039426 File Offset: 0x00037626
		private object InternalSuppressPii(ProviderPropertyDefinition propertyDefinition, object input)
		{
			return SuppressingPiiProperty.TryRedact(propertyDefinition, input, SuppressingPiiContext.PiiMap);
		}

		// Token: 0x04000AFC RID: 2812
		public const int DefaultSize = 16;

		// Token: 0x04000AFD RID: 2813
		private static object[] EmptyArray = new object[0];

		// Token: 0x04000AFE RID: 2814
		private static readonly object ChangeMarker = PropertyBag.ChangeMarkerClass.Instance;

		// Token: 0x04000AFF RID: 2815
		[NonSerialized]
		private Dictionary<ProviderPropertyDefinition, object> currentValues;

		// Token: 0x04000B00 RID: 2816
		[NonSerialized]
		private object lockObject = new object();

		// Token: 0x04000B01 RID: 2817
		[NonSerialized]
		private Dictionary<ProviderPropertyDefinition, object> originalValues;

		// Token: 0x04000B02 RID: 2818
		[NonSerialized]
		private int recursionDepth;

		// Token: 0x04000B03 RID: 2819
		[NonSerialized]
		private bool shouldCallSetters = true;

		// Token: 0x04000B04 RID: 2820
		private bool readOnly;

		// Token: 0x04000B05 RID: 2821
		private bool storeValuesOnly;

		// Token: 0x04000B06 RID: 2822
		[NonSerialized]
		private bool isDeserializationComplete;

		// Token: 0x04000B07 RID: 2823
		[NonSerialized]
		private IEnumerable<PropertyDefinition> fullPropertyDefinitions;

		// Token: 0x04000B08 RID: 2824
		[NonSerialized]
		private EventHandler mvpChangingHandler;

		// Token: 0x04000B09 RID: 2825
		[NonSerialized]
		private EventHandler mvpChangedHandler;

		// Token: 0x04000B0A RID: 2826
		[NonSerialized]
		private UpdateErrorsDelegate updateErrors;

		// Token: 0x04000B0B RID: 2827
		[NonSerialized]
		private List<ValidationError> serializationErrors;

		// Token: 0x04000B0C RID: 2828
		private Version serializerAssemblyVersion;

		// Token: 0x04000B0D RID: 2829
		private PropertyBag.ValuePair[] serializedCurrentValues;

		// Token: 0x04000B0E RID: 2830
		private PropertyBag.ValuePair[] serializedOriginalValues;

		// Token: 0x0200020F RID: 527
		[Serializable]
		private struct ValuePair
		{
			// Token: 0x04000B0F RID: 2831
			public ProviderPropertyDefinition Key;

			// Token: 0x04000B10 RID: 2832
			public object Value;
		}

		// Token: 0x02000210 RID: 528
		[ImmutableObject(true)]
		[Serializable]
		private class ChangeMarkerClass : ICloneable, ISerializable
		{
			// Token: 0x06001295 RID: 4757 RVA: 0x0003944B File Offset: 0x0003764B
			private ChangeMarkerClass()
			{
			}

			// Token: 0x06001296 RID: 4758 RVA: 0x00039453 File Offset: 0x00037653
			public object Clone()
			{
				return this;
			}

			// Token: 0x06001297 RID: 4759 RVA: 0x00039456 File Offset: 0x00037656
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				info.SetType(typeof(PropertyBag.ChangeMarkerClass.ChangeMarkerSerializationHelper));
			}

			// Token: 0x04000B11 RID: 2833
			public static readonly PropertyBag.ChangeMarkerClass Instance = new PropertyBag.ChangeMarkerClass();

			// Token: 0x02000211 RID: 529
			[Serializable]
			private class ChangeMarkerSerializationHelper : IObjectReference
			{
				// Token: 0x06001299 RID: 4761 RVA: 0x00039474 File Offset: 0x00037674
				public object GetRealObject(StreamingContext context)
				{
					return PropertyBag.ChangeMarkerClass.Instance;
				}
			}
		}

		// Token: 0x02000212 RID: 530
		private class OriginalPropertyBag : IPropertyBag, IReadOnlyPropertyBag
		{
			// Token: 0x0600129B RID: 4763 RVA: 0x00039483 File Offset: 0x00037683
			public OriginalPropertyBag(PropertyBag internalBag)
			{
				if (internalBag == null)
				{
					throw new ArgumentNullException("internalBag");
				}
				this.internalBag = internalBag;
			}

			// Token: 0x170005B5 RID: 1461
			public object this[PropertyDefinition propertyDefinition]
			{
				get
				{
					ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)propertyDefinition;
					object result;
					if (this.internalBag.TryGetOriginalValue(providerPropertyDefinition, out result))
					{
						return result;
					}
					return providerPropertyDefinition.DefaultValue;
				}
				set
				{
					throw new InvalidOperationException("Dev Error: Code is trying to modify the PropertyBag on a code path that should only be reading values.");
				}
			}

			// Token: 0x0600129E RID: 4766 RVA: 0x000394D8 File Offset: 0x000376D8
			public void SetProperties(ICollection<PropertyDefinition> propertyDefinitionArray, object[] propertyValuesArray)
			{
				throw new InvalidOperationException("Dev Error: Code is trying to modify the PropertyBag on a code path that should only be reading values.");
			}

			// Token: 0x0600129F RID: 4767 RVA: 0x000394E4 File Offset: 0x000376E4
			public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
			{
				if (propertyDefinitionArray == null)
				{
					throw new ArgumentNullException("propertyDefinitions");
				}
				object[] array = new object[propertyDefinitionArray.Count];
				int num = 0;
				foreach (PropertyDefinition propertyDefinition in propertyDefinitionArray)
				{
					array[num++] = this[propertyDefinition];
				}
				return array;
			}

			// Token: 0x04000B12 RID: 2834
			private PropertyBag internalBag;
		}
	}
}
