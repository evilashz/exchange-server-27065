using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000601 RID: 1537
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum AssemblyBuilderAccess
	{
		// Token: 0x04001DC2 RID: 7618
		Run = 1,
		// Token: 0x04001DC3 RID: 7619
		Save = 2,
		// Token: 0x04001DC4 RID: 7620
		RunAndSave = 3,
		// Token: 0x04001DC5 RID: 7621
		ReflectionOnly = 6,
		// Token: 0x04001DC6 RID: 7622
		RunAndCollect = 9
	}
}
