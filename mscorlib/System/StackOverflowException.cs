using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000082 RID: 130
	[ComVisible(true)]
	[Serializable]
	public sealed class StackOverflowException : SystemException
	{
		// Token: 0x060006CB RID: 1739 RVA: 0x000178E2 File Offset: 0x00015AE2
		public StackOverflowException() : base(Environment.GetResourceString("Arg_StackOverflowException"))
		{
			base.SetErrorCode(-2147023895);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000178FF File Offset: 0x00015AFF
		public StackOverflowException(string message) : base(message)
		{
			base.SetErrorCode(-2147023895);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00017913 File Offset: 0x00015B13
		public StackOverflowException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147023895);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00017928 File Offset: 0x00015B28
		internal StackOverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
