using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security.Util
{
	// Token: 0x0200034D RID: 845
	internal static class Config
	{
		// Token: 0x06002AF9 RID: 11001 RVA: 0x0009F96C File Offset: 0x0009DB6C
		[SecurityCritical]
		private static void GetFileLocales()
		{
			if (Config.m_machineConfig == null)
			{
				string machineConfig = null;
				Config.GetMachineDirectory(JitHelpers.GetStringHandleOnStack(ref machineConfig));
				Config.m_machineConfig = machineConfig;
			}
			if (Config.m_userConfig == null)
			{
				string userConfig = null;
				Config.GetUserDirectory(JitHelpers.GetStringHandleOnStack(ref userConfig));
				Config.m_userConfig = userConfig;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x0009F9B7 File Offset: 0x0009DBB7
		internal static string MachineDirectory
		{
			[SecurityCritical]
			get
			{
				Config.GetFileLocales();
				return Config.m_machineConfig;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x0009F9C5 File Offset: 0x0009DBC5
		internal static string UserDirectory
		{
			[SecurityCritical]
			get
			{
				Config.GetFileLocales();
				return Config.m_userConfig;
			}
		}

		// Token: 0x06002AFC RID: 11004
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int SaveDataByte(string path, [In] byte[] data, int length);

		// Token: 0x06002AFD RID: 11005
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool RecoverData(ConfigId id);

		// Token: 0x06002AFE RID: 11006
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void SetQuickCache(ConfigId id, QuickCacheEntryType quickCacheFlags);

		// Token: 0x06002AFF RID: 11007
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern bool GetCacheEntry(ConfigId id, int numKey, [In] byte[] key, int keyLength, ObjectHandleOnStack retData);

		// Token: 0x06002B00 RID: 11008 RVA: 0x0009F9D4 File Offset: 0x0009DBD4
		[SecurityCritical]
		internal static bool GetCacheEntry(ConfigId id, int numKey, byte[] key, out byte[] data)
		{
			byte[] array = null;
			bool cacheEntry = Config.GetCacheEntry(id, numKey, key, key.Length, JitHelpers.GetObjectHandleOnStack<byte[]>(ref array));
			data = array;
			return cacheEntry;
		}

		// Token: 0x06002B01 RID: 11009
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddCacheEntry(ConfigId id, int numKey, [In] byte[] key, int keyLength, byte[] data, int dataLength);

		// Token: 0x06002B02 RID: 11010 RVA: 0x0009F9FA File Offset: 0x0009DBFA
		[SecurityCritical]
		internal static void AddCacheEntry(ConfigId id, int numKey, byte[] key, byte[] data)
		{
			Config.AddCacheEntry(id, numKey, key, key.Length, data, data.Length);
		}

		// Token: 0x06002B03 RID: 11011
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void ResetCacheData(ConfigId id);

		// Token: 0x06002B04 RID: 11012
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetMachineDirectory(StringHandleOnStack retDirectory);

		// Token: 0x06002B05 RID: 11013
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetUserDirectory(StringHandleOnStack retDirectory);

		// Token: 0x06002B06 RID: 11014
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool WriteToEventLog(string message);

		// Token: 0x04001108 RID: 4360
		private static volatile string m_machineConfig;

		// Token: 0x04001109 RID: 4361
		private static volatile string m_userConfig;
	}
}
