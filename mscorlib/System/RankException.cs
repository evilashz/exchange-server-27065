using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000127 RID: 295
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class RankException : SystemException
	{
		// Token: 0x06001101 RID: 4353 RVA: 0x0003325D File Offset: 0x0003145D
		[__DynamicallyInvokable]
		public RankException() : base(Environment.GetResourceString("Arg_RankException"))
		{
			base.SetErrorCode(-2146233065);
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0003327A File Offset: 0x0003147A
		[__DynamicallyInvokable]
		public RankException(string message) : base(message)
		{
			base.SetErrorCode(-2146233065);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0003328E File Offset: 0x0003148E
		[__DynamicallyInvokable]
		public RankException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233065);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x000332A3 File Offset: 0x000314A3
		protected RankException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
