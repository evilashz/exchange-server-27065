using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000B5 RID: 181
	[ComVisible(true)]
	[Serializable]
	public class CannotUnloadAppDomainException : SystemException
	{
		// Token: 0x06000A82 RID: 2690 RVA: 0x000217B9 File Offset: 0x0001F9B9
		public CannotUnloadAppDomainException() : base(Environment.GetResourceString("Arg_CannotUnloadAppDomainException"))
		{
			base.SetErrorCode(-2146234347);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x000217D6 File Offset: 0x0001F9D6
		public CannotUnloadAppDomainException(string message) : base(message)
		{
			base.SetErrorCode(-2146234347);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x000217EA File Offset: 0x0001F9EA
		public CannotUnloadAppDomainException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146234347);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x000217FF File Offset: 0x0001F9FF
		protected CannotUnloadAppDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
