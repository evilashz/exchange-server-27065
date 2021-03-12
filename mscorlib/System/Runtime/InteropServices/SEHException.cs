using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000928 RID: 2344
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class SEHException : ExternalException
	{
		// Token: 0x060060A1 RID: 24737 RVA: 0x00149B48 File Offset: 0x00147D48
		[__DynamicallyInvokable]
		public SEHException()
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x060060A2 RID: 24738 RVA: 0x00149B5B File Offset: 0x00147D5B
		[__DynamicallyInvokable]
		public SEHException(string message) : base(message)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x060060A3 RID: 24739 RVA: 0x00149B6F File Offset: 0x00147D6F
		[__DynamicallyInvokable]
		public SEHException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x060060A4 RID: 24740 RVA: 0x00149B84 File Offset: 0x00147D84
		protected SEHException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060060A5 RID: 24741 RVA: 0x00149B8E File Offset: 0x00147D8E
		[__DynamicallyInvokable]
		public virtual bool CanResume()
		{
			return false;
		}
	}
}
