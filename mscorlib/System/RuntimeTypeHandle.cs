using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x02000130 RID: 304
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct RuntimeTypeHandle : ISerializable
	{
		// Token: 0x060011BB RID: 4539 RVA: 0x00036B2C File Offset: 0x00034D2C
		internal RuntimeTypeHandle GetNativeHandle()
		{
			RuntimeType type = this.m_type;
			if (type == null)
			{
				throw new ArgumentNullException(null, Environment.GetResourceString("Arg_InvalidHandle"));
			}
			return new RuntimeTypeHandle(type);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00036B60 File Offset: 0x00034D60
		internal RuntimeType GetTypeChecked()
		{
			RuntimeType type = this.m_type;
			if (type == null)
			{
				throw new ArgumentNullException(null, Environment.GetResourceString("Arg_InvalidHandle"));
			}
			return type;
		}

		// Token: 0x060011BD RID: 4541
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInstanceOfType(RuntimeType type, object o);

		// Token: 0x060011BE RID: 4542 RVA: 0x00036B90 File Offset: 0x00034D90
		[SecuritySafeCritical]
		internal unsafe static Type GetTypeHelper(Type typeStart, Type[] genericArgs, IntPtr pModifiers, int cModifiers)
		{
			Type type = typeStart;
			if (genericArgs != null)
			{
				type = type.MakeGenericType(genericArgs);
			}
			if (cModifiers > 0)
			{
				int* value = (int*)pModifiers.ToPointer();
				for (int i = 0; i < cModifiers; i++)
				{
					if ((byte)Marshal.ReadInt32((IntPtr)((void*)value), i * 4) == 15)
					{
						type = type.MakePointerType();
					}
					else if ((byte)Marshal.ReadInt32((IntPtr)((void*)value), i * 4) == 16)
					{
						type = type.MakeByRefType();
					}
					else if ((byte)Marshal.ReadInt32((IntPtr)((void*)value), i * 4) == 29)
					{
						type = type.MakeArrayType();
					}
					else
					{
						type = type.MakeArrayType(Marshal.ReadInt32((IntPtr)((void*)value), ++i * 4));
					}
				}
			}
			return type;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00036C33 File Offset: 0x00034E33
		[__DynamicallyInvokable]
		public static bool operator ==(RuntimeTypeHandle left, object right)
		{
			return left.Equals(right);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00036C3D File Offset: 0x00034E3D
		[__DynamicallyInvokable]
		public static bool operator ==(object left, RuntimeTypeHandle right)
		{
			return right.Equals(left);
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00036C47 File Offset: 0x00034E47
		[__DynamicallyInvokable]
		public static bool operator !=(RuntimeTypeHandle left, object right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00036C54 File Offset: 0x00034E54
		[__DynamicallyInvokable]
		public static bool operator !=(object left, RuntimeTypeHandle right)
		{
			return !right.Equals(left);
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00036C61 File Offset: 0x00034E61
		internal static RuntimeTypeHandle EmptyHandle
		{
			get
			{
				return new RuntimeTypeHandle(null);
			}
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00036C69 File Offset: 0x00034E69
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (!(this.m_type != null))
			{
				return 0;
			}
			return this.m_type.GetHashCode();
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00036C88 File Offset: 0x00034E88
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is RuntimeTypeHandle && ((RuntimeTypeHandle)obj).m_type == this.m_type;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00036CB8 File Offset: 0x00034EB8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public bool Equals(RuntimeTypeHandle handle)
		{
			return handle.m_type == this.m_type;
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00036CCC File Offset: 0x00034ECC
		public IntPtr Value
		{
			[SecurityCritical]
			get
			{
				if (!(this.m_type != null))
				{
					return IntPtr.Zero;
				}
				return this.m_type.m_handle;
			}
		}

		// Token: 0x060011C8 RID: 4552
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetValueInternal(RuntimeTypeHandle handle);

		// Token: 0x060011C9 RID: 4553 RVA: 0x00036CED File Offset: 0x00034EED
		internal RuntimeTypeHandle(RuntimeType type)
		{
			this.m_type = type;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00036CF6 File Offset: 0x00034EF6
		internal bool IsNullHandle()
		{
			return this.m_type == null;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00036D04 File Offset: 0x00034F04
		[SecuritySafeCritical]
		internal static bool IsPrimitive(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return (corElementType >= CorElementType.Boolean && corElementType <= CorElementType.R8) || corElementType == CorElementType.I || corElementType == CorElementType.U;
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00036D30 File Offset: 0x00034F30
		[SecuritySafeCritical]
		internal static bool IsByRef(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.ByRef;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00036D4C File Offset: 0x00034F4C
		[SecuritySafeCritical]
		internal static bool IsPointer(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.Ptr;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00036D68 File Offset: 0x00034F68
		[SecuritySafeCritical]
		internal static bool IsArray(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.Array || corElementType == CorElementType.SzArray;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00036D88 File Offset: 0x00034F88
		[SecuritySafeCritical]
		internal static bool IsSzArray(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.SzArray;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00036DA4 File Offset: 0x00034FA4
		[SecuritySafeCritical]
		internal static bool HasElementType(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.Array || corElementType == CorElementType.SzArray || corElementType == CorElementType.Ptr || corElementType == CorElementType.ByRef;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00036DD0 File Offset: 0x00034FD0
		[SecurityCritical]
		internal static IntPtr[] CopyRuntimeTypeHandles(RuntimeTypeHandle[] inHandles, out int length)
		{
			if (inHandles == null || inHandles.Length == 0)
			{
				length = 0;
				return null;
			}
			IntPtr[] array = new IntPtr[inHandles.Length];
			for (int i = 0; i < inHandles.Length; i++)
			{
				array[i] = inHandles[i].Value;
			}
			length = array.Length;
			return array;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00036E18 File Offset: 0x00035018
		[SecurityCritical]
		internal static IntPtr[] CopyRuntimeTypeHandles(Type[] inHandles, out int length)
		{
			if (inHandles == null || inHandles.Length == 0)
			{
				length = 0;
				return null;
			}
			IntPtr[] array = new IntPtr[inHandles.Length];
			for (int i = 0; i < inHandles.Length; i++)
			{
				array[i] = inHandles[i].GetTypeHandleInternal().Value;
			}
			length = array.Length;
			return array;
		}

		// Token: 0x060011D3 RID: 4563
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CreateInstance(RuntimeType type, bool publicOnly, bool noCheck, ref bool canBeCached, ref RuntimeMethodHandleInternal ctor, ref bool bNeedSecurityCheck);

		// Token: 0x060011D4 RID: 4564
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CreateCaInstance(RuntimeType type, IRuntimeMethodInfo ctor);

		// Token: 0x060011D5 RID: 4565
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object Allocate(RuntimeType type);

		// Token: 0x060011D6 RID: 4566
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object CreateInstanceForAnotherGenericParameter(RuntimeType type, RuntimeType genericParameter);

		// Token: 0x060011D7 RID: 4567 RVA: 0x00036E61 File Offset: 0x00035061
		internal RuntimeType GetRuntimeType()
		{
			return this.m_type;
		}

		// Token: 0x060011D8 RID: 4568
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern CorElementType GetCorElementType(RuntimeType type);

		// Token: 0x060011D9 RID: 4569
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeAssembly GetAssembly(RuntimeType type);

		// Token: 0x060011DA RID: 4570
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeModule GetModule(RuntimeType type);

		// Token: 0x060011DB RID: 4571 RVA: 0x00036E69 File Offset: 0x00035069
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public ModuleHandle GetModuleHandle()
		{
			return new ModuleHandle(RuntimeTypeHandle.GetModule(this.m_type));
		}

		// Token: 0x060011DC RID: 4572
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetBaseType(RuntimeType type);

		// Token: 0x060011DD RID: 4573
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern TypeAttributes GetAttributes(RuntimeType type);

		// Token: 0x060011DE RID: 4574
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetElementType(RuntimeType type);

		// Token: 0x060011DF RID: 4575
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CompareCanonicalHandles(RuntimeType left, RuntimeType right);

		// Token: 0x060011E0 RID: 4576
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetArrayRank(RuntimeType type);

		// Token: 0x060011E1 RID: 4577
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetToken(RuntimeType type);

		// Token: 0x060011E2 RID: 4578
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeMethodHandleInternal GetMethodAt(RuntimeType type, int slot);

		// Token: 0x060011E3 RID: 4579 RVA: 0x00036E7B File Offset: 0x0003507B
		internal static RuntimeTypeHandle.IntroducedMethodEnumerator GetIntroducedMethods(RuntimeType type)
		{
			return new RuntimeTypeHandle.IntroducedMethodEnumerator(type);
		}

		// Token: 0x060011E4 RID: 4580
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RuntimeMethodHandleInternal GetFirstIntroducedMethod(RuntimeType type);

		// Token: 0x060011E5 RID: 4581
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNextIntroducedMethod(ref RuntimeMethodHandleInternal method);

		// Token: 0x060011E6 RID: 4582
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool GetFields(RuntimeType type, IntPtr* result, int* count);

		// Token: 0x060011E7 RID: 4583
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type[] GetInterfaces(RuntimeType type);

		// Token: 0x060011E8 RID: 4584
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetConstraints(RuntimeTypeHandle handle, ObjectHandleOnStack types);

		// Token: 0x060011E9 RID: 4585 RVA: 0x00036E84 File Offset: 0x00035084
		[SecuritySafeCritical]
		internal Type[] GetConstraints()
		{
			Type[] result = null;
			RuntimeTypeHandle.GetConstraints(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Type[]>(ref result));
			return result;
		}

		// Token: 0x060011EA RID: 4586
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr GetGCHandle(RuntimeTypeHandle handle, GCHandleType type);

		// Token: 0x060011EB RID: 4587 RVA: 0x00036EA6 File Offset: 0x000350A6
		[SecurityCritical]
		internal IntPtr GetGCHandle(GCHandleType type)
		{
			return RuntimeTypeHandle.GetGCHandle(this.GetNativeHandle(), type);
		}

		// Token: 0x060011EC RID: 4588
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetNumVirtuals(RuntimeType type);

		// Token: 0x060011ED RID: 4589
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void VerifyInterfaceIsImplemented(RuntimeTypeHandle handle, RuntimeTypeHandle interfaceHandle);

		// Token: 0x060011EE RID: 4590 RVA: 0x00036EB4 File Offset: 0x000350B4
		[SecuritySafeCritical]
		internal void VerifyInterfaceIsImplemented(RuntimeTypeHandle interfaceHandle)
		{
			RuntimeTypeHandle.VerifyInterfaceIsImplemented(this.GetNativeHandle(), interfaceHandle.GetNativeHandle());
		}

		// Token: 0x060011EF RID: 4591
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetInterfaceMethodImplementationSlot(RuntimeTypeHandle handle, RuntimeTypeHandle interfaceHandle, RuntimeMethodHandleInternal interfaceMethodHandle);

		// Token: 0x060011F0 RID: 4592 RVA: 0x00036EC8 File Offset: 0x000350C8
		[SecuritySafeCritical]
		internal int GetInterfaceMethodImplementationSlot(RuntimeTypeHandle interfaceHandle, RuntimeMethodHandleInternal interfaceMethodHandle)
		{
			return RuntimeTypeHandle.GetInterfaceMethodImplementationSlot(this.GetNativeHandle(), interfaceHandle.GetNativeHandle(), interfaceMethodHandle);
		}

		// Token: 0x060011F1 RID: 4593
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsZapped(RuntimeType type);

		// Token: 0x060011F2 RID: 4594
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsDoNotForceOrderOfConstructorsSet();

		// Token: 0x060011F3 RID: 4595
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsComObject(RuntimeType type, bool isGenericCOM);

		// Token: 0x060011F4 RID: 4596
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsContextful(RuntimeType type);

		// Token: 0x060011F5 RID: 4597
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInterface(RuntimeType type);

		// Token: 0x060011F6 RID: 4598
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool _IsVisible(RuntimeTypeHandle typeHandle);

		// Token: 0x060011F7 RID: 4599 RVA: 0x00036EDD File Offset: 0x000350DD
		[SecuritySafeCritical]
		internal static bool IsVisible(RuntimeType type)
		{
			return RuntimeTypeHandle._IsVisible(new RuntimeTypeHandle(type));
		}

		// Token: 0x060011F8 RID: 4600
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsSecurityCritical(RuntimeTypeHandle typeHandle);

		// Token: 0x060011F9 RID: 4601 RVA: 0x00036EEA File Offset: 0x000350EA
		[SecuritySafeCritical]
		internal bool IsSecurityCritical()
		{
			return RuntimeTypeHandle.IsSecurityCritical(this.GetNativeHandle());
		}

		// Token: 0x060011FA RID: 4602
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsSecuritySafeCritical(RuntimeTypeHandle typeHandle);

		// Token: 0x060011FB RID: 4603 RVA: 0x00036EF7 File Offset: 0x000350F7
		[SecuritySafeCritical]
		internal bool IsSecuritySafeCritical()
		{
			return RuntimeTypeHandle.IsSecuritySafeCritical(this.GetNativeHandle());
		}

		// Token: 0x060011FC RID: 4604
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsSecurityTransparent(RuntimeTypeHandle typeHandle);

		// Token: 0x060011FD RID: 4605 RVA: 0x00036F04 File Offset: 0x00035104
		[SecuritySafeCritical]
		internal bool IsSecurityTransparent()
		{
			return RuntimeTypeHandle.IsSecurityTransparent(this.GetNativeHandle());
		}

		// Token: 0x060011FE RID: 4606
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasProxyAttribute(RuntimeType type);

		// Token: 0x060011FF RID: 4607
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsValueType(RuntimeType type);

		// Token: 0x06001200 RID: 4608
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void ConstructName(RuntimeTypeHandle handle, TypeNameFormatFlags formatFlags, StringHandleOnStack retString);

		// Token: 0x06001201 RID: 4609 RVA: 0x00036F14 File Offset: 0x00035114
		[SecuritySafeCritical]
		internal string ConstructName(TypeNameFormatFlags formatFlags)
		{
			string result = null;
			RuntimeTypeHandle.ConstructName(this.GetNativeHandle(), formatFlags, JitHelpers.GetStringHandleOnStack(ref result));
			return result;
		}

		// Token: 0x06001202 RID: 4610
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* _GetUtf8Name(RuntimeType type);

		// Token: 0x06001203 RID: 4611 RVA: 0x00036F37 File Offset: 0x00035137
		[SecuritySafeCritical]
		internal static Utf8String GetUtf8Name(RuntimeType type)
		{
			return new Utf8String(RuntimeTypeHandle._GetUtf8Name(type));
		}

		// Token: 0x06001204 RID: 4612
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CanCastTo(RuntimeType type, RuntimeType target);

		// Token: 0x06001205 RID: 4613
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetDeclaringType(RuntimeType type);

		// Token: 0x06001206 RID: 4614
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IRuntimeMethodInfo GetDeclaringMethod(RuntimeType type);

		// Token: 0x06001207 RID: 4615
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetDefaultConstructor(RuntimeTypeHandle handle, ObjectHandleOnStack method);

		// Token: 0x06001208 RID: 4616 RVA: 0x00036F44 File Offset: 0x00035144
		[SecuritySafeCritical]
		internal IRuntimeMethodInfo GetDefaultConstructor()
		{
			IRuntimeMethodInfo result = null;
			RuntimeTypeHandle.GetDefaultConstructor(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<IRuntimeMethodInfo>(ref result));
			return result;
		}

		// Token: 0x06001209 RID: 4617
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetTypeByName(string name, bool throwOnError, bool ignoreCase, bool reflectionOnly, StackCrawlMarkHandle stackMark, IntPtr pPrivHostBinder, bool loadTypeFromPartialName, ObjectHandleOnStack type);

		// Token: 0x0600120A RID: 4618 RVA: 0x00036F66 File Offset: 0x00035166
		internal static RuntimeType GetTypeByName(string name, bool throwOnError, bool ignoreCase, bool reflectionOnly, ref StackCrawlMark stackMark, bool loadTypeFromPartialName)
		{
			return RuntimeTypeHandle.GetTypeByName(name, throwOnError, ignoreCase, reflectionOnly, ref stackMark, IntPtr.Zero, loadTypeFromPartialName);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00036F7C File Offset: 0x0003517C
		[SecuritySafeCritical]
		internal static RuntimeType GetTypeByName(string name, bool throwOnError, bool ignoreCase, bool reflectionOnly, ref StackCrawlMark stackMark, IntPtr pPrivHostBinder, bool loadTypeFromPartialName)
		{
			if (name != null && name.Length != 0)
			{
				RuntimeType result = null;
				RuntimeTypeHandle.GetTypeByName(name, throwOnError, ignoreCase, reflectionOnly, JitHelpers.GetStackCrawlMarkHandle(ref stackMark), pPrivHostBinder, loadTypeFromPartialName, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref result));
				return result;
			}
			if (throwOnError)
			{
				throw new TypeLoadException(Environment.GetResourceString("Arg_TypeLoadNullStr"));
			}
			return null;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00036FC7 File Offset: 0x000351C7
		internal static Type GetTypeByName(string name, ref StackCrawlMark stackMark)
		{
			return RuntimeTypeHandle.GetTypeByName(name, false, false, false, ref stackMark, false);
		}

		// Token: 0x0600120D RID: 4621
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetTypeByNameUsingCARules(string name, RuntimeModule scope, ObjectHandleOnStack type);

		// Token: 0x0600120E RID: 4622 RVA: 0x00036FD4 File Offset: 0x000351D4
		[SecuritySafeCritical]
		internal static RuntimeType GetTypeByNameUsingCARules(string name, RuntimeModule scope)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException("name");
			}
			RuntimeType result = null;
			RuntimeTypeHandle.GetTypeByNameUsingCARules(name, scope.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref result));
			return result;
		}

		// Token: 0x0600120F RID: 4623
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void GetInstantiation(RuntimeTypeHandle type, ObjectHandleOnStack types, bool fAsRuntimeTypeArray);

		// Token: 0x06001210 RID: 4624 RVA: 0x00037010 File Offset: 0x00035210
		[SecuritySafeCritical]
		internal RuntimeType[] GetInstantiationInternal()
		{
			RuntimeType[] result = null;
			RuntimeTypeHandle.GetInstantiation(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType[]>(ref result), true);
			return result;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00037034 File Offset: 0x00035234
		[SecuritySafeCritical]
		internal Type[] GetInstantiationPublic()
		{
			Type[] result = null;
			RuntimeTypeHandle.GetInstantiation(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Type[]>(ref result), false);
			return result;
		}

		// Token: 0x06001212 RID: 4626
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern void Instantiate(RuntimeTypeHandle handle, IntPtr* pInst, int numGenericArgs, ObjectHandleOnStack type);

		// Token: 0x06001213 RID: 4627 RVA: 0x00037058 File Offset: 0x00035258
		[SecurityCritical]
		internal unsafe RuntimeType Instantiate(Type[] inst)
		{
			int numGenericArgs;
			IntPtr[] array = RuntimeTypeHandle.CopyRuntimeTypeHandles(inst, out numGenericArgs);
			fixed (IntPtr* ptr = array)
			{
				RuntimeType result = null;
				RuntimeTypeHandle.Instantiate(this.GetNativeHandle(), ptr, numGenericArgs, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref result));
				GC.KeepAlive(inst);
				return result;
			}
		}

		// Token: 0x06001214 RID: 4628
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void MakeArray(RuntimeTypeHandle handle, int rank, ObjectHandleOnStack type);

		// Token: 0x06001215 RID: 4629 RVA: 0x000370A8 File Offset: 0x000352A8
		[SecuritySafeCritical]
		internal RuntimeType MakeArray(int rank)
		{
			RuntimeType result = null;
			RuntimeTypeHandle.MakeArray(this.GetNativeHandle(), rank, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref result));
			return result;
		}

		// Token: 0x06001216 RID: 4630
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void MakeSZArray(RuntimeTypeHandle handle, ObjectHandleOnStack type);

		// Token: 0x06001217 RID: 4631 RVA: 0x000370CC File Offset: 0x000352CC
		[SecuritySafeCritical]
		internal RuntimeType MakeSZArray()
		{
			RuntimeType result = null;
			RuntimeTypeHandle.MakeSZArray(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref result));
			return result;
		}

		// Token: 0x06001218 RID: 4632
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void MakeByRef(RuntimeTypeHandle handle, ObjectHandleOnStack type);

		// Token: 0x06001219 RID: 4633 RVA: 0x000370F0 File Offset: 0x000352F0
		[SecuritySafeCritical]
		internal RuntimeType MakeByRef()
		{
			RuntimeType result = null;
			RuntimeTypeHandle.MakeByRef(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref result));
			return result;
		}

		// Token: 0x0600121A RID: 4634
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void MakePointer(RuntimeTypeHandle handle, ObjectHandleOnStack type);

		// Token: 0x0600121B RID: 4635 RVA: 0x00037114 File Offset: 0x00035314
		[SecurityCritical]
		internal RuntimeType MakePointer()
		{
			RuntimeType result = null;
			RuntimeTypeHandle.MakePointer(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref result));
			return result;
		}

		// Token: 0x0600121C RID: 4636
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool IsCollectible(RuntimeTypeHandle handle);

		// Token: 0x0600121D RID: 4637
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasInstantiation(RuntimeType type);

		// Token: 0x0600121E RID: 4638 RVA: 0x00037136 File Offset: 0x00035336
		internal bool HasInstantiation()
		{
			return RuntimeTypeHandle.HasInstantiation(this.GetTypeChecked());
		}

		// Token: 0x0600121F RID: 4639
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetGenericTypeDefinition(RuntimeTypeHandle type, ObjectHandleOnStack retType);

		// Token: 0x06001220 RID: 4640 RVA: 0x00037144 File Offset: 0x00035344
		[SecuritySafeCritical]
		internal static RuntimeType GetGenericTypeDefinition(RuntimeType type)
		{
			RuntimeType runtimeType = type;
			if (RuntimeTypeHandle.HasInstantiation(runtimeType) && !RuntimeTypeHandle.IsGenericTypeDefinition(runtimeType))
			{
				RuntimeTypeHandle.GetGenericTypeDefinition(runtimeType.GetTypeHandleInternal(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref runtimeType));
			}
			return runtimeType;
		}

		// Token: 0x06001221 RID: 4641
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGenericTypeDefinition(RuntimeType type);

		// Token: 0x06001222 RID: 4642
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGenericVariable(RuntimeType type);

		// Token: 0x06001223 RID: 4643 RVA: 0x00037176 File Offset: 0x00035376
		internal bool IsGenericVariable()
		{
			return RuntimeTypeHandle.IsGenericVariable(this.GetTypeChecked());
		}

		// Token: 0x06001224 RID: 4644
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGenericVariableIndex(RuntimeType type);

		// Token: 0x06001225 RID: 4645 RVA: 0x00037184 File Offset: 0x00035384
		[SecuritySafeCritical]
		internal int GetGenericVariableIndex()
		{
			RuntimeType typeChecked = this.GetTypeChecked();
			if (!RuntimeTypeHandle.IsGenericVariable(typeChecked))
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericParameter"));
			}
			return RuntimeTypeHandle.GetGenericVariableIndex(typeChecked);
		}

		// Token: 0x06001226 RID: 4646
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ContainsGenericVariables(RuntimeType handle);

		// Token: 0x06001227 RID: 4647 RVA: 0x000371B6 File Offset: 0x000353B6
		[SecuritySafeCritical]
		internal bool ContainsGenericVariables()
		{
			return RuntimeTypeHandle.ContainsGenericVariables(this.GetTypeChecked());
		}

		// Token: 0x06001228 RID: 4648
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool SatisfiesConstraints(RuntimeType paramType, IntPtr* pTypeContext, int typeContextLength, IntPtr* pMethodContext, int methodContextLength, RuntimeType toType);

		// Token: 0x06001229 RID: 4649 RVA: 0x000371C4 File Offset: 0x000353C4
		[SecurityCritical]
		internal unsafe static bool SatisfiesConstraints(RuntimeType paramType, RuntimeType[] typeContext, RuntimeType[] methodContext, RuntimeType toType)
		{
			int typeContextLength;
			IntPtr[] array = RuntimeTypeHandle.CopyRuntimeTypeHandles(typeContext, out typeContextLength);
			int methodContextLength;
			IntPtr[] array2 = RuntimeTypeHandle.CopyRuntimeTypeHandles(methodContext, out methodContextLength);
			fixed (IntPtr* ptr = array)
			{
				fixed (IntPtr* ptr2 = array2)
				{
					bool result = RuntimeTypeHandle.SatisfiesConstraints(paramType, ptr, typeContextLength, ptr2, methodContextLength, toType);
					GC.KeepAlive(typeContext);
					GC.KeepAlive(methodContext);
					return result;
				}
			}
		}

		// Token: 0x0600122A RID: 4650
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr _GetMetadataImport(RuntimeType type);

		// Token: 0x0600122B RID: 4651 RVA: 0x0003723A File Offset: 0x0003543A
		[SecurityCritical]
		internal static MetadataImport GetMetadataImport(RuntimeType type)
		{
			return new MetadataImport(RuntimeTypeHandle._GetMetadataImport(type), type);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00037248 File Offset: 0x00035448
		[SecurityCritical]
		private RuntimeTypeHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			RuntimeType type = (RuntimeType)info.GetValue("TypeObj", typeof(RuntimeType));
			this.m_type = type;
			if (this.m_type == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
			}
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x000372A4 File Offset: 0x000354A4
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.m_type == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFieldState"));
			}
			info.AddValue("TypeObj", this.m_type, typeof(RuntimeType));
		}

		// Token: 0x0600122E RID: 4654
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsEquivalentTo(RuntimeType rtType1, RuntimeType rtType2);

		// Token: 0x0600122F RID: 4655
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsEquivalentType(RuntimeType type);

		// Token: 0x0400065F RID: 1631
		private RuntimeType m_type;

		// Token: 0x02000ACC RID: 2764
		internal struct IntroducedMethodEnumerator
		{
			// Token: 0x06006925 RID: 26917 RVA: 0x00169237 File Offset: 0x00167437
			[SecuritySafeCritical]
			internal IntroducedMethodEnumerator(RuntimeType type)
			{
				this._handle = RuntimeTypeHandle.GetFirstIntroducedMethod(type);
				this._firstCall = true;
			}

			// Token: 0x06006926 RID: 26918 RVA: 0x0016924C File Offset: 0x0016744C
			[SecuritySafeCritical]
			public bool MoveNext()
			{
				if (this._firstCall)
				{
					this._firstCall = false;
				}
				else if (this._handle.Value != IntPtr.Zero)
				{
					RuntimeTypeHandle.GetNextIntroducedMethod(ref this._handle);
				}
				return !(this._handle.Value == IntPtr.Zero);
			}

			// Token: 0x170011E0 RID: 4576
			// (get) Token: 0x06006927 RID: 26919 RVA: 0x001692A4 File Offset: 0x001674A4
			public RuntimeMethodHandleInternal Current
			{
				get
				{
					return this._handle;
				}
			}

			// Token: 0x06006928 RID: 26920 RVA: 0x001692AC File Offset: 0x001674AC
			public RuntimeTypeHandle.IntroducedMethodEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x04003113 RID: 12563
			private bool _firstCall;

			// Token: 0x04003114 RID: 12564
			private RuntimeMethodHandleInternal _handle;
		}
	}
}
