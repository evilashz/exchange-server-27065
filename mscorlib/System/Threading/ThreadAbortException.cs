using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004EB RID: 1259
	[ComVisible(true)]
	[Serializable]
	public sealed class ThreadAbortException : SystemException
	{
		// Token: 0x06003C99 RID: 15513 RVA: 0x000E2383 File Offset: 0x000E0583
		internal ThreadAbortException() : base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.ThreadAbort))
		{
			base.SetErrorCode(-2146233040);
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x000E239C File Offset: 0x000E059C
		internal ThreadAbortException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06003C9B RID: 15515 RVA: 0x000E23A6 File Offset: 0x000E05A6
		public object ExceptionState
		{
			[SecuritySafeCritical]
			get
			{
				return Thread.CurrentThread.AbortReason;
			}
		}
	}
}
