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
using Microsoft.Exchange.Server.Storage.HA;
using Microsoft.Isam.Esent.Interop;

// Token: 0x02000001 RID: 1
internal class <Module>
{
	// Token: 0x06000001 RID: 1 RVA: 0x000011F8 File Offset: 0x000005F8
	internal unsafe static int PrepareInstanceForBackup(_ESEBACK_CONTEXT* pBackupContext, ulong ulInstanceId, void* pvReserved)
	{
		IntPtr context = new IntPtr((void*)pBackupContext);
		JET_INSTANCE instance = default(JET_INSTANCE);
		IntPtr value = new IntPtr((long)ulInstanceId);
		instance.Value = value;
		IntPtr reserved = new IntPtr(pvReserved);
		return EsebackCallbacks.ManagedCallbacks.PrepareInstanceForBackup(context, instance, reserved);
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000123C File Offset: 0x0000063C
	internal unsafe static int DoneWithInstanceForBackup(_ESEBACK_CONTEXT* pBackupContext, ulong ulInstanceId, uint fComplete, void* pvReserved)
	{
		IntPtr context = new IntPtr((void*)pBackupContext);
		JET_INSTANCE instance = default(JET_INSTANCE);
		IntPtr value = new IntPtr((long)ulInstanceId);
		instance.Value = value;
		IntPtr reserved = new IntPtr(pvReserved);
		return EsebackCallbacks.ManagedCallbacks.DoneWithInstanceForBackup(context, instance, fComplete, reserved);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000185C File Offset: 0x00000C5C
	internal unsafe static int GetDatabasesInfo(_ESEBACK_CONTEXT* pBackupContext, uint* pcInfo, _INSTANCE_BACKUP_INFO** prgInfo, uint fReserved)
	{
		MINSTANCE_BACKUP_INFO[] array = null;
		IntPtr context = new IntPtr((void*)pBackupContext);
		array = null;
		int databasesInfo = EsebackCallbacks.ManagedCallbacks.GetDatabasesInfo(context, out array, fReserved);
		*(int*)pcInfo = array.Length;
		*(long*)prgInfo = StructConversion.MakeUnmanagedBackupInfos(array);
		return databasesInfo;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000170C File Offset: 0x00000B0C
	internal unsafe static int FreeDatabasesInfo(_ESEBACK_CONTEXT* __unnamed000, uint cInfo, _INSTANCE_BACKUP_INFO* rgInfo)
	{
		StructConversion.FreeUnmanagedBackupInfos(cInfo, rgInfo);
		return 0;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00001890 File Offset: 0x00000C90
	internal unsafe static int IsSGReplicated(_ESEBACK_CONTEXT* pContext, ulong jetinst, int* pfReplicated, uint cbSGGuid, ushort* wszSGGuid, uint* pcInfo, _LOGSHIP_INFO** prgInfo)
	{
		MLOGSHIP_INFO[] array = null;
		IntPtr context = new IntPtr((void*)pContext);
		JET_INSTANCE instance = default(JET_INSTANCE);
		IntPtr value = new IntPtr((long)jetinst);
		instance.Value = value;
		Guid guid = default(Guid);
		array = null;
		bool flag;
		int result = EsebackCallbacks.ManagedCallbacks.IsSGReplicated(context, instance, out flag, out guid, out array);
		*pfReplicated = (flag ? 1 : 0);
		if (null != array)
		{
			*(int*)pcInfo = array.Length;
			*(long*)prgInfo = StructConversion.MakeUnmanagedLogshipInfos(array);
			if (null == wszSGGuid)
			{
				return result;
			}
			ushort* ptr = null;
			try
			{
				ptr = StructConversion.WszFromString(guid.ToString());
				<Module>.wcscpy_s(wszSGGuid, cbSGGuid >> (int)1L, (ushort*)ptr);
				return result;
			}
			finally
			{
				StructConversion.FreeWsz(ptr);
			}
		}
		*(int*)pcInfo = 0;
		*(long*)prgInfo = 0L;
		return result;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00001724 File Offset: 0x00000B24
	internal unsafe static int FreeShipLogInfo(uint cInfo, _LOGSHIP_INFO* rgInfo)
	{
		StructConversion.FreeUnmanagedLogshipInfos(cInfo, rgInfo);
		return 0;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00001280 File Offset: 0x00000680
	internal static int ServerAccessCheck()
	{
		return EsebackCallbacks.ManagedCallbacks.ServerAccessCheck();
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00001A80 File Offset: 0x00000E80
	internal unsafe static int Trace(sbyte* szData)
	{
		string data = new string(szData);
		return EsebackCallbacks.ManagedCallbacks.Trace(data);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002724 File Offset: 0x00001B24
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.NativeDll.IsInDllMain()
	{
		return (<Module>.__native_dllmain_reason != uint.MaxValue) ? 1 : 0;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002740 File Offset: 0x00001B40
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.NativeDll.IsInProcessAttach()
	{
		return (<Module>.__native_dllmain_reason == 1U) ? 1 : 0;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002758 File Offset: 0x00001B58
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.NativeDll.IsInProcessDetach()
	{
		return (<Module>.__native_dllmain_reason == 0U) ? 1 : 0;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002770 File Offset: 0x00001B70
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

	// Token: 0x0600000D RID: 13 RVA: 0x00002FA8 File Offset: 0x000023A8
	internal static void <CrtImplementationDetails>.ThrowNestedModuleLoadException(System.Exception innerException, System.Exception nestedException)
	{
		throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, nestedException);
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002970 File Offset: 0x00001D70
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage)
	{
		throw new ModuleLoadException(errorMessage);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002984 File Offset: 0x00001D84
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage, System.Exception innerException)
	{
		throw new ModuleLoadException(errorMessage, innerException);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002AA0 File Offset: 0x00001EA0
	internal static void <CrtImplementationDetails>.RegisterModuleUninitializer(EventHandler handler)
	{
		ModuleUninitializer._ModuleUninitializer.AddHandler(handler);
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002AB8 File Offset: 0x00001EB8
	[SecuritySafeCritical]
	internal unsafe static Guid <CrtImplementationDetails>.FromGUID(_GUID* guid)
	{
		Guid result = new Guid((uint)(*guid), *(guid + 4L), *(guid + 6L), *(guid + 8L), *(guid + 9L), *(guid + 10L), *(guid + 11L), *(guid + 12L), *(guid + 13L), *(guid + 14L), *(guid + 15L));
		return result;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002B08 File Offset: 0x00001F08
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

	// Token: 0x06000013 RID: 19 RVA: 0x00002B90 File Offset: 0x00001F90
	internal unsafe static void __release_appdomain(IUnknown* ppUnk)
	{
		object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ppUnk, *(*(long*)ppUnk + 16L));
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002BAC File Offset: 0x00001FAC
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

	// Token: 0x06000015 RID: 21 RVA: 0x00002C0C File Offset: 0x0000200C
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

	// Token: 0x06000016 RID: 22 RVA: 0x00002CD0 File Offset: 0x000020D0
	[SecuritySafeCritical]
	internal unsafe static int <CrtImplementationDetails>.DefaultDomain.DoNothing(void* cookie)
	{
		GC.KeepAlive(int.MaxValue);
		return 0;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002CF0 File Offset: 0x000020F0
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

	// Token: 0x06000018 RID: 24 RVA: 0x00002D44 File Offset: 0x00002144
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

	// Token: 0x06000019 RID: 25 RVA: 0x00002DC4 File Offset: 0x000021C4
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

	// Token: 0x0600001A RID: 26 RVA: 0x00002E00 File Offset: 0x00002200
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsUninitialization()
	{
		return <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002E14 File Offset: 0x00002214
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.DefaultDomain.Initialize()
	{
		<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00003CEC File Offset: 0x000030EC
	internal static void ??__E?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00003D00 File Offset: 0x00003100
	internal static void ??__E?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00003D14 File Offset: 0x00003114
	internal static void ??__E?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA@@YMXXZ()
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = false;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00003D28 File Offset: 0x00003128
	internal static void ??__E?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)0;
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00003D3C File Offset: 0x0000313C
	internal static void ??__E?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)0;
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00003D50 File Offset: 0x00003150
	internal static void ??__E?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)0;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00003D64 File Offset: 0x00003164
	internal static void ??__E?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)0;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002FFC File Offset: 0x000023FC
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeVtables(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during vtable initialization.\n");
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)1;
		<Module>._initterm_m((method*)(&<Module>.?A0xc6f23461.__xi_vt_a), (method*)(&<Module>.?A0xc6f23461.__xi_vt_z));
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)2;
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00003030 File Offset: 0x00002430
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load while attempting to initialize the default appdomain.\n");
		<Module>.<CrtImplementationDetails>.DefaultDomain.Initialize();
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00003050 File Offset: 0x00002450
	[DebuggerStepThrough]
	[SecurityCritical]
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

	// Token: 0x06000026 RID: 38 RVA: 0x000030EC File Offset: 0x000024EC
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerProcess(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during process initialization.\n");
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)1;
		<Module>._initatexit_m();
		<Module>._initterm_m((method*)(&<Module>.?A0xc6f23461.__xc_mp_a), (method*)(&<Module>.?A0xc6f23461.__xc_mp_z));
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)2;
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x0000312C File Offset: 0x0000252C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during appdomain initialization.\n");
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)1;
		<Module>._initatexit_app_domain();
		<Module>._initterm_m((method*)(&<Module>.?A0xc6f23461.__xc_ma_a), (method*)(&<Module>.?A0xc6f23461.__xc_ma_z));
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A = (Progress.State)2;
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00003168 File Offset: 0x00002568
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during registration for the unload events.\n");
		<Module>.<CrtImplementationDetails>.RegisterModuleUninitializer(new EventHandler(<Module>.<CrtImplementationDetails>.LanguageSupport.DomainUnload));
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00003194 File Offset: 0x00002594
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[DebuggerStepThrough]
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

	// Token: 0x0600002A RID: 42 RVA: 0x00002E30 File Offset: 0x00002230
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain()
	{
		<Module>._app_exit_callback();
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002E44 File Offset: 0x00002244
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

	// Token: 0x0600002C RID: 44 RVA: 0x00002E80 File Offset: 0x00002280
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

	// Token: 0x0600002D RID: 45 RVA: 0x00002EB8 File Offset: 0x000022B8
	[PrePrepareMethod]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
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

	// Token: 0x0600002E RID: 46 RVA: 0x00003298 File Offset: 0x00002698
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DebuggerStepThrough]
	[SecurityCritical]
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

	// Token: 0x0600002F RID: 47 RVA: 0x0000330C File Offset: 0x0000270C
	[SecurityCritical]
	internal unsafe static LanguageSupport* <CrtImplementationDetails>.LanguageSupport.{ctor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{ctor}(A_0);
		return A_0;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00003324 File Offset: 0x00002724
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.{dtor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{dtor}(A_0);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00003338 File Offset: 0x00002738
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
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

	// Token: 0x06000032 RID: 50 RVA: 0x000033F4 File Offset: 0x000027F4
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

	// Token: 0x06000033 RID: 51 RVA: 0x00002EF4 File Offset: 0x000022F4
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<System::String\u0020^>* {ctor}(gcroot<System::String\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002F18 File Offset: 0x00002318
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void {dtor}(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Free();
		*A_0 = 0L;
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00002F40 File Offset: 0x00002340
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static gcroot<System::String\u0020^>* =(gcroot<System::String\u0020^>* A_0, string t)
	{
		IntPtr value = new IntPtr(*A_0);
		((GCHandle)value).Target = t;
		return A_0;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002F68 File Offset: 0x00002368
	[SecuritySafeCritical]
	internal unsafe static string PE$AAVString@System@@(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr value = new IntPtr(*A_0);
		return ((GCHandle)value).Target;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00003468 File Offset: 0x00002868
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

	// Token: 0x06000038 RID: 56 RVA: 0x00003970 File Offset: 0x00002D70
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Construct(object value)
	{
		<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00003498 File Offset: 0x00002898
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

	// Token: 0x0600003A RID: 58 RVA: 0x000034E8 File Offset: 0x000028E8
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

	// Token: 0x0600003B RID: 59 RVA: 0x0000350C File Offset: 0x0000290C
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

	// Token: 0x0600003C RID: 60 RVA: 0x00003534 File Offset: 0x00002934
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.AtExitLock.IsInitialized()
	{
		return (<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x0000398C File Offset: 0x00002D8C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.AddRef()
	{
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() == null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Construct(new object());
			<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA = 0;
		}
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA++;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00003550 File Offset: 0x00002950
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

	// Token: 0x0600003F RID: 63 RVA: 0x00003578 File Offset: 0x00002978
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.Enter()
	{
		Monitor.Enter(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00003590 File Offset: 0x00002990
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.Exit()
	{
		Monitor.Exit(<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get());
	}

	// Token: 0x06000041 RID: 65 RVA: 0x000035A8 File Offset: 0x000029A8
	[SecurityCritical]
	[DebuggerStepThrough]
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

	// Token: 0x06000042 RID: 66 RVA: 0x000035C8 File Offset: 0x000029C8
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

	// Token: 0x06000043 RID: 67 RVA: 0x000039BC File Offset: 0x00002DBC
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __alloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.AddRef();
		return <Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized();
	}

	// Token: 0x06000044 RID: 68 RVA: 0x000035E8 File Offset: 0x000029E8
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void __dealloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.RemoveRef();
	}

	// Token: 0x06000045 RID: 69 RVA: 0x000035FC File Offset: 0x000029FC
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

	// Token: 0x06000046 RID: 70 RVA: 0x00003784 File Offset: 0x00002B84
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

	// Token: 0x06000047 RID: 71 RVA: 0x000039D4 File Offset: 0x00002DD4
	[DebuggerStepThrough]
	[SecurityCritical]
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

	// Token: 0x06000048 RID: 72 RVA: 0x00003A1C File Offset: 0x00002E1C
	internal static method _onexit_m(method _Function)
	{
		return (<Module>._atexit_m(_Function) == -1) ? 0L : _Function;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00003834 File Offset: 0x00002C34
	[SecurityCritical]
	internal unsafe static int _atexit_m(method func)
	{
		return <Module>._atexit_helper(<Module>.EncodePointer(func), &<Module>.?A0xf28fb846.__exit_list_size, &<Module>.?A0xf28fb846.__onexitend_m, &<Module>.?A0xf28fb846.__onexitbegin_m);
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00003A38 File Offset: 0x00002E38
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

	// Token: 0x0600004B RID: 75 RVA: 0x0000385C File Offset: 0x00002C5C
	[HandleProcessCorruptedStateExceptions]
	[SecurityCritical]
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

	// Token: 0x0600004C RID: 76 RVA: 0x00003A7C File Offset: 0x00002E7C
	[SecurityCritical]
	internal static method _onexit_m_appdomain(method _Function)
	{
		return (<Module>._atexit_m_appdomain(_Function) == -1) ? 0L : _Function;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00003948 File Offset: 0x00002D48
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _atexit_m_appdomain(method func)
	{
		return <Module>._atexit_helper(<Module>.EncodePointer(func), &<Module>.__exit_list_size_app_domain, &<Module>.__onexitend_app_domain, &<Module>.__onexitbegin_app_domain);
	}

	// Token: 0x0600004E RID: 78
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* DecodePointer(void* Ptr);

	// Token: 0x0600004F RID: 79
	[SuppressUnmanagedCodeSecurity]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* EncodePointer(void* Ptr);

	// Token: 0x06000050 RID: 80 RVA: 0x00003A98 File Offset: 0x00002E98
	[DebuggerStepThrough]
	[SecurityCritical]
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

	// Token: 0x06000051 RID: 81 RVA: 0x00003AC8 File Offset: 0x00002EC8
	[SecurityCritical]
	[DebuggerStepThrough]
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

	// Token: 0x06000052 RID: 82 RVA: 0x00003AF0 File Offset: 0x00002EF0
	[DebuggerStepThrough]
	internal static ModuleHandle <CrtImplementationDetails>.ThisModule.Handle()
	{
		return typeof(ThisModule).Module.ModuleHandle;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00003B40 File Offset: 0x00002F40
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

	// Token: 0x06000054 RID: 84 RVA: 0x00003B14 File Offset: 0x00002F14
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static method <CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(method methodToken)
	{
		return <Module>.<CrtImplementationDetails>.ThisModule.Handle().ResolveMethodHandle(methodToken).GetFunctionPointer().ToPointer();
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00003B70 File Offset: 0x00002F70
	[HandleProcessCorruptedStateExceptions]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
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

	// Token: 0x06000056 RID: 86 RVA: 0x00003BB4 File Offset: 0x00002FB4
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

	// Token: 0x06000057 RID: 87 RVA: 0x00003BF8 File Offset: 0x00002FF8
	[HandleProcessCorruptedStateExceptions]
	[SecurityCritical]
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

	// Token: 0x06000058 RID: 88 RVA: 0x00001AA0 File Offset: 0x00000EA0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void* @new(ulong);

	// Token: 0x06000059 RID: 89 RVA: 0x00003C52 File Offset: 0x00003052
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern int wcscpy_s(ushort*, ulong, ushort*);

	// Token: 0x0600005A RID: 90 RVA: 0x00003C40 File Offset: 0x00003040
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern int HrESEBackupRestoreUnregister();

	// Token: 0x0600005B RID: 91 RVA: 0x00003C4C File Offset: 0x0000304C
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern void delete(void*);

	// Token: 0x0600005C RID: 92 RVA: 0x00003C46 File Offset: 0x00003046
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern int HrESEBackupRestoreRegister2(ushort*, uint, ushort*, _ESEBACK_CALLBACKS*, _GUID*);

	// Token: 0x0600005D RID: 93 RVA: 0x00002CC0 File Offset: 0x000020C0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal unsafe static extern void* _getFiberPtrId();

	// Token: 0x0600005E RID: 94 RVA: 0x0000205E File Offset: 0x0000145E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void _amsg_exit(int);

	// Token: 0x0600005F RID: 95 RVA: 0x00002260 File Offset: 0x00001660
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig)]
	internal static extern void __security_init_cookie();

	// Token: 0x06000060 RID: 96 RVA: 0x00003C64 File Offset: 0x00003064
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void Sleep(uint);

	// Token: 0x06000061 RID: 97 RVA: 0x00003C58 File Offset: 0x00003058
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal static extern void _cexit();

	// Token: 0x06000062 RID: 98 RVA: 0x00003C5E File Offset: 0x0000305E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged)]
	internal unsafe static extern int __FrameUnwindFilter(_EXCEPTION_POINTERS*);

	// Token: 0x04000001 RID: 1 RVA: 0x00013000 File Offset: 0x00011000
	public static method __m2mep@?PrepareInstanceForBackup@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KPEAX@Z;

	// Token: 0x04000002 RID: 2 RVA: 0x00013010 File Offset: 0x00011010
	public static method __m2mep@?DoneWithInstanceForBackup@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KKPEAX@Z;

	// Token: 0x04000003 RID: 3 RVA: 0x00013050 File Offset: 0x00011050
	public static method __m2mep@?GetDatabasesInfo@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@PEAKPEAPEAU_INSTANCE_BACKUP_INFO@@K@Z;

	// Token: 0x04000004 RID: 4 RVA: 0x00013030 File Offset: 0x00011030
	public static method __m2mep@?FreeDatabasesInfo@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@KPEAU_INSTANCE_BACKUP_INFO@@@Z;

	// Token: 0x04000005 RID: 5 RVA: 0x00013060 File Offset: 0x00011060
	public static method __m2mep@?IsSGReplicated@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KPEAHKPEAGPEAKPEAPEAU_LOGSHIP_INFO@@@Z;

	// Token: 0x04000006 RID: 6 RVA: 0x00013040 File Offset: 0x00011040
	public static method __m2mep@?FreeShipLogInfo@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJKPEAU_LOGSHIP_INFO@@@Z;

	// Token: 0x04000007 RID: 7 RVA: 0x00013020 File Offset: 0x00011020
	public static method __m2mep@?ServerAccessCheck@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJXZ;

	// Token: 0x04000008 RID: 8 RVA: 0x00013070 File Offset: 0x00011070
	public static method __m2mep@?Trace@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJQEBD@Z;

	// Token: 0x04000009 RID: 9 RVA: 0x00005270 File Offset: 0x00003A70
	public unsafe static int** __unep@?PrepareInstanceForBackup@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KPEAX@Z;

	// Token: 0x0400000A RID: 10 RVA: 0x00005278 File Offset: 0x00003A78
	public unsafe static int** __unep@?DoneWithInstanceForBackup@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KKPEAX@Z;

	// Token: 0x0400000B RID: 11 RVA: 0x000052A0 File Offset: 0x00003AA0
	public unsafe static int** __unep@?ServerAccessCheck@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJXZ;

	// Token: 0x0400000C RID: 12 RVA: 0x00005288 File Offset: 0x00003A88
	public unsafe static int** __unep@?FreeDatabasesInfo@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@KPEAU_INSTANCE_BACKUP_INFO@@@Z;

	// Token: 0x0400000D RID: 13 RVA: 0x00005298 File Offset: 0x00003A98
	public unsafe static int** __unep@?FreeShipLogInfo@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJKPEAU_LOGSHIP_INFO@@@Z;

	// Token: 0x0400000E RID: 14 RVA: 0x00005280 File Offset: 0x00003A80
	public unsafe static int** __unep@?GetDatabasesInfo@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@PEAKPEAPEAU_INSTANCE_BACKUP_INFO@@K@Z;

	// Token: 0x0400000F RID: 15 RVA: 0x00005290 File Offset: 0x00003A90
	public unsafe static int** __unep@?IsSGReplicated@InteropCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJPEAU_ESEBACK_CONTEXT@@_KPEAHKPEAGPEAKPEAPEAU_LOGSHIP_INFO@@@Z;

	// Token: 0x04000010 RID: 16 RVA: 0x000052A8 File Offset: 0x00003AA8
	unsafe static int** __unep@?ErrESECBTrace@NativeCallbacks@HA@Storage@Server@Exchange@Microsoft@@$$FSAJQEBDZZ;

	// Token: 0x04000011 RID: 17 RVA: 0x000052F0 File Offset: 0x00003AF0
	internal static __s_GUID _GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x04000012 RID: 18 RVA: 0x00005320 File Offset: 0x00003B20
	internal static __s_GUID _GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x04000013 RID: 19 RVA: 0x000051D0 File Offset: 0x000039D0
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_z;

	// Token: 0x04000014 RID: 20 RVA: 0x000051D8 File Offset: 0x000039D8
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_a;

	// Token: 0x04000015 RID: 21
	[FixedAddressValueType]
	internal static Progress.State ?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A;

	// Token: 0x04000016 RID: 22 RVA: 0x000051A0 File Offset: 0x000039A0
	internal static method ?InitializedVtables$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000017 RID: 23
	[FixedAddressValueType]
	internal static bool ?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA;

	// Token: 0x04000018 RID: 24 RVA: 0x00005198 File Offset: 0x00003998
	internal static method ?IsDefaultDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000019 RID: 25 RVA: 0x00005180 File Offset: 0x00003980
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_a;

	// Token: 0x0400001A RID: 26
	[FixedAddressValueType]
	internal static Progress.State ?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A;

	// Token: 0x0400001B RID: 27 RVA: 0x000051B8 File Offset: 0x000039B8
	internal static method ?InitializedPerAppDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400001C RID: 28 RVA: 0x000051C0 File Offset: 0x000039C0
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_z;

	// Token: 0x0400001D RID: 29
	[FixedAddressValueType]
	internal static Progress.State ?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A;

	// Token: 0x0400001E RID: 30 RVA: 0x000051A8 File Offset: 0x000039A8
	internal static method ?InitializedNative$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400001F RID: 31 RVA: 0x000051E0 File Offset: 0x000039E0
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_z;

	// Token: 0x04000020 RID: 32 RVA: 0x00005310 File Offset: 0x00003B10
	internal static __s_GUID _GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x04000021 RID: 33
	[FixedAddressValueType]
	internal static int ?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x04000022 RID: 34 RVA: 0x00005190 File Offset: 0x00003990
	internal static method ?Uninitialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000023 RID: 35
	[FixedAddressValueType]
	internal static int ?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x04000024 RID: 36 RVA: 0x00005188 File Offset: 0x00003988
	internal static method ?Initialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000025 RID: 37 RVA: 0x00013ADF File Offset: 0x00011ADF
	internal static bool ?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000026 RID: 38
	[FixedAddressValueType]
	internal static Progress.State ?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4State@Progress@2@A;

	// Token: 0x04000027 RID: 39 RVA: 0x00013ADC File Offset: 0x00011ADC
	internal static bool ?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000028 RID: 40 RVA: 0x00013ADD File Offset: 0x00011ADD
	internal static bool ?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000029 RID: 41 RVA: 0x00013AD8 File Offset: 0x00011AD8
	internal static int ?Count@AllDomains@<CrtImplementationDetails>@@2HA;

	// Token: 0x0400002A RID: 42 RVA: 0x000052B4 File Offset: 0x00003AB4
	internal static uint ?ProcessAttach@NativeDll@<CrtImplementationDetails>@@0IB;

	// Token: 0x0400002B RID: 43 RVA: 0x000052B8 File Offset: 0x00003AB8
	internal static uint ?ThreadAttach@NativeDll@<CrtImplementationDetails>@@0IB;

	// Token: 0x0400002C RID: 44 RVA: 0x00013098 File Offset: 0x00011098
	internal static TriBool.State ?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A;

	// Token: 0x0400002D RID: 45 RVA: 0x000052B0 File Offset: 0x00003AB0
	internal static uint ?ProcessDetach@NativeDll@<CrtImplementationDetails>@@0IB;

	// Token: 0x0400002E RID: 46 RVA: 0x000052BC File Offset: 0x00003ABC
	internal static uint ?ThreadDetach@NativeDll@<CrtImplementationDetails>@@0IB;

	// Token: 0x0400002F RID: 47 RVA: 0x000052C0 File Offset: 0x00003AC0
	internal static uint ?ProcessVerifier@NativeDll@<CrtImplementationDetails>@@0IB;

	// Token: 0x04000030 RID: 48 RVA: 0x00013094 File Offset: 0x00011094
	internal static TriBool.State ?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4State@TriBool@2@A;

	// Token: 0x04000031 RID: 49 RVA: 0x00013ADE File Offset: 0x00011ADE
	internal static bool ?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000032 RID: 50 RVA: 0x000051C8 File Offset: 0x000039C8
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_a;

	// Token: 0x04000033 RID: 51 RVA: 0x00005300 File Offset: 0x00003B00
	internal static __s_GUID _GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x04000034 RID: 52 RVA: 0x000051B0 File Offset: 0x000039B0
	internal static method ?InitializedPerProcess$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000035 RID: 53 RVA: 0x000130B0 File Offset: 0x000110B0
	public static method __m2mep@?IsInDllMain@NativeDll@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000036 RID: 54 RVA: 0x000130C0 File Offset: 0x000110C0
	public static method __m2mep@?IsInProcessAttach@NativeDll@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000037 RID: 55 RVA: 0x000130D0 File Offset: 0x000110D0
	public static method __m2mep@?IsInProcessDetach@NativeDll@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000038 RID: 56 RVA: 0x000130E0 File Offset: 0x000110E0
	public static method __m2mep@?IsSafeForManagedCode@NativeDll@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000039 RID: 57 RVA: 0x00013250 File Offset: 0x00011250
	public static method __m2mep@?ThrowNestedModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVException@System@@0@Z;

	// Token: 0x0400003A RID: 58 RVA: 0x000130F0 File Offset: 0x000110F0
	public static method __m2mep@?ThrowModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVString@System@@@Z;

	// Token: 0x0400003B RID: 59 RVA: 0x00013100 File Offset: 0x00011100
	public static method __m2mep@?ThrowModuleLoadException@<CrtImplementationDetails>@@$$FYMXPE$AAVString@System@@PE$AAVException@3@@Z;

	// Token: 0x0400003C RID: 60 RVA: 0x00013110 File Offset: 0x00011110
	public static method __m2mep@?RegisterModuleUninitializer@<CrtImplementationDetails>@@$$FYMXPE$AAVEventHandler@System@@@Z;

	// Token: 0x0400003D RID: 61 RVA: 0x00013120 File Offset: 0x00011120
	public static method __m2mep@?FromGUID@<CrtImplementationDetails>@@$$FYM?AVGuid@System@@AEBU_GUID@@@Z;

	// Token: 0x0400003E RID: 62 RVA: 0x00013130 File Offset: 0x00011130
	public static method __m2mep@?__get_default_appdomain@@$$FYAJPEAPEAUIUnknown@@@Z;

	// Token: 0x0400003F RID: 63 RVA: 0x00013140 File Offset: 0x00011140
	public static method __m2mep@?__release_appdomain@@$$FYAXPEAUIUnknown@@@Z;

	// Token: 0x04000040 RID: 64 RVA: 0x00013150 File Offset: 0x00011150
	public static method __m2mep@?GetDefaultDomain@<CrtImplementationDetails>@@$$FYMPE$AAVAppDomain@System@@XZ;

	// Token: 0x04000041 RID: 65 RVA: 0x00013160 File Offset: 0x00011160
	public static method __m2mep@?DoCallBackInDefaultDomain@<CrtImplementationDetails>@@$$FYAXP6AJPEAX@Z0@Z;

	// Token: 0x04000042 RID: 66 RVA: 0x00013170 File Offset: 0x00011170
	public static method __m2mep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000043 RID: 67 RVA: 0x00013180 File Offset: 0x00011180
	public static method __m2mep@?HasPerProcess@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000044 RID: 68 RVA: 0x00013190 File Offset: 0x00011190
	public static method __m2mep@?HasNative@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000045 RID: 69 RVA: 0x000131A0 File Offset: 0x000111A0
	public static method __m2mep@?NeedsInitialization@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000046 RID: 70 RVA: 0x000131B0 File Offset: 0x000111B0
	public static method __m2mep@?NeedsUninitialization@DefaultDomain@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x04000047 RID: 71 RVA: 0x000131C0 File Offset: 0x000111C0
	public static method __m2mep@?Initialize@DefaultDomain@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x04000048 RID: 72 RVA: 0x00013260 File Offset: 0x00011260
	public static method __m2mep@?InitializeVtables@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x04000049 RID: 73 RVA: 0x00013270 File Offset: 0x00011270
	public static method __m2mep@?InitializeDefaultAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x0400004A RID: 74 RVA: 0x00013280 File Offset: 0x00011280
	public static method __m2mep@?InitializeNative@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x0400004B RID: 75 RVA: 0x00013290 File Offset: 0x00011290
	public static method __m2mep@?InitializePerProcess@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x0400004C RID: 76 RVA: 0x000132A0 File Offset: 0x000112A0
	public static method __m2mep@?InitializePerAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x0400004D RID: 77 RVA: 0x000132B0 File Offset: 0x000112B0
	public static method __m2mep@?InitializeUninitializer@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x0400004E RID: 78 RVA: 0x000132C0 File Offset: 0x000112C0
	public static method __m2mep@?_Initialize@LanguageSupport@<CrtImplementationDetails>@@$$FAEAAXXZ;

	// Token: 0x0400004F RID: 79 RVA: 0x000131D0 File Offset: 0x000111D0
	public static method __m2mep@?UninitializeAppDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x04000050 RID: 80 RVA: 0x000131E0 File Offset: 0x000111E0
	public static method __m2mep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000051 RID: 81 RVA: 0x000131F0 File Offset: 0x000111F0
	public static method __m2mep@?UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x04000052 RID: 82 RVA: 0x00013200 File Offset: 0x00011200
	public static method __m2mep@?DomainUnload@LanguageSupport@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@PE$AAVEventArgs@4@@Z;

	// Token: 0x04000053 RID: 83 RVA: 0x000132D0 File Offset: 0x000112D0
	public static method __m2mep@?Cleanup@LanguageSupport@<CrtImplementationDetails>@@$$FAEAMXPE$AAVException@System@@@Z;

	// Token: 0x04000054 RID: 84 RVA: 0x000132E0 File Offset: 0x000112E0
	public static method __m2mep@??0LanguageSupport@<CrtImplementationDetails>@@$$FQEAA@XZ;

	// Token: 0x04000055 RID: 85 RVA: 0x000132F0 File Offset: 0x000112F0
	public static method __m2mep@??1LanguageSupport@<CrtImplementationDetails>@@$$FQEAA@XZ;

	// Token: 0x04000056 RID: 86 RVA: 0x00013300 File Offset: 0x00011300
	public static method __m2mep@?Initialize@LanguageSupport@<CrtImplementationDetails>@@$$FQEAAXXZ;

	// Token: 0x04000057 RID: 87 RVA: 0x000130A0 File Offset: 0x000110A0
	public static method cctor@@$$FYMXXZ;

	// Token: 0x04000058 RID: 88 RVA: 0x00013210 File Offset: 0x00011210
	public static method __m2mep@??0?$gcroot@PE$AAVString@System@@@@$$FQEAA@XZ;

	// Token: 0x04000059 RID: 89 RVA: 0x00013220 File Offset: 0x00011220
	public static method __m2mep@??1?$gcroot@PE$AAVString@System@@@@$$FQEAA@XZ;

	// Token: 0x0400005A RID: 90 RVA: 0x00013230 File Offset: 0x00011230
	public static method __m2mep@??4?$gcroot@PE$AAVString@System@@@@$$FQEAMAEAU0@PE$AAVString@System@@@Z;

	// Token: 0x0400005B RID: 91 RVA: 0x00013240 File Offset: 0x00011240
	public static method __m2mep@??B?$gcroot@PE$AAVString@System@@@@$$FQEBMPE$AAVString@System@@XZ;

	// Token: 0x0400005C RID: 92 RVA: 0x00005330 File Offset: 0x00003B30
	public unsafe static int** __unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400005D RID: 93 RVA: 0x00005338 File Offset: 0x00003B38
	public unsafe static int** __unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400005E RID: 94
	[FixedAddressValueType]
	internal static ulong __exit_list_size_app_domain;

	// Token: 0x0400005F RID: 95
	[FixedAddressValueType]
	internal unsafe static method* __onexitbegin_app_domain;

	// Token: 0x04000060 RID: 96 RVA: 0x00013C28 File Offset: 0x00011C28
	internal static ulong __exit_list_size;

	// Token: 0x04000061 RID: 97
	[FixedAddressValueType]
	internal unsafe static method* __onexitend_app_domain;

	// Token: 0x04000062 RID: 98 RVA: 0x00013C18 File Offset: 0x00011C18
	internal unsafe static method* __onexitbegin_m;

	// Token: 0x04000063 RID: 99 RVA: 0x00013C20 File Offset: 0x00011C20
	internal unsafe static method* __onexitend_m;

	// Token: 0x04000064 RID: 100
	[FixedAddressValueType]
	internal static int ?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA;

	// Token: 0x04000065 RID: 101
	[FixedAddressValueType]
	internal unsafe static void* ?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA;

	// Token: 0x04000066 RID: 102 RVA: 0x000133D8 File Offset: 0x000113D8
	public static method __m2mep@?_handle@AtExitLock@<CrtImplementationDetails>@@$$FCMPE$AAVGCHandle@InteropServices@Runtime@System@@XZ;

	// Token: 0x04000067 RID: 103 RVA: 0x00013488 File Offset: 0x00011488
	public static method __m2mep@?_lock_Construct@AtExitLock@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@@Z;

	// Token: 0x04000068 RID: 104 RVA: 0x000133E8 File Offset: 0x000113E8
	public static method __m2mep@?_lock_Set@AtExitLock@<CrtImplementationDetails>@@$$FCMXPE$AAVObject@System@@@Z;

	// Token: 0x04000069 RID: 105 RVA: 0x000133F8 File Offset: 0x000113F8
	public static method __m2mep@?_lock_Get@AtExitLock@<CrtImplementationDetails>@@$$FCMPE$AAVObject@System@@XZ;

	// Token: 0x0400006A RID: 106 RVA: 0x00013408 File Offset: 0x00011408
	public static method __m2mep@?_lock_Destruct@AtExitLock@<CrtImplementationDetails>@@$$FCAXXZ;

	// Token: 0x0400006B RID: 107 RVA: 0x00013418 File Offset: 0x00011418
	public static method __m2mep@?IsInitialized@AtExitLock@<CrtImplementationDetails>@@$$FSA_NXZ;

	// Token: 0x0400006C RID: 108 RVA: 0x00013498 File Offset: 0x00011498
	public static method __m2mep@?AddRef@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x0400006D RID: 109 RVA: 0x00013428 File Offset: 0x00011428
	public static method __m2mep@?RemoveRef@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x0400006E RID: 110 RVA: 0x00013438 File Offset: 0x00011438
	public static method __m2mep@?Enter@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x0400006F RID: 111 RVA: 0x00013448 File Offset: 0x00011448
	public static method __m2mep@?Exit@AtExitLock@<CrtImplementationDetails>@@$$FSAXXZ;

	// Token: 0x04000070 RID: 112 RVA: 0x00013458 File Offset: 0x00011458
	public static method __m2mep@?__global_lock@?A0xf28fb846@@$$FYA_NXZ;

	// Token: 0x04000071 RID: 113 RVA: 0x00013468 File Offset: 0x00011468
	public static method __m2mep@?__global_unlock@?A0xf28fb846@@$$FYA_NXZ;

	// Token: 0x04000072 RID: 114 RVA: 0x000134A8 File Offset: 0x000114A8
	public static method __m2mep@?__alloc_global_lock@?A0xf28fb846@@$$FYA_NXZ;

	// Token: 0x04000073 RID: 115 RVA: 0x00013478 File Offset: 0x00011478
	public static method __m2mep@?__dealloc_global_lock@?A0xf28fb846@@$$FYAXXZ;

	// Token: 0x04000074 RID: 116 RVA: 0x00013348 File Offset: 0x00011348
	public static method __m2mep@?_atexit_helper@@$$J0YMHP6MXXZPEA_KPEAPEAP6MXXZ2@Z;

	// Token: 0x04000075 RID: 117 RVA: 0x00013358 File Offset: 0x00011358
	public static method __m2mep@?_exit_callback@@$$J0YMXXZ;

	// Token: 0x04000076 RID: 118 RVA: 0x00013398 File Offset: 0x00011398
	public static method __m2mep@?_initatexit_m@@$$J0YMHXZ;

	// Token: 0x04000077 RID: 119 RVA: 0x000133A8 File Offset: 0x000113A8
	public static method __m2mep@?_onexit_m@@$$J0YMP6MHXZP6MHXZ@Z;

	// Token: 0x04000078 RID: 120 RVA: 0x00013368 File Offset: 0x00011368
	public static method __m2mep@?_atexit_m@@$$J0YMHP6MXXZ@Z;

	// Token: 0x04000079 RID: 121 RVA: 0x000133B8 File Offset: 0x000113B8
	public static method __m2mep@?_initatexit_app_domain@@$$J0YMHXZ;

	// Token: 0x0400007A RID: 122 RVA: 0x00013378 File Offset: 0x00011378
	public static method __m2mep@?_app_exit_callback@@$$J0YMXXZ;

	// Token: 0x0400007B RID: 123 RVA: 0x000133C8 File Offset: 0x000113C8
	public static method __m2mep@?_onexit_m_appdomain@@$$J0YMP6MHXZP6MHXZ@Z;

	// Token: 0x0400007C RID: 124 RVA: 0x00013388 File Offset: 0x00011388
	public static method __m2mep@?_atexit_m_appdomain@@$$J0YMHP6MXXZ@Z;

	// Token: 0x0400007D RID: 125 RVA: 0x000134B8 File Offset: 0x000114B8
	public static method __m2mep@?_initterm_e@@$$FYMHPEAP6AHXZ0@Z;

	// Token: 0x0400007E RID: 126 RVA: 0x000134C8 File Offset: 0x000114C8
	public static method __m2mep@?_initterm@@$$FYMXPEAP6AXXZ0@Z;

	// Token: 0x0400007F RID: 127 RVA: 0x000134E8 File Offset: 0x000114E8
	public static method __m2mep@?Handle@ThisModule@<CrtImplementationDetails>@@$$FCM?AVModuleHandle@System@@XZ;

	// Token: 0x04000080 RID: 128 RVA: 0x000134D8 File Offset: 0x000114D8
	public static method __m2mep@?_initterm_m@@$$FYMXPEBQ6MPEBXXZ0@Z;

	// Token: 0x04000081 RID: 129 RVA: 0x000134F8 File Offset: 0x000114F8
	public static method __m2mep@??$ResolveMethod@$$A6MPEBXXZ@ThisModule@<CrtImplementationDetails>@@$$FSMP6MPEBXXZP6MPEBXXZ@Z;

	// Token: 0x04000082 RID: 130 RVA: 0x00013508 File Offset: 0x00011508
	public static method __m2mep@?___CxxCallUnwindDtor@@$$J0YMXP6MXPEAX@Z0@Z;

	// Token: 0x04000083 RID: 131 RVA: 0x00013518 File Offset: 0x00011518
	public static method __m2mep@?___CxxCallUnwindDelDtor@@$$J0YMXP6MXPEAX@Z0@Z;

	// Token: 0x04000084 RID: 132 RVA: 0x00013528 File Offset: 0x00011528
	public static method __m2mep@?___CxxCallUnwindVecDtor@@$$J0YMXP6MXPEAX_KHP6MX0@Z@Z01H2@Z;

	// Token: 0x04000085 RID: 133 RVA: 0x00005158 File Offset: 0x00003958
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_z;

	// Token: 0x04000086 RID: 134 RVA: 0x00005150 File Offset: 0x00003950
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_a;

	// Token: 0x04000087 RID: 135 RVA: 0x00005160 File Offset: 0x00003960
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_a;

	// Token: 0x04000088 RID: 136 RVA: 0x00013C68 File Offset: 0x00011C68
	internal static volatile __enative_startup_state __native_startup_state;

	// Token: 0x04000089 RID: 137 RVA: 0x00005178 File Offset: 0x00003978
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_z;

	// Token: 0x0400008A RID: 138 RVA: 0x00013C60 File Offset: 0x00011C60
	internal unsafe static volatile void* __native_startup_lock;

	// Token: 0x0400008B RID: 139 RVA: 0x00013090 File Offset: 0x00011090
	internal static volatile uint __native_dllmain_reason;
}
