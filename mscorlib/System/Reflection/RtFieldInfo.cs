using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020005BA RID: 1466
	[Serializable]
	internal sealed class RtFieldInfo : RuntimeFieldInfo, IRuntimeFieldInfo
	{
		// Token: 0x06004501 RID: 17665
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PerformVisibilityCheckOnField(IntPtr field, object target, RuntimeType declaringType, FieldAttributes attr, uint invocationFlags);

		// Token: 0x06004502 RID: 17666 RVA: 0x000FD1B4 File Offset: 0x000FB3B4
		private bool IsNonW8PFrameworkAPI()
		{
			if (base.GetRuntimeType().IsNonW8PFrameworkAPI())
			{
				return true;
			}
			if (this.m_declaringType.IsEnum)
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
			return false;
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06004503 RID: 17667 RVA: 0x000FD214 File Offset: 0x000FB414
		internal INVOCATION_FLAGS InvocationFlags
		{
			get
			{
				if ((this.m_invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_INITIALIZED) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					Type declaringType = this.DeclaringType;
					bool flag = declaringType is ReflectionOnlyType;
					INVOCATION_FLAGS invocation_FLAGS = INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN;
					if ((declaringType != null && declaringType.ContainsGenericParameters) || (declaringType == null && this.Module.Assembly.ReflectionOnly) || flag)
					{
						invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE;
					}
					if (invocation_FLAGS == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
					{
						if ((this.m_fieldAttributes & FieldAttributes.InitOnly) != FieldAttributes.PrivateScope)
						{
							invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR;
						}
						if ((this.m_fieldAttributes & FieldAttributes.HasFieldRVA) != FieldAttributes.PrivateScope)
						{
							invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR;
						}
						bool flag2 = this.IsSecurityCritical && !this.IsSecuritySafeCritical;
						bool flag3 = (this.m_fieldAttributes & FieldAttributes.FieldAccessMask) != FieldAttributes.Public || (declaringType != null && declaringType.NeedsReflectionSecurityCheck);
						if (flag2 || flag3)
						{
							invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY;
						}
						Type fieldType = this.FieldType;
						if (fieldType.IsPointer || fieldType.IsEnum || fieldType.IsPrimitive)
						{
							invocation_FLAGS |= INVOCATION_FLAGS.INVOCATION_FLAGS_RISKY_METHOD;
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

		// Token: 0x06004504 RID: 17668 RVA: 0x000FD32E File Offset: 0x000FB52E
		private RuntimeAssembly GetRuntimeAssembly()
		{
			return this.m_declaringType.GetRuntimeAssembly();
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x000FD33B File Offset: 0x000FB53B
		[SecurityCritical]
		internal RtFieldInfo(RuntimeFieldHandleInternal handle, RuntimeType declaringType, RuntimeType.RuntimeTypeCache reflectedTypeCache, BindingFlags bindingFlags) : base(reflectedTypeCache, declaringType, bindingFlags)
		{
			this.m_fieldHandle = handle.Value;
			this.m_fieldAttributes = RuntimeFieldHandle.GetAttributes(handle);
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06004506 RID: 17670 RVA: 0x000FD360 File Offset: 0x000FB560
		RuntimeFieldHandleInternal IRuntimeFieldInfo.Value
		{
			[SecuritySafeCritical]
			get
			{
				return new RuntimeFieldHandleInternal(this.m_fieldHandle);
			}
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x000FD370 File Offset: 0x000FB570
		internal void CheckConsistency(object target)
		{
			if ((this.m_fieldAttributes & FieldAttributes.Static) == FieldAttributes.Static || this.m_declaringType.IsInstanceOfType(target))
			{
				return;
			}
			if (target == null)
			{
				throw new TargetException(Environment.GetResourceString("RFLCT.Targ_StatFldReqTarg"));
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Arg_FieldDeclTarget"), this.Name, this.m_declaringType, target.GetType()));
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x000FD3D8 File Offset: 0x000FB5D8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal override bool CacheEquals(object o)
		{
			RtFieldInfo rtFieldInfo = o as RtFieldInfo;
			return rtFieldInfo != null && rtFieldInfo.m_fieldHandle == this.m_fieldHandle;
		}

		// Token: 0x06004509 RID: 17673 RVA: 0x000FD404 File Offset: 0x000FB604
		[SecurityCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		internal void InternalSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture, ref StackCrawlMark stackMark)
		{
			INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
			RuntimeType runtimeType = this.DeclaringType as RuntimeType;
			if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				if (runtimeType != null && runtimeType.ContainsGenericParameters)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Arg_UnboundGenField"));
				}
				if ((runtimeType == null && this.Module.Assembly.ReflectionOnly) || runtimeType is ReflectionOnlyType)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Arg_ReflectionOnlyField"));
				}
				throw new FieldAccessException();
			}
			else
			{
				this.CheckConsistency(obj);
				RuntimeType runtimeType2 = (RuntimeType)this.FieldType;
				value = runtimeType2.CheckValue(value, binder, culture, invokeAttr);
				if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
					if (executingAssembly != null && !executingAssembly.IsSafeForReflection())
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
						{
							this.FullName
						}));
					}
				}
				if ((invocationFlags & (INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY | INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR)) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					RtFieldInfo.PerformVisibilityCheckOnField(this.m_fieldHandle, obj, this.m_declaringType, this.m_fieldAttributes, (uint)this.m_invocationFlags);
				}
				bool domainInitialized = false;
				if (runtimeType == null)
				{
					RuntimeFieldHandle.SetValue(this, obj, value, runtimeType2, this.m_fieldAttributes, null, ref domainInitialized);
					return;
				}
				domainInitialized = runtimeType.DomainInitialized;
				RuntimeFieldHandle.SetValue(this, obj, value, runtimeType2, this.m_fieldAttributes, runtimeType, ref domainInitialized);
				runtimeType.DomainInitialized = domainInitialized;
				return;
			}
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x000FD548 File Offset: 0x000FB748
		[SecurityCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		internal void UnsafeSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			RuntimeType runtimeType = this.DeclaringType as RuntimeType;
			RuntimeType runtimeType2 = (RuntimeType)this.FieldType;
			value = runtimeType2.CheckValue(value, binder, culture, invokeAttr);
			bool domainInitialized = false;
			if (runtimeType == null)
			{
				RuntimeFieldHandle.SetValue(this, obj, value, runtimeType2, this.m_fieldAttributes, null, ref domainInitialized);
				return;
			}
			domainInitialized = runtimeType.DomainInitialized;
			RuntimeFieldHandle.SetValue(this, obj, value, runtimeType2, this.m_fieldAttributes, runtimeType, ref domainInitialized);
			runtimeType.DomainInitialized = domainInitialized;
		}

		// Token: 0x0600450B RID: 17675 RVA: 0x000FD5BC File Offset: 0x000FB7BC
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		internal object InternalGetValue(object obj, ref StackCrawlMark stackMark)
		{
			INVOCATION_FLAGS invocationFlags = this.InvocationFlags;
			RuntimeType runtimeType = this.DeclaringType as RuntimeType;
			if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NO_INVOKE) == INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				this.CheckConsistency(obj);
				if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
					if (executingAssembly != null && !executingAssembly.IsSafeForReflection())
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
						{
							this.FullName
						}));
					}
				}
				RuntimeType runtimeType2 = (RuntimeType)this.FieldType;
				if ((invocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NEED_SECURITY) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					RtFieldInfo.PerformVisibilityCheckOnField(this.m_fieldHandle, obj, this.m_declaringType, this.m_fieldAttributes, (uint)(this.m_invocationFlags & ~INVOCATION_FLAGS.INVOCATION_FLAGS_IS_CTOR));
				}
				return this.UnsafeGetValue(obj);
			}
			if (runtimeType != null && this.DeclaringType.ContainsGenericParameters)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_UnboundGenField"));
			}
			if ((runtimeType == null && this.Module.Assembly.ReflectionOnly) || runtimeType is ReflectionOnlyType)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_ReflectionOnlyField"));
			}
			throw new FieldAccessException();
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x000FD6C0 File Offset: 0x000FB8C0
		[SecurityCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		internal object UnsafeGetValue(object obj)
		{
			RuntimeType runtimeType = this.DeclaringType as RuntimeType;
			RuntimeType fieldType = (RuntimeType)this.FieldType;
			bool domainInitialized = false;
			if (runtimeType == null)
			{
				return RuntimeFieldHandle.GetValue(this, obj, fieldType, null, ref domainInitialized);
			}
			domainInitialized = runtimeType.DomainInitialized;
			object value = RuntimeFieldHandle.GetValue(this, obj, fieldType, runtimeType, ref domainInitialized);
			runtimeType.DomainInitialized = domainInitialized;
			return value;
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x0600450D RID: 17677 RVA: 0x000FD717 File Offset: 0x000FB917
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_name == null)
				{
					this.m_name = RuntimeFieldHandle.GetName(this);
				}
				return this.m_name;
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x0600450E RID: 17678 RVA: 0x000FD733 File Offset: 0x000FB933
		internal string FullName
		{
			get
			{
				return string.Format("{0}.{1}", this.DeclaringType.FullName, this.Name);
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x0600450F RID: 17679 RVA: 0x000FD750 File Offset: 0x000FB950
		public override int MetadataToken
		{
			[SecuritySafeCritical]
			get
			{
				return RuntimeFieldHandle.GetToken(this);
			}
		}

		// Token: 0x06004510 RID: 17680 RVA: 0x000FD758 File Offset: 0x000FB958
		[SecuritySafeCritical]
		internal override RuntimeModule GetRuntimeModule()
		{
			return RuntimeTypeHandle.GetModule(RuntimeFieldHandle.GetApproxDeclaringType(this));
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x000FD768 File Offset: 0x000FB968
		public override object GetValue(object obj)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.InternalGetValue(obj, ref stackCrawlMark);
		}

		// Token: 0x06004512 RID: 17682 RVA: 0x000FD780 File Offset: 0x000FB980
		public override object GetRawConstantValue()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06004513 RID: 17683 RVA: 0x000FD787 File Offset: 0x000FB987
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		public unsafe override object GetValueDirect(TypedReference obj)
		{
			if (obj.IsNull)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_TypedReference_Null"));
			}
			return RuntimeFieldHandle.GetValueDirect(this, (RuntimeType)this.FieldType, (void*)(&obj), (RuntimeType)this.DeclaringType);
		}

		// Token: 0x06004514 RID: 17684 RVA: 0x000FD7C4 File Offset: 0x000FB9C4
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			this.InternalSetValue(obj, value, invokeAttr, binder, culture, ref stackCrawlMark);
		}

		// Token: 0x06004515 RID: 17685 RVA: 0x000FD7E2 File Offset: 0x000FB9E2
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		public unsafe override void SetValueDirect(TypedReference obj, object value)
		{
			if (obj.IsNull)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_TypedReference_Null"));
			}
			RuntimeFieldHandle.SetValueDirect(this, (RuntimeType)this.FieldType, (void*)(&obj), value, (RuntimeType)this.DeclaringType);
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06004516 RID: 17686 RVA: 0x000FD820 File Offset: 0x000FBA20
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				Type declaringType = this.DeclaringType;
				if ((declaringType == null && this.Module.Assembly.ReflectionOnly) || declaringType is ReflectionOnlyType)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAllowedInReflectionOnly"));
				}
				return new RuntimeFieldHandle(this);
			}
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x000FD86D File Offset: 0x000FBA6D
		internal IntPtr GetFieldHandle()
		{
			return this.m_fieldHandle;
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06004518 RID: 17688 RVA: 0x000FD875 File Offset: 0x000FBA75
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_fieldAttributes;
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06004519 RID: 17689 RVA: 0x000FD87D File Offset: 0x000FBA7D
		public override Type FieldType
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_fieldType == null)
				{
					this.m_fieldType = new Signature(this, this.m_declaringType).FieldType;
				}
				return this.m_fieldType;
			}
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x000FD8AA File Offset: 0x000FBAAA
		[SecuritySafeCritical]
		public override Type[] GetRequiredCustomModifiers()
		{
			return new Signature(this, this.m_declaringType).GetCustomModifiers(1, true);
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x000FD8BF File Offset: 0x000FBABF
		[SecuritySafeCritical]
		public override Type[] GetOptionalCustomModifiers()
		{
			return new Signature(this, this.m_declaringType).GetCustomModifiers(1, false);
		}

		// Token: 0x04001C01 RID: 7169
		private IntPtr m_fieldHandle;

		// Token: 0x04001C02 RID: 7170
		private FieldAttributes m_fieldAttributes;

		// Token: 0x04001C03 RID: 7171
		private string m_name;

		// Token: 0x04001C04 RID: 7172
		private RuntimeType m_fieldType;

		// Token: 0x04001C05 RID: 7173
		private INVOCATION_FLAGS m_invocationFlags;
	}
}
