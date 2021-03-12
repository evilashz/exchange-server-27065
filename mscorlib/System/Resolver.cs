using System;
using System.Reflection;
using System.Security;
using System.Threading;

namespace System
{
	// Token: 0x0200013B RID: 315
	internal abstract class Resolver
	{
		// Token: 0x060012DD RID: 4829
		internal abstract RuntimeType GetJitContext(ref int securityControlFlags);

		// Token: 0x060012DE RID: 4830
		internal abstract byte[] GetCodeInfo(ref int stackSize, ref int initLocals, ref int EHCount);

		// Token: 0x060012DF RID: 4831
		internal abstract byte[] GetLocalsSignature();

		// Token: 0x060012E0 RID: 4832
		[SecurityCritical]
		internal unsafe abstract void GetEHInfo(int EHNumber, void* exception);

		// Token: 0x060012E1 RID: 4833
		internal abstract byte[] GetRawEHInfo();

		// Token: 0x060012E2 RID: 4834
		internal abstract string GetStringLiteral(int token);

		// Token: 0x060012E3 RID: 4835
		[SecurityCritical]
		internal abstract void ResolveToken(int token, out IntPtr typeHandle, out IntPtr methodHandle, out IntPtr fieldHandle);

		// Token: 0x060012E4 RID: 4836
		internal abstract byte[] ResolveSignature(int token, int fromMethod);

		// Token: 0x060012E5 RID: 4837
		internal abstract MethodInfo GetDynamicMethod();

		// Token: 0x060012E6 RID: 4838
		internal abstract CompressedStack GetSecurityContext();

		// Token: 0x02000ACE RID: 2766
		internal struct CORINFO_EH_CLAUSE
		{
			// Token: 0x04003126 RID: 12582
			internal int Flags;

			// Token: 0x04003127 RID: 12583
			internal int TryOffset;

			// Token: 0x04003128 RID: 12584
			internal int TryLength;

			// Token: 0x04003129 RID: 12585
			internal int HandlerOffset;

			// Token: 0x0400312A RID: 12586
			internal int HandlerLength;

			// Token: 0x0400312B RID: 12587
			internal int ClassTokenOrFilterOffset;
		}
	}
}
