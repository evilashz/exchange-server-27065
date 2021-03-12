using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020004FD RID: 1277
	[ComVisible(true)]
	[Serializable]
	public class ThreadStateException : SystemException
	{
		// Token: 0x06003D09 RID: 15625 RVA: 0x000E31D0 File Offset: 0x000E13D0
		public ThreadStateException() : base(Environment.GetResourceString("Arg_ThreadStateException"))
		{
			base.SetErrorCode(-2146233056);
		}

		// Token: 0x06003D0A RID: 15626 RVA: 0x000E31ED File Offset: 0x000E13ED
		public ThreadStateException(string message) : base(message)
		{
			base.SetErrorCode(-2146233056);
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x000E3201 File Offset: 0x000E1401
		public ThreadStateException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233056);
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x000E3216 File Offset: 0x000E1416
		protected ThreadStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
