using System;
using System.Collections;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200020A RID: 522
	[Serializable]
	public class DagNetMultiValuedProperty<T> : MultiValuedProperty<T>
	{
		// Token: 0x06001231 RID: 4657 RVA: 0x000373F4 File Offset: 0x000355F4
		internal DagNetMultiValuedProperty(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage) : base(readOnly, propertyDefinition, values, invalidValues, readOnlyErrorMessage)
		{
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00037403 File Offset: 0x00035603
		internal DagNetMultiValuedProperty(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values) : base(readOnly, propertyDefinition, values)
		{
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0003740E File Offset: 0x0003560E
		public DagNetMultiValuedProperty(ICollection values) : base(values)
		{
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00037417 File Offset: 0x00035617
		public DagNetMultiValuedProperty(object value) : base(value)
		{
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00037420 File Offset: 0x00035620
		public DagNetMultiValuedProperty()
		{
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00037428 File Offset: 0x00035628
		public new static implicit operator DagNetMultiValuedProperty<T>(object[] array)
		{
			if (array == null)
			{
				return null;
			}
			return new DagNetMultiValuedProperty<T>(true, null, array);
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00037437 File Offset: 0x00035637
		protected override object SerializeValue(object value)
		{
			return value;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0003743A File Offset: 0x0003563A
		protected override object DeserializeValue(object value)
		{
			return value;
		}
	}
}
