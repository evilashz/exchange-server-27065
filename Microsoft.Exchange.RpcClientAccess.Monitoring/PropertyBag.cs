using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000046 RID: 70
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PropertyBag : IPropertyBag
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000681A File Offset: 0x00004A1A
		public IEnumerable<ContextProperty> AllProperties
		{
			get
			{
				return this.propertyBag.Keys;
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00006827 File Offset: 0x00004A27
		public bool TryGet(ContextProperty property, out object value)
		{
			if (this.propertyBag.TryGetValue(property, out value))
			{
				return true;
			}
			if (property.TryGetDefaultValue(out value))
			{
				this.propertyBag.Add(property, value);
				return true;
			}
			return false;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006854 File Offset: 0x00004A54
		public void Set(ContextProperty property, object value)
		{
			if (value == null && property.Type.IsValueType && property.Type.IsGenericType && property.Type.GetGenericTypeDefinition() != typeof(Nullable<>))
			{
				throw new InvalidCastException(string.Format("Cannot assign null to a property of a non-nullable value type {0}", property.Type));
			}
			if (value != null && !property.Type.IsAssignableFrom(value.GetType()))
			{
				value = Convert.ChangeType(value, property.Type);
			}
			this.propertyBag[property] = value;
		}

		// Token: 0x040000D4 RID: 212
		private readonly IDictionary<ContextProperty, object> propertyBag = new Dictionary<ContextProperty, object>();
	}
}
