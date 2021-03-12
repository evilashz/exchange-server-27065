using System;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x02000610 RID: 1552
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ILGenerator))]
	[ComVisible(true)]
	public class ILGenerator : _ILGenerator
	{
		// Token: 0x0600497B RID: 18811 RVA: 0x00108F78 File Offset: 0x00107178
		internal static int[] EnlargeArray(int[] incoming)
		{
			int[] array = new int[incoming.Length * 2];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x00108F9C File Offset: 0x0010719C
		private static byte[] EnlargeArray(byte[] incoming)
		{
			byte[] array = new byte[incoming.Length * 2];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x00108FC0 File Offset: 0x001071C0
		private static byte[] EnlargeArray(byte[] incoming, int requiredSize)
		{
			byte[] array = new byte[requiredSize];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x00108FE0 File Offset: 0x001071E0
		private static __FixupData[] EnlargeArray(__FixupData[] incoming)
		{
			__FixupData[] array = new __FixupData[incoming.Length * 2];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x00109004 File Offset: 0x00107204
		private static __ExceptionInfo[] EnlargeArray(__ExceptionInfo[] incoming)
		{
			__ExceptionInfo[] array = new __ExceptionInfo[incoming.Length * 2];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06004980 RID: 18816 RVA: 0x00109027 File Offset: 0x00107227
		internal int CurrExcStackCount
		{
			get
			{
				return this.m_currExcStackCount;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06004981 RID: 18817 RVA: 0x0010902F File Offset: 0x0010722F
		internal __ExceptionInfo[] CurrExcStack
		{
			get
			{
				return this.m_currExcStack;
			}
		}

		// Token: 0x06004982 RID: 18818 RVA: 0x00109037 File Offset: 0x00107237
		internal ILGenerator(MethodInfo methodBuilder) : this(methodBuilder, 64)
		{
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x00109044 File Offset: 0x00107244
		internal ILGenerator(MethodInfo methodBuilder, int size)
		{
			if (size < 16)
			{
				this.m_ILStream = new byte[16];
			}
			else
			{
				this.m_ILStream = new byte[size];
			}
			this.m_length = 0;
			this.m_labelCount = 0;
			this.m_fixupCount = 0;
			this.m_labelList = null;
			this.m_fixupData = null;
			this.m_exceptions = null;
			this.m_exceptionCount = 0;
			this.m_currExcStack = null;
			this.m_currExcStackCount = 0;
			this.m_RelocFixupList = null;
			this.m_RelocFixupCount = 0;
			this.m_ScopeTree = new ScopeTree();
			this.m_LineNumberInfo = new LineNumberInfo();
			this.m_methodBuilder = methodBuilder;
			this.m_localCount = 0;
			MethodBuilder methodBuilder2 = this.m_methodBuilder as MethodBuilder;
			if (methodBuilder2 == null)
			{
				this.m_localSignature = SignatureHelper.GetLocalVarSigHelper(null);
				return;
			}
			this.m_localSignature = SignatureHelper.GetLocalVarSigHelper(methodBuilder2.GetTypeBuilder().Module);
		}

		// Token: 0x06004984 RID: 18820 RVA: 0x00109120 File Offset: 0x00107320
		internal virtual void RecordTokenFixup()
		{
			if (this.m_RelocFixupList == null)
			{
				this.m_RelocFixupList = new int[8];
			}
			else if (this.m_RelocFixupList.Length <= this.m_RelocFixupCount)
			{
				this.m_RelocFixupList = ILGenerator.EnlargeArray(this.m_RelocFixupList);
			}
			int[] relocFixupList = this.m_RelocFixupList;
			int relocFixupCount = this.m_RelocFixupCount;
			this.m_RelocFixupCount = relocFixupCount + 1;
			relocFixupList[relocFixupCount] = this.m_length;
		}

		// Token: 0x06004985 RID: 18821 RVA: 0x00109184 File Offset: 0x00107384
		internal void InternalEmit(OpCode opcode)
		{
			int length;
			if (opcode.Size != 1)
			{
				byte[] ilstream = this.m_ILStream;
				length = this.m_length;
				this.m_length = length + 1;
				ilstream[length] = (byte)(opcode.Value >> 8);
			}
			byte[] ilstream2 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream2[length] = (byte)opcode.Value;
			this.UpdateStackSize(opcode, opcode.StackChange());
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x001091EC File Offset: 0x001073EC
		internal void UpdateStackSize(OpCode opcode, int stackchange)
		{
			this.m_maxMidStackCur += stackchange;
			if (this.m_maxMidStackCur > this.m_maxMidStack)
			{
				this.m_maxMidStack = this.m_maxMidStackCur;
			}
			else if (this.m_maxMidStackCur < 0)
			{
				this.m_maxMidStackCur = 0;
			}
			if (opcode.EndsUncondJmpBlk())
			{
				this.m_maxStackSize += this.m_maxMidStack;
				this.m_maxMidStack = 0;
				this.m_maxMidStackCur = 0;
			}
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x0010925D File Offset: 0x0010745D
		[SecurityCritical]
		private int GetMethodToken(MethodBase method, Type[] optionalParameterTypes, bool useMethodDef)
		{
			return ((ModuleBuilder)this.m_methodBuilder.Module).GetMethodTokenInternal(method, optionalParameterTypes, useMethodDef);
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x00109277 File Offset: 0x00107477
		[SecurityCritical]
		internal virtual SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			return this.GetMemberRefSignature(call, returnType, parameterTypes, optionalParameterTypes, 0);
		}

		// Token: 0x06004989 RID: 18825 RVA: 0x00109285 File Offset: 0x00107485
		[SecurityCritical]
		private SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes, int cGenericParameters)
		{
			return ((ModuleBuilder)this.m_methodBuilder.Module).GetMemberRefSignature(call, returnType, parameterTypes, optionalParameterTypes, cGenericParameters);
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x001092A4 File Offset: 0x001074A4
		internal byte[] BakeByteArray()
		{
			if (this.m_currExcStackCount != 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_UnclosedExceptionBlock"));
			}
			if (this.m_length == 0)
			{
				return null;
			}
			int length = this.m_length;
			byte[] array = new byte[length];
			Array.Copy(this.m_ILStream, array, length);
			for (int i = 0; i < this.m_fixupCount; i++)
			{
				int num = this.GetLabelPos(this.m_fixupData[i].m_fixupLabel) - (this.m_fixupData[i].m_fixupPos + this.m_fixupData[i].m_fixupInstSize);
				if (this.m_fixupData[i].m_fixupInstSize == 1)
				{
					if (num < -128 || num > 127)
					{
						throw new NotSupportedException(Environment.GetResourceString("NotSupported_IllegalOneByteBranch", new object[]
						{
							this.m_fixupData[i].m_fixupPos,
							num
						}));
					}
					if (num < 0)
					{
						array[this.m_fixupData[i].m_fixupPos] = (byte)(256 + num);
					}
					else
					{
						array[this.m_fixupData[i].m_fixupPos] = (byte)num;
					}
				}
				else
				{
					ILGenerator.PutInteger4InArray(num, this.m_fixupData[i].m_fixupPos, array);
				}
			}
			return array;
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x001093EC File Offset: 0x001075EC
		internal __ExceptionInfo[] GetExceptions()
		{
			if (this.m_currExcStackCount != 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_UnclosedExceptionBlock"));
			}
			if (this.m_exceptionCount == 0)
			{
				return null;
			}
			__ExceptionInfo[] array = new __ExceptionInfo[this.m_exceptionCount];
			Array.Copy(this.m_exceptions, array, this.m_exceptionCount);
			ILGenerator.SortExceptions(array);
			return array;
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x00109440 File Offset: 0x00107640
		internal void EnsureCapacity(int size)
		{
			if (this.m_length + size >= this.m_ILStream.Length)
			{
				if (this.m_length + size >= 2 * this.m_ILStream.Length)
				{
					this.m_ILStream = ILGenerator.EnlargeArray(this.m_ILStream, this.m_length + size);
					return;
				}
				this.m_ILStream = ILGenerator.EnlargeArray(this.m_ILStream);
			}
		}

		// Token: 0x0600498D RID: 18829 RVA: 0x0010949E File Offset: 0x0010769E
		internal void PutInteger4(int value)
		{
			this.m_length = ILGenerator.PutInteger4InArray(value, this.m_length, this.m_ILStream);
		}

		// Token: 0x0600498E RID: 18830 RVA: 0x001094B8 File Offset: 0x001076B8
		private static int PutInteger4InArray(int value, int startPos, byte[] array)
		{
			array[startPos++] = (byte)value;
			array[startPos++] = (byte)(value >> 8);
			array[startPos++] = (byte)(value >> 16);
			array[startPos++] = (byte)(value >> 24);
			return startPos;
		}

		// Token: 0x0600498F RID: 18831 RVA: 0x001094EC File Offset: 0x001076EC
		private int GetLabelPos(Label lbl)
		{
			int labelValue = lbl.GetLabelValue();
			if (labelValue < 0 || labelValue >= this.m_labelCount)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadLabel"));
			}
			if (this.m_labelList[labelValue] < 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadLabelContent"));
			}
			return this.m_labelList[labelValue];
		}

		// Token: 0x06004990 RID: 18832 RVA: 0x00109544 File Offset: 0x00107744
		private void AddFixup(Label lbl, int pos, int instSize)
		{
			if (this.m_fixupData == null)
			{
				this.m_fixupData = new __FixupData[8];
			}
			else if (this.m_fixupData.Length <= this.m_fixupCount)
			{
				this.m_fixupData = ILGenerator.EnlargeArray(this.m_fixupData);
			}
			this.m_fixupData[this.m_fixupCount].m_fixupPos = pos;
			this.m_fixupData[this.m_fixupCount].m_fixupLabel = lbl;
			this.m_fixupData[this.m_fixupCount].m_fixupInstSize = instSize;
			this.m_fixupCount++;
		}

		// Token: 0x06004991 RID: 18833 RVA: 0x001095DB File Offset: 0x001077DB
		internal int GetMaxStackSize()
		{
			return this.m_maxStackSize;
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x001095E4 File Offset: 0x001077E4
		private static void SortExceptions(__ExceptionInfo[] exceptions)
		{
			int num = exceptions.Length;
			for (int i = 0; i < num; i++)
			{
				int num2 = i;
				for (int j = i + 1; j < num; j++)
				{
					if (exceptions[num2].IsInner(exceptions[j]))
					{
						num2 = j;
					}
				}
				__ExceptionInfo _ExceptionInfo = exceptions[i];
				exceptions[i] = exceptions[num2];
				exceptions[num2] = _ExceptionInfo;
			}
		}

		// Token: 0x06004993 RID: 18835 RVA: 0x00109634 File Offset: 0x00107834
		internal int[] GetTokenFixups()
		{
			if (this.m_RelocFixupCount == 0)
			{
				return null;
			}
			int[] array = new int[this.m_RelocFixupCount];
			Array.Copy(this.m_RelocFixupList, array, this.m_RelocFixupCount);
			return array;
		}

		// Token: 0x06004994 RID: 18836 RVA: 0x0010966A File Offset: 0x0010786A
		public virtual void Emit(OpCode opcode)
		{
			this.EnsureCapacity(3);
			this.InternalEmit(opcode);
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x0010967C File Offset: 0x0010787C
		public virtual void Emit(OpCode opcode, byte arg)
		{
			this.EnsureCapacity(4);
			this.InternalEmit(opcode);
			byte[] ilstream = this.m_ILStream;
			int length = this.m_length;
			this.m_length = length + 1;
			ilstream[length] = arg;
		}

		// Token: 0x06004996 RID: 18838 RVA: 0x001096B0 File Offset: 0x001078B0
		[CLSCompliant(false)]
		public void Emit(OpCode opcode, sbyte arg)
		{
			this.EnsureCapacity(4);
			this.InternalEmit(opcode);
			int length;
			if (arg < 0)
			{
				byte[] ilstream = this.m_ILStream;
				length = this.m_length;
				this.m_length = length + 1;
				ilstream[length] = (byte)(256 + (int)arg);
				return;
			}
			byte[] ilstream2 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream2[length] = (byte)arg;
		}

		// Token: 0x06004997 RID: 18839 RVA: 0x0010970C File Offset: 0x0010790C
		public virtual void Emit(OpCode opcode, short arg)
		{
			this.EnsureCapacity(5);
			this.InternalEmit(opcode);
			byte[] ilstream = this.m_ILStream;
			int length = this.m_length;
			this.m_length = length + 1;
			ilstream[length] = (byte)arg;
			byte[] ilstream2 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream2[length] = (byte)(arg >> 8);
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x0010975D File Offset: 0x0010795D
		public virtual void Emit(OpCode opcode, int arg)
		{
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.PutInteger4(arg);
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x00109774 File Offset: 0x00107974
		[SecuritySafeCritical]
		public virtual void Emit(OpCode opcode, MethodInfo meth)
		{
			if (meth == null)
			{
				throw new ArgumentNullException("meth");
			}
			if (opcode.Equals(OpCodes.Call) || opcode.Equals(OpCodes.Callvirt) || opcode.Equals(OpCodes.Newobj))
			{
				this.EmitCall(opcode, meth, null);
				return;
			}
			int stackchange = 0;
			bool useMethodDef = opcode.Equals(OpCodes.Ldtoken) || opcode.Equals(OpCodes.Ldftn) || opcode.Equals(OpCodes.Ldvirtftn);
			int methodToken = this.GetMethodToken(meth, null, useMethodDef);
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.UpdateStackSize(opcode, stackchange);
			this.RecordTokenFixup();
			this.PutInteger4(methodToken);
		}

		// Token: 0x0600499A RID: 18842 RVA: 0x00109824 File Offset: 0x00107A24
		[SecuritySafeCritical]
		public virtual void EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			int num = 0;
			if (optionalParameterTypes != null && (callingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAVarArgCallingConvention"));
			}
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			SignatureHelper memberRefSignature = this.GetMemberRefSignature(callingConvention, returnType, parameterTypes, optionalParameterTypes);
			this.EnsureCapacity(7);
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
			this.UpdateStackSize(OpCodes.Calli, num);
			this.RecordTokenFixup();
			this.PutInteger4(moduleBuilder.GetSignatureToken(memberRefSignature).Token);
		}

		// Token: 0x0600499B RID: 18843 RVA: 0x001098E0 File Offset: 0x00107AE0
		public virtual void EmitCalli(OpCode opcode, CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes)
		{
			int num = 0;
			int num2 = 0;
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			if (parameterTypes != null)
			{
				num2 = parameterTypes.Length;
			}
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(moduleBuilder, unmanagedCallConv, returnType);
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
			this.UpdateStackSize(OpCodes.Calli, num);
			this.EnsureCapacity(7);
			this.Emit(OpCodes.Calli);
			this.RecordTokenFixup();
			this.PutInteger4(moduleBuilder.GetSignatureToken(methodSigHelper).Token);
		}

		// Token: 0x0600499C RID: 18844 RVA: 0x00109990 File Offset: 0x00107B90
		[SecuritySafeCritical]
		public virtual void EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
		{
			if (methodInfo == null)
			{
				throw new ArgumentNullException("methodInfo");
			}
			if (!opcode.Equals(OpCodes.Call) && !opcode.Equals(OpCodes.Callvirt) && !opcode.Equals(OpCodes.Newobj))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotMethodCallOpcode"), "opcode");
			}
			int num = 0;
			int methodToken = this.GetMethodToken(methodInfo, optionalParameterTypes, false);
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (methodInfo.ReturnType != typeof(void))
			{
				num++;
			}
			Type[] parameterTypes = methodInfo.GetParameterTypes();
			if (parameterTypes != null)
			{
				num -= parameterTypes.Length;
			}
			if (!(methodInfo is SymbolMethod) && !methodInfo.IsStatic && !opcode.Equals(OpCodes.Newobj))
			{
				num--;
			}
			if (optionalParameterTypes != null)
			{
				num -= optionalParameterTypes.Length;
			}
			this.UpdateStackSize(opcode, num);
			this.RecordTokenFixup();
			this.PutInteger4(methodToken);
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x00109A78 File Offset: 0x00107C78
		public virtual void Emit(OpCode opcode, SignatureHelper signature)
		{
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			int num = 0;
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			int token = moduleBuilder.GetSignatureToken(signature).Token;
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				num -= signature.ArgumentCount;
				num--;
				this.UpdateStackSize(opcode, num);
			}
			this.RecordTokenFixup();
			this.PutInteger4(token);
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x00109AF4 File Offset: 0x00107CF4
		[SecuritySafeCritical]
		[ComVisible(true)]
		public virtual void Emit(OpCode opcode, ConstructorInfo con)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			int num = 0;
			int methodToken = this.GetMethodToken(con, null, true);
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (opcode.StackBehaviourPush == StackBehaviour.Varpush)
			{
				num++;
			}
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				Type[] parameterTypes = con.GetParameterTypes();
				if (parameterTypes != null)
				{
					num -= parameterTypes.Length;
				}
			}
			this.UpdateStackSize(opcode, num);
			this.RecordTokenFixup();
			this.PutInteger4(methodToken);
		}

		// Token: 0x0600499F RID: 18847 RVA: 0x00109B70 File Offset: 0x00107D70
		[SecuritySafeCritical]
		public virtual void Emit(OpCode opcode, Type cls)
		{
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			int token;
			if (opcode == OpCodes.Ldtoken && cls != null && cls.IsGenericTypeDefinition)
			{
				token = moduleBuilder.GetTypeToken(cls).Token;
			}
			else
			{
				token = moduleBuilder.GetTypeTokenInternal(cls).Token;
			}
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.RecordTokenFixup();
			this.PutInteger4(token);
		}

		// Token: 0x060049A0 RID: 18848 RVA: 0x00109BEC File Offset: 0x00107DEC
		public virtual void Emit(OpCode opcode, long arg)
		{
			this.EnsureCapacity(11);
			this.InternalEmit(opcode);
			byte[] ilstream = this.m_ILStream;
			int length = this.m_length;
			this.m_length = length + 1;
			ilstream[length] = (byte)arg;
			byte[] ilstream2 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream2[length] = (byte)(arg >> 8);
			byte[] ilstream3 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream3[length] = (byte)(arg >> 16);
			byte[] ilstream4 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream4[length] = (byte)(arg >> 24);
			byte[] ilstream5 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream5[length] = (byte)(arg >> 32);
			byte[] ilstream6 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream6[length] = (byte)(arg >> 40);
			byte[] ilstream7 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream7[length] = (byte)(arg >> 48);
			byte[] ilstream8 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream8[length] = (byte)(arg >> 56);
		}

		// Token: 0x060049A1 RID: 18849 RVA: 0x00109CEC File Offset: 0x00107EEC
		[SecuritySafeCritical]
		public unsafe virtual void Emit(OpCode opcode, float arg)
		{
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			uint num = *(uint*)(&arg);
			byte[] ilstream = this.m_ILStream;
			int length = this.m_length;
			this.m_length = length + 1;
			ilstream[length] = (byte)num;
			byte[] ilstream2 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream2[length] = (byte)(num >> 8);
			byte[] ilstream3 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream3[length] = (byte)(num >> 16);
			byte[] ilstream4 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream4[length] = (byte)(num >> 24);
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x00109D7C File Offset: 0x00107F7C
		[SecuritySafeCritical]
		public unsafe virtual void Emit(OpCode opcode, double arg)
		{
			this.EnsureCapacity(11);
			this.InternalEmit(opcode);
			ulong num = (ulong)(*(long*)(&arg));
			byte[] ilstream = this.m_ILStream;
			int length = this.m_length;
			this.m_length = length + 1;
			ilstream[length] = (byte)num;
			byte[] ilstream2 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream2[length] = (byte)(num >> 8);
			byte[] ilstream3 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream3[length] = (byte)(num >> 16);
			byte[] ilstream4 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream4[length] = (byte)(num >> 24);
			byte[] ilstream5 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream5[length] = (byte)(num >> 32);
			byte[] ilstream6 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream6[length] = (byte)(num >> 40);
			byte[] ilstream7 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream7[length] = (byte)(num >> 48);
			byte[] ilstream8 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream8[length] = (byte)(num >> 56);
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x00109E84 File Offset: 0x00108084
		public virtual void Emit(OpCode opcode, Label label)
		{
			int labelValue = label.GetLabelValue();
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (OpCodes.TakesSingleByteArgument(opcode))
			{
				this.AddFixup(label, this.m_length, 1);
				this.m_length++;
				return;
			}
			this.AddFixup(label, this.m_length, 4);
			this.m_length += 4;
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x00109EE8 File Offset: 0x001080E8
		public virtual void Emit(OpCode opcode, Label[] labels)
		{
			if (labels == null)
			{
				throw new ArgumentNullException("labels");
			}
			int num = labels.Length;
			this.EnsureCapacity(num * 4 + 7);
			this.InternalEmit(opcode);
			this.PutInteger4(num);
			int i = num * 4;
			int num2 = 0;
			while (i > 0)
			{
				this.AddFixup(labels[num2], this.m_length, i);
				this.m_length += 4;
				i -= 4;
				num2++;
			}
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x00109F58 File Offset: 0x00108158
		public virtual void Emit(OpCode opcode, FieldInfo field)
		{
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			int token = moduleBuilder.GetFieldToken(field).Token;
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.RecordTokenFixup();
			this.PutInteger4(token);
		}

		// Token: 0x060049A6 RID: 18854 RVA: 0x00109FA4 File Offset: 0x001081A4
		public virtual void Emit(OpCode opcode, string str)
		{
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			int token = moduleBuilder.GetStringConstant(str).Token;
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.PutInteger4(token);
		}

		// Token: 0x060049A7 RID: 18855 RVA: 0x00109FE8 File Offset: 0x001081E8
		public virtual void Emit(OpCode opcode, LocalBuilder local)
		{
			if (local == null)
			{
				throw new ArgumentNullException("local");
			}
			int localIndex = local.GetLocalIndex();
			if (local.GetMethodBuilder() != this.m_methodBuilder)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_UnmatchedMethodForLocal"), "local");
			}
			if (opcode.Equals(OpCodes.Ldloc))
			{
				switch (localIndex)
				{
				case 0:
					opcode = OpCodes.Ldloc_0;
					break;
				case 1:
					opcode = OpCodes.Ldloc_1;
					break;
				case 2:
					opcode = OpCodes.Ldloc_2;
					break;
				case 3:
					opcode = OpCodes.Ldloc_3;
					break;
				default:
					if (localIndex <= 255)
					{
						opcode = OpCodes.Ldloc_S;
					}
					break;
				}
			}
			else if (opcode.Equals(OpCodes.Stloc))
			{
				switch (localIndex)
				{
				case 0:
					opcode = OpCodes.Stloc_0;
					break;
				case 1:
					opcode = OpCodes.Stloc_1;
					break;
				case 2:
					opcode = OpCodes.Stloc_2;
					break;
				case 3:
					opcode = OpCodes.Stloc_3;
					break;
				default:
					if (localIndex <= 255)
					{
						opcode = OpCodes.Stloc_S;
					}
					break;
				}
			}
			else if (opcode.Equals(OpCodes.Ldloca) && localIndex <= 255)
			{
				opcode = OpCodes.Ldloca_S;
			}
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (opcode.OperandType == OperandType.InlineNone)
			{
				return;
			}
			int length;
			if (!OpCodes.TakesSingleByteArgument(opcode))
			{
				byte[] ilstream = this.m_ILStream;
				length = this.m_length;
				this.m_length = length + 1;
				ilstream[length] = (byte)localIndex;
				byte[] ilstream2 = this.m_ILStream;
				length = this.m_length;
				this.m_length = length + 1;
				ilstream2[length] = (byte)(localIndex >> 8);
				return;
			}
			if (localIndex > 255)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadInstructionOrIndexOutOfBound"));
			}
			byte[] ilstream3 = this.m_ILStream;
			length = this.m_length;
			this.m_length = length + 1;
			ilstream3[length] = (byte)localIndex;
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x0010A1A0 File Offset: 0x001083A0
		public virtual Label BeginExceptionBlock()
		{
			if (this.m_exceptions == null)
			{
				this.m_exceptions = new __ExceptionInfo[2];
			}
			if (this.m_currExcStack == null)
			{
				this.m_currExcStack = new __ExceptionInfo[2];
			}
			if (this.m_exceptionCount >= this.m_exceptions.Length)
			{
				this.m_exceptions = ILGenerator.EnlargeArray(this.m_exceptions);
			}
			if (this.m_currExcStackCount >= this.m_currExcStack.Length)
			{
				this.m_currExcStack = ILGenerator.EnlargeArray(this.m_currExcStack);
			}
			Label label = this.DefineLabel();
			__ExceptionInfo _ExceptionInfo = new __ExceptionInfo(this.m_length, label);
			__ExceptionInfo[] exceptions = this.m_exceptions;
			int num = this.m_exceptionCount;
			this.m_exceptionCount = num + 1;
			exceptions[num] = _ExceptionInfo;
			__ExceptionInfo[] currExcStack = this.m_currExcStack;
			num = this.m_currExcStackCount;
			this.m_currExcStackCount = num + 1;
			currExcStack[num] = _ExceptionInfo;
			return label;
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x0010A260 File Offset: 0x00108460
		public virtual void EndExceptionBlock()
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			this.m_currExcStack[this.m_currExcStackCount - 1] = null;
			this.m_currExcStackCount--;
			Label endLabel = _ExceptionInfo.GetEndLabel();
			int currentState = _ExceptionInfo.GetCurrentState();
			if (currentState == 1 || currentState == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Argument_BadExceptionCodeGen"));
			}
			if (currentState == 2)
			{
				this.Emit(OpCodes.Leave, endLabel);
			}
			else if (currentState == 3 || currentState == 4)
			{
				this.Emit(OpCodes.Endfinally);
			}
			if (this.m_labelList[endLabel.GetLabelValue()] == -1)
			{
				this.MarkLabel(endLabel);
			}
			else
			{
				this.MarkLabel(_ExceptionInfo.GetFinallyEndLabel());
			}
			_ExceptionInfo.Done(this.m_length);
		}

		// Token: 0x060049AA RID: 18858 RVA: 0x0010A330 File Offset: 0x00108530
		public virtual void BeginExceptFilterBlock()
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			Label endLabel = _ExceptionInfo.GetEndLabel();
			this.Emit(OpCodes.Leave, endLabel);
			_ExceptionInfo.MarkFilterAddr(this.m_length);
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x0010A384 File Offset: 0x00108584
		public virtual void BeginCatchBlock(Type exceptionType)
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
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
				Label endLabel = _ExceptionInfo.GetEndLabel();
				this.Emit(OpCodes.Leave, endLabel);
			}
			_ExceptionInfo.MarkCatchAddr(this.m_length, exceptionType);
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x0010A41C File Offset: 0x0010861C
		public virtual void BeginFaultBlock()
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			Label endLabel = _ExceptionInfo.GetEndLabel();
			this.Emit(OpCodes.Leave, endLabel);
			_ExceptionInfo.MarkFaultAddr(this.m_length);
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x0010A470 File Offset: 0x00108670
		public virtual void BeginFinallyBlock()
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			int currentState = _ExceptionInfo.GetCurrentState();
			Label endLabel = _ExceptionInfo.GetEndLabel();
			int num = 0;
			if (currentState != 0)
			{
				this.Emit(OpCodes.Leave, endLabel);
				num = this.m_length;
			}
			this.MarkLabel(endLabel);
			Label label = this.DefineLabel();
			_ExceptionInfo.SetFinallyEndLabel(label);
			this.Emit(OpCodes.Leave, label);
			if (num == 0)
			{
				num = this.m_length;
			}
			_ExceptionInfo.MarkFinallyAddr(this.m_length, num);
		}

		// Token: 0x060049AE RID: 18862 RVA: 0x0010A508 File Offset: 0x00108708
		public virtual Label DefineLabel()
		{
			if (this.m_labelList == null)
			{
				this.m_labelList = new int[4];
			}
			if (this.m_labelCount >= this.m_labelList.Length)
			{
				this.m_labelList = ILGenerator.EnlargeArray(this.m_labelList);
			}
			this.m_labelList[this.m_labelCount] = -1;
			int labelCount = this.m_labelCount;
			this.m_labelCount = labelCount + 1;
			return new Label(labelCount);
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x0010A570 File Offset: 0x00108770
		public virtual void MarkLabel(Label loc)
		{
			int labelValue = loc.GetLabelValue();
			if (labelValue < 0 || labelValue >= this.m_labelList.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidLabel"));
			}
			if (this.m_labelList[labelValue] != -1)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_RedefinedLabel"));
			}
			this.m_labelList[labelValue] = this.m_length;
		}

		// Token: 0x060049B0 RID: 18864 RVA: 0x0010A5D0 File Offset: 0x001087D0
		public virtual void ThrowException(Type excType)
		{
			if (excType == null)
			{
				throw new ArgumentNullException("excType");
			}
			if (!excType.IsSubclassOf(typeof(Exception)) && excType != typeof(Exception))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotExceptionType"));
			}
			ConstructorInfo constructor = excType.GetConstructor(Type.EmptyTypes);
			if (constructor == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MissingDefaultConstructor"));
			}
			this.Emit(OpCodes.Newobj, constructor);
			this.Emit(OpCodes.Throw);
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x0010A664 File Offset: 0x00108864
		public virtual void EmitWriteLine(string value)
		{
			this.Emit(OpCodes.Ldstr, value);
			Type[] types = new Type[]
			{
				typeof(string)
			};
			MethodInfo method = typeof(Console).GetMethod("WriteLine", types);
			this.Emit(OpCodes.Call, method);
		}

		// Token: 0x060049B2 RID: 18866 RVA: 0x0010A6B4 File Offset: 0x001088B4
		public virtual void EmitWriteLine(LocalBuilder localBuilder)
		{
			if (this.m_methodBuilder == null)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_BadILGeneratorUsage"));
			}
			MethodInfo method = typeof(Console).GetMethod("get_Out");
			this.Emit(OpCodes.Call, method);
			this.Emit(OpCodes.Ldloc, localBuilder);
			Type[] array = new Type[1];
			object localType = localBuilder.LocalType;
			if (localType is TypeBuilder || localType is EnumBuilder)
			{
				throw new ArgumentException(Environment.GetResourceString("NotSupported_OutputStreamUsingTypeBuilder"));
			}
			array[0] = (Type)localType;
			MethodInfo method2 = typeof(TextWriter).GetMethod("WriteLine", array);
			if (method2 == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmitWriteLineType"), "localBuilder");
			}
			this.Emit(OpCodes.Callvirt, method2);
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x0010A784 File Offset: 0x00108984
		public virtual void EmitWriteLine(FieldInfo fld)
		{
			if (fld == null)
			{
				throw new ArgumentNullException("fld");
			}
			MethodInfo method = typeof(Console).GetMethod("get_Out");
			this.Emit(OpCodes.Call, method);
			if ((fld.Attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
			{
				this.Emit(OpCodes.Ldsfld, fld);
			}
			else
			{
				this.Emit(OpCodes.Ldarg, 0);
				this.Emit(OpCodes.Ldfld, fld);
			}
			Type[] array = new Type[1];
			object fieldType = fld.FieldType;
			if (fieldType is TypeBuilder || fieldType is EnumBuilder)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_OutputStreamUsingTypeBuilder"));
			}
			array[0] = (Type)fieldType;
			MethodInfo method2 = typeof(TextWriter).GetMethod("WriteLine", array);
			if (method2 == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmitWriteLineType"), "fld");
			}
			this.Emit(OpCodes.Callvirt, method2);
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x0010A86E File Offset: 0x00108A6E
		public virtual LocalBuilder DeclareLocal(Type localType)
		{
			return this.DeclareLocal(localType, false);
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x0010A878 File Offset: 0x00108A78
		public virtual LocalBuilder DeclareLocal(Type localType, bool pinned)
		{
			MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
			if (methodBuilder == null)
			{
				throw new NotSupportedException();
			}
			if (methodBuilder.IsTypeCreated())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
			}
			if (localType == null)
			{
				throw new ArgumentNullException("localType");
			}
			if (methodBuilder.m_bIsBaked)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBaked"));
			}
			this.m_localSignature.AddArgument(localType, pinned);
			LocalBuilder result = new LocalBuilder(this.m_localCount, localType, methodBuilder, pinned);
			this.m_localCount++;
			return result;
		}

		// Token: 0x060049B6 RID: 18870 RVA: 0x0010A910 File Offset: 0x00108B10
		public virtual void UsingNamespace(string usingNamespace)
		{
			if (usingNamespace == null)
			{
				throw new ArgumentNullException("usingNamespace");
			}
			if (usingNamespace.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "usingNamespace");
			}
			MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
			if (methodBuilder == null)
			{
				throw new NotSupportedException();
			}
			int currentActiveScopeIndex = methodBuilder.GetILGenerator().m_ScopeTree.GetCurrentActiveScopeIndex();
			if (currentActiveScopeIndex == -1)
			{
				methodBuilder.m_localSymInfo.AddUsingNamespace(usingNamespace);
				return;
			}
			this.m_ScopeTree.AddUsingNamespaceToCurrentScope(usingNamespace);
		}

		// Token: 0x060049B7 RID: 18871 RVA: 0x0010A991 File Offset: 0x00108B91
		public virtual void MarkSequencePoint(ISymbolDocumentWriter document, int startLine, int startColumn, int endLine, int endColumn)
		{
			if (startLine == 0 || startLine < 0 || endLine == 0 || endLine < 0)
			{
				throw new ArgumentOutOfRangeException("startLine");
			}
			this.m_LineNumberInfo.AddLineNumberInfo(document, this.m_length, startLine, startColumn, endLine, endColumn);
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x0010A9C6 File Offset: 0x00108BC6
		public virtual void BeginScope()
		{
			this.m_ScopeTree.AddScopeInfo(ScopeAction.Open, this.m_length);
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x0010A9DA File Offset: 0x00108BDA
		public virtual void EndScope()
		{
			this.m_ScopeTree.AddScopeInfo(ScopeAction.Close, this.m_length);
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060049BA RID: 18874 RVA: 0x0010A9EE File Offset: 0x00108BEE
		public virtual int ILOffset
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x060049BB RID: 18875 RVA: 0x0010A9F6 File Offset: 0x00108BF6
		void _ILGenerator.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x0010A9FD File Offset: 0x00108BFD
		void _ILGenerator.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x0010AA04 File Offset: 0x00108C04
		void _ILGenerator.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x0010AA0B File Offset: 0x00108C0B
		void _ILGenerator.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001E03 RID: 7683
		private const int defaultSize = 16;

		// Token: 0x04001E04 RID: 7684
		private const int DefaultFixupArraySize = 8;

		// Token: 0x04001E05 RID: 7685
		private const int DefaultLabelArraySize = 4;

		// Token: 0x04001E06 RID: 7686
		private const int DefaultExceptionArraySize = 2;

		// Token: 0x04001E07 RID: 7687
		private int m_length;

		// Token: 0x04001E08 RID: 7688
		private byte[] m_ILStream;

		// Token: 0x04001E09 RID: 7689
		private int[] m_labelList;

		// Token: 0x04001E0A RID: 7690
		private int m_labelCount;

		// Token: 0x04001E0B RID: 7691
		private __FixupData[] m_fixupData;

		// Token: 0x04001E0C RID: 7692
		private int m_fixupCount;

		// Token: 0x04001E0D RID: 7693
		private int[] m_RelocFixupList;

		// Token: 0x04001E0E RID: 7694
		private int m_RelocFixupCount;

		// Token: 0x04001E0F RID: 7695
		private int m_exceptionCount;

		// Token: 0x04001E10 RID: 7696
		private int m_currExcStackCount;

		// Token: 0x04001E11 RID: 7697
		private __ExceptionInfo[] m_exceptions;

		// Token: 0x04001E12 RID: 7698
		private __ExceptionInfo[] m_currExcStack;

		// Token: 0x04001E13 RID: 7699
		internal ScopeTree m_ScopeTree;

		// Token: 0x04001E14 RID: 7700
		internal LineNumberInfo m_LineNumberInfo;

		// Token: 0x04001E15 RID: 7701
		internal MethodInfo m_methodBuilder;

		// Token: 0x04001E16 RID: 7702
		internal int m_localCount;

		// Token: 0x04001E17 RID: 7703
		internal SignatureHelper m_localSignature;

		// Token: 0x04001E18 RID: 7704
		private int m_maxStackSize;

		// Token: 0x04001E19 RID: 7705
		private int m_maxMidStack;

		// Token: 0x04001E1A RID: 7706
		private int m_maxMidStackCur;
	}
}
