using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000105 RID: 261
	[Serializable]
	internal class Exception : Exception
	{
		// Token: 0x06000088 RID: 136 RVA: 0x0000340C File Offset: 0x0000280C
		protected Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000033F4 File Offset: 0x000027F4
		public Exception(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000033E0 File Offset: 0x000027E0
		public Exception(string message) : base(message)
		{
		}
	}
}
