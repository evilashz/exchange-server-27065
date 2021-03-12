using System;
using System.Collections;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200002C RID: 44
	public static class ArrayExtension
	{
		// Token: 0x06000206 RID: 518 RVA: 0x00008434 File Offset: 0x00006634
		public static WorkUnit[] DeepCopy(this WorkUnit[] workUnitArray)
		{
			WorkUnit[] array = null;
			if (workUnitArray != null)
			{
				array = new WorkUnit[workUnitArray.Length];
				for (int i = 0; i < workUnitArray.Length; i++)
				{
					array[i] = new WorkUnit(workUnitArray[i].Text, workUnitArray[i].Icon, workUnitArray[i].Target);
				}
			}
			return array;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000847E File Offset: 0x0000667E
		public static bool IsEmptyCollection(this ICollection collection)
		{
			return collection == null || collection.Count == 0;
		}
	}
}
