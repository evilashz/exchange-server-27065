using System;
using System.Collections;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	internal class MapiPropertyBag : PropertyBag
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00005002 File Offset: 0x00003202
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return MapiObjectSchema.ExchangeVersion;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00005009 File Offset: 0x00003209
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return MapiObjectSchema.ObjectState;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00005010 File Offset: 0x00003210
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return MapiObjectSchema.Identity;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00005018 File Offset: 0x00003218
		internal bool IsChangedOrInitialized(ProviderPropertyDefinition key)
		{
			if (!base.IsModified(key))
			{
				return false;
			}
			if (base.IsChanged(key))
			{
				return true;
			}
			object obj = null;
			base.TryGetField(key, ref obj);
			return key.DefaultValue == obj;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005050 File Offset: 0x00003250
		internal override MultiValuedPropertyBase CreateMultiValuedProperty(ProviderPropertyDefinition propertyDefinition, bool createAsReadOnly, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage)
		{
			return ADValueConvertor.CreateGenericMultiValuedProperty(propertyDefinition, createAsReadOnly, values, invalidValues, readOnlyErrorMessage);
		}
	}
}
