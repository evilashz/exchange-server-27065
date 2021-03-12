using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D2 RID: 2514
	internal abstract class RuntimeClass : __ComObject
	{
		// Token: 0x060063D8 RID: 25560
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetRedirectedGetHashCodeMD();

		// Token: 0x060063D9 RID: 25561
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int RedirectGetHashCode(IntPtr pMD);

		// Token: 0x060063DA RID: 25562 RVA: 0x001531F8 File Offset: 0x001513F8
		[SecuritySafeCritical]
		public override int GetHashCode()
		{
			IntPtr redirectedGetHashCodeMD = this.GetRedirectedGetHashCodeMD();
			if (redirectedGetHashCodeMD == IntPtr.Zero)
			{
				return base.GetHashCode();
			}
			return this.RedirectGetHashCode(redirectedGetHashCodeMD);
		}

		// Token: 0x060063DB RID: 25563
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetRedirectedToStringMD();

		// Token: 0x060063DC RID: 25564
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string RedirectToString(IntPtr pMD);

		// Token: 0x060063DD RID: 25565 RVA: 0x00153228 File Offset: 0x00151428
		[SecuritySafeCritical]
		public override string ToString()
		{
			IStringable stringable = this as IStringable;
			if (stringable != null)
			{
				return stringable.ToString();
			}
			IntPtr redirectedToStringMD = this.GetRedirectedToStringMD();
			if (redirectedToStringMD == IntPtr.Zero)
			{
				return base.ToString();
			}
			return this.RedirectToString(redirectedToStringMD);
		}

		// Token: 0x060063DE RID: 25566
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetRedirectedEqualsMD();

		// Token: 0x060063DF RID: 25567
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool RedirectEquals(object obj, IntPtr pMD);

		// Token: 0x060063E0 RID: 25568 RVA: 0x00153268 File Offset: 0x00151468
		[SecuritySafeCritical]
		public override bool Equals(object obj)
		{
			IntPtr redirectedEqualsMD = this.GetRedirectedEqualsMD();
			if (redirectedEqualsMD == IntPtr.Zero)
			{
				return base.Equals(obj);
			}
			return this.RedirectEquals(obj, redirectedEqualsMD);
		}
	}
}
