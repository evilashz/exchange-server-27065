using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Microsoft.Exchange.Rpc.Nspi
{
	// Token: 0x0200029F RID: 671
	public class SafeSRowHandle : SafeRpcMemoryHandle
	{
		// Token: 0x06000CA1 RID: 3233 RVA: 0x0002DE50 File Offset: 0x0002D250
		public SafeSRowHandle(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0002DE3C File Offset: 0x0002D23C
		public SafeSRowHandle()
		{
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0002DE64 File Offset: 0x0002D264
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		[return: MarshalAs(UnmanagedType.U1)]
		protected unsafe override bool ReleaseHandle()
		{
			if (!this.IsInvalid)
			{
				ulong num = (ulong)(*(long*)((byte*)this.handle.ToPointer() + 8L));
				if (num != 0UL)
				{
					<Module>.MIDL_user_free(num);
				}
				<Module>.MIDL_user_free(this.handle.ToPointer());
			}
			return true;
		}
	}
}
