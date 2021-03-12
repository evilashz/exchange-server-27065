using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using <CppImplementationDetails>;
using <CrtImplementationDetails>;

// Token: 0x02000001 RID: 1
internal class <Module>
{
	// Token: 0x06000001 RID: 1 RVA: 0x00001504 File Offset: 0x00000904
	internal unsafe static ushort* GetUnmanagedUni(string pstr)
	{
		return (ushort*)Marshal.StringToHGlobalUni(pstr).ToPointer();
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00001520 File Offset: 0x00000920
	internal unsafe static void FreeUnmanagedUni(ushort* sz)
	{
		if (sz != null)
		{
			IntPtr hglobal = new IntPtr((void*)sz);
			Marshal.FreeHGlobal(hglobal);
		}
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00001540 File Offset: 0x00000940
	internal unsafe static byte[] MakeManagedBytes(byte* _pb, int _cb)
	{
		byte[] array = new byte[_cb];
		if (_cb > 0)
		{
			Marshal.Copy((IntPtr)((void*)_pb), array, 0, _cb);
		}
		return array;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00001568 File Offset: 0x00000968
	internal static int GetEC(int hr)
	{
		if (-939585531 == hr || -939585532 == hr)
		{
			return Marshal.GetLastWin32Error();
		}
		if (-939587625 == hr)
		{
			return 10;
		}
		if (-939647163 == hr)
		{
			return 11;
		}
		if (21 == hr)
		{
			return 17;
		}
		return (-939587619 == hr) ? 18 : hr;
	}

	// Token: 0x06000005 RID: 5
	[SuppressUnmanagedCodeSecurity]
	[DllImport("Microsoft.Exchange.Cluster.ReplicaSeeder.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public static extern void CleanupLogger();

	// Token: 0x06000006 RID: 6
	[SuppressUnmanagedCodeSecurity]
	[DllImport("Microsoft.Exchange.Cluster.ReplicaSeeder.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public static extern void SetupLogger();

	// Token: 0x06000007 RID: 7
	[DllImport("esebcli2.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public unsafe static extern int HrESESeedForceNewLog(void* hccxBackupContext);

	// Token: 0x06000008 RID: 8
	[DllImport("esebcli2.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public unsafe static extern int HrESESeedGetDatabaseInfo(void* hccxBackupContext, ushort* wszDatabase, void* pvBuffer, uint cbBuffer, uint* pcbActual, uint fFlags);

	// Token: 0x06000009 RID: 9
	[DllImport("esebcli2.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public unsafe static extern int HrESESeedReadPages(void* hccxBackupContext, ushort* wszDatabase, uint pgnoStart, uint cpg, void* pvBuffer, uint cbBuffer, uint* pcbRead, uint fFlags, uint* pulGenLow, uint* pulGenHigh);

	// Token: 0x0600000A RID: 10
	[SuppressUnmanagedCodeSecurity]
	[DllImport("Microsoft.Exchange.Cluster.ReplicaSeeder.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public unsafe static extern int EcDoBackupReadFileEx(void* hccxBackupContext, void* hSourceFile, long cbSourceFileOffset, byte* pbOutputBuffer, uint cbSourceFileToRead, uint* pcbSourceFileActuallyRead);

	// Token: 0x0600000B RID: 11
	[DllImport("esebcli2.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public unsafe static extern int HrESEBackupCloseFile(void* hccxBackupContext, void* hFile);

	// Token: 0x0600000C RID: 12
	[DllImport("esebcli2.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public unsafe static extern int HrESEBackupOpenFile(void* hccxBackupContext, ushort* wszFileName, uint cbReadHintSize, uint cSections, void** rghFile, long* rgliSectionSize);

	// Token: 0x0600000D RID: 13
	[DllImport("esebcli2.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public unsafe static extern void ESEBackupRestoreFreeRegisteredInfo(uint cRegisteredInfo, _ESE_REGISTERED_INFO* aRegisteredInfo);

	// Token: 0x0600000E RID: 14
	[DllImport("esebcli2.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public unsafe static extern int HrESEBackupEnd(void* hccxBackupContext);

	// Token: 0x0600000F RID: 15
	[DllImport("esebcli2.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public unsafe static extern int HrESEBackupInstanceEnd(void* hccxBackupContext, uint fFlags);

	// Token: 0x06000010 RID: 16
	[DllImport("esebcli2.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public unsafe static extern void ESEBackupFreeInstanceInfo(uint cInstanceInfo, _INSTANCE_BACKUP_INFO* aInstanceInfo);

	// Token: 0x06000011 RID: 17
	[SuppressUnmanagedCodeSecurity]
	[DllImport("Microsoft.Exchange.Cluster.ReplicaSeeder.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public unsafe static extern int EcDoHrESEBackupSetup(void* hccxBackupContext, long hInstanceId, uint btBackupType, ushort* wszTransferAddrs);

	// Token: 0x06000012 RID: 18
	[DllImport("esebcli2.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public unsafe static extern int HrESESeedPrepare(ushort* wszBackupServer, ushort* wszBackupAnnotation, uint fFlags, uint ulTimeoutInMsec, uint* pcInstanceInfo, _INSTANCE_BACKUP_INFO** paInstanceInfo, void** phccxBackupContext);

	// Token: 0x06000013 RID: 19
	[DllImport("esebcli2.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public unsafe static extern int HrESEBackupRestoreGetRegisteredEx(ushort* wszServerName, ushort* wszDisplayName, uint fFlags, uint ulTimeoutInMsec, uint* pcRegisteredInfo, _ESE_REGISTERED_INFO** paRegisteredInfo);

	// Token: 0x06000014 RID: 20 RVA: 0x000021F8 File Offset: 0x000015F8
	internal unsafe static ushort* GetUnmanagedUni(string pstr)
	{
		return (ushort*)Marshal.StringToHGlobalUni(pstr).ToPointer();
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002214 File Offset: 0x00001614
	internal unsafe static void FreeUnmanagedUni(ushort* sz)
	{
		IntPtr hglobal = new IntPtr((void*)sz);
		Marshal.FreeHGlobal(hglobal);
	}

	// Token: 0x06000016 RID: 22
	[DllImport("esebcli2.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public unsafe static extern int HrESELogShipOpenEx(ushort* wszLogShipServer, ushort* wszLogShipAnnotation, ushort* wszSGGuid, ushort* wszClientId, ushort* wszSGBaseName, ushort* wszSGLogFilePath, int fCircularLogging, uint ulFlags, uint ulMsecTimeout, void** phccxLogShipContext);

	// Token: 0x06000017 RID: 23
	[DllImport("esebcli2.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public unsafe static extern int HrESELogShipClose(void* hccxLogShipContext);

	// Token: 0x06000018 RID: 24
	[DllImport("esebcli2.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	public unsafe static extern int HrESELogShipSuccessful(void* hccxLogShipContext, int lgenReplayed, int* plgenTruncated, uint grfFlags);

	// Token: 0x06000019 RID: 25 RVA: 0x00003350 File Offset: 0x00002750
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.NativeDll.IsInDllMain()
	{
		return (<Module>.__native_dllmain_reason != uint.MaxValue) ? 1 : 0;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000336C File Offset: 0x0000276C
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.NativeDll.IsInProcessAttach()
	{
		return (<Module>.__native_dllmain_reason == 1U) ? 1 : 0;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00003384 File Offset: 0x00002784
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.NativeDll.IsInProcessDetach()
	{
		return (<Module>.__native_dllmain_reason == 0U) ? 1 : 0;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x0000339C File Offset: 0x0000279C
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.NativeDll.IsSafeForManagedCode()
	{
		if (((<Module>.__native_dllmain_reason != 4294967295U) ? 1 : 0) == 0)
		{
			return 1;
		}
		int num;
		if (((<Module>.__native_dllmain_reason == 1U) ? 1 : 0) == 0 && ((<Module>.__native_dllmain_reason == 0U) ? 1 : 0) == 0)
		{
			num = 1;
		}
		else
		{
			num = 0;
		}
		return (byte)num;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00003BD4 File Offset: 0x00002FD4
	internal static void <CrtImplementationDetails>.ThrowNestedModuleLoadException(System.Exception innerException, System.Exception nestedException)
	{
		throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, nestedException);
	}

	// Token: 0x0600001E RID: 30 RVA: 0x0000359C File Offset: 0x0000299C
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage)
	{
		throw new ModuleLoadException(errorMessage);
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000035B0 File Offset: 0x000029B0
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage, System.Exception innerException)
	{
		throw new ModuleLoadException(errorMessage, innerException);
	}

	// Token: 0x06000020 RID: 32 RVA: 0x000036CC File Offset: 0x00002ACC
	internal static void <CrtImplementationDetails>.RegisterModuleUninitializer(EventHandler handler)
	{
		ModuleUninitializer._ModuleUninitializer.AddHandler(handler);
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000036E4 File Offset: 0x00002AE4
	[SecuritySafeCritical]
	internal unsafe static Guid <CrtImplementationDetails>.FromGUID(_GUID* guid)
	{
		Guid result = new Guid((uint)(*guid), *(guid + 4L), *(guid + 6L), *(guid + 8L), *(guid + 9L), *(guid + 10L), *(guid + 11L), *(guid + 12L), *(guid + 13L), *(guid + 14L), *(guid + 15L));
		return result;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00003734 File Offset: 0x00002B34
	[SecurityCritical]
	internal unsafe static int __get_default_appdomain(IUnknown** ppUnk)
	{
		ICorRuntimeHost* ptr = null;
		int num;
		try
		{
			Guid riid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e);
			Guid clsid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e);
			ptr = (ICorRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(clsid, riid).ToPointer();
			goto IL_3E;
		}
		catch (System.Exception e)
		{
			num = Marshal.GetHRForException(e);
		}
		if (num < 0)
		{
			return num;
		}
		IL_3E:
		num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown**), ptr, ppUnk, *(*(long*)ptr + 104L));
		ICorRuntimeHost* ptr2 = ptr;
		object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
		return num;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x000037BC File Offset: 0x00002BBC
	internal unsafe static void __release_appdomain(IUnknown* ppUnk)
	{
		object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ppUnk, *(*(long*)ppUnk + 16L));
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000037D8 File Offset: 0x00002BD8
	[SecurityCritical]
	internal unsafe static AppDomain <CrtImplementationDetails>.GetDefaultDomain()
	{
		IUnknown* ptr = null;
		int num = <Module>.__get_default_appdomain(&ptr);
		if (num >= 0)
		{
			try
			{
				IntPtr pUnk = new IntPtr((void*)ptr);
				return (AppDomain)Marshal.GetObjectForIUnknown(pUnk);
			}
			finally
			{
				<Module>.__release_appdomain(ptr);
			}
		}
		Marshal.ThrowExceptionForHR(num);
		return null;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00003838 File Offset: 0x00002C38
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.DoCallBackInDefaultDomain(method function, void* cookie)
	{
		Guid riid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02);
		ICLRRuntimeHost* ptr = (ICLRRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02), riid).ToPointer();
		try
		{
			AppDomain appDomain = <Module>.<CrtImplementationDetails>.GetDefaultDomain();
			int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl) (System.Void*),System.Void*), ptr, appDomain.Id, function, cookie, *(*(long*)ptr + 64L));
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}
		finally
		{
			ICLRRuntimeHost* ptr2 = ptr;
			object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, *(*(long*)ptr2 + 16L));
		}
	}

	// Token: 0x06000026 RID: 38 RVA: 0x000038FC File Offset: 0x00002CFC
	[SecuritySafeCritical]
	internal unsafe static int <CrtImplementationDetails>.DefaultDomain.DoNothing(void* cookie)
	{
		GC.KeepAlive(int.MaxValue);
		return 0;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x0000391C File Offset: 0x00002D1C
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasPerProcess()
	{
		if (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A == (TriBool.State)2)
		{
			void** ptr = (void**)(&<Module>.?A0xc6f23461.__xc_mp_a);
			if (ref <Module>.?A0xc6f23461.__xc_mp_a < ref <Module>.?A0xc6f23461.__xc_mp_z)
			{
				while (*(long*)ptr == 0L)
				{
					ptr += 8L / (long)sizeof(void*);
					if (ptr >= (void**)(&<Module>.?A0xc6f23461.__xc_mp_z))
					{
						goto IL_35;
					}
				}
				<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A = (TriBool.State)(-1);
				return 1;
			}
			IL_35:
			<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A = (TriBool.State)0;
			return 0;
		}
		return (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A == (TriBool.State)(-1)) ? 1 : 0;
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00003970 File Offset: 0x00002D70
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasNative()
	{
		if (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A == (TriBool.State)2)
		{
			void** ptr = (void**)(&<Module>.__xi_a);
			if (ref <Module>.__xi_a < ref <Module>.__xi_z)
			{
				while (*(long*)ptr == 0L)
				{
					ptr += 8L / (long)sizeof(void*);
					if (ptr >= (void**)(&<Module>.__xi_z))
					{
						goto IL_35;
					}
				}
				<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A = (TriBool.State)(-1);
				return 1;
			}
			IL_35:
			void** ptr2 = (void**)(&<Module>.__xc_a);
			if (ref <Module>.__xc_a < ref <Module>.__xc_z)
			{
				while (*(long*)ptr2 == 0L)
				{
					ptr2 += 8L / (long)sizeof(void*);
					if (ptr2 >= (void**)(&<Module>.__xc_z))
					{
						goto IL_62;
					}
				}
				<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A = (TriBool.State)(-1);
				return 1;
			}
			IL_62:
			<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A = (TriBool.State)0;
			return 0;
		}
		return (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A == (TriBool.State)(-1)) ? 1 : 0;
	}

	// Token: 0x06000029 RID: 41 RVA: 0x000039F0 File Offset: 0x00002DF0
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsInitialization()
	{
		int num;
		if ((<Module>.<CrtImplementationDetails>.DefaultDomain.HasPerProcess() != null && !<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA) || (<Module>.<CrtImplementationDetails>.DefaultDomain.HasNative() != null && !<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA && <Module>.__native_startup_state == (__enative_startup_state)0))
		{
			num = 1;
		}
		else
		{
			num = 0;
		}
		return (byte)num;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00003A2C File Offset: 0x00002E2C
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsUninitialization()
	{
		return <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00003A40 File Offset: 0x00002E40
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.DefaultDomain.Initialize()
	{
		<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00005440 File Offset: 0x00004840
	internal static void ??__E?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00005454 File Offset: 0x00004854
	internal static void ??__E?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00005468 File Offset: 0x00004868
	internal static void ??__E?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA@@YMXXZ()
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = false;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x0000547C File Offset: 0x0000487C
	internal static void ??__E?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)0;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00005490 File Offset: 0x00004890
	internal static void ??__E?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)0;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000054A4 File Offset: 0x000048A4
	internal static void ??__E?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)0;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x000054B8 File Offset: 0x000048B8
	internal static void ??__E?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)0;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00003C28 File Offset: 0x00003028
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeVtables(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during vtable initialization.\n");
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)1;
		<Module>._initterm_m((method*)(&<Module>.?A0xc6f23461.__xi_vt_a), (method*)(&<Module>.?A0xc6f23461.__xi_vt_z));
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)2;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00003C5C File Offset: 0x0000305C
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load while attempting to initialize the default appdomain.\n");
		<Module>.<CrtImplementationDetails>.DefaultDomain.Initialize();
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00003C7C File Offset: 0x0000307C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeNative(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during native initialization.\n");
		<Module>.__security_init_cookie();
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
		if (<Module>.<CrtImplementationDetails>.NativeDll.IsSafeForManagedCode() == null)
		{
			<Module>._amsg_exit(33);
		}
		if (<Module>.__native_startup_state == (__enative_startup_state)1)
		{
			<Module>._amsg_exit(33);
		}
		else if (<Module>.__native_startup_state == (__enative_startup_state)0)
		{
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)1;
			<Module>.__native_startup_state = (__enative_startup_state)1;
			if (<Module>._initterm_e((method*)(&<Module>.__xi_a), (method*)(&<Module>.__xi_z)) != 0)
			{
				<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0));
			}
			<Module>._initterm((method*)(&<Module>.__xc_a), (method*)(&<Module>.__xc_z));
			<Module>.__native_startup_state = (__enative_startup_state)2;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)2;
		}
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00003D18 File Offset: 0x00003118
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerProcess(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during process initialization.\n");
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)1;
		<Module>._initatexit_m();
		<Module>._initterm_m((method*)(&<Module>.?A0xc6f23461.__xc_mp_a), (method*)(&<Module>.?A0xc6f23461.__xc_mp_z));
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)2;
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00003D58 File Offset: 0x00003158
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during appdomain initialization.\n");
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)1;
		<Module>._initatexit_app_domain();
		<Module>._initterm_m((method*)(&<Module>.?A0xc6f23461.__xc_ma_a), (method*)(&<Module>.?A0xc6f23461.__xc_ma_z));
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)2;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00003D94 File Offset: 0x00003194
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during registration for the unload events.\n");
		<Module>.<CrtImplementationDetails>.RegisterModuleUninitializer(new EventHandler(<Module>.<CrtImplementationDetails>.LanguageSupport.DomainUnload));
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00003DC0 File Offset: 0x000031C0
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport._Initialize(LanguageSupport* A_0)
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = AppDomain.CurrentDomain.IsDefaultAppDomain();
		<Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA = (<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA || <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA);
		void* ptr = <Module>._getFiberPtrId();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			while (num2 == 0)
			{
				try
				{
				}
				finally
				{
					void* ptr2 = Interlocked.CompareExchange(ref <Module>.__native_startup_lock, ptr, 0L);
					if (ptr2 == null)
					{
						num2 = 1;
					}
					else if (ptr2 == ptr)
					{
						num = 1;
						num2 = 1;
					}
				}
				if (num2 == 0)
				{
					<Module>.Sleep(1000);
				}
			}
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeVtables(A_0);
			if (<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeNative(A_0);
				<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerProcess(A_0);
			}
			else
			{
				num3 = ((<Module>.<CrtImplementationDetails>.DefaultDomain.NeedsInitialization() != 0) ? 1 : num3);
			}
		}
		finally
		{
			if (num == 0)
			{
				Interlocked.Exchange(ref <Module>.__native_startup_lock, 0L);
			}
		}
		if (num3 != 0)
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(A_0);
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(A_0);
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 1;
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(A_0);
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00003A5C File Offset: 0x00002E5C
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain()
	{
		<Module>._app_exit_callback();
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00003A70 File Offset: 0x00002E70
	[SecurityCritical]
	internal unsafe static int <CrtImplementationDetails>.LanguageSupport._UninitializeDefaultDomain(void* cookie)
	{
		<Module>._exit_callback();
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		if (<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA)
		{
			<Module>._cexit();
			<Module>.__native_startup_state = (__enative_startup_state)0;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		}
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		return 0;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00003AAC File Offset: 0x00002EAC
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain()
	{
		if (<Module>.<CrtImplementationDetails>.DefaultDomain.NeedsUninitialization() != null)
		{
			if (AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport._UninitializeDefaultDomain(null);
			}
			else
			{
				<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
			}
		}
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00003AE4 File Offset: 0x00002EE4
	[SecurityCritical]
	[PrePrepareMethod]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal static void <CrtImplementationDetails>.LanguageSupport.DomainUnload(object source, EventArgs arguments)
	{
		if (<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA != 0 && Interlocked.Exchange(ref <Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA, 1) == 0)
		{
			byte b = (Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0) ? 1 : 0;
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
			if (b != 0)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00003EC4 File Offset: 0x000032C4
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Cleanup(LanguageSupport* A_0, System.Exception innerException)
	{
		try
		{
			bool flag = ((Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0) ? 1 : 0) != 0;
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
		catch (System.Exception nestedException)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, nestedException);
		}
		catch (object obj)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, null);
		}
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00003F38 File Offset: 0x00003338
	[SecurityCritical]
	internal unsafe static LanguageSupport* <CrtImplementationDetails>.LanguageSupport.{ctor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{ctor}(A_0);
		return A_0;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00003F50 File Offset: 0x00003350
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.{dtor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{dtor}(A_0);
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00003F64 File Offset: 0x00003364
	[SecurityCritical]
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Initialize(LanguageSupport* A_0)
	{
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load.\n");
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				Interlocked.Increment(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA);
				flag = true;
			}
			<Module>.<CrtImplementationDetails>.LanguageSupport._Initialize(A_0);
		}
		catch (System.Exception innerException)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, innerException);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), innerException);
		}
		catch (object obj)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, null);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), null);
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00004020 File Offset: 0x00003420
	[DebuggerStepThrough]
	[SecurityCritical]
	static unsafe <Module>()
	{
		LanguageSupport languageSupport;
		<Module>.<CrtImplementationDetails>.LanguageSupport.{ctor}(ref languageSupport);
		try
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.Initialize(ref languageSupport);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(<CrtImplementationDetails>.LanguageSupport.{dtor}), (void*)(&languageSupport));
			throw;
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.{dtor}(ref languageSupport);
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00003B20 File Offset: 0x00002F20
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static gcroot<System::String\u0020^>* {ctor}(gcroot<System::String\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00003B44 File Offset: 0x00002F44
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void {dtor}(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Free();
		*A_0 = 0L;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00003B6C File Offset: 0x00002F6C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<System::String\u0020^>* =(gcroot<System::String\u0020^>* A_0, string t)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = t;
		return A_0;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00003B94 File Offset: 0x00002F94
	[SecuritySafeCritical]
	internal unsafe static string PE$AAVString@System@@(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		return ((GCHandle)value).Target;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00004094 File Offset: 0x00003494
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static ValueType <CrtImplementationDetails>.AtExitLock._handle()
	{
		if (<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA != null)
		{
			IntPtr value = new IntPtr(<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA);
			return GCHandle.FromIntPtr(value);
		}
		return null;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x0000459C File Offset: 0x0000399C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Construct(object value)
	{
		<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000040C4 File Offset: 0x000034C4
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Set(object value)
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType == null)
		{
			valueType = GCHandle.Alloc(value);
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = GCHandle.ToIntPtr((GCHandle)valueType).ToPointer();
		}
		else
		{
			((GCHandle)valueType).Target = value;
		}
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00004114 File Offset: 0x00003514
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static object <CrtImplementationDetails>.AtExitLock._lock_Get()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType != null)
		{
			return ((GCHandle)valueType).Target;
		}
		return null;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00004138 File Offset: 0x00003538
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Destruct()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType != null)
		{
			((GCHandle)valueType).Free();
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		}
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00004160 File Offset: 0x00003560
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.AtExitLock.IsInitialized()
	{
		return (<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x000045B8 File Offset: 0x000039B8
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.AddRef()
	{
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() == null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Construct(new object());
			<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA = 0;
		}
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA++;
	}

	// Token: 0x0600004E RID: 78 RVA: 0x0000417C File Offset: 0x0000357C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.RemoveRef()
	{
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA += -1;
		if (<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA == 0)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Destruct();
		}
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000041A4 File Offset: 0x000035A4
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.Enter()
	{
		Monitor.Enter(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000041BC File Offset: 0x000035BC
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.Exit()
	{
		Monitor.Exit(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x06000051 RID: 81 RVA: 0x000041D4 File Offset: 0x000035D4
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __global_lock()
	{
		bool result = false;
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() != null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock.Enter();
			result = true;
		}
		return result;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x000041F4 File Offset: 0x000035F4
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __global_unlock()
	{
		bool result = false;
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() != null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock.Exit();
			result = true;
		}
		return result;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x000045E8 File Offset: 0x000039E8
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __alloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.AddRef();
		return <Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized();
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00004214 File Offset: 0x00003614
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void __dealloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.RemoveRef();
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00004228 File Offset: 0x00003628
	[SecurityCritical]
	internal unsafe static int _atexit_helper(method func, ulong* __pexit_list_size, method** __ponexitend_e, method** __ponexitbegin_e)
	{
		method system.Void_u0020() = 0L;
		if (func == null)
		{
			return -1;
		}
		if (<Module>.?A0xf28fb846.__global_lock() == 1)
		{
			try
			{
				method* ptr = (method*)<Module>.DecodePointer(*(long*)__ponexitbegin_e);
				method* ptr2 = (method*)<Module>.DecodePointer(*(long*)__ponexitend_e);
				long num = (long)(ptr2 - ptr);
				if (*__pexit_list_size - 1UL < (ulong)num >> 3)
				{
					try
					{
						ulong num2 = *__pexit_list_size * 8UL;
						ulong num3 = (num2 < 4096UL) ? num2 : 4096UL;
						IntPtr cb = new IntPtr((int)(num2 + num3));
						IntPtr pv = new IntPtr((void*)ptr);
						IntPtr intPtr = Marshal.ReAllocHGlobal(pv, cb);
						IntPtr intPtr2 = intPtr;
						ptr2 = (method*)((byte*)intPtr2.ToPointer() + num);
						ptr = (method*)intPtr2.ToPointer();
						ulong num4 = *__pexit_list_size;
						ulong num5 = (512UL < num4) ? 512UL : num4;
						*__pexit_list_size = num4 + num5;
					}
					catch (OutOfMemoryException)
					{
						IntPtr cb2 = new IntPtr((int)(*__pexit_list_size * 8UL + 12UL));
						IntPtr pv2 = new IntPtr((void*)ptr);
						IntPtr intPtr3 = Marshal.ReAllocHGlobal(pv2, cb2);
						IntPtr intPtr4 = intPtr3;
						ptr2 = (intPtr4.ToPointer() - ptr) / (IntPtr)sizeof(method) + ptr2;
						ptr = (method*)intPtr4.ToPointer();
						*__pexit_list_size += 4UL;
					}
				}
				*(long*)ptr2 = func;
				ptr2 += 8L / (long)sizeof(method);
				system.Void_u0020() = func;
				*(long*)__ponexitbegin_e = <Module>.EncodePointer((void*)ptr);
				*(long*)__ponexitend_e = <Module>.EncodePointer((void*)ptr2);
			}
			catch (OutOfMemoryException)
			{
			}
			finally
			{
				<Module>.?A0xf28fb846.__global_unlock();
			}
			if (system.Void_u0020() != null)
			{
				return 0;
			}
		}
		return -1;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x000043B0 File Offset: 0x000037B0
	[SecurityCritical]
	internal unsafe static void _exit_callback()
	{
		if (<Module>.?A0xf28fb846.__exit_list_size != 0UL)
		{
			method* ptr = (method*)<Module>.DecodePointer((void*)<Module>.?A0xf28fb846.__onexitbegin_m);
			method* ptr2 = (method*)<Module>.DecodePointer((void*)<Module>.?A0xf28fb846.__onexitend_m);
			if (ptr != -1L && ptr != null && ptr2 != null)
			{
				method* ptr3 = ptr;
				method* ptr4 = ptr2;
				for (;;)
				{
					ptr2 -= 8L / (long)sizeof(method);
					if (ptr2 < ptr)
					{
						break;
					}
					if (*(long*)ptr2 != <Module>.EncodePointer(null))
					{
						void* ptr5 = <Module>.DecodePointer(*(long*)ptr2);
						*(long*)ptr2 = <Module>.EncodePointer(null);
						calli(System.Void(), ptr5);
						method* ptr6 = (method*)<Module>.DecodePointer((void*)<Module>.?A0xf28fb846.__onexitbegin_m);
						method* ptr7 = (method*)<Module>.DecodePointer((void*)<Module>.?A0xf28fb846.__onexitend_m);
						if (ptr3 != ptr6 || ptr4 != ptr7)
						{
							ptr3 = ptr6;
							ptr = ptr6;
							ptr4 = ptr7;
							ptr2 = ptr7;
						}
					}
				}
				IntPtr hglobal = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(hglobal);
			}
			<Module>.?A0xf28fb846.__dealloc_global_lock();
		}
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00004600 File Offset: 0x00003A00
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static int _initatexit_m()
	{
		int result = 0;
		if (<Module>.?A0xf28fb846.__alloc_global_lock() == 1)
		{
			<Module>.?A0xf28fb846.__onexitbegin_m = (method*)<Module>.EncodePointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.?A0xf28fb846.__onexitend_m = <Module>.?A0xf28fb846.__onexitbegin_m;
			<Module>.?A0xf28fb846.__exit_list_size = 32UL;
			result = 1;
		}
		return result;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00004648 File Offset: 0x00003A48
	internal static method _onexit_m(method _Function)
	{
		return (<Module>._atexit_m(_Function) == -1) ? 0L : _Function;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00004460 File Offset: 0x00003860
	[SecurityCritical]
	internal unsafe static int _atexit_m(method func)
	{
		return <Module>._atexit_helper(<Module>.EncodePointer(func), &<Module>.?A0xf28fb846.__exit_list_size, &<Module>.?A0xf28fb846.__onexitend_m, &<Module>.?A0xf28fb846.__onexitbegin_m);
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00004664 File Offset: 0x00003A64
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _initatexit_app_domain()
	{
		if (<Module>.?A0xf28fb846.__alloc_global_lock() == 1)
		{
			<Module>.__onexitbegin_app_domain = (method*)<Module>.EncodePointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.__onexitend_app_domain = <Module>.__onexitbegin_app_domain;
			<Module>.__exit_list_size_app_domain = 32UL;
		}
		return 1;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00004488 File Offset: 0x00003888
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	internal unsafe static void _app_exit_callback()
	{
		if (<Module>.__exit_list_size_app_domain != 0UL)
		{
			method* ptr = (method*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
			method* ptr2 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
			try
			{
				if (ptr != -1L && ptr != null && ptr2 != null)
				{
					method* ptr3 = ptr;
					method* ptr4 = ptr2;
					for (;;)
					{
						do
						{
							ptr2 -= 8L / (long)sizeof(method);
						}
						while (ptr2 >= ptr && *(long*)ptr2 == <Module>.EncodePointer(null));
						if (ptr2 < ptr)
						{
							break;
						}
						method system.Void_u0020() = <Module>.DecodePointer(*(long*)ptr2);
						*(long*)ptr2 = <Module>.EncodePointer(null);
						calli(System.Void(), system.Void_u0020());
						method* ptr5 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
						method* ptr6 = (method*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
						if (ptr3 != ptr5 || ptr4 != ptr6)
						{
							ptr3 = ptr5;
							ptr = ptr5;
							ptr4 = ptr6;
							ptr2 = ptr6;
						}
					}
				}
			}
			finally
			{
				IntPtr hglobal = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(hglobal);
				<Module>.?A0xf28fb846.__dealloc_global_lock();
			}
		}
	}

	// Token: 0x0600005C RID: 92 RVA: 0x000046A8 File Offset: 0x00003AA8
	[SecurityCritical]
	internal static method _onexit_m_appdomain(method _Function)
	{
		return (<Module>._atexit_m_appdomain(_Function) == -1) ? 0L : _Function;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00004574 File Offset: 0x00003974
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _atexit_m_appdomain(method func)
	{
		return <Module>._atexit_helper(<Module>.EncodePointer(func), &<Module>.__exit_list_size_app_domain, &<Module>.__onexitend_app_domain, &<Module>.__onexitbegin_app_domain);
	}

	// Token: 0x0600005E RID: 94
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* DecodePointer(void* Ptr);

	// Token: 0x0600005F RID: 95
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* EncodePointer(void* Ptr);

	// Token: 0x06000060 RID: 96 RVA: 0x000046C4 File Offset: 0x00003AC4
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static int _initterm_e(method* pfbegin, method* pfend)
	{
		int num = 0;
		if (pfbegin < pfend)
		{
			while (num == 0)
			{
				ulong num2 = (ulong)(*(long*)pfbegin);
				if (num2 != 0UL)
				{
					num = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(), num2);
				}
				pfbegin += 8L / (long)sizeof(method);
				if (pfbegin >= pfend)
				{
					break;
				}
			}
		}
		return num;
	}

	// Token: 0x06000061 RID: 97 RVA: 0x000046F4 File Offset: 0x00003AF4
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void _initterm(method* pfbegin, method* pfend)
	{
		if (pfbegin < pfend)
		{
			do
			{
				ulong num = (ulong)(*(long*)pfbegin);
				if (num != 0UL)
				{
					calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(), num);
				}
				pfbegin += 8L / (long)sizeof(method);
			}
			while (pfbegin < pfend);
		}
	}

	// Token: 0x06000062 RID: 98 RVA: 0x0000471C File Offset: 0x00003B1C
	[DebuggerStepThrough]
	internal static ModuleHandle <CrtImplementationDetails>.ThisModule.Handle()
	{
		return typeof(ThisModule).Module.ModuleHandle;
	}

	// Token: 0x06000063 RID: 99 RVA: 0x0000476C File Offset: 0x00003B6C
	[DebuggerStepThrough]
	[SecurityCritical]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void _initterm_m(method* pfbegin, method* pfend)
	{
		if (pfbegin < pfend)
		{
			do
			{
				ulong num = (ulong)(*(long*)pfbegin);
				if (num != 0UL)
				{
					object obj = calli(System.Void modopt(System.Runtime.CompilerServices.IsConst)*(), <Module>.<CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(num));
				}
				pfbegin += 8L / (long)sizeof(method);
			}
			while (pfbegin < pfend);
		}
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00004740 File Offset: 0x00003B40
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static method <CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(method methodToken)
	{
		return <Module>.<CrtImplementationDetails>.ThisModule.Handle().ResolveMethodHandle(methodToken).GetFunctionPointer().ToPointer();
	}

	// Token: 0x06000065 RID: 101 RVA: 0x0000479C File Offset: 0x00003B9C
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void ___CxxCallUnwindDtor(method pDtor, void* pThis)
	{
		try
		{
			calli(System.Void(System.Void*), pThis, pDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x06000066 RID: 102 RVA: 0x000047E0 File Offset: 0x00003BE0
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void ___CxxCallUnwindDelDtor(method pDtor, void* pThis)
	{
		try
		{
			calli(System.Void(System.Void*), pThis, pDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00004824 File Offset: 0x00003C24
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void ___CxxCallUnwindVecDtor(method pVecDtor, void* ptr, ulong size, int count, method pDtor)
	{
		try
		{
			calli(System.Void(System.Void*,System.UInt64,System.Int32,System.Void (System.Void*)), ptr, size, count, pDtor, pVecDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00001060 File Offset: 0x00000460
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static extern bool IsSpaceEnough(long, long, ushort*);

	// Token: 0x06000069 RID: 105 RVA: 0x00005380 File Offset: 0x00004780
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern int VirtualFree(void*, ulong, uint);

	// Token: 0x0600006A RID: 106 RVA: 0x0000537A File Offset: 0x0000477A
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void* VirtualAlloc(void*, ulong, uint, uint);

	// Token: 0x0600006B RID: 107 RVA: 0x00005368 File Offset: 0x00004768
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern int _wcsicmp(ushort*, ushort*);

	// Token: 0x0600006C RID: 108 RVA: 0x000038EC File Offset: 0x00002CEC
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void* _getFiberPtrId();

	// Token: 0x0600006D RID: 109 RVA: 0x00002C80 File Offset: 0x00002080
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void _amsg_exit(int);

	// Token: 0x0600006E RID: 110 RVA: 0x00002E80 File Offset: 0x00002280
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal static extern void __security_init_cookie();

	// Token: 0x0600006F RID: 111 RVA: 0x00005386 File Offset: 0x00004786
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void Sleep(uint);

	// Token: 0x06000070 RID: 112 RVA: 0x0000536E File Offset: 0x0000476E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void _cexit();

	// Token: 0x06000071 RID: 113 RVA: 0x00005374 File Offset: 0x00004774
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern int __FrameUnwindFilter(_EXCEPTION_POINTERS*);

	// Token: 0x04000001 RID: 1 RVA: 0x000079F8 File Offset: 0x000059F8
	internal static _GUID GUID_PROCESSOR_PERF_DECREASE_TIME;

	// Token: 0x04000002 RID: 2 RVA: 0x00007A68 File Offset: 0x00005A68
	internal static _GUID GUID_PROCESSOR_IDLE_TIME_CHECK;

	// Token: 0x04000003 RID: 3 RVA: 0x00007AB8 File Offset: 0x00005AB8
	internal static _GUID GUID_PROCESSOR_CORE_PARKING_INCREASE_POLICY;

	// Token: 0x04000004 RID: 4 RVA: 0x00007B38 File Offset: 0x00005B38
	internal static _GUID GUID_PROCESSOR_CORE_PARKING_AFFINITY_WEIGHTING;

	// Token: 0x04000005 RID: 5 RVA: 0x000077E8 File Offset: 0x000057E8
	internal static _GUID GUID_ALLOW_SYSTEM_REQUIRED;

	// Token: 0x04000006 RID: 6 RVA: 0x000074B8 File Offset: 0x000054B8
	internal static _GUID FIREWALL_PORT_OPEN_GUID;

	// Token: 0x04000007 RID: 7 RVA: 0x000077A8 File Offset: 0x000057A8
	internal static _GUID GUID_ALLOW_AWAYMODE;

	// Token: 0x04000008 RID: 8 RVA: 0x00007698 File Offset: 0x00005698
	internal static _GUID GUID_ALLOW_DISPLAY_REQUIRED;

	// Token: 0x04000009 RID: 9 RVA: 0x00007408 File Offset: 0x00005408
	internal static _GUID PPM_IDLESTATES_DATA_GUID;

	// Token: 0x0400000A RID: 10 RVA: 0x00007AA8 File Offset: 0x00005AA8
	internal static _GUID GUID_PROCESSOR_CORE_PARKING_DECREASE_THRESHOLD;

	// Token: 0x0400000B RID: 11 RVA: 0x00007A58 File Offset: 0x00005A58
	internal static _GUID GUID_PROCESSOR_IDLE_STATE_MAXIMUM;

	// Token: 0x0400000C RID: 12 RVA: 0x00007618 File Offset: 0x00005618
	internal static _GUID GUID_VIDEO_DIM_TIMEOUT;

	// Token: 0x0400000D RID: 13 RVA: 0x000079E8 File Offset: 0x000059E8
	internal static _GUID GUID_PROCESSOR_PERF_INCREASE_TIME;

	// Token: 0x0400000E RID: 14 RVA: 0x000078D8 File Offset: 0x000058D8
	internal static _GUID GUID_BATTERY_DISCHARGE_ACTION_2;

	// Token: 0x0400000F RID: 15 RVA: 0x00007498 File Offset: 0x00005498
	internal static _GUID DOMAIN_JOIN_GUID;

	// Token: 0x04000010 RID: 16 RVA: 0x00007478 File Offset: 0x00005478
	internal static _GUID NETWORK_MANAGER_FIRST_IP_ADDRESS_ARRIVAL_GUID;

	// Token: 0x04000011 RID: 17 RVA: 0x00007788 File Offset: 0x00005788
	internal static _GUID GUID_CRITICAL_POWER_TRANSITION;

	// Token: 0x04000012 RID: 18 RVA: 0x000073D8 File Offset: 0x000053D8
	internal static _GUID PPM_PERFSTATE_DOMAIN_CHANGE_GUID;

	// Token: 0x04000013 RID: 19 RVA: 0x000072F8 File Offset: 0x000052F8
	internal static _GUID GUID_DEVINTERFACE_PARTITION;

	// Token: 0x04000014 RID: 20 RVA: 0x00007598 File Offset: 0x00005598
	internal static _GUID GUID_IDLE_RESILIENCY_SUBGROUP;

	// Token: 0x04000015 RID: 21 RVA: 0x00007A18 File Offset: 0x00005A18
	internal static _GUID GUID_PROCESSOR_PERF_BOOST_POLICY;

	// Token: 0x04000016 RID: 22 RVA: 0x00007BB8 File Offset: 0x00005BB8
	internal static _GUID GUID_PROCESSOR_PARKING_HEADROOM_THRESHOLD;

	// Token: 0x04000017 RID: 23 RVA: 0x00007318 File Offset: 0x00005318
	internal static _GUID GUID_DEVINTERFACE_WRITEONCEDISK;

	// Token: 0x04000018 RID: 24 RVA: 0x00007678 File Offset: 0x00005678
	internal static _GUID GUID_VIDEO_ADAPTIVE_DISPLAY_BRIGHTNESS;

	// Token: 0x04000019 RID: 25 RVA: 0x00007688 File Offset: 0x00005688
	internal static _GUID GUID_CONSOLE_DISPLAY_STATE;

	// Token: 0x0400001A RID: 26 RVA: 0x00007718 File Offset: 0x00005718
	internal static _GUID GUID_DISK_ADAPTIVE_POWERDOWN;

	// Token: 0x0400001B RID: 27 RVA: 0x00007608 File Offset: 0x00005608
	internal static _GUID GUID_VIDEO_ADAPTIVE_PERCENT_INCREASE;

	// Token: 0x0400001C RID: 28 RVA: 0x000075B8 File Offset: 0x000055B8
	internal static _GUID GUID_DISK_COALESCING_POWERDOWN_TIMEOUT;

	// Token: 0x0400001D RID: 29 RVA: 0x000074E8 File Offset: 0x000054E8
	internal static _GUID USER_POLICY_PRESENT_GUID;

	// Token: 0x0400001E RID: 30 RVA: 0x00007BF8 File Offset: 0x00005BF8
	internal static _GUID GUID_SYSTEM_COOLING_POLICY;

	// Token: 0x0400001F RID: 31 RVA: 0x000075F8 File Offset: 0x000055F8
	internal static _GUID GUID_VIDEO_ANNOYANCE_TIMEOUT;

	// Token: 0x04000020 RID: 32 RVA: 0x00007368 File Offset: 0x00005368
	internal static _GUID GUID_DEVINTERFACE_STORAGEPORT;

	// Token: 0x04000021 RID: 33 RVA: 0x00007B68 File Offset: 0x00005B68
	internal static _GUID GUID_PROCESSOR_CORE_PARKING_OVER_UTILIZATION_WEIGHTING;

	// Token: 0x04000022 RID: 34 RVA: 0x00007C88 File Offset: 0x00005C88
	internal static _GUID GUID_IDLE_BACKGROUND_TASK;

	// Token: 0x04000023 RID: 35 RVA: 0x00007668 File Offset: 0x00005668
	internal static _GUID GUID_VIDEO_CURRENT_MONITOR_BRIGHTNESS;

	// Token: 0x04000024 RID: 36 RVA: 0x00007C58 File Offset: 0x00005C58
	internal static _GUID GUID_GLOBAL_USER_PRESENCE;

	// Token: 0x04000025 RID: 37 RVA: 0x00007558 File Offset: 0x00005558
	internal static _GUID NO_SUBGROUP_GUID;

	// Token: 0x04000026 RID: 38 RVA: 0x00007308 File Offset: 0x00005308
	internal static _GUID GUID_DEVINTERFACE_TAPE;

	// Token: 0x04000027 RID: 39 RVA: 0x00007AE8 File Offset: 0x00005AE8
	internal static _GUID GUID_PROCESSOR_CORE_PARKING_MIN_CORES;

	// Token: 0x04000028 RID: 40 RVA: 0x00007918 File Offset: 0x00005918
	internal static _GUID GUID_BATTERY_DISCHARGE_LEVEL_3;

	// Token: 0x04000029 RID: 41 RVA: 0x000073B8 File Offset: 0x000053B8
	internal static _GUID GUID_DEVINTERFACE_SERENUM_BUS_ENUMERATOR;

	// Token: 0x0400002A RID: 42 RVA: 0x000078B8 File Offset: 0x000058B8
	internal static _GUID GUID_BATTERY_DISCHARGE_LEVEL_1;

	// Token: 0x0400002B RID: 43 RVA: 0x00007438 File Offset: 0x00005438
	internal static _GUID PPM_THERMALCONSTRAINT_GUID;

	// Token: 0x0400002C RID: 44 RVA: 0x00007398 File Offset: 0x00005398
	internal static _GUID GUID_DEVINTERFACE_HIDDEN_VOLUME;

	// Token: 0x0400002D RID: 45 RVA: 0x00007388 File Offset: 0x00005388
	internal static _GUID GUID_DEVINTERFACE_SES;

	// Token: 0x0400002E RID: 46 RVA: 0x000077D8 File Offset: 0x000057D8
	internal static _GUID IID_IPrintDialogCallback;

	// Token: 0x0400002F RID: 47 RVA: 0x00007638 File Offset: 0x00005638
	internal static _GUID GUID_MONITOR_POWER_ON;

	// Token: 0x04000030 RID: 48 RVA: 0x000074C8 File Offset: 0x000054C8
	internal static _GUID FIREWALL_PORT_CLOSE_GUID;

	// Token: 0x04000031 RID: 49 RVA: 0x00007A48 File Offset: 0x00005A48
	internal static _GUID GUID_PROCESSOR_IDLE_DISABLE;

	// Token: 0x04000032 RID: 50 RVA: 0x000078F8 File Offset: 0x000058F8
	internal static _GUID GUID_BATTERY_DISCHARGE_FLAGS_2;

	// Token: 0x04000033 RID: 51 RVA: 0x00007988 File Offset: 0x00005988
	internal static _GUID GUID_PROCESSOR_IDLESTATE_POLICY;

	// Token: 0x04000034 RID: 52 RVA: 0x00007AF8 File Offset: 0x00005AF8
	internal static _GUID GUID_PROCESSOR_CORE_PARKING_INCREASE_TIME;

	// Token: 0x04000035 RID: 53 RVA: 0x00007AD8 File Offset: 0x00005AD8
	internal static _GUID GUID_PROCESSOR_CORE_PARKING_MAX_CORES;

	// Token: 0x04000036 RID: 54 RVA: 0x00007818 File Offset: 0x00005818
	internal static _GUID GUID_POWERBUTTON_ACTION;

	// Token: 0x04000037 RID: 55 RVA: 0x00007C08 File Offset: 0x00005C08
	internal static _GUID GUID_LOCK_CONSOLE_ON_WAKE;

	// Token: 0x04000038 RID: 56 RVA: 0x000079C8 File Offset: 0x000059C8
	internal static _GUID GUID_PROCESSOR_PERF_INCREASE_POLICY;

	// Token: 0x04000039 RID: 57 RVA: 0x00007648 File Offset: 0x00005648
	internal static _GUID GUID_DEVICE_POWER_POLICY_VIDEO_BRIGHTNESS;

	// Token: 0x0400003A RID: 58 RVA: 0x000072E8 File Offset: 0x000052E8
	internal static _GUID GUID_DEVINTERFACE_CDROM;

	// Token: 0x0400003B RID: 59 RVA: 0x00007B18 File Offset: 0x00005B18
	internal static _GUID GUID_PROCESSOR_CORE_PARKING_AFFINITY_HISTORY_DECREASE_FACTOR;

	// Token: 0x0400003C RID: 60 RVA: 0x00007A98 File Offset: 0x00005A98
	internal static _GUID GUID_PROCESSOR_CORE_PARKING_INCREASE_THRESHOLD;

	// Token: 0x0400003D RID: 61 RVA: 0x00007B58 File Offset: 0x00005B58
	internal static _GUID GUID_PROCESSOR_CORE_PARKING_OVER_UTILIZATION_HISTORY_THRESHOLD;

	// Token: 0x0400003E RID: 62 RVA: 0x00007508 File Offset: 0x00005508
	internal static _GUID NAMED_PIPE_EVENT_GUID;

	// Token: 0x0400003F RID: 63 RVA: 0x000075C8 File Offset: 0x000055C8
	internal static _GUID GUID_EXECUTION_REQUIRED_REQUEST_TIMEOUT;

	// Token: 0x04000040 RID: 64 RVA: 0x000076D8 File Offset: 0x000056D8
	internal static _GUID GUID_DISK_SUBGROUP;

	// Token: 0x04000041 RID: 65 RVA: 0x00007588 File Offset: 0x00005588
	internal static _GUID GUID_ACTIVE_POWERSCHEME;

	// Token: 0x04000042 RID: 66 RVA: 0x000073E8 File Offset: 0x000053E8
	internal static _GUID PPM_IDLESTATE_CHANGE_GUID;

	// Token: 0x04000043 RID: 67 RVA: 0x00007898 File Offset: 0x00005898
	internal static _GUID GUID_BATTERY_DISCHARGE_FLAGS_0;

	// Token: 0x04000044 RID: 68 RVA: 0x00007B88 File Offset: 0x00005B88
	internal static _GUID GUID_PROCESSOR_PARKING_CORE_OVERRIDE;

	// Token: 0x04000045 RID: 69 RVA: 0x000076E8 File Offset: 0x000056E8
	internal static _GUID GUID_DISK_POWERDOWN_TIMEOUT;

	// Token: 0x04000046 RID: 70 RVA: 0x000076B8 File Offset: 0x000056B8
	internal static _GUID GUID_ADAPTIVE_POWER_BEHAVIOR_SUBGROUP;

	// Token: 0x04000047 RID: 71 RVA: 0x00007328 File Offset: 0x00005328
	internal static _GUID GUID_DEVINTERFACE_VOLUME;

	// Token: 0x04000048 RID: 72 RVA: 0x00007808 File Offset: 0x00005808
	internal static _GUID IID_IPrintDialogServices;

	// Token: 0x04000049 RID: 73 RVA: 0x00007A08 File Offset: 0x00005A08
	internal static _GUID GUID_PROCESSOR_PERF_TIME_CHECK;

	// Token: 0x0400004A RID: 74 RVA: 0x00007888 File Offset: 0x00005888
	internal static _GUID GUID_BATTERY_DISCHARGE_LEVEL_0;

	// Token: 0x0400004B RID: 75 RVA: 0x00007568 File Offset: 0x00005568
	internal static _GUID ALL_POWERSCHEMES_GUID;

	// Token: 0x0400004C RID: 76 RVA: 0x00007488 File Offset: 0x00005488
	internal static _GUID NETWORK_MANAGER_LAST_IP_ADDRESS_REMOVAL_GUID;

	// Token: 0x0400004D RID: 77 RVA: 0x000076A8 File Offset: 0x000056A8
	internal static _GUID GUID_VIDEO_CONSOLE_LOCK_TIMEOUT;

	// Token: 0x0400004E RID: 78 RVA: 0x00007C18 File Offset: 0x00005C18
	internal static _GUID GUID_DEVICE_IDLE_POLICY;

	// Token: 0x0400004F RID: 79 RVA: 0x00007B28 File Offset: 0x00005B28
	internal static _GUID GUID_PROCESSOR_CORE_PARKING_AFFINITY_HISTORY_THRESHOLD;

	// Token: 0x04000050 RID: 80 RVA: 0x00007728 File Offset: 0x00005728
	internal static _GUID GUID_SLEEP_SUBGROUP;

	// Token: 0x04000051 RID: 81 RVA: 0x000073A8 File Offset: 0x000053A8
	internal static _GUID GUID_DEVINTERFACE_COMPORT;

	// Token: 0x04000052 RID: 82 RVA: 0x00007CB8 File Offset: 0x00005CB8
	internal static _GUID GUID_PCIEXPRESS_SETTINGS_SUBGROUP;

	// Token: 0x04000053 RID: 83 RVA: 0x000074F8 File Offset: 0x000054F8
	internal static _GUID RPC_INTERFACE_EVENT_GUID;

	// Token: 0x04000054 RID: 84 RVA: 0x00007748 File Offset: 0x00005748
	internal static _GUID GUID_STANDBY_TIMEOUT;

	// Token: 0x04000055 RID: 85 RVA: 0x00007528 File Offset: 0x00005528
	internal static _GUID GUID_MAX_POWER_SAVINGS;

	// Token: 0x04000056 RID: 86 RVA: 0x00007938 File Offset: 0x00005938
	internal static _GUID GUID_PROCESSOR_SETTINGS_SUBGROUP;

	// Token: 0x04000057 RID: 87 RVA: 0x00007848 File Offset: 0x00005848
	internal static _GUID GUID_LIDCLOSE_ACTION;

	// Token: 0x04000058 RID: 88 RVA: 0x000075A8 File Offset: 0x000055A8
	internal static _GUID GUID_IDLE_RESILIENCY_PERIOD;

	// Token: 0x04000059 RID: 89 RVA: 0x00007878 File Offset: 0x00005878
	internal static _GUID GUID_BATTERY_DISCHARGE_ACTION_0;

	// Token: 0x0400005A RID: 90 RVA: 0x00007C38 File Offset: 0x00005C38
	internal static _GUID GUID_LIDSWITCH_STATE_CHANGE;

	// Token: 0x0400005B RID: 91 RVA: 0x00007538 File Offset: 0x00005538
	internal static _GUID GUID_MIN_POWER_SAVINGS;

	// Token: 0x0400005C RID: 92 RVA: 0x00007858 File Offset: 0x00005858
	internal static _GUID GUID_LIDOPEN_POWERSTATE;

	// Token: 0x0400005D RID: 93 RVA: 0x00007838 File Offset: 0x00005838
	internal static _GUID GUID_USERINTERFACEBUTTON_ACTION;

	// Token: 0x0400005E RID: 94 RVA: 0x000075D8 File Offset: 0x000055D8
	internal static _GUID GUID_VIDEO_SUBGROUP;

	// Token: 0x0400005F RID: 95 RVA: 0x000079A8 File Offset: 0x000059A8
	internal static _GUID GUID_PROCESSOR_PERF_INCREASE_THRESHOLD;

	// Token: 0x04000060 RID: 96 RVA: 0x00007A38 File Offset: 0x00005A38
	internal static _GUID GUID_PROCESSOR_IDLE_ALLOW_SCALING;

	// Token: 0x04000061 RID: 97 RVA: 0x00007B48 File Offset: 0x00005B48
	internal static _GUID GUID_PROCESSOR_CORE_PARKING_OVER_UTILIZATION_HISTORY_DECREASE_FACTOR;

	// Token: 0x04000062 RID: 98 RVA: 0x00007358 File Offset: 0x00005358
	internal static _GUID GUID_DEVINTERFACE_CDCHANGER;

	// Token: 0x04000063 RID: 99 RVA: 0x000079B8 File Offset: 0x000059B8
	internal static _GUID GUID_PROCESSOR_PERF_DECREASE_THRESHOLD;

	// Token: 0x04000064 RID: 100 RVA: 0x00007968 File Offset: 0x00005968
	internal static _GUID GUID_PROCESSOR_THROTTLE_MINIMUM;

	// Token: 0x04000065 RID: 101 RVA: 0x00007BD8 File Offset: 0x00005BD8
	internal static _GUID GUID_PROCESSOR_PERF_LATENCY_HINT;

	// Token: 0x04000066 RID: 102 RVA: 0x00007C48 File Offset: 0x00005C48
	internal static _GUID GUID_BATTERY_PERCENTAGE_REMAINING;

	// Token: 0x04000067 RID: 103 RVA: 0x00007A88 File Offset: 0x00005A88
	internal static _GUID GUID_PROCESSOR_IDLE_PROMOTE_THRESHOLD;

	// Token: 0x04000068 RID: 104 RVA: 0x00007378 File Offset: 0x00005378
	internal static _GUID GUID_DEVINTERFACE_VMLUN;

	// Token: 0x04000069 RID: 105 RVA: 0x00007BA8 File Offset: 0x00005BA8
	internal static _GUID GUID_PROCESSOR_PARKING_CONCURRENCY_THRESHOLD;

	// Token: 0x0400006A RID: 106 RVA: 0x000073F8 File Offset: 0x000053F8
	internal static _GUID PPM_PERFSTATES_DATA_GUID;

	// Token: 0x0400006B RID: 107 RVA: 0x00007778 File Offset: 0x00005778
	internal static _GUID GUID_HIBERNATE_FASTS4_POLICY;

	// Token: 0x0400006C RID: 108 RVA: 0x00019540 File Offset: 0x00017540
	internal static int ReplicaSeeder;

	// Token: 0x0400006D RID: 109 RVA: 0x00007758 File Offset: 0x00005758
	internal static _GUID GUID_UNATTEND_SLEEP_TIMEOUT;

	// Token: 0x0400006E RID: 110 RVA: 0x00007658 File Offset: 0x00005658
	internal static _GUID GUID_DEVICE_POWER_POLICY_VIDEO_DIM_BRIGHTNESS;

	// Token: 0x0400006F RID: 111 RVA: 0x00007338 File Offset: 0x00005338
	internal static _GUID GUID_DEVINTERFACE_MEDIUMCHANGER;

	// Token: 0x04000070 RID: 112 RVA: 0x00007C68 File Offset: 0x00005C68
	internal static _GUID GUID_SESSION_DISPLAY_STATUS;

	// Token: 0x04000071 RID: 113 RVA: 0x000077C8 File Offset: 0x000057C8
	internal static _GUID GUID_ALLOW_RTC_WAKE;

	// Token: 0x04000072 RID: 114 RVA: 0x00007BC8 File Offset: 0x00005BC8
	internal static _GUID GUID_PROCESSOR_PERF_HISTORY;

	// Token: 0x04000073 RID: 115 RVA: 0x00007868 File Offset: 0x00005868
	internal static _GUID GUID_BATTERY_SUBGROUP;

	// Token: 0x04000074 RID: 116 RVA: 0x00007348 File Offset: 0x00005348
	internal static _GUID GUID_DEVINTERFACE_FLOPPY;

	// Token: 0x04000075 RID: 117 RVA: 0x000074A8 File Offset: 0x000054A8
	internal static _GUID DOMAIN_LEAVE_GUID;

	// Token: 0x04000076 RID: 118 RVA: 0x00007548 File Offset: 0x00005548
	internal static _GUID GUID_TYPICAL_POWER_SAVINGS;

	// Token: 0x04000077 RID: 119 RVA: 0x00007958 File Offset: 0x00005958
	internal static _GUID GUID_PROCESSOR_THROTTLE_MAXIMUM;

	// Token: 0x04000078 RID: 120 RVA: 0x00007998 File Offset: 0x00005998
	internal static _GUID GUID_PROCESSOR_PERFSTATE_POLICY;

	// Token: 0x04000079 RID: 121 RVA: 0x000073C8 File Offset: 0x000053C8
	internal static _GUID PPM_PERFSTATE_CHANGE_GUID;

	// Token: 0x0400007A RID: 122 RVA: 0x00007A28 File Offset: 0x00005A28
	internal static _GUID GUID_PROCESSOR_PERF_BOOST_MODE;

	// Token: 0x0400007B RID: 123 RVA: 0x00007AC8 File Offset: 0x00005AC8
	internal static _GUID GUID_PROCESSOR_CORE_PARKING_DECREASE_POLICY;

	// Token: 0x0400007C RID: 124 RVA: 0x00007B78 File Offset: 0x00005B78
	internal static _GUID GUID_PROCESSOR_CORE_PARKING_OVER_UTILIZATION_THRESHOLD;

	// Token: 0x0400007D RID: 125 RVA: 0x00007768 File Offset: 0x00005768
	internal static _GUID GUID_HIBERNATE_TIMEOUT;

	// Token: 0x0400007E RID: 126 RVA: 0x00007458 File Offset: 0x00005458
	internal static _GUID GUID_DEVINTERFACE_SMARTCARD_READER;

	// Token: 0x0400007F RID: 127 RVA: 0x00007BE8 File Offset: 0x00005BE8
	internal static _GUID GUID_PROCESSOR_DISTRIBUTE_UTILITY;

	// Token: 0x04000080 RID: 128 RVA: 0x000076C8 File Offset: 0x000056C8
	internal static _GUID GUID_NON_ADAPTIVE_INPUT_TIMEOUT;

	// Token: 0x04000081 RID: 129 RVA: 0x00007798 File Offset: 0x00005798
	internal static _GUID GUID_SYSTEM_AWAYMODE;

	// Token: 0x04000082 RID: 130 RVA: 0x00007708 File Offset: 0x00005708
	internal static _GUID GUID_DISK_BURST_IGNORE_THRESHOLD;

	// Token: 0x04000083 RID: 131 RVA: 0x00007CC8 File Offset: 0x00005CC8
	internal static _GUID GUID_PCIEXPRESS_ASPM_POLICY;

	// Token: 0x04000084 RID: 132 RVA: 0x00007928 File Offset: 0x00005928
	internal static _GUID GUID_BATTERY_DISCHARGE_FLAGS_3;

	// Token: 0x04000085 RID: 133 RVA: 0x00007A78 File Offset: 0x00005A78
	internal static _GUID GUID_PROCESSOR_IDLE_DEMOTE_THRESHOLD;

	// Token: 0x04000086 RID: 134 RVA: 0x00007448 File Offset: 0x00005448
	internal static _GUID PPM_PERFMON_PERFSTATE_GUID;

	// Token: 0x04000087 RID: 135 RVA: 0x00007CD8 File Offset: 0x00005CD8
	internal static _GUID GUID_ENABLE_SWITCH_FORCED_SHUTDOWN;

	// Token: 0x04000088 RID: 136 RVA: 0x000079D8 File Offset: 0x000059D8
	internal static _GUID GUID_PROCESSOR_PERF_DECREASE_POLICY;

	// Token: 0x04000089 RID: 137 RVA: 0x00007418 File Offset: 0x00005418
	internal static _GUID PPM_IDLE_ACCOUNTING_GUID;

	// Token: 0x0400008A RID: 138 RVA: 0x00007908 File Offset: 0x00005908
	internal static _GUID GUID_BATTERY_DISCHARGE_ACTION_3;

	// Token: 0x0400008B RID: 139 RVA: 0x00007B08 File Offset: 0x00005B08
	internal static _GUID GUID_PROCESSOR_CORE_PARKING_DECREASE_TIME;

	// Token: 0x0400008C RID: 140 RVA: 0x00007C28 File Offset: 0x00005C28
	internal static _GUID GUID_ACDC_POWER_SOURCE;

	// Token: 0x0400008D RID: 141 RVA: 0x00007738 File Offset: 0x00005738
	internal static _GUID GUID_SLEEP_IDLE_THRESHOLD;

	// Token: 0x0400008E RID: 142 RVA: 0x00007578 File Offset: 0x00005578
	internal static _GUID GUID_POWERSCHEME_PERSONALITY;

	// Token: 0x0400008F RID: 143 RVA: 0x000078C8 File Offset: 0x000058C8
	internal static _GUID GUID_BATTERY_DISCHARGE_FLAGS_1;

	// Token: 0x04000090 RID: 144 RVA: 0x00007B98 File Offset: 0x00005B98
	internal static _GUID GUID_PROCESSOR_PARKING_PERF_STATE;

	// Token: 0x04000091 RID: 145 RVA: 0x00007428 File Offset: 0x00005428
	internal static _GUID PPM_IDLE_ACCOUNTING_EX_GUID;

	// Token: 0x04000092 RID: 146 RVA: 0x000077F8 File Offset: 0x000057F8
	internal static _GUID GUID_SYSTEM_BUTTON_SUBGROUP;

	// Token: 0x04000093 RID: 147 RVA: 0x00007CA8 File Offset: 0x00005CA8
	internal static _GUID GUID_APPLAUNCH_BUTTON;

	// Token: 0x04000094 RID: 148 RVA: 0x00007978 File Offset: 0x00005978
	internal static _GUID GUID_PROCESSOR_ALLOW_THROTTLING;

	// Token: 0x04000095 RID: 149 RVA: 0x000074D8 File Offset: 0x000054D8
	internal static _GUID MACHINE_POLICY_PRESENT_GUID;

	// Token: 0x04000096 RID: 150 RVA: 0x000076F8 File Offset: 0x000056F8
	internal static _GUID GUID_DISK_IDLE_TIMEOUT;

	// Token: 0x04000097 RID: 151 RVA: 0x00007C78 File Offset: 0x00005C78
	internal static _GUID GUID_SESSION_USER_PRESENCE;

	// Token: 0x04000098 RID: 152 RVA: 0x00007C98 File Offset: 0x00005C98
	internal static _GUID GUID_BACKGROUND_TASK_NOTIFICATION;

	// Token: 0x04000099 RID: 153 RVA: 0x00007468 File Offset: 0x00005468
	internal static _GUID PPM_THERMAL_POLICY_CHANGE_GUID;

	// Token: 0x0400009A RID: 154 RVA: 0x00007518 File Offset: 0x00005518
	internal static _GUID CUSTOM_SYSTEM_STATE_CHANGE_EVENT_GUID;

	// Token: 0x0400009B RID: 155 RVA: 0x000077B8 File Offset: 0x000057B8
	internal static _GUID GUID_ALLOW_STANDBY_STATES;

	// Token: 0x0400009C RID: 156 RVA: 0x000072D8 File Offset: 0x000052D8
	internal static _GUID GUID_DEVINTERFACE_DISK;

	// Token: 0x0400009D RID: 157 RVA: 0x000078A8 File Offset: 0x000058A8
	internal static _GUID GUID_BATTERY_DISCHARGE_ACTION_1;

	// Token: 0x0400009E RID: 158 RVA: 0x00007948 File Offset: 0x00005948
	internal static _GUID GUID_PROCESSOR_THROTTLE_POLICY;

	// Token: 0x0400009F RID: 159 RVA: 0x000078E8 File Offset: 0x000058E8
	internal static _GUID GUID_BATTERY_DISCHARGE_LEVEL_2;

	// Token: 0x040000A0 RID: 160 RVA: 0x00007828 File Offset: 0x00005828
	internal static _GUID GUID_SLEEPBUTTON_ACTION;

	// Token: 0x040000A1 RID: 161 RVA: 0x000075E8 File Offset: 0x000055E8
	internal static _GUID GUID_VIDEO_POWERDOWN_TIMEOUT;

	// Token: 0x040000A2 RID: 162 RVA: 0x00007628 File Offset: 0x00005628
	internal static _GUID GUID_VIDEO_ADAPTIVE_POWERDOWN;

	// Token: 0x040000A3 RID: 163 RVA: 0x00007D30 File Offset: 0x00005D30
	internal static $ArrayType$$$BY0P@$$CBG ??_C@_1BO@LKIGKJOJ@?$AAC?$AAl?$AAu?$AAs?$AAt?$AAe?$AAr?$AA?4?$AAR?$AAe?$AAp?$AAl?$AAa?$AAy?$AA?$AA@;

	// Token: 0x040000A4 RID: 164 RVA: 0x00007D50 File Offset: 0x00005D50
	internal static $ArrayType$$$BY0CA@$$CBG ??_C@_1EA@IDJOIADD@?$AAH?$AAr?$AAE?$AAS?$AAE?$AAB?$AAa?$AAc?$AAk?$AAu?$AAp?$AAS?$AAe?$AAt?$AAu?$AAp?$AA?3?$AA?5?$AAn?$AAo?$AA?5?$AAe?$AAd?$AAb?$AA?5?$AAo?$AAn?$AAl?$AAi?$AAn?$AAe?$AA?$AA@;

	// Token: 0x040000A5 RID: 165 RVA: 0x00007D90 File Offset: 0x00005D90
	internal static $ArrayType$$$BY0BF@$$CBG ??_C@_1CK@OOGKDIDK@?$AA?$CF?$AAs?$AA?$DL?$AA?5?$AAE?$AAr?$AAr?$AAo?$AAr?$AA?5?$AAC?$AAo?$AAd?$AAe?$AA?3?$AA?5?$AA?$CF?$AA?$CD?$AAx?$AA?4?$AA?$AA@;

	// Token: 0x040000A6 RID: 166 RVA: 0x00007DC0 File Offset: 0x00005DC0
	internal static $ArrayType$$$BY0BB@$$CBD ??_C@_0BB@DEJIFCPK@HrESEBackupSetup?$AA@;

	// Token: 0x040000A7 RID: 167 RVA: 0x00007DE0 File Offset: 0x00005DE0
	internal static $ArrayType$$$BY0EG@$$CBD ??_C@_0EG@JGPBOCNG@f?3?215?400?41497?2sources?2dev?2cluste@;

	// Token: 0x040000A8 RID: 168 RVA: 0x00007E28 File Offset: 0x00005E28
	internal static $ArrayType$$$BY0BP@$$CBG ??_C@_1DO@BEMMPGGP@?$AA?$CF?$AAh?$AAs?$AA?5?$AA?$EA?$AA?5?$AA?$CF?$AAd?$AA?3?$AA?5?$AA?$CF?$AAh?$AAs?$AA?5?$AAf?$AAa?$AAi?$AAl?$AAe?$AAd?$AA?5?$AAw?$AAi?$AAt?$AAh?$AA?5?$AA?$CF?$AA?$CD?$AAx?$AA?4?$AA?$AA@;

	// Token: 0x040000A9 RID: 169 RVA: 0x00007E70 File Offset: 0x00005E70
	internal static $ArrayType$$$BY0CG@$$CBG ??_C@_1EM@MIFDHKEH@?$AA?$CF?$AAh?$AAs?$AA?5?$AA?$EA?$AA?5?$AA?$CF?$AAd?$AA?3?$AA?5?$AA?$CF?$AAh?$AAs?$AA?5?$AAc?$AAo?$AAm?$AAp?$AAl?$AAe?$AAt?$AAe?$AAd?$AA?5?$AAs?$AAu?$AAc?$AAc?$AAe?$AAs?$AAs?$AAf@;

	// Token: 0x040000AA RID: 170 RVA: 0x00007EC0 File Offset: 0x00005EC0
	internal static $ArrayType$$$BY0BE@$$CBD ??_C@_0BE@BFBFPMIP@HrESEBackupReadFile?$AA@;

	// Token: 0x040000AB RID: 171 RVA: 0x00007EE0 File Offset: 0x00005EE0
	internal static $ArrayType$$$BY0EA@$$CBG ??_C@_1IA@IGHGLPMJ@?$AAE?$AAr?$AAr?$AAo?$AAr?$AA?3?$AA?5?$AAB?$AAR?$AA_?$AAL?$AAo?$AAg?$AAE?$AAr?$AAr?$AAo?$AAr?$AA?5?$AAf?$AAa?$AAi?$AAl?$AAe?$AAd?$AA?5?$AAb?$AAe?$AAc?$AAa?$AAu?$AAs@;

	// Token: 0x040000AC RID: 172 RVA: 0x00007F60 File Offset: 0x00005F60
	internal static $ArrayType$$$BY0BD@$$CBG ??_C@_1CG@CFLDDKCE@?$AA?5?$AAe?$AAr?$AAr?$AAo?$AAr?$AA?5?$AAc?$AAo?$AAd?$AAe?$AA?5?$AA?$DN?$AA?5?$AA?$CF?$AA?$CD?$AAx?$AA?4?$AA?$AA@;

	// Token: 0x040000AD RID: 173 RVA: 0x00007F90 File Offset: 0x00005F90
	internal static $ArrayType$$$BY0EF@$$CBG ??_C@_1IK@NCAECPEB@?$AAE?$AAr?$AAr?$AAo?$AAr?$AA?3?$AA?5?$AAB?$AAR?$AA_?$AAL?$AAo?$AAg?$AAE?$AAr?$AAr?$AAo?$AAr?$AA?5?$AAf?$AAa?$AAi?$AAl?$AAe?$AAd?$AA?5?$AAt?$AAo?$AA?5?$AAa?$AAp?$AAp@;

	// Token: 0x040000AE RID: 174 RVA: 0x00008020 File Offset: 0x00006020
	internal static $ArrayType$$$BY0FA@$$CBG ??_C@_1KA@DOFAHBGC@?$AAE?$AAr?$AAr?$AAo?$AAr?$AA?3?$AA?5?$AAB?$AAR?$AA_?$AAL?$AAo?$AAg?$AAE?$AAr?$AAr?$AAo?$AAr?$AA?5?$AAw?$AAa?$AAs?$AA?5?$AAo?$AAu?$AAt?$AA?5?$AAo?$AAf?$AA?5?$AAb?$AAu@;

	// Token: 0x040000AF RID: 175 RVA: 0x00007ED4 File Offset: 0x00005ED4
	internal static $ArrayType$$$BY02$$CBG ??_C@_15GANGMFKL@?$AA?$CF?$AAs?$AA?$AA@;

	// Token: 0x040000B0 RID: 176 RVA: 0x000080C0 File Offset: 0x000060C0
	internal static $ArrayType$$$BY0EF@$$CBG ??_C@_1IK@MLLIKKLI@?$AAE?$AAr?$AAr?$AAo?$AAr?$AA?3?$AA?5?$AAB?$AAR?$AA_?$AAL?$AAo?$AAg?$AAE?$AAr?$AAr?$AAo?$AAr?$AA?5?$AAf?$AAa?$AAi?$AAl?$AAe?$AAd?$AA?5?$AAt?$AAo?$AA?5?$AAf?$AAo?$AAr@;

	// Token: 0x040000B1 RID: 177 RVA: 0x00008150 File Offset: 0x00006150
	internal static $ArrayType$$$BY0CH@$$CBG ??_C@_1EO@NIBKGOJF@?$AAR?$AAe?$AAc?$AAe?$AAi?$AAv?$AAe?$AAd?$AA?5?$AAe?$AAr?$AAr?$AAo?$AAr?$AA?3?$AA?5?$AA?$CF?$AA?$CD?$AAx?$AA?0?$AA?5?$AAe?$AAr?$AAr?$AAo?$AAr?$AA?5?$AAc?$AAo?$AAd?$AAe?$AA?3@;

	// Token: 0x040000B2 RID: 178 RVA: 0x000081A0 File Offset: 0x000061A0
	internal static $ArrayType$$$BY0CG@$$CBG ??_C@_1EM@IBOADBE@?$AAR?$AAe?$AAc?$AAe?$AAi?$AAv?$AAe?$AAd?$AA?5?$AAe?$AAr?$AAr?$AAo?$AAr?$AA?3?$AA?5?$AA?$CF?$AA?$CD?$AAx?$AA?0?$AA?5?$AAe?$AAr?$AAr?$AAo?$AAr?$AA?5?$AAc?$AAo?$AAd?$AAe?$AA?3@;

	// Token: 0x040000B3 RID: 179 RVA: 0x00019544 File Offset: 0x00017544
	internal static int g_fTraceInit;

	// Token: 0x040000B4 RID: 180 RVA: 0x00019010 File Offset: 0x00017010
	internal unsafe static ushort* g_pszComponentName;

	// Token: 0x040000B5 RID: 181 RVA: 0x00019000 File Offset: 0x00017000
	internal static _GUID g_ReplayServiceTraceGuid;

	// Token: 0x040000B6 RID: 182 RVA: 0x00019028 File Offset: 0x00017028
	public static method __m2mep@?GetUnmanagedUni@?A0x7763efd2@@$$FYMPEAGPE$AAVString@System@@@Z;

	// Token: 0x040000B7 RID: 183 RVA: 0x00019038 File Offset: 0x00017038
	public static method __m2mep@?FreeUnmanagedUni@?A0x7763efd2@@$$FYAXPEAG@Z;

	// Token: 0x040000B8 RID: 184 RVA: 0x00019048 File Offset: 0x00017048
	public static method __m2mep@?MakeManagedBytes@?A0x7763efd2@@$$FYMP$01EAEPEAEH@Z;

	// Token: 0x040000B9 RID: 185 RVA: 0x00019018 File Offset: 0x00017018
	public static method __m2mep@?GetEC@ReplicaSeeder@Cluster@Exchange@Microsoft@@$$FYAJJ@Z;

	// Token: 0x040000BA RID: 186 RVA: 0x000081F0 File Offset: 0x000061F0
	internal static $ArrayType$$$BY0BM@$$CBG g_wszEndpointAnnotation;

	// Token: 0x040000BB RID: 187 RVA: 0x00019058 File Offset: 0x00017058
	public static method __m2mep@?GetUnmanagedUni@?A0x885d7a58@@$$FYMPEAGPE$AAVString@System@@@Z;

	// Token: 0x040000BC RID: 188 RVA: 0x00019068 File Offset: 0x00017068
	public static method __m2mep@?FreeUnmanagedUni@?A0x885d7a58@@$$FYAXPEAG@Z;

	// Token: 0x040000BD RID: 189 RVA: 0x00008260 File Offset: 0x00006260
	internal static __s_GUID _GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x040000BE RID: 190 RVA: 0x00008290 File Offset: 0x00006290
	internal static __s_GUID _GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x040000BF RID: 191 RVA: 0x00007280 File Offset: 0x00005280
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_z;

	// Token: 0x040000C0 RID: 192 RVA: 0x00007288 File Offset: 0x00005288
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_a;

	// Token: 0x040000C1 RID: 193
	[FixedAddressValueType]
	internal static Progress.State ?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A;

	// Token: 0x040000C2 RID: 194 RVA: 0x00007250 File Offset: 0x00005250
	internal static method ?InitializedVtables$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000C3 RID: 195
	[FixedAddressValueType]
	internal static bool ?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA;

	// Token: 0x040000C4 RID: 196 RVA: 0x00007248 File Offset: 0x00005248
	internal static method ?IsDefaultDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000C5 RID: 197 RVA: 0x00007230 File Offset: 0x00005230
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_a;

	// Token: 0x040000C6 RID: 198
	[FixedAddressValueType]
	internal static Progress.State ?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A;

	// Token: 0x040000C7 RID: 199 RVA: 0x00007268 File Offset: 0x00005268
	internal static method ?InitializedPerAppDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000C8 RID: 200 RVA: 0x00007270 File Offset: 0x00005270
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_z;

	// Token: 0x040000C9 RID: 201
	[FixedAddressValueType]
	internal static Progress.State ?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A;

	// Token: 0x040000CA RID: 202 RVA: 0x00007258 File Offset: 0x00005258
	internal static method ?InitializedNative$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000CB RID: 203 RVA: 0x00007290 File Offset: 0x00005290
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_z;

	// Token: 0x040000CC RID: 204 RVA: 0x00008280 File Offset: 0x00006280
	internal static __s_GUID _GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x040000CD RID: 205
	[FixedAddressValueType]
	internal static int ?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x040000CE RID: 206 RVA: 0x00007240 File Offset: 0x00005240
	internal static method ?Uninitialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000CF RID: 207
	[FixedAddressValueType]
	internal static int ?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x040000D0 RID: 208 RVA: 0x00007238 File Offset: 0x00005238
	internal static method ?Initialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000D1 RID: 209 RVA: 0x00019ADF File Offset: 0x00017ADF
	internal static bool ?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x040000D2 RID: 210
	[FixedAddressValueType]
	internal static Progress.State ?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A;

	// Token: 0x040000D3 RID: 211 RVA: 0x00019ADC File Offset: 0x00017ADC
	internal static bool ?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x040000D4 RID: 212 RVA: 0x00019ADD File Offset: 0x00017ADD
	internal static bool ?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x040000D5 RID: 213 RVA: 0x00019AD8 File Offset: 0x00017AD8
	internal static int ?Count@AllDomains@<CrtImplementationDetails>@@2HA;

	// Token: 0x040000D6 RID: 214 RVA: 0x00008234 File Offset: 0x00006234
	internal static uint ?ProcessAttach@NativeDll@<CrtImplementationDetails>@@0IB;

	// Token: 0x040000D7 RID: 215 RVA: 0x00008238 File Offset: 0x00006238
	internal static uint ?ThreadAttach@NativeDll@<CrtImplementationDetails>@@0IB;

	// Token: 0x040000D8 RID: 216 RVA: 0x00019098 File Offset: 0x00017098
	internal static TriBool.State ?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A;

	// Token: 0x040000D9 RID: 217 RVA: 0x00008230 File Offset: 0x00006230
	internal static uint ?ProcessDetach@NativeDll@<CrtImplementationDetails>@@0IB;

	// Token: 0x040000DA RID: 218 RVA: 0x0000823C File Offset: 0x0000623C
	internal static uint ?ThreadDetach@NativeDll@<CrtImplementationDetails>@@0IB;

	// Token: 0x040000DB RID: 219 RVA: 0x00008240 File Offset: 0x00006240
	internal static uint ?ProcessVerifier@NativeDll@<CrtImplementationDetails>@@0IB;

	// Token: 0x040000DC RID: 220 RVA: 0x00019094 File Offset: 0x00017094
	internal static TriBool.State ?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A;

	// Token: 0x040000DD RID: 221 RVA: 0x00019ADE File Offset: 0x00017ADE
	internal static bool ?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x040000DE RID: 222 RVA: 0x00007278 File Offset: 0x00005278
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_a;

	// Token: 0x040000DF RID: 223 RVA: 0x00008270 File Offset: 0x00006270
	internal static __s_GUID _GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x040000E0 RID: 224 RVA: 0x00007260 File Offset: 0x00005260
	internal static method ?InitializedPerProcess$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x040000E1 RID: 225 RVA: 0x000190B0 File Offset: 0x000170B0
	public static method __m2mep@?IsInDllMain@NativeDll@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x040000E2 RID: 226 RVA: 0x000190C0 File Offset: 0x000170C0
	public static method __m2mep@?IsInProcessAttach@NativeDll@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x040000E3 RID: 227 RVA: 0x000190D0 File Offset: 0x000170D0
	public static method __m2mep@?IsInProcessDetach@NativeDll@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x040000E4 RID: 228 RVA: 0x000190E0 File Offset: 0x000170E0
	public static method __m2mep@?IsSafeForManagedCode@NativeDll@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x040000E5 RID: 229 RVA: 0x00019250 File Offset: 0x00017250
	public static method __m2mep@?ThrowNestedModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVException@System@@0@Z;

	// Token: 0x040000E6 RID: 230 RVA: 0x000190F0 File Offset: 0x000170F0
	public static method __m2mep@?ThrowModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVString@System@@@Z;

	// Token: 0x040000E7 RID: 231 RVA: 0x00019100 File Offset: 0x00017100
	public static method __m2mep@?ThrowModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVString@System@@PE$AAVException@3@@Z;

	// Token: 0x040000E8 RID: 232 RVA: 0x00019110 File Offset: 0x00017110
	public static method __m2mep@?RegisterModuleUninitializer@<CrtImplementationDetails>@@$$FYMXPE$AAVEventHandler@System@@@Z;

	// Token: 0x040000E9 RID: 233 RVA: 0x00019120 File Offset: 0x00017120
	public static method __m2mep@?FromGUID@<CrtImplementationDetails>@@$$FYM?AVGuid@System@@AEBU_GUID@@@Z;

	// Token: 0x040000EA RID: 234 RVA: 0x00019130 File Offset: 0x00017130
	public static method __m2mep@?__get_default_appdomain@@$$FYAJPEAPEAUIUnknown@@@Z;

	// Token: 0x040000EB RID: 235 RVA: 0x00019140 File Offset: 0x00017140
	public static method __m2mep@?__release_appdomain@@$$FYAXPEAUIUnknown@@@Z;

	// Token: 0x040000EC RID: 236 RVA: 0x00019150 File Offset: 0x00017150
	public static method __m2mep@?GetDefaultDomain@<CrtImplementationDetails>@@$$FYMPE$AAVAppDomain@System@@XZ;

	// Token: 0x040000ED RID: 237 RVA: 0x00019160 File Offset: 0x00017160
	public static method __m2mep@?DoCallBackInDefaultDomain@<CrtImplementationDetails>@@$$FYAXP6AJPEAX@Z0@Z;

	// Token: 0x040000EE RID: 238 RVA: 0x00019170 File Offset: 0x00017170
	public static method __m2mep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x040000EF RID: 239 RVA: 0x00019180 File Offset: 0x00017180
	public static method __m2mep@?HasPerProcess@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x040000F0 RID: 240 RVA: 0x00019190 File Offset: 0x00017190
	public static method __m2mep@?HasNative@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x040000F1 RID: 241 RVA: 0x000191A0 File Offset: 0x000171A0
	public static method __m2mep@?NeedsInitialization@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x040000F2 RID: 242 RVA: 0x000191B0 File Offset: 0x000171B0
	public static method __m2mep@?NeedsUninitialization@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x040000F3 RID: 243 RVA: 0x000191C0 File Offset: 0x000171C0
	public static method __m2mep@?Initialize@DefaultDomain@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x040000F4 RID: 244 RVA: 0x00019260 File Offset: 0x00017260
	public static method __m2mep@?InitializeVtables@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x040000F5 RID: 245 RVA: 0x00019270 File Offset: 0x00017270
	public static method __m2mep@?InitializeDefaultAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x040000F6 RID: 246 RVA: 0x00019280 File Offset: 0x00017280
	public static method __m2mep@?InitializeNative@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x040000F7 RID: 247 RVA: 0x00019290 File Offset: 0x00017290
	public static method __m2mep@?InitializePerProcess@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x040000F8 RID: 248 RVA: 0x000192A0 File Offset: 0x000172A0
	public static method __m2mep@?InitializePerAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x040000F9 RID: 249 RVA: 0x000192B0 File Offset: 0x000172B0
	public static method __m2mep@?InitializeUninitializer@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x040000FA RID: 250 RVA: 0x000192C0 File Offset: 0x000172C0
	public static method __m2mep@?_Initialize@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x040000FB RID: 251 RVA: 0x000191D0 File Offset: 0x000171D0
	public static method __m2mep@?UninitializeAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x040000FC RID: 252 RVA: 0x000191E0 File Offset: 0x000171E0
	public static method __m2mep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x040000FD RID: 253 RVA: 0x000191F0 File Offset: 0x000171F0
	public static method __m2mep@?UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x040000FE RID: 254 RVA: 0x00019200 File Offset: 0x00017200
	public static method __m2mep@?DomainUnload@LanguageSupport@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@PE$AAVEventArgs@4@@Z;

	// Token: 0x040000FF RID: 255 RVA: 0x000192D0 File Offset: 0x000172D0
	public static method __m2mep@?Cleanup@LanguageSupport@<CrtImplementationDetails>@@$$FAEAMXPE$AAVException@System@@@Z;

	// Token: 0x04000100 RID: 256 RVA: 0x000192E0 File Offset: 0x000172E0
	public static method __m2mep@??0LanguageSupport@<CrtImplementationDetails>@@$$FQEAA@XZ;

	// Token: 0x04000101 RID: 257 RVA: 0x000192F0 File Offset: 0x000172F0
	public static method __m2mep@??1LanguageSupport@<CrtImplementationDetails>@@$$FQEAA@XZ;

	// Token: 0x04000102 RID: 258 RVA: 0x00019300 File Offset: 0x00017300
	public static method __m2mep@?Initialize@LanguageSupport@<CrtImplementationDetails>@@$$FQEAAXXZ;

	// Token: 0x04000103 RID: 259 RVA: 0x000190A0 File Offset: 0x000170A0
	public static method cctor@@$$FYMXXZ;

	// Token: 0x04000104 RID: 260 RVA: 0x00019210 File Offset: 0x00017210
	public static method __m2mep@??0?$gcroot@PE$AAVString@System@@@@$$FQEAA@XZ;

	// Token: 0x04000105 RID: 261 RVA: 0x00019220 File Offset: 0x00017220
	public static method __m2mep@??1?$gcroot@PE$AAVString@System@@@@$$FQEAA@XZ;

	// Token: 0x04000106 RID: 262 RVA: 0x00019230 File Offset: 0x00017230
	public static method __m2mep@??4?$gcroot@PE$AAVString@System@@@@$$FQEAMAEAU0@PE$AAVString@System@@@Z;

	// Token: 0x04000107 RID: 263 RVA: 0x00019240 File Offset: 0x00017240
	public static method __m2mep@??B?$gcroot@PE$AAVString@System@@@@$$FQEBMPE$AAVString@System@@XZ;

	// Token: 0x04000108 RID: 264 RVA: 0x000082A0 File Offset: 0x000062A0
	public unsafe static int** __unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000109 RID: 265 RVA: 0x000082A8 File Offset: 0x000062A8
	public unsafe static int** __unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400010A RID: 266
	[FixedAddressValueType]
	internal static ulong __exit_list_size_app_domain;

	// Token: 0x0400010B RID: 267
	[FixedAddressValueType]
	internal unsafe static method* __onexitbegin_app_domain;

	// Token: 0x0400010C RID: 268 RVA: 0x00019C28 File Offset: 0x00017C28
	internal static ulong __exit_list_size;

	// Token: 0x0400010D RID: 269
	[FixedAddressValueType]
	internal unsafe static method* __onexitend_app_domain;

	// Token: 0x0400010E RID: 270 RVA: 0x00019C18 File Offset: 0x00017C18
	internal unsafe static method* __onexitbegin_m;

	// Token: 0x0400010F RID: 271 RVA: 0x00019C20 File Offset: 0x00017C20
	internal unsafe static method* __onexitend_m;

	// Token: 0x04000110 RID: 272
	[FixedAddressValueType]
	internal static int ?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA;

	// Token: 0x04000111 RID: 273
	[FixedAddressValueType]
	internal unsafe static void* ?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA;

	// Token: 0x04000112 RID: 274 RVA: 0x000193D8 File Offset: 0x000173D8
	public static method __m2mep@?_handle@AtExitLock@<CrtImplementationDetails>@@$$FCMPE$AAVGCHandle@InteropServices@Runtime@System@@XZ;

	// Token: 0x04000113 RID: 275 RVA: 0x00019488 File Offset: 0x00017488
	public static method __m2mep@?_lock_Construct@AtExitLock@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@@Z;

	// Token: 0x04000114 RID: 276 RVA: 0x000193E8 File Offset: 0x000173E8
	public static method __m2mep@?_lock_Set@AtExitLock@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@@Z;

	// Token: 0x04000115 RID: 277 RVA: 0x000193F8 File Offset: 0x000173F8
	public static method __m2mep@?_lock_Get@AtExitLock@<CrtImplementationDetails>@@$$FCMPE$AAVObject@System@@XZ;

	// Token: 0x04000116 RID: 278 RVA: 0x00019408 File Offset: 0x00017408
	public static method __m2mep@?_lock_Destruct@AtExitLock@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x04000117 RID: 279 RVA: 0x00019418 File Offset: 0x00017418
	public static method __m2mep@?IsInitialized@AtExitLock@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000118 RID: 280 RVA: 0x00019498 File Offset: 0x00017498
	public static method __m2mep@?AddRef@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x04000119 RID: 281 RVA: 0x00019428 File Offset: 0x00017428
	public static method __m2mep@?RemoveRef@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x0400011A RID: 282 RVA: 0x00019438 File Offset: 0x00017438
	public static method __m2mep@?Enter@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x0400011B RID: 283 RVA: 0x00019448 File Offset: 0x00017448
	public static method __m2mep@?Exit@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x0400011C RID: 284 RVA: 0x00019458 File Offset: 0x00017458
	public static method __m2mep@?__global_lock@?A0xf28fb846@@$$FYA_NXZ;

	// Token: 0x0400011D RID: 285 RVA: 0x00019468 File Offset: 0x00017468
	public static method __m2mep@?__global_unlock@?A0xf28fb846@@$$FYA_NXZ;

	// Token: 0x0400011E RID: 286 RVA: 0x000194A8 File Offset: 0x000174A8
	public static method __m2mep@?__alloc_global_lock@?A0xf28fb846@@$$FYA_NXZ;

	// Token: 0x0400011F RID: 287 RVA: 0x00019478 File Offset: 0x00017478
	public static method __m2mep@?__dealloc_global_lock@?A0xf28fb846@@$$FYAXXZ;

	// Token: 0x04000120 RID: 288 RVA: 0x00019348 File Offset: 0x00017348
	public static method __m2mep@?_atexit_helper@@$$J0YMHP6MXXZPEA_KPEAPEAP6MXXZ2@Z;

	// Token: 0x04000121 RID: 289 RVA: 0x00019358 File Offset: 0x00017358
	public static method __m2mep@?_exit_callback@@$$J0YMXXZ;

	// Token: 0x04000122 RID: 290 RVA: 0x00019398 File Offset: 0x00017398
	public static method __m2mep@?_initatexit_m@@$$J0YMHXZ;

	// Token: 0x04000123 RID: 291 RVA: 0x000193A8 File Offset: 0x000173A8
	public static method __m2mep@?_onexit_m@@$$J0YMP6MHXZP6MHXZ@Z;

	// Token: 0x04000124 RID: 292 RVA: 0x00019368 File Offset: 0x00017368
	public static method __m2mep@?_atexit_m@@$$J0YMHP6MXXZ@Z;

	// Token: 0x04000125 RID: 293 RVA: 0x000193B8 File Offset: 0x000173B8
	public static method __m2mep@?_initatexit_app_domain@@$$J0YMHXZ;

	// Token: 0x04000126 RID: 294 RVA: 0x00019378 File Offset: 0x00017378
	public static method __m2mep@?_app_exit_callback@@$$J0YMXXZ;

	// Token: 0x04000127 RID: 295 RVA: 0x000193C8 File Offset: 0x000173C8
	public static method __m2mep@?_onexit_m_appdomain@@$$J0YMP6MHXZP6MHXZ@Z;

	// Token: 0x04000128 RID: 296 RVA: 0x00019388 File Offset: 0x00017388
	public static method __m2mep@?_atexit_m_appdomain@@$$J0YMHP6MXXZ@Z;

	// Token: 0x04000129 RID: 297 RVA: 0x000194B8 File Offset: 0x000174B8
	public static method __m2mep@?_initterm_e@@$$FYMHPEAP6AHXZ0@Z;

	// Token: 0x0400012A RID: 298 RVA: 0x000194C8 File Offset: 0x000174C8
	public static method __m2mep@?_initterm@@$$FYMXPEAP6AXXZ0@Z;

	// Token: 0x0400012B RID: 299 RVA: 0x000194E8 File Offset: 0x000174E8
	public static method __m2mep@?Handle@ThisModule@<CrtImplementationDetails>@@$$FCM?AVModuleHandle@System@@XZ;

	// Token: 0x0400012C RID: 300 RVA: 0x000194D8 File Offset: 0x000174D8
	public static method __m2mep@?_initterm_m@@$$FYMXPEBQ6MPEBXXZ0@Z;

	// Token: 0x0400012D RID: 301 RVA: 0x000194F8 File Offset: 0x000174F8
	public static method __m2mep@??$ResolveMethod@$$A6MPEBXXZ@ThisModule@<CrtImplementationDetails>@@$$FSMP6MPEBXXZP6MPEBXXZ@Z;

	// Token: 0x0400012E RID: 302 RVA: 0x00019508 File Offset: 0x00017508
	public static method __m2mep@?___CxxCallUnwindDtor@@$$J0YMXP6MXPEAX@Z0@Z;

	// Token: 0x0400012F RID: 303 RVA: 0x00019518 File Offset: 0x00017518
	public static method __m2mep@?___CxxCallUnwindDelDtor@@$$J0YMXP6MXPEAX@Z0@Z;

	// Token: 0x04000130 RID: 304 RVA: 0x00019528 File Offset: 0x00017528
	public static method __m2mep@?___CxxCallUnwindVecDtor@@$$J0YMXP6MXPEAX_KHP6MX0@Z@Z01H2@Z;

	// Token: 0x04000131 RID: 305 RVA: 0x00019C60 File Offset: 0x00017C60
	internal static byte g_fTracing;

	// Token: 0x04000132 RID: 306 RVA: 0x00007208 File Offset: 0x00005208
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_z;

	// Token: 0x04000133 RID: 307 RVA: 0x000071F8 File Offset: 0x000051F8
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_a;

	// Token: 0x04000134 RID: 308 RVA: 0x00007210 File Offset: 0x00005210
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_a;

	// Token: 0x04000135 RID: 309 RVA: 0x0001A0A8 File Offset: 0x000180A8
	internal static volatile __enative_startup_state __native_startup_state;

	// Token: 0x04000136 RID: 310 RVA: 0x00007228 File Offset: 0x00005228
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_z;

	// Token: 0x04000137 RID: 311 RVA: 0x0001A0A0 File Offset: 0x000180A0
	internal unsafe static volatile void* __native_startup_lock;

	// Token: 0x04000138 RID: 312 RVA: 0x00019090 File Offset: 0x00017090
	internal static volatile uint __native_dllmain_reason;
}
