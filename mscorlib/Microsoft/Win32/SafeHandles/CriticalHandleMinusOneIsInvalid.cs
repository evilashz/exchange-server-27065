using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000025 RID: 37
	[SecurityCritical]
	[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
	public abstract class CriticalHandleMinusOneIsInvalid : CriticalHandle
	{
		// Token: 0x06000177 RID: 375 RVA: 0x000048CA File Offset: 0x00002ACA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalHandleMinusOneIsInvalid() : base(new IntPtr(-1))
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000048D8 File Offset: 0x00002AD8
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == new IntPtr(-1);
			}
		}
	}
}
