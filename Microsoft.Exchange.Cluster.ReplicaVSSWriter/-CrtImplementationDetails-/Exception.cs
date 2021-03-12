using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200022A RID: 554
	[Serializable]
	internal class Exception : Exception
	{
		// Token: 0x0600011A RID: 282 RVA: 0x0000DBF0 File Offset: 0x0000CFF0
		protected Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000DBD8 File Offset: 0x0000CFD8
		public Exception(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000DBC4 File Offset: 0x0000CFC4
		public Exception(string message) : base(message)
		{
		}
	}
}
