using System;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x02000604 RID: 1540
	internal class DynamicILGenerator : ILGenerator
	{
		// Token: 0x060048B7 RID: 18615 RVA: 0x00106546 File Offset: 0x00104746
		internal DynamicILGenerator(DynamicMethod method, byte[] methodSignature, int size) : base(method, size)
		{
			this.m_scope = new DynamicScope();
			this.m_methodSigToken = this.m_scope.GetTokenFor(methodSignature);
		}

		// Token: 0x060048B8 RID: 18616 RVA: 0x0010656D File Offset: 0x0010476D
		[SecurityCritical]
		internal void GetCallableMethod(RuntimeModule module, DynamicMethod dm)
		{
			dm.m_methodHandle = ModuleHandle.GetDynamicMethod(dm, module, this.m_methodBuilder.Name, (byte[])this.m_scope[this.m_methodSigToken], new DynamicResolver(this));
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060048B9 RID: 18617 RVA: 0x001065A3 File Offset: 0x001047A3
		private bool ProfileAPICheck
		{
			get
			{
				return ((DynamicMethod)this.m_methodBuilder).ProfileAPICheck;
			}
		}

		// Token: 0x060048BA RID: 18618 RVA: 0x001065B8 File Offset: 0x001047B8
		public override LocalBuilder DeclareLocal(Type localType, bool pinned)
		{
			if (localType == null)
			{
				throw new ArgumentNullException("localType");
			}
			RuntimeType runtimeType = localType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			if (this.ProfileAPICheck && (runtimeType.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
				{
					runtimeType.FullName
				}));
			}
			LocalBuilder result = new LocalBuilder(this.m_localCount, localType, this.m_methodBuilder);
			this.m_localSignature.AddArgument(localType, pinned);
			this.m_localCount++;
			return result;
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x0010665C File Offset: 0x0010485C
		[SecuritySafeCritical]
		public override void Emit(OpCode opcode, MethodInfo meth)
		{
			if (meth == null)
			{
				throw new ArgumentNullException("meth");
			}
			int num = 0;
			DynamicMethod dynamicMethod = meth as DynamicMethod;
			int tokenFor;
			if (dynamicMethod == null)
			{
				RuntimeMethodInfo runtimeMethodInfo = meth as RuntimeMethodInfo;
				if (runtimeMethodInfo == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "meth");
				}
				RuntimeType runtimeType = runtimeMethodInfo.GetRuntimeType();
				if (runtimeType != null && (runtimeType.IsGenericType || runtimeType.IsArray))
				{
					tokenFor = this.GetTokenFor(runtimeMethodInfo, runtimeType);
				}
				else
				{
					tokenFor = this.GetTokenFor(runtimeMethodInfo);
				}
			}
			else
			{
				if (opcode.Equals(OpCodes.Ldtoken) || opcode.Equals(OpCodes.Ldftn) || opcode.Equals(OpCodes.Ldvirtftn))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOpCodeOnDynamicMethod"));
				}
				tokenFor = this.GetTokenFor(dynamicMethod);
			}
			base.EnsureCapacity(7);
			base.InternalEmit(opcode);
			if (opcode.StackBehaviourPush == StackBehaviour.Varpush && meth.ReturnType != typeof(void))
			{
				num++;
			}
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				num -= meth.GetParametersNoCopy().Length;
			}
			if (!meth.IsStatic && !opcode.Equals(OpCodes.Newobj) && !opcode.Equals(OpCodes.Ldtoken) && !opcode.Equals(OpCodes.Ldftn))
			{
				num--;
			}
			base.UpdateStackSize(opcode, num);
			base.PutInteger4(tokenFor);
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x001067C4 File Offset: 0x001049C4
		[ComVisible(true)]
		public override void Emit(OpCode opcode, ConstructorInfo con)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			RuntimeConstructorInfo runtimeConstructorInfo = con as RuntimeConstructorInfo;
			if (runtimeConstructorInfo == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "con");
			}
			RuntimeType runtimeType = runtimeConstructorInfo.GetRuntimeType();
			int tokenFor;
			if (runtimeType != null && (runtimeType.IsGenericType || runtimeType.IsArray))
			{
				tokenFor = this.GetTokenFor(runtimeConstructorInfo, runtimeType);
			}
			else
			{
				tokenFor = this.GetTokenFor(runtimeConstructorInfo);
			}
			base.EnsureCapacity(7);
			base.InternalEmit(opcode);
			base.UpdateStackSize(opcode, 1);
			base.PutInteger4(tokenFor);
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x0010685C File Offset: 0x00104A5C
		public override void Emit(OpCode opcode, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			int tokenFor = this.GetTokenFor(runtimeType);
			base.EnsureCapacity(7);
			base.InternalEmit(opcode);
			base.PutInteger4(tokenFor);
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x001068BC File Offset: 0x00104ABC
		public override void Emit(OpCode opcode, FieldInfo field)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			RuntimeFieldInfo runtimeFieldInfo = field as RuntimeFieldInfo;
			if (runtimeFieldInfo == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeFieldInfo"), "field");
			}
			int tokenFor;
			if (field.DeclaringType == null)
			{
				tokenFor = this.GetTokenFor(runtimeFieldInfo);
			}
			else
			{
				tokenFor = this.GetTokenFor(runtimeFieldInfo, runtimeFieldInfo.GetRuntimeType());
			}
			base.EnsureCapacity(7);
			base.InternalEmit(opcode);
			base.PutInteger4(tokenFor);
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x00106940 File Offset: 0x00104B40
		public override void Emit(OpCode opcode, string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int tokenForString = this.GetTokenForString(str);
			base.EnsureCapacity(7);
			base.InternalEmit(opcode);
			base.PutInteger4(tokenForString);
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x00106978 File Offset: 0x00104B78
		[SecuritySafeCritical]
		public override void EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			int num = 0;
			if (optionalParameterTypes != null && (callingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAVarArgCallingConvention"));
			}
			SignatureHelper memberRefSignature = this.GetMemberRefSignature(callingConvention, returnType, parameterTypes, optionalParameterTypes);
			base.EnsureCapacity(7);
			this.Emit(OpCodes.Calli);
			if (returnType != typeof(void))
			{
				num++;
			}
			if (parameterTypes != null)
			{
				num -= parameterTypes.Length;
			}
			if (optionalParameterTypes != null)
			{
				num -= optionalParameterTypes.Length;
			}
			if ((callingConvention & CallingConventions.HasThis) == CallingConventions.HasThis)
			{
				num--;
			}
			num--;
			base.UpdateStackSize(OpCodes.Calli, num);
			int tokenForSig = this.GetTokenForSig(memberRefSignature.GetSignature(true));
			base.PutInteger4(tokenForSig);
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x00106A1C File Offset: 0x00104C1C
		public override void EmitCalli(OpCode opcode, CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes)
		{
			int num = 0;
			int num2 = 0;
			if (parameterTypes != null)
			{
				num2 = parameterTypes.Length;
			}
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(unmanagedCallConv, returnType);
			if (parameterTypes != null)
			{
				for (int i = 0; i < num2; i++)
				{
					methodSigHelper.AddArgument(parameterTypes[i]);
				}
			}
			if (returnType != typeof(void))
			{
				num++;
			}
			if (parameterTypes != null)
			{
				num -= num2;
			}
			num--;
			base.UpdateStackSize(OpCodes.Calli, num);
			base.EnsureCapacity(7);
			this.Emit(OpCodes.Calli);
			int tokenForSig = this.GetTokenForSig(methodSigHelper.GetSignature(true));
			base.PutInteger4(tokenForSig);
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x00106AB0 File Offset: 0x00104CB0
		[SecuritySafeCritical]
		public override void EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
		{
			if (methodInfo == null)
			{
				throw new ArgumentNullException("methodInfo");
			}
			if (!opcode.Equals(OpCodes.Call) && !opcode.Equals(OpCodes.Callvirt) && !opcode.Equals(OpCodes.Newobj))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotMethodCallOpcode"), "opcode");
			}
			if (methodInfo.ContainsGenericParameters)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_GenericsInvalid"), "methodInfo");
			}
			if (methodInfo.DeclaringType != null && methodInfo.DeclaringType.ContainsGenericParameters)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_GenericsInvalid"), "methodInfo");
			}
			int num = 0;
			int memberRefToken = this.GetMemberRefToken(methodInfo, optionalParameterTypes);
			base.EnsureCapacity(7);
			base.InternalEmit(opcode);
			if (methodInfo.ReturnType != typeof(void))
			{
				num++;
			}
			num -= methodInfo.GetParameterTypes().Length;
			if (!(methodInfo is SymbolMethod) && !methodInfo.IsStatic && !opcode.Equals(OpCodes.Newobj))
			{
				num--;
			}
			if (optionalParameterTypes != null)
			{
				num -= optionalParameterTypes.Length;
			}
			base.UpdateStackSize(opcode, num);
			base.PutInteger4(memberRefToken);
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x00106BD8 File Offset: 0x00104DD8
		public override void Emit(OpCode opcode, SignatureHelper signature)
		{
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			int num = 0;
			base.EnsureCapacity(7);
			base.InternalEmit(opcode);
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				num -= signature.ArgumentCount;
				num--;
				base.UpdateStackSize(opcode, num);
			}
			int tokenForSig = this.GetTokenForSig(signature.GetSignature(true));
			base.PutInteger4(tokenForSig);
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x00106C38 File Offset: 0x00104E38
		public override Label BeginExceptionBlock()
		{
			return base.BeginExceptionBlock();
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x00106C40 File Offset: 0x00104E40
		public override void EndExceptionBlock()
		{
			base.EndExceptionBlock();
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x00106C48 File Offset: 0x00104E48
		public override void BeginExceptFilterBlock()
		{
			throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x00106C5C File Offset: 0x00104E5C
		public override void BeginCatchBlock(Type exceptionType)
		{
			if (base.CurrExcStackCount == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
			}
			__ExceptionInfo _ExceptionInfo = base.CurrExcStack[base.CurrExcStackCount - 1];
			RuntimeType runtimeType = exceptionType as RuntimeType;
			if (_ExceptionInfo.GetCurrentState() == 1)
			{
				if (exceptionType != null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_ShouldNotSpecifyExceptionType"));
				}
				this.Emit(OpCodes.Endfilter);
			}
			else
			{
				if (exceptionType == null)
				{
					throw new ArgumentNullException("exceptionType");
				}
				if (runtimeType == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
				}
				Label endLabel = _ExceptionInfo.GetEndLabel();
				this.Emit(OpCodes.Leave, endLabel);
				base.UpdateStackSize(OpCodes.Nop, 1);
			}
			_ExceptionInfo.MarkCatchAddr(this.ILOffset, exceptionType);
			_ExceptionInfo.m_filterAddr[_ExceptionInfo.m_currentCatch - 1] = this.GetTokenFor(runtimeType);
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x00106D36 File Offset: 0x00104F36
		public override void BeginFaultBlock()
		{
			throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x00106D47 File Offset: 0x00104F47
		public override void BeginFinallyBlock()
		{
			base.BeginFinallyBlock();
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x00106D4F File Offset: 0x00104F4F
		public override void UsingNamespace(string ns)
		{
			throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x00106D60 File Offset: 0x00104F60
		public override void MarkSequencePoint(ISymbolDocumentWriter document, int startLine, int startColumn, int endLine, int endColumn)
		{
			throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x00106D71 File Offset: 0x00104F71
		public override void BeginScope()
		{
			throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x00106D82 File Offset: 0x00104F82
		public override void EndScope()
		{
			throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x00106D94 File Offset: 0x00104F94
		[SecurityCritical]
		private int GetMemberRefToken(MethodBase methodInfo, Type[] optionalParameterTypes)
		{
			if (optionalParameterTypes != null && (methodInfo.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAVarArgCallingConvention"));
			}
			RuntimeMethodInfo runtimeMethodInfo = methodInfo as RuntimeMethodInfo;
			DynamicMethod dynamicMethod = methodInfo as DynamicMethod;
			if (runtimeMethodInfo == null && dynamicMethod == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), "methodInfo");
			}
			ParameterInfo[] parametersNoCopy = methodInfo.GetParametersNoCopy();
			Type[] array;
			if (parametersNoCopy != null && parametersNoCopy.Length != 0)
			{
				array = new Type[parametersNoCopy.Length];
				for (int i = 0; i < parametersNoCopy.Length; i++)
				{
					array[i] = parametersNoCopy[i].ParameterType;
				}
			}
			else
			{
				array = null;
			}
			SignatureHelper memberRefSignature = this.GetMemberRefSignature(methodInfo.CallingConvention, MethodBuilder.GetMethodBaseReturnType(methodInfo), array, optionalParameterTypes);
			if (runtimeMethodInfo != null)
			{
				return this.GetTokenForVarArgMethod(runtimeMethodInfo, memberRefSignature);
			}
			return this.GetTokenForVarArgMethod(dynamicMethod, memberRefSignature);
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x00106E60 File Offset: 0x00105060
		[SecurityCritical]
		internal override SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			int num;
			if (parameterTypes == null)
			{
				num = 0;
			}
			else
			{
				num = parameterTypes.Length;
			}
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(call, returnType);
			for (int i = 0; i < num; i++)
			{
				methodSigHelper.AddArgument(parameterTypes[i]);
			}
			if (optionalParameterTypes != null && optionalParameterTypes.Length != 0)
			{
				methodSigHelper.AddSentinel();
				for (int i = 0; i < optionalParameterTypes.Length; i++)
				{
					methodSigHelper.AddArgument(optionalParameterTypes[i]);
				}
			}
			return methodSigHelper;
		}

		// Token: 0x060048D0 RID: 18640 RVA: 0x00106EBE File Offset: 0x001050BE
		internal override void RecordTokenFixup()
		{
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x00106EC0 File Offset: 0x001050C0
		private int GetTokenFor(RuntimeType rtType)
		{
			if (this.ProfileAPICheck && (rtType.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
				{
					rtType.FullName
				}));
			}
			return this.m_scope.GetTokenFor(rtType.TypeHandle);
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x00106F10 File Offset: 0x00105110
		private int GetTokenFor(RuntimeFieldInfo runtimeField)
		{
			if (this.ProfileAPICheck)
			{
				RtFieldInfo rtFieldInfo = runtimeField as RtFieldInfo;
				if (rtFieldInfo != null && (rtFieldInfo.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
					{
						rtFieldInfo.FullName
					}));
				}
			}
			return this.m_scope.GetTokenFor(runtimeField.FieldHandle);
		}

		// Token: 0x060048D3 RID: 18643 RVA: 0x00106F70 File Offset: 0x00105170
		private int GetTokenFor(RuntimeFieldInfo runtimeField, RuntimeType rtType)
		{
			if (this.ProfileAPICheck)
			{
				RtFieldInfo rtFieldInfo = runtimeField as RtFieldInfo;
				if (rtFieldInfo != null && (rtFieldInfo.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
					{
						rtFieldInfo.FullName
					}));
				}
				if ((rtType.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
					{
						rtType.FullName
					}));
				}
			}
			return this.m_scope.GetTokenFor(runtimeField.FieldHandle, rtType.TypeHandle);
		}

		// Token: 0x060048D4 RID: 18644 RVA: 0x00107000 File Offset: 0x00105200
		private int GetTokenFor(RuntimeConstructorInfo rtMeth)
		{
			if (this.ProfileAPICheck && (rtMeth.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
				{
					rtMeth.FullName
				}));
			}
			return this.m_scope.GetTokenFor(rtMeth.MethodHandle);
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x00107050 File Offset: 0x00105250
		private int GetTokenFor(RuntimeConstructorInfo rtMeth, RuntimeType rtType)
		{
			if (this.ProfileAPICheck)
			{
				if ((rtMeth.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
					{
						rtMeth.FullName
					}));
				}
				if ((rtType.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
					{
						rtType.FullName
					}));
				}
			}
			return this.m_scope.GetTokenFor(rtMeth.MethodHandle, rtType.TypeHandle);
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x001070D0 File Offset: 0x001052D0
		private int GetTokenFor(RuntimeMethodInfo rtMeth)
		{
			if (this.ProfileAPICheck && (rtMeth.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
				{
					rtMeth.FullName
				}));
			}
			return this.m_scope.GetTokenFor(rtMeth.MethodHandle);
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x00107120 File Offset: 0x00105320
		private int GetTokenFor(RuntimeMethodInfo rtMeth, RuntimeType rtType)
		{
			if (this.ProfileAPICheck)
			{
				if ((rtMeth.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
					{
						rtMeth.FullName
					}));
				}
				if ((rtType.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
					{
						rtType.FullName
					}));
				}
			}
			return this.m_scope.GetTokenFor(rtMeth.MethodHandle, rtType.TypeHandle);
		}

		// Token: 0x060048D8 RID: 18648 RVA: 0x001071A0 File Offset: 0x001053A0
		private int GetTokenFor(DynamicMethod dm)
		{
			return this.m_scope.GetTokenFor(dm);
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x001071B0 File Offset: 0x001053B0
		private int GetTokenForVarArgMethod(RuntimeMethodInfo rtMeth, SignatureHelper sig)
		{
			if (this.ProfileAPICheck && (rtMeth.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
				{
					rtMeth.FullName
				}));
			}
			VarArgMethod varArgMethod = new VarArgMethod(rtMeth, sig);
			return this.m_scope.GetTokenFor(varArgMethod);
		}

		// Token: 0x060048DA RID: 18650 RVA: 0x00107204 File Offset: 0x00105404
		private int GetTokenForVarArgMethod(DynamicMethod dm, SignatureHelper sig)
		{
			VarArgMethod varArgMethod = new VarArgMethod(dm, sig);
			return this.m_scope.GetTokenFor(varArgMethod);
		}

		// Token: 0x060048DB RID: 18651 RVA: 0x00107225 File Offset: 0x00105425
		private int GetTokenForString(string s)
		{
			return this.m_scope.GetTokenFor(s);
		}

		// Token: 0x060048DC RID: 18652 RVA: 0x00107233 File Offset: 0x00105433
		private int GetTokenForSig(byte[] sig)
		{
			return this.m_scope.GetTokenFor(sig);
		}

		// Token: 0x04001DCA RID: 7626
		internal DynamicScope m_scope;

		// Token: 0x04001DCB RID: 7627
		private int m_methodSigToken;
	}
}
