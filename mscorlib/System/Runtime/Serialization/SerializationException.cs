using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000712 RID: 1810
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class SerializationException : SystemException
	{
		// Token: 0x060050B4 RID: 20660 RVA: 0x0011B3AF File Offset: 0x001195AF
		[__DynamicallyInvokable]
		public SerializationException() : base(SerializationException._nullMessage)
		{
			base.SetErrorCode(-2146233076);
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x0011B3C7 File Offset: 0x001195C7
		[__DynamicallyInvokable]
		public SerializationException(string message) : base(message)
		{
			base.SetErrorCode(-2146233076);
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x0011B3DB File Offset: 0x001195DB
		[__DynamicallyInvokable]
		public SerializationException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233076);
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x0011B3F0 File Offset: 0x001195F0
		protected SerializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04002389 RID: 9097
		private static string _nullMessage = Environment.GetResourceString("Arg_SerializationException");
	}
}
