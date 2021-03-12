using System;
using System.Collections;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000776 RID: 1910
	internal sealed class SerObjectInfoInit
	{
		// Token: 0x0400264A RID: 9802
		internal Hashtable seenBeforeTable = new Hashtable();

		// Token: 0x0400264B RID: 9803
		internal int objectInfoIdCount = 1;

		// Token: 0x0400264C RID: 9804
		internal SerStack oiPool = new SerStack("SerObjectInfo Pool");
	}
}
