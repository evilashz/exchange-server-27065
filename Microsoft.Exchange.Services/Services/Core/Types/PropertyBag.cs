using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000613 RID: 1555
	internal class PropertyBag
	{
		// Token: 0x060030C5 RID: 12485 RVA: 0x000B6952 File Offset: 0x000B4B52
		internal PropertyBag()
		{
			this.propertyBag = new Dictionary<PropertyInformation, object>();
		}

		// Token: 0x17000ABB RID: 2747
		internal object this[PropertyInformation property]
		{
			get
			{
				return this.propertyBag[property];
			}
			set
			{
				if (value == null)
				{
					if (this.propertyBag.ContainsKey(property))
					{
						this.propertyBag.Remove(property);
						return;
					}
				}
				else
				{
					this.propertyBag[property] = value;
				}
			}
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x000B69A1 File Offset: 0x000B4BA1
		internal bool Contains(PropertyInformation property)
		{
			return this.propertyBag.ContainsKey(property);
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x000B69AF File Offset: 0x000B4BAF
		internal bool Remove(PropertyInformation property)
		{
			return this.propertyBag.Remove(property);
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x000B69BD File Offset: 0x000B4BBD
		internal void Clear()
		{
			this.propertyBag.Clear();
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060030CB RID: 12491 RVA: 0x000B69CA File Offset: 0x000B4BCA
		internal List<PropertyInformation> LoadedProperties
		{
			get
			{
				return this.propertyBag.Keys.ToList<PropertyInformation>();
			}
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000B69DC File Offset: 0x000B4BDC
		internal T GetValueOrDefault<T>(PropertyInformation property, T defaultValue)
		{
			object obj;
			if (this.propertyBag.TryGetValue(property, out obj))
			{
				return (T)((object)obj);
			}
			return defaultValue;
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000B6A04 File Offset: 0x000B4C04
		internal T GetValueOrDefault<T>(PropertyInformation property)
		{
			return this.GetValueOrDefault<T>(property, default(T));
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x000B6A24 File Offset: 0x000B4C24
		internal T? GetNullableValue<T>(PropertyInformation property) where T : struct
		{
			object obj;
			if (this.propertyBag.TryGetValue(property, out obj))
			{
				return new T?((T)((object)obj));
			}
			return null;
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x000B6A56 File Offset: 0x000B4C56
		internal void SetNullableValue<T>(PropertyInformation property, T? value) where T : struct
		{
			if (value != null)
			{
				this[property] = value.Value;
				return;
			}
			this.Remove(property);
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x000B6A80 File Offset: 0x000B4C80
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<PropertyInformation, object> keyValuePair in this.propertyBag)
			{
				stringBuilder.Append(string.Format("{0} : {1}\n", keyValuePair.Key.LocalName, (keyValuePair.Value == null) ? "null" : keyValuePair.Value.ToString()));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001BFE RID: 7166
		private Dictionary<PropertyInformation, object> propertyBag;
	}
}
