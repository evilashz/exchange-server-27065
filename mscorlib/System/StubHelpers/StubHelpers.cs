using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x0200057F RID: 1407
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	[SuppressUnmanagedCodeSecurity]
	internal static class StubHelpers
	{
		// Token: 0x06004212 RID: 16914
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsQCall(IntPtr pMD);

		// Token: 0x06004213 RID: 16915
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InitDeclaringType(IntPtr pMD);

		// Token: 0x06004214 RID: 16916
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetNDirectTarget(IntPtr pMD);

		// Token: 0x06004215 RID: 16917
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetDelegateTarget(Delegate pThis, ref IntPtr pStubArg);

		// Token: 0x06004216 RID: 16918
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DemandPermission(IntPtr pNMD);

		// Token: 0x06004217 RID: 16919
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetLastError();

		// Token: 0x06004218 RID: 16920
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ThrowInteropParamException(int resID, int paramIdx);

		// Token: 0x06004219 RID: 16921 RVA: 0x000F59A8 File Offset: 0x000F3BA8
		[SecurityCritical]
		internal static IntPtr AddToCleanupList(ref CleanupWorkList pCleanupWorkList, SafeHandle handle)
		{
			if (pCleanupWorkList == null)
			{
				pCleanupWorkList = new CleanupWorkList();
			}
			CleanupWorkListElement cleanupWorkListElement = new CleanupWorkListElement(handle);
			pCleanupWorkList.Add(cleanupWorkListElement);
			return StubHelpers.SafeHandleAddRef(handle, ref cleanupWorkListElement.m_owned);
		}

		// Token: 0x0600421A RID: 16922 RVA: 0x000F59DB File Offset: 0x000F3BDB
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static void DestroyCleanupList(ref CleanupWorkList pCleanupWorkList)
		{
			if (pCleanupWorkList != null)
			{
				pCleanupWorkList.Destroy();
				pCleanupWorkList = null;
			}
		}

		// Token: 0x0600421B RID: 16923 RVA: 0x000F59EC File Offset: 0x000F3BEC
		internal static Exception GetHRExceptionObject(int hr)
		{
			Exception ex = StubHelpers.InternalGetHRExceptionObject(hr);
			ex.InternalPreserveStackTrace();
			return ex;
		}

		// Token: 0x0600421C RID: 16924
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Exception InternalGetHRExceptionObject(int hr);

		// Token: 0x0600421D RID: 16925 RVA: 0x000F5A08 File Offset: 0x000F3C08
		internal static Exception GetCOMHRExceptionObject(int hr, IntPtr pCPCMD, object pThis)
		{
			Exception ex = StubHelpers.InternalGetCOMHRExceptionObject(hr, pCPCMD, pThis, false);
			ex.InternalPreserveStackTrace();
			return ex;
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x000F5A28 File Offset: 0x000F3C28
		internal static Exception GetCOMHRExceptionObject_WinRT(int hr, IntPtr pCPCMD, object pThis)
		{
			Exception ex = StubHelpers.InternalGetCOMHRExceptionObject(hr, pCPCMD, pThis, true);
			ex.InternalPreserveStackTrace();
			return ex;
		}

		// Token: 0x0600421F RID: 16927
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Exception InternalGetCOMHRExceptionObject(int hr, IntPtr pCPCMD, object pThis, bool fForWinRT);

		// Token: 0x06004220 RID: 16928
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr CreateCustomMarshalerHelper(IntPtr pMD, int paramToken, IntPtr hndManagedType);

		// Token: 0x06004221 RID: 16929 RVA: 0x000F5A46 File Offset: 0x000F3C46
		[SecurityCritical]
		internal static IntPtr SafeHandleAddRef(SafeHandle pHandle, ref bool success)
		{
			if (pHandle == null)
			{
				throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_SafeHandle"));
			}
			pHandle.DangerousAddRef(ref success);
			if (!success)
			{
				return IntPtr.Zero;
			}
			return pHandle.DangerousGetHandle();
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x000F5A74 File Offset: 0x000F3C74
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static void SafeHandleRelease(SafeHandle pHandle)
		{
			if (pHandle == null)
			{
				throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_SafeHandle"));
			}
			try
			{
				pHandle.DangerousRelease();
			}
			catch (Exception ex)
			{
				Mda.ReportErrorSafeHandleRelease(ex);
			}
		}

		// Token: 0x06004223 RID: 16931
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetCOMIPFromRCW(object objSrc, IntPtr pCPCMD, out IntPtr ppTarget, out bool pfNeedsRelease);

		// Token: 0x06004224 RID: 16932
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetCOMIPFromRCW_WinRT(object objSrc, IntPtr pCPCMD, out IntPtr ppTarget);

		// Token: 0x06004225 RID: 16933
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetCOMIPFromRCW_WinRTSharedGeneric(object objSrc, IntPtr pCPCMD, out IntPtr ppTarget);

		// Token: 0x06004226 RID: 16934
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetCOMIPFromRCW_WinRTDelegate(object objSrc, IntPtr pCPCMD, out IntPtr ppTarget);

		// Token: 0x06004227 RID: 16935
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ShouldCallWinRTInterface(object objSrc, IntPtr pCPCMD);

		// Token: 0x06004228 RID: 16936
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Delegate GetTargetForAmbiguousVariantCall(object objSrc, IntPtr pMT, out bool fUseString);

		// Token: 0x06004229 RID: 16937
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void StubRegisterRCW(object pThis);

		// Token: 0x0600422A RID: 16938
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void StubUnregisterRCW(object pThis);

		// Token: 0x0600422B RID: 16939
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetDelegateInvokeMethod(Delegate pThis);

		// Token: 0x0600422C RID: 16940
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object GetWinRTFactoryObject(IntPtr pCPCMD);

		// Token: 0x0600422D RID: 16941
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetWinRTFactoryReturnValue(object pThis, IntPtr pCtorEntry);

		// Token: 0x0600422E RID: 16942
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetOuterInspectable(object pThis, IntPtr pCtorMD);

		// Token: 0x0600422F RID: 16943
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Exception TriggerExceptionSwallowedMDA(Exception ex, IntPtr pManagedTarget);

		// Token: 0x06004230 RID: 16944
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CheckCollectedDelegateMDA(IntPtr pEntryThunk);

		// Token: 0x06004231 RID: 16945
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ProfilerBeginTransitionCallback(IntPtr pSecretParam, IntPtr pThread, object pThis);

		// Token: 0x06004232 RID: 16946
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ProfilerEndTransitionCallback(IntPtr pMD, IntPtr pThread);

		// Token: 0x06004233 RID: 16947 RVA: 0x000F5AB8 File Offset: 0x000F3CB8
		internal static void CheckStringLength(int length)
		{
			StubHelpers.CheckStringLength((uint)length);
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x000F5AC0 File Offset: 0x000F3CC0
		internal static void CheckStringLength(uint length)
		{
			if (length > 2147483632U)
			{
				throw new MarshalDirectiveException(Environment.GetResourceString("Marshaler_StringTooLong"));
			}
		}

		// Token: 0x06004235 RID: 16949
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern int strlen(sbyte* ptr);

		// Token: 0x06004236 RID: 16950
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DecimalCanonicalizeInternal(ref decimal dec);

		// Token: 0x06004237 RID: 16951
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void FmtClassUpdateNativeInternal(object obj, byte* pNative, ref CleanupWorkList pCleanupWorkList);

		// Token: 0x06004238 RID: 16952
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void FmtClassUpdateCLRInternal(object obj, byte* pNative);

		// Token: 0x06004239 RID: 16953
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void LayoutDestroyNativeInternal(byte* pNative, IntPtr pMT);

		// Token: 0x0600423A RID: 16954
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object AllocateInternal(IntPtr typeHandle);

		// Token: 0x0600423B RID: 16955
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void MarshalToUnmanagedVaListInternal(IntPtr va_list, uint vaListSize, IntPtr pArgIterator);

		// Token: 0x0600423C RID: 16956
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void MarshalToManagedVaListInternal(IntPtr va_list, IntPtr pArgIterator);

		// Token: 0x0600423D RID: 16957
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint CalcVaListSize(IntPtr va_list);

		// Token: 0x0600423E RID: 16958
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ValidateObject(object obj, IntPtr pMD, object pThis);

		// Token: 0x0600423F RID: 16959
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void LogPinnedArgument(IntPtr localDesc, IntPtr nativeArg);

		// Token: 0x06004240 RID: 16960
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ValidateByref(IntPtr byref, IntPtr pMD, object pThis);

		// Token: 0x06004241 RID: 16961
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetStubContext();

		// Token: 0x06004242 RID: 16962
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetStubContextAddr();

		// Token: 0x06004243 RID: 16963
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void TriggerGCForMDA();
	}
}
