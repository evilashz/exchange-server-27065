using System;
using System.Collections;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200025B RID: 603
	[Serializable]
	public abstract class ProtocolAddressCollection<T> : MultiValuedProperty<T> where T : ProtocolAddress
	{
		// Token: 0x0600144F RID: 5199 RVA: 0x0003FE8A File Offset: 0x0003E08A
		public ProtocolAddressCollection()
		{
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0003FE92 File Offset: 0x0003E092
		public ProtocolAddressCollection(object value) : base(value)
		{
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0003FE9B File Offset: 0x0003E09B
		public ProtocolAddressCollection(ICollection values) : base(values)
		{
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0003FEA4 File Offset: 0x0003E0A4
		public ProtocolAddressCollection(Hashtable table) : base(table)
		{
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0003FEAD File Offset: 0x0003E0AD
		internal ProtocolAddressCollection(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values) : base(readOnly, propertyDefinition, values)
		{
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0003FEB8 File Offset: 0x0003E0B8
		internal ProtocolAddressCollection(bool readOnly, ProviderPropertyDefinition propertyDefinition, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage) : base(readOnly, propertyDefinition, values, invalidValues, readOnlyErrorMessage)
		{
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0003FEC8 File Offset: 0x0003E0C8
		public string[] ToStringArray()
		{
			string[] array = new string[this.Count];
			for (int i = 0; i < this.Count; i++)
			{
				string[] array2 = array;
				int num = i;
				T t = base[i];
				array2[num] = t.ToString();
			}
			return array;
		}
	}
}
