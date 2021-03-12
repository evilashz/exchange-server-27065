using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020004FE RID: 1278
	[Serializable]
	public sealed class ThreadStartException : SystemException
	{
		// Token: 0x06003D0D RID: 15629 RVA: 0x000E3220 File Offset: 0x000E1420
		private ThreadStartException() : base(Environment.GetResourceString("Arg_ThreadStartException"))
		{
			base.SetErrorCode(-2146233051);
		}

		// Token: 0x06003D0E RID: 15630 RVA: 0x000E323D File Offset: 0x000E143D
		private ThreadStartException(Exception reason) : base(Environment.GetResourceString("Arg_ThreadStartException"), reason)
		{
			base.SetErrorCode(-2146233051);
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x000E325B File Offset: 0x000E145B
		internal ThreadStartException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
