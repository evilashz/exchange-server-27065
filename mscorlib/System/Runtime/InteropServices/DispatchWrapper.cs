using System;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000931 RID: 2353
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DispatchWrapper
	{
		// Token: 0x060060FD RID: 24829 RVA: 0x0014AB90 File Offset: 0x00148D90
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public DispatchWrapper(object obj)
		{
			if (obj != null)
			{
				IntPtr idispatchForObject = Marshal.GetIDispatchForObject(obj);
				Marshal.Release(idispatchForObject);
			}
			this.m_WrappedObject = obj;
		}

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x060060FE RID: 24830 RVA: 0x0014ABBB File Offset: 0x00148DBB
		[__DynamicallyInvokable]
		public object WrappedObject
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002AD2 RID: 10962
		private object m_WrappedObject;
	}
}
