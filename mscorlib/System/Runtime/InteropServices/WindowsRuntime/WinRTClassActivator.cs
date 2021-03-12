using System;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E6 RID: 2534
	internal sealed class WinRTClassActivator : MarshalByRefObject, IWinRTClassActivator
	{
		// Token: 0x0600649E RID: 25758 RVA: 0x00155020 File Offset: 0x00153220
		[SecurityCritical]
		public object ActivateInstance(string activatableClassId)
		{
			ManagedActivationFactory managedActivationFactory = WindowsRuntimeMarshal.GetManagedActivationFactory(this.LoadWinRTType(activatableClassId));
			return managedActivationFactory.ActivateInstance();
		}

		// Token: 0x0600649F RID: 25759 RVA: 0x00155040 File Offset: 0x00153240
		[SecurityCritical]
		public IntPtr GetActivationFactory(string activatableClassId, ref Guid iid)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr result;
			try
			{
				intPtr = WindowsRuntimeMarshal.GetActivationFactoryForType(this.LoadWinRTType(activatableClassId));
				IntPtr zero = IntPtr.Zero;
				int num = Marshal.QueryInterface(intPtr, ref iid, out zero);
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				result = zero;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.Release(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060064A0 RID: 25760 RVA: 0x001550A4 File Offset: 0x001532A4
		private Type LoadWinRTType(string acid)
		{
			Type type = Type.GetType(acid + ", Windows, ContentType=WindowsRuntime");
			if (type == null)
			{
				throw new COMException(-2147221164);
			}
			return type;
		}

		// Token: 0x060064A1 RID: 25761 RVA: 0x001550D7 File Offset: 0x001532D7
		[SecurityCritical]
		internal IntPtr GetIWinRTClassActivator()
		{
			return Marshal.GetComInterfaceForObject(this, typeof(IWinRTClassActivator));
		}
	}
}
