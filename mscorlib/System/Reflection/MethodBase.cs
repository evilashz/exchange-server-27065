using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020005D9 RID: 1497
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_MethodBase))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public abstract class MethodBase : MemberInfo, _MethodBase
	{
		// Token: 0x060045D2 RID: 17874 RVA: 0x000FECE8 File Offset: 0x000FCEE8
		[__DynamicallyInvokable]
		public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"));
			}
			MethodBase methodBase = RuntimeType.GetMethodBase(handle.GetMethodInfo());
			Type declaringType = methodBase.DeclaringType;
			if (declaringType != null && declaringType.IsGenericType)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_MethodDeclaringTypeGeneric"), methodBase, declaringType.GetGenericTypeDefinition()));
			}
			return methodBase;
		}

		// Token: 0x060045D3 RID: 17875 RVA: 0x000FED55 File Offset: 0x000FCF55
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle, RuntimeTypeHandle declaringType)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"));
			}
			return RuntimeType.GetMethodBase(declaringType.GetRuntimeType(), handle.GetMethodInfo());
		}

		// Token: 0x060045D4 RID: 17876 RVA: 0x000FED84 File Offset: 0x000FCF84
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static MethodBase GetCurrentMethod()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeMethodInfo.InternalGetCurrentMethod(ref stackCrawlMark);
		}

		// Token: 0x060045D6 RID: 17878 RVA: 0x000FEDA4 File Offset: 0x000FCFA4
		[__DynamicallyInvokable]
		public static bool operator ==(MethodBase left, MethodBase right)
		{
			if (left == right)
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			MethodInfo left2;
			MethodInfo right2;
			if ((left2 = (left as MethodInfo)) != null && (right2 = (right as MethodInfo)) != null)
			{
				return left2 == right2;
			}
			ConstructorInfo left3;
			ConstructorInfo right3;
			return (left3 = (left as ConstructorInfo)) != null && (right3 = (right as ConstructorInfo)) != null && left3 == right3;
		}

		// Token: 0x060045D7 RID: 17879 RVA: 0x000FEE10 File Offset: 0x000FD010
		[__DynamicallyInvokable]
		public static bool operator !=(MethodBase left, MethodBase right)
		{
			return !(left == right);
		}

		// Token: 0x060045D8 RID: 17880 RVA: 0x000FEE1C File Offset: 0x000FD01C
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060045D9 RID: 17881 RVA: 0x000FEE25 File Offset: 0x000FD025
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x000FEE30 File Offset: 0x000FD030
		[SecurityCritical]
		private IntPtr GetMethodDesc()
		{
			return this.MethodHandle.Value;
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060045DB RID: 17883 RVA: 0x000FEE4B File Offset: 0x000FD04B
		internal virtual bool IsDynamicallyInvokable
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x000FEE4E File Offset: 0x000FD04E
		internal virtual ParameterInfo[] GetParametersNoCopy()
		{
			return this.GetParameters();
		}

		// Token: 0x060045DD RID: 17885
		[__DynamicallyInvokable]
		public abstract ParameterInfo[] GetParameters();

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060045DE RID: 17886 RVA: 0x000FEE56 File Offset: 0x000FD056
		[__DynamicallyInvokable]
		public virtual MethodImplAttributes MethodImplementationFlags
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetMethodImplementationFlags();
			}
		}

		// Token: 0x060045DF RID: 17887
		public abstract MethodImplAttributes GetMethodImplementationFlags();

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060045E0 RID: 17888
		[__DynamicallyInvokable]
		public abstract RuntimeMethodHandle MethodHandle { [__DynamicallyInvokable] get; }

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060045E1 RID: 17889
		[__DynamicallyInvokable]
		public abstract MethodAttributes Attributes { [__DynamicallyInvokable] get; }

		// Token: 0x060045E2 RID: 17890
		public abstract object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060045E3 RID: 17891 RVA: 0x000FEE5E File Offset: 0x000FD05E
		[__DynamicallyInvokable]
		public virtual CallingConventions CallingConvention
		{
			[__DynamicallyInvokable]
			get
			{
				return CallingConventions.Standard;
			}
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x000FEE61 File Offset: 0x000FD061
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public virtual Type[] GetGenericArguments()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060045E5 RID: 17893 RVA: 0x000FEE72 File Offset: 0x000FD072
		[__DynamicallyInvokable]
		public virtual bool IsGenericMethodDefinition
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060045E6 RID: 17894 RVA: 0x000FEE75 File Offset: 0x000FD075
		[__DynamicallyInvokable]
		public virtual bool ContainsGenericParameters
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060045E7 RID: 17895 RVA: 0x000FEE78 File Offset: 0x000FD078
		[__DynamicallyInvokable]
		public virtual bool IsGenericMethod
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060045E8 RID: 17896 RVA: 0x000FEE7B File Offset: 0x000FD07B
		public virtual bool IsSecurityCritical
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060045E9 RID: 17897 RVA: 0x000FEE82 File Offset: 0x000FD082
		public virtual bool IsSecuritySafeCritical
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060045EA RID: 17898 RVA: 0x000FEE89 File Offset: 0x000FD089
		public virtual bool IsSecurityTransparent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x000FEE90 File Offset: 0x000FD090
		[DebuggerStepThrough]
		[DebuggerHidden]
		[__DynamicallyInvokable]
		public object Invoke(object obj, object[] parameters)
		{
			return this.Invoke(obj, BindingFlags.Default, null, parameters, null);
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060045EC RID: 17900 RVA: 0x000FEE9D File Offset: 0x000FD09D
		[__DynamicallyInvokable]
		public bool IsPublic
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060045ED RID: 17901 RVA: 0x000FEEAA File Offset: 0x000FD0AA
		[__DynamicallyInvokable]
		public bool IsPrivate
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060045EE RID: 17902 RVA: 0x000FEEB7 File Offset: 0x000FD0B7
		[__DynamicallyInvokable]
		public bool IsFamily
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Family;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060045EF RID: 17903 RVA: 0x000FEEC4 File Offset: 0x000FD0C4
		[__DynamicallyInvokable]
		public bool IsAssembly
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Assembly;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060045F0 RID: 17904 RVA: 0x000FEED1 File Offset: 0x000FD0D1
		[__DynamicallyInvokable]
		public bool IsFamilyAndAssembly
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamANDAssem;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060045F1 RID: 17905 RVA: 0x000FEEDE File Offset: 0x000FD0DE
		[__DynamicallyInvokable]
		public bool IsFamilyOrAssembly
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamORAssem;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060045F2 RID: 17906 RVA: 0x000FEEEB File Offset: 0x000FD0EB
		[__DynamicallyInvokable]
		public bool IsStatic
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.Static) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060045F3 RID: 17907 RVA: 0x000FEEF9 File Offset: 0x000FD0F9
		[__DynamicallyInvokable]
		public bool IsFinal
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.Final) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060045F4 RID: 17908 RVA: 0x000FEF07 File Offset: 0x000FD107
		[__DynamicallyInvokable]
		public bool IsVirtual
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.Virtual) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060045F5 RID: 17909 RVA: 0x000FEF15 File Offset: 0x000FD115
		[__DynamicallyInvokable]
		public bool IsHideBySig
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.HideBySig) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060045F6 RID: 17910 RVA: 0x000FEF26 File Offset: 0x000FD126
		[__DynamicallyInvokable]
		public bool IsAbstract
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.Abstract) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x060045F7 RID: 17911 RVA: 0x000FEF37 File Offset: 0x000FD137
		[__DynamicallyInvokable]
		public bool IsSpecialName
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.Attributes & MethodAttributes.SpecialName) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x060045F8 RID: 17912 RVA: 0x000FEF48 File Offset: 0x000FD148
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public bool IsConstructor
		{
			[__DynamicallyInvokable]
			get
			{
				return this is ConstructorInfo && !this.IsStatic && (this.Attributes & MethodAttributes.RTSpecialName) == MethodAttributes.RTSpecialName;
			}
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x000FEF6F File Offset: 0x000FD16F
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Demand, Flags = ReflectionPermissionFlag.MemberAccess)]
		public virtual MethodBody GetMethodBody()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060045FA RID: 17914 RVA: 0x000FEF78 File Offset: 0x000FD178
		internal static string ConstructParameters(Type[] parameterTypes, CallingConventions callingConvention, bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string value = "";
			foreach (Type type in parameterTypes)
			{
				stringBuilder.Append(value);
				string text = type.FormatTypeName(serialization);
				if (type.IsByRef && !serialization)
				{
					stringBuilder.Append(text.TrimEnd(new char[]
					{
						'&'
					}));
					stringBuilder.Append(" ByRef");
				}
				else
				{
					stringBuilder.Append(text);
				}
				value = ", ";
			}
			if ((callingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
			{
				stringBuilder.Append(value);
				stringBuilder.Append("...");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x060045FB RID: 17915 RVA: 0x000FF015 File Offset: 0x000FD215
		internal string FullName
		{
			get
			{
				return string.Format("{0}.{1}", this.DeclaringType.FullName, this.FormatNameAndSig());
			}
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x000FF032 File Offset: 0x000FD232
		internal string FormatNameAndSig()
		{
			return this.FormatNameAndSig(false);
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x000FF03C File Offset: 0x000FD23C
		internal virtual string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.Name);
			stringBuilder.Append("(");
			stringBuilder.Append(MethodBase.ConstructParameters(this.GetParameterTypes(), this.CallingConvention, serialization));
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x000FF08C File Offset: 0x000FD28C
		internal virtual Type[] GetParameterTypes()
		{
			ParameterInfo[] parametersNoCopy = this.GetParametersNoCopy();
			Type[] array = new Type[parametersNoCopy.Length];
			for (int i = 0; i < parametersNoCopy.Length; i++)
			{
				array[i] = parametersNoCopy[i].ParameterType;
			}
			return array;
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x000FF0C4 File Offset: 0x000FD2C4
		[SecuritySafeCritical]
		internal object[] CheckArguments(object[] parameters, Binder binder, BindingFlags invokeAttr, CultureInfo culture, Signature sig)
		{
			object[] array = new object[parameters.Length];
			ParameterInfo[] array2 = null;
			for (int i = 0; i < parameters.Length; i++)
			{
				object obj = parameters[i];
				RuntimeType runtimeType = sig.Arguments[i];
				if (obj == Type.Missing)
				{
					if (array2 == null)
					{
						array2 = this.GetParametersNoCopy();
					}
					if (array2[i].DefaultValue == DBNull.Value)
					{
						throw new ArgumentException(Environment.GetResourceString("Arg_VarMissNull"), "parameters");
					}
					obj = array2[i].DefaultValue;
				}
				array[i] = runtimeType.CheckValue(obj, binder, culture, invokeAttr);
			}
			return array;
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x000FF148 File Offset: 0x000FD348
		Type _MethodBase.GetType()
		{
			return base.GetType();
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06004601 RID: 17921 RVA: 0x000FF150 File Offset: 0x000FD350
		bool _MethodBase.IsPublic
		{
			get
			{
				return this.IsPublic;
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06004602 RID: 17922 RVA: 0x000FF158 File Offset: 0x000FD358
		bool _MethodBase.IsPrivate
		{
			get
			{
				return this.IsPrivate;
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06004603 RID: 17923 RVA: 0x000FF160 File Offset: 0x000FD360
		bool _MethodBase.IsFamily
		{
			get
			{
				return this.IsFamily;
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06004604 RID: 17924 RVA: 0x000FF168 File Offset: 0x000FD368
		bool _MethodBase.IsAssembly
		{
			get
			{
				return this.IsAssembly;
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06004605 RID: 17925 RVA: 0x000FF170 File Offset: 0x000FD370
		bool _MethodBase.IsFamilyAndAssembly
		{
			get
			{
				return this.IsFamilyAndAssembly;
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06004606 RID: 17926 RVA: 0x000FF178 File Offset: 0x000FD378
		bool _MethodBase.IsFamilyOrAssembly
		{
			get
			{
				return this.IsFamilyOrAssembly;
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06004607 RID: 17927 RVA: 0x000FF180 File Offset: 0x000FD380
		bool _MethodBase.IsStatic
		{
			get
			{
				return this.IsStatic;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06004608 RID: 17928 RVA: 0x000FF188 File Offset: 0x000FD388
		bool _MethodBase.IsFinal
		{
			get
			{
				return this.IsFinal;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06004609 RID: 17929 RVA: 0x000FF190 File Offset: 0x000FD390
		bool _MethodBase.IsVirtual
		{
			get
			{
				return this.IsVirtual;
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x0600460A RID: 17930 RVA: 0x000FF198 File Offset: 0x000FD398
		bool _MethodBase.IsHideBySig
		{
			get
			{
				return this.IsHideBySig;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x0600460B RID: 17931 RVA: 0x000FF1A0 File Offset: 0x000FD3A0
		bool _MethodBase.IsAbstract
		{
			get
			{
				return this.IsAbstract;
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x0600460C RID: 17932 RVA: 0x000FF1A8 File Offset: 0x000FD3A8
		bool _MethodBase.IsSpecialName
		{
			get
			{
				return this.IsSpecialName;
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x0600460D RID: 17933 RVA: 0x000FF1B0 File Offset: 0x000FD3B0
		bool _MethodBase.IsConstructor
		{
			get
			{
				return this.IsConstructor;
			}
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x000FF1B8 File Offset: 0x000FD3B8
		void _MethodBase.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x000FF1BF File Offset: 0x000FD3BF
		void _MethodBase.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x000FF1C6 File Offset: 0x000FD3C6
		void _MethodBase.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004611 RID: 17937 RVA: 0x000FF1CD File Offset: 0x000FD3CD
		void _MethodBase.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}
	}
}
