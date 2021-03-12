using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x02000632 RID: 1586
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_SignatureHelper))]
	[ComVisible(true)]
	public sealed class SignatureHelper : _SignatureHelper
	{
		// Token: 0x06004BDB RID: 19419 RVA: 0x00112945 File Offset: 0x00110B45
		[SecuritySafeCritical]
		public static SignatureHelper GetMethodSigHelper(Module mod, Type returnType, Type[] parameterTypes)
		{
			return SignatureHelper.GetMethodSigHelper(mod, CallingConventions.Standard, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x00112954 File Offset: 0x00110B54
		[SecurityCritical]
		internal static SignatureHelper GetMethodSigHelper(Module mod, CallingConventions callingConvention, Type returnType, int cGenericParam)
		{
			return SignatureHelper.GetMethodSigHelper(mod, callingConvention, cGenericParam, returnType, null, null, null, null, null);
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x0011296F File Offset: 0x00110B6F
		[SecuritySafeCritical]
		public static SignatureHelper GetMethodSigHelper(Module mod, CallingConventions callingConvention, Type returnType)
		{
			return SignatureHelper.GetMethodSigHelper(mod, callingConvention, returnType, null, null, null, null, null);
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x00112980 File Offset: 0x00110B80
		internal static SignatureHelper GetMethodSpecSigHelper(Module scope, Type[] inst)
		{
			SignatureHelper signatureHelper = new SignatureHelper(scope, MdSigCallingConvention.GenericInst);
			signatureHelper.AddData(inst.Length);
			foreach (Type clsArgument in inst)
			{
				signatureHelper.AddArgument(clsArgument);
			}
			return signatureHelper;
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x001129BC File Offset: 0x00110BBC
		[SecurityCritical]
		internal static SignatureHelper GetMethodSigHelper(Module scope, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
		{
			return SignatureHelper.GetMethodSigHelper(scope, callingConvention, 0, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x001129DC File Offset: 0x00110BDC
		[SecurityCritical]
		internal static SignatureHelper GetMethodSigHelper(Module scope, CallingConventions callingConvention, int cGenericParam, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
		{
			if (returnType == null)
			{
				returnType = typeof(void);
			}
			MdSigCallingConvention mdSigCallingConvention = MdSigCallingConvention.Default;
			if ((callingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
			{
				mdSigCallingConvention = MdSigCallingConvention.Vararg;
			}
			if (cGenericParam > 0)
			{
				mdSigCallingConvention |= MdSigCallingConvention.Generic;
			}
			if ((callingConvention & CallingConventions.HasThis) == CallingConventions.HasThis)
			{
				mdSigCallingConvention |= MdSigCallingConvention.HasThis;
			}
			SignatureHelper signatureHelper = new SignatureHelper(scope, mdSigCallingConvention, cGenericParam, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers);
			signatureHelper.AddArguments(parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
			return signatureHelper;
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x00112A3C File Offset: 0x00110C3C
		[SecuritySafeCritical]
		public static SignatureHelper GetMethodSigHelper(Module mod, CallingConvention unmanagedCallConv, Type returnType)
		{
			if (returnType == null)
			{
				returnType = typeof(void);
			}
			MdSigCallingConvention callingConvention;
			if (unmanagedCallConv == CallingConvention.Cdecl)
			{
				callingConvention = MdSigCallingConvention.C;
			}
			else if (unmanagedCallConv == CallingConvention.StdCall || unmanagedCallConv == CallingConvention.Winapi)
			{
				callingConvention = MdSigCallingConvention.StdCall;
			}
			else if (unmanagedCallConv == CallingConvention.ThisCall)
			{
				callingConvention = MdSigCallingConvention.ThisCall;
			}
			else
			{
				if (unmanagedCallConv != CallingConvention.FastCall)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_UnknownUnmanagedCallConv"), "unmanagedCallConv");
				}
				callingConvention = MdSigCallingConvention.FastCall;
			}
			return new SignatureHelper(mod, callingConvention, returnType, null, null);
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x00112AA3 File Offset: 0x00110CA3
		public static SignatureHelper GetLocalVarSigHelper()
		{
			return SignatureHelper.GetLocalVarSigHelper(null);
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x00112AAB File Offset: 0x00110CAB
		public static SignatureHelper GetMethodSigHelper(CallingConventions callingConvention, Type returnType)
		{
			return SignatureHelper.GetMethodSigHelper(null, callingConvention, returnType);
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x00112AB5 File Offset: 0x00110CB5
		public static SignatureHelper GetMethodSigHelper(CallingConvention unmanagedCallingConvention, Type returnType)
		{
			return SignatureHelper.GetMethodSigHelper(null, unmanagedCallingConvention, returnType);
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x00112ABF File Offset: 0x00110CBF
		public static SignatureHelper GetLocalVarSigHelper(Module mod)
		{
			return new SignatureHelper(mod, MdSigCallingConvention.LocalSig);
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x00112AC8 File Offset: 0x00110CC8
		public static SignatureHelper GetFieldSigHelper(Module mod)
		{
			return new SignatureHelper(mod, MdSigCallingConvention.Field);
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x00112AD1 File Offset: 0x00110CD1
		public static SignatureHelper GetPropertySigHelper(Module mod, Type returnType, Type[] parameterTypes)
		{
			return SignatureHelper.GetPropertySigHelper(mod, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x00112ADF File Offset: 0x00110CDF
		public static SignatureHelper GetPropertySigHelper(Module mod, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
		{
			return SignatureHelper.GetPropertySigHelper(mod, (CallingConventions)0, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x00112AF4 File Offset: 0x00110CF4
		[SecuritySafeCritical]
		public static SignatureHelper GetPropertySigHelper(Module mod, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
		{
			if (returnType == null)
			{
				returnType = typeof(void);
			}
			MdSigCallingConvention mdSigCallingConvention = MdSigCallingConvention.Property;
			if ((callingConvention & CallingConventions.HasThis) == CallingConventions.HasThis)
			{
				mdSigCallingConvention |= MdSigCallingConvention.HasThis;
			}
			SignatureHelper signatureHelper = new SignatureHelper(mod, mdSigCallingConvention, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers);
			signatureHelper.AddArguments(parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
			return signatureHelper;
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x00112B3E File Offset: 0x00110D3E
		[SecurityCritical]
		internal static SignatureHelper GetTypeSigToken(Module mod, Type type)
		{
			if (mod == null)
			{
				throw new ArgumentNullException("module");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return new SignatureHelper(mod, type);
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x00112B6F File Offset: 0x00110D6F
		private SignatureHelper(Module mod, MdSigCallingConvention callingConvention)
		{
			this.Init(mod, callingConvention);
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x00112B7F File Offset: 0x00110D7F
		[SecurityCritical]
		private SignatureHelper(Module mod, MdSigCallingConvention callingConvention, int cGenericParameters, Type returnType, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
		{
			this.Init(mod, callingConvention, cGenericParameters);
			if (callingConvention == MdSigCallingConvention.Field)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadFieldSig"));
			}
			this.AddOneArgTypeHelper(returnType, requiredCustomModifiers, optionalCustomModifiers);
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x00112BB0 File Offset: 0x00110DB0
		[SecurityCritical]
		private SignatureHelper(Module mod, MdSigCallingConvention callingConvention, Type returnType, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers) : this(mod, callingConvention, 0, returnType, requiredCustomModifiers, optionalCustomModifiers)
		{
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x00112BC0 File Offset: 0x00110DC0
		[SecurityCritical]
		private SignatureHelper(Module mod, Type type)
		{
			this.Init(mod);
			this.AddOneArgTypeHelper(type);
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x00112BD8 File Offset: 0x00110DD8
		private void Init(Module mod)
		{
			this.m_signature = new byte[32];
			this.m_currSig = 0;
			this.m_module = (mod as ModuleBuilder);
			this.m_argCount = 0;
			this.m_sigDone = false;
			this.m_sizeLoc = -1;
			if (this.m_module == null && mod != null)
			{
				throw new ArgumentException(Environment.GetResourceString("NotSupported_MustBeModuleBuilder"));
			}
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x00112C41 File Offset: 0x00110E41
		private void Init(Module mod, MdSigCallingConvention callingConvention)
		{
			this.Init(mod, callingConvention, 0);
		}

		// Token: 0x06004BF1 RID: 19441 RVA: 0x00112C4C File Offset: 0x00110E4C
		private void Init(Module mod, MdSigCallingConvention callingConvention, int cGenericParam)
		{
			this.Init(mod);
			this.AddData((int)callingConvention);
			if (callingConvention == MdSigCallingConvention.Field || callingConvention == MdSigCallingConvention.GenericInst)
			{
				this.m_sizeLoc = -1;
				return;
			}
			if (cGenericParam > 0)
			{
				this.AddData(cGenericParam);
			}
			int currSig = this.m_currSig;
			this.m_currSig = currSig + 1;
			this.m_sizeLoc = currSig;
		}

		// Token: 0x06004BF2 RID: 19442 RVA: 0x00112C9A File Offset: 0x00110E9A
		[SecurityCritical]
		private void AddOneArgTypeHelper(Type argument, bool pinned)
		{
			if (pinned)
			{
				this.AddElementType(CorElementType.Pinned);
			}
			this.AddOneArgTypeHelper(argument);
		}

		// Token: 0x06004BF3 RID: 19443 RVA: 0x00112CB0 File Offset: 0x00110EB0
		[SecurityCritical]
		private void AddOneArgTypeHelper(Type clsArgument, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
		{
			if (optionalCustomModifiers != null)
			{
				foreach (Type type in optionalCustomModifiers)
				{
					if (type == null)
					{
						throw new ArgumentNullException("optionalCustomModifiers");
					}
					if (type.HasElementType)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_ArraysInvalid"), "optionalCustomModifiers");
					}
					if (type.ContainsGenericParameters)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_GenericsInvalid"), "optionalCustomModifiers");
					}
					this.AddElementType(CorElementType.CModOpt);
					int token = this.m_module.GetTypeToken(type).Token;
					this.AddToken(token);
				}
			}
			if (requiredCustomModifiers != null)
			{
				foreach (Type type2 in requiredCustomModifiers)
				{
					if (type2 == null)
					{
						throw new ArgumentNullException("requiredCustomModifiers");
					}
					if (type2.HasElementType)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_ArraysInvalid"), "requiredCustomModifiers");
					}
					if (type2.ContainsGenericParameters)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_GenericsInvalid"), "requiredCustomModifiers");
					}
					this.AddElementType(CorElementType.CModReqd);
					int token2 = this.m_module.GetTypeToken(type2).Token;
					this.AddToken(token2);
				}
			}
			this.AddOneArgTypeHelper(clsArgument);
		}

		// Token: 0x06004BF4 RID: 19444 RVA: 0x00112DEA File Offset: 0x00110FEA
		[SecurityCritical]
		private void AddOneArgTypeHelper(Type clsArgument)
		{
			this.AddOneArgTypeHelperWorker(clsArgument, false);
		}

		// Token: 0x06004BF5 RID: 19445 RVA: 0x00112DF4 File Offset: 0x00110FF4
		[SecurityCritical]
		private void AddOneArgTypeHelperWorker(Type clsArgument, bool lastWasGenericInst)
		{
			if (clsArgument.IsGenericParameter)
			{
				if (clsArgument.DeclaringMethod != null)
				{
					this.AddElementType(CorElementType.MVar);
				}
				else
				{
					this.AddElementType(CorElementType.Var);
				}
				this.AddData(clsArgument.GenericParameterPosition);
				return;
			}
			if (clsArgument.IsGenericType && (!clsArgument.IsGenericTypeDefinition || !lastWasGenericInst))
			{
				this.AddElementType(CorElementType.GenericInst);
				this.AddOneArgTypeHelperWorker(clsArgument.GetGenericTypeDefinition(), true);
				Type[] genericArguments = clsArgument.GetGenericArguments();
				this.AddData(genericArguments.Length);
				foreach (Type clsArgument2 in genericArguments)
				{
					this.AddOneArgTypeHelper(clsArgument2);
				}
				return;
			}
			if (clsArgument is TypeBuilder)
			{
				TypeBuilder typeBuilder = (TypeBuilder)clsArgument;
				TypeToken typeToken;
				if (typeBuilder.Module.Equals(this.m_module))
				{
					typeToken = typeBuilder.TypeToken;
				}
				else
				{
					typeToken = this.m_module.GetTypeToken(clsArgument);
				}
				if (clsArgument.IsValueType)
				{
					this.InternalAddTypeToken(typeToken, CorElementType.ValueType);
					return;
				}
				this.InternalAddTypeToken(typeToken, CorElementType.Class);
				return;
			}
			else if (clsArgument is EnumBuilder)
			{
				TypeBuilder typeBuilder2 = ((EnumBuilder)clsArgument).m_typeBuilder;
				TypeToken typeToken2;
				if (typeBuilder2.Module.Equals(this.m_module))
				{
					typeToken2 = typeBuilder2.TypeToken;
				}
				else
				{
					typeToken2 = this.m_module.GetTypeToken(clsArgument);
				}
				if (clsArgument.IsValueType)
				{
					this.InternalAddTypeToken(typeToken2, CorElementType.ValueType);
					return;
				}
				this.InternalAddTypeToken(typeToken2, CorElementType.Class);
				return;
			}
			else
			{
				if (clsArgument.IsByRef)
				{
					this.AddElementType(CorElementType.ByRef);
					clsArgument = clsArgument.GetElementType();
					this.AddOneArgTypeHelper(clsArgument);
					return;
				}
				if (clsArgument.IsPointer)
				{
					this.AddElementType(CorElementType.Ptr);
					this.AddOneArgTypeHelper(clsArgument.GetElementType());
					return;
				}
				if (clsArgument.IsArray)
				{
					if (clsArgument.IsSzArray)
					{
						this.AddElementType(CorElementType.SzArray);
						this.AddOneArgTypeHelper(clsArgument.GetElementType());
						return;
					}
					this.AddElementType(CorElementType.Array);
					this.AddOneArgTypeHelper(clsArgument.GetElementType());
					int arrayRank = clsArgument.GetArrayRank();
					this.AddData(arrayRank);
					this.AddData(0);
					this.AddData(arrayRank);
					for (int j = 0; j < arrayRank; j++)
					{
						this.AddData(0);
					}
					return;
				}
				else
				{
					CorElementType corElementType = CorElementType.Max;
					if (clsArgument is RuntimeType)
					{
						corElementType = RuntimeTypeHandle.GetCorElementType((RuntimeType)clsArgument);
						if (corElementType == CorElementType.Class)
						{
							if (clsArgument == typeof(object))
							{
								corElementType = CorElementType.Object;
							}
							else if (clsArgument == typeof(string))
							{
								corElementType = CorElementType.String;
							}
						}
					}
					if (SignatureHelper.IsSimpleType(corElementType))
					{
						this.AddElementType(corElementType);
						return;
					}
					if (this.m_module == null)
					{
						this.InternalAddRuntimeType(clsArgument);
						return;
					}
					if (clsArgument.IsValueType)
					{
						this.InternalAddTypeToken(this.m_module.GetTypeToken(clsArgument), CorElementType.ValueType);
						return;
					}
					this.InternalAddTypeToken(this.m_module.GetTypeToken(clsArgument), CorElementType.Class);
					return;
				}
			}
		}

		// Token: 0x06004BF6 RID: 19446 RVA: 0x0011309C File Offset: 0x0011129C
		private void AddData(int data)
		{
			if (this.m_currSig + 4 > this.m_signature.Length)
			{
				this.m_signature = this.ExpandArray(this.m_signature);
			}
			if (data <= 127)
			{
				byte[] signature = this.m_signature;
				int currSig = this.m_currSig;
				this.m_currSig = currSig + 1;
				signature[currSig] = (byte)(data & 255);
				return;
			}
			if (data <= 16383)
			{
				byte[] signature2 = this.m_signature;
				int currSig = this.m_currSig;
				this.m_currSig = currSig + 1;
				signature2[currSig] = (byte)(data >> 8 | 128);
				byte[] signature3 = this.m_signature;
				currSig = this.m_currSig;
				this.m_currSig = currSig + 1;
				signature3[currSig] = (byte)(data & 255);
				return;
			}
			if (data <= 536870911)
			{
				byte[] signature4 = this.m_signature;
				int currSig = this.m_currSig;
				this.m_currSig = currSig + 1;
				signature4[currSig] = (byte)(data >> 24 | 192);
				byte[] signature5 = this.m_signature;
				currSig = this.m_currSig;
				this.m_currSig = currSig + 1;
				signature5[currSig] = (byte)(data >> 16 & 255);
				byte[] signature6 = this.m_signature;
				currSig = this.m_currSig;
				this.m_currSig = currSig + 1;
				signature6[currSig] = (byte)(data >> 8 & 255);
				byte[] signature7 = this.m_signature;
				currSig = this.m_currSig;
				this.m_currSig = currSig + 1;
				signature7[currSig] = (byte)(data & 255);
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_LargeInteger"));
		}

		// Token: 0x06004BF7 RID: 19447 RVA: 0x001131E4 File Offset: 0x001113E4
		private void AddData(uint data)
		{
			if (this.m_currSig + 4 > this.m_signature.Length)
			{
				this.m_signature = this.ExpandArray(this.m_signature);
			}
			byte[] signature = this.m_signature;
			int currSig = this.m_currSig;
			this.m_currSig = currSig + 1;
			signature[currSig] = (byte)(data & 255U);
			byte[] signature2 = this.m_signature;
			currSig = this.m_currSig;
			this.m_currSig = currSig + 1;
			signature2[currSig] = (byte)(data >> 8 & 255U);
			byte[] signature3 = this.m_signature;
			currSig = this.m_currSig;
			this.m_currSig = currSig + 1;
			signature3[currSig] = (byte)(data >> 16 & 255U);
			byte[] signature4 = this.m_signature;
			currSig = this.m_currSig;
			this.m_currSig = currSig + 1;
			signature4[currSig] = (byte)(data >> 24 & 255U);
		}

		// Token: 0x06004BF8 RID: 19448 RVA: 0x001132A0 File Offset: 0x001114A0
		private void AddData(ulong data)
		{
			if (this.m_currSig + 8 > this.m_signature.Length)
			{
				this.m_signature = this.ExpandArray(this.m_signature);
			}
			byte[] signature = this.m_signature;
			int currSig = this.m_currSig;
			this.m_currSig = currSig + 1;
			signature[currSig] = (byte)(data & 255UL);
			byte[] signature2 = this.m_signature;
			currSig = this.m_currSig;
			this.m_currSig = currSig + 1;
			signature2[currSig] = (byte)(data >> 8 & 255UL);
			byte[] signature3 = this.m_signature;
			currSig = this.m_currSig;
			this.m_currSig = currSig + 1;
			signature3[currSig] = (byte)(data >> 16 & 255UL);
			byte[] signature4 = this.m_signature;
			currSig = this.m_currSig;
			this.m_currSig = currSig + 1;
			signature4[currSig] = (byte)(data >> 24 & 255UL);
			byte[] signature5 = this.m_signature;
			currSig = this.m_currSig;
			this.m_currSig = currSig + 1;
			signature5[currSig] = (byte)(data >> 32 & 255UL);
			byte[] signature6 = this.m_signature;
			currSig = this.m_currSig;
			this.m_currSig = currSig + 1;
			signature6[currSig] = (byte)(data >> 40 & 255UL);
			byte[] signature7 = this.m_signature;
			currSig = this.m_currSig;
			this.m_currSig = currSig + 1;
			signature7[currSig] = (byte)(data >> 48 & 255UL);
			byte[] signature8 = this.m_signature;
			currSig = this.m_currSig;
			this.m_currSig = currSig + 1;
			signature8[currSig] = (byte)(data >> 56 & 255UL);
		}

		// Token: 0x06004BF9 RID: 19449 RVA: 0x001133F0 File Offset: 0x001115F0
		private void AddElementType(CorElementType cvt)
		{
			if (this.m_currSig + 1 > this.m_signature.Length)
			{
				this.m_signature = this.ExpandArray(this.m_signature);
			}
			byte[] signature = this.m_signature;
			int currSig = this.m_currSig;
			this.m_currSig = currSig + 1;
			signature[currSig] = cvt;
		}

		// Token: 0x06004BFA RID: 19450 RVA: 0x0011343C File Offset: 0x0011163C
		private void AddToken(int token)
		{
			int num = token & 16777215;
			MetadataTokenType metadataTokenType = (MetadataTokenType)(token & -16777216);
			if (num > 67108863)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_LargeInteger"));
			}
			num <<= 2;
			if (metadataTokenType == MetadataTokenType.TypeRef)
			{
				num |= 1;
			}
			else if (metadataTokenType == MetadataTokenType.TypeSpec)
			{
				num |= 2;
			}
			this.AddData(num);
		}

		// Token: 0x06004BFB RID: 19451 RVA: 0x00113496 File Offset: 0x00111696
		private void InternalAddTypeToken(TypeToken clsToken, CorElementType CorType)
		{
			this.AddElementType(CorType);
			this.AddToken(clsToken.Token);
		}

		// Token: 0x06004BFC RID: 19452 RVA: 0x001134AC File Offset: 0x001116AC
		[SecurityCritical]
		private unsafe void InternalAddRuntimeType(Type type)
		{
			this.AddElementType(CorElementType.Internal);
			IntPtr value = type.GetTypeHandleInternal().Value;
			if (this.m_currSig + sizeof(void*) > this.m_signature.Length)
			{
				this.m_signature = this.ExpandArray(this.m_signature);
			}
			byte* ptr = (byte*)(&value);
			for (int i = 0; i < sizeof(void*); i++)
			{
				byte[] signature = this.m_signature;
				int currSig = this.m_currSig;
				this.m_currSig = currSig + 1;
				signature[currSig] = ptr[i];
			}
		}

		// Token: 0x06004BFD RID: 19453 RVA: 0x0011352D File Offset: 0x0011172D
		private byte[] ExpandArray(byte[] inArray)
		{
			return this.ExpandArray(inArray, inArray.Length * 2);
		}

		// Token: 0x06004BFE RID: 19454 RVA: 0x0011353C File Offset: 0x0011173C
		private byte[] ExpandArray(byte[] inArray, int requiredLength)
		{
			if (requiredLength < inArray.Length)
			{
				requiredLength = inArray.Length * 2;
			}
			byte[] array = new byte[requiredLength];
			Array.Copy(inArray, array, inArray.Length);
			return array;
		}

		// Token: 0x06004BFF RID: 19455 RVA: 0x00113568 File Offset: 0x00111768
		private void IncrementArgCounts()
		{
			if (this.m_sizeLoc == -1)
			{
				return;
			}
			this.m_argCount++;
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x00113584 File Offset: 0x00111784
		private void SetNumberOfSignatureElements(bool forceCopy)
		{
			int currSig = this.m_currSig;
			if (this.m_sizeLoc == -1)
			{
				return;
			}
			if (this.m_argCount < 128 && !forceCopy)
			{
				this.m_signature[this.m_sizeLoc] = (byte)this.m_argCount;
				return;
			}
			int num;
			if (this.m_argCount < 128)
			{
				num = 1;
			}
			else if (this.m_argCount < 16384)
			{
				num = 2;
			}
			else
			{
				num = 4;
			}
			byte[] array = new byte[this.m_currSig + num - 1];
			array[0] = this.m_signature[0];
			Array.Copy(this.m_signature, this.m_sizeLoc + 1, array, this.m_sizeLoc + num, currSig - (this.m_sizeLoc + 1));
			this.m_signature = array;
			this.m_currSig = this.m_sizeLoc;
			this.AddData(this.m_argCount);
			this.m_currSig = currSig + (num - 1);
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06004C01 RID: 19457 RVA: 0x00113656 File Offset: 0x00111856
		internal int ArgumentCount
		{
			get
			{
				return this.m_argCount;
			}
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x0011365E File Offset: 0x0011185E
		internal static bool IsSimpleType(CorElementType type)
		{
			return type <= CorElementType.String || (type == CorElementType.TypedByRef || type == CorElementType.I || type == CorElementType.U || type == CorElementType.Object);
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x0011367E File Offset: 0x0011187E
		internal byte[] InternalGetSignature(out int length)
		{
			if (!this.m_sigDone)
			{
				this.m_sigDone = true;
				this.SetNumberOfSignatureElements(false);
			}
			length = this.m_currSig;
			return this.m_signature;
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x001136A4 File Offset: 0x001118A4
		internal byte[] InternalGetSignatureArray()
		{
			int argCount = this.m_argCount;
			int currSig = this.m_currSig;
			int num = currSig;
			if (argCount < 127)
			{
				num++;
			}
			else if (argCount < 16383)
			{
				num += 2;
			}
			else
			{
				num += 4;
			}
			byte[] array = new byte[num];
			int destinationIndex = 0;
			array[destinationIndex++] = this.m_signature[0];
			if (argCount <= 127)
			{
				array[destinationIndex++] = (byte)(argCount & 255);
			}
			else if (argCount <= 16383)
			{
				array[destinationIndex++] = (byte)(argCount >> 8 | 128);
				array[destinationIndex++] = (byte)(argCount & 255);
			}
			else
			{
				if (argCount > 536870911)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_LargeInteger"));
				}
				array[destinationIndex++] = (byte)(argCount >> 24 | 192);
				array[destinationIndex++] = (byte)(argCount >> 16 & 255);
				array[destinationIndex++] = (byte)(argCount >> 8 & 255);
				array[destinationIndex++] = (byte)(argCount & 255);
			}
			Array.Copy(this.m_signature, 2, array, destinationIndex, currSig - 2);
			array[num - 1] = 0;
			return array;
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x001137C1 File Offset: 0x001119C1
		public void AddArgument(Type clsArgument)
		{
			this.AddArgument(clsArgument, null, null);
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x001137CC File Offset: 0x001119CC
		[SecuritySafeCritical]
		public void AddArgument(Type argument, bool pinned)
		{
			if (argument == null)
			{
				throw new ArgumentNullException("argument");
			}
			this.IncrementArgCounts();
			this.AddOneArgTypeHelper(argument, pinned);
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x001137F0 File Offset: 0x001119F0
		public void AddArguments(Type[] arguments, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
		{
			if (requiredCustomModifiers != null && (arguments == null || requiredCustomModifiers.Length != arguments.Length))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MismatchedArrays", new object[]
				{
					"requiredCustomModifiers",
					"arguments"
				}));
			}
			if (optionalCustomModifiers != null && (arguments == null || optionalCustomModifiers.Length != arguments.Length))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MismatchedArrays", new object[]
				{
					"optionalCustomModifiers",
					"arguments"
				}));
			}
			if (arguments != null)
			{
				for (int i = 0; i < arguments.Length; i++)
				{
					this.AddArgument(arguments[i], (requiredCustomModifiers == null) ? null : requiredCustomModifiers[i], (optionalCustomModifiers == null) ? null : optionalCustomModifiers[i]);
				}
			}
		}

		// Token: 0x06004C08 RID: 19464 RVA: 0x00113891 File Offset: 0x00111A91
		[SecuritySafeCritical]
		public void AddArgument(Type argument, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
		{
			if (this.m_sigDone)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_SigIsFinalized"));
			}
			if (argument == null)
			{
				throw new ArgumentNullException("argument");
			}
			this.IncrementArgCounts();
			this.AddOneArgTypeHelper(argument, requiredCustomModifiers, optionalCustomModifiers);
		}

		// Token: 0x06004C09 RID: 19465 RVA: 0x001138CE File Offset: 0x00111ACE
		public void AddSentinel()
		{
			this.AddElementType(CorElementType.Sentinel);
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x001138D8 File Offset: 0x00111AD8
		public override bool Equals(object obj)
		{
			if (!(obj is SignatureHelper))
			{
				return false;
			}
			SignatureHelper signatureHelper = (SignatureHelper)obj;
			if (!signatureHelper.m_module.Equals(this.m_module) || signatureHelper.m_currSig != this.m_currSig || signatureHelper.m_sizeLoc != this.m_sizeLoc || signatureHelper.m_sigDone != this.m_sigDone)
			{
				return false;
			}
			for (int i = 0; i < this.m_currSig; i++)
			{
				if (this.m_signature[i] != signatureHelper.m_signature[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004C0B RID: 19467 RVA: 0x0011395C File Offset: 0x00111B5C
		public override int GetHashCode()
		{
			int num = this.m_module.GetHashCode() + this.m_currSig + this.m_sizeLoc;
			if (this.m_sigDone)
			{
				num++;
			}
			for (int i = 0; i < this.m_currSig; i++)
			{
				num += this.m_signature[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x001139B5 File Offset: 0x00111BB5
		public byte[] GetSignature()
		{
			return this.GetSignature(false);
		}

		// Token: 0x06004C0D RID: 19469 RVA: 0x001139C0 File Offset: 0x00111BC0
		internal byte[] GetSignature(bool appendEndOfSig)
		{
			if (!this.m_sigDone)
			{
				if (appendEndOfSig)
				{
					this.AddElementType(CorElementType.End);
				}
				this.SetNumberOfSignatureElements(true);
				this.m_sigDone = true;
			}
			if (this.m_signature.Length > this.m_currSig)
			{
				byte[] array = new byte[this.m_currSig];
				Array.Copy(this.m_signature, array, this.m_currSig);
				this.m_signature = array;
			}
			return this.m_signature;
		}

		// Token: 0x06004C0E RID: 19470 RVA: 0x00113A28 File Offset: 0x00111C28
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Length: " + this.m_currSig + Environment.NewLine);
			if (this.m_sizeLoc != -1)
			{
				stringBuilder.Append("Arguments: " + this.m_signature[this.m_sizeLoc] + Environment.NewLine);
			}
			else
			{
				stringBuilder.Append("Field Signature" + Environment.NewLine);
			}
			stringBuilder.Append("Signature: " + Environment.NewLine);
			for (int i = 0; i <= this.m_currSig; i++)
			{
				stringBuilder.Append(this.m_signature[i] + "  ");
			}
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}

		// Token: 0x06004C0F RID: 19471 RVA: 0x00113AFC File Offset: 0x00111CFC
		void _SignatureHelper.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004C10 RID: 19472 RVA: 0x00113B03 File Offset: 0x00111D03
		void _SignatureHelper.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004C11 RID: 19473 RVA: 0x00113B0A File Offset: 0x00111D0A
		void _SignatureHelper.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004C12 RID: 19474 RVA: 0x00113B11 File Offset: 0x00111D11
		void _SignatureHelper.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040020E2 RID: 8418
		private const int NO_SIZE_IN_SIG = -1;

		// Token: 0x040020E3 RID: 8419
		private byte[] m_signature;

		// Token: 0x040020E4 RID: 8420
		private int m_currSig;

		// Token: 0x040020E5 RID: 8421
		private int m_sizeLoc;

		// Token: 0x040020E6 RID: 8422
		private ModuleBuilder m_module;

		// Token: 0x040020E7 RID: 8423
		private bool m_sigDone;

		// Token: 0x040020E8 RID: 8424
		private int m_argCount;
	}
}
