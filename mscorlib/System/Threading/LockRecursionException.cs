using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x020004D2 RID: 1234
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class LockRecursionException : Exception
	{
		// Token: 0x06003B3C RID: 15164 RVA: 0x000DF8A9 File Offset: 0x000DDAA9
		[__DynamicallyInvokable]
		public LockRecursionException()
		{
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x000DF8B1 File Offset: 0x000DDAB1
		[__DynamicallyInvokable]
		public LockRecursionException(string message) : base(message)
		{
		}

		// Token: 0x06003B3E RID: 15166 RVA: 0x000DF8BA File Offset: 0x000DDABA
		protected LockRecursionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06003B3F RID: 15167 RVA: 0x000DF8C4 File Offset: 0x000DDAC4
		[__DynamicallyInvokable]
		public LockRecursionException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
