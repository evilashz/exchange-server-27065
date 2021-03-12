using System;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200087E RID: 2174
	[__DynamicallyInvokable]
	public static class RuntimeHelpers
	{
		// Token: 0x06005C65 RID: 23653
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void InitializeArray(Array array, RuntimeFieldHandle fldHandle);

		// Token: 0x06005C66 RID: 23654
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object GetObjectValue(object obj);

		// Token: 0x06005C67 RID: 23655
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _RunClassConstructor(RuntimeType type);

		// Token: 0x06005C68 RID: 23656 RVA: 0x00144855 File Offset: 0x00142A55
		[__DynamicallyInvokable]
		public static void RunClassConstructor(RuntimeTypeHandle type)
		{
			RuntimeHelpers._RunClassConstructor(type.GetRuntimeType());
		}

		// Token: 0x06005C69 RID: 23657
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _RunModuleConstructor(RuntimeModule module);

		// Token: 0x06005C6A RID: 23658 RVA: 0x00144863 File Offset: 0x00142A63
		public static void RunModuleConstructor(ModuleHandle module)
		{
			RuntimeHelpers._RunModuleConstructor(module.GetRuntimeModule());
		}

		// Token: 0x06005C6B RID: 23659
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _PrepareMethod(IRuntimeMethodInfo method, IntPtr* pInstantiation, int cInstantiation);

		// Token: 0x06005C6C RID: 23660
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void _CompileMethod(IRuntimeMethodInfo method);

		// Token: 0x06005C6D RID: 23661 RVA: 0x00144871 File Offset: 0x00142A71
		[SecurityCritical]
		public static void PrepareMethod(RuntimeMethodHandle method)
		{
			RuntimeHelpers._PrepareMethod(method.GetMethodInfo(), null, 0);
		}

		// Token: 0x06005C6E RID: 23662 RVA: 0x00144884 File Offset: 0x00142A84
		[SecurityCritical]
		public unsafe static void PrepareMethod(RuntimeMethodHandle method, RuntimeTypeHandle[] instantiation)
		{
			int cInstantiation;
			IntPtr[] array = RuntimeTypeHandle.CopyRuntimeTypeHandles(instantiation, out cInstantiation);
			fixed (IntPtr* ptr = array)
			{
				RuntimeHelpers._PrepareMethod(method.GetMethodInfo(), ptr, cInstantiation);
				GC.KeepAlive(instantiation);
			}
		}

		// Token: 0x06005C6F RID: 23663
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PrepareDelegate(Delegate d);

		// Token: 0x06005C70 RID: 23664
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PrepareContractedDelegate(Delegate d);

		// Token: 0x06005C71 RID: 23665
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetHashCode(object o);

		// Token: 0x06005C72 RID: 23666
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public new static extern bool Equals(object o1, object o2);

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06005C73 RID: 23667 RVA: 0x001448C9 File Offset: 0x00142AC9
		[__DynamicallyInvokable]
		public static int OffsetToStringData
		{
			[NonVersionable]
			[__DynamicallyInvokable]
			get
			{
				return 12;
			}
		}

		// Token: 0x06005C74 RID: 23668
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EnsureSufficientExecutionStack();

		// Token: 0x06005C75 RID: 23669
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ProbeForSufficientStack();

		// Token: 0x06005C76 RID: 23670 RVA: 0x001448CD File Offset: 0x00142ACD
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void PrepareConstrainedRegions()
		{
			RuntimeHelpers.ProbeForSufficientStack();
		}

		// Token: 0x06005C77 RID: 23671 RVA: 0x001448D4 File Offset: 0x00142AD4
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void PrepareConstrainedRegionsNoOP()
		{
		}

		// Token: 0x06005C78 RID: 23672
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ExecuteCodeWithGuaranteedCleanup(RuntimeHelpers.TryCode code, RuntimeHelpers.CleanupCode backoutCode, object userData);

		// Token: 0x06005C79 RID: 23673 RVA: 0x001448D6 File Offset: 0x00142AD6
		[PrePrepareMethod]
		internal static void ExecuteBackoutCodeHelper(object backoutCode, object userData, bool exceptionThrown)
		{
			((RuntimeHelpers.CleanupCode)backoutCode)(userData, exceptionThrown);
		}

		// Token: 0x02000C55 RID: 3157
		// (Invoke) Token: 0x06006FB8 RID: 28600
		public delegate void TryCode(object userData);

		// Token: 0x02000C56 RID: 3158
		// (Invoke) Token: 0x06006FBC RID: 28604
		public delegate void CleanupCode(object userData, bool exceptionThrown);
	}
}
