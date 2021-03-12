using System;
using System.Collections;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A83 RID: 2691
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SimplePropertyBag : PropertyBag
	{
		// Token: 0x0600627C RID: 25212 RVA: 0x001A01BE File Offset: 0x0019E3BE
		public SimplePropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
		}

		// Token: 0x0600627D RID: 25213 RVA: 0x001A01C8 File Offset: 0x0019E3C8
		public SimplePropertyBag(ProviderPropertyDefinition idPropDef, ProviderPropertyDefinition statePropDef, ProviderPropertyDefinition verPropDef, bool readOnly, int initialSize) : this(readOnly, initialSize)
		{
			this.objectIdentityPropertyDefinition = idPropDef;
			this.objectStatePropertyDefinition = statePropDef;
			this.objectVersionPropertyDefinition = verPropDef;
		}

		// Token: 0x0600627E RID: 25214 RVA: 0x001A01E9 File Offset: 0x0019E3E9
		public SimplePropertyBag()
		{
		}

		// Token: 0x0600627F RID: 25215 RVA: 0x001A01F1 File Offset: 0x0019E3F1
		public SimplePropertyBag(ProviderPropertyDefinition idPropDef, ProviderPropertyDefinition statePropDef, ProviderPropertyDefinition verPropDef) : this()
		{
			this.objectIdentityPropertyDefinition = idPropDef;
			this.objectStatePropertyDefinition = statePropDef;
			this.objectVersionPropertyDefinition = verPropDef;
		}

		// Token: 0x17001B43 RID: 6979
		// (get) Token: 0x06006280 RID: 25216 RVA: 0x001A020E File Offset: 0x0019E40E
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return this.objectVersionPropertyDefinition;
			}
		}

		// Token: 0x17001B44 RID: 6980
		// (get) Token: 0x06006281 RID: 25217 RVA: 0x001A0216 File Offset: 0x0019E416
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return this.objectStatePropertyDefinition;
			}
		}

		// Token: 0x17001B45 RID: 6981
		// (get) Token: 0x06006282 RID: 25218 RVA: 0x001A021E File Offset: 0x0019E41E
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return this.objectIdentityPropertyDefinition;
			}
		}

		// Token: 0x06006283 RID: 25219 RVA: 0x001A0226 File Offset: 0x0019E426
		public void SetObjectIdentityPropertyDefinition(ProviderPropertyDefinition idPd)
		{
			this.objectIdentityPropertyDefinition = idPd;
		}

		// Token: 0x06006284 RID: 25220 RVA: 0x001A022F File Offset: 0x0019E42F
		public void SetObjectStatePropertyDefinition(ProviderPropertyDefinition statePd)
		{
			this.objectStatePropertyDefinition = statePd;
		}

		// Token: 0x06006285 RID: 25221 RVA: 0x001A0238 File Offset: 0x0019E438
		public void SetObjectVersionPropertyDefinition(ProviderPropertyDefinition verPd)
		{
			this.objectVersionPropertyDefinition = verPd;
		}

		// Token: 0x06006286 RID: 25222 RVA: 0x001A0241 File Offset: 0x0019E441
		internal override MultiValuedPropertyBase CreateMultiValuedProperty(ProviderPropertyDefinition propertyDefinition, bool createAsReadOnly, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage)
		{
			return StoreValueConverter.CreateGenericMultiValuedProperty(propertyDefinition, createAsReadOnly, values, invalidValues, readOnlyErrorMessage);
		}

		// Token: 0x040037C8 RID: 14280
		private ProviderPropertyDefinition objectIdentityPropertyDefinition;

		// Token: 0x040037C9 RID: 14281
		private ProviderPropertyDefinition objectStatePropertyDefinition;

		// Token: 0x040037CA RID: 14282
		private ProviderPropertyDefinition objectVersionPropertyDefinition;
	}
}
