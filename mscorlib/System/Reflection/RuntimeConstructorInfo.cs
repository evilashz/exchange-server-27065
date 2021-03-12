using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020005A6 RID: 1446
	[Serializable]
	internal sealed class RuntimeConstructorInfo : ConstructorInfo, ISerializable, IRuntimeMethodInfo
	{
		// Token: 0x060043DA RID: 17370 RVA: 0x000F89A0 File Offset: 0x000F6BA0
		private bool IsNonW8PFrameworkAPI()
		{
			if (this.DeclaringType.IsArray && base.IsPublic && !base.IsStatic)
			{
				return false;
			}
			RuntimeAssembly runtimeAssembly = this.GetRuntimeAssembly();
			if (runtimeAssembly.IsFrameworkAssembly())
			{
				int invocableAttributeCtorToken = runtimeAssembly.InvocableAttributeCtorToken;
				if (System.Reflection.MetadataToken.IsNullToken(invocableAttributeCtorToken) || !CustomAttribute.IsAttributeDefined(this.GetRuntimeModule(), this.MetadataToken, invocableAttributeCtorToken))
				{
					return true;
				}
			}
			return this.GetRuntimeType().IsNonW8PFrameworkAPI();
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060043DB RID: 17371 RVA: 0x000F8A10 File Offset: 0x000F6C10
		internal override bool IsDynamicallyInvokable
		{
			get
			{
				return !AppDomain.ProfileAPICheck || !this.IsNonW8PFrameworkAPI();
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060043DC RID: 17372 RVA: 0x000F8A24 File Offset: 0x000F6C24
		internal INVOCATION_FLAGS InvocationFlags
		{
			[SecuritySafeCritical]
			get
			{
				if ((this.m_invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					INVOCATION_FLAGS invocation_FLAGS = INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR;
					Type declaringType = this.DeclaringType;
					if (declaringType == typeof(void) || (declaringType != null && declaringType.ContainsGenericParameters) || (this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs || (this.Attributes & MethodAttributes.RequireSecObject) == MethodAttributes.RequireSecObject)
					{
						invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE;
					}
					else if (base.IsStatic || (declaringType != null && declaringType.IsAbstract))
					{
						invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NO_CTOR_INVOKE;
					}
					else
					{
						invocation_FLAGS |= RuntimeMethodHandle.GetSecurityFlags(this);
						if ((invocation_FLAGS & INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN && ((this.Attributes & MethodAttributes.MemberAccessMask) != MethodAttributes.Public || (declaringType != null && declaringType.NeedsReflectionSecurityCheck)))
						{
							invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY;
						}
						if (typeof(Delegate).IsAssignableFrom(this.DeclaringType))
						{
							invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_IS_DELEGATE_CTOR;
						}
					}
					if (AppDomain.ProfileAPICheck && this.IsNonW8PFrameworkAPI())
					{
						invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API;
					}
					this.m_invocationFlags = (invocation_FLAGS | INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED);
				}
				return this.m_invocationFlags;
			}
		}

		// Token: 0x060043DD RID: 17373 RVA: 0x000F8B1E File Offset: 0x000F6D1E
		[SecurityCritical]
		internal RuntimeConstructorInfo(RuntimeMethodHandleInternal handle, RuntimeType declaringType, RuntimeType.RuntimeTypeCache reflectedTypeCache, MethodAttributes methodAttributes, BindingFlags bindingFlags)
		{
			this.m_bindingFlags = bindingFlags;
			this.m_reflectedTypeCache = reflectedTypeCache;
			this.m_declaringType = declaringType;
			this.m_handle = handle.Value;
			this.m_methodAttributes = methodAttributes;
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060043DE RID: 17374 RVA: 0x000F8B54 File Offset: 0x000F6D54
		internal RemotingMethodCachedData RemotingCache
		{
			get
			{
				RemotingMethodCachedData remotingMethodCachedData = this.m_cachedData;
				if (remotingMethodCachedData == null)
				{
					remotingMethodCachedData = new RemotingMethodCachedData(this);
					RemotingMethodCachedData remotingMethodCachedData2 = Interlocked.CompareExchange<RemotingMethodCachedData>(ref this.m_cachedData, remotingMethodCachedData, null);
					if (remotingMethodCachedData2 != null)
					{
						remotingMethodCachedData = remotingMethodCachedData2;
					}
				}
				return remotingMethodCachedData;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060043DF RID: 17375 RVA: 0x000F8B86 File Offset: 0x000F6D86
		RuntimeMethodHandleInternal IRuntimeMethodInfo.Value
		{
			[SecuritySafeCritical]
			get
			{
				return new RuntimeMethodHandleInternal(this.m_handle);
			}
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x000F8B94 File Offset: 0x000F6D94
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal override bool CacheEquals(object o)
		{
			RuntimeConstructorInfo runtimeConstructorInfo = o as RuntimeConstructorInfo;
			return runtimeConstructorInfo != null && runtimeConstructorInfo.m_handle == this.m_handle;
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x060043E1 RID: 17377 RVA: 0x000F8BBE File Offset: 0x000F6DBE
		private Signature Signature
		{
			get
			{
				if (this.m_signature == null)
				{
					this.m_signature = new Signature(this, this.m_declaringType);
				}
				return this.m_signature;
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x060043E2 RID: 17378 RVA: 0x000F8BE8 File Offset: 0x000F6DE8
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x000F8BF8 File Offset: 0x000F6DF8
		private void CheckConsistency(object target)
		{
			if (target == null && base.IsStatic)
			{
				return;
			}
			if (this.m_declaringType.IsInstanceOfType(target))
			{
				return;
			}
			if (target == null)
			{
				throw new TargetException(Environment.GetResourceString("RFLCT.Targ_StatMethReqTarg"));
			}
			throw new TargetException(Environment.GetResourceString("RFLCT.Targ_ITargMismatch"));
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x060043E4 RID: 17380 RVA: 0x000F8C44 File Offset: 0x000F6E44
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.m_bindingFlags;
			}
		}

		// Token: 0x060043E5 RID: 17381 RVA: 0x000F8C4C File Offset: 0x000F6E4C
		internal RuntimeMethodHandle GetMethodHandle()
		{
			return new RuntimeMethodHandle(this);
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x060043E6 RID: 17382 RVA: 0x000F8C54 File Offset: 0x000F6E54
		internal bool IsOverloaded
		{
			get
			{
				return this.m_reflectedTypeCache.GetConstructorList(RuntimeType.MemberListType.CaseSensitive, this.Name).Length > 1;
			}
		}

		// Token: 0x060043E7 RID: 17383 RVA: 0x000F8C6D File Offset: 0x000F6E6D
		public override string ToString()
		{
			if (this.m_toString == null)
			{
				this.m_toString = "Void " + base.FormatNameAndSig();
			}
			return this.m_toString;
		}

		// Token: 0x060043E8 RID: 17384 RVA: 0x000F8C93 File Offset: 0x000F6E93
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x060043E9 RID: 17385 RVA: 0x000F8CAC File Offset: 0x000F6EAC
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType);
		}

		// Token: 0x060043EA RID: 17386 RVA: 0x000F8D00 File Offset: 0x000F6F00
		[SecuritySafeCritical]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.IsDefined(this, runtimeType);
		}

		// Token: 0x060043EB RID: 17387 RVA: 0x000F8D52 File Offset: 0x000F6F52
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x060043EC RID: 17388 RVA: 0x000F8D5A File Offset: 0x000F6F5A
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				return RuntimeMethodHandle.GetName(this);
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x060043ED RID: 17389 RVA: 0x000F8D62 File Offset: 0x000F6F62
		[ComVisible(true)]
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Constructor;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x060043EE RID: 17390 RVA: 0x000F8D65 File Offset: 0x000F6F65
		public override Type DeclaringType
		{
			get
			{
				if (!this.m_reflectedTypeCache.IsGlobal)
				{
					return this.m_declaringType;
				}
				return null;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x060043EF RID: 17391 RVA: 0x000F8D7E File Offset: 0x000F6F7E
		public override Type ReflectedType
		{
			get
			{
				if (!this.m_reflectedTypeCache.IsGlobal)
				{
					return this.ReflectedTypeInternal;
				}
				return null;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x060043F0 RID: 17392 RVA: 0x000F8D95 File Offset: 0x000F6F95
		public override int MetadataToken
		{
			[SecuritySafeCritical]
			get
			{
				return RuntimeMethodHandle.GetMethodDef(this);
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x060043F1 RID: 17393 RVA: 0x000F8D9D File Offset: 0x000F6F9D
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x060043F2 RID: 17394 RVA: 0x000F8DA5 File Offset: 0x000F6FA5
		internal RuntimeType GetRuntimeType()
		{
			return this.m_declaringType;
		}

		// Token: 0x060043F3 RID: 17395 RVA: 0x000F8DAF File Offset: 0x000F6FAF
		internal RuntimeModule GetRuntimeModule()
		{
			return RuntimeTypeHandle.GetModule(this.m_declaringType);
		}

		// Token: 0x060043F4 RID: 17396 RVA: 0x000F8DBE File Offset: 0x000F6FBE
		internal RuntimeAssembly GetRuntimeAssembly()
		{
			return this.GetRuntimeModule().GetRuntimeAssembly();
		}

		// Token: 0x060043F5 RID: 17397 RVA: 0x000F8DCB File Offset: 0x000F6FCB
		internal override Type GetReturnType()
		{
			return this.Signature.ReturnType;
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x000F8DD8 File Offset: 0x000F6FD8
		[SecuritySafeCritical]
		internal override ParameterInfo[] GetParametersNoCopy()
		{
			if (this.m_parameters == null)
			{
				this.m_parameters = RuntimeParameterInfo.GetParameters(this, this, this.Signature);
			}
			return this.m_parameters;
		}

		// Token: 0x060043F7 RID: 17399 RVA: 0x000F8DFC File Offset: 0x000F6FFC
		public override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] parametersNoCopy = this.GetParametersNoCopy();
			if (parametersNoCopy.Length == 0)
			{
				return parametersNoCopy;
			}
			ParameterInfo[] array = new ParameterInfo[parametersNoCopy.Length];
			Array.Copy(parametersNoCopy, array, parametersNoCopy.Length);
			return array;
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x000F8E2A File Offset: 0x000F702A
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return RuntimeMethodHandle.GetImplAttributes(this);
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060043F9 RID: 17401 RVA: 0x000F8E34 File Offset: 0x000F7034
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				Type declaringType = this.DeclaringType;
				if ((declaringType == null && this.Module.Assembly.ReflectionOnly) || declaringType is ReflectionOnlyType)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAllowedInReflectionOnly"));
				}
				return new RuntimeMethodHandle(this);
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x060043FA RID: 17402 RVA: 0x000F8E81 File Offset: 0x000F7081
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_methodAttributes;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x060043FB RID: 17403 RVA: 0x000F8E89 File Offset: 0x000F7089
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.Signature.CallingConvention;
			}
		}

		// Token: 0x060043FC RID: 17404 RVA: 0x000F8E98 File Offset: 0x000F7098
		internal static void CheckCanCreateInstance(Type declaringType, bool isVarArg)
		{
			if (declaringType == null)
			{
				throw new ArgumentNullException("declaringType");
			}
			if (declaringType is ReflectionOnlyType)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_ReflectionOnlyInvoke"));
			}
			if (declaringType.IsInterface)
			{
				throw new MemberAccessException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Acc_CreateInterfaceEx"), declaringType));
			}
			if (declaringType.IsAbstract)
			{
				throw new MemberAccessException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Acc_CreateAbstEx"), declaringType));
			}
			if (declaringType.GetRootElementType() == typeof(ArgIterator))
			{
				throw new NotSupportedException();
			}
			if (isVarArg)
			{
				throw new NotSupportedException();
			}
			if (declaringType.ContainsGenericParameters)
			{
				throw new MemberAccessException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Acc_CreateGenericEx"), declaringType));
			}
			if (declaringType == typeof(void))
			{
				throw new MemberAccessException(Environment.GetResourceString("Access_Void"));
			}
		}

		// Token: 0x060043FD RID: 17405 RVA: 0x000F8F82 File Offset: 0x000F7182
		internal void ThrowNoInvokeException()
		{
			RuntimeConstructorInfo.CheckCanCreateInstance(this.DeclaringType, (this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs);
			if ((this.Attributes & MethodAttributes.Static) == MethodAttributes.Static)
			{
				throw new MemberAccessException(Environment.GetResourceString("Acc_NotClassInit"));
			}
			throw new TargetException();
		}

		// Token: 0x060043FE RID: 17406 RVA: 0x000F8FBC File Offset: 0x000F71BC
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
			if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				this.ThrowNoInvokeException();
			}
			this.CheckConsistency(obj);
			if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackCrawlMark);
				if (executingAssembly != null && !executingAssembly.IsSafeForReflection())
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
					{
						base.FullName
					}));
				}
			}
			if (obj != null)
			{
				new SecurityPermission(SecurityPermissionFlag.SkipVerification).Demand();
			}
			if ((invocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY | INVOCATION_FLAGS.INVOCATION_FLAGS_RISKY_METHOD)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_RISKY_METHOD) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					CodeAccessPermission.Demand(PermissionType.ReflectionMemberAccess);
				}
				if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					RuntimeMethodHandle.PerformSecurityCheck(obj, this, this.m_declaringType, (uint)this.m_invocationFlags);
				}
			}
			Signature signature = this.Signature;
			int num = signature.Arguments.Length;
			int num2 = (parameters != null) ? parameters.Length : 0;
			if (num != num2)
			{
				throw new TargetParameterCountException(Environment.GetResourceString("Arg_ParmCnt"));
			}
			if (num2 > 0)
			{
				object[] array = base.CheckArguments(parameters, binder, invokeAttr, culture, signature);
				object result = RuntimeMethodHandle.InvokeMethod(obj, array, signature, false);
				for (int i = 0; i < array.Length; i++)
				{
					parameters[i] = array[i];
				}
				return result;
			}
			return RuntimeMethodHandle.InvokeMethod(obj, null, signature, false);
		}

		// Token: 0x060043FF RID: 17407 RVA: 0x000F90D8 File Offset: 0x000F72D8
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Demand, Flags = ReflectionPermissionFlag.MemberAccess)]
		public override MethodBody GetMethodBody()
		{
			MethodBody methodBody = RuntimeMethodHandle.GetMethodBody(this, this.ReflectedTypeInternal);
			if (methodBody != null)
			{
				methodBody.m_methodBase = this;
			}
			return methodBody;
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06004400 RID: 17408 RVA: 0x000F90FD File Offset: 0x000F72FD
		public override bool IsSecurityCritical
		{
			get
			{
				return RuntimeMethodHandle.IsSecurityCritical(this);
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06004401 RID: 17409 RVA: 0x000F9105 File Offset: 0x000F7305
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return RuntimeMethodHandle.IsSecuritySafeCritical(this);
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06004402 RID: 17410 RVA: 0x000F910D File Offset: 0x000F730D
		public override bool IsSecurityTransparent
		{
			get
			{
				return RuntimeMethodHandle.IsSecurityTransparent(this);
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06004403 RID: 17411 RVA: 0x000F9115 File Offset: 0x000F7315
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.DeclaringType != null && this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x000F9134 File Offset: 0x000F7334
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
			RuntimeTypeHandle typeHandle = this.m_declaringType.TypeHandle;
			if ((invocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE | INVOCATION_FLAGS.INVOCATION_FLAGS_NO_CTOR_INVOKE | INVOCATION_FLAGS.INVOCATION_FLAGS_CONTAINS_STACK_POINTERS)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				this.ThrowNoInvokeException();
			}
			if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackCrawlMark);
				if (executingAssembly != null && !executingAssembly.IsSafeForReflection())
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
					{
						base.FullName
					}));
				}
			}
			if ((invocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY | INVOCATION_FLAGS.INVOCATION_FLAGS_RISKY_METHOD | INVOCATION_FLAGS.INVOCATION_FLAGS_IS_DELEGATE_CTOR)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_RISKY_METHOD) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					CodeAccessPermission.Demand(PermissionType.ReflectionMemberAccess);
				}
				if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					RuntimeMethodHandle.PerformSecurityCheck(null, this, this.m_declaringType, (uint)(this.m_invocationFlags | INVOCATION_FLAGS.INVOCATION_FLAGS_CONSTRUCTOR_INVOKE));
				}
				if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_IS_DELEGATE_CTOR) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
				}
			}
			Signature signature = this.Signature;
			int num = signature.Arguments.Length;
			int num2 = (parameters != null) ? parameters.Length : 0;
			if (num != num2)
			{
				throw new TargetParameterCountException(Environment.GetResourceString("Arg_ParmCnt"));
			}
			if (num2 > 0)
			{
				object[] array = base.CheckArguments(parameters, binder, invokeAttr, culture, signature);
				object result = RuntimeMethodHandle.InvokeMethod(null, array, signature, true);
				for (int i = 0; i < array.Length; i++)
				{
					parameters[i] = array[i];
				}
				return result;
			}
			return RuntimeMethodHandle.InvokeMethod(null, null, signature, true);
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x000F9269 File Offset: 0x000F7469
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Constructor, null);
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x000F9299 File Offset: 0x000F7499
		internal string SerializationToString()
		{
			return this.FormatNameAndSig(true);
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x000F92A2 File Offset: 0x000F74A2
		internal void SerializationInvoke(object target, SerializationInfo info, StreamingContext context)
		{
			RuntimeMethodHandle.SerializationInvoke(this, target, info, ref context);
		}

		// Token: 0x04001B91 RID: 7057
		private volatile RuntimeType m_declaringType;

		// Token: 0x04001B92 RID: 7058
		private RuntimeType.RuntimeTypeCache m_reflectedTypeCache;

		// Token: 0x04001B93 RID: 7059
		private string m_toString;

		// Token: 0x04001B94 RID: 7060
		private ParameterInfo[] m_parameters;

		// Token: 0x04001B95 RID: 7061
		private object _empty1;

		// Token: 0x04001B96 RID: 7062
		private object _empty2;

		// Token: 0x04001B97 RID: 7063
		private object _empty3;

		// Token: 0x04001B98 RID: 7064
		private IntPtr m_handle;

		// Token: 0x04001B99 RID: 7065
		private MethodAttributes m_methodAttributes;

		// Token: 0x04001B9A RID: 7066
		private BindingFlags m_bindingFlags;

		// Token: 0x04001B9B RID: 7067
		private volatile Signature m_signature;

		// Token: 0x04001B9C RID: 7068
		private INVOCATION_FLAGS m_invocationFlags;

		// Token: 0x04001B9D RID: 7069
		private RemotingMethodCachedData m_cachedData;
	}
}
