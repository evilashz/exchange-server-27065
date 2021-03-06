using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000029 RID: 41
	public static class InteropShim
	{
		// Token: 0x06000343 RID: 835 RVA: 0x00008B18 File Offset: 0x00006D18
		public unsafe static byte[] SerializeDatabaseInfo(JET_DBINFOMISC databaseInfo)
		{
			int num = Marshal.SizeOf(typeof(NATIVE_DBINFOMISC7));
			int num2 = 4 + num;
			byte[] array = null;
			if (databaseInfo != null)
			{
				array = new byte[num2];
				Array.Copy(BitConverter.GetBytes(20100701), array, 4);
				NATIVE_DBINFOMISC7 nativeDbinfomisc = databaseInfo.GetNativeDbinfomisc7();
				fixed (byte* ptr = &array[4])
				{
					IntPtr ptr2 = new IntPtr((void*)ptr);
					Marshal.StructureToPtr(nativeDbinfomisc, ptr2, false);
				}
			}
			return array;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00008B84 File Offset: 0x00006D84
		public unsafe static bool DeserializeDatabaseInfo(byte[] serializedDatabaseInfo, out JET_DBINFOMISC databaseInfo)
		{
			databaseInfo = null;
			bool result = false;
			if (serializedDatabaseInfo == null)
			{
				result = true;
			}
			else
			{
				int num = BitConverter.ToInt32(serializedDatabaseInfo, 0);
				if (num == 20100701)
				{
					int num2 = 4;
					if (serializedDatabaseInfo.Length >= num2 + Marshal.SizeOf(typeof(NATIVE_DBINFOMISC7)))
					{
						result = true;
						NATIVE_DBINFOMISC7 native_DBINFOMISC;
						fixed (byte* ptr = &serializedDatabaseInfo[num2])
						{
							IntPtr ptr2 = new IntPtr((void*)ptr);
							native_DBINFOMISC = (NATIVE_DBINFOMISC7)Marshal.PtrToStructure(ptr2, typeof(NATIVE_DBINFOMISC7));
						}
						databaseInfo = new JET_DBINFOMISC();
						databaseInfo.SetFromNativeDbinfoMisc(ref native_DBINFOMISC);
					}
					else if (serializedDatabaseInfo.Length >= num2 + Marshal.SizeOf(typeof(NATIVE_DBINFOMISC6)))
					{
						result = true;
						NATIVE_DBINFOMISC6 native_DBINFOMISC2;
						fixed (byte* ptr3 = &serializedDatabaseInfo[num2])
						{
							IntPtr ptr4 = new IntPtr((void*)ptr3);
							native_DBINFOMISC2 = (NATIVE_DBINFOMISC6)Marshal.PtrToStructure(ptr4, typeof(NATIVE_DBINFOMISC6));
						}
						databaseInfo = new JET_DBINFOMISC();
						databaseInfo.SetFromNativeDbinfoMisc6(ref native_DBINFOMISC2);
					}
				}
			}
			return result;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00008C68 File Offset: 0x00006E68
		internal static ulong Uint64FromLogTime(JET_LOGTIME? logtime)
		{
			ulong result = 0UL;
			if (logtime != null)
			{
				result = logtime.Value.ToUint64();
			}
			return result;
		}

		// Token: 0x0400007D RID: 125
		public const int MagicSerializeSignature = 20100701;
	}
}
