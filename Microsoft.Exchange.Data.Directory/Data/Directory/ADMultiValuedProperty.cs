using System;
using System.Collections;
using System.ComponentModel;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200002E RID: 46
	[TypeConverter(typeof(SimpleGenericsTypeConverter))]
	[Serializable]
	public class ADMultiValuedProperty<T> : MultiValuedProperty<T>
	{
		// Token: 0x060002DD RID: 733 RVA: 0x0000FC94 File Offset: 0x0000DE94
		internal ADMultiValuedProperty(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage) : base(readOnly, propertyDefinition, values, invalidValues, readOnlyErrorMessage)
		{
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000FCA3 File Offset: 0x0000DEA3
		internal ADMultiValuedProperty(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values) : base(readOnly, propertyDefinition, values)
		{
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000FCAE File Offset: 0x0000DEAE
		public ADMultiValuedProperty(ICollection values) : base(values)
		{
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000FCB7 File Offset: 0x0000DEB7
		public ADMultiValuedProperty(object value) : base(value)
		{
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
		public ADMultiValuedProperty()
		{
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000FCC8 File Offset: 0x0000DEC8
		protected override object SerializeValue(object value)
		{
			return ADValueConvertor.SerializeData((ADPropertyDefinition)this.PropertyDefinition, value);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000FCDB File Offset: 0x0000DEDB
		protected override object DeserializeValue(object value)
		{
			return ADValueConvertor.DeserializeData((ADPropertyDefinition)this.PropertyDefinition, value);
		}
	}
}
