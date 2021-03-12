using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000608 RID: 1544
	internal sealed class GenericMethodInfo
	{
		// Token: 0x0600490E RID: 18702 RVA: 0x00107CA5 File Offset: 0x00105EA5
		internal GenericMethodInfo(RuntimeMethodHandle methodHandle, RuntimeTypeHandle context)
		{
			this.m_methodHandle = methodHandle;
			this.m_context = context;
		}

		// Token: 0x04001DDB RID: 7643
		internal RuntimeMethodHandle m_methodHandle;

		// Token: 0x04001DDC RID: 7644
		internal RuntimeTypeHandle m_context;
	}
}
