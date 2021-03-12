using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200060A RID: 1546
	internal sealed class VarArgMethod
	{
		// Token: 0x06004910 RID: 18704 RVA: 0x00107CD1 File Offset: 0x00105ED1
		internal VarArgMethod(DynamicMethod dm, SignatureHelper signature)
		{
			this.m_dynamicMethod = dm;
			this.m_signature = signature;
		}

		// Token: 0x06004911 RID: 18705 RVA: 0x00107CE7 File Offset: 0x00105EE7
		internal VarArgMethod(RuntimeMethodInfo method, SignatureHelper signature)
		{
			this.m_method = method;
			this.m_signature = signature;
		}

		// Token: 0x04001DDF RID: 7647
		internal RuntimeMethodInfo m_method;

		// Token: 0x04001DE0 RID: 7648
		internal DynamicMethod m_dynamicMethod;

		// Token: 0x04001DE1 RID: 7649
		internal SignatureHelper m_signature;
	}
}
