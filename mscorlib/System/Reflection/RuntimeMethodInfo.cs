using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020005DC RID: 1500
	[Serializable]
	internal sealed class RuntimeMethodInfo : MethodInfo, ISerializable, IRuntimeMethodInfo
	{
		// Token: 0x06004626 RID: 17958 RVA: 0x000FF2AC File Offset: 0x000FD4AC
		private bool IsNonW8PFrameworkAPI()
		{
			if (this.m_declaringType.IsArray && base.IsPublic && !base.IsStatic)
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
			if (this.GetRuntimeType().IsNonW8PFrameworkAPI())
			{
				return true;
			}
			if (this.IsGenericMethod && !this.IsGenericMethodDefinition)
			{
				foreach (Type type in this.GetGenericArguments())
				{
					if (((RuntimeType)type).IsNonW8PFrameworkAPI())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06004627 RID: 17959 RVA: 0x000FF356 File Offset: 0x000FD556
		internal override bool IsDynamicallyInvokable
		{
			get
			{
				return !AppDomain.ProfileAPICheck || !this.IsNonW8PFrameworkAPI();
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06004628 RID: 17960 RVA: 0x000FF36C File Offset: 0x000FD56C
		internal INVOCATION_FLAGS InvocationFlags
		{
			[SecuritySafeCritical]
			get
			{
				if ((this.m_invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					Type declaringType = this.DeclaringType;
					INVOCATION_FLAGS invocation_FLAGS;
					if (this.ContainsGenericParameters || this.ReturnType.IsByRef || (declaringType != null && declaringType.ContainsGenericParameters) || (this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs || (this.Attributes & MethodAttributes.RequireSecObject) == MethodAttributes.RequireSecObject)
					{
						invocation_FLAGS = INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE;
					}
					else
					{
						invocation_FLAGS = RuntimeMethodHandle.GetSecurityFlags(this);
						if ((invocation_FLAGS & INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
						{
							if ((this.Attributes & MethodAttributes.MemberAccessMask) != MethodAttributes.Public || (declaringType != null && declaringType.NeedsReflectionSecurityCheck))
							{
								invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY;
							}
							else if (this.IsGenericMethod)
							{
								Type[] genericArguments = this.GetGenericArguments();
								for (int i = 0; i < genericArguments.Length; i++)
								{
									if (genericArguments[i].NeedsReflectionSecurityCheck)
									{
										invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY;
										break;
									}
								}
							}
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

		// Token: 0x06004629 RID: 17961 RVA: 0x000FF455 File Offset: 0x000FD655
		[SecurityCritical]
		internal RuntimeMethodInfo(RuntimeMethodHandleInternal handle, RuntimeType declaringType, RuntimeType.RuntimeTypeCache reflectedTypeCache, MethodAttributes methodAttributes, BindingFlags bindingFlags, object keepalive)
		{
			this.m_bindingFlags = bindingFlags;
			this.m_declaringType = declaringType;
			this.m_keepalive = keepalive;
			this.m_handle = handle.Value;
			this.m_reflectedTypeCache = reflectedTypeCache;
			this.m_methodAttributes = methodAttributes;
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x0600462A RID: 17962 RVA: 0x000FF490 File Offset: 0x000FD690
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

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x0600462B RID: 17963 RVA: 0x000FF4C2 File Offset: 0x000FD6C2
		RuntimeMethodHandleInternal IRuntimeMethodInfo.Value
		{
			[SecuritySafeCritical]
			get
			{
				return new RuntimeMethodHandleInternal(this.m_handle);
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x0600462C RID: 17964 RVA: 0x000FF4CF File Offset: 0x000FD6CF
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x000FF4DC File Offset: 0x000FD6DC
		[SecurityCritical]
		private ParameterInfo[] FetchNonReturnParameters()
		{
			if (this.m_parameters == null)
			{
				this.m_parameters = RuntimeParameterInfo.GetParameters(this, this, this.Signature);
			}
			return this.m_parameters;
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x000FF4FF File Offset: 0x000FD6FF
		[SecurityCritical]
		private ParameterInfo FetchReturnParameter()
		{
			if (this.m_returnParameter == null)
			{
				this.m_returnParameter = RuntimeParameterInfo.GetReturnParameter(this, this, this.Signature);
			}
			return this.m_returnParameter;
		}

		// Token: 0x0600462F RID: 17967 RVA: 0x000FF524 File Offset: 0x000FD724
		internal override string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.Name);
			TypeNameFormatFlags format = serialization ? TypeNameFormatFlags.FormatSerialization : TypeNameFormatFlags.FormatBasic;
			if (this.IsGenericMethod)
			{
				stringBuilder.Append(RuntimeMethodHandle.ConstructInstantiation(this, format));
			}
			stringBuilder.Append("(");
			stringBuilder.Append(MethodBase.ConstructParameters(this.GetParameterTypes(), this.CallingConvention, serialization));
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x000FF598 File Offset: 0x000FD798
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal override bool CacheEquals(object o)
		{
			RuntimeMethodInfo runtimeMethodInfo = o as RuntimeMethodInfo;
			return runtimeMethodInfo != null && runtimeMethodInfo.m_handle == this.m_handle;
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06004631 RID: 17969 RVA: 0x000FF5C2 File Offset: 0x000FD7C2
		internal Signature Signature
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

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06004632 RID: 17970 RVA: 0x000FF5E4 File Offset: 0x000FD7E4
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.m_bindingFlags;
			}
		}

		// Token: 0x06004633 RID: 17971 RVA: 0x000FF5EC File Offset: 0x000FD7EC
		internal RuntimeMethodHandle GetMethodHandle()
		{
			return new RuntimeMethodHandle(this);
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x000FF5F4 File Offset: 0x000FD7F4
		[SecuritySafeCritical]
		internal RuntimeMethodInfo GetParentDefinition()
		{
			if (!base.IsVirtual || this.m_declaringType.IsInterface)
			{
				return null;
			}
			RuntimeType runtimeType = (RuntimeType)this.m_declaringType.BaseType;
			if (runtimeType == null)
			{
				return null;
			}
			int slot = RuntimeMethodHandle.GetSlot(this);
			if (RuntimeTypeHandle.GetNumVirtuals(runtimeType) <= slot)
			{
				return null;
			}
			return (RuntimeMethodInfo)RuntimeType.GetMethodBase(runtimeType, RuntimeTypeHandle.GetMethodAt(runtimeType, slot));
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x000FF658 File Offset: 0x000FD858
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return this.m_declaringType;
		}

		// Token: 0x06004636 RID: 17974 RVA: 0x000FF660 File Offset: 0x000FD860
		public override string ToString()
		{
			if (this.m_toString == null)
			{
				this.m_toString = this.ReturnType.FormatTypeName() + " " + base.FormatNameAndSig();
			}
			return this.m_toString;
		}

		// Token: 0x06004637 RID: 17975 RVA: 0x000FF691 File Offset: 0x000FD891
		public override int GetHashCode()
		{
			if (this.IsGenericMethod)
			{
				return ValueType.GetHashCodeOfPtr(this.m_handle);
			}
			return base.GetHashCode();
		}

		// Token: 0x06004638 RID: 17976 RVA: 0x000FF6B0 File Offset: 0x000FD8B0
		[SecuritySafeCritical]
		public override bool Equals(object obj)
		{
			if (!this.IsGenericMethod)
			{
				return obj == this;
			}
			RuntimeMethodInfo runtimeMethodInfo = obj as RuntimeMethodInfo;
			if (runtimeMethodInfo == null || !runtimeMethodInfo.IsGenericMethod)
			{
				return false;
			}
			IRuntimeMethodInfo runtimeMethodInfo2 = RuntimeMethodHandle.StripMethodInstantiation(this);
			IRuntimeMethodInfo runtimeMethodInfo3 = RuntimeMethodHandle.StripMethodInstantiation(runtimeMethodInfo);
			if (runtimeMethodInfo2.Value.Value != runtimeMethodInfo3.Value.Value)
			{
				return false;
			}
			Type[] genericArguments = this.GetGenericArguments();
			Type[] genericArguments2 = runtimeMethodInfo.GetGenericArguments();
			if (genericArguments.Length != genericArguments2.Length)
			{
				return false;
			}
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (genericArguments[i] != genericArguments2[i])
				{
					return false;
				}
			}
			return !(this.DeclaringType != runtimeMethodInfo.DeclaringType) && !(this.ReflectedType != runtimeMethodInfo.ReflectedType);
		}

		// Token: 0x06004639 RID: 17977 RVA: 0x000FF782 File Offset: 0x000FD982
		[SecuritySafeCritical]
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType, inherit);
		}

		// Token: 0x0600463A RID: 17978 RVA: 0x000FF79C File Offset: 0x000FD99C
		[SecuritySafeCritical]
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
			return CustomAttribute.GetCustomAttributes(this, runtimeType, inherit);
		}

		// Token: 0x0600463B RID: 17979 RVA: 0x000FF7F0 File Offset: 0x000FD9F0
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
			return CustomAttribute.IsDefined(this, runtimeType, inherit);
		}

		// Token: 0x0600463C RID: 17980 RVA: 0x000FF843 File Offset: 0x000FDA43
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x0600463D RID: 17981 RVA: 0x000FF84B File Offset: 0x000FDA4B
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_name == null)
				{
					this.m_name = RuntimeMethodHandle.GetName(this);
				}
				return this.m_name;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x0600463E RID: 17982 RVA: 0x000FF867 File Offset: 0x000FDA67
		public override Type DeclaringType
		{
			get
			{
				if (this.m_reflectedTypeCache.IsGlobal)
				{
					return null;
				}
				return this.m_declaringType;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x0600463F RID: 17983 RVA: 0x000FF87E File Offset: 0x000FDA7E
		public override Type ReflectedType
		{
			get
			{
				if (this.m_reflectedTypeCache.IsGlobal)
				{
					return null;
				}
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06004640 RID: 17984 RVA: 0x000FF89A File Offset: 0x000FDA9A
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Method;
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06004641 RID: 17985 RVA: 0x000FF89D File Offset: 0x000FDA9D
		public override int MetadataToken
		{
			[SecuritySafeCritical]
			get
			{
				return RuntimeMethodHandle.GetMethodDef(this);
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06004642 RID: 17986 RVA: 0x000FF8A5 File Offset: 0x000FDAA5
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x000FF8AD File Offset: 0x000FDAAD
		internal RuntimeType GetRuntimeType()
		{
			return this.m_declaringType;
		}

		// Token: 0x06004644 RID: 17988 RVA: 0x000FF8B5 File Offset: 0x000FDAB5
		internal RuntimeModule GetRuntimeModule()
		{
			return this.m_declaringType.GetRuntimeModule();
		}

		// Token: 0x06004645 RID: 17989 RVA: 0x000FF8C2 File Offset: 0x000FDAC2
		internal RuntimeAssembly GetRuntimeAssembly()
		{
			return this.GetRuntimeModule().GetRuntimeAssembly();
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06004646 RID: 17990 RVA: 0x000FF8CF File Offset: 0x000FDACF
		public override bool IsSecurityCritical
		{
			get
			{
				return RuntimeMethodHandle.IsSecurityCritical(this);
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06004647 RID: 17991 RVA: 0x000FF8D7 File Offset: 0x000FDAD7
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return RuntimeMethodHandle.IsSecuritySafeCritical(this);
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06004648 RID: 17992 RVA: 0x000FF8DF File Offset: 0x000FDADF
		public override bool IsSecurityTransparent
		{
			get
			{
				return RuntimeMethodHandle.IsSecurityTransparent(this);
			}
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x000FF8E7 File Offset: 0x000FDAE7
		[SecuritySafeCritical]
		internal override ParameterInfo[] GetParametersNoCopy()
		{
			this.FetchNonReturnParameters();
			return this.m_parameters;
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x000FF8F8 File Offset: 0x000FDAF8
		[SecuritySafeCritical]
		public override ParameterInfo[] GetParameters()
		{
			this.FetchNonReturnParameters();
			if (this.m_parameters.Length == 0)
			{
				return this.m_parameters;
			}
			ParameterInfo[] array = new ParameterInfo[this.m_parameters.Length];
			Array.Copy(this.m_parameters, array, this.m_parameters.Length);
			return array;
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x000FF93F File Offset: 0x000FDB3F
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return RuntimeMethodHandle.GetImplAttributes(this);
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x0600464C RID: 17996 RVA: 0x000FF947 File Offset: 0x000FDB47
		internal bool IsOverloaded
		{
			get
			{
				return this.m_reflectedTypeCache.GetMethodList(RuntimeType.MemberListType.CaseSensitive, this.Name).Length > 1;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x0600464D RID: 17997 RVA: 0x000FF960 File Offset: 0x000FDB60
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

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x0600464E RID: 17998 RVA: 0x000FF9AD File Offset: 0x000FDBAD
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_methodAttributes;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x0600464F RID: 17999 RVA: 0x000FF9B5 File Offset: 0x000FDBB5
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.Signature.CallingConvention;
			}
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x000FF9C4 File Offset: 0x000FDBC4
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

		// Token: 0x06004651 RID: 18001 RVA: 0x000FF9E9 File Offset: 0x000FDBE9
		private void CheckConsistency(object target)
		{
			if ((this.m_methodAttributes & MethodAttributes.Static) == MethodAttributes.Static || this.m_declaringType.IsInstanceOfType(target))
			{
				return;
			}
			if (target == null)
			{
				throw new TargetException(Environment.GetResourceString("RFLCT.Targ_StatMethReqTarg"));
			}
			throw new TargetException(Environment.GetResourceString("RFLCT.Targ_ITargMismatch"));
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x000FFA2C File Offset: 0x000FDC2C
		[SecuritySafeCritical]
		private void ThrowNoInvokeException()
		{
			Type declaringType = this.DeclaringType;
			if ((declaringType == null && this.Module.Assembly.ReflectionOnly) || declaringType is ReflectionOnlyType)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_ReflectionOnlyInvoke"));
			}
			if ((this.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_CONTAINS_STACK_POINTERS) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				throw new NotSupportedException();
			}
			if ((this.CallingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
			{
				throw new NotSupportedException();
			}
			if (this.DeclaringType.ContainsGenericParameters || this.ContainsGenericParameters)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_UnboundGenParam"));
			}
			if (base.IsAbstract)
			{
				throw new MemberAccessException();
			}
			if (this.ReturnType.IsByRef)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_ByRefReturn"));
			}
			throw new TargetException();
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x000FFAF0 File Offset: 0x000FDCF0
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			object[] arguments = this.InvokeArgumentsCheck(obj, invokeAttr, binder, parameters, culture);
			INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
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
			return this.UnsafeInvokeInternal(obj, parameters, arguments);
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x000FFB88 File Offset: 0x000FDD88
		[SecurityCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		internal object UnsafeInvoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			object[] arguments = this.InvokeArgumentsCheck(obj, invokeAttr, binder, parameters, culture);
			return this.UnsafeInvokeInternal(obj, parameters, arguments);
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x000FFBB0 File Offset: 0x000FDDB0
		[SecurityCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		private object UnsafeInvokeInternal(object obj, object[] parameters, object[] arguments)
		{
			if (arguments == null || arguments.Length == 0)
			{
				return RuntimeMethodHandle.InvokeMethod(obj, null, this.Signature, false);
			}
			object result = RuntimeMethodHandle.InvokeMethod(obj, arguments, this.Signature, false);
			for (int i = 0; i < arguments.Length; i++)
			{
				parameters[i] = arguments[i];
			}
			return result;
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x000FFBF8 File Offset: 0x000FDDF8
		[DebuggerStepThrough]
		[DebuggerHidden]
		private object[] InvokeArgumentsCheck(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			Signature signature = this.Signature;
			int num = signature.Arguments.Length;
			int num2 = (parameters != null) ? parameters.Length : 0;
			INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
			if ((invocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE | INVOCATION_FLAGS.INVOCATION_FLAGS_CONTAINS_STACK_POINTERS)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				this.ThrowNoInvokeException();
			}
			this.CheckConsistency(obj);
			if (num != num2)
			{
				throw new TargetParameterCountException(Environment.GetResourceString("Arg_ParmCnt"));
			}
			if (num2 != 0)
			{
				return base.CheckArguments(parameters, binder, invokeAttr, culture, signature);
			}
			return null;
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06004657 RID: 18007 RVA: 0x000FFC64 File Offset: 0x000FDE64
		public override Type ReturnType
		{
			get
			{
				return this.Signature.ReturnType;
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06004658 RID: 18008 RVA: 0x000FFC71 File Offset: 0x000FDE71
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return this.ReturnParameter;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06004659 RID: 18009 RVA: 0x000FFC79 File Offset: 0x000FDE79
		public override ParameterInfo ReturnParameter
		{
			[SecuritySafeCritical]
			get
			{
				this.FetchReturnParameter();
				return this.m_returnParameter;
			}
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x000FFC88 File Offset: 0x000FDE88
		[SecuritySafeCritical]
		public override MethodInfo GetBaseDefinition()
		{
			if (!base.IsVirtual || base.IsStatic || this.m_declaringType == null || this.m_declaringType.IsInterface)
			{
				return this;
			}
			int slot = RuntimeMethodHandle.GetSlot(this);
			RuntimeType runtimeType = (RuntimeType)this.DeclaringType;
			RuntimeType reflectedType = runtimeType;
			RuntimeMethodHandleInternal methodHandle = default(RuntimeMethodHandleInternal);
			do
			{
				int numVirtuals = RuntimeTypeHandle.GetNumVirtuals(runtimeType);
				if (numVirtuals <= slot)
				{
					break;
				}
				methodHandle = RuntimeTypeHandle.GetMethodAt(runtimeType, slot);
				reflectedType = runtimeType;
				runtimeType = (RuntimeType)runtimeType.BaseType;
			}
			while (runtimeType != null);
			return (MethodInfo)RuntimeType.GetMethodBase(reflectedType, methodHandle);
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x000FFD18 File Offset: 0x000FDF18
		[SecuritySafeCritical]
		public override Delegate CreateDelegate(Type delegateType)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.CreateDelegateInternal(delegateType, null, (DelegateBindingFlags)132, ref stackCrawlMark);
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x000FFD38 File Offset: 0x000FDF38
		[SecuritySafeCritical]
		public override Delegate CreateDelegate(Type delegateType, object target)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.CreateDelegateInternal(delegateType, target, DelegateBindingFlags.RelaxedSignature, ref stackCrawlMark);
		}

		// Token: 0x0600465D RID: 18013 RVA: 0x000FFD58 File Offset: 0x000FDF58
		[SecurityCritical]
		private Delegate CreateDelegateInternal(Type delegateType, object firstArgument, DelegateBindingFlags bindingFlags, ref StackCrawlMark stackMark)
		{
			if (delegateType == null)
			{
				throw new ArgumentNullException("delegateType");
			}
			RuntimeType runtimeType = delegateType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "delegateType");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "delegateType");
			}
			Delegate @delegate = Delegate.CreateDelegateInternal(runtimeType, this, firstArgument, bindingFlags, ref stackMark);
			if (@delegate == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
			}
			return @delegate;
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x000FFDDC File Offset: 0x000FDFDC
		[SecuritySafeCritical]
		public override MethodInfo MakeGenericMethod(params Type[] methodInstantiation)
		{
			if (methodInstantiation == null)
			{
				throw new ArgumentNullException("methodInstantiation");
			}
			RuntimeType[] array = new RuntimeType[methodInstantiation.Length];
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericMethodDefinition", new object[]
				{
					this
				}));
			}
			for (int i = 0; i < methodInstantiation.Length; i++)
			{
				Type type = methodInstantiation[i];
				if (type == null)
				{
					throw new ArgumentNullException();
				}
				RuntimeType runtimeType = type as RuntimeType;
				if (runtimeType == null)
				{
					Type[] array2 = new Type[methodInstantiation.Length];
					for (int j = 0; j < methodInstantiation.Length; j++)
					{
						array2[j] = methodInstantiation[j];
					}
					methodInstantiation = array2;
					return MethodBuilderInstantiation.MakeGenericMethod(this, methodInstantiation);
				}
				array[i] = runtimeType;
			}
			RuntimeType[] genericArgumentsInternal = this.GetGenericArgumentsInternal();
			RuntimeType.SanityCheckGenericArguments(array, genericArgumentsInternal);
			MethodInfo result = null;
			try
			{
				result = (RuntimeType.GetMethodBase(this.ReflectedTypeInternal, RuntimeMethodHandle.GetStubIfNeeded(new RuntimeMethodHandleInternal(this.m_handle), this.m_declaringType, array)) as MethodInfo);
			}
			catch (VerificationException e)
			{
				RuntimeType.ValidateGenericArguments(this, array, e);
				throw;
			}
			return result;
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x000FFEE8 File Offset: 0x000FE0E8
		internal RuntimeType[] GetGenericArgumentsInternal()
		{
			return RuntimeMethodHandle.GetMethodInstantiationInternal(this);
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x000FFEF0 File Offset: 0x000FE0F0
		public override Type[] GetGenericArguments()
		{
			Type[] array = RuntimeMethodHandle.GetMethodInstantiationPublic(this);
			if (array == null)
			{
				array = EmptyArray<Type>.Value;
			}
			return array;
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x000FFF0E File Offset: 0x000FE10E
		public override MethodInfo GetGenericMethodDefinition()
		{
			if (!this.IsGenericMethod)
			{
				throw new InvalidOperationException();
			}
			return RuntimeType.GetMethodBase(this.m_declaringType, RuntimeMethodHandle.StripMethodInstantiation(this)) as MethodInfo;
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06004662 RID: 18018 RVA: 0x000FFF34 File Offset: 0x000FE134
		public override bool IsGenericMethod
		{
			get
			{
				return RuntimeMethodHandle.HasMethodInstantiation(this);
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06004663 RID: 18019 RVA: 0x000FFF3C File Offset: 0x000FE13C
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return RuntimeMethodHandle.IsGenericMethodDefinition(this);
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06004664 RID: 18020 RVA: 0x000FFF44 File Offset: 0x000FE144
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.DeclaringType != null && this.DeclaringType.ContainsGenericParameters)
				{
					return true;
				}
				if (!this.IsGenericMethod)
				{
					return false;
				}
				Type[] genericArguments = this.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (genericArguments[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x000FFF9C File Offset: 0x000FE19C
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.m_reflectedTypeCache.IsGlobal)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_GlobalMethodSerialization"));
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Method, (this.IsGenericMethod & !this.IsGenericMethodDefinition) ? this.GetGenericArguments() : null);
		}

		// Token: 0x06004666 RID: 18022 RVA: 0x0010000E File Offset: 0x000FE20E
		internal string SerializationToString()
		{
			return this.ReturnType.FormatTypeName(true) + " " + this.FormatNameAndSig(true);
		}

		// Token: 0x06004667 RID: 18023 RVA: 0x00100030 File Offset: 0x000FE230
		internal static MethodBase InternalGetCurrentMethod(ref StackCrawlMark stackMark)
		{
			IRuntimeMethodInfo currentMethod = RuntimeMethodHandle.GetCurrentMethod(ref stackMark);
			if (currentMethod == null)
			{
				return null;
			}
			return RuntimeType.GetMethodBase(currentMethod);
		}

		// Token: 0x04001CE5 RID: 7397
		private IntPtr m_handle;

		// Token: 0x04001CE6 RID: 7398
		private RuntimeType.RuntimeTypeCache m_reflectedTypeCache;

		// Token: 0x04001CE7 RID: 7399
		private string m_name;

		// Token: 0x04001CE8 RID: 7400
		private string m_toString;

		// Token: 0x04001CE9 RID: 7401
		private ParameterInfo[] m_parameters;

		// Token: 0x04001CEA RID: 7402
		private ParameterInfo m_returnParameter;

		// Token: 0x04001CEB RID: 7403
		private BindingFlags m_bindingFlags;

		// Token: 0x04001CEC RID: 7404
		private MethodAttributes m_methodAttributes;

		// Token: 0x04001CED RID: 7405
		private Signature m_signature;

		// Token: 0x04001CEE RID: 7406
		private RuntimeType m_declaringType;

		// Token: 0x04001CEF RID: 7407
		private object m_keepalive;

		// Token: 0x04001CF0 RID: 7408
		private INVOCATION_FLAGS m_invocationFlags;

		// Token: 0x04001CF1 RID: 7409
		private RemotingMethodCachedData m_cachedData;
	}
}
