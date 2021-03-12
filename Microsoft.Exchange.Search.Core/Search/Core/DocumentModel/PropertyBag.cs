using System;
using System.Collections.Generic;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.DocumentModel
{
	// Token: 0x020000A3 RID: 163
	internal class PropertyBag : IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x0000F271 File Offset: 0x0000D471
		internal PropertyBag()
		{
			this.values = new Dictionary<PropertyDefinition, object>(128);
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0000F289 File Offset: 0x0000D489
		public IEnumerable<KeyValuePair<PropertyDefinition, object>> Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000F294 File Offset: 0x0000D494
		public TPropertyValue GetProperty<TPropertyValue>(PropertyDefinition property)
		{
			object obj;
			if (!this.TryGetProperty(property, out obj))
			{
				throw new PropertyErrorException(property.Name);
			}
			if (!typeof(TPropertyValue).IsAssignableFrom(obj.GetType()))
			{
				throw new PropertyTypeErrorException(property.Name);
			}
			return (TPropertyValue)((object)obj);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000F2E1 File Offset: 0x0000D4E1
		public bool TryGetProperty(PropertyDefinition property, out object value)
		{
			Util.ThrowOnNullArgument(property, "property");
			return this.values.TryGetValue(property, out value);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000F2FB File Offset: 0x0000D4FB
		public void SetProperty<TPropertyValue>(PropertyDefinition property, TPropertyValue value)
		{
			this.InternalValidateSetProperty(property, value);
			this.InternalValidatePropertyValueType(property, typeof(TPropertyValue));
			this.values[property] = value;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000F32D File Offset: 0x0000D52D
		public void SetProperty(PropertyDefinition property, object value)
		{
			this.InternalValidateSetProperty(property, value);
			this.InternalValidatePropertyValueType(property, value.GetType());
			this.values[property] = value;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000F351 File Offset: 0x0000D551
		private void InternalValidateSetProperty(PropertyDefinition property, object value)
		{
			Util.ThrowOnNullArgument(property, "property");
			Util.ThrowOnNullArgument(value, "value");
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000F36C File Offset: 0x0000D56C
		private void InternalValidatePropertyValueType(PropertyDefinition property, Type valueType)
		{
			Type type = property.Type;
			if (!type.Equals(valueType) && !type.IsAssignableFrom(valueType))
			{
				throw new ArgumentException();
			}
		}

		// Token: 0x04000241 RID: 577
		private const int IntialCapacity = 128;

		// Token: 0x04000242 RID: 578
		private Dictionary<PropertyDefinition, object> values;
	}
}
