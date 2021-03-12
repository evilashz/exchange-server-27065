using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000456 RID: 1110
	[Serializable]
	public class EwsStoreObject : ConfigurableObject
	{
		// Token: 0x06003147 RID: 12615 RVA: 0x000C9A38 File Offset: 0x000C7C38
		public EwsStoreObject() : base(new SimplePropertyBag(EwsStoreObjectSchema.Identity, EwsStoreObjectSchema.ObjectState, EwsStoreObjectSchema.ExchangeVersion))
		{
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06003148 RID: 12616 RVA: 0x000C9A54 File Offset: 0x000C7C54
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return EwsStoreObject.schema;
			}
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06003149 RID: 12617 RVA: 0x000C9A5B File Offset: 0x000C7C5B
		internal virtual string ItemClass
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x0600314A RID: 12618 RVA: 0x000C9A5E File Offset: 0x000C7C5E
		// (set) Token: 0x0600314B RID: 12619 RVA: 0x000C9A70 File Offset: 0x000C7C70
		internal Guid? PolicyTag
		{
			get
			{
				return (Guid?)this[EwsStoreObjectSchema.PolicyTag];
			}
			set
			{
				this[EwsStoreObjectSchema.PolicyTag] = value;
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x0600314C RID: 12620 RVA: 0x000C9A83 File Offset: 0x000C7C83
		// (set) Token: 0x0600314D RID: 12621 RVA: 0x000C9A95 File Offset: 0x000C7C95
		public string AlternativeId
		{
			get
			{
				return (string)this[EwsStoreObjectSchema.AlternativeId];
			}
			set
			{
				this[EwsStoreObjectSchema.AlternativeId] = value;
			}
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x0600314E RID: 12622 RVA: 0x000C9AA3 File Offset: 0x000C7CA3
		public new EwsStoreObjectId Identity
		{
			get
			{
				return (EwsStoreObjectId)base.Identity;
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x0600314F RID: 12623 RVA: 0x000C9AB0 File Offset: 0x000C7CB0
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x000C9AB7 File Offset: 0x000C7CB7
		internal virtual SearchFilter ItemClassFilter
		{
			get
			{
				if (!string.IsNullOrEmpty(this.ItemClass))
				{
					return new SearchFilter.IsEqualTo(ItemSchema.ItemClass, this.ItemClass);
				}
				return null;
			}
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x000C9AD8 File Offset: 0x000C7CD8
		internal virtual SearchFilter VersioningFilter
		{
			get
			{
				return new SearchFilter.Not(new SearchFilter.SearchFilterCollection(0, new SearchFilter[]
				{
					new SearchFilter.IsGreaterThanOrEqualTo(ExtendedEwsStoreObjectSchema.ExchangeVersion, this.MaximumSupportedExchangeObjectVersion.NextMajorVersion.ToInt64()),
					new SearchFilter.Exists(ExtendedEwsStoreObjectSchema.ExchangeVersion)
				}));
			}
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x000C9B28 File Offset: 0x000C7D28
		internal void CopyFromItemObject(Item item, ExchangeVersion ewsVersion)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			base.InstantiationErrors.Clear();
			foreach (PropertyDefinition propertyDefinition in this.ObjectSchema.AllProperties)
			{
				if (propertyDefinition != EwsStoreObjectSchema.ExchangeVersion)
				{
					EwsStoreObjectPropertyDefinition ewsStoreObjectPropertyDefinition = propertyDefinition as EwsStoreObjectPropertyDefinition;
					if (ewsStoreObjectPropertyDefinition != null)
					{
						object obj = null;
						if (ewsStoreObjectPropertyDefinition.GetItemProperty(item, out obj))
						{
							if (ewsStoreObjectPropertyDefinition.StorePropertyDefinition.Version > ewsVersion)
							{
								ExTraceGlobals.StorageTracer.TraceDebug(0L, "Skip loading property '{0}.{1}' because the current EWS version '{2}' is lower than '{3}'.", new object[]
								{
									base.GetType().FullName,
									ewsStoreObjectPropertyDefinition.Name,
									ewsVersion,
									ewsStoreObjectPropertyDefinition.StorePropertyDefinition.Version
								});
							}
							else
							{
								Exception ex = null;
								try
								{
									obj = EwsStoreValueConverter.ConvertValueFromStore(ewsStoreObjectPropertyDefinition, obj);
									this.propertyBag.SetField(ewsStoreObjectPropertyDefinition, obj);
									base.InstantiationErrors.AddRange(ewsStoreObjectPropertyDefinition.ValidateProperty(obj, this.propertyBag, true));
								}
								catch (LocalizedException ex2)
								{
									base.InstantiationErrors.Add(new PropertyValidationError(ex2.LocalizedString, ewsStoreObjectPropertyDefinition, obj));
								}
								catch (InvalidCastException ex3)
								{
									ex = ex3;
								}
								catch (FormatException ex4)
								{
									ex = ex4;
								}
								catch (SerializationException ex5)
								{
									ex = ex5;
								}
								if (ex != null)
								{
									base.InstantiationErrors.Add(new PropertyValidationError(new LocalizedString(ex.Message), ewsStoreObjectPropertyDefinition, obj));
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000C9D18 File Offset: 0x000C7F18
		internal void CopyChangeToItemObject(Item item, ExchangeVersion ewsVersion)
		{
			if (base.ObjectState == ObjectState.Unchanged)
			{
				return;
			}
			if (item.IsNew && !string.IsNullOrEmpty(this.ItemClass))
			{
				EwsStoreObjectSchema.ItemClass.SetItemProperty(item, this.ItemClass);
			}
			foreach (PropertyDefinition propertyDefinition in this.ObjectSchema.AllProperties)
			{
				EwsStoreObjectPropertyDefinition ewsStoreObjectPropertyDefinition = propertyDefinition as EwsStoreObjectPropertyDefinition;
				if (ewsStoreObjectPropertyDefinition != null && !ewsStoreObjectPropertyDefinition.IsReadOnly && !ewsStoreObjectPropertyDefinition.IsCalculated && (base.ObjectState == ObjectState.New || base.IsChanged(ewsStoreObjectPropertyDefinition)))
				{
					if (ewsStoreObjectPropertyDefinition.StorePropertyDefinition.Version > ewsVersion && !ewsStoreObjectPropertyDefinition.IsMandatory)
					{
						ExTraceGlobals.StorageTracer.TraceDebug(0L, "Skip saving property '{0}.{1}' because the current EWS version '{2}' is lower than '{3}'.", new object[]
						{
							base.GetType().FullName,
							ewsStoreObjectPropertyDefinition.Name,
							ewsVersion,
							ewsStoreObjectPropertyDefinition.StorePropertyDefinition.Version
						});
					}
					else
					{
						object obj = this[ewsStoreObjectPropertyDefinition];
						if (obj == ewsStoreObjectPropertyDefinition.DefaultValue && !ewsStoreObjectPropertyDefinition.PersistDefaultValue)
						{
							if (base.ObjectState == ObjectState.New)
							{
								continue;
							}
							obj = null;
						}
						if (obj != null)
						{
							obj = EwsStoreValueConverter.ConvertValueToStore(ewsStoreObjectPropertyDefinition, obj);
						}
						ewsStoreObjectPropertyDefinition.SetItemProperty(item, obj);
					}
				}
			}
		}

		// Token: 0x04001AA9 RID: 6825
		private static ObjectSchema schema = new EwsStoreObjectSchema();
	}
}
