using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000090 RID: 144
	[ComVisible(true)]
	[Serializable]
	public class ApplicationException : Exception
	{
		// Token: 0x0600076D RID: 1901 RVA: 0x00019F2B File Offset: 0x0001812B
		public ApplicationException() : base(Environment.GetResourceString("Arg_ApplicationException"))
		{
			base.SetErrorCode(-2146232832);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00019F48 File Offset: 0x00018148
		public ApplicationException(string message) : base(message)
		{
			base.SetErrorCode(-2146232832);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00019F5C File Offset: 0x0001815C
		public ApplicationException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146232832);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00019F71 File Offset: 0x00018171
		protected ApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
