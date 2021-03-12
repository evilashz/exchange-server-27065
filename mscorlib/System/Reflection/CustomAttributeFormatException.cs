using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020005A1 RID: 1441
	[ComVisible(true)]
	[Serializable]
	public class CustomAttributeFormatException : FormatException
	{
		// Token: 0x060043BC RID: 17340 RVA: 0x000F887C File Offset: 0x000F6A7C
		public CustomAttributeFormatException() : base(Environment.GetResourceString("Arg_CustomAttributeFormatException"))
		{
			base.SetErrorCode(-2146232827);
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x000F8899 File Offset: 0x000F6A99
		public CustomAttributeFormatException(string message) : base(message)
		{
			base.SetErrorCode(-2146232827);
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x000F88AD File Offset: 0x000F6AAD
		public CustomAttributeFormatException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146232827);
		}

		// Token: 0x060043BF RID: 17343 RVA: 0x000F88C2 File Offset: 0x000F6AC2
		protected CustomAttributeFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
