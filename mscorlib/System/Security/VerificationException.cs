using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security
{
	// Token: 0x020001F6 RID: 502
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class VerificationException : SystemException
	{
		// Token: 0x06001E3D RID: 7741 RVA: 0x00069A92 File Offset: 0x00067C92
		[__DynamicallyInvokable]
		public VerificationException() : base(Environment.GetResourceString("Verification_Exception"))
		{
			base.SetErrorCode(-2146233075);
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x00069AAF File Offset: 0x00067CAF
		[__DynamicallyInvokable]
		public VerificationException(string message) : base(message)
		{
			base.SetErrorCode(-2146233075);
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x00069AC3 File Offset: 0x00067CC3
		[__DynamicallyInvokable]
		public VerificationException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233075);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x00069AD8 File Offset: 0x00067CD8
		protected VerificationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
