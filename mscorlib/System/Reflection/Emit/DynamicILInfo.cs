using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x02000606 RID: 1542
	[ComVisible(true)]
	public class DynamicILInfo
	{
		// Token: 0x060048EB RID: 18667 RVA: 0x0010778C File Offset: 0x0010598C
		internal DynamicILInfo(DynamicScope scope, DynamicMethod method, byte[] methodSignature)
		{
			this.m_method = method;
			this.m_scope = scope;
			this.m_methodSignature = this.m_scope.GetTokenFor(methodSignature);
			this.m_exceptions = EmptyArray<byte>.Value;
			this.m_code = EmptyArray<byte>.Value;
			this.m_localSignature = EmptyArray<byte>.Value;
		}

		// Token: 0x060048EC RID: 18668 RVA: 0x001077E0 File Offset: 0x001059E0
		[SecurityCritical]
		internal void GetCallableMethod(RuntimeModule module, DynamicMethod dm)
		{
			dm.m_methodHandle = ModuleHandle.GetDynamicMethod(dm, module, this.m_method.Name, (byte[])this.m_scope[this.m_methodSignature], new DynamicResolver(this));
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060048ED RID: 18669 RVA: 0x00107816 File Offset: 0x00105A16
		internal byte[] LocalSignature
		{
			get
			{
				if (this.m_localSignature == null)
				{
					this.m_localSignature = SignatureHelper.GetLocalVarSigHelper().InternalGetSignatureArray();
				}
				return this.m_localSignature;
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060048EE RID: 18670 RVA: 0x00107836 File Offset: 0x00105A36
		internal byte[] Exceptions
		{
			get
			{
				return this.m_exceptions;
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060048EF RID: 18671 RVA: 0x0010783E File Offset: 0x00105A3E
		internal byte[] Code
		{
			get
			{
				return this.m_code;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060048F0 RID: 18672 RVA: 0x00107846 File Offset: 0x00105A46
		internal int MaxStackSize
		{
			get
			{
				return this.m_maxStackSize;
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060048F1 RID: 18673 RVA: 0x0010784E File Offset: 0x00105A4E
		public DynamicMethod DynamicMethod
		{
			get
			{
				return this.m_method;
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060048F2 RID: 18674 RVA: 0x00107856 File Offset: 0x00105A56
		internal DynamicScope DynamicScope
		{
			get
			{
				return this.m_scope;
			}
		}

		// Token: 0x060048F3 RID: 18675 RVA: 0x0010785E File Offset: 0x00105A5E
		public void SetCode(byte[] code, int maxStackSize)
		{
			this.m_code = ((code != null) ? ((byte[])code.Clone()) : EmptyArray<byte>.Value);
			this.m_maxStackSize = maxStackSize;
		}

		// Token: 0x060048F4 RID: 18676 RVA: 0x00107884 File Offset: 0x00105A84
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe void SetCode(byte* code, int codeSize, int maxStackSize)
		{
			if (codeSize < 0)
			{
				throw new ArgumentOutOfRangeException("codeSize", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (codeSize > 0 && code == null)
			{
				throw new ArgumentNullException("code");
			}
			this.m_code = new byte[codeSize];
			for (int i = 0; i < codeSize; i++)
			{
				this.m_code[i] = *code;
				code++;
			}
			this.m_maxStackSize = maxStackSize;
		}

		// Token: 0x060048F5 RID: 18677 RVA: 0x001078EC File Offset: 0x00105AEC
		public void SetExceptions(byte[] exceptions)
		{
			this.m_exceptions = ((exceptions != null) ? ((byte[])exceptions.Clone()) : EmptyArray<byte>.Value);
		}

		// Token: 0x060048F6 RID: 18678 RVA: 0x0010790C File Offset: 0x00105B0C
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe void SetExceptions(byte* exceptions, int exceptionsSize)
		{
			if (exceptionsSize < 0)
			{
				throw new ArgumentOutOfRangeException("exceptionsSize", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (exceptionsSize > 0 && exceptions == null)
			{
				throw new ArgumentNullException("exceptions");
			}
			this.m_exceptions = new byte[exceptionsSize];
			for (int i = 0; i < exceptionsSize; i++)
			{
				this.m_exceptions[i] = *exceptions;
				exceptions++;
			}
		}

		// Token: 0x060048F7 RID: 18679 RVA: 0x0010796D File Offset: 0x00105B6D
		public void SetLocalSignature(byte[] localSignature)
		{
			this.m_localSignature = ((localSignature != null) ? ((byte[])localSignature.Clone()) : EmptyArray<byte>.Value);
		}

		// Token: 0x060048F8 RID: 18680 RVA: 0x0010798C File Offset: 0x00105B8C
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe void SetLocalSignature(byte* localSignature, int signatureSize)
		{
			if (signatureSize < 0)
			{
				throw new ArgumentOutOfRangeException("signatureSize", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (signatureSize > 0 && localSignature == null)
			{
				throw new ArgumentNullException("localSignature");
			}
			this.m_localSignature = new byte[signatureSize];
			for (int i = 0; i < signatureSize; i++)
			{
				this.m_localSignature[i] = *localSignature;
				localSignature++;
			}
		}

		// Token: 0x060048F9 RID: 18681 RVA: 0x001079ED File Offset: 0x00105BED
		[SecuritySafeCritical]
		public int GetTokenFor(RuntimeMethodHandle method)
		{
			return this.DynamicScope.GetTokenFor(method);
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x001079FB File Offset: 0x00105BFB
		public int GetTokenFor(DynamicMethod method)
		{
			return this.DynamicScope.GetTokenFor(method);
		}

		// Token: 0x060048FB RID: 18683 RVA: 0x00107A09 File Offset: 0x00105C09
		public int GetTokenFor(RuntimeMethodHandle method, RuntimeTypeHandle contextType)
		{
			return this.DynamicScope.GetTokenFor(method, contextType);
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x00107A18 File Offset: 0x00105C18
		public int GetTokenFor(RuntimeFieldHandle field)
		{
			return this.DynamicScope.GetTokenFor(field);
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x00107A26 File Offset: 0x00105C26
		public int GetTokenFor(RuntimeFieldHandle field, RuntimeTypeHandle contextType)
		{
			return this.DynamicScope.GetTokenFor(field, contextType);
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x00107A35 File Offset: 0x00105C35
		public int GetTokenFor(RuntimeTypeHandle type)
		{
			return this.DynamicScope.GetTokenFor(type);
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x00107A43 File Offset: 0x00105C43
		public int GetTokenFor(string literal)
		{
			return this.DynamicScope.GetTokenFor(literal);
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x00107A51 File Offset: 0x00105C51
		public int GetTokenFor(byte[] signature)
		{
			return this.DynamicScope.GetTokenFor(signature);
		}

		// Token: 0x04001DD3 RID: 7635
		private DynamicMethod m_method;

		// Token: 0x04001DD4 RID: 7636
		private DynamicScope m_scope;

		// Token: 0x04001DD5 RID: 7637
		private byte[] m_exceptions;

		// Token: 0x04001DD6 RID: 7638
		private byte[] m_code;

		// Token: 0x04001DD7 RID: 7639
		private byte[] m_localSignature;

		// Token: 0x04001DD8 RID: 7640
		private int m_maxStackSize;

		// Token: 0x04001DD9 RID: 7641
		private int m_methodSignature;
	}
}
