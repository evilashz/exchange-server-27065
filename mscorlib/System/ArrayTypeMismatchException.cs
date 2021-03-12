using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000AB RID: 171
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ArrayTypeMismatchException : SystemException
	{
		// Token: 0x060009C2 RID: 2498 RVA: 0x0001F6F2 File Offset: 0x0001D8F2
		[__DynamicallyInvokable]
		public ArrayTypeMismatchException() : base(Environment.GetResourceString("Arg_ArrayTypeMismatchException"))
		{
			base.SetErrorCode(-2146233085);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0001F70F File Offset: 0x0001D90F
		[__DynamicallyInvokable]
		public ArrayTypeMismatchException(string message) : base(message)
		{
			base.SetErrorCode(-2146233085);
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0001F723 File Offset: 0x0001D923
		[__DynamicallyInvokable]
		public ArrayTypeMismatchException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233085);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0001F738 File Offset: 0x0001D938
		protected ArrayTypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
