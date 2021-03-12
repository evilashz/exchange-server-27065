using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005DE RID: 1502
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum PortableExecutableKinds
	{
		// Token: 0x04001CF4 RID: 7412
		NotAPortableExecutableImage = 0,
		// Token: 0x04001CF5 RID: 7413
		ILOnly = 1,
		// Token: 0x04001CF6 RID: 7414
		Required32Bit = 2,
		// Token: 0x04001CF7 RID: 7415
		PE32Plus = 4,
		// Token: 0x04001CF8 RID: 7416
		Unmanaged32Bit = 8,
		// Token: 0x04001CF9 RID: 7417
		[ComVisible(false)]
		Preferred32Bit = 16
	}
}
