using System;
using System.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B84 RID: 2948
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ActionOrderComparer : IComparer
	{
		// Token: 0x06006A45 RID: 27205 RVA: 0x001C60F8 File Offset: 0x001C42F8
		public int Compare(object x, object y)
		{
			ActionBase actionBase = x as ActionBase;
			ActionBase actionBase2 = y as ActionBase;
			if (actionBase == null || actionBase2 == null)
			{
				throw new ArgumentNullException();
			}
			if (actionBase.ActionOrder == actionBase2.ActionOrder)
			{
				return 0;
			}
			if (actionBase.ActionOrder <= actionBase2.ActionOrder)
			{
				return -1;
			}
			return 1;
		}
	}
}
