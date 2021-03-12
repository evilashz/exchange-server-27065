using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E28 RID: 3624
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SinglePropertyBag : IReadOnlyPropertyBag
	{
		// Token: 0x06007D6F RID: 32111 RVA: 0x002299BF File Offset: 0x00227BBF
		public SinglePropertyBag(PropertyDefinition property, object propertyValue)
		{
			this.property = property;
			this.propertyValue = propertyValue;
		}

		// Token: 0x17002185 RID: 8581
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				if (propertyDefinition == this.property)
				{
					return this.propertyValue;
				}
				return new PropertyError(propertyDefinition, PropertyErrorCode.NotFound);
			}
		}

		// Token: 0x06007D71 RID: 32113 RVA: 0x002299EE File Offset: 0x00227BEE
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06007D72 RID: 32114 RVA: 0x002299F5 File Offset: 0x00227BF5
		public override string ToString()
		{
			return string.Format("{0}={1}", this.property, this.propertyValue);
		}

		// Token: 0x04005585 RID: 21893
		private PropertyDefinition property;

		// Token: 0x04005586 RID: 21894
		private object propertyValue;
	}
}
