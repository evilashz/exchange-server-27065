using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x02000508 RID: 1288
	[ComVisible(false)]
	[__DynamicallyInvokable]
	[Serializable]
	public class WaitHandleCannotBeOpenedException : ApplicationException
	{
		// Token: 0x06003D88 RID: 15752 RVA: 0x000E4871 File Offset: 0x000E2A71
		[__DynamicallyInvokable]
		public WaitHandleCannotBeOpenedException() : base(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException"))
		{
			base.SetErrorCode(-2146233044);
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x000E488E File Offset: 0x000E2A8E
		[__DynamicallyInvokable]
		public WaitHandleCannotBeOpenedException(string message) : base(message)
		{
			base.SetErrorCode(-2146233044);
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x000E48A2 File Offset: 0x000E2AA2
		[__DynamicallyInvokable]
		public WaitHandleCannotBeOpenedException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233044);
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x000E48B7 File Offset: 0x000E2AB7
		protected WaitHandleCannotBeOpenedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
