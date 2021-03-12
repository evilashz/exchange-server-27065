using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics.Components.Setup;
using Microsoft.Exchange.Setup.Common;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000023 RID: 35
	internal class NativeMethodProvider : INativeMethodProvider
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00002DAC File Offset: 0x00000FAC
		public string GetSiteName(string server)
		{
			IntPtr zero = IntPtr.Zero;
			int num = NativeMethodProvider.DsGetSiteName(server, ref zero);
			string result = string.Empty;
			if (num == 0 && zero != IntPtr.Zero)
			{
				result = Marshal.PtrToStringUni(zero);
			}
			if (zero != IntPtr.Zero)
			{
				NativeMethodProvider.NetApiBufferFree(zero);
			}
			return result;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002DFC File Offset: 0x00000FFC
		public uint GetAccessCheck(string ntsdString, string listString)
		{
			byte[] ntsd = NativeMethodProvider.ExtFormatByteArrayFromString(ntsdString);
			return this.GetAccessCheck(ntsd, listString);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002E18 File Offset: 0x00001018
		public uint GetAccessCheck(byte[] ntsd, string listString)
		{
			uint desiredAccess = 33554432U;
			NativeMethodProvider.GENERIC_MAPPING genericMapping = NativeMethodProvider.GenericMapping(string.Empty);
			NativeMethodProvider.OBJECT_TYPE_LIST[] list = NativeMethodProvider.ObjectTypeList(string.Empty);
			return this.AccessCheck(desiredAccess, ntsd, genericMapping, list);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002E4C File Offset: 0x0000104C
		public bool TokenMembershipCheck(string sid)
		{
			bool result = false;
			IntPtr zero = IntPtr.Zero;
			try
			{
				if (!NativeMethodProvider.ConvertStringSidToSid(sid, out zero))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
				if (!NativeMethodProvider.CheckTokenMembership(IntPtr.Zero, zero, ref result))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
			catch (Exception e)
			{
				SetupLogger.LogError(e);
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					NativeMethodProvider.LocalFree(zero);
				}
			}
			return result;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002ED0 File Offset: 0x000010D0
		public bool IsCoreServer()
		{
			int num;
			bool flag = NativeMethodProvider.GetProductInfo(Environment.OSVersion.Version.Major, Environment.OSVersion.Version.Minor, 0, 0, out num);
			ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(2332437821U, ref num);
			if (flag)
			{
				flag = ((long)num == 12L || (long)num == 39L || (long)num == 13L || (long)num == 40L || (long)num == 14L || (long)num == 41L);
			}
			return flag;
		}

		// Token: 0x06000067 RID: 103
		[DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool ConvertSidToStringSid(IntPtr sid, [MarshalAs(UnmanagedType.LPTStr)] [In] [Out] ref string pStringSid);

		// Token: 0x06000068 RID: 104
		[DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool ConvertStringSidToSid([MarshalAs(UnmanagedType.LPTStr)] [In] string stringSid, out IntPtr sid);

		// Token: 0x06000069 RID: 105
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool CheckTokenMembership(IntPtr TokenHandle, IntPtr SidToCheck, ref bool IsMember);

		// Token: 0x0600006A RID: 106
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr LocalFree(IntPtr hMem);

		// Token: 0x0600006B RID: 107
		[DllImport("NetApi32.dll", CharSet = CharSet.Unicode)]
		private static extern int DsGetSiteName(string server, ref IntPtr siteName);

		// Token: 0x0600006C RID: 108
		[DllImport("NetApi32.dll", CharSet = CharSet.Unicode)]
		private static extern int NetApiBufferFree(IntPtr ptr);

		// Token: 0x0600006D RID: 109
		[DllImport("kernel32.dll")]
		private static extern bool GetProductInfo(int dwOSMajorVersion, int dwOSMinorVersion, int dwSpMajorVersion, int dwSpMinorVersion, out int pdwReturnedProductType);

		// Token: 0x0600006E RID: 110
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetCurrentThread();

		// Token: 0x0600006F RID: 111
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetCurrentProcess();

		// Token: 0x06000070 RID: 112
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool ImpersonateSelf(int impersonationLevel);

		// Token: 0x06000071 RID: 113
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool OpenThreadToken(IntPtr ProcessHandle, uint DesiredAccess, bool openAsSelf, ref IntPtr TokenHandle);

		// Token: 0x06000072 RID: 114
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, ref IntPtr TokenHandle);

		// Token: 0x06000073 RID: 115
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CloseHandle(IntPtr handle);

		// Token: 0x06000074 RID: 116
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool RevertToSelf();

		// Token: 0x06000075 RID: 117 RVA: 0x00002F48 File Offset: 0x00001148
		private static byte[] ExtFormatByteArrayFromString(string valString)
		{
			byte[] array = new byte[valString.Length / 2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Convert.ToByte(valString.Substring(2 * i, 2), 16);
			}
			return array;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002F88 File Offset: 0x00001188
		private static uint AccessMask(string maskString)
		{
			uint num = 0U;
			string[] array = maskString.Split(new char[]
			{
				'|'
			});
			string[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				string text = array2[i];
				string a;
				if ((a = text) == null)
				{
					goto IL_74;
				}
				if (!(a == "STANDARD_RIGHTS_ALL"))
				{
					if (!(a == "SPECIFIC_RIGHTS_ALL"))
					{
						if (!(a == "MAXIMUM_ALLOWED"))
						{
							goto IL_74;
						}
						num |= 33554432U;
					}
					else
					{
						num |= 65535U;
					}
				}
				else
				{
					num |= 2031616U;
				}
				IL_7B:
				i++;
				continue;
				IL_74:
				num = uint.Parse(text);
				goto IL_7B;
			}
			return num;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003020 File Offset: 0x00001220
		private static NativeMethodProvider.OBJECT_TYPE_LIST[] ObjectTypeList(string listString)
		{
			if (listString.Length == 0)
			{
				return new NativeMethodProvider.OBJECT_TYPE_LIST[0];
			}
			string[] array = listString.Split(new char[]
			{
				'|'
			});
			NativeMethodProvider.OBJECT_TYPE_LIST[] array2 = new NativeMethodProvider.OBJECT_TYPE_LIST[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i][1] != ';')
				{
					throw new ArgumentException();
				}
				short level = short.Parse(array[i].Substring(0, 1));
				Guid objectType = new Guid(array[i].Substring(2));
				array2[i] = new NativeMethodProvider.OBJECT_TYPE_LIST(level, 0, objectType);
			}
			return array2;
		}

		// Token: 0x06000078 RID: 120
		[DllImport("advapi32.dll")]
		private static extern void MapGenericMask(ref uint accessMask, ref NativeMethodProvider.GENERIC_MAPPING genericMapping);

		// Token: 0x06000079 RID: 121 RVA: 0x000030B4 File Offset: 0x000012B4
		private static NativeMethodProvider.GENERIC_MAPPING GenericMapping(string mapping)
		{
			NativeMethodProvider.GENERIC_MAPPING result = default(NativeMethodProvider.GENERIC_MAPPING);
			if (mapping.Length > 0)
			{
				string[] array = mapping.Split(new char[]
				{
					','
				});
				if (array.Length != 4)
				{
					throw new ArgumentException();
				}
				result.GenericRead = Convert.ToUInt32(array[0], 16);
				result.GenericWrite = Convert.ToUInt32(array[1], 16);
				result.GenericExecute = Convert.ToUInt32(array[2], 16);
				result.GenericAll = Convert.ToUInt32(array[3], 16);
			}
			else
			{
				result.GenericRead = 2147483648U;
				result.GenericWrite = 1073741824U;
				result.GenericExecute = 536870912U;
				result.GenericAll = 268435456U;
			}
			return result;
		}

		// Token: 0x0600007A RID: 122
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool AccessCheckByType([MarshalAs(UnmanagedType.LPArray)] byte[] pSecurityDescriptor, IntPtr principalSelfSid, IntPtr clientToken, uint DesiredAccess, IntPtr objectTypeList, int ObjectTypeListLength, ref NativeMethodProvider.GENERIC_MAPPING GenericMapping, IntPtr PrivilegeSet, ref int PrivilegeSetLength, ref uint GrantedAccess, ref int AccessStatus);

		// Token: 0x0600007B RID: 123 RVA: 0x0000316C File Offset: 0x0000136C
		private IntPtr GetTokenHandle()
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			int num = 0;
			if (!NativeMethodProvider.OpenThreadToken(NativeMethodProvider.GetCurrentThread(), 8U, true, ref zero))
			{
				num = Marshal.GetLastWin32Error();
				if (num == 1008)
				{
					num = 0;
					if (!NativeMethodProvider.OpenProcessToken(NativeMethodProvider.GetCurrentProcess(), 8U, ref zero))
					{
						num = Marshal.GetLastWin32Error();
					}
				}
			}
			if (num != 0)
			{
				throw new Win32Exception(num);
			}
			return zero;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000031C8 File Offset: 0x000013C8
		private uint AccessCheck(uint desiredAccess, byte[] ntsd, NativeMethodProvider.GENERIC_MAPPING genericMapping, NativeMethodProvider.OBJECT_TYPE_LIST[] list)
		{
			uint result = 0U;
			int num = 0;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			bool flag = false;
			try
			{
				intPtr2 = Marshal.AllocHGlobal(1024);
				int num2 = 1024;
				NativeMethodProvider.MapGenericMask(ref desiredAccess, ref genericMapping);
				if (!NativeMethodProvider.ImpersonateSelf(2))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw new Win32Exception(lastWin32Error);
				}
				flag = true;
				intPtr = this.GetTokenHandle();
				intPtr3 = NativeMethodProvider.OBJECT_TYPE_LIST.NativeStruct(list);
				if (!NativeMethodProvider.AccessCheckByType(ntsd, IntPtr.Zero, intPtr, desiredAccess, intPtr3, list.Length, ref genericMapping, intPtr2, ref num2, ref result, ref num))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					SetupLogger.LogError(new Win32Exception(lastWin32Error));
				}
			}
			finally
			{
				if (intPtr3 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr3);
				}
				if (intPtr != IntPtr.Zero)
				{
					NativeMethodProvider.CloseHandle(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
				if (flag)
				{
					NativeMethodProvider.RevertToSelf();
				}
			}
			return result;
		}

		// Token: 0x04000074 RID: 116
		private const int ERRORNOTOKEN = 1008;

		// Token: 0x04000075 RID: 117
		private const int SecurityImpersonation = 2;

		// Token: 0x04000076 RID: 118
		private const uint TOKENQUERY = 8U;

		// Token: 0x04000077 RID: 119
		private const uint MaximumAllowed = 33554432U;

		// Token: 0x04000078 RID: 120
		private const uint ProductDatacenterServerCore = 12U;

		// Token: 0x04000079 RID: 121
		private const uint ProductDatacenterServerCoreV = 39U;

		// Token: 0x0400007A RID: 122
		private const uint ProductStandardServerCore = 13U;

		// Token: 0x0400007B RID: 123
		private const uint ProductStandardServerCoreV = 40U;

		// Token: 0x0400007C RID: 124
		private const uint ProductEnterpriseServerCore = 14U;

		// Token: 0x0400007D RID: 125
		private const uint ProductEnterpriseServerCoreV = 41U;

		// Token: 0x02000024 RID: 36
		private struct OBJECT_TYPE_LIST_NATIVE
		{
			// Token: 0x0400007E RID: 126
			internal short Level;

			// Token: 0x0400007F RID: 127
			internal short Sbz;

			// Token: 0x04000080 RID: 128
			internal IntPtr ObjectType;
		}

		// Token: 0x02000025 RID: 37
		private struct OBJECT_TYPE_LIST
		{
			// Token: 0x0600007D RID: 125 RVA: 0x000032C4 File Offset: 0x000014C4
			public OBJECT_TYPE_LIST(short Level, short Sbz, Guid ObjectType)
			{
				this.Level = Level;
				this.Sbz = Sbz;
				this.ObjectType = ObjectType;
			}

			// Token: 0x0600007E RID: 126 RVA: 0x000032DC File Offset: 0x000014DC
			public static IntPtr NativeStruct(NativeMethodProvider.OBJECT_TYPE_LIST[] list)
			{
				if (list.Length == 0)
				{
					return IntPtr.Zero;
				}
				NativeMethodProvider.OBJECT_TYPE_LIST_NATIVE object_TYPE_LIST_NATIVE = default(NativeMethodProvider.OBJECT_TYPE_LIST_NATIVE);
				int num = Marshal.SizeOf(typeof(Guid));
				int num2 = Marshal.SizeOf(typeof(NativeMethodProvider.OBJECT_TYPE_LIST_NATIVE));
				IntPtr intPtr = Marshal.AllocHGlobal((num + num2) * list.Length);
				IntPtr intPtr2 = intPtr;
				IntPtr intPtr3 = (IntPtr)(((long)intPtr2 + (long)num2) * (long)list.Length);
				foreach (NativeMethodProvider.OBJECT_TYPE_LIST object_TYPE_LIST in list)
				{
					object_TYPE_LIST_NATIVE.Level = object_TYPE_LIST.Level;
					object_TYPE_LIST_NATIVE.Sbz = object_TYPE_LIST.Sbz;
					object_TYPE_LIST_NATIVE.ObjectType = intPtr3;
					Marshal.StructureToPtr(object_TYPE_LIST_NATIVE, intPtr2, false);
					Guid objectType = object_TYPE_LIST.ObjectType;
					Marshal.Copy(objectType.ToByteArray(), 0, intPtr3, num);
					intPtr2 = (IntPtr)((long)intPtr2 + (long)num2);
					intPtr3 = (IntPtr)((long)intPtr3 + (long)num);
				}
				return intPtr;
			}

			// Token: 0x04000081 RID: 129
			internal short Level;

			// Token: 0x04000082 RID: 130
			internal short Sbz;

			// Token: 0x04000083 RID: 131
			internal Guid ObjectType;
		}

		// Token: 0x02000026 RID: 38
		private struct GENERIC_MAPPING
		{
			// Token: 0x04000084 RID: 132
			public uint GenericRead;

			// Token: 0x04000085 RID: 133
			public uint GenericWrite;

			// Token: 0x04000086 RID: 134
			public uint GenericExecute;

			// Token: 0x04000087 RID: 135
			public uint GenericAll;
		}
	}
}
