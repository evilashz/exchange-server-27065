using System;
using System.Collections;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200012B RID: 299
	[Serializable]
	internal class CollectionPropertyMaxCountConstraint : CollectionPropertyDefinitionConstraint
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x00020BFE File Offset: 0x0001EDFE
		public CollectionPropertyMaxCountConstraint(int maxCount)
		{
			if (maxCount < 0)
			{
				throw new ArgumentOutOfRangeException("maxCount");
			}
			this.maxCount = maxCount;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00020C1C File Offset: 0x0001EE1C
		public override PropertyConstraintViolationError Validate(IEnumerable value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			ICollection collection = value as ICollection;
			int num = 0;
			if (collection != null)
			{
				num = collection.Count;
			}
			else
			{
				IEnumerator enumerator = value.GetEnumerator();
				while (enumerator.MoveNext())
				{
					num++;
				}
			}
			if (num > this.maxCount)
			{
				return new PropertyConstraintViolationError(DataStrings.CollectiionWithTooManyItemsFormat(this.maxCount.ToString()), propertyDefinition, value, this);
			}
			return null;
		}

		// Token: 0x04000660 RID: 1632
		private int maxCount;
	}
}
