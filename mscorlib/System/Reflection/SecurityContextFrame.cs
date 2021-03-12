using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005AF RID: 1455
	internal struct SecurityContextFrame
	{
		// Token: 0x0600444E RID: 17486
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Push(RuntimeAssembly assembly);

		// Token: 0x0600444F RID: 17487
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Pop();

		// Token: 0x04001BCB RID: 7115
		private IntPtr m_GSCookie;

		// Token: 0x04001BCC RID: 7116
		private IntPtr __VFN_table;

		// Token: 0x04001BCD RID: 7117
		private IntPtr m_Next;

		// Token: 0x04001BCE RID: 7118
		private IntPtr m_Assembly;
	}
}
