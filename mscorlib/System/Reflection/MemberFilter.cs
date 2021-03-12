using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005D3 RID: 1491
	// (Invoke) Token: 0x060045B4 RID: 17844
	[ComVisible(true)]
	[Serializable]
	public delegate bool MemberFilter(MemberInfo m, object filterCriteria);
}
