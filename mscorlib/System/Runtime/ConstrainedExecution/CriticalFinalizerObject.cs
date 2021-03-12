using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.ConstrainedExecution
{
	// Token: 0x020006FC RID: 1788
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class CriticalFinalizerObject
	{
		// Token: 0x06005052 RID: 20562 RVA: 0x0011A6C2 File Offset: 0x001188C2
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalFinalizerObject()
		{
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x0011A6CC File Offset: 0x001188CC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~CriticalFinalizerObject()
		{
		}
	}
}
