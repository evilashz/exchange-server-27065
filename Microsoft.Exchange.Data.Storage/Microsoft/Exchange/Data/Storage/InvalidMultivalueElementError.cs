using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EAE RID: 3758
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class InvalidMultivalueElementError : PropertyValidationError
	{
		// Token: 0x06008232 RID: 33330 RVA: 0x00238E7E File Offset: 0x0023707E
		internal InvalidMultivalueElementError(PropertyValidationError elementError, object invalidMultivalueData, int invalidElementIndex) : base(new LocalizedString(ServerStrings.ExInvalidMultivalueElement(invalidElementIndex)), elementError.PropertyDefinition, invalidMultivalueData)
		{
			this.elementError = elementError;
			this.invalidElementIndex = invalidElementIndex;
		}

		// Token: 0x1700227E RID: 8830
		// (get) Token: 0x06008233 RID: 33331 RVA: 0x00238EAB File Offset: 0x002370AB
		public int InvalidElementIndex
		{
			get
			{
				return this.invalidElementIndex;
			}
		}

		// Token: 0x1700227F RID: 8831
		// (get) Token: 0x06008234 RID: 33332 RVA: 0x00238EB3 File Offset: 0x002370B3
		public PropertyValidationError InvalidElementError
		{
			get
			{
				return this.elementError;
			}
		}

		// Token: 0x06008235 RID: 33333 RVA: 0x00238EBB File Offset: 0x002370BB
		public override string ToString()
		{
			return string.Format("The element with index {0} of the multivalue property {1} is invalid. Invalid data = {2}.", this.invalidElementIndex, base.PropertyDefinition, base.InvalidData);
		}

		// Token: 0x0400576C RID: 22380
		private readonly int invalidElementIndex;

		// Token: 0x0400576D RID: 22381
		private readonly PropertyValidationError elementError;
	}
}
