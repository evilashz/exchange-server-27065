using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x02000603 RID: 1539
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ConstructorBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ConstructorBuilder : ConstructorInfo, _ConstructorBuilder
	{
		// Token: 0x0600488D RID: 18573 RVA: 0x001061C7 File Offset: 0x001043C7
		private ConstructorBuilder()
		{
		}

		// Token: 0x0600488E RID: 18574 RVA: 0x001061D0 File Offset: 0x001043D0
		[SecurityCritical]
		internal ConstructorBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers, ModuleBuilder mod, TypeBuilder type)
		{
			this.m_methodBuilder = new MethodBuilder(name, attributes, callingConvention, null, null, null, parameterTypes, requiredCustomModifiers, optionalCustomModifiers, mod, type, false);
			type.m_listMethods.Add(this.m_methodBuilder);
			int num;
			byte[] array = this.m_methodBuilder.GetMethodSignature().InternalGetSignature(out num);
			MethodToken token = this.m_methodBuilder.GetToken();
		}

		// Token: 0x0600488F RID: 18575 RVA: 0x00106230 File Offset: 0x00104430
		[SecurityCritical]
		internal ConstructorBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, ModuleBuilder mod, TypeBuilder type) : this(name, attributes, callingConvention, parameterTypes, null, null, mod, type)
		{
		}

		// Token: 0x06004890 RID: 18576 RVA: 0x0010624E File Offset: 0x0010444E
		internal override Type[] GetParameterTypes()
		{
			return this.m_methodBuilder.GetParameterTypes();
		}

		// Token: 0x06004891 RID: 18577 RVA: 0x0010625B File Offset: 0x0010445B
		private TypeBuilder GetTypeBuilder()
		{
			return this.m_methodBuilder.GetTypeBuilder();
		}

		// Token: 0x06004892 RID: 18578 RVA: 0x00106268 File Offset: 0x00104468
		internal ModuleBuilder GetModuleBuilder()
		{
			return this.GetTypeBuilder().GetModuleBuilder();
		}

		// Token: 0x06004893 RID: 18579 RVA: 0x00106275 File Offset: 0x00104475
		public override string ToString()
		{
			return this.m_methodBuilder.ToString();
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06004894 RID: 18580 RVA: 0x00106282 File Offset: 0x00104482
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_methodBuilder.MetadataTokenInternal;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06004895 RID: 18581 RVA: 0x0010628F File Offset: 0x0010448F
		public override Module Module
		{
			get
			{
				return this.m_methodBuilder.Module;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06004896 RID: 18582 RVA: 0x0010629C File Offset: 0x0010449C
		public override Type ReflectedType
		{
			get
			{
				return this.m_methodBuilder.ReflectedType;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06004897 RID: 18583 RVA: 0x001062A9 File Offset: 0x001044A9
		public override Type DeclaringType
		{
			get
			{
				return this.m_methodBuilder.DeclaringType;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06004898 RID: 18584 RVA: 0x001062B6 File Offset: 0x001044B6
		public override string Name
		{
			get
			{
				return this.m_methodBuilder.Name;
			}
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x001062C3 File Offset: 0x001044C3
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x001062D4 File Offset: 0x001044D4
		public override ParameterInfo[] GetParameters()
		{
			ConstructorInfo constructor = this.GetTypeBuilder().GetConstructor(this.m_methodBuilder.m_parameterTypes);
			return constructor.GetParameters();
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x0600489B RID: 18587 RVA: 0x001062FE File Offset: 0x001044FE
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_methodBuilder.Attributes;
			}
		}

		// Token: 0x0600489C RID: 18588 RVA: 0x0010630B File Offset: 0x0010450B
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_methodBuilder.GetMethodImplementationFlags();
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x0600489D RID: 18589 RVA: 0x00106318 File Offset: 0x00104518
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.m_methodBuilder.MethodHandle;
			}
		}

		// Token: 0x0600489E RID: 18590 RVA: 0x00106325 File Offset: 0x00104525
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x0600489F RID: 18591 RVA: 0x00106336 File Offset: 0x00104536
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_methodBuilder.GetCustomAttributes(inherit);
		}

		// Token: 0x060048A0 RID: 18592 RVA: 0x00106344 File Offset: 0x00104544
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_methodBuilder.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060048A1 RID: 18593 RVA: 0x00106353 File Offset: 0x00104553
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_methodBuilder.IsDefined(attributeType, inherit);
		}

		// Token: 0x060048A2 RID: 18594 RVA: 0x00106362 File Offset: 0x00104562
		public MethodToken GetToken()
		{
			return this.m_methodBuilder.GetToken();
		}

		// Token: 0x060048A3 RID: 18595 RVA: 0x0010636F File Offset: 0x0010456F
		public ParameterBuilder DefineParameter(int iSequence, ParameterAttributes attributes, string strParamName)
		{
			attributes &= ~ParameterAttributes.ReservedMask;
			return this.m_methodBuilder.DefineParameter(iSequence, attributes, strParamName);
		}

		// Token: 0x060048A4 RID: 18596 RVA: 0x00106388 File Offset: 0x00104588
		public void SetSymCustomAttribute(string name, byte[] data)
		{
			this.m_methodBuilder.SetSymCustomAttribute(name, data);
		}

		// Token: 0x060048A5 RID: 18597 RVA: 0x00106397 File Offset: 0x00104597
		public ILGenerator GetILGenerator()
		{
			if (this.m_isDefaultConstructor)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DefaultConstructorILGen"));
			}
			return this.m_methodBuilder.GetILGenerator();
		}

		// Token: 0x060048A6 RID: 18598 RVA: 0x001063BC File Offset: 0x001045BC
		public ILGenerator GetILGenerator(int streamSize)
		{
			if (this.m_isDefaultConstructor)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DefaultConstructorILGen"));
			}
			return this.m_methodBuilder.GetILGenerator(streamSize);
		}

		// Token: 0x060048A7 RID: 18599 RVA: 0x001063E2 File Offset: 0x001045E2
		public void SetMethodBody(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
		{
			if (this.m_isDefaultConstructor)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DefaultConstructorDefineBody"));
			}
			this.m_methodBuilder.SetMethodBody(il, maxStack, localSignature, exceptionHandlers, tokenFixups);
		}

		// Token: 0x060048A8 RID: 18600 RVA: 0x00106410 File Offset: 0x00104610
		[SecuritySafeCritical]
		public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
		{
			if (pset == null)
			{
				throw new ArgumentNullException("pset");
			}
			if (!Enum.IsDefined(typeof(SecurityAction), action) || action == SecurityAction.RequestMinimum || action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse)
			{
				throw new ArgumentOutOfRangeException("action");
			}
			if (this.m_methodBuilder.IsTypeCreated())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
			}
			byte[] array = pset.EncodeXml();
			TypeBuilder.AddDeclarativeSecurity(this.GetModuleBuilder().GetNativeHandle(), this.GetToken().Token, action, array, array.Length);
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060048A9 RID: 18601 RVA: 0x001064A2 File Offset: 0x001046A2
		public override CallingConventions CallingConvention
		{
			get
			{
				if (this.DeclaringType.IsGenericType)
				{
					return CallingConventions.HasThis;
				}
				return CallingConventions.Standard;
			}
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x001064B5 File Offset: 0x001046B5
		public Module GetModule()
		{
			return this.m_methodBuilder.GetModule();
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x060048AB RID: 18603 RVA: 0x001064C2 File Offset: 0x001046C2
		[Obsolete("This property has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		public Type ReturnType
		{
			get
			{
				return this.GetReturnType();
			}
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x001064CA File Offset: 0x001046CA
		internal override Type GetReturnType()
		{
			return this.m_methodBuilder.ReturnType;
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x060048AD RID: 18605 RVA: 0x001064D7 File Offset: 0x001046D7
		public string Signature
		{
			get
			{
				return this.m_methodBuilder.Signature;
			}
		}

		// Token: 0x060048AE RID: 18606 RVA: 0x001064E4 File Offset: 0x001046E4
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.m_methodBuilder.SetCustomAttribute(con, binaryAttribute);
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x001064F3 File Offset: 0x001046F3
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.m_methodBuilder.SetCustomAttribute(customBuilder);
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x00106501 File Offset: 0x00104701
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			this.m_methodBuilder.SetImplementationFlags(attributes);
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x060048B1 RID: 18609 RVA: 0x0010650F File Offset: 0x0010470F
		// (set) Token: 0x060048B2 RID: 18610 RVA: 0x0010651C File Offset: 0x0010471C
		public bool InitLocals
		{
			get
			{
				return this.m_methodBuilder.InitLocals;
			}
			set
			{
				this.m_methodBuilder.InitLocals = value;
			}
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x0010652A File Offset: 0x0010472A
		void _ConstructorBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060048B4 RID: 18612 RVA: 0x00106531 File Offset: 0x00104731
		void _ConstructorBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x00106538 File Offset: 0x00104738
		void _ConstructorBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x0010653F File Offset: 0x0010473F
		void _ConstructorBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001DC8 RID: 7624
		private readonly MethodBuilder m_methodBuilder;

		// Token: 0x04001DC9 RID: 7625
		internal bool m_isDefaultConstructor;
	}
}
