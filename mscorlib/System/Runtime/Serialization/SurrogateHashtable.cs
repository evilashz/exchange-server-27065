using System;
using System.Collections;

namespace System.Runtime.Serialization
{
	// Token: 0x0200072E RID: 1838
	internal class SurrogateHashtable : Hashtable
	{
		// Token: 0x060051D3 RID: 20947 RVA: 0x0011F01C File Offset: 0x0011D21C
		internal SurrogateHashtable(int size) : base(size)
		{
		}

		// Token: 0x060051D4 RID: 20948 RVA: 0x0011F028 File Offset: 0x0011D228
		protected override bool KeyEquals(object key, object item)
		{
			SurrogateKey surrogateKey = (SurrogateKey)item;
			SurrogateKey surrogateKey2 = (SurrogateKey)key;
			return surrogateKey2.m_type == surrogateKey.m_type && (surrogateKey2.m_context.m_state & surrogateKey.m_context.m_state) == surrogateKey.m_context.m_state && surrogateKey2.m_context.Context == surrogateKey.m_context.Context;
		}
	}
}
