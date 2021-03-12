using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000F5 RID: 245
	[Serializable]
	public sealed class InsufficientMemoryException : OutOfMemoryException
	{
		// Token: 0x06000F09 RID: 3849 RVA: 0x0002ED65 File Offset: 0x0002CF65
		public InsufficientMemoryException() : base(Exception.GetMessageFromNativeResources(Exception.ExceptionMessageKind.OutOfMemory))
		{
			base.SetErrorCode(-2146233027);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0002ED7E File Offset: 0x0002CF7E
		public InsufficientMemoryException(string message) : base(message)
		{
			base.SetErrorCode(-2146233027);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0002ED92 File Offset: 0x0002CF92
		public InsufficientMemoryException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233027);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0002EDA7 File Offset: 0x0002CFA7
		private InsufficientMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
