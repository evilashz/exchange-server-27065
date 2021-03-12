using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x02000575 RID: 1397
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class NullableMarshaler
	{
		// Token: 0x06004201 RID: 16897 RVA: 0x000F5690 File Offset: 0x000F3890
		[SecurityCritical]
		internal static IntPtr ConvertToNative<T>(ref T? pManaged) where T : struct
		{
			if (pManaged != null)
			{
				object o = IReferenceFactory.CreateIReference(pManaged);
				return Marshal.GetComInterfaceForObject(o, typeof(IReference<T>));
			}
			return IntPtr.Zero;
		}

		// Token: 0x06004202 RID: 16898 RVA: 0x000F56CC File Offset: 0x000F38CC
		[SecurityCritical]
		internal static void ConvertToManagedRetVoid<T>(IntPtr pNative, ref T? retObj) where T : struct
		{
			retObj = NullableMarshaler.ConvertToManaged<T>(pNative);
		}

		// Token: 0x06004203 RID: 16899 RVA: 0x000F56DC File Offset: 0x000F38DC
		[SecurityCritical]
		internal static T? ConvertToManaged<T>(IntPtr pNative) where T : struct
		{
			if (pNative != IntPtr.Zero)
			{
				object wrapper = InterfaceMarshaler.ConvertToManagedWithoutUnboxing(pNative);
				return (T?)CLRIReferenceImpl<T>.UnboxHelper(wrapper);
			}
			return null;
		}
	}
}
