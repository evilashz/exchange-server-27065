using System;

namespace System.Reflection
{
	// Token: 0x020005C5 RID: 1477
	internal sealed class LoaderAllocator
	{
		// Token: 0x06004554 RID: 17748 RVA: 0x000FDD04 File Offset: 0x000FBF04
		private LoaderAllocator()
		{
			this.m_slots = new object[5];
			this.m_scout = new LoaderAllocatorScout();
		}

		// Token: 0x04001C19 RID: 7193
		private LoaderAllocatorScout m_scout;

		// Token: 0x04001C1A RID: 7194
		private object[] m_slots;

		// Token: 0x04001C1B RID: 7195
		internal CerHashtable<RuntimeMethodInfo, RuntimeMethodInfo> m_methodInstantiations;

		// Token: 0x04001C1C RID: 7196
		private int m_slotsUsed;
	}
}
