using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000609 RID: 1545
	internal sealed class GenericFieldInfo
	{
		// Token: 0x0600490F RID: 18703 RVA: 0x00107CBB File Offset: 0x00105EBB
		internal GenericFieldInfo(RuntimeFieldHandle fieldHandle, RuntimeTypeHandle context)
		{
			this.m_fieldHandle = fieldHandle;
			this.m_context = context;
		}

		// Token: 0x04001DDD RID: 7645
		internal RuntimeFieldHandle m_fieldHandle;

		// Token: 0x04001DDE RID: 7646
		internal RuntimeTypeHandle m_context;
	}
}
