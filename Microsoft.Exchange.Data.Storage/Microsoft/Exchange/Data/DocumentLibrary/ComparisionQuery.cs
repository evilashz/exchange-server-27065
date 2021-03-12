using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006B0 RID: 1712
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ComparisionQuery<GPropType> : SinglePropertyQuery
	{
		// Token: 0x0600454E RID: 17742 RVA: 0x00127240 File Offset: 0x00125440
		protected ComparisionQuery(int index, GPropType propValue) : base(index)
		{
			if (propValue == null)
			{
				throw new ArgumentNullException("invalid propValue");
			}
			this.PropValue = propValue;
		}

		// Token: 0x040025F5 RID: 9717
		protected readonly GPropType PropValue;
	}
}
