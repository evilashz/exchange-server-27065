using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000936 RID: 2358
	[ComVisible(true)]
	public sealed class ExtensibleClassFactory
	{
		// Token: 0x06006107 RID: 24839 RVA: 0x0014AC4D File Offset: 0x00148E4D
		private ExtensibleClassFactory()
		{
		}

		// Token: 0x06006108 RID: 24840
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RegisterObjectCreationCallback(ObjectCreationDelegate callback);
	}
}
