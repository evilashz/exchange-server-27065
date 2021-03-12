using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EB7 RID: 3767
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class StoreByteArrayLengthConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x17002286 RID: 8838
		// (get) Token: 0x06008251 RID: 33361 RVA: 0x002394A0 File Offset: 0x002376A0
		public int MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x06008252 RID: 33362 RVA: 0x002394A8 File Offset: 0x002376A8
		public StoreByteArrayLengthConstraint(int maxLength)
		{
			if (maxLength <= 0)
			{
				throw new ArgumentException("maxLength must be greater than 0", "maxLength");
			}
			this.maxLength = maxLength;
		}

		// Token: 0x06008253 RID: 33363 RVA: 0x002394CC File Offset: 0x002376CC
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			byte[] array = value as byte[];
			if (array != null && array.Length > this.maxLength)
			{
				return new PropertyConstraintViolationError(new LocalizedString(ServerStrings.ExConstraintViolationByteArrayLengthTooLong(propertyDefinition.Name, (long)this.maxLength, (long)array.Length)), propertyDefinition, value, this);
			}
			return null;
		}

		// Token: 0x04005774 RID: 22388
		private int maxLength;
	}
}
