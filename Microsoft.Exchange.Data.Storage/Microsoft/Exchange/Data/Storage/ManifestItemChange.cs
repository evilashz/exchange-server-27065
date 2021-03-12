using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200083F RID: 2111
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ManifestItemChange : ManifestChangeBase
	{
		// Token: 0x06004E66 RID: 20070 RVA: 0x001487E4 File Offset: 0x001469E4
		internal ManifestItemChange(bool isNewItem, PropValue[] headerPropertyValues, PropValue[] propertyValues)
		{
			this.isNewItem = isNewItem;
			this.propertyValues = Util.MergeArrays<PropValue>(new ICollection<PropValue>[]
			{
				headerPropertyValues,
				propertyValues
			});
		}

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x06004E67 RID: 20071 RVA: 0x00148819 File Offset: 0x00146A19
		public bool IsNewItem
		{
			get
			{
				return this.isNewItem;
			}
		}

		// Token: 0x17001631 RID: 5681
		// (get) Token: 0x06004E68 RID: 20072 RVA: 0x00148821 File Offset: 0x00146A21
		public PropValue[] PropertyValues
		{
			get
			{
				return this.propertyValues;
			}
		}

		// Token: 0x04002AC5 RID: 10949
		private readonly bool isNewItem;

		// Token: 0x04002AC6 RID: 10950
		private readonly PropValue[] propertyValues;
	}
}
