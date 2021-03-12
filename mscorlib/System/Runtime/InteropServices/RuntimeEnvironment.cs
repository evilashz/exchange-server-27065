using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000927 RID: 2343
	[ComVisible(true)]
	public class RuntimeEnvironment
	{
		// Token: 0x06006094 RID: 24724 RVA: 0x00149A60 File Offset: 0x00147C60
		[Obsolete("Do not create instances of the RuntimeEnvironment class.  Call the static methods directly on this type instead", true)]
		public RuntimeEnvironment()
		{
		}

		// Token: 0x06006095 RID: 24725
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetModuleFileName();

		// Token: 0x06006096 RID: 24726
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetDeveloperPath();

		// Token: 0x06006097 RID: 24727
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetHostBindingFile();

		// Token: 0x06006098 RID: 24728
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void _GetSystemVersion(StringHandleOnStack retVer);

		// Token: 0x06006099 RID: 24729 RVA: 0x00149A68 File Offset: 0x00147C68
		public static bool FromGlobalAccessCache(Assembly a)
		{
			return a.GlobalAssemblyCache;
		}

		// Token: 0x0600609A RID: 24730 RVA: 0x00149A70 File Offset: 0x00147C70
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string GetSystemVersion()
		{
			string result = null;
			RuntimeEnvironment._GetSystemVersion(JitHelpers.GetStringHandleOnStack(ref result));
			return result;
		}

		// Token: 0x0600609B RID: 24731 RVA: 0x00149A8C File Offset: 0x00147C8C
		[SecuritySafeCritical]
		public static string GetRuntimeDirectory()
		{
			string runtimeDirectoryImpl = RuntimeEnvironment.GetRuntimeDirectoryImpl();
			new FileIOPermission(FileIOPermissionAccess.PathDiscovery, runtimeDirectoryImpl).Demand();
			return runtimeDirectoryImpl;
		}

		// Token: 0x0600609C RID: 24732
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetRuntimeDirectoryImpl();

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x0600609D RID: 24733 RVA: 0x00149AAC File Offset: 0x00147CAC
		public static string SystemConfigurationFile
		{
			[SecuritySafeCritical]
			get
			{
				StringBuilder stringBuilder = new StringBuilder(260);
				stringBuilder.Append(RuntimeEnvironment.GetRuntimeDirectory());
				stringBuilder.Append(AppDomainSetup.RuntimeConfigurationFile);
				string text = stringBuilder.ToString();
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
				return text;
			}
		}

		// Token: 0x0600609E RID: 24734
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr GetRuntimeInterfaceImpl([MarshalAs(UnmanagedType.LPStruct)] [In] Guid clsid, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid);

		// Token: 0x0600609F RID: 24735 RVA: 0x00149AF0 File Offset: 0x00147CF0
		[SecurityCritical]
		[ComVisible(false)]
		public static IntPtr GetRuntimeInterfaceAsIntPtr(Guid clsid, Guid riid)
		{
			return RuntimeEnvironment.GetRuntimeInterfaceImpl(clsid, riid);
		}

		// Token: 0x060060A0 RID: 24736 RVA: 0x00149AFC File Offset: 0x00147CFC
		[SecurityCritical]
		[ComVisible(false)]
		public static object GetRuntimeInterfaceAsObject(Guid clsid, Guid riid)
		{
			IntPtr intPtr = IntPtr.Zero;
			object objectForIUnknown;
			try
			{
				intPtr = RuntimeEnvironment.GetRuntimeInterfaceImpl(clsid, riid);
				objectForIUnknown = Marshal.GetObjectForIUnknown(intPtr);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.Release(intPtr);
				}
			}
			return objectForIUnknown;
		}
	}
}
