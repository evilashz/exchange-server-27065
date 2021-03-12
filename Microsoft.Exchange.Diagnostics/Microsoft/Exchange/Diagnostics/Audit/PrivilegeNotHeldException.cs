using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Diagnostics.Audit
{
	// Token: 0x0200019B RID: 411
	public class PrivilegeNotHeldException : Exception
	{
		// Token: 0x06000B6B RID: 2923 RVA: 0x000299F1 File Offset: 0x00027BF1
		public PrivilegeNotHeldException(string msg) : base(msg)
		{
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x000299FA File Offset: 0x00027BFA
		public PrivilegeNotHeldException(SerializationInfo info, StreamingContext context)
		{
		}
	}
}
