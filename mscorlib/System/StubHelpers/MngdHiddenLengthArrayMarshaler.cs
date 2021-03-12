using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x02000572 RID: 1394
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class MngdHiddenLengthArrayMarshaler
	{
		// Token: 0x060041DE RID: 16862
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CreateMarshaler(IntPtr pMarshalState, IntPtr pMT, IntPtr cbElementSize, ushort vt);

		// Token: 0x060041DF RID: 16863
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertSpaceToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060041E0 RID: 16864
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertContentsToNative(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060041E1 RID: 16865 RVA: 0x000F4ED8 File Offset: 0x000F30D8
		[SecurityCritical]
		internal unsafe static void ConvertContentsToNative_DateTime(ref DateTimeOffset[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				DateTimeNative* ptr = *(IntPtr*)((void*)pNativeHome);
				for (int i = 0; i < managedArray.Length; i++)
				{
					DateTimeOffsetMarshaler.ConvertToNative(ref managedArray[i], out ptr[i]);
				}
			}
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x000F4F18 File Offset: 0x000F3118
		[SecurityCritical]
		internal unsafe static void ConvertContentsToNative_Type(ref Type[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				TypeNameNative* ptr = *(IntPtr*)((void*)pNativeHome);
				for (int i = 0; i < managedArray.Length; i++)
				{
					SystemTypeMarshaler.ConvertToNative(managedArray[i], ptr + i);
				}
			}
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x000F4F58 File Offset: 0x000F3158
		[SecurityCritical]
		internal unsafe static void ConvertContentsToNative_Exception(ref Exception[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				int* ptr = *(IntPtr*)((void*)pNativeHome);
				for (int i = 0; i < managedArray.Length; i++)
				{
					ptr[i] = HResultExceptionMarshaler.ConvertToNative(managedArray[i]);
				}
			}
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x000F4F90 File Offset: 0x000F3190
		[SecurityCritical]
		internal unsafe static void ConvertContentsToNative_Nullable<T>(ref T?[] managedArray, IntPtr pNativeHome) where T : struct
		{
			if (managedArray != null)
			{
				IntPtr* ptr = *(IntPtr*)((void*)pNativeHome);
				for (int i = 0; i < managedArray.Length; i++)
				{
					ptr[i] = NullableMarshaler.ConvertToNative<T>(ref managedArray[i]);
				}
			}
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x000F4FD4 File Offset: 0x000F31D4
		[SecurityCritical]
		internal unsafe static void ConvertContentsToNative_KeyValuePair<K, V>(ref KeyValuePair<K, V>[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				IntPtr* ptr = *(IntPtr*)((void*)pNativeHome);
				for (int i = 0; i < managedArray.Length; i++)
				{
					ptr[i] = KeyValuePairMarshaler.ConvertToNative<K, V>(ref managedArray[i]);
				}
			}
		}

		// Token: 0x060041E6 RID: 16870
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertSpaceToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome, int elementCount);

		// Token: 0x060041E7 RID: 16871
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ConvertContentsToManaged(IntPtr pMarshalState, ref object pManagedHome, IntPtr pNativeHome);

		// Token: 0x060041E8 RID: 16872 RVA: 0x000F5018 File Offset: 0x000F3218
		[SecurityCritical]
		internal unsafe static void ConvertContentsToManaged_DateTime(ref DateTimeOffset[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				DateTimeNative* ptr = *(IntPtr*)((void*)pNativeHome);
				for (int i = 0; i < managedArray.Length; i++)
				{
					DateTimeOffsetMarshaler.ConvertToManaged(out managedArray[i], ref ptr[i]);
				}
			}
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x000F5058 File Offset: 0x000F3258
		[SecurityCritical]
		internal unsafe static void ConvertContentsToManaged_Type(ref Type[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				TypeNameNative* ptr = *(IntPtr*)((void*)pNativeHome);
				for (int i = 0; i < managedArray.Length; i++)
				{
					SystemTypeMarshaler.ConvertToManaged(ptr + i, ref managedArray[i]);
				}
			}
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x000F509C File Offset: 0x000F329C
		[SecurityCritical]
		internal unsafe static void ConvertContentsToManaged_Exception(ref Exception[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				int* ptr = *(IntPtr*)((void*)pNativeHome);
				for (int i = 0; i < managedArray.Length; i++)
				{
					managedArray[i] = HResultExceptionMarshaler.ConvertToManaged(ptr[i]);
				}
			}
		}

		// Token: 0x060041EB RID: 16875 RVA: 0x000F50D4 File Offset: 0x000F32D4
		[SecurityCritical]
		internal unsafe static void ConvertContentsToManaged_Nullable<T>(ref T?[] managedArray, IntPtr pNativeHome) where T : struct
		{
			if (managedArray != null)
			{
				IntPtr* ptr = *(IntPtr*)((void*)pNativeHome);
				for (int i = 0; i < managedArray.Length; i++)
				{
					managedArray[i] = NullableMarshaler.ConvertToManaged<T>(ptr[i]);
				}
			}
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x000F5118 File Offset: 0x000F3318
		[SecurityCritical]
		internal unsafe static void ConvertContentsToManaged_KeyValuePair<K, V>(ref KeyValuePair<K, V>[] managedArray, IntPtr pNativeHome)
		{
			if (managedArray != null)
			{
				IntPtr* ptr = *(IntPtr*)((void*)pNativeHome);
				for (int i = 0; i < managedArray.Length; i++)
				{
					managedArray[i] = KeyValuePairMarshaler.ConvertToManaged<K, V>(ptr[i]);
				}
			}
		}

		// Token: 0x060041ED RID: 16877
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearNativeContents(IntPtr pMarshalState, IntPtr pNativeHome, int cElements);

		// Token: 0x060041EE RID: 16878 RVA: 0x000F515C File Offset: 0x000F335C
		[SecurityCritical]
		internal unsafe static void ClearNativeContents_Type(IntPtr pNativeHome, int cElements)
		{
			TypeNameNative* ptr = *(IntPtr*)((void*)pNativeHome);
			if (ptr != null)
			{
				for (int i = 0; i < cElements; i++)
				{
					SystemTypeMarshaler.ClearNative(ptr);
					ptr++;
				}
			}
		}
	}
}
