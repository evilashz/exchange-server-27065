using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000016 RID: 22
	internal class MarshalHelper
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000029E0 File Offset: 0x00000BE0
		internal static IntPtr StringArrayToIntPtr(string[] stringArray)
		{
			int num = stringArray.Length;
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * num);
			for (int i = 0; i < num; i++)
			{
				Marshal.WriteIntPtr(intPtr, Marshal.SizeOf(typeof(IntPtr)) * i, Marshal.StringToHGlobalUni(stringArray[i]));
			}
			return intPtr;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A34 File Offset: 0x00000C34
		internal static void FreeIntPtrOfMarshalledObjectsArray(IntPtr marshalledArrayPtr, int numStrings)
		{
			for (int i = 0; i < numStrings; i++)
			{
				IntPtr hglobal = Marshal.ReadIntPtr(marshalledArrayPtr, Marshal.SizeOf(typeof(IntPtr)) * i);
				Marshal.FreeHGlobal(hglobal);
			}
			Marshal.FreeHGlobal(marshalledArrayPtr);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A74 File Offset: 0x00000C74
		internal static IntPtr ByteArrayToIntPtr(byte[] byteArray)
		{
			int num = byteArray.Length;
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.Copy(byteArray, 0, intPtr, num);
			return intPtr;
		}
	}
}
