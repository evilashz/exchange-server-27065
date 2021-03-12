using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x0200013A RID: 314
	internal class Signature
	{
		// Token: 0x060012D2 RID: 4818
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern void GetSignature(void* pCorSig, int cCorSig, RuntimeFieldHandleInternal fieldHandle, IRuntimeMethodInfo methodHandle, RuntimeType declaringType);

		// Token: 0x060012D3 RID: 4819 RVA: 0x00037E6C File Offset: 0x0003606C
		[SecuritySafeCritical]
		public Signature(IRuntimeMethodInfo method, RuntimeType[] arguments, RuntimeType returnType, CallingConventions callingConvention)
		{
			this.m_pMethod = method.Value;
			this.m_arguments = arguments;
			this.m_returnTypeORfieldType = returnType;
			this.m_managedCallingConventionAndArgIteratorFlags = (int)((byte)callingConvention);
			this.GetSignature(null, 0, default(RuntimeFieldHandleInternal), method, null);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00037EB8 File Offset: 0x000360B8
		[SecuritySafeCritical]
		public Signature(IRuntimeMethodInfo methodHandle, RuntimeType declaringType)
		{
			this.GetSignature(null, 0, default(RuntimeFieldHandleInternal), methodHandle, declaringType);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00037EDF File Offset: 0x000360DF
		[SecurityCritical]
		public Signature(IRuntimeFieldInfo fieldHandle, RuntimeType declaringType)
		{
			this.GetSignature(null, 0, fieldHandle.Value, null, declaringType);
			GC.KeepAlive(fieldHandle);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00037F00 File Offset: 0x00036100
		[SecurityCritical]
		public unsafe Signature(void* pCorSig, int cCorSig, RuntimeType declaringType)
		{
			this.GetSignature(pCorSig, cCorSig, default(RuntimeFieldHandleInternal), null, declaringType);
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00037F26 File Offset: 0x00036126
		internal CallingConventions CallingConvention
		{
			get
			{
				return (CallingConventions)((byte)this.m_managedCallingConventionAndArgIteratorFlags);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00037F2F File Offset: 0x0003612F
		internal RuntimeType[] Arguments
		{
			get
			{
				return this.m_arguments;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00037F37 File Offset: 0x00036137
		internal RuntimeType ReturnType
		{
			get
			{
				return this.m_returnTypeORfieldType;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x00037F3F File Offset: 0x0003613F
		internal RuntimeType FieldType
		{
			get
			{
				return this.m_returnTypeORfieldType;
			}
		}

		// Token: 0x060012DB RID: 4827
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CompareSig(Signature sig1, Signature sig2);

		// Token: 0x060012DC RID: 4828
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type[] GetCustomModifiers(int position, bool required);

		// Token: 0x04000677 RID: 1655
		internal RuntimeType[] m_arguments;

		// Token: 0x04000678 RID: 1656
		internal RuntimeType m_declaringType;

		// Token: 0x04000679 RID: 1657
		internal RuntimeType m_returnTypeORfieldType;

		// Token: 0x0400067A RID: 1658
		internal object m_keepalive;

		// Token: 0x0400067B RID: 1659
		[SecurityCritical]
		internal unsafe void* m_sig;

		// Token: 0x0400067C RID: 1660
		internal int m_managedCallingConventionAndArgIteratorFlags;

		// Token: 0x0400067D RID: 1661
		internal int m_nSizeOfArgStack;

		// Token: 0x0400067E RID: 1662
		internal int m_csig;

		// Token: 0x0400067F RID: 1663
		internal RuntimeMethodHandleInternal m_pMethod;

		// Token: 0x02000ACD RID: 2765
		internal enum MdSigCallingConvention : byte
		{
			// Token: 0x04003116 RID: 12566
			Generics = 16,
			// Token: 0x04003117 RID: 12567
			HasThis = 32,
			// Token: 0x04003118 RID: 12568
			ExplicitThis = 64,
			// Token: 0x04003119 RID: 12569
			CallConvMask = 15,
			// Token: 0x0400311A RID: 12570
			Default = 0,
			// Token: 0x0400311B RID: 12571
			C,
			// Token: 0x0400311C RID: 12572
			StdCall,
			// Token: 0x0400311D RID: 12573
			ThisCall,
			// Token: 0x0400311E RID: 12574
			FastCall,
			// Token: 0x0400311F RID: 12575
			Vararg,
			// Token: 0x04003120 RID: 12576
			Field,
			// Token: 0x04003121 RID: 12577
			LocalSig,
			// Token: 0x04003122 RID: 12578
			Property,
			// Token: 0x04003123 RID: 12579
			Unmgd,
			// Token: 0x04003124 RID: 12580
			GenericInst,
			// Token: 0x04003125 RID: 12581
			Max
		}
	}
}
