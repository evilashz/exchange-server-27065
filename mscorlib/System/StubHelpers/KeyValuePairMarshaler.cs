using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x0200057B RID: 1403
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class KeyValuePairMarshaler
	{
		// Token: 0x0600420B RID: 16907 RVA: 0x000F58C8 File Offset: 0x000F3AC8
		[SecurityCritical]
		internal static IntPtr ConvertToNative<K, V>([In] ref KeyValuePair<K, V> pair)
		{
			IKeyValuePair<K, V> o = new CLRIKeyValuePairImpl<K, V>(ref pair);
			return Marshal.GetComInterfaceForObject(o, typeof(IKeyValuePair<K, V>));
		}

		// Token: 0x0600420C RID: 16908 RVA: 0x000F58EC File Offset: 0x000F3AEC
		[SecurityCritical]
		internal static KeyValuePair<K, V> ConvertToManaged<K, V>(IntPtr pInsp)
		{
			object obj = InterfaceMarshaler.ConvertToManagedWithoutUnboxing(pInsp);
			IKeyValuePair<K, V> keyValuePair = (IKeyValuePair<K, V>)obj;
			return new KeyValuePair<K, V>(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x0600420D RID: 16909 RVA: 0x000F5918 File Offset: 0x000F3B18
		[SecurityCritical]
		internal static object ConvertToManagedBox<K, V>(IntPtr pInsp)
		{
			return KeyValuePairMarshaler.ConvertToManaged<K, V>(pInsp);
		}
	}
}
