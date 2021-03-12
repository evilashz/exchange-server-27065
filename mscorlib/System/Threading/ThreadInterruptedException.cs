using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020004EC RID: 1260
	[ComVisible(true)]
	[Serializable]
	public class ThreadInterruptedException : SystemException
	{
		// Token: 0x06003C9C RID: 15516 RVA: 0x000E23B2 File Offset: 0x000E05B2
		public ThreadInterruptedException() : base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.ThreadInterrupted))
		{
			base.SetErrorCode(-2146233063);
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x000E23CB File Offset: 0x000E05CB
		public ThreadInterruptedException(string message) : base(message)
		{
			base.SetErrorCode(-2146233063);
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x000E23DF File Offset: 0x000E05DF
		public ThreadInterruptedException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233063);
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x000E23F4 File Offset: 0x000E05F4
		protected ThreadInterruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
