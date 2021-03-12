using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200011F RID: 287
	[Serializable]
	public abstract class ConfigurableObject : IPropertyBag, IReadOnlyPropertyBag, IConfigurable, ICloneable, IVersionable, ICmdletProxyable
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000A05 RID: 2565 RVA: 0x0001EF64 File Offset: 0x0001D164
		// (remove) Token: 0x06000A06 RID: 2566 RVA: 0x0001EF7D File Offset: 0x0001D17D
		internal event ConvertOutputPropertyDelegate OutputPropertyConverter
		{
			add
			{
				this.outputPropertyConverter = (ConvertOutputPropertyDelegate)Delegate.Combine(this.outputPropertyConverter, value);
			}
			remove
			{
				this.outputPropertyConverter = (ConvertOutputPropertyDelegate)Delegate.Remove(this.outputPropertyConverter, value);
			}
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0001EF98 File Offset: 0x0001D198
		internal ConfigurableObject(PropertyBag propertyBag)
		{
			if (propertyBag == null)
			{
				throw new ArgumentNullException("propertyBag");
			}
			this.propertyBag = propertyBag;
			if (propertyBag.Count == 0)
			{
				this.ObjectState = (ObjectState)propertyBag.ObjectStatePropertyDefinition.DefaultValue;
				this.propertyBag.ResetChangeTracking(propertyBag.ObjectStatePropertyDefinition);
			}
			if (this.ObjectState == ObjectState.New && this.propertyBag.ObjectVersionPropertyDefinition != null && !this.propertyBag.Contains(this.propertyBag.ObjectVersionPropertyDefinition))
			{
				this.propertyBag.SetField(this.propertyBag.ObjectVersionPropertyDefinition, this.MaximumSupportedExchangeObjectVersion);
				this.propertyBag.ResetChangeTracking(this.propertyBag.ObjectVersionPropertyDefinition);
			}
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0001F04F File Offset: 0x0001D24F
		public object GetProxyInfo()
		{
			return this.proxyInfo;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0001F057 File Offset: 0x0001D257
		public void SetProxyInfo(object proxyInfoValue)
		{
			if (this.proxyInfo != null && proxyInfoValue != null)
			{
				return;
			}
			this.proxyInfo = proxyInfoValue;
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0001F06C File Offset: 0x0001D26C
		internal virtual ExchangeObjectVersion ExchangeVersion
		{
			get
			{
				if (this.propertyBag.ObjectVersionPropertyDefinition == null)
				{
					throw new NotSupportedException(DataStrings.ExceptionVersionlessObject);
				}
				return (ExchangeObjectVersion)this[this.propertyBag.ObjectVersionPropertyDefinition];
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0001F0A1 File Offset: 0x0001D2A1
		ExchangeObjectVersion IVersionable.ExchangeVersion
		{
			get
			{
				return this.ExchangeVersion;
			}
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0001F0AC File Offset: 0x0001D2AC
		internal void SetExchangeVersion(ExchangeObjectVersion newVersion)
		{
			this.CheckWritable();
			if (this.propertyBag.ObjectVersionPropertyDefinition == null)
			{
				throw new NotSupportedException(DataStrings.ExceptionVersionlessObject);
			}
			ExchangeObjectVersion exchangeVersion = this.ExchangeVersion;
			if (null == newVersion)
			{
				newVersion = (ExchangeObjectVersion)this.propertyBag.ObjectVersionPropertyDefinition.DefaultValue;
			}
			if (newVersion.IsOlderThan(exchangeVersion))
			{
				List<ProviderPropertyDefinition> list = new List<ProviderPropertyDefinition>(this.propertyBag.Keys.Cast<ProviderPropertyDefinition>());
				foreach (ProviderPropertyDefinition providerPropertyDefinition in list)
				{
					if (providerPropertyDefinition.VersionAdded.IsNewerThan(newVersion) && !providerPropertyDefinition.VersionAdded.IsNewerThan(exchangeVersion))
					{
						if (this.propertyBag.IsReadOnlyProperty(providerPropertyDefinition) || providerPropertyDefinition.IsCalculated)
						{
							this.propertyBag.SetField(providerPropertyDefinition, null);
						}
						else
						{
							this[providerPropertyDefinition] = null;
						}
					}
				}
			}
			this.propertyBag.SetObjectVersion(newVersion);
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000A0D RID: 2573
		internal abstract ObjectSchema ObjectSchema { get; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0001F1B8 File Offset: 0x0001D3B8
		ObjectSchema IVersionable.ObjectSchema
		{
			get
			{
				return this.ObjectSchema;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0001F1C0 File Offset: 0x0001D3C0
		internal virtual ObjectSchema DeserializationSchema
		{
			get
			{
				return this.ObjectSchema;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0001F1C8 File Offset: 0x0001D3C8
		internal virtual ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2007;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0001F1CF File Offset: 0x0001D3CF
		ExchangeObjectVersion IVersionable.MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return this.MaximumSupportedExchangeObjectVersion;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0001F1D7 File Offset: 0x0001D3D7
		internal bool IsReadOnly
		{
			get
			{
				return this.propertyBag.IsReadOnly;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0001F1E4 File Offset: 0x0001D3E4
		bool IVersionable.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0001F1EC File Offset: 0x0001D3EC
		internal virtual void SetIsReadOnly(bool valueToSet)
		{
			this.propertyBag.SetIsReadOnly(valueToSet);
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0001F1FA File Offset: 0x0001D3FA
		internal virtual bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0001F1FD File Offset: 0x0001D3FD
		bool IVersionable.ExchangeVersionUpgradeSupported
		{
			get
			{
				return this.ExchangeVersionUpgradeSupported;
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0001F208 File Offset: 0x0001D408
		internal virtual bool IsPropertyAccessible(PropertyDefinition propertyDefinition)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			ProviderPropertyDefinition providerPropertyDefinition = propertyDefinition as ProviderPropertyDefinition;
			return providerPropertyDefinition == null || !providerPropertyDefinition.VersionAdded.ExchangeBuild.IsNewerThan(this.ExchangeVersion.ExchangeBuild) || this.ExchangeVersionUpgradeSupported;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001F255 File Offset: 0x0001D455
		bool IVersionable.IsPropertyAccessible(PropertyDefinition propertyDefinition)
		{
			return this.IsPropertyAccessible(propertyDefinition);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001F260 File Offset: 0x0001D460
		public virtual object Clone()
		{
			ConfigurableObject configurableObject = (ConfigurableObject)CloneHelper.SerializeObj(this);
			if (this.DeserializationSchema == null)
			{
				List<PropertyDefinition> list = new List<PropertyDefinition>(this.propertyBag.Keys.Count);
				foreach (object obj in this.propertyBag.Keys)
				{
					PropertyDefinition item = (PropertyDefinition)obj;
					list.Add(item);
				}
				configurableObject.propertyBag.SetConfigObjectSchema(list);
			}
			return configurableObject;
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0001F2F8 File Offset: 0x0001D4F8
		public virtual ObjectId Identity
		{
			get
			{
				return (ObjectId)this[this.propertyBag.ObjectIdentityPropertyDefinition];
			}
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0001F310 File Offset: 0x0001D510
		public ValidationError[] Validate()
		{
			List<ValidationError> list = new List<ValidationError>();
			this.ValidateRead(list);
			this.ValidateWrite(list);
			return list.ToArray();
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0001F338 File Offset: 0x0001D538
		internal ValidationError[] ValidateRead()
		{
			List<ValidationError> list = new List<ValidationError>();
			this.ValidateRead(list);
			return list.ToArray();
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0001F358 File Offset: 0x0001D558
		internal ValidationError[] ValidateWrite()
		{
			List<ValidationError> list = new List<ValidationError>();
			this.ValidateWrite(list);
			return list.ToArray();
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001F378 File Offset: 0x0001D578
		protected bool TryConvertOutputProperty(object value, PropertyDefinition property, out object convertedValue)
		{
			convertedValue = null;
			return this.outputPropertyConverter != null && ObjectOutputHelper.TryConvertOutputProperty(value, this, property, null, this.outputPropertyConverter.GetInvocationList(), out convertedValue);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0001F39C File Offset: 0x0001D59C
		protected bool TryConvertOutputProperty(object value, string propertyInStr, out object convertedValue)
		{
			convertedValue = null;
			return this.outputPropertyConverter != null && ObjectOutputHelper.TryConvertOutputProperty(value, this, null, propertyInStr, this.outputPropertyConverter.GetInvocationList(), out convertedValue);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001F3C0 File Offset: 0x0001D5C0
		protected virtual void ValidateRead(List<ValidationError> errors)
		{
			this.ValidateRead(errors, (this.ObjectSchema != null) ? this.ObjectSchema.AllProperties : null);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001F444 File Offset: 0x0001D644
		internal void ValidateRead(List<ValidationError> errors, IEnumerable<PropertyDefinition> propertiesToValidate)
		{
			ExchangeObjectVersion exchangeObjectVersion = (this.propertyBag.ObjectVersionPropertyDefinition == null) ? null : this.ExchangeVersion;
			if (propertiesToValidate != null)
			{
				foreach (PropertyDefinition propertyDefinition in propertiesToValidate)
				{
					ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)propertyDefinition;
					if (providerPropertyDefinition.IsCalculated)
					{
						bool onlyCacheValue = null != exchangeObjectVersion && exchangeObjectVersion.IsOlderThan(providerPropertyDefinition.VersionAdded);
						this.ValidateCalculatedProperty(providerPropertyDefinition, this.propertyBag, errors, true, onlyCacheValue);
					}
				}
			}
			List<ValidationError> list = this.instantiationErrors;
			if (list == null || list.Count == 0)
			{
				return;
			}
			list.RemoveAll(delegate(ValidationError error)
			{
				PropertyValidationError propertyValidationError = error as PropertyValidationError;
				if (propertyValidationError == null)
				{
					return false;
				}
				ProviderPropertyDefinition providerPropertyDefinition2 = propertyValidationError.PropertyDefinition as ProviderPropertyDefinition;
				if (providerPropertyDefinition2 == null)
				{
					return false;
				}
				bool flag = providerPropertyDefinition2.IsMultivalued && ((MultiValuedPropertyBase)this[providerPropertyDefinition2]).Changed;
				if (flag)
				{
					ExTraceGlobals.ValidationTracer.TraceDebug<ValidationError>((long)this.GetHashCode(), "Removing instantiation error '{0}'.", error);
				}
				return flag;
			});
			errors.AddRange(list);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001F508 File Offset: 0x0001D708
		protected virtual void ValidateWrite(List<ValidationError> errors)
		{
			if (!this.IsReadOnly)
			{
				this.RunFullPropertyValidation(errors);
			}
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0001F519 File Offset: 0x0001D719
		internal virtual bool SkipFullPropertyValidation(ProviderPropertyDefinition propertyDefinition)
		{
			return !propertyDefinition.IsMultivalued && !propertyDefinition.HasAutogeneratedConstraints && this.propertyBag.IsChanged(propertyDefinition);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0001F53C File Offset: 0x0001D73C
		private void RunFullPropertyValidation(List<ValidationError> errors)
		{
			IEnumerable<PropertyDefinition> enumerable = (this.ObjectSchema == null) ? ((IEnumerable<PropertyDefinition>)this.propertyBag.Keys) : this.ObjectSchema.AllProperties;
			foreach (PropertyDefinition propertyDefinition in enumerable)
			{
				ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)propertyDefinition;
				if (!this.SkipFullPropertyValidation(providerPropertyDefinition))
				{
					if (providerPropertyDefinition.IsCalculated)
					{
						this.ValidateCalculatedProperty(providerPropertyDefinition, this.propertyBag, errors, false, false);
					}
					else
					{
						object value = null;
						this.propertyBag.TryGetField(providerPropertyDefinition, ref value);
						IList<ValidationError> list = providerPropertyDefinition.ValidateProperty(value, this.propertyBag, false);
						if (list.Count > 0)
						{
							foreach (ValidationError item in list)
							{
								if (!errors.Contains(item))
								{
									errors.Add(item);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0001F648 File Offset: 0x0001D848
		private void ValidateCalculatedProperty(ProviderPropertyDefinition propertyDefinition, PropertyBag propertyBag, List<ValidationError> errors, bool useOnlyReadConstraints, bool onlyCacheValue)
		{
			try
			{
				object value = propertyBag[propertyDefinition];
				if (!onlyCacheValue)
				{
					IList<ValidationError> list = propertyDefinition.ValidateProperty(value, propertyBag, useOnlyReadConstraints);
					if (list.Count > 0)
					{
						foreach (ValidationError item in list)
						{
							if (!errors.Contains(item))
							{
								errors.Add(item);
							}
						}
					}
				}
			}
			catch (DataValidationException ex)
			{
				ExTraceGlobals.ValidationTracer.TraceWarning<ProviderPropertyDefinition, DataValidationException>(0L, "Calculated property {0} threw an exception {1}.", propertyDefinition, ex);
				if (useOnlyReadConstraints && !onlyCacheValue)
				{
					errors.Add(ex.Error);
				}
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0001F6F8 File Offset: 0x0001D8F8
		public virtual bool IsValid
		{
			get
			{
				return 0 == this.Validate().Length;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0001F708 File Offset: 0x0001D908
		// (set) Token: 0x06000A28 RID: 2600 RVA: 0x0001F773 File Offset: 0x0001D973
		internal ObjectState ObjectState
		{
			get
			{
				ObjectState objectState = (ObjectState)this.propertyBag[this.propertyBag.ObjectStatePropertyDefinition];
				if (objectState == ObjectState.Unchanged && this.IsChanged())
				{
					objectState = ObjectState.Changed;
					this.propertyBag.SetField(this.propertyBag.ObjectStatePropertyDefinition, objectState);
					this.propertyBag.ResetChangeTracking(this.propertyBag.ObjectStatePropertyDefinition);
				}
				return objectState;
			}
			private set
			{
				this.propertyBag.SetField(this.propertyBag.ObjectStatePropertyDefinition, value);
				this.propertyBag.ResetChangeTracking(this.propertyBag.ObjectStatePropertyDefinition);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0001F7A8 File Offset: 0x0001D9A8
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return this.ObjectState;
			}
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0001F7B0 File Offset: 0x0001D9B0
		internal void CheckWritable()
		{
			if (!this.IsReadOnly)
			{
				return;
			}
			if (this.propertyBag.ObjectVersionPropertyDefinition != null && this.MaximumSupportedExchangeObjectVersion.IsOlderThan(this.ExchangeVersion))
			{
				throw new InvalidObjectOperationException(DataStrings.ExceptionReadOnlyBecauseTooNew(this.ExchangeVersion, this.MaximumSupportedExchangeObjectVersion));
			}
			throw new InvalidObjectOperationException(DataStrings.ExceptionReadOnlyPropertyBag);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0001F808 File Offset: 0x0001DA08
		internal virtual void CopyChangesFrom(ConfigurableObject changedObject)
		{
			if (changedObject == null)
			{
				throw new ArgumentNullException("changedObject");
			}
			this.CheckWritable();
			ProviderPropertyDefinition[] array = new ProviderPropertyDefinition[changedObject.propertyBag.Keys.Count];
			changedObject.propertyBag.Keys.CopyTo(array, 0);
			foreach (ProviderPropertyDefinition providerPropertyDefinition in array)
			{
				if (!providerPropertyDefinition.IsReadOnly && (!providerPropertyDefinition.IsWriteOnce || this.ObjectState == ObjectState.New) && (changedObject.propertyBag.SaveCalculatedValues || !providerPropertyDefinition.IsCalculated) && changedObject.IsModified(providerPropertyDefinition))
				{
					object obj = changedObject[providerPropertyDefinition];
					MultiValuedPropertyBase multiValuedPropertyBase = obj as MultiValuedPropertyBase;
					if (!providerPropertyDefinition.IsMultivalued || multiValuedPropertyBase == null || multiValuedPropertyBase.IsChangesOnlyCopy)
					{
						this[providerPropertyDefinition] = obj;
					}
					else
					{
						MultiValuedPropertyBase multiValuedPropertyBase2 = (MultiValuedPropertyBase)this[providerPropertyDefinition];
						multiValuedPropertyBase2.CopyChangesFrom(multiValuedPropertyBase);
						this.CleanupInstantiationErrors(providerPropertyDefinition);
					}
				}
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0001F8F2 File Offset: 0x0001DAF2
		void IConfigurable.CopyChangesFrom(IConfigurable source)
		{
			this.CopyChangesFrom((ConfigurableObject)source);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0001F98C File Offset: 0x0001DB8C
		private void CleanupInstantiationErrors(ProviderPropertyDefinition property)
		{
			List<ValidationError> list = this.instantiationErrors;
			if (list == null || list.Count == 0)
			{
				return;
			}
			List<ProviderPropertyDefinition> listToClear = new List<ProviderPropertyDefinition>();
			listToClear.Add(property);
			if (property.IsCalculated)
			{
				foreach (ProviderPropertyDefinition providerPropertyDefinition in property.SupportingProperties)
				{
					if (this.IsChanged(providerPropertyDefinition))
					{
						listToClear.Add(providerPropertyDefinition);
					}
				}
			}
			list.RemoveAll(delegate(ValidationError error)
			{
				PropertyValidationError propertyValidationError = error as PropertyValidationError;
				ProviderPropertyDefinition providerPropertyDefinition2 = (ProviderPropertyDefinition)propertyValidationError.PropertyDefinition;
				bool flag = false;
				if (providerPropertyDefinition2 != null)
				{
					flag = listToClear.Contains(providerPropertyDefinition2);
					if (flag)
					{
						this.propertyBag.MarkAsChanged(providerPropertyDefinition2);
						if (ExTraceGlobals.ValidationTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.ValidationTracer.TraceDebug<ValidationError, string, string>((long)this.GetHashCode(), "Removing instantiation error '{0}'. Because property {1} has been modified directly or it is a supporting property of {2}.", error, providerPropertyDefinition2.Name, property.Name);
						}
					}
				}
				return flag;
			});
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0001FA58 File Offset: 0x0001DC58
		internal void MarkAsDeleted()
		{
			this.ObjectState = ObjectState.Deleted;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001FA61 File Offset: 0x0001DC61
		internal void ResetChangeTracking(bool markAsUnchanged)
		{
			this.propertyBag.ResetChangeTracking();
			if (markAsUnchanged || ObjectState.Changed == this.ObjectState)
			{
				this.ObjectState = ObjectState.Unchanged;
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0001FA81 File Offset: 0x0001DC81
		internal void ResetChangeTracking()
		{
			this.ResetChangeTracking(false);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0001FA8A File Offset: 0x0001DC8A
		void IConfigurable.ResetChangeTracking()
		{
			this.ResetChangeTracking();
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0001FA94 File Offset: 0x0001DC94
		private void UpdateInstantiationErrors(ValidationError[] errors)
		{
			ExTraceGlobals.SerializationTracer.TraceFunction((long)this.GetHashCode(), "ConfigurableObject.UpdateInstantiationErrors(). Errors = {0}", (object[])errors);
			if (errors == null)
			{
				throw new ArgumentNullException("errors");
			}
			if (errors.Length > 0)
			{
				bool flag = false;
				List<ValidationError> list = this.InstantiationErrors;
				foreach (ValidationError item in errors)
				{
					if (!list.Contains(item))
					{
						list.Add(item);
						flag = true;
					}
				}
				if (flag)
				{
					ExTraceGlobals.SerializationTracer.TraceDebug((long)this.GetHashCode(), "ConfigurableObject.UpdateInstantiationErrors(). Serialization added new Errors. Marking object as ReadOnly.", (object[])errors);
					this.SetIsReadOnly(true);
					list.Add(new ObjectValidationError(DataStrings.ErrorObjectSerialization(this.Identity, ConfigurableObject.ExecutingAssemblyVersion, this.serializerAssemblyVersion), this.Identity, null));
				}
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001FB56 File Offset: 0x0001DD56
		internal bool TryGetValueWithoutDefault(PropertyDefinition propertyDefinition, out object value)
		{
			return this.propertyBag.TryGetValueWithoutDefault(propertyDefinition, out value);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001FB68 File Offset: 0x0001DD68
		internal bool TryGetOriginalValue<T>(ProviderPropertyDefinition key, out T value)
		{
			object obj;
			bool result = this.propertyBag.TryGetOriginalValue(key, out obj);
			value = (T)((object)obj);
			return result;
		}

		// Token: 0x17000356 RID: 854
		internal virtual object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)propertyDefinition;
				object obj;
				try
				{
					obj = this.propertyBag[providerPropertyDefinition];
					obj = this.InternalSuppressPii(propertyDefinition, obj);
					object obj2;
					if (this.TryConvertOutputProperty(obj, providerPropertyDefinition, out obj2))
					{
						obj = obj2;
					}
				}
				catch (DataValidationException arg)
				{
					ExTraceGlobals.ValidationTracer.TraceError<ProviderPropertyDefinition, DataValidationException>(0L, "Calculated property {0} threw an exception {1}. Returning default value.", providerPropertyDefinition, arg);
					obj = this.propertyBag.SetField(providerPropertyDefinition, providerPropertyDefinition.DefaultValue);
					this.propertyBag.ResetChangeTracking(providerPropertyDefinition);
				}
				return obj;
			}
			set
			{
				ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)propertyDefinition;
				this.propertyBag[providerPropertyDefinition] = value;
				this.CleanupInstantiationErrors(providerPropertyDefinition);
			}
		}

		// Token: 0x17000357 RID: 855
		object IPropertyBag.this[PropertyDefinition propertyDefinition]
		{
			get
			{
				return this[propertyDefinition];
			}
			set
			{
				this[propertyDefinition] = value;
			}
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0001FC50 File Offset: 0x0001DE50
		internal void SetProperties(ICollection<PropertyDefinition> propertyDefinitions, object[] propertyValues)
		{
			if (propertyDefinitions == null)
			{
				throw new ArgumentNullException("propertyDefinitions");
			}
			if (this.IsReadOnly && this.propertyBag.ObjectVersionPropertyDefinition != null && this.MaximumSupportedExchangeObjectVersion.IsOlderThan(this.ExchangeVersion))
			{
				throw new InvalidObjectOperationException(DataStrings.ExceptionReadOnlyBecauseTooNew(this.ExchangeVersion, this.MaximumSupportedExchangeObjectVersion));
			}
			ProviderPropertyDefinition[] array = new ProviderPropertyDefinition[propertyDefinitions.Count];
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in propertyDefinitions)
			{
				array[num++] = (ProviderPropertyDefinition)propertyDefinition;
			}
			this.propertyBag.SetProperties(array, propertyValues);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0001FD08 File Offset: 0x0001DF08
		void IPropertyBag.SetProperties(ICollection<PropertyDefinition> propertyDefinitions, object[] propertyValues)
		{
			this.SetProperties(propertyDefinitions, propertyValues);
		}

		// Token: 0x17000358 RID: 856
		object IReadOnlyPropertyBag.this[PropertyDefinition propertyDefinition]
		{
			get
			{
				return this[propertyDefinition];
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001FD1C File Offset: 0x0001DF1C
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitions)
		{
			if (propertyDefinitions == null)
			{
				throw new ArgumentNullException("propertyDefinitions");
			}
			ProviderPropertyDefinition[] array = new ProviderPropertyDefinition[propertyDefinitions.Count];
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in propertyDefinitions)
			{
				array[num++] = (ProviderPropertyDefinition)propertyDefinition;
			}
			return this.propertyBag.GetProperties(array);
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0001FD94 File Offset: 0x0001DF94
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x0001FDAF File Offset: 0x0001DFAF
		internal List<ValidationError> InstantiationErrors
		{
			get
			{
				if (this.instantiationErrors == null)
				{
					this.instantiationErrors = new List<ValidationError>();
				}
				return this.instantiationErrors;
			}
			set
			{
				this.instantiationErrors = value;
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0001FDB8 File Offset: 0x0001DFB8
		internal void EnableSaveCalculatedValues()
		{
			this.propertyBag.EnableSaveCalculatedValues();
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001FDC5 File Offset: 0x0001DFC5
		internal virtual void InitializeSchema()
		{
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001FDC8 File Offset: 0x0001DFC8
		internal IList<PropertyDefinition> GetChangedPropertyDefinitions()
		{
			IEnumerable<PropertyDefinition> enumerable = (this.ObjectSchema == null) ? ((IEnumerable<PropertyDefinition>)this.propertyBag.Keys) : this.ObjectSchema.AllProperties;
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			foreach (PropertyDefinition propertyDefinition in enumerable)
			{
				ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)propertyDefinition;
				if (this.propertyBag.IsChanged(providerPropertyDefinition))
				{
					list.Add(providerPropertyDefinition);
				}
			}
			return list;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001FE50 File Offset: 0x0001E050
		internal ConfigurableObject GetOriginalObject()
		{
			ConfigurableObject configurableObject = (ConfigurableObject)Activator.CreateInstance(base.GetType());
			configurableObject.propertyBag = this.propertyBag.GetOriginalBag();
			return configurableObject;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0001FE80 File Offset: 0x0001E080
		internal void OverrideCorruptedValuesWithDefault()
		{
			ValidationError[] array = this.Validate();
			foreach (ValidationError validationError in array)
			{
				PropertyValidationError propertyValidationError = validationError as PropertyValidationError;
				if (propertyValidationError != null)
				{
					this[propertyValidationError.PropertyDefinition] = (propertyValidationError.PropertyDefinition as ProviderPropertyDefinition).DefaultValue;
				}
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001FED3 File Offset: 0x0001E0D3
		private bool IsChanged()
		{
			return this.propertyBag.Changed;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0001FEE0 File Offset: 0x0001E0E0
		internal bool IsModified(ProviderPropertyDefinition providerPropertyDefinition)
		{
			return this.propertyBag.IsModified(providerPropertyDefinition);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001FEEE File Offset: 0x0001E0EE
		internal bool IsChanged(ProviderPropertyDefinition providerPropertyDefinition)
		{
			return this.propertyBag.IsChanged(providerPropertyDefinition);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0001FEFC File Offset: 0x0001E0FC
		internal static IList<ProviderPropertyDefinition> GetPropertiesThatDiffer(ConfigurableObject objA, ConfigurableObject objB, IList<ProviderPropertyDefinition> properties)
		{
			if (objA == null)
			{
				throw new ArgumentNullException("objA");
			}
			if (objB == null)
			{
				throw new ArgumentNullException("objB");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			List<ProviderPropertyDefinition> list = new List<ProviderPropertyDefinition>();
			for (int i = 0; i < properties.Count; i++)
			{
				ProviderPropertyDefinition providerPropertyDefinition = properties[i];
				if (!object.Equals(objA[providerPropertyDefinition], objB[providerPropertyDefinition]))
				{
					list.Add(providerPropertyDefinition);
				}
			}
			return list;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001FF70 File Offset: 0x0001E170
		internal static bool AreEqual(ConfigurableObject objA, ConfigurableObject objB)
		{
			if (objA == null || objB == null)
			{
				return objA == null && objB == null;
			}
			if (objA.GetType() != objB.GetType())
			{
				return false;
			}
			ObjectSchema objectSchema = objA.ObjectSchema;
			foreach (PropertyDefinition propertyDefinition in objectSchema.AllProperties)
			{
				if (!object.Equals(objA[propertyDefinition], objB[propertyDefinition]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00020004 File Offset: 0x0001E204
		internal virtual bool SkipPiiRedaction
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00020007 File Offset: 0x0001E207
		private static void CheckCallStack()
		{
			if (!Environment.StackTrace.Contains("Clone"))
			{
				throw new NotSupportedException("ConfigurableObjects without schema can only be serialized for cloning.");
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00020028 File Offset: 0x0001E228
		private object InternalSuppressPii(PropertyDefinition propertyDefinition, object value)
		{
			if (SuppressingPiiContext.NeedPiiSuppression)
			{
				bool skipPiiRedaction;
				using (SuppressingPiiContext.Create(false, null))
				{
					skipPiiRedaction = this.SkipPiiRedaction;
				}
				if (!skipPiiRedaction)
				{
					value = SuppressingPiiProperty.TryRedact(propertyDefinition, value, SuppressingPiiContext.PiiMap);
				}
			}
			return value;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002007C File Offset: 0x0001E27C
		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.serializerAssemblyVersion = ConfigurableObject.ExecutingAssemblyVersion;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00020089 File Offset: 0x0001E289
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (this.propertyBag != null && this.DeserializationSchema != null)
			{
				this.propertyBag.SetUpdateErrorsCallback(new UpdateErrorsDelegate(this.UpdateInstantiationErrors));
				this.propertyBag.SetConfigObjectSchema(this.DeserializationSchema.AllProperties);
			}
		}

		// Token: 0x0400062B RID: 1579
		private Version serializerAssemblyVersion;

		// Token: 0x0400062C RID: 1580
		private List<ValidationError> instantiationErrors;

		// Token: 0x0400062D RID: 1581
		internal PropertyBag propertyBag;

		// Token: 0x0400062E RID: 1582
		[NonSerialized]
		private ConvertOutputPropertyDelegate outputPropertyConverter;

		// Token: 0x0400062F RID: 1583
		[NonSerialized]
		private object proxyInfo;

		// Token: 0x04000630 RID: 1584
		internal static Version ExecutingAssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
	}
}
