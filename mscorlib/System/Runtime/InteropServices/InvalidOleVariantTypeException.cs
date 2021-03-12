using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000920 RID: 2336
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class InvalidOleVariantTypeException : SystemException
	{
		// Token: 0x06005FA1 RID: 24481 RVA: 0x00147F6D File Offset: 0x0014616D
		[__DynamicallyInvokable]
		public InvalidOleVariantTypeException() : base(Environment.GetResourceString("Arg_InvalidOleVariantTypeException"))
		{
			base.SetErrorCode(-2146233039);
		}

		// Token: 0x06005FA2 RID: 24482 RVA: 0x00147F8A File Offset: 0x0014618A
		[__DynamicallyInvokable]
		public InvalidOleVariantTypeException(string message) : base(message)
		{
			base.SetErrorCode(-2146233039);
		}

		// Token: 0x06005FA3 RID: 24483 RVA: 0x00147F9E File Offset: 0x0014619E
		[__DynamicallyInvokable]
		public InvalidOleVariantTypeException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233039);
		}

		// Token: 0x06005FA4 RID: 24484 RVA: 0x00147FB3 File Offset: 0x001461B3
		protected InvalidOleVariantTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
