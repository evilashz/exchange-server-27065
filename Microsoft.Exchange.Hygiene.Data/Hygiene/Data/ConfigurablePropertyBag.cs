using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000003 RID: 3
	internal abstract class ConfigurablePropertyBag : IPropertyBag, IReadOnlyPropertyBag, IConfigurable
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19
		public abstract ObjectId Identity { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002323 File Offset: 0x00000523
		public bool IsValid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000232A File Offset: 0x0000052A
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002332 File Offset: 0x00000532
		public virtual ObjectState ObjectState { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000233B File Offset: 0x0000053B
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002348 File Offset: 0x00000548
		public object PhysicalInstanceID
		{
			get
			{
				return this[ConfigurablePropertyBag.PhysicalInstanceKeyProp];
			}
			set
			{
				this[ConfigurablePropertyBag.PhysicalInstanceKeyProp] = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002358 File Offset: 0x00000558
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000237D File Offset: 0x0000057D
		internal IPropertyBag PropertyBag
		{
			get
			{
				IPropertyBag result;
				if ((result = this.propertyBag) == null)
				{
					result = (this.propertyBag = new ConfigurablePropertyBag.DictionaryBasedPropertyBag());
				}
				return result;
			}
			set
			{
				this.propertyBag = value;
			}
		}

		// Token: 0x1700000B RID: 11
		public virtual object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				return this.PropertyBag[propertyDefinition];
			}
			set
			{
				this.PropertyBag[propertyDefinition] = value;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023E4 File Offset: 0x000005E4
		public virtual IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			if (isChangedOnly)
			{
				return ((Dictionary<PropertyDefinition, object>)this.PropertyBag).Keys;
			}
			return ConfigurablePropertyBag.propertyDefinitions.GetOrAdd(this.GetSchemaType(), (Type type) => (from field in type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
			select field.GetValue(null)).OfType<PropertyDefinition>().ToArray<PropertyDefinition>());
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002432 File Offset: 0x00000632
		public virtual Type GetSchemaType()
		{
			return base.GetType();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000243A File Offset: 0x0000063A
		public ValidationError[] Validate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002441 File Offset: 0x00000641
		public void SetProperties(ICollection<PropertyDefinition> propertyDefinitionArray, object[] propertyValuesArray)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002448 File Offset: 0x00000648
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000244F File Offset: 0x0000064F
		public void CopyChangesFrom(IConfigurable source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002456 File Offset: 0x00000656
		public void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000245D File Offset: 0x0000065D
		public bool TryGetValue(PropertyDefinition property, out object value)
		{
			return ((Dictionary<PropertyDefinition, object>)this.PropertyBag).TryGetValue(property, out value);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002471 File Offset: 0x00000671
		public virtual void Expand()
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002473 File Offset: 0x00000673
		public virtual void Collapse()
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002475 File Offset: 0x00000675
		public void RemoveProperty(PropertyDefinition propertyDefinition)
		{
			((Dictionary<PropertyDefinition, object>)this.PropertyBag).Remove(propertyDefinition);
		}

		// Token: 0x04000002 RID: 2
		public static readonly HygienePropertyDefinition PhysicalInstanceKeyProp = DalHelper.PhysicalInstanceKeyProp;

		// Token: 0x04000003 RID: 3
		public static readonly HygienePropertyDefinition FssCopyIdProp = DalHelper.FssCopyIdProp;

		// Token: 0x04000004 RID: 4
		private static readonly ConcurrentDictionary<Type, PropertyDefinition[]> propertyDefinitions = new ConcurrentDictionary<Type, PropertyDefinition[]>();

		// Token: 0x04000005 RID: 5
		private IPropertyBag propertyBag;

		// Token: 0x02000004 RID: 4
		private class DictionaryBasedPropertyBag : Dictionary<PropertyDefinition, object>, IPropertyBag, IReadOnlyPropertyBag
		{
			// Token: 0x1700000C RID: 12
			public new object this[PropertyDefinition propertyDefinition]
			{
				get
				{
					object result;
					if (base.TryGetValue(propertyDefinition, out result))
					{
						return result;
					}
					if (propertyDefinition is ProviderPropertyDefinition && ((ProviderPropertyDefinition)propertyDefinition).IsMultivalued)
					{
						return null;
					}
					if (!propertyDefinition.Type.IsValueType)
					{
						return null;
					}
					return Activator.CreateInstance(propertyDefinition.Type);
				}
				set
				{
					base[propertyDefinition] = value;
				}
			}

			// Token: 0x0600002E RID: 46 RVA: 0x00002509 File Offset: 0x00000709
			public void SetProperties(ICollection<PropertyDefinition> propertyDefinitionArray, object[] propertyValuesArray)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600002F RID: 47 RVA: 0x00002510 File Offset: 0x00000710
			public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
			{
				throw new NotImplementedException();
			}
		}
	}
}
