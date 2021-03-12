using System;
using System.Collections;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200005A RID: 90
	[Serializable]
	internal class ADPropertyBag : PropertyBag
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x0001945D File Offset: 0x0001765D
		public ADPropertyBag(bool readOnly, int initialSize) : base(readOnly, initialSize)
		{
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00019467 File Offset: 0x00017667
		public ADPropertyBag() : base(false, 16)
		{
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00019472 File Offset: 0x00017672
		internal override ProviderPropertyDefinition ObjectVersionPropertyDefinition
		{
			get
			{
				return ADObjectSchema.ExchangeVersion;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00019479 File Offset: 0x00017679
		internal override ProviderPropertyDefinition ObjectStatePropertyDefinition
		{
			get
			{
				return ADObjectSchema.ObjectState;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00019480 File Offset: 0x00017680
		internal override ProviderPropertyDefinition ObjectIdentityPropertyDefinition
		{
			get
			{
				return ADObjectSchema.Id;
			}
		}

		// Token: 0x170000F2 RID: 242
		public override object this[PropertyDefinition key]
		{
			get
			{
				return base[key];
			}
			set
			{
				base[key] = value;
				if (key != ADObjectSchema.RawName)
				{
					if (key == ADObjectSchema.Id)
					{
						ADObjectId adobjectId = (ADObjectId)this[ADObjectSchema.Id];
						base.SetField(ADObjectSchema.RawName, adobjectId.Rdn.UnescapedName);
					}
					return;
				}
				ADObjectId adobjectId2 = (ADObjectId)this[ADObjectSchema.Id];
				if (adobjectId2 == null)
				{
					return;
				}
				string prefix = adobjectId2.Rdn.Prefix;
				ADObjectId childId = adobjectId2.Parent.GetChildId(prefix, (string)value);
				base.SetField(ADObjectSchema.Id, new ADObjectId(childId.DistinguishedName, adobjectId2.ObjectGuid));
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001952F File Offset: 0x0001772F
		internal override MultiValuedPropertyBase CreateMultiValuedProperty(ProviderPropertyDefinition propertyDefinition, bool createAsReadOnly, ICollection values, ICollection invalidValues, LocalizedString? readOnlyErrorMessage)
		{
			return ADValueConvertor.CreateGenericMultiValuedProperty(propertyDefinition, createAsReadOnly, values, invalidValues, readOnlyErrorMessage);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001953D File Offset: 0x0001773D
		internal override object SerializeData(ProviderPropertyDefinition propertyDefinition, object input)
		{
			return ADValueConvertor.SerializeData(propertyDefinition, input);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00019546 File Offset: 0x00017746
		internal override object DeserializeData(ProviderPropertyDefinition propertyDefinition, object input)
		{
			return ADValueConvertor.DeserializeData(propertyDefinition, input);
		}
	}
}
