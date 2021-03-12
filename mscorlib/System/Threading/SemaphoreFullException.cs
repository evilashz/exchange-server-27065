using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020004E4 RID: 1252
	[ComVisible(false)]
	[TypeForwardedFrom("System, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	[__DynamicallyInvokable]
	[Serializable]
	public class SemaphoreFullException : SystemException
	{
		// Token: 0x06003BF0 RID: 15344 RVA: 0x000E16BB File Offset: 0x000DF8BB
		[__DynamicallyInvokable]
		public SemaphoreFullException() : base(Environment.GetResourceString("Threading_SemaphoreFullException"))
		{
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x000E16CD File Offset: 0x000DF8CD
		[__DynamicallyInvokable]
		public SemaphoreFullException(string message) : base(message)
		{
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x000E16D6 File Offset: 0x000DF8D6
		[__DynamicallyInvokable]
		public SemaphoreFullException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x000E16E0 File Offset: 0x000DF8E0
		protected SemaphoreFullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
