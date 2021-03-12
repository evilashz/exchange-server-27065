using System;

namespace System.Runtime.Serialization
{
	// Token: 0x0200072D RID: 1837
	[Serializable]
	internal class SurrogateKey
	{
		// Token: 0x060051D1 RID: 20945 RVA: 0x0011EFF9 File Offset: 0x0011D1F9
		internal SurrogateKey(Type type, StreamingContext context)
		{
			this.m_type = type;
			this.m_context = context;
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x0011F00F File Offset: 0x0011D20F
		public override int GetHashCode()
		{
			return this.m_type.GetHashCode();
		}

		// Token: 0x0400240C RID: 9228
		internal Type m_type;

		// Token: 0x0400240D RID: 9229
		internal StreamingContext m_context;
	}
}
