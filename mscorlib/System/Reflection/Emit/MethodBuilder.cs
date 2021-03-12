using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x02000619 RID: 1561
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_MethodBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class MethodBuilder : MethodInfo, _MethodBuilder
	{
		// Token: 0x060049FB RID: 18939 RVA: 0x0010B5AC File Offset: 0x001097AC
		internal MethodBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, ModuleBuilder mod, TypeBuilder type, bool bIsGlobalMethod)
		{
			this.Init(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null, mod, type, bIsGlobalMethod);
		}

		// Token: 0x060049FC RID: 18940 RVA: 0x0010B5E0 File Offset: 0x001097E0
		internal MethodBuilder(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, ModuleBuilder mod, TypeBuilder type, bool bIsGlobalMethod)
		{
			this.Init(name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, mod, type, bIsGlobalMethod);
		}

		// Token: 0x060049FD RID: 18941 RVA: 0x0010B618 File Offset: 0x00109818
		private void Init(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, ModuleBuilder mod, TypeBuilder type, bool bIsGlobalMethod)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			if (name[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), "name");
			}
			if (mod == null)
			{
				throw new ArgumentNullException("mod");
			}
			if (parameterTypes != null)
			{
				foreach (Type left in parameterTypes)
				{
					if (left == null)
					{
						throw new ArgumentNullException("parameterTypes");
					}
				}
			}
			this.m_strName = name;
			this.m_module = mod;
			this.m_containingType = type;
			this.m_returnType = returnType;
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				callingConvention |= CallingConventions.HasThis;
			}
			else if ((attributes & MethodAttributes.Virtual) != MethodAttributes.PrivateScope)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_NoStaticVirtual"));
			}
			if ((attributes & MethodAttributes.SpecialName) != MethodAttributes.SpecialName && (type.Attributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask && (attributes & (MethodAttributes.Virtual | MethodAttributes.Abstract)) != (MethodAttributes.Virtual | MethodAttributes.Abstract) && (attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadAttributeOnInterfaceMethod"));
			}
			this.m_callingConvention = callingConvention;
			if (parameterTypes != null)
			{
				this.m_parameterTypes = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.m_parameterTypes, parameterTypes.Length);
			}
			else
			{
				this.m_parameterTypes = null;
			}
			this.m_returnTypeRequiredCustomModifiers = returnTypeRequiredCustomModifiers;
			this.m_returnTypeOptionalCustomModifiers = returnTypeOptionalCustomModifiers;
			this.m_parameterTypeRequiredCustomModifiers = parameterTypeRequiredCustomModifiers;
			this.m_parameterTypeOptionalCustomModifiers = parameterTypeOptionalCustomModifiers;
			this.m_iAttributes = attributes;
			this.m_bIsGlobalMethod = bIsGlobalMethod;
			this.m_bIsBaked = false;
			this.m_fInitLocals = true;
			this.m_localSymInfo = new LocalSymInfo();
			this.m_ubBody = null;
			this.m_ilGenerator = null;
			this.m_dwMethodImplFlags = MethodImplAttributes.IL;
		}

		// Token: 0x060049FE RID: 18942 RVA: 0x0010B7C4 File Offset: 0x001099C4
		internal void CheckContext(params Type[][] typess)
		{
			this.m_module.CheckContext(typess);
		}

		// Token: 0x060049FF RID: 18943 RVA: 0x0010B7D2 File Offset: 0x001099D2
		internal void CheckContext(params Type[] types)
		{
			this.m_module.CheckContext(types);
		}

		// Token: 0x06004A00 RID: 18944 RVA: 0x0010B7E0 File Offset: 0x001099E0
		[SecurityCritical]
		internal void CreateMethodBodyHelper(ILGenerator il)
		{
			if (il == null)
			{
				throw new ArgumentNullException("il");
			}
			int num = 0;
			ModuleBuilder module = this.m_module;
			this.m_containingType.ThrowIfCreated();
			if (this.m_bIsBaked)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodHasBody"));
			}
			if (il.m_methodBuilder != this && il.m_methodBuilder != null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadILGeneratorUsage"));
			}
			this.ThrowIfShouldNotHaveBody();
			if (il.m_ScopeTree.m_iOpenScopeCount != 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OpenLocalVariableScope"));
			}
			this.m_ubBody = il.BakeByteArray();
			this.m_mdMethodFixups = il.GetTokenFixups();
			__ExceptionInfo[] exceptions = il.GetExceptions();
			int num2 = this.CalculateNumberOfExceptions(exceptions);
			if (num2 > 0)
			{
				this.m_exceptions = new ExceptionHandler[num2];
				for (int i = 0; i < exceptions.Length; i++)
				{
					int[] filterAddresses = exceptions[i].GetFilterAddresses();
					int[] catchAddresses = exceptions[i].GetCatchAddresses();
					int[] catchEndAddresses = exceptions[i].GetCatchEndAddresses();
					Type[] catchClass = exceptions[i].GetCatchClass();
					int numberOfCatches = exceptions[i].GetNumberOfCatches();
					int startAddress = exceptions[i].GetStartAddress();
					int endAddress = exceptions[i].GetEndAddress();
					int[] exceptionTypes = exceptions[i].GetExceptionTypes();
					for (int j = 0; j < numberOfCatches; j++)
					{
						int exceptionTypeToken = 0;
						if (catchClass[j] != null)
						{
							exceptionTypeToken = module.GetTypeTokenInternal(catchClass[j]).Token;
						}
						switch (exceptionTypes[j])
						{
						case 0:
						case 1:
						case 4:
							this.m_exceptions[num++] = new ExceptionHandler(startAddress, endAddress, filterAddresses[j], catchAddresses[j], catchEndAddresses[j], exceptionTypes[j], exceptionTypeToken);
							break;
						case 2:
							this.m_exceptions[num++] = new ExceptionHandler(startAddress, exceptions[i].GetFinallyEndAddress(), filterAddresses[j], catchAddresses[j], catchEndAddresses[j], exceptionTypes[j], exceptionTypeToken);
							break;
						}
					}
				}
			}
			this.m_bIsBaked = true;
			if (module.GetSymWriter() != null)
			{
				SymbolToken method = new SymbolToken(this.MetadataTokenInternal);
				ISymbolWriter symWriter = module.GetSymWriter();
				symWriter.OpenMethod(method);
				symWriter.OpenScope(0);
				if (this.m_symCustomAttrs != null)
				{
					foreach (MethodBuilder.SymCustomAttr symCustomAttr in this.m_symCustomAttrs)
					{
						module.GetSymWriter().SetSymAttribute(new SymbolToken(this.MetadataTokenInternal), symCustomAttr.m_name, symCustomAttr.m_data);
					}
				}
				if (this.m_localSymInfo != null)
				{
					this.m_localSymInfo.EmitLocalSymInfo(symWriter);
				}
				il.m_ScopeTree.EmitScopeTree(symWriter);
				il.m_LineNumberInfo.EmitLineNumberInfo(symWriter);
				symWriter.CloseScope(il.ILOffset);
				symWriter.CloseMethod();
			}
		}

		// Token: 0x06004A01 RID: 18945 RVA: 0x0010BAD4 File Offset: 0x00109CD4
		internal void ReleaseBakedStructures()
		{
			if (!this.m_bIsBaked)
			{
				return;
			}
			this.m_ubBody = null;
			this.m_localSymInfo = null;
			this.m_mdMethodFixups = null;
			this.m_localSignature = null;
			this.m_exceptions = null;
		}

		// Token: 0x06004A02 RID: 18946 RVA: 0x0010BB02 File Offset: 0x00109D02
		internal override Type[] GetParameterTypes()
		{
			if (this.m_parameterTypes == null)
			{
				this.m_parameterTypes = EmptyArray<Type>.Value;
			}
			return this.m_parameterTypes;
		}

		// Token: 0x06004A03 RID: 18947 RVA: 0x0010BB20 File Offset: 0x00109D20
		internal static Type GetMethodBaseReturnType(MethodBase method)
		{
			MethodInfo methodInfo;
			if ((methodInfo = (method as MethodInfo)) != null)
			{
				return methodInfo.ReturnType;
			}
			ConstructorInfo constructorInfo;
			if ((constructorInfo = (method as ConstructorInfo)) != null)
			{
				return constructorInfo.GetReturnType();
			}
			return null;
		}

		// Token: 0x06004A04 RID: 18948 RVA: 0x0010BB60 File Offset: 0x00109D60
		internal void SetToken(MethodToken token)
		{
			this.m_tkMethod = token;
		}

		// Token: 0x06004A05 RID: 18949 RVA: 0x0010BB69 File Offset: 0x00109D69
		internal byte[] GetBody()
		{
			return this.m_ubBody;
		}

		// Token: 0x06004A06 RID: 18950 RVA: 0x0010BB71 File Offset: 0x00109D71
		internal int[] GetTokenFixups()
		{
			return this.m_mdMethodFixups;
		}

		// Token: 0x06004A07 RID: 18951 RVA: 0x0010BB7C File Offset: 0x00109D7C
		[SecurityCritical]
		internal SignatureHelper GetMethodSignature()
		{
			if (this.m_parameterTypes == null)
			{
				this.m_parameterTypes = EmptyArray<Type>.Value;
			}
			this.m_signature = SignatureHelper.GetMethodSigHelper(this.m_module, this.m_callingConvention, (this.m_inst != null) ? this.m_inst.Length : 0, (this.m_returnType == null) ? typeof(void) : this.m_returnType, this.m_returnTypeRequiredCustomModifiers, this.m_returnTypeOptionalCustomModifiers, this.m_parameterTypes, this.m_parameterTypeRequiredCustomModifiers, this.m_parameterTypeOptionalCustomModifiers);
			return this.m_signature;
		}

		// Token: 0x06004A08 RID: 18952 RVA: 0x0010BC0C File Offset: 0x00109E0C
		internal byte[] GetLocalSignature(out int signatureLength)
		{
			if (this.m_localSignature != null)
			{
				signatureLength = this.m_localSignature.Length;
				return this.m_localSignature;
			}
			if (this.m_ilGenerator != null && this.m_ilGenerator.m_localCount != 0)
			{
				return this.m_ilGenerator.m_localSignature.InternalGetSignature(out signatureLength);
			}
			return SignatureHelper.GetLocalVarSigHelper(this.m_module).InternalGetSignature(out signatureLength);
		}

		// Token: 0x06004A09 RID: 18953 RVA: 0x0010BC6A File Offset: 0x00109E6A
		internal int GetMaxStack()
		{
			if (this.m_ilGenerator != null)
			{
				return this.m_ilGenerator.GetMaxStackSize() + this.ExceptionHandlerCount;
			}
			return this.m_maxStack;
		}

		// Token: 0x06004A0A RID: 18954 RVA: 0x0010BC8D File Offset: 0x00109E8D
		internal ExceptionHandler[] GetExceptionHandlers()
		{
			return this.m_exceptions;
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06004A0B RID: 18955 RVA: 0x0010BC95 File Offset: 0x00109E95
		internal int ExceptionHandlerCount
		{
			get
			{
				if (this.m_exceptions == null)
				{
					return 0;
				}
				return this.m_exceptions.Length;
			}
		}

		// Token: 0x06004A0C RID: 18956 RVA: 0x0010BCAC File Offset: 0x00109EAC
		internal int CalculateNumberOfExceptions(__ExceptionInfo[] excp)
		{
			int num = 0;
			if (excp == null)
			{
				return 0;
			}
			for (int i = 0; i < excp.Length; i++)
			{
				num += excp[i].GetNumberOfCatches();
			}
			return num;
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x0010BCDA File Offset: 0x00109EDA
		internal bool IsTypeCreated()
		{
			return this.m_containingType != null && this.m_containingType.IsCreated();
		}

		// Token: 0x06004A0E RID: 18958 RVA: 0x0010BCF7 File Offset: 0x00109EF7
		internal TypeBuilder GetTypeBuilder()
		{
			return this.m_containingType;
		}

		// Token: 0x06004A0F RID: 18959 RVA: 0x0010BCFF File Offset: 0x00109EFF
		internal ModuleBuilder GetModuleBuilder()
		{
			return this.m_module;
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x0010BD08 File Offset: 0x00109F08
		[SecuritySafeCritical]
		public override bool Equals(object obj)
		{
			if (!(obj is MethodBuilder))
			{
				return false;
			}
			if (!this.m_strName.Equals(((MethodBuilder)obj).m_strName))
			{
				return false;
			}
			if (this.m_iAttributes != ((MethodBuilder)obj).m_iAttributes)
			{
				return false;
			}
			SignatureHelper methodSignature = ((MethodBuilder)obj).GetMethodSignature();
			return methodSignature.Equals(this.GetMethodSignature());
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x0010BD6B File Offset: 0x00109F6B
		public override int GetHashCode()
		{
			return this.m_strName.GetHashCode();
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x0010BD78 File Offset: 0x00109F78
		[SecuritySafeCritical]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(1000);
			stringBuilder.Append("Name: " + this.m_strName + " " + Environment.NewLine);
			stringBuilder.Append("Attributes: " + (int)this.m_iAttributes + Environment.NewLine);
			stringBuilder.Append("Method Signature: " + this.GetMethodSignature() + Environment.NewLine);
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06004A13 RID: 18963 RVA: 0x0010BE00 File Offset: 0x0010A000
		public override string Name
		{
			get
			{
				return this.m_strName;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06004A14 RID: 18964 RVA: 0x0010BE08 File Offset: 0x0010A008
		internal int MetadataTokenInternal
		{
			get
			{
				return this.GetToken().Token;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06004A15 RID: 18965 RVA: 0x0010BE23 File Offset: 0x0010A023
		public override Module Module
		{
			get
			{
				return this.m_containingType.Module;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06004A16 RID: 18966 RVA: 0x0010BE30 File Offset: 0x0010A030
		public override Type DeclaringType
		{
			get
			{
				if (this.m_containingType.m_isHiddenGlobalType)
				{
					return null;
				}
				return this.m_containingType;
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06004A17 RID: 18967 RVA: 0x0010BE47 File Offset: 0x0010A047
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06004A18 RID: 18968 RVA: 0x0010BE4A File Offset: 0x0010A04A
		public override Type ReflectedType
		{
			get
			{
				return this.DeclaringType;
			}
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x0010BE52 File Offset: 0x0010A052
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x0010BE63 File Offset: 0x0010A063
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_dwMethodImplFlags;
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06004A1B RID: 18971 RVA: 0x0010BE6B File Offset: 0x0010A06B
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_iAttributes;
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06004A1C RID: 18972 RVA: 0x0010BE73 File Offset: 0x0010A073
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_callingConvention;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06004A1D RID: 18973 RVA: 0x0010BE7B File Offset: 0x0010A07B
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06004A1E RID: 18974 RVA: 0x0010BE8C File Offset: 0x0010A08C
		public override bool IsSecurityCritical
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06004A1F RID: 18975 RVA: 0x0010BE9D File Offset: 0x0010A09D
		public override bool IsSecuritySafeCritical
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06004A20 RID: 18976 RVA: 0x0010BEAE File Offset: 0x0010A0AE
		public override bool IsSecurityTransparent
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
			}
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x0010BEBF File Offset: 0x0010A0BF
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06004A22 RID: 18978 RVA: 0x0010BEC2 File Offset: 0x0010A0C2
		public override Type ReturnType
		{
			get
			{
				return this.m_returnType;
			}
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x0010BECC File Offset: 0x0010A0CC
		public override ParameterInfo[] GetParameters()
		{
			if (!this.m_bIsBaked || this.m_containingType == null || this.m_containingType.BakedRuntimeType == null)
			{
				throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_TypeNotCreated"));
			}
			MethodInfo method = this.m_containingType.GetMethod(this.m_strName, this.m_parameterTypes);
			return method.GetParameters();
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06004A24 RID: 18980 RVA: 0x0010BF30 File Offset: 0x0010A130
		public override ParameterInfo ReturnParameter
		{
			get
			{
				if (!this.m_bIsBaked || this.m_containingType == null || this.m_containingType.BakedRuntimeType == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeNotCreated"));
				}
				MethodInfo method = this.m_containingType.GetMethod(this.m_strName, this.m_parameterTypes);
				return method.ReturnParameter;
			}
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x0010BF94 File Offset: 0x0010A194
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x0010BFA5 File Offset: 0x0010A1A5
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x0010BFB6 File Offset: 0x0010A1B6
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06004A28 RID: 18984 RVA: 0x0010BFC7 File Offset: 0x0010A1C7
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.m_bIsGenMethDef;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06004A29 RID: 18985 RVA: 0x0010BFCF File Offset: 0x0010A1CF
		public override bool ContainsGenericParameters
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x0010BFD6 File Offset: 0x0010A1D6
		public override MethodInfo GetGenericMethodDefinition()
		{
			if (!this.IsGenericMethod)
			{
				throw new InvalidOperationException();
			}
			return this;
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06004A2B RID: 18987 RVA: 0x0010BFE7 File Offset: 0x0010A1E7
		public override bool IsGenericMethod
		{
			get
			{
				return this.m_inst != null;
			}
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x0010BFF2 File Offset: 0x0010A1F2
		public override Type[] GetGenericArguments()
		{
			return this.m_inst;
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x0010BFFA File Offset: 0x0010A1FA
		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			return MethodBuilderInstantiation.MakeGenericMethod(this, typeArguments);
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x0010C004 File Offset: 0x0010A204
		public GenericTypeParameterBuilder[] DefineGenericParameters(params string[] names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			if (names.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EmptyArray"), "names");
			}
			if (this.m_inst != null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GenericParametersAlreadySet"));
			}
			for (int i = 0; i < names.Length; i++)
			{
				if (names[i] == null)
				{
					throw new ArgumentNullException("names");
				}
			}
			if (this.m_tkMethod.Token != 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBuilderBaked"));
			}
			this.m_bIsGenMethDef = true;
			this.m_inst = new GenericTypeParameterBuilder[names.Length];
			for (int j = 0; j < names.Length; j++)
			{
				this.m_inst[j] = new GenericTypeParameterBuilder(new TypeBuilder(names[j], j, this));
			}
			return this.m_inst;
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x0010C0CB File Offset: 0x0010A2CB
		internal void ThrowIfGeneric()
		{
			if (this.IsGenericMethod && !this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x0010C0E4 File Offset: 0x0010A2E4
		[SecuritySafeCritical]
		public MethodToken GetToken()
		{
			if (this.m_tkMethod.Token != 0)
			{
				return this.m_tkMethod;
			}
			MethodToken tokenNoLock = new MethodToken(0);
			List<MethodBuilder> listMethods = this.m_containingType.m_listMethods;
			lock (listMethods)
			{
				if (this.m_tkMethod.Token != 0)
				{
					return this.m_tkMethod;
				}
				int i;
				for (i = this.m_containingType.m_lastTokenizedMethod + 1; i < this.m_containingType.m_listMethods.Count; i++)
				{
					MethodBuilder methodBuilder = this.m_containingType.m_listMethods[i];
					tokenNoLock = methodBuilder.GetTokenNoLock();
					if (methodBuilder == this)
					{
						break;
					}
				}
				this.m_containingType.m_lastTokenizedMethod = i;
			}
			return tokenNoLock;
		}

		// Token: 0x06004A31 RID: 18993 RVA: 0x0010C1B4 File Offset: 0x0010A3B4
		[SecurityCritical]
		private MethodToken GetTokenNoLock()
		{
			int sigLength;
			byte[] signature = this.GetMethodSignature().InternalGetSignature(out sigLength);
			int num = TypeBuilder.DefineMethod(this.m_module.GetNativeHandle(), this.m_containingType.MetadataTokenInternal, this.m_strName, signature, sigLength, this.Attributes);
			this.m_tkMethod = new MethodToken(num);
			if (this.m_inst != null)
			{
				foreach (GenericTypeParameterBuilder genericTypeParameterBuilder in this.m_inst)
				{
					if (!genericTypeParameterBuilder.m_type.IsCreated())
					{
						genericTypeParameterBuilder.m_type.CreateType();
					}
				}
			}
			TypeBuilder.SetMethodImpl(this.m_module.GetNativeHandle(), num, this.m_dwMethodImplFlags);
			return this.m_tkMethod;
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x0010C264 File Offset: 0x0010A464
		public void SetParameters(params Type[] parameterTypes)
		{
			this.CheckContext(parameterTypes);
			this.SetSignature(null, null, null, parameterTypes, null, null);
		}

		// Token: 0x06004A33 RID: 18995 RVA: 0x0010C279 File Offset: 0x0010A479
		public void SetReturnType(Type returnType)
		{
			this.CheckContext(new Type[]
			{
				returnType
			});
			this.SetSignature(returnType, null, null, null, null, null);
		}

		// Token: 0x06004A34 RID: 18996 RVA: 0x0010C298 File Offset: 0x0010A498
		public void SetSignature(Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			if (this.m_tkMethod.Token != 0)
			{
				return;
			}
			this.CheckContext(new Type[]
			{
				returnType
			});
			this.CheckContext(new Type[][]
			{
				returnTypeRequiredCustomModifiers,
				returnTypeOptionalCustomModifiers,
				parameterTypes
			});
			this.CheckContext(parameterTypeRequiredCustomModifiers);
			this.CheckContext(parameterTypeOptionalCustomModifiers);
			this.ThrowIfGeneric();
			if (returnType != null)
			{
				this.m_returnType = returnType;
			}
			if (parameterTypes != null)
			{
				this.m_parameterTypes = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.m_parameterTypes, parameterTypes.Length);
			}
			this.m_returnTypeRequiredCustomModifiers = returnTypeRequiredCustomModifiers;
			this.m_returnTypeOptionalCustomModifiers = returnTypeOptionalCustomModifiers;
			this.m_parameterTypeRequiredCustomModifiers = parameterTypeRequiredCustomModifiers;
			this.m_parameterTypeOptionalCustomModifiers = parameterTypeOptionalCustomModifiers;
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x0010C344 File Offset: 0x0010A544
		[SecuritySafeCritical]
		public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string strParamName)
		{
			if (position < 0)
			{
				throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_ParamSequence"));
			}
			this.ThrowIfGeneric();
			this.m_containingType.ThrowIfCreated();
			if (position > 0 && (this.m_parameterTypes == null || position > this.m_parameterTypes.Length))
			{
				throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_ParamSequence"));
			}
			attributes &= ~ParameterAttributes.ReservedMask;
			return new ParameterBuilder(this, position, attributes, strParamName);
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x0010C3AF File Offset: 0x0010A5AF
		[SecuritySafeCritical]
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			this.ThrowIfGeneric();
			this.m_containingType.ThrowIfCreated();
			if (this.m_retParam == null)
			{
				this.m_retParam = new ParameterBuilder(this, 0, ParameterAttributes.None, null);
			}
			this.m_retParam.SetMarshal(unmanagedMarshal);
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x0010C3E8 File Offset: 0x0010A5E8
		public void SetSymCustomAttribute(string name, byte[] data)
		{
			this.ThrowIfGeneric();
			this.m_containingType.ThrowIfCreated();
			ModuleBuilder module = this.m_module;
			if (module.GetSymWriter() == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
			}
			if (this.m_symCustomAttrs == null)
			{
				this.m_symCustomAttrs = new List<MethodBuilder.SymCustomAttr>();
			}
			this.m_symCustomAttrs.Add(new MethodBuilder.SymCustomAttr(name, data));
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x0010C44C File Offset: 0x0010A64C
		[SecuritySafeCritical]
		public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
		{
			if (pset == null)
			{
				throw new ArgumentNullException("pset");
			}
			this.ThrowIfGeneric();
			if (!Enum.IsDefined(typeof(SecurityAction), action) || action == SecurityAction.RequestMinimum || action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse)
			{
				throw new ArgumentOutOfRangeException("action");
			}
			this.m_containingType.ThrowIfCreated();
			byte[] array = null;
			int cb = 0;
			if (!pset.IsEmpty())
			{
				array = pset.EncodeXml();
				cb = array.Length;
			}
			TypeBuilder.AddDeclarativeSecurity(this.m_module.GetNativeHandle(), this.MetadataTokenInternal, action, array, cb);
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x0010C4D8 File Offset: 0x0010A6D8
		public void SetMethodBody(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
		{
			if (il == null)
			{
				throw new ArgumentNullException("il", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (maxStack < 0)
			{
				throw new ArgumentOutOfRangeException("maxStack", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this.m_bIsBaked)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBaked"));
			}
			this.m_containingType.ThrowIfCreated();
			this.ThrowIfGeneric();
			byte[] localSignature2 = null;
			ExceptionHandler[] array = null;
			int[] array2 = null;
			byte[] array3 = (byte[])il.Clone();
			if (localSignature != null)
			{
				localSignature2 = (byte[])localSignature.Clone();
			}
			if (exceptionHandlers != null)
			{
				array = MethodBuilder.ToArray<ExceptionHandler>(exceptionHandlers);
				MethodBuilder.CheckExceptionHandlerRanges(array, array3.Length);
			}
			if (tokenFixups != null)
			{
				array2 = MethodBuilder.ToArray<int>(tokenFixups);
				int num = array3.Length - 4;
				for (int i = 0; i < array2.Length; i++)
				{
					if (array2[i] < 0 || array2[i] > num)
					{
						throw new ArgumentOutOfRangeException("tokenFixups[" + i + "]", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
						{
							0,
							num
						}));
					}
				}
			}
			this.m_ubBody = array3;
			this.m_localSignature = localSignature2;
			this.m_exceptions = array;
			this.m_mdMethodFixups = array2;
			this.m_maxStack = maxStack;
			this.m_ilGenerator = null;
			this.m_bIsBaked = true;
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x0010C61C File Offset: 0x0010A81C
		private static T[] ToArray<T>(IEnumerable<T> sequence)
		{
			T[] array = sequence as T[];
			if (array != null)
			{
				return (T[])array.Clone();
			}
			return new List<T>(sequence).ToArray();
		}

		// Token: 0x06004A3B RID: 19003 RVA: 0x0010C64C File Offset: 0x0010A84C
		private static void CheckExceptionHandlerRanges(ExceptionHandler[] exceptionHandlers, int maxOffset)
		{
			for (int i = 0; i < exceptionHandlers.Length; i++)
			{
				ExceptionHandler exceptionHandler = exceptionHandlers[i];
				if (exceptionHandler.m_filterOffset > maxOffset || exceptionHandler.m_tryEndOffset > maxOffset || exceptionHandler.m_handlerEndOffset > maxOffset)
				{
					throw new ArgumentOutOfRangeException("exceptionHandlers[" + i + "]", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
					{
						0,
						maxOffset
					}));
				}
				if (exceptionHandler.Kind == ExceptionHandlingClauseOptions.Clause && exceptionHandler.ExceptionTypeToken == 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTypeToken", new object[]
					{
						exceptionHandler.ExceptionTypeToken
					}), "exceptionHandlers[" + i + "]");
				}
			}
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x0010C71C File Offset: 0x0010A91C
		public void CreateMethodBody(byte[] il, int count)
		{
			this.ThrowIfGeneric();
			if (this.m_bIsBaked)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBaked"));
			}
			this.m_containingType.ThrowIfCreated();
			if (il != null && (count < 0 || count > il.Length))
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (il == null)
			{
				this.m_ubBody = null;
				return;
			}
			this.m_ubBody = new byte[count];
			Array.Copy(il, this.m_ubBody, count);
			this.m_localSignature = null;
			this.m_exceptions = null;
			this.m_mdMethodFixups = null;
			this.m_maxStack = 16;
			this.m_bIsBaked = true;
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x0010C7BC File Offset: 0x0010A9BC
		[SecuritySafeCritical]
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			this.ThrowIfGeneric();
			this.m_containingType.ThrowIfCreated();
			this.m_dwMethodImplFlags = attributes;
			this.m_canBeRuntimeImpl = true;
			TypeBuilder.SetMethodImpl(this.m_module.GetNativeHandle(), this.MetadataTokenInternal, attributes);
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x0010C7F4 File Offset: 0x0010A9F4
		public ILGenerator GetILGenerator()
		{
			this.ThrowIfGeneric();
			this.ThrowIfShouldNotHaveBody();
			if (this.m_ilGenerator == null)
			{
				this.m_ilGenerator = new ILGenerator(this);
			}
			return this.m_ilGenerator;
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x0010C81C File Offset: 0x0010AA1C
		public ILGenerator GetILGenerator(int size)
		{
			this.ThrowIfGeneric();
			this.ThrowIfShouldNotHaveBody();
			if (this.m_ilGenerator == null)
			{
				this.m_ilGenerator = new ILGenerator(this, size);
			}
			return this.m_ilGenerator;
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x0010C845 File Offset: 0x0010AA45
		private void ThrowIfShouldNotHaveBody()
		{
			if ((this.m_dwMethodImplFlags & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL || (this.m_dwMethodImplFlags & MethodImplAttributes.ManagedMask) != MethodImplAttributes.IL || (this.m_iAttributes & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope || this.m_isDllImport)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ShouldNotHaveMethodBody"));
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06004A41 RID: 19009 RVA: 0x0010C881 File Offset: 0x0010AA81
		// (set) Token: 0x06004A42 RID: 19010 RVA: 0x0010C88F File Offset: 0x0010AA8F
		public bool InitLocals
		{
			get
			{
				this.ThrowIfGeneric();
				return this.m_fInitLocals;
			}
			set
			{
				this.ThrowIfGeneric();
				this.m_fInitLocals = value;
			}
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x0010C89E File Offset: 0x0010AA9E
		public Module GetModule()
		{
			return this.GetModuleBuilder();
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06004A44 RID: 19012 RVA: 0x0010C8A6 File Offset: 0x0010AAA6
		public string Signature
		{
			[SecuritySafeCritical]
			get
			{
				return this.GetMethodSignature().ToString();
			}
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x0010C8B4 File Offset: 0x0010AAB4
		[SecuritySafeCritical]
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.ThrowIfGeneric();
			TypeBuilder.DefineCustomAttribute(this.m_module, this.MetadataTokenInternal, this.m_module.GetConstructorToken(con).Token, binaryAttribute, false, false);
			if (this.IsKnownCA(con))
			{
				this.ParseCA(con, binaryAttribute);
			}
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x0010C924 File Offset: 0x0010AB24
		[SecuritySafeCritical]
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.ThrowIfGeneric();
			customBuilder.CreateCustomAttribute(this.m_module, this.MetadataTokenInternal);
			if (this.IsKnownCA(customBuilder.m_con))
			{
				this.ParseCA(customBuilder.m_con, customBuilder.m_blob);
			}
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x0010C978 File Offset: 0x0010AB78
		private bool IsKnownCA(ConstructorInfo con)
		{
			Type declaringType = con.DeclaringType;
			return declaringType == typeof(MethodImplAttribute) || declaringType == typeof(DllImportAttribute);
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x0010C9B8 File Offset: 0x0010ABB8
		private void ParseCA(ConstructorInfo con, byte[] blob)
		{
			Type declaringType = con.DeclaringType;
			if (declaringType == typeof(MethodImplAttribute))
			{
				this.m_canBeRuntimeImpl = true;
				return;
			}
			if (declaringType == typeof(DllImportAttribute))
			{
				this.m_canBeRuntimeImpl = true;
				this.m_isDllImport = true;
			}
		}

		// Token: 0x06004A49 RID: 19017 RVA: 0x0010CA06 File Offset: 0x0010AC06
		void _MethodBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x0010CA0D File Offset: 0x0010AC0D
		void _MethodBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x0010CA14 File Offset: 0x0010AC14
		void _MethodBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x0010CA1B File Offset: 0x0010AC1B
		void _MethodBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001E4F RID: 7759
		internal string m_strName;

		// Token: 0x04001E50 RID: 7760
		private MethodToken m_tkMethod;

		// Token: 0x04001E51 RID: 7761
		private ModuleBuilder m_module;

		// Token: 0x04001E52 RID: 7762
		internal TypeBuilder m_containingType;

		// Token: 0x04001E53 RID: 7763
		private int[] m_mdMethodFixups;

		// Token: 0x04001E54 RID: 7764
		private byte[] m_localSignature;

		// Token: 0x04001E55 RID: 7765
		internal LocalSymInfo m_localSymInfo;

		// Token: 0x04001E56 RID: 7766
		internal ILGenerator m_ilGenerator;

		// Token: 0x04001E57 RID: 7767
		private byte[] m_ubBody;

		// Token: 0x04001E58 RID: 7768
		private ExceptionHandler[] m_exceptions;

		// Token: 0x04001E59 RID: 7769
		private const int DefaultMaxStack = 16;

		// Token: 0x04001E5A RID: 7770
		private int m_maxStack = 16;

		// Token: 0x04001E5B RID: 7771
		internal bool m_bIsBaked;

		// Token: 0x04001E5C RID: 7772
		private bool m_bIsGlobalMethod;

		// Token: 0x04001E5D RID: 7773
		private bool m_fInitLocals;

		// Token: 0x04001E5E RID: 7774
		private MethodAttributes m_iAttributes;

		// Token: 0x04001E5F RID: 7775
		private CallingConventions m_callingConvention;

		// Token: 0x04001E60 RID: 7776
		private MethodImplAttributes m_dwMethodImplFlags;

		// Token: 0x04001E61 RID: 7777
		private SignatureHelper m_signature;

		// Token: 0x04001E62 RID: 7778
		internal Type[] m_parameterTypes;

		// Token: 0x04001E63 RID: 7779
		private ParameterBuilder m_retParam;

		// Token: 0x04001E64 RID: 7780
		private Type m_returnType;

		// Token: 0x04001E65 RID: 7781
		private Type[] m_returnTypeRequiredCustomModifiers;

		// Token: 0x04001E66 RID: 7782
		private Type[] m_returnTypeOptionalCustomModifiers;

		// Token: 0x04001E67 RID: 7783
		private Type[][] m_parameterTypeRequiredCustomModifiers;

		// Token: 0x04001E68 RID: 7784
		private Type[][] m_parameterTypeOptionalCustomModifiers;

		// Token: 0x04001E69 RID: 7785
		private GenericTypeParameterBuilder[] m_inst;

		// Token: 0x04001E6A RID: 7786
		private bool m_bIsGenMethDef;

		// Token: 0x04001E6B RID: 7787
		private List<MethodBuilder.SymCustomAttr> m_symCustomAttrs;

		// Token: 0x04001E6C RID: 7788
		internal bool m_canBeRuntimeImpl;

		// Token: 0x04001E6D RID: 7789
		internal bool m_isDllImport;

		// Token: 0x02000C0D RID: 3085
		private struct SymCustomAttr
		{
			// Token: 0x06006F3D RID: 28477 RVA: 0x0017E642 File Offset: 0x0017C842
			public SymCustomAttr(string name, byte[] data)
			{
				this.m_name = name;
				this.m_data = data;
			}

			// Token: 0x04003673 RID: 13939
			public string m_name;

			// Token: 0x04003674 RID: 13940
			public byte[] m_data;
		}
	}
}
