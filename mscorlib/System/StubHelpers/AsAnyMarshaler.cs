using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace System.StubHelpers
{
	// Token: 0x02000574 RID: 1396
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	[SecurityCritical]
	internal struct AsAnyMarshaler
	{
		// Token: 0x060041F4 RID: 16884 RVA: 0x000F5191 File Offset: 0x000F3391
		private static bool IsIn(int dwFlags)
		{
			return (dwFlags & 268435456) != 0;
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x000F519D File Offset: 0x000F339D
		private static bool IsOut(int dwFlags)
		{
			return (dwFlags & 536870912) != 0;
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x000F51A9 File Offset: 0x000F33A9
		private static bool IsAnsi(int dwFlags)
		{
			return (dwFlags & 16711680) != 0;
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x000F51B5 File Offset: 0x000F33B5
		private static bool IsThrowOn(int dwFlags)
		{
			return (dwFlags & 65280) != 0;
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x000F51C1 File Offset: 0x000F33C1
		private static bool IsBestFit(int dwFlags)
		{
			return (dwFlags & 255) != 0;
		}

		// Token: 0x060041F9 RID: 16889 RVA: 0x000F51CD File Offset: 0x000F33CD
		internal AsAnyMarshaler(IntPtr pvArrayMarshaler)
		{
			this.pvArrayMarshaler = pvArrayMarshaler;
			this.backPropAction = AsAnyMarshaler.BackPropAction.None;
			this.layoutType = null;
			this.cleanupWorkList = null;
		}

		// Token: 0x060041FA RID: 16890 RVA: 0x000F51EC File Offset: 0x000F33EC
		[SecurityCritical]
		private unsafe IntPtr ConvertArrayToNative(object pManagedHome, int dwFlags)
		{
			Type elementType = pManagedHome.GetType().GetElementType();
			VarEnum varEnum;
			switch (Type.GetTypeCode(elementType))
			{
			case TypeCode.Object:
				if (elementType == typeof(IntPtr))
				{
					varEnum = ((IntPtr.Size == 4) ? VarEnum.VT_I4 : VarEnum.VT_I8);
					goto IL_10D;
				}
				if (elementType == typeof(UIntPtr))
				{
					varEnum = ((IntPtr.Size == 4) ? VarEnum.VT_UI4 : VarEnum.VT_UI8);
					goto IL_10D;
				}
				break;
			case TypeCode.Boolean:
				varEnum = (VarEnum)254;
				goto IL_10D;
			case TypeCode.Char:
				varEnum = (AsAnyMarshaler.IsAnsi(dwFlags) ? ((VarEnum)253) : VarEnum.VT_UI2);
				goto IL_10D;
			case TypeCode.SByte:
				varEnum = VarEnum.VT_I1;
				goto IL_10D;
			case TypeCode.Byte:
				varEnum = VarEnum.VT_UI1;
				goto IL_10D;
			case TypeCode.Int16:
				varEnum = VarEnum.VT_I2;
				goto IL_10D;
			case TypeCode.UInt16:
				varEnum = VarEnum.VT_UI2;
				goto IL_10D;
			case TypeCode.Int32:
				varEnum = VarEnum.VT_I4;
				goto IL_10D;
			case TypeCode.UInt32:
				varEnum = VarEnum.VT_UI4;
				goto IL_10D;
			case TypeCode.Int64:
				varEnum = VarEnum.VT_I8;
				goto IL_10D;
			case TypeCode.UInt64:
				varEnum = VarEnum.VT_UI8;
				goto IL_10D;
			case TypeCode.Single:
				varEnum = VarEnum.VT_R4;
				goto IL_10D;
			case TypeCode.Double:
				varEnum = VarEnum.VT_R8;
				goto IL_10D;
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_NDirectBadObject"));
			IL_10D:
			int num = (int)varEnum;
			if (AsAnyMarshaler.IsBestFit(dwFlags))
			{
				num |= 65536;
			}
			if (AsAnyMarshaler.IsThrowOn(dwFlags))
			{
				num |= 16777216;
			}
			MngdNativeArrayMarshaler.CreateMarshaler(this.pvArrayMarshaler, IntPtr.Zero, num);
			IntPtr result;
			IntPtr pNativeHome = new IntPtr((void*)(&result));
			MngdNativeArrayMarshaler.ConvertSpaceToNative(this.pvArrayMarshaler, ref pManagedHome, pNativeHome);
			if (AsAnyMarshaler.IsIn(dwFlags))
			{
				MngdNativeArrayMarshaler.ConvertContentsToNative(this.pvArrayMarshaler, ref pManagedHome, pNativeHome);
			}
			if (AsAnyMarshaler.IsOut(dwFlags))
			{
				this.backPropAction = AsAnyMarshaler.BackPropAction.Array;
			}
			return result;
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x000F537C File Offset: 0x000F357C
		[SecurityCritical]
		private static IntPtr ConvertStringToNative(string pManagedHome, int dwFlags)
		{
			IntPtr intPtr;
			if (AsAnyMarshaler.IsAnsi(dwFlags))
			{
				intPtr = CSTRMarshaler.ConvertToNative(dwFlags & 65535, pManagedHome, IntPtr.Zero);
			}
			else
			{
				StubHelpers.CheckStringLength(pManagedHome.Length);
				int num = (pManagedHome.Length + 1) * 2;
				intPtr = Marshal.AllocCoTaskMem(num);
				string.InternalCopy(pManagedHome, intPtr, num);
			}
			return intPtr;
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x000F53CC File Offset: 0x000F35CC
		[SecurityCritical]
		private unsafe IntPtr ConvertStringBuilderToNative(StringBuilder pManagedHome, int dwFlags)
		{
			IntPtr intPtr;
			if (AsAnyMarshaler.IsAnsi(dwFlags))
			{
				StubHelpers.CheckStringLength(pManagedHome.Capacity);
				int num = pManagedHome.Capacity * Marshal.SystemMaxDBCSCharSize + 4;
				intPtr = Marshal.AllocCoTaskMem(num);
				byte* ptr = (byte*)((void*)intPtr);
				*(ptr + num - 3) = 0;
				*(ptr + num - 2) = 0;
				*(ptr + num - 1) = 0;
				if (AsAnyMarshaler.IsIn(dwFlags))
				{
					int num2;
					byte[] src = AnsiCharMarshaler.DoAnsiConversion(pManagedHome.ToString(), AsAnyMarshaler.IsBestFit(dwFlags), AsAnyMarshaler.IsThrowOn(dwFlags), out num2);
					Buffer.Memcpy(ptr, 0, src, 0, num2);
					ptr[num2] = 0;
				}
				if (AsAnyMarshaler.IsOut(dwFlags))
				{
					this.backPropAction = AsAnyMarshaler.BackPropAction.StringBuilderAnsi;
				}
			}
			else
			{
				int num3 = pManagedHome.Capacity * 2 + 4;
				intPtr = Marshal.AllocCoTaskMem(num3);
				byte* ptr2 = (byte*)((void*)intPtr);
				*(ptr2 + num3 - 1) = 0;
				*(ptr2 + num3 - 2) = 0;
				if (AsAnyMarshaler.IsIn(dwFlags))
				{
					int num4 = pManagedHome.Length * 2;
					pManagedHome.InternalCopy(intPtr, num4);
					ptr2[num4] = 0;
					(ptr2 + num4)[1] = 0;
				}
				if (AsAnyMarshaler.IsOut(dwFlags))
				{
					this.backPropAction = AsAnyMarshaler.BackPropAction.StringBuilderUnicode;
				}
			}
			return intPtr;
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x000F54D0 File Offset: 0x000F36D0
		[SecurityCritical]
		private unsafe IntPtr ConvertLayoutToNative(object pManagedHome, int dwFlags)
		{
			int cb = Marshal.SizeOfHelper(pManagedHome.GetType(), false);
			IntPtr result = Marshal.AllocCoTaskMem(cb);
			if (AsAnyMarshaler.IsIn(dwFlags))
			{
				StubHelpers.FmtClassUpdateNativeInternal(pManagedHome, (byte*)result.ToPointer(), ref this.cleanupWorkList);
			}
			if (AsAnyMarshaler.IsOut(dwFlags))
			{
				this.backPropAction = AsAnyMarshaler.BackPropAction.Layout;
			}
			this.layoutType = pManagedHome.GetType();
			return result;
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x000F5528 File Offset: 0x000F3728
		[SecurityCritical]
		internal IntPtr ConvertToNative(object pManagedHome, int dwFlags)
		{
			if (pManagedHome == null)
			{
				return IntPtr.Zero;
			}
			if (pManagedHome is ArrayWithOffset)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MarshalAsAnyRestriction"));
			}
			IntPtr result;
			string pManagedHome2;
			StringBuilder pManagedHome3;
			if (pManagedHome.GetType().IsArray)
			{
				result = this.ConvertArrayToNative(pManagedHome, dwFlags);
			}
			else if ((pManagedHome2 = (pManagedHome as string)) != null)
			{
				result = AsAnyMarshaler.ConvertStringToNative(pManagedHome2, dwFlags);
			}
			else if ((pManagedHome3 = (pManagedHome as StringBuilder)) != null)
			{
				result = this.ConvertStringBuilderToNative(pManagedHome3, dwFlags);
			}
			else
			{
				if (!pManagedHome.GetType().IsLayoutSequential && !pManagedHome.GetType().IsExplicitLayout)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_NDirectBadObject"));
				}
				result = this.ConvertLayoutToNative(pManagedHome, dwFlags);
			}
			return result;
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x000F55D0 File Offset: 0x000F37D0
		[SecurityCritical]
		internal unsafe void ConvertToManaged(object pManagedHome, IntPtr pNativeHome)
		{
			switch (this.backPropAction)
			{
			case AsAnyMarshaler.BackPropAction.Array:
				MngdNativeArrayMarshaler.ConvertContentsToManaged(this.pvArrayMarshaler, ref pManagedHome, new IntPtr((void*)(&pNativeHome)));
				return;
			case AsAnyMarshaler.BackPropAction.Layout:
				StubHelpers.FmtClassUpdateCLRInternal(pManagedHome, (byte*)pNativeHome.ToPointer());
				return;
			case AsAnyMarshaler.BackPropAction.StringBuilderAnsi:
			{
				sbyte* newBuffer = (sbyte*)pNativeHome.ToPointer();
				((StringBuilder)pManagedHome).ReplaceBufferAnsiInternal(newBuffer, Win32Native.lstrlenA(pNativeHome));
				return;
			}
			case AsAnyMarshaler.BackPropAction.StringBuilderUnicode:
			{
				char* newBuffer2 = (char*)pNativeHome.ToPointer();
				((StringBuilder)pManagedHome).ReplaceBufferInternal(newBuffer2, Win32Native.lstrlenW(pNativeHome));
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x000F5656 File Offset: 0x000F3856
		[SecurityCritical]
		internal void ClearNative(IntPtr pNativeHome)
		{
			if (pNativeHome != IntPtr.Zero)
			{
				if (this.layoutType != null)
				{
					Marshal.DestroyStructure(pNativeHome, this.layoutType);
				}
				Win32Native.CoTaskMemFree(pNativeHome);
			}
			StubHelpers.DestroyCleanupList(ref this.cleanupWorkList);
		}

		// Token: 0x04001B1E RID: 6942
		private const ushort VTHACK_ANSICHAR = 253;

		// Token: 0x04001B1F RID: 6943
		private const ushort VTHACK_WINBOOL = 254;

		// Token: 0x04001B20 RID: 6944
		private IntPtr pvArrayMarshaler;

		// Token: 0x04001B21 RID: 6945
		private AsAnyMarshaler.BackPropAction backPropAction;

		// Token: 0x04001B22 RID: 6946
		private Type layoutType;

		// Token: 0x04001B23 RID: 6947
		private CleanupWorkList cleanupWorkList;

		// Token: 0x02000C01 RID: 3073
		private enum BackPropAction
		{
			// Token: 0x04003641 RID: 13889
			None,
			// Token: 0x04003642 RID: 13890
			Array,
			// Token: 0x04003643 RID: 13891
			Layout,
			// Token: 0x04003644 RID: 13892
			StringBuilderAnsi,
			// Token: 0x04003645 RID: 13893
			StringBuilderUnicode
		}
	}
}
