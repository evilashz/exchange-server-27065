using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000BF RID: 191
	[ComVisible(true)]
	[Serializable]
	public class TypeUnloadedException : SystemException
	{
		// Token: 0x06000AFD RID: 2813 RVA: 0x00022B6A File Offset: 0x00020D6A
		public TypeUnloadedException() : base(Environment.GetResourceString("Arg_TypeUnloadedException"))
		{
			base.SetErrorCode(-2146234349);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00022B87 File Offset: 0x00020D87
		public TypeUnloadedException(string message) : base(message)
		{
			base.SetErrorCode(-2146234349);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00022B9B File Offset: 0x00020D9B
		public TypeUnloadedException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146234349);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00022BB0 File Offset: 0x00020DB0
		protected TypeUnloadedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
