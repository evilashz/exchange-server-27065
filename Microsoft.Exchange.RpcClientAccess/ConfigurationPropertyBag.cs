using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ConfigurationPropertyBag
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000029E8 File Offset: 0x00000BE8
		internal ConfigurationPropertyBag(ConfigurationPropertyBag previousConfiguration, IDictionary<ConfigurationSchema.Property, object> overrides)
		{
			if (previousConfiguration != null)
			{
				this.propertyStore = new Dictionary<ConfigurationSchema.Property, object>(previousConfiguration.propertyStore);
				this.isValid = previousConfiguration.isValid;
			}
			else
			{
				this.propertyStore = new Dictionary<ConfigurationSchema.Property, object>();
			}
			if (overrides != null)
			{
				foreach (KeyValuePair<ConfigurationSchema.Property, object> keyValuePair in overrides)
				{
					this.propertyStore[keyValuePair.Key] = keyValuePair.Value;
				}
				this.protectedProperties = overrides.Keys;
				return;
			}
			this.protectedProperties = Array<ConfigurationSchema.Property>.Empty;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002A98 File Offset: 0x00000C98
		internal bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AA0 File Offset: 0x00000CA0
		internal void Delete(IEnumerable<ConfigurationSchema.Property> properties)
		{
			this.EnsureNotFrozen();
			foreach (ConfigurationSchema.Property property in properties)
			{
				if (!this.protectedProperties.Contains(property))
				{
					this.propertyStore.Remove(property);
				}
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B04 File Offset: 0x00000D04
		internal void Freeze()
		{
			this.isFrozen = true;
			this.protectedProperties = null;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B14 File Offset: 0x00000D14
		internal TValue Get<TValue>(ConfigurationSchema.Property<TValue> property)
		{
			object obj;
			if (!this.propertyStore.TryGetValue(property, out obj))
			{
				return property.DefaultValue;
			}
			return (TValue)((object)obj);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B3E File Offset: 0x00000D3E
		internal void MarkInvalid()
		{
			this.isValid = false;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B47 File Offset: 0x00000D47
		internal void Set<TValue>(ConfigurationSchema.Property<TValue> property, TValue value)
		{
			this.EnsureNotFrozen();
			if (!this.protectedProperties.Contains(property))
			{
				this.propertyStore[property] = value;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B6F File Offset: 0x00000D6F
		private void EnsureNotFrozen()
		{
			if (this.isFrozen)
			{
				throw new InvalidOperationException("ConfigurationPropertyBag cannot be modified once it's frozen");
			}
		}

		// Token: 0x0400001F RID: 31
		private readonly Dictionary<ConfigurationSchema.Property, object> propertyStore;

		// Token: 0x04000020 RID: 32
		private ICollection<ConfigurationSchema.Property> protectedProperties;

		// Token: 0x04000021 RID: 33
		private bool isFrozen;

		// Token: 0x04000022 RID: 34
		private bool isValid = true;
	}
}
