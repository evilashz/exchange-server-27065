﻿using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x02000134 RID: 308
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct RuntimeMethodHandle : ISerializable
	{
		// Token: 0x06001238 RID: 4664 RVA: 0x00037365 File Offset: 0x00035565
		internal static IRuntimeMethodInfo EnsureNonNullMethodInfo(IRuntimeMethodInfo method)
		{
			if (method == null)
			{
				throw new ArgumentNullException(null, Environment.GetResourceString("Arg_InvalidHandle"));
			}
			return method;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x0003737C File Offset: 0x0003557C
		internal static RuntimeMethodHandle EmptyHandle
		{
			get
			{
				return default(RuntimeMethodHandle);
			}
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00037392 File Offset: 0x00035592
		internal RuntimeMethodHandle(IRuntimeMethodInfo method)
		{
			this.m_value = method;
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0003739B File Offset: 0x0003559B
		internal IRuntimeMethodInfo GetMethodInfo()
		{
			return this.m_value;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000373A3 File Offset: 0x000355A3
		[SecurityCritical]
		private static IntPtr GetValueInternal(RuntimeMethodHandle rmh)
		{
			return rmh.Value;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x000373AC File Offset: 0x000355AC
		[SecurityCritical]
		private RuntimeMethodHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MethodBase methodBase = (MethodBase)info.GetValue("MethodObj", typeof(MethodBase));
			this.m_value = methodBase.MethodHandle.m_value;
			if (this.m_value == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
			}
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00037410 File Offset: 0x00035610
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.m_value == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFieldState"));
			}
			MethodBase methodBase = RuntimeType.GetMethodBase(this.m_value);
			info.AddValue("MethodObj", methodBase, typeof(MethodBase));
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x00037468 File Offset: 0x00035668
		public IntPtr Value
		{
			[SecurityCritical]
			get
			{
				if (this.m_value == null)
				{
					return IntPtr.Zero;
				}
				return this.m_value.Value.Value;
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00037496 File Offset: 0x00035696
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return ValueType.GetHashCodeOfPtr(this.Value);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x000374A4 File Offset: 0x000356A4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is RuntimeMethodHandle && ((RuntimeMethodHandle)obj).Value == this.Value;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x000374D4 File Offset: 0x000356D4
		[__DynamicallyInvokable]
		public static bool operator ==(RuntimeMethodHandle left, RuntimeMethodHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x000374DE File Offset: 0x000356DE
		[__DynamicallyInvokable]
		public static bool operator !=(RuntimeMethodHandle left, RuntimeMethodHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x000374EB File Offset: 0x000356EB
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public bool Equals(RuntimeMethodHandle handle)
		{
			return handle.Value == this.Value;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x000374FF File Offset: 0x000356FF
		internal bool IsNullHandle()
		{
			return this.m_value == null;
		}

		// Token: 0x06001246 RID: 4678
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern IntPtr GetFunctionPointer(RuntimeMethodHandleInternal handle);

		// Token: 0x06001247 RID: 4679 RVA: 0x0003750C File Offset: 0x0003570C
		[SecurityCritical]
		public IntPtr GetFunctionPointer()
		{
			IntPtr functionPointer = RuntimeMethodHandle.GetFunctionPointer(RuntimeMethodHandle.EnsureNonNullMethodInfo(this.m_value).Value);
			GC.KeepAlive(this.m_value);
			return functionPointer;
		}

		// Token: 0x06001248 RID: 4680
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CheckLinktimeDemands(IRuntimeMethodInfo method, RuntimeModule module, bool isDecoratedTargetSecurityTransparent);

		// Token: 0x06001249 RID: 4681
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool IsCAVisibleFromDecoratedType(RuntimeTypeHandle attrTypeHandle, IRuntimeMethodInfo attrCtor, RuntimeTypeHandle sourceTypeHandle, RuntimeModule sourceModule);

		// Token: 0x0600124A RID: 4682
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IRuntimeMethodInfo _GetCurrentMethod(ref StackCrawlMark stackMark);

		// Token: 0x0600124B RID: 4683 RVA: 0x0003753B File Offset: 0x0003573B
		[SecuritySafeCritical]
		internal static IRuntimeMethodInfo GetCurrentMethod(ref StackCrawlMark stackMark)
		{
			return RuntimeMethodHandle._GetCurrentMethod(ref stackMark);
		}

		// Token: 0x0600124C RID: 4684
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MethodAttributes GetAttributes(RuntimeMethodHandleInternal method);

		// Token: 0x0600124D RID: 4685 RVA: 0x00037544 File Offset: 0x00035744
		[SecurityCritical]
		internal static MethodAttributes GetAttributes(IRuntimeMethodInfo method)
		{
			MethodAttributes attributes = RuntimeMethodHandle.GetAttributes(method.Value);
			GC.KeepAlive(method);
			return attributes;
		}

		// Token: 0x0600124E RID: 4686
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MethodImplAttributes GetImplAttributes(IRuntimeMethodInfo method);

		// Token: 0x0600124F RID: 4687
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void ConstructInstantiation(IRuntimeMethodInfo method, TypeNameFormatFlags format, StringHandleOnStack retString);

		// Token: 0x06001250 RID: 4688 RVA: 0x00037564 File Offset: 0x00035764
		[SecuritySafeCritical]
		internal static string ConstructInstantiation(IRuntimeMethodInfo method, TypeNameFormatFlags format)
		{
			string result = null;
			RuntimeMethodHandle.ConstructInstantiation(RuntimeMethodHandle.EnsureNonNullMethodInfo(method), format, JitHelpers.GetStringHandleOnStack(ref result));
			return result;
		}

		// Token: 0x06001251 RID: 4689
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetDeclaringType(RuntimeMethodHandleInternal method);

		// Token: 0x06001252 RID: 4690 RVA: 0x00037588 File Offset: 0x00035788
		[SecuritySafeCritical]
		internal static RuntimeType GetDeclaringType(IRuntimeMethodInfo method)
		{
			RuntimeType declaringType = RuntimeMethodHandle.GetDeclaringType(method.Value);
			GC.KeepAlive(method);
			return declaringType;
		}

		// Token: 0x06001253 RID: 4691
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetSlot(RuntimeMethodHandleInternal method);

		// Token: 0x06001254 RID: 4692 RVA: 0x000375A8 File Offset: 0x000357A8
		[SecurityCritical]
		internal static int GetSlot(IRuntimeMethodInfo method)
		{
			int slot = RuntimeMethodHandle.GetSlot(method.Value);
			GC.KeepAlive(method);
			return slot;
		}

		// Token: 0x06001255 RID: 4693
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetMethodDef(IRuntimeMethodInfo method);

		// Token: 0x06001256 RID: 4694
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetName(RuntimeMethodHandleInternal method);

		// Token: 0x06001257 RID: 4695 RVA: 0x000375C8 File Offset: 0x000357C8
		[SecurityCritical]
		internal static string GetName(IRuntimeMethodInfo method)
		{
			string name = RuntimeMethodHandle.GetName(method.Value);
			GC.KeepAlive(method);
			return name;
		}

		// Token: 0x06001258 RID: 4696
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* _GetUtf8Name(RuntimeMethodHandleInternal method);

		// Token: 0x06001259 RID: 4697 RVA: 0x000375E8 File Offset: 0x000357E8
		[SecurityCritical]
		internal static Utf8String GetUtf8Name(RuntimeMethodHandleInternal method)
		{
			return new Utf8String(RuntimeMethodHandle._GetUtf8Name(method));
		}

		// Token: 0x0600125A RID: 4698
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool MatchesNameHash(RuntimeMethodHandleInternal method, uint hash);

		// Token: 0x0600125B RID: 4699
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object InvokeMethod(object target, object[] arguments, Signature sig, bool constructor);

		// Token: 0x0600125C RID: 4700 RVA: 0x000375F5 File Offset: 0x000357F5
		[SecurityCritical]
		internal static INVOCATION_FLAGS GetSecurityFlags(IRuntimeMethodInfo handle)
		{
			return (INVOCATION_FLAGS)RuntimeMethodHandle.GetSpecialSecurityFlags(handle);
		}

		// Token: 0x0600125D RID: 4701
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetSpecialSecurityFlags(IRuntimeMethodInfo method);

		// Token: 0x0600125E RID: 4702
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void PerformSecurityCheck(object obj, RuntimeMethodHandleInternal method, RuntimeType parent, uint invocationFlags);

		// Token: 0x0600125F RID: 4703 RVA: 0x000375FD File Offset: 0x000357FD
		[SecurityCritical]
		internal static void PerformSecurityCheck(object obj, IRuntimeMethodInfo method, RuntimeType parent, uint invocationFlags)
		{
			RuntimeMethodHandle.PerformSecurityCheck(obj, method.Value, parent, invocationFlags);
			GC.KeepAlive(method);
		}

		// Token: 0x06001260 RID: 4704
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SerializationInvoke(IRuntimeMethodInfo method, object target, SerializationInfo info, ref StreamingContext context);

		// Token: 0x06001261 RID: 4705
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool _IsTokenSecurityTransparent(RuntimeModule module, int metaDataToken);

		// Token: 0x06001262 RID: 4706 RVA: 0x00037614 File Offset: 0x00035814
		[SecurityCritical]
		internal static bool IsTokenSecurityTransparent(Module module, int metaDataToken)
		{
			return RuntimeMethodHandle._IsTokenSecurityTransparent(module.ModuleHandle.GetRuntimeModule(), metaDataToken);
		}

		// Token: 0x06001263 RID: 4707
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool _IsSecurityCritical(IRuntimeMethodInfo method);

		// Token: 0x06001264 RID: 4708 RVA: 0x00037635 File Offset: 0x00035835
		[SecuritySafeCritical]
		internal static bool IsSecurityCritical(IRuntimeMethodInfo method)
		{
			return RuntimeMethodHandle._IsSecurityCritical(method);
		}

		// Token: 0x06001265 RID: 4709
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool _IsSecuritySafeCritical(IRuntimeMethodInfo method);

		// Token: 0x06001266 RID: 4710 RVA: 0x0003763D File Offset: 0x0003583D
		[SecuritySafeCritical]
		internal static bool IsSecuritySafeCritical(IRuntimeMethodInfo method)
		{
			return RuntimeMethodHandle._IsSecuritySafeCritical(method);
		}

		// Token: 0x06001267 RID: 4711
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool _IsSecurityTransparent(IRuntimeMethodInfo method);

		// Token: 0x06001268 RID: 4712 RVA: 0x00037645 File Offset: 0x00035845
		[SecuritySafeCritical]
		internal static bool IsSecurityTransparent(IRuntimeMethodInfo method)
		{
			return RuntimeMethodHandle._IsSecurityTransparent(method);
		}

		// Token: 0x06001269 RID: 4713
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetMethodInstantiation(RuntimeMethodHandleInternal method, ObjectHandleOnStack types, bool fAsRuntimeTypeArray);

		// Token: 0x0600126A RID: 4714 RVA: 0x00037650 File Offset: 0x00035850
		[SecuritySafeCritical]
		internal static RuntimeType[] GetMethodInstantiationInternal(IRuntimeMethodInfo method)
		{
			RuntimeType[] result = null;
			RuntimeMethodHandle.GetMethodInstantiation(RuntimeMethodHandle.EnsureNonNullMethodInfo(method).Value, JitHelpers.GetObjectHandleOnStack<RuntimeType[]>(ref result), true);
			GC.KeepAlive(method);
			return result;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00037680 File Offset: 0x00035880
		[SecuritySafeCritical]
		internal static RuntimeType[] GetMethodInstantiationInternal(RuntimeMethodHandleInternal method)
		{
			RuntimeType[] result = null;
			RuntimeMethodHandle.GetMethodInstantiation(method, JitHelpers.GetObjectHandleOnStack<RuntimeType[]>(ref result), true);
			return result;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x000376A0 File Offset: 0x000358A0
		[SecuritySafeCritical]
		internal static Type[] GetMethodInstantiationPublic(IRuntimeMethodInfo method)
		{
			RuntimeType[] result = null;
			RuntimeMethodHandle.GetMethodInstantiation(RuntimeMethodHandle.EnsureNonNullMethodInfo(method).Value, JitHelpers.GetObjectHandleOnStack<RuntimeType[]>(ref result), false);
			GC.KeepAlive(method);
			return result;
		}

		// Token: 0x0600126D RID: 4717
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasMethodInstantiation(RuntimeMethodHandleInternal method);

		// Token: 0x0600126E RID: 4718 RVA: 0x000376D0 File Offset: 0x000358D0
		[SecuritySafeCritical]
		internal static bool HasMethodInstantiation(IRuntimeMethodInfo method)
		{
			bool result = RuntimeMethodHandle.HasMethodInstantiation(method.Value);
			GC.KeepAlive(method);
			return result;
		}

		// Token: 0x0600126F RID: 4719
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeMethodHandleInternal GetStubIfNeeded(RuntimeMethodHandleInternal method, RuntimeType declaringType, RuntimeType[] methodInstantiation);

		// Token: 0x06001270 RID: 4720
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeMethodHandleInternal GetMethodFromCanonical(RuntimeMethodHandleInternal method, RuntimeType declaringType);

		// Token: 0x06001271 RID: 4721
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGenericMethodDefinition(RuntimeMethodHandleInternal method);

		// Token: 0x06001272 RID: 4722 RVA: 0x000376F0 File Offset: 0x000358F0
		[SecuritySafeCritical]
		internal static bool IsGenericMethodDefinition(IRuntimeMethodInfo method)
		{
			bool result = RuntimeMethodHandle.IsGenericMethodDefinition(method.Value);
			GC.KeepAlive(method);
			return result;
		}

		// Token: 0x06001273 RID: 4723
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsTypicalMethodDefinition(IRuntimeMethodInfo method);

		// Token: 0x06001274 RID: 4724
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetTypicalMethodDefinition(IRuntimeMethodInfo method, ObjectHandleOnStack outMethod);

		// Token: 0x06001275 RID: 4725 RVA: 0x00037710 File Offset: 0x00035910
		[SecuritySafeCritical]
		internal static IRuntimeMethodInfo GetTypicalMethodDefinition(IRuntimeMethodInfo method)
		{
			if (!RuntimeMethodHandle.IsTypicalMethodDefinition(method))
			{
				RuntimeMethodHandle.GetTypicalMethodDefinition(method, JitHelpers.GetObjectHandleOnStack<IRuntimeMethodInfo>(ref method));
			}
			return method;
		}

		// Token: 0x06001276 RID: 4726
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void StripMethodInstantiation(IRuntimeMethodInfo method, ObjectHandleOnStack outMethod);

		// Token: 0x06001277 RID: 4727 RVA: 0x00037728 File Offset: 0x00035928
		[SecuritySafeCritical]
		internal static IRuntimeMethodInfo StripMethodInstantiation(IRuntimeMethodInfo method)
		{
			IRuntimeMethodInfo result = method;
			RuntimeMethodHandle.StripMethodInstantiation(method, JitHelpers.GetObjectHandleOnStack<IRuntimeMethodInfo>(ref result));
			return result;
		}

		// Token: 0x06001278 RID: 4728
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsDynamicMethod(RuntimeMethodHandleInternal method);

		// Token: 0x06001279 RID: 4729
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void Destroy(RuntimeMethodHandleInternal method);

		// Token: 0x0600127A RID: 4730
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Resolver GetResolver(RuntimeMethodHandleInternal method);

		// Token: 0x0600127B RID: 4731
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetCallerType(StackCrawlMarkHandle stackMark, ObjectHandleOnStack retType);

		// Token: 0x0600127C RID: 4732 RVA: 0x00037748 File Offset: 0x00035948
		[SecuritySafeCritical]
		internal static RuntimeType GetCallerType(ref StackCrawlMark stackMark)
		{
			RuntimeType result = null;
			RuntimeMethodHandle.GetCallerType(JitHelpers.GetStackCrawlMarkHandle(ref stackMark), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref result));
			return result;
		}

		// Token: 0x0600127D RID: 4733
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MethodBody GetMethodBody(IRuntimeMethodInfo method, RuntimeType declaringType);

		// Token: 0x0600127E RID: 4734
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsConstructor(RuntimeMethodHandleInternal method);

		// Token: 0x0600127F RID: 4735
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern LoaderAllocator GetLoaderAllocator(RuntimeMethodHandleInternal method);

		// Token: 0x0400066B RID: 1643
		private IRuntimeMethodInfo m_value;
	}
}
