using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x02000085 RID: 133
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Delegate : ICloneable, ISerializable
	{
		// Token: 0x060006D7 RID: 1751 RVA: 0x000179D4 File Offset: 0x00015BD4
		[SecuritySafeCritical]
		protected Delegate(object target, string method)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (!this.BindToMethodName(target, (RuntimeType)target.GetType(), method, (DelegateBindingFlags)10))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00017A2C File Offset: 0x00015C2C
		[SecuritySafeCritical]
		protected Delegate(Type target, string method)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (target.IsGenericType && target.ContainsGenericParameters)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_UnboundGenParam"), "target");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = target as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "target");
			}
			this.BindToMethodName(null, runtimeType, method, (DelegateBindingFlags)37);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00017AB7 File Offset: 0x00015CB7
		private Delegate()
		{
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00017ABF File Offset: 0x00015CBF
		[__DynamicallyInvokable]
		public object DynamicInvoke(params object[] args)
		{
			return this.DynamicInvokeImpl(args);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00017AC8 File Offset: 0x00015CC8
		[SecuritySafeCritical]
		protected virtual object DynamicInvokeImpl(object[] args)
		{
			RuntimeMethodHandleInternal methodHandle = new RuntimeMethodHandleInternal(this.GetInvokeMethod());
			RuntimeMethodInfo runtimeMethodInfo = (RuntimeMethodInfo)RuntimeType.GetMethodBase((RuntimeType)base.GetType(), methodHandle);
			return runtimeMethodInfo.UnsafeInvoke(this, BindingFlags.Default, null, args, null);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00017B04 File Offset: 0x00015D04
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			if (obj == null || !Delegate.InternalEqualTypes(this, obj))
			{
				return false;
			}
			Delegate @delegate = (Delegate)obj;
			if (this._target == @delegate._target && this._methodPtr == @delegate._methodPtr && this._methodPtrAux == @delegate._methodPtrAux)
			{
				return true;
			}
			if (this._methodPtrAux.IsNull())
			{
				if (!@delegate._methodPtrAux.IsNull())
				{
					return false;
				}
				if (this._target != @delegate._target)
				{
					return false;
				}
			}
			else
			{
				if (@delegate._methodPtrAux.IsNull())
				{
					return false;
				}
				if (this._methodPtrAux == @delegate._methodPtrAux)
				{
					return true;
				}
			}
			if (this._methodBase == null || @delegate._methodBase == null || !(this._methodBase is MethodInfo) || !(@delegate._methodBase is MethodInfo))
			{
				return Delegate.InternalEqualMethodHandles(this, @delegate);
			}
			return this._methodBase.Equals(@delegate._methodBase);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00017BEF File Offset: 0x00015DEF
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00017BFC File Offset: 0x00015DFC
		[__DynamicallyInvokable]
		public static Delegate Combine(Delegate a, Delegate b)
		{
			if (a == null)
			{
				return b;
			}
			return a.CombineImpl(b);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00017C0C File Offset: 0x00015E0C
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public static Delegate Combine(params Delegate[] delegates)
		{
			if (delegates == null || delegates.Length == 0)
			{
				return null;
			}
			Delegate @delegate = delegates[0];
			for (int i = 1; i < delegates.Length; i++)
			{
				@delegate = Delegate.Combine(@delegate, delegates[i]);
			}
			return @delegate;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00017C40 File Offset: 0x00015E40
		[__DynamicallyInvokable]
		public virtual Delegate[] GetInvocationList()
		{
			return new Delegate[]
			{
				this
			};
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00017C59 File Offset: 0x00015E59
		[__DynamicallyInvokable]
		public MethodInfo Method
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetMethodImpl();
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00017C64 File Offset: 0x00015E64
		[SecuritySafeCritical]
		protected virtual MethodInfo GetMethodImpl()
		{
			if (this._methodBase == null || !(this._methodBase is MethodInfo))
			{
				IRuntimeMethodInfo runtimeMethodInfo = this.FindMethodHandle();
				RuntimeType runtimeType = RuntimeMethodHandle.GetDeclaringType(runtimeMethodInfo);
				if ((RuntimeTypeHandle.IsGenericTypeDefinition(runtimeType) || RuntimeTypeHandle.HasInstantiation(runtimeType)) && (RuntimeMethodHandle.GetAttributes(runtimeMethodInfo) & MethodAttributes.Static) <= MethodAttributes.PrivateScope)
				{
					if (this._methodPtrAux == (IntPtr)0)
					{
						Type type = this._target.GetType();
						Type genericTypeDefinition = runtimeType.GetGenericTypeDefinition();
						while (type != null)
						{
							if (type.IsGenericType && type.GetGenericTypeDefinition() == genericTypeDefinition)
							{
								runtimeType = (type as RuntimeType);
								break;
							}
							type = type.BaseType;
						}
					}
					else
					{
						MethodInfo method = base.GetType().GetMethod("Invoke");
						runtimeType = (RuntimeType)method.GetParameters()[0].ParameterType;
					}
				}
				this._methodBase = (MethodInfo)RuntimeType.GetMethodBase(runtimeType, runtimeMethodInfo);
			}
			return (MethodInfo)this._methodBase;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00017D5B File Offset: 0x00015F5B
		[__DynamicallyInvokable]
		public object Target
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetTarget();
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00017D63 File Offset: 0x00015F63
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static Delegate Remove(Delegate source, Delegate value)
		{
			if (source == null)
			{
				return null;
			}
			if (value == null)
			{
				return source;
			}
			if (!Delegate.InternalEqualTypes(source, value))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTypeMis"));
			}
			return source.RemoveImpl(value);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00017D90 File Offset: 0x00015F90
		[__DynamicallyInvokable]
		public static Delegate RemoveAll(Delegate source, Delegate value)
		{
			Delegate @delegate;
			do
			{
				@delegate = source;
				source = Delegate.Remove(source, value);
			}
			while (@delegate != source);
			return @delegate;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00017DB4 File Offset: 0x00015FB4
		protected virtual Delegate CombineImpl(Delegate d)
		{
			throw new MulticastNotSupportedException(Environment.GetResourceString("Multicast_Combine"));
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00017DC5 File Offset: 0x00015FC5
		protected virtual Delegate RemoveImpl(Delegate d)
		{
			if (!d.Equals(this))
			{
				return this;
			}
			return null;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00017DD3 File Offset: 0x00015FD3
		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00017DDB File Offset: 0x00015FDB
		public static Delegate CreateDelegate(Type type, object target, string method)
		{
			return Delegate.CreateDelegate(type, target, method, false, true);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00017DE7 File Offset: 0x00015FE7
		public static Delegate CreateDelegate(Type type, object target, string method, bool ignoreCase)
		{
			return Delegate.CreateDelegate(type, target, method, ignoreCase, true);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00017DF4 File Offset: 0x00015FF4
		[SecuritySafeCritical]
		public static Delegate CreateDelegate(Type type, object target, string method, bool ignoreCase, bool throwOnBindFailure)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
			}
			Delegate @delegate = Delegate.InternalAlloc(runtimeType);
			if (!@delegate.BindToMethodName(target, (RuntimeType)target.GetType(), method, (DelegateBindingFlags)26 | (ignoreCase ? DelegateBindingFlags.CaselessMatching : ((DelegateBindingFlags)0))))
			{
				if (throwOnBindFailure)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
				}
				@delegate = null;
			}
			return @delegate;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00017EB1 File Offset: 0x000160B1
		public static Delegate CreateDelegate(Type type, Type target, string method)
		{
			return Delegate.CreateDelegate(type, target, method, false, true);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00017EBD File Offset: 0x000160BD
		public static Delegate CreateDelegate(Type type, Type target, string method, bool ignoreCase)
		{
			return Delegate.CreateDelegate(type, target, method, ignoreCase, true);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00017ECC File Offset: 0x000160CC
		[SecuritySafeCritical]
		public static Delegate CreateDelegate(Type type, Type target, string method, bool ignoreCase, bool throwOnBindFailure)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (target.IsGenericType && target.ContainsGenericParameters)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_UnboundGenParam"), "target");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = type as RuntimeType;
			RuntimeType runtimeType2 = target as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
			}
			if (runtimeType2 == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "target");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
			}
			Delegate @delegate = Delegate.InternalAlloc(runtimeType);
			if (!@delegate.BindToMethodName(null, runtimeType2, method, (DelegateBindingFlags)5 | (ignoreCase ? DelegateBindingFlags.CaselessMatching : ((DelegateBindingFlags)0))))
			{
				if (throwOnBindFailure)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
				}
				@delegate = null;
			}
			return @delegate;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00017FD0 File Offset: 0x000161D0
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Delegate CreateDelegate(Type type, MethodInfo method, bool throwOnBindFailure)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
			}
			RuntimeMethodInfo runtimeMethodInfo = method as RuntimeMethodInfo;
			if (runtimeMethodInfo == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "method");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Delegate @delegate = Delegate.CreateDelegateInternal(runtimeType, runtimeMethodInfo, null, (DelegateBindingFlags)132, ref stackCrawlMark);
			if (@delegate == null && throwOnBindFailure)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
			}
			return @delegate;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00018097 File Offset: 0x00016297
		[__DynamicallyInvokable]
		public static Delegate CreateDelegate(Type type, object firstArgument, MethodInfo method)
		{
			return Delegate.CreateDelegate(type, firstArgument, method, true);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x000180A4 File Offset: 0x000162A4
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Delegate CreateDelegate(Type type, object firstArgument, MethodInfo method, bool throwOnBindFailure)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
			}
			RuntimeMethodInfo runtimeMethodInfo = method as RuntimeMethodInfo;
			if (runtimeMethodInfo == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "method");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Delegate @delegate = Delegate.CreateDelegateInternal(runtimeType, runtimeMethodInfo, firstArgument, DelegateBindingFlags.RelaxedSignature, ref stackCrawlMark);
			if (@delegate == null && throwOnBindFailure)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
			}
			return @delegate;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001816B File Offset: 0x0001636B
		[__DynamicallyInvokable]
		public static bool operator ==(Delegate d1, Delegate d2)
		{
			if (d1 == null)
			{
				return d2 == null;
			}
			return d1.Equals(d2);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001817C File Offset: 0x0001637C
		[__DynamicallyInvokable]
		public static bool operator !=(Delegate d1, Delegate d2)
		{
			if (d1 == null)
			{
				return d2 != null;
			}
			return !d1.Equals(d2);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00018190 File Offset: 0x00016390
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00018198 File Offset: 0x00016398
		[SecurityCritical]
		internal static Delegate CreateDelegateNoSecurityCheck(Type type, object target, RuntimeMethodHandle method)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method.IsNullHandle())
			{
				throw new ArgumentNullException("method");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "type");
			}
			if (!runtimeType.IsDelegate())
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
			}
			Delegate @delegate = Delegate.InternalAlloc(runtimeType);
			if (!@delegate.BindToMethodInfo(target, method.GetMethodInfo(), RuntimeMethodHandle.GetDeclaringType(method.GetMethodInfo()), (DelegateBindingFlags)192))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
			}
			return @delegate;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00018248 File Offset: 0x00016448
		[SecurityCritical]
		internal static Delegate CreateDelegateNoSecurityCheck(RuntimeType type, object firstArgument, MethodInfo method)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			RuntimeMethodInfo runtimeMethodInfo = method as RuntimeMethodInfo;
			if (runtimeMethodInfo == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "method");
			}
			if (!type.IsDelegate())
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), "type");
			}
			Delegate @delegate = Delegate.UnsafeCreateDelegate(type, runtimeMethodInfo, firstArgument, (DelegateBindingFlags)192);
			if (@delegate == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
			}
			return @delegate;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000182E1 File Offset: 0x000164E1
		[__DynamicallyInvokable]
		public static Delegate CreateDelegate(Type type, MethodInfo method)
		{
			return Delegate.CreateDelegate(type, method, true);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000182EC File Offset: 0x000164EC
		[SecuritySafeCritical]
		internal static Delegate CreateDelegateInternal(RuntimeType rtType, RuntimeMethodInfo rtMethod, object firstArgument, DelegateBindingFlags flags, ref StackCrawlMark stackMark)
		{
			bool flag = (rtMethod.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) > INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN;
			bool flag2 = (rtType.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) > INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN;
			if (flag || flag2)
			{
				RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
				if (executingAssembly != null && !executingAssembly.IsSafeForReflection())
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
					{
						flag ? rtMethod.FullName : rtType.FullName
					}));
				}
			}
			return Delegate.UnsafeCreateDelegate(rtType, rtMethod, firstArgument, flags);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00018364 File Offset: 0x00016564
		[SecurityCritical]
		internal static Delegate UnsafeCreateDelegate(RuntimeType rtType, RuntimeMethodInfo rtMethod, object firstArgument, DelegateBindingFlags flags)
		{
			Delegate @delegate = Delegate.InternalAlloc(rtType);
			if (@delegate.BindToMethodInfo(firstArgument, rtMethod, rtMethod.GetDeclaringTypeInternal(), flags))
			{
				return @delegate;
			}
			return null;
		}

		// Token: 0x060006FA RID: 1786
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool BindToMethodName(object target, RuntimeType methodType, string method, DelegateBindingFlags flags);

		// Token: 0x060006FB RID: 1787
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool BindToMethodInfo(object target, IRuntimeMethodInfo method, RuntimeType methodType, DelegateBindingFlags flags);

		// Token: 0x060006FC RID: 1788
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MulticastDelegate InternalAlloc(RuntimeType type);

		// Token: 0x060006FD RID: 1789
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MulticastDelegate InternalAllocLike(Delegate d);

		// Token: 0x060006FE RID: 1790
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalEqualTypes(object a, object b);

		// Token: 0x060006FF RID: 1791
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DelegateConstruct(object target, IntPtr slot);

		// Token: 0x06000700 RID: 1792
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetMulticastInvoke();

		// Token: 0x06000701 RID: 1793
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetInvokeMethod();

		// Token: 0x06000702 RID: 1794
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IRuntimeMethodInfo FindMethodHandle();

		// Token: 0x06000703 RID: 1795
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool InternalEqualMethodHandles(Delegate left, Delegate right);

		// Token: 0x06000704 RID: 1796
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr AdjustTarget(object target, IntPtr methodPtr);

		// Token: 0x06000705 RID: 1797
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetCallStub(IntPtr methodPtr);

		// Token: 0x06000706 RID: 1798 RVA: 0x0001838C File Offset: 0x0001658C
		[SecuritySafeCritical]
		internal virtual object GetTarget()
		{
			if (!this._methodPtrAux.IsNull())
			{
				return null;
			}
			return this._target;
		}

		// Token: 0x06000707 RID: 1799
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CompareUnmanagedFunctionPtrs(Delegate d1, Delegate d2);

		// Token: 0x040002F3 RID: 755
		[SecurityCritical]
		internal object _target;

		// Token: 0x040002F4 RID: 756
		[SecurityCritical]
		internal object _methodBase;

		// Token: 0x040002F5 RID: 757
		[SecurityCritical]
		internal IntPtr _methodPtr;

		// Token: 0x040002F6 RID: 758
		[SecurityCritical]
		internal IntPtr _methodPtrAux;
	}
}
