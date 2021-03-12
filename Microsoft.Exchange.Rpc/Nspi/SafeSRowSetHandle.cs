using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Microsoft.Exchange.Rpc.Nspi
{
	// Token: 0x020002A2 RID: 674
	public class SafeSRowSetHandle : SafeRpcMemoryHandle
	{
		// Token: 0x06000CA9 RID: 3241 RVA: 0x0002E248 File Offset: 0x0002D648
		public SafeSRowSetHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0002E234 File Offset: 0x0002D634
		public SafeSRowSetHandle()
		{
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0002E25C File Offset: 0x0002D65C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		protected unsafe override bool ReleaseHandle()
		{
			if (!this.IsInvalid)
			{
				_SRowSet_r* ptr = (_SRowSet_r*)this.handle.ToPointer();
				uint num = 0;
				if (0 < *(int*)ptr)
				{
					do
					{
						<Module>.MIDL_user_free(((num + 1UL / 8UL) * 16L)[ptr / 8]);
						num++;
					}
					while (num < *(int*)ptr);
				}
				<Module>.MIDL_user_free(this.handle.ToPointer());
			}
			return true;
		}
	}
}
