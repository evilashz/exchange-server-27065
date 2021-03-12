using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000081 RID: 129
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class OutOfMemoryException : SystemException
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x00017896 File Offset: 0x00015A96
		[__DynamicallyInvokable]
		public OutOfMemoryException() : base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.OutOfMemory))
		{
			base.SetErrorCode(-2147024882);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x000178AF File Offset: 0x00015AAF
		[__DynamicallyInvokable]
		public OutOfMemoryException(string message) : base(message)
		{
			base.SetErrorCode(-2147024882);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000178C3 File Offset: 0x00015AC3
		[__DynamicallyInvokable]
		public OutOfMemoryException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147024882);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000178D8 File Offset: 0x00015AD8
		protected OutOfMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
